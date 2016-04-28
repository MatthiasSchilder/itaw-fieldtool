namespace fieldtool
{
    partial class FrmSettings
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFontPicker = new System.Windows.Forms.Button();
            this.tbFont = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.picBoxBorderColor = new System.Windows.Forms.PictureBox();
            this.picBoxBackground = new System.Windows.Forms.PictureBox();
            this.picBoxTextfarbe = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkShowMassstab = new System.Windows.Forms.CheckBox();
            this.lblMassstab = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnSchließen = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.picColor = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.lblLegendActive = new System.Windows.Forms.Label();
            this.chkLegendActive = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numAlpha = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBorderColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTextfarbe)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picColor)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAlpha)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(579, 485);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(571, 459);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Karte";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numAlpha);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.chkLegendActive);
            this.groupBox1.Controls.Add(this.lblLegendActive);
            this.groupBox1.Controls.Add(this.btnFontPicker);
            this.groupBox1.Controls.Add(this.tbFont);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.picBoxBorderColor);
            this.groupBox1.Controls.Add(this.picBoxBackground);
            this.groupBox1.Controls.Add(this.picBoxTextfarbe);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.lblPosition);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(6, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(559, 247);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Legende - Darstellung";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnFontPicker
            // 
            this.btnFontPicker.Location = new System.Drawing.Point(358, 70);
            this.btnFontPicker.Name = "btnFontPicker";
            this.btnFontPicker.Size = new System.Drawing.Size(86, 24);
            this.btnFontPicker.TabIndex = 14;
            this.btnFontPicker.Text = "Auswählen";
            this.btnFontPicker.UseVisualStyleBackColor = true;
            this.btnFontPicker.Click += new System.EventHandler(this.btnFontPicker_Click);
            // 
            // tbFont
            // 
            this.tbFont.Location = new System.Drawing.Point(170, 73);
            this.tbFont.Name = "tbFont";
            this.tbFont.ReadOnly = true;
            this.tbFont.Size = new System.Drawing.Size(182, 20);
            this.tbFont.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Font";
            // 
            // picBoxBorderColor
            // 
            this.picBoxBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxBorderColor.Location = new System.Drawing.Point(170, 183);
            this.picBoxBorderColor.Name = "picBoxBorderColor";
            this.picBoxBorderColor.Size = new System.Drawing.Size(20, 20);
            this.picBoxBorderColor.TabIndex = 11;
            this.picBoxBorderColor.TabStop = false;
            this.picBoxBorderColor.Click += new System.EventHandler(this.picBoxBorderColor_Click);
            // 
            // picBoxBackground
            // 
            this.picBoxBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxBackground.Location = new System.Drawing.Point(170, 125);
            this.picBoxBackground.Name = "picBoxBackground";
            this.picBoxBackground.Size = new System.Drawing.Size(20, 20);
            this.picBoxBackground.TabIndex = 10;
            this.picBoxBackground.TabStop = false;
            this.picBoxBackground.Click += new System.EventHandler(this.picBoxBackground_Click);
            // 
            // picBoxTextfarbe
            // 
            this.picBoxTextfarbe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxTextfarbe.Location = new System.Drawing.Point(170, 99);
            this.picBoxTextfarbe.Name = "picBoxTextfarbe";
            this.picBoxTextfarbe.Size = new System.Drawing.Size(20, 20);
            this.picBoxTextfarbe.TabIndex = 9;
            this.picBoxTextfarbe.TabStop = false;
            this.picBoxTextfarbe.Click += new System.EventHandler(this.picBoxTextfarbe_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Rahmenfarbe";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(170, 46);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(182, 21);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(27, 49);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(112, 13);
            this.lblPosition.TabIndex = 2;
            this.lblPosition.Text = "Position Legendenfeld";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Hintergrundfarbe";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Textfarbe";
            // 
            // chkShowMassstab
            // 
            this.chkShowMassstab.AutoSize = true;
            this.chkShowMassstab.Location = new System.Drawing.Point(170, 27);
            this.chkShowMassstab.Name = "chkShowMassstab";
            this.chkShowMassstab.Size = new System.Drawing.Size(15, 14);
            this.chkShowMassstab.TabIndex = 4;
            this.chkShowMassstab.UseVisualStyleBackColor = true;
            this.chkShowMassstab.CheckedChanged += new System.EventHandler(this.chkShowMassstab_CheckedChanged);
            // 
            // lblMassstab
            // 
            this.lblMassstab.AutoSize = true;
            this.lblMassstab.Location = new System.Drawing.Point(27, 27);
            this.lblMassstab.Name = "lblMassstab";
            this.lblMassstab.Size = new System.Drawing.Size(119, 26);
            this.lblMassstab.TabIndex = 3;
            this.lblMassstab.Text = "Maßstabsleiste in Karte \r\nzeigen";
            // 
            // btnSchließen
            // 
            this.btnSchließen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSchließen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSchließen.Location = new System.Drawing.Point(516, 499);
            this.btnSchließen.Name = "btnSchließen";
            this.btnSchließen.Size = new System.Drawing.Size(75, 23);
            this.btnSchließen.TabIndex = 1;
            this.btnSchließen.Text = "Schließen";
            this.btnSchließen.UseVisualStyleBackColor = true;
            this.btnSchließen.Click += new System.EventHandler(this.btnSchließen_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(571, 459);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Aktivitätsplot";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // picColor
            // 
            this.picColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picColor.Location = new System.Drawing.Point(144, 26);
            this.picColor.Name = "picColor";
            this.picColor.Size = new System.Drawing.Size(26, 26);
            this.picColor.TabIndex = 4;
            this.picColor.TabStop = false;
            this.picColor.Click += new System.EventHandler(this.picColor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Farbe \"NoData\" im \r\nAktivitätsplot";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblMassstab);
            this.groupBox2.Controls.Add(this.chkShowMassstab);
            this.groupBox2.Location = new System.Drawing.Point(6, 373);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(559, 80);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Verschiedenes";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkedListBox1);
            this.groupBox3.Location = new System.Drawing.Point(6, 258);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(559, 109);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Legende - Inhalte";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(30, 19);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(523, 79);
            this.checkedListBox1.TabIndex = 0;
            // 
            // lblLegendActive
            // 
            this.lblLegendActive.AutoSize = true;
            this.lblLegendActive.Location = new System.Drawing.Point(27, 22);
            this.lblLegendActive.Name = "lblLegendActive";
            this.lblLegendActive.Size = new System.Drawing.Size(95, 13);
            this.lblLegendActive.TabIndex = 15;
            this.lblLegendActive.Text = "Legende anzeigen";
            // 
            // chkLegendActive
            // 
            this.chkLegendActive.AutoSize = true;
            this.chkLegendActive.Location = new System.Drawing.Point(170, 27);
            this.chkLegendActive.Name = "chkLegendActive";
            this.chkLegendActive.Size = new System.Drawing.Size(15, 14);
            this.chkLegendActive.TabIndex = 16;
            this.chkLegendActive.UseVisualStyleBackColor = true;
            this.chkLegendActive.CheckedChanged += new System.EventHandler(this.chkLegendActive_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Transparenz Hintergrund";
            // 
            // numAlpha
            // 
            this.numAlpha.DecimalPlaces = 2;
            this.numAlpha.Location = new System.Drawing.Point(170, 156);
            this.numAlpha.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAlpha.Name = "numAlpha";
            this.numAlpha.Size = new System.Drawing.Size(52, 20);
            this.numAlpha.TabIndex = 20;
            this.numAlpha.ValueChanged += new System.EventHandler(this.numAlpha_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.picColor);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(565, 100);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Darstellung";
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSchließen;
            this.ClientSize = new System.Drawing.Size(603, 534);
            this.Controls.Add(this.btnSchließen);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.Text = "Einstellungen";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBorderColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTextfarbe)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picColor)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numAlpha)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnSchließen;
        private System.Windows.Forms.CheckBox chkShowMassstab;
        private System.Windows.Forms.Label lblMassstab;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.PictureBox picBoxBorderColor;
        private System.Windows.Forms.PictureBox picBoxBackground;
        private System.Windows.Forms.PictureBox picBoxTextfarbe;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnFontPicker;
        private System.Windows.Forms.TextBox tbFont;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox picColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkLegendActive;
        private System.Windows.Forms.Label lblLegendActive;
        private System.Windows.Forms.NumericUpDown numAlpha;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}