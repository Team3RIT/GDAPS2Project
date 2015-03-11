using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Team3Game
{
    static class GameVariables
    {
        static public Piece[,] board = new Piece[boardSpaceDim,boardSpaceDim]; //white is bottom, black is top
        public const int screenWidth = 800;
        public const int screenHeight = 480;
        public const int boardDim = 400; //square board, only need one dimension
        public const int spaceDim = boardDim / boardSpaceDim; //square spaces, one dimension
        public const int boardSpaceDim = 8; //icons should be 50px square
        public const int screenbufferHorizontal = 200;
        public const int screenbufferVertical = 40;
        
        //location.X*spaceDim, location.Y*spaceDim will work for giving a top left screen location.


        
    }
}
