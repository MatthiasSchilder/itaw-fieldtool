using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fieldtool.Controls
{
    public class WorkaroundTreeView : TreeView
    {
        // https://social.msdn.microsoft.com/Forums/windows/en-US/9d717ce0-ec6b-4758-a357-6bb55591f956/possible-bug-in-net-treeview-treenode-checked-state-inconsistent?forum=winforms
        // https://support.microsoft.com/en-us/kb/192188

        ////  Private Declare Function GetWindowLong Lib "user32" Alias _
        //// "GetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long) As Long

        ////Private Declare Function SetWindowLong Lib "user32" Alias _
        //// "SetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long, _
        //// ByVal dwNewLong As Long) As Long

        //[DllImport("user32.dll", SetLastError = true)]
        //static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        //[DllImport("user32.dll")]
        //static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        //public WorkaroundTreeView()
        //{
        //    var curStyle = GetWindowLong(this.Handle, -16);
        //    SetWindowLong(this.Handle, -16, curStyle | 0x100);
        //}




        protected override void WndProc(ref Message m)
        {
            // Suppress WM_LBUTTONDBLCLK
            if (m.Msg == 0x203) { m.Result = IntPtr.Zero; }
            else base.WndProc(ref m);
        }
    }
}
