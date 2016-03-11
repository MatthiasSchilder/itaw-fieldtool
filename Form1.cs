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
    public partial class Form1 : Form
    {
        public Form1(Bitmap bmp)
        {
            
            InitializeComponent();
            pictureBox1.Image = bmp;
        }
    }
}
