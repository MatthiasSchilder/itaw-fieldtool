using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fieldtool
{
    class ActivityTimeSpan
    {
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }

        public ActivityTimeSpan(DateTime startsAt, DateTime endsAt)
        {
            StartsAt = startsAt;
            EndsAt = endsAt;
        }

        public bool ContainsDate(DateTime dateTime)
        {
            if (dateTime >= StartsAt && dateTime < EndsAt)
                return true;
            return false;
        }
    }

    class AccelBurstActivityCalculator
    {
        private const int SplitDayInNumSegments = 240;
        private const int MinutesPerDay = 1440;
        private TimeSpan TimeSpanOneDay = new TimeSpan(1, 0, 0, 0);

        private FtTransmitterAccelData TagAccelData;
        private DateTime StartsAt;
        private DateTime EndsAt;

        private BitArray MapProcessedBursts;
        public AccelBurstActivityCalculator(FtTransmitterAccelData tagAccelData, DateTime start, DateTime stop)
        {
            TagAccelData = tagAccelData;
            StartsAt = start;
            EndsAt = stop;
            MapProcessedBursts = new BitArray(TagAccelData.AccelerationSeries.Count);
        }

        public Dictionary<DateTime, double[]> Process()
        {
            DateTime currentDay = StartsAt;
            Dictionary<DateTime, double[]> result = new Dictionary<DateTime, double[]>();
            while (currentDay < EndsAt)
            {
                List<ActivityTimeSpan> timeSlots = SplitDayInTimeslots(currentDay);

                double[] activityPerSlot = new double[SplitDayInNumSegments];
                int idx = 0;
                foreach (var timeslot in timeSlots)
                    activityPerSlot[idx++] = ProcessTimeslot(timeslot);

                result.Add(currentDay, activityPerSlot);

                currentDay += TimeSpanOneDay;
            }
            return result;
        }

        private double ProcessTimeslot(ActivityTimeSpan timeSlot)
        {
            var relevantBursts = GetRelevantAccelBursts(timeSlot);
            double value = double.MinValue;

            foreach (var burst in relevantBursts)
                value = Math.Max(value, CalculateBurstActivity(burst));

            return value;
        }

        private double CalculateBurstActivity(FtTransmitterAccelDataSeries accelBurst)
        {
            double sumX = SumElements(accelBurst.GetXArr());
            double sumY = SumElements(accelBurst.GetYArr());
            double sumZ = SumElements(accelBurst.GetZArr());

            int n = accelBurst.AccelerationRawValues.Count/3;
            return (sumX + sumY + sumZ)/((n - 1)*3);
        }

        private int SumElements(int[] arr)
        {
            int n = arr.Length;
            int result = 0;
            for (int i = 0; i < n - 2; i++)
                result += Math.Abs(arr[i + 1] - arr[i]);

            return result;
        }

        private List<FtTransmitterAccelDataSeries> GetRelevantAccelBursts(ActivityTimeSpan timeSlot)
        {
            List<FtTransmitterAccelDataSeries> result = new List<FtTransmitterAccelDataSeries>();
            for (int i = 0; i < TagAccelData.AccelerationSeries.Count; i++)
            {
                if (MapProcessedBursts[i])
                    continue;
                var burst = TagAccelData.AccelerationSeries[i];
                if (burst.StartTimestamp > timeSlot.EndsAt)
                    break; // da die Accel-Bursts chronologisch in AccelerationSeries vorliegen kann hier abgebrochen werden.

                if (timeSlot.ContainsDate(burst.StartTimestamp))
                {
                    result.Add(burst);
                    MapProcessedBursts[i] = true;
                }            
            }
            return result;
        }

        private List<ActivityTimeSpan> SplitDayInTimeslots(DateTime date)
        {
            int minutesPerSegment = MinutesPerDay/SplitDayInNumSegments;
            TimeSpan segmentTimeSpan = new TimeSpan(0, 0, minutesPerSegment, 0);

            List<ActivityTimeSpan> segments = new List<ActivityTimeSpan>(SplitDayInNumSegments);

            DateTime segmentStartsAt = date;
            for (int i = 0; i < SplitDayInNumSegments; i++)
            {
                var segmentEndsAt = segmentStartsAt + segmentTimeSpan;
                var timespan = new ActivityTimeSpan(segmentStartsAt, segmentEndsAt);
                segments.Add(timespan);

                segmentStartsAt = segmentEndsAt;
            }
            return segments;
        }
    }
}
