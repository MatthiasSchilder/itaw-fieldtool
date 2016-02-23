using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
