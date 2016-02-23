using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool
{
    public class FtTransmitterAccelData : FtTransmitterData
    {
        private List<String> _columns = new List<string>()
        {
            "key-bin-checksum",
            "tag-serial-number",
            "start-timestamp",
            "acceleration-sampling-frequency-per-axis",
            "acceleration-axes",
            "accelerations-raw"
        };

        public List<FtTransmitterAccelDataSeries> AccelerationSeries = new 
            List<FtTransmitterAccelDataSeries>(); 

        public FtTransmitterAccelData(string filePath)
        {
            TextReader accelDataReader = File.OpenText(filePath);
            String line;
            accelDataReader.ReadLine(); // skip first line with fileheader
            while ((line = accelDataReader.ReadLine()) != null)
            {
                AccelerationSeries.Add(new FtTransmitterAccelDataSeries(line));
            }
        }

    }
}
