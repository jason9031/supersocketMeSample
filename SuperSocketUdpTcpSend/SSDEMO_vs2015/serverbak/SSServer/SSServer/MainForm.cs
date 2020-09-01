using SSServer.Command;
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

namespace SSServer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        
        MyServer app;
        private void MainForm_Load(object sender, EventArgs e)
        {
            var config = new SuperSocket.SocketBase.Config.ServerConfig()
            {
                Name = "SSServer",
                ServerTypeName = "SServer",
                ClearIdleSession= true, //60秒执行一次清理90秒没数据传送的连接
                ClearIdleSessionInterval = 60,
                IdleSessionTimeOut = 90,
                MaxRequestLength = 2048, //最大包长度
                Mode=SuperSocket.SocketBase.SocketMode.Udp,
                Ip = "Any",
                Port = 18888,
                MaxConnectionNumber = 60000,
            };
            app = new MyServer(app_NewSessionConnected, app_SessionClosed);
            LogHelper.SetOnLog(new LogHelper.LogEvent((m) =>
            {
                txtAll.Text = string.Join(" ", m, "\r\n");
                txtAll.Select(txtAll.TextLength, 0);
                txtAll.ScrollToCaret();
            }));
            app.Setup(config);
            if (!app.Start())
            {
                LogHelper.WriteLog(string.Format("Socket {0}:{1}启动失败，请检查权限或端口是否被占用！", config.Ip, config.Port));
            }
        }

        void app_SessionClosed(MySession session, SuperSocket.SocketBase.CloseReason value)
        {
            LogHelper.WriteLog("链接断开：" + session.SessionID);
        }

        void app_NewSessionConnected(MySession session)
        {
            LogHelper.WriteLog("新链接：" + session.SessionID);
        }
        
        private void btn_clear_Click(object sender, EventArgs e)
        {
            LogHelper.allLines.Clear();
            LogHelper.allLines.Add("清空了..");
            LogHelper.displayLength = 0;
        }

        private void btn_sendmsg_Click(object sender, EventArgs e)
        {
            if (app != null && app.State == SuperSocket.SocketBase.ServerState.Running && txt_msg.Text.Length > 0)
            {
                foreach (var item in app.GetAllSessions())
                {
                    new CMD_0004_DownText().Push(item, txt_msg.Text);
                }

                txt_msg.Text = "";
            }
        }
    }
}
