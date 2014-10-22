using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSafeGraph
{
    class Graph
    {
        private Vertex Root;
        private int searching = 0;
        private Semaphore changeSemaphore = new Semaphore(1, 1);
        public Graph()
        {
            List<Vertex> graph = new List<Vertex>();
            Random r = new Random(1);
            char[] a = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            graph.Add(Root = new Vertex(a[graph.Count]));
            while (graph.Count < a.Length)
            {
                Vertex _old = graph[r.Next(graph.Count)];
                Vertex _new = new Vertex(a[graph.Count]);
                new Edge(_new, _old);
                graph.Add(_new);
            }
        }

        public void insert(Vertex vertex, Vertex[] next, Vertex[] previous)
        {
            changeSemaphore.WaitOne();
            awaitSearch();

            for (int i = 0; i < next.Length; i++)
            {
                new Edge(next[i], vertex);
            }

            for (int i = 0; i < previous.Length; i++)
            {
                new Edge(vertex, previous[i]);
            }

            changeSemaphore.Release();
        }

        public void insert(Edge edge, Vertex next, Vertex previous)
        {
            changeSemaphore.WaitOne();
            awaitSearch();

            edge.Next = next;
            edge.Previous = previous;

            changeSemaphore.Release();
        }

        public void delete(Vertex vertex)
        {
            changeSemaphore.WaitOne();
            awaitSearch();

            vertex.deleteReferences();
            vertex = null;

            changeSemaphore.Release();
        }

        public void delete(Edge edge)
        {
            changeSemaphore.WaitOne();
            awaitSearch();

            edge.deleteReferences();
            edge = null;

            changeSemaphore.Release();
        }

        public void change(Vertex vertex, char lable)
        {
            changeSemaphore.WaitOne();
            awaitSearch();

            vertex.label = lable;

            changeSemaphore.Release();
        }

        public void change(Edge edge, Vertex next, Vertex previous)
        {
            changeSemaphore.WaitOne();
            awaitSearch();

            edge.changeEdge(next, previous);

            changeSemaphore.Release();
        }

        public void depthFirstsearch()
        {
            changeSemaphore.WaitOne();
            searching++;
            changeSemaphore.Release();

            HashSet<Vertex> visited = new HashSet<Vertex>();
            Stack<Vertex> path = new Stack<Vertex>();

            path.Push(Root);
            visited.Add(Root);

            while (path.Count > 0)
            {
                bool newVertex = false;
                foreach (Vertex vertex in path.Peek().Vertexes)
                {
                    if (!visited.Contains(vertex))
                    {
                        newVertex = true;

                        visited.Add(vertex);
                        path.Push(vertex);
                    }
                }
                if (!newVertex)
                {
                    path.Pop();
                }
            }

            searching--;
        }


        private bool isSearching()
        {
            return searching != 0;
        }

        private void awaitSearch()
        {
            while (isSearching()) ;
        }
    }
}
