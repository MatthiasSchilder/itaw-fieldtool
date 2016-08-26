using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using SharpMap.Rendering.Symbolizer;

namespace fieldtool.SharpmapExt.Symbolizers
{
    [Serializable]
    public class FtBasicLineSymbolizer : FtBaseSymbolizer
    {
        public Pen OutlinePen { get; set; }

        private BasicLineSymbolizer InternalSymbolizer;

        public FtBasicLineSymbolizer(Color visuColor, bool labeled = false)
        {
            InternalSymbolizer = new BasicLineSymbolizer();
        }
        public override object Clone()
        {
            throw new NotImplementedException();
            //var res = (FtCrossPointSymbolizer)MemberwiseClone();
            //return null;
        }

        public override void OnRenderInternal(PointF pt, Graphics g)
        {
            //g.DrawRectangle();


            g.DrawLine(OutlinePen, new PointF(pt.X - Size.Width / 2f, pt.Y), new PointF(pt.X + Size.Width / 2f, pt.Y));
            g.DrawLine(OutlinePen, new PointF(pt.X, pt.Y - Size.Height / 2f), new PointF(pt.X, pt.Y + Size.Height / 2f));


        }
    }
}
