using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSP_Boom.Model
{
    class EndNode<T> : Node
    {
        public T Value { get; private set; }

        public EndNode(Node parent, T value)
            : base(parent)
        {
            Value = value;
        }
    }
}
