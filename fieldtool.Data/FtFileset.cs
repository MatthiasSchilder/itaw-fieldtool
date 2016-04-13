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
        public int Id { get; set; }

        public FtFileset(int id)
        {
            Id = id;
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
            HashSet<string> tagIDs = new HashSet<string>();
            foreach (string id in filenames.Select(GetTagId))
            {
                tagIDs.Add(id);
            }
            var basePath = Path.GetDirectoryName(filenames[0]);

            var allFileSetsInDirectory = FileSetFromDirectory(basePath);

            List<FtFileset> resultingFileSets = new List<FtFileset>();
            foreach (var fileset in allFileSetsInDirectory)
            {
                if(tagIDs.Contains(fileset.Id.ToString()))
                    resultingFileSets.Add(fileset);
            }
            return resultingFileSets;
        }

        public static List<FtFileset> FileSetFromDirectory(string directoryPath)
        {
            var files = Directory.EnumerateFiles(directoryPath);
            var dict = new Dictionary<int, FtFileset>();

            foreach (var fileFullpath in files)
            {
                var fileName = Path.GetFileName(fileFullpath);
                string id = GetTagId(fileName);
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
                    if (fileset.IsFunctionAvailable(fileFunction))
                        Debug.Assert(false);
                    fileset.AddFile(fileFunction, fileFullpath);
                    dict.Add(numid, fileset);
                }
            }

            return dict.Values.ToList();
        }

        private static String GetTagId(String filename)
        {
            return filename.Where(c => Char.IsDigit(c)).Aggregate("", (current, c) => current + c.ToString());
        }

        private static FtFileFunction GetFunction(String name)
        {
            if (name.StartsWith("info"))
                return FtFileFunction.TagInfo;
            if (Path.GetFileNameWithoutExtension(name).EndsWith("acc"))
                return FtFileFunction.AccelData;
            return FtFileFunction.GPSData;
        }

    }
}
