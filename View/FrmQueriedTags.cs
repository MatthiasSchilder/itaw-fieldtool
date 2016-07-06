using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpMap.Data;

namespace fieldtool.View
{
    public partial class FrmQueriedTags : Form
    {
        public FrmQueriedTags(List<FeatureDataTable> tabs)
        {
            InitializeComponent();

            BindingSource source = new BindingSource();
            source.DataSource = MergeTables(tabs);

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = source;

            this.Text = String.Format("Gefundene Tags");
        }

        private DataTable MergeTables(List<FeatureDataTable> tabs)
        {
            DataTable dt = new DataTable();

            var columns = tabs.First().Columns;
            foreach (DataColumn column in columns)
            {
                dt.Columns.Add(column.ToString(), column.DataType);
            }

            foreach (var tab in tabs)
            {
                foreach (DataRow row in tab.Rows)
                {
                    dt.Rows.Add(CloneRow(dt, row));
                }
            }

            return dt;
        }

        private DataRow CloneRow(DataTable dt, DataRow row)
        {
            DataRow result = dt.NewRow();
            result.ItemArray = (object[])row.ItemArray.Clone();
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
