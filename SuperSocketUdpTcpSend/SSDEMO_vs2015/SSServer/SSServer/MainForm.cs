using Receive.udp.Command;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Receive.udp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }
        #region Comm Function
        
        private void btn_clear_Click(object sender, EventArgs e)
        {
            LogHelper.allLines.Clear();
            LogHelper.allLines.Add("清空了..");
            LogHelper.displayLength = 0;
        }
        #endregion
        #region UDP Server
        private static IServerConfig SERVER_CONFIG;
        private static IRootConfig ROOT_CONFIG;
        public static IServerConfig DefaultServerConfig
        {
            get
            {
                return new ServerConfig
                {
                    LogCommand = true,
                    MaxConnectionNumber = 1000,
                    Mode = SocketMode.Udp,
                    Name = "Udp Socket Server",
                    ServerTypeName = "Udp Socket Server",
                    Ip = "Any",
                    Port = 18888,
                    ClearIdleSession = true,
                    ClearIdleSessionInterval = 1,
                    IdleSessionTimeOut = 50,
                    SendingQueueSize = 100,
                    ReceiveBufferSize = 50000
                };
            }
        }

        private static void initData()
        {
            //UdpAppServer.PRIVATE_KEY.Add("webSession", "f21c2a0689443179082e02f8f44079");
            //UdpAppServer.PRIVATE_KEY.Add("001", "key001");
            //UdpAppServer.PRIVATE_KEY.Add("002", "key002");
            //UdpAppServer.PRIVATE_KEY.Add("003", "key003");
        } 


        void app_SessionClosed(UdpSession session, SuperSocket.SocketBase.CloseReason value)
        {
            LogHelper.WriteLog("链接断开：" + session.SessionID );
        }

        void app_NewSessionConnected(UdpSession session)
        {
            LogHelper.WriteLog("新链接：" + session.SessionID);
        }


        private void BtnUDPServer_Click(object sender, EventArgs e)
        {
            initData();
            SERVER_CONFIG = DefaultServerConfig;
            ROOT_CONFIG = new RootConfig();
            var testServer = new UdpAppServer(app_NewSessionConnected, app_SessionClosed);

            LogHelper.SetOnLog(new LogHelper.LogEvent((m) =>
            {
                txtAll.Text = string.Join(" ", m, "\r\n");
                txtAll.Select(txtAll.TextLength, 0);
                txtAll.ScrollToCaret();
            }));

            testServer.Setup(ROOT_CONFIG, SERVER_CONFIG);
            //testServer.Start();
            if (!testServer.Start())
            {
                LogHelper.WriteLog(string.Format("UDP  {0}:{1}启动失败，请检查权限或端口是否被占用！", SERVER_CONFIG.Ip, SERVER_CONFIG.Port));
            }
        }    
        #endregion
    }
}
