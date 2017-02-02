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
        private Dictionary<FtTransmitterDataset, FtPuntualVectorLayer> ActiveVectorLayers;
        private Dictionary<FtTransmitterMCPDataEntry, PolygonalVectorLayer> ActivePolygonalVectorLayers;

        private PuntualLegendDecoration PuntualLegendDecoration;
        private PolygonalLegendDecoration PolygonalLegendDecoration;

        #region Init
        public FtMap(FtProject project) : base()
        {
            this.BackColor = System.Drawing.Color.White;
            Init(project);
            project.DataChangedEventHandler += DataChangedEventHandler;
            ActiveVectorLayers = new Dictionary<FtTransmitterDataset, FtPuntualVectorLayer>();
            ActivePolygonalVectorLayers = new Dictionary<FtTransmitterMCPDataEntry, PolygonalVectorLayer>();

            PuntualLegendDecoration = new PuntualLegendDecoration();
            PolygonalLegendDecoration = new PolygonalLegendDecoration();
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

            if(eventArgs.FeatureType == FtDatasetFeatureType.Puntual)
                UpdatePuntualFeature(dataset);
            else if (eventArgs.FeatureType == FtDatasetFeatureType.Polygonal)
                UpdatePolygonalFeature(dataset);
        }

        private void UpdatePuntualFeature(FtTransmitterDataset dataset)
        {
            if (!dataset.Active)
            {
                if (ActiveVectorLayers.ContainsKey(dataset))
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

            UpdatePuntualLegend(ActiveVectorLayers.Keys.ToList());
        }

        private void UpdatePolygonalFeature(FtTransmitterDataset dataset)
        {
            ErasePolygonalFeatures(dataset);
            DrawPolygonalFeatures(dataset);

            UpdatePolygonalLegend(ActivePolygonalVectorLayers.Keys.ToList());
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

        private void DrawPolygonalFeatures(FtTransmitterDataset dataset)
        {
            foreach (var polygonalFeature in dataset.MCPData)
            {
                if (!polygonalFeature.Active)
                    continue;

                var poly = this.Factory.CreatePolygon(polygonalFeature.Polygon.Vertices.ToArray());
                var polygonalVectorLayer = new PolygonalVectorLayer(dataset.TagId.ToString() + "MCP" + polygonalFeature.PercentageMCP, new GeometryProvider(poly))
                {
                    Symbolizer = new FtPolygonWithAlphaSymbolizer(polygonalFeature.Color, 
                        Properties.Settings.Default.HRPolygonDrawMode == HomeRangePolygonDrawMode.NurUmring)
                };

                ActivePolygonalVectorLayers.Add(polygonalFeature, polygonalVectorLayer);
                this.VariableLayers.Add(polygonalVectorLayer);
            }
        }

        private void ErasePolygonalFeatures(FtTransmitterDataset dataset)
        {
            foreach (var mcpEntry in dataset.MCPData)
            {
                if (!ActivePolygonalVectorLayers.ContainsKey(mcpEntry))
                    continue;

                var activePolygonalVectorLayerMCP = ActivePolygonalVectorLayers[mcpEntry];

                ActivePolygonalVectorLayers.Remove(mcpEntry);
                this.VariableLayers.Remove(activePolygonalVectorLayerMCP);
            }

            //var puntualVectorLayer = ActiveVectorLayers[dataset];
        }

        private void AddScaleBar()
        {
            SharpMap.Rendering.Decoration.ScaleBar.ScaleBar scaleBar =
                new SharpMap.Rendering.Decoration.ScaleBar.ScaleBar();
            scaleBar.BarColor1 = Color.Black;
            scaleBar.BarColor2 = Color.White;
            this.Decorations.Add(scaleBar);
        }

        private void UpdatePuntualLegend(List<FtTransmitterDataset> datasets)
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

        private void UpdatePolygonalLegend(List<FtTransmitterMCPDataEntry> mcps)
        {
            if (!mcps.Any())
            {
                if (!this.Decorations.Contains(PolygonalLegendDecoration))
                {
                    return;
                }
                this.Decorations.Remove(PolygonalLegendDecoration);
                return;
            }

            PolygonalLegendDecoration.Update(mcps);
            // wenn die Legende schon drin ist, einmal entfernen, um Redraw mit neuen Items zu erzwingen
            if (this.Decorations.Contains(PolygonalLegendDecoration))
                this.Decorations.Remove(PolygonalLegendDecoration);
            this.Decorations.Add(PolygonalLegendDecoration);
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
