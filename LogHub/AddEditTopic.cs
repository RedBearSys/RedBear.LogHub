using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogHub
{
    public partial class AddEditTopic : Form
    {
        private ViewMode _mode = ViewMode.Add;

        public enum ViewMode
        {
            Add,
            Edit
        }

        public ViewMode Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;

                if (value == ViewMode.Edit)
                {
                    Text = @"Edit Source";
                }
                else
                {
                    Text = @"Add Source";
                }
            }
        }
        public LogSource Source { get; set; }

        public AddEditTopic()
        {
            InitializeComponent();
            Source = new LogSource();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            errProv.Clear();
            Close();
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                Source.ConnectionString = txtConnString.Text;
                Source.Prefix = txtPrefix.Text;
                Source.Topic = txtTopic.Text;
                Source.Enabled = chkEnabled.Checked;

                btnOK.Enabled = false;
                btnCancel.Enabled = false;

                await Task.Run(() =>
                {
                    if (Mode == ViewMode.Add)
                    {
                        LogSources.Instance.Add(Source);
                    }
                    else
                    {
                        LogSources.Instance.Update(Source);
                    }
                });

                Close();
            }
        }

        private void txtTopic_Validating(object sender, CancelEventArgs e)
        {
            var value = txtTopic.Text.Replace("\\", "/");

            if (value.Length == 0)
            {
                e.Cancel = true;
                errProv.SetError(txtTopic, @"A topic must be provided.");
            }
            else if (value.Length > 260)
            {
                e.Cancel = true;
                errProv.SetError(txtTopic, @"The topic name is too long.");
            }
            else if (value.StartsWith("/") || value.EndsWith("/"))
            {
                e.Cancel = true;
                errProv.SetError(txtTopic, @"The topic name cannot begin or end with slashes.");
            }
            else if (value.Contains("?") || value.Contains("#") || value.Contains("@"))
            {
                e.Cancel = true;
                errProv.SetError(txtTopic, @"The topic name cannot contain '?', '#' or '@'.");
            }
        }

        private void txtConnString_Validating(object sender, CancelEventArgs e)
        {
            if (txtConnString.Text.Length == 0)
            {
                e.Cancel = true;
                errProv.SetError(txtConnString, @"A connection string must be provided.");
            }
        }

        private void AddEditTopic_Load(object sender, EventArgs e)
        {
            if (Mode == ViewMode.Edit)
            {
                txtTopic.Text = Source.Topic;
                txtPrefix.Text = Source.Prefix;
                txtConnString.Text = Source.ConnectionString;
                chkEnabled.Checked = Source.Enabled;
            }
        }
    }
}
