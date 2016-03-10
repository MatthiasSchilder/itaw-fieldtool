using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool
{
    public interface IFtProjectMainView : IView
    {
        event EventHandler OpenProject;
        event EventHandler SaveProject;
        event EventHandler CloseProject;
        event EventHandler NewProject;

        event EventHandler ShowProjectProperties;
        event EventHandler ShowMovebankImport;
        event EventHandler ShowEinstellungen;
        event EventHandler ShowInfo;


        event SharpMap.Forms.MapBox.MouseEventHandler MouseMovedOnMap;
    }
}
