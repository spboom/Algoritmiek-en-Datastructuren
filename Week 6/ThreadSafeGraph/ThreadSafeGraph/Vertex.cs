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

        public Vertex(char lab) // constructor
        {
            label = lab;
            wasVisited = false;
        }
    } 
}
