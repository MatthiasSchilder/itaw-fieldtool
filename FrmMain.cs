using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeoAPI.Geometries;
using System.IO;
using System.Windows.Media.Imaging;

namespace fieldtool
{
    public partial class FrmMain : Form, IFtProjectMainView
    {
        private FtProjectMainPresenter Presenter { get; set; }

        private const String WindowTitleNoProject = "itaw Fieldtool vX.XX";
        private const String WindowTitleProject = "itaw Fieldtool vX.XX - {0}";

        public FrmMain()
        {
            Presenter = new FtProjectMainPresenter(this);
            InitializeComponent();
            RegisterPresenterEvents();
            InvokeInitialize(new EventArgs());

            mapBox1.MouseMove += MouseMovedOnMap;
        }

        private void RegisterPresenterEvents()
        {
            Presenter.CursorCoordinatesChanged += CursorCoordinatesChanged;
            Presenter.MapChanged += MapChanged;
            Presenter.ProjectStateChanged += ProjectStateChanged;
            Presenter.MovebankImported += MovebankImported;
        }

        private void MovebankImported(object sender, MovebankImportedArgs movebankImportedArgs)
        {
            PopulateDatasetListview(movebankImportedArgs.Datasets);
        }

        private void PopulateDatasetListview(List<FtTransmitterDataset> datasets)
        {
            foreach (var dataset in datasets)
            {
                var listViewItem = new ListViewItem(dataset.TagId.ToString());
                listViewItem.Tag = dataset.TagId;
                lviDatasets.Items.Add(listViewItem);
            }
        }

        private void ProjectStateChanged(object sender, ProjectStateArgs projectStateArgs)
        {
            if (projectStateArgs.ProjektGeladen)
                this.Text = String.Format(WindowTitleProject, projectStateArgs.Name);
            else
            {
                this.Text = WindowTitleNoProject;
            }
        }

        private void MapChanged(object sender, MapChangedArgs eventArgs)
        {
            mapBox1.Map = eventArgs.Map;
            if (mapBox1.Map == null)
                return;

            try
            {
                mapBox1.Map.ZoomToExtents();
            }
            catch (Exception)
            {
            }
            mapBox1.Refresh();
        }

        private void CursorCoordinatesChanged(object sender, CursorCoordsChangedArgs cursorCoordsChangedArgs)
        {
            var xDisplay = Math.Round(cursorCoordsChangedArgs.X, 4);
            var yDisplay = Math.Round(cursorCoordsChangedArgs.Y, 4);
            statusLabelCoords.Text = String.Format((string)statusLabelCoords.Tag, xDisplay, yDisplay);
        }

        public void SetActiveTool(SharpMap.Forms.MapBox.Tools tool)
        {
            this.mapBox1.ActiveTool = tool;
        }

        private bool _statePanning = false;


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (_statePanning)
            {
                SetActiveTool(SharpMap.Forms.MapBox.Tools.None);
                _statePanning = false;
            }
            else
            {
                SetActiveTool(SharpMap.Forms.MapBox.Tools.Pan);
                _statePanning = true;
            }
                
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SetActiveTool(SharpMap.Forms.MapBox.Tools.ZoomIn);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SetActiveTool(SharpMap.Forms.MapBox.Tools.ZoomWindow);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            mapBox1.Map?.ZoomToExtents();
            mapBox1.Refresh();
        }


        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bearbeitenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void eigenschaftenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowProjectProperties(new EventArgs());
        }

        private void movebankLadenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowMovebankImport(new EventArgs());
        }

        private void einstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowEinstellungen(new EventArgs());
        }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeNewProject(new EventArgs());
        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeOpenProject(new EventArgs());
        }

        private void schließenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeCloseProject(new EventArgs());
        }

        private void speichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeSaveProject(new EventArgs());
        }



        private void kartenansichtAlsBildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (FtManager.Instance().Projekt.ExportToClipboard)
            //{
            //    Clipboard.SetImage(mapBox1.Image);
            //    MessageBox.Show("Die Kartenansicht wurde in die Zwischenablage kopiert.");
            //}
            //else
            //{
            //    SaveFileDialog dialog = new SaveFileDialog();
            //    dialog.Filter = "PNG-Grafik|*.png";
            //    DialogResult dr = dialog.ShowDialog();


            //    if (dr != DialogResult.OK)
            //        return;


            //    mapBox1.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            //}

        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            InvokeSaveProject(new EventArgs());
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            InvokeOpenProject(new EventArgs());
        }

        public event EventHandler OpenProject;
        public void InvokeOpenProject(EventArgs e)
        {
            EventHandler handler = OpenProject;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CloseProject;

        public void InvokeCloseProject(EventArgs e)
        {
            EventHandler handler = CloseProject;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler SaveProject;
        public void InvokeSaveProject(EventArgs e)
        {
            EventHandler handler = SaveProject;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        public event EventHandler NewProject;
        public void InvokeNewProject(EventArgs e)
        {
            EventHandler handler = NewProject;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public event EventHandler ShowProjectProperties;
        public void InvokeShowProjectProperties(EventArgs e)
        {
            EventHandler handler = ShowProjectProperties;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        
        public event EventHandler ShowMovebankImport;
        public void InvokeShowMovebankImport(EventArgs e)
        {
            EventHandler handler = ShowMovebankImport;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public event EventHandler ShowEinstellungen;
        

        public void InvokeShowEinstellungen(EventArgs e)
        {
            EventHandler handler = ShowEinstellungen;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ShowInfo;


        public void InvokeShowInfo(EventArgs e)
        {
            EventHandler handler = ShowInfo;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ShowRawTagInfo;
        public void InvokeShowRawTagInfo(EventArgs e)
        {
            EventHandler handler = ShowRawTagInfo;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ShowRawAccel;
        public void InvokeShowRawAccel(EventArgs e)
        {
            EventHandler handler = ShowRawAccel;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ShowRawGPS;
        public void InvokeShowRawGPS(EventArgs e)
        {
            EventHandler handler = ShowRawGPS;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<CurrentDatasetChangedEventArgs> CurrentDatasetChanged;
        public void InvokeCurrentDatasetChanged(CurrentDatasetChangedEventArgs e)
        {
            EventHandler<CurrentDatasetChangedEventArgs> handler = CurrentDatasetChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        public event SharpMap.Forms.MapBox.MouseEventHandler MouseMovedOnMap;

        public event EventHandler Initialize;
        public void InvokeInitialize(EventArgs e)
        {
            EventHandler handler = Initialize;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowInfo(new EventArgs());
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Column == 1 && e.Row == 0)
            {
                var rectangle = e.CellBounds;
                rectangle.Inflate(-2, -2);

                ControlPaint.DrawBorder(e.Graphics, rectangle, SystemColors.ControlDark, ButtonBorderStyle.Solid); // 3D border
                
            }
        }

        private void tagInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowRawTagInfo(new EventArgs());
        }

        private void beschleunigungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowRawAccel(new EventArgs());
        }

        private void gPSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowRawGPS(new EventArgs());
        }

        private void lviDatasets_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItems = lviDatasets.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            if (selectedItems[0] == null)
            {
                InvokeCurrentDatasetChanged(new CurrentDatasetChangedEventArgs(null));
                return;
            }
            InvokeCurrentDatasetChanged(new CurrentDatasetChangedEventArgs((int)selectedItems[0].Tag));
        }
    }
    public class CurrentDatasetChangedEventArgs : EventArgs
    {
        public int? CurrentTagId { get; private set; }
        public CurrentDatasetChangedEventArgs(int? currentTagId)
        {
            CurrentTagId = currentTagId;
        }
    }
}
