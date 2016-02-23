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
    public partial class FrmProjectProperties : Form
    {
        private FtProject _project;

        public FrmProjectProperties(FtProject project)
        {
            InitializeComponent();
            _project = project;

            Init();
        }

        private void Init()
        {
            this.Text = String.Format((string) this.Tag, _project.ProjectName);
            this.lblProjName.Text = _project.ProjectName;
            this.lblProjPath.Text = _project.ProjectFilePath;

            string[] arr = new[] {"", "test", "tost"};

            lvRasterkarten.Items.Add(new ListViewItem(arr));

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
