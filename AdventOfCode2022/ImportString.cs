using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Import
    {
        public static string ImportString()
        {

           string importString = System.IO.File.ReadAllText(@"C:\Users\miche\source\repos\AdventOfCode2022\InputFiles\Day_11.txt");
            return importString;
        }

    }
}
