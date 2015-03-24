using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MapTool
{
    //Margaret

    //STILL NEEDS A SHIT TON OF WORK
    //
    public static class MapGenerator
    {
        public static Random rng = new Random();
        //for weighting the random number generator results
        
        private static int towerCount;
        private static int structureCount;
        private static int midpoint1;
        private static int midpoint2;
        static MainForm owner;
        public static void GenerateMap(int dim, MainForm own)
        {
            
            owner = own;
            if (dim < 20)
            {
                towerCount = rng.Next(5, 8); //smaller space, less towers.
                if (towerCount % 2 == 0)//keep it odd
                    towerCount++;
            }
            else
            {
                towerCount = rng.Next(5, 12);
                if (towerCount % 2 == 0)
                    towerCount++;
            }
            if (dim < 20)
            {
                structureCount = rng.Next(5, 16); //smaller space, less structures.
                
            }
            else
            {
                structureCount = rng.Next(5, 20);
                
            }

            int randomx;
            int randomy;
            int quadrantCount = 0; //keeps track of the quadrant. Q2 = 0, Q1 = 1, Q4 = 2, Q3 = 3
            bool success = false; //if a tower was put down successfully
                midpoint1 = dim / 2;
                midpoint2 = dim / 4;

                while(towerCount > 0 ) //do the towers first
                {
                    randomx = rng.Next(-midpoint2, midpoint2);
                    randomy = rng.Next(-midpoint2, midpoint2);
                    
                    switch(quadrantCount)
                    {
                        case 0: //upper left quadrant (Q2)
                            {
                                if (owner.nums[midpoint2 + randomx, midpoint2 + randomy] == 0)
                                {
                                    owner.nums[midpoint2 + randomx, midpoint2 + randomy] = 2;
                                    owner.tiles[midpoint2 + randomx, midpoint2 + randomy].Image = Image.FromFile("../Images/tower.png");
                                    success = true;
                                }
                                break;
                            }
                        case 1: //upper right quadrant (Q1)
                            {
                                if (owner.nums[midpoint1 + midpoint2 + randomx, midpoint2 + randomy] == 0)
                                {
                                    owner.nums[midpoint1 + midpoint2 + randomx, midpoint2 + randomy] = 2;
                                    owner.tiles[midpoint1 + midpoint2 + randomx, midpoint2 + randomy].Image = Image.FromFile("../Images/tower.png");
                                    success = true;
                                }
                                break;
                            }
                        case 2: //lower right quadrant (Q4)
                            {
                                if (owner.nums[midpoint1 + midpoint2 + randomx, midpoint1 + midpoint2 + randomy] == 0)
                                {
                                    owner.nums[midpoint1 + midpoint2 + randomx, midpoint1 + midpoint2 + randomy] = 2;
                                    owner.tiles[midpoint1 + midpoint2 + randomx, midpoint1 + midpoint2 + randomy].Image = Image.FromFile("../Images/tower.png");
                                    success = true;
                                }
                                break;
                            }
                        case 3: //lower left quadrant (Q3)
                            {
                                if (owner.nums[midpoint2 + randomx, midpoint1 + midpoint2 + randomy] == 0)
                                {
                                    owner.nums[midpoint2 + randomx, midpoint1 + midpoint2 + randomy] = 2;
                                    owner.tiles[midpoint2 + randomx, midpoint1 + midpoint2 + randomy].Image = Image.FromFile("../Images/tower.png");
                                    success = true;
                                }
                                break;
                            }
                    }
                    if(success)
                    towerCount--; //a tower has been placed, so one less exists

                    if(quadrantCount == 3) //keep quadrant looping
                        quadrantCount = 0;
                    else
                        quadrantCount++;

                    success = false;
                }
                
            



        }
    }
}
