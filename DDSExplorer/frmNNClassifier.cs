// Neural Network Classifier

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Threading;

using NeuralNetworks;

namespace DDSExplorer
{
	/// <summary>
	/// Summary description for Form.
	/// </summary>
	public class frmNNClassifier : System.Windows.Forms.Form
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private double learningRate = 0.1;
        private double momentum = 0.0;
        private double sigmoidAlphaValue = 2.0;
        private int neuronsInFirstLayer = 20;
        private int iterations = 1000;

        private Thread workerThread = null;
        private Thread attributeSelectThread = null;

        private bool bIsMultilpleInstance;
        private bool bAttSelected;

        private frmMain frmMain;

        NNClassifierUtil NNUtil = new NNClassifierUtil();

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private GroupBox groupBox4;
        private Button selectButton;
        private TextBox attributecountBox;
        private Label label9;
        private GroupBox groupBox2;
        private TextBox neuronsBox;
        private Label label4;
        private TextBox momentumBox;
        private Label label2;
        private TextBox alphaBox;
        private Label label3;
        private TextBox learningRateBox;
        private Label label1;
        private TextBox iterationsBox;
        private Label label8;
        private Label label5;
        private GroupBox groupBox1;
        private Button stopButton;
        private Button startButton;
        private DataGridView dgvAttData;
        private Button clearButton;
        private StatusStrip statusStrip1;
        private Label label10;
        private TextBox AccuracyBox;
        private Button breakButton;
        private ToolStripStatusLabel toolStripStatusLabel1;
        

		// Constructor
        public frmNNClassifier(bool bIsMultilpleInstance, frmMain  frmMain)
		{
            this.frmMain = frmMain;
            this.bIsMultilpleInstance = bIsMultilpleInstance;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent( );

            Control.CheckForIllegalCrossThreadCalls = false;

			// update some controls
			UpdateSettings( );
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.AccuracyBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.breakButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.attributecountBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.neuronsBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.momentumBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.alphaBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.learningRateBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.iterationsBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvAttData = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttData)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(429, 283);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.AccuracyBox);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(421, 257);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Controls";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // AccuracyBox
            // 
            this.AccuracyBox.Enabled = false;
            this.AccuracyBox.Location = new System.Drawing.Point(103, 225);
            this.AccuracyBox.Name = "AccuracyBox";
            this.AccuracyBox.Size = new System.Drawing.Size(88, 20);
            this.AccuracyBox.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(19, 225);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 14);
            this.label10.TabIndex = 13;
            this.label10.Text = "Accuracy";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.breakButton);
            this.groupBox4.Controls.Add(this.clearButton);
            this.groupBox4.Controls.Add(this.selectButton);
            this.groupBox4.Controls.Add(this.attributecountBox);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(216, 103);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(195, 144);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Attribute selection";
            // 
            // breakButton
            // 
            this.breakButton.Enabled = false;
            this.breakButton.Location = new System.Drawing.Point(90, 82);
            this.breakButton.Name = "breakButton";
            this.breakButton.Size = new System.Drawing.Size(75, 23);
            this.breakButton.TabIndex = 14;
            this.breakButton.Text = "Break";
            this.breakButton.Click += new System.EventHandler(this.breakButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Enabled = false;
            this.clearButton.Location = new System.Drawing.Point(90, 113);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 13;
            this.clearButton.Text = "Clear";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.Location = new System.Drawing.Point(90, 53);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(75, 23);
            this.selectButton.TabIndex = 12;
            this.selectButton.Text = "Select";
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // attributecountBox
            // 
            this.attributecountBox.Location = new System.Drawing.Point(90, 24);
            this.attributecountBox.Name = "attributecountBox";
            this.attributecountBox.Size = new System.Drawing.Size(75, 20);
            this.attributecountBox.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(6, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 14);
            this.label9.TabIndex = 10;
            this.label9.Text = "Attribute count";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.neuronsBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.momentumBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.alphaBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.learningRateBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.iterationsBox);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(195, 195);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // neuronsBox
            // 
            this.neuronsBox.Location = new System.Drawing.Point(125, 95);
            this.neuronsBox.Name = "neuronsBox";
            this.neuronsBox.Size = new System.Drawing.Size(60, 20);
            this.neuronsBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Neurons in first layer:";
            // 
            // momentumBox
            // 
            this.momentumBox.Location = new System.Drawing.Point(125, 45);
            this.momentumBox.Name = "momentumBox";
            this.momentumBox.Size = new System.Drawing.Size(60, 20);
            this.momentumBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Momentum:";
            // 
            // alphaBox
            // 
            this.alphaBox.Location = new System.Drawing.Point(125, 70);
            this.alphaBox.Name = "alphaBox";
            this.alphaBox.Size = new System.Drawing.Size(60, 20);
            this.alphaBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Sigmoid\'s alpha value:";
            // 
            // learningRateBox
            // 
            this.learningRateBox.Location = new System.Drawing.Point(125, 20);
            this.learningRateBox.Name = "learningRateBox";
            this.learningRateBox.Size = new System.Drawing.Size(60, 20);
            this.learningRateBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Learning rate:";
            // 
            // iterationsBox
            // 
            this.iterationsBox.Location = new System.Drawing.Point(125, 155);
            this.iterationsBox.Name = "iterationsBox";
            this.iterationsBox.Size = new System.Drawing.Size(60, 20);
            this.iterationsBox.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(126, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 14);
            this.label8.TabIndex = 25;
            this.label8.Text = "( 0 - inifinity )";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Iterations:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stopButton);
            this.groupBox1.Controls.Add(this.startButton);
            this.groupBox1.Location = new System.Drawing.Point(216, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 90);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Training";
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(59, 51);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 8;
            this.stopButton.Text = "S&top";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(59, 22);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "&Start";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvAttData);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(421, 257);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "DataSet";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvAttData
            // 
            this.dgvAttData.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvAttData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttData.Location = new System.Drawing.Point(6, 6);
            this.dgvAttData.Name = "dgvAttData";
            this.dgvAttData.Size = new System.Drawing.Size(409, 248);
            this.dgvAttData.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 300);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(451, 22);
            this.statusStrip1.TabIndex = 10;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // frmNNClassifier
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(451, 322);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmNNClassifier";
            this.Text = "Neural Network Classifier";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNNClassifier_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttData)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        // On NNClassifier form closing
        private void frmNNClassifier_FormClosing(object sender, FormClosingEventArgs e)
        {
            // check if worker thread is running
            if ((workerThread != null) && (workerThread.IsAlive))
                workerThread.Join();

            // check if attributeSelect thread is running
            if ((attributeSelectThread != null) && (attributeSelectThread.IsAlive))
                attributeSelectThread.Join();

            frmMain.NNClassifierNotification();      
        }

        
		// Update settings controls
		private void UpdateSettings( )
		{
            learningRateBox.Text = learningRate.ToString();
            momentumBox.Text = momentum.ToString();
            alphaBox.Text = sigmoidAlphaValue.ToString();
            neuronsBox.Text = neuronsInFirstLayer.ToString();
            iterationsBox.Text = iterations.ToString();
		}

		// Enable/disable controls
		private void EnableControls( bool enable )
		{
            learningRateBox.Enabled = enable;
            momentumBox.Enabled = enable;
            alphaBox.Enabled = enable;
            neuronsBox.Enabled = enable;
            iterationsBox.Enabled = enable;

            startButton.Enabled = enable;
            stopButton.Enabled = !enable;
			
		}

        // On button "Start" - start learning procedure
		private void startButton_Click(object sender, System.EventArgs e)
		{
            AccuracyBox.Text = "";
            
            // get learning rate
			try
			{
				learningRate = Math.Max( 0.00001, Math.Min( 1, double.Parse( learningRateBox.Text ) ) );
			}
			catch
			{
				learningRate = 0.1;
			}
            // get momentum
            try
            {
                momentum = Math.Max(0, Math.Min(0.5, double.Parse(momentumBox.Text)));
            }
            catch
            {
                momentum = 0;
            }
            // get sigmoid's alpha value
            try
            {
                sigmoidAlphaValue = Math.Max(0.001, Math.Min(50, double.Parse(alphaBox.Text)));
            }
            catch
            {
                sigmoidAlphaValue = 2;
            }
            // get neurons count in first layer
            try
            {
                neuronsInFirstLayer = Math.Max(5, Math.Min(50, int.Parse(neuronsBox.Text)));
            }
            catch
            {
                neuronsInFirstLayer = 20;
            }
            // iterations
            try
            {
                iterations = Math.Max(0, int.Parse(iterationsBox.Text));
            }
            catch
            {
                iterations = 1000;
            }

			// update settings controls
			UpdateSettings( );

			// disable all settings controls
			EnableControls( false );
            clearButton.Enabled = false;
            selectButton.Enabled = false;

			toolStripStatusLabel1.Text = "Training";
            if (!bAttSelected)
                NNClassifierUtil.SetNNDataTable(LoadUtil.GetDataTable());

            // run worker thread
            workerThread = new Thread(new ThreadStart(SearchSolution));
			workerThread.Start( );
		}

		// On button "Stop" - stop learning procedure
		private void stopButton_Click(object sender, System.EventArgs e)
		{
            stopButton.Enabled = false;

			// stop worker thread
            // TODO : check if this completes the worker thread and then stops
			workerThread.Join( );
			workerThread = null;

            toolStripStatusLabel1.Text = null;
            AccuracyBox.Text = null;
   		}


        // On button "Break" - stop attribute selection procedure
        private void breakButton_Click(object sender, EventArgs e)
        {
            breakButton.Enabled = false;

            // stop attributeSelect thread
            attributeSelectThread.Join();
            attributeSelectThread = null;

            toolStripStatusLabel1.Text = null;
        }


        // worker thread
        private void SearchSolution()
        {
            if (bIsMultilpleInstance)
            {
                AccuracyBox.Text = (NNUtil.BootStrapEvaluate()).ToString();
            }
            else
            {
                AccuracyBox.Text = (NNUtil.CrossFoldEvaluate()).ToString();
            }

            toolStripStatusLabel1.Text = "Training Complete";

            // enable all settings controls
            EnableControls(true);
            if (bAttSelected)
                clearButton.Enabled = true;
            else
                selectButton.Enabled = true;
           
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (attributecountBox.Text.Trim() == "")
            {
                MessageBox.Show("Enter an attribute count");
                startButton.Enabled = true;
                return;
            }

            // disable all setting controls
            EnableControls(false);
            breakButton.Enabled = true;
            selectButton.Enabled = false;
            clearButton.Enabled = false;
            stopButton.Enabled = false;

            toolStripStatusLabel1.Text = "Selecting Attributes";

            // run attributeSelect thread
            attributeSelectThread = new Thread(new ThreadStart(AttributeSelection));
            attributeSelectThread.Start();

        }


        // attributeSelect thread
        public void AttributeSelection()
        {
            int attributeCount = Convert.ToInt32(attributecountBox.Text);

            NNAttributeSelection NNAttSelect = new NNAttributeSelection();
            NNAttSelect.SelectAttributes(attributeCount, bIsMultilpleInstance);

            dgvAttData.DataSource = NNClassifierUtil.GetNNDataTable();
            toolStripStatusLabel1.Text = "Selecting Complete";

            bAttSelected = true;
            clearButton.Enabled = true;
            breakButton.Enabled = false;
            EnableControls(true);
        }


        private void clearButton_Click(object sender, EventArgs e)
        {
            NNClassifierUtil.SetNNDataTable(null);

            bAttSelected = false;
            dgvAttData.DataSource = null;
            attributecountBox.Text = null;
            toolStripStatusLabel1.Text = null;
            selectButton.Enabled = true;
            clearButton.Enabled = false;
            stopButton.Enabled = false;
        }

       
	}
}
