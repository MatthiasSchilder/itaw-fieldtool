using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace fieldtool
{
    public class FtProject
    {
        [XmlIgnore]
        public List<FtTransmitterDataset> Datasets { get; set; }
        
        public FtMapConfig MapConfig { get; set; }

        public String ProjectName { get; set; }
        public String ProjectFilePath { get;  set; }

        public FtProject()
        {
            
        }

        public FtProject(string filepath)
        {
            ProjectName = Path.GetFileNameWithoutExtension(filepath);
            ProjectFilePath = filepath;
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
    }
}
