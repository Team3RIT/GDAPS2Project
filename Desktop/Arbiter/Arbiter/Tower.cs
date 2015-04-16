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
        public bool isClaimed = false;
        public Unit claimedBy;

        public Tower(int x, int y):base(x,y)
        {
            rank = 5; //anything should be able to get in unless the if statement below catches it
            GameVariables.towers.Add(this);
            GameVariables.board[x, y] = this;
            icon = Game1.Tower;
        }

        public void Claim(Unit unit)
        {
            if(unit.Location == this.Location && unit.Rank != 1) //lets the unit occupy the gameboard space
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
