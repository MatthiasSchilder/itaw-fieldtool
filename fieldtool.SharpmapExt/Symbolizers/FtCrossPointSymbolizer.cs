using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace fieldtool.SharpmapExt.Symbolizers
{
    [Serializable]
    public class FtCrossPointSymbolizer : FtBasePointSymbolizer
    {
        public Pen OutlinePen { get; set; }

        public FtCrossPointSymbolizer(Color visuColor) : base()
        {
            OutlinePen = new Pen(visuColor);
            Size = new Size(8, 8);
        }
        public override object Clone()
        {
            var res = (FtCrossPointSymbolizer)MemberwiseClone();
            res.OutlinePen = OutlinePen;
            return res;
        }

        public override void OnRenderInternal(PointF pt, Graphics g)
        {
            using (Matrix m = new Matrix())
            {
                m.RotateAt(45, pt);
                g.Transform = m;
                g.DrawLine(OutlinePen, new PointF(pt.X - Size.Width / 2, pt.Y), new PointF(pt.X + Size.Width / 2, pt.Y));
                g.DrawLine(OutlinePen, new PointF(pt.X, pt.Y - Size.Height / 2), new PointF(pt.X, pt.Y + Size.Height / 2));
                g.ResetTransform();
            }

        }

        public override Size Size
        {
            get; set;
        }
    }
}
