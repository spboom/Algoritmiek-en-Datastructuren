using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSP_Boom.Model
{
    class SplitNode : Node
    {
        private Node leftChild;

        private Node RightChild;

        public SplitNode(Node parent)
            : base(parent)
        { }

        public void SetChilds(Node left, Node right)
        {
            leftChild = left;
            RightChild = right;
        }
    }
}
