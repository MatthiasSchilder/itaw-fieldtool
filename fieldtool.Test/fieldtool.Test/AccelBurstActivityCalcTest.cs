using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fieldtool.Test
{
    [TestClass]
    public class AccelBurstActivityCalcTest
    {
        private string PathAccelTestData =
            @"..\..\..\..\data\testdata\movebank\tag1704_acc.txt";
        private AccelBurstActivityCalculator Calculator;

        [TestInitialize]
        public void Init()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                PathAccelTestData);
            var tagAccelData = new FtTransmitterAccelData(path);
            
            Calculator = new AccelBurstActivityCalculator(tagAccelData, 
                new DateTime(2015, 3, 15), 
                new DateTime(2016, 2, 3));
            //Calculator.Process()
        }
        [TestMethod]
        public void TestStartTimeStamp()
        {
            DateTime expectedValueMissing = new DateTime(2015, 3, 15, 9, 50, 0);
            DateTime expectedValueVollst = new DateTime(2015, 3, 15, 13, 51, 59);
            
        }

        




    }
}
