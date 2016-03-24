using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMap.Rendering.Symbolizer;

namespace SharpMap.Rendering.Symbolizer
{
    [Serializable]
    public class NumericPointSymbolizer : PointSymbolizer
    {
        private Font Font;
        private int StartCnt;
        private Brush Foreground;
        private StringFormat StringFormat;

        public NumericPointSymbolizer() : base()
        {
            Font = new Font("Arial", 12);
            StartCnt = 1;
            Foreground = Brushes.Firebrick;

            StringFormat = new StringFormat(StringFormatFlags.NoClip) { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.None };
        }
        public override object Clone()
        {
            var res = (NumericPointSymbolizer)MemberwiseClone();
            res.Font = (Font)Font.Clone();
            res.Foreground = (Brush)Foreground.Clone();

            return res;
        }

        public override void OnRenderInternal(PointF pt, Graphics g)
        {
            g.DrawString(StartCnt++.ToString(), Font, Foreground, pt, StringFormat);
        }

        public override Size Size { get; set; }
    }
}
