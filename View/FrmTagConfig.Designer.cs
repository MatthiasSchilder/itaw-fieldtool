namespace fieldtool.View
{
    partial class FrmTagConfig
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmboVisualizer = new System.Windows.Forms.ComboBox();
            this.lblVisualizer = new System.Windows.Forms.Label();
            this.lblFarbe = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.cmboVisualizer);
            this.groupBox1.Controls.Add(this.lblVisualizer);
            this.groupBox1.Controls.Add(this.lblFarbe);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 106);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Visualisierung in der Karte";
            // 
            // cmboVisualizer
            // 
            this.cmboVisualizer.FormattingEnabled = true;
            this.cmboVisualizer.Location = new System.Drawing.Point(120, 61);
            this.cmboVisualizer.Name = "cmboVisualizer";
            this.cmboVisualizer.Size = new System.Drawing.Size(153, 21);
            this.cmboVisualizer.TabIndex = 3;
            // 
            // lblVisualizer
            // 
            this.lblVisualizer.AutoSize = true;
            this.lblVisualizer.Location = new System.Drawing.Point(35, 64);
            this.lblVisualizer.Name = "lblVisualizer";
            this.lblVisualizer.Size = new System.Drawing.Size(62, 13);
            this.lblVisualizer.TabIndex = 2;
            this.lblVisualizer.Text = "Visualisierer";
            // 
            // lblFarbe
            // 
            this.lblFarbe.AutoSize = true;
            this.lblFarbe.Location = new System.Drawing.Point(35, 32);
            this.lblFarbe.Name = "lblFarbe";
            this.lblFarbe.Size = new System.Drawing.Size(34, 13);
            this.lblFarbe.TabIndex = 0;
            this.lblFarbe.Text = "Farbe";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(224, 124);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Schließen";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(120, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 23);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // FrmTagConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(311, 155);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTagConfig";
            this.Text = "Konfiguration für Tag XXXX";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmboVisualizer;
        private System.Windows.Forms.Label lblVisualizer;
        private System.Windows.Forms.Label lblFarbe;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}