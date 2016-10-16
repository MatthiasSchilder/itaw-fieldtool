using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI;
using GeoAPI.Geometries;
using SharpMap.Data;

namespace fieldtool.SharpmapExt.Providers
{
    public class DataTableLine : SharpMap.Data.Providers.PreparedGeometryProvider
    {
        private DataTable Table;
        private String OidColumnName;
        private String x1;
        private String y1;
        private String x2;
        private String y2;

        private IGeometryFactory Factory = 
            GeometryServiceProvider.Instance.CreateGeometryFactory();

        private IGeometry DatarowToLine(DataRow row)
        {
            Coordinate[] coords = new Coordinate[2];

            coords[0] = new Coordinate((double)row.ItemArray[1], (double)row.ItemArray[2]);
            coords[1] = new Coordinate((double)row.ItemArray[3], (double)row.ItemArray[4]);

            return Factory.CreateLineString(coords);
        }

        public DataTableLine(DataTable table, string oidColumnName, string x1, string y1, string x2, string y2)
        {
            Table = table;
        }

        public override Collection<IGeometry> GetGeometriesInView(Envelope bbox)
        {
            Collection<IGeometry> result = new Collection<IGeometry>();

            foreach(var row in Table.Select())
                result.Add(DatarowToLine(row));

            return result;
        }

        public override Collection<uint> GetObjectIDsInView(Envelope bbox)
        {
            Collection<uint> result = new Collection<uint>();

            foreach (var row in Table.Select())
                result.Add((uint)row.ItemArray[0]);

            return result;
        }

        public override IGeometry GetGeometryByID(uint oid)
        {
            var row = Table.Select($"{OidColumnName} = 'm'")[0];
            if (row != null)
                return DatarowToLine(row);
            return null;
        }

        public override void ExecuteIntersectionQuery(Envelope box, FeatureDataSet ds)
        {
            return;
        }

        public override int GetFeatureCount()
        {
            return Table.Rows.Count;
        }

        public override FeatureDataRow GetFeature(uint rowId)
        {
            return (FeatureDataRow) Table.Select($"{OidColumnName} = 'm'")[0];
        }

        public override Envelope GetExtents()
        {
            return new Envelope(0, double.MaxValue, 0, double.MaxValue);
        }
    }
}
