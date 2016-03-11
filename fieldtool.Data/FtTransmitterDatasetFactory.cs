using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fieldtool;

namespace SharpmapGDAL
{
    public class FtTransmitterDatasetFactory
    {
        public static string SchemeFilenameTagInfo = "info_tag%%%%.txt";
        public static string SchemeFilenameAccelData = "tag%%%%_acc.txt";
        public static string SchemeFilenameGPSData = "tag%%%%_gps.txt";

        public static FtTransmitterDataset LoadFileset(FtFileset fileset)
        {
            FtTransmitterDataset transmitterDataset =
                new FtTransmitterDataset(fileset.Id);

            if (!fileset.IsFunctionAvailable(FtFileFunction.TagInfo))
                throw new Exception($"TagInfo not available for Tag {fileset.Id}. Skipping.");

            transmitterDataset.AddTagInfoData(
                new FtTransmitterTagInfoData(fileset.GetFilepathForFunction(FtFileFunction.TagInfo)));

            if(fileset.IsFunctionAvailable(FtFileFunction.AccelData))
                transmitterDataset.AddAccelData(
                    new FtTransmitterAccelData(fileset.GetFilepathForFunction(FtFileFunction.AccelData)));

            if (fileset.IsFunctionAvailable(FtFileFunction.GPSData))
                transmitterDataset.AddGPSData(
                    new FtTransmitterGpsData(fileset.GetFilepathForFunction(FtFileFunction.GPSData)));

            return transmitterDataset;
        }

        public static List<FtTransmitterDataset> LoadFilesets(List<FtFileset> filesets)
        {
            List<FtTransmitterDataset> transmitterDatasets = 
                new List<FtTransmitterDataset>(filesets.Count);

            foreach (var fileset in filesets)
                transmitterDatasets.Add(LoadFileset(fileset));

            return transmitterDatasets;
        }
    }
}
