using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day_08
    {

        public static void Day_08_Part01()
        {
            string importString = Import.ImportString("Day_08.txt");
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            int[,] treeGrid = new int[lines[0].Length,lines.Length];
            bool[,] visibilityGrid = new bool[lines[0].Length, lines.Length];


            int counterX = 0;

            foreach (string line in lines)
            {
                //Console.WriteLine("current Line: "+line);
                int counterY = 0;
                char[] tempCharArr = line.ToCharArray();
                foreach(Char c in tempCharArr)
                {
                    //Console.WriteLine("current Char "+c.ToString());
                    treeGrid[counterX, counterY] = (int)char.GetNumericValue(c);
                    //Console.WriteLine("current TreeGrid position: "+treeGrid[counterX,counterY]);
                    counterY++;
                }
                counterX++;

            }



            for (int x =0; x<treeGrid.GetLength(0); x++)
            {
                for(int y=0; y<treeGrid.GetLength(1); y++)
                {
                    Console.Write(treeGrid[x,y]);

                }
                Console.WriteLine();
            }
            // go through tree grid and populate visibilty grid
            for (int x = 0; x < treeGrid.GetLength(0); x++)
            {
                for (int y = 0; y < treeGrid.GetLength(1); y++)
                {
                    bool visibleLeft = true;
                    bool visibleTop = true;
                    bool visibleRight = true;
                    bool visibleDown = true;
                    int currentHeight = treeGrid[x, y];
                    if (x-1>= 0)
                    {
                        //can we go left? if yes iterate through all trees to the left
                        for(int i =x-1; i >= 0; i--)
                        {
                            if (currentHeight <= treeGrid[i, y])
                            {
                                visibleLeft = false;
                                //Console.WriteLine("found a tree tall enough left");
                            }
                        }
                    }
                    if (x + 1 <= treeGrid.GetLength(0))
                    {
                        //Console.WriteLine("Can we go right? x is "+x+" Length 0 is"+treeGrid.GetLength(0));
                        
                        //can we go right? if yes iterate through all trees on the right 
                        for (int i = x+1; i < treeGrid.GetLength(0); i++)
                        {
                            //Console.WriteLine("made it to the right");
                            if (currentHeight <= treeGrid[i, y])
                            {
                                visibleRight = false;
                               // Console.WriteLine("found a tree tall enough right");
                            }
                        }
                    }
                    if(y-1 >= 0)
                    {
                        // can we go up? if yes iterate through all tree to the top
                        for (int i = y-1; i >= 0; i--)
                        {
                            if (currentHeight <= treeGrid[x, i])
                            {
                                visibleTop = false;
                            }
                        }
                    }
                    if (y + 1 <= treeGrid.GetLength(1))
                    {
                        // can we go down? if yes iterate through all trees to the south
                        for (int i = y+1; i < treeGrid.GetLength(1); i++)
                        {
                            //Console.WriteLine("made it down");
                            if (currentHeight <= treeGrid[x, i])
                            {
                                visibleDown = false;
                                //Console.WriteLine("found a tree tall enough down");
                            }
                        }
                    }
                   // Console.WriteLine("l: "+visibleLeft + " u: "+visibleTop+ " r: "+visibleRight + " d: "+visibleDown);
                    if(visibleLeft == false && visibleTop==false && visibleRight==false && visibleDown==false )
                    {
                        visibilityGrid[x, y] = false;
                    }
                    else
                    {
                        visibilityGrid[x, y] = true;
                    }
                }
                Console.WriteLine();
            }

            //visualize visibility Grid 
            for (int x = 0; x < visibilityGrid.GetLength(0); x++)
            {
                for (int y = 0; y < visibilityGrid.GetLength(1); y++)
                {
                    Console.Write(visibilityGrid[x, y]);

                }
                Console.WriteLine();
            }

            //count the trees but disregard the outer perimeter
            int hiddenTreeCounter = 0;
            for (int x=1; x<visibilityGrid.GetLength(0)-1; x++)
            {
                for (int y=1; y < visibilityGrid.GetLength(1)-1; y++)
                {
                    if (!visibilityGrid[x, y])
                    {
                        hiddenTreeCounter++;
                    }
                }
            }
            Console.WriteLine("The number of hidden trees is "+hiddenTreeCounter);
            int visibleTrees = treeGrid.GetLength(0) * treeGrid.GetLength(1) - hiddenTreeCounter;
            Console.WriteLine("The amount of visible trees is: "+ visibleTrees);

            //Part 02:
            int[,] scenicScore = new int[treeGrid.GetLength(0),treeGrid.GetLength(1)];

            for(int x=0 ; x < scenicScore.GetLength(0); x++)
            {
                for (int y=0 ; y<scenicScore.GetLength(1); y++)
                {
                    //any variables we might need go here
                    int currentHeight = treeGrid[x, y];
                    int treeCounterWest = 0;
                    int treeCounterNorth = 0;
                    int treeCounterEast = 0;
                    int treeCounterSouth = 0;
                    int currentScenicScore = 0;

                    //Can we go left?
                    if (x - 1 >= 0)
                    {
                        bool stopped = false;
                        for (int i = x - 1; i >= 0; i--)
                        {
                            if (currentHeight > treeGrid[i, y] && stopped == false)
                            {
                                treeCounterWest++;
                            }
                            else if(currentHeight <= treeGrid[i,y] && stopped==false)
                            {
                                treeCounterWest++;
                                stopped = true;
                            }
                        }
                    }
                    //Can we go up?
                    if (y - 1 >= 0)
                    {
                        bool stopped = false;
                        for (int i = y - 1; i >= 0; i--)
                        {
                            if (currentHeight > treeGrid[x, i] && stopped == false)
                            {
                                treeCounterNorth++;
                            }
                            else if (currentHeight <= treeGrid[x, i] && stopped == false)
                            {
                                treeCounterNorth++;
                                stopped = true;
                            }
                        }
                    }

                    //Can we go right?
                    if (x + 1 <= treeGrid.GetLength(0))
                    {
                        bool stopped = false;
                        for (int i = x + 1; i < treeGrid.GetLength(0); i++)
                        {
                            if (currentHeight > treeGrid[i, y] && stopped == false)
                            {
                                treeCounterEast++;
                            }
                            else if (currentHeight <= treeGrid[i, y] && stopped == false)
                            {
                                treeCounterEast++;
                                stopped = true;
                            }
                        }
                    }

                    //Can we go down?
                    if (y + 1 <= treeGrid.GetLength(1))
                    {
                        bool stopped = false;
                        for (int i = y + 1; i < treeGrid.GetLength(0); i++)
                        {
                            if (currentHeight > treeGrid[x, i] && stopped == false)
                            {
                                treeCounterSouth++;
                            }
                            else if (currentHeight <= treeGrid[x, i] && stopped == false)
                            {
                                treeCounterSouth++;
                                stopped = true;
                            }
                        }
                    }

                    currentScenicScore = treeCounterEast * treeCounterNorth * treeCounterWest * treeCounterSouth;
                    scenicScore[x, y] = currentScenicScore;
                    //Console.WriteLine("current SCenic SCore: " + currentScenicScore);
                }
            }

            int highestScenicScore = 0;
            for(int x = 0; x < scenicScore.GetLength(0); x++)
            {
                for (int y = 0; y < scenicScore.GetLength(1); y++)
                {
                    if (scenicScore[x, y] > highestScenicScore)
                    {
                        highestScenicScore = scenicScore[x, y];
                    }

                }
            }

            Console.WriteLine("highest possible scenic score is "+highestScenicScore);
        }
    }
}
