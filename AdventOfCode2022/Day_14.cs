using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day_14
    {

        public static void Day_14_Part01()
        {
            string importString = Import.ImportString("Day_14.txt");
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            List<List<Coordinate>> pathInstructionsList = new List<List<Coordinate>>();
            int lineCounter = 0;
            //Parse input 
            foreach (string line in lines)
            {
                int instructionsCounter = 0;
                string[] instructionCoords = line.Split(new string[] { " -> " }, StringSplitOptions.None);
                pathInstructionsList.Add(new List<Coordinate>());
                
                foreach (string instruction in instructionCoords)
                {
                    string[] tempString = instruction.Split(new string[] { "," }, StringSplitOptions.None);
                    int[] coordinates = Array.ConvertAll(tempString, s => int.Parse(s)); 
                    pathInstructionsList[lineCounter].Add(new Coordinate(coordinates[0],coordinates[1]));

                    //Console.Write(pathInstructionsList[lineCounter][instructionsCounter]+",");
                    instructionsCounter++;
                }
                //Console.WriteLine();
                lineCounter++;

            }

            //find furthest rocks
            int lastRock=0;

            foreach (List<Coordinate> path in pathInstructionsList)
            {
                foreach(Coordinate coord in path)
                {
                    if ( coord.y > lastRock)
                        {
                        lastRock = coord.y;
                        }
                }
            }
            //Console.WriteLine("the furthest rock is "+lastRock);

            //construct map
            Tile[,] tileMap = new Tile[515, lastRock+5];

            for(int x=0; x < tileMap.GetLength(0); x++)
            {
                tileMap[x, lastRock+1].material = Material.Void;
            }

            //add sand Spawner material
            tileMap[500, 0].material = Material.SandSpawner;

            //use the instructions to construct cave walls
            foreach(List<Coordinate> path in pathInstructionsList)
            {
                for(int i=0; i<path.Count-1; i++)
                {
                    if (path[i].x== path[i + 1].x)
                    {
                        //iterate along y axis
                        int x = path[i].x;
                        if(path[i].y > path[i + 1].y)
                        {
                            // count down
                            for (int y=path[i].y; y>=path[i+1].y; y--)
                            {
                                tileMap[x, y].material = Material.Rock;

                            }
                            
                        }
                        else if (path[i].y < path[i + 1].y)
                        {
                            //count up
                            for (int y = path[i].y; y <= path[i + 1].y; y++)
                            {
                                tileMap[x, y].material = Material.Rock;
                            }
                        }
                    }
                    if (path[i].y== path[i + 1].y)
                    {
                        //iterate along x axis
                        int y = path[i].y;
                        if (path[i].x > path[i + 1].x)
                        {
                            // count down
                            for (int x = path[i].x; x >= path[i + 1].x; x--)
                            {
                                tileMap[x, y].material = Material.Rock;
                            }

                        }
                        else if (path[i].x < path[i + 1].x)
                        {
                            //count up
                            for (int x = path[i].x; x <= path[i + 1].x; x++)
                            {
                                tileMap[x, y].material = Material.Rock;
                            }
                        }
                    }
                }
            }

            // repeat until some sand hits the void
            bool theVoid = false;
            int settledCounter = 0;

            while (!theVoid)
            {
                //Spawn and simulate Sand
                bool settled = false;

                Coordinate currentSandFlow = new Coordinate(500, 0);
                while (!settled)
                {
                    if (tileMap[currentSandFlow.x, currentSandFlow.y + 1].material == Material.Air)
                    {
                        currentSandFlow = new Coordinate(currentSandFlow.x, currentSandFlow.y + 1);

                    }
                    else if (tileMap[currentSandFlow.x - 1, currentSandFlow.y + 1].material == Material.Air)
                    {
                        currentSandFlow = new Coordinate(currentSandFlow.x - 1, currentSandFlow.y + 1);
                    }
                    else if (tileMap[currentSandFlow.x + 1, currentSandFlow.y + 1].material == Material.Air)
                    {
                        currentSandFlow = new Coordinate(currentSandFlow.x + 1, currentSandFlow.y + 1);
                    }else if(tileMap[currentSandFlow.x, currentSandFlow.y + 1].material == Material.Void)
                    {
                        //settled = true;
                        theVoid = true;
                        break;
                    }
                    else if (tileMap[currentSandFlow.x-1, currentSandFlow.y + 1].material == Material.Void)
                    {
                        //settled = true;
                        theVoid = true;
                        break;
                    }
                    else if (tileMap[currentSandFlow.x+1, currentSandFlow.y + 1].material == Material.Void)
                    {
                        //settled = true;
                        theVoid = true;
                        break;
                    }
                    else
                    {
                        settledCounter++;
                        settled = true;
                        
                    }

                }
                tileMap[currentSandFlow.x, currentSandFlow.y].material = Material.Sand;

                if (theVoid == true)
                {
                    break;
                }

                // Draw Map
                /*
                //Console.Clear();
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    for (int x = 400; x < tileMap.GetLength(0); x++)
                    {
                        if (tileMap[x, y].material == Material.Air)
                        {
                            Console.Write(".");
                        }
                        else if (tileMap[x, y].material == Material.Rock)
                        {
                            Console.Write("#");
                        }
                        else if (tileMap[x, y].material == Material.Void)
                        {
                            Console.Write("x");
                        }
                        else if (tileMap[x, y].material == Material.Sand)
                        {
                            Console.Write("o");
                        }
                        else if (tileMap[x, y].material == Material.SandSpawner)
                        {
                            Console.Write("+");
                        }
                    }
                    Console.WriteLine();
                }*/
            }


            // Draw Map
            //Console.Clear();
            for (int y = 0; y < tileMap.GetLength(1); y++)
            {
                for (int x = 400; x < tileMap.GetLength(0); x++)
                {
                    if (tileMap[x, y].material == Material.Air)
                    {
                        Console.Write(".");
                    }
                    else if (tileMap[x, y].material == Material.Rock)
                    {
                        Console.Write("#");
                    }
                    else if (tileMap[x, y].material == Material.Void)
                    {
                        Console.Write("x");
                    }
                    else if (tileMap[x, y].material == Material.Sand)
                    {
                        Console.Write("o");
                    }
                    else if (tileMap[x, y].material == Material.SandSpawner)
                    {
                        Console.Write("+");
                    }
                }
                Console.WriteLine();

            }
            Console.WriteLine("Part01: ");
            Console.WriteLine("settled units of sand: " + settledCounter);

        }

        public static void Day_14_Part02()
        {
            string importString = Import.ImportString("Day_14.txt");
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            List<List<Coordinate>> pathInstructionsList = new List<List<Coordinate>>();
            int lineCounter = 0;
            //Parse input 
            foreach (string line in lines)
            {
                int instructionsCounter = 0;
                string[] instructionCoords = line.Split(new string[] { " -> " }, StringSplitOptions.None);
                pathInstructionsList.Add(new List<Coordinate>());

                foreach (string instruction in instructionCoords)
                {
                    string[] tempString = instruction.Split(new string[] { "," }, StringSplitOptions.None);
                    int[] coordinates = Array.ConvertAll(tempString, s => int.Parse(s));
                    pathInstructionsList[lineCounter].Add(new Coordinate(coordinates[0], coordinates[1]));

                    //Console.Write(pathInstructionsList[lineCounter][instructionsCounter] + ",");
                    instructionsCounter++;
                }
                //Console.WriteLine();
                lineCounter++;

            }

            //find furthest rocks
            int lastRock = 0;

            foreach (List<Coordinate> path in pathInstructionsList)
            {
                foreach (Coordinate coord in path)
                {
                    if (coord.y > lastRock)
                    {
                        lastRock = coord.y;
                    }
                }
            }
            //Console.WriteLine("the furthest rock is " + lastRock);

            //construct map
            Tile[,] tileMap = new Tile[1000, lastRock + 5];

            for (int x = 0; x < tileMap.GetLength(0); x++)
            {
                tileMap[x, lastRock + 2].material = Material.Void;
            }

            //add sand Spawner material
            tileMap[500, 0].material = Material.SandSpawner;

            //use the instructions to construct cave walls
            foreach (List<Coordinate> path in pathInstructionsList)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    if (path[i].x == path[i + 1].x)
                    {
                        //iterate along y axis
                        int x = path[i].x;
                        if (path[i].y > path[i + 1].y)
                        {
                            // count down
                            for (int y = path[i].y; y >= path[i + 1].y; y--)
                            {
                                tileMap[x, y].material = Material.Rock;

                            }

                        }
                        else if (path[i].y < path[i + 1].y)
                        {
                            //count up
                            for (int y = path[i].y; y <= path[i + 1].y; y++)
                            {
                                tileMap[x, y].material = Material.Rock;
                            }
                        }
                    }
                    if (path[i].y == path[i + 1].y)
                    {
                        //iterate along x axis
                        int y = path[i].y;
                        if (path[i].x > path[i + 1].x)
                        {
                            // count down
                            for (int x = path[i].x; x >= path[i + 1].x; x--)
                            {
                                tileMap[x, y].material = Material.Rock;
                            }

                        }
                        else if (path[i].x < path[i + 1].x)
                        {
                            //count up
                            for (int x = path[i].x; x <= path[i + 1].x; x++)
                            {
                                tileMap[x, y].material = Material.Rock;
                            }
                        }
                    }
                }
            }

            // repeat until some sand hits the void
            bool theVoid = false;
            int settledCounter = 0;

            while (!theVoid)
            {
                //Spawn and simulate Sand
                bool settled = false;

                Coordinate currentSandFlow = new Coordinate(500, 0);
                while (!settled)
                {
                    if (tileMap[currentSandFlow.x, currentSandFlow.y + 1].material == Material.Air)
                    {
                        currentSandFlow = new Coordinate(currentSandFlow.x, currentSandFlow.y + 1);

                    }
                    else if (tileMap[currentSandFlow.x - 1, currentSandFlow.y + 1].material == Material.Air)
                    {
                        currentSandFlow = new Coordinate(currentSandFlow.x - 1, currentSandFlow.y + 1);
                    }
                    else if (tileMap[currentSandFlow.x + 1, currentSandFlow.y + 1].material == Material.Air)
                    {
                        currentSandFlow = new Coordinate(currentSandFlow.x + 1, currentSandFlow.y + 1);
                    }
                    else
                    {
                        settledCounter++;
                        settled = true;

                    }

                    if (currentSandFlow.x== 500 && currentSandFlow.y == 0)
                    {
                        theVoid = true;
                        break;
                    }

                }
                tileMap[currentSandFlow.x, currentSandFlow.y].material = Material.Sand;

                if (theVoid == true)
                {
                    break;
                }

               
            }


            // Draw Map
            //Console.Clear();
            /*
            for (int y = 0; y < tileMap.GetLength(1); y++)
            {
                for (int x = 400; x < tileMap.GetLength(0); x++)
                {
                    if (tileMap[x, y].material == Material.Air)
                    {
                        Console.Write(".");
                    }
                    else if (tileMap[x, y].material == Material.Rock)
                    {
                        Console.Write("#");
                    }
                    else if (tileMap[x, y].material == Material.Void)
                    {
                        Console.Write("x");
                    }
                    else if (tileMap[x, y].material == Material.Sand)
                    {
                        Console.Write("o");
                    }
                    else if (tileMap[x, y].material == Material.SandSpawner)
                    {
                        Console.Write("+");
                    }
                }

                //Console.WriteLine();

            }
            */
            Console.WriteLine("Part02: ");
            Console.WriteLine("settled units of sand: " + settledCounter);

        }

        public enum Material
        {
            Air,
            Rock,
            Sand,
            SandSpawner,
            Void
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
                return "x: " + x + " y: " + y;
            }

        }


        public struct Tile
        {
            public Tile(Coordinate coordinate, Material material)
            {
                this.coordinate = coordinate;
                this.material = material;
            }
            public Coordinate coordinate;
            public Material material;

            public override string ToString()
            {
                return "x: " + coordinate.x + " y: " + coordinate.y + " material: " + material;
            }
        }
    }
}
