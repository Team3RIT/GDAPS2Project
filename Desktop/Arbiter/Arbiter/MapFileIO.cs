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
        private static BinaryReader reader;
        private static BinaryWriter writer;

        public static void ReadMapFile(string filepath) //will read file and make changes to board. 
        {
            if (!File.Exists(filepath))
                return; //method just dies if it's a bad path
            
            reader = new BinaryReader(File.Open(filepath,FileMode.Open)); //initialize the reader

            Piece[,] board = new Piece[GameVariables.boardSpaceDim, GameVariables.boardSpaceDim];
            int x;
            int y;
            while ((x = reader.ReadInt32()) != -1) // -1 will be used as the sign to switch from structures to towers
            {
                y = reader.ReadInt32();
                if (x < GameVariables.boardSpaceDim && x >= 0 && y < GameVariables.boardSpaceDim && y >= 0)
                new Structure(x, y, null); //this null thing needs to be switched out with a structure texture. As it is it will mess things up.
                
            }
            while((x = reader.ReadInt32()) != null) //until end of file
            {
                y = reader.ReadInt32();
                if (x < GameVariables.boardSpaceDim && x >= 0 && y < GameVariables.boardSpaceDim && y >= 0)
                    new Tower(x, y, null); //same deal with the tower texture

            }
        }

        public static void SaveGame(string filepath) //saves game data
        {
            writer = new BinaryWriter(File.Open(filepath, FileMode.Create)); //initialize the reader, it will overwrite or create the file
            for (int y = 0; y < GameVariables.boardSpaceDim; y++) //cycle through all the array values
            {
                for (int x = 0; x < GameVariables.boardSpaceDim; x++) 
                {
                    if (GameVariables.board[x, y] != null)
                    {
                        if (GameVariables.board[x, y].Rank == 1) //two pieces have rank 1
                        {
                            if (GameVariables.board[x, y] is StandardUnit)
                            {
                                writer.Write(GameVariables.board[x, y].Rank); 
                            }
                            else
                                writer.Write(4);
                        }
                        else
                        {
                            writer.Write(GameVariables.board[x, y].Rank);
                        }
                    }
                    else
                        writer.Write(-1); //for blank spaces
                }
            }
        }

        public static void LoadGame(string filepath) //loads game data
        {
            if (!File.Exists(filepath))
                return; //method just dies if it's a bad path

            reader = new BinaryReader(File.Open(filepath, FileMode.Open)); //initialize the reader
            int i;
            int x = 0;
            int y = 0;
            while((i = reader.ReadInt32()) != null)
            {
                switch(i)
                {
                    case -1:
                        {
                            GameVariables.board[x, y] = null;
                            break;
                        }
                    case 0:
                        {
                            
                        }
                }
            }
        }


    }
}
