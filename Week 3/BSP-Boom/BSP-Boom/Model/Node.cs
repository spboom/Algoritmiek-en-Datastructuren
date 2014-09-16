using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSP_Boom.Model
{
    class Node
    {
        private Node Parent { get; set; }

        public Node(Node parent)
        {
            Parent = parent;
        }
    }
}
