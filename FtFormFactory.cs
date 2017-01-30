using System.Windows.Forms;

namespace fieldtool
{
    class FtFormFactory
    {
        public static DialogResult ShowDialog(Form form)
        {
            form.Icon = Properties.Resources.itaw;
            return form.ShowDialog();
        }

        public static void Show(Form form)
        {
            form.Icon = Properties.Resources.itaw;
            form.Show();
        }
    }
}
