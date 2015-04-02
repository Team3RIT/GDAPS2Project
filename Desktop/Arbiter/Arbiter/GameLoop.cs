using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arbiter
{
    //Joshua Buettner
    // this class contains the game loop that will control the state of the game as well as create the variables for in game
    class GameLoop
    {
        Tower t1 = new Tower(5,8);
        Tower t2 = new Tower(10, 16);
        Tower t3 = new Tower(6, 87);
        Tower t4 = new Tower(4, 5);
        Tower t5 = new Tower(7, 8);
        private int counter = 0;
        private int playerNum;
        private bool hasWon;
        private string winner;
        private string name1 = "p1";
        private string name2 = "p2";
        private bool isOver = false;
        Player p1;
        Player p2;

        // constructor
        public GameLoop()
        {
            p1 = new Player(name1,1,1,1,2,2,3,3,4,4,5,5);
            p2 = new Player(name2,2,1,1,2,2,3,3,4,4,5,5);
        }
        public void Loop()
        {
            while(isOver == false)
            {
                while(counter == 0)
                {
                    //if(// insert code that allows selection)
                }
            }
        }
    }
}
