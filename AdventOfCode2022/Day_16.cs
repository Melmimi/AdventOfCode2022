using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day_16
    {
        public static void Day_16_Part01()
        {
            string importString = Import.ImportString("Day_16.txt");
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            Graph<Valve> valveGraph = new Graph<Valve>();
            valveGraph.nodes = new List<GraphNode<Valve>>();

            Console.WriteLine("There is no answer for day 16 yet.");

            foreach(string line in lines)
            {
                string[] halves = line.Split(';');

                //process first half
                string[] tempString1 = halves[0].Split(' ');

                string nodeName = tempString1[1];
                string[] tempString2 = tempString1[4].Split('=');
                int nodeFlowRate = Int32.Parse(tempString2[1]);

                // create node
                Valve currentValve = new Valve(nodeName, nodeFlowRate);
                valveGraph.nodes.Add(new GraphNode<Valve>() { Data = currentValve, neighbours = new List<GraphNode<Valve>>() }) ;

            }
            // establish neighbours


            foreach (string line in lines)
            {
                string[] halves = line.Split(';');
                string[] firstHalf = halves[0].Split(' ');
                string[] tempString1 = halves[1].Split(' ');
                string[] tempString2;
                string[] tempString3;
                string nodeName = firstHalf[1];

                //process second half
                List<string> nodeNeighbours = new List<string>();

                if (tempString1.Length > 6)
                {
                    tempString2 = halves[1].Split(new string[] { " valves " }, StringSplitOptions.None);
                    tempString3 = tempString2[1].Split(new string[] { ", " }, StringSplitOptions.None);
                    foreach(string neighbour in tempString3)
                    {
                        nodeNeighbours.Add(neighbour);
                    }

                }
                else
                {
                    tempString3 = new string[1];
                    tempString3[0] = tempString1[5];
                    nodeNeighbours.Add(tempString1[5]);

                }

                GraphNode<Valve> currentNode;
                foreach(GraphNode<Valve> node in valveGraph.nodes)
                {
                    if (node.Data.name == nodeName)
                    {
                        currentNode = node;
                        foreach (string name in nodeNeighbours)
                        {
                            foreach(GraphNode<Valve> neighbour in valveGraph.nodes)
                            {
                                if (neighbour.Data.name == name)
                                {
                                    currentNode.neighbours.Add(neighbour);
                                }
                            }

                        }
                    }
                }

            }


            //DEbug print
            /*
            foreach (GraphNode<Valve> node in valveGraph.nodes)
            {
                Console.Write(node.Data + " ; ");
                foreach (GraphNode<Valve> neighbour in node.neighbours)
                {
                    Console.Write(neighbour.Data.name + ", ");
                }
                Console.WriteLine();
            }
            */


            //search logic

            //make a list/stack of all nodes that have flowRate of more than 0
            //go through them in order and calculate the amount of presure released in that paticular path
            //i'm gonna need a search algorithm everytime I go to the next valve from my list to see what the shortest path is
            //this will be a major headache
            // reshuffle the stack--- How do I do that?
            // go again

            //tipp from reddit to consider: 
            //can I maybe precalculate how long a given permutation would take and then only calculate the sub 30min ones?

        }
    }
    public struct Valve
    {
        public Valve(string name, int flowRate)
        {
            this.name = name;
            this.flowRate = flowRate;
        }
        public string name;
        public int flowRate;

        public override string ToString()
        {
            return "name: " + name + " flowRate: " + flowRate;
        }

    }
}
