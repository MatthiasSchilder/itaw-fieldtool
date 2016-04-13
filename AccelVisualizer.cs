using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool
{
    class AccelVisualizer
    {
        private const int LineHeightPx = 15;
        private const int LineHeightWoMarginPx = 13;

        public Bitmap RenderBurstActivitiesToBitmap(Dictionary<DateTime, double[]> result)
        {
            var dateBitmaps = CreateDateBitmaps(result.Keys.ToList());
            var dateBitmap = MergeDateBitmaps(dateBitmaps);

            var burstBitmap = VisuBursts(result.Values.ToList());

            return MergeDateBurstBitmap(dateBitmap, burstBitmap);
        }

        private Bitmap MergeDateBurstBitmap(Bitmap dateBitmap, Bitmap burstBitmap)
        {
            Bitmap bitmap = new Bitmap(dateBitmap.Width + burstBitmap.Width, dateBitmap.Height);
            int xOffsBurstBitmap = dateBitmap.Width;

            for (int i = 0; i < dateBitmap.Height; i++)
            {
                for (int j = 0; j < dateBitmap.Width; j++)
                {
                    bitmap.SetPixel(j, i, dateBitmap.GetPixel(j, i));
                }
            }

            for (int i = 0; i < burstBitmap.Height; i++)
            {
                for (int j = 0; j < burstBitmap.Width; j++)
                {
                    bitmap.SetPixel(j + xOffsBurstBitmap, i, burstBitmap.GetPixel(j, i));
                }
            }

            return bitmap;
        }

        private List<Bitmap> CreateDateBitmaps(List<DateTime> dates)
        {
            List<Bitmap> images = new List<Bitmap>();
            Font font = new Font(FontFamily.GenericMonospace, 8, FontStyle.Regular);
            foreach (var date in dates)
            {
                images.Add(DrawText(date.ToString("d"), font, Color.Black));
            }
            return images;
        }


        private int pxPerLine;
        private int pxPerCol;
        private double maxValue;
        private List<double> _maxValues;

        private Bitmap VisuBursts(List<double[]> arrays)
        {
            pxPerCol = 1;
            _maxValues = new List<double>();
            
            foreach (var valArray in arrays)
            {
                maxValue = double.MinValue;
                foreach (var value in valArray)
                {
                    maxValue = Math.Max(maxValue, value);
                }
                _maxValues.Add(maxValue);
            }
            Bitmap bmp = new Bitmap(240 * pxPerCol, arrays.Count * 15);
            
            int i = 0;
            foreach (var arr in arrays)
            {
                WriteBurstToBitmap(arr, bmp, i, _maxValues[i++]);
            }
            return bmp;
        }

        private void WriteBurstToBitmap(double[] data, Bitmap bmp, int i, double bezugsWert)
        {
            var pxLineOffs = i * 15;

            int xoffs = 0;
            foreach (var val in data)
            {
                var color = GetColor(bezugsWert, val);

                for (int l = 0; l < pxPerCol; l++)
                {
                    for (int m = 1; m <= 13; m++)
                    {
                        bmp.SetPixel(xoffs + l, pxLineOffs + m, color);
                    }
                }

                xoffs += pxPerCol;
            }
        }

        private Color GetColor(double bezugswert, double value)
        {
            if (value.Equals(double.MinValue))
                return Color.LightGoldenrodYellow;
            int val = (int)((value / bezugswert) * 192);
            return Color.FromArgb((192 - val), (192 - val), (192 - val));

        }

        private Bitmap MergeDateBitmaps(List<Bitmap> images)
        {
            int height = images.Count * LineHeightPx;

            int width = images[0].Width;

            Bitmap img = new Bitmap(width, height);
            

            int k = 0;
            foreach (var image in images)
            {
                var lineIdxOffs = LineHeightPx*k++;
                for (int i = 0; i <= LineHeightWoMarginPx; i++)
                {
                    for (int j = 0; j < image.Width; j++)
                    {
                        var color = image.GetPixel(j, i);

                        img.SetPixel(j, i + lineIdxOffs + 1, color);
                    }
                }
            }

            return img;
        }

        private Bitmap DrawText(String text, Font font, Color textColor)
        {
            //first, create a dummy bitmap just to get a graphics object
            Bitmap img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int) LineHeightPx);

            drawing = Graphics.FromImage(img);

            drawing.Clear(Color.White);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, (LineHeightPx - (int) textSize.Height)/2);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;

        }
    }
}
