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
        event EventHandler<MRUProjectOpenEventArgs> OpenMRUProject;
        event EventHandler SaveProject;
        event EventHandler CloseProject;
        event EventHandler NewProject;

        event EventHandler ShowProjectProperties;
        event EventHandler<MovebankImportStartArgs> ShowMovebankImport;
        event EventHandler ShowEinstellungen;
        event EventHandler ShowInfo;

        event EventHandler CreateMCPs;

        event EventHandler ShowRawTagInfo;
        event EventHandler ShowRawAccel;
        event EventHandler ShowRawGPS;
        event EventHandler ShowActivityDiagram;
        event EventHandler ShowTagGraphs;
        event EventHandler ExportCurrentMapEnvelope;

        event EventHandler<CurrentDatasetChangedEventArgs> CurrentDatasetChanged;
        event SharpMap.Forms.MapBox.MouseEventHandler MouseMovedOnMap;
        event EventHandler<DatasetCheckedEventArgs> DatasetCheckedChanged;
        event EventHandler<MapDisplayIntervalChangedEventArgs> MapDisplayIntervalChanged;
    }
}
