using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;
using SharpMap.Data;
using SharpMap.Data.Providers;

namespace fieldtool.SharpmapExt
{
    public class DataTableLine : PreparedGeometryProvider, IDisposable
    {
        //private DataTable DataTable1;

        private FeatureDataTable FeatureDataTable;

        private string OidColumnName;
        private string StartPointXColumnName;
        private string StartPointYColumnName;

        private string EndPointXColumnName;
        private string EndPointYColumnName;

        public DataTableLine(DataTable dataTable, string oidColumnName,
            string startPointXColumnName, string startPointYColumnName, string endPointXColumnName, string endPointYColumnName)
        {
            OidColumnName = oidColumnName;

            StartPointXColumnName = startPointXColumnName;
            StartPointYColumnName = startPointYColumnName;

            EndPointXColumnName = endPointXColumnName;
            EndPointYColumnName = endPointYColumnName;

            //DataTable1 = dataTable;
            FeatureDataTable = new FeatureDataTable();
            foreach (DataColumn col in dataTable.Columns)
            {
                FeatureDataTable.Columns.Add(col.ColumnName, col.DataType, col.Expression);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                FeatureDataTable.ImportRow(row);
                FeatureDataRow fdr = FeatureDataTable.Rows[FeatureDataTable.Rows.Count - 1] as FeatureDataRow;
                fdr.Geometry = Factory.CreateLineString(new[] {
                    new Coordinate((double) row[StartPointXColumnName], (double) row[StartPointYColumnName]),
                    new Coordinate((double) row[EndPointXColumnName], (double) row[EndPointYColumnName])});
            }
        }

        public override Collection<IGeometry> GetGeometriesInView(Envelope bbox)
        {
            var oidsInView = GetObjectIDsInView(bbox);

            Collection<IGeometry> result = new Collection<IGeometry>();
            foreach (var oid in oidsInView)
                result.Add(GetGeometryByID(oid));

            return result;
        }

        public override Collection<uint> GetObjectIDsInView(Envelope bbox)
        {
            Collection<uint> result = new Collection<uint>();

            foreach (FeatureDataRow row in FeatureDataTable.Rows)
            {
                var oid = (uint)row[OidColumnName];
                var entityEnv = GetExtentsByUid(oid);
                
                if(bbox.Intersects(entityEnv))
                    result.Add(oid);
            }
            return result;

        }

        public override IGeometry GetGeometryByID(uint oid)
        {
            var featureRows = FeatureDataTable.Select($"{OidColumnName} = {oid}");
            var row = featureRows[0];

            return Factory.CreateLineString(new[] {
                new Coordinate((double) row[StartPointXColumnName], (double) row[StartPointYColumnName]),
                new Coordinate((double) row[EndPointXColumnName], (double) row[EndPointYColumnName])});
        }

        public override void ExecuteIntersectionQuery(Envelope box, FeatureDataSet ds)
        {
            var result = GetObjectIDsInView(box);

            FeatureDataTable fdt = new FeatureDataTable(FeatureDataTable);

            foreach (DataColumn col in FeatureDataTable.Columns)
            {
                fdt.Columns.Add(col.ColumnName, col.DataType, col.Expression);
            }

            foreach (var oid in result)
            {
                DataRow dr = GetFeature(oid);

                fdt.ImportRow(dr);
                FeatureDataRow fdr = fdt.Rows[fdt.Rows.Count - 1] as FeatureDataRow;
                fdr.Geometry = GetGeometryByID((uint) dr[OidColumnName]);
            }

            ds.Tables.Add(fdt);
        }

        public override int GetFeatureCount()
        {
            return FeatureDataTable.Rows.Count;
        }

        public override FeatureDataRow GetFeature(uint rowId)
        {
            var featureRows = FeatureDataTable.Select($"{OidColumnName} = {rowId}");

            
          
            return (FeatureDataRow) featureRows[0];
        }

        public override Envelope GetExtents()
        {
            double xmin = double.MaxValue, xmax = double.MinValue;
            double ymin = double.MaxValue, ymax = double.MinValue;

            Envelope wholeEnvelope = new Envelope();

            foreach (DataRow row in FeatureDataTable.Rows)
            {
                var oid = (uint) row[OidColumnName];
                var env = GetExtentsByUid(oid);
                wholeEnvelope.ExpandToInclude(env);
            }
            return wholeEnvelope;
        }

        private Envelope GetExtentsByUid(uint oid)
        {
            var featureRows = FeatureDataTable.Select($"{OidColumnName} = {oid}");
            if (!featureRows.Any())
                throw new Exception("Kein Feature für OID gefunden.");

            var row = featureRows[0];

            var startPointX = (double) row[StartPointXColumnName];
            var startPointY = (double) row[StartPointYColumnName];
            var endPointX = (double) row[EndPointXColumnName];
            var endPointY = (double) row[EndPointYColumnName];

            var preMinX = Math.Min(startPointX, endPointX);
            var preMaxX = Math.Max(startPointX, endPointX);
            var preMinY = Math.Min(startPointY, endPointY);
            var preMaxY = Math.Max(startPointY, endPointY);

            double xmin = double.MaxValue, xmax = double.MinValue;
            double ymin = double.MaxValue, ymax = double.MinValue;

            xmin = Math.Min(xmin, preMinX);
            xmax = Math.Max(xmax, preMaxX);
            ymin = Math.Min(ymin, preMinY);
            ymax = Math.Max(ymax, preMaxY);

            return new Envelope(xmin, xmax, ymin, ymax);
        }
    }
}
