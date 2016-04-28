using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpMap.Rendering.Decoration;

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
            InitAnchorCombobox();
        }

        private void InitAnchorCombobox()
        {
            foreach (var value in Enum.GetValues(typeof (MapDecorationAnchor)))
            {
                comboBox1.Items.Add(value);
            }
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

        private void btnFontPicker_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() != DialogResult.OK)
                return;

            tbFont.Text = String.Format("{0} {1} ({2} pt)", fontDialog1.Font.Name, fontDialog1.Font.Style, fontDialog1.Font.SizeInPoints);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void picBoxTextfarbe_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Properties.Settings.Default.AccPlotNoDataColor;
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog() != DialogResult.OK)
                return;
            Properties.Settings.Default.AccPlotNoDataColor = colorDialog1.Color;
            Properties.Settings.Default.Save();
            picBoxTextfarbe.BackColor = colorDialog1.Color;
        }

        private void picBoxBackground_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Properties.Settings.Default.AccPlotNoDataColor;
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog() != DialogResult.OK)
                return;
            Properties.Settings.Default.AccPlotNoDataColor = colorDialog1.Color;
            Properties.Settings.Default.Save();
            picBoxBackground.BackColor = colorDialog1.Color;
        }

        private void picBoxBorderColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Properties.Settings.Default.AccPlotNoDataColor;
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog() != DialogResult.OK)
                return;
            Properties.Settings.Default.AccPlotNoDataColor = colorDialog1.Color;
            Properties.Settings.Default.Save();
            picBoxBorderColor.BackColor = colorDialog1.Color;
        }
    }
}
