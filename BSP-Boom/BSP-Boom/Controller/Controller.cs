using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSP_Boom.Model;

namespace BSP_Boom.Controller
{
    class Controller
    {
        GameObject[] gameObjects;
        public Controller()
        {
            gameObjects = new GameObject[8];
            gameObjects[0] = new GameObject(new double[] { 900, 100 });
            gameObjects[1] = new GameObject(new double[] { 100, 100 });
            gameObjects[2] = new GameObject(new double[] { 50, 750 });
            gameObjects[3] = new GameObject(new double[] { 110, 90 });
            gameObjects[4] = new GameObject(new double[] { 950, 50 });
            gameObjects[5] = new GameObject(new double[] { 60, 800 });
            gameObjects[6] = new GameObject(new double[] { 40, 800 });
            gameObjects[7] = new GameObject(new double[] { 700, 850 });
        }
        public GameObject[] QuickSort(int min, int max, int dimension)
        {
            if (max - min <= 1)
            {
                return null;
            }
            dimension = dimension % GameObject.DIMENSION;

            int middlePos = median(min, max, dimension);

            double middle = gameObjects[middlePos].getPosition(dimension);
            Console.WriteLine("Dimension:" + (dimension == 0 ? "x" : "y") + " value: " + middle);

            int lower = min, upper = max;
            while (lower < upper)
            {
                while (lower < gameObjects.Length && lower < middlePos && lower < max && gameObjects[lower].getPosition(dimension) <= middle)
                {
                    lower++;
                }
                while (upper > 0 && upper > middlePos && upper > lower && gameObjects[upper].getPosition(dimension) >= middle)
                {
                    upper--;
                }
                swap<GameObject>(lower, upper, gameObjects);

                if (middlePos == upper)
                {
                    middlePos = lower;
                    //upper--;
                }

                else if (middlePos == lower)
                {
                    middlePos = upper;
                    //lower++;
                }
                //else
                {
                    //lower++;
                    //upper--;
                }
            }
            //if (max - min > 2)
            {
                QuickSort(min, middlePos, dimension + 1);
                QuickSort(middlePos + 1, max, dimension + 1);
            }

            return null;
        }

        private T[] swap<T>(int pos1, int pos2, T[] array)
        {
            T temp = array[pos1];
            array[pos1] = array[pos2];
            array[pos2] = temp;

            return array;
        }

        private int median(int min, int max, int dimension)
        {
            int middlePos = (min + max) / 2;
            double first, middle, last;
            first = gameObjects[min].getPosition(dimension);
            middle = gameObjects[middlePos].getPosition(dimension);
            last = gameObjects[max].getPosition(dimension);

            if (first <= middle && first <= last)
            {
                if (middle <= last)
                {
                    swap<GameObject>(middlePos, max - 1, gameObjects);
                }
                else
                {
                    swap<GameObject>(max, max-1, gameObjects);
                    swap<GameObject>(middlePos, max, gameObjects);
                }
            }
            else if (middle <= first && middle <= last)
            {
                if (first <= last)
                {
                    swap<GameObject>(min, max - 1, gameObjects);
                }
                else
                {
                    swap<GameObject>(max, max - 1, gameObjects);
                    swap<GameObject>(min, max, gameObjects);
                }
            }
            else if (last <= first && last <= middle)//TODO
            {
                if (first <= middle)
                {
                    swap<GameObject>(min, max - 1, gameObjects);
                }
                else
                {
                    swap<GameObject>(max, max - 1, gameObjects);
                    swap<GameObject>(min, max, gameObjects);
                }
            }
            /*
            if (first >= middle && first <= last || first <= middle && first >= last)
            {
                return min;
            }

            if (middle >= first && middle <= last || middle <= first && middle >= last)
            {
                return middlePos;
            }

            if (last >= first && last <= middle || last <= first && last >= middle)
            {
                return max;
            }
            */

            return -1;
        }

        public void Sort()
        {
            QuickSort(0, gameObjects.Length - 1, 0);
        }
    }
}
