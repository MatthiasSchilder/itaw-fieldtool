﻿namespace SharpmapGDAL
{
    public static class PMap
    {
        private static int _num = 0;


        public static SharpMap.Map InitializeMap(float angle)
        {
            return InitializeGeoTiff(angle);
        }

        private static SharpMap.Map InitializeGeoTiff(float angle)
        {
            try
            {
                var map = new SharpMap.Map();
                map.BackColor = System.Drawing.Color.White;
                const string relativePath = "GeoData/GeoTiff/";

                SharpMap.Layers.GdalRasterLayer layer;

                if (!System.IO.File.Exists(relativePath + "format01-image_a.tif"))
                {
                    throw new System.Exception("Make sure the data is in the relative directory: " + relativePath);
                }

                layer = new SharpMap.Layers.GdalRasterLayer("GeoTiffA", relativePath + "format01-image_a.tif");
                map.Layers.Add(layer);
                layer = new SharpMap.Layers.GdalRasterLayer("GeoTiffB", relativePath + "format01-image_b.tif");
                map.Layers.Add(layer);
                layer = new SharpMap.Layers.GdalRasterLayer("GeoTiffC", relativePath + "format01-image_c.tif");
                map.Layers.Add(layer);
                layer = new SharpMap.Layers.GdalRasterLayer("GeoTiffD", relativePath + "format01-image_d.tif");
                map.Layers.Add(layer);

                SharpMap.Layers.VectorLayer shapeLayer;

                if (!System.IO.File.Exists(relativePath + "outline.shp"))
                {
                    throw new System.Exception("Make sure the data is in the relative directory: " + relativePath);
                }

                shapeLayer = new SharpMap.Layers.VectorLayer("outline", new SharpMap.Data.Providers.ShapeFile(relativePath + "outline.shp"));
                shapeLayer.Style.Fill = System.Drawing.Brushes.Transparent;
                shapeLayer.Style.Outline = System.Drawing.Pens.Black;
                shapeLayer.Style.EnableOutline = true;
                shapeLayer.Style.Enabled = true;
                map.Layers.Add(shapeLayer);

                map.ZoomToExtents();

                //System.Drawing.Drawing2D.Matrix mat = new System.Drawing.Drawing2D.Matrix();
                //mat.RotateAt(angle, map.WorldToImage(map.Center));
                //map.MapTransform = mat;

                return map;
            }
            catch (System.TypeInitializationException ex)
            {
                if (ex.Message == "The type initializer for 'OSGeo.GDAL.GdalPINVOKE' threw an exception.")
                {
                    throw new System.Exception(
                        string.Format(
                            "The application threw a PINVOKE exception. You probably need to copy the unmanaged dll's to your bin directory. They are a part of fwtools {0}. You can download it from: http://home.gdal.org/fwtools/",
                            SharpMap.Layers.GdalRasterLayer.FWToolsVersion));
                }
                throw;
            }

        }

        //private static readonly string[] Vrts = new[] { @"..\DEM\Golden_CO.dem", "contours_sample_polyline_play_polyline.asc", "contours_sample_polyline_play1_polyline.vrt", "contours_sample_polyline_play2_polyline.vrt", "contours_sample_polyline_play3_polyline.vrt", "contours_sample_polyline_play3_polyline.vrt" };
        //private const string RelativePath = "GeoData/VRT/";
        //private static SharpMap.Map InitializeVRT(ref int index, float angle)
        //{
        //    SharpMap.Map map = new SharpMap.Map();
        //    int ind = index - 6;
        //    if (ind >= Vrts.Length) ind = 0;

        //    if (!System.IO.File.Exists(RelativePath + Vrts[ind]))
        //    {
        //        throw new System.Exception("Make sure the data is in the relative directory: " + RelativePath);
        //    }

        //    SharpMap.Layers.GdalRasterLayer layer = new SharpMap.Layers.GdalRasterLayer("VirtualRasterTable", RelativePath + Vrts[ind]);

        //    var ext = System.IO.Path.GetExtension(layer.Filename);
        //    map.Layers.Add(layer);
        //    _gdalSampleDataset = string.Format("'{0}'", ext != null ? ext.ToUpper() : string.Empty);
        //    map.ZoomToExtents();

        //    System.Drawing.Drawing2D.Matrix mat = new System.Drawing.Drawing2D.Matrix();
        //    mat.RotateAt(angle, map.WorldToImage(map.Center));
        //    map.MapTransform = mat;
        //    //index++;
        //    return map;
        //}

        //public static SharpMap.Map InitializeMap(int angle, string[] filenames)
        //{
        //    if (filenames == null || filenames.Length == 0) return null;

        //    var map = new SharpMap.Map();
        //    for (int i = 0; i < filenames.Length; i++)
        //        map.Layers.Add(new SharpMap.Layers.GdalRasterLayer(System.IO.Path.GetFileName(filenames[i]), filenames[i]));

        //    var mat = new System.Drawing.Drawing2D.Matrix();
        //    mat.RotateAt(angle, map.WorldToImage(map.Center));
        //    map.MapTransform = mat;
        //    map.ZoomToExtents();
        //    return map;
        //}
    }

}