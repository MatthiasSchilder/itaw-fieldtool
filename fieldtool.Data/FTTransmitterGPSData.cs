using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool
{
    public class FtTransmitterGpsData : FtTransmitterData, IEnumerable<FtTransmitterGpsDataSeries>
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
                catch (Exception ex)
                {

                }

            }
        }

        public IEnumerator<FtTransmitterGpsDataSeries> GetEnumerator()
        {
            if (DateTimestop.HasValue && DateTimestart.HasValue)
            {
                return GpsSeries.Where(gps => gps.StartTimestamp >= DateTimestart && gps.StartTimestamp <= DateTimestop)
                    .GetEnumerator();
            }
            return GpsSeries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public DateTime? DateTimestart;
        public DateTime? DateTimestop;
        public void SetIntervalFilter(DateTime start, DateTime stop)
        {
            DateTimestart = start;
            DateTimestop = stop;
        }
    }
}
