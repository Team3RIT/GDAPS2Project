using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Margaret
namespace Arbiter
{
    class Player
    {
        #region attributes
        string name; //player name
        int playerNum; //should be 1 to 4, the "player" that owns the immobile pieces will be player 0.

        #endregion

        #region constructor
        public Player(string nm, int playerNumber)
        {
            name = nm;
            playerNum = playerNumber;
        }
        #endregion
    }
}
