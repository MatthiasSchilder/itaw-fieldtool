using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool.FrameworkExt
{
    public static class DatetimeExt
    {
        private static int MinutesPerDay = 1440;

        public static List<FtTimeSpan> SplitInTimeslots(this DateTime date, int numTimeslots)
        {
            int minutesPerSegment = MinutesPerDay / numTimeslots;
            TimeSpan segmentTimeSpan = new TimeSpan(0, 0, minutesPerSegment, 0);

            List<FtTimeSpan> segments = new List<FtTimeSpan>(numTimeslots);

            DateTime segmentStartsAt = date;
            for (int i = 0; i < numTimeslots; i++)
            {
                var segmentEndsAt = segmentStartsAt + segmentTimeSpan;
                var timespan = new FtTimeSpan(segmentStartsAt, segmentEndsAt);
                segments.Add(timespan);

                segmentStartsAt = segmentEndsAt;
            }
            return segments;
        }
    }
}
