using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSafeGraph
{
    public class Vertex
    {
        public char label; // label (e.g. ‘A’)
        public bool wasVisited;
        private List<Edge> edges;

        public Vertex(char lab) // constructor
        {
            label = lab;
            wasVisited = false;
            edges = new List<Edge>();
        }

        public void removeEdge(Edge edge)
        {
            edges.Remove(edge);
        }

        public void addEdge(Edge edge)
        {
            edges.Add(edge);
        }
    }
}
