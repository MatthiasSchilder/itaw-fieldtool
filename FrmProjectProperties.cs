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

            chkScaleBarDarstellen.Checked = _project.MapConfig.ScaleBarDarstellen;

            UpdateLayerListViews();
        }

        private void UpdateLayerListViews()
        {
            lvRasterkarten.Items.Clear();
            foreach (var ftLayer in _project.MapConfig.RasterLayer)
                lvRasterkarten.Items.Add(CreateLayerListViewItem(ftLayer));

            lvVektorkarten.Items.Clear();
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
            dialog.Multiselect = true;
            DialogResult dr = dialog.ShowDialog();
            if (dr == DialogResult.Abort)
                return;

            foreach(var filename in dialog.FileNames)
                _project.MapConfig.AddLayer(FtLayerType.FtRasterLayer, filename);
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkScaleBarDarstellen_CheckedChanged(object sender, EventArgs e)
        {
            _project.MapConfig.ScaleBarDarstellen = (sender as CheckBox).Checked;
        }

        private void btnChooseDefaultPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = true;
            DialogResult dr = dialog.ShowDialog();
            if (dr != DialogResult.OK)
                return;

            tbDefaultLookupPath.Text = dialog.SelectedPath;
        }
    }
}
