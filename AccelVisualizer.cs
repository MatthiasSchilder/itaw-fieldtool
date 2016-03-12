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
        public Bitmap RenderBurstActivitiesToBitmap(Dictionary<DateTime, double[]> result)
        {
            var dateBitmaps = CreateDateBitmaps(result.Keys.ToList());
            var dateBitmap = MergeDateBitmaps(dateBitmaps, out pxPerLine);

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
            Font font = new Font(FontFamily.GenericMonospace, 7, FontStyle.Regular);
            foreach (var date in dates)
            {
                images.Add(DrawText(date.ToString("d"), font, Color.Black));
            }
            return images;
        }


        private int pxPerLine;
        private int pxPerCol;
        private double maxValue;

        private Bitmap VisuBursts(List<double[]> arrays)
        {
            pxPerCol = 3;

            maxValue = double.MinValue;
            foreach (var valArray in arrays)
            {
                foreach (var value in valArray)
                {
                    maxValue = Math.Max(maxValue, value);
                }
            }
            Bitmap bmp = new Bitmap(240 * pxPerCol, arrays.Count * pxPerLine);
            int i = 0;
            foreach (var arr in arrays)
            {
                WriteBurstToBitmap(arr, bmp, i++);
            }
            return bmp;
        }

        private void WriteBurstToBitmap(double[] data, Bitmap bmp, int i)
        {
            var pxLineOffs = i * pxPerLine;

            int xoffs = 0;
            foreach (var val in data)
            {
                var color = GetColor(maxValue, val);

                for (int l = 0; l < pxPerCol; l++)
                {
                    for (int m = 0; m < pxPerLine; m++)
                    {
                        bmp.SetPixel(xoffs + l, pxLineOffs + m, color);
                    }
                }

                xoffs += pxPerCol;
            }
        }

        private Color GetColor(double maxValue, double value)
        {
            if (value.Equals(double.MinValue))
                return Color.DarkSalmon;
            int val = (int)((value / maxValue) * 255);
            return Color.FromArgb(255 - val, 255 - val, 255 - val);

        }

        private Bitmap MergeDateBitmaps(List<Bitmap> images, out int pxPerLine)
        {
            int height = 0;
            foreach (var image in images)
                height += image.Height;

            int heightPerDay = height/images.Count;
            pxPerLine = heightPerDay;

            int width = images[0].Width;

            Bitmap img = new Bitmap(width, height);
            

            int k = 0;
            foreach (var image in images)
            {
                var lineIdxOffs = heightPerDay*k++;
                for (int i = 0; i < image.Height; i++)
                {
                    for (int j = 0; j < image.Width; j++)
                    {
                        var color = image.GetPixel(j, i);

                        img.SetPixel(j, i + lineIdxOffs, color);
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
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            drawing.Clear(Color.White);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;

        }
    }
}
