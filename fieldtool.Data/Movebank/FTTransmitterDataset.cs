using System;
using System.Drawing;
using fieldtool.SharpmapExt.Symbolizers;
using SharpmapGDAL;
using System.Collections.Generic;
using fieldtool.Data.Geometry;

namespace fieldtool.Data.Movebank
{
    public enum FtDatasetFeatureType
    {
        Puntual,
        Polygonal
    }

    public class FtTransmitterDataset
    {
        public int TagId { get; private set; }
        public FtFileset Fileset { get; private set; }

        public bool Active { get; set; }

        public FtTagVisulization Visulization;

        public FtTransmitterTagInfoData TagInfoData { get; private set; }
        public FtTransmitterAccelData   AccelData { get; private set; }
        public FtTransmitterGpsData     GPSData { get; private set; }

        public FtTransmitterMCPData MCPData { get; private set; }

        public FtTransmitterDataset(int id, FtFileset fileset)
        {
            TagId = id;
            Fileset = fileset;

            Random rnd = new Random();
            Visulization = new FtTagVisulization
            {
                Color = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)),
                
            };
            Visulization.Symbolizer = new FtDotPointSymbolizer(this.Visulization.Color);

            MCPData = new FtTransmitterMCPData();
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

        public void AddMCP(FtTransmitterMCPDataEntry mcpEntry)
        {
            MCPData.Add(mcpEntry);
        }
       

    }
}
