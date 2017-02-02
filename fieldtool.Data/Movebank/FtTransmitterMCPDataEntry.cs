using System;
using System.Drawing;
using fieldtool.Data.Geometry;
using GeoAPI.Geometries;
using System.Linq;

namespace fieldtool.Data.Movebank
{
    public class FtTransmitterMCPDataEntry
    {
        public FtPolygon Polygon { get; }
        public bool Active { get; set; }
        public int PercentageMCP { get; set; }

        public FtTransmitterDataset Parent { get; }

        public Color Color { get; }

        public FtTransmitterMCPDataEntry(FtTransmitterDataset dataset, int percentageMCP, Color color)
        {
            Color = color;
            Active = true;
            PercentageMCP = percentageMCP;

            var validPositions =
                dataset.GPSData.Where(gps => gps.IsValid())
                    .Select(gps => new Coordinate(gps.Rechtswert.Value, gps.Hochwert.Value))
                    .ToList();

            FtMultipoint multipoint = new FtMultipoint(validPositions);
            Polygon = multipoint.MinimumConvexPolygon(percentageMCP);
            Polygon.Vertices.Add(Polygon.Vertices[0]);

            Parent = dataset;
        }  
    }
}
