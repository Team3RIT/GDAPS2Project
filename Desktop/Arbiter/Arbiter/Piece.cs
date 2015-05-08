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
        public Color color = Color.White;
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
        public int RegionX
        {
            set
            {
                region.X = value;
            }
        }
        public int RegionY
        {
            set
            {
                region.Y = value;
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

            
            
        }
        #endregion

        #region methods
        public virtual List<Vector2> Select()
        {
            return null;
        }
        public void Remove(Unit piece)
        {
            return; //does nothing, towers and structures can't be removed
        }
        #endregion

    }
}
