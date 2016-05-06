using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using fieldtool.Data;
using fieldtool.Decorations;
using fieldtool.Symbolizers;
using GeoAPI.Geometries;
using SharpMap.Data;
using SharpMap.Data.Providers;
using SharpMap.Layers.Symbolizer;
using SharpMap.Rendering.Decoration;
using SharpMap.Rendering.Decoration.ScaleBar;
using SharpMap.Rendering.Symbolizer;

namespace fieldtool
{
    public class FtMap : SharpMap.Map
    {
        private FtProject _project;

        private Dictionary<int, PolygonalVectorLayer> PolygonalVectorLayers;
        private Dictionary<int, PuntalVectorLayer> PuntalVectorLayers;
        private List<MapDecoration> CustomDecorations;

        public FtMap(FtProject project) : base()
        {
            _project = project;
            this.BackColor = System.Drawing.Color.White;
            Init(project);
            project.DataChangedEventHandler += DataChangedEventHandler;
            PolygonalVectorLayers = new Dictionary<int, PolygonalVectorLayer>();
            PuntalVectorLayers = new Dictionary<int, PuntalVectorLayer>();
            CustomDecorations = new List<MapDecoration>();
        }

        private void DataChangedEventHandler(object sender, EventArgs eventArgs)
        {
            RemoveCustomDecorations();
            foreach (var dataset in _project.Datasets)
            {
                if (PuntalVectorLayers.ContainsKey(dataset.TagId))
                {
                    this.VariableLayers.Remove(PuntalVectorLayers[dataset.TagId]);
                    PuntalVectorLayers.Remove(dataset.TagId);
                }

                if (!dataset.Active)
                    continue;

                //var geometryProvider = new GeometryFeatureProvider(GpsDataToCoordinates(dataset.GPSData));


                FeatureDataTable fdt = new FeatureDataTable();
                
                fdt.Columns.Add("Name", typeof (string));
                fdt.Columns.Add("bla", typeof(string));
                fdt.Columns.Add("blub", typeof(string));

                foreach (var bla in GpsDataToCoordinates(dataset.GPSData))
                {
                    //var fdr = fdt.NewRow();
                    //fdr.Geometry = bla;
                    //fdr.ItemArray[1] = "2";
                    //fdr.ItemArray[2] = "3;";
                    
                    //fdt.AddRow(fdr);

                    var fdr = (FeatureDataRow) fdt.Rows.Add("bla", "bluirr", "blirr");
                    fdr.Geometry = bla;
                }
                var geometryProvider = new GeometryFeatureProvider(fdt);

                //var blabla = new GeometryFeatureProvider();
                //geometryProvider.Features = new fe
                //geometryProvider.FilterDelegate = new FilterProvider.FilterMethod()
                //SharpMap.Data.FeatureDataRow fdr; fdr.


                ISymbolizer<IPuntal> symbolizer;
                Random rnd = new Random();
                var rndnum = rnd.Next(0, 2);

                //if(rndnum == 0)
                //    symbolizer = new FtTriangleePointSymbolizer(dataset.VisulizationColor);
                //if (rndnum == 1)
                    symbolizer = new FtTriangleePointSymbolizer(dataset.Visulization.VisulizationColor);
                //else
                //    symbolizer = new FtRectanglePointSymbolizer(dataset.VisulizationColor, 45);
                symbolizer.SmoothingMode = SmoothingMode.HighSpeed;
                var puntalVectorLayers = new PuntalVectorLayer(dataset.TagId.ToString(), geometryProvider, symbolizer);
                
                this.VariableLayers.Add(puntalVectorLayers);
                PuntalVectorLayers.Add(dataset.TagId, puntalVectorLayers);


                //var labelLayer = new SharpMap.Layers.LabelLayer("blabla");
                //labelLayer.
            }
            AddLegendDecoration(_project.Datasets);

        }

        private IEnumerable<IPoint> GpsDataToCoordinates(IEnumerable<FtTransmitterGpsDataSeries> gpsSeries)
        {
            foreach (var gps in gpsSeries)
            {
                if (gps.IsValid())
                {
                    yield return this.Factory.CreatePoint(new Coordinate(gps.Rechtswert.Value, gps.Hochwert.Value));
                }
            }
        }

        public void AddPolygonalData(FtTransmitterDataset dataset, FtPolygon polygon)
        {
            var poly = this.Factory.CreatePolygon(polygon.Vertices.ToArray());
            var polygonalVectorLayer = new PolygonalVectorLayer(dataset.TagId.ToString() + "MCP", new GeometryProvider(poly))
            {
                Symbolizer = new FtPolygonWithAlphaSymbolizer(dataset.Visulization.VisulizationColor)
            };

            this.VariableLayers.Add(polygonalVectorLayer);
            //PolygonalVectorLayers.Add(polygonalVectorLayer);
        }

        private  void Init(FtProject project)
        {
            foreach (var rasterLayer in project.MapConfig.RasterLayer)
                if(rasterLayer.Active)
                    this.AddTiffLayer(Path.GetFileNameWithoutExtension(rasterLayer.FilePath), rasterLayer.FilePath);

            foreach (var vektorLayer in project.MapConfig.VektorLayer)
                if (vektorLayer.Active)
                    this.AddShapeLayer(Path.GetFileNameWithoutExtension(vektorLayer.FilePath), vektorLayer.FilePath);

            if(Properties.Settings.Default.MapScalebarActive)
                AddScaleBar();
            //map.AddDecoLayer();
            //map.AddBLALayer();
            //this.ZoomToExtents();
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

            map.ZoomToExtents();
        }

        public void AddScaleBar()
        {
            SharpMap.Rendering.Decoration.ScaleBar.ScaleBar scaleBar =
                new SharpMap.Rendering.Decoration.ScaleBar.ScaleBar();
            scaleBar.BarColor1 = Color.Black;
            scaleBar.BarColor2 = Color.White;
            this.Decorations.Add(scaleBar);


        }

        public void AddLegendDecoration(List<FtTransmitterDataset> datasets)
        {
            var activeDatasets = datasets.Where(d => d.Active).ToList();
            if (!activeDatasets.Any())
                return;
            LegendDecoration legend = new LegendDecoration(activeDatasets);
            this.Decorations.Add(legend);
            CustomDecorations.Add(legend);
        }

        public void RemoveCustomDecorations()
        {
            foreach (var decoration in CustomDecorations)
            {
                this.Decorations.Remove(decoration);
            }
            CustomDecorations.Clear();
        }

        public void AddTiffLayer(string name, string path)
        {
            var layer = new SharpMap.Layers.GdalRasterLayerCachingProxy(name, path);
            layer.ColorCorrect = false;
            this.BackgroundLayer.Add(layer);
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
    }
}
