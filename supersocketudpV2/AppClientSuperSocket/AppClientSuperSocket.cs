using IMESAgent.GUI;
using IMESAgent.SocketClientEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppClientSuperSocket
{
    public partial class AppClientSuperSocket : Form
    {
        private ucConfiguration ucConfiguration = null;
        private ucConnection ucConnection = null;
        private ucTestMode ucTestMode = null;
        private ClientHelper helper = null;
        private const int WM_WINDOWCLOSE = 0x0010;

        private delegate void RefreshAgentStatus(AgentStatus status);
        private delegate void FlushDatetime(string datetime);
        private delegate void FlushBlockingCount(string count);
        public AppClientSuperSocket()
        {
            InitializeComponent();
            InitializeEvents();
            InitializeUserControl();
            InitializeIPAddress();
        }

        private void InitializeUserControl()
        {
            ucConnection = new ucConnection();
            ucConnection.Parent = TClient;
            ucConnection.Dock = DockStyle.Fill;

            ucConfiguration = new ucConfiguration();
            ucConfiguration.Parent = TClientConfig;
            ucConfiguration.Dock = DockStyle.Fill;

#if TestMode
            InitializeDebugMode();
#endif
        }
        private void InitializeDebugMode()
        {
            var tabPage = new TabPage();
            tabPage.Text = "Debug Mode";

            Tabctl.TabPages.Add(tabPage);

            ucTestMode = new ucTestMode();
            ucTestMode.Parent = tabPage;
            ucTestMode.Dock = DockStyle.Fill;
        }
        private void InitializeEvents()
        {
            this.Load += FrmAgnetMain_Load;

            AppEvents.Instance.FlushActivedDatetimeEvent += UpdateStartTime;
            AppEvents.Instance.ShowDialogEvent += DoShowDialog;
            AppEvents.Instance.UpdateConnectionStatusEvent += UpdateStatus;
            AppEvents.Instance.FlushBlockingCountEvent += UpdateBlockingCount;
        }
        private void UpdateBlockingCount(string count)
        {
            if (this.InvokeRequired)
            {
                var flush = new FlushBlockingCount(UpdateBlockingCount);
                this.Invoke(flush, new object[] { count });
            }
            else
            {
                toolStripStatusLabel1.Text = count;
                //tslBlockingStatusField.Text = count;
            }
        }
        private void DoShowDialog(string msg, string title)
        {
            PopupWindow.ShowDialog(msg, title);
        }
        private void InitializeIPAddress()
        {
            helper = ClientHelper.Instance;

            if (helper == null) throw new Exception(ExceptionMessage.NoIpAddress);

            txtAgent.Text = helper.GetIpAddress().ToString();
            txtAgent.ReadOnly = true;
        }
        private void FrmAgnetMain_Load(object sender, EventArgs e)
        {
            try
            {
                helper.SetupTasks(Application.StartupPath); //sunjie 8 
                this.Text = "IMES Agent - " + Application.ProductVersion;

                UpdateStartTime(DateTime.Now.ToString(Settings.NormalDateTimeFormat));
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                PopupWindow.ShowDialog("Error in Load : " + ex.Message, UserMessage.Error);
            }
        }
        private void UpdateStartTime(string datetime)
        {
            if (this.InvokeRequired)
            {
                var flush = new FlushDatetime(UpdateStartTime);
                this.Invoke(flush, new object[] { datetime });
            }
            else
            {
                toolStripStatusLabel1.Text = datetime;
            }
        }
        public void UpdateStatus(AgentStatus status)
        {
            if (lbStatus.InvokeRequired)
            {
                var refresh = new RefreshAgentStatus(UpdateStatus);
                this.Invoke(refresh, new object[] { status });
            }
            else
            {
                if (status == AgentStatus.ONLINE)
                {
                    if (!string.Equals(lbStatus.Text, status.ToString()))
                    {
                        lbStatus.Text = status.ToString();
                        lbStatus.BackColor = System.Drawing.Color.LimeGreen;
                        lbStatus.ForeColor = System.Drawing.Color.Black;
                    }
                }
                else
                {
                    if (status == AgentStatus.ERROR || status == AgentStatus.OFFLINE)
                    {
                        if (!string.Equals(lbStatus.Text, status.ToString()))
                        {
                            lbStatus.Text = status.ToString();
                            lbStatus.BackColor = System.Drawing.Color.Red;
                            lbStatus.ForeColor = System.Drawing.Color.White;
                        }
                    }
                }
            }
        }
    }
}
