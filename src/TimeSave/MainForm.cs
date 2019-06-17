using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace TimeSave
{
    public partial class MainForm : Form
    {
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

        private void LbPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnStartRead.Enabled = true;
        }

        private void BtnStartRead_Click(object sender, EventArgs e)
        {
            using (var s = new SerialManager())
            {
                s.OnComplete += (o, data) =>
                {
                    using (var sfd = new SaveFileDialog())
                    {
                        sfd.Filter = @"Text files (*.txt)|*.txt|All files (*.*)|*.*";
                        sfd.Title = @"Save file";
                        sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        var result = sfd.ShowDialog();
                        if (result != DialogResult.OK)
                        {
                            return;
                        }

                        File.WriteAllText(sfd.FileName, data);
                        MessageBox.Show($@"File written to {sfd.FileName}", @"File written", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    BtnStartRead.Enabled = true;
                    LbPorts.Enabled = true;
                };
                BtnStartRead.Enabled = false;
                LbPorts.Enabled = false;
                var worker = new BackgroundWorker();
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerAsync(new object[] { LbPorts.SelectedItem.ToString(), s });
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!(e.Argument is object[] param))
            {
                return;
            }

            var sm = (SerialManager)param[1];
            var port = (string)param[0];
            sm.Run(port);
        }
    }
}
