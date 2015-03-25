using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

// Margaret
namespace Arbiter
{
    /// <summary>
    /// Handles the reading of maps from binary files, as well as reading/writing save games from/to binary files.
    /// </summary>
    public static class FileIO
    {
        private static BinaryReader reader; //everything in binary files
        private static BinaryWriter writer;

        #region map methods

        /// <summary>
        /// Reads and creates a map from a file. Will change board array and tower/structure lists automatically.
        /// Does nothing if file path does not exist.
        /// 
        /// File Format:
        /// int board dimension (square board only needs 1)
        /// int x, int y of structure locations looped until
        /// int -1 to signal switch
        /// int x, int y of tower locations looped until
        /// end of file.
        /// </summary>
        /// <param name="filepath">name of file only. will read from maps directory within the bin folder</param>
        public static void ReadMapFile(string filepath) //will read file and make changes to board. 
        {
            if (!File.Exists("..\\maps\\"+filepath))
                return; //method just dies if it's a bad path

            reader = new BinaryReader(File.Open("..\\maps\\" + filepath, FileMode.Open)); //initialize the reader

            Piece[,] board = new Piece[GameVariables.boardSpaceDim, GameVariables.boardSpaceDim];
            int x;
            int y;
            x = reader.ReadInt32();
            GameVariables.boardSpaceDim = x;
            while ((x = reader.ReadInt32()) != -1) // -1 will be used as the sign to switch from structures to towers
            {
                y = reader.ReadInt32();
                if (x < GameVariables.boardSpaceDim && x >= 0 && y < GameVariables.boardSpaceDim && y >= 0)
                   new Structure(x, y); 
                
            }
            while((x = reader.ReadInt32()) != null) //until end of file
            {
                y = reader.ReadInt32();
                if (x < GameVariables.boardSpaceDim && x >= 0 && y < GameVariables.boardSpaceDim && y >= 0)
                    new Tower(x, y); 

            }
            reader.Close();
        }
        #endregion

        #region save file handling

        /// <summary>
        /// Writes save game to a binary file. Includes all pieces, objects, and owners.
        /// Will include all map details by default.
        /// Will create file if no file exists, overwrite otherwise.
        /// </summary>
        /// <param name="filepath"> should be name only. Saves in default directory </param>
        public static void SaveGame(string filepath) //saves game data
        {
            writer = new BinaryWriter(File.Open(filepath, FileMode.Create)); //initialize the reader, it will overwrite or create the file
            writer.Write(GameVariables.boardSpaceDim);
            for (int y = 0; y < GameVariables.boardSpaceDim; y++) //cycle through all the array values
            {
                for (int x = 0; x < GameVariables.boardSpaceDim; x++) 
                {
                    if (GameVariables.board[x, y] != null)
                    {
                        if (GameVariables.board[x, y].Rank == 1) //two pieces have rank 1
                        {
                            if (GameVariables.board[x, y] is HeavyUnit)
                            {
                                writer.Write(GameVariables.board[x, y].Rank); //writes out rank, then owner id, then owner name
                                writer.Write(GameVariables.board[x, y].owner.ID);
                                writer.Write(GameVariables.board[x, y].owner.Name);
                            }
                            else
                            {
                                writer.Write(4);
                                writer.Write(GameVariables.board[x, y].owner.ID);
                                writer.Write(GameVariables.board[x, y].owner.Name);
                            }
                        }
                        else
                        {
                            writer.Write(GameVariables.board[x, y].Rank);
                            writer.Write(GameVariables.board[x, y].owner.ID);
                            writer.Write(GameVariables.board[x, y].owner.Name);
                        }
                    }
                    else
                        writer.Write(-1); //for blank spaces
                }
            }
            writer.Close();
        }


        /// <summary>
        /// File format is as follows:
        /// int dimension
        /// loops until end of file in sets of 3
        /// int identifier (-1 = blank, 0 = structure, 1 = heavy, 2 = standard, 3 = light, 4 = jumper, 5 = tower)
        /// int ownerID (player #, or -1 for owned by map)
        /// string playerName
        /// 
        /// will have one set of data for each space on the board + the dimension at the beginning.
        /// </summary>
        /// <param name="filepath"> should be same filepath as used to save game</param>
        public static void LoadGame(string filepath) //loads game data
        {
            if (!File.Exists(filepath))
                return; //method just dies if it's a bad path

            reader = new BinaryReader(File.Open(filepath, FileMode.Open)); //initialize the reader
            int i;
            int x = 0;
            int y = 0;
            Player player;
            int ownerID;
            string ownerName;
            GameVariables.boardSpaceDim = reader.ReadInt32(); //gets board dimension
            while((i = reader.ReadInt32()) != null)
            {
                ownerID = reader.ReadInt32();
                ownerName = reader.ReadString(); //data will come in sets of 3, so these ones should not be null.
                player = new Player(ownerName, ownerID);
                if (!GameVariables.players.Contains(player)) //don't want duplicates in the list
                    GameVariables.players.Add(player);
                switch(i)
                {
                    case -1: //empty space
                        {
                            GameVariables.board[x, y] = null;
                            break;
                        }
                    case 0: //structure
                        {
                            GameVariables.board[x, y] = new Structure(x, y); // all of these units need filler content
                            break;
                        }
                    case 1: //heavy unit
                        {

                            GameVariables.board[x,y] = new HeavyUnit(x,y,player);
                            break;
                        }
                    case 2: //standard unit
                        {
                            GameVariables.board[x,y] = new StandardUnit(x,y,player);
                            break;
                        }
                    case 3: //light unit
                        {
                            GameVariables.board[x,y] = new LightUnit(x,y,player);
                            break;
                        }
                    case 4: //jumper unit
                        {
                            GameVariables.board[x,y] = new JumperUnit(x,y,player);
                            break;
                        }
                    case 5: //Tower
                        {
                            GameVariables.board[x,y] = new Tower(x,y);
                            break;
                        }
                }
            }
            reader.Close();
        }

        #endregion


    }
}
