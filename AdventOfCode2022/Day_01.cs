using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day01
    {
        public static void Day_01_Part01()
        {
            List<int> calorieList = new List<int>();
            int index = 0;
            string importString = Import.ImportString();
            Console.WriteLine(importString);

            string[] lines = importString.Split(new string[] { Environment.NewLine },StringSplitOptions.None);
            calorieList.Add(0);

            foreach(string line in lines)
            {
                if(line == "")
                {
                    index++;
                    calorieList.Add(0);
                }
                else
                {
                    calorieList[index] = calorieList[index] + Int32.Parse(line);
                    Console.WriteLine("Elf "+index+ " carries "+calorieList[index]+" Calories.");
                }

            }

            int max = calorieList.Max();
            Console.WriteLine("Part01: The elf with the most calories is carrying " + max + " calories.");

            List<int> descendingCalories = new List<int>(calorieList.OrderByDescending(i => i));

            int topThree = 0;
            for (int i = 0; i < 3; i++)
            {
                topThree = topThree + descendingCalories[i];
            }

            Console.WriteLine("Part02: The top three elves are carrying " + topThree + " total calories");
        }
        
    }
}
