using System;
using System.Drawing;
using System.Windows.Forms;

namespace fieldtool.View
{
    public partial class FrmBurstActivityVisu : Form
    {
        public FrmBurstActivityVisu(FtTransmitterDataset dataset, Color noDataColor)
        {
            InitializeComponent();
            this.Text = String.Format(this.Text, dataset.TagId);
            accVisualizer1.Setdata(noDataColor, dataset.AccelData.CalculatedActivities);
        }

        private void FrmBurstActivityVisu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetImage(accVisualizer1.Image);
            }
        }

        private void btnChancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(accVisualizer1.Image);
        }
    }
}
