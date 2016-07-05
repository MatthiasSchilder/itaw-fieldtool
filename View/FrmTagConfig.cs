using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using fieldtool.Data.Movebank;
using fieldtool.SharpmapExt.Symbolizers;
using GeoAPI.Geometries;
using SharpMap.Rendering.Symbolizer;

namespace fieldtool.View
{
    public partial class FrmTagConfig : Form
    {
        private FtTransmitterDataset _dataset;

        private Type NewSymbolizerType;
        private Color NewColor;

        public FrmTagConfig(FtTransmitterDataset dataset)
        {
            _dataset = dataset;
            InitializeComponent();
            this.Text = $"Konfiguration für Tag {dataset.TagId}";

            pictureBox1.BackColor = dataset.Visulization.VisulizationColor;

            InitVisualizersCombobox();
            NewSymbolizerType = dataset.Visulization.Symbolizer.GetType();
            NewColor = dataset.Visulization.VisulizationColor;

            int idx = 0;
            foreach (var item in cmboVisualizer.Items)
            {
                if ((Type)((ComboBoxItem) item).Tag == NewSymbolizerType)
                    break;
                idx++;
            }
            cmboVisualizer.SelectedIndex = idx;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = true;
            var dr = colorDialog1.ShowDialog();
            if (dr != DialogResult.OK)
                return;

            pictureBox1.BackColor = colorDialog1.Color;
            NewColor = colorDialog1.Color;
        }

        private void InitVisualizersCombobox()
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Content = "Kreuze";
            item.Tag = typeof (FtCrossPointSymbolizer);
            cmboVisualizer.Items.Add(item);

            item = new ComboBoxItem();
            item.Content = "Punkte";
            item.Tag = typeof(FtDotPointSymbolizer);
            cmboVisualizer.Items.Add(item);

            item = new ComboBoxItem();
            item.Content = "Rechtecke";
            item.Tag = typeof(FtRectanglePointSymbolizer);
            cmboVisualizer.Items.Add(item);

            item = new ComboBoxItem();
            item.Content = "Dreiecke";
            item.Tag = typeof(FtTriangleePointSymbolizer);
            cmboVisualizer.Items.Add(item);

            item = new ComboBoxItem();
            item.Content = "Kreuze + Label";
            item.Tag = typeof(FtLabeledCrossPointSymbolizer);
            cmboVisualizer.Items.Add(item);

            cmboVisualizer.DisplayMember = "Content";
        }

        private void cmboVisualizer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var type = cmboVisualizer.SelectedItem as ComboBoxItem;
            if (type == null)
                return;

            NewSymbolizerType = (Type)(type.Tag);
        }

        private void FrmTagConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            _dataset.Visulization.VisulizationColor = NewColor;
            _dataset.Visulization.Symbolizer = (ISymbolizer<IPuntal>)Activator.CreateInstance(NewSymbolizerType, NewColor);
        }
    }
}
