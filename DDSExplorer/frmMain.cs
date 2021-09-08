using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace DDSExplorer
{
    public partial class frmMain : Form
    {
        BackgroundWorker bgworker;

        bool bIsMultilpleInstance = false;

        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new frmMain());
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if(rbDTClassifier.Checked) 
            {
                frmDTClassifier _frmdtclassifier = new frmDTClassifier(bIsMultilpleInstance, this);
               _frmdtclassifier.Show();
            }

            else if (rbNNClassifier.Checked)
            {
               frmNNClassifier _frmnnclassifier = new frmNNClassifier(bIsMultilpleInstance, this);
               _frmnnclassifier.Show();
            }
            else
            {
                MessageBox.Show("Classifier info is not selected");
                return;
            }

            toolStripStatusLabel.Text = "Classifier is working";
            btnLoad.Enabled = false;
            btnClear.Enabled = false;
            btnGo.Enabled = false;

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (rbMultiFold.Checked == false && rbSingleFold.Checked == false)
            {
                MessageBox.Show("Dataset info is not selected");
                return;
            }

            if (rbMultiFold.Checked)
                bIsMultilpleInstance = true;
            else if (rbSingleFold.Checked)
                bIsMultilpleInstance = false;

            // show file selection dialog
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadUtil.SetTableName(openFileDialog.FileName);
                btnLoad.Enabled = false;

                bgworker = new BackgroundWorker();

                bgworker.DoWork += LoadData_DoWork;
                bgworker.RunWorkerCompleted += LoadComplete_RunWorkerCompleted;
                bgworker.RunWorkerAsync();
            }
        }

        public void LoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            toolStripStatusLabel.Text = "Loading";
            LoadUtil.LoadData(bIsMultilpleInstance);   
        }

        public void LoadComplete_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvData.DataSource = LoadUtil.GetDataTable();

            toolStripStatusLabel.Text = "Loading Complete";
            btnClear.Enabled = true;
            grpClassifier.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            LoadUtil.SetTableName("");
            LoadUtil.SetDataTable(null);
            dgvData.DataSource = null;
            toolStripStatusLabel.Text = "";
            grpClassifier.Enabled = false;
            btnClear.Enabled = false;
            btnLoad.Enabled = true;

        }

        public void NNClassifierNotification()
        {
            btnClear.Enabled = true;
            btnGo.Enabled = true;
            toolStripStatusLabel.Text = null;
        }

        public void DTClassifierNotification()
        {
            btnClear.Enabled = true;
            btnGo.Enabled = true;
            toolStripStatusLabel.Text = null;
        }


    }
}