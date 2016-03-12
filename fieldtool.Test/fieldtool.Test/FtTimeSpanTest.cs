using System;
using System.IO;
using System.Linq;
using System.Reflection;
using fieldtool.FrameworkExt;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fieldtool.Test
{
    [TestClass]
    public class FtTimeSpanTest
    {
        private DateTime StartsAt = new DateTime(2015, 5, 25, 0, 0, 0);
        private DateTime EndsAt = new DateTime(2015, 5, 25, 0, 6, 0);

        [TestMethod]
        public void FtTimeSpanBoundsTest()
        {
            var ftTimeSpan = new FtTimeSpan(StartsAt, EndsAt);

            Assert.IsTrue(ftTimeSpan.ContainsDate(StartsAt));
            Assert.IsFalse(ftTimeSpan.ContainsDate(EndsAt));
        }
    }
}
