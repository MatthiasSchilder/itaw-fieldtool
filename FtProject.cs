using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Xml.Serialization;
using fieldtool.Annotations;
using fieldtool.Data.Movebank;
using SharpmapGDAL;

namespace fieldtool
{
    public class FtProject : INotifyPropertyChanged
    {
        [XmlIgnore]
        public EventHandler<DataChangedEventArgs> DataChangedEventHandler;

        [XmlIgnore]
        public List<FtTransmitterDataset> Datasets { get; set; }

        [XmlIgnore]
        public List<FtFileset> MovebankFilesets;

        public FtMapConfig MapConfig { get; set; }

        public String ProjectName { get; set; }
        public String ProjectFilePath { get;  set; }
        public String DefaultMovebankLookupPath { get; set; }

        [XmlIgnore]
        public bool DefaultMovebankLookupPathAvailable => !String.IsNullOrEmpty(DefaultMovebankLookupPath);

        public List<int> TagBlacklist { get; set; }

        public int EPSGSourceProjection { get; set; }
        public int EPSGTargetProjection { get; set; }

        public FtProject()
        {
            
        }

        public FtProject(string filepath)
        {
            Datasets = new List<FtTransmitterDataset>();
            TagBlacklist = new List<int>();
            MapConfig = new FtMapConfig();

            ProjectName = Path.GetFileNameWithoutExtension(filepath);
            ProjectFilePath = filepath;

            EPSGSourceProjection = 4326;
            EPSGTargetProjection = 31467;
        }

        public void SetDatasetFeatureState(int tagId, bool checkState)
        {
            if (!Datasets.Any())
                return;

            var dataset = Datasets.Find(d => d.TagId == tagId);
            dataset.Active = checkState;
            DataChangedEventHandler(this, new DataChangedEventArgs(dataset));
        }

        public void SetIntervalFilter(FtTransmitterDataset dataset, DateTime start, DateTime stop)
        {
            dataset.GPSData.SetIntervalFilter(start, stop);
            DataChangedEventHandler(this, new DataChangedEventArgs(dataset));
        }

        public void LoadDatasets(Action<int> setupAction, Action<string> stepAction, Action finishAction)
        {
            FtTransmitterDatasetFactory.SetupAction = setupAction;
            FtTransmitterDatasetFactory.StepAction = stepAction;
            FtTransmitterDatasetFactory.FinishAction = finishAction;

            Datasets = FtTransmitterDatasetFactory.LoadFilesets(MovebankFilesets, TagBlacklist);
        }

        public FtTransmitterDataset GetTransmitterDataset(int tagId)
        {
            return Datasets?.Find(d => d.TagId == tagId);
        }

        public void Save()
        {
            FtProject.Serialize(this);
        }

        public static FtProject Open(string filepath)
        {
            var projekt = Deserialize(filepath);
            return projekt;
        }

        private static void Serialize(FtProject project)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(FtProject));
            using (TextWriter writer = new StreamWriter(project.ProjectFilePath))
            {
                serializer.Serialize(writer, project);
            }
        }

        private static FtProject Deserialize(string filepath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(FtProject));
            TextReader reader = new StreamReader(filepath);
            object obj = deserializer.Deserialize(reader);
            FtProject project = (FtProject) obj;
            reader.Close();

            return project;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DataChangedEventArgs
    {
        public FtTransmitterDataset Dataset { get; }
        public DataChangedEventArgs(FtTransmitterDataset dataset)
        {
            Dataset = dataset;
        }
    }
}
