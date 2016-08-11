using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace fieldtool.SharpmapExt.Symbolizers
{
    [Serializable]
    public class FtCrossPointSymbolizer : FtBasePointSymbolizer
    {
        public Pen OutlinePen { get; set; }

        public FtCrossPointSymbolizer(Color visuColor, bool labeled = false) : base(labeled)
        {
            OutlinePen = new Pen(visuColor);
        }
        public override object Clone()
        {
            var res = (FtCrossPointSymbolizer)MemberwiseClone();
            return res;
        }

        public override void OnRenderInternal(PointF pt, Graphics g)
        {
            //g.DrawRectangle();


            g.DrawLine(OutlinePen, new PointF(pt.X - Size.Width / 2f, pt.Y), new PointF(pt.X + Size.Width / 2f, pt.Y));
            g.DrawLine(OutlinePen, new PointF(pt.X, pt.Y - Size.Height / 2f), new PointF(pt.X, pt.Y + Size.Height / 2f));


        }

        public override Size Size
        {
            get; set;
        }
    }
}
