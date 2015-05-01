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

    
    class StandardUnit : Unit
    {
        public StandardUnit(int x, int y, Player own)
            : base(x, y, own)
        {
            rank = 2;
            icon = Game1.Standard;
        }

    }
   
    class HeavyUnit : Unit
    {
        public HeavyUnit(int x, int y, Player own)
            : base(x, y, own)
        {
            rank = 1;
            icon = Game1.Heavy;
        }
    }

    class LightUnit : Unit
    {
        public LightUnit(int x, int y, Player own)
            : base(x, y, own)
        {
            rank = 3;
            icon = Game1.Light;
        }

        public override List<Vector2> Select()
        {
            base.Select(); //do standard Select
            Vector2 plannedMove;

            //add additional possibilities for 3 space movement
            //one space up, two spaces right
            {
                plannedMove = new Vector2(location.X + 2, location.Y - 1);
                if (GameVariables.OnBoard(plannedMove))
                {
                    if (this.PathTracker(this.location, plannedMove, "horizontal")) //because of the way path tracker works, it's easier to iterate this way
                        if (this.PathTracker(this.location, plannedMove, "vertical"))
                            possibleMoves.Add(plannedMove);
                }
            }
            //one space up, two spaces left
            {
               
                plannedMove = new Vector2(location.X - 2, location.Y - 1);
                if (GameVariables.OnBoard(plannedMove))
                {
                    if (this.PathTracker(this.location, plannedMove, "horizontal"))
                        if (this.PathTracker(this.location, plannedMove, "vertical"))
                            possibleMoves.Add(plannedMove);
                }
            }
            //one space down, two spaces left
            {
                plannedMove = new Vector2(location.X - 2, location.Y + 1);
                if (GameVariables.OnBoard(plannedMove))
                {
                    if (this.PathTracker(this.location, plannedMove, "horizontal"))
                        if (this.PathTracker(this.location, plannedMove, "vertical"))
                            possibleMoves.Add(plannedMove);
                }
            }
             //one space down, two spaces right
            {
                plannedMove = new Vector2(location.X + 2, location.Y + 1);
                if (GameVariables.OnBoard(plannedMove))
                {
                    if (this.PathTracker(this.location, plannedMove, "horizontal"))
                        if (this.PathTracker(this.location, plannedMove, "vertical"))
                            possibleMoves.Add(plannedMove);
                }
            }
            //one space right, two spaces up
            {
                plannedMove = new Vector2(location.X + 1, location.Y - 2);
               
                    if (GameVariables.OnBoard(plannedMove))
                    {
                        if (this.PathTracker(this.location, plannedMove, "horizontal"))
                            if (this.PathTracker(this.location, plannedMove, "vertical"))
                                possibleMoves.Add(plannedMove);
                    }
            }
            //one space right, two spaces down
            {
                plannedMove = new Vector2(location.X + 1, location.Y + 2);
                if (GameVariables.OnBoard(plannedMove))
                {
                    if (this.PathTracker(this.location, plannedMove, "horizontal"))
                        if (this.PathTracker(this.location, plannedMove, "vertical"))
                            possibleMoves.Add(plannedMove);
                }
            }
            //one space left, two spaces up
            {
                plannedMove = new Vector2(location.X - 1, location.Y - 2);
                if (GameVariables.OnBoard(plannedMove))
                {
                    if (this.PathTracker(this.location, plannedMove, "horizontal"))
                        if (this.PathTracker(this.location, plannedMove, "vertical"))
                            possibleMoves.Add(plannedMove);
                }
            }
             //one space left, two spaces down
            {
                plannedMove = new Vector2(location.X - 1, location.Y + 2);
                if (GameVariables.OnBoard(plannedMove))
                {
                    if (this.PathTracker(this.location, plannedMove, "horizontal"))
                        if (this.PathTracker(this.location, plannedMove, "vertical"))
                            possibleMoves.Add(plannedMove);
                    
                }
            }


            return possibleMoves;
        }
    
    }

    class JumperUnit : Unit
    {
        public JumperUnit(int x, int y,  Player own)
            : base(x, y, own)
        {
            rank = 1;
            icon = Game1.Jumper;
        }

        public override List<Vector2> Select()
        {
            base.Select(); // get normal movement

            //code for jump movement
            bool up = CheckSides(this.Location,0);
            bool down = CheckSides(this.Location,2);
            bool left = CheckSides(this.Location, 3);
            bool right = CheckSides(this.Location, 1);
            int i = 0;
            if(up)
            {   //must check that it's on the board first to avoid out of bounds exception
                while (GameVariables.OnBoard(new Vector2(Location.X, Location.Y + i)) && CheckSides(new Vector2(Location.X, Location.Y - i), 0)) //follows that line until it ends
                {
                    i++;
                }
                
                possibleMoves.Add(new Vector2(this.Location.X, Location.Y - i - 1)); //add on +1 to be the space after the last occupied space
                i = 0; //reset to 0 for next one

            }
            if(down)
            {
                while (GameVariables.OnBoard(new Vector2(Location.X, Location.Y + i)) && CheckSides(new Vector2(Location.X, Location.Y + i), 2)) //follows that line until it ends
                {
                    i++;
                }
                possibleMoves.Add(new Vector2(this.Location.X, Location.Y + i + 1)); //add on +1 to be the space after the last occupied space
                i = 0; //reset to 0 for next one
            }
            if(left)
            {
                while (GameVariables.OnBoard(new Vector2(Location.X-i, Location.Y)) && CheckSides(new Vector2(Location.X-i, Location.Y), 3)) //follows that line until it ends
                {
                    i++;
                }
                possibleMoves.Add(new Vector2(this.Location.X-i-1, Location.Y)); //add on 1 to be the space after the last occupied space
                i = 0; //reset to 0 for next one
            }
            if (right)
            {
                while (GameVariables.OnBoard(new Vector2(Location.X + i, Location.Y)) && CheckSides(new Vector2(Location.X+i, Location.Y), 1)) //follows that line until it ends
                {
                    i++;
                }
                possibleMoves.Add(new Vector2(this.Location.X + i + 1, Location.Y)); //add on 1 to be the space after the last occupied space
                i = 0; //reset to 0 for next one
            }


            this.Trim(ref possibleMoves);
            return possibleMoves;
        }
        public bool CheckSides(Vector2 origin, int direction) //0 is up, 1 is right, 2 is down, 3 is left
        {
            
            switch(direction)
            {
                case 0:
                    {
                        if (GameVariables.OnBoard(new Vector2(origin.X, origin.Y-1)) && GameVariables.board[(int)origin.X, (int)origin.Y - 1] != null)
                            return true;
                        else
                            return false;
                    }
                case 1:
                    {
                        if (GameVariables.OnBoard(new Vector2(origin.X+1, origin.Y)) && GameVariables.board[(int)origin.X + 1, (int)origin.Y] != null)
                            return true;
                        else
                            return false;
                    }
                case 2:
                    {
                        if (GameVariables.OnBoard(new Vector2(origin.X, origin.Y+1)) && GameVariables.board[(int)origin.X, (int)origin.Y + 1] != null)
                            return true;
                        else
                            return false;
                    }
                case 3:
                    {
                        if (GameVariables.OnBoard(new Vector2 (origin.X-1, origin.Y))&& GameVariables.board[(int)origin.X-1, (int)origin.Y] != null)
                            return true;
                        else 
                            return false;
                    }
                    
            }
            return 
                false;
        }
    }

}
  

