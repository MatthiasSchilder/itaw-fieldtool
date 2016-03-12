using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fieldtool.Test
{
    [TestClass]
    public class FtTransmitterAccelDataTest
    {
        private string PathAccelTestData =
            @"..\..\..\..\data\testdata\movebank\tag1704_acc.txt";
        private FtTransmitterAccelData AccelData;

        [TestInitialize]
        public void Init()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                PathAccelTestData);
            AccelData = new FtTransmitterAccelData(path);
            
        }
        [TestMethod]
        public void TestFirstLastTimeStamp()
        {
            DateTime expectedValueStart = new DateTime(2015, 3, 14, 18, 10, 0);
            DateTime expectedValueEnd= new DateTime(2016, 2, 3, 13, 30, 0);

            Assert.AreEqual(AccelData.GetFirstBurstTimestamp(), expectedValueStart);
            Assert.AreEqual(AccelData.GetLastBurstTimestamp(), expectedValueEnd);
        }

        




    }
}
