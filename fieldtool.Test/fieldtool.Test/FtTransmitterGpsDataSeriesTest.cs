using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fieldtool.Test
{
    [TestClass]
    public class FtTransmitterGPSDataSeriesTest
    {
        private const String TestDataMissingValues =
                "0764038825,2605,2015-03-15 09:50:00.000,,,,0,D,120,,3718,3375,11,,,,";
        private const String TestDataVollst =
            "4029566304,2605,2015-03-15 13:51:59.000,7.8242214,52.4636663,99.9,3,A,117,2015-03-15 13:53:56.000,3715,3403,13,2.61,351.27,6.56,25.60";

        private FtTransmitterGpsDataSeries SeriesMissingData;
        private FtTransmitterGpsDataSeries SeriesVollstData;

        [TestInitialize]
        public void Init()
        {
            SeriesMissingData = new FtTransmitterGpsDataSeries(TestDataMissingValues);
            SeriesVollstData = new FtTransmitterGpsDataSeries(TestDataVollst);
        }
        [TestMethod]
        public void TestStartTimeStamp()
        {
            DateTime expectedValueMissing = new DateTime(2015, 3, 15, 9, 50, 0);
            DateTime expectedValueVollst = new DateTime(2015, 3, 15, 13, 51, 59);
            
            Assert.AreEqual(expectedValueMissing, SeriesMissingData.StartTimestamp);
            Assert.AreEqual(expectedValueVollst, SeriesVollstData.StartTimestamp);
        }

        [TestMethod]
        public void TestLatLon()
        {
            double expectedLon = 7.8242214;
            double expectedLat = 52.4636663;

            Assert.AreEqual(expectedLat, SeriesVollstData.Latitude);
            Assert.AreEqual(expectedLon, SeriesVollstData.Longitude);
        }

        [TestMethod]
        public void TestTimestampOfFix()
        {
            DateTime timestampVollst = new DateTime(2015, 3, 15, 13, 53, 56);
            
            Assert.AreEqual(timestampVollst, SeriesVollstData.TimestampOfFix);
            Assert.AreEqual(null, SeriesMissingData.TimestampOfFix);
        }

        [TestMethod]
        public void TestBatteryVoltage()
        {
            short expectedBatteryVoltage = 3718;
            short expectedBatteryVoltageFix = 3375;

            Assert.AreEqual(expectedBatteryVoltage, SeriesMissingData.BatteryVoltage);
            Assert.AreEqual(expectedBatteryVoltageFix, SeriesMissingData.BatteryVoltageFix);
        }

        [TestMethod]
        public void TestUsedTimeToFix()
        {
            short expectedTimeToFixVollst = 117;
            short expectedTimeToFixMissing = 120;

            Assert.AreEqual(expectedTimeToFixVollst, SeriesVollstData.UsedTimeToGetFix);
            Assert.AreEqual(expectedTimeToFixMissing, SeriesMissingData.UsedTimeToGetFix);
        }

        [TestMethod]
        public void TestTemperature()
        {
            short expectedTemperature = 11;

            Assert.AreEqual(expectedTemperature, SeriesMissingData.Temperature);
        }

        [TestMethod]
        public void TestHeightAboveEllipsoid()
        {
            double? expectedHeightAboveEllipsoidVollst = 99.9;
            double? expectedHeightAboveEllipsoidMissing = null;

            Assert.AreEqual(expectedHeightAboveEllipsoidVollst, SeriesVollstData.HeightAboveEllipsoid);
            Assert.AreEqual(expectedHeightAboveEllipsoidMissing, SeriesMissingData.HeightAboveEllipsoid);
        }

        [TestMethod]
        public void TestHeadingDegree()
        {
            double? expectedHeadingDegreeVollst = 351.27;
            double? expectedHeadingDegreeMissing = null;

            Assert.AreEqual(expectedHeadingDegreeVollst, SeriesVollstData.HeadingDegree);
            Assert.AreEqual(expectedHeadingDegreeMissing, SeriesMissingData.HeadingDegree);
        }

        [TestMethod]
        public void TestSpeedOverGround()
        {
            double? expectedSpeedOverGroundVollst = 2.61;
            double? expectedSpeedOverGroundMissing = null;

            Assert.AreEqual(expectedSpeedOverGroundVollst, SeriesVollstData.SpeedOverGround);
            Assert.AreEqual(expectedSpeedOverGroundMissing, SeriesMissingData.SpeedOverGround);
        }

        [TestMethod]
        public void TestIsValid()
        {
            Assert.IsTrue(SeriesVollstData.IsValid());
            Assert.IsFalse(SeriesMissingData.IsValid());
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


        //    Assert.IsTrue(SeriesVollstData.IsValid());
        //    Assert.IsFalse(SeriesMissingData.IsValid());
        //}



    }
}
