using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day_12
    {
        public static void Day_12_Part01()
        {
            string importString = Import.ImportString("Day_12.txt");
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            Tile[,] tileMap = new Tile[lines[0].Length, lines.Length];


            Coordinate StartingPosition = new Coordinate();
            Coordinate EndPosition = new Coordinate();

            List<Tile> frontierList = new List<Tile>();
            List<Tile> potentialStarts = new List<Tile>();

            //set up height map array
            for (int y = 0; y < lines.Length; y++)
            {
                char[] columns = lines[y].ToCharArray();

                for (int x = 0; x < columns.Length; x++)
                {
                    if (columns[x] != 'S' && columns[x] != 'E')
                    {
                        tileMap[x, y].height = Convert.ToInt32(columns[x]);
                        tileMap[x, y].coordinate = new Coordinate(x, y);
                        if (columns[x] == 'a')
                        {
                            potentialStarts.Add(tileMap[x, y]);
                        }
                        // Console.WriteLine("height at " + x + "," + y + " is " + heightmap[x, y]);
                        //regular character needs to be converted to a height
                    }
                    else if (columns[x] == 'S')
                    {
                        tileMap[x, y].height = Convert.ToInt32('a');
                        tileMap[x, y].coordinate = new Coordinate(x, y);
                        tileMap[x, y].entryDirection = EntryDirection.StartingPosition;
                        StartingPosition = new Coordinate(x, y);
                        //frontierList.Add(tileMap[x, y]);
                        potentialStarts.Add(tileMap[x, y]);
                        //Console.WriteLine("Starting height at " + x + "," + y + " is " + heightmap[x, y]);


                    }
                    else if (columns[x] == 'E')
                    {
                        tileMap[x, y].height = Convert.ToInt32('z');
                        tileMap[x, y].coordinate = new Coordinate(x, y);
                        EndPosition = new Coordinate(x, y);
                        //Console.WriteLine("End height at " + x + "," + y + " is " + heightmap[x, y]);

                    }
                }
            }

            List<int> pathCounterList = new List<int>();
            //Console.WriteLine("number of potential starts: "+potentialStarts.Count);

            Console.WriteLine("My approach for this one is a bit brute-force so this might take a while");
            Console.WriteLine("Part 01:");
            retraceSteps(tileMap[StartingPosition.x,StartingPosition.y]);

            Console.WriteLine("Part 02:");
        foreach(Tile Start in potentialStarts)
            {
                retraceSteps(Start);
                
            }
            //Breadth first search through the Tile array
            //Console.WriteLine(pathCounterList.Count+" paths found");
            /*
            foreach(int path in pathCounterList)
            {
                Console.WriteLine(path);
            }
            */
            Console.WriteLine("The shortest path is " + pathCounterList.Min());

            void retraceSteps(Tile Start)
            {
                //Console.WriteLine("current Start:" +Start);
                // reset visited locations
                for (int x = 0; x < tileMap.GetLength(0); x++)
                {
                    for (int y = 0; y < tileMap.GetLength(1); y++)
                    {
                        tileMap[x, y].entryDirection = EntryDirection.Unvisited;
                    }
                }

                tileMap[Start.coordinate.x, Start.coordinate.y].entryDirection = EntryDirection.StartingPosition;
                frontierList = new List<Tile>() { Start };
                for (int i = 0; i < frontierList.Count; i++)
                {
                    Tile currentTile = frontierList[i];
                    int currentX = currentTile.coordinate.x;
                    int currentY = currentTile.coordinate.y;

                    if (currentX - 1 >= 0 && tileMap[currentX - 1, currentY].entryDirection == EntryDirection.Unvisited)
                    {
                        //we could go left
                        if (currentTile.height == tileMap[currentX - 1, currentY].height
                            || currentTile.height > tileMap[currentX - 1, currentY].height
                            || currentTile.height + 1 == tileMap[currentX - 1, currentY].height)
                        {
                            //Console.WriteLine("valid position found to the left, added new child");
                            tileMap[currentX - 1, currentY].entryDirection = EntryDirection.East;
                            if (!frontierList.Contains(tileMap[currentX - 1, currentY]))
                            {
                                frontierList.Add(tileMap[currentX - 1, currentY]);
                            }
                        }
                    }
                    if (currentX + 1 < tileMap.GetLength(0) && tileMap[currentX + 1, currentY].entryDirection == EntryDirection.Unvisited)
                    {
                        //we could go right
                        if (currentTile.height == tileMap[currentX + 1, currentY].height
                            || currentTile.height > tileMap[currentX + 1, currentY].height
                            || currentTile.height + 1 == tileMap[currentX + 1, currentY].height)
                        {
                            //Console.WriteLine("valid position found to the right, added new child");
                            tileMap[currentX + 1, currentY].entryDirection = EntryDirection.West;
                            if (!frontierList.Contains(tileMap[currentX + 1, currentY]))
                            {
                                frontierList.Add(tileMap[currentX + 1, currentY]);
                            }
                        }
                    }
                    if (currentY - 1 >= 0 && tileMap[currentX, currentY - 1].entryDirection == EntryDirection.Unvisited)
                    {
                        //we could go up
                        if (currentTile.height == tileMap[currentX, currentY - 1].height
                            || currentTile.height > tileMap[currentX, currentY - 1].height
                            || currentTile.height + 1 == tileMap[currentX, currentY - 1].height)
                        {
                            //Console.WriteLine("valid position found to the north, added new child");
                            tileMap[currentX, currentY - 1].entryDirection = EntryDirection.South;
                            if (!frontierList.Contains(tileMap[currentX, currentY - 1]))
                            {
                                frontierList.Add(tileMap[currentX, currentY - 1]);
                            }
                        }
                    }
                    if (currentY + 1 < tileMap.GetLength(1) && tileMap[currentX, currentY + 1].entryDirection == EntryDirection.Unvisited)
                    {
                        //we could go left
                        if (currentTile.height == tileMap[currentX, currentY + 1].height
                            || currentTile.height > tileMap[currentX, currentY + 1].height
                            || currentTile.height + 1 == tileMap[currentX, currentY + 1].height)
                        {
                            //Console.WriteLine("valid position found to the north, added new child");
                            tileMap[currentX, currentY + 1].entryDirection = EntryDirection.North;
                            if (!frontierList.Contains(tileMap[currentX, currentY + 1]))
                            {
                                frontierList.Add(tileMap[currentX, currentY + 1]);
                            }
                        }
                    }

                }

                //retrace steps from end position

                Coordinate currentPosition = EndPosition;
                int pathCounter = 0;
                bool invalidStart = false;
                while (!currentPosition.Equals(new Coordinate(Start.coordinate.x, Start.coordinate.y)) && invalidStart == false)
                {
                    //Console.WriteLine("current Position: " + currentPosition);
                    switch (tileMap[currentPosition.x, currentPosition.y].entryDirection)
                    {
                        case EntryDirection.West:
                            currentPosition = new Coordinate(currentPosition.x - 1, currentPosition.y);
                            pathCounter++;
                            break;

                        case EntryDirection.East:
                            currentPosition = new Coordinate(currentPosition.x + 1, currentPosition.y);
                            pathCounter++;
                            break;

                        case EntryDirection.North:
                            currentPosition = new Coordinate(currentPosition.x, currentPosition.y - 1);
                            pathCounter++;
                            break;

                        case EntryDirection.South:
                            currentPosition = new Coordinate(currentPosition.x, currentPosition.y + 1);
                            pathCounter++;
                            break;

                        case EntryDirection.Unvisited:
                            //Console.WriteLine("Found an unvisited Tile");
                            invalidStart = true;
                            break;
                    }
                }
                if (invalidStart == false)
                {
                    Console.WriteLine("The found Path is " + pathCounter + " units long");
                    pathCounterList.Add(pathCounter);
                }
            }


        }

        public enum EntryDirection
        {
            Unvisited,
            North,
            East,
            South,
            West,
            StartingPosition
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
            public Tile(Coordinate coordinate, int height, EntryDirection entryDirection)
            {
                this.coordinate = coordinate;
                this.height = height;
                this.entryDirection = entryDirection;
            }
            public Coordinate coordinate;
            public int height;
            public EntryDirection entryDirection;

            public override string ToString()
            {
                return "x: " + coordinate.x + " y: " + coordinate.y + " height: " + height + " entryDirection: " + entryDirection;
            }
        }




        /*
// set up tree
Tree<Coordinate> pathTree = new Tree<Coordinate>();
pathTree.Root = new TreeNode<Coordinate>() { Data = new Coordinate(0, 0), Parent = null };
pathTree.Root.Children = new List<TreeNode<Coordinate>>();
pathTree.Root.Path = new List<Coordinate>() {new Coordinate (pathTree.Root.Data.x, pathTree.Root.Data.y) };

List<TreeNode<Coordinate>> nodeList = new List<TreeNode<Coordinate>>();
nodeList.Add(pathTree.Root);

List<TreeNode<Coordinate>> nonLeafList = new List<TreeNode<Coordinate>>();

List<int> pathLengthList = new List<int>();


//set up height map array
for (int y = 0; y < lines.Length; y++)
{
    char[] columns = lines[y].ToCharArray();

    for (int x = 0; x < columns.Length; x++)
    {
        if (columns[x] != 'S' && columns[x] != 'E')
        {
            heightmap[x, y] = Convert.ToInt32(columns[x]);
           // Console.WriteLine("height at " + x + "," + y + " is " + heightmap[x, y]);
            //regular character needs to be converted to a height
        } else if (columns[x] == 'S')
        {
            heightmap[x, y] = Convert.ToInt32('a');
            StartingPosition = new Coordinate(x, y);
            pathTree.Root.Data = StartingPosition;

            Console.WriteLine("Starting height at " + x + "," + y + " is " + heightmap[x, y]);


        }
        else if (columns[x] == 'E')
        {
            heightmap[x, y] = Convert.ToInt32('z');
            EndPosition = new Coordinate(x, y);
            Console.WriteLine("End height at " + x + "," + y + " is " + heightmap[x, y]);

        }
    }
}

// how about we make a tree where starting Position is the root and then every possible next step is a child
// if I filter the "legal" moves before addign them to the graph I might not even need to store the height in the Node
// my tree has a height function already, in theory the End position leaf note with the smallest height would be our answer



for (int i = 0; i < nodeList.Count; i++)
{
    Console.WriteLine("i: "+i);
    TreeNode<Coordinate> leaf = nodeList[i];
    Console.WriteLine(" current Node"+ leaf.Data);

        //processing:

    if (leaf.Data.x - 1 >= 0 &&entryDirectionMap[leaf.Data.x-1,leaf.Data.y] == EntryDirection.Unvisited )
    {
        if (heightmap[leaf.Data.x, leaf.Data.y] == heightmap[leaf.Data.x - 1, leaf.Data.y]
            || heightmap[leaf.Data.x, leaf.Data.y] - 1 == heightmap[leaf.Data.x - 1, leaf.Data.y]
            || heightmap[leaf.Data.x, leaf.Data.y] + 1 == heightmap[leaf.Data.x - 1, leaf.Data.y])
        {
        //Console.WriteLine("valid position found to the left, added new child");
        TreeNode<Coordinate> newChild = new TreeNode<Coordinate>()
        {
            Data = new Coordinate(leaf.Data.x - 1, leaf.Data.y),
            Parent = leaf,
            Children = new List<TreeNode<Coordinate>>(),
            Path = new List<Coordinate>(leaf.Path)
        };
            entryDirectionMap[leaf.Data.x - 1, leaf.Data.y] = EntryDirection.East;
        newChild.Path.Add(new Coordinate(newChild.Data.x,newChild.Data.y));
        leaf.Children.Add(newChild);


        if (!nonLeafList.Contains(leaf))
        {
            nonLeafList.Add(leaf);
        }

        }

        //we could go left
    }
    if (leaf.Data.x + 1 < heightmap.GetLength(0) && entryDirectionMap[leaf.Data.x +1 , leaf.Data.y]==EntryDirection.Unvisited )
    {
        //we could go right
        if (heightmap[leaf.Data.x, leaf.Data.y] == heightmap[leaf.Data.x + 1, leaf.Data.y]
            || heightmap[leaf.Data.x, leaf.Data.y] - 1 == heightmap[leaf.Data.x + 1, leaf.Data.y]
            || heightmap[leaf.Data.x, leaf.Data.y] + 1 == heightmap[leaf.Data.x + 1, leaf.Data.y])
        {
            //Console.WriteLine("valid position found to the right, added new child");
            TreeNode<Coordinate> newChild = new TreeNode<Coordinate>()
            {
                Data = new Coordinate(leaf.Data.x + 1, leaf.Data.y),
                Parent = leaf,
                Children = new List<TreeNode<Coordinate>>(),
                Path = new List<Coordinate>(leaf.Path)
            };

            entryDirectionMap[leaf.Data.x + 1, leaf.Data.y] = EntryDirection.West;
            newChild.Path.Add(new Coordinate(newChild.Data.x, newChild.Data.y));
            leaf.Children.Add(newChild);
            if (!nonLeafList.Contains(leaf))
            {
                nonLeafList.Add(leaf);
            }
        }

    }
    if (leaf.Data.y - 1 >= 0 && entryDirectionMap[leaf.Data.x, leaf.Data.y-1] == EntryDirection.Unvisited)
    {
        //we could go up
        if (heightmap[leaf.Data.x, leaf.Data.y] == heightmap[leaf.Data.x, leaf.Data.y - 1]
            || heightmap[leaf.Data.x, leaf.Data.y] - 1 == heightmap[leaf.Data.x, leaf.Data.y - 1]
            || heightmap[leaf.Data.x, leaf.Data.y] + 1 == heightmap[leaf.Data.x, leaf.Data.y - 1])
        {
            //Console.WriteLine("valid position found to the north, added new child");
            TreeNode<Coordinate> newChild = new TreeNode<Coordinate>()
            {
                Data = new Coordinate(leaf.Data.x, leaf.Data.y-1),
                Parent = leaf,
                Children = new List<TreeNode<Coordinate>>(),
                Path = new List<Coordinate>(leaf.Path)
            };
            entryDirectionMap[leaf.Data.x, leaf.Data.y-1] = EntryDirection.South;
            newChild.Path.Add(new Coordinate(newChild.Data.x, newChild.Data.y));
            leaf.Children.Add(newChild);
            if (!nonLeafList.Contains(leaf))
            {
                nonLeafList.Add(leaf);
            }
        }
    }
    if (leaf.Data.y + 1 < heightmap.GetLength(1) && entryDirectionMap[leaf.Data.x, leaf.Data.y+1] == EntryDirection.Unvisited)
    {
        //we could go left
        if (heightmap[leaf.Data.x, leaf.Data.y] == heightmap[leaf.Data.x, leaf.Data.y + 1]
            || heightmap[leaf.Data.x, leaf.Data.y] - 1 == heightmap[leaf.Data.x, leaf.Data.y + 1]
            || heightmap[leaf.Data.x, leaf.Data.y] + 1 == heightmap[leaf.Data.x, leaf.Data.y + 1])
        {
            //Console.WriteLine("valid position found to the south, added new child");
            TreeNode<Coordinate> newChild = new TreeNode<Coordinate>()
            {
                Data = new Coordinate(leaf.Data.x, leaf.Data.y+1),
                Parent = leaf,
                Children = new List<TreeNode<Coordinate>>(),
                Path = new List<Coordinate>(leaf.Path)
            };
            entryDirectionMap[leaf.Data.x, leaf.Data.y + 1] = EntryDirection.North;
            newChild.Path.Add(new Coordinate(newChild.Data.x, newChild.Data.y));
            leaf.Children.Add(newChild);
            if (!nonLeafList.Contains(leaf))
            {
                nonLeafList.Add(leaf);
            }
        }
    }

    foreach(TreeNode<Coordinate> child in leaf.Children)
    {
        if (child.Data.x != EndPosition.x || child.Data.y !=EndPosition.y)
        {
            nodeList.Add(child);
        }
        else
        {
            Console.WriteLine("found an end leaf with length "+leaf.Path.Count);
            pathLengthList.Add(leaf.Path.Count);
        }

    }

}


foreach(TreeNode<Coordinate> node in nodeList)
{
    if (!nonLeafList.Contains(node) && node.Data.x == EndPosition.x && node.Data.y == EndPosition.y)
    {
        int currentHeight = node.GetHeight()-1;
        Console.WriteLine("one of the possible Paths is "+currentHeight + "units long");
        Console.WriteLine("or maybe "+ node.Path.Count + " units?" );
    }
}

// Console.WriteLine("shortest possible Path " + pathLengthList.Min());



Coordinate currentPosition = EndPosition;
int pathCounter = 0;
while (!currentPosition.Equals(StartingPosition))
{
    Console.WriteLine("current Position: "+currentPosition);
    switch (entryDirectionMap[currentPosition.x, currentPosition.y])
    {
        case EntryDirection.West:
            currentPosition = new Coordinate(currentPosition.x - 1, currentPosition.y);
            pathCounter++;
            break;

        case EntryDirection.East:
            currentPosition = new Coordinate(currentPosition.x + 1, currentPosition.y);
            pathCounter++;
            break;

        case EntryDirection.North:
            currentPosition = new Coordinate(currentPosition.x, currentPosition.y - 1);
            pathCounter++;
            break;

        case EntryDirection.South:
            currentPosition = new Coordinate(currentPosition.x, currentPosition.y + 1);
            pathCounter++;
            break;

        case EntryDirection.Unvisited:
            Console.WriteLine("Found an unvisited Tile");
            break;
    }
}
Console.WriteLine("The found Path is " + pathCounter + "unit long");
*/

    }

}