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
    public static class FileIO
    {
        private static BinaryReader reader; //everything in binary files
        private static BinaryWriter writer;

        #region map methods
        public static bool ReadMapFile(string filepath) //will read file and make changes to board. 
        {
            if (!File.Exists("..\\..\\maps\\"+filepath))
                return false; //method just dies if it's a bad path

            reader = new BinaryReader(File.Open("..\\..\\maps\\" + filepath+".dat", FileMode.Open)); //initialize the reader

            Piece[,] board = new Piece[GameVariables.BoardSpaceDim, GameVariables.BoardSpaceDim];
            int x; 
            int y;
            x = reader.ReadInt32();
            GameVariables.BoardSpaceDim = (int)x;
            while ((x = reader.ReadInt32()) != -1) // -1 will be used as the sign to switch from structures to towers
            {
                y = reader.ReadInt32();
                if (x < GameVariables.BoardSpaceDim && x >= 0 && y < GameVariables.BoardSpaceDim && y >= 0)
                   new Structure(x, y); 
                
            }
            while(reader.BaseStream.Position != reader.BaseStream.Length) //until end of file
            {
                x = reader.ReadInt32();
                y = reader.ReadInt32();
                if (x < GameVariables.BoardSpaceDim && x >= 0 && y < GameVariables.BoardSpaceDim && y >= 0)
                    new Tower(x, y); 

            }
            reader.Close();
            return true;
        }
        #endregion

        #region save file handling
        public static void SaveGame(string filepath) //saves game data
        {
            writer = new BinaryWriter(File.Open("..\\..\\savedGames\\"+filepath+".dat", FileMode.Create)); //initialize the reader, it will overwrite or create the file
            for (int y = 0; y < GameVariables.BoardSpaceDim; y++) //cycle through all the array values
            {
                for (int x = 0; x < GameVariables.BoardSpaceDim; x++) 
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

                    //board location
                    writer.Write(x);
                    writer.Write(y);
                }
            }

            writer.Write(-40); //loop's over
            //gameplay data 
            writer.Write(Game1.currentPlayer);
            foreach(Unit movedUnit in Game1.movedUnits)
            {
                writer.Write((int)movedUnit.Location.X);
                writer.Write((int)movedUnit.Location.Y);
                
            }
            writer.Close();
        }

        public static void LoadGame(string filepath) //loads game data
        {
            if (!File.Exists("..\\..\\savedGames\\" + filepath+".dat"))
                return; //method just dies if it's a bad path

            reader = new BinaryReader(File.Open("..\\..\\savedGames\\" + filepath+".dat", FileMode.Open)); //initialize the reader
            int i;
            int x = 0;
            int y = 0;
            Player player = null;
            int ownerID;
            string ownerName;
            while ((i = reader.ReadInt32())!= -40) //flag is -40
            {
                
                if (i != -1) //not a blank space
                {
                    ownerID = reader.ReadInt32();
                    ownerName = reader.ReadString(); //data will come in sets of 3, so these ones should not be null.
                    x = reader.ReadInt32();
                    y = reader.ReadInt32();
                    player = new Player(ownerName, ownerID);
                    if (!GameVariables.players.Contains(player)) //don't want duplicates in the list
                        GameVariables.players.Add(player);
                }
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
            Game1.currentPlayer = reader.ReadInt32();
            Game1.movedUnits.Clear();
            while(reader.BaseStream.Position != reader.BaseStream.Length)
            {
                x = reader.ReadInt32();
                y = reader.ReadInt32();
                Game1.movedUnits.Add((Unit)GameVariables.board[x,y]);
            }
            reader.Close();
        }

        #endregion


    }
}
