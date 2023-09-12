using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day_10
    {
        public static void Day_10_Part01()
        {

            string importString = Import.ImportString("Day_10.txt");
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            int currentLine=0;

            int cycle = 1;
            int signalCounter = 19;
            int x = 1;
            //Instruction currentInstruction;

            int cooldownCounter = -1;
            int currentValue = 0;

            List<int> signalStrengthsList = new List<int>();


            //iterate cycles
            while (cycle < 221)
            {
                if (signalCounter == 0)
                {
                    int signalStrength = x * cycle;
                    signalStrengthsList.Add(signalStrength);
                    signalCounter = 40;
                    Console.WriteLine("Signal Strength at cycle " + cycle + " is " + x * cycle);
                }

                if (cooldownCounter<0 && lines[currentLine] != "noop")
                {
                    string[] tempString = lines[currentLine].Split(' ');
                    currentValue = Int32.Parse(tempString[1]); 
                    currentLine++;
                    cooldownCounter = 1;
                }
                else if(cooldownCounter < 0 && lines[currentLine] == "noop")
                {
                    currentLine++;

                }

                if (cooldownCounter>0)
                {
                    cooldownCounter--;
                }else if(cooldownCounter == 0)
                {
                    x = x + currentValue;
                    cooldownCounter--;
                }

                Console.WriteLine("after cycle " + cycle + " x is: " + x + " current Value is "+currentValue+ " current Cooldown is: "+cooldownCounter);

                

                cycle++;
                signalCounter--;
            }

            int signalSum = 0;
            foreach (int signalStrength in signalStrengthsList)
            {
                signalSum = signalSum + signalStrength;
            }

            Console.WriteLine("The sum of the signal strengths is: "+signalSum);

        }

        public static void Day_10_Part02()
        {

            string importString = Import.ImportString("Day_10.txt");
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            int currentLine = 0;

            int cycle = 1;

            int x = 1;
            //Instruction currentInstruction;

            int cooldownCounter = -1;
            int currentValue = 0;
            char[,] crtRow = new char[6,40];
            int crtPosition = 0;
            int crtRowNumber = 0;

            List<int> signalStrengthsList = new List<int>();

            //iterate cycles
            while (cycle < 241)
            {
                if (crtPosition >= 40)
                {
                    crtPosition = 0;
                    crtRowNumber++;
                }

                //CRT draws pixel in position x, check if crtPosition and spritePosition (x, x-1, x+1) line up
                if (x == crtPosition || x - 1 == crtPosition || x + 1 == crtPosition)
                {
                    crtRow[crtRowNumber, crtPosition] = '#';
                }
                else
                {
                    crtRow[crtRowNumber, crtPosition] = '.';
                }


                if (cooldownCounter < 0 && lines[currentLine] != "noop")
                {
                    string[] tempString = lines[currentLine].Split(' ');
                    currentValue = Int32.Parse(tempString[1]);
                    currentLine++;
                    cooldownCounter = 1;
                }
                else if (cooldownCounter < 0 && lines[currentLine] == "noop")
                {
                    currentLine++;
                }

                if (cooldownCounter > 0)
                {
                    cooldownCounter--;
                }
                else if (cooldownCounter == 0)
                {
                    x = x + currentValue;
                    cooldownCounter--;
                }



                //visualize current CRT row

                cycle++;
                crtPosition++;
            }

            for(int i = 0; i < crtRow.GetLength(0); i++)
            {
                for(int j = 0; j < crtRow.GetLength(1); j++)
                {
                    Console.Write(crtRow[i,j]);
                }
                Console.WriteLine();
            }

        }

        public static Instruction addInstruction(string instruction)
        {
            string[] tempString = instruction.Split(' ');
            int value = Int32.Parse(tempString[1]);
            return new Instruction(value, 2);

        }



    }

    public struct Instruction
    {
        public Instruction(int x, int timer)
        {
            this.x = x;
            this.timer = timer;
        }
        public int x;
        public int timer;

        public override string ToString()
        {
            return "x: " + x + " timer: " + timer;
        }

    }
}
