using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace TimeSave
{
    public partial class MainForm : Form
    {
        private bool _isRunning = false;
        public MainForm()
        {
            InitializeComponent();
        }

        private void SourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/joachimdalen/TimeSave");
        }

        private void BwPortFetcher_DoWork(object sender, DoWorkEventArgs e)
        {
            var ports = SerialPort.GetPortNames();
            BwPortFetcher.ReportProgress(0, ports);
            Debug.WriteLine("Did work, sleeping for 5 seconds");
            Thread.Sleep(5000);
        }

        private void BwPortFetcher_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.UserState is object[] ports && (ports.Length != 0 && !_isRunning))
            {
                Debug.WriteLine("Found ports, appending to list");
                LbPorts.Items.Clear();
                LbPorts.Items.AddRange(ports);
            }
            else
            {
                Debug.WriteLine("No ports found, restarting worker");
                // Restart the worker
                BwPortFetcher.RunWorkerAsync();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BwPortFetcher.RunWorkerAsync();
        }
    }
}
