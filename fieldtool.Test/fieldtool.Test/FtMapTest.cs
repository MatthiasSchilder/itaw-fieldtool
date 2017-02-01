using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fieldtool;
using fieldtool.Data;
using fieldtool.Data.Movebank;
using fieldtool.SharpmapExt.Symbolizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpmapGDAL;
using SharpMap.Data.Providers;
using SharpMap.Layers;

namespace fieldtool.Test
{
    [TestClass]
    public class FtMapTest
    {
        private FtProject _project;
        private FtMap _map;

        [TestInitialize]
        public void Init()
        {
            _project = new FtProject(Path.GetRandomFileName());

            var ftFileset = new FtFileset(3914);
            ftFileset.AddFile(FtFileFunction.GPSData, "data/testdata/movebank/tag3914_gps.txt");
            ftFileset.AddFile(FtFileFunction.AccelData, "data/testdata/movebank/tag3914_acc.txt");
            ftFileset.AddFile(FtFileFunction.TagInfo, "data/testdata/movebank/info_tag3914.txt");

            _project.MovebankFilesets = new List<FtFileset>() {ftFileset};

            ProjectionManager.SetSourceProjection(4326);
            ProjectionManager.SetTargetProjection(31467);

            _project.LoadDatasets(null, null, null);

            _map = new FtMap(_project);

            _project.Datasets.First().Visulization.Symbolizer = new FtDotPointSymbolizer(System.Drawing.Color.Black);



            //_gpsData = new FtTransmitterGpsData(3914, "data/testdata/movebank/tag3914_gps.txt");
        }

        [TestMethod]
        public void TestLayerCount1()
        {
            _project.SetDatasetFeatureState(3914, true);

            var layerCount = _map.VariableLayers.Count;
            var expectedValue = 1;

            Assert.AreEqual(expectedValue, layerCount);
        }

        [TestMethod]
        public void TestLayerCount2()
        {
            _project.SetDatasetFeatureState(3914, true);
            _project.SetDatasetFeatureState(3914, false);

            var layerCount = _map.VariableLayers.Count;
            var expectedValue = 0;

            Assert.AreEqual(expectedValue, layerCount);
        }

        [TestMethod]
        public void TestDoubleToggle()
        {
            _project.SetDatasetFeatureState(3914, true);
            _project.SetDatasetFeatureState(3914, true);

            var layerCount = _map.VariableLayers.Count;
            var expectedValue = 1;

            Assert.AreEqual(expectedValue, layerCount);
        }

        [TestMethod]
        public void TestEntCount()
        {
            _project.SetDatasetFeatureState(3914, true);

            var layer = _map.VariableLayers.First() as VectorLayer;
            if(layer == null)
                Assert.Fail();
            var ds = layer.DataSource as DataTablePoint;
            if (ds == null)
                Assert.Fail();

            var expectedValue = 10040;

            Assert.AreEqual(ds.Table.Rows.Count, expectedValue);
        }

        [TestMethod]
        public void TestCountInFullViewMatchesDS()
        {
            _project.SetDatasetFeatureState(3914, true);

            var layer = _map.VariableLayers.First() as VectorLayer;
            if (layer == null)
                Assert.Fail();
            var ds = layer.DataSource as DataTablePoint;
            if (ds == null)
                Assert.Fail();

            var countInDS = ((DataTablePoint) ds).Table.Rows.Count;
            
            var dsExtent = ds.GetExtents();

            var geosInEnv =  (int) ds.GetObjectIDsInView(dsExtent).Count;
            Assert.AreEqual(geosInEnv, countInDS);
        }

        [TestMethod]
        public void TestEntCountWithOwnEnvVsDSEnv()
        {
            _project.SetDatasetFeatureState(3914, true);

            var layer = _map.VariableLayers.First() as VectorLayer;
            if (layer == null)
                Assert.Fail();
            var ds = layer.DataSource as DataTablePoint;
            if (ds == null)
                Assert.Fail();

            var dsExtent = ds.GetExtents();

            var ownExtent = _project.Datasets[0].GPSData.GetEnvelope();

            var geosInEnvDS = (int)ds.GetObjectIDsInView(dsExtent).Count;
            var geosInEnvOwn = (int)ds.GetObjectIDsInView(ownExtent).Count;

            Assert.AreEqual(geosInEnvDS, geosInEnvOwn);
        }

        [TestMethod]
        public void TestDataProviderEnvVsOwnEnv()
        {
            _project.SetDatasetFeatureState(3914, true);

            var layer = _map.VariableLayers.First() as VectorLayer;
            if (layer == null)
                Assert.Fail();
            var ds = layer.DataSource as DataTablePoint;
            if (ds == null)
                Assert.Fail();

            var dsExtent = ds.GetExtents();

            var ownExtent = _project.Datasets[0].GPSData.GetEnvelope();

            if(dsExtent.MaxX != ownExtent.MaxX)
                Assert.Fail("MaxX");

            if (dsExtent.MinX != ownExtent.MinX)
                Assert.Fail("MinX");

            if (dsExtent.MaxY != ownExtent.MaxY)
                Assert.Fail("MaxY");

            if (dsExtent.MinY != ownExtent.MinY)
                Assert.Fail("MinY");

        }

        [TestMethod]
        public void TestDTPointCountsMatchesGeometriesCount()
        {
            _project.SetDatasetFeatureState(3914, true);

            var layer = _map.VariableLayers.First() as VectorLayer;
            if (layer == null)
                Assert.Fail();
            var ds = layer.DataSource as DataTablePoint;
            if (ds == null)
                Assert.Fail();

            var dsExtent = ds.GetExtents();

            var geosInEnvDS = (int)ds.GetObjectIDsInView(dsExtent).Count;

            var geosCount = ds.GetGeometriesInView(dsExtent).Count;

            Assert.AreEqual(geosInEnvDS, geosCount);
        }

        [TestMethod]
        public void TestFilter()
        {
            _project.SetDatasetFeatureState(3914, true);

            _project.SetIntervalFilter(_project.Datasets[0], new DateTime(2015, 3, 20, 13, 49, 50), new DateTime(2015, 03, 21, 13, 20, 0));

            var layer = _map.VariableLayers.First() as VectorLayer;
            if (layer == null)
                Assert.Fail();
            var ds = layer.DataSource as DataTablePoint;
            if (ds == null)
                Assert.Fail();

            var featureCount = ds.GetFeatureCount();

            Assert.AreEqual(featureCount, 10);

        }

        [TestMethod]
        public void TestDoubleFiltering()
        {
            _project.SetDatasetFeatureState(3914, true);

            _project.SetIntervalFilter(_project.Datasets[0], new DateTime(2015, 3, 20, 13, 49, 50), new DateTime(2015, 03, 21, 13, 20, 0));

            var layer = _map.VariableLayers.First() as VectorLayer;
            if (layer == null)
                Assert.Fail();
            var ds = layer.DataSource as DataTablePoint;
            if (ds == null)
                Assert.Fail();

            var featureCount = ds.GetFeatureCount();

            Assert.AreEqual(featureCount, 10);

            _project.SetIntervalFilter(_project.Datasets[0], new DateTime(2015, 3, 21, 13, 20, 0), new DateTime(2015, 03, 21, 13, 20, 0));

            layer = _map.VariableLayers.First() as VectorLayer;
            if (layer == null)
                Assert.Fail();
            ds = layer.DataSource as DataTablePoint;
            if (ds == null)
                Assert.Fail();
            featureCount = ds.GetFeatureCount();
            Assert.AreEqual(featureCount,1);

        }

        [TestMethod]
        public void TestEntCountAfterToggling()
        {
            _project.SetDatasetFeatureState(3914, true);
            _project.SetDatasetFeatureState(3914, false);

            Assert.AreEqual(false, _map.VariableLayers.Any());
        }
    }
}
