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
        public StandardUnit(int x, int y, Texture2D icn, Player own)
            : base(x, y, icn, own)
        {
            rank = 2;
        }

    }
    class HeavyUnit : Unit
    {
        public HeavyUnit(int x, int y, Texture2D icn, Player own)
            : base(x, y, icn, own)
        {
            rank = 1;
        }
    }

    class LightUnit : Unit
    {
        public LightUnit(int x, int y, Texture2D icn, Player own)
            : base(x, y, icn, own)
        {
            rank = 3;
        }

        new public List<Vector2> Select()
        {
            base.Select(); //do standard Select
            Vector2 plannedMove;

            //add additional possibilities for 3 space movement
            //one space up, two spaces right
            {
                plannedMove = new Vector2(location.X + 2, location.Y - 1);
                if (this.PathTracker(this.location, new Vector2(plannedMove.X + 2, plannedMove.Y), "horizontal")) //because of the way path tracker works, it's easier to iterate this way
                    if (this.PathTracker(this.location, new Vector2(plannedMove.X, plannedMove.Y - 1), "vertical"))
                        possibleMoves.Add(plannedMove);
            }
            //one space up, two spaces left
            {
                plannedMove = new Vector2(location.X - 2, location.Y - 1);
                if (this.PathTracker(this.location, new Vector2(plannedMove.X - 2, plannedMove.Y), "horizontal"))
                    if (this.PathTracker(this.location, new Vector2(plannedMove.X, plannedMove.Y - 1), "vertical"))
                        possibleMoves.Add(plannedMove);
            }
            //one space down, two spaces left
            {
                plannedMove = new Vector2(location.X - 2, location.Y + 1);
                if (this.PathTracker(this.location, new Vector2(plannedMove.X - 2, plannedMove.Y), "horizontal"))
                    if (this.PathTracker(this.location, new Vector2(plannedMove.X, plannedMove.Y + 1), "vertical"))
                        possibleMoves.Add(plannedMove);
            }
             //one space down, two spaces right
            {
                plannedMove = new Vector2(location.X + 2, location.Y + 1);
                if (this.PathTracker(this.location, new Vector2(plannedMove.X + 2, plannedMove.Y), "horizontal"))
                    if (this.PathTracker(this.location, new Vector2(plannedMove.X, plannedMove.Y + 1), "vertical"))
                        possibleMoves.Add(plannedMove);
            }
            //one space right, two spaces up
            {
                plannedMove = new Vector2(location.X + 1, location.Y - 2);
                if (this.PathTracker(this.location, new Vector2(plannedMove.X + 1, plannedMove.Y), "horizontal"))
                    if (this.PathTracker(this.location, new Vector2(plannedMove.X, plannedMove.Y - 2), "vertical"))
                        possibleMoves.Add(plannedMove);
            }
            //one space right, two spaces down
            {
                plannedMove = new Vector2(location.X + 1, location.Y + 2);
                if (this.PathTracker(this.location, new Vector2(plannedMove.X + 1, plannedMove.Y), "horizontal"))
                    if (this.PathTracker(this.location, new Vector2(plannedMove.X, plannedMove.Y + 2), "vertical"))
                        possibleMoves.Add(plannedMove);
            }
            //one space left, two spaces up
            {
                plannedMove = new Vector2(location.X - 1, location.Y - 2);
                if (this.PathTracker(this.location, new Vector2(plannedMove.X - 1, plannedMove.Y), "horizontal"))
                    if (this.PathTracker(this.location, new Vector2(plannedMove.X, plannedMove.Y - 2), "vertical"))
                        possibleMoves.Add(plannedMove);
            }
             //one space left, two spaces down
            {
                plannedMove = new Vector2(location.X - 1, location.Y + 2);
                if (this.PathTracker(this.location, new Vector2(plannedMove.X - 1, plannedMove.Y), "horizontal"))
                    if(this.PathTracker(this.location, new Vector2(plannedMove.X, plannedMove.Y+2), "vertical"))
                        possibleMoves.Add(plannedMove);
            }


            return possibleMoves;
        }
    
    }

    class JumperUnit : Unit
    {
        public JumperUnit(int x, int y, Texture2D icn, Player own)
            : base(x, y, icn, own)
        {
            rank = 1;
        }

        new public List<Vector2> Select()
        {
            base.Select(); // get normal movement

            //code for jump movement
            List<Piece> pieces = CheckSides(this);
            List<Piece> pieces2 = new List<Piece>();
            //needs more code
            foreach(Piece piece in pieces) //go through each possible jump configuration
            {

                if (CheckSides(piece).Count > 1) //it has to be touching at least one piece
                    pieces2.Add(piece);
                       
            }

            return possibleMoves;
        }
        public List<Piece> CheckSides(Piece origin) //checks all 4 adjacent spaces for more pieces.
        {
            List<Piece> pieces = new List<Piece>();
            if (GameVariables.board[(int)origin.Location.X + 1, (int)origin.Location.Y] != null)
                pieces.Add(GameVariables.board[(int)origin.Location.X + 1, (int)origin.Location.Y]);
            if (GameVariables.board[(int)origin.Location.X - 1, (int)origin.Location.Y] != null)
                pieces.Add(GameVariables.board[(int)origin.Location.X - 1, (int)origin.Location.Y]);
            if (GameVariables.board[(int)origin.Location.X, (int)origin.Location.Y+1] != null)
                pieces.Add(GameVariables.board[(int)origin.Location.X, (int)origin.Location.Y + 1]);
            if (GameVariables.board[(int)origin.Location.X, (int)origin.Location.Y-1] != null)
                pieces.Add(GameVariables.board[(int)origin.Location.X, (int)origin.Location.Y - 1]);
            return pieces;
        }
    }

}
  

