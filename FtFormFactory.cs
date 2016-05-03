using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fieldtool
{
    class FtFormFactory
    {
        public static DialogResult ShowDialog(Form form)
        {
            form.Icon = Properties.Resources.MainIcon;
            return form.ShowDialog();
        }

        public static void Show(Form form)
        {
            form.Icon = Properties.Resources.MainIcon;
            form.Show();
        }
    }
}
