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
    class Tower:Piece
    {
        private bool isClaimed = false;
        private Unit claimedBy;

        public Tower(int x, int y, Texture2D icn):base(x,y,icn)
        {
            rank = 0;
            GameVariables.towers.Add(this);
        }

        public void Claim(Unit unit)
        {
            if(unit.Location == this.Location && unit.Rank != 3) //lets the unit occupy the gameboard space
            {
                isClaimed = true;
                claimedBy = unit;
            }
        }

        public void Abandon(Unit unit)
        {
            isClaimed = false;
            claimedBy = null;
            GameVariables.board[(int)location.X, (int)location.Y] = this;
        }
        
    }
}
