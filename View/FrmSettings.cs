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
            chkShowMassstab.Checked = Properties.Settings.Default.MapScalebarActive;
            InitAnchorCombobox();
            InitListboxLegendContents();
            InitWithDefaultValues();
        }

        private void InitWithDefaultValues()
        {
            UpdateFontTextbox();
            chkLegendActive.Checked = Properties.Settings.Default.MapLegendActive;
            numAlpha.Value = (decimal)Properties.Settings.Default.MapLegendBackgroundAlpha;
            picBoxTextfarbe.BackColor = Properties.Settings.Default.MapLegendTextColor;
            picBoxBackground.BackColor = Properties.Settings.Default.MapLegendBackgroundColor;
            picBoxBorderColor.BackColor = Properties.Settings.Default.MapLegendBorderColor;
            comboBox1.SelectedItem = Properties.Settings.Default.MapLegendAnchor;
            chkRoundEdges.Checked = Properties.Settings.Default.MapLegendBorderRoundEdges;
        }

        private void InitAnchorCombobox()
        {
            foreach (var value in Enum.GetValues(typeof (MapDecorationAnchor)))
            {
                comboBox1.Items.Add(value);
            }
        }

        private void InitListboxLegendContents()
        {
            checkedListBox1.Items.Add("Farbe", true);
            checkedListBox1.Items.Add("Tag-Bezeichner (ID)", true);
            checkedListBox1.Items.Add("dargestellter Zeitraum", true);
            checkedListBox1.Items.Add("Freitext", true);
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
            Properties.Settings.Default.MapScalebarActive = chkShowMassstab.Checked;
            Properties.Settings.Default.Save();
        }

        private void btnFontPicker_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() != DialogResult.OK)
                return;

            Properties.Settings.Default.MapLegendFont = fontDialog1.Font;
            Properties.Settings.Default.Save();

            tbFont.Text = $"{fontDialog1.Font.Name} {fontDialog1.Font.Style} ({fontDialog1.Font.SizeInPoints} pt)";
        }

        private void UpdateFontTextbox()
        {
            var font = Properties.Settings.Default.MapLegendFont;
            tbFont.Text = String.Format("{0} {1} ({2} pt)", font.Name, font.Style, font.SizeInPoints);
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
            Properties.Settings.Default.MapLegendTextColor = colorDialog1.Color;
            Properties.Settings.Default.Save();
            picBoxTextfarbe.BackColor = colorDialog1.Color;
        }

        private void picBoxBackground_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Properties.Settings.Default.AccPlotNoDataColor;
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog() != DialogResult.OK)
                return;
            Properties.Settings.Default.MapLegendBackgroundColor = colorDialog1.Color;
            Properties.Settings.Default.Save();
            picBoxBackground.BackColor = colorDialog1.Color;
        }

        private void picBoxBorderColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Properties.Settings.Default.AccPlotNoDataColor;
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog() != DialogResult.OK)
                return;
            Properties.Settings.Default.MapLegendBorderColor = colorDialog1.Color;
            Properties.Settings.Default.Save();
            picBoxBorderColor.BackColor = colorDialog1.Color;
        }

        private void chkLegendActive_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MapLegendActive = chkLegendActive.Checked;
            Properties.Settings.Default.Save();
        }

        private void chkRoundEdges_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MapLegendBorderRoundEdges = chkRoundEdges.Checked;
            Properties.Settings.Default.Save();
        }

        private void numAlpha_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MapLegendBackgroundAlpha = (float)numAlpha.Value;
            Properties.Settings.Default.Save();
        }
    }
}

