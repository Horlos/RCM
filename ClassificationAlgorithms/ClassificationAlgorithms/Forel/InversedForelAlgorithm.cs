using System;
using System.Collections.Generic;
using System.Linq;
using Forel;

namespace ClassificationAlgorithms.Forel
{
    public class InversedForelAlgorithm
    {
        private int _clustersCount;
        private double _radiusStep;

        public InversedForelAlgorithm()
        {
            _clustersCount = 3;
            _radiusStep = 0.5;
        }

        public InversedForelAlgorithm(int clustersCount, int radiusStep)
        {
            _clustersCount = clustersCount;
            _radiusStep = radiusStep;
        }

        public double RadiusStep
        {
            get { return _radiusStep; }
        }

        public int ClustersCount
        {
            get { return _clustersCount; }
        }

        public double Radius { get; set; }

        public IEnumerable<ClusteredPoint> Cluster(IList<Point> points)
        {
            var clusteredPoints = new List<ClusteredPoint>();
            var radius = FindMaxRadius(points);
            var prevRadius = radius;
            var radiusStep = RadiusStep;
            while (radius > 0)
            {
                var testPoints = points.ToList();
                var clusters = GetClusters(testPoints, radius).ToList();
                if (clusters.Count < ClustersCount)
                {
                    prevRadius = radius;
                    radius -= RadiusStep;
                }
                else if (clusters.Count > ClustersCount)
                {
                    radiusStep = radiusStep/10;
                    radius = prevRadius;
                }
                else
                {
                    clusteredPoints.AddRange(clusters);
                    Radius = radius;
                    break;
                }
            }

            return clusteredPoints;
        }

        private IEnumerable<ClusteredPoint> GetClusters(IList<Point> points, double radius)
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
                    insidePoints.AddRange(points.Where(point => IsInsideRadius(center, point, radius)));

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

        private bool IsInsideRadius(Point center, Point point, double radius)
        {
            return Math.Sqrt(Math.Pow((point.X - center.X), 2) + Math.Pow((point.Y - center.Y), 2)) <
                   radius;
        }

        private double FindMaxRadius(IList<Point> points)
        {
            var maxRadius = 0.0d;
            for (var i = 0; i < points.Count; i++)
            {
                for(var j = 0; j < points.Count; j++)
                {
                    if (i != j)
                    {
                        var radius =
                            Math.Sqrt(Math.Pow((points[i].X - points[j].X), 2) +
                                      Math.Pow((points[i].Y - points[j].Y), 2));
                        maxRadius = Math.Max(radius, maxRadius);
                    }
                }
            }
            return maxRadius;
        }

    }
}
