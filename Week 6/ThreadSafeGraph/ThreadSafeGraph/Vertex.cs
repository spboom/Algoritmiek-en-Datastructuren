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
        public List<Edge> toEdges;
        public List<Edge> fromEdges;
        public Vertex[] Vertexes
        {
            get
            {
                List<Vertex> vertexes = new List<Vertex>();
                foreach (Edge edge in toEdges)
                {
                    vertexes.Add(edge.Next);
                }
                return vertexes.ToArray();
            }
        }

        public Vertex(char lab) // constructor
        {
            label = lab;
            wasVisited = false;
            toEdges = new List<Edge>();
            fromEdges = new List<Edge>();
        }

        public void removeEdge(Edge edge)
        {
            if (toEdges.Contains(edge))
            {
                toEdges.Remove(edge);
            }
            if (fromEdges.Contains(edge))
            {
                fromEdges.Remove(edge);
            }
        }

        public void addToEdge(Edge edge)
        {
            toEdges.Add(edge);
        }

        public void addFromEdge(Edge edge)
        {
            fromEdges.Add(edge);
        }

        public override string ToString()
        {
            return base.ToString() + " " + label;
        }

        public void deleteReferences()
        {
            foreach (Edge e in fromEdges)
            {
                removeEdge(e);
            }
            foreach (Edge e in toEdges)
            {
                removeEdge(e);
            }

        }
    }
}
