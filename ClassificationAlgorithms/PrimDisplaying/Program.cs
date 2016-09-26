using System;
using System.Globalization;
using System.IO;
using Graph;

public static class Program
{
    public static void Main()
    {
        var sourseGraph = new Graph.Graph();
        var resultGraph = new Graph.Graph();
        var graphFile = "myGraph.txt";
        StreamReader reader = new StreamReader(graphFile);

        char[] splits = { ' ', ',', '\t' };
        string line = reader.ReadLine();
        string[] parts = line.Split(splits, StringSplitOptions.RemoveEmptyEntries);

        // find out number of vertices and edges
        var vertixes = int.Parse(parts[0]);
        var edges = vertixes * (vertixes - 1) / 2;

        for (int i = 0; i < vertixes; i++)
        {
            var vertex = new Vertex(i);
            sourseGraph.AddVertex(vertex);
        }

        for (int u = 0; u < vertixes; u++)
        {
            line = reader.ReadLine();
            parts = line.Split(splits, StringSplitOptions.RemoveEmptyEntries);
            for (int v = 0; v < parts.Length; v++)
            {
                if (v != u)
                {
                    var weight = double.Parse(parts[v], NumberStyles.Any, CultureInfo.InvariantCulture);
                    var edge = new Edge(u, v, weight);
                    sourseGraph.AddEdge(edge);

                }
            }
        }

        var prima = new Prima(sourseGraph.GetMatrix());
        resultGraph.InsertListEdge(prima.ResultListEdge);
        resultGraph.Vertexes = sourseGraph.Vertexes;
        //Console.WriteLine(prima.Log);
        foreach (var edge in resultGraph.Edges)
        {
            Console.WriteLine(string.Format("{0} - {1} - {2}", edge.FirstVertex, edge.Weight, edge.SecondVertex));
        }

        Console.ReadKey();
    }
}