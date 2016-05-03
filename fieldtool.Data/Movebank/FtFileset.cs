using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SharpmapGDAL
{
    public enum FtFileFunction
    {
        TagInfo,
        AccelData,
        GPSData
    }
    /// <summary>
    /// A FtFileset 
    /// </summary>
    public class FtFileset : Dictionary<FtFileFunction, String>
    {
        public int TagId { get; set; }

        public FtFileset(int id)
        {
            TagId = id;
        }

        public void AddFile(FtFileFunction function, String filePath)
        {
            this.Add(function, filePath);
        }

        public bool IsFunctionAvailable(FtFileFunction function)
        {
            bool functionAvabailable = this.ContainsKey(function);
            return functionAvabailable;
        }

        public String GetFilepathForFunction(FtFileFunction function)
        {
            if(!IsFunctionAvailable(function))
                throw new Exception("Function not available");

            return this[function];
        }

        public static List<FtFileset> FileSetFromMultiselect(List<String> filenames)
        {
            HashSet<int> tagIDs = new HashSet<int>();
            foreach(var filename in filenames)
            {
                int id;
                if (!GetTagIdFromFilename(filename, out id))
                    continue;
                tagIDs.Add(id);
            }

            var basePath = Path.GetDirectoryName(filenames[0]);

            var allFileSetsInDirectory = FileSetFromDirectory(basePath);

            List<FtFileset> resultingFileSets = new List<FtFileset>();
            foreach (var fileset in allFileSetsInDirectory)
            {
                if(tagIDs.Contains(fileset.TagId))
                    resultingFileSets.Add(fileset);
            }
            return resultingFileSets;
        }

        public static List<FtFileset> FileSetFromDirectory(string directoryPath)
        {
            var files = Directory.EnumerateFiles(directoryPath,"*.txt",SearchOption.TopDirectoryOnly);
            var dict = new Dictionary<int, FtFileset>();

            foreach (var fileFullpath in files)
            {
                var fileName = Path.GetFileName(fileFullpath);
                var fileFunction = GetFunction(fileName);

                if (!fileFunction.HasValue)
                    continue;

                int id;
                if (!GetTagIdFromFilename(fileName, out id))
                    continue;
                if (dict.ContainsKey(id))
                {
                    var fs = dict[id];
                    if (fs.IsFunctionAvailable(fileFunction.Value))
                        Debug.Assert(false);
                    fs.AddFile(fileFunction.Value, fileFullpath);
                }
                else
                {
                    var fileset = new FtFileset(id);
                    if (fileset.IsFunctionAvailable(fileFunction.Value))
                        Debug.Assert(false);
                    fileset.AddFile(fileFunction.Value, fileFullpath);
                    dict.Add(id, fileset);
                }
            }

            return dict.Values.ToList();
        }

        private static bool GetTagIdFromFilename(String filename, out int id)
        {
            var result =  filename.Where(c => Char.IsDigit(c)).Aggregate("", (current, c) => current + c.ToString());
            return int.TryParse(result, out id);
        }

        private static FtFileFunction? GetFunction(String name)
        {
            if (name.StartsWith("info"))
                return FtFileFunction.TagInfo;
            if (Path.GetFileNameWithoutExtension(name).EndsWith("acc"))
                return FtFileFunction.AccelData;
            if(Path.GetFileNameWithoutExtension(name).EndsWith("gps"))
                return FtFileFunction.GPSData;

            return null;
        }

    }
}
