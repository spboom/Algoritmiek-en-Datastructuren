using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSafeGraph
{
    public class Edge
    {
        private Vertex next;
        public Vertex Next
        {
            get { return next; }
            set
            {
                if (Next != null)
                {
                    Next.removeEdge(this);
                }
                next = value;
                if (Next != null && !Next.fromEdges.Contains(this))
                {
                    Next.addFromEdge(this);
                }
            }
        }

        private Vertex previous;
        public Vertex Previous
        {
            get { return previous; }
            set
            {
                if (Previous != null)
                {
                    Previous.removeEdge(this);
                }

                previous = value;
                if (Previous != null && !Previous.toEdges.Contains(this))
                {
                    Previous.addToEdge(this);
                }
            }
        }

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

        public void deleteReferences()
        {
            Next.removeEdge(this);
            Previous.removeEdge(this);
        }
    }
}
