﻿using fieldtool.Controls;

namespace fieldtool.View
{
    partial class FrmAccAxisView
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkX = new System.Windows.Forms.CheckBox();
            this.chkZ = new System.Windows.Forms.CheckBox();
            this.chkY = new System.Windows.Forms.CheckBox();
            this.cmboAggregate = new System.Windows.Forms.ComboBox();
            this.lblAggregate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.BorderlineColor = System.Drawing.Color.Black;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 43);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            this.chart1.Size = new System.Drawing.Size(1089, 382);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.MouseEnter += new System.EventHandler(this.chart1_MouseEnter);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "aggregierte Burstwerte",
            "berechnete Aktivitäten"});
            this.comboBox1.Location = new System.Drawing.Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(257, 21);
            this.comboBox1.TabIndex = 8;
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(945, 431);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 10;
            this.btnCopy.Text = "Kopieren";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1026, 431);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Schließen";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkX
            // 
            this.chkX.AutoSize = true;
            this.chkX.Checked = true;
            this.chkX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkX.Location = new System.Drawing.Point(276, 14);
            this.chkX.Name = "chkX";
            this.chkX.Size = new System.Drawing.Size(33, 17);
            this.chkX.TabIndex = 11;
            this.chkX.Text = "X";
            this.chkX.UseVisualStyleBackColor = true;
            this.chkX.CheckedChanged += new System.EventHandler(this.chkX_CheckedChanged);
            // 
            // chkZ
            // 
            this.chkZ.AutoSize = true;
            this.chkZ.Checked = true;
            this.chkZ.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkZ.Location = new System.Drawing.Point(354, 14);
            this.chkZ.Name = "chkZ";
            this.chkZ.Size = new System.Drawing.Size(33, 17);
            this.chkZ.TabIndex = 12;
            this.chkZ.Text = "Z";
            this.chkZ.UseVisualStyleBackColor = true;
            this.chkZ.CheckedChanged += new System.EventHandler(this.chkZ_CheckedChanged);
            // 
            // chkY
            // 
            this.chkY.AutoSize = true;
            this.chkY.Checked = true;
            this.chkY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkY.Location = new System.Drawing.Point(315, 14);
            this.chkY.Name = "chkY";
            this.chkY.Size = new System.Drawing.Size(33, 17);
            this.chkY.TabIndex = 13;
            this.chkY.Text = "Y";
            this.chkY.UseVisualStyleBackColor = true;
            this.chkY.CheckedChanged += new System.EventHandler(this.chkY_CheckedChanged);
            // 
            // cmboAggregate
            // 
            this.cmboAggregate.FormattingEnabled = true;
            this.cmboAggregate.Items.AddRange(new object[] {
            "avg",
            "min",
            "max",
            "median"});
            this.cmboAggregate.Location = new System.Drawing.Point(497, 12);
            this.cmboAggregate.Name = "cmboAggregate";
            this.cmboAggregate.Size = new System.Drawing.Size(142, 21);
            this.cmboAggregate.TabIndex = 14;
            // 
            // lblAggregate
            // 
            this.lblAggregate.AutoSize = true;
            this.lblAggregate.Location = new System.Drawing.Point(403, 15);
            this.lblAggregate.Name = "lblAggregate";
            this.lblAggregate.Size = new System.Drawing.Size(88, 13);
            this.lblAggregate.TabIndex = 15;
            this.lblAggregate.Text = "Aggregatfunktion";
            // 
            // FrmAccAxisView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 466);
            this.Controls.Add(this.lblAggregate);
            this.Controls.Add(this.cmboAggregate);
            this.Controls.Add(this.chkY);
            this.Controls.Add(this.chkZ);
            this.Controls.Add(this.chkX);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.chart1);
            this.Name = "FrmAccAxisView";
            this.Text = "FrmAccAxisView";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkX;
        private System.Windows.Forms.CheckBox chkZ;
        private System.Windows.Forms.CheckBox chkY;
        private System.Windows.Forms.ComboBox cmboAggregate;
        private System.Windows.Forms.Label lblAggregate;
    }
}