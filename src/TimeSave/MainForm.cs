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
        private readonly PortFetcher _portFetcher = new PortFetcher();
        public MainForm()
        {
            InitializeComponent();
        }

        private void SourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/joachimdalen/TimeSave");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _portFetcher.PortListBox = LbPorts;
            _portFetcher.RunWorkerAsync();
        }
    }
}
