using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            gameObjects = new GameObject[0];
        }

        public void fill(int amount, bool seeded = true)
        {
            Random r = new Random();
            if (seeded)
            {
                r = new Random(1);
            }

            gameObjects = new GameObject[amount];
            for (int i = 0; i < amount; i++)
            {
                gameObjects[i] = new GameObject(new double[] { r.NextDouble() * (double.MaxValue - 1), r.NextDouble() * (double.MaxValue - 1) });
            }
        }
        public Node QuickSort(int min, int max, int dimension, Node parent = null)
        {
            if (max - min == 0)
            {
                return new EndNode(parent, gameObjects[min]);
            }
            dimension = dimension % GameObject.DIMENSION;

            double middle = median(min, max, dimension);

            int middlePos = max - 1;
            //Console.WriteLine("Dimension:" + (dimension == 0 ? "x" : "y") + " value: " + middle);

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
                if (gameObjects[lower].getPosition(dimension) > gameObjects[upper].getPosition(dimension))
                {
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
            }

            SplitNode node = new SplitNode(parent);
            Node left = null;
            Node right = null;

            if (min <= middlePos)
            {
                left = QuickSort(min, middlePos, dimension + 1, node);
            }
            if (middlePos < max)
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

            //set upper and lower bound array in splitnode
            for (int i = 0; i < GameObject.DIMENSION; i++)
            {
                root.lowerBound(i);
                root.upperBound(i);
            }

            var found = search(900, 100, root);
        }

        public void benchmark1()
        {
            Stopwatch exisitngTree = new Stopwatch();
            Stopwatch nonExisitngTree = new Stopwatch();
            Stopwatch array = new Stopwatch();
            int times = 10000, objects = 3;

            int[] values = new int[] { 5, 50, 500 };

            for (int w = 0; w <= objects; w++)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    fill(values[i]);
                    Random r = new Random(1);
                    int pos = r.Next(values[i]);
                    double x = -1, y = -1;

                    List<int> ints = new List<int>() { w };
                    for (int l = w - 1; l > 0; l--)
                    {
                        int g = w;
                        while (ints.Contains(g))
                        {
                            g = r.Next(gameObjects.Length);
                        }
                        ints.Add(g);
                        gameObjects[g] = gameObjects[w];
                    }

                    x = gameObjects[pos].getPosition(0);
                    y = gameObjects[pos].getPosition(1);


                    array.Restart();
                    for (int k = 0; k < times; k++)
                    {
                        GameObject[] foundArray = searchArray(x, y);
                    }
                    array.Stop();

                    Array copy = Array.CreateInstance(typeof(GameObject), gameObjects.Length);
                    gameObjects.CopyTo(copy, 0);
                    nonExisitngTree.Reset();
                    exisitngTree.Reset();
                    for (int k = 0; k < times; k++)
                    {
                        gameObjects = copy.OfType<GameObject>().ToArray();
                        nonExisitngTree.Start();
                        Node root = QuickSort(0, gameObjects.Length - 1, 0);
                        for (int j = 0; j < GameObject.DIMENSION; j++)
                        {
                            root.lowerBound(j);
                            root.upperBound(j);
                        }

                        exisitngTree.Start();
                        GameObject[] foundTree = search(x, y, root);
                        nonExisitngTree.Stop();
                        exisitngTree.Stop();
                    }

                    Console.WriteLine("Clicked objects = " + w);
                    Console.WriteLine("Amount = " + values[i]);
                    Console.WriteLine("ExistingTree = " + exisitngTree.ElapsedMilliseconds);
                    Console.WriteLine("NonExistingTree = " + nonExisitngTree.ElapsedMilliseconds);
                    Console.WriteLine("array = " + array.ElapsedMilliseconds);
                    Console.WriteLine();
                }
            }
        }

        public GameObject[] searchArray(double x, double y)
        {
            List<GameObject> found = new List<GameObject>();
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i].getPosition(0) == x && gameObjects[i].getPosition(1) == y)
                {
                    found.Add(gameObjects[i]);
                }
            }
            return found.ToArray();
        }

        public GameObject[] search(double x, double y, Node node)
        {
            List<GameObject> found = new List<GameObject>();

            if (node.GetType() == typeof(SplitNode))
            {
                SplitNode splitNode = (SplitNode)node;

                if (splitNode.leftChild.lowerBound(0) <= x && splitNode.leftChild.upperBound(0) >= x && splitNode.leftChild.lowerBound(1) <= y && splitNode.leftChild.upperBound(1) >= y)
                {
                    found.AddRange(search(x, y, splitNode.leftChild));
                }
                if (splitNode.RightChild.lowerBound(0) <= x && splitNode.RightChild.upperBound(0) >= x && splitNode.RightChild.lowerBound(1) <= y && splitNode.RightChild.upperBound(1) >= y)
                {
                    found.AddRange(search(x, y, splitNode.RightChild));
                }
            }
            else if (node.GetType() == typeof(EndNode))
            {
                EndNode endNode = (EndNode)node;
                if (endNode.lowerBound(0) == x && endNode.lowerBound(1) == y)
                {
                    found.Add(endNode.Value);
                }
            }
            return found.ToArray();
        }

        private Node ArrayToBSBTree(GameObject[] array, int min, int max, Node parent = null)
        {
            int middle = (max + min) / 2;
            if (max - min == 0)
            {
                return new EndNode(parent, array[middle]);
            }
            else
            {
                SplitNode node = new SplitNode(parent);
                Node left = null;
                Node right = null;
                if (middle >= min)
                {
                    left = ArrayToBSBTree(array, min, middle, node);
                }
                if (middle < max)
                {
                    right = ArrayToBSBTree(array, middle + 1, max, node);
                }
                node.SetChilds(left, right);
                return node;
            }
        }
    }
}
