using System;
using System.Collections.Generic;
using System.Drawing;
using fieldtool.Data.Movebank;
using SharpMap;
using SharpMap.Rendering.Decoration;

namespace fieldtool.Decorations
{
    class PolygonalLegendDecoration : MapDecoration
    {
        /// <summary>
        /// Gets or sets the disclaimer text
        /// </summary>
        public List<FtTransmitterMCPDataEntry> MCPs
        {
            get;
            set;
        }

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
        public PolygonalLegendDecoration()
        {
            this.Font = Properties.Settings.Default.MapLegendFont;
            this.ForeColor = Properties.Settings.Default.MapLegendTextColor;
            this.Opacity =Properties.Settings.Default.MapLegendBackgroundAlpha;
            ForeGroundBrush = new SolidBrush(this.ForeColor);

            base.Anchor = /*Properties.Settings.Default.MapLegendAnchor;*/ MapDecorationAnchor.LeftTop;
            base.BorderMargin = new Size(3, 3);
            base.BorderColor = Properties.Settings.Default.MapLegendBorderColor;
            base.BackgroundColor = base.OpacityColor(Properties.Settings.Default.MapLegendBackgroundColor);
            base.RoundedEdges = Properties.Settings.Default.MapLegendBorderRoundEdges;
        }

        public void Update(List<FtTransmitterMCPDataEntry> mcps)
        {
            MCPs = mcps;
        }

        private const String FormatString = "{0} ({1}%)";
        private const int colorFieldOffs = 20; // px
        protected override Size InternalSize(Graphics g, Map map)
        {
            double cumulHeight = 0;
            double maxWidth = double.MinValue;

            foreach (var mcp in MCPs)
            {
                SizeF s = g.MeasureString(CreateLegendString(mcp), this.Font);
                cumulHeight += s.Height;
                maxWidth = Math.Max(s.Width, maxWidth);
            }

            maxWidth += colorFieldOffs;
            
            return new Size((int)System.Math.Ceiling(maxWidth), (int)System.Math.Ceiling(cumulHeight));
        }

        private string CreateLegendString(FtTransmitterMCPDataEntry mcp)
        {
            return String.Format(FormatString, "MCP", mcp.PercentageMCP);
        }

        protected override void OnRender(Graphics g, Map map)
        {
            RectangleF layoutRectangle = g.ClipBounds;
            var rowHeight = CalcRowHeight(layoutRectangle);

            int i = 0;
            foreach (var dataset in MCPs)
            {
                CreateLegendRow(dataset, g, layoutRectangle.X, layoutRectangle.Y + rowHeight * i++, rowHeight);
            }
        }

        private void CreateLegendRow(FtTransmitterMCPDataEntry mcp, Graphics g, float x, float y, float rowHeight)
        {
            var spacingOffs = rowHeight*0.15;
            string str = CreateLegendString(mcp);
            
            //g.DrawRectangle(new Pen(dataset.VisulizationColor), x, (float) (y + spacingOffs), rowHeight, (float)(rowHeight - (float)(2 * spacingOffs)));
            g.FillRectangle(new SolidBrush(Color.BlueViolet), x, (float)(y + spacingOffs), rowHeight, (float)(rowHeight - (float)(2 * spacingOffs)));
            g.DrawString(CreateLegendString(mcp), Font, ForeGroundBrush, x + colorFieldOffs, y);
        }

        private float CalcRowHeight(RectangleF layoutRectangle)
        {
           return layoutRectangle.Size.Height / MCPs.Count;
        }
    }
}
