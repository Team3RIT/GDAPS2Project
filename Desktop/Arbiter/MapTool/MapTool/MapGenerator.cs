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
    //will augment with midpoint displacement and logic later. Right now it's ultra random
    public static class MapGenerator
    {
        public static Random rng = new Random();
        //for weighting the random number generator results
        private static int totalWeight; // range of rng
        private static int towerWeight;
        private static int structureWeight;
        private static int towerCount;
        private static int structureCount;
        static MainForm owner;
        public static void GenerateMap(int dim, MainForm own)
        {
            int randomNum;
            owner = own;
            if (dim < 20)
            {
                towerCount = rng.Next(5, 8); //smaller space, less towers.
            }
            else
            {
                towerCount = rng.Next(5, 12);
            }
            if (dim < 20)
            {
                structureCount = rng.Next(5, 16); //smaller space, less structures.
            }
            else
            {
                structureCount = rng.Next(5, 20);
            }

            totalWeight = (int)Math.Pow((double)dim, 2.0); //the casting won't lose data, square of an int is an int
            
            for(int i = 0; i < dim; i++)
            {
                for(int j = 0; j < dim; j++)
                {
                    structureWeight = towerCount + structureCount; //keep the weighting accurate
                    towerWeight = towerCount;
                    randomNum = rng.Next(0, totalWeight + 1);

                    if(randomNum < towerWeight && towerCount != 0)
                    {
                        owner.nums[i, j] = 2;
                        owner.tiles[i, j].Image = Image.FromFile("../Images/tower.png");
                        towerCount--;
                    }
                    else if(randomNum < structureWeight && structureCount != 0)
                    {
                        owner.nums[i, j] = 1;
                        owner.tiles[i, j].Image = Image.FromFile("../Images/structure.jpg");
                        structureCount--;
                    }
                    else
                    {
                        owner.nums[i, j] = 0;
                        owner.tiles[i, j].Image = null;
                    }
                    totalWeight--;
                }
            }



        }
    }
}
