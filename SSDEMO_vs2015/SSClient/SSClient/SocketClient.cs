
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace SSClient
{
    public partial class SocketClient : Form
    {
        public SocketClient()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void SocketClient_Load(object sender, EventArgs e)
        {

            LogHelper.SetOnLog(new LogHelper.LogEvent((m) =>
            {
                txtAll.Text = txtAll.Text = (m + "\r\n"); 
                txtAll.Select(txtAll.TextLength, 0);
                txtAll.ScrollToCaret();
            }));
        }
        static EasyClient<MyPackageInfo> client = null;
        static System.Timers.Timer timer = null;
        private async void connectServer()
        {
            client = new EasyClient<MyPackageInfo>();
            client.Initialize(new MyReceiveFilter());
            client.Connected += OnClientConnected;
            client.NewPackageReceived += OnPackageReceived;
            client.Error += OnClientError;
            client.Closed += OnClientClosed;
            var connected = await client.ConnectAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 18888));
            if (connected)
            {
                //name 2 length 2 body 6 bit 心跳包
                var rs = new List<byte> { 0, 1, 0, 6 };
                rs.AddRange(System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString("1ssfff")));

                LogHelper.WriteLog("发送数据：" + DataHelper.ByteToHex(rs.ToArray(), rs.Count));
                client.Send(rs.ToArray());


                //每5秒发一次心跳
                timer = new System.Timers.Timer(50000);
                timer.Elapsed += new System.Timers.ElapsedEventHandler((s, x) =>
                {
                    if (client.IsConnected && cbHeart.Checked)
                    {
                        rs = new List<byte> { 0, 2, 0, 6 };
                        rs.AddRange(DateTime.Now.ToString("yy MM dd HH mm ss").Split(' ').Select(m => byte.Parse(m)).ToArray());

                        LogHelper.WriteLog("发送数据：" + DataHelper.ByteToHex(rs.ToArray(), rs.Count));
                        client.Send(rs.ToArray());
                    }
                });
                timer.Enabled = true;
                timer.Start();
            }
            else
            {
                LogHelper.WriteLog("连接服务器失败");
            }
        }

        private void OnPackageReceived(object sender, PackageEventArgs<MyPackageInfo> e)
        {
            LogHelper.WriteLog("收到文本下行：" + e.Package.Body);
            //16进制
            //LogHelper.WriteLog("下行：" + DataHelper.ByteToHex(e.Package.Header, e.Package.Header.Length) + DataHelper.ByteToHex(e.Package.Data, e.Package.Data.Length));

            //回复服务端确认
            if ((e.Package.Header[0] * 256 + e.Package.Header[1]) == 4)
            {
                var arr = new List<byte> { 0, 4, 0, 1, 1 };
                client.Send(arr.ToArray());
            }
        }

        private void OnClientConnected(object sender, EventArgs e)
        {
            LogHelper.WriteLog("已连接到服务器...");
        }

        private void OnClientClosed(object sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Dispose();
            }
            LogHelper.WriteLog("连接已断开...");
        }

        private void OnClientError(object sender, ErrorEventArgs e)
        {
            LogHelper.WriteLog("客户端错误：" + e.Exception.Message);
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            if(client == null || !client.IsConnected)
                connectServer();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (client != null && client.IsConnected && txtMsg.Text.Length > 0)
            {
                var arr = new List<byte> { 0, 3 };
                var txt = System.Text.Encoding.UTF8.GetBytes(txtMsg.Text);
                arr.Add((byte)(txt.Length / 256));
                arr.Add((byte)txt.Length);
                arr.AddRange(txt);
                client.Send(arr.ToArray());

                LogHelper.WriteLog("发送数据：" + DataHelper.ByteToHex(arr.ToArray(), arr.Count));
                txtMsg.Text = "";
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            LogHelper.allLines.Clear();
            LogHelper.allLines.Add("清空了..");
            LogHelper.displayLength = 0;
        }
    }
}
