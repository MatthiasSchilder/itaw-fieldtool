using System.Collections;
using System.Collections.Generic;
using GeoAPI.Geometries;
using System;
using System.Linq;
using System.Diagnostics;

namespace fieldtool.Data.Geometry
{
    public class FtMultipoint : IEnumerable<Coordinate>
    {
        private List<Coordinate> _points;
        public FtMultipoint(List<Coordinate> points)
        {
            _points = points;
        }

        public IEnumerator<Coordinate> GetEnumerator()
        {
            return ((IEnumerable<Coordinate>) _points).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public FtPolygon MinimumConvexPolygon(int mcpPerc)
        {
            _points = RemovePoints(_points, mcpPerc);

            var resultPolygonVertices = quickHull(_points);
            return new FtPolygon(resultPolygonVertices);
        }

        private List<Coordinate> RemovePoints(List<Coordinate> points, int mcpPerc)
        {
            if (mcpPerc == 100)
                return points;

            var pointCnt = points.Count;
            var pointCntToKeep = Math.Ceiling(pointCnt * mcpPerc / 100d);

            var centroid = GetCentroid(points);

            var pointsAsArray = points.ToArray();
            Dictionary<int, double> distancesDict = new Dictionary<int, double>();

            for(int i = 0; i < pointsAsArray.Length; i++)
            {
                distancesDict.Add(i, centroid.Distance(pointsAsArray[i]));
            }
            List<Coordinate> result = new List<Coordinate>();
            int j = 0;
            foreach (KeyValuePair<int, double> item in distancesDict.OrderBy(key => key.Value))
            {
                if (j == pointCntToKeep)
                    break;
                result.Add(pointsAsArray[j++]);
            }
            return result;

        }

        private Coordinate GetCentroid(List<Coordinate> points)
        {
            var resultPolygonVertices = quickHull(_points);

            //resultPolygonVertices.Reverse();

            double sum = 0;
            for(int i = 0; i < resultPolygonVertices.Count; i++)
            {
                sum += _points[i].X * _points[(i + 1) % resultPolygonVertices.Count].Y - _points[(i + 1) % resultPolygonVertices.Count].X * _points[i].Y;
            }

            double A = 0.5 * sum;

            double sumXs = 0;
            for (int i = 0; i < resultPolygonVertices.Count - 1; i++)
            {
                sumXs += (_points[i].X + _points[i + 1].X) * (_points[i].X * _points[i + 1].Y - _points[i + 1].X * _points[i].Y);
            }

            double xs = (1 / (6 * A)) * sumXs;

            double sumYs = 0;
            for (int i = 0; i < resultPolygonVertices.Count - 1; i++)
            {
                sumYs += (_points[i].Y + _points[i + 1].Y) * (_points[i].X * _points[i + 1].Y - _points[i + 1].X * _points[i].Y);
            }

            double ys = (1 / (6 * A)) * sumYs;
            Debug.WriteLine(String.Format("x {0} y {1}", xs, ys));
            return new Coordinate(xs, ys);
        }

        private List<Coordinate> quickHull(List<Coordinate> points)
        {
            List<Coordinate> conveXHull = new List<Coordinate>();
            if (points.Count < 3) return points;

            int minPoint = -1, maXPoint = -1;
            double minX = double.MaxValue;
            double maXX = double.MinValue;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X < minX)
                {
                    minX = points[i].X;
                    minPoint = i;
                }
                if (points[i].X > maXX)
                {
                    maXX = points[i].X;
                    maXPoint = i;
                }
            }
            Coordinate A = points[minPoint];
            Coordinate B = points[maXPoint];

            conveXHull.Add(A);
            conveXHull.Add(B);
            points.Remove(A);
            points.Remove(B);

            List<Coordinate> leftSet = new List<Coordinate>();
            List<Coordinate> rightSet = new List<Coordinate>();

            for (int i = 0; i < points.Count; i++)
            {
                Coordinate p = points[i];
                if (pointLocation(A, B, p) == -1)
                    leftSet.Add(p);
                else
                    rightSet.Add(p);
            }

            hullSet(A, B, rightSet, conveXHull);
            hullSet(B, A, leftSet, conveXHull);

            return conveXHull;
        }

        /*
         * Computes the square of the GetDistance of point C to the segment defined bY points AB
         */
        private double GetDistance(Coordinate A, Coordinate B, Coordinate C)
        {
            double ABX = B.X - A.X;
            double ABY = B.Y - A.Y;
            double num = ABX * (A.Y - C.Y) - ABY * (A.X - C.X);
            if (num < 0) num = -num;
            return num;
        }

        private void hullSet(Coordinate A, Coordinate B, List<Coordinate> set, List<Coordinate> hull)
        {
            int insertPosition = hull.IndexOf(B);
            if (set.Count == 0) return;
            if (set.Count == 1)
            {
                Coordinate p = set[0];
                set.Remove(p);
                hull.Insert(insertPosition, p);
                return;
            }
            double dist = double.MinValue;
            int furthestPoint = -1;
            for (int i = 0; i < set.Count; i++)
            {
                Coordinate p = set[i];
                double distance = GetDistance(A, B, p);
                if (distance > dist)
                {
                    dist = distance;
                    furthestPoint = i;
                }
            }
            Coordinate P = set[furthestPoint];
            set.RemoveAt(furthestPoint);
            hull.Insert(insertPosition, P);

            // Determine who's to the left of AP
            List<Coordinate> leftSetAP = new List<Coordinate>();
            for (int i = 0; i < set.Count; i++)
            {
                Coordinate M = set[i];
                if (pointLocation(A, P, M) == 1)
                {
                    leftSetAP.Add(M);
                }
            }

            // Determine who's to the left of PB
            List<Coordinate> leftSetPB = new List<Coordinate>();
            for (int i = 0; i < set.Count; i++)
            {
                Coordinate M = set[i];
                if (pointLocation(P, B, M) == 1)
                {
                    leftSetPB.Add(M);
                }
            }
            hullSet(A, P, leftSetAP, hull);
            hullSet(P, B, leftSetPB, hull);

        }

        private double pointLocation(Coordinate A, Coordinate B, Coordinate P)
        {
            double cp1 = (B.X - A.X) * (P.Y - A.Y) - (B.Y - A.Y) * (P.X - A.X);
            return (cp1 > 0) ? 1 : -1;
        }
    }
}
