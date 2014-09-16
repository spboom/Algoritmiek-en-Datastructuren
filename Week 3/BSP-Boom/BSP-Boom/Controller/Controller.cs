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
        public Node QuickSort(int min, int max, int dimension, Node parent = null)
        {
            if (max - min == 0)
            {
                return new EndNode<GameObject>(parent, gameObjects[min]);
            }
            dimension = dimension % GameObject.DIMENSION;

            double middle = median(min, max, dimension);

            int middlePos = max - 1;
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
                }

                else if (middlePos == lower)
                {
                    middlePos = upper;
                }
            }

            SplitNode node = new SplitNode(parent);
            Node left = null;
            Node right = null;

            if (min < middlePos)
            {
                left = QuickSort(min, middlePos, dimension + 1, node);
            }
            if (middlePos <= max)
            {
                right = QuickSort(middlePos + 1, max, dimension + 1, node);
            }

            node.SetChilds(left, right);
            return node;

        }

        private T[] swap<T>(int pos1, int pos2, T[] array)
        {
            T temp = array[pos1];
            array[pos1] = array[pos2];
            array[pos2] = temp;

            return array;
        }

        private double median(int min, int max, int dimension)
        {
            int middle = (min + max) / 2;

            if (gameObjects[min].getPosition(dimension) > gameObjects[middle].getPosition(dimension))
            {
                swap<GameObject>(min, middle, gameObjects);
            }

            if (gameObjects[min].getPosition(dimension) > gameObjects[max].getPosition(dimension))
            {
                swap<GameObject>(min, max, gameObjects);
            }

            if (gameObjects[middle].getPosition(dimension) > gameObjects[max].getPosition(dimension))
            {
                swap<GameObject>(middle, max, gameObjects);
            }

            swap<GameObject>(middle, max - 1, gameObjects);

            return gameObjects[max - 1].getPosition(dimension);


        }

        public void Sort()
        {
            Node root = QuickSort(0, gameObjects.Length - 1, 0);
            //Node root = ArrayToBSBTree<GameObject>(gameObjects, 0, gameObjects.Length - 1);
        }

        private Node ArrayToBSBTree<T>(T[] array, int min, int max, Node parent = null)
        {
            int middle = (max + min) / 2;
            if (max - min == 0)
            {
                return new EndNode<T>(parent, array[middle]);
            }
            else
            {
                SplitNode node = new SplitNode(parent);
                Node left = null;
                Node right = null;
                if (middle >= min)
                {
                    left = ArrayToBSBTree<T>(array, min, middle, node);
                }
                if (middle < max)
                {
                    right = ArrayToBSBTree<T>(array, middle + 1, max, node);
                }
                node.SetChilds(left, right);
                return node;
            }
        }
    }
}
