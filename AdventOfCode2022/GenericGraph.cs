using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Graph<T>
    {
        public List<GraphNode<T>> nodes { get; set; }
    }

    public class GraphNode<T>
    {
        public T Data { get; set; }
        public List<GraphNode<T>> neighbours { get; set; }


    }
}
