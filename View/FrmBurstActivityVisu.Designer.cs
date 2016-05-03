namespace fieldtool.View
{
    partial class FrmBurstActivityVisu
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnChancel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.accVisualizer1 = new fieldtool.Controls.AccVisualizer();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accVisualizer1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.accVisualizer1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(415, 568);
            this.panel1.TabIndex = 1;
            // 
            // btnChancel
            // 
            this.btnChancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnChancel.Location = new System.Drawing.Point(352, 587);
            this.btnChancel.Name = "btnChancel";
            this.btnChancel.Size = new System.Drawing.Size(75, 23);
            this.btnChancel.TabIndex = 3;
            this.btnChancel.Text = "Schließen";
            this.btnChancel.UseVisualStyleBackColor = true;
            this.btnChancel.Click += new System.EventHandler(this.btnChancel_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(271, 587);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Kopieren";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // accVisualizer1
            // 
            this.accVisualizer1.Location = new System.Drawing.Point(3, 3);
            this.accVisualizer1.Name = "accVisualizer1";
            this.accVisualizer1.Size = new System.Drawing.Size(383, 117);
            this.accVisualizer1.TabIndex = 0;
            this.accVisualizer1.TabStop = false;
            // 
            // FrmBurstActivityVisu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnChancel;
            this.ClientSize = new System.Drawing.Size(436, 622);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnChancel);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmBurstActivityVisu";
            this.Text = "Aktivitätsplot zu Tag {0}";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBurstActivityVisu_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.accVisualizer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnChancel;
        private System.Windows.Forms.Button button1;
        private Controls.AccVisualizer accVisualizer1;
    }
}