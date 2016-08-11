using System;
using System.IO;
using System.Windows.Forms;
using fieldtool.Presenter;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace fieldtool.View
{
    public partial class FrmProjectProperties : Form, IFtProjectPropertiesView
    {
        private FtProjectPropertiesPresenter Presenter { get; set; }
        private FtProject _project;

        public event EventHandler Initialize;

        public FrmProjectProperties(FtProject project)
        {
            _project = project;
            Presenter = new FtProjectPropertiesPresenter(this);

            InitializeComponent();
            Init();
        }

        private void Init()
        {
            this.Text = String.Format((string) this.Tag, _project.ProjectName);
            this.lblProjName.Text = _project.ProjectName;
            this.lblProjPath.Text = _project.ProjectFilePath;

            numEPSGSource.Value = _project.EPSGSourceProjection;
            numEPSGTarget.Value = _project.EPSGTargetProjection;

            if (_project.DefaultMovebankLookupPathAvailable)
                tbDefaultLookupPath.Text = _project.DefaultMovebankLookupPath;

            UpdateLayerListViews();
            UpdateTagBlacklist();
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

        private void UpdateTagBlacklist()
        {
            lbTagBlacklist.Items.Clear();
            foreach (var blEntry in _project.TagBlacklist)
                lbTagBlacklist.Items.Add(blEntry);
        }

        private ListViewItem CreateLayerListViewItem(FtLayer layer)
        {
            string[] subItemsArr = { "", Path.GetFileNameWithoutExtension(layer.FilePath), layer.FilePath };
            return new ListViewItem(subItemsArr) {Checked = layer.Active};
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
            if (lvRasterkarten.SelectedItems.Count == 0)
                return;

            foreach (ListViewItem selItem in lvRasterkarten.SelectedItems)
            {
                var lviFilePath = selItem.SubItems[2].Text;
                _project.MapConfig.DeleteLayer(FtLayerType.FtRasterLayer, lviFilePath);
            }
                
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

        private void btnChooseDefaultPath_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult dr = dialog.ShowDialog();
            if (dr != CommonFileDialogResult.Ok)
                return;

            tbDefaultLookupPath.Text = dialog.FileName;
            _project.DefaultMovebankLookupPath = dialog.FileName;
        }

        private void btnAddBlacklistEntry_Click(object sender, EventArgs e)
        {
            var frm = new FrmEnterTagId();
            if (FtFormFactory.ShowDialog(frm) != DialogResult.OK)
                return;
            if (!frm.TagIDValid)
                return;

            _project.TagBlacklist.Add(frm.TagID);
            UpdateTagBlacklist();
        }

        private void btnDelBlacklistEntry_Click(object sender, EventArgs e)
        {

        }

        private void numEPSGSource_ValueChanged(object sender, EventArgs e)
        {
            _project.EPSGSourceProjection = (int)numEPSGSource.Value;
        }

        private void numEPSGTarget_ValueChanged(object sender, EventArgs e)
        {
            _project.EPSGTargetProjection = (int)numEPSGTarget.Value;
        }
    }
}
