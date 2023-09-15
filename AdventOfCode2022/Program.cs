using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AdventOfCode2022
{
    class Program
    {
        static void Main(string[] args)
        {
            //don't forget to also change the input file location in ImportString.cs, when generating solutions for other days
            string userInput=requestDay();
            executeDay(userInput);
            Console.ReadLine();

            string requestDay()
            {
                Console.WriteLine("Which day would you like to generate an answer for?");

                return Console.ReadLine();
            }

            void executeDay(string input)
            {
                int day = Int32.Parse(input);

                switch (day)
                {
                    case 1:
                        Day01.Day_01_Part01();
                        executeDay(requestDay());
                        break;

                    case 2:
                        Day02.Day_02_Part01();
                        Day02.Day_02_Part02();
                        executeDay(requestDay());
                        break;

                    case 3:
                        Day_03.Day_03_Part01();
                        Day_03.Day_03_Part02();
                        executeDay(requestDay());
                        break;

                    case 4:
                        Day_04.Day_04_Part01();
                        Day_04.Day_04_Part02();
                        executeDay(requestDay());
                        break;

                    case 5:
                        Day_05.Day_05_Part01();
                        Day_05.Day_05_Part02();
                        executeDay(requestDay());
                        break;

                    case 6:
                        Day_06.Day_06_Part01();
                        Day_06.Day_06_Part02();
                        executeDay(requestDay());
                        break;

                    case 7:
                        Day_07.Day_07_Part01();
                        executeDay(requestDay());
                        break;

                    case 8:
                        Day_08.Day_08_Part01();
                        executeDay(requestDay());
                        break;

                    case 9:
                        Day_09.Day_09_Part01();
                        Day_09.Day_09_Part02();
                        executeDay(requestDay());
                        break;

                    case 10:
                        Day_10.Day_10_Part01();
                        Day_10.Day_10_Part02();
                        executeDay(requestDay());
                        break;

                    case 11:
                        Day_11.Day_11_Part01();
                        Day_11.Day_11_Part02();
                        executeDay(requestDay());
                        break;

                    case 12:
                        Day_12.Day_12_Part01();
                        executeDay(requestDay());
                        break;

                    case 13:
                        Day_13.Day_13_Part01();
                        executeDay(requestDay());
                        break;

                    case 14:
                        Day_14.Day_14_Part01();
                        Day_14.Day_14_Part02();
                        executeDay(requestDay());
                        break;

                    case 16:
                        Day_16.Day_16_Part01();
                        executeDay(requestDay());
                        break;

                    case 17:
                        Day_17.Day_17_Part01();
                        executeDay(requestDay());
                        break;

                    case 18:
                        Day_18.Day_18_Part01();
                        executeDay(requestDay());
                        break;

                    case 21:
                        Day_21.Day_21_Part01();
                        executeDay(requestDay());
                        break;


                    default:
                        Console.WriteLine("There is no answer for day "+input+" yet");
                        executeDay(requestDay());
                        break;
                }
            }


        }
         

    }


}
