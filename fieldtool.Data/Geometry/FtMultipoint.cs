using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoAPI.Geometries;

namespace fieldtool.Data
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

        public FtPolygon MinimumConvexPolygon()
        {
            var resultPolygonVertices = quickHull(_points);
            return new FtPolygon(resultPolygonVertices);
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
