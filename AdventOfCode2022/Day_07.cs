using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day_07
    {
        public static void Day_07_Part01()
        {
            string importString = Import.ImportString("Day_07.txt");
            string[] lines = importString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            Tree<dataPoint> tree = new Tree<dataPoint>();
            TreeNode<dataPoint> currentDirectory;

            List<TreeNode<dataPoint>> directoryList = new List<TreeNode<dataPoint>>();
            List<TreeNode<dataPoint>> fileList = new List<TreeNode<dataPoint>>();

            // set up root
            tree.Root = new TreeNode<dataPoint>() { Data = new dataPoint(FileType.Root, 0, "root"), Parent = null };
            tree.Root.Children = new List<TreeNode<dataPoint>>();
            currentDirectory = tree.Root;

            //put data into tree
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("$ ls"))
                {
                   // Console.WriteLine("list directory: " + currentDirectory.Data.fileName);
                }
                else if (lines[i].Contains("$ cd"))
                {
                    if (lines[i].Contains(".."))
                    {
                        currentDirectory = currentDirectory.Parent;
                        //Console.WriteLine("return to parent directory " + currentDirectory.Data.fileName);

                    }
                    else if (lines[i].Contains("/"))
                    {
                       // Console.WriteLine("start");
                    }
                    else
                    {
                        string[] tempString = lines[i].Split(' ');
                        currentDirectory = currentDirectory.Children.Find(dP => dP.Data.fileName == tempString[2]);

                        //Console.WriteLine(" enter directory " + currentDirectory.Data.fileName);

                    }

                }
                else

                {
                    if (lines[i].Contains("dir"))
                    {
                        string[] tempString = lines[i].Split(' ');
                        currentDirectory.Children.Add(new TreeNode<dataPoint>()
                        {
                            Data = new dataPoint(FileType.Directory, 0, tempString[1]),
                            Parent = currentDirectory,
                            Children = new List<TreeNode<dataPoint>>()
                        }
                        );

                        directoryList.Add(currentDirectory.Children[currentDirectory.Children.Count - 1]);
                        //Console.WriteLine("add new Directory Node " + currentDirectory.Children[currentDirectory.Children.Count - 1].Data.fileName);
                    }
                    else
                    {
                        string[] tempString = lines[i].Split(' ');
                        currentDirectory.Children.Add(new TreeNode<dataPoint>()
                        {
                            Data = new dataPoint(FileType.File, Convert.ToInt32(tempString[0]), tempString[1]),
                            Parent = currentDirectory,
                            Children = new List<TreeNode<dataPoint>>()
                        });

                        fileList.Add(currentDirectory.Children[currentDirectory.Children.Count - 1]);
                        //Console.WriteLine("add new file Node " + currentDirectory.Children[currentDirectory.Children.Count - 1].Data.fileName);
                    }
                }



            }

            int maxHeight = 0;

            //find deletable directories
            foreach (TreeNode<dataPoint> file in fileList)
            {
                int currentHeight = file.GetHeight();
                if (currentHeight > maxHeight)
                {
                    maxHeight = currentHeight;
                }
            }
            //Console.WriteLine("Max Height is " + maxHeight);


            List<int> directorySizeList = new List<int>();
            foreach (TreeNode<dataPoint> directory in directoryList)
            {

                int directorySize = 0;
                List<TreeNode<dataPoint>> parentList = new List<TreeNode<dataPoint>>();
                List<TreeNode<dataPoint>> childFileList = new List<TreeNode<dataPoint>>();

                parentList.Add(directory);
                for (int i = 0; i < parentList.Count; i++)
                {
                    TreeNode<dataPoint> parent = parentList[i];
                    foreach (TreeNode<dataPoint> child in parent.Children)
                    {
                        if (child.Children.Count > 0)
                        {
                            parentList.Add(child);
                        }
                    }
                }

                foreach (TreeNode<dataPoint> parent in parentList)
                {

                    childFileList.AddRange(parent.Children.FindAll(dP => dP.Data.fileType == FileType.File));
                }

                foreach (TreeNode<dataPoint> child in childFileList)
                {
                    directorySize = directorySize + child.Data.fileSize;
                }
                //Console.WriteLine("directory Size: "+directorySize);
                directorySizeList.Add(directorySize);
            }

            int totalDeletableDirectoriesSize = 0;
            List<int> descendingDirectorySizeList = directorySizeList.OrderByDescending(x => x).ToList();
            foreach (int directorySize in directorySizeList)
            {
                if (directorySize <= 100000)
                {
                    totalDeletableDirectoriesSize = totalDeletableDirectoriesSize + directorySize;

                }
            }
            Console.WriteLine("total size of deletable directories: " + totalDeletableDirectoriesSize);

            //Part02:

            int totalUsedFileSpace = 0;
            foreach(TreeNode<dataPoint> file in fileList)
            {
                totalUsedFileSpace = totalUsedFileSpace + file.Data.fileSize;
            }
            Console.WriteLine("total used up space is: "+ totalUsedFileSpace);
            int toFreeUp = totalUsedFileSpace - 40000000;
            Console.WriteLine("space to free up: " + toFreeUp);

            Console.WriteLine("descending DirectorySize List Count: "+descendingDirectorySizeList.Count);
            foreach(int ds in descendingDirectorySizeList)
            {
                Console.WriteLine("ds: "+ds);
                if(ds >= toFreeUp)
                {
                    Console.WriteLine(" eligible directory to delete: "+ ds);
                }
            }

            List<TreeNode<dataPoint>> parents = new List<TreeNode<dataPoint>>();

        }

        public struct dataPoint
        {
            public dataPoint(FileType fileType ,int fileSize, string fileName)
            {
                this.fileType = fileType;
                this.fileSize = fileSize;
                this.fileName = fileName;
            }
            public FileType fileType;
            public int fileSize;
            public string fileName;

            public override string ToString()
            {
                return "fileType: " + fileType+ " fileSize: "+ fileSize +" fileName: "+fileName;
            }
        }

        public enum FileType
        {
            Root,
            Directory,
            File
        }


    }
}
