using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using fieldtool.Data.Movebank;
using fieldtool.Presenter;
using GeoAPI.Geometries;
using SharpMap.Data;
using SharpMap.Forms;

namespace fieldtool.View
{
    public partial class FrmMain : Form, IFtProjectMainView
    {
        private FtProjectMainPresenter Presenter { get; }

        private const String WindowTitleNoProject = "itaw Fieldtool v{0}";
        private const String WindowTitleProject = "itaw Fieldtool v{0} - {1}";

        private FrmImportingBusy ProgressWindows;

        public FrmMain()
        {
            Presenter = new FtProjectMainPresenter(this);
            InitializeComponent();
            RegisterPresenterEvents();
            InvokeInitialize(new EventArgs());
            SetWindowTitle(false, null);

            mapBox1.MouseMove += MouseMovedOnMap;
            mapBox1.MapChanging += MapBox1OnMapChanging;
            dateIntervalPicker1.IntervalChanged += DateIntervalPicker1_IntervalChanged;
            AddRecentlyUsedProjects();
        }

        private void MapBox1OnMapChanging(object sender, CancelEventArgs cancelEventArgs)
        {
            mapBox1.ShowProgressUpdate = true;
        }

        private void DateIntervalPicker1_IntervalChanged(object sender, EventArgs e)
        {
            InvokeMapDisplayIntervalChanged(new MapDisplayIntervalChangedEventArgs(dateIntervalPicker1.StartTimpestamp, dateIntervalPicker1.EndTimestamp));
        }

        private void AddRecentlyUsedProjects()
        {
            var begin = GetStartIdxMRUList();
            RemoveMRUEntries(dateiToolStripMenuItem.DropDownItems, begin);

            if (!String.IsNullOrEmpty(Properties.Settings.Default.MRUFile1))
                InsertMenuEntry(dateiToolStripMenuItem.DropDownItems, ++begin, Properties.Settings.Default.MRUFile1);
            if (!String.IsNullOrEmpty(Properties.Settings.Default.MRUFile2))
                InsertMenuEntry(dateiToolStripMenuItem.DropDownItems, ++begin, Properties.Settings.Default.MRUFile2);
            if (!String.IsNullOrEmpty(Properties.Settings.Default.MRUFile3))
                InsertMenuEntry(dateiToolStripMenuItem.DropDownItems, ++begin, Properties.Settings.Default.MRUFile3);

        }

        private int GetStartIdxMRUList()
        {
            int i = 0;
            foreach (ToolStripItem item in dateiToolStripMenuItem.DropDownItems)
                if (item.Name != "spacerInsertMRUAfter")
                    i++;
                else
                    break;
            return i;
        }

        private void RemoveMRUEntries(ToolStripItemCollection collection, int beginIdx)
        {
            List<ToolStripItem> mruMenuItems = new List<ToolStripItem>();
            for (int i = beginIdx + 1; i < collection.Count; i++)
            {
                var item = collection[i];
                if (item is ToolStripSeparator)
                {
                    break;
                }
                mruMenuItems.Add(item);                    
            }
            foreach(var mruMenuItem in mruMenuItems)
                collection.Remove(mruMenuItem);
        }

        private bool first = true;
        private void InsertMenuEntry(ToolStripItemCollection collection, int idx, string content)
        {
            var menuItem = new ToolStripMenuItem(content);
            menuItem.Click += MRUMenuItemClick;
            menuItem.Tag = content;
            if(first)
            {
                menuItem.ShortcutKeys = Keys.F4;
                first = false;
            }
            collection.Insert(idx, menuItem);
        }

        private void MRUMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem) sender;
            InvokeMRUOpenProject(new MRUProjectOpenEventArgs((String) item.Tag));
        }

        private void AddMRUItem(string projectfilePath)
        {
            List<String> currentMRUEntries = new List<string>()
            {
                Properties.Settings.Default.MRUFile1,
                Properties.Settings.Default.MRUFile2,
                Properties.Settings.Default.MRUFile3
            };

            if (currentMRUEntries.Contains(projectfilePath))
            {
                var idx = currentMRUEntries.IndexOf(projectfilePath);
                var item = currentMRUEntries[idx];
                currentMRUEntries.RemoveAt(idx);
                currentMRUEntries.Insert(0, item);
            }
            else // Project taucht noch nicht in der MRU-List auf.
            {
                currentMRUEntries[2] = currentMRUEntries[1];
                currentMRUEntries[1] = currentMRUEntries[0];
                currentMRUEntries[0] = projectfilePath;
            }
            Properties.Settings.Default.MRUFile1 = currentMRUEntries[0];
            Properties.Settings.Default.MRUFile2 = currentMRUEntries[1];
            Properties.Settings.Default.MRUFile3 = currentMRUEntries[2];
            Properties.Settings.Default.Save();
            AddRecentlyUsedProjects();
        }

        private void RegisterPresenterEvents()
        {
            Presenter.CursorCoordinatesChanged += CursorCoordinatesChanged;
            Presenter.MapChanged += MapChanged;
            Presenter.ProjectStateChanged += ProjectStateChanged;
            Presenter.MovebankImported += MovebankImported;
            Presenter.rMapDisplayIntervalChanged += rrMapDisplayIntervalChanged;
            Presenter.MapEnvelopeExportRequested += MapEnvelopeExportRequested;
            Presenter.SetupProgress += SetupProgress;
            Presenter.StepProgress += StepProgress;
            Presenter.FinishProgress += FinishProgress;
            Presenter.MapZoomToEnvelopeRequested += MapZoomToEnvelopeRequested;
        }

        private void MapZoomToEnvelopeRequested(object sender, MapZoomToEnvelopeArgs mapZoomToEnvelopeArgs)
        {
            mapBox1.Map.ZoomToBox(mapZoomToEnvelopeArgs.Env);
            mapBox1.Refresh();
        }

        private void FinishProgress(object sender, EventArgs eventArgs)
        {
            ProgressWindows.Close();
        }

        private void StepProgress(object sender, StepProgressArgs stepProgressArgs)
        {
            ProgressWindows.Invoke(new MethodInvoker(() => ProgressWindows.Step((stepProgressArgs.TagName))));
            //ProgressWindows.Step(stepProgressArgs.TagName);
            //Debug.WriteLine("stepping");
        }

        private void SetupProgress(object sender, SetupProgressArgs setupProgressArgs)
        {
            ProgressWindows = new FrmImportingBusy(setupProgressArgs.NumTags);
            ProgressWindows.Location = GetProgressbarLocation(this.Location, this.Size, ProgressWindows.Size);
            ProgressWindows.StartPosition = FormStartPosition.Manual;
            ProgressWindows.Show(this);
        }

        private Point GetProgressbarLocation(Point location, Size mainFormSize, Size importFormSize)
        {
            Point mainFormCenterPoint = new Point(location.X + mainFormSize.Width / 2, location.Y + mainFormSize.Height / 2);
            mainFormCenterPoint.X -= importFormSize.Width/2;
            mainFormCenterPoint.Y -= importFormSize.Height/2;

            return mainFormCenterPoint;
        }

        private void MapEnvelopeExportRequested(object sender, MapEnvelopeExportRequestedArgs args)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PNG-Grafik|*.png";
            dialog.FileName = "Karte" + GetMapExportFilename(args.ActiveDatasets);
            DialogResult dr = dialog.ShowDialog();

            if (dr != DialogResult.OK)
                return;

            var width = mapBox1.Image.Width;
            var height = mapBox1.Image.Height;

            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);

            

            mapBox1.Map.RenderMap(g);

            bmp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
        }

        private void rrMapDisplayIntervalChanged(object sender, MapDisplayIntervalChangedEventArgs movebankImportedArgs)
        {
            var maxDate = movebankImportedArgs.IntervalEnd;
            var minDate = movebankImportedArgs.IntervalStart;
            dateIntervalPicker1.SetDateInterval(minDate, maxDate);
        }

        private void MovebankImported(object sender, MovebankImportedArgs movebankImportedArgs)
        {
            PopulateTreeView(movebankImportedArgs.Datasets);
        }

        private void PopulateTreeView(List<FtTransmitterDataset> datasets)
        {
            PopulateImageList(datasets);
            treeViewTagList.Nodes.Clear();
            int i = 0;
            foreach (var dataset in datasets)
            {
                var parentTn = CreateParentTreeNode(dataset, i++);
                var punktwolkteTn = CreatePunktwolkeTreeNode(dataset, parentTn.ImageIndex);

                
                treeViewTagList.Nodes.Add(parentTn);
                parentTn.Nodes.Add(punktwolkteTn);
            }           

            

            treeViewTagList.ExpandAll();
        }

        private TreeNode CreateParentTreeNode(FtTransmitterDataset dataset, int i)
        {
            //var tn = treeViewTagList.Nodes.Add(CreateTreeViewNodeDescriptor(dataset));
            var tn = new TreeNode(CreateTreeViewNodeDescriptor(dataset));
            tn.Tag = dataset.TagId;
            tn.Checked = dataset.Active;
            tn.ImageIndex = i;
            tn.SelectedImageIndex = i;

            return tn;
        }

        private TreeNode CreatePunktwolkeTreeNode(FtTransmitterDataset dataset, int imageIndex)
        {
            var tn = new TreeNode("Punktwolke");
            tn.Tag = dataset.TagId;
            tn.Checked = dataset.Active;
            tn.ImageIndex = imageIndex;
            tn.SelectedImageIndex = imageIndex;

            return tn;
        }

        private TreeNode GetParentTreeNodeForTag(FtTransmitterDataset dataset)
        {
            if (treeViewTagList.Nodes.Count == 0)
                return null;

            foreach (TreeNode treeNode in treeViewTagList.Nodes)
            {
                if ((int) treeNode.Tag == dataset.TagId)
                    return treeNode;
            }
            return null;
        }

        private String CreateTreeViewNodeDescriptor(FtTransmitterDataset dataset)
        {
            string fmt = "{0} ({1})";
            return String.Format(fmt, dataset.TagId, dataset.GPSData.GpsSeries.GetLatestGpsDataEntry().StartTimestamp);
        }

        private void PopulateImageList(List<FtTransmitterDataset> datasets)
        {
            imageListColorKeys.Images.Clear();
            foreach (var dataset in datasets)
            {
                var img = CreateMonochromaticImage(dataset.Visulization.VisulizationColor);
                imageListColorKeys.Images.Add(img);
            }
            
        }

        private Image CreateMonochromaticImage(Color color)
        {
            Bitmap bmp = new Bitmap(16, 16);
            for (int i = 2; i < 13; i++)
            {
                for (int j = 2; j < 13; j++)
                {
                    bmp.SetPixel(i, j, color);
                }
            }
            return bmp;
        }

        private void ProjectStateChanged(object sender, ProjectStateArgs projectStateArgs)
        {
            if (projectStateArgs.ProjektGeladen)
            {
                SetWindowTitle(true, projectStateArgs.FullFilePath);
                AddMRUItem(projectStateArgs.FullFilePath);
            }
            else
            {
                SetWindowTitle(false, null);
            }
        }

        private void SetWindowTitle(bool projektGeladen, string projektName)
        {
            if (projektGeladen)
            {
                this.Text = String.Format(WindowTitleProject, Application.ProductVersion, projektName);
            }
            else
            {
                this.Text = String.Format(WindowTitleNoProject, Application.ProductVersion);
            }
        }

        private void MapChanged(object sender, MapChangedArgs eventArgs)
        {
            mapBox1.Map = eventArgs.Map;
            if (mapBox1.Map == null)
                return;

            if(eventArgs.ZoomToFit)
            {
                try
                {
                    mapBox1.Map.ZoomToExtents();
                }
                catch (Exception)
                {
                }
            }

            mapBox1.Refresh();
        }

        private void CursorCoordinatesChanged(object sender, CursorCoordsChangedArgs cursorCoordsChangedArgs)
        {
            var xDisplay = Math.Round(cursorCoordsChangedArgs.X, 4);
            var yDisplay = Math.Round(cursorCoordsChangedArgs.Y, 4);
            statusLabelCoords.Text = String.Format((string)statusLabelCoords.Tag, xDisplay, yDisplay);
        }

        public void SetActiveTool(SharpMap.Forms.MapBox.Tools tool)
        {
            this.mapBox1.ActiveTool = tool;
        }

        private bool _statePanning = false;


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (_statePanning)
            {
                SetActiveTool(SharpMap.Forms.MapBox.Tools.None);
                _statePanning = false;
            }
            else
            {
                SetActiveTool(SharpMap.Forms.MapBox.Tools.Pan);
                _statePanning = true;
            }
                
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void eigenschaftenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowProjectProperties(new EventArgs());
        }

        private void movebankLadenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowMovebankImport(new MovebankImportStartArgs(false));
        }

        private void movebankEinzelsetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowMovebankImport(new MovebankImportStartArgs(true));
        }

        private void einstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowEinstellungen(new EventArgs());
        }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeNewProject(new EventArgs());
        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeOpenProject(new EventArgs());
        }

        private void schließenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeCloseProject(new EventArgs());
        }

        private void speichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeSaveProject(new EventArgs());
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            InvokeSaveProject(new EventArgs());
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            InvokeOpenProject(new EventArgs());
        }

        public event EventHandler OpenProject;
        public void InvokeOpenProject(EventArgs e)
        {
            EventHandler handler = OpenProject;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<MRUProjectOpenEventArgs> OpenMRUProject;
        public void InvokeMRUOpenProject(MRUProjectOpenEventArgs e)
        {
            EventHandler<MRUProjectOpenEventArgs> handler = OpenMRUProject;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CloseProject;

        public void InvokeCloseProject(EventArgs e)
        {
            EventHandler handler = CloseProject;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler SaveProject;
        public void InvokeSaveProject(EventArgs e)
        {
            EventHandler handler = SaveProject;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        public event EventHandler NewProject;
        public void InvokeNewProject(EventArgs e)
        {
            EventHandler handler = NewProject;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public event EventHandler ShowProjectProperties;
        public void InvokeShowProjectProperties(EventArgs e)
        {
            EventHandler handler = ShowProjectProperties;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        
        public event EventHandler<MovebankImportStartArgs> ShowMovebankImport;
        public void InvokeShowMovebankImport(MovebankImportStartArgs e)
        {
            EventHandler<MovebankImportStartArgs> handler = ShowMovebankImport;
            if (handler != null)
            {
                handler(this, e);
            }
        }

       
        public event EventHandler ShowLoggerBinImport;
        public void InvokeShowLoggerBinImport(EventArgs e)
        {
            EventHandler handler = ShowLoggerBinImport;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ShowEinstellungen;
        

        public void InvokeShowEinstellungen(EventArgs e)
        {
            EventHandler handler = ShowEinstellungen;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ShowInfo;


        public void InvokeShowInfo(EventArgs e)
        {
            EventHandler handler = ShowInfo;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ShowRawTagInfo;
        public void InvokeShowRawTagInfo(EventArgs e)
        {
            EventHandler handler = ShowRawTagInfo;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ShowRawAccel;
        public void InvokeShowRawAccel(EventArgs e)
        {
            EventHandler handler = ShowRawAccel;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ShowRawGPS;
        public void InvokeShowRawGPS(EventArgs e)
        {
            EventHandler handler = ShowRawGPS;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ShowActivityDiagram;
        public void InvokeShowActivityDiagram(EventArgs e)
        {
            EventHandler handler = ShowActivityDiagram;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ShowTagGraphs;
        public void InvokeShowTagGraphs(EventArgs e)
        {
            EventHandler handler = ShowTagGraphs;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ExportCurrentMapEnvelope;
        

        public void InvokeExportCurrentMapEnvelope(EventArgs e)
        {
            EventHandler handler = ExportCurrentMapEnvelope;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ExportAsShape;
        public void InvokeExportAsShape(EventArgs e)
        {
            EventHandler handler = ExportAsShape;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CreateMCPs;
        public void InvokeCreateMCPs(EventArgs e)
        {
            EventHandler handler = CreateMCPs;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<CurrentDatasetChangedEventArgs> CurrentDatasetChanged;
        public void InvokeCurrentDatasetChanged(CurrentDatasetChangedEventArgs e)
        {
            EventHandler<CurrentDatasetChangedEventArgs> handler = CurrentDatasetChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<DatasetCheckedEventArgs> DatasetCheckedChanged;
        public void InvokeDatasetCheckedChanged(DatasetCheckedEventArgs e)
        {
            EventHandler<DatasetCheckedEventArgs> handler = DatasetCheckedChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<MapDisplayIntervalChangedEventArgs> MapDisplayIntervalChanged;
        public void InvokeMapDisplayIntervalChanged(MapDisplayIntervalChangedEventArgs e)
        {
            EventHandler<MapDisplayIntervalChangedEventArgs> handler = MapDisplayIntervalChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        public event SharpMap.Forms.MapBox.MouseEventHandler MouseMovedOnMap;

        public event EventHandler Initialize;
        public void InvokeInitialize(EventArgs e)
        {
            EventHandler handler = Initialize;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowInfo(new EventArgs());
        }

        private void tagInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowRawTagInfo(new EventArgs());
        }

        private void beschleunigungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowRawAccel(new EventArgs());
        }

        private void gPSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowRawGPS(new EventArgs());
        }

        private void aktivitätsdiagrammToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowActivityDiagram(new EventArgs());
        }

        private void graphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowTagGraphs(new EventArgs());
        }

        private void kartenansichtAlsBildToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            InvokeExportCurrentMapEnvelope(new EventArgs());
        }

        private String GetMapExportFilename(List<FtTransmitterDataset> datasets)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var dataset in datasets)
            {
                sb.Append("_");
                sb.Append($"Tag{dataset.TagId}");
            }

            return sb.ToString();
        }

        private void mCPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeCreateMCPs(new EventArgs());
        }

        private void lviDatasets_ItemChecked(object sender, TreeViewEventArgs e)
        {
            InvokeDatasetCheckedChanged(new DatasetCheckedEventArgs((int)e.Node.Tag, e.Node.Checked));
            InvokeCurrentDatasetChanged(new CurrentDatasetChangedEventArgs((int)e.Node.Tag));
            mapBox1.Refresh();
        }

        private void treeViewTagList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            InvokeCurrentDatasetChanged(new CurrentDatasetChangedEventArgs((int)e.Node.Tag));
        }

        private void mapBox1_MouseDrag(Coordinate worldPos, MouseEventArgs imagePos)
        {
            //Debug.WriteLine("im dragging " + i++);
        }

        private void konfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            var parent = menuItem.GetCurrentParent();
            var args = new CurrentDatasetChangedEventArgs((int)parent.Tag);
            InvokeShowTagConfig(args);
        }

        private void tabelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            var parent = menuItem.GetCurrentParent();
            var args = new CurrentDatasetChangedEventArgs((int)parent.Tag);
            InvokeShowTagTabelle(args);
        }

        private void ToggleMouseWheelPan()
        {
            if (this.mapBox1.ActiveTool == MapBox.Tools.Pan)
                this.mapBox1.ActiveTool = MapBox.Tools.None;
            else
            {
                this.mapBox1.ActiveTool = MapBox.Tools.Pan;
                this.mapBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            }
        }

        private void mapBox1_MouseUp(Coordinate worldPos, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                ToggleMouseWheelPan();
                return;
            }

            if (!Control.ModifierKeys.HasFlag(Keys.Control))
                return;

            var p = mapBox1.Map.ImageToWorld(e.Location);
            FeatureDataSet ds = new FeatureDataSet();
            
            foreach (var layer in mapBox1.Map.VariableLayers)
            {
                //Test if the layer is queryable?
                var queryLayer = layer as SharpMap.Layers.ICanQueryLayer;
                if (queryLayer != null)
                {
                    var env = new Envelope(new Coordinate(p.X-10, p.Y-10), new Coordinate(p.X+10 , p.Y+10));
                    queryLayer.ExecuteIntersectionQuery(env, ds);
                }
                    
                    //queryLayer.ExecuteIntersectionQuery(p, ds);
            }

            List<FeatureDataTable> foundTabs = new List<FeatureDataTable>();
            foreach (var tab in ds.Tables)
            {
                foundTabs.Add(tab);
            }

            if (!foundTabs.Any(tab => tab.Rows.Count > 0)) return;

            FrmQueriedTags frm = new FrmQueriedTags(foundTabs);
            frm.Show();
        }

        private void aktivitätsverlaufToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowActivityVerlauf(new EventArgs());
        }

        public event EventHandler ShowActivityVerlauf;
        private void InvokeShowActivityVerlauf(EventArgs eventArgs)
        {
            EventHandler handler = ShowActivityVerlauf;
            if (handler != null)
            {
                handler(this, eventArgs);
            }
        }

        public event EventHandler<CurrentDatasetChangedEventArgs> ShowTagTabelle;
        private void InvokeShowTagTabelle(CurrentDatasetChangedEventArgs eventArgs)
        {
            EventHandler<CurrentDatasetChangedEventArgs> handler = ShowTagTabelle;
            if (handler != null)
            {
                handler(this, eventArgs);
            }
        }

        public event EventHandler<CurrentDatasetChangedEventArgs> ShowTagConfig;
        private void InvokeShowTagConfig(CurrentDatasetChangedEventArgs eventArgs)
        {
            EventHandler<CurrentDatasetChangedEventArgs> handler = ShowTagConfig;
            if (handler != null)
            {
                handler(this, eventArgs);
            }
        }

        private void treeViewTagList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            tagContextMenu.Tag = e.Node.Tag;
            tagContextMenu.Show(e.Location);
        }

        private void alsShapefilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeExportAsShape(new EventArgs());
        }

        private void loggerbinalleSetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeShowLoggerBinImport(new EventArgs());
        }

        private void mapBox1_MouseDown(Coordinate worldPos, MouseEventArgs eventArgs)
        {
            if (eventArgs.Button == MouseButtons.Middle)
            {
                ToggleMouseWheelPan();
            }
        }

        private void zoomAufTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            var parent = menuItem.GetCurrentParent();
            var args = new CurrentDatasetChangedEventArgs((int)parent.Tag);
            InvokeZoomToTag(args);
        }

        public event EventHandler<CurrentDatasetChangedEventArgs> ZoomToTag;
        private void InvokeZoomToTag(CurrentDatasetChangedEventArgs eventArgs)
        {
            EventHandler<CurrentDatasetChangedEventArgs> handler = ZoomToTag;
            if (handler != null)
            {
                handler(this, eventArgs);
            }
        }
    }
    public class CurrentDatasetChangedEventArgs : EventArgs
    {
        public int? CurrentTagId { get; private set; }
        public CurrentDatasetChangedEventArgs(int? currentTagId)
        {
            CurrentTagId = currentTagId;
        }
    }

    public class MRUProjectOpenEventArgs : EventArgs
    {
        public String FullFilePath { get; private set; }
        public MRUProjectOpenEventArgs(string fullFilePath)
        {
            FullFilePath = fullFilePath;
        }
    }

    public class DatasetCheckedEventArgs : EventArgs
    {
        public int TagId { get; private set; }
        public bool Checked { get; private set; }
        public DatasetCheckedEventArgs(int id, bool check)
        {
            TagId = id;
            Checked = check;
        }
    }

    public class MovebankImportStartArgs : EventArgs
    {
        public bool EinzelImport;
        public MovebankImportStartArgs(bool einzelImport)
        {
            EinzelImport = einzelImport;
        }
    }

    public class MapDisplayIntervalChangedEventArgs : EventArgs
    {
        public DateTime IntervalStart;
        public DateTime IntervalEnd;

        public MapDisplayIntervalChangedEventArgs(DateTime start, DateTime end)
        {
            IntervalStart = start;
            IntervalEnd = end;
        }

    }
}

