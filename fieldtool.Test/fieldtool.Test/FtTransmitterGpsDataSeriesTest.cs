using System;
using System.Diagnostics;
using System.Linq;
using DotSpatial.Projections;
using fieldtool.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using fieldtool.Data.Movebank;

namespace fieldtool.Test
{
    [TestClass]
    public class FtTransmitterGPSDataSeriesTest
    {
        private const String TestDataMissingValues =
                "0764038825,2605,2015-03-15 09:50:00.000,,,,0,D,120,,3718,3375,11,,,,";
        private const String TestDataVollst =
            "4029566304,2605,2015-03-15 13:51:59.000,7.8242214,52.4636663,99.9,3,A,117,2015-03-15 13:53:56.000,3715,3403,13,2.61,351.27,6.56,25.60";

        private FtTransmitterGpsDataEntry _entryMissingData;
        private FtTransmitterGpsDataEntry _entryVollstData;

        [TestInitialize]
        public void Init()
        {
            ProjectionManager.SetSourceProjection(4326);
            ProjectionManager.SetTargetProjection(31467);

            _entryMissingData = new FtTransmitterGpsDataEntry(TestDataMissingValues);
            _entryVollstData = new FtTransmitterGpsDataEntry(TestDataVollst);
        }
        [TestMethod]
        public void TestStartTimeStamp()
        {
            DateTime expectedValueMissing = new DateTime(2015, 3, 15, 9, 50, 0);
            DateTime expectedValueVollst = new DateTime(2015, 3, 15, 13, 51, 59);
            
            Assert.AreEqual(expectedValueMissing, _entryMissingData.StartTimestamp);
            Assert.AreEqual(expectedValueVollst, _entryVollstData.StartTimestamp);
        }

        [TestMethod]
        public void TestLatLon()
        {
            double expectedLon = 7.8242214;
            double expectedLat = 52.4636663;

            Assert.AreEqual(expectedLat, _entryVollstData.Latitude);
            Assert.AreEqual(expectedLon, _entryVollstData.Longitude);
        }

        [TestMethod]
        public void TestTimestampOfFix()
        {
            DateTime timestampVollst = new DateTime(2015, 3, 15, 13, 53, 56);
            
            Assert.AreEqual(timestampVollst, _entryVollstData.TimestampOfFix);
            Assert.AreEqual(null, _entryMissingData.TimestampOfFix);
        }

        [TestMethod]
        public void TestBatteryVoltage()
        {
            short expectedBatteryVoltage = 3718;
            short expectedBatteryVoltageFix = 3375;

            Assert.AreEqual(expectedBatteryVoltage, _entryMissingData.BatteryVoltage);
            Assert.AreEqual(expectedBatteryVoltageFix, _entryMissingData.BatteryVoltageFix);
        }

        [TestMethod]
        public void TestUsedTimeToFix()
        {
            short expectedTimeToFixVollst = 117;
            short expectedTimeToFixMissing = 120;

            Assert.AreEqual(expectedTimeToFixVollst, _entryVollstData.UsedTimeToGetFix);
            Assert.AreEqual(expectedTimeToFixMissing, _entryMissingData.UsedTimeToGetFix);
        }

        [TestMethod]
        public void TestTemperature()
        {
            short expectedTemperature = 11;

            Assert.AreEqual(expectedTemperature, _entryMissingData.Temperature);
        }

        [TestMethod]
        public void TestHeightAboveEllipsoid()
        {
            double? expectedHeightAboveEllipsoidVollst = 99.9;
            double? expectedHeightAboveEllipsoidMissing = null;

            Assert.AreEqual(expectedHeightAboveEllipsoidVollst, _entryVollstData.HeightAboveEllipsoid);
            Assert.AreEqual(expectedHeightAboveEllipsoidMissing, _entryMissingData.HeightAboveEllipsoid);
        }

        [TestMethod]
        public void TestHeadingDegree()
        {
            double? expectedHeadingDegreeVollst = 351.27;
            double? expectedHeadingDegreeMissing = null;

            Assert.AreEqual(expectedHeadingDegreeVollst, _entryVollstData.HeadingDegree);
            Assert.AreEqual(expectedHeadingDegreeMissing, _entryMissingData.HeadingDegree);
        }

        [TestMethod]
        public void TestSpeedOverGround()
        {
            double? expectedSpeedOverGroundVollst = 2.61;
            double? expectedSpeedOverGroundMissing = null;

            Assert.AreEqual(expectedSpeedOverGroundVollst, _entryVollstData.SpeedOverGround);
            Assert.AreEqual(expectedSpeedOverGroundMissing, _entryMissingData.SpeedOverGround);
        }

        [TestMethod]
        public void TestIsValid()
        {
            Assert.IsTrue(_entryVollstData.IsValid());
            Assert.IsFalse(_entryMissingData.IsValid());
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
