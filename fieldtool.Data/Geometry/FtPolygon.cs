using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;

namespace fieldtool.Data
{
    public class FtPolygon
    {
        public List<Coordinate> Vertices;

        public FtPolygon(List<Coordinate> vertices)
        {
            Vertices = vertices;
        }
    }
}
