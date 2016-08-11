using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace fieldtool.SharpmapExt.Symbolizers
{
    [Serializable]
    public class FtRectanglePointSymbolizer : FtBasePointSymbolizer
    {
        public Pen OutlinePen { get; set; }
        public float Angle { get; set; }

        public FtRectanglePointSymbolizer(Color visuColor, bool labeled = false) : base(labeled)
        {
            OutlinePen = new Pen(visuColor);
        }
        public override object Clone()
        {
            var res = (FtRectanglePointSymbolizer)MemberwiseClone();
            res.OutlinePen = OutlinePen;
            res.Angle = Angle;
            return res;
        }

        public override void OnRenderInternal(PointF pt, Graphics g)
        {
            var rect = new RectangleF(new PointF(pt.X - Size.Width / 2f, pt.Y - Size.Height / 2f),
                new SizeF(Size.Width, Size.Height));
            g.DrawRectangle(OutlinePen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public override Size Size
        {
            get; set;
        }
    }
}
