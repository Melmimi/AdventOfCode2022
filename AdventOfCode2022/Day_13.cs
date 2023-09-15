using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day_13
    {
        public static void Day_13_Part01()
        {
            string importString = Import.ImportString("Day_13.txt");
            string[] packages = importString.Split(new string[] { Environment.NewLine+ Environment.NewLine }, StringSplitOptions.None);

            int index = 0;

            Console.WriteLine("No solution for day 13 yet.");

            foreach (string package in packages)
            {
                string[] sides = package.Split(new string[] {Environment.NewLine }, StringSplitOptions.None);
                string leftSide = sides[0];
                string rightSide = sides[1];

                Tree<int> tree = new Tree<int>();
                // set up root
                //tree.Root = new TreeNode<int>();
                tree.Root = new TreeNode<int> { Parent = null, Children = new List<TreeNode<int>>()};

                processList(tree.Root,leftSide);




                index++;
            }
            
            Console.WriteLine(packages[0]);



            void processList(TreeNode<int> parent, string inputString)
            {
                //remove first and last bracket

                List<char> inputCharList = new List<char>();
                inputCharList.AddRange(inputString);

                inputCharList.RemoveAt(inputCharList.Count-1);
                inputCharList.RemoveAt(0);

                string bracketLess = new string(inputCharList.ToArray());

                //split along ','

                string[]  listItems = bracketLess.Split(',');

                foreach(string item in listItems)
                {
                    if (item.Length > 1)
                    {
                        Console.WriteLine("item is a list and needs to be processed further "+ item);
                    }else if (item.Length == 1)
                    {
                        Console.WriteLine("item is a single digit: "+item);
                    }
                }


                Console.WriteLine(bracketLess);

                //set up a Node ready to be added to the tree
                // do we really want this to return something 
                //or do we just want this to be a void and we add stuff to the tree anyways since it'S a local funtion?

                // remove first and last bracket
                // split along the ','
                // if all the results are one character long we got ourselves a list
                // any result longer than one character needs to be processed again

            }
        }
    }
}
