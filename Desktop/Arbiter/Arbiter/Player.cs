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
