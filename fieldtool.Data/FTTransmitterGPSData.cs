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
            if (dateTimestop.HasValue && dateTimestart.HasValue)
            {
                return GpsSeries.Where(gps => gps.StartTimestamp >= dateTimestart && gps.StartTimestamp <= dateTimestop)
                    .GetEnumerator();
            }
            return GpsSeries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        private DateTime? dateTimestart;
        private DateTime? dateTimestop;
        public void SetIntervalFilter(DateTime start, DateTime stop)
        {
            dateTimestart = start;
            dateTimestop = stop;
        }
    }
}
