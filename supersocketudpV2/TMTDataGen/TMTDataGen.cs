using CounterTMT;
using SSServer;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using TMTDataGen.Supersocket;
using TMTDataGen.Supersocket.My;
using SuperSocket.SocketBase;

namespace TMTDataGen
{
    public partial class TMTDataGen : Form
    {

        private delegate void FlushOutPut(string msg);
        private delegate void FlushLogTxt(string msg);
        static EasyClient<MyPackageInfo> client = null;
        static System.Timers.Timer timer = null;
        public TMTDataGen()
        {
            InitializeComponent();
            AppEvents.Instance.UpdateScreenEvent += LogToScreen;
            AppEvents.Instance.UpdateLogEvent += LogToTxt;
        }
        private void LogToScreen(string st)
        {
            if (this.richLog.InvokeRequired)
            {
                var flush = new FlushOutPut(LogToScreen);
                this.Invoke(flush, new object[] { st });
            }
            else
            {
                var msg = string.Format("{0}   {1}", DateTime.Now.ToString(Settings.NormalDateTimeFormatDash), st);



                if (richLog.Text.Length >= 2000)
                {
                    var index = richLog.Text.Length;
                    richLog.Text = (msg + "\r\n" + richLog.Text).Substring(0, 2000);
                }
                else
                {
                    richLog.Text = (msg + "\r\n" + richLog.Text);
                }
            }
        }
        private void LogToTxt(byte[] st)
        {
            string result = string.Empty;
            try
            {
                string msg = Encoding.Default.GetString(st); //读出的数据为16进制

                for (int iCountRecv = 0; iCountRecv < st.Length; iCountRecv++)
                {
                    string hexOutput = System.Convert.ToString(st[iCountRecv], 16);
                    int sLen = Encoding.Default.GetByteCount(hexOutput);
                    if (sLen == 1)
                        hexOutput = '0' + hexOutput;
                    result += hexOutput; //Convert.ToString(byte, 16)把byte转化成十六进制string 
                }
                result = result.ToUpper();

            }
            catch (Exception ex)
            {
                AppLog.Error(st + " 16 to string Err " + ex.ToString());
            }
            AppLog.Info(result);
            //if (this.richLog.InvokeRequired)
            //{
            //    var flush = new FlushLogTxt(LogToTxt);
            //    this.Invoke(flush, new object[] { st });
            //}
            //else
            //{
            //    var msg = string.Format("{0}   {1}", DateTime.Now.ToString(Settings.NormalDateTimeFormatDash), st);

            //    if (richLog.Text.Length >= 500)
            //    {
            //        var index = richLog.Text.Length;
            //        richLog.Text = (st + "\r\n" + richLog.Text).Substring(0, 500);
            //    }
            //    else
            //    {
            //        richLog.Text = (st + "\r\n" + richLog.Text);
            //    }
            //}
        }
        private void BtnUDPServer_Click(object sender, EventArgs e)
        {
            if (client == null || !client.IsConnected)
                connectServer();


            //MyTcpClient c = new MyTcpClient("127.0.0.1", 2020);
            //SuperSocketMessage.SSMessage msg = new SuperSocketMessage.SSMessage();
            //while (true)
            //{
            //    string m = Console.ReadLine();
            //    msg.Type = 1;
            //    msg.Message = m;
            //    c.Send(msg.ToBytes());
            //}
        }
        private void OnClientConnected(object sender, EventArgs e)
        {
            AppLog.Info("已连接到服务器...");
        }

        private void OnClientClosed(object sender, EventArgs e)
        {
            AppLog.Info("连接已断开...");
        }

        private void OnClientError(object sender, ErrorEventArgs e)
        {
            AppLog.Info("客户端错误：" + e.Exception.Message);
        }
        private void OnPackageReceived(object sender, PackageEventArgs<MyPackageInfo> e)
        {
            AppLog.Info("收到文本下行：" + e.Package.Body);
            //16进制
            //  AppLog.Info("下行：" + DataHelper.ByteToHex(e.Package.Header, e.Package.Header.Length) + DataHelper.ByteToHex(e.Package.Data, e.Package.Data.Length));

            //回复服务端确认
            if ((e.Package.Header[0] * 256 + e.Package.Header[1]) == 4)
            {
                var arr = new List<byte> { 0, 4, 0, 1, 1 };
                client.Send(arr.ToArray());
            }
        }
        private async void connectServer()
        {
            client = new EasyClient<MyPackageInfo>();
            client.Initialize(new MyReceiveFilter());
            client.Connected += OnClientConnected;
            client.NewPackageReceived += OnPackageReceived;
            client.Error += OnClientError;
            client.Closed += OnClientClosed;
           
            int port = 0;


            if (!int.TryParse(txtPort.Text.Trim(),out  port) )
            {
                AppEvents.Instance.OnUpdateScreenRun("端口设置错误");
                return;
            }

            var connected = await client.ConnectAsync(new IPEndPoint(IPAddress.Parse(txtIP.Text.Trim()), port));
            if (connected)
            {
                //name 2 length 2 body 6 bit 心跳包
                var rs = new List<byte> { 0, 1, 0, 6 };
                rs.AddRange(System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString("1ssfff")));

                AppLog.Info("发送数据：" + DataHelper.ByteToHex(rs.ToArray(), rs.Count));
                client.Send(rs.ToArray());


                //每5秒发一次心跳
                timer = new System.Timers.Timer(200000);
                timer.Elapsed += new System.Timers.ElapsedEventHandler((s, x) =>
                {
                    if (client.IsConnected && chkTimingHeart.Checked)
                    {
                        rs = new List<byte> { 0, 2, 0, 6 };
                        rs.AddRange(DateTime.Now.ToString("yy MM dd HH mm ss").Split(' ').Select(m => byte.Parse(m)).ToArray());

                        AppLog.Info("发送数据：" + DataHelper.ByteToHex(rs.ToArray(), rs.Count));
                        client.Send(rs.ToArray());
                    }
                });
                timer.Enabled = true;
                timer.Start();
            }
            else
            {
                AppLog.Info("连接服务器失败");
                            
                    AppEvents.Instance.OnUpdateScreenRun("连接服务器失败");
            }
        }

        private void BtnSendmsg_Click(object sender, EventArgs e)
        {
            if (client != null && client.IsConnected && txtMsg.Text.Length > 0)
            {
                var arr = new List<byte> { 0, 3 };
                var txt = System.Text.Encoding.UTF8.GetBytes(txtMsg.Text);
                arr.Add((byte)(txt.Length / 256));
                arr.Add((byte)txt.Length);
                arr.AddRange(txt);
                client.Send(arr.ToArray());

                AppLog.Info("发送数据：" + DataHelper.ByteToHex(arr.ToArray(), arr.Count));
                txtMsg.Text = "";
                AppEvents.Instance.OnUpdateScreenRun("发送数据：" + DataHelper.ByteToHex(arr.ToArray(), arr.Count));
            }
            else
            AppEvents.Instance.OnUpdateScreenRun("未发送数据");
        }
    }
}
