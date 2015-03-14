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

namespace Arbiter
{
    static class GameVariables
    {
        static public Piece[,] board = new Piece[boardSpaceDim,boardSpaceDim]; //white is bottom, black is top
        public const int screenWidth = 800;
        public const int screenHeight = 680;
        public const int boardDim = 400; //square board, only need one dimension
        public const int spaceDim = boardDim / boardSpaceDim; //square spaces, one dimension
        public const int boardSpaceDim = 10; //icons should be 64px square
        public const int screenbufferHorizontal = (screenWidth - boardDim)/2;
        public const int screenbufferVertical = (screenHeight - boardDim) / 2;
        
        //location.X*spaceDim, location.Y*spaceDim will work for giving a top left screen location.

        public static bool OnBoard(Vector2 vector)
        {
            if (vector.X >= 0 && vector.Y >= 0 && vector.X < boardSpaceDim && vector.Y < boardSpaceDim)
                return true;
            else
                return false;
        }


        
    }
}
