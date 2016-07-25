namespace fieldtool.View
{
    partial class FrmExportShape
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPick = new System.Windows.Forms.Button();
            this.grpExportTags = new System.Windows.Forms.GroupBox();
            this.rbSelectedTags = new System.Windows.Forms.RadioButton();
            this.rbAlleTags = new System.Windows.Forms.RadioButton();
            this.grpAusgabe = new System.Windows.Forms.GroupBox();
            this.lblFiles = new System.Windows.Forms.Label();
            this.rbMultiShapeExport = new System.Windows.Forms.RadioButton();
            this.rbSingleShapeExport = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpExportTags.SuspendLayout();
            this.grpAusgabe.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(87, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(344, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pfad";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(455, 197);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnPick
            // 
            this.btnPick.Location = new System.Drawing.Point(437, 21);
            this.btnPick.Name = "btnPick";
            this.btnPick.Size = new System.Drawing.Size(75, 23);
            this.btnPick.TabIndex = 3;
            this.btnPick.Text = "Auswählen";
            this.btnPick.UseVisualStyleBackColor = true;
            this.btnPick.Click += new System.EventHandler(this.btnPick_Click);
            // 
            // grpExportTags
            // 
            this.grpExportTags.Controls.Add(this.rbSelectedTags);
            this.grpExportTags.Controls.Add(this.rbAlleTags);
            this.grpExportTags.Location = new System.Drawing.Point(12, 118);
            this.grpExportTags.Name = "grpExportTags";
            this.grpExportTags.Size = new System.Drawing.Size(518, 73);
            this.grpExportTags.TabIndex = 4;
            this.grpExportTags.TabStop = false;
            this.grpExportTags.Text = "Tags exportieren";
            // 
            // rbSelectedTags
            // 
            this.rbSelectedTags.AutoSize = true;
            this.rbSelectedTags.Location = new System.Drawing.Point(87, 42);
            this.rbSelectedTags.Name = "rbSelectedTags";
            this.rbSelectedTags.Size = new System.Drawing.Size(113, 17);
            this.rbSelectedTags.TabIndex = 1;
            this.rbSelectedTags.Text = "Ausgewählte Tags";
            this.rbSelectedTags.UseVisualStyleBackColor = true;
            // 
            // rbAlleTags
            // 
            this.rbAlleTags.AutoSize = true;
            this.rbAlleTags.Checked = true;
            this.rbAlleTags.Location = new System.Drawing.Point(87, 19);
            this.rbAlleTags.Name = "rbAlleTags";
            this.rbAlleTags.Size = new System.Drawing.Size(69, 17);
            this.rbAlleTags.TabIndex = 0;
            this.rbAlleTags.TabStop = true;
            this.rbAlleTags.Text = "Alle Tags";
            this.rbAlleTags.UseVisualStyleBackColor = true;
            // 
            // grpAusgabe
            // 
            this.grpAusgabe.Controls.Add(this.lblFiles);
            this.grpAusgabe.Controls.Add(this.rbMultiShapeExport);
            this.grpAusgabe.Controls.Add(this.label1);
            this.grpAusgabe.Controls.Add(this.rbSingleShapeExport);
            this.grpAusgabe.Controls.Add(this.textBox1);
            this.grpAusgabe.Controls.Add(this.btnPick);
            this.grpAusgabe.Location = new System.Drawing.Point(12, 12);
            this.grpAusgabe.Name = "grpAusgabe";
            this.grpAusgabe.Size = new System.Drawing.Size(518, 100);
            this.grpAusgabe.TabIndex = 6;
            this.grpAusgabe.TabStop = false;
            this.grpAusgabe.Text = "Ausgabe";
            // 
            // lblFiles
            // 
            this.lblFiles.AutoSize = true;
            this.lblFiles.Location = new System.Drawing.Point(21, 51);
            this.lblFiles.Name = "lblFiles";
            this.lblFiles.Size = new System.Drawing.Size(44, 13);
            this.lblFiles.TabIndex = 4;
            this.lblFiles.Text = "Dateien";
            // 
            // rbMultiShapeExport
            // 
            this.rbMultiShapeExport.AutoSize = true;
            this.rbMultiShapeExport.Location = new System.Drawing.Point(87, 74);
            this.rbMultiShapeExport.Name = "rbMultiShapeExport";
            this.rbMultiShapeExport.Size = new System.Drawing.Size(181, 17);
            this.rbMultiShapeExport.TabIndex = 3;
            this.rbMultiShapeExport.Text = "pro Tag ein Shapefile exportieren";
            this.rbMultiShapeExport.UseVisualStyleBackColor = true;
            // 
            // rbSingleShapeExport
            // 
            this.rbSingleShapeExport.AutoSize = true;
            this.rbSingleShapeExport.Checked = true;
            this.rbSingleShapeExport.Location = new System.Drawing.Point(87, 51);
            this.rbSingleShapeExport.Name = "rbSingleShapeExport";
            this.rbSingleShapeExport.Size = new System.Drawing.Size(198, 17);
            this.rbSingleShapeExport.TabIndex = 2;
            this.rbSingleShapeExport.TabStop = true;
            this.rbSingleShapeExport.Text = "alle Tags in ein Shapefile exportieren";
            this.rbSingleShapeExport.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(293, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Hilfe";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(374, 197);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Schließen";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmExportShape
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 225);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grpAusgabe);
            this.Controls.Add(this.grpExportTags);
            this.Controls.Add(this.btnExport);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmExportShape";
            this.Text = "Als Shapefiles exportieren";
            this.Load += new System.EventHandler(this.FrmExportShape_Load);
            this.grpExportTags.ResumeLayout(false);
            this.grpExportTags.PerformLayout();
            this.grpAusgabe.ResumeLayout(false);
            this.grpAusgabe.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnPick;
        private System.Windows.Forms.GroupBox grpExportTags;
        private System.Windows.Forms.RadioButton rbSelectedTags;
        private System.Windows.Forms.RadioButton rbAlleTags;
        private System.Windows.Forms.GroupBox grpAusgabe;
        private System.Windows.Forms.Label lblFiles;
        private System.Windows.Forms.RadioButton rbMultiShapeExport;
        private System.Windows.Forms.RadioButton rbSingleShapeExport;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnClose;
    }
}