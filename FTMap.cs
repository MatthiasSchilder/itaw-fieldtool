using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;
using SharpMap.Data.Providers;
using SharpMap.Layers.Symbolizer;
using SharpMap.Rendering.Decoration.ScaleBar;
using SharpMap.Rendering.Symbolizer;

namespace fieldtool
{
    public class FtMap : SharpMap.Map
    {
        public FtMap(FtProject project) : base()
        {
            this.BackColor = System.Drawing.Color.White;
            Init(project);
        }

        private  void Init(FtProject project)
        {
            foreach (var rasterLayer in project.MapConfig.RasterLayer)
                if(rasterLayer.Active)
                    this.AddTiffLayer(Path.GetFileNameWithoutExtension(rasterLayer.FilePath), rasterLayer.FilePath);

            foreach (var vektorLayer in project.MapConfig.VektorLayer)
                if (vektorLayer.Active)
                    this.AddShapeLayer(Path.GetFileNameWithoutExtension(vektorLayer.FilePath), vektorLayer.FilePath);

            if(project.MapConfig.ScaleBarDarstellen)
                AddScaleBar();
            //map.AddDecoLayer();
            //map.AddBLALayer();
            this.ZoomToExtents();
        }

        public static void InitWithTestData(FtMap map)
        {
            var relativePath = "data/testdata/GeoData/GeoTiff/";
            var namePathDict = new Dictionary<string, string>()
            {
                { "GeoTiffA", relativePath + "airport.tif" }
                //{ "GeoTiffA", relativePath + "format01-image_a.tif" },
                //{ "GeoTiffB", relativePath + "format01-image_b.tif" },
                //{ "GeoTiffC", relativePath + "format01-image_c.tif" },
                //{ "GeoTiffD", relativePath + "format01-image_d.tif" },

            };

            foreach(var kvp in namePathDict)
                map.AddTiffLayer(kvp.Key, kvp.Value);

            //map.AddShapeLayer("", relativePath + "outline.shp");
            map.AddDecoLayer();
            //map.AddBLALayer();
            map.ZoomToExtents();
        }

        public void AddScaleBar()
        {
            SharpMap.Rendering.Decoration.ScaleBar.ScaleBar scaleBar =
                new SharpMap.Rendering.Decoration.ScaleBar.ScaleBar();
            this.Decorations.Add(scaleBar);   
        }

        public void AddDecoLayer()
        {
            var deco = new MyNonMovingDeco();
            this.Decorations.Add(deco);
        }

        public void AddBLALayer()
        {
            this.Layers.Add(new PuntalVectorLayer("Puntal with CharacterPointSymbolizer",
                    new GeometryProvider(this.Factory.CreatePoint(new Coordinate(0,0))),
                    new CharacterPointSymbolizer
                    {
                        Font = new Font("Arial", 120, FontStyle.Bold, GraphicsUnit.Pixel),
                        CharacterIndex = 65,
                        Foreground = new SolidBrush(Color.Violet),
                        Halo = 2,
                        HaloBrush = new SolidBrush(Color.Silver)
                    }));
        }

        public void AddTiffLayer(string name, string path)
        {
            var layer = new SharpMap.Layers.GdalRasterLayer(name, path);
            this.Layers.Add(layer);
        }

        public void AddShapeLayer(string name, string shapefilePath)
        {
            var shapeLayer = new SharpMap.Layers.VectorLayer("outline", new SharpMap.Data.Providers.ShapeFile(shapefilePath));
            shapeLayer.Style.Fill = System.Drawing.Brushes.Transparent;
            shapeLayer.Style.Outline = System.Drawing.Pens.Black;
            shapeLayer.Style.EnableOutline = true;
            shapeLayer.Style.Enabled = true;
            this.Layers.Add(shapeLayer);
        }




        //public void AddTiffLayer2(float angle)
        //{


        //    //    //System.Drawing.Drawing2D.Matrix mat = new System.Drawing.Drawing2D.Matrix();
        //    //    //mat.RotateAt(angle, map.WorldToImage(map.Center));
        //    //    //map.MapTransform = mat;
        //    //}
        //    //catch (System.TypeInitializationException ex)
        //    //{
        //    //    if (ex.Message == "The type initializer for 'OSGeo.GDAL.GdalPINVOKE' threw an exception.")
        //    //    {
        //    //        throw new System.Exception(
        //    //            string.Format(
        //    //                "The application threw a PINVOKE exception. You probably need to copy the unmanaged dll's to your bin directory. They are a part of fwtools {0}. You can download it from: http://home.gdal.org/fwtools/",
        //    //                SharpMap.Layers.GdalRasterLayer.FWToolsVersion));
        //    //    }
        //    //    throw;
        //    //}

        //}

    }
}
