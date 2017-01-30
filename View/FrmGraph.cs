using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using fieldtool.Data.Movebank;
using System.Collections.Generic;

namespace fieldtool.View
{
    public partial class FrmGraph : Form
    {
        private class GraphYScaleSettings
        {
            public object MinValue;
            public object MaxValue;
        }

        private Dictionary<EGraphType, GraphYScaleSettings> yScaleSettings = 
            new Dictionary<EGraphType, GraphYScaleSettings>();


        private enum EGraphType
        {
            BatteryVoltageVsTime,
            FixBatteryVoltageVsTime,
            UsedTimeToGetFixVsTime,
            FixBatteryVoltageAndTempVsTime
        }

        private String GetDisplayNameForGraphType(EGraphType type)
        {
            switch (type)
            {
                case EGraphType.BatteryVoltageVsTime:
                    return "Batteriespannung vs. Datum";
                case EGraphType.FixBatteryVoltageVsTime:
                    return "Fix-Batteriespannung vs. Datum";
                case EGraphType.UsedTimeToGetFixVsTime:
                    return "Zeit bis GPS-Fix vs. Datum";
                case EGraphType.FixBatteryVoltageAndTempVsTime:
                    return "Fix-Batteriespannung / Temperatur vs. Datum";
                default:
                    return "";
            }
        }

        private const double AXIS_UNIT_MV_DEFAULT_INTERVAL = 100;
        private Color GRID_COLOR = Color.LightGray;

        private FtTransmitterDataset _dataset;
        public FrmGraph(FtTransmitterDataset dataset)
        {
            _dataset = dataset;
            
            InitializeComponent();
            
            this.Text = $"Graphdarstellung für Tag-ID {_dataset.TagId}";
            PopulateComboBox();
            SetMinMaxDate();
        }

        private void SetMinMaxDate()
        {
            var maxDate = _dataset.GPSData.GpsSeries.Max(series => series.StartTimestamp);
            var minDate = _dataset.GPSData.GpsSeries.Min(series => series.StartTimestamp);
            dateIntervalPicker1.SetDateInterval(minDate, maxDate);
        }

        private void PopulateComboBox()
        {
            foreach (EGraphType value in Enum.GetValues(typeof(EGraphType)))
            {
                var displayName = GetDisplayNameForGraphType(value);
                var comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = displayName;
                comboBoxItem.Tag = value;
                comboBox1.Items.Add(comboBoxItem);
            }
            comboBox1.DisplayMember = "Content";
            comboBox1.SelectedIndex = 0;
        }

        private void ShowFixtimeVsDate()
        {
            chart2.Titles[0].Text = $"Zeit bis GPS-Fix vs. Zeit - Tag-ID {_dataset.TagId}";

            chart2.ChartAreas.Clear();
            chart2.ChartAreas.Add(new ChartArea());
            chart2.ChartAreas[0].AxisX.Title = "Datum [TT.MM.JJ]";
            chart2.ChartAreas[0].AxisY.Title = "Zeit bis GPS-Fix [s]";
            chart2.ChartAreas[0].AxisX.LabelStyle.Angle = -65;
            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "dd.MM.yy";

            chart2.ChartAreas[0].AxisY.LabelStyle.TruncatedLabels = false;
            chart2.ChartAreas[0].AxisY.Interval = 10;
            chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;

            chart2.Series.Clear();
            Series battVoltagevsTimeSeries = new Series("BattVoltageVsTime")
            {
                ChartType = SeriesChartType.Line,
                XAxisType = AxisType.Primary,
                YAxisType = AxisType.Primary,
                Enabled = true,
                XValueType = ChartValueType.Date,
                YValueType = ChartValueType.Int32
            };

            foreach (var gpsDataPoint in _dataset.GPSData.GpsSeries.Where(dp => dp.StartTimestamp >= dateIntervalPicker1.StartTimpestamp && dp.StartTimestamp <= dateIntervalPicker1.EndTimestamp))
            {
                battVoltagevsTimeSeries.Points.AddXY(gpsDataPoint.StartTimestamp.ToOADate(), gpsDataPoint.UsedTimeToGetFix);
            }

            if (battVoltagevsTimeSeries.Points.Any())
            {
                var min = battVoltagevsTimeSeries.Points.Min(dp => dp.YValues[0]);
                chart2.ChartAreas[0].AxisY.Minimum = Math.Floor(min - min % 5);
                chart2.ChartAreas[0].AxisY.Maximum = 120;
            }

            chart2.Series.Add(battVoltagevsTimeSeries);
        }

        private void ShowFixBatteryVoltageTemperatureVsTime()
        {
            chart2.Titles[0].Text = $"Fix-Batteriespannung / Temperatur vs. Zeit - Tag-ID {_dataset.TagId}";

            chart2.ChartAreas.Clear();
            chart2.ChartAreas.Add(new ChartArea());
            chart2.ChartAreas[0].AxisX.Title = "Datum [TT.MM.JJ]";
            chart2.ChartAreas[0].AxisY.Title = "Spannung [mV]";
            chart2.ChartAreas[0].AxisY2.Title = "Temperatur (°C)";

            chart2.ChartAreas[0].AxisX.LabelStyle.Angle = -65;
            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "dd.MM.yy";
            chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;

            chart2.ChartAreas[0].AxisY.LabelStyle.TruncatedLabels = false;
            chart2.ChartAreas[0].AxisY.Interval = 100;
            chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            chart2.ChartAreas[0].AxisY2.LabelStyle.TruncatedLabels = false;
            chart2.ChartAreas[0].AxisY2.Interval = 5;
            chart2.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Gray;

            chart2.Series.Clear();
            Series battVoltagevsTimeSeries = new Series("BattVoltageVsTime")
            {
                ChartType = SeriesChartType.Line,
                XAxisType = AxisType.Primary,
                YAxisType = AxisType.Primary,
                Enabled = true,
                XValueType = ChartValueType.Date,
                YValueType = ChartValueType.Int32
            };
            
            Series tempvsTimeSeries = new Series("TempVsTime")
            {
                ChartType = SeriesChartType.Line,
                XAxisType = AxisType.Primary,
                YAxisType = AxisType.Secondary,
                Enabled = true,
                XValueType = ChartValueType.Date,
                YValueType = ChartValueType.Int32
            };
            foreach (var gpsDataPoint in _dataset.GPSData.GpsSeries.Where(dp => dp.StartTimestamp >= dateIntervalPicker1.StartTimpestamp && dp.StartTimestamp <= dateIntervalPicker1.EndTimestamp))
            {
                battVoltagevsTimeSeries.Points.AddXY(gpsDataPoint.StartTimestamp.ToOADate(), gpsDataPoint.BatteryVoltageFix);
                tempvsTimeSeries.Points.AddXY(gpsDataPoint.StartTimestamp.ToOADate(), gpsDataPoint.Temperature);
            }

            if (battVoltagevsTimeSeries.Points.Any())
            {
                var min = battVoltagevsTimeSeries.Points.Min(dp => dp.YValues[0]);
                chart2.ChartAreas[0].AxisY.Minimum = Math.Floor(min - min % 100);
                var max = battVoltagevsTimeSeries.Points.Max(dp => dp.YValues[0]);
                chart2.ChartAreas[0].AxisY.Maximum = Math.Ceiling(max + (100 - max % 100));
            }

            if (tempvsTimeSeries.Points.Any())
            {
                var min = tempvsTimeSeries.Points.Min(dp => dp.YValues[0]);
                if (min < 0)
                    chart2.ChartAreas[0].AxisY2.Minimum = min - (5 + min % 5);
                else
                    chart2.ChartAreas[0].AxisY2.Minimum = Math.Floor(min - min % 5);

                var max = tempvsTimeSeries.Points.Max(dp => dp.YValues[0]);
                chart2.ChartAreas[0].AxisY2.Maximum = Math.Ceiling(max + (5 - max % 5));
            }

            chart2.Series.Add(battVoltagevsTimeSeries);
            chart2.Series.Add(tempvsTimeSeries);

            chart2.ApplyPaletteColors();
            chart2.ChartAreas[0].AxisY.TitleForeColor = battVoltagevsTimeSeries.Color;
            chart2.ChartAreas[0].AxisY2.TitleForeColor = tempvsTimeSeries.Color;
        }

        private void ShowFixBatteryVoltageVsTime()
        {
            chart2.Titles[0].Text = $"Fix-Batteriespannung vs. Zeit - Tag-ID {_dataset.TagId}";

            chart2.ChartAreas.Clear();
            chart2.ChartAreas.Add(new ChartArea());
            chart2.ChartAreas[0].AxisX.Title = "Datum [TT.MM.JJ]";
            chart2.ChartAreas[0].AxisY.Title = "Spannung [mV]";

            chart2.ChartAreas[0].AxisX.LabelStyle.Angle = -65;
            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "dd.MM.yy";

            chart2.ChartAreas[0].AxisY.LabelStyle.TruncatedLabels = false;
            chart2.ChartAreas[0].AxisY.Interval = 100;
            chart2.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.LightGray;

            chart2.Series.Clear();
            Series battVoltagevsTimeSeries = new Series("BattVoltageVsTime")
            {
                ChartType = SeriesChartType.Line,
                XAxisType = AxisType.Primary,
                YAxisType = AxisType.Primary,
                Enabled = true,
                XValueType = ChartValueType.Date,
                YValueType = ChartValueType.Int32
            };

            foreach (var gpsDataPoint in _dataset.GPSData.GpsSeries.Where(dp => dp.StartTimestamp >= dateIntervalPicker1.StartTimpestamp && dp.StartTimestamp <= dateIntervalPicker1.EndTimestamp))
            {
                battVoltagevsTimeSeries.Points.AddXY(gpsDataPoint.StartTimestamp.ToOADate(), gpsDataPoint.BatteryVoltageFix);
            }

            if (battVoltagevsTimeSeries.Points.Any())
            {
                var min = battVoltagevsTimeSeries.Points.Min(dp => dp.YValues[0]);
                chart2.ChartAreas[0].AxisY.Minimum = Math.Floor(min - min % 100);
                var max = battVoltagevsTimeSeries.Points.Max(dp => dp.YValues[0]);
                chart2.ChartAreas[0].AxisY.Maximum = Math.Ceiling(max + (100 - max % 100));
            }

            chart2.Series.Add(battVoltagevsTimeSeries);
        }
        private void ShowBatteryVoltageVsTime()
        {
            chart2.Titles[0].Text = $"Batteriespannung vs. Zeit - Tag-ID {_dataset.TagId}";

            chart2.ChartAreas.Clear();
            chart2.ChartAreas.Add(new ChartArea());
            chart2.ChartAreas[0].AxisX.Title = "Datum [TT.MM.JJ]";
            chart2.ChartAreas[0].AxisY.Title = "Spannung [mV]";

            chart2.ChartAreas[0].AxisX.LabelStyle.Angle = -65;
            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "dd.MM.yy";

            chart2.ChartAreas[0].AxisY.LabelStyle.TruncatedLabels = false;
            chart2.ChartAreas[0].AxisY.Interval = 100;
            chart2.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.LightGray;

            chart2.Series.Clear();
            Series battVoltagevsTimeSeries = new Series("BattVoltageVsTime")
            {
                ChartType = SeriesChartType.Line,
                XAxisType = AxisType.Primary,
                YAxisType = AxisType.Primary,
                Enabled = true,
                XValueType = ChartValueType.Date,
                YValueType = ChartValueType.Int32
            };

            foreach (var gpsDataPoint in _dataset.GPSData.GpsSeries.Where(dp => dp.StartTimestamp >= dateIntervalPicker1.StartTimpestamp && dp.StartTimestamp <= dateIntervalPicker1.EndTimestamp))
            {
                battVoltagevsTimeSeries.Points.AddXY(gpsDataPoint.StartTimestamp.ToOADate(), gpsDataPoint.BatteryVoltage);
            }

            if(battVoltagevsTimeSeries.Points.Any())
            {
                var min = battVoltagevsTimeSeries.Points.Min(dp => dp.YValues[0]);
                chart2.ChartAreas[0].AxisY.Minimum = Math.Floor(min - min % 100);
                var max = battVoltagevsTimeSeries.Points.Max(dp => dp.YValues[0]);
                chart2.ChartAreas[0].AxisY.Maximum = Math.Ceiling(max + (100 - max % 100));
            }

            chart2.Series.Add(battVoltagevsTimeSeries);           
        }

        private void FrmGraph_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                GraphImageToClipboard();
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            GraphImageToClipboard();
        }

        private void GraphImageToClipboard()
        {
            var memStream = new MemoryStream();
            chart2.SaveImage(memStream, ImageFormat.Bmp);

            var bitmap = new Bitmap(memStream);
            Clipboard.SetImage(bitmap);
        }

        private void dateIntervalPicker1_IntervalChanged(object sender, EventArgs e)
        {
            UpdateVisu();
        }

        private EGraphType? GetGraphType()
        {
            var comboboxItem = comboBox1.SelectedItem as ComboBoxItem;
            return (EGraphType?) comboboxItem?.Tag;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVisu();
        }

        private void UpdateVisu()
        {
            var graphType = GetGraphType();

            switch (graphType)
            {
                case EGraphType.BatteryVoltageVsTime:
                    ShowBatteryVoltageVsTime();
                    break;
                case EGraphType.FixBatteryVoltageVsTime:
                    ShowFixBatteryVoltageVsTime();
                    break;
                case EGraphType.UsedTimeToGetFixVsTime:
                    ShowFixtimeVsDate();
                    break;
                case EGraphType.FixBatteryVoltageAndTempVsTime:
                    ShowFixBatteryVoltageTemperatureVsTime();
                    break;
                default:
                    return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnScaleAxis_Click(object sender, EventArgs e)
        {

        }
    }
}
