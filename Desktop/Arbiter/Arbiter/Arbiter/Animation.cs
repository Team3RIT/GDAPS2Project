using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework; //added
using Microsoft.Xna.Framework.Content; //added
using Microsoft.Xna.Framework.Graphics; //added
using Microsoft.Xna.Framework.Input; //added
using Microsoft.Xna.Framework.Storage; //added
using Microsoft.Xna.Framework.GamerServices; //added

namespace Arbiter
{
    public static class Animation
    {
        //Nick Serrao
        //contains the algorithm for animating the images in their rectangles
        //WARNING THIS CODE REQUIRES MORE TESTING AND IS CURRENTLY NOT IMPLEMENTED!!!!!!!!!!

        public static void Animate(SpriteBatch batch,Texture2D piece, Rectangle rect, int finalX, int finalY)
        {
            //move the rectangle incrementally untill it reaches its final psition 
            // increments repeatedly recursively

            if (rect.X == finalX || rect.Y == finalY)
                return; //end recursion if the final destination has been reached by the rectangle (this is the recursions base case)

            int differenceX = finalX - rect.X; //find the total differences between the current x position of the rectangle and its final pposition
            int differenceY = finalY - rect.Y; //find the total differences between the current y position of the rectangle and its final position

            if (differenceX > 0) //if the difference is positive increase its x value
                rect.X = rect.X + 2;

            if (differenceX < 0) //if the difference is negative, decrease the x value
                rect.X = rect.X - 2;

            if (differenceY > 0)  //if the difference is positive increase the y position
                rect.Y = rect.Y + 2;

            if (differenceY < 0)  //if the difference is negative decrease the y position
                rect.Y = rect.Y - 2;

            //draw the piece!
            batch.Draw(piece, rect, Color.White);

            Animate(batch, piece, rect, finalX, finalY);
        }






    }
}
