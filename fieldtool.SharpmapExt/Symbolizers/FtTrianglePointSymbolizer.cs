using System;
using System.Drawing;

namespace fieldtool.SharpmapExt.Symbolizers
{
    [Serializable]
    public class FtTriangleePointSymbolizer : FtBasePointSymbolizer
    {
        public Pen OutlinePen { get; set; }

        public FtTriangleePointSymbolizer(Color visuColor, bool labeled = false) : base(labeled)
        {
            OutlinePen = new Pen(visuColor, 1);
        }
        public override object Clone()
        {
            var res = (FtRectanglePointSymbolizer)MemberwiseClone();
            res.OutlinePen = OutlinePen;
            return res;
        }

        public override void OnRenderInternal(PointF pt, Graphics g)
        {
            PointF[] points = new PointF[3];
            
            points[0] = new PointF(pt.X - Size.Width / 2, pt.Y - Size.Height / 2);
            points[1] = new PointF(pt.X + Size.Width / 2, pt.Y - Size.Height / 2);
            points[2] = new PointF(pt.X, pt.Y + Size.Height / 2);
            g.DrawPolygon(OutlinePen, points);
        }

        public override Size Size
        {
            get; set;
        }
    }
}

