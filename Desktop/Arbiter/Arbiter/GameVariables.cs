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
    /// <summary>
    /// Holds the static variables and constants that are needed for gameplay implementation and
    /// drawing of the map/board.
    /// </summary>
    static class GameVariables
    {
        static public Piece[,] board = new Piece[boardSpaceDim,boardSpaceDim]; //game board

        static public List<Tower> towers = new List<Tower>(); //important lists that multiple classes will need
        static public List<Structure> structures = new List<Structure>();
        static public List<Player> players = new List<Player>();

        //board dimensions
        public const int screenWidth = 800;
        public const int screenHeight = 680;
        public static int boardDim = 640; //square board, only need one dimension
        public static int spaceDim = boardDim / boardSpaceDim; //square spaces, one dimension
        public static int boardSpaceDim = 10; //icons should be 64px square
        public static int screenbufferHorizontal = (screenWidth - boardDim)/2;
        public static int screenbufferVertical = (screenHeight - boardDim) / 2;
        
        //location.X*spaceDim, location.Y*spaceDim will work for giving a top left screen location.

        /// <summary>
        /// Determines if the vector is within the bounds of the board array.
        /// </summary>
        /// <param name="vector">should be an integer value just by nature of the game, but not required</param>
        /// <returns>True if on board, false if not.</returns>
        public static bool OnBoard(Vector2 vector)
        {
            if (vector.X >= 0 && vector.Y >= 0 && vector.X < boardSpaceDim && vector.Y < boardSpaceDim)
                return true;
            else
                return false;
        }


        
    }
}
