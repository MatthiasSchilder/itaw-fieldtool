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

        public FtRectanglePointSymbolizer(Color visuColor) : base()
        {
            OutlinePen = new Pen(visuColor);
            Size = new Size(8, 8);
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
            using (Matrix m = new Matrix())
            {
                m.RotateAt(Angle, pt);
                g.Transform = m;
                g.DrawRectangle(OutlinePen, pt.X, pt.Y, Size.Width, Size.Height);
                g.ResetTransform();
            }
        }

        public override Size Size
        {
            get; set;
        }
    }
}
