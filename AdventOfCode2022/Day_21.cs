using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day_21
    {
        public static void Day_21_Part01()
        {
            string importString = Import.ImportString("Day_21.txt");
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            TreeNode<Monkey> root = new TreeNode<Monkey>() { Parent = null, Data= new Monkey("root", 0, Operation.undefined) };
            Tree<Monkey> tree = new Tree<Monkey>() { Root = root};

            List<TreeNode<Monkey>> currentNodes = new List<TreeNode<Monkey>>();
            List<TreeNode<Monkey>> leafNodes = new List<TreeNode<Monkey>>();
            currentNodes.Add(root);
            string leftChild= "empty";
            string rightChild= "empty";

            //set up tree
            for (int i=0; i < currentNodes.Count; i++)
            {
                foreach (string line in lines)
                {
                    string[] tempString = line.Split(' ');
                    string lineID = tempString[0].Substring(0, 4);
                    if (lineID == currentNodes[i].Data.id)
                    {
                       
                        if (tempString.Length > 2)
                        {
                            switch (tempString[2])
                            {
                                case "+":
                                    currentNodes[i].Data = new Monkey(lineID, 0, Operation.addition);
                                    break;
                                case "-":
                                    currentNodes[i].Data = new Monkey(lineID, 0, Operation.subtraction);
                                    break;
                                case "*":
                                    currentNodes[i].Data = new Monkey(lineID, 0, Operation.multiplication);
                                    break;
                                case "/":
                                    currentNodes[i].Data = new Monkey(lineID, 0, Operation.division);
                                    break;
                            }
                            leftChild = tempString[1];
                            rightChild = tempString[3];

                            currentNodes[i].Children = new List<TreeNode<Monkey>>();
                            currentNodes[i].Children.Add(new TreeNode<Monkey>() { Parent = currentNodes[i], Data = new Monkey(leftChild, 0, Operation.undefined) });
                            currentNodes[i].Children.Add(new TreeNode<Monkey>() { Parent = currentNodes[i], Data = new Monkey(rightChild, 0, Operation.undefined) });
                            foreach (TreeNode<Monkey> child in currentNodes[i].Children)
                            {
                                currentNodes.Add(child);
                            }

                        }
                        else
                        {
                            currentNodes[i].Data = new Monkey(lineID, Int32.Parse(tempString[1]), Operation.undefined);
                            leafNodes.Add(currentNodes[i]);
                        }


                        //Console.WriteLine("created Node: "+currentNodes[i].Data);
                    }
                }

            }

            // calculate the tree somehow
            for(int i=0; i<leafNodes.Count; i++)
            {
                //Console.WriteLine("current leaf Node: "+leafNodes[i].Data);
                if (leafNodes[i].Children!= null && leafNodes[i].Children[0].Data.value!=0 && leafNodes[i].Children[1].Data.value != 0)
                {
                    if (leafNodes[i].Data.operation != Operation.undefined)
                    {
                        long value;
                        switch (leafNodes[i].Data.operation)
                        {
                            case Operation.addition:
                                value = leafNodes[i].Children[0].Data.value + leafNodes[i].Children[1].Data.value;
                                leafNodes[i].Data = new Monkey(leafNodes[i].Data.id, value, leafNodes[i].Data.operation);
                                break;

                            case Operation.subtraction:
                                value = leafNodes[i].Children[0].Data.value - leafNodes[i].Children[1].Data.value;
                                leafNodes[i].Data = new Monkey(leafNodes[i].Data.id, value, leafNodes[i].Data.operation);
                                break;

                            case Operation.multiplication:
                                value = leafNodes[i].Children[0].Data.value * leafNodes[i].Children[1].Data.value;
                                leafNodes[i].Data = new Monkey(leafNodes[i].Data.id, value, leafNodes[i].Data.operation);
                                break;
                            case Operation.division:
                                value = leafNodes[i].Children[0].Data.value / leafNodes[i].Children[1].Data.value;
                                leafNodes[i].Data = new Monkey(leafNodes[i].Data.id, value, leafNodes[i].Data.operation);
                                break;
                        }
                    }
                    if (leafNodes[i].Parent != null)
                    {
                        leafNodes.Add(leafNodes[i].Parent);
                    }
                    Console.WriteLine("current leaf Node after processing: " + leafNodes[i].Data);
                }
                else if(leafNodes[i].Children!= null)
                {
                    leafNodes.Add(leafNodes[i]);
                    Console.WriteLine("one of the children of "+leafNodes[i].Data.id +" was not ready, moving node to the end of the list");
                }
                else
                {
                    if (leafNodes[i].Parent != null)
                    {
                        leafNodes.Add(leafNodes[i].Parent);
                    }
                }

            }

            Console.WriteLine("The monkey named root shouts: "+root.Data.value);
        }

        public struct Monkey
        {

            public Monkey(string id, long value, Operation operation)
            {
                this.id = id;
                this.value = value;
                this.operation = operation;
            }
            public string id;
            public long value;
            public Operation operation;

            public override string ToString()
            {
                return "id: " + id + " value: " + value + " operationType: " + operation;
            }


            

        }
        public enum Operation
        {
            undefined,
            addition,
            subtraction,
            multiplication,
            division
        }
    }
}
