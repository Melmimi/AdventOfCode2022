using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day_18
    {
        public static void Day_18_Part01()
        {
            bool[,,] dropletMap = new bool[22, 22, 22];

            string importString = Import.ImportString("Day_18.txt");
            string[] coordinates = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (string coordinate in coordinates)
            {
                string[] splitCoordinate = coordinate.Split(',');
                int x = Int32.Parse(splitCoordinate[0]);
                int y = Int32.Parse(splitCoordinate[1]);
                int z = Int32.Parse(splitCoordinate[2]);

                dropletMap[x, y, z] = true;

            }

            int totalExposedSides = 0;
            for (int z = 0; z < dropletMap.GetLength(2); z++)
            {
                for (int y = 0; y < dropletMap.GetLength(1); y++)
                {
                    for (int x = 0; x < dropletMap.GetLength(0); x++)
                    {
                        if (dropletMap[x, y, z] == true)
                        {
                            //ry to go left
                            if (x - 1 >= 0)
                            {
                                if (dropletMap[x - 1, y, z] == false)
                                {
                                    totalExposedSides++;
                                }
                            }
                            else
                            {
                                totalExposedSides++;
                            }

                            //try to go right
                            if (x + 1 < dropletMap.GetLength(0))
                            {
                                if (dropletMap[x + 1, y, z] == false)
                                {
                                    totalExposedSides++;
                                }
                            }
                            else
                            {
                                totalExposedSides++;
                            }

                            //try to go up
                            if (y - 1 >= 0)
                            {
                                if (dropletMap[x, y - 1, z] == false)
                                {
                                    totalExposedSides++;
                                }
                            }
                            else
                            {
                                totalExposedSides++;
                            }
                            //try to go down
                            if (y + 1 < dropletMap.GetLength(1))
                            {
                                if (dropletMap[x, y + 1, z] == false)
                                {
                                    totalExposedSides++;
                                }
                            }
                            else
                            {
                                totalExposedSides++;
                            }
                            // try to go forward
                            if (z - 1 >= 0)
                            {
                                if (dropletMap[x, y, z - 1] == false)
                                {
                                    totalExposedSides++;
                                }
                            }
                            else
                            {
                                totalExposedSides++;
                            }
                            //try to go backward

                            if (z + 1 < dropletMap.GetLength(2))
                            {
                                if (dropletMap[x, y, z + 1] == false)
                                {
                                    totalExposedSides++;
                                }
                            }
                            else
                            {
                                totalExposedSides++;
                            }


                        }



                    }
                }
            }
            Console.WriteLine("number of exposed Sides is: " + totalExposedSides);

            //Part 2:

            //make a material enum
            Material[,,] materialMap = new Material[22, 22, 22];

            //projection might be the wrong way to go about this, maybe a flood fill would be better
            //start a flood fill from every unoccupied spot on the 6 sides maybe?

            List<Coordinate3> floodFillList = new List<Coordinate3>();
            floodFillList.Add(new Coordinate3(0, 0, 0));
            floodFillList.Add(new Coordinate3(21, 21, 21));
            floodFillList.Add(new Coordinate3(0, 0, 21));
            floodFillList.Add(new Coordinate3(0, 21, 21));
            floodFillList.Add(new Coordinate3(21, 21, 0));
            floodFillList.Add(new Coordinate3(21, 0, 21));
            floodFillList.Add(new Coordinate3(0, 21, 0));
            floodFillList.Add(new Coordinate3(21, 0, 0));

            //iterate through floodFill List and add newly discovered air spaces 

            for (int i = 0; i < floodFillList.Count; i++)
            {
                int x = floodFillList[i].x;
                int y = floodFillList[i].y;
                int z = floodFillList[i].z;

                //try to go left
                if (x - 1 >= 0)
                {
                    if (dropletMap[x - 1, y, z] == false)
                    {
                        if (!floodFillList.Contains(new Coordinate3(x - 1, y, z)))
                        {
                            floodFillList.Add(new Coordinate3(x - 1, y, z));
                        }
                        materialMap[x - 1, y, z] = Material.Water;
                    }
                    else
                    {
                        materialMap[x - 1, y, z] = Material.Crust;
                    }
                }

                //try to go right
                if (x + 1 < dropletMap.GetLength(0))
                {
                    if (dropletMap[x + 1, y, z] == false)
                    {
                        if (!floodFillList.Contains(new Coordinate3(x + 1, y, z)))
                        {
                            floodFillList.Add(new Coordinate3(x + 1, y, z));
                        }
                        materialMap[x + 1, y, z] = Material.Water;

                    }
                    else
                    {
                        materialMap[x + 1, y, z] = Material.Crust;
                    }
                }

                //try to go up
                if (y - 1 >= 0)
                {
                    if (dropletMap[x, y - 1, z] == false)
                    {
                        if (!floodFillList.Contains(new Coordinate3(x, y - 1, z)))
                        {
                            floodFillList.Add(new Coordinate3(x, y - 1, z));
                        }
                        materialMap[x, y - 1, z] = Material.Water;
                    }
                    else
                    {
                        materialMap[x, y - 1, z] = Material.Crust;
                    }
                }

                //try to go down
                if (y + 1 < dropletMap.GetLength(1))
                {
                    if (dropletMap[x, y + 1, z] == false)
                    {
                        if (!floodFillList.Contains(new Coordinate3(x, y + 1, z)))
                        {
                            floodFillList.Add(new Coordinate3(x, y + 1, z));
                        }
                        materialMap[x, y + 1, z] = Material.Water;
                    }
                    else
                    {
                        materialMap[x, y + 1, z] = Material.Crust;
                    }
                }

                // try to go forward
                if (z - 1 >= 0)
                {
                    if (dropletMap[x, y, z - 1] == false)
                    {
                        if (!floodFillList.Contains(new Coordinate3(x, y, z - 1)))
                        {
                            floodFillList.Add(new Coordinate3(x, y, z - 1));
                        }
                        materialMap[x, y, z - 1] = Material.Water;
                    }
                    else
                    {
                        materialMap[x, y, z - 1] = Material.Crust;
                    }
                }

                //try to go backward

                if (z + 1 < dropletMap.GetLength(2))
                {
                    if (dropletMap[x, y, z + 1] == false)
                    {
                        if (!floodFillList.Contains(new Coordinate3(x, y, z + 1)))
                        {
                            floodFillList.Add(new Coordinate3(x, y, z + 1));
                        }
                        materialMap[x, y, z + 1] = Material.Water;

                    }
                    else
                    {
                        materialMap[x, y, z + 1] = Material.Crust;
                    }
                }


            }
            //I can't help but feel like there are some airpockets touching the 0-sides   


            // add up all the crusts
            int totalExposedExteriorSides = 0;

            for (int z = 0; z < dropletMap.GetLength(2); z++)
            {
                for (int y = 0; y < dropletMap.GetLength(1); y++)
                {
                    for (int x = 0; x < dropletMap.GetLength(0); x++)
                    {
                        if (materialMap[x, y, z] == Material.Crust)
                        {

                            //ry to go left
                            if (x - 1 >= 0)
                            {
                                if (materialMap[x - 1, y, z] == Material.Water)
                                {
                                    totalExposedExteriorSides++;
                                }
                            }
                            else
                            {
                                totalExposedExteriorSides++;
                            }

                            //try to go right
                            if (x + 1 < dropletMap.GetLength(0))
                            {
                                if (materialMap[x + 1, y, z] == Material.Water)
                                {
                                    totalExposedExteriorSides++;
                                }
                            }
                            else
                            {
                                totalExposedExteriorSides++;
                            }

                            //try to go up
                            if (y - 1 >= 0)
                            {
                                if (materialMap[x, y - 1, z] == Material.Water)
                                {
                                    totalExposedExteriorSides++;
                                }
                            }
                            else
                            {
                                totalExposedExteriorSides++;
                            }
                            //try to go down
                            if (y + 1 < dropletMap.GetLength(1))
                            {
                                if (materialMap[x, y + 1, z] == Material.Water)
                                {
                                    totalExposedExteriorSides++;
                                }
                            }
                            else
                            {
                                totalExposedExteriorSides++;
                            }
                            // try to go forward
                            if (z - 1 >= 0)
                            {
                                if (materialMap[x, y, z - 1] == Material.Water)
                                {
                                    totalExposedExteriorSides++;
                                }
                            }
                            else
                            {
                                totalExposedExteriorSides++;
                            }
                            //try to go backward

                            if (z + 1 < dropletMap.GetLength(2))
                            {
                                if (materialMap[x, y, z + 1] == Material.Water)
                                {
                                    totalExposedExteriorSides++;
                                }
                            }
                            else
                            {
                                totalExposedExteriorSides++;
                            }

                        }

                    }
                }
            }

            Console.WriteLine("total number of exposed exterior sides is " + totalExposedExteriorSides);

            //maybe make a debug drawing that prints the cube in slices and has a different symbol for crust?
            for (int z = 0; z < dropletMap.GetLength(2); z++)
            {
                for (int y = 0; y < dropletMap.GetLength(1); y++)
                {
                    for (int x = 0; x < dropletMap.GetLength(0); x++)
                    {
                        if (dropletMap[x, y, z] == false)
                        {
                            Console.Write(".");
                        }
                        else if (dropletMap[x, y, z] == true)
                        {
                            Console.Write("#");
                        }
                        else
                        {
                            Console.Write("?");
                        }

                        

                    }
                    Console.WriteLine();
                }
                Console.WriteLine("_______________________________________________________________________________________");

                for (int y = 0; y < dropletMap.GetLength(1); y++)
                {
                    for (int x = 0; x < dropletMap.GetLength(0); x++)
                    {
                        if (materialMap[x, y, z] == Material.Water)
                        {
                            Console.Write(".");
                        }
                        else if (materialMap[x, y, z] == Material.Crust)
                        {
                            Console.Write("c");
                        }
                        else
                        {
                            Console.Write(".");
                        }

                    }
                    Console.WriteLine();
                }
                Console.WriteLine("_______________________________________________________________________________________");
            }


            for (int z = 0; z < dropletMap.GetLength(2); z++)
            {
                for (int y = 0; y < dropletMap.GetLength(1); y++)
                {
                    for (int x = 0; x < dropletMap.GetLength(0); x++)
                    {
                        if (materialMap[x, y, z] == Material.Water)
                        {
                            Console.Write(".");
                        }
                        else if (materialMap[x, y, z] == Material.Crust)
                        {
                            Console.Write("#");
                        }
                        else
                        {
                            Console.Write(".");
                        }

                    }
                    Console.WriteLine();
                }
                Console.WriteLine("_______________________________________________________________________________________");
            }

        }


        public enum Material
        {
            Undefined,
            Water,
            Crust
        }


        public struct Coordinate3
        {

            public Coordinate3(int x, int y, int z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
            public int x;
            public int y;
            public int z;

            public override string ToString()
            {
                return "x: " + x + " y: " + y + "z: "+z;
            }



        }
    }
}
