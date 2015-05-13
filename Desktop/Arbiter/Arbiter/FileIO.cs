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

            if (!File.Exists("..\\..\\maps\\" + filepath + ".dat"))
                return false; //method just dies if it's a bad path

            reader = new BinaryReader(File.Open("..\\..\\maps\\" + filepath + ".dat", FileMode.Open)); //initialize the reader

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
            while (reader.BaseStream.Position != reader.BaseStream.Length) //until end of file
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
            writer = new BinaryWriter(File.Open("..\\..\\savedGames\\" + filepath + ".dat", FileMode.Create)); //initialize the reader, it will overwrite or create the file
            writer.Write(GameVariables.BoardSpaceDim);
            foreach (Player player in GameVariables.players)
            {
                writer.Write(1); //does fuck all. it's just not -500.
                writer.Write(player.Name);
                writer.Write(player.ID);
                writer.Write(player.VictoryTally);
            }
            writer.Write(-500);
            for (int x = 0; x < GameVariables.BoardSpaceDim; x++) //cycle through all the array values
            {
                for (int y = 0; y < GameVariables.BoardSpaceDim; y++)
                {
                    if (GameVariables.board[x, y] != null)
                    {
                        if (GameVariables.board[x, y].Rank == 1) //two pieces have rank 1
                        {
                            if (GameVariables.board[x, y] is HeavyUnit)
                            {
                                writer.Write(GameVariables.board[x, y].Rank); //writes out rank, then owner id, then owner name
                                writer.Write(GameVariables.board[x, y].owner.ID);

                            }
                            else
                            {
                                writer.Write(4);
                                writer.Write(GameVariables.board[x, y].owner.ID);

                            }
                        }
                        else
                        {
                            writer.Write(GameVariables.board[x, y].Rank);
                            writer.Write(GameVariables.board[x, y].owner.ID);


                        }
                        //board location
                        
                        writer.Write(Game1.movedUnits.Contains(GameVariables.board[x, y]));
                        if (GameVariables.board[x, y] is LightUnit || GameVariables.board[x, y] is StandardUnit) //only types that can claim towers
                        {
                            bool own = false;
                            foreach (Tower tower in GameVariables.towers)
                            {
                                if (tower.claimedBy == GameVariables.board[x, y])
                                {
                                    own = true;

                                    break;
                                }
                            }
                            writer.Write(own);
                        }
                    }
                    else
                        writer.Write(-1); //for blank spaces


                }
            }

            
            //gameplay data 
            writer.Write(Game1.currentPlayer);
            writer.Close();
        }

        public static bool LoadGame(string filepath) //loads game data
        {
            if (!File.Exists("..\\..\\savedGames\\" + filepath + ".dat"))
                return false; //method just dies if it's a bad path

            reader = new BinaryReader(File.Open("..\\..\\savedGames\\" + filepath + ".dat", FileMode.Open)); //initialize the reader
            int i;

            bool moved = false;
            Player player = null;
            int ownerID;

            bool own;
            Game1.movedUnits.Clear();
            GameVariables.players.Clear();

            GameVariables.BoardSpaceDim = reader.ReadInt32();
            while ((i = reader.ReadInt32()) != -500) //flag is -500
            {
                player = new Player(reader.ReadString(), reader.ReadInt32());
                player.VictoryTally = reader.ReadInt32();
                GameVariables.players.Add(player);
            }


            for (int x = 0; x < GameVariables.BoardSpaceDim; x++)
            {
                for (int y = 0; y < GameVariables.BoardSpaceDim; y++)
                {
                    i = reader.ReadInt32();
                    if (i != -1)
                    {
                        ownerID = reader.ReadInt32();
                        //data will come in sets of 3, so these ones should not be null.
                        if (ownerID != -1)
                        {
                            player = GameVariables.players[ownerID];
                        }
                        else
                        {
                            player = GameVariables.players[0];
                        }

                        moved = reader.ReadBoolean();


                        switch (i)
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
                                    HeavyUnit unit = new HeavyUnit(x, y, player);
                                    GameVariables.board[x, y] = unit;
                                    if (moved)
                                        Game1.movedUnits.Add(unit);
                                    break;
                                }
                            case 2: //standard unit
                                {
                                    StandardUnit unit = new StandardUnit(x, y, player);
                                    GameVariables.board[x, y] = unit;
                                    if (moved)
                                        Game1.movedUnits.Add(unit);
                                    own = reader.ReadBoolean();
                                    if (own)
                                    {
                                        Tower newTower = new Tower(x, y);
                                        
                                        newTower.Claim(unit);

                                    }
                                    break;
                                }
                            case 3: //light unit
                                {
                                    LightUnit unit = new LightUnit(x, y, player);
                                    GameVariables.board[x, y] = unit;
                                    if (moved)
                                        Game1.movedUnits.Add(unit);
                                    own = reader.ReadBoolean();
                                    if (own)
                                    {
                                        Tower newTower = new Tower(x, y);
                                        player.TowersOwned.Add(newTower);
                                        newTower.Claim(unit);

                                    }
                                    break;
                                }
                            case 4: //jumper unit
                                {
                                    JumperUnit unit = new JumperUnit(x, y, player);
                                    GameVariables.board[x, y] = unit;
                                    if (moved)
                                        Game1.movedUnits.Add(unit);
                                    break;
                                }
                            case 5: //Tower
                                {
                                    GameVariables.board[x, y] = new Tower(x, y);
                                    break;
                                }
                        }
                    }


                }
            }
                Game1.currentPlayer = reader.ReadInt32();

                reader.Close();
                return true;
            

        #endregion


        }
    }
}
