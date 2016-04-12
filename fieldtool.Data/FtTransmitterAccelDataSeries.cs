using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
       
        public int[] AccelerationRawValues { get; private set; }

        public bool IsValid { get; private set; }

        public FtTransmitterAccelDataSeries(string line)
        {
            string[] columns = line.Split(',');
            ProcessColumns(columns);
        }

        private void ProcessColumns(string[] columns)
        {
            if (columns.Count() != 6)
                return;
            StartTimestamp = DateTime.Parse(columns[2]);
            AccelerationSamplingFrequency = double.Parse(columns[3], NumStyle, CultInfo);  
            AccelerationAxes = columns[4];

            string[] rawValues = columns[5].Split(' ');
            AccelerationRawValues = new int[rawValues.Count()];

            int i = 0;
            foreach (var rawValue in rawValues)
                if (rawValue != null && rawValue != " ")
                    AccelerationRawValues[i++] = IntParseFast(rawValue);

            IsValid = true;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IntParseFast(string value)
        {
            int result = 0;
            for (int i = 0; i < value.Length; i++)
            {
                result = 10 * result + (value[i] - 48);
            }
            return result;
        }


        public int[] GetXArr()
        {
            var rawValueCount = AccelerationRawValues.Length;
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
            var rawValueCount = AccelerationRawValues.Length;
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
            var rawValueCount = AccelerationRawValues.Length;
            List<int> result = new List<int>(rawValueCount);
            int idx = 2;
            while (idx < rawValueCount)
            {
                result.Add(AccelerationRawValues[idx]);
                idx += 3;
            }

            return result.ToArray();
        }

        private static readonly NumberStyles NumStyle = NumberStyles.Number;
        private static readonly CultureInfo CultInfo = CultureInfo.InvariantCulture;
    }
}
