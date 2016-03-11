using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool
{
    public class FtTransmitterGpsData : FtTransmitterData
    {
        public List<FtTransmitterGpsDataSeries> GpsSeries = new
            List<FtTransmitterGpsDataSeries>();

        public FtTransmitterGpsData(string filePath)
        {
            TextReader accelDataReader = File.OpenText(filePath);
            String line;
            accelDataReader.ReadLine(); // skip first line with fileheader
            while ((line = accelDataReader.ReadLine()) != null)
            {
                try
                {
                    GpsSeries.Add(new FtTransmitterGpsDataSeries(line));
                }
                catch (Exception)
                {

                }

            }
        }
    }
}
