using System;
using System.Collections.Generic;

namespace Graph
{
    public class Graph
    {
        private List<Vertex> _listVertexes;
        private List<Edge> _listEdges;
        private double[,] _matrix;

        public Graph()
        {
            _listVertexes = new List<Vertex>();
            _listEdges = new List<Edge>();
        }

        public Graph(List<Vertex> vertexes, List<Edge> edges)
        {
            foreach (Vertex vertex in vertexes)
                CheckVertex(vertex);

            foreach (Edge edge in edges)
                CheckNumberOfVertex(edge);

            _listVertexes = new List<Vertex>();
            _listEdges = new List<Edge>();

            _listVertexes = vertexes;
            _listEdges = edges;
        }

        public List<Vertex> Vertexes
        {
            get { return _listVertexes; }
            set
            {
                foreach (Vertex ver in value)
                    try
                    {
                        CheckVertex(ver);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                _listVertexes = value;
            }
        }

        public List<Edge> Edges 
        { 
            get { return _listEdges; }
            set 
            {   
                foreach (Edge ed in value)
                    try
                    {
                        CheckNumberOfVertex(ed);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                _listEdges = value;
            }
        }

        public Vertex GetVertex(int index)
        {
            return _listVertexes[index];
        }

        public Edge GetEdge(int index)
        {
            return _listEdges[index];
        }

        public void AddVertex(Vertex item)
        {
            CheckVertex(item);

            _listVertexes.Add(item);
        }

        public void AddEdge(Edge item)
        {
            try
            {
                CheckNumberOfVertex(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            _listEdges.Add(item);            
        }

        public int[,] GetSingleMatrix()
        {
            int[,] matrix = new int[_listVertexes.Count, _listVertexes.Count];

            foreach (Edge ed in _listEdges)
                matrix[ed.FirstVertex, ed.SecondVertex] = matrix[ed.SecondVertex, ed.FirstVertex] = 1;

            return matrix;
        }

        private void CheckVertex(Vertex item)
        {
            foreach (Vertex ver in _listVertexes)
                if (item.Number == ver.Number)
                    throw new Exception("In the collection has a vertex with this number");
        }

        private void CheckNumberOfVertex(Edge item)
        {
            bool conformity = false;
            foreach (int number in item.GetNumberOfVertex())
            {
                conformity = false;
                foreach (Vertex ver in _listVertexes)
                    if (number == ver.Number)
                    {
                        conformity = true;
                        break;
                    }
            }

            if (conformity == false)
                throw new Exception("Not found vertex. Check the number of vertices of the edge.");
        }

        public void InsertListEdge(List<double[]> listEdge)
        {
            foreach (double[] ed in listEdge)
                _listEdges.Add(new Edge((int)ed[0], (int)ed[1], ed[2]));
        }

        public double[,] GetMatrix()
        {
            _matrix = new double[_listVertexes.Count, _listVertexes.Count];

            foreach (Edge ed in _listEdges)
                _matrix[ed.FirstVertex, ed.SecondVertex] = _matrix[ed.SecondVertex, ed.FirstVertex] = ed.Weight;

            return _matrix;
        }
    }
}
