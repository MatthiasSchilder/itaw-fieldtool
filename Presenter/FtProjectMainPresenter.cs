using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using fieldtool.Data;
using fieldtool.View;
using GeoAPI.Geometries;
using Microsoft.WindowsAPICodePack.Dialogs;
using SharpmapGDAL;

namespace fieldtool.Presenter
{
    class CursorCoordsChangedArgs : EventArgs
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
        public CursorCoordsChangedArgs(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
    class MapChangedArgs : EventArgs
    {
        public FtMap Map { get; private set; }
        public bool ZoomToFit { get; private set; }
        public MapChangedArgs(FtMap map, bool zoomToFit = false)
        {
            Map = map;
            ZoomToFit = zoomToFit;
        }
    }

    class ProjectStateArgs : EventArgs
    {
        public bool ProjektGeladen { get; private set; }
        public String Name { get; private set; }
        public String FullFilePath { get; private set; }
        public ProjectStateArgs(String fullFilepath, String projektName, bool projektGeladen)
        {
            FullFilePath = fullFilepath;
            ProjektGeladen = projektGeladen;
            Name = projektName;
        }
    }

    class MovebankImportedArgs : EventArgs
    {
        public List<FtTransmitterDataset> Datasets { get; set; }
        public MovebankImportedArgs(List<FtTransmitterDataset> datasets)
        {
            Datasets = datasets;
        }
    }

    class MapEnvelopeExportRequestedArgs : EventArgs
    {
        public List<FtTransmitterDataset> ActiveDatasets { get; private set; }
        public MapEnvelopeExportRequestedArgs(List<FtTransmitterDataset> activeDatasets)
        {
            ActiveDatasets = activeDatasets;
        }
    }

    class FtProjectMainPresenter : Presenter<IFtProjectMainView>
    {
        public EventHandler<MapDisplayIntervalChangedEventArgs> rMapDisplayIntervalChanged;
        public void InvokeMapDisplayIntervalChanged(MapDisplayIntervalChangedEventArgs e)
        {
            var handler = rMapDisplayIntervalChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public EventHandler<MapEnvelopeExportRequestedArgs> MapEnvelopeExportRequested;
        public void InvokeMapEnvelopeExportRequested(MapEnvelopeExportRequestedArgs e)
        {
            var handler = MapEnvelopeExportRequested;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public EventHandler<CursorCoordsChangedArgs> CursorCoordinatesChanged;
        public void InvokeCursorCoordinatesChanged(CursorCoordsChangedArgs e)
        {
            var handler = CursorCoordinatesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public EventHandler<MapChangedArgs> MapChanged;
        public void InvokeMapChanged(MapChangedArgs e)
        {
            var handler = MapChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public EventHandler<ProjectStateArgs> ProjectStateChanged;
        public void InvokeProjectStateChanged(ProjectStateArgs e)
        {
            var handler = ProjectStateChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public EventHandler<MovebankImportedArgs> MovebankImported;
        public void InvokeMovebankImported(MovebankImportedArgs e)
        {
            var handler = MovebankImported;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private FtProject Project { get; set; }

        private FtMap _map;
        private bool MapNeedsRefresh { get; set; }

        private FtTransmitterDataset CurrentDataset { get; set; }
        private bool CurrentDatasetAvailable
        {
            get { return CurrentDataset != null; }
        }

        private FtMap Map
        {
            get
            {
                if (_map == null || MapNeedsRefresh)
                {
                    _map = new FtMap(Project);
                    MapNeedsRefresh = false;
                }
                return _map;
            }
        }

        public FtProjectMainPresenter(IFtProjectMainView view) : base(view)
        {
              
        }

        protected override void OnViewInitialize(object sender, EventArgs e)
        {
            base.OnViewInitialize(sender, e);

            View.OpenProject += View_OpenProject;
            View.OpenMRUProject += View_OpenMRUProject;
            View.SaveProject += View_SaveProject;
            View.CloseProject += View_CloseProject;
            View.NewProject += View_NewProject;
            View.ShowProjectProperties += View_ShowProjectProperties;
            View.ShowEinstellungen += View_ShowEinstellungen;
            View.MouseMovedOnMap += View_MouseMovedOnMap;
            View.ShowInfo += View_ShowInfo;
            View.ShowMovebankImport += View_ShowMovebankImport;
            View.ShowRawTagInfo += View_ShowRawTagInfo;
            View.ShowRawAccel += View_ShowRawAccel;
            View.ShowRawGPS += View_ShowRawGPS;
            View.CurrentDatasetChanged += View_CurrentDatasetChanged;
            View.ShowActivityDiagram += View_ShowActivityDiagram;
            View.ShowTagGraphs += View_ShowTagGraphs;
            View.DatasetCheckedChanged += View_DatasetCheckedChanged;
            View.MapDisplayIntervalChanged += View_MapDisplayIntervalChanged;
            View.CreateMCPs += View_CreateMCPs;
            View.ExportCurrentMapEnvelope += View_ExportCurrentMapEnvelope;
        }

        private void View_ExportCurrentMapEnvelope(object sender, EventArgs e)
        {
            var activeTags = Project.Datasets.Where(dataset => dataset.Active).ToList();
            InvokeMapEnvelopeExportRequested(new MapEnvelopeExportRequestedArgs(activeTags));
        }

        private void View_MapDisplayIntervalChanged(object sender, MapDisplayIntervalChangedEventArgs e)
        {
            
            Project.SetIntervalFilter(CurrentDataset, e.IntervalStart, e.IntervalEnd);
            //DataChangedEventHandler(this, new EventArgs());
            InvokeMapChanged(new MapChangedArgs(Map));
        }

        private void View_CreateMCPs(object sender, EventArgs e)
        {
            foreach (var dataset in Project.Datasets.Where(dataset => dataset.Active))
            {
                FtMultipoint multipoint = new FtMultipoint(dataset.GPSData.
                    GpsSeries.Where(gps => gps.IsValid()).Select(gps => new Coordinate(gps.Rechtswert.Value, gps.Hochwert.Value)).ToList());
                var mcp = multipoint.MinimumConvexPolygon();
                mcp.Vertices.Add(mcp.Vertices[0]);
                Map.AddPolygonalData(dataset, mcp);
            }

            InvokeMapChanged(new MapChangedArgs(Map));
        }

        private void View_ShowEinstellungen(object sender, EventArgs e)
        {
            FtFormFactory.Show(new FrmSettings());
        }

        private void View_DatasetCheckedChanged(object sender, DatasetCheckedEventArgs e)
        {
            Project.SetDatasetState(e.TagId, e.Checked);
            InvokeMapChanged(new MapChangedArgs(Map));
        }

        private void View_OpenMRUProject(object sender, MRUProjectOpenEventArgs e)
        {
            if (IsProjectLoaded())
            {
                MessageBox.Show("Bitte schließen Sie zunächst das geöffnete Projekt.");
                return;
            }
            if (!OpenProject(e.FullFilePath))
                MessageBox.Show(
                    $"Die Datei {Path.GetFileName(e.FullFilePath)} existiert an der angegebenen Stelle nicht.");
        }

        private void View_ShowTagGraphs(object sender, EventArgs e)
        {
            if (!CurrentDatasetAvailable)
                return;
            FrmGraph frm = new FrmGraph(CurrentDataset);
            FtFormFactory.Show(frm);
        }

        private void View_ShowActivityDiagram(object sender, EventArgs e)
        {
            if (!CurrentDatasetAvailable)
                return;

            var frmAccPlot = new FrmBurstActivityVisu(CurrentDataset,
                Properties.Settings.Default.AccPlotNoDataColor);
            FtFormFactory.Show(frmAccPlot);
        }
        
        private void View_CurrentDatasetChanged(object sender, CurrentDatasetChangedEventArgs e)
        {
            CurrentDataset = !e.CurrentTagId.HasValue ? null : Project.GetTransmitterDataset(e.CurrentTagId.Value);
            if (CurrentDataset == null)
                return;
            if (CurrentDataset.GPSData.GpsSeries.Count == 0)
                return;

            var maxDate = CurrentDataset.GPSData.GpsSeries.Max(series => series.StartTimestamp);
            var minDate = CurrentDataset.GPSData.GpsSeries.Min(series => series.StartTimestamp);
            InvokeMapDisplayIntervalChanged(new MapDisplayIntervalChangedEventArgs(minDate, maxDate));
        }

        private void View_ShowRawGPS(object sender, EventArgs e)
        {
            if (!CurrentDatasetAvailable)
                return;
            System.Diagnostics.Process.Start(CurrentDataset.Fileset.GetFilepathForFunction(FtFileFunction.GPSData));
        }

        private void View_ShowRawAccel(object sender, EventArgs e)
        {
            if (!CurrentDatasetAvailable)
                return;
            System.Diagnostics.Process.Start(CurrentDataset.Fileset.GetFilepathForFunction(FtFileFunction.AccelData));
        }

        private void View_ShowRawTagInfo(object sender, EventArgs e)
        {
            if (!CurrentDatasetAvailable)
                return;
            System.Diagnostics.Process.Start(CurrentDataset.Fileset.GetFilepathForFunction(FtFileFunction.TagInfo));
        }

        private void View_ShowMovebankImport(object sender, EventArgs e)
        {
            if (Project == null)
                return;
            var args = (MovebankImportStartArgs) e;
            if (args.EinzelImport)
            {
                CommonOpenFileDialog movebankOpenFileDialog = new CommonOpenFileDialog {Multiselect = true};
                if (Project.DefaultMovebankLookupPathAvailable)
                    movebankOpenFileDialog.DefaultDirectory = Project.DefaultMovebankLookupPath;
                movebankOpenFileDialog.Filters.Add(
                    new CommonFileDialogFilter("Movebank", "*.txt"
                    ));

                CommonFileDialogResult dr = movebankOpenFileDialog.ShowDialog();
                if (dr != CommonFileDialogResult.Ok)
                    return;
                Project.MovebankFilesets = FtFileset.FileSetFromMultiselect(movebankOpenFileDialog.FileNames.ToList());
                ImportMovebank();
            }
            else
            {
                CommonOpenFileDialog movebankOpenFileDialog = new CommonOpenFileDialog
                {
                    IsFolderPicker = true,
                    Multiselect = false
                };
                if (Project.DefaultMovebankLookupPathAvailable)
                    movebankOpenFileDialog.DefaultDirectory = Project.DefaultMovebankLookupPath;
                CommonFileDialogResult dr = movebankOpenFileDialog.ShowDialog();
                if (dr != CommonFileDialogResult.Ok)
                    return;

                Project.MovebankFilesets = FtFileset.FileSetFromDirectory(movebankOpenFileDialog.FileName);
                ImportMovebank();
            }
        }

        private void ImportMovebank()
        {
            Project.LoadDatasets();
            InvokeMovebankImported(new MovebankImportedArgs(Project.Datasets));
        }

        private void View_ShowInfo(object sender, EventArgs e)
        {
            FtFormFactory.ShowDialog(new FrmInfo());
        }

        private void View_MouseMovedOnMap(GeoAPI.Geometries.Coordinate worldPos, MouseEventArgs imagePos)
        {
            InvokeCursorCoordinatesChanged(new CursorCoordsChangedArgs(worldPos.X, worldPos.Y, 0));
        }

        private void View_ShowProjectProperties(object sender, EventArgs e)
        {
            if (!IsProjectLoaded())
            {
                MessageBox.Show("Die Projekteigenschaften können nicht angezeigt werden, da kein Projekt geöffnet ist.");
                return;
            }

            FrmProjectProperties frm = new FrmProjectProperties(Project);
            FtFormFactory.ShowDialog(frm);

            MapNeedsRefresh = true;
            InvokeMapChanged(new MapChangedArgs(Map, true));
        }

        private void View_NewProject(object sender, EventArgs e)
        {
            if (IsProjectLoaded())
            {
                MessageBox.Show("Bitte schließen Sie zunächst das geöffnete Projekt.");
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "FieldTool-Projekt|*.ftproj";

            DialogResult dr = dialog.ShowDialog();
            if (dr != DialogResult.OK)
                return;

            Project = new FtProject(dialog.FileName);
            Project.Save();
            ProjectionManager.SetSourceProjection(Project.EPSGSourceProjection);
            ProjectionManager.SetTargetProjection(Project.EPSGTargetProjection);
            InvokeProjectStateChanged(new ProjectStateArgs(Project.ProjectFilePath, Project.ProjectName, true));
        }

        private void View_CloseProject(object sender, EventArgs e)
        {
            if (!IsProjectLoaded())
            {
                MessageBox.Show("Es ist kein Projekt geöffnet.");
                return;
            }

            DialogResult dr = MessageBox.Show("Soll das Projekt geschlossen werden?", "Schließen?",
                MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Project = null;
            }

            InvokeMapChanged(new MapChangedArgs(null));
            InvokeProjectStateChanged(new ProjectStateArgs(null, null, false));
        }

        private void View_SaveProject(object sender, EventArgs e)
        {
            if (!IsProjectLoaded())
            {
                MessageBox.Show("Es ist kein Projekt geöffnet.");
                return;
            }
            Project.Save();
        }

        private void View_OpenProject(object sender, EventArgs e)
        {
            if (IsProjectLoaded())
            {
                MessageBox.Show("Bitte schließen Sie zunächst das geöffnete Projekt.");
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "FieldTool-Projekt|*.ftproj";
            DialogResult dr = dialog.ShowDialog();
            if (dr != DialogResult.OK)
                return;

            OpenProject(dialog.FileName);
        }

        private bool OpenProject(String fullFilePath)
        {
            if (!File.Exists(fullFilePath))
                return false;

            Project = FtProject.Open(fullFilePath);

            ProjectionManager.SetSourceProjection(Project.EPSGSourceProjection);
            ProjectionManager.SetTargetProjection(Project.EPSGTargetProjection);

            InvokeMapChanged(new MapChangedArgs(Map, true));
            InvokeProjectStateChanged(new ProjectStateArgs(Project.ProjectFilePath, Project.ProjectName, true));

            return true;
        }

        public bool IsProjectLoaded()
        {
            if (Project == null)
                return false;
            return true;
        }
    }
}

