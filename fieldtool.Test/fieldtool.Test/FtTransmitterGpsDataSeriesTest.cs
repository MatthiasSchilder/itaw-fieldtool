using System;
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

        private FtTransmitterGpsDataSeries SeriesMissing;
        private FtTransmitterGpsDataSeries SeriesVollst;

        [TestInitialize]
        public void Init()
        {
            SeriesMissing = new FtTransmitterGpsDataSeries(TestDataMissingValues);
            SeriesVollst = new FtTransmitterGpsDataSeries(TestDataVollst);
        }
        [TestMethod]
        public void TestStartTimeStamp()
        {
            DateTime expectedValueMissing = new DateTime(2015, 3, 15, 9, 50, 0);
            DateTime expectedValueVollst = new DateTime(2015, 3, 15, 13, 51, 59);
            
            Assert.AreEqual(expectedValueMissing, SeriesMissing.StartTimestamp);
            Assert.AreEqual(expectedValueVollst, SeriesVollst.StartTimestamp);
        }

        [TestMethod]
        public void TestLatLon()
        {
            double expectedLon = 7.8242214;
            double expectedLat = 52.4636663;

            Assert.AreEqual(expectedLat, SeriesVollst.Latitude);
            Assert.AreEqual(expectedLon, SeriesVollst.Longitude);
        }




    }
}
