using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace fieldtool.SharpmapExt.Symbolizers
{
    [Serializable]
    public class FtDotPointSymbolizer : FtBasePointSymbolizer
    {
        public Pen OutlinePen { get; set; }
        public Brush FillBrush { get; set; }

        public FtDotPointSymbolizer(Color visuColor) : base()
        {
            OutlinePen = new Pen(new SolidBrush(visuColor));
            FillBrush = new SolidBrush(ControlPaint.Light(visuColor, 0.2f));
        }
        public override object Clone()
        {
            var res = (FtDotPointSymbolizer)MemberwiseClone();
            res.OutlinePen = OutlinePen;
            return res;
        }

        public override void OnRenderInternal(PointF pt, Graphics g)
        {
            using (Matrix m = new Matrix())
            {
                m.RotateAt(45, pt);
                g.Transform = m;
                g.DrawEllipse(OutlinePen, new RectangleF(new PointF(pt.X, pt.Y), new SizeF(Size.Width, Size.Height)));
                g.FillEllipse(FillBrush, new RectangleF(new PointF(pt.X, pt.Y), new SizeF(Size.Width, Size.Height)));
                g.ResetTransform();
            }

        }

        public override Size Size
        {
            get; set;
        }
    }
}
