using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSP_Boom.Model
{
    class SplitNode : Node
    {
        public Node leftChild { get; private set; }
        public Node RightChild { get; private set; }

        private double[] lowerArray;
        private double[] upperArray;

        public SplitNode(Node parent)
            : base(parent)
        {
            lowerArray = new double[GameObject.DIMENSION];
            upperArray = new double[GameObject.DIMENSION];

            for (int i = 0; i < lowerArray.Length; i++)
            {
                lowerArray[i] = -1;
            }
            for (int i = 0; i < upperArray.Length; i++)
            {
                upperArray[i] = -1;
            }
        }

        public void SetChilds(Node left, Node right)
        {
            leftChild = left;
            RightChild = right;
        }

        public override double lowerBound(int index)
        {
            if (lowerArray[index] == -1)
            {
                double left, right;
                if (leftChild != null)
                {
                    left = leftChild.lowerBound(index);
                }
                else
                {
                    left = double.MaxValue;
                }

                if (RightChild != null)
                {
                    right = RightChild.lowerBound(index);
                }
                else
                {
                    right = double.MaxValue;
                }

                lowerArray[index] = left < right ? left : right;
            }

            return lowerArray[index];
        }

        public override double upperBound(int index)
        {
            if (upperArray[index] == -1)
            {
                double left, right;
                if (leftChild != null)
                {
                    left = leftChild.upperBound(index);
                }
                else
                {
                    left = double.MinValue;
                }

                if (RightChild != null)
                {
                    right = RightChild.upperBound(index);
                }
                else
                {
                    right = double.MinValue;
                }

                upperArray[index] = left > right ? left : right;
            }
            return upperArray[index];
        }
    }
}
