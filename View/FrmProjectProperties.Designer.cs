namespace fieldtool
{
    partial class FrmProjectProperties
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnDelBlacklistEntry = new System.Windows.Forms.Button();
            this.btnAddBlacklistEntry = new System.Windows.Forms.Button();
            this.lbTagBlacklist = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChooseDefaultPath = new System.Windows.Forms.Button();
            this.lblProjPath = new System.Windows.Forms.Label();
            this.tbDefaultLookupPath = new System.Windows.Forms.TextBox();
            this.lblProjName = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkScaleBarDarstellen = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvVektorkarten = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDeleteVektor = new System.Windows.Forms.Button();
            this.btnAddVektor = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvRasterkarten = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeadName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeadPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDeleteRaster = new System.Windows.Forms.Button();
            this.btnAddRaster = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numEPSGSource = new System.Windows.Forms.NumericUpDown();
            this.numEPSGTarget = new System.Windows.Forms.NumericUpDown();
            this.ftProjectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblNameSourceProjection = new System.Windows.Forms.Label();
            this.lblNameTargetProjection = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEPSGSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEPSGTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ftProjectBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(482, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(474, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Allgemein";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnDelBlacklistEntry);
            this.groupBox5.Controls.Add(this.btnAddBlacklistEntry);
            this.groupBox5.Controls.Add(this.lbTagBlacklist);
            this.groupBox5.Location = new System.Drawing.Point(3, 220);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(462, 130);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Auszuschließende Tags beim Importvorgang";
            // 
            // btnDelBlacklistEntry
            // 
            this.btnDelBlacklistEntry.Location = new System.Drawing.Point(381, 48);
            this.btnDelBlacklistEntry.Name = "btnDelBlacklistEntry";
            this.btnDelBlacklistEntry.Size = new System.Drawing.Size(75, 23);
            this.btnDelBlacklistEntry.TabIndex = 3;
            this.btnDelBlacklistEntry.Text = "Löschen";
            this.btnDelBlacklistEntry.UseVisualStyleBackColor = true;
            this.btnDelBlacklistEntry.Click += new System.EventHandler(this.btnDelBlacklistEntry_Click);
            // 
            // btnAddBlacklistEntry
            // 
            this.btnAddBlacklistEntry.Location = new System.Drawing.Point(381, 19);
            this.btnAddBlacklistEntry.Name = "btnAddBlacklistEntry";
            this.btnAddBlacklistEntry.Size = new System.Drawing.Size(75, 23);
            this.btnAddBlacklistEntry.TabIndex = 2;
            this.btnAddBlacklistEntry.Text = "Hinzufügen";
            this.btnAddBlacklistEntry.UseVisualStyleBackColor = true;
            this.btnAddBlacklistEntry.Click += new System.EventHandler(this.btnAddBlacklistEntry_Click);
            // 
            // lbTagBlacklist
            // 
            this.lbTagBlacklist.FormattingEnabled = true;
            this.lbTagBlacklist.Location = new System.Drawing.Point(26, 19);
            this.lbTagBlacklist.Name = "lbTagBlacklist";
            this.lbTagBlacklist.Size = new System.Drawing.Size(349, 95);
            this.lbTagBlacklist.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.btnChooseDefaultPath);
            this.groupBox4.Controls.Add(this.lblProjPath);
            this.groupBox4.Controls.Add(this.tbDefaultLookupPath);
            this.groupBox4.Controls.Add(this.lblProjName);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(462, 113);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Projektinformationen";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Projektname";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 26);
            this.label3.TabIndex = 7;
            this.label3.Text = "Standardsuchpfad\r\n für Movebanks";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Speicherpfad";
            // 
            // btnChooseDefaultPath
            // 
            this.btnChooseDefaultPath.Location = new System.Drawing.Point(381, 74);
            this.btnChooseDefaultPath.Name = "btnChooseDefaultPath";
            this.btnChooseDefaultPath.Size = new System.Drawing.Size(75, 23);
            this.btnChooseDefaultPath.TabIndex = 6;
            this.btnChooseDefaultPath.Text = "Auswählen";
            this.btnChooseDefaultPath.UseVisualStyleBackColor = true;
            this.btnChooseDefaultPath.Click += new System.EventHandler(this.btnChooseDefaultPath_Click);
            // 
            // lblProjPath
            // 
            this.lblProjPath.AutoSize = true;
            this.lblProjPath.Location = new System.Drawing.Point(156, 49);
            this.lblProjPath.Name = "lblProjPath";
            this.lblProjPath.Size = new System.Drawing.Size(35, 13);
            this.lblProjPath.TabIndex = 3;
            this.lblProjPath.Text = "label4";
            // 
            // tbDefaultLookupPath
            // 
            this.tbDefaultLookupPath.Location = new System.Drawing.Point(159, 76);
            this.tbDefaultLookupPath.Name = "tbDefaultLookupPath";
            this.tbDefaultLookupPath.Size = new System.Drawing.Size(216, 20);
            this.tbDefaultLookupPath.TabIndex = 5;
            // 
            // lblProjName
            // 
            this.lblProjName.AutoSize = true;
            this.lblProjName.Location = new System.Drawing.Point(156, 23);
            this.lblProjName.Name = "lblProjName";
            this.lblProjName.Size = new System.Drawing.Size(35, 13);
            this.lblProjName.TabIndex = 4;
            this.lblProjName.Text = "label3";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(474, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Karten";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkScaleBarDarstellen);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(462, 68);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Allgemein";
            // 
            // chkScaleBarDarstellen
            // 
            this.chkScaleBarDarstellen.AutoSize = true;
            this.chkScaleBarDarstellen.Location = new System.Drawing.Point(22, 19);
            this.chkScaleBarDarstellen.Name = "chkScaleBarDarstellen";
            this.chkScaleBarDarstellen.Size = new System.Drawing.Size(144, 17);
            this.chkScaleBarDarstellen.TabIndex = 0;
            this.chkScaleBarDarstellen.Text = "Maßstabsleiste darstellen";
            this.chkScaleBarDarstellen.UseVisualStyleBackColor = true;
            this.chkScaleBarDarstellen.CheckedChanged += new System.EventHandler(this.chkScaleBarDarstellen_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvVektorkarten);
            this.groupBox2.Controls.Add(this.btnDeleteVektor);
            this.groupBox2.Controls.Add(this.btnAddVektor);
            this.groupBox2.Location = new System.Drawing.Point(6, 225);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(462, 144);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Vektorkarten";
            // 
            // lvVektorkarten
            // 
            this.lvVektorkarten.CheckBoxes = true;
            this.lvVektorkarten.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvVektorkarten.FullRowSelect = true;
            this.lvVektorkarten.Location = new System.Drawing.Point(6, 19);
            this.lvVektorkarten.Name = "lvVektorkarten";
            this.lvVektorkarten.Size = new System.Drawing.Size(369, 114);
            this.lvVektorkarten.TabIndex = 6;
            this.lvVektorkarten.UseCompatibleStateImageBehavior = false;
            this.lvVektorkarten.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 30;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 103;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Dateipfad";
            this.columnHeader4.Width = 232;
            // 
            // btnDeleteVektor
            // 
            this.btnDeleteVektor.Location = new System.Drawing.Point(381, 48);
            this.btnDeleteVektor.Name = "btnDeleteVektor";
            this.btnDeleteVektor.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteVektor.TabIndex = 3;
            this.btnDeleteVektor.Text = "Löschen";
            this.btnDeleteVektor.UseVisualStyleBackColor = true;
            this.btnDeleteVektor.Click += new System.EventHandler(this.btnDeleteVektor_Click);
            // 
            // btnAddVektor
            // 
            this.btnAddVektor.Location = new System.Drawing.Point(381, 19);
            this.btnAddVektor.Name = "btnAddVektor";
            this.btnAddVektor.Size = new System.Drawing.Size(75, 23);
            this.btnAddVektor.TabIndex = 2;
            this.btnAddVektor.Text = "Hinzufügen";
            this.btnAddVektor.UseVisualStyleBackColor = true;
            this.btnAddVektor.Click += new System.EventHandler(this.btnAddVektor_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvRasterkarten);
            this.groupBox1.Controls.Add(this.btnDeleteRaster);
            this.groupBox1.Controls.Add(this.btnAddRaster);
            this.groupBox1.Location = new System.Drawing.Point(6, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 139);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rasterkarten";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lvRasterkarten
            // 
            this.lvRasterkarten.CheckBoxes = true;
            this.lvRasterkarten.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.colHeadName,
            this.colHeadPath});
            this.lvRasterkarten.FullRowSelect = true;
            this.lvRasterkarten.Location = new System.Drawing.Point(6, 19);
            this.lvRasterkarten.Name = "lvRasterkarten";
            this.lvRasterkarten.Size = new System.Drawing.Size(369, 114);
            this.lvRasterkarten.TabIndex = 5;
            this.lvRasterkarten.UseCompatibleStateImageBehavior = false;
            this.lvRasterkarten.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 30;
            // 
            // colHeadName
            // 
            this.colHeadName.Text = "Name";
            this.colHeadName.Width = 103;
            // 
            // colHeadPath
            // 
            this.colHeadPath.Text = "Dateipfad";
            this.colHeadPath.Width = 232;
            // 
            // btnDeleteRaster
            // 
            this.btnDeleteRaster.Location = new System.Drawing.Point(381, 48);
            this.btnDeleteRaster.Name = "btnDeleteRaster";
            this.btnDeleteRaster.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteRaster.TabIndex = 1;
            this.btnDeleteRaster.Text = "Löschen";
            this.btnDeleteRaster.UseVisualStyleBackColor = true;
            this.btnDeleteRaster.Click += new System.EventHandler(this.btnDeleteRaster_Click);
            // 
            // btnAddRaster
            // 
            this.btnAddRaster.Location = new System.Drawing.Point(381, 19);
            this.btnAddRaster.Name = "btnAddRaster";
            this.btnAddRaster.Size = new System.Drawing.Size(75, 23);
            this.btnAddRaster.TabIndex = 0;
            this.btnAddRaster.Text = "Hinzufügen";
            this.btnAddRaster.UseVisualStyleBackColor = true;
            this.btnAddRaster.Click += new System.EventHandler(this.btnAddRaster_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(415, 468);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Schließen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblNameTargetProjection);
            this.groupBox6.Controls.Add(this.lblNameSourceProjection);
            this.groupBox6.Controls.Add(this.numEPSGTarget);
            this.groupBox6.Controls.Add(this.numEPSGSource);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Location = new System.Drawing.Point(6, 125);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(462, 89);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Koordinatensystem";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Ursprung";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Ziel";
            // 
            // numEPSGSource
            // 
            this.numEPSGSource.Location = new System.Drawing.Point(159, 24);
            this.numEPSGSource.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numEPSGSource.Name = "numEPSGSource";
            this.numEPSGSource.Size = new System.Drawing.Size(71, 20);
            this.numEPSGSource.TabIndex = 2;
            // 
            // numEPSGTarget
            // 
            this.numEPSGTarget.Location = new System.Drawing.Point(159, 57);
            this.numEPSGTarget.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numEPSGTarget.Name = "numEPSGTarget";
            this.numEPSGTarget.Size = new System.Drawing.Size(71, 20);
            this.numEPSGTarget.TabIndex = 3;
            // 
            // ftProjectBindingSource
            // 
            this.ftProjectBindingSource.DataSource = typeof(fieldtool.FtProject);
            // 
            // lblNameSourceProjection
            // 
            this.lblNameSourceProjection.AutoSize = true;
            this.lblNameSourceProjection.Location = new System.Drawing.Point(236, 26);
            this.lblNameSourceProjection.Name = "lblNameSourceProjection";
            this.lblNameSourceProjection.Size = new System.Drawing.Size(27, 13);
            this.lblNameSourceProjection.TabIndex = 4;
            this.lblNameSourceProjection.Text = "({0})";
            // 
            // lblNameTargetProjection
            // 
            this.lblNameTargetProjection.AutoSize = true;
            this.lblNameTargetProjection.Location = new System.Drawing.Point(236, 59);
            this.lblNameTargetProjection.Name = "lblNameTargetProjection";
            this.lblNameTargetProjection.Size = new System.Drawing.Size(27, 13);
            this.lblNameTargetProjection.TabIndex = 5;
            this.lblNameTargetProjection.Text = "({0})";
            // 
            // FrmProjectProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 499);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmProjectProperties";
            this.Tag = "Projekteigenschaften - {0}";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEPSGSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEPSGTarget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ftProjectBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblProjPath;
        private System.Windows.Forms.BindingSource ftProjectBindingSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProjName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDeleteRaster;
        private System.Windows.Forms.Button btnAddRaster;
        private System.Windows.Forms.Button btnDeleteVektor;
        private System.Windows.Forms.Button btnAddVektor;
        private System.Windows.Forms.ListView lvRasterkarten;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader colHeadName;
        private System.Windows.Forms.ColumnHeader colHeadPath;
        private System.Windows.Forms.ListView lvVektorkarten;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkScaleBarDarstellen;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnChooseDefaultPath;
        private System.Windows.Forms.TextBox tbDefaultLookupPath;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnDelBlacklistEntry;
        private System.Windows.Forms.Button btnAddBlacklistEntry;
        private System.Windows.Forms.ListBox lbTagBlacklist;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown numEPSGTarget;
        private System.Windows.Forms.NumericUpDown numEPSGSource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNameTargetProjection;
        private System.Windows.Forms.Label lblNameSourceProjection;
    }
}