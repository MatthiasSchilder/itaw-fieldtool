using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using SharpMap;
using SharpMap.Rendering.Symbolizer;
using SharpMap.Utilities;
using GeoAPI.Geometries;

namespace fieldtool.SharpmapExt.Symbolizers
{
    public abstract class FtBasePointSymbolizer : BaseSymbolizer, IPointSymbolizer, IFtBaseSymbolizer
    {
        public bool Labeled { get; }

        protected FtBasePointSymbolizer(bool labeled)
        {
            Labeled = labeled;
            this.SmoothingMode = SmoothingMode.HighSpeed;
            Size = new Size(12, 12);
        }

        private float _scale = 1f;

        /// <summary>
        /// Offset of the point from the point
        /// </summary>
        public PointF Offset
        {
            get;
            set;
        }

        /// <summary>
        /// Rotation of the symbol
        /// </summary>
        public float Rotation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Size of the symbol
        /// <para>
        /// Implementations may ignore the setter, the getter must return a <see cref="P:SharpMap.Rendering.Symbolizer.PointSymbolizer.Size" /> with positive width and height values.
        /// </para>
        /// </summary>
        public abstract Size Size
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the scale 
        /// </summary>
        public virtual float Scale
        {
            get
            {
                return this._scale;
            }
            set
            {
                if (value <= 0f)
                {
                    return;
                }
                this._scale = value;
            }
        }

        /// <summary>
        /// Function to render the symbol
        /// </summary>
        /// <param name="map">The map</param>
        /// <param name="point">The point to symbolize</param>
        /// <param name="g">The graphics object</param>
        protected void RenderPoint(Map map, Coordinate point, Graphics g)
        {
            if (point == null)
            {
                return;
            }
            PointF pp = Transform.WorldtoMap(point, map);
            if (this.Rotation != 0f && !float.IsNaN(this.Rotation))
            {
                Matrix startingTransform = g.Transform.Clone();
                Matrix transform = g.Transform;
                PointF rotationCenter = pp;
                transform.RotateAt(this.Rotation, rotationCenter);
                g.Transform = transform;
                this.OnRenderInternal(pp, g);
                g.Transform = startingTransform;
                return;
            }
            this.OnRenderInternal(pp, g);
        }

        /// <summary>
        /// Function that does the actual rendering
        /// </summary>
        /// <param name="pt">The point</param>
        /// <param name="g">The graphics object</param>
        public abstract void OnRenderInternal(PointF pt, Graphics g);

        /// <summary>
        /// Utility function to transform any <see cref="T:SharpMap.Rendering.Symbolizer.IPointSymbolizer" /> into an unscaled <see cref="T:SharpMap.Rendering.Symbolizer.RasterPointSymbolizer" />. This may bring performance benefits.
        /// </summary>
        /// <returns></returns>
        public virtual IPointSymbolizer ToRasterPointSymbolizer()
        {
            Bitmap bitmap = new Bitmap(this.Size.Width, this.Size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Transparent);
                this.OnRenderInternal(new PointF((float)this.Size.Width * 0.5f, (float)this.Size.Height * 0.5f), g);
            }
            return new RasterPointSymbolizer
            {
                Offset = this.Offset,
                Rotation = this.Rotation,
                Scale = this.Scale,
                Symbol = bitmap
            };
        }

        /// <summary>
        /// Function to render the geometry
        /// </summary>
        /// <param name="map">The map object, mainly needed for transformation purposes.</param>
        /// <param name="geometry">The geometry to symbolize.</param>
        /// <param name="graphics">The graphics object to use.</param>
        public void Render(Map map, IPuntal geometry, Graphics graphics)
        {
            IMultiPoint mp = geometry as IMultiPoint;
            if (mp != null)
            {
                Coordinate[] coordinates = mp.Coordinates;
                for (int i = 0; i < coordinates.Length; i++)
                {
                    Coordinate point = coordinates[i];
                    this.RenderPoint(map, point, graphics);
                }
                return;
            }
            this.RenderPoint(map, ((IPoint)geometry).Coordinate, graphics);
        }


        public static Func<Color, FtBasePointSymbolizer> GetRandomPointSymbolizer()
        {
            Random rnd = new Random();
            switch (rnd.Next(0, 4))
            {
                case 0:
                    return color => new FtCrossPointSymbolizer(color);
                case 1:
                    return color => new FtDotPointSymbolizer(color);
                case 2:
                    return color => new FtRectanglePointSymbolizer(color);
                case 3:
                    return color => new FtTriangleePointSymbolizer(color);
                default:
                    return color => new FtCrossPointSymbolizer(color);
            }
        }
    }
}
