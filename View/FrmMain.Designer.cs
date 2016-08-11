using fieldtool.Controls;

namespace fieldtool.View
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.mapBox1 = new SharpMap.Forms.MapBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.öffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.movebankLadenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.movebankEinzelsetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggerbinalleSetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.kartenansichtAlsBildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alsShapefilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schließenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.eigenschaftenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spacerInsertMRUAfter = new System.Windows.Forms.ToolStripSeparator();
            this.spacerInsertMRUBefore = new System.Windows.Forms.ToolStripSeparator();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bearbeitenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aktivitätsdiagrammToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aktivitätsverlaufToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rohdatenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beschleunigungToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.auswertungToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.streifgebieteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mCPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.überToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabelCoords = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dateIntervalPicker1 = new fieldtool.Controls.DateIntervalPicker();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.treeViewTagList = new fieldtool.Controls.WorkaroundTreeView();
            this.tagContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.konfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabelleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListColorKeys = new System.Windows.Forms.ImageList(this.components);
            this.mapZoomToolStrip1 = new SharpMap.Forms.ToolBar.MapZoomToolStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tagContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapBox1
            // 
            this.mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.None;
            this.mapBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.mapBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.mapBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapBox1.FineZoomFactor = 10D;
            this.mapBox1.Location = new System.Drawing.Point(0, 0);
            this.mapBox1.MapQueryMode = SharpMap.Forms.MapBox.MapQueryType.LayerByIndex;
            this.mapBox1.Name = "mapBox1";
            this.mapBox1.PreviewMode = SharpMap.Forms.MapBox.PreviewModes.Fast;
            this.mapBox1.QueryGrowFactor = 5F;
            this.mapBox1.QueryLayerIndex = 0;
            this.mapBox1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox1.ShowProgressUpdate = false;
            this.mapBox1.Size = new System.Drawing.Size(1080, 521);
            this.mapBox1.TabIndex = 0;
            this.mapBox1.Text = "mapBox1";
            this.mapBox1.WheelZoomMagnitude = -2D;
            this.mapBox1.MouseUp += new SharpMap.Forms.MapBox.MouseEventHandler(this.mapBox1_MouseUp);
            this.mapBox1.MouseDrag += new SharpMap.Forms.MapBox.MouseEventHandler(this.mapBox1_MouseDrag);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.bearbeitenToolStripMenuItem,
            this.tagToolStripMenuItem,
            this.auswertungToolStripMenuItem,
            this.überToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(1243, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neuToolStripMenuItem,
            this.öffnenToolStripMenuItem,
            this.importToolStripMenuItem1,
            this.exportToolStripMenuItem1,
            this.speichernToolStripMenuItem,
            this.schließenToolStripMenuItem,
            this.toolStripSeparator1,
            this.eigenschaftenToolStripMenuItem,
            this.spacerInsertMRUAfter,
            this.spacerInsertMRUBefore,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.dateiToolStripMenuItem.Text = "Projekt";
            // 
            // neuToolStripMenuItem
            // 
            this.neuToolStripMenuItem.Name = "neuToolStripMenuItem";
            this.neuToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.neuToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.neuToolStripMenuItem.Text = "Neu";
            this.neuToolStripMenuItem.Click += new System.EventHandler(this.neuToolStripMenuItem_Click);
            // 
            // öffnenToolStripMenuItem
            // 
            this.öffnenToolStripMenuItem.Name = "öffnenToolStripMenuItem";
            this.öffnenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.öffnenToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.öffnenToolStripMenuItem.Text = "Öffnen";
            this.öffnenToolStripMenuItem.Click += new System.EventHandler(this.öffnenToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem1
            // 
            this.importToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.movebankLadenToolStripMenuItem,
            this.movebankEinzelsetsToolStripMenuItem,
            this.loggerbinalleSetsToolStripMenuItem});
            this.importToolStripMenuItem1.Name = "importToolStripMenuItem1";
            this.importToolStripMenuItem1.Size = new System.Drawing.Size(188, 22);
            this.importToolStripMenuItem1.Text = "Import";
            // 
            // movebankLadenToolStripMenuItem
            // 
            this.movebankLadenToolStripMenuItem.Name = "movebankLadenToolStripMenuItem";
            this.movebankLadenToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.movebankLadenToolStripMenuItem.Text = "Movebank (alle Sets)";
            this.movebankLadenToolStripMenuItem.Click += new System.EventHandler(this.movebankLadenToolStripMenuItem_Click);
            // 
            // movebankEinzelsetsToolStripMenuItem
            // 
            this.movebankEinzelsetsToolStripMenuItem.Name = "movebankEinzelsetsToolStripMenuItem";
            this.movebankEinzelsetsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.movebankEinzelsetsToolStripMenuItem.Text = "Movebank (Einzelsets)";
            this.movebankEinzelsetsToolStripMenuItem.Click += new System.EventHandler(this.movebankEinzelsetsToolStripMenuItem_Click);
            // 
            // loggerbinalleSetsToolStripMenuItem
            // 
            this.loggerbinalleSetsToolStripMenuItem.Name = "loggerbinalleSetsToolStripMenuItem";
            this.loggerbinalleSetsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.loggerbinalleSetsToolStripMenuItem.Text = "logger.bin (alle Sets)";
            this.loggerbinalleSetsToolStripMenuItem.Click += new System.EventHandler(this.loggerbinalleSetsToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem1
            // 
            this.exportToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kartenansichtAlsBildToolStripMenuItem,
            this.alsShapefilesToolStripMenuItem});
            this.exportToolStripMenuItem1.Name = "exportToolStripMenuItem1";
            this.exportToolStripMenuItem1.Size = new System.Drawing.Size(188, 22);
            this.exportToolStripMenuItem1.Text = "Export";
            // 
            // kartenansichtAlsBildToolStripMenuItem
            // 
            this.kartenansichtAlsBildToolStripMenuItem.Name = "kartenansichtAlsBildToolStripMenuItem";
            this.kartenansichtAlsBildToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.kartenansichtAlsBildToolStripMenuItem.Text = "Kartenansicht als Bild";
            this.kartenansichtAlsBildToolStripMenuItem.Click += new System.EventHandler(this.kartenansichtAlsBildToolStripMenuItem_Click_1);
            // 
            // alsShapefilesToolStripMenuItem
            // 
            this.alsShapefilesToolStripMenuItem.Name = "alsShapefilesToolStripMenuItem";
            this.alsShapefilesToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.alsShapefilesToolStripMenuItem.Text = "Als Shapefile(s)";
            this.alsShapefilesToolStripMenuItem.Click += new System.EventHandler(this.alsShapefilesToolStripMenuItem_Click);
            // 
            // speichernToolStripMenuItem
            // 
            this.speichernToolStripMenuItem.Name = "speichernToolStripMenuItem";
            this.speichernToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.speichernToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.speichernToolStripMenuItem.Text = "Speichern";
            this.speichernToolStripMenuItem.Click += new System.EventHandler(this.speichernToolStripMenuItem_Click);
            // 
            // schließenToolStripMenuItem
            // 
            this.schließenToolStripMenuItem.Name = "schließenToolStripMenuItem";
            this.schließenToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.schließenToolStripMenuItem.Text = "Schließen";
            this.schließenToolStripMenuItem.Click += new System.EventHandler(this.schließenToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(185, 6);
            // 
            // eigenschaftenToolStripMenuItem
            // 
            this.eigenschaftenToolStripMenuItem.Name = "eigenschaftenToolStripMenuItem";
            this.eigenschaftenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.eigenschaftenToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.eigenschaftenToolStripMenuItem.Text = "Eigenschaften";
            this.eigenschaftenToolStripMenuItem.Click += new System.EventHandler(this.eigenschaftenToolStripMenuItem_Click);
            // 
            // spacerInsertMRUAfter
            // 
            this.spacerInsertMRUAfter.Name = "spacerInsertMRUAfter";
            this.spacerInsertMRUAfter.Size = new System.Drawing.Size(185, 6);
            // 
            // spacerInsertMRUBefore
            // 
            this.spacerInsertMRUBefore.Name = "spacerInsertMRUBefore";
            this.spacerInsertMRUBefore.Size = new System.Drawing.Size(185, 6);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // bearbeitenToolStripMenuItem
            // 
            this.bearbeitenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.einstellungenToolStripMenuItem});
            this.bearbeitenToolStripMenuItem.Name = "bearbeitenToolStripMenuItem";
            this.bearbeitenToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.bearbeitenToolStripMenuItem.Text = "Bearbeiten";
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.einstellungenToolStripMenuItem.Text = "Einstellungen";
            this.einstellungenToolStripMenuItem.Click += new System.EventHandler(this.einstellungenToolStripMenuItem_Click);
            // 
            // tagToolStripMenuItem
            // 
            this.tagToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aktivitätsdiagrammToolStripMenuItem,
            this.aktivitätsverlaufToolStripMenuItem,
            this.graphToolStripMenuItem,
            this.rohdatenToolStripMenuItem});
            this.tagToolStripMenuItem.Name = "tagToolStripMenuItem";
            this.tagToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.tagToolStripMenuItem.Text = "Tag";
            // 
            // aktivitätsdiagrammToolStripMenuItem
            // 
            this.aktivitätsdiagrammToolStripMenuItem.Name = "aktivitätsdiagrammToolStripMenuItem";
            this.aktivitätsdiagrammToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.aktivitätsdiagrammToolStripMenuItem.Text = "Aktivitätsplot";
            this.aktivitätsdiagrammToolStripMenuItem.Click += new System.EventHandler(this.aktivitätsdiagrammToolStripMenuItem_Click);
            // 
            // aktivitätsverlaufToolStripMenuItem
            // 
            this.aktivitätsverlaufToolStripMenuItem.Name = "aktivitätsverlaufToolStripMenuItem";
            this.aktivitätsverlaufToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.aktivitätsverlaufToolStripMenuItem.Text = "Aktivitätsverlauf";
            this.aktivitätsverlaufToolStripMenuItem.Click += new System.EventHandler(this.aktivitätsverlaufToolStripMenuItem_Click);
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.graphToolStripMenuItem.Text = "Graph";
            this.graphToolStripMenuItem.Click += new System.EventHandler(this.graphToolStripMenuItem_Click);
            // 
            // rohdatenToolStripMenuItem
            // 
            this.rohdatenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tagInfoToolStripMenuItem,
            this.beschleunigungToolStripMenuItem,
            this.gPSToolStripMenuItem});
            this.rohdatenToolStripMenuItem.Name = "rohdatenToolStripMenuItem";
            this.rohdatenToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.rohdatenToolStripMenuItem.Text = "Rohdaten";
            // 
            // tagInfoToolStripMenuItem
            // 
            this.tagInfoToolStripMenuItem.Name = "tagInfoToolStripMenuItem";
            this.tagInfoToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.tagInfoToolStripMenuItem.Text = "TagInfo";
            this.tagInfoToolStripMenuItem.Click += new System.EventHandler(this.tagInfoToolStripMenuItem_Click);
            // 
            // beschleunigungToolStripMenuItem
            // 
            this.beschleunigungToolStripMenuItem.Name = "beschleunigungToolStripMenuItem";
            this.beschleunigungToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.beschleunigungToolStripMenuItem.Text = "Beschleunigung";
            this.beschleunigungToolStripMenuItem.Click += new System.EventHandler(this.beschleunigungToolStripMenuItem_Click);
            // 
            // gPSToolStripMenuItem
            // 
            this.gPSToolStripMenuItem.Name = "gPSToolStripMenuItem";
            this.gPSToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.gPSToolStripMenuItem.Text = "GPS";
            this.gPSToolStripMenuItem.Click += new System.EventHandler(this.gPSToolStripMenuItem_Click);
            // 
            // auswertungToolStripMenuItem
            // 
            this.auswertungToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.streifgebieteToolStripMenuItem});
            this.auswertungToolStripMenuItem.Name = "auswertungToolStripMenuItem";
            this.auswertungToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.auswertungToolStripMenuItem.Text = "Auswertung";
            // 
            // streifgebieteToolStripMenuItem
            // 
            this.streifgebieteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mCPToolStripMenuItem});
            this.streifgebieteToolStripMenuItem.Name = "streifgebieteToolStripMenuItem";
            this.streifgebieteToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.streifgebieteToolStripMenuItem.Text = "Streifgebiete";
            // 
            // mCPToolStripMenuItem
            // 
            this.mCPToolStripMenuItem.Name = "mCPToolStripMenuItem";
            this.mCPToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.mCPToolStripMenuItem.Text = "MCP";
            this.mCPToolStripMenuItem.Click += new System.EventHandler(this.mCPToolStripMenuItem_Click);
            // 
            // überToolStripMenuItem
            // 
            this.überToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem});
            this.überToolStripMenuItem.Name = "überToolStripMenuItem";
            this.überToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.überToolStripMenuItem.Text = "Über";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelCoords});
            this.statusStrip1.Location = new System.Drawing.Point(0, 678);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1243, 24);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabelCoords
            // 
            this.statusLabelCoords.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusLabelCoords.Name = "statusLabelCoords";
            this.statusLabelCoords.Size = new System.Drawing.Size(121, 19);
            this.statusLabelCoords.Tag = "Koordinaten: {0} / {1}";
            this.statusLabelCoords.Text = "Koordinaten: {0} / {1}";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.55028F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.44971F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1243, 654);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tabControl1, 2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 557);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1237, 94);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dateIntervalPicker1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1229, 68);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Filter";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dateIntervalPicker1
            // 
            this.dateIntervalPicker1.Location = new System.Drawing.Point(81, 15);
            this.dateIntervalPicker1.Margin = new System.Windows.Forms.Padding(0);
            this.dateIntervalPicker1.Name = "dateIntervalPicker1";
            this.dateIntervalPicker1.Size = new System.Drawing.Size(439, 21);
            this.dateIntervalPicker1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabPage2.Size = new System.Drawing.Size(1229, 68);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Statistik";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(149, 20);
            this.textBox1.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.mapBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(158, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1082, 523);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.treeViewTagList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(149, 523);
            this.panel2.TabIndex = 8;
            // 
            // treeViewTagList
            // 
            this.treeViewTagList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewTagList.CheckBoxes = true;
            this.treeViewTagList.ContextMenuStrip = this.tagContextMenu;
            this.treeViewTagList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewTagList.ImageIndex = 0;
            this.treeViewTagList.ImageList = this.imageListColorKeys;
            this.treeViewTagList.ItemHeight = 16;
            this.treeViewTagList.Location = new System.Drawing.Point(0, 0);
            this.treeViewTagList.Name = "treeViewTagList";
            this.treeViewTagList.SelectedImageIndex = 0;
            this.treeViewTagList.ShowLines = false;
            this.treeViewTagList.ShowPlusMinus = false;
            this.treeViewTagList.ShowRootLines = false;
            this.treeViewTagList.Size = new System.Drawing.Size(147, 521);
            this.treeViewTagList.TabIndex = 5;
            this.treeViewTagList.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.lviDatasets_ItemChecked);
            this.treeViewTagList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTagList_AfterSelect);
            this.treeViewTagList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewTagList_NodeMouseClick);
            // 
            // tagContextMenu
            // 
            this.tagContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.konfigurationToolStripMenuItem,
            this.tabelleToolStripMenuItem});
            this.tagContextMenu.Name = "contextMenuStrip1";
            this.tagContextMenu.Size = new System.Drawing.Size(148, 48);
            // 
            // konfigurationToolStripMenuItem
            // 
            this.konfigurationToolStripMenuItem.Name = "konfigurationToolStripMenuItem";
            this.konfigurationToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.konfigurationToolStripMenuItem.Text = "Konfiguration";
            this.konfigurationToolStripMenuItem.Click += new System.EventHandler(this.konfigurationToolStripMenuItem_Click);
            // 
            // tabelleToolStripMenuItem
            // 
            this.tabelleToolStripMenuItem.Name = "tabelleToolStripMenuItem";
            this.tabelleToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.tabelleToolStripMenuItem.Text = "Tabelle";
            this.tabelleToolStripMenuItem.Click += new System.EventHandler(this.tabelleToolStripMenuItem_Click);
            // 
            // imageListColorKeys
            // 
            this.imageListColorKeys.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListColorKeys.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListColorKeys.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // mapZoomToolStrip1
            // 
            this.mapZoomToolStrip1.AllowMerge = false;
            this.mapZoomToolStrip1.Enabled = false;
            this.mapZoomToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mapZoomToolStrip1.Location = new System.Drawing.Point(0, 24);
            this.mapZoomToolStrip1.MapControl = this.mapBox1;
            this.mapZoomToolStrip1.Name = "mapZoomToolStrip1";
            this.mapZoomToolStrip1.Size = new System.Drawing.Size(1243, 25);
            this.mapZoomToolStrip1.TabIndex = 7;
            this.mapZoomToolStrip1.Text = "mapZoomToolStrip1";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 702);
            this.Controls.Add(this.mapZoomToolStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "FieldTool";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tagContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpMap.Forms.MapBox mapBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bearbeitenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem überToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelCoords;
        private System.Windows.Forms.ToolStripMenuItem neuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem öffnenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem schließenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem eigenschaftenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator spacerInsertMRUAfter;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aktivitätsdiagrammToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem rohdatenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tagInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beschleunigungToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gPSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator spacerInsertMRUBefore;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem movebankLadenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem kartenansichtAlsBildToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem movebankEinzelsetsToolStripMenuItem;
        private SharpMap.Forms.ToolBar.MapZoomToolStrip mapZoomToolStrip1;
        private System.Windows.Forms.ToolStripMenuItem auswertungToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem streifgebieteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mCPToolStripMenuItem;
        private System.Windows.Forms.ImageList imageListColorKeys;
        private System.Windows.Forms.ContextMenuStrip tagContextMenu;
        private System.Windows.Forms.ToolStripMenuItem konfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tabelleToolStripMenuItem;
        private WorkaroundTreeView treeViewTagList;
        private DateIntervalPicker dateIntervalPicker1;
        private System.Windows.Forms.ToolStripMenuItem aktivitätsverlaufToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alsShapefilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggerbinalleSetsToolStripMenuItem;
    }
}

