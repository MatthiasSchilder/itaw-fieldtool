using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool
{
    public class FtTransmitterGpsDataSeries : FtTransmitterData
    {
        public DateTime StartTimestamp { get; private set; }
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }
        public double HeightAboveEllipsoid { get; private set; }
        public short TypeOfFix { get; private set; }
        public string Status { get; private set; }
        public double UsedTimeToGetFix { get; private set; }
        public DateTime TimestampOfFix { get; private set; }
        public double BatteryVoltage { get; private set; }
        public double BatteryVoltageFix { get; private set; }
        public double Temperature { get; private set; }
        public double SpeedOverGround { get; private set; }
        public double HeadingDegree { get; private set; }
        
        public FtTransmitterGpsDataSeries(string line)
        {
            string[] columns = SeperateColumns(line);
            ProcessColumns(columns);
        }

        private string[] SeperateColumns(String line)
        {
            return line.Split(',');
        }

        private void ProcessColumns(string[] columns)
        {
            StartTimestamp = DateTime.Parse(columns[2]);
            Longitude = double.Parse(columns[3]);
            Latitude = double.Parse(columns[4]);
            HeightAboveEllipsoid = double.Parse(columns[5]);
            TypeOfFix = short.Parse(columns[6]);
            Status = columns[7];
            UsedTimeToGetFix = double.Parse(columns[8]);
            TimestampOfFix = DateTime.Parse(columns[9]);
            BatteryVoltage = double.Parse(columns[10]);
            BatteryVoltageFix = double.Parse(columns[11]);
            Temperature = double.Parse(columns[12]);
            SpeedOverGround = double.Parse(columns[13]);
            HeadingDegree = double.Parse(columns[14]);
        }
    }
}
