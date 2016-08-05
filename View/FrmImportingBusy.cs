using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fieldtool.View
{
    public partial class FrmImportingBusy : Form
    {
        private const string status = "Importiere Tag {0}...";

        public FrmImportingBusy(int numTags)
        {
            InitializeComponent();
            progressBar1.Maximum = numTags;
        }

        public void Step(string tagName)
        {
            progressBar1.Value++;
            label1.Text = string.Format(status, tagName);
            this.Invalidate();
            this.Refresh();
        }
    }
}
