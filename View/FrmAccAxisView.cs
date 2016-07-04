using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Media;

namespace fieldtool.View
{
    public partial class FrmAccAxisView : Form
    {
        private DateTime DataStartTimestamp;
        private DateTime DataStopTimestamp;

        private double DataStartTimestampOA;
        private double DataStopTimestampOA;


        public FrmAccAxisView(FtTransmitterAccelData accelData)
        {
            InitializeComponent();

            

            chart1.ChartAreas.Add(new ChartArea());
            chart1.ChartAreas[0].AxisX.Title = "Datum";
            chart1.ChartAreas[0].AxisY.Title = "ACC";

            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;


            //chart1.ChartAreas[0].AxisX.Interval = 5.0;
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            //chart1.ChartAreas[0].CursorX.IntervalType = DateTimeIntervalType.Minutes
            //chart1./*ChartAreas*/[0].CursorX.

                chart1.ChartAreas[0].CursorX.Interval = 0.01;


            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Minutes;



            Series SeriesX = new Series("X");
            SeriesX.ChartType = SeriesChartType.FastLine;
            SeriesX.XAxisType = AxisType.Primary;
            SeriesX.YAxisType = AxisType.Primary;
            SeriesX.XValueType = ChartValueType.Date;
            SeriesX.YValueType = ChartValueType.Int32;
            SeriesX.Color = System.Drawing.Color.Green;
            

            Series SeriesY = new Series("Y");
            SeriesY.ChartType = SeriesChartType.FastLine;
            SeriesY.XAxisType = AxisType.Primary;
            SeriesY.YAxisType = AxisType.Primary;
            SeriesY.XValueType = ChartValueType.Date;
            SeriesY.YValueType = ChartValueType.Int32;
            SeriesY.Color = System.Drawing.Color.Blue;

            Series SeriesZ = new Series("Z");
            SeriesZ.ChartType = SeriesChartType.FastLine;
            SeriesZ.XAxisType = AxisType.Primary;
            SeriesZ.YAxisType = AxisType.Primary;
            SeriesZ.XValueType = ChartValueType.Date;
            
            SeriesZ.YValueType = ChartValueType.Int32;
            SeriesZ.Color = System.Drawing.Color.Red;

            DataStartTimestamp = accelData.GetFirstBurstTimestamp();
            DataStopTimestamp = accelData.GetLastBurstTimestamp();

            DataStartTimestampOA = DataStartTimestamp.ToOADate();
            DataStopTimestampOA = DataStopTimestamp.ToOADate();

            chart1.ChartAreas[0].AxisX.ScaleView = new AxisScaleView();
            chart1.ChartAreas[0].AxisX.ScaleView.SmallScrollSize = 0;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Minutes;
            chart1.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Minutes;
            chart1.ChartAreas[0].AxisX.ScaleView.MinSize = 1;

            double minY_SeriesX;
            double minY_SeriesY;
            double minY_SeriesZ;

            double maxY_SeriesX;
            double maxY_SeriesY;
            double maxY_SeriesZ;

            minY_SeriesX = minY_SeriesZ = minY_SeriesY = Double.MaxValue;
            maxY_SeriesZ = maxY_SeriesX = maxY_SeriesY = Double.MinValue;

            double minY_X = double.MaxValue, maxY = double.MinValue;
            foreach (var acc in accelData.AccelerationSeries)
            {
                var xArr = acc.GetXArr();
                var yArr = acc.GetYArr();
                var zArr = acc.GetZArr();

                var avgX = xArr.ToList().Average();

                var avgY = yArr.ToList().Average();
                var avgZ = zArr.ToList().Average();

                minY_SeriesX = Math.Min(minY_SeriesX, avgX);
                minY_SeriesY = Math.Min(minY_SeriesY, avgY);
                minY_SeriesZ = Math.Min(minY_SeriesZ, avgZ);

                maxY_SeriesX = Math.Max(maxY_SeriesX, avgX);
                maxY_SeriesY = Math.Max(maxY_SeriesY, avgY);
                maxY_SeriesZ = Math.Max(maxY_SeriesZ, avgZ);

                SeriesX.Points.AddXY(acc.StartTimestamp.ToOADate(), avgX);
                SeriesY.Points.AddXY(acc.StartTimestamp.ToOADate(), avgY);
                SeriesZ.Points.AddXY(acc.StartTimestamp.ToOADate(), avgZ);

            }

            chart1.ChartAreas[0].AxisY.Minimum = Math.Min(minY_SeriesX, Math.Min(minY_SeriesY, minY_SeriesZ));
            chart1.ChartAreas[0].AxisY.Maximum = Math.Max(maxY_SeriesX, Math.Max(maxY_SeriesY, maxY_SeriesZ));

            //MessageBox.Show(SeriesX.Points.Count.ToString());

            chart1.Series.Clear();
            chart1.Series.Add(SeriesX);
            chart1.Series.Add(SeriesY);
            chart1.Series.Add(SeriesZ);

            chart1.MouseWheel += Chart1OnMouseWheel;


        }


        private double[] ZoomWidthsInDecimalDays = new[]
        {100, 50, 25, 10, 5, 2, 1, 0.5, 0.25, 0.166667, 0.08333333, 0.0625, 0.0416666, 0.0208333};

        private int ZoomLevel = -1;
        private const int MaxZoomLevel = 13;
        private const int MinZoomLevel = -1;

        private void Chart1OnMouseWheel(object sender, MouseEventArgs mouseEventArgs)
        {
            
            var wheelXYPos = mouseEventArgs.Location;
            var xValue = chart1.ChartAreas[0].AxisX.PixelPositionToValue(wheelXYPos.X);

            var zoomInDate = DateTime.FromOADate(xValue);

            if (mouseEventArgs.Delta > 0)
            {
                ZoomLevel = Math.Min(++ZoomLevel, MaxZoomLevel);

                var xMin = Math.Max(xValue - ZoomWidthsInDecimalDays[ZoomLevel], DataStartTimestampOA);
                var xMax = Math.Min(xValue + ZoomWidthsInDecimalDays[ZoomLevel], DataStopTimestampOA);

                chart1.ChartAreas[0].AxisX.ScaleView.Zoom(xMin, xMax);
            }
            else
            {
                ZoomLevel = Math.Max(MinZoomLevel, --ZoomLevel);
                if (ZoomLevel == -1)
                    chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                else
                {
                    var xMin = Math.Max(xValue - ZoomWidthsInDecimalDays[ZoomLevel], DataStartTimestampOA);
                    var xMax = Math.Min(xValue + ZoomWidthsInDecimalDays[ZoomLevel], DataStopTimestampOA);
                    chart1.ChartAreas[0].AxisX.ScaleView.Zoom(xMin, xMax);
                }
                    
            }
        }

        private void chkX_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Series["X"].Enabled = ((CheckBox) sender).Checked;
        }

        private void chkY_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Series["Y"].Enabled = ((CheckBox)sender).Checked;
        }

        private void chkZ_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Series["Z"].Enabled = ((CheckBox)sender).Checked;
        }
    }
}
