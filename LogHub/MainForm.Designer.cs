namespace LogHub
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbStop = new System.Windows.Forms.ToolStripButton();
            this.lstSources = new System.Windows.Forms.DataGridView();
            this.topicDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prefixDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.logSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopLogHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstSources)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logSourceBindingSource)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbEdit,
            this.tsbDelete,
            this.toolStripSeparator1,
            this.tsbConfig,
            this.toolStripSeparator2,
            this.tsbStop});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(518, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(88, 22);
            this.tsbAdd.Text = "Add Source";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbEdit
            // 
            this.tsbEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEdit.Enabled = false;
            this.tsbEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsbEdit.Image")));
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(23, 22);
            this.tsbEdit.Text = "Edit Source";
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDelete.Enabled = false;
            this.tsbDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbDelete.Image")));
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbDelete.Text = "Delete";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbConfig
            // 
            this.tsbConfig.Image = ((System.Drawing.Image)(resources.GetObject("tsbConfig.Image")));
            this.tsbConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConfig.Name = "tsbConfig";
            this.tsbConfig.Size = new System.Drawing.Size(63, 22);
            this.tsbConfig.Text = "Config";
            this.tsbConfig.Click += new System.EventHandler(this.tsbConfig_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbStop
            // 
            this.tsbStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbStop.Image")));
            this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStop.Name = "tsbStop";
            this.tsbStop.Size = new System.Drawing.Size(50, 22);
            this.tsbStop.Text = "Quit";
            this.tsbStop.Click += new System.EventHandler(this.tsbStop_Click);
            // 
            // lstSources
            // 
            this.lstSources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSources.AutoGenerateColumns = false;
            this.lstSources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lstSources.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.topicDataGridViewTextBoxColumn,
            this.prefixDataGridViewTextBoxColumn,
            this.enabledDataGridViewCheckBoxColumn});
            this.lstSources.DataSource = this.logSourceBindingSource;
            this.lstSources.Location = new System.Drawing.Point(12, 28);
            this.lstSources.MultiSelect = false;
            this.lstSources.Name = "lstSources";
            this.lstSources.ReadOnly = true;
            this.lstSources.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lstSources.Size = new System.Drawing.Size(494, 225);
            this.lstSources.TabIndex = 3;
            this.lstSources.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.lstSources_DataBindingComplete);
            this.lstSources.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.lstSources_RowStateChanged);
            this.lstSources.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstSources_MouseDoubleClick);
            // 
            // topicDataGridViewTextBoxColumn
            // 
            this.topicDataGridViewTextBoxColumn.DataPropertyName = "Topic";
            this.topicDataGridViewTextBoxColumn.HeaderText = "Topic";
            this.topicDataGridViewTextBoxColumn.Name = "topicDataGridViewTextBoxColumn";
            this.topicDataGridViewTextBoxColumn.ReadOnly = true;
            this.topicDataGridViewTextBoxColumn.Width = 200;
            // 
            // prefixDataGridViewTextBoxColumn
            // 
            this.prefixDataGridViewTextBoxColumn.DataPropertyName = "Prefix";
            this.prefixDataGridViewTextBoxColumn.HeaderText = "Prefix";
            this.prefixDataGridViewTextBoxColumn.Name = "prefixDataGridViewTextBoxColumn";
            this.prefixDataGridViewTextBoxColumn.ReadOnly = true;
            this.prefixDataGridViewTextBoxColumn.Width = 200;
            // 
            // enabledDataGridViewCheckBoxColumn
            // 
            this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
            this.enabledDataGridViewCheckBoxColumn.HeaderText = "Enabled";
            this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
            this.enabledDataGridViewCheckBoxColumn.ReadOnly = true;
            this.enabledDataGridViewCheckBoxColumn.Width = 50;
            // 
            // logSourceBindingSource
            // 
            this.logSourceBindingSource.DataSource = typeof(LogHub.LogSource);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "LogHub";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.stopLogHubToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(104, 48);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // stopLogHubToolStripMenuItem
            // 
            this.stopLogHubToolStripMenuItem.Name = "stopLogHubToolStripMenuItem";
            this.stopLogHubToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.stopLogHubToolStripMenuItem.Text = "Quit";
            this.stopLogHubToolStripMenuItem.Click += new System.EventHandler(this.stopLogHubToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 265);
            this.Controls.Add(this.lstSources);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "LogHub";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseEnter += new System.EventHandler(this.MainForm_MouseEnter);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstSources)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logSourceBindingSource)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbStop;
        private System.Windows.Forms.DataGridView lstSources;
        private System.Windows.Forms.BindingSource logSourceBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn topicDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prefixDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem stopLogHubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

