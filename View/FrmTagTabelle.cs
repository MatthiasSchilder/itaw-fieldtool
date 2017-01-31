using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using fieldtool.Data.Movebank;

namespace fieldtool.View
{
    public partial class FrmTagTabelle : Form
    {
        private FtTransmitterDataset _dataset;

        public FrmTagTabelle(FtTransmitterDataset dataset)
        {
            _dataset = dataset;
            _dataset.GPSData.FilterChanged += GPSData_FilterChanged;
            InitializeComponent();

            this.Text =
                $"GPS-Daten für Tag-ID {_dataset.TagId} von ({_dataset.GPSData.DateTimeFilterStart} - {_dataset.GPSData.DateTimeFilterStop})";

            BindingSource source = new BindingSource();
            source.DataSource = CreateDataTable();

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = source;
        }

        private void GPSData_FilterChanged(object sender, EventArgs e)
        {
            BindingSource source = new BindingSource();
            source.DataSource = CreateDataTable();

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = source;
        }

        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("StartTimestamp", typeof(DateTime));
            dt.Columns.Add("Rechtswert", typeof(double));
            dt.Columns.Add("Hochwert", typeof(double));
            dt.Columns.Add("Longitude", typeof(double));
            dt.Columns.Add("Latitude", typeof(double));

            foreach (var gpsEntry in _dataset.GPSData)
            {
                dt.Rows.Add(CreateRow(dt, gpsEntry));
            }

            return dt;
        }

        private DataRow CreateRow(DataTable dt, FtTransmitterGpsDataEntry gpsDataEntry)
        {
            DataRow result = dt.NewRow();
            result.ItemArray = (object[]) new object[]
                {gpsDataEntry.StartTimestamp, gpsDataEntry.Rechtswert, gpsDataEntry.Hochwert, gpsDataEntry.Longitude, gpsDataEntry.Latitude};
            return result;
        }
    }
}
