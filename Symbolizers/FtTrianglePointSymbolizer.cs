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
    public class FtTriangleePointSymbolizer : FtBasePointSymbolizer
    {
        public Pen OutlinePen { get; set; }

        public FtTriangleePointSymbolizer(Color visuColor) : base()
        {
            OutlinePen = new Pen(visuColor, 2);
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
            PointF[] points = new PointF[3];
            
            points[0] = new PointF(pt.X - Size.Width / 2, pt.Y - Size.Height / 2);
            points[1] = new PointF(pt.X + Size.Width / 2, pt.Y - Size.Height / 2);
            points[2] = new PointF(pt.X, pt.Y + Size.Height / 2);
            g.DrawPolygon(OutlinePen, points);
            //g.DrawRectangle(OutlinePen, pt.X, pt.Y, Size.Width, Size.Height);
        }

        public override Size Size
        {
            get; set;
        }
    }
}

