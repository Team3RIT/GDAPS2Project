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
    public abstract class Piece
    {
        #region attributes
        public Color color;
        protected Vector2 location;
        protected Rectangle region;
        public Player owner = new Player("Terrain",-1); //There will be essentially a map player that never has a turn that owns inanimate pieces, just to allow the use of a Piece array.
        public Texture2D icon;                                           //IMPORTANT: Do not use id numbers < 1 for other players!
        protected int rank;
        #endregion

        #region properties
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

        #endregion

        #region constructor
        public Piece(int x, int y)
        {
            if (x >= 0 && x < GameVariables.BoardSpaceDim && y >= 0 && x < GameVariables.BoardSpaceDim && GameVariables.board[x, y] == null) // if both are within bounds and the space is empty
            {                                                                      // the array gets checked last so there's no out of bounds exception       
                location.X = x;
                location.Y = y;
                GameVariables.board[x, y] =this;
                region = new Rectangle((int)(location.X * GameVariables.spaceDim + GameVariables.screenbufferHorizontal), (int)(location.Y * GameVariables.spaceDim + GameVariables.screenbufferVertical), GameVariables.spaceDim, GameVariables.spaceDim);
            }

            switch(owner.ID) //player 1, 2, 3, .. etc
            {
                case -1:
                    {
                        color = Color.White;
                        break;
                    }
                case 1:
                    {
                        color = Color.Red;
                        break;
                    }

                case 2:
                    {
                        color = Color.Blue;
                        break;
                    }

                case 3:
                    {
                        color = Color.Green;
                        break;
                    }
                case 4:
                    {
                        color = Color.Orange;
                        break;
                    }
                
            }
        }
        #endregion

        #region methods
        public List<Vector2> Select()
        {
            return null;
        }
        public void Remove(Unit piece)
        {
            if (location.X == piece.location.X && location.Y == piece.location.Y)
            {

                GameVariables.board[(int)location.X, (int)location.Y] = piece; //takes itself off the back end board

            }
        }
        #endregion

    }
}
