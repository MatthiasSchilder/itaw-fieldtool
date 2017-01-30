using System;
using System.Windows.Forms;

namespace fieldtool.View
{
    public partial class FrmEnterTagId : Form
    {
        private int? _tagID;
        public int TagID => _tagID.Value;
        public bool TagIDValid => _tagID.HasValue;

        public FrmEnterTagId()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int tagID;
            if (int.TryParse(textBox1.Text, out tagID))
                _tagID = tagID;

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
