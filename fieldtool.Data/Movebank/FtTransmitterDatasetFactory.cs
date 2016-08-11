using System;
using System.Collections.Generic;
using System.Linq;
using SharpmapGDAL;
using fieldtool.Util;

namespace fieldtool.Data.Movebank
{
    public class FtTransmitterDatasetFactory
    {
        public static string SchemeFilenameTagInfo = "info_tag%%%%.txt";
        public static string SchemeFilenameAccelData = "tag%%%%_acc.txt";
        public static string SchemeFilenameGPSData = "tag%%%%_gps.txt";

        public static Action<int> SetupAction;
        private static void InvokeSetupAction(int num)
        {
            if (SetupAction == null)
                return;
            SetupAction(num);
        }

        public static Action<string> StepAction;
        private static void InvokeStepAction(string tagName)
        {
            if (StepAction == null)
                return;
            StepAction(tagName);
        }
        public static Action FinishAction;
        private static void InvokeFinishAction()
        {
            if (FinishAction == null)
                return;
            FinishAction();
        }

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
                    new FtTransmitterAccelData(fileset.TagId, fileset.GetFilepathForFunction(FtFileFunction.AccelData)));

            if (fileset.IsFunctionAvailable(FtFileFunction.GPSData))
                transmitterDataset.AddGPSData(
                    new FtTransmitterGpsData(fileset.TagId, fileset.GetFilepathForFunction(FtFileFunction.GPSData)));

            return transmitterDataset;
        }

        public static List<FtTransmitterDataset> LoadFilesets(List<FtFileset> filesets, List<int> tagBlacklist)
        {
            List<FtTransmitterDataset> transmitterDatasets = 
                new List<FtTransmitterDataset>(filesets.Count);

            InvokeSetupAction(filesets.Count);

            foreach (var fileset in filesets)
            {
                try
                {
                    InvokeStepAction(fileset.TagId.ToString());
                    var ftTransmitterDataset = LoadFileset(fileset, tagBlacklist);
                    if (ftTransmitterDataset != null)
                        transmitterDatasets.Add(ftTransmitterDataset);
                }
                catch (Exception ex)
                {


                }
            }
            InvokeFinishAction();

            return transmitterDatasets;
        }
    }
}
