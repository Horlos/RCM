using System;
using System.Collections.Generic;
using System.Linq;

namespace Forel
{
    public class ForelAlgorithm
    {
        private double _radius;

        public ForelAlgorithm()
        {
            _radius = 1.65;
        }

        public ForelAlgorithm(double radius)
        {
            _radius = radius;
        }

        public double Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        public IEnumerable<ClusteredPoint> Cluster(IList<Point> points)
        {
            var clusteredPoints = new List<ClusteredPoint>();
            while (points.Count > 0)
            {
                var clusteredPoint = new ClusteredPoint();
                var insidePoints = new List<Point>();

                var random = new Random();
                var index = random.Next(0, points.Count);
                Point center = points[index];
                Point newCenter = center;

                while (center == newCenter)
                {
                    insidePoints.AddRange(points.Where(point => IsInsideRadius(center, point)));

                    //power center
                    double powerX = 0;
                    double powerY = 0;
                    foreach (var p in insidePoints)
                    {
                        powerX += p.X;
                        powerY += p.Y;
                    }

                    var count = insidePoints.Count;
                    double powerCenterX = powerX / count;
                    double powerCenterY = powerY / count;
                    newCenter = new Point(powerCenterX, powerCenterY);
                }

                clusteredPoint.Points = insidePoints;
                clusteredPoint.Center = newCenter;
                clusteredPoints.Add(clusteredPoint);

                foreach (var p in insidePoints)
                {
                    points.Remove(p);
                }
            }

            return clusteredPoints;
        }

        private bool IsInsideRadius(Point center, Point point)
        {
            return Math.Sqrt(Math.Pow((point.X - center.X), 2) + Math.Pow((point.Y - center.Y), 2)) <
                   Radius;
        }
    }
}
