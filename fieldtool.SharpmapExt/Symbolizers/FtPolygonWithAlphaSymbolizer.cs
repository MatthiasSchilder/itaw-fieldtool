using SharpMap;
using SharpMap.Rendering.Symbolizer;
using System.Drawing;
using System.Drawing.Drawing2D;
using GeoAPI.Geometries;

namespace fieldtool.SharpmapExt.Symbolizers
{
    public class FtPolygonWithAlphaSymbolizer : PolygonSymbolizer
    {
        /// <summary>
        /// Gets or sets the pen to render the outline of the polygon
        /// </summary>
        public Pen Outline
        {
            get;
            set;
        }

        public Color VisulizationColor { get; set; }

        /// <summary>
        /// Creates an instance of this class
        /// </summary>
        public FtPolygonWithAlphaSymbolizer(Color color)
        {
            this.Outline = new Pen(color, 1f);

            var fillColor = Color.FromArgb(255, color.R, color.G, color.B);
            this.Fill = new SolidBrush(fillColor);

            //this.Fill = new HatchBrush(HatchStyle.Cross, fillColor, Color.Transparent);
        }

        /// <summary>
        /// Method to release all managed resources
        /// </summary>
        protected override void ReleaseManagedResources()
        {
            base.CheckDisposed();
            if (this.Outline != null)
            {
                this.Outline.Dispose();
                this.Outline = null;
            }
            base.ReleaseManagedResources();
        }


        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override object Clone()
        {
            return new FtPolygonWithAlphaSymbolizer(VisulizationColor)
            {
                Fill = (Brush)base.Fill.Clone(),
                Outline = (Pen)this.Outline.Clone(),
                RenderOrigin = base.RenderOrigin,
            };
        }

        /// <summary>
        /// Method that does the actual rendering of geometries
        /// </summary>
        /// <param name="map">The map</param>
        /// <param name="polygon">The feature</param>
        /// <param name="g">The graphics object</param>
        protected override void OnRenderInternal(Map map, IPolygon polygon, Graphics g)
        {
            PointF[] array = polygon.TransformToImage(map);

            if (base.Fill != null)
            {
                //g.FillClosedCurve(base.Fill, array);
                g.FillPolygon(base.Fill, array);
            }
            if (this.Outline != null)
            {
                g.DrawPolygon(this.Outline, array);
                //g.DrawClosedCurve(this.Outline, array);
            }
        }
    }
}
