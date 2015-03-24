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

namespace Chess
{
    public class Knight:Piece
    {
        


        public Knight(int x, int y, Texture2D icon, bool color):base(x,y,icon,color)
        {

        }

        public override void Select()
        {
            
            possibleMoves = null; //clears out the list so it's a new set
                        //between the Trim method and the Knights ability to jump, no checking required.
           

            //standard knight movement
            
            //two spaces up

            //left
            possibleMoves.Add(new Vector2(location.X - 1, location.Y - 2));
            //right
            possibleMoves.Add(new Vector2(location.X + 1, location.Y - 2));

            //two spaces left
            
            //up
            possibleMoves.Add(new Vector2(location.X - 2, location.Y - 1));
            //down
            possibleMoves.Add(new Vector2(location.X - 2, location.Y + 1));

            //two spaces down

            //left
            possibleMoves.Add(new Vector2(location.X - 1, location.Y + 2));
            //right
            possibleMoves.Add(new Vector2(location.X + 1, location.Y + 2));

            //two spaces right

            //up
            possibleMoves.Add(new Vector2(location.X + 2, location.Y - 1));
            //down
            possibleMoves.Add(new Vector2(location.X + 2, location.Y + 1));

            this.Trim(possibleMoves);
          }

    }
}
