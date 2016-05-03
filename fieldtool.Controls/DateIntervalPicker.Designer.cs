namespace fieldtool
{
    partial class DateIntervalPicker
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btn7d = new System.Windows.Forms.Button();
            this.btn14d = new System.Windows.Forms.Button();
            this.btn30d = new System.Windows.Forms.Button();
            this.btnWholeRange = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = " dd.MM.yy";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(181, 0);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(88, 20);
            this.dateTimePicker2.TabIndex = 13;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Ende";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Beginn";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = " dd.MM.yy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(49, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(88, 20);
            this.dateTimePicker1.TabIndex = 10;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // btn7d
            // 
            this.btn7d.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn7d.Location = new System.Drawing.Point(275, 0);
            this.btn7d.Name = "btn7d";
            this.btn7d.Size = new System.Drawing.Size(35, 21);
            this.btn7d.TabIndex = 14;
            this.btn7d.Text = "7d";
            this.toolTip1.SetToolTip(this.btn7d, "letzte 7 Tage anzeigen");
            this.btn7d.UseVisualStyleBackColor = true;
            this.btn7d.Click += new System.EventHandler(this.btn7d_Click);
            // 
            // btn14d
            // 
            this.btn14d.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn14d.Location = new System.Drawing.Point(316, 0);
            this.btn14d.Name = "btn14d";
            this.btn14d.Size = new System.Drawing.Size(35, 21);
            this.btn14d.TabIndex = 15;
            this.btn14d.Text = "14d";
            this.toolTip1.SetToolTip(this.btn14d, "letzte 14 Tage anzeigen");
            this.btn14d.UseVisualStyleBackColor = true;
            this.btn14d.Click += new System.EventHandler(this.btn14d_Click);
            // 
            // btn30d
            // 
            this.btn30d.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn30d.Location = new System.Drawing.Point(357, 0);
            this.btn30d.Name = "btn30d";
            this.btn30d.Size = new System.Drawing.Size(35, 21);
            this.btn30d.TabIndex = 16;
            this.btn30d.Text = "30d";
            this.toolTip1.SetToolTip(this.btn30d, "letzte 30 Tage anzeigen");
            this.btn30d.UseVisualStyleBackColor = true;
            this.btn30d.Click += new System.EventHandler(this.btn30d_Click);
            // 
            // btnWholeRange
            // 
            this.btnWholeRange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWholeRange.Location = new System.Drawing.Point(398, 0);
            this.btnWholeRange.Name = "btnWholeRange";
            this.btnWholeRange.Size = new System.Drawing.Size(35, 21);
            this.btnWholeRange.TabIndex = 17;
            this.btnWholeRange.Text = "∞";
            this.toolTip1.SetToolTip(this.btnWholeRange, "kompletten Datenbereich anzeigen");
            this.btnWholeRange.UseVisualStyleBackColor = true;
            this.btnWholeRange.Click += new System.EventHandler(this.button1_Click);
            // 
            // DateIntervalPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnWholeRange);
            this.Controls.Add(this.btn30d);
            this.Controls.Add(this.btn14d);
            this.Controls.Add(this.btn7d);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DateIntervalPicker";
            this.Size = new System.Drawing.Size(439, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btn7d;
        private System.Windows.Forms.Button btn14d;
        private System.Windows.Forms.Button btn30d;
        private System.Windows.Forms.Button btnWholeRange;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
