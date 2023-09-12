using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Import
    {
        public static string ImportString(string fileName)
        {

           string importString = System.IO.File.ReadAllText( @"..\..\InputFiles\"+fileName);
           return importString;
        }

    }
}
