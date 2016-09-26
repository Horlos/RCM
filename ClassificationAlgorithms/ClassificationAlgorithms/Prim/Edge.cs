using System;
using System.Collections.Generic;

namespace Graph
{
    public class Edge
    {
        private double _weight;
        private int _firstVertex;
        private int _secondVertex;

        public Edge(int firstVertex, int secondVertex, double weight)
        {
            if (firstVertex == secondVertex)
                throw new Exception("Error! FirstVertex and FinalVertex need not be equal");

            _firstVertex = firstVertex;
            _secondVertex = secondVertex;
            _weight = weight;
        }

        public List<int> GetNumberOfVertex()
        {
            List<int> listNumber = new List<int>();
            listNumber.Add(_firstVertex);
            listNumber.Add(_secondVertex);

            return listNumber;
        }

        public int FirstVertex
        {
            get { return _firstVertex; }
            set { _firstVertex = value; }
        }

        public int SecondVertex
        {
            get { return _secondVertex; }
            set { _secondVertex = value; }
        }

        public double Weight
        {
            get { return _weight; }
            set
            {
                if (_weight < 0)
                    throw new Exception("Error! Weight edges must be positive");

                _weight = value;
            }
        }
    }
}
