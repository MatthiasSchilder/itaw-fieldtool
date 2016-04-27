using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpmapGDAL;

namespace fieldtool
{
    public class FtTransmitterDataset
    {
        public int TagId { get; private set; }
        public FtFileset Fileset { get; private set; }

        public bool Active { get; set; }

        public Color VisulizationColor;

        public FtTransmitterTagInfoData TagInfoData { get; private set; }
        public FtTransmitterAccelData   AccelData { get; private set; }
        public FtTransmitterGpsData     GPSData { get; private set; }

        public FtTransmitterDataset(int id, FtFileset fileset)
        {
            TagId = id;
            Fileset = fileset;

            Random rnd = new Random();
            VisulizationColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
        }

        public void AddTagInfoData(FtTransmitterTagInfoData tagInfoData)
        {
            TagInfoData = tagInfoData;
        }

        public void AddAccelData(FtTransmitterAccelData accelData)
        {
            AccelData = accelData;
        }

        public void AddGPSData(FtTransmitterGpsData gpsData)
        {
            GPSData = gpsData;
        }

        public bool IsFunctionAvailable(FtFileFunction function)
        {
            if (function == FtFileFunction.TagInfo)
                return TagInfoData != null;
            if (function == FtFileFunction.AccelData)
                return AccelData != null;
            return GPSData != null;
        }
    }
}
