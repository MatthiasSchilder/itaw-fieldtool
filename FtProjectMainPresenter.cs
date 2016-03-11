using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using SharpmapGDAL;

namespace fieldtool
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
        public MapChangedArgs(FtMap map)
        {
            Map = map;
        }
    }

    class ProjectStateArgs : EventArgs
    {
        public bool ProjektGeladen { get; private set; }
        public String Name { get; private set; }
        public ProjectStateArgs(String projektName, bool projektGeladen)
        {
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

    class FtProjectMainPresenter : Presenter<IFtProjectMainView>
    {
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
            View.SaveProject += View_SaveProject;
            View.CloseProject += View_CloseProject;
            View.NewProject += View_NewProject;
            View.ShowProjectProperties += View_ShowProjectProperties;
            View.MouseMovedOnMap += View_MouseMovedOnMap;
            View.ShowInfo += View_ShowInfo;
            View.ShowMovebankImport += View_ShowMovebankImport;
        }

        private void View_ShowMovebankImport(object sender, EventArgs e)
        {
            CommonOpenFileDialog movebankOpenFileDialog = new CommonOpenFileDialog();
            movebankOpenFileDialog.IsFolderPicker = true;

            CommonFileDialogResult dr = movebankOpenFileDialog.ShowDialog();
            if (dr != CommonFileDialogResult.Ok)
                return;

            Project.LoadDatasets(movebankOpenFileDialog.FileName);
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
            InvokeMapChanged(new MapChangedArgs(Map));
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
            InvokeProjectStateChanged(new ProjectStateArgs(Project.ProjectName, true));
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
            InvokeProjectStateChanged(new ProjectStateArgs(null, false));
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

            Project = FtProject.Deserialize(dialog.FileName);

            InvokeMapChanged(new MapChangedArgs(Map));
            InvokeProjectStateChanged(new ProjectStateArgs(Project.ProjectName, true));
        }

        public bool IsProjectLoaded()
        {
            if (Project == null)
                return false;
            return true;
        }
    }
}
