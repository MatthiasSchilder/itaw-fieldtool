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
    public partial class FrmMCPPercentage : Form
    {
        public int PercentageMCP
        {
            get
            {
                return (int) numericUpDown1.Value;
            }
        }
        public FrmMCPPercentage()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(PercentageMCP == 0)
            {
                MessageBox.Show("0% nicht zulässig");
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
