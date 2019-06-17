namespace TimeSave
{
    partial class MainForm
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
            this.LbPorts = new System.Windows.Forms.ListBox();
            this.BtnStartRead = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // LbPorts
            // 
            this.LbPorts.FormattingEnabled = true;
            this.LbPorts.Location = new System.Drawing.Point(12, 44);
            this.LbPorts.Name = "LbPorts";
            this.LbPorts.Size = new System.Drawing.Size(211, 56);
            this.LbPorts.TabIndex = 0;
            this.LbPorts.SelectedIndexChanged += new System.EventHandler(this.LbPorts_SelectedIndexChanged);
            // 
            // BtnStartRead
            // 
            this.BtnStartRead.Enabled = false;
            this.BtnStartRead.Location = new System.Drawing.Point(12, 106);
            this.BtnStartRead.Name = "BtnStartRead";
            this.BtnStartRead.Size = new System.Drawing.Size(211, 23);
            this.BtnStartRead.TabIndex = 1;
            this.BtnStartRead.Text = "Start";
            this.BtnStartRead.UseVisualStyleBackColor = true;
            this.BtnStartRead.Click += new System.EventHandler(this.BtnStartRead_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(12, 135);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(211, 23);
            this.BtnExit.TabIndex = 2;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.SourceToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(235, 24);
            this.MainMenuStrip.TabIndex = 3;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // SourceToolStripMenuItem
            // 
            this.SourceToolStripMenuItem.Name = "SourceToolStripMenuItem";
            this.SourceToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.SourceToolStripMenuItem.Text = "Source";
            this.SourceToolStripMenuItem.Click += new System.EventHandler(this.SourceToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 170);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnStartRead);
            this.Controls.Add(this.LbPorts);
            this.Controls.Add(this.MainMenuStrip);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TimeSave";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LbPorts;
        private System.Windows.Forms.Button BtnStartRead;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SourceToolStripMenuItem;
    }
}

