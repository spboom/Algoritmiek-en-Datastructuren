using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSP_Boom.Model
{
    class EndNode : Node
    {
        public GameObject Value { get; private set; }

        public EndNode(Node parent, GameObject value)
            : base(parent)
        {
            Value = value;
        }

        public override double lowerBound(int index)
        {
            return getValue(index);
        }

        public override double upperBound(int index)
        {
            return getValue(index);
        }

        private double getValue(int index)
        {
            return Value.getPosition(index);
        }
    }
}
