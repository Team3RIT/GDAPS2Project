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
    //still needs en passant code
    public class Pawn:Piece
    {
        
        private Vector2 plannedMove = new Vector2();
        public List<Vector2> attackSpace; //to account for the fact that pawns cannot attack straight on and therefore cannot have every space they can move to be a dangerous space.


        public Pawn(int x, int y, Texture2D icon, bool color):base(x,y,icon,color)
        {
            attackSpace = new List<Vector2>(); 
        }

        public override void Select()
        {
            
            possibleMoves = null; //clears out the list so it's a new set

            //any forward pawn movement will need to be checked for collision - cannot capture unless diagonal
            if(hasMoved == false) //first move a pawn makes
            {
                if (this.color == false) //gets to move two spaces on first move only
                {
                    plannedMove = new Vector2(location.X, location.Y + 2);
                    if (this.PathTracker(this.location, new Vector2(plannedMove.X, plannedMove.Y + 1), "vertical")) //has to be messed with so we don't get a false positive - pawns can't capture vertically
                        possibleMoves.Add(plannedMove);
                }
                if (this.color == true)
                {
                    plannedMove = new Vector2(location.X, location.Y - 2);
                    if (this.PathTracker(this.location, new Vector2(plannedMove.X, plannedMove.Y - 1), "vertical"))
                        possibleMoves.Add(plannedMove);
                }
                
            }

            //standard pawn movement
            if (this.color == false)  //forward has different meanings for black and white pieces...
            {
                plannedMove = new Vector2(location.X, location.Y + 1);
                if (this.PathTracker(this.location, new Vector2(plannedMove.X, plannedMove.Y+1), "vertical"))
                    possibleMoves.Add(plannedMove);
            }
            if (this.color == true)
            { 
                plannedMove = new Vector2(location.X, location.Y - 1);
                if (this.PathTracker(this.location, new Vector2(plannedMove.X,plannedMove.Y-1), "vertical"))
                    possibleMoves.Add(plannedMove);
            }
            


            //attacking requires no validation, only moving one space
            //diagonally forward and to the right
            if(this.color == false)
                plannedMove = new Vector2(location.X+1, location.Y+1); //black pawn
            if(this.color == true)
                plannedMove = new Vector2(location.X+1, location.Y-1); //white pawn
            if(GameVariables.board[(int)plannedMove.X,(int)plannedMove.Y] != null && GameVariables.board[(int)plannedMove.X,(int)plannedMove.Y].color != this.color) //piece in the space of an opposite color
            {
                possibleMoves.Add(plannedMove);
                
            }
            attackSpace.Add(plannedMove); //even if the pawn can't move there now, it's an unsafe place for a king

            //diagonally forward and to the left
            if (this.color == false)
                plannedMove = new Vector2(location.X-1, location.Y+1); //black pawn
            if (this.color == true)
                plannedMove = new Vector2(location.X-1, location.Y-1); //white pawn
            if (GameVariables.board[(int)plannedMove.X, (int)plannedMove.Y] != null && GameVariables.board[(int)plannedMove.X, (int)plannedMove.Y].color != this.color) //piece in the space of an opposite color
            {
                possibleMoves.Add(plannedMove);
            }
            attackSpace.Add(plannedMove);

            this.Trim(possibleMoves);
            this.Trim(attackSpace);
        }

        public new void Move(Vector2 newLocation) //the new location should be well validated before making it here
        {
            //en passant hacky bullshit
            if(!hasMoved&&Math.Abs(newLocation.Y - location.Y) == 2)
            {
                if(newLocation.Y < location.Y)
                {
                    GameVariables.board[(int)newLocation.X, (int)newLocation.Y - 1] = this;
                }
                else //covers greater than case
                {
                    GameVariables.board[(int)newLocation.X, (int)newLocation.Y + 1] = this;
                }
            }
            else //cleanup from the hacky bullshit
            {
                if (GameVariables.board[(int)location.X, (int)location.Y + 1] == this)
                    GameVariables.board[(int)location.X, (int)location.Y + 1] = null;

                if (GameVariables.board[(int)location.X, (int)location.Y - 1] == this)
                    GameVariables.board[(int)location.X, (int)location.Y - 1] = null;
            }

            GameVariables.board[(int)location.X, (int)location.Y] = null;
            location = newLocation;
            if (GameVariables.board[(int)location.X, (int)location.Y] != null && GameVariables.board[(int)location.X, (int)location.Y].color != this.color) //have to check to make sure there is a piece there before trying to look at its color
            {
                GameVariables.board[(int)location.X, (int)location.Y].Remove(this); //call the other piece's remove function
            }
            else
            {
                GameVariables.board[(int)location.X, (int)location.Y] = this; //put this piece there
            }
            
            

            if(color && location.Y == 0)
            {
                //get a new piece
                this.Remove(this); //get rid of the pawn
                Game1.AddPiece();
 
                
            }

            if(!color && location.Y == GameVariables.boardSpaceDim -1)
            {
                //get a new piece 
                this.Remove(this); //get rid of the pawn
                Game1.AddPiece();
            }

            this.hasMoved = true; //completed the first move
        }
        
    }
}
