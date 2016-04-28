using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMap;
using SharpMap.Rendering.Decoration;
using SharpMap.Rendering.Symbolizer;

namespace fieldtool.Decorations
{
    class LegendDecoration : MapDecoration
    {
        /// <summary>
        /// Gets or sets the disclaimer text
        /// </summary>
        public List<string> Texts
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

        /// <summary>
        /// Creates an instance of this class
        /// </summary>
        public LegendDecoration(List<FtTransmitterDataset> datasets)
        {
            this.Font = SystemFonts.DefaultFont;
            this.ForeColor = Color.Black;
            Texts = new List<string>();
            foreach (var dataset in datasets)
            {
                Texts.Add(dataset.TagId.ToString());
            }
            base.Anchor = MapDecorationAnchor.CenterBottom;
            this.Font = new Font("Arial", 8f, FontStyle.Italic);
            base.BorderMargin = new Size(3, 3);
            base.BorderColor = Color.Black;
            base.BorderWidth = 1;
            base.RoundedEdges = false;

            base.Anchor = MapDecorationAnchor.LeftTop;
        }


        /// <summary>
        /// Function to compute the required size for rendering the map decoration object
        /// <para>This is just the size of the decoration object, border settings are excluded</para>
        /// </summary>
        /// <param name="g"></param>
        /// <param name="map"></param>
        /// <returns>The</returns>
        protected override Size InternalSize(Graphics g, Map map)
        {
            double cumulHeight = 0;
            double width = 0;
            foreach (var text in Texts)
            {
                SizeF s = g.MeasureString(text, this.Font);
                cumulHeight += s.Height;
                width = s.Width;
            }
            

            return new Size((int)System.Math.Ceiling(width), (int)System.Math.Ceiling(cumulHeight));
        }

        /// <summary>
        /// Function to render the actual map decoration
        /// </summary>
        /// <param name="g"></param>
        /// <param name="map"></param>
        protected override void OnRender(Graphics g, Map map)
        {
            RectangleF layoutRectangle = g.ClipBounds;
            SolidBrush b = new SolidBrush(base.OpacityColor(this.ForeColor));

            var pxPerRow = layoutRectangle.Size.Height / Texts.Count;


            //g.DrawString(this.Text, this.Font, b, layoutRectangle);
            int i = 0;
            foreach (var text in Texts)
            {
                g.DrawString(text, Font, b, layoutRectangle.X, layoutRectangle.Y + pxPerRow * i++);
            }
        }
    }
}
