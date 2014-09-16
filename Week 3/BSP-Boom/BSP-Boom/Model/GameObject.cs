using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSP_Boom.Model
{
    class GameObject
    {
        public static readonly int DIMENSION = 2;
        private double[] position = new double[DIMENSION];

        public GameObject(double[] position)
        {
            this.position = position;
        }

        public double getPosition(int index)
        {
            return position[index];
        }

        public override string ToString()
        {
            String stringValue = "[";
            for (int i = 0; i < DIMENSION; i++)
            {
                if (i != 0)
                {
                    stringValue += ", ";
                }
                stringValue += getPosition(i);
            }
            stringValue += "]";

            return stringValue;
        }
    }
}
