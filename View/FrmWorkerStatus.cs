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
    public partial class FrmWorkerStatus : Form
    {
        public FrmWorkerStatus()
        {
            InitializeComponent();

            var lvi = new ListViewItem();
            var lvi2 = new ListViewItem();

            for (int i = 0; i < 3; i++)
            {
                var lvis1 = new ListViewItem.ListViewSubItem();
                lvis1.Text = "blub;";
                lvis1.Name = "colHeaderWorkDescr";

                var lvis2 = new ListViewItem.ListViewSubItem();
                lvis2.Text = "blub";

                lvi.SubItems.Add(lvis1);
                lvi2.SubItems.Add(lvis2);
            }
            
            listView1.Items.Add(lvi);
            listView1.Items.Add(lvi2);
        }

        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void listView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                var margin = 3;
                var bounds = e.Bounds;
                var newLoc = new Point(bounds.Location.X + margin, bounds.Location.Y + margin);

                var newRect = new Rectangle(newLoc, new Size(bounds.Width - 2* margin, bounds.Height - 2* margin));

                var halfrect = new Rectangle(newRect.Location, new Size(newRect.Width / 2, newRect.Height));

                e.Graphics.FillRectangle(Brushes.LightGray, newRect);
                e.Graphics.FillRectangle(Brushes.DodgerBlue, halfrect);
                

            }
            else
            {
                e.DrawDefault = true;
            }
        }

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void listView1_DrawColumnHeader_1(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }
    }
}
