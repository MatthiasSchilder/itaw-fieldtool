using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using fieldtool.Data;
using fieldtool.Data.Movebank;
using fieldtool.Decorations;
using fieldtool.SharpmapExt.Symbolizers;
using GeoAPI.Geometries;
using SharpMap.Data;
using SharpMap.Data.Providers;
using SharpMap.Layers;
using SharpMap.Layers.Symbolizer;
using SharpMap.Rendering.Decoration;
using SharpMap.Rendering.Decoration.ScaleBar;
using SharpMap.Rendering.Symbolizer;
using SharpMap.Styles;

namespace fieldtool
{
    public class FtMap : SharpMap.Map
    {
        private FtProject _project;

        private Dictionary<int, PolygonalVectorLayer> PolygonalVectorLayers;
        //private Dictionary<int, Layer> Layers;
        private List<MapDecoration> CustomDecorations;

        public FtMap(FtProject project) : base()
        {
            _project = project;
            this.BackColor = System.Drawing.Color.White;
            Init(project);
            project.DataChangedEventHandler += DataChangedEventHandler;
            PolygonalVectorLayers = new Dictionary<int, PolygonalVectorLayer>();
            //Layers = new Dictionary<int, Layer>();
            CustomDecorations = new List<MapDecoration>();
        }

        private Font MapFont;
        private void DataChangedEventHandler(object sender, EventArgs eventArgs)
        {
            Debug.WriteLine("Entering data changed handler" + DateTime.Now);
            RemoveCustomDecorations();
            
            this.VariableLayers.Clear();
            MapFont = new Font("Arial", Properties.Settings.Default.VisualizerTextsize);
            foreach (var dataset in _project.Datasets)
            {
                if (!dataset.Active)
                    continue;

                var dtPoint = dataset.GPSData.AsDataTablePoint();

                var symbolizerLayer = new PuntalVectorLayer(dataset.TagId.ToString(), dtPoint);
                var symbolizer = dataset.Visulization.Symbolizer;
                symbolizer.SmoothingMode = SmoothingMode.HighSpeed;
                symbolizerLayer.Symbolizer = symbolizer;
                

                LabelLayer labelLayer = null;
                if (symbolizer is FtBasePointSymbolizer && (symbolizer as FtBasePointSymbolizer).Labeled)
                {
                    labelLayer = new LabelLayer($"Label{dataset.TagId}")
                    {
                        DataSource = dtPoint,

                        LabelColumn = "num",
                        LabelPositionDelegate = LabelPositionDelegate, 
                        SmoothingMode = SmoothingMode.HighSpeed,
                        Style = { Font = MapFont, VerticalAlignment = LabelStyle.VerticalAlignmentEnum.Top, HorizontalAlignment = LabelStyle.HorizontalAlignmentEnum.Left, CollisionDetection = false, ForeColor = dataset.Visulization.VisulizationColor}
                        
                    };

                symbolizerLayer.LabelLayer = labelLayer;
                }
                if (labelLayer != null)
                {
                    this.VariableLayers.Add(labelLayer);
                }
                this.VariableLayers.Add(symbolizerLayer);


            }
            AddLegendDecoration(_project.Datasets);

        }

        private static Image i = new Bitmap(1,1);
        private static Graphics g = Graphics.FromImage(i);

        private Coordinate LabelPositionDelegate(FeatureDataRow fdr)
        {
            var screenCoord = this.WorldToImage(fdr.Geometry.Coordinate, false);
           
            //var size = TextRenderer.MeasureText(((int)fdr["num"]).ToString(), MapFont,);

            var size = g.MeasureString(((int) fdr["num"]).ToString(), MapFont);

            var lblScreenCoord = new PointF(screenCoord.X - size.Width / 2, screenCoord.Y + 6 + 2);
            return new Coordinate(this.ImageToWorld(lblScreenCoord));
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
