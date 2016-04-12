using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotSpatial.Projections;
using GeoAPI.Geometries;

namespace fieldtool
{
    class ProectionManager
    {
        private static ProjectionInfo ProjectionInfoEPSG4314 = ProjectionInfo.FromEpsgCode(4314);
        private static ProjectionInfo ProjectionInfoEPSG31467 = ProjectionInfo.FromEpsgCode(31467);

        public static Coordinate ReprojectCoordinate(Coordinate coord)
        {
            double[] xyArr = new[] {coord.X, coord.Y};
            double[] zArr = new[] {coord.Z};
            Reproject.ReprojectPoints(xyArr, zArr, ProjectionInfoEPSG4314, ProjectionInfoEPSG31467, 0, 1);

            return new Coordinate(xyArr[0], xyArr[1], zArr[0]);
        }

    }
}
