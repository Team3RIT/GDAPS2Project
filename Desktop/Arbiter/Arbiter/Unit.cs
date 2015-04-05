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
        public bool hasMoved; //Don't need this anymore; thought of a better way of doing things than the way we discussed earlier -Travis
        
        #endregion

        #region constructor
        public Unit(int x, int y, Player own):base(x,y)
        {
           owner = own;
           //hasMoved = false;
           switch (owner.ID) //player 1, 2, 3, .. etc
           {
               
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
            if (!possibleMoves.Contains(newLocation)) //if it's not valid, just another check..
                return; //quit the method
            Tower myTower = null; // will be null if the loop doesnt find a tower
            Tower newTower = null;// will be null if you aren't moving to the tower
            foreach(Tower tower in GameVariables.towers) //checks all the towers to see if this piece is currently on one before moving
            {
                if (tower.Location == this.Location)
                {
                   
                    myTower = tower;
                    break;
                }
                if(tower.Location == newLocation)
                {
                    newTower = tower;
                }
            }
            if (myTower != null)
                myTower.Abandon(this); //abandon it if you're on one
            
            if(newTower != null)
                newTower.Claim(this);
            if (GameVariables.board[(int)location.X, (int)location.Y] != null && GameVariables.board[(int)location.X, (int)location.Y].Rank < rank && GameVariables.board[(int)location.X, (int)location.Y].owner != this.owner) //have to check to make sure there is a piece there before trying to look at its owner
            {
                GameVariables.board[(int)location.X, (int)location.Y].Remove(this);
            }
            else
            {
                GameVariables.board[(int)newLocation.X, (int)newLocation.Y] = this;
                GameVariables.board[(int)location.X, (int)location.Y] = null; //previous space will be left empty    
                location = newLocation;
                region.X = (int)location.X*GameVariables.spaceDim+GameVariables.screenbufferHorizontal;
                region.Y = (int)location.Y*GameVariables.spaceDim+GameVariables.screenbufferVertical;
            }

        }
        public override List<Vector2> Select()
        {
            possibleMoves.Clear(); //clears out the list so it's a new set
            Vector2 plannedMove;

            for (int i = 1; i <= rank; i++) //moving down 
            {
                plannedMove = new Vector2(location.X, location.Y + i);
                if (GameVariables.OnBoard(plannedMove))
                {
                    if (this.PathTracker(this.location, plannedMove, "vertical"))
                        possibleMoves.Add(plannedMove);
                }
            }

            for (int i = 1; i <= rank; i++) //moving up 
            {
                
                plannedMove = new Vector2(location.X, location.Y - i);
                if (GameVariables.OnBoard(plannedMove))
                {
                    if (this.PathTracker(this.location, plannedMove, "vertical"))
                        possibleMoves.Add(plannedMove);
                }
            }
            for (int i = 1; i <= rank; i++) //moving right
            {
                plannedMove = new Vector2(location.X+i, location.Y);
                if (GameVariables.OnBoard(plannedMove))
                {
                    if (this.PathTracker(this.location,plannedMove, "horizontal"))
                        possibleMoves.Add(plannedMove);
                }
            }
            for (int i = 1; i <= rank; i++) //moving left
            {
                plannedMove = new Vector2(location.X - i, location.Y);
                if (GameVariables.OnBoard(plannedMove))
                {
                    if (this.PathTracker(this.location,plannedMove, "horizontal"))
                        possibleMoves.Add(plannedMove);
                }
            }
            for (int i = 1; i <= rank/2; i++) //diagonally up and left
            {
                
                    plannedMove = new Vector2(location.X - i, location.Y - i);
                if (GameVariables.OnBoard(plannedMove))
                {
                    if (this.PathTracker(this.location, plannedMove, "diagonal"))
                        possibleMoves.Add(plannedMove);
                }
            }
            for (int i = 1; i <= rank / 2; i++) //diagonally up and right
            {
                plannedMove = new Vector2(location.X + i, location.Y - i);
                if (GameVariables.OnBoard(plannedMove))
                {
                    if (this.PathTracker(this.location, plannedMove, "diagonal"))
                        possibleMoves.Add(plannedMove);
                }
            }
            for (int i = 1; i <= rank / 2; i++) //diagonally down and left
            {
                plannedMove = new Vector2(location.X - i, location.Y + i);
                if (GameVariables.OnBoard(plannedMove))
                {
                    if (this.PathTracker(this.location, plannedMove, "diagonal"))
                        possibleMoves.Add(plannedMove);
                }
            }
            for (int i = 1; i <= rank / 2; i++) //diagonally down and right
            {
                plannedMove = new Vector2(location.X + i, location.Y + i);
                if (GameVariables.OnBoard(plannedMove))
                {
                    if (this.PathTracker(this.location, plannedMove, "diagonal"))
                        possibleMoves.Add(plannedMove);
                }
            }
            this.Trim(ref possibleMoves);
            return possibleMoves;
        }
        
        
        new public void Remove(Unit piece) //removed piece double verifies being taken
        {
            if(location.X == piece.location.X && location.Y == piece.location.Y)
            {
                
                GameVariables.board[(int)location.X, (int)location.Y] = piece; //take it off the board

            }

          
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
                                 
                                if (GameVariables.board[(int)locationinitial.X, (int)locationinitial.Y] != null) //check for collision
                                {
                                    return false; //not clear to make this movement

                                }
                                locationinitial.X++; //move one space in the correct direction
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
            if ((GameVariables.board[(int)locationfinal.X, (int)locationfinal.Y] == null || GameVariables.board[(int)locationfinal.X, (int)locationfinal.Y].owner != this.owner)
                && rank < GameVariables.board[(int)locationfinal.X, (int)locationfinal.Y].Rank) //The piece is allowed in that spot
            {
                if (GameVariables.board[(int)locationfinal.X, (int)locationfinal.Y].Rank == 0 && rank == 3) //heavy units cant go into towers
                    return false;
                else
                    return true;
            }
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
