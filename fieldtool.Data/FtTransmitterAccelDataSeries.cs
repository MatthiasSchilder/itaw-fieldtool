using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool
{
    public class FtTransmitterAccelDataSeries
    {
        // Zeilenformat:
        // key-bin-checksum,tag-serial-number,start-timestamp,acceleration-sampling-frequency-per-axis,acceleration-axes,accelerations-raw

        public DateTime StartTimestamp { get; private set; }
        public double   AccelerationSamplingFrequency { get; private set; }
        public String   AccelerationAxes { get; private set; }
        public List<int> AccelerationRawValues { get; private set; }

        public FtTransmitterAccelDataSeries(string line)
        {
            string[] columns = SeperateColumns(line);
            ProcessColumns(columns);
        }

        private string[] SeperateColumns(String line)
        {
            return line.Split(',');
        }

        private void ProcessColumns(string[] columns)
        {
            StartTimestamp = DateTime.Parse(columns[2]);
            AccelerationSamplingFrequency = FtHelper.DoubleParse(columns[3]);
            AccelerationAxes = columns[4];
            AccelerationRawValues = ProcessRawAccelValues(columns[5]);
        }

        private List<int> ProcessRawAccelValues(string rawAccelValues)
        {
            string[] rawValues = rawAccelValues.Split(' ');
            List<int> result = new List<int>();
            foreach (var rawValue in rawValues)
                if(!String.IsNullOrEmpty(rawValue))
                    result.Add(int.Parse(rawValue));

            return result;
        }

        public int[] GetXArr()
        {
            var rawValueCount = AccelerationRawValues.Count;
            List<int> result = new List<int>(rawValueCount);
            int idx = 0;
            while (idx < rawValueCount)
            {
                result.Add(AccelerationRawValues[idx]);
                idx += 3;
            }
            return result.ToArray();
        }
        public int[] GetYArr()
        {
            var rawValueCount = AccelerationRawValues.Count;
            List<int> result = new List<int>(rawValueCount);
            int idx = 1;
            while (idx < rawValueCount)
            {
                result.Add(AccelerationRawValues[idx]);
                idx += 3;
            }

            return result.ToArray();
        }
        public int[] GetZArr()
        {
            var rawValueCount = AccelerationRawValues.Count;
            List<int> result = new List<int>(rawValueCount);
            int idx = 2;
            while (idx < rawValueCount)
            {
                result.Add(AccelerationRawValues[idx]);
                idx += 3;
            }

            return result.ToArray();
        }

    }
}
