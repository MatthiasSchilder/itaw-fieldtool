using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool
{
    public class FtTransmitterTagInfoData
    {
        public DateTime? LastDownload { get; }
        public bool LastDownloadTimeAvailable => LastDownload.HasValue;

        private const string PATTERN1 = "d.M.yyyy";
        private const string PATTERN2 = "HH:mm:ss";

        public FtTransmitterTagInfoData(string filePath)
        {
            try
            {
                TextReader accelDataReader = File.OpenText(filePath);
                String line;
                accelDataReader.ReadLine(); // skip first line with fileheader

                List<String> aheadLines = new List<string>();
                while ((line = accelDataReader.ReadLine()) != null)
                {
                    if (line.Contains("Data download:"))
                    {
                        aheadLines.Clear();
                        accelDataReader.ReadLine(); // eine Zeile verwerfen
                        aheadLines.Add(accelDataReader.ReadLine());
                    }
                }
                // 3012,2.6.2015,Tu,20:34:40
                string[] dateComponents = aheadLines.Last().Split(',');
                DateTime component1;
                DateTime.TryParseExact(dateComponents[1], PATTERN1, CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out component1);
                
                DateTime component2;
                DateTime.TryParseExact(dateComponents[3], PATTERN2, CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out component2);

                LastDownload = new DateTime(component1.Year, component1.Month, component1.Day, component2.Hour, component2.Minute, component2.Second);
            }
            catch (Exception)
            {
                
            }
        }

    }
}
