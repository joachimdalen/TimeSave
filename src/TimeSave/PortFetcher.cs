using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace TimeSave
{
    internal class PortFetcher : BackgroundWorker
    {
        private int _failCount = 0;
        public ListBox PortListBox { get; internal set; }

        internal PortFetcher()
        {
            WorkerReportsProgress = true;
        }
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            try
            {
                var ports = SerialPort.GetPortNames();
                ReportProgress(0, ports);
                Debug.WriteLine("Did work, sleeping for 5 seconds");
                Thread.Sleep(5000);

            }
            catch (Exception exception)
            {
                Debug.Write(exception);
                _failCount++;
            }
        }

        protected override void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.UserState is object[] ports && (ports.Length != 0))
                {
                    _failCount = 0;
                    Debug.WriteLine("Found ports, appending to list");
                    PortListBox.Items.Clear();
                    PortListBox.Items.AddRange((object[])e.UserState);
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
                        RunWorkerAsync();
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.Write(exception);
                _failCount++;
            }
        }
    }
}
