using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day_06
    {
        public static void Day_06_Part01()
        {
            string importString = Import.ImportString("Day_06.txt");

            char[] input = importString.ToCharArray();
            int counter = 0;
            List<char> lastFour = new List<char>();
            bool answerFound = false;


            foreach (char c in input)
            {

                lastFour.Add(c);
                counter++;

                if (lastFour.Count >= 4)
                {
                    if (lastFour.Count > 4)
                    {
                        lastFour.RemoveAt(0);

                    }

                    if (lastFour[0] != lastFour[1] && lastFour[0] != lastFour[2] && lastFour[0] != lastFour[3] && lastFour[1] != lastFour[2] && lastFour[1] != lastFour[3] && lastFour[2] != lastFour[3] && !answerFound)
                    {
                        Console.WriteLine("the first start of packet marker ends at position: " + counter);
                        answerFound = true;
                    }
                }

            }
        }

        public static void Day_06_Part02()
        {
            string importString = Import.ImportString("Day_06.txt");

            char[] input = importString.ToCharArray();

            for (int i = 0; i < input.Length - 15; i++)
            {
                List<char> lastFourteen = new List<char>();
                for (int j = i; j < i + 14; j++)
                {

                    if (!lastFourteen.Contains(input[j]))
                    {
                        lastFourteen.Add(input[j]);
                        if (lastFourteen.Count == 14)
                        {
                            Console.WriteLine("Communications packet found at " + (j+1));
                            i = input.Length;
                        }
                    }
                    else
                    {
                        j = i + 15;

                    }

                    

                }
            }

        }
    }
}

