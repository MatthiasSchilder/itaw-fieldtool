using System;
using fieldtool.Data.Geometry;
using GeoAPI.Geometries;
using System.Linq;

namespace fieldtool.Data.Movebank
{
    public class FtTransmitterMCPDataEntry
    {
        public FtPolygon Polygon { get; }
        public bool Enabled { get; set; }
        public int PercentageMCP { get; set; }

        public FtTransmitterMCPDataEntry(FtTransmitterDataset dataset, int percentageMCP)
        {
            Enabled = true;
            PercentageMCP = percentageMCP;

            var validPositions =
                dataset.GPSData.Where(gps => gps.IsValid())
                    .Select(gps => new Coordinate(gps.Rechtswert.Value, gps.Hochwert.Value))
                    .ToList();

            FtMultipoint multipoint = new FtMultipoint(validPositions);
            Polygon = multipoint.MinimumConvexPolygon(percentageMCP);
            Polygon.Vertices.Add(Polygon.Vertices[0]);
        }  
    }
}
