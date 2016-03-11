using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fieldtool.Test
{
    [TestClass]
    public class FtTransmitterAccelDataSeriesTest
    {
        private const String TestData =
                "1418255167,3520,2015-05-05 15:40:00.000,10.54,XYZ,2019 2050 1525 2013 2053 1526 2018 2048 1530 2016 2056 1526 2016 2053 1529 2018 2057 1536 2020 2052 1532 2009 2064 1532 2014 2057 1527 2016 2048 1524 2024 2060 1541 2017 2054 1528 2018 2056 1528 2018 2053 1528 2020 2058 1536 2020 2052 1539 2022 2050 1536 2017 2058 1531 2010 2052 1529 2018 2056 1528 2011 2045 1536 2020 2055 1529 2014 2053 1525 2016 2054 1533 2011 2055 1528 2017 2048 1523";
        private FtTransmitterAccelDataSeries Series;

        [TestInitialize]
        public void Init()
        {
            Series = new FtTransmitterAccelDataSeries(TestData);
        }
        [TestMethod]
        public void TestRawValueCount()
        {
            int rawValueCount = Series.AccelerationRawValues.Count;
            Assert.AreEqual(rawValueCount, 78);
        }
        [TestMethod]
        public void TestRawValueComponentCount()
        {
            int xComponentCount = Series.GetXArr().Length;
            int yComponentCount = Series.GetYArr().Length;
            int zComponentCount = Series.GetZArr().Length;
            Assert.AreEqual(xComponentCount + yComponentCount + zComponentCount, 78);
        }

        [TestMethod]
        public void TestXComponents()
        {
            int[] xComponents = Series.GetXArr();

            int first = xComponents.First();
            int last = xComponents.Last();

            Assert.AreEqual(first, 2019);
            Assert.AreEqual(last, 2017);

        }

        [TestMethod]
        public void TestYComponents()
        {
            int[] yComponents = Series.GetYArr();

            int first = yComponents.First();
            int last = yComponents.Last();

            Assert.AreEqual(first, 2050);
            Assert.AreEqual(last, 2048);

        }

        [TestMethod]
        public void TestZComponents()
        {
            int[] zComponents = Series.GetZArr();

            int first = zComponents.First();
            int last = zComponents.Last();

            Assert.AreEqual(first, 1525);
            Assert.AreEqual(last, 1523);
        }

        [TestMethod]
        public void TestDateCast()
        {
            DateTime expectedDateTime = new DateTime(2015, 5, 5, 15, 40, 0);
            Assert.AreEqual(expectedDateTime, Series.StartTimestamp);
        }

        [TestMethod]
        public void TestSamplingFreq()
        {
            double expectedValue = 10.54;
            Assert.AreEqual(expectedValue, Series.AccelerationSamplingFrequency);
        }

        [TestMethod]
        public void TestSamplingAxes()
        {
            string expectedValue = "XYZ";
            Assert.AreEqual(expectedValue, Series.AccelerationAxes);
        }

    }
}
