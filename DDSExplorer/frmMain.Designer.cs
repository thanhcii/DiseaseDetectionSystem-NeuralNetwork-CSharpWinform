namespace DDSExplorer
{
    partial class frmMain
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
            this.grpClassifier = new System.Windows.Forms.GroupBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.rbDTClassifier = new System.Windows.Forms.RadioButton();
            this.rbNNClassifier = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbMultiFold = new System.Windows.Forms.RadioButton();
            this.rbSingleFold = new System.Windows.Forms.RadioButton();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.grpClassifier.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpClassifier
            // 
            this.grpClassifier.Controls.Add(this.btnGo);
            this.grpClassifier.Controls.Add(this.rbDTClassifier);
            this.grpClassifier.Controls.Add(this.rbNNClassifier);
            this.grpClassifier.Enabled = false;
            this.grpClassifier.Location = new System.Drawing.Point(7, 373);
            this.grpClassifier.Name = "grpClassifier";
            this.grpClassifier.Size = new System.Drawing.Size(150, 89);
            this.grpClassifier.TabIndex = 1;
            this.grpClassifier.TabStop = false;
            this.grpClassifier.Text = "Classifier";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(36, 60);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 3;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // rbDTClassifier
            // 
            this.rbDTClassifier.AutoSize = true;
            this.rbDTClassifier.Location = new System.Drawing.Point(9, 37);
            this.rbDTClassifier.Name = "rbDTClassifier";
            this.rbDTClassifier.Size = new System.Drawing.Size(91, 17);
            this.rbDTClassifier.TabIndex = 1;
            this.rbDTClassifier.TabStop = true;
            this.rbDTClassifier.Text = "Decision Tree";
            this.rbDTClassifier.UseVisualStyleBackColor = true;
            // 
            // rbNNClassifier
            // 
            this.rbNNClassifier.AutoSize = true;
            this.rbNNClassifier.Location = new System.Drawing.Point(9, 19);
            this.rbNNClassifier.Name = "rbNNClassifier";
            this.rbNNClassifier.Size = new System.Drawing.Size(102, 17);
            this.rbNNClassifier.TabIndex = 0;
            this.rbNNClassifier.TabStop = true;
            this.rbNNClassifier.Text = "Neural Network ";
            this.rbNNClassifier.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnLoad);
            this.groupBox1.Controls.Add(this.dgvData);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(635, 363);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DataSet";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbMultiFold);
            this.groupBox3.Controls.Add(this.rbSingleFold);
            this.groupBox3.Location = new System.Drawing.Point(9, 304);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(131, 53);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // rbMultiFold
            // 
            this.rbMultiFold.AutoSize = true;
            this.rbMultiFold.Location = new System.Drawing.Point(12, 32);
            this.rbMultiFold.Name = "rbMultiFold";
            this.rbMultiFold.Size = new System.Drawing.Size(105, 17);
            this.rbMultiFold.TabIndex = 2;
            this.rbMultiFold.TabStop = true;
            this.rbMultiFold.Text = "Multiple Instance";
            this.rbMultiFold.UseVisualStyleBackColor = true;
            // 
            // rbSingleFold
            // 
            this.rbSingleFold.AutoSize = true;
            this.rbSingleFold.Location = new System.Drawing.Point(12, 8);
            this.rbSingleFold.Name = "rbSingleFold";
            this.rbSingleFold.Size = new System.Drawing.Size(98, 17);
            this.rbSingleFold.TabIndex = 1;
            this.rbSingleFold.TabStop = true;
            this.rbSingleFold.Text = "Single Instance";
            this.rbSingleFold.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Enabled = false;
            this.btnClear.Location = new System.Drawing.Point(156, 336);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(156, 309);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dgvData
            // 
            this.dgvData.AllowDrop = true;
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToOrderColumns = true;
            this.dgvData.AllowUserToResizeColumns = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.GridColor = System.Drawing.SystemColors.Control;
            this.dgvData.Location = new System.Drawing.Point(9, 18);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(617, 284);
            this.dgvData.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(163, 373);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(479, 89);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Results";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Select data file";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 467);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(649, 22);
            this.statusStrip.TabIndex = 9;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(649, 489);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpClassifier);
            this.MaximumSize = new System.Drawing.Size(657, 600);
            this.MinimumSize = new System.Drawing.Size(657, 506);
            this.Name = "frmMain";
            this.Text = "Disease Detection System";
            this.grpClassifier.ResumeLayout(false);
            this.grpClassifier.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpClassifier;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.RadioButton rbDTClassifier;
        private System.Windows.Forms.RadioButton rbNNClassifier;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbMultiFold;
        private System.Windows.Forms.RadioButton rbSingleFold;
    }
}