using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Forel
{
    public class FileManager
    {
        public static IEnumerable<Point> GetData()
        {
            var points = new List<Point>();
            var input = File.ReadAllText("inputFile.txt");

            var resultArray = input.Split('\n')
                .Select(row => row.Trim()
                    .Split(',')
                    .Select(ParseToDouble)
                    .ToList())
                .ToList();

            foreach (var row in resultArray)
            {
                for (var l = 0; l < row.Count - 1; l++)
                    points.Add(new Point(row[l], row[l + 1]));
            }

            return points;
        }

        private static double ParseToDouble(string value)
        {
            double result;
            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return result;

            return 0;
        }
    }
}