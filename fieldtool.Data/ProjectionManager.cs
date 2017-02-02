using DotSpatial.Projections;
using GeoAPI.Geometries;
using System;

namespace fieldtool.Data
{
    public class ProjectionManager
    {
        public static event EventHandler TargetProjectionChanged;

        public static ProjectionInfo SourceProjection { get; private set; }
        public static ProjectionInfo TargetProjection { get; private set; }

        public static void SetSourceProjection(int epsg)
        {
            SourceProjection = ProjectionInfo.FromEpsgCode(epsg);
        }

        public static void SetTargetProjection(int epsg)
        {
            TargetProjection = ProjectionInfo.FromEpsgCode(epsg);
            TargetProjectionChanged?.Invoke(null, new EventArgs());

        }

        public static Coordinate ReprojectCoordinate(Coordinate coord)
        {
            double[] xyArr = {coord.X, coord.Y};
            double[] zArr = {coord.Z};
            Reproject.ReprojectPoints(xyArr, zArr, SourceProjection, TargetProjection, 0, 1);

            return new Coordinate(xyArr[0], xyArr[1], zArr[0]);
        }

    }
}
