using System;
using System.IO;
using System.Windows.Forms;
using DotSpatial.Data;
using DotSpatial.Topology;
using fieldtool.Data.Movebank;

namespace fieldtool.View
{
    public partial class FrmExportShape : Form
    {
        private FtProject _ftProject;

        private string _exportPath;

        private string ExportPath
        {
            get { return _exportPath; }
            set
            {
                _exportPath = value;
                textBox1.Text = value;
            }
        }

        public FrmExportShape(FtProject projekt)
        {
            _ftProject = projekt;
            InitializeComponent();
        }

        private void FrmExportShape_Load(object sender, EventArgs e)
        {

        }

        private void btnPick_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Shapefile (.shp) | *.shp";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            ExportPath = sfd.FileName;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ExportPath))
            {
                MessageBox.Show("Bitte wählen Sie einen Exportpfad aus.");
                return;
            }

            Export();

            MessageBox.Show("Export wurde abgeschlossen.");
            this.Close();
        }

        private void Export()
        {
            if(rbSingleShapeExport.Checked)
                SingleFeatureSetExport();
            else
                MultiFeatureSetExport();
        }
        private FeatureSet CreateFeatureSet()
        {
            FeatureSet fs = new FeatureSet(FeatureType.Point);
            fs.DataTable.Columns.Add("TagID", typeof(string));
            fs.DataTable.Columns.Add("Timestamp", typeof(DateTime));
            fs.DataTable.Columns.Add("TimestampOfFix", typeof(DateTime));

            fs.DataTable.Columns.Add("UsedTimeToGetFix", typeof(short));
            fs.DataTable.Columns.Add("HeightAboveEllipsoid", typeof(double));
            fs.DataTable.Columns.Add("HeadingDegree", typeof(double));
            fs.DataTable.Columns.Add("SpeedOverGround", typeof(double));
            fs.DataTable.Columns.Add("Status", typeof(string));
            fs.DataTable.Columns.Add("BatteryVoltage", typeof(short));
            fs.DataTable.Columns.Add("BatteryVoltageAtFix", typeof(short));
            fs.DataTable.Columns.Add("Temperature", typeof(short));
            return fs;

        }

        private void SingleFeatureSetExport()
        {
            FeatureSet fs = CreateFeatureSet();
            foreach (var dataset in _ftProject.Datasets)
            {
                if (!ExportTag(dataset.TagId))
                    continue;

                foreach (var gpsPoint in dataset.GPSData.GpsSeries)
                {
                    GpsDataPointToFeatureRow(dataset.TagId, fs, gpsPoint);
                }
            }
            fs.SaveAs(ExportPath, true);
        }

        private void MultiFeatureSetExport()
        {
            foreach (var dataset in _ftProject.Datasets)
            {
                if (!ExportTag(dataset.TagId))
                    continue;
                FeatureSet fs = CreateFeatureSet();
                foreach (var gpsPoint in dataset.GPSData.GpsSeries)
                {
                    GpsDataPointToFeatureRow(dataset.TagId, fs, gpsPoint);
                }
                fs.SaveAs(CreateMultiexportFilename(dataset.TagId), true);
            }
        }

        private void GpsDataPointToFeatureRow(int tagID, FeatureSet fs, FtTransmitterGpsDataEntry gpsPoint)
        {
            DotSpatial.Topology.Point point = null;
            if (!gpsPoint.Rechtswert.HasValue || !gpsPoint.Hochwert.HasValue)
                point = new DotSpatial.Topology.Point(double.NaN, double.NaN);
            else
                point = new DotSpatial.Topology.Point(gpsPoint.Rechtswert.Value, gpsPoint.Hochwert.Value);
            var feature = fs.AddFeature(point);

            feature.DataRow.BeginEdit();
            feature.DataRow["TagID"] = tagID;
            feature.DataRow["Timestamp"] = gpsPoint.StartTimestamp;
            if (gpsPoint.TimestampOfFix.HasValue)
                feature.DataRow["TimestampOfFix"] = gpsPoint.TimestampOfFix;
            feature.DataRow["UsedTimeToGetFix"] = gpsPoint.UsedTimeToGetFix;
            if(gpsPoint.HeightAboveEllipsoid.HasValue)
                feature.DataRow["HeightAboveEllipsoid"] = gpsPoint.HeightAboveEllipsoid.Value;
            if (gpsPoint.HeadingDegree.HasValue)
                feature.DataRow["HeadingDegree"] = gpsPoint.HeadingDegree.Value;
            if (gpsPoint.SpeedOverGround.HasValue)
                feature.DataRow["SpeedOverGround"] = gpsPoint.SpeedOverGround.Value;
            feature.DataRow["Status"] = gpsPoint.Status;
            feature.DataRow["BatteryVoltage"] = gpsPoint.BatteryVoltage;
            feature.DataRow["BatteryVoltageAtFix"] = gpsPoint.BatteryVoltageFix;
            feature.DataRow["Temperature"] = gpsPoint.Temperature;

            feature.DataRow.EndEdit();
        }

        private bool ExportTag(int tagId)
        {
            if (rbAlleTags.Checked)
                return true;

            if (_ftProject.Datasets.Find(dataset => dataset.TagId == tagId).Active)
                return true;
            return false;
        }

        private String CreateMultiexportFilename(int tagId)
        {
            var dirPath = Path.GetDirectoryName(ExportPath);
            var filePath = Path.GetFileNameWithoutExtension(ExportPath);
            var fileExt = Path.GetExtension(ExportPath);

            var finalFileName = Path.ChangeExtension($"{filePath}_{tagId}", fileExt);

            return Path.Combine(dirPath, finalFileName);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
