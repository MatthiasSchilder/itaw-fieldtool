using System.Drawing;
using System.Drawing.Drawing2D;
using fieldtool.Data.Movebank;
using fieldtool.SharpmapExt.Symbolizers;
using GeoAPI.Geometries;
using SharpMap.Data;
using SharpMap.Data.Providers;
using SharpMap.Layers;
using SharpMap.Rendering.Symbolizer;
using SharpMap.Styles;

namespace fieldtool.Layers
{
    /// <summary>
    /// Layer für die Darstellung der Orte
    /// </summary>
    public class FtPuntualVectorLayer : SharpMap.Layers.LayerCollection
    {
        public LabelLayer LabelLayer;
        public VectorLayer SymbolizerLayer;

        private FtMap _ftMap;

        private System.Drawing.Font MapFont = 
            new Font("Arial", Properties.Settings.Default.VisualizerTextsize);


        public FtPuntualVectorLayer(FtMap ftMap, FtTransmitterDataset dataset)
        {
            _ftMap = ftMap;

            IProvider dataSource = null;
            var symbolizer = dataset.Visulization.Symbolizer;

            var pointSymbolizer = symbolizer as IPointSymbolizer;
            if (pointSymbolizer != null)
            {
                dataSource = dataset.GPSData.AsDataTablePoint();
                SymbolizerLayer = new VectorLayer(dataset.TagId.ToString(), dataSource)
                {
                    Style = { PointSymbolizer = pointSymbolizer }
                };

                pointSymbolizer.Size = new Size(Properties.Settings.Default.VisualizerMarkersize,
                    Properties.Settings.Default.VisualizerMarkersize);
            }

            var lineSymbolizer = symbolizer as FtBasicLineSymbolizer;
            if (lineSymbolizer != null)
            {
                dataSource = dataset.GPSData.AsDataTableLine();
                SymbolizerLayer = new VectorLayer(dataset.TagId.ToString(), dataSource)
                {
                    Style = { LineSymbolizer = lineSymbolizer }
                };

                lineSymbolizer.Size = new Size(Properties.Settings.Default.VisualizerMarkersize,
                    Properties.Settings.Default.VisualizerMarkersize);
            }

            SymbolizerLayer.SmoothingMode = SmoothingMode.HighQuality;
            symbolizer.SmoothingMode = SmoothingMode.HighQuality;

            if (dataSource?.GetFeatureCount() > 500)
            {
                SymbolizerLayer.SmoothingMode = SmoothingMode.HighSpeed;
                symbolizer.SmoothingMode = SmoothingMode.HighSpeed;
            }

            if ((symbolizer as IFtBaseSymbolizer).Labeled)
            {
                LabelLayer = new LabelLayer($"Label{dataset.TagId}")
                {
                    DataSource = dataset.GPSData.AsDataTablePoint(),
                    LabelColumn = "num",
                    LabelPositionDelegate = LabelPositionDelegate,
                    SmoothingMode = SmoothingMode.HighSpeed,
                    Style = { Font = MapFont, VerticalAlignment = LabelStyle.VerticalAlignmentEnum.Top, HorizontalAlignment = LabelStyle.HorizontalAlignmentEnum.Left, CollisionDetection = false, ForeColor = dataset.Visulization.Color }
                };
            }
            if (LabelLayer != null)
            {
                this.Add(LabelLayer);
            }
            this.Add(SymbolizerLayer);
        }

        private static readonly Graphics _stringMeasurementGraphicsDummy =
            Graphics.FromImage(new Bitmap(1, 1));
        private Coordinate LabelPositionDelegate(FeatureDataRow fdr)
        {
            var screenCoord = _ftMap.WorldToImage(fdr.Geometry.Coordinate, false);
            var size = _stringMeasurementGraphicsDummy.MeasureString(((int)fdr["num"]).ToString(), MapFont);

            var lblScreenCoord = new PointF(screenCoord.X - size.Width / 2, screenCoord.Y + 6 + 2);
            return new Coordinate(_ftMap.ImageToWorld(lblScreenCoord));
        }



    }
}
