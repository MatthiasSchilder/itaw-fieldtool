using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using fieldtool.Annotations;
using SharpmapGDAL;

namespace fieldtool
{
    public class FtProject : INotifyPropertyChanged
    {
        [XmlIgnore]
        public List<FtTransmitterDataset> Datasets { get; set; }
        
        public FtMapConfig MapConfig { get; set; }

        public String ProjectName { get; set; }
        public String ProjectFilePath { get;  set; }

        public bool   ExportToClipboard { get; set; }

        public FtProject()
        {
            
        }

        public FtProject(string filepath)
        {
            Datasets = new List<FtTransmitterDataset>();
            MapConfig = new FtMapConfig();

            ProjectName = Path.GetFileNameWithoutExtension(filepath);
            ProjectFilePath = filepath;
            ExportToClipboard = false;
        }

        public void LoadDatasets(string filepath)
        {
            var filesets = FtFileset.EnumerateFileSets(filepath);
            Datasets = FtTransmitterDatasetFactory.LoadFilesets(filesets);
        }

        public FtTransmitterDataset GetTransmitterDataset(int tagId)
        {
            if (Datasets == null)
                return null;
            return Datasets.Find(d => d.TagId == tagId);
        }

        public void Save()
        {
            FtProject.Serialize(this);
        }

        public static void Serialize(FtProject project)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(FtProject));
            using (TextWriter writer = new StreamWriter(project.ProjectFilePath))
            {
                serializer.Serialize(writer, project);
            }
        }

        public static FtProject Deserialize(string filepath)
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
}
