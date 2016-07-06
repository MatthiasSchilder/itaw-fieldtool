using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Media;
using fieldtool.Data.Movebank;
using fieldtool.Util;

namespace fieldtool.View
{
    public partial class FrmAccAxisView : Form
    {
        private enum EAggregateFunction
        {
            AVG,
            MIN,
            MAX,
            MEDIAN
        }

        private enum EDataDisplayMode
        {
            AGGREGATEDRAW,
            PROCESSEDACCS
        }

        private EDataDisplayMode GetDataDisplayMode()
        {
            if((string) comboBox1.SelectedItem == "aggregierte Burstwerte")
                return EDataDisplayMode.AGGREGATEDRAW;
            else
                return EDataDisplayMode.PROCESSEDACCS;
        }

        private EAggregateFunction GetAggregateFunction()
        {
            string aggregateFunctionAsString = (string) cmboAggregate.SelectedItem;
            switch (aggregateFunctionAsString)
            {
                case "avg":
                    return EAggregateFunction.AVG;
                case "min":
                    return EAggregateFunction.MIN;
                case "max":
                    return EAggregateFunction.MAX;
                case "median":
                    return EAggregateFunction.MEDIAN;
                default:
                    return EAggregateFunction.AVG;
            }
        }

        private DateTime DataStartTimestamp;
        private DateTime DataStopTimestamp;

        private double DataStartTimestampOA;
        private double DataStopTimestampOA;

        private FtTransmitterDataset dataset;


        public FrmAccAxisView(FtTransmitterDataset transmitterData)
        {
            InitializeComponent();
            dataset = transmitterData;
            this.Text = $"Beschleunigungswerte für Tag {dataset.TagId} im zeitlichen Verlauf";
            cmboAggregate.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            RegenChart();
            cmboAggregate.SelectedIndexChanged += cmboAggregate_SelectedIndexChanged;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            chart1.MouseWheel += Chart1OnMouseWheel;

            DataStartTimestamp = dataset.AccelData.GetFirstBurstTimestamp();
            DataStopTimestamp = dataset.AccelData.GetLastBurstTimestamp();
            //dateIntervalPicker1.

            DataStartTimestampOA = DataStartTimestamp.ToOADate();
            DataStopTimestampOA = DataStopTimestamp.ToOADate();
            //chart1.CursorPositionChanged += Chart1OnCursorPositionChanged;
        }

        private void DateIntervalPicker1OnIntervalChanged(object sender, EventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        private void RegenChart()
        {
            chart1.ChartAreas.Clear();

            chart1.ChartAreas.Add(new ChartArea());
            chart1.ChartAreas[0].AxisX.Title = "t [TT.MM.JJ HH:MM]";
            chart1.ChartAreas[0].AxisY.Title = "ACC";

            chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "dd.MM.yy HH:mm";

            chart1.ChartAreas[0].AxisX.ScaleView = new AxisScaleView();
            chart1.ChartAreas[0].AxisX.ScaleView.SmallScrollSize = 0;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Auto;
            chart1.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Auto;
            //chart1.ChartAreas[0].AxisX.ScaleView.MinSize = 1;

            chart1.Series.Clear();
            if (GetDataDisplayMode() == EDataDisplayMode.AGGREGATEDRAW)
            {
                foreach (Series ser in CreateRawValueSeries())
                    chart1.Series.Add(ser);
            }
            else if (GetDataDisplayMode() == EDataDisplayMode.PROCESSEDACCS)
            {
                chart1.Series.Add(CreateProcessedDACCSSeries());
            }

        }

        private Series CreateProcessedDACCSSeries()
        {
            Series series = new Series("berechnete Aktivitäten");
            series.ChartType = SeriesChartType.FastLine;
            series.XAxisType = AxisType.Primary;
            series.YAxisType = AxisType.Primary;
            series.XValueType = ChartValueType.Date;
            series.YValueType = ChartValueType.Double;
            series.Color = System.Drawing.Color.Green;

            foreach (var acc in dataset.AccelData.CalculatedActivities)
            {
                int i = 0;
                foreach (var value in acc.Value)
                {
                    var newval = value;
                    if (value == double.MinValue)
                        newval = 0;
                    series.Points.AddXY(acc.Key.AddMinutes(6*i++).ToOADate(), newval);
                }
            }

            return series;

        }

        private List<Series> CreateRawValueSeries()
        {
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


            double minY_SeriesX;
            double minY_SeriesY;
            double minY_SeriesZ;

            double maxY_SeriesX;
            double maxY_SeriesY;
            double maxY_SeriesZ;

            minY_SeriesX = minY_SeriesZ = minY_SeriesY = Double.MaxValue;
            maxY_SeriesZ = maxY_SeriesX = maxY_SeriesY = Double.MinValue;

            double minY_X = double.MaxValue, maxY = double.MinValue;
            foreach (var acc in dataset.AccelData.AccelerationSeries)
            {
                var xArr = acc.GetXArr();
                var yArr = acc.GetYArr();
                var zArr = acc.GetZArr();

                var avgX = ProcessWithAggregateFunction(xArr);
                var avgY = ProcessWithAggregateFunction(yArr);
                var avgZ = ProcessWithAggregateFunction(zArr);

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

            return new List<Series> {SeriesX, SeriesY, SeriesZ};
        }



        private double ProcessWithAggregateFunction(int[] arr)
        {
            var aggregateFunction = GetAggregateFunction();
            switch (aggregateFunction)
            {
                case EAggregateFunction.AVG:
                    return arr.ToList().Average();
                case EAggregateFunction.MIN:
                    return arr.ToList().Min();
                case EAggregateFunction.MAX:
                    return arr.ToList().Max();
                case EAggregateFunction.MEDIAN:
                    return MathHelper.Median(arr.ToList());
                default:
                    return arr.ToList().Average();

            }
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
            if (GetDataDisplayMode() == EDataDisplayMode.PROCESSEDACCS)
                return;
            chart1.Series["X"].Enabled = ((CheckBox) sender).Checked;
        }

        private void chkY_CheckedChanged(object sender, EventArgs e)
        {
            if (GetDataDisplayMode() == EDataDisplayMode.PROCESSEDACCS)
                return;
            chart1.Series["Y"].Enabled = ((CheckBox)sender).Checked;
        }

        private void chkZ_CheckedChanged(object sender, EventArgs e)
        {
            if (GetDataDisplayMode() == EDataDisplayMode.PROCESSEDACCS)
                return;
            chart1.Series["Z"].Enabled = ((CheckBox)sender).Checked;
        }

        private void cmboAggregate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GetDataDisplayMode() == EDataDisplayMode.PROCESSEDACCS)
                return;
            RegenChart();
        }

        private void chart1_MouseEnter(object sender, EventArgs e)
        {
            chart1.Focus();
        }

        //private void comboBox1_CursorChanged(object sender, EventArgs e)
        //{
            
           
        //}

        private void Chart1OnCursorPositionChanged(object sender, CursorEventArgs cursorEventArgs)
        {
            if (double.IsNaN(cursorEventArgs.NewPosition))
            {
                Debug.WriteLine("nan");
                return;
            }
                
            Debug.WriteLine(DateTime.FromOADate(cursorEventArgs.NewPosition));

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GetDataDisplayMode() == EDataDisplayMode.PROCESSEDACCS)
            {
                chkX.Enabled = false;
                chkY.Enabled = false;
                chkZ.Enabled = false;
                cmboAggregate.Enabled = false;
            }
            else if (GetDataDisplayMode() == EDataDisplayMode.AGGREGATEDRAW)
            {
                chkX.Enabled = true;
                chkY.Enabled = true;
                chkZ.Enabled = true;
                cmboAggregate.Enabled = true;
            }
            RegenChart();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            GraphImageToClipboard();
        }

        private void GraphImageToClipboard()
        {
            var memStream = new MemoryStream();
            chart1.SaveImage(memStream, ImageFormat.Bmp);

            var bitmap = new Bitmap(memStream);
            Clipboard.SetImage(bitmap);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
