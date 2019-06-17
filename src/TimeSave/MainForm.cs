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
        private int _failCount = 0;
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
            try
            {
                var ports = SerialPort.GetPortNames();
                BwPortFetcher.ReportProgress(0, ports);
                Debug.WriteLine("Did work, sleeping for 5 seconds");
                Thread.Sleep(5000);

            }
            catch
            {
                _failCount++;
            }

        }

        private void BwPortFetcher_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.UserState is object[] ports && (ports.Length != 0 && !_isRunning))
                {
                    _failCount = 0;
                    Debug.WriteLine("Found ports, appending to list");
                    LbPorts.Items.Clear();
                    LbPorts.Items.AddRange(ports);
                }
                else
                {
                    Debug.WriteLine("No ports found, restarting worker");
                    if (_failCount >= 3)
                    {
                        MessageBox.Show(@"An error occurred while checking for new ports. Please restart the application.",
                            @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Debug.WriteLine("Stopped due to fail count");
                    }
                    else
                    {
                        // Restart the worker
                        BwPortFetcher.RunWorkerAsync();
                    }
                }
            }
            catch
            {
                _failCount++;
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BwPortFetcher.RunWorkerAsync();
        }
    }
}
