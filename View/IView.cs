using System;

namespace fieldtool.View
{
    public interface IView
    {
        event EventHandler Initialize;
        event EventHandler Load;
    }
}
