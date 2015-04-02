using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arbiter
{
    //Travis

    //Class for holding player and piece information as well as running the game.
    class Match
    {
        Unit[,] pieces; //Player followed by piece number.
        int whoseTurn; //Index of whose turn it happens to be.
        int[] victoryTally;
        public Match(int playerCount)
        {
            //initialize Player and Pieces arrays to the number of players specified in match creation.
            pieces = new Unit[playerCount+1, 14];
            GameVariables.players.Add(new Player("Terrain", 0));
            for (int i = 1; i < playerCount + 1; i++ )
                GameVariables.players.Add(new Player(("Player " + i), i));
            whoseTurn = 1;
            victoryTally = new int[playerCount];
            for (int i = 0; i < victoryTally.Length; i++)
                victoryTally[i] = 0;
        }
        public void Draft()
        {
            //Will eventually contain algorithm for a fair draft system for multiple players.
            //For now, just places pieces on the board for the two-player game created in Game1.

            
            //Player 1's pieces
            for(int i = 0; i < 8; i++) //8 Standard Units
            {
                pieces[1, i] = new StandardUnit(i + 1, 9, GameVariables.players[1]);
            }
            //2 of each other unit
            pieces[1, 8] = new HeavyUnit(4, 8, GameVariables.players[1]);
            pieces[1, 9] = new HeavyUnit(5, 8, GameVariables.players[1]);
            pieces[1, 10] = new JumperUnit(2, 8, GameVariables.players[1]);
            pieces[1, 11] = new JumperUnit(7, 8, GameVariables.players[1]);
            pieces[1, 12] = new LightUnit(3, 8, GameVariables.players[1]);
            pieces[1, 13] = new LightUnit(6, 8, GameVariables.players[1]);



            //Player 2's pieces
            for (int i = 0; i < 8; i++) //8 Standard Units
            {
                pieces[1, i] = new StandardUnit(i + 1, 0, GameVariables.players[2]);
            }
            //2 of each other unit
            pieces[1, 8] = new HeavyUnit(4, 1, GameVariables.players[2]);
            pieces[1, 9] = new HeavyUnit(5, 1, GameVariables.players[2]);
            pieces[1, 10] = new JumperUnit(2, 1, GameVariables.players[2]);
            pieces[1, 11] = new JumperUnit(7, 1, GameVariables.players[2]);
            pieces[1, 12] = new LightUnit(3, 1, GameVariables.players[2]);
            pieces[1, 13] = new LightUnit(6, 1, GameVariables.players[2]);
        }

         public bool TurnManager()
        {
            //First, check to see if the player's victory tally is 2 or greater. If it is, that player has won.
            //while (victoryTally[whoseTurn - 1] < 2)
            
                //Run a turn, then, when said turn is over, advance the turn to the next player. When this returns true, someone has won.
             //Things that happen at the end of a turn go in here
            
                //Check Win Condition
                //First, check number of owned towers
                int towersControlled = 0;
                for (int i = 0; i < GameVariables.towers.Count; i++)
                {
                    for (int j = 0; j < 14; j++)
                        if (GameVariables.towers[i].Location == pieces[whoseTurn, j].Location)
                        {
                            towersControlled++;
                        }
                }
                //If the player owns the majority of the towers, increment their victory tally by 1.
                if (towersControlled > (float)(GameVariables.towers.Count / 2))
                {
                    //victoryTally[whoseTurn - 1]++;
                    GameVariables.victoryTally = 2;
                }
                //else //If they don't own the majority of the towers at the end of their turn, their tally is reset to 0.
                //{
                  //  victoryTally[whoseTurn - 1] = 0;
                //}
            

                //Advance the turn
                whoseTurn++;
                if (whoseTurn > GameVariables.players.Count - 1)
                {
                    whoseTurn = 1;
                }
                //GameVariables.players[whoseTurn].MovedUnits = 0;

                if (GameVariables.victoryTally >= 2)
                { 
                    return true;
                }
                GameVariables.victoryTally = 0; //reset victory tally
                return false;
            
        }
    }
}
