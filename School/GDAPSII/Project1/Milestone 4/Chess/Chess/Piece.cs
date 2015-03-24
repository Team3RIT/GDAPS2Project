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
    public abstract class Piece
    {
        public Vector2 location;
        public Texture2D icon;
        
        public bool color; //true is white, false is black
        public List<Vector2> possibleMoves; //the legal moves that a player can make
        public Rectangle region;
        public bool hasMoved = false;

        public Piece(int x, int y, Texture2D icn, bool colr)
        {
           if(x>=0 && x <GameVariables.boardSpaceDim && y >=0 && x < GameVariables.boardSpaceDim && GameVariables.board[x,y] == null) // if both are within bounds and the space is empty
           {                                                                      // the array gets checked last so there's no out of bounds exception       
               location.X = x;
               location.Y = y;
               GameVariables.board[x, y] = this;
           }
           icon = icn;
           possibleMoves = new List<Vector2>();
           region = new Rectangle((int)location.X,(int) location.Y, GameVariables.spaceDim, GameVariables.spaceDim);
           color = colr;

        }

        public void Move(Vector2 newLocation) //the new location should be well validated before making it here
        {
            GameVariables.board[(int)location.X, (int)location.Y] = null;
            location = newLocation;
            if(GameVariables.board[(int)location.X,(int)location.Y] != null && GameVariables.board[(int)location.X,(int)location.Y].color != this.color) //have to check to make sure there is a piece there before trying to look at its color
            {
                GameVariables.board[(int)location.X, (int)location.Y].Remove(this);
            }

            hasMoved = true;

        }
        abstract public void Select();
        
        public void Remove(Piece piece) //removed piece double verifies being taken
        {
            if(location.X == piece.location.X && location.Y == piece.location.Y)
            {
                
                GameVariables.board[(int)location.X, (int)location.Y] = piece; //take it off the back end board

            }

            #region Removing piece from Game1 Lists

            //take it off the "physical" board
            //Kings shouldn't ever be taken, because checkmate is an endgame condition.

            if(this is Pawn)
            {
                if(this.color)
                {
                    Game1.whitePawns.Remove((Pawn)this);
                }
                else
                {
                    Game1.blackPawns.Remove((Pawn)this);
                }
            }

            if (this is Knight)
            {
                if (this.color)
                {
                    Game1.whiteKnights.Remove((Knight)this);
                }
                else
                {
                    Game1.blackKnights.Remove((Knight)this);
                }
            }

            if (this is Bishop)
            {
                if (this.color)
                {
                    Game1.whiteBishops.Remove((Bishop)this);
                }
                else
                {
                    Game1.blackBishops.Remove((Bishop)this);
                }
            }
            if (this is Rook)
            {
                if (this.color)
                {
                    Game1.whiteRooks.Remove((Rook)this);
                }
                else
                {
                    Game1.blackRooks.Remove((Rook)this);
                }
            }
            if (this is Queen)
            {
                if (this.color)
                {
                    Game1.whiteQueen.Remove((Queen)this);
                }
                else
                {
                    Game1.blackQueen.Remove((Queen)this);
                }
            }
            #endregion
        }

        public bool PathTracker(Vector2 locationinitial, Vector2 locationfinal, string type)//makes sure nothing is in the way
        {
            switch(type.ToLower()) //just in case a capital letter sneaks in to the hard coding
            {
                    //does not handle knight movement because knights can jump
                case "vertical":
                    {
                        if(locationinitial.Y - locationfinal.Y > 0) //if trying to move up
                        {
                            locationinitial.Y--;
                            while(locationinitial.Y != locationfinal.Y) //until you've reached the destination
                            {
                                //move one space in the correct direction
                                if(GameVariables.board[(int)locationinitial.X,(int)locationinitial.Y]!=null) //check for collision
                                {
                                    return false; //not clear to make this movement

                                }
                                locationinitial.Y--;
                            }
                        }
              
                        if (locationinitial.Y - locationfinal.Y < 0) //if trying to move down
                        {
                            locationinitial.Y++;
                            while (locationinitial.Y != locationfinal.Y) //until you've reached the destination
                            {
                                 //move one space in the correct direction
                                if (GameVariables.board[(int)locationinitial.X, (int)locationinitial.Y] != null) //check for collision
                                {
                                    return false; //not clear to make this movement

                                }
                                locationinitial.Y++;
                            }
                        }

                        //the not moving at all case should not happen due to the way movement is handled
                        return true; 
                    }
                case "horizontal":
                    {
                        if (locationinitial.X - locationfinal.X > 0) //if trying to move to the left
                        {
                            locationinitial.X--;
                            while (locationinitial.X != locationfinal.X) //until you've reached the destination
                            {
                                 //move one space in the correct direction
                                if (GameVariables.board[(int)locationinitial.X, (int)locationinitial.Y] != null) //check for collision
                                {
                                    return false; //not clear to make this movement

                                }
                                locationinitial.X--;
                            }
                        }
                        if (locationinitial.X - locationfinal.X < 0) //if trying to move to the right
                        {
                            locationinitial.X++;
                            while (locationinitial.X != locationfinal.X) //until you've reached the destination
                            {
                                locationinitial.X++; //move one space in the correct direction
                                if (GameVariables.board[(int)locationinitial.X, (int)locationinitial.Y] != null) //check for collision
                                {
                                    return false; //not clear to make this movement

                                }
                                locationinitial.X++;
                            }
                        }
                        return true;
                    }
 
                case "diagonal": 
                    {
                        if ((locationinitial.X - locationfinal.X > 0)&&(locationinitial.Y - locationfinal.Y > 0)) //if trying to move up/left
                        {
                            locationinitial.X--;
                            locationinitial.Y--;
                            while (locationinitial != locationfinal) //until you've reached the destination
                            {
                                 //move one space in the correct direction
                                if (GameVariables.board[(int)locationinitial.X, (int)locationinitial.Y] != null) //check for collision
                                {
                                    return false; //not clear to make this movement

                                }
                                locationinitial.X--;
                                locationinitial.Y--;
                            }
                        }
                        if ((locationinitial.X - locationfinal.X < 0) && (locationinitial.Y - locationfinal.Y > 0)) //if trying to move up/right
                        {
                            locationinitial.X++;
                            locationinitial.Y--;
                            while (locationinitial != locationfinal) //until you've reached the destination
                            {
                                //move one space in the correct direction
                                if (GameVariables.board[(int)locationinitial.X, (int)locationinitial.Y] != null) //check for collision
                                {
                                    return false; //not clear to make this movement

                                }
                                locationinitial.X++;
                                locationinitial.Y--;
                            }
                        }
                        if ((locationinitial.X - locationfinal.X > 0) && (locationinitial.Y - locationfinal.Y < 0)) //if trying to move down/left
                        {
                            locationinitial.X--;
                            locationinitial.Y++;
                            while (locationinitial != locationfinal) //until you've reached the destination
                            {
                                 //move one space in the correct direction
                                if (GameVariables.board[(int)locationinitial.X, (int)locationinitial.Y] != null) //check for collision
                                {
                                    return false; //not clear to make this movement

                                }
                                locationinitial.X--;
                                locationinitial.Y++;
                            }
                        }
                        if ((locationinitial.X - locationfinal.X < 0) && (locationinitial.Y - locationfinal.Y < 0)) //if trying to move down/right
                        {
                            locationinitial.X++;
                            locationinitial.Y++; 
                            while (locationinitial != locationfinal) //until you've reached the destination
                            {
                                //move one space in the correct direction
                                if (GameVariables.board[(int)locationinitial.X, (int)locationinitial.Y] != null) //check for collision
                                {
                                    return false; //not clear to make this movement

                                }
                                locationinitial.X++;
                                locationinitial.Y++; 
                            }
                        }
                        
                        return true;
                    }
            }
            if (GameVariables.board[(int)locationfinal.X, (int)locationfinal.Y] == null || GameVariables.board[(int)locationfinal.X, (int)locationfinal.Y].color != this.color)
                return true;
            else
                return false;
        }


        public void Trim(List<Vector2> list) //takes moves that would take you off the board out of possible moves
        {
            foreach(Vector2 space in list)
            {
                if (space.X < 0 || space.X >= GameVariables.boardSpaceDim || space.Y < 0 || space.Y >= GameVariables.boardSpaceDim)
                    list.Remove(space);
                
            }
        }




    }
}
