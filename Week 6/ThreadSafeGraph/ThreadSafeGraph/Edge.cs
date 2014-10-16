using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSafeGraph
{
    class Edge
    {
        public Vertex Next;
        public Vertex Previous;

        public Edge(Vertex next, Vertex previous)
        {
            Next = next;
            Previous = previous;
        }

        public void changeEdge(Vertex next, Vertex previous)
        {
            Next = next;
            Previous = previous;
        }


    }
}
