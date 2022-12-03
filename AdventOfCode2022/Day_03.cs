using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day_03
    {
        public static void Day_03_Part01()
        {
            string importString = Import.ImportString();
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            int totalPriorities = 0;

            foreach(string line in lines)
            {

                char[] charArray1 = line.Substring(0, line.Length / 2).ToCharArray();
                char[] charArray2 = line.Substring(line.Length / 2).ToCharArray();

                IEnumerable<char> both = charArray1.Intersect(charArray2);



                foreach(char character in both)
                {
                    if (Char.IsUpper(character))
                    {
                        totalPriorities = totalPriorities + Convert.ToInt32(character) - 38;
                    }
                    else
                    {
                        totalPriorities = totalPriorities + Convert.ToInt32(character) - 96;
                    }
                    
                }

            }

            Console.WriteLine("The total sum of Prioritites is: " + totalPriorities);
        }

        public static void Day_03_Part02()
        {

            string importString = Import.ImportString();
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            int totalPriorities = 0;

            for (int i=0; i < lines.Length ; i=i+3)
            {
                char[] charArray1 = lines[i].ToCharArray();
                char[] charArray2 = lines[i + 1].ToCharArray();
                char[] charArray3 = lines[i + 2].ToCharArray();

                IEnumerable<char> intersection = charArray1.Intersect(charArray2.Intersect(charArray3));

                foreach(char badge in intersection)
                {

                    if (Char.IsUpper(badge))
                    {
                        totalPriorities = totalPriorities + Convert.ToInt32(badge) - 38;
                    }
                    else
                    {
                        totalPriorities = totalPriorities + Convert.ToInt32(badge) - 96;
                    }

                }
                
            }

            Console.WriteLine("The total Priorities value for Day 3 Part 2 is "+totalPriorities);

        }
    }
}
