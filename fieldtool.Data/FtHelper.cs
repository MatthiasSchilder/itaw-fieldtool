using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool
{
    class FtHelper
    {

        public static bool DoubleTryParse(string str, out double value)
        {
            return double.TryParse(str, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
        }

        public static double DoubleParse(string str)
        {
            return double.Parse(str, NumberStyles.Number, CultureInfo.InvariantCulture);
        }
    }
}
