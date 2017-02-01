using System.Collections.Generic;
using GeoAPI.Geometries;

namespace fieldtool.Data.Geometry
{
    public class FtPolygon
    {
        public List<Coordinate> Vertices { get; set; }

        public FtPolygon()
        {
            
        }

        public FtPolygon(List<Coordinate> vertices)
        {
            Vertices = vertices;
        }
    }
}
