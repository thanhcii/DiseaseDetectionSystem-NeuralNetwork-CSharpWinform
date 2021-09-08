// Decision Tree Classifier

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Threading;

using DecisionTrees;

namespace DDSExplorer
{
	/// <summary>
	/// Summary description for Form.
	/// </summary>
	public class frmDTClassifier : System.Windows.Forms.Form
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private Thread workerThread = null;
        private bool bIsMultilpleInstance;

        private frmMain frmMain;

        DTClassifierUtil DTUtil = new DTClassifierUtil();
        private GroupBox groupBox1;
        private Button stopButton;
        private Button startButton;
        private StatusStrip statusStrip1;
        private Label label10;
        private TextBox AccuracyBox;
        private TextBox textBox1;
        private Label label11;
        private GroupBox groupBox5;
        private Button button1;
        private Button button2;
        private Button button3;
        private TextBox textBox2;
        private Label label12;
        private GroupBox groupBox6;
        private TextBox textBox3;
        private Label label13;
        private TextBox textBox4;
        private Label label14;
        private GroupBox groupBox7;
        private TextBox textBox5;
        private Label label15;
        private TextBox textBox6;
        private Label label16;
        private TextBox textBox7;
        private Label label17;
        private TextBox textBox8;
        private Label label18;
        private TextBox textBox9;
        private Label label19;
        private Label label20;
        private GroupBox groupBox8;
        private Button button4;
        private Button button5;
        private Label label1;
        private TextBox NPVBox;
        private ToolStripStatusLabel toolStripStatusLabel1;
        

		// Constructor
        public frmDTClassifier(bool bIsMultilpleInstance, frmMain frmMain)
		{
            this.frmMain = frmMain;
            this.bIsMultilpleInstance = bIsMultilpleInstance;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent( );

            Control.CheckForIllegalCrossThreadCalls = false;
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
            this.AccuracyBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.NPVBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // AccuracyBox
            // 
            this.AccuracyBox.Enabled = false;
            this.AccuracyBox.Location = new System.Drawing.Point(236, 29);
            this.AccuracyBox.Name = "AccuracyBox";
            this.AccuracyBox.Size = new System.Drawing.Size(75, 20);
            this.AccuracyBox.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(158, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 14);
            this.label10.TabIndex = 13;
            this.label10.Text = "Accuracy";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stopButton);
            this.groupBox1.Controls.Add(this.startButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(140, 92);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Training";
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(32, 50);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 8;
            this.stopButton.Text = "S&top";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(32, 21);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "&Start";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 117);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(327, 22);
            this.statusStrip1.TabIndex = 10;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(306, 262);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(75, 20);
            this.textBox1.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(233, 265);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 14);
            this.label11.TabIndex = 13;
            this.label11.Text = "Accuracy";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Controls.Add(this.button3);
            this.groupBox5.Controls.Add(this.textBox2);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Location = new System.Drawing.Point(216, 103);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(195, 144);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Attribute selection";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(90, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Break";
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(90, 113);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Clear";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(90, 53);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Select";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(90, 24);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(75, 20);
            this.textBox2.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(6, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 14);
            this.label12.TabIndex = 10;
            this.label12.Text = "Attribute count";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.textBox3);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.textBox4);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Enabled = false;
            this.groupBox6.Location = new System.Drawing.Point(6, 207);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(195, 75);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Current iteration";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(125, 45);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(60, 20);
            this.textBox3.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(10, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 16);
            this.label13.TabIndex = 2;
            this.label13.Text = "Error:";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(125, 20);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(60, 20);
            this.textBox4.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(10, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 16);
            this.label14.TabIndex = 0;
            this.label14.Text = "Iteration:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.textBox5);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.textBox6);
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Controls.Add(this.textBox7);
            this.groupBox7.Controls.Add(this.label17);
            this.groupBox7.Controls.Add(this.textBox8);
            this.groupBox7.Controls.Add(this.label18);
            this.groupBox7.Controls.Add(this.textBox9);
            this.groupBox7.Controls.Add(this.label19);
            this.groupBox7.Controls.Add(this.label20);
            this.groupBox7.Location = new System.Drawing.Point(6, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(195, 195);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Settings";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(125, 95);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(60, 20);
            this.textBox5.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(10, 97);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(115, 15);
            this.label15.TabIndex = 6;
            this.label15.Text = "Neurons in first layer:";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(125, 45);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(60, 20);
            this.textBox6.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(10, 47);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 17);
            this.label16.TabIndex = 2;
            this.label16.Text = "Momentum:";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(125, 70);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(60, 20);
            this.textBox7.TabIndex = 5;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(10, 72);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 15);
            this.label17.TabIndex = 4;
            this.label17.Text = "Sigmoid\'s alpha value:";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(125, 20);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(60, 20);
            this.textBox8.TabIndex = 1;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(10, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(78, 14);
            this.label18.TabIndex = 0;
            this.label18.Text = "Learning rate:";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(125, 155);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(60, 20);
            this.textBox9.TabIndex = 9;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(126, 175);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(58, 14);
            this.label19.TabIndex = 25;
            this.label19.Text = "( 0 - inifinity )";
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(10, 157);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(70, 16);
            this.label20.TabIndex = 8;
            this.label20.Text = "Iterations:";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.button4);
            this.groupBox8.Controls.Add(this.button5);
            this.groupBox8.Location = new System.Drawing.Point(216, 6);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(195, 90);
            this.groupBox8.TabIndex = 9;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Training";
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(59, 51);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "S&top";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(59, 22);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 2;
            this.button5.Text = "&Start";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(158, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 15;
            this.label1.Text = "NPV";
            // 
            // NPVBox
            // 
            this.NPVBox.Enabled = false;
            this.NPVBox.Location = new System.Drawing.Point(236, 61);
            this.NPVBox.Name = "NPVBox";
            this.NPVBox.Size = new System.Drawing.Size(75, 20);
            this.NPVBox.TabIndex = 16;
            // 
            // frmDTClassifier
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(327, 139);
            this.Controls.Add(this.NPVBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AccuracyBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmDTClassifier";
            this.Text = "Decision Tree Classifier";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDTClassifier_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        // On DTClassifier form closing
        private void frmDTClassifier_FormClosing(object sender, FormClosingEventArgs e)
        {
            // check if worker thread is running
            if ((workerThread != null) && (workerThread.IsAlive))
                workerThread.Join();

            frmMain.DTClassifierNotification();
        }

		// Enable/disable controls
		private void EnableControls( bool enable )
		{
            startButton.Enabled = enable;
            stopButton.Enabled = !enable;	
		}

        // On button "Start" - start learning procedure
		private void startButton_Click(object sender, System.EventArgs e)
		{
            AccuracyBox.Text = "";

			// disable all settings controls
			EnableControls( false );
            
			toolStripStatusLabel1.Text = "Training";
            
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


        // worker thread
        private void SearchSolution()
        {
            double dNPValue = 0.0;
            if (bIsMultilpleInstance)
            {
                AccuracyBox.Text = (DTUtil.BootStrapEvaluate(out dNPValue)).ToString();
                NPVBox.Text = dNPValue.ToString();
            }
            else
            {
                AccuracyBox.Text = (DTUtil.CrossFoldEvaluate(out dNPValue)).ToString();
                NPVBox.Text = dNPValue.ToString();
            }

            toolStripStatusLabel1.Text = "Training Complete";

            // enable all settings controls
            EnableControls(true);
           
        }
	}
}
