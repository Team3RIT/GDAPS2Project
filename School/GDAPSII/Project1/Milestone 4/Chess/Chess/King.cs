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
    public class King:Piece
    {
        public List<Vector2> checkSpaces;
        private Vector2 plannedMove = new Vector2();
        public bool checkmate = false;
        public bool check = false;
        
        public King(int x, int y, Texture2D icon, bool color):base(x,y,icon,color)
        {
            checkSpaces = new List<Vector2>();

        }

        public override void Select()
        {
            possibleMoves = null;
            //The king is sort of tricky, needs his own personal validation method, which I kept in the King class
            CheckSpaces();

            if (possibleMoves.Count == 0 && checkSpaces.Contains(location))
                checkmate = true;
            else if (checkSpaces.Contains(location))
                check = true;
            else
                check = false;

            //up
            plannedMove = new Vector2(location.X, location.Y - 1);
            if (!checkSpaces.Contains(plannedMove))
                possibleMoves.Add(plannedMove);
            //down
            plannedMove = new Vector2(location.X, location.Y + 1);
            if (!checkSpaces.Contains(plannedMove))
                possibleMoves.Add(plannedMove);
            //left
            plannedMove = new Vector2(location.X - 1, location.Y);
            if (!checkSpaces.Contains(plannedMove))
                possibleMoves.Add(plannedMove);
            //right
            plannedMove = new Vector2(location.X+1, location.Y);
            if (!checkSpaces.Contains(plannedMove))
                possibleMoves.Add(plannedMove);

            //castling
            if(color && this.hasMoved == false && check == false)
            {
                foreach (Rook rook in Game1.whiteRooks)
                {
                    if (rook.hasMoved ==false)
                    {
                        if(PathTracker(this.location, rook.location, "horizontal"))
                        {
                            if(rook.location.X == 0)
                            {
                                //king cannot move through danger when castling
                                if(!checkSpaces.Contains(new Vector2 (this.location.X-1,this.location.Y))&&!checkSpaces.Contains(new Vector2(this.location.X-2,this.location.Y)))
                                {
                                   //figure out how to handle this further, need to move rook when king makes this move
                                    
                                }
                            }
                            if(rook.location.X == 7)
                            {
                                //king cannot move through danger when castling
                                if (!checkSpaces.Contains(new Vector2(this.location.X + 1, this.location.Y)) && !checkSpaces.Contains(new Vector2(this.location.X + 2, this.location.Y)))
                                {
                                   
                                }
                            }
                        }
                    }
                }

            }
            else
            {

            }

            this.Trim(possibleMoves);
             //when the game checks again, it'll reset it if the king was in check last turn and is no longer


        }

        public void CheckSpaces() //compiles a list of dangerous spaces on the board
        {
            if(color == true) //white king
            {
                foreach(Pawn pawn in Game1.blackPawns)
                {
                    pawn.Select(); //check where they could move now
                    checkSpaces.AddRange(pawn.attackSpace);
                }
                foreach(Knight knight in Game1.blackKnights)
                {
                    knight.Select();
                    checkSpaces.AddRange(knight.possibleMoves);
                }
                foreach(Bishop bishop in Game1.blackBishops)
                {
                    bishop.Select();
                    checkSpaces.AddRange(bishop.possibleMoves);
                }
                foreach (Rook rook in Game1.blackRooks)
                {
                    rook.Select();
                    checkSpaces.AddRange(rook.possibleMoves);
                }

                foreach (Queen queen in Game1.blackQueen)
                {
                    queen.Select();
                    checkSpaces.AddRange(queen.possibleMoves);
                }

                Game1.blackKing.Select();
                checkSpaces.AddRange(Game1.blackKing.possibleMoves);
            }
            else //black king
            {
                foreach (Pawn pawn in Game1.whitePawns)
                {
                    pawn.Select(); //check where they could move on their next turn
                    checkSpaces.AddRange(pawn.attackSpace);
                }
                foreach (Knight knight in Game1.whiteKnights)
                {
                    knight.Select();
                    checkSpaces.AddRange(knight.possibleMoves);
                }
                foreach (Bishop bishop in Game1.whiteBishops)
                {
                    bishop.Select();
                    checkSpaces.AddRange(bishop.possibleMoves);
                }
                foreach (Rook rook in Game1.whiteRooks)
                {
                    rook.Select();
                    checkSpaces.AddRange(rook.possibleMoves);
                }

                foreach (Queen queen in Game1.whiteQueen)
                {
                    queen.Select();
                    checkSpaces.AddRange(queen.possibleMoves);
                }

                Game1.whiteKing.Select();
                checkSpaces.AddRange(Game1.whiteKing.possibleMoves);
            }
        }
    }
}
