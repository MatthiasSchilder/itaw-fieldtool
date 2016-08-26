using System;
using System.Diagnostics;
using System.Linq;
using DotSpatial.Projections;
using fieldtool.Data;
using fieldtool.Data.Movebank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fieldtool.Test
{
    [TestClass]
    public class FtTransmitterGPSDataTest
    {
        private FtTransmitterGpsData _gpsData;

        [TestInitialize]
        public void Init()
        {
            ProjectionManager.SetSourceProjection(4326);
            ProjectionManager.SetTargetProjection(31467);

            _gpsData = new FtTransmitterGpsData(3914, "data/testdata/movebank/tag3914_gps.txt");
        }
       

        [TestMethod]
        public void TestDataPointCount()
        {
            Assert.AreEqual(_gpsData.GpsSeries.Count, 10156);
        }

        [TestMethod]
        public void TestDataFirstAndLastTimestampMatching()
        {
            Assert.AreEqual(_gpsData.GpsSeries.GetFirstGpsDataEntry().StartTimestamp, new DateTime(2015,3,20,13,49,51));
            Assert.AreEqual(_gpsData.GpsSeries.GetLatestGpsDataEntry().StartTimestamp, new DateTime(2015,11,4,18,30,0));
        }

        [TestMethod]
        public void TestDataFilterCorrectResultCount1()
        {
            const int expectedCount = 10;

            _gpsData.SetIntervalFilter(new DateTime(2015,3,20,13,49,50), new DateTime(2015,03,21,13,20,0));

            Assert.AreEqual(_gpsData.ToList().Count, expectedCount);

            var dtPoint = _gpsData.AsDataTablePoint();
            Assert.AreEqual(dtPoint.Table.Rows.Count, expectedCount);


        }

        // Start vor Ende
        [TestMethod]
        public void TestDataFilterCorrectResultCount2()
        {
            const int expectedCount = 0;

            _gpsData.SetIntervalFilter(new DateTime(2015, 3, 22, 13, 49, 50), new DateTime(2015, 03, 21, 13, 20, 0));

            Assert.AreEqual(_gpsData.ToList().Count, expectedCount);

            var dtPoint = _gpsData.AsDataTablePoint();
            Assert.AreEqual(dtPoint.Table.Rows.Count, expectedCount);
        }


        [TestMethod]
        public void TestDataFilterCorrectResultCount3()
        {
            const int expectedCount = 1;

            _gpsData.SetIntervalFilter(new DateTime(2015, 3, 21, 13, 20, 0), new DateTime(2015, 03, 21, 13, 20, 0));

            Assert.AreEqual(_gpsData.ToList().Count, expectedCount);

            var dtPoint = _gpsData.AsDataTablePoint();
            Assert.AreEqual(dtPoint.Table.Rows.Count, expectedCount);
        }

        // Start und Ende haben Datenpunkt
        [TestMethod]
        public void TestDataFilterCorrectResultCount4()
        {
            const int expectedCount = 6;

            _gpsData.SetIntervalFilter(new DateTime(2015, 3, 21, 13, 30, 0), new DateTime(2015, 03, 22, 6, 59, 58));

            Assert.AreEqual(_gpsData.ToList().Count, expectedCount);

            var dtPoint = _gpsData.AsDataTablePoint();
            Assert.AreEqual(dtPoint.Table.Rows.Count, expectedCount);
        }

        // Interval to small
        [TestMethod]
        public void TestDataFilterCorrectResultCount5()
        {
            const int expectedCount = 0;

            _gpsData.SetIntervalFilter(new DateTime(2015, 3, 21, 14, 10, 0), new DateTime(2015, 03, 21, 14, 20, 0));

            Assert.AreEqual(_gpsData.ToList().Count, expectedCount);

            var dtPoint = _gpsData.AsDataTablePoint();
            Assert.AreEqual(dtPoint.Table.Rows.Count, expectedCount);
        }

        // Interval to small
        [TestMethod]
        public void TestDataFilterCorrectResultCount6()
        {
            const int expectedCount = 1;

            _gpsData.SetIntervalFilter(new DateTime(2015, 11, 1, 15, 44, 0), new DateTime(2015, 11, 1, 15, 46, 0));

            Assert.AreEqual(_gpsData.ToList().Count, expectedCount);
            Assert.AreEqual(_gpsData.ToArray()[0].IsValid(), false);

            var dtPoint = _gpsData.AsDataTablePoint();
            Assert.AreEqual(dtPoint.Table.Rows.Count, 0);
        }

        // Interval to small
        [TestMethod]
        public void TestDataFilterCorrectResultCount7()
        {
            const int expectedCount = 1;

            _gpsData.SetIntervalFilter(new DateTime(2015, 3, 24, 8, 20, 0), new DateTime(2015, 3, 24, 8, 20, 0));

            Assert.AreEqual(_gpsData.ToList().Count, expectedCount);

            var dtPoint = _gpsData.AsDataTablePoint();
            Assert.AreEqual(dtPoint.Table.Rows.Count, expectedCount);
        }

        // Interval to small
        [TestMethod]
        public void TestDataFilterCorrectResultCount8()
        {
            const int expectedCount = 0;

            _gpsData.SetIntervalFilter(new DateTime(2015, 3, 24, 8, 20, 1), new DateTime(2015, 3, 24, 8, 20, 1));

            Assert.AreEqual(_gpsData.ToList().Count, expectedCount);

            var dtPoint = _gpsData.AsDataTablePoint();
            Assert.AreEqual(dtPoint.Table.Rows.Count, expectedCount);
        }

        [TestMethod]
        public void TestDataInvalidLinesCount()
        {
            const int expectedCount = 116;

            var invalidCount = _gpsData.GpsSeries.Count(dp => !dp.IsValid());

            Assert.AreEqual(invalidCount, expectedCount);
        }

        [TestMethod]
        public void TestDataInvalidLinesCount2()
        {
            const int expectedCount = 116;

            Assert.AreEqual(_gpsData.InvalidCount, expectedCount);
        }





        //[TestMethod]
        //public void TestPerf()
        //{
        //    var gpsDataSeries = new FtTransmitterGpsData(3548, @"C:\Users\Matthias\Downloads\schilder\schilder\software");

        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();
        //    var blub = gpsDataSeries.AsDataTablePoint();
        //    sw.Stop();
        //    Debug.WriteLine(blub.Table.Rows.Count);


        //    Assert.IsTrue(_entryVollstData.IsValid());
        //    Assert.IsFalse(_entryMissingData.IsValid());
        //}



    }
}
