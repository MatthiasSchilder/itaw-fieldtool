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

namespace SharpmapGDAL
{
    public partial class MainWindow : Form
    {
        private TMap tmap;

        public MainWindow()
        {
            InitializeComponent();

            tmap = new TMap();
            SetupMapbox();
        }

        private void SetupMapbox()
        {
            TMap.InitWithTestData(tmap);
            mapBox1.Map = tmap;
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
            tmap?.ZoomToExtents();
            mapBox1.Refresh();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
