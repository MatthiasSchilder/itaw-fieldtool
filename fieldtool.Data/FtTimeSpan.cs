using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool.FrameworkExt
{
    public class FtTimeSpan
    {
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }

        public FtTimeSpan(DateTime startsAt, DateTime endsAt)
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
}
