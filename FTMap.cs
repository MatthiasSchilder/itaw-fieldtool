using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
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
            //Debug.WriteLine("Entering data changed handler" + DateTime.Now);
            RemoveCustomDecorations();
            
            this.VariableLayers.Clear();
            MapFont = new Font("Arial", Properties.Settings.Default.VisualizerTextsize);
            foreach (var dataset in _project.Datasets)
            {
                if (!dataset.Active)
                    continue;

                VectorLayer symbolizerLayer;
                IProvider dataSource;
                var symbolizer = dataset.Visulization.Symbolizer;
                
                if (symbolizer is IPointSymbolizer)
                {
                    dataSource = dataset.GPSData.AsDataTablePoint();
                    symbolizerLayer = new VectorLayer(dataset.TagId.ToString(), dataSource);

                    symbolizerLayer.Style.PointSymbolizer = (IPointSymbolizer)symbolizer;

                    (symbolizer as IPointSymbolizer).Size = new Size(Properties.Settings.Default.VisualizerMarkersize,
                        Properties.Settings.Default.VisualizerMarkersize);
                }
                else
                {
                    dataSource = dataset.GPSData.AsDataTableLine();
                    symbolizerLayer = new VectorLayer(dataset.TagId.ToString(), dataSource);
                    symbolizerLayer.Style.LineSymbolizer = (FtBasicLineSymbolizer) symbolizer;

                    (symbolizer as FtBasicLineSymbolizer).Size = new Size(Properties.Settings.Default.VisualizerMarkersize,
                        Properties.Settings.Default.VisualizerMarkersize);
                }

                symbolizerLayer.SmoothingMode = SmoothingMode.HighSpeed;
                symbolizer.SmoothingMode = SmoothingMode.HighSpeed;

                LabelLayer labelLayer = null;
                if((symbolizer as IFtBaseSymbolizer).Labeled)
                {
                    labelLayer = new LabelLayer($"Label{dataset.TagId}")
                    {
                        DataSource = dataset.GPSData.AsDataTablePoint(), 
                        LabelColumn = "num",
                        LabelPositionDelegate = LabelPositionDelegate, 
                        SmoothingMode = SmoothingMode.HighSpeed,
                        Style = { Font = MapFont, VerticalAlignment = LabelStyle.VerticalAlignmentEnum.Top, HorizontalAlignment = LabelStyle.HorizontalAlignmentEnum.Left, CollisionDetection = false, ForeColor = dataset.Visulization.VisulizationColor}
                    };
                }
                if (labelLayer != null)
                {
                    this.VariableLayers.Add(labelLayer);
                }
                this.VariableLayers.Add(symbolizerLayer);
            }
            
            AddLegendDecoration(_project.Datasets);

        }

        private static readonly Graphics _stringMeasurementGraphicsDummy = 
            Graphics.FromImage(new Bitmap(1, 1));
        private Coordinate LabelPositionDelegate(FeatureDataRow fdr)
        {
            var screenCoord = this.WorldToImage(fdr.Geometry.Coordinate, false);
            var size = _stringMeasurementGraphicsDummy.MeasureString(((int) fdr["num"]).ToString(), MapFont);

            var lblScreenCoord = new PointF(screenCoord.X - size.Width / 2, screenCoord.Y + 6 + 2);
            return new Coordinate(this.ImageToWorld(lblScreenCoord));
        }

        public void AddPolygonalData(FtTransmitterDataset dataset, FtPolygon polygon)
        {
            var poly = this.Factory.CreatePolygon(polygon.Vertices.ToArray());
            var rnd = new Random();
            var polygonalVectorLayer = new PolygonalVectorLayer(dataset.TagId.ToString() + "MCP", new GeometryProvider(poly))
            {
                Symbolizer = new FtPolygonWithAlphaSymbolizer(Color.FromArgb(0, rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)))
            };

            this.VariableLayers.Add(polygonalVectorLayer);
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
