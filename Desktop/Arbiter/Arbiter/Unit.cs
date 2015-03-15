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

//Margaret
namespace Arbiter
{
    public abstract class Unit:Piece
    {
        #region attributes
        
        
        protected List<Vector2> possibleMoves = new List<Vector2>(); //the legal moves that a player can make
        
        #endregion

        #region constructor
        public Unit(int x, int y, Texture2D icn, Player own):base(x,y,icn)
        {
           owner = own;

        }
        #endregion

        #region properties

        

        public List<Vector2> PossibleMoves
        {
            get
            {
                return possibleMoves;
            }
        }

    

    

        #endregion

        #region methods
        public void Move(Vector2 newLocation) //the new location should be well validated before making it here
        {
            GameVariables.board[(int)location.X, (int)location.Y] = null;
            location = newLocation;
            if(GameVariables.board[(int)location.X,(int)location.Y] is Unit && GameVariables.board[(int)location.X,(int)location.Y].owner != this.owner) //have to check to make sure there is a piece there before trying to look at its owner
            {
                GameVariables.board[(int)location.X, (int)location.Y].Remove(this);
            }

            

        }
        abstract public List<Vector2> Select();
        
        public void Remove(Unit piece) //removed piece double verifies being taken
        {
            if(location.X == piece.location.X && location.Y == piece.location.Y)
            {
                
                GameVariables.board[(int)location.X, (int)location.Y] = piece; //take it off the back end board

            }

            #region Removing piece from Game1 Lists

            //take it off the "physical" board
            //Kings shouldn't ever be taken, because checkmate is an endgame condition.
            //this stuff is Chess specific, will be adapted to future unit types
            /*if(this is Pawn)
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
             */
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
            if (GameVariables.board[(int)locationfinal.X, (int)locationfinal.Y] == null || GameVariables.board[(int)locationfinal.X, (int)locationfinal.Y].owner != this.owner)
                return true;
            else
                return false;
        }


        public void Trim(ref List<Vector2> list) //takes moves that would take you off the board out of possible moves //note: this is a pass by reference, we haven't covered it in class yet so if you aren't familiar, READ UP
        {
            foreach(Vector2 space in list.ToList()) //program doesn't like changing the actual list while enumerating
            {
                if (!GameVariables.OnBoard(space))
                    list.Remove(space);
                else if (GameVariables.board[(int)space.X, (int)space.Y] != null && GameVariables.board[(int)space.X, (int)space.Y].owner == this.owner)
                    list.Remove(space);
                
            }
        }

        #endregion


    }
}
