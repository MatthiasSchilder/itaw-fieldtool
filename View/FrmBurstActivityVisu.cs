using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fieldtool
{
    public partial class FrmBurstActivityVisu : Form
    {
        public FrmBurstActivityVisu(int tagId, Image bmp)
        {
            InitializeComponent();
            this.Text = String.Format(this.Text, tagId);
            pictureBox1.Image = bmp;
        }

        private void FrmBurstActivityVisu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetImage(pictureBox1.Image);
            }
        }

        private void btnChancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
        }
    }
}
