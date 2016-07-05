using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool.Util
{
    public static class MathHelper
    {

        /// <summary>
        /// Gets the median from the list
        /// </summary>
        /// <typeparam name="T">The data type of the list</typeparam>
        /// <param name="Values">The list of values</param>
        /// <returns>The median value</returns>
        public static T Median<T>(System.Collections.Generic.List<T> Values)
        {
            if (Values.Count == 0)
                return default(T);
            Values.Sort();
            return Values[(Values.Count / 2)];
        }
    }
}
