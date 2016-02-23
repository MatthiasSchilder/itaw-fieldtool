using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fieldtool
{
    public partial class FrmProjectProperties : Form
    {
        private FtProject _project;

        public FrmProjectProperties(FtProject project)
        {
            InitializeComponent();
            _project = project;

            Init();
        }

        private void Init()
        {
            this.Text = String.Format((string) this.Tag, _project.ProjectName);
            this.lblProjName.Text = _project.ProjectName;
            this.lblProjPath.Text = _project.ProjectFilePath;

            UpdateLayerListViews();
        }

        private void UpdateLayerListViews()
        {
            foreach (var ftLayer in _project.MapConfig.RasterLayer)
                lvRasterkarten.Items.Add(CreateLayerListViewItem(ftLayer));

            foreach (var ftLayer in _project.MapConfig.VektorLayer)
                lvVektorkarten.Items.Add(CreateLayerListViewItem(ftLayer));
        }

        private ListViewItem CreateLayerListViewItem(FtLayer layer)
        {
            string[] subItemsArr = new[] { "", Path.GetFileNameWithoutExtension(layer.FilePath), layer.FilePath };
            ListViewItem lvi = new ListViewItem(subItemsArr);
            lvi.Checked = layer.Active;

            return lvi;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnAddRaster_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "GeoTIFF|*.tif";
            DialogResult dr = dialog.ShowDialog();
            if (dr == DialogResult.Abort)
                return;

            _project.MapConfig.AddLayer(FtLayerType.FtRasterLayer, dialog.FileName);
            UpdateLayerListViews();
        }

        private void btnDeleteRaster_Click(object sender, EventArgs e)
        {
            UpdateLayerListViews();

        }

        private void btnAddVektor_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Shapefiles|*.shp";
            DialogResult dr = dialog.ShowDialog();
            if (dr == DialogResult.Abort)
                return;

            _project.MapConfig.AddLayer(FtLayerType.FtVektorLayer, dialog.FileName);
            UpdateLayerListViews();
        }

        private void btnDeleteVektor_Click(object sender, EventArgs e)
        {
            UpdateLayerListViews();
        }
    }
}
