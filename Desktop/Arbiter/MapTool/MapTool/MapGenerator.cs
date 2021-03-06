﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MapTool
{
    //Margaret

    //Need weighting algorithm
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
            owner.MapClear(); //clear the board
            if (dim < 20)
            {
                towerCount = (dim-6)/3; //smaller space, less towers.
                if (towerCount % 2 == 0)//keep it odd
                    towerCount++;
            }
            else
            {
                towerCount = rng.Next(5, 10);
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
                                if (owner.nums[midpoint2 + randomx, midpoint2 + randomy] == 0 
                                    && checkSides(new Point(midpoint2 + randomx, midpoint2 + randomy)))
                                {
                                    owner.nums[midpoint2 + randomx, midpoint2 + randomy] = 2;
                                    owner.tiles[midpoint2 + randomx, midpoint2 + randomy].Image = Image.FromFile("../Images/tower.png");
                                    success = true;
                                }
                                break;
                            }
                        case 1: //upper right quadrant (Q1)
                            {
                                if (owner.nums[midpoint1 + midpoint2 + randomx, midpoint2 + randomy] == 0
                                    && checkSides(new Point(midpoint1 + midpoint2 + randomx, midpoint2 + randomy)))
                                {
                                    owner.nums[midpoint1 + midpoint2 + randomx, midpoint2 + randomy] = 2;
                                    owner.tiles[midpoint1 + midpoint2 + randomx, midpoint2 + randomy].Image = Image.FromFile("../Images/tower.png");
                                    success = true;
                                }
                                break;
                            }
                        case 2: //lower right quadrant (Q4)
                            {
                                if (owner.nums[midpoint1 + midpoint2 + randomx, midpoint1 + midpoint2 + randomy] == 0
                                    && checkSides(new Point(midpoint1 + midpoint2 + randomx, midpoint1 + midpoint2 + randomy)))
                                {
                                    owner.nums[midpoint1 + midpoint2 + randomx, midpoint1 + midpoint2 + randomy] = 2;
                                    owner.tiles[midpoint1 + midpoint2 + randomx, midpoint1 + midpoint2 + randomy].Image = Image.FromFile("../Images/tower.png");
                                    success = true;
                                }
                                break;
                            }
                        case 3: //lower left quadrant (Q3)
                            {
                                if (owner.nums[midpoint2 + randomx, midpoint1 + midpoint2 + randomy] == 0
                                    && checkSides(new Point(midpoint2 + randomx, midpoint1 + midpoint2 + randomy)))
                                {
                                    owner.nums[midpoint2 + randomx, midpoint1 + midpoint2 + randomy] = 2;
                                    owner.tiles[midpoint2 + randomx, midpoint1 + midpoint2 + randomy].Image = Image.FromFile("../Images/tower.png");
                                    success = true;
                                }
                                break;
                            }
                    }
                    if (success)
                    {
                        towerCount--; //a tower has been placed, so one less exists

                        if (quadrantCount == 3) //keep quadrant looping
                            quadrantCount = 0;
                        else
                            quadrantCount++;
                    }

                    success = false;
                }
                
            

            

        }

        public static bool checkSides(Point location)
        {
            if (location.X > 2 && //check these first so no out of range exceptions
                location.Y > 2 &&
                location.X < owner.nums.GetLength(0) - 3 &&
                location.Y < owner.nums.GetLength(0) - 3 &&
                owner.nums[location.X + 1, location.Y + 1] == 0 && //checks alllllll the tiles around, makes sure it's not surrounded by anything
                owner.nums[location.X + 1, location.Y] == 0 &&
                owner.nums[location.X - 1, location.Y] == 0 &&
                owner.nums[location.X, location.Y + 1] == 0 &&
                owner.nums[location.X, location.Y - 1] == 0 &&
                owner.nums[location.X - 1, location.Y + 1] == 0 &&
                owner.nums[location.X - 1, location.Y - 1] == 0 &&
                owner.nums[location.X + 1, location.Y - 1] == 0 
               )
                return true;
            else
                return false;
        }
    }
}
