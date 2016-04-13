using DotSpatial.Projections;
using GeoAPI.Geometries;

namespace fieldtool.Data
{
    public class ProjectionManager
    {
        public static ProjectionInfo SourceProjection;
        public static ProjectionInfo TargetProjection;

        public static void SetSourceProjection(int epsg)
        {
            SourceProjection = ProjectionInfo.FromEpsgCode(epsg);
        }

        public static void SetTargetProjection(int epsg)
        {
            TargetProjection = ProjectionInfo.FromEpsgCode(epsg);
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
