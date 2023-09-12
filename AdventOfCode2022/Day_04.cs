using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day_04
    {
        public static void Day_04_Part01()
        {
            string importString = Import.ImportString("Day_04.txt");
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            int fullyContainedPairs = 0;

            foreach (string line in lines)
            {
                string[] inputs = line.Split(',');
                string[] assignment1 = inputs[0].Split('-');
                string[] assignment2 = inputs[1].Split('-');
                List<int> sections1 = new List<int>();
                List<int> sections2 = new List<int>();


                for (int i=Int32.Parse(assignment1[0]); i<= Int32.Parse(assignment1[1]); i++)
                {
                    sections1.Add(i);

                }

                for (int i = Int32.Parse(assignment2[0]); i <= Int32.Parse(assignment2[1]); i++)
                {
                    sections2.Add(i);

                }
                
                bool running = true;
                foreach (int section in sections2)
                {
                    if (running ==true && !sections1.Contains(section))
                    {
                        running = false;
                    }
                }
                if (running == true)
                {
                    fullyContainedPairs++;
                }
                else
                {
                    running = true;
                    foreach (int section in sections1)
                    {
                        if (running == true && !sections2.Contains(section))
                        {
                            running = false;
                        }
                    }
                    if(running == true)
                    {
                        fullyContainedPairs++;
                    }
                }

                
            }
            Console.WriteLine("The number of Pairs where one range fully covers the other is: " + fullyContainedPairs);
        }

        public static void Day_04_Part02()
        {
            string importString = Import.ImportString("Day_04.txt");
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            int overlappingPairs = 0;

            foreach (string line in lines)
            {
                string[] inputs = line.Split(',');
                string[] assignment1 = inputs[0].Split('-');
                string[] assignment2 = inputs[1].Split('-');
                List<int> sections1 = new List<int>();
                List<int> sections2 = new List<int>();


                for (int i = Int32.Parse(assignment1[0]); i <= Int32.Parse(assignment1[1]); i++)
                {
                    sections1.Add(i);

                }

                for (int i = Int32.Parse(assignment2[0]); i <= Int32.Parse(assignment2[1]); i++)
                {
                    sections2.Add(i);

                }

                //To change:
                bool overlap = false;
                foreach (int section in sections2)
                {
                    if (overlap == false && sections1.Contains(section))
                    {
                        overlap = true;
                    }
                }
                if (overlap == true)
                {
                    overlappingPairs++;
                }
                else
                {
                    foreach (int section in sections1)
                    {
                        if (overlap == false && sections2.Contains(section))
                        {
                            overlap = true;
                        }
                    }
                    if (overlap == true)
                    {
                        overlappingPairs++;
                    }
                }


            }
            Console.WriteLine("The number of Pairs that overlap is: " + overlappingPairs);
        }
    }
}

