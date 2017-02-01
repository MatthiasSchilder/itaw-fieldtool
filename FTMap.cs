using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using fieldtool.Data;
using fieldtool.Data.Movebank;
using fieldtool.Decorations;
using fieldtool.Layers;
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
        private Font MapFont;

        private List<MapDecoration> CustomDecorations;

        private Dictionary<FtTransmitterDataset, FtPuntualVectorLayer> ActiveVectorLayers;

        private PuntualLegendDecoration PuntualLegendDecoration;

        #region Init
        public FtMap(FtProject project) : base()
        {
            _project = project;
            this.BackColor = System.Drawing.Color.White;
            Init(project);
            project.DataChangedEventHandler += DataChangedEventHandler;
            CustomDecorations = new List<MapDecoration>();
            ActiveVectorLayers = new Dictionary<FtTransmitterDataset, FtPuntualVectorLayer>();

            PuntualLegendDecoration = new PuntualLegendDecoration();
        }
        private void Init(FtProject project)
        {
            foreach (var rasterLayer in project.MapConfig.RasterLayer)
                if (rasterLayer.Active)
                    this.AddTiffLayer(Path.GetFileNameWithoutExtension(rasterLayer.FilePath), rasterLayer.FilePath);

            foreach (var vektorLayer in project.MapConfig.VektorLayer)
                if (vektorLayer.Active)
                    this.AddShapeLayer(Path.GetFileNameWithoutExtension(vektorLayer.FilePath), vektorLayer.FilePath);

            if (Properties.Settings.Default.MapScalebarActive)
                AddScaleBar();
        }
        #endregion Init

        private void DataChangedEventHandler(object sender, DataChangedEventArgs eventArgs)
        {
            var dataset = eventArgs.Dataset;
            if (!dataset.Active)
            {
                if(ActiveVectorLayers.ContainsKey(dataset))
                    ErasePuntualFeature(dataset);
            }
            else // dataset.Active
            {
                if (ActiveVectorLayers.ContainsKey(dataset))
                {
                    ErasePuntualFeature(dataset);
                    DrawPuntualFeature(dataset);
                }
                else
                {
                    DrawPuntualFeature(dataset);
                }
            }
           
            UpdatePuntualLegends(ActiveVectorLayers.Keys.ToList());
            //AddPolygonalLegendDecoration(drawnPolygonalFeatures);
        }

        private void DrawPuntualFeature(FtTransmitterDataset dataset)
        {
            var puntualLayer = new FtPuntualVectorLayer(this, dataset);

            ActiveVectorLayers.Add(dataset, puntualLayer);
            VariableLayers.AddCollection(puntualLayer);
        }

        private void ErasePuntualFeature(FtTransmitterDataset dataset)
        {
            var puntualVectorLayer = ActiveVectorLayers[dataset];

            ActiveVectorLayers.Remove(dataset);

            // Labellayer kann null sein, wenn für das Feature keine Labels angezeigt werden sollen
            if(puntualVectorLayer.LabelLayer != null)
                VariableLayers.Remove(puntualVectorLayer.LabelLayer);
            VariableLayers.Remove(puntualVectorLayer.SymbolizerLayer);
        }

        private void DrawPolygonalFeature(FtTransmitterDataset dataset)
        {
            //var poly = this.Factory.CreatePolygon(polygon.Vertices.ToArray());
            //var rnd = new Random();
            //var polygonalVectorLayer = new PolygonalVectorLayer(dataset.TagId.ToString() + "MCP", new GeometryProvider(poly))
            //{
            //    Symbolizer = new FtPolygonWithAlphaSymbolizer(Color.FromArgb(0, rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)))
            //};

            //this.VariableLayers.Add(polygonalVectorLayer);
        }

        private void AddScaleBar()
        {
            SharpMap.Rendering.Decoration.ScaleBar.ScaleBar scaleBar =
                new SharpMap.Rendering.Decoration.ScaleBar.ScaleBar();
            scaleBar.BarColor1 = Color.Black;
            scaleBar.BarColor2 = Color.White;
            this.Decorations.Add(scaleBar);
        }

        private void UpdatePuntualLegends(List<FtTransmitterDataset> datasets)
        {
            if (!datasets.Any())
            {
                if (!this.Decorations.Contains(PuntualLegendDecoration))
                {
                    return;
                }
                this.Decorations.Remove(PuntualLegendDecoration);
                return;
            }

            PuntualLegendDecoration.Update(datasets);
            // wenn die Legende schon drin ist, einmal entfernen, um Redraw mit neuen Items zu erzwingen
            if (this.Decorations.Contains(PuntualLegendDecoration))
                this.Decorations.Remove(PuntualLegendDecoration);
            this.Decorations.Add(PuntualLegendDecoration);
        }

        private void AddPolygonalLegendDecoration(List<FtTransmitterDataset> datasets)
        {
            if (!datasets.Any())
                return;
            PolygonalLegendDecoration puntualLegend = new PolygonalLegendDecoration(datasets);
            this.Decorations.Add(puntualLegend);
            CustomDecorations.Add(puntualLegend);
        }

        private void AddTiffLayer(string name, string path)
        {
            var layer = new SharpMap.Layers.GdalRasterLayerCachingProxy(name, path) {ColorCorrect = false};
            this.BackgroundLayer.Add(layer);
        }

        private void AddShapeLayer(string name, string shapefilePath)
        {
            var shapeLayer = new SharpMap.Layers.VectorLayer("outline",
                new SharpMap.Data.Providers.ShapeFile(shapefilePath))
            {
                Style =
                {
                    Fill = System.Drawing.Brushes.Transparent,
                    Outline = System.Drawing.Pens.Black,
                    EnableOutline = true,
                    Enabled = true
                }
            };
            this.Layers.Add(shapeLayer);
        }
    }
}
