using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day_09
    {
        public static void Day_09_Part01()
        {
            string importString = Import.ImportString("Day_09.txt");
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);


            Coordinate head = new Coordinate(0,0);
            Coordinate tail = new Coordinate(0, 0);
            List <Coordinate> visitedLocations = new List<Coordinate>();

            foreach(string line in lines)
            {
                string[] splitLine = line.Split(' ');
                //Console.WriteLine("Ausgangssituation: " +head+ " Anweisung: " + line);
                for (int i = 0; i < Int32.Parse(splitLine[1]); i++)
                {
                    switch (splitLine[0])
                    {
                        case "L":
                            head.x--;

                            break;

                        case "U":
                            head.y++;
                            break;

                        case "R":
                            head.x++;
                            break;

                        case "D":
                            head.y--;
                            break;

                    }


                    //Simulate Tail
                    tail = simulateTail(head, tail);


                    if (!visitedLocations.Contains(tail))
                    {
                        visitedLocations.Add(tail);
                    }
                    //Console.WriteLine("current Position head " + head);
                    //Console.WriteLine("current Position tail " + tail);
                }
                
                //Debug visualization
                /*
                for(int y=5; y >=0; y--)
                {
                    for(int x = 0; x < 6; x++)
                    {
                        if(x==head.x && y == head.y)
                        {
                            Console.Write("H");
                        }else if(x==tail.x && y == tail.y)
                        {
                            Console.Write("T");
                        }
                        else
                        {
                            Console.Write(".");
                        }
                    }
                    Console.WriteLine();
                }
                */
            }

            Console.WriteLine("tail has visited "+visitedLocations.Count+ " locations at least once");
        }

        public static void Day_09_Part02()
        {
            string importString = Import.ImportString("Day_09.txt");
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            List<Coordinate> ropeSegments = new List<Coordinate>();
            List<Coordinate> visitedLocations = new List<Coordinate>();

            for(int i=0; i < 10; i++)
            {
                ropeSegments.Add(new Coordinate(0, 0));
            }


            foreach (string line in lines)
            {
                string[] splitLine = line.Split(' ');
                Console.WriteLine("Ausgangssituation: " +ropeSegments[0]+ " Anweisung: " + line);
                for (int i = 0; i < Int32.Parse(splitLine[1]); i++)
                {
                    switch (splitLine[0])
                    {
                        case "L":
                            ropeSegments[0] = new Coordinate(ropeSegments[0].x - 1, ropeSegments[0].y);
                            //ropeSegments[0].x--;
                            break;

                        case "U":
                            ropeSegments[0] = new Coordinate(ropeSegments[0].x, ropeSegments[0].y+1);
                            // head.y++;
                            break;

                        case "R":
                            ropeSegments[0] = new Coordinate(ropeSegments[0].x + 1, ropeSegments[0].y);
                            // head.x++;
                            break;

                        case "D":
                            ropeSegments[0] = new Coordinate(ropeSegments[0].x , ropeSegments[0].y-1);
                            // head.y--;
                            break;

                    }

                    

                    //Simulate Tail

                    for (int j=1; j < ropeSegments.Count; j++)
                    {
                        ropeSegments[j] = simulateTail(ropeSegments[j-1], ropeSegments[j]);

                        if (j==9 && !visitedLocations.Contains(ropeSegments[j]))
                        {
                            visitedLocations.Add(ropeSegments[j]);
                        }
                    }


                    //Console.WriteLine("current Position head " + head);
                    //Console.WriteLine("current Position tail " + tail);

                }


                //Debug visualization
                /*
                for (int y = 15; y >= -5; y--)
                {
                    for (int x = -11; x < 15; x++)
                    {

                        if (x == ropeSegments[0].x && y == ropeSegments[0].y)
                        {
                            Console.Write("H");
                        }
                        else if (x == 0 && y == 0)
                        {
                            Console.Write("s");
                        }
                        else if (x == ropeSegments[1].x && y == ropeSegments[1].y)
                        {
                            Console.Write("1");
                        }
                        else if (x == ropeSegments[2].x && y == ropeSegments[2].y)
                        {
                            Console.Write("2");
                        }
                        else if (x == ropeSegments[3].x && y == ropeSegments[3].y)
                        {
                            Console.Write("3");
                        }
                        else if (x == ropeSegments[4].x && y == ropeSegments[4].y)
                        {
                            Console.Write("4");
                        }
                        else if (x == ropeSegments[5].x && y == ropeSegments[5].y)
                        {
                            Console.Write("5");
                        }
                        else if (x == ropeSegments[6].x && y == ropeSegments[6].y)
                        {
                            Console.Write("6");
                        }
                        else if (x == ropeSegments[7].x && y == ropeSegments[7].y)
                        {
                            Console.Write("7");
                        }
                        else if (x == ropeSegments[8].x && y == ropeSegments[8].y)
                        {
                            Console.Write("8");
                        }
                        else if (x == ropeSegments[9].x && y == ropeSegments[9].y)
                        {
                            Console.Write("T");
                        }
                        else
                        {
                            Console.Write(".");
                        }
                    }
                    Console.WriteLine();
                }
                */


            }

            Console.WriteLine("tail has visited " + visitedLocations.Count + " locations at least once");
        }

        public static Coordinate simulateTail(Coordinate head, Coordinate tail)
        {

            if (Math.Abs(head.x - tail.x) > 1 || Math.Abs(head.y - tail.y) > 1)
            {
                // What direction do we need to catch up in?

                //right
                if (Math.Abs(head.x - tail.x) > 1 && head.x > tail.x && head.y==tail.y)
                {
                    tail.x++;
                }
                //left
                if (Math.Abs(head.x - tail.x) > 1 && head.x < tail.x && head.y == tail.y)
                {
                    tail.x--;
                }
                //up
                if (Math.Abs(head.y - tail.y) > 1 && head.y > tail.y && head.x == tail.x)
                {
                    tail.y++;
                }
                //down
                if (Math.Abs(head.y - tail.y) > 1 && head.y < tail.y && head.x == tail.x)
                {
                    tail.y--;
                }

                if (Math.Abs(head.x - tail.x) > 1 || Math.Abs(head.y - tail.y)>1)
                {
                    //upright
                    if (head.x > tail.x && head.y > tail.y)
                    {
                        tail.x++;
                        tail.y++;
                    }
                    //upleft
                    if (head.x < tail.x && head.y > tail.y)
                    {
                        tail.x--;
                        tail.y++;
                    }
                    //downright
                    if (head.x > tail.x && head.y < tail.y)
                    {
                        tail.x++;
                        tail.y--;
                    }
                    //downleft
                    if (head.x < tail.x && head.y < tail.y)
                    {
                        tail.x--;
                        tail.y--;
                    }

                }
               
                
                


            }
            return tail;
        }

        public struct Coordinate
        {
            public Coordinate(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public int x;
            public int y;

            public override string ToString()
            {
                return "x: " + x + " y: " + y ;
            }

        }

}

}
