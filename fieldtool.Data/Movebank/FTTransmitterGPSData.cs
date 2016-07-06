using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMap.Data.Providers;

namespace fieldtool
{
    public class FtTransmitterGpsData : FtTransmitterData, IEnumerable<FtTransmitterGpsDataSeries>
    {
        private int _tagID;
        public List<FtTransmitterGpsDataSeries> GpsSeries = new
            List<FtTransmitterGpsDataSeries>();

        public FtTransmitterGpsData(int tagID, string filePath)
        {
            _tagID = tagID;
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

        public DataTablePoint AsDataTablePoint()
        {
            var dataProvider = new DataTable();

            dataProvider.Columns.Add("id", typeof(int));
            dataProvider.Columns.Add("tagID", typeof (int));
            dataProvider.Columns.Add("num", typeof (int));
            dataProvider.Columns.Add("x", typeof (double));
            dataProvider.Columns.Add("y", typeof (double));
            dataProvider.Columns.Add("timestamp", typeof (DateTime));
            
            int i = 0;
            int gesCount = this.Count();
            foreach (var gpsPoint in this)
            {
                dataProvider.Rows.Add(i, _tagID , gesCount - i, gpsPoint.Rechtswert, gpsPoint.Hochwert, gpsPoint.StartTimestamp);
                i++;
            }

            return new DataTablePoint(dataProvider, "id", "x", "y");
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
