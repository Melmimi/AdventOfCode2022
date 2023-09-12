using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day_11
    {
        public static void Day_11_Part01()
        {
            string importString = Import.ImportString("Day_11.txt");
            string[] monkeys = importString.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None);
            List<Monkey> monkeyList = new List<Monkey>();

            //Initialise the monkeys
            foreach (string monkeystring in monkeys)
            {
                Monkey monkey = new Monkey();
                monkey.heldItems = new List<int>();
                monkey.inspectionCounter = 0;
                string[] lines = monkeystring.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                // save the monkeys ID just in case
                string[] monkeyIDString = lines[0].Split(' ',':');
                monkey.monkeyID = Int32.Parse(monkeyIDString[1]);

                //get the starting Items from the Input and add them to the current monkey held Items list
                string[] startingItemsString = lines[1].Split(':');
                startingItemsString = startingItemsString[1].Split(',');

                foreach (string s in startingItemsString)
                {
                    int itemInt = Int32.Parse(s);
                    monkey.heldItems.Add(itemInt);
                }

                //determine operation type and save it in the current monkey as well as the int by which the operation is perormed
                string[] operationString = lines[2].Split(' ');
                //OperationType currentOperationType;
                if (operationString[6] == "*" &&operationString[7]=="old")
                {
                    monkey.operationType = OperationType.quadration;
                }
                if (operationString[6] == "*" && operationString[7] != "old")
                {
                    monkey.operationType = OperationType.multiplication;
                    monkey.operationInt = Int32.Parse(operationString[7]);
                }
                if (operationString[6] == "+")
                {
                    monkey.operationType = OperationType.addition;
                    monkey.operationInt = Int32.Parse(operationString[7]);
                }

                //something with the test , possibly also needs to be saved in the monkey
                string[]divisionIntString= lines[3].Split(' ');
                monkey.divisionInt = Int32.Parse(divisionIntString[divisionIntString.Length-1]);


                // definetly need to save what monkey to throw to as well as what number we are trying to divide by
                string[] trueMonkeyString = lines[4].Split(' ');
                monkey.trueMonkeyID = Int32.Parse(trueMonkeyString[trueMonkeyString.Length - 1]);
                

                string[] falseMonkeySring = lines[5].Split(' ');
                monkey.falseMonkeyID = Int32.Parse(falseMonkeySring[falseMonkeySring.Length - 1]);


                monkeyList.Add(monkey);

            }

            //set the true and false monkey references

            foreach(Monkey monkey in monkeyList)
            {
                monkey.falseMonkey = monkeyList[monkey.falseMonkeyID];
                monkey.trueMonkey = monkeyList[monkey.trueMonkeyID];
            }

            //debug print starting items per monkey
            /*
            foreach (Monkey monkey in monkeyList)
            {
                Console.WriteLine("Monkey: " + monkey.monkeyID);
                Console.Write("starting items: ");
                foreach(long item in monkey.heldItems)
                {
                    Console.Write(item + ",");
                }
                Console.WriteLine();
                Console.WriteLine("Operation Type is " + monkey.operationType);
                Console.WriteLine("Operation int is: "+monkey.operationInt);
                Console.WriteLine("Test division is by " + monkey.divisionInt);
                Console.WriteLine("true monkey is. " + monkey.trueMonkeyID);
                Console.WriteLine("false monkey is. " + monkey.falseMonkeyID);

            }
            */


            //simulate 20 rounds
            for(int round =0;round<20; round++)
            {
                foreach(Monkey monkey in monkeyList)
                {

                    monkey.inspect();
                    //Console.WriteLine(" ");
                }

                
                
            }

            /*
            foreach (Monkey monkey in monkeyList)
            {
                foreach (int item in monkey.heldItems)
                {
                    Console.Write(item + ",");
                }
                Console.WriteLine();
            }
            */

            List<int> inspectionCounterList = new List<int>();
            foreach (Monkey monkey in monkeyList)
            {
                //Console.WriteLine("monkey "+monkey.monkeyID+" inspected "+monkey.inspectionCounter+" times");
                inspectionCounterList.Add(monkey.inspectionCounter);
            }

            inspectionCounterList = inspectionCounterList.OrderByDescending(i => i).ToList();

            Console.WriteLine("The two most active monkeys had an inspectionCount of "+inspectionCounterList[0]+ " and " +inspectionCounterList[1]);
            Console.WriteLine("This equates to a total value of "+inspectionCounterList[0]*inspectionCounterList[1]+" monkey buisness units");




        }

        public static void Day_11_Part02()
        {
            //list of ints to store the worry level, each digit is stored as a seperate int

            //implement "long multiplication" to multiply the worry level lists correctly

            // use division rules to determine if a given worry level list is divisible by the division int

            Console.WriteLine("No solution for Part 02 yet");

        }
    }

    class Monkey
    {
        public int monkeyID { get; set; }

        public List<int> heldItems { get; set; }

        public OperationType operationType { get; set; }
        public int operationInt { get; set; }

        public int divisionInt { get; set; }

        public int trueMonkeyID { get; set; }

        public int falseMonkeyID { get; set; }

        public Monkey falseMonkey { get; set; }

        public Monkey trueMonkey { get; set; }

        public int inspectionCounter { get; set; }
        
        public void inspect()
        {
            
            switch (operationType)
            {
                case OperationType.addition:
                    foreach (int item in heldItems)
                    {
                        inspectionCounter++;
                        //Console.WriteLine("monkey " + monkeyID + " is inspecting an item with worry level "+ item);
                        // inspect te item/ raise worry level as defined by operation
                        double worryLevel = item + operationInt;
                        //Console.WriteLine("monkey is performing an addition, the result is: "+worryLevel);
                        // loose interest/ divide by three and round down

                        worryLevel = Math.Floor(worryLevel/3);
                        int newItem = Convert.ToInt32(worryLevel);
                        //Console.WriteLine("worry Level decreased to: " + newItem);
                        // test the item to see where to throw to
                        if (newItem % divisionInt == 0)
                        {
                            //throw to true monkey
                            //Console.WriteLine("test came back true, throwing to trueMonkey: "+trueMonkeyID);
                            trueMonkey.heldItems.Add(newItem);
                        }
                        else
                        {
                            //throw to false monkey
                            //Console.WriteLine("test came back false, throwing to false Monkey: " + falseMonkeyID);
                            falseMonkey.heldItems.Add(newItem);
                        }

                    }
                    // empty held items list
                    heldItems= new List<int>();
                    break;

                case OperationType.multiplication:
                    foreach (int item in heldItems)
                    {
                        inspectionCounter++;
                        //Console.WriteLine("monkey " + monkeyID + " is inspecting an item with worry level " + item);
                        // inspect te item/ raise worry level as defined by operation
                        double worryLevel = item * operationInt;
                        // loose interest/ divide by three and round down
                        //Console.WriteLine("monkey is performing a multiplication, the result is: " + worryLevel);

                        worryLevel = Math.Floor(worryLevel / 3);
                        int newItem = Convert.ToInt32(worryLevel);
                        //Console.WriteLine("worry Level decreased to: " + newItem);
                        // test the item to see where to throw to
                        if (newItem % divisionInt == 0)
                        {
                            //throw to true monkey
                            //Console.WriteLine("test came back true, throwing to trueMonkey: " + trueMonkeyID);
                            trueMonkey.heldItems.Add(newItem);
                        }
                        else
                        {
                            //throw to false monkey
                            //Console.WriteLine("test came back false, throwing to false Monkey: " + falseMonkeyID);
                            falseMonkey.heldItems.Add(newItem);
                        }

                    }
                    // empty held items list
                    heldItems = new List<int>();

                    break;

                case OperationType.quadration:
                    foreach (int item in heldItems)
                    {
                        inspectionCounter++;
                        //Console.WriteLine("monkey " + monkeyID + " is inspecting an item with worry level " + item);
                        // inspect te item/ raise worry level as defined by operation
                        double worryLevel = item *item;
                        // loose interest/ divide by three and round down
                        //Console.WriteLine("monkey is performing a quadration, the result is: " + worryLevel);

                        worryLevel = Math.Floor(worryLevel / 3);
                        int newItem = Convert.ToInt32(worryLevel);
                        //Console.WriteLine("worry Level decreased to: " + newItem);
                        // test the item to see where to throw to
                        if (newItem % divisionInt == 0)
                        {
                            //throw to true monkey
                            //Console.WriteLine("test came back true, throwing to trueMonkey: " + trueMonkeyID);
                            trueMonkey.heldItems.Add(newItem);
                        }
                        else
                        {
                            //throw to false monkey
                            //Console.WriteLine("test came back false, throwing to false Monkey: " + falseMonkeyID);
                            falseMonkey.heldItems.Add(newItem);
                        }

                    }
                    // empty held items list
                    heldItems = new List<int>();

                    break;
            }
        }

        // I think the operation function should go here, but not sure

        //I think the test/ throw function should go here as well, but not sure

    }

    public enum OperationType
    {
        multiplication,
        addition,
        quadration
    }
}
