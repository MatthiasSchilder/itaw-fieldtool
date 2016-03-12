using System;
using System.IO;
using System.Linq;
using System.Reflection;
using fieldtool.FrameworkExt;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fieldtool.Test
{
    [TestClass]
    public class DateTimeExtTest
    {
        [TestMethod]
        public void TestSplitInTimeslots()
        {
            var today = new DateTime(2015, 5, 25);

            var timeslots = today.SplitInTimeslots(240);
            Assert.AreEqual(240, timeslots.Count);
        }
    }
}
