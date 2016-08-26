using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool.Data.Movebank
{
    public class FtTransmitterGPSDataSeries : List<FtTransmitterGpsDataEntry>
    {
        public FtTransmitterGpsDataEntry GetFirstGpsDataEntry()
        {
            return this.First();
        }
        public FtTransmitterGpsDataEntry GetLatestGpsDataEntry()
        {
            return this.Last();
        }
    }
}
