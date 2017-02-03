using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DotSpatial.Topology;
using fieldtool.Data;
using fieldtool.Data.Geometry;
using fieldtool.Data.Movebank;
using fieldtool.Util;
using fieldtool.View;
using Microsoft.WindowsAPICodePack.Dialogs;
using SharpmapGDAL;
using Coordinate = GeoAPI.Geometries.Coordinate;
using Envelope = GeoAPI.Geometries.Envelope;

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

    class NewEntityAvailableArgs : EventArgs
    {
        public List<FtTransmitterDataset> Datasets { get; set; }
        public NewEntityAvailableArgs(List<FtTransmitterDataset> datasets)
        {
            Datasets = datasets;
        }
    }

    class MCPAvailableArgs : EventArgs
    {
        public FtTransmitterDataset Dataset;
        public readonly int PercentageMCP;
        public MCPAvailableArgs(FtTransmitterDataset dataset, int percentageMCP)
        {
            this.Dataset = dataset;
            this.PercentageMCP = percentageMCP;
        }
    }

    class SetupProgressArgs : EventArgs
    {
        public int NumTags { get; private set; }
        public SetupProgressArgs(int numTags)
        {
            NumTags = numTags;
        }
    }

    class StepProgressArgs : EventArgs
    {
        public string TagName { get; private set; }
        public StepProgressArgs(string tagName)
        {
            TagName = tagName;
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

    class MapZoomToEnvelopeArgs : EventArgs
    {
        public Envelope Env { get; private set; }
        public MapZoomToEnvelopeArgs(Envelope env)
        {
            Env = env;
        }
    }

    class FtProjectMainPresenter : Presenter<IFtProjectMainView>
    {
        public EventHandler<MapZoomToEnvelopeArgs> MapZoomToEnvelopeRequested;
        private void InvokeMapZoomToEnvelopeRequested(MapZoomToEnvelopeArgs e)
        {
            MapZoomToEnvelopeRequested?.Invoke(this, e);
        }

        public EventHandler<MapDisplayIntervalChangedEventArgs> rMapDisplayIntervalChanged;
        private void InvokeMapDisplayIntervalChanged(MapDisplayIntervalChangedEventArgs e)
        {
            rMapDisplayIntervalChanged?.Invoke(this, e);
        }

        public EventHandler<MapEnvelopeExportRequestedArgs> MapEnvelopeExportRequested;
        private void InvokeMapEnvelopeExportRequested(MapEnvelopeExportRequestedArgs e)
        {
            MapEnvelopeExportRequested?.Invoke(this, e);
        }

        public EventHandler<CursorCoordsChangedArgs> CursorCoordinatesChanged;
        private void InvokeCursorCoordinatesChanged(CursorCoordsChangedArgs e)
        {
            CursorCoordinatesChanged?.Invoke(this, e);
        }

        public EventHandler<MapChangedArgs> MapChanged;
        private void InvokeMapChanged(MapChangedArgs e)
        {
            MapChanged?.Invoke(this, e);
        }

        public EventHandler<ProjectStateArgs> ProjectStateChanged;
        private void InvokeProjectStateChanged(ProjectStateArgs e)
        {
            ProjectStateChanged?.Invoke(this, e);
        }

        public EventHandler<NewEntityAvailableArgs> NewEntityAvailable;
        private void InvokeNewEntityAvailable(NewEntityAvailableArgs e)
        {
            NewEntityAvailable?.Invoke(this, e);
        }

        public EventHandler<MCPAvailableArgs> MCPAvailable;
        private void InvokeMCPAvailable(MCPAvailableArgs e)
        {
            MCPAvailable?.Invoke(this, e);
        }

        public EventHandler<SetupProgressArgs> SetupProgress;
        private void InvokeSetupProgress(int numTags)
        {
            SetupProgress?.Invoke(this, new SetupProgressArgs(numTags));
        }


        public EventHandler<StepProgressArgs> StepProgress;
        private void InvokeStepProgress(string tagName)
        {
            StepProgress?.Invoke(this, new StepProgressArgs(tagName));
        }

        public EventHandler FinishProgress;
        public void InvokeFinishProgress()
        {;
            FinishProgress?.Invoke(this, new EventArgs());
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
            View.ShowLoggerBinImport += ViewOnShowLoggerBinImport;
            View.ShowRawTagInfo += View_ShowRawTagInfo;
            View.ShowRawAccel += View_ShowRawAccel;
            View.ShowRawGPS += View_ShowRawGPS;
            View.CurrentDatasetChanged += View_CurrentDatasetChanged;
            View.ShowActivityDiagram += View_ShowActivityDiagram;
            View.ShowActivityVerlauf += View_ShowActivityVerlauf;
            View.ShowTagConfig += ViewOnShowTagConfig;
            View.ShowTagTabelle += View_ShowTagTabelle;
            View.ZoomToTag += ViewOnZoomToTag;
            View.ShowTagGraphs += View_ShowTagGraphs;
            View.DatasetCheckedChanged += View_DatasetCheckedChanged;
            View.MapDisplayIntervalChanged += View_MapDisplayIntervalChanged;
            View.CreateMCPs += View_CreateMCPs;
            View.CreateKDE += View_CreateKDE;
            View.ExportCurrentMapEnvelope += View_ExportCurrentMapEnvelope;
            View.ExportAsShape += ViewOnExportAsShape;
        }

        private void ViewOnShowLoggerBinImport(object sender, EventArgs eventArgs)
        {
            if (Project == null)
            {
                MessageBox.Show("Bitte zunächst ein Projekt erstellen oder öffnen.");
                return;
            }

            CommonOpenFileDialog loggerBinOpenFileDialog = new CommonOpenFileDialog { Multiselect = false };
            loggerBinOpenFileDialog.Filters.Add(
                new CommonFileDialogFilter("e-obs Binärdatei v7.2", "*.bin"
                ));

            CommonFileDialogResult dr = loggerBinOpenFileDialog.ShowDialog();
            if (dr != CommonFileDialogResult.Ok)
                return;

            const string DecoderBinaryFilename = "decoder_v7_2.exe";

            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo(
                    Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location),
                        DecoderBinaryFilename),
                    $"-f {loggerBinOpenFileDialog.FileName} -c m")
                {
                    CreateNoWindow = false,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Normal,
                    WorkingDirectory = Path.GetDirectoryName(loggerBinOpenFileDialog.FileName)
                },
                EnableRaisingEvents = true
            };
            proc.Exited += ProcOnExited;
            AsyncOp = AsyncOperationManager.CreateOperation(null);
            proc.Start();       
        }

        private AsyncOperation AsyncOp = null;
        private void ProcOnExited(object sender, EventArgs eventArgs)
        {
            var startInfo = ((Process) sender).StartInfo;
            AsyncOp.Post(ProcOnExited_MainThreadDelegate, startInfo);
        }

        private void ProcOnExited_MainThreadDelegate(object o)
        {
            var startInfo = (ProcessStartInfo) o;
            Project.MovebankFilesets = FtFileset.FileSetFromDirectory(startInfo.WorkingDirectory);
            ImportMovebank();
        }

        private void ViewOnExportAsShape(object sender, EventArgs eventArgs)
        {
            FrmExportShape frm = new FrmExportShape(Project);

            FtFormFactory.ShowDialog(frm);
        }

        private void ViewOnShowTagConfig(object sender, ContextMenuItemClickedEventArgs eventArgs)
        {
            if (eventArgs.TagObject.NodeType == TreeNodeTagObject.TreeViewNodeType.MCPNode)
                return;

            var dataset = eventArgs.TagObject.NodeDataset;
            if (dataset == null)
                return;

            FrmTagConfig frm = new FrmTagConfig(dataset);
            frm.ShowDialog();

            InvokeNewEntityAvailable(new NewEntityAvailableArgs(Project.Datasets));
        }

        private void View_ShowTagTabelle(object sender, ContextMenuItemClickedEventArgs eventArgs)
        {
            if (eventArgs.TagObject.NodeType == TreeNodeTagObject.TreeViewNodeType.MCPNode)
                return;

            var dataset = eventArgs.TagObject.NodeDataset;
            if (dataset == null)
                return;

            FrmTagTabelle frm = new FrmTagTabelle(dataset);
            frm.Show();
        }


        private void ViewOnZoomToTag(object sender, ContextMenuItemClickedEventArgs currentDatasetChangedEventArgs)
        {
            if (currentDatasetChangedEventArgs.TagObject.NodeType == TreeNodeTagObject.TreeViewNodeType.MCPNode)
                return;

            var dataset = currentDatasetChangedEventArgs.TagObject.NodeDataset;

            var envelope = dataset.GPSData.GetEnvelope();
            InvokeMapZoomToEnvelopeRequested(new MapZoomToEnvelopeArgs(envelope));
        }

        private void View_ShowActivityVerlauf(object sender, EventArgs e)
        {
            if (!CurrentDatasetAvailable)
                return;
            
            FrmAccAxisView frm = new FrmAccAxisView(CurrentDataset);
            frm.Show();
        }

        private void View_ExportCurrentMapEnvelope(object sender, EventArgs e)
        {
            var activeTags = Project.Datasets.Where(dataset => dataset.Active).ToList();
            InvokeMapEnvelopeExportRequested(new MapEnvelopeExportRequestedArgs(activeTags));
        }

        private void View_MapDisplayIntervalChanged(object sender, MapDisplayIntervalChangedEventArgs e)
        {
            Project.SetIntervalFilter(CurrentDataset, e.IntervalStart, e.IntervalEnd);
            InvokeMapChanged(new MapChangedArgs(Map));
        }

        private Dictionary<int, Color> MCPPercToColorDictBluish = new Dictionary<int, Color>()
        {
            {10, Color.FromArgb(0, 1, 229)},
            {20, Color.FromArgb(0, 22, 204)},
            {30, Color.FromArgb(0, 43, 180)},
            {40, Color.FromArgb(0, 64, 156)},
            {50, Color.FromArgb(0, 85, 132)},
            {60, Color.FromArgb(0, 106, 107)},
            {70, Color.FromArgb(0, 127, 83)},
            {80, Color.FromArgb(0, 148, 59)},
            {90, Color.FromArgb(0, 169, 135)},
            {100, Color.FromArgb(0, 191, 11)},
        };

        private Dictionary<int, Color> MCPPercToColorDictRedish = new Dictionary<int, Color>()
        {
            {100, Color.FromArgb(0, 16, 229)},
            {90, Color.FromArgb(38, 0, 227)},
            {80, Color.FromArgb(92, 0, 225)},
            {70, Color.FromArgb(145, 0, 223)},
            {60, Color.FromArgb(198, 0, 221)},
            {50, Color.FromArgb(220, 0, 190)},
            {40, Color.FromArgb(218, 0, 136)},
            {30, Color.FromArgb(216, 0, 82)},
            {20, Color.FromArgb(214, 0, 30)},
            {10, Color.FromArgb(213, 23, 0)},
        };
        private void View_CreateMCPs(object sender, CreateMCPEventArgs e)
        {
            
            int[] mcpPercentages = null;
            if (e.CreationMode == CreateMCPEventArgs.MCPCreationMode.Manual)
            {
                var frm = new FrmMCPPercentage();
                if (frm.ShowDialog() == DialogResult.Cancel)
                    return;
                mcpPercentages = new[] {frm.PercentageMCP};
            }
            else
            {
                mcpPercentages = new[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            }

            foreach (var dataset in Project.Datasets.Where(dataset => dataset.Active))
            {
                foreach (var percentage in mcpPercentages.Reverse())
                {
                    if(MCPPercToColorDictRedish.ContainsKey(percentage))
                        dataset.AddMCP(new FtTransmitterMCPDataEntry(dataset, percentage, MCPPercToColorDictRedish[percentage]));
                    else
                    {
                        dataset.AddMCP(new FtTransmitterMCPDataEntry(dataset, percentage, dataset.Visulization.Color));
                    }
                }
                //InvokeMCPAvailable(new MCPAvailableArgs(dataset, percentageMCP));
            }
            InvokeNewEntityAvailable(new NewEntityAvailableArgs(Project.Datasets));
            InvokeMapChanged(new MapChangedArgs(Map));
        }

        private void View_CreateKDE(object sender, EventArgs e)
        {
            FtTransmitterDataset dataset = Project.Datasets.First(ds => ds.Active);

            var xValuesAsArray = dataset.GPSData.XValueArray;
            var yValuesAsArray = dataset.GPSData.YValueArray;

            var xVariance = Accord.Statistics.Measures.Variance(xValuesAsArray);
            var yVariance = Accord.Statistics.Measures.Variance(yValuesAsArray);

            var cellsize = Math.Sqrt(Math.Min(xVariance, yVariance))/100;

            var env = dataset.GPSData.GetEnvelope();

            var rowCnt = (int)Math.Ceiling(env.Height / cellsize);
            var colCnt = (int)Math.Ceiling(env.Width / cellsize);

            var z = Math.Max(rowCnt, colCnt);

            Dictionary<Envelope, Tuple<int, int>> envelopes = new Dictionary<Envelope, Tuple<int, int>>();
            for(int i = 0; i < z; i++)
            {
                for(int j = 0; j < z; j++)
                {
                    Coordinate c1 = new Coordinate(env.MinX + j * cellsize, env.MaxY - i * cellsize);
                    Coordinate c2 = new Coordinate(env.MinX + (j + 1) * cellsize, env.MaxY - (i + 1) * cellsize);

                    envelopes.Add(new Envelope(c1, c2), new Tuple<int, int>(i, j));
                }
            }

            List<double[]> samples = new List<double[]>();
            foreach(var gpsPoint in dataset.GPSData)
            {
                if (!gpsPoint.IsValid())
                    continue;
                foreach (var kvp in envelopes)
                {
                    if (kvp.Key.Contains(new Coordinate(gpsPoint.Rechtswert.Value, gpsPoint.Hochwert.Value)))
                    {
                        samples.Add(new double[] { kvp.Value.Item2, kvp.Value.Item1 });
                        break;
                    }
                }
            }

            double[][] samplesarr = new double[samples.Count][];
            int h = 0;
            foreach (var blub in samples)
                samplesarr[h++] = blub;

            Accord.Statistics.Distributions.Multivariate.MultivariateEmpiricalDistribution kde =
                new Accord.Statistics.Distributions.Multivariate.MultivariateEmpiricalDistribution(samplesarr);
            
            Bitmap bmp = new Bitmap(z, z);

            List<double> densities = new List<double>();
            for (int i = 0; i < z; i++)
            {
                for (int j = 0; j < z; j++)
                {
                    densities.Add(kde.ProbabilityDensityFunction(new double[] { i, j }));
                }
            }

            var minDens = densities.Min();
            var maxDens = densities.Max();

            var diffDens = maxDens - minDens;

            for (int i = 0; i < z; i++)
            {
                for(int j = 0; j < z; j++)
                {
                    var densityAtPoint = kde.ProbabilityDensityFunction(new double[] { i, j});

                    Debug.WriteLine(diffDens / densityAtPoint);

                    var greyValue = (int)Math.Floor(255 * densityAtPoint / maxDens);

                    bmp.SetPixel(i, j, Color.FromArgb(greyValue, greyValue, greyValue));

                    //var bla = (int) Math.Floor(120*densityAtPoint/maxDens);

                    //int r, g, b;
                    //ColorUtil.HsvToRgb(bla, 1, 1, out r, out g, out b);
                    //bmp.SetPixel(i, j, Color.FromArgb(r, g, b));

                }
            }

            var userPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var fileName = Path.ChangeExtension(Path.GetRandomFileName(), "png");
            var outPath = Path.Combine(userPath, fileName);
            bmp.Save(outPath);

            if (
                MessageBox.Show(String.Format("Die Ausgabedatei wurde unter {0} abgelegt. Öffnen?", outPath), "Fertig.",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                System.Diagnostics.Process.Start(outPath);

        }

        private void View_ShowEinstellungen(object sender, EventArgs e)
        {
            FtFormFactory.Show(new FrmSettings());
        }

        private void View_DatasetCheckedChanged(object sender, DatasetCheckedEventArgs e)
        {
            if(e.TagObject.NodeType == TreeNodeTagObject.TreeViewNodeType.PunktewolkeNode)
                Project.SetDatasetFeatureState(e.TagObject, e.Checked, FtDatasetFeatureType.Puntual);
            else if (e.TagObject.NodeType == TreeNodeTagObject.TreeViewNodeType.MCPNode)
                Project.SetDatasetFeatureState(e.TagObject, e.Checked, FtDatasetFeatureType.Polygonal);

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
            if (e.TagObject.NodeType == TreeNodeTagObject.TreeViewNodeType.MCPNode)
                return; 

            CurrentDataset = e.TagObject.NodeDataset;
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
            {
                MessageBox.Show("Bitte zunächst ein Projekt erstellen oder öffnen.");
                return;
            }
                
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
            Project.LoadDatasets(InvokeSetupProgress, InvokeStepProgress, InvokeFinishProgress);
            InvokeNewEntityAvailable(new NewEntityAvailableArgs(Project.Datasets));
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

