using System;

namespace Graph
{
    public class Vertex
    {
        private string exp = "The coordinates of the vertices have to be positive";
        private int _number;

        public Vertex(int number)
        {
            CheckVertexOfNumber(Number);
            _number = number;
        }

        public int Number
        {
            get { return _number; }
            set 
            {
                CheckVertexOfNumber(value);
                _number = value; 
            }
        }
     
        private void CheckVertexOfNumber(int numb)
        {
            if (Number < 0)
                throw new Exception("Vertex number must be greater than zero");
        }
    }
}
