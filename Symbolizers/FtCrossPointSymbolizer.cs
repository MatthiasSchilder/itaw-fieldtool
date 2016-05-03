using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMap.Rendering.Symbolizer;

namespace fieldtool.Symbolizers
{
    [Serializable]
    public class FtCrossPointSymbolizer : FtBasePointSymbolizer
    {
        public Pen OutlinePen { get; set; }

        public FtCrossPointSymbolizer(Color visuColor) : base()
        {
            OutlinePen = new Pen(visuColor);
            Size = new Size(16, 16);
        }
        public override object Clone()
        {
            var res = (FtRectanglePointSymbolizer)MemberwiseClone();
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
