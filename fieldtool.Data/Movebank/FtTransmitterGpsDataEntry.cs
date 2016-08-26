using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotSpatial.Projections;
using fieldtool.Data;
using GeoAPI.CoordinateSystems;
using GeoAPI.CoordinateSystems.Transformations;
using GeoAPI.Geometries;

namespace fieldtool
{
    public class FtTransmitterGpsDataEntry : FtTransmitterData
    {
        public DateTime StartTimestamp { get; private set; }
        public double? Rechtswert { get; private set; }
        public double? Hochwert { get; private set; }
        public double? Longitude { get; private set; }
        public double? Latitude { get; private set; }
        public double? HeightAboveEllipsoid { get; private set; }
        public short TypeOfFix { get; private set; }
        public string Status { get; private set; }
        public short UsedTimeToGetFix { get; private set; }
        public DateTime? TimestampOfFix { get; private set; }
        public short BatteryVoltage { get; private set; }
        public short BatteryVoltageFix { get; private set; }
        public short Temperature { get; private set; }
        public double? SpeedOverGround { get; private set; }
        public double? HeadingDegree { get; private set; }
        
        public FtTransmitterGpsDataEntry(string line)
        {
            string[] columns = SeperateColumns(line);
            ProcessColumns(columns);
        }


        public bool IsValid()
        {
            return Longitude.HasValue && Latitude.HasValue;
        }

        private string[] SeperateColumns(String line)
        {
            return line.Split(',');
        }

        private void ProcessColumns(string[] columns)
        {
            StartTimestamp = DateTime.Parse(columns[2]);

            double longitude;
            if(FtHelper.DoubleTryParse(columns[3], out longitude))
                Longitude = longitude;

            double latitude;
            if (FtHelper.DoubleTryParse(columns[4], out latitude))
                Latitude = latitude;

            if (Longitude.HasValue && Latitude.HasValue)
            {
                var reprojectedCoord =
                    ProjectionManager.ReprojectCoordinate(new Coordinate(Longitude.Value, 
                    Latitude.Value, 
                    HeightAboveEllipsoid ?? 0));
                Rechtswert = reprojectedCoord.X;
                Hochwert = reprojectedCoord.Y;
            }

            double heightAboveEllipsoid;
            if (FtHelper.DoubleTryParse(columns[5], out heightAboveEllipsoid))
                HeightAboveEllipsoid = heightAboveEllipsoid;

            TypeOfFix = short.Parse(columns[6]);
            Status = columns[7];
            UsedTimeToGetFix = short.Parse(columns[8]);

            DateTime timestampOfFix;
            if (DateTime.TryParse(columns[9], out timestampOfFix))
                TimestampOfFix = timestampOfFix;

            BatteryVoltage = short.Parse(columns[10]);
            BatteryVoltageFix = short.Parse(columns[11]);
            Temperature = short.Parse(columns[12]);

            double speedOverGround;
            if (FtHelper.DoubleTryParse(columns[13], out speedOverGround))
                SpeedOverGround = speedOverGround;

            double headingDegree;
            if (FtHelper.DoubleTryParse(columns[14], out headingDegree))
                HeadingDegree = headingDegree;
        }
    }
}
