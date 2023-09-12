using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day02
    {
        public static void Day_02_Part01()
        {
            string importString = Import.ImportString("Day_02.txt");
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            int totalScore = 0;

            foreach (string line in lines)
            {
                string[] picks = line.Split(' ');
                //Console.WriteLine(picks[0] + " " + picks[1]);

                if (picks[0] == "A" && picks[1] == "X")
                {
                    totalScore = totalScore + 3+1;
                }else if(picks[0]=="A"&& picks[1] == "Y")
                {
                    totalScore = totalScore + 6 + 2;
                }
                else if(picks[0]=="A"&& picks[1] == "Z")
                {
                    totalScore = totalScore + 0 + 3;
                }
                else if(picks[0] == "B" && picks[1] == "X")
                {
                    totalScore = totalScore + 0 + 1;
                }else if (picks[0] == "B" && picks[1] == "Y")
                {
                    totalScore = totalScore + 3 + 2;
                }else if (picks[0] == "B" && picks[1] == "Z")
                {
                    totalScore = totalScore + 6 + 3;
                }else if (picks[0] == "C" && picks[1] == "X")
                {
                    totalScore = totalScore + 6 + 1;
                }
                else if (picks[0] == "C" && picks[1] == "Y")
                {
                    totalScore = totalScore + 0 + 2;
                }else if (picks[0] == "C" && picks[1] == "Z")
                {
                    totalScore = totalScore + 3 + 3;
                }

            }

            Console.WriteLine("Day 2 Part 1: totalScore is " + totalScore);
        }
        public static void Day_02_Part02()
        {
            string importString = Import.ImportString("Day_02.txt");
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            int totalScore = 0;

            foreach(string line in lines)
            {
                string[] picks = line.Split(' ');

                
                if (picks[1] == "X")
                {
                    if (picks[0] == "A")
                    {
                        totalScore = totalScore + 3;
                    }else if (picks[0] == "B")
                    {
                        totalScore = totalScore + 1;
                    }else if (picks[0] == "C")
                    {
                        totalScore = totalScore + 2;
                    }
                }
                else if (picks[1] == "Y")
                {
                    totalScore = totalScore + 3;
                    if (picks[0] == "A")
                    {
                        totalScore = totalScore + 1;
                    }
                    else if (picks[0] == "B")
                    {
                        totalScore = totalScore + 2;
                    }
                    else if (picks[0] == "C")
                    {
                        totalScore = totalScore + 3;
                    }

                }
                else if (picks[1] == "Z")
                {

                    totalScore = totalScore + 6;
                    if (picks[0] == "A")
                    {
                        totalScore = totalScore + 2;
                    }
                    else if (picks[0] == "B")
                    {
                        totalScore = totalScore + 3;
                    }
                    else if (picks[0] == "C")
                    {
                        totalScore = totalScore + 1;
                    }

                }


            }

            Console.WriteLine("Day 2 Part 2: totalScore is " +totalScore);
        }
    }
}
