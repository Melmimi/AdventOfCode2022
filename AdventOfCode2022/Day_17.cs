using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day_17
    {



        public static void Day_17_Part01()
        {
            string importString = Import.ImportString("Day_17.txt");
            char[] instructionsArray = importString.ToCharArray();
            Tile[,] tileMap = new Tile[9, 1000000];
            List <Shape> shapesList = new List<Shape>();

            int currentShapeIndex = 0;
            int currentInstructionIndex = 0;

            Console.WriteLine(instructionsArray[0].ToString());

            //establish Walls and Floor
            for (int x = 0; x < tileMap.GetLength(0); x++)
            {
                tileMap[x, 0].material = Material.Floor;
            }
            for (int y = 1; y < tileMap.GetLength(1); y++)
            {
                tileMap[0, y].material = Material.Wall;
                tileMap[8, y].material = Material.Wall;
            }

            //add shape recipes to the shape List
            List<Coordinate> longHorizontalList = new List<Coordinate>() {new Coordinate(0,0), new Coordinate(1,0),new Coordinate (2,0), new Coordinate(3,0) };
            Shape longHorizontal = new Shape(4,1,longHorizontalList);
            shapesList.Add(longHorizontal);
            List<Coordinate> crossList = new List<Coordinate>() { new Coordinate(1, 0), new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(2, 1), new Coordinate(1, 2) };
            Shape cross = new Shape(3,3,crossList);
            shapesList.Add(cross);
            List<Coordinate> invertedLList = new List<Coordinate>() { new Coordinate(0, 0), new Coordinate(1,0), new Coordinate(2,0),new Coordinate (2,1), new Coordinate (2,2)};
            Shape invertedL = new Shape(3,3,invertedLList);
            shapesList.Add(invertedL);
            List<Coordinate> longVerticalList = new List<Coordinate>() { new Coordinate(0, 0), new Coordinate(0,1), new Coordinate(0,2), new Coordinate(0,3) };
            Shape longVertical = new Shape(1,4,longVerticalList);
            shapesList.Add(longVertical);
            List<Coordinate> squareList = new List<Coordinate>() {new Coordinate(0,0), new Coordinate(0,1), new Coordinate(1,0), new Coordinate(1,1) };
            Shape square = new Shape(2,2,squareList);
            shapesList.Add(square);



            int highestRock = 0;

            for(int round=0; round<2022; round++)
            {

                //Spawn Shape in the correcct location
                Shape currentShape = new Shape(shapesList[currentShapeIndex].width,shapesList[currentShapeIndex].height, new List<Coordinate>(shapesList[currentShapeIndex].coordinates));

                if (currentShapeIndex + 1 < shapesList.Count)
                {
                    currentShapeIndex++;
                }
                else
                {
                    currentShapeIndex = 0;
                }


                for(int i=0; i<currentShape.coordinates.Count; i++)
                {
                    currentShape.coordinates[i] = new Coordinate(currentShape.coordinates[i].x + 3, currentShape.coordinates[i].y + highestRock + 4);
                    //tileMap[currentShape.coordinates[i].x, currentShape.coordinates[i].y].material = Material.Rock;
                }

                //make the Shape Fall
                currentShape = simulateShape(currentShape);


                //draw in the fallen Shape
                for (int i = 0; i < currentShape.coordinates.Count; i++)
                {
                    tileMap[currentShape.coordinates[i].x, currentShape.coordinates[i].y].material = Material.Rock;
                    if (currentShape.coordinates[i].y > highestRock)
                    {

                        highestRock = currentShape.coordinates[i].y;
                    }
                }




            }


            


            //Debug Draw
            // Draw Map
            //Console.Clear();
            for (int y = 100; y >=0; y--)
            {
                for (int x = 0; x < tileMap.GetLength(0); x++)
                {
                    if (tileMap[x, y].material == Material.Air)
                    {
                        Console.Write(".");
                    }
                    else if (tileMap[x, y].material == Material.Rock)
                    {
                        Console.Write("#");
                    }
                    else if (tileMap[x, y].material == Material.fallingRock)
                    {
                        Console.Write("@");
                    }
                    else if (tileMap[x, y].material == Material.Wall)
                    {
                        Console.Write("|");
                    }
                    else if (tileMap[x, y].material == Material.Floor)
                    {
                        Console.Write("_");
                    }
                }
                Console.WriteLine();

            }

            Console.WriteLine("the tower of Rocks will be "+highestRock+" units tall");

            //find pattern
            List<List<Tile>> patternLists = new List<List<Tile>>();
            for(int i=0; i<= 9;i++)
            {
                patternLists.Add(new List<Tile>());
            }
            //copy over line by line to second array
            for(int y=1; y<tileMap.GetLength(1); y++)
            {
                for (int x=0; x<8; x++)
                {
                    patternLists[x].Add(new Tile(tileMap[x,y].coordinate, tileMap[x, y].material));
                }
            }

            //double the array and see if it fits with the first n lines of the original array
            /*
            int y2 = 0;
            bool interrupted = false;
            for (int y = 0; y< (patternLists[0].Count-1)*2; y++)
            {
                for(int x = 0; x < 8; x++)
                {
                    if (tileMap[x, y].material != patternLists[x][y2].material)
                    {
                        interrupted = true;
                    }
                }

                
                if (y2== patternLists[0].Count - 1)
                {
                    y2 = 0;
                }
                else
                {
                    y2++;
                }
            }
            if (interrupted == false)
            {
                Console.WriteLine("pattern found! after"+ (patternLists[0].Count-1) +" rounds");
            }

    */



            Shape simulateShape(Shape inputShape)
            {
                Shape tempShape = new Shape(inputShape.width, inputShape.height, new List<Coordinate>(inputShape.coordinates));

                if (instructionsArray[currentInstructionIndex] == '<')
                {
                    //Console.WriteLine("moving left as per instructions Index " + currentInstructionIndex);
                    tempShape = moveLeft(tileMap, tempShape);
                }
                else if (instructionsArray[currentInstructionIndex] == '>')
                {
                    //Console.WriteLine("moving right as per instructions Index " + currentInstructionIndex);
                    tempShape = moveRight(tileMap, tempShape);
                }
                
                if (currentInstructionIndex + 1 < instructionsArray.Length)
                {
                    currentInstructionIndex++;
                }
                else
                {
                    currentInstructionIndex = 0;
                }
                
                Shape safePointShape = new Shape(tempShape.width, tempShape.height, new List<Coordinate>(tempShape.coordinates));

                //move all coordinates one down
                //Console.WriteLine("moving down");
                for (int i = 0; i < tempShape.coordinates.Count; i++)
                {

                    tempShape.coordinates[i] = new Coordinate(tempShape.coordinates[i].x, tempShape.coordinates[i].y - 1);
                }

                bool collision = false;
                //check if any of them overlap with anything
                for (int i = 0; i < tempShape.coordinates.Count; i++)
                {
                    if (tileMap[tempShape.coordinates[i].x, tempShape.coordinates[i].y].material != Material.Air)
                    {
                        collision = true;

                    }
                }

                if (collision == false)
                {
                    return simulateShape(tempShape);
                }
                else
                {

                    return safePointShape;

                }

            }

        }



        public static Shape fallDown(Tile[,] tileMap,Shape inputShape)
        {
            Shape tempShape = new Shape(inputShape.width,inputShape.height,new List<Coordinate>(inputShape.coordinates));

            //move all coordinates one down
            for(int i=0;i<tempShape.coordinates.Count; i++)
            {
                tempShape.coordinates[i] = new Coordinate(tempShape.coordinates[i].x, tempShape.coordinates[i].y - 1);
            }

            bool collision = false;
            //check if any of them overlap with anything
            for (int i = 0; i < tempShape.coordinates.Count; i++)
            {
                if(tileMap[tempShape.coordinates[i].x, tempShape.coordinates[i].y].material != Material.Air)
                {
                    collision = true;

                }
            }

            if (collision == false)
            {
                return tempShape;
            }
            else
            {
                return inputShape;
            }



        }

        public static Shape moveLeft(Tile[,] tileMap, Shape inputShape)
        {
            Shape tempShape = new Shape(inputShape.width, inputShape.height, new List<Coordinate>(inputShape.coordinates));

            //move all coordinates one down
            for (int i = 0; i < tempShape.coordinates.Count; i++)
            {
                tempShape.coordinates[i] = new Coordinate(tempShape.coordinates[i].x-1, tempShape.coordinates[i].y);
            }

            bool collision = false;
            //check if any of them overlap with anything
            for (int i = 0; i < tempShape.coordinates.Count; i++)
            {
                if (tileMap[tempShape.coordinates[i].x, tempShape.coordinates[i].y].material != Material.Air)
                {
                    collision = true;

                }
            }

            if (collision == false)
            {
                return tempShape;
            }
            else
            {
                return inputShape;
            }

        }

        public static Shape moveRight(Tile[,] tileMap, Shape inputShape)
        {
            Shape tempShape = new Shape(inputShape.width, inputShape.height, new List<Coordinate>(inputShape.coordinates));

            //move all coordinates one down
            for (int i = 0; i < tempShape.coordinates.Count; i++)
            {
                tempShape.coordinates[i] = new Coordinate(tempShape.coordinates[i].x + 1, tempShape.coordinates[i].y);
            }

            bool collision = false;
            //check if any of them overlap with anything
            for (int i = 0; i < tempShape.coordinates.Count; i++)
            {
                if (tileMap[tempShape.coordinates[i].x, tempShape.coordinates[i].y].material != Material.Air)
                {
                    collision = true;

                }
            }

            if (collision == false)
            {
                return tempShape;
            }
            else
            {
                return inputShape;
            }

        }


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

    public struct Shape
    {
        public Shape(int width, int height, List<Coordinate> coordinates)
        {
            this.width = width;
            this.height = height;
            this.coordinates = coordinates;
        }
        public int width;
        public int height;
        public List<Coordinate> coordinates;

        public override string ToString()
        {
            return "width: " + width + " height: " + height ;
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

    public enum Material
    {
        Air,
        fallingRock,
        Rock,
        Wall,
        Floor,

    }
}
