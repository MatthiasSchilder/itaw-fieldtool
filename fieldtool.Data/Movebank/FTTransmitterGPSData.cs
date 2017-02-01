using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using fieldtool.SharpmapExt;
using GeoAPI.Geometries;
using SharpMap.Data.Providers;

namespace fieldtool.Data.Movebank
{
    public class FtTransmitterGpsData : IEnumerable<FtTransmitterGpsDataEntry>
    {
        private int _tagID;
        public FtTransmitterGPSDataSeries GpsSeries = new
            FtTransmitterGPSDataSeries();

        public int InvalidCount { get; private set; }

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
                    var entry = new FtTransmitterGpsDataEntry(line);
                    GpsSeries.Add(entry);
                    if (!entry.IsValid())
                        InvalidCount++;
                }
                catch (Exception ex)
                {

                }
            }
        }

        public IEnumerator<FtTransmitterGpsDataEntry> GetEnumerator()
        {
            if (DateTimeFilterStop.HasValue && DateTimeFilterStart.HasValue)
            {
                Debug.WriteLine($"In GetEnumerator: FilterStart: {DateTimeFilterStart} FilterStop {DateTimeFilterStop}");
                return GpsSeries.Where(gps => gps.StartTimestamp >= DateTimeFilterStart && gps.StartTimestamp <= DateTimeFilterStop)
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
                if (!gpsPoint.IsValid())
                    continue;

                dataProvider.Rows.Add(i, _tagID , gesCount - i, gpsPoint.Rechtswert, gpsPoint.Hochwert, gpsPoint.StartTimestamp);
                i++;
            }

            return new DataTablePoint(dataProvider, "id", "x", "y");
        }

        public DataTableLine AsDataTableLine()
        {
            var dataProvider = new DataTable();

            dataProvider.Columns.Add("id", typeof(uint));
            dataProvider.Columns.Add("tagID", typeof(int));
            dataProvider.Columns.Add("num", typeof(int));
            dataProvider.Columns.Add("startx", typeof(double));
            dataProvider.Columns.Add("starty", typeof(double));
            dataProvider.Columns.Add("endx", typeof(double));
            dataProvider.Columns.Add("endy", typeof(double));

            var list = this.ToArray();

            for (int i = 1; i < list.Length; i++)
            {
                if (!list[i].IsValid() || !list[i - 1].IsValid())
                    continue;

                dataProvider.Rows.Add(i - 1, _tagID, list.Length - i, list[i - 1].Rechtswert, list[i - 1].Hochwert, list[i].Rechtswert, list[i].Hochwert);
            }

            return new DataTableLine(dataProvider, "id", "startx", "starty", "endx", "endy");
        }

        public Envelope GetEnvelope()
        {
            double xmin = double.MaxValue, xmax = double.MinValue;
            double ymin = double.MaxValue, ymax = double.MinValue;

            foreach (var gpsPoint in this)
            {
                if(!gpsPoint.IsValid())
                    continue;
               
                xmin = Math.Min(xmin, gpsPoint.Rechtswert.Value);
                xmax = Math.Max(xmax, gpsPoint.Rechtswert.Value);
                ymin = Math.Min(ymin, gpsPoint.Hochwert.Value);
                ymax = Math.Max(ymax, gpsPoint.Hochwert.Value);
            }
            return new Envelope(xmin, xmax, ymin, ymax);
        }

        public DateTime? DateTimeFilterStart;
        public DateTime? DateTimeFilterStop;
        public void SetIntervalFilter(DateTime start, DateTime stop)
        {
            DateTimeFilterStart = start;
            DateTimeFilterStop = stop;

            InvokeFilterChanged();
        }

        public event EventHandler FilterChanged;
        private void InvokeFilterChanged()
        {
            FilterChanged?.Invoke(this, new EventArgs());
        }
    }
}
