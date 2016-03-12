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
    }
}
