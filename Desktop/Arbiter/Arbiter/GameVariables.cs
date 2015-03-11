using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arbiter
{
    static class GameVariables
    {
        static public Piece[,] board = new Piece[boardSpaceDim,boardSpaceDim]; 
        public const int screenWidth = 800;
        public const int screenHeight = 640;
        public const int boardDim = 640; //square board, only need one dimension
        public const int spaceDim = boardDim / boardSpaceDim; //square spaces, one dimension
        public const int boardSpaceDim = 10; //icons should be 64px square
        public const int screenbufferHorizontal = (screenWidth - boardDim)/2;
        public const int screenbufferVertical = (screenHeight - boardDim)/2;
        
        //location.X*spaceDim, location.Y*spaceDim will work for giving a top left screen location (used in recangle drawing)


        
    }
}
