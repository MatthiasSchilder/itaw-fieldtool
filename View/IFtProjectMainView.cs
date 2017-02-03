using System;
using fieldtool.View;

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
        event EventHandler ShowLoggerBinImport;
        event EventHandler ShowEinstellungen;
        event EventHandler ShowInfo;

        event EventHandler<CreateMCPEventArgs> CreateMCPs;
        event EventHandler CreateKDE;

        event EventHandler ShowRawTagInfo;
        event EventHandler ShowRawAccel;
        event EventHandler ShowRawGPS;
        event EventHandler ShowActivityDiagram;
        event EventHandler ShowActivityVerlauf;
        event EventHandler<ContextMenuItemClickedEventArgs> ShowTagConfig;
        event EventHandler<ContextMenuItemClickedEventArgs> ShowTagTabelle;
        event EventHandler<ContextMenuItemClickedEventArgs> ZoomToTag;
        event EventHandler ShowTagGraphs;
        event EventHandler ExportCurrentMapEnvelope;
        event EventHandler ExportAsShape;

        event EventHandler<CurrentDatasetChangedEventArgs> CurrentDatasetChanged;
        event SharpMap.Forms.MapBox.MouseEventHandler MouseMovedOnMap;
        event EventHandler<DatasetCheckedEventArgs> DatasetCheckedChanged;
        event EventHandler<MapDisplayIntervalChangedEventArgs> MapDisplayIntervalChanged;
        
    }
}
