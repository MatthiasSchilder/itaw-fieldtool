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

        public static FtTransmitterDataset LoadFileset(FtFileset fileset, List<int> tagBlacklist)
        {
            if (tagBlacklist.Contains(fileset.TagId))
                return null;

            FtTransmitterDataset transmitterDataset =
                new FtTransmitterDataset(fileset.TagId, fileset);

            if (!fileset.IsFunctionAvailable(FtFileFunction.TagInfo))
                throw new Exception($"TagInfo not available for Tag {fileset.TagId}. Skipping.");

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

        public static List<FtTransmitterDataset> LoadFilesets(List<FtFileset> filesets, List<int> tagBlacklist)
        {
            List<FtTransmitterDataset> transmitterDatasets = 
                new List<FtTransmitterDataset>(filesets.Count);

            //foreach (var fileSet in filesets)
            //{
            //    var loadedFS = LoadFileset(fileSet, tagBlacklist);
            //    if(loadedFS != null)
            //        transmitterDatasets.Add(loadedFS);
            //}

            //Parallel.ForEach(filesets, fileset =>
            //{
            //    var loadedFS = LoadFileset(fileset, tagBlacklist);
            //    if (loadedFS != null)
            //        transmitterDatasets.Add(loadedFS);
            //});


            transmitterDatasets.AddRange(filesets.Select(
                fileset => LoadFileset(fileset, tagBlacklist)).Where(result => result != null));

            return transmitterDatasets;
        }
    }
}
