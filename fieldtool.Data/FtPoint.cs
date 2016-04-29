using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool.Data
{


    public class FtPoint
    {
        public FtPoint(double rechtswert, double hochwert)
        {
            Rechtswert = rechtswert;
            Hochwert = hochwert;
        }

        public double Rechtswert { get; set; }
        public double Hochwert { get; set; }
    }
}
