using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

//Margaret
namespace Arbiter
{
    static class GameVariables
    {
        public static Piece[,] board; //game board
        public static Vector2 gamePadLocation;
        static public List<Tower> towers; //important lists that multiple classes will need
        static public List<Structure> structures;
        static public List<Player> players;

        //board dimensions
        public const int screenWidth = 1000;
        public const int screenHeight = 800;
        public static int boardDim = 640; //square board, only need one dimension
        public static int spaceDim; //square spaces, one dimension
        private static int boardSpaceDim; 
        public static int screenbufferHorizontal = (screenWidth - boardDim)/2;
        public static int screenbufferVertical = (screenHeight - boardDim)/2;
        //location.X*spaceDim+screenbufferHorizontal, location.Y*spaceDim+screenbufferHorizontal will work for giving a board location.

     
        public const int NumPiecesPerTurn = 5;
        

        static GameVariables() //static constructor
        {
            
            boardSpaceDim = 10; //icons should be 64px square
            spaceDim = boardDim / boardSpaceDim;
            board = new Piece[boardSpaceDim,boardSpaceDim];
            towers = new List<Tower>();
            structures = new List<Structure>();
            players = new List<Player>();
            gamePadLocation = new Vector2(0, 0);

        }
        public static int BoardSpaceDim //to avoid divide by zero exception
        {
            get
            {
                return boardSpaceDim;
            }
            set //should only be used when loading a map/saved game, because it will clear everything.
            {
                if (value > 0) //avoids divide by zero error
                {
                    boardSpaceDim = value;
                    spaceDim = boardDim / boardSpaceDim;
                    board = new Piece[boardSpaceDim, boardSpaceDim];
                }
            }
        }
        
        public static bool OnBoard(Vector2 vector)
        {
            if (vector.X >= 0 && vector.Y >= 0 && vector.X < boardSpaceDim && vector.Y < boardSpaceDim)
                return true;
            else
                return false;
        }

        public static void ClearBoard()
        {
            BoardSpaceDim = boardSpaceDim;
            Game1.movedUnits.Clear();
        }

        
    }
}
