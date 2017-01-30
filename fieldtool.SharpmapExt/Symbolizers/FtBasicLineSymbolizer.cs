using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SharpMap.Rendering.Symbolizer;
using SharpMap;
using GeoAPI.Geometries;
using SharpMap.Layers;
using System.Numerics;
using SharpMap.Utilities;

namespace fieldtool.SharpmapExt.Symbolizers
{
    [Serializable]
    public class FtBasicLineSymbolizer : BasicLineSymbolizer, IFtBaseSymbolizer
    {
        public Pen OutlinePen { get; set; }
        public Brush FillBrush { get; set; }
        public bool Labeled { get; }

        public Size Size
        {
            get; set;
        }

        private PointF[] ArrowCoords = new[] {new PointF(0, -6), new PointF(9, -24), new PointF(-9, -24)};

        public FtBasicLineSymbolizer(Color visuColor, Size size, bool labeled = false)
        {
            OutlinePen = new Pen(new SolidBrush(visuColor));
            FillBrush = new SolidBrush(ControlPaint.LightLight(visuColor));
            Labeled = labeled;
            Size = size;
        }
        public override object Clone()
        {
            return new FtBasicLineSymbolizer(OutlinePen.Color, Size);
        }

        protected override void OnRenderInternal(Map map, ILineString lineString, Graphics graphics)
        {
            var points = lineString.TransformToImage(map);

            for (int i = 1; i < points.Length; i++)
            {
                var start = points[i - 1];
                var end = points[i];

                graphics.DrawLine(OutlinePen, start, end);

                var rect = new RectangleF(new PointF(start.X - Size.Width / 2f, start.Y - Size.Height / 2f),
                    new SizeF(12, 12));

                graphics.DrawEllipse(OutlinePen, rect);
                graphics.FillEllipse(FillBrush, rect);

                var rect2 = new RectangleF(new PointF(end.X - Size.Width / 2f, end.Y - Size.Height / 2f),
                    new SizeF(12, 12));

                graphics.DrawEllipse(OutlinePen, rect2);
                graphics.FillEllipse(FillBrush, rect2);

                Vector3 vStart = new Vector3(start.X, start.Y, 0);
                Vector3 vEnd = new Vector3(end.X, end.Y, 0);

                var vDiff = vStart - vEnd ;

                var vLineNorm = Vector3.Normalize(vDiff);

                var angleY_ = Math.Atan2(Vector3.UnitY.Y, Vector3.UnitY.X) - Math.Atan2(vLineNorm.Y, vLineNorm.X);
                //angle of 2 relative to 1 = atan2(v2.y, v2.x) - atan2(v1.y, v1.x)

                var matrix = Matrix4x4.CreateRotationZ((float)-angleY_);
                matrix.Translation = vStart - Vector3.Zero;

                PointF[] transformedPoints = new PointF[3];
                int j = 0;
                foreach (var item in ArrowCoords)
                {
                    var v = new Vector3(item.X, item.Y, 0);
                    var res = Vector3.Transform(v, matrix);
                    transformedPoints[j++] = new PointF(res.X, res.Y);
                }

                graphics.DrawPolygon(OutlinePen, transformedPoints);
                graphics.FillPolygon(FillBrush, transformedPoints);
            }
        }
    }
}
