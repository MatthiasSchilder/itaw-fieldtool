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

        public static List<FtFileset> EnumerateFileSets(string directoryPath)
        {
            var files = Directory.EnumerateFiles(directoryPath);
            var dict = new Dictionary<int, FtFileset>();

            foreach (var fileFullpath in files)
            {
                var fileName = Path.GetFileName(fileFullpath);
                string id = StripID(fileName);
                var fileFunction = GetFunction(fileName);

                int numid = int.Parse(id);
                if (dict.ContainsKey(numid))
                {
                    var fs = dict[numid];
                    if (fs.IsFunctionAvailable(fileFunction))
                        Debug.Assert(false);
                    fs.AddFile(fileFunction, fileFullpath);
                }
                else
                {
                    var fileset = new FtFileset(numid);
                    if(fileset.IsFunctionAvailable(fileFunction))
                        Debug.Assert(false);
                    fileset.AddFile(fileFunction, fileFullpath);
                    dict.Add(numid, fileset);
                }
            }

            return dict.Values.ToList();
        }

        private static String StripID(String filename)
        {
            String result = "";
            foreach (var c in filename)
            {
                if (!Char.IsDigit(c))
                    continue;
                result += c.ToString();
            }
            return result;

        }

        private static FtFileFunction GetFunction(String name)
        {
            if(name.StartsWith("info"))
                return FtFileFunction.TagInfo;
            if(Path.GetFileNameWithoutExtension(name).EndsWith("acc"))
                return FtFileFunction.AccelData;
            return FtFileFunction.GPSData;
        }

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
