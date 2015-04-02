using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Margaret
namespace Arbiter
{
    public class Player
    {
        #region attributes
        string name; //player name
        int playerNum; //should be 1 to 4, the "player" that owns the immobile pieces will be player 0.

        //int movedUnits; //number of units moved by the player during their turn. --Sorry for touching your code! -Travis
        //shouldn't be necessary, turns are states - Margaret

       /* StandardUnit pi1;
        StandardUnit pi2;
        StandardUnit pi3;
        StandardUnit pi4;
        StandardUnit pi5; */

        #endregion

        #region constructor
        public Player(string nm, int playerNumber)
        {
            name = nm;
            playerNum = playerNumber;

        }
        public Player(string nm, int playerNumber, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, int x5, int y5)
        {
            name = nm;
            playerNum = playerNumber;
            /*
            pi1 = new StandardUnit(x1,y1,this);
            pi2 = new StandardUnit(x2,y2,this);
            pi3 = new StandardUnit(x2,y2,this);
            pi4 = new StandardUnit(x2,y2,this);
            pi5 = new StandardUnit(x2,y2,this);
             */

        }
        #endregion

        public int ID
        {
            get
            {
                return playerNum;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
        }

    }
}
