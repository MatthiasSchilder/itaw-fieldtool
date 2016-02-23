using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fieldtool;

namespace SharpmapGDAL
{
    class FtTransmitterDatasetFactory
    {
        private const string SchemeFilenameTagInfo = "info_tag%%%%.txt";
        private const string SchemeFilenameAccelData = "tag%%%%_acc.txt";
        private const string SchemeFilenameGPSData = "tag%%%%_gps.txt";

        public FtTransmitterDatasetFactory()
        {
        }

        public FtTransmitterDataset LoadFileset(FtFileset fileset)
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

        public List<FtTransmitterDataset> LoadFilesets(List<FtFileset> filesets)
        {
            List<FtTransmitterDataset> transmitterDatasets = 
                new List<FtTransmitterDataset>(filesets.Count);

            foreach (var fileset in filesets)
                transmitterDatasets.Add(LoadFileset(fileset));

            return transmitterDatasets;
        }


    }
}
