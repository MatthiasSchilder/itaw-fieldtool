using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool
{
    public enum FtLayerType
    {
        FtRasterLayer,
        FtVektorLayer
    }
    public class FtLayer
    {
        public FtLayerType LayerType { get; set; }
        public bool Active { get; set; }
        public String FilePath { get; set; }

        public FtLayer()
        {
            
        }

        public FtLayer(FtLayerType layerType, bool active, string filePath)
        {
            LayerType = layerType;
            Active = active;
            FilePath = filePath;
        }
    }

    public class FtMapConfig
    {
        public List<FtLayer> RasterLayer { get; set; }
        public List<FtLayer> VektorLayer { get; set; }

        public FtMapConfig()
        {
            RasterLayer = new List<FtLayer>();
            VektorLayer = new List<FtLayer>();
        }

        public void AddLayer(FtLayerType layerType, String filePath)
        {
            if (layerType == FtLayerType.FtRasterLayer)
                AddRasterLayer(filePath);
            else if (layerType == FtLayerType.FtVektorLayer)
                AddVektorLayer(filePath);
        }

        private void AddRasterLayer(String filePath)
        {
            RasterLayer.Add(new FtLayer(FtLayerType.FtRasterLayer, true, filePath));
        }

        private void AddVektorLayer(String filePath)
        {
            VektorLayer.Add(new FtLayer(FtLayerType.FtVektorLayer, true, filePath));
        }

        public void DeleteLayer(FtLayerType layerType, String filePath)
        {
            if (layerType == FtLayerType.FtRasterLayer)
                DeleteRasterLayer(filePath);
            else if (layerType == FtLayerType.FtVektorLayer) 
                DeleteVektorLayer(filePath);
        }
        private void DeleteRasterLayer(String filePath)
        {
            RasterLayer.RemoveAll(v => v.FilePath == filePath);
        }

        private void DeleteVektorLayer(String filePath)
        {
            VektorLayer.RemoveAll(v => v.FilePath == filePath);
        }
    }
}
