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
        //private List<String> _columns = new List<string>()
        //{
        //    "key-bin-checksum",
        //    "tag-serial-number",
        //    "start-timestamp",
        //    "acceleration-sampling-frequency-per-axis",
        //    "acceleration-axes",
        //    "accelerations-raw"
        //};

        public List<FtTransmitterAccelDataSeries> AccelerationSeries = new 
            List<FtTransmitterAccelDataSeries>();

        public Dictionary<DateTime, double[]> CalculatedActivities;
        private int _tagID;

        public FtTransmitterAccelData(int tagID, string filePath)
        {
            _tagID = tagID;
            TextReader accelDataReader = File.OpenText(filePath);
            String line;
            accelDataReader.ReadLine(); // skip first line with fileheader
            while ((line = accelDataReader.ReadLine()) != null)
            {
                FtTransmitterAccelDataSeries series = new FtTransmitterAccelDataSeries(line);
                if(series.IsValid)
                    AccelerationSeries.Add(series);               
            }

            AccelBurstActivityCalculator accCalculator = new AccelBurstActivityCalculator(this, 
                                                        GetFirstBurstDate(),
                                                        GetLastBurstDate());
            CalculatedActivities = accCalculator.Process();
        }

        public DateTime GetFirstBurstTimestamp()
        {
            return AccelerationSeries.Min(burst => burst.StartTimestamp);
        }

        public DateTime GetLastBurstTimestamp()
        {
            return AccelerationSeries.Max(burst => burst.StartTimestamp);
        }

        public DateTime GetFirstBurstDate()
        {
            var res = GetFirstBurstTimestamp();
            return new DateTime(res.Year, res.Month, res.Day, 0, 0, 0);
        }

        public DateTime GetLastBurstDate()
        {
            var res = GetLastBurstTimestamp();
            return new DateTime(res.Year, res.Month, res.Day, 23, 59, 59);
        }

    }
}
