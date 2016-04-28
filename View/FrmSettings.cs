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
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            picColor.BackColor = Properties.Settings.Default.AccPlotNoDataColor;
            chkShowMassstab.Checked = Properties.Settings.Default.ShowMapMasssstab;
        }

        private void picColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Properties.Settings.Default.AccPlotNoDataColor;
            if (colorDialog1.ShowDialog() != DialogResult.OK)
                return;
            Properties.Settings.Default.AccPlotNoDataColor = colorDialog1.Color;
            Properties.Settings.Default.Save();
            picColor.BackColor = Properties.Settings.Default.AccPlotNoDataColor;
        }

        private void btnSchließen_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkShowMassstab_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowMapMasssstab = chkShowMassstab.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
