using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace fieldtool.SharpmapExt.Symbolizers
{
    [Serializable]
    public class FtArrowPointSymbolizer : FtBasePointSymbolizer
    {
        public Pen OutlinePen { get; set; }

        public FtArrowPointSymbolizer(Color visuColor, bool labeled = false) : base(labeled)
        {
            OutlinePen = new Pen(visuColor);
            Size = new Size(8, 8);
        }
        public override object Clone()
        {
            var res = (FtArrowPointSymbolizer)MemberwiseClone();
            return res;
        }

        public override void OnRenderInternal(PointF pt, Graphics g)
        {
            var bottomPoint = new PointF(pt.X, pt.Y - Size.Height / 2f);
            var topPoint = new PointF(pt.X, pt.Y + Size.Height / 2f);

            var leftPoint = new PointF(pt.X - Size.Width / 2f, pt.Y);
            var rightPoint = new PointF(pt.X + Size.Width / 2f, pt.Y);


            g.DrawLine(OutlinePen, bottomPoint, topPoint);
            g.DrawLine(OutlinePen, topPoint, leftPoint);
            g.DrawLine(OutlinePen, topPoint, rightPoint);
        }

        public override Size Size
        {
            get; set;
        }
    }
}
