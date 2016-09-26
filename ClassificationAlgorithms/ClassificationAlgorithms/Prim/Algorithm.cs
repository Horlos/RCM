using System;
using System.Collections;
using System.Collections.Generic;

namespace Graph
{
    public class Algorithm
    {
        public Algorithm()
        {

        }

        public bool ChekMatrix(double[,] Matrix)
        {
            int iTotal = 0;
            bool[] bVisited = new bool[(int) Math.Sqrt((Matrix.Length))];
            Queue q = new Queue();
            q.Enqueue(0);
            bVisited[0] = true;

            do
            {
                int i = (int)q.Dequeue();
                iTotal++;
                for (int j = 0; j < (int) Math.Sqrt((Matrix.Length)); j++)

                    if (Matrix[i, j] != 0 && !bVisited[j])
                    {
                        q.Enqueue(j);
                        bVisited[j] = true;
                    }

            }while (q.Count != 0);
            if (iTotal == (int) Math.Sqrt((Matrix.Length)))
                return true;
            else
                return false;
        }

        public bool CheckWeightdge(int[,] Matrix)
        {
            for (int i = 0; i < (int)Math.Sqrt((Matrix.Length)); i++ )
                for (int j = i; j < (int)Math.Sqrt((Matrix.Length)); j++ )
                    if (Matrix[i, j] < 0)
                        return false;

            return true;
        }
    }

    public class Prima
    {
        private double[,] _sourceMatrix;
        private List<double[]> _resultListEdge;
        private string _log;

        public string Log
        {
            get { return _log; }
        }

        public double[,] SourseMatrix
        {
            get { return _sourceMatrix; }
            set { _sourceMatrix = value; }
        }

        public List<double[]> ResultListEdge
        {
            get { return Calculate(); }
        }

        public Prima(double[,] matrix)
        {
            if (matrix.Length < 2)
                throw new Exception("It must be 2 or more!");
            if (new Algorithm().ChekMatrix(matrix) == false)
                throw new Exception("Weight of the edge should be greater than or equal to zero");
            if (new Algorithm().ChekMatrix(matrix) == false)
                throw new Exception("Not all vertices are connected!");

            _sourceMatrix = matrix;
        }

        private void RecordLog(int rows, int column)
        {
            _log += "Found minimal relative to the vertex edge " + rows + ".\r\n";
            _log += "This edge " + rows + " - " + column + "\r\n";
            _log += "Remove all the values of the rows  " + rows + " and " + column + " of the adjacency matrix.\r\n";
            _log += "-------------------------------------\r\n";
        }

        private List<double[]> Calculate()
        {
            double[,] workingMatrix = _sourceMatrix;
            _resultListEdge = new List<double[]>();
            List<int> listLabelVertex = new List<int>();
            listLabelVertex.Add(0);

            int countVertex = (int)Math.Sqrt(workingMatrix.Length);

            while (_resultListEdge.Count < countVertex - 1)
            {
                double minimum = double.MaxValue;
                int currentRow = 0,
                currentСolumn = 0;

                for (int i = 0; i < listLabelVertex.Count; i++)
                {
                    int j = listLabelVertex[i];
                    for (int k = 0; k < countVertex; k++)
                        if (workingMatrix[k, j] < minimum && workingMatrix[k, j] != 0)
                        {
                            minimum = workingMatrix[k, j];
                            currentСolumn = j;
                            currentRow = k;
                        }
                }

                for (int j = 0; j < countVertex; j++)
                {
                    workingMatrix[currentRow, j] = 0;
                    workingMatrix[currentСolumn, j] = 0;
                }

                RecordLog(currentRow, currentСolumn);

                listLabelVertex.Add(currentRow);
                _resultListEdge.Add(new[] { currentRow, currentСolumn, minimum });
            }
            return _resultListEdge;
        }
    }
}
