using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMap;
using SharpMap.Rendering.Decoration;
using SharpMap.Utilities;

namespace SharpmapGDAL
{
    class MyNonMovingDeco : SharpMap.Rendering.Decoration.MapDecoration
    {
        /// <summary>
        /// Creates an instance of this class
        /// </summary>
        public MyNonMovingDeco()
        {
            Size = new Size(45, 45);
            ForeColor = Color.Silver;
            Location = new Point(5, 5);
            Anchor = MapDecorationAnchor.Center;
            MyMapDecoImage = Image.FromFile(@"./data/images/mynonmovingdeco.png");
        }

        /// <summary>
        /// Gets or sets the NorthArrowImage
        /// </summary>
        public Image MyMapDecoImage { get; set; }

        /// <summary>
        /// Gets or sets the size of the North arrow Bitmap
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// Gets or sets the fore color
        /// </summary>
        public Color ForeColor { get; set; }


        #region MapDecoration overrides

        protected override Size InternalSize(Graphics g, Map map)
        {
            return Size;
        }

        protected override void OnRender(Graphics g, Map map)
        {
            var image = MyMapDecoImage;

            var mapSize = map.Size;

            //Get rotation
            var ptTop = map.ImageToWorld(new PointF(mapSize.Width / 2f, 0f), true);
            var ptBottom = map.ImageToWorld(new PointF(mapSize.Width / 2f, mapSize.Height * 0.5f), true);

            var dx = ptTop.X - ptBottom.X;
            var dy = ptBottom.Y - ptTop.Y;
            var length = Math.Sqrt(dx * dx + dy * dy);

            var cos = dx / length;

            var rot = -90 + (dy > 0 ? -1 : 1) * Math.Acos(cos) / GeoSpatialMath.DegToRad;
            var halfSize = new Size((int)(0.5f * Size.Width), (int)(0.5f * Size.Height));
            var oldTransform = g.Transform;

            var clip = g.ClipBounds;
            var newTransform = new Matrix(1f, 0f, 0f, 1f,
                                          clip.Left + halfSize.Width,
                                          clip.Top + halfSize.Height);
            newTransform.Rotate((float)rot);

            // Setup image attributes
            var ia = new ImageAttributes();
            var cmap = new[] {
                new ColorMap { OldColor = Color.Transparent, NewColor = OpacityColor(BackgroundColor) },
                new ColorMap { OldColor = Color.Black, NewColor = OpacityColor(ForeColor) }
            };
            ia.SetRemapTable(cmap);

            g.Transform = newTransform;

            var rect = new Rectangle(-halfSize.Width, -halfSize.Height, Size.Width, Size.Height);
            g.DrawImage(image, rect, 0, 0, image.Size.Width, image.Size.Height, GraphicsUnit.Pixel, ia);

            g.Transform = oldTransform;
        }

        #endregion

    }
}
