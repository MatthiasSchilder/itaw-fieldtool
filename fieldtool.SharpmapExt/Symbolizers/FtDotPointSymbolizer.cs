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

        public FtDotPointSymbolizer(Color visuColor, bool labeled = false) : base(labeled)
        {
            OutlinePen = new Pen(new SolidBrush(visuColor));
            FillBrush = new SolidBrush(ControlPaint.Light(visuColor, 0.2f));
        }
        public override object Clone()
        {
            var res = (FtDotPointSymbolizer)MemberwiseClone();
            res.OutlinePen = OutlinePen;
            res.FillBrush = FillBrush;
            return res;
        }

        public override void OnRenderInternal(PointF pt, Graphics g)
        {
            var rect = new RectangleF(new PointF(pt.X - Size.Width/2f, pt.Y - Size.Height/2f),
                new SizeF(Size.Width, Size.Height));


            g.DrawEllipse(OutlinePen, rect);
            g.FillEllipse(FillBrush, rect);
        }

        public override Size Size
        {
            get; set;
        }
    }
}
