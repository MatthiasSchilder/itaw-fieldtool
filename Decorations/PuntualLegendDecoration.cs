using System;
using System.Collections.Generic;
using System.Drawing;
using fieldtool.Data.Movebank;
using SharpMap;
using SharpMap.Rendering.Decoration;

namespace fieldtool.Decorations
{
    class PuntualLegendDecoration : MapDecoration
    {
        /// <summary>
        /// Gets or sets the disclaimer text
        /// </summary>
        public List<FtTransmitterDataset> Datasets
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the disclaimer font
        /// </summary>
        public Font Font
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the font color
        /// </summary>
        public Color ForeColor
        {
            get;
            set;
        }

        public SolidBrush ForeGroundBrush;

        /// <summary>
        /// Creates an instance of this class
        /// </summary>
        public PuntualLegendDecoration()
        {
            this.Font = Properties.Settings.Default.MapLegendFont;
            this.ForeColor = Properties.Settings.Default.MapLegendTextColor;
            this.Opacity =Properties.Settings.Default.MapLegendBackgroundAlpha;
            ForeGroundBrush = new SolidBrush(this.ForeColor);

            base.Anchor = Properties.Settings.Default.MapLegendAnchor;
            base.BorderMargin = new Size(3, 3);
            base.BorderColor = Properties.Settings.Default.MapLegendBorderColor;
            base.BackgroundColor = base.OpacityColor(Properties.Settings.Default.MapLegendBackgroundColor);
            base.RoundedEdges = Properties.Settings.Default.MapLegendBorderRoundEdges;
        }

        public void Update(List<FtTransmitterDataset> datasets)
        {
            Datasets = datasets;
        }

        private const String FormatString = "{0} ({1})";
        private const int colorFieldOffs = 20; // px
        protected override Size InternalSize(Graphics g, Map map)
        {
            double cumulHeight = 0;
            double maxWidth = double.MinValue;

            foreach (var dataset in Datasets)
            {
                string str = CreateLegendString(dataset);

                SizeF s = g.MeasureString(String.Format(FormatString, dataset.TagId, str), this.Font);
                cumulHeight += s.Height;
                maxWidth = Math.Max(s.Width, maxWidth);
            }

            maxWidth += colorFieldOffs;
            
            return new Size((int)System.Math.Ceiling(maxWidth), (int)System.Math.Ceiling(cumulHeight));
        }

        private string CreateLegendString(FtTransmitterDataset dataset)
        {
            DateTime start, end;
            const string format = "g";

            if (dataset.GPSData.DateTimeFilterStart.HasValue && dataset.GPSData.DateTimeFilterStop.HasValue)
            {
                start = dataset.GPSData.DateTimeFilterStart.Value;
                end = dataset.GPSData.DateTimeFilterStop.Value;
            }
            else
            {
                start = dataset.GPSData.GpsSeries.GetFirstGpsDataEntry().StartTimestamp;
                end = dataset.GPSData.GpsSeries.GetLatestGpsDataEntry().StartTimestamp;
            }

            return String.Format("{0} - {1}", start.ToString(format), end.ToString(format));
        }

        protected override void OnRender(Graphics g, Map map)
        {
            RectangleF layoutRectangle = g.ClipBounds;
            var rowHeight = CalcRowHeight(layoutRectangle);

            int i = 0;
            foreach (var dataset in Datasets)
            {
                CreateLegendRow(dataset, g, layoutRectangle.X, layoutRectangle.Y + rowHeight * i++, rowHeight);
            }
        }

        private void CreateLegendRow(FtTransmitterDataset dataset, Graphics g, float x, float y, float rowHeight)
        {
            var spacingOffs = rowHeight*0.15;
            string str = CreateLegendString(dataset);
            
            //g.DrawRectangle(new Pen(dataset.Color), x, (float) (y + spacingOffs), rowHeight, (float)(rowHeight - (float)(2 * spacingOffs)));
            g.FillRectangle(new SolidBrush(dataset.Visulization.Color), x, (float)(y + spacingOffs), rowHeight, (float)(rowHeight - (float)(2 * spacingOffs)));
            g.DrawString(String.Format(FormatString, dataset.TagId, str), Font, ForeGroundBrush, x + colorFieldOffs, y);
        }

        private float CalcRowHeight(RectangleF layoutRectangle)
        {
           return layoutRectangle.Size.Height / Datasets.Count;
        }
    }
}
