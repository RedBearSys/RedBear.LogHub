using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogHub
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            var form = new AddEditTopic();
            form.ShowDialog();
            RefreshList();
        }

        private void RefreshList()
        {
            lstSources.DataSource = null;
            lstSources.DataSource = LogSources.Instance.Sources;
            tsbEdit.Enabled = false;
            tsbDelete.Enabled = false;
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            var source = (LogSource) lstSources.SelectedRows[0].DataBoundItem;
            var form = new AddEditTopic
            {
                Mode = AddEditTopic.ViewMode.Edit,
                Source = source
            };
            form.ShowDialog();
            RefreshList();
        }

        private void lstSources_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstSources.SelectedRows.Count > 0)
            {
                var source = (LogSource)lstSources.SelectedRows[0].DataBoundItem;

                if (source != null)
                {
                    var form = new AddEditTopic
                    {
                        Mode = AddEditTopic.ViewMode.Edit,
                        Source = source
                    };
                    form.ShowDialog();
                    RefreshList();
                }
            }
        }

        private void lstSources_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            lstSources.ClearSelection();
        }

        private void lstSources_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (lstSources.SelectedRows.Count > 0)
                {
                    tsbEdit.Enabled = true;
                    tsbDelete.Enabled = true;
                }
            }
            else
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show(@"Are you sure you want to delete this source?", @"Delete Source",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var source = (LogSource)lstSources.SelectedRows[0].DataBoundItem;
                LogSources.Instance.Delete(source);
                RefreshList();
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            Text += @" " + Application.ProductVersion;
            notifyIcon.Text += @" " + Application.ProductVersion;
            Visible = false;
            ShowInTaskbar = false;

            LogSources.Instance.Load();
            RefreshList();

            await Task.Run(() => LogSources.Instance.StartListeners());
        }

        private void tsbStop_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Visible = true;
            ShowInTaskbar = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall && e.CloseReason != CloseReason.WindowsShutDown)
            {
                e.Cancel = true;
                Visible = false;
                ShowInTaskbar = false;
            }
        }

        private void stopLogHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Visible = true;
            ShowInTaskbar = true;
        }

        private void tsbConfig_Click(object sender, EventArgs e)
        {
            var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LogHub", "NLog.xml");
            Process.Start("explorer.exe", $"/select,\"{file}\"");
        }

        private void CheckButtonVisibility()
        {
            if (lstSources.SelectedRows.Count > 0)
            {
                tsbEdit.Enabled = true;
                tsbDelete.Enabled = true;
            }
            else
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
            }
        }

        private void MainForm_MouseEnter(object sender, EventArgs e)
        {
            CheckButtonVisibility();
        }
    }
}
