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
    //Margaret
    abstract class Piece
    {
        protected Vector2 location;
        protected Texture2D icon;
        protected Rectangle region;
        public Player owner = new Player("Map",-1); //There will be essentially a map player that never has a turn that owns inanimate pieces, just to allow the use of a Piece array.
                                                    //IMPORTANT: Do not use negative ID numbers for other players!
        protected int rank;


        public Rectangle Region
        {
            get
            {
                return region;
            }
        }


        public Player Owner
        {
            get
            {
                return owner;
            }
        }

        public Vector2 Location
        {
            get
            {
                return location;
            }
        }

        public int Rank
        {
            get
            {
                return rank;
            }
        }



        public Piece(int x, int y, Texture2D icn)
        {
            if (x >= 0 && x < GameVariables.boardSpaceDim && y >= 0 && x < GameVariables.boardSpaceDim && GameVariables.board[x, y] == null) // if both are within bounds and the space is empty
            {                                                                      // the array gets checked last so there's no out of bounds exception       
                location.X = x;
                location.Y = y;
                GameVariables.board[x, y] =this;
                region = new Rectangle((int)(location.X * GameVariables.spaceDim + GameVariables.screenbufferHorizontal), (int)(location.Y * GameVariables.spaceDim + GameVariables.screenbufferVertical), GameVariables.spaceDim, GameVariables.spaceDim);
                icon = icn;
            }
        }

        public void Remove(Unit piece)
        {
            if (location.X == piece.location.X && location.Y == piece.location.Y)
            {

                GameVariables.board[(int)location.X, (int)location.Y] = piece; //takes itself off the back end board

            }
        }

        
    }
}
