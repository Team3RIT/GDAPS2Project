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
    public class Queen:Piece
    {
        private Vector2 plannedMove = new Vector2();

        public Queen(int x, int y, Texture2D icon, bool color):base(x,y,icon,color)
        {

        }

        public override void Select()
        {

            possibleMoves = null; //clears out the list so it's a new set
            //combines rook and bishop movement
            

            //standard rook movement

            //up
            plannedMove = new Vector2(location.X, location.Y - 1);
            while (this.PathTracker(location, plannedMove, "vertical") && plannedMove.X >= 0 && plannedMove.Y >= 0 && (int)plannedMove.X < GameVariables.boardSpaceDim && (int)plannedMove.Y < GameVariables.boardSpaceDim)
            {
                possibleMoves.Add(plannedMove);

                plannedMove.Y--;
            }

            //left

            plannedMove = new Vector2(location.X - 1, location.Y);
            while (this.PathTracker(location, plannedMove, "horizontal") && plannedMove.X >= 0 && plannedMove.Y >= 0 && (int)plannedMove.X < GameVariables.boardSpaceDim && (int)plannedMove.Y < GameVariables.boardSpaceDim)
            {
                possibleMoves.Add(plannedMove);
                plannedMove.X--;

            }

            //down
            plannedMove = new Vector2(location.X, location.Y + 1);
            while (this.PathTracker(location, plannedMove, "vertical") && plannedMove.X >= 0 && plannedMove.Y >= 0 && (int)plannedMove.X < GameVariables.boardSpaceDim && (int)plannedMove.Y < GameVariables.boardSpaceDim)
            {
                possibleMoves.Add(plannedMove);

                plannedMove.Y++;
            }

            //right
            plannedMove = new Vector2(location.X + 1, location.Y);
            while (this.PathTracker(location, plannedMove, "horizontal") && plannedMove.X >= 0 && plannedMove.Y >= 0 && (int)plannedMove.X < GameVariables.boardSpaceDim && (int)plannedMove.Y < GameVariables.boardSpaceDim)
            {
                possibleMoves.Add(plannedMove);
                plannedMove.X++;

            }

            //standard bishop movement

            //diagonal up/right
            plannedMove = new Vector2(location.X + 1, location.Y - 1);
            while (this.PathTracker(location, plannedMove, "diagonal") && plannedMove.X >= 0 && plannedMove.Y >= 0 && (int)plannedMove.X < GameVariables.boardSpaceDim && (int)plannedMove.Y < GameVariables.boardSpaceDim)
            {
                possibleMoves.Add(plannedMove);
                plannedMove.X++;
                plannedMove.Y--;
            }

            //diagonal up/left

            plannedMove = new Vector2(location.X - 1, location.Y - 1);
            while (this.PathTracker(location, plannedMove, "diagonal") && plannedMove.X >= 0 && plannedMove.Y >= 0 && (int)plannedMove.X < GameVariables.boardSpaceDim && (int)plannedMove.Y < GameVariables.boardSpaceDim)
            {
                possibleMoves.Add(plannedMove);
                plannedMove.X--;
                plannedMove.Y--;
            }

            //diagonal down/right
            plannedMove = new Vector2(location.X + 1, location.Y + 1);
            while (this.PathTracker(location, plannedMove, "diagonal") && plannedMove.X >= 0 && plannedMove.Y >= 0 && (int)plannedMove.X < GameVariables.boardSpaceDim && (int)plannedMove.Y < GameVariables.boardSpaceDim)
            {
                possibleMoves.Add(plannedMove);
                plannedMove.X++;
                plannedMove.Y++;
            }

            //diagonal down/left
            plannedMove = new Vector2(location.X - 1, location.Y + 1);
            while (this.PathTracker(location, plannedMove, "diagonal") && plannedMove.X >= 0 && plannedMove.Y >= 0 && (int)plannedMove.X < GameVariables.boardSpaceDim && (int)plannedMove.Y < GameVariables.boardSpaceDim)
            {
                possibleMoves.Add(plannedMove);
                plannedMove.X--;
                plannedMove.Y++;
            }

            this.Trim(possibleMoves); //shouldn't actually need to trim considering how the loops work, but just in case..
        }
    }
}
