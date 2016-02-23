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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            mapBox1.MouseMove += MapBox1OnMouseMove;
        }

        private void MapBox1OnMouseMove(Coordinate worldPos, MouseEventArgs imagePos)
        {
            toolStripStatusLabel1.Text = String.Format((string)toolStripStatusLabel1.Tag, worldPos.X, worldPos.Y);
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

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

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
            FtManager.Instance().ShowProjectPropertiesDialog();
            UpdateGUI();
        }

        private void movebankLadenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FtManager.Instance().ShowImportMovebankDialog();
        }

        private void einstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FtManager.Instance().ShowSettingsDialog();
        }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FtManager.Instance().NewProject();
        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FtManager.Instance().OpenProject();
            UpdateGUI();
        }

        private void schließenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FtManager.Instance().CloseProject();
        }

        private void speichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FtManager.Instance().SaveProject();
        }

        private void UpdateGUI()
        {
            FtProject project = FtManager.Instance().Projekt;
            if (project == null)
                ;
            else
            {
                FtMap ftMap = new FtMap(project);
                mapBox1.Map = ftMap;
            }

        }

        private void kartenansichtAlsBildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FtManager.Instance().Projekt.ExportToClipboard)
            {
                Clipboard.SetImage(mapBox1.Image);
                MessageBox.Show("Die Kartenansicht wurde in die Zwischenablage kopiert.");
            }
            else
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "PNG-Grafik|*.png";
                DialogResult dr = dialog.ShowDialog();


                if (dr != DialogResult.OK)
                    return;


                mapBox1.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }

        }
    }
}
