using System;
using System.IO;
using System.Windows.Forms;
using AppClientSuperSocket;
using IMESAgent.SocketClientEngine;
using IMESAgent.SocketClientEngine.FileLoaders;

namespace IMESAgent.GUI
{
    public partial class ucConnection : UserControl
    {
        private delegate void FlushOutPut(string msg);
        private ClientHelper helper = null;

        public ucConnection()
        {
            InitializeComponent();

            this.Load += Connection_Load;
            AppEvents.Instance.UpdateScreenEvent += LogToScreen;
        }

        private void Initialize()
        {
            if (helper.ConfigInfo != null)
            {
                var agentInfo = helper.ConfigInfo;

                txtServerAddress.Text = agentInfo.SocketIpAddress;
                txtServerPort.Text = agentInfo.SocketPortNumber.ToString();
                txtBackDir.Text = agentInfo.BackupDirectory;
                cbRetentionPeriod.Text = agentInfo.LogRetentionPeroid;
            }
        }

        private void Connection_Load(object sender, EventArgs e)
        {
            helper = ClientHelper.Instance;

            txtBackDir.ReadOnly = true;
            Initialize();
            //helper.SetupTasks();
        }
        private void btnApplyServerSetting_Click(object sender, EventArgs e)
        {
            using (var dialog = new frmPassword())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var result = DialogResult.None;
                    var newIP = txtServerAddress.Text.TrimEnd();
                    var newPort = txtServerPort.Text.TrimEnd();

                    if (helper.ConfigInfo == null) return;

                    if (string.Equals(newIP, helper.ConfigInfo.SocketIpAddress) && string.Equals(newPort, helper.ConfigInfo.SocketPortNumber.ToString()))
                    {
                        result = PopupWindow.ShowDialog(ExceptionMessage.NothingChanged);
                    }
                    else
                    {
                        // popup warning message about how to active new configuration 
                        PopupWindow.ShowDialog("The IMESAgent must be restarted to active the new configuration.", UserMessage.Warning);
                        helper.SaveIPAddressInfo(newIP, newPort);
                    }
                }
            }
        }

        private void LogToScreen(string st)
        {
            if (this.lstOutput.InvokeRequired)
            {
                var flush = new FlushOutPut(LogToScreen);
                this.Invoke(flush, new object[] { st });
            }
            else
            {
                var msg = string.Format("{0}   {1}", DateTime.Now.ToString(Settings.NormalDateTimeFormatDash), st);

                if (lstOutput.Items.Count >= 500)
                {
                    var index = lstOutput.Items.Count;
                    lstOutput.Items.RemoveAt(index - 1);
                }

                lstOutput.Items.Insert(0, msg);
            }
        }
        private void btnBroswer_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtBackDir.Text = dialog.SelectedPath;
                }
            }
        }

        private void btnApplyLocalSetting_Click(object sender, EventArgs e)
        {
            if (helper.ConfigInfo == null) return;

            var result = DialogResult.None;
            var path = txtBackDir.Text.TrimEnd();
            var days = cbRetentionPeriod.Text;

            if (string.Equals(path, helper.ConfigInfo.BackupDirectory) && string.Equals(days, helper.ConfigInfo.LogRetentionPeroid))
            {
                result = PopupWindow.ShowDialog(ExceptionMessage.NothingChanged);
            }
            else
            {
                result = PopupWindow.ShowDialog(ExceptionMessage.ConfirmModification, UserMessage.Question);

                if (result == DialogResult.Yes)
                {
                    PopupWindow.ShowDialog("The IMESAgent must be restarted to active the new configuration.", UserMessage.Warning);
                    helper.SaveLogConfig(path, days);
                }
            }
        }

        private void btnGetLog_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBackDir.Text))
            {
                if (System.IO.Directory.Exists(txtBackDir.Text))
                {
                    System.Diagnostics.Process.Start(txtBackDir.Text);
                }
            }
        }
    }
}
