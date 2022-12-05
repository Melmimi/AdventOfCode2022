using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day_05
    {
        public static void Day_05_Part01()
        {
            string importString = Import.ImportString();
            string[] inputs = importString.Split('-');

            string[] startingStackLines = inputs[0].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string[] instructionsLines = inputs[1].Split(new string[] { Environment.NewLine },StringSplitOptions.None);

            List<List<char>> stacksLists = new List<List<char>>();


            int rowsCounter = 0;
            for (int i= startingStackLines.Length-1; i >= 0; i--)
            {
                char[] linesSplit = startingStackLines[i].ToCharArray();
                if (startingStackLines[i].Contains('['))
                {
                    int columnsCounter = 0;
                    for (int j =1; j<linesSplit.Length; j = j + 4)
                    {
                        if(linesSplit[j]!=' ')
                        {
                            stacksLists[columnsCounter].Add(linesSplit[j]);
                            
                        }
                        columnsCounter++;
                    }
                    rowsCounter++;
                }
                else
                {
                    foreach (char character in linesSplit)
                    {
                        if(character!= ' ')
                        {
                            stacksLists.Add(new List<char>());
                        }
                    }

                }

            }

            foreach (List<char> row in stacksLists)
            {
                foreach (char c in row)
                {
                    Console.Write(c.ToString());
                }
                Console.WriteLine();
            }

            foreach (string line in instructionsLines)
            {
                if (line != "") { 
                    Console.WriteLine(line);
                    int[] instructions = Array.ConvertAll(line.Split(new string[] { "move ", " from ", " to " }, StringSplitOptions.RemoveEmptyEntries), s => int.Parse(s));

                    int amount = instructions[0];
                    int origin = instructions[1]-1;
                    int goal = instructions[2]-1;

                    Console.WriteLine("amount: " + amount + ", origin: " + origin + ", goal: " + goal);
                        
                    for (int i = amount; i > 0; i--)
                    {

                        stacksLists[goal].Add(stacksLists[origin][stacksLists[origin].Count-1]);
                        stacksLists[origin].RemoveAt(stacksLists[origin].Count - 1);

                        

                    }

                    foreach (List<char> row in stacksLists)
                    {
                        foreach (char c in row)
                        {
                            Console.Write(c.ToString());
                        }
                        Console.WriteLine();
                    }
                }
                
            }

            for (int i=0; i<9; i++)
            {
                Console.WriteLine("Last box in " + i + " is " + stacksLists[i][stacksLists[i].Count - 1]);
            }
            /*
            Console.WriteLine("Last box in 1 is:" + stacksLists[0][stacksLists[0].Count - 1]);
            Console.WriteLine("Last box in 2 is:" + stacksLists[1][stacksLists[1].Count - 1]);
            Console.WriteLine("Last box in 3 is:" + stacksLists[2][stacksLists[2].Count - 1]);
            */


            //Console.WriteLine("Ausgangssituation: " + inputs[0]);
            //Console.WriteLine("crane instructions: " + inputs[1]);

        }
        public static void Day_05_Part02()
        {
            string importString = Import.ImportString();
            string[] inputs = importString.Split('-');

            string[] startingStackLines = inputs[0].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string[] instructionsLines = inputs[1].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            List<List<char>> stacksLists = new List<List<char>>();


            int rowsCounter = 0;
            for (int i = startingStackLines.Length - 1; i >= 0; i--)
            {
                char[] linesSplit = startingStackLines[i].ToCharArray();
                if (startingStackLines[i].Contains('['))
                {
                    int columnsCounter = 0;
                    for (int j = 1; j < linesSplit.Length; j = j + 4)
                    {
                        if (linesSplit[j] != ' ')
                        {
                            stacksLists[columnsCounter].Add(linesSplit[j]);

                        }
                        columnsCounter++;
                    }
                    rowsCounter++;
                }
                else
                {
                    foreach (char character in linesSplit)
                    {
                        if (character != ' ')
                        {
                            stacksLists.Add(new List<char>());
                        }
                    }

                }

            }

            foreach (List<char> row in stacksLists)
            {
                foreach (char c in row)
                {
                    Console.Write(c.ToString());
                }
                Console.WriteLine();
            }

            foreach (string line in instructionsLines)
            {
                if (line != "")
                {
                    Console.WriteLine(line);
                    int[] instructions = Array.ConvertAll(line.Split(new string[] { "move ", " from ", " to " }, StringSplitOptions.RemoveEmptyEntries), s => int.Parse(s));

                    int amount = instructions[0];
                    int origin = instructions[1] - 1;
                    int goal = instructions[2] - 1;

                    Console.WriteLine("amount: " + amount + ", origin: " + origin + ", goal: " + goal);
                    
                    

                    //This part needs to change
                    for (int i = amount; i > 0; i--)
                    {
                        stacksLists[goal].Add(stacksLists[origin][stacksLists[origin].Count - i]);
                    }
                    for (int i = amount; i > 0; i--)
                    {
                        stacksLists[origin].RemoveAt(stacksLists[origin].Count - 1);

                    }



                    // until here
                    foreach (List<char> row in stacksLists)
                    {
                        foreach (char c in row)
                        {
                            Console.Write(c.ToString());
                        }
                        Console.WriteLine();
                    }
                }

            }

            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine("Last box in " + i + " is " + stacksLists[i][stacksLists[i].Count - 1]);
            }

        }
    }
}
