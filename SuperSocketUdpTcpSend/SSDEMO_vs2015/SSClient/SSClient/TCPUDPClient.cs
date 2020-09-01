
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPUDPClient
{
    public partial class SSClient : Form
    {
        public SSClient()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
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
            var connected = await client.ConnectAsync(new IPEndPoint(IPAddress.Parse(txtIP.Text.ToString().Trim()), int.Parse(txtPort.Text.ToString().Trim())));
            if (connected)
            {
                //name 2 length 2 body 6 bit 心跳包
                var rs = new List<byte> { 0, 1, 0, 6 };
                string timeLogin = DateTime.Now.ToString("1ssfff");


                //rs.AddRange(System.Text.Encoding.UTF8.GetBytes(timeLogin));

                //LogHelper.WriteLog("发送login数据：" + DataHelper.ByteToHex(rs.ToArray(), rs.Count));
                //client.Send(rs.ToArray());

                byte[] dataHeart = HeartToPack(txtNickName.Text.ToString().Trim(), txtIDType.Text.ToString().Trim());
                LogHelper.WriteLog("发送 Login 26 数据：" + DataHelper.ByteToHex(dataHeart.ToArray(), dataHeart.Length));
                client.Send(dataHeart.ToArray());

                //每5秒发一次心跳
                timer = new System.Timers.Timer(20000);
                timer.Elapsed += new System.Timers.ElapsedEventHandler((s, x) =>
                {
                    if (client.IsConnected && cbHeart.Checked)
                    {
                        //rs = new List<byte> { 0, 2, 0, 6 };
                        //rs.AddRange(DateTime.Now.ToString("yy MM dd HH mm ss").Split(' ').Select(m => byte.Parse(m)).ToArray());
                        //string timeIDHeart = "#" + txtNickName.Text.ToString().Trim() + "#" + txtIDType.Text.ToString().Trim();
                        //rs.AddRange(System.Text.Encoding.UTF8.GetBytes(timeIDHeart.PadLeft(26, '$')));
                        //LogHelper.WriteLog("发送数据：" + DataHelper.ByteToHex(rs.ToArray(), rs.Count) + "  Heart Length " + rs.Count + "  " + timeIDHeart);
                        //client.Send(rs.ToArray());

                        dataHeart = HeartToPack(txtNickName.Text.ToString().Trim(), txtIDType.Text.ToString().Trim());
                        LogHelper.WriteLog("发送 Heart 26 数据：" + DataHelper.ByteToHex(dataHeart.ToArray(), dataHeart.Length) );
                        client.Send(dataHeart.ToArray());
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

        private void OnClientError(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            LogHelper.WriteLog("客户端错误：" + e.Exception.Message);
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            if (client == null || !client.IsConnected)
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
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            LogHelper.allLines.Clear();
            LogHelper.allLines.Add("清空了..");
            LogHelper.displayLength = 0;
        }
        public static string ReadFromLogFileUsingPwd(string file = "")//  读取文件 jjjsss 目录 + 内容
        {
            string strPath = System.Windows.Forms.Application.StartupPath + @"\log\";

            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }

            if (string.IsNullOrEmpty(file))
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = false;//该值确定是否可以选择多个文件
                dialog.Title = "请选择文件夹";
                dialog.Filter = "所有文件(*.*)|*.*";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    strPath = dialog.FileName;
                    logPath = strPath;
                }
            }
            else
            {
                strPath = file;
            }
            string ReturnStrMsg = string.Empty;
            if (!File.Exists(strPath))
            {
                ReturnStrMsg = "";
            }
            else
            {
                int ReadLineCount = 0;
                /**////从指定的目录以打开或者创建的形式读取日志文件
                FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Read);// (Server.MapPath("upedFile") + "\\logfile.txt", FileMode.OpenOrCreate, FileAccess.Read);
                /**////定义输出字符串
                StringBuilder output = new StringBuilder();
                /**////初始化该字符串的长度为0
                output.Length = 0;
                /**////为上面创建的文件流创建读取数据流
                StreamReader read = new StreamReader(fs);
                /**////设置当前流的起始位置为文件流的起始点
                read.BaseStream.Seek(0, SeekOrigin.Begin);
                /**////读取文件
                while ((read.Peek() > -1))
                {
                    /**////取文件的一行内容并换行               StreamReader sr = new StreamReader( fn );  string st = string.Empty; while ( !sr.EndOfStream ) {st = sr.ReadLine(); }
                    string ReadLineBuf = read.ReadLine();
                    if (ReadLineBuf.Trim() != string.Empty)
                    {
                        output.Append(ReadLineBuf.Trim() + "\n");
                        ReadLineCount++;
                    }
                }
                /**////关闭释放读数据流
                read.Close();
                ReturnStrMsg = output.ToString().Trim();
            }
            return ReturnStrMsg;
        }
        protected virtual FileInfo[] GetFiles(string folder, string filter)
        {
            try
            {
                FileInfo[] files = null;
                if (Directory.Exists(folder))
                {
                    var info = new DirectoryInfo(folder);
                    var option = SearchOption.AllDirectories;



                    //    Engine.Logger.Info(" info.GetFiles(filter, option)  " + option.ToString() + "----" + folder + "---" + filter.ToString());

                    files = info.GetFiles(filter, option);

                    //Array.Sort(files, new MyDateSorter());

                }
                return files;
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public static string logPath;
        public static void writelog(string strLog, string strConfigFile = "", bool bConfigFiles = false)
        {
            string strPath = System.Windows.Forms.Application.StartupPath + @"\LogExp\";
            strPath = string.IsNullOrEmpty(logPath) ? strPath : (System.IO.Path.GetDirectoryName(logPath) + @"\");
            string strLogPathName = strPath + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now).Substring(0, 10) + "_locallog" + ".dt";
            try
            {
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
            }
            catch (Exception ex)
            {
                return;
            }
            string strLogMessage = string.Empty;
            StreamWriter swLog;
            if ((!bConfigFiles) && (string.IsNullOrWhiteSpace(strConfigFile)))
            {
                strLogMessage = string.Format("{0}:#{1}", DateTime.Now, (strLog));

            }
            else if (!string.IsNullOrWhiteSpace(strConfigFile))
            {
                strLogMessage = strLog;
                strLogPathName = strConfigFile;
            }
            if (!File.Exists(strLogPathName))
            {
                swLog = new StreamWriter(strLogPathName);
            }
            else
            {
                swLog = File.AppendText(strLogPathName);
            }

            swLog.WriteLine(strLogMessage);
            swLog.Close();

        }
        private static byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            try
            {
                for (int i = 0; i < returnBytes.Length; i++)
                    returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            catch (Exception ex)
            {
                return null;
            }
            return returnBytes;
        }

        private void BtnSendOneFiles_Click(object sender, EventArgs e)
        {
            string strPath = "";
            if (!string.IsNullOrEmpty(txtPath.Text.ToString().Trim()) && !File.Exists(txtPath.Text.ToString().Trim()))
            {

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = false;//该值确定是否可以选择多个文件
                dialog.Title = "请选择文件夹";
                dialog.Filter = "所有文件(*.*)|*.*";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    strPath = dialog.FileName;
                }
                else
                {
                    LogHelper.WriteLog("打开路径失败");
                    return;
                }
                string fullName = strPath;

                string strFileData = ReadFromLogFileUsingPwd(fullName);
                string[] strLine = strFileData.Split('\n');
                //CounterTMT.AppEvents.Instance.OnUpdateScreenRun("File data length = " + strLine.Length.ToString());
                int iLineTermialHeart = 0;
                int iLineCounterNormal = 0;
                int iLineError = 0;
                string strLineTrans;
                for (int iLineCount = 0; iLineCount < strLine.Length; iLineCount++)
                {
                    if (client != null && client.IsConnected && strLine[iLineCount].Length > 0)
                    {
                        byte[] BSend = strToHexByte(strLine[iLineCount]);
                        var arr = new List<byte> { };

                        for (int iSend = 0; iSend < BSend.Length; iSend++)
                        {
                            arr.Add(BSend[iSend]);
                        }
                        client.Send(arr.ToArray());
                        LogHelper.WriteLog("发送数据：" + DataHelper.ByteToHex(arr.ToArray(), arr.Count));
                    }
                }
            }
            else
            {
                string fullName = strPath;

                string strFileData = ReadFromLogFileUsingPwd(fullName);
                string[] strLine = strFileData.Split('\n');
                //CounterTMT.AppEvents.Instance.OnUpdateScreenRun("File data length = " + strLine.Length.ToString());
                int iLineTermialHeart = 0;
                int iLineCounterNormal = 0;
                int iLineError = 0;
                string strLineTrans;
                for (int iLineCount = 0; iLineCount < strLine.Length; iLineCount++)
                {
                    if (client != null && client.IsConnected && strLine[iLineCount].Length > 0)
                    {
                        byte[] BSend = strToHexByte(strLine[iLineCount]);
                        var arr = new List<byte> { };

                        for (int iSend = 0; iSend < BSend.Length; iSend++)
                        {
                            arr.Add(BSend[iSend]);
                        }
                        client.Send(arr.ToArray());
                        LogHelper.WriteLog("发送数据：" + DataHelper.ByteToHex(arr.ToArray(), arr.Count));
                    }
                }
            }
        }

        private void BtnSendPathAllFiles_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPath.Text.ToString().Trim().Contains(@"\"))
                {
                    FileInfo[] files = GetFiles(txtPath.Text.ToString().Trim(), "*.txt");
                    string strFiles = string.Empty;
                    foreach (var f in files)
                    {
                        string fullName = f.FullName;
                        strFiles += (fullName + "\r\n");

                        string strFileData = ReadFromLogFileUsingPwd(fullName);
                        string[] strLine = strFileData.Split('\n');
                        //CounterTMT.AppEvents.Instance.OnUpdateScreenRun("File data length = " + strLine.Length.ToString());
                        int iLineTermialHeart = 0;
                        int iLineCounterNormal = 0;
                        int iLineError = 0;
                        string strLineTrans;
                        for (int iLineCount = 0; iLineCount < strLine.Length; iLineCount++)
                        {
                            if (client != null && client.IsConnected && strLine[iLineCount].Length > 0)
                            {
                                byte[] BSend = strToHexByte(strLine[iLineCount]);
                                var arr = new List<byte> { };

                                for (int iSend = 0; iSend < BSend.Length; iSend++)
                                {
                                    arr.Add(BSend[iSend]);
                                }
                                client.Send(arr.ToArray());
                                LogHelper.WriteLog("发送数据：" + DataHelper.ByteToHex(arr.ToArray(), arr.Count));
                            }
                        }


                    }


                }
                else
                {

                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Multiselect = false;//该值确定是否可以选择多个文件
                    dialog.Title = "请选择文件夹";
                    dialog.Filter = "所有文件(*.*)|*.*";
                    string strPath = "";
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        strPath = dialog.FileName;
                    }
                    else
                    {
                        LogHelper.WriteLog("打开路径失败");
                        return;
                    }
                    FileInfo[] files = GetFiles(strPath.ToString().Trim(), "*.txt");
                    string strFiles = string.Empty;
                    foreach (var f in files)
                    {
                        string fullName = f.FullName;
                        strFiles += (fullName + "\r\n");

                        string strFileData = ReadFromLogFileUsingPwd(fullName);
                        string[] strLine = strFileData.Split('\n');
                        //CounterTMT.AppEvents.Instance.OnUpdateScreenRun("File data length = " + strLine.Length.ToString());
                        int iLineTermialHeart = 0;
                        int iLineCounterNormal = 0;
                        int iLineError = 0;
                        string strLineTrans;
                        for (int iLineCount = 0; iLineCount < strLine.Length; iLineCount++)
                        {
                            if (client != null && client.IsConnected && strLine[iLineCount].Length > 0)
                            {
                                byte[] BSend = strToHexByte(strLine[iLineCount]);
                                var arr = new List<byte> { };

                                for (int iSend = 0; iSend < BSend.Length; iSend++)
                                {
                                    arr.Add(BSend[iSend]);
                                }
                                client.Send(arr.ToArray());
                                LogHelper.WriteLog("发送数据：" + DataHelper.ByteToHex(arr.ToArray(), arr.Count));
                            }
                        }


                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void BtnUDPPath_Click(object sender, EventArgs e)
        {




            Socket udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //while (true)

            //发送数据


            try
            {
                EndPoint serverPoint = new IPEndPoint(IPAddress.Parse(txtUDPIP.Text.ToString().Trim()), int.Parse(txtUDPPort.Text.ToString().Trim()));
                if (txtUDPPath.Text.ToString().Trim().Contains(@"\"))
                {
                    FileInfo[] files = GetFiles(txtUDPPath.Text.ToString().Trim(), "*.txt");
                    string strFiles = string.Empty;
                    foreach (var f in files)
                    {
                        string fullName = f.FullName;
                        strFiles += (fullName + "\r\n");

                        string strFileData = ReadFromLogFileUsingPwd(fullName);
                        string[] strLine = strFileData.Split('\n');
                        //CounterTMT.AppEvents.Instance.OnUpdateScreenRun("File data length = " + strLine.Length.ToString());
                        int iLineTermialHeart = 0;
                        int iLineCounterNormal = 0;
                        int iLineError = 0;
                        string strLineTrans;
                        for (int iLineCount = 0; iLineCount < strLine.Length; iLineCount++)
                        {
                            if (udpClient != null && strLine[iLineCount].Length > 0)
                            {
                                byte[] BSend = strToHexByte(strLine[iLineCount]);
                                var arr = new List<byte> { };

                                byte[] data = Encoding.UTF8.GetBytes(strLine[iLineCount]);
                                for (int iSend = 0; iSend < BSend.Length; iSend++)
                                {
                                    arr.Add(BSend[iSend]);
                                }
                                udpClient.SendTo(arr.ToArray(), serverPoint);

                                LogHelper.WriteLog("发送数据：" + DataHelper.ByteToHex(arr.ToArray(), arr.Count));
                            }
                        }


                    }


                }
                else
                {

                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Multiselect = false;//该值确定是否可以选择多个文件
                    dialog.Title = "请选择文件夹";
                    dialog.Filter = "所有文件(*.*)|*.*";
                    string strPath = "";
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        strPath = dialog.FileName;
                    }
                    else
                    {
                        LogHelper.WriteLog("打开路径失败");
                        return;
                    }
                    FileInfo[] files = GetFiles(strPath.ToString().Trim(), "*.txt");
                    string strFiles = string.Empty;
                    foreach (var f in files)
                    {
                        string fullName = f.FullName;
                        strFiles += (fullName + "\r\n");

                        string strFileData = ReadFromLogFileUsingPwd(fullName);
                        string[] strLine = strFileData.Split('\n');
                        //CounterTMT.AppEvents.Instance.OnUpdateScreenRun("File data length = " + strLine.Length.ToString());
                        int iLineTermialHeart = 0;
                        int iLineCounterNormal = 0;
                        int iLineError = 0;
                        string strLineTrans;
                        for (int iLineCount = 0; iLineCount < strLine.Length; iLineCount++)
                        {
                            if (udpClient != null && strLine[iLineCount].Length > 0)
                            {
                                byte[] BSend = strToHexByte(strLine[iLineCount]);
                                var arr = new List<byte> { };

                                byte[] data = Encoding.UTF8.GetBytes(strLine[iLineCount]);
                                for (int iSend = 0; iSend < BSend.Length; iSend++)
                                {
                                    arr.Add(BSend[iSend]);
                                }
                                udpClient.SendTo(arr.ToArray(), serverPoint);

                                LogHelper.WriteLog("发送数据：" + DataHelper.ByteToHex(arr.ToArray(), arr.Count));
                            }
                        }


                    }
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                udpClient.Close();
            }

        }

        private void BtnUDPFile_Click(object sender, EventArgs e)
        {

            Socket udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //while (true)

            //发送数据


            try
            {
                EndPoint serverPoint = new IPEndPoint(IPAddress.Parse(txtUDPIP.Text.ToString().Trim()), int.Parse(txtUDPPort.Text.ToString().Trim()));
                string strPath = "";
                if (!string.IsNullOrEmpty(txtUDPPath.Text.ToString().Trim()) && !File.Exists(txtUDPPath.Text.ToString().Trim()))
                {

                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Multiselect = false;//该值确定是否可以选择多个文件
                    dialog.Title = "请选择文件夹";
                    dialog.Filter = "所有文件(*.*)|*.*";
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        strPath = dialog.FileName;
                    }
                    else
                    {
                        LogHelper.WriteLog("打开路径失败");
                        return;
                    }
                    string fullName = strPath;

                    string strFileData = ReadFromLogFileUsingPwd(fullName);
                    string[] strLine = strFileData.Split('\n');
                    //CounterTMT.AppEvents.Instance.OnUpdateScreenRun("File data length = " + strLine.Length.ToString());
                    int iLineTermialHeart = 0;
                    int iLineCounterNormal = 0;
                    int iLineError = 0;
                    string strLineTrans;
                    for (int iLineCount = 0; iLineCount < strLine.Length; iLineCount++)
                    {
                        if (udpClient != null && strLine[iLineCount].Length > 0)
                        {
                            byte[] BSend = strToHexByte(strLine[iLineCount]);
                            var arr = new List<byte> { };

                            byte[] data = Encoding.UTF8.GetBytes(strLine[iLineCount]);
                            for (int iSend = 0; iSend < BSend.Length; iSend++)
                            {
                                arr.Add(BSend[iSend]);
                            }
                            udpClient.SendTo(arr.ToArray(), serverPoint);

                            LogHelper.WriteLog("发送数据：" + DataHelper.ByteToHex(arr.ToArray(), arr.Count));
                        }
                    }
                }
                else
                {
                    string fullName = strPath;

                    string strFileData = ReadFromLogFileUsingPwd(fullName);
                    string[] strLine = strFileData.Split('\n');
                    //CounterTMT.AppEvents.Instance.OnUpdateScreenRun("File data length = " + strLine.Length.ToString());
                    int iLineTermialHeart = 0;
                    int iLineCounterNormal = 0;
                    int iLineError = 0;
                    string strLineTrans;
                    for (int iLineCount = 0; iLineCount < strLine.Length; iLineCount++)
                    {
                        if (udpClient != null && strLine[iLineCount].Length > 0)
                        {
                            byte[] BSend = strToHexByte(strLine[iLineCount]);
                            var arr = new List<byte> { };

                            byte[] data = Encoding.UTF8.GetBytes(strLine[iLineCount]);
                            for (int iSend = 0; iSend < BSend.Length; iSend++)
                            {
                                arr.Add(BSend[iSend]);
                            }
                            udpClient.SendTo(arr.ToArray(), serverPoint);

                            LogHelper.WriteLog("发送数据：" + DataHelper.ByteToHex(arr.ToArray(), arr.Count));
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                udpClient.Close();
            }
        }

        private void BtnUDPTxt_Click(object sender, EventArgs e)
        {

            Socket udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //while (true)

            //发送数据


            try
            {
                EndPoint serverPoint = new IPEndPoint(IPAddress.Parse(txtUDPIP.Text.ToString().Trim()), int.Parse(txtUDPPort.Text.ToString().Trim()));
                if (udpClient != null && btnUDPMsg.Text.ToString().Trim().Length > 0)
                {
                    byte[] BSend = strToHexByte(btnUDPMsg.Text.ToString().Trim());
                    var arr = new List<byte> { };

                    byte[] data = Encoding.UTF8.GetBytes(btnUDPMsg.Text.ToString().Trim());
                    for (int iSend = 0; iSend < BSend.Length; iSend++)
                    {
                        arr.Add(BSend[iSend]);
                    }
                    udpClient.SendTo(arr.ToArray(), serverPoint);

                    LogHelper.WriteLog("发送数据：" + DataHelper.ByteToHex(arr.ToArray(), arr.Count));
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                udpClient.Close();
            }
        }

        private void BtnUDPConnect_Click(object sender, EventArgs e)
        {

        }

        private void BtnSendTCP_Click(object sender, EventArgs e)
        {
            iSendTcp = 0;
            TimerTcp.Enabled = !TimerTcp.Enabled;
            if (TimerTcp.Enabled)
            {
                btnSendTCP.Text = "TCP开始发送";
            }
            else
            {
                btnSendTCP.Text = "TCP发送初始化";
            }
        }
        int iSendTcp = 0;
        int iSendUdp = 0;
        private void TimerTcp_Tick(object sender, EventArgs e)
        {
            if (iSendTcp++ < 100)
                if (client != null && client.IsConnected && txtMsg.Text.Length > 0)
                {
                    var arr = new List<byte> { 0, 3 };
                    var txt = System.Text.Encoding.UTF8.GetBytes(txtMsg.Text);
                    arr.Add((byte)(txt.Length / 256));
                    arr.Add((byte)txt.Length);
                    arr.AddRange(txt);
                    client.Send(arr.ToArray());

                    LogHelper.WriteLog("发送数据：" + DataHelper.ByteToHex(arr.ToArray(), arr.Count));
                }
                else
                {
                    iSendTcp = 0;
                    TimerTcp.Enabled = false;
                }
        }

        private void BtnSendUdp_Click(object sender, EventArgs e)
        {
            iSendUdp = 0;
            TimerUdp.Enabled = !TimerUdp.Enabled;
            if (TimerUdp.Enabled)
            {
                btnSendUdp.Text = "UDP开始发送";
            }
            else
            {
                btnSendUdp.Text = "UDP发送初始化";
            }
        }

        private void TimerUdp_Tick(object sender, EventArgs e)
        {
            if (iSendUdp++ < 100)
            {
                Socket udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                //while (true)

                //发送数据


                try
                {
                    EndPoint serverPoint = new IPEndPoint(IPAddress.Parse(txtUDPIP.Text.ToString().Trim()), int.Parse(txtUDPPort.Text.ToString().Trim()));
                    if (udpClient != null && btnUDPMsg.Text.ToString().Trim().Length > 0)
                    {
                        byte[] BSend = strToHexByte(btnUDPMsg.Text.ToString().Trim());
                        var arr = new List<byte> { };

                        byte[] data = Encoding.UTF8.GetBytes(btnUDPMsg.Text.ToString().Trim());
                        for (int iSend = 0; iSend < BSend.Length; iSend++)
                        {
                            arr.Add(BSend[iSend]);
                        }
                        udpClient.SendTo(arr.ToArray(), serverPoint);

                        LogHelper.WriteLog("发送数据：" + DataHelper.ByteToHex(arr.ToArray(), arr.Count));
                    }

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    udpClient.Close();
                }
            }
            else
            {
                iSendUdp = 0;
                TimerUdp.Enabled = false;
            }
        }









        private static List<int> GetPidByPort(Process p, int port)
        {
            int result;
            bool b = true;
            p.Start();
            p.StandardInput.WriteLine(string.Format("netstat -ano|findstr \"{0}\"", port));
            p.StandardInput.WriteLine("exit");
            StreamReader reader = p.StandardOutput;
            List<int> list_pid = new List<int>();
            try
            {


                string strLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    strLine = strLine.Trim();
                    if (strLine.Length > 0 && ((strLine.Contains("TCP") || strLine.Contains("UDP"))))
                    {
                        Regex r = new Regex(@"\s+");
                        string[] strArr = r.Split(strLine);
                        if (strArr.Length == 4)
                        {
                            b = int.TryParse(strArr[3], out result);
                            if (b && !list_pid.Contains(result))
                                list_pid.Add(result);
                        }
                        else if (strArr.Length == 5)
                        {
                            b = int.TryParse(strArr[4], out result);
                            if (b && !list_pid.Contains(result))
                                list_pid.Add(result);
                        }
                    }
                    strLine = reader.ReadLine();
                }


            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                p.WaitForExit();
                reader.Close();
                p.Close();
            }
            return list_pid;
        }
        private static List<string> GetProcessNameByPid(Process p, List<int> list_pid)
        {
            p.Start();
            List<string> list_process = new List<string>();

            foreach (var pid in list_pid)
            {
                p.StandardInput.WriteLine(string.Format("tasklist |find \"{0}\"", pid));
                p.StandardInput.WriteLine("exit");
                StreamReader reader = p.StandardOutput;//截取输出流
                string strLine = reader.ReadLine();//每次读取一行

                while (!reader.EndOfStream)
                {
                    strLine = strLine.Trim();
                    if (strLine.Length > 0 && ((strLine.Contains(".exe"))))
                    {
                        Regex r = new Regex(@"\s+");
                        string[] strArr = r.Split(strLine);
                        if (strArr.Length > 0)
                        {
                            list_process.Add(strArr[0]);
                        }
                    }
                    strLine = reader.ReadLine();
                }
                p.WaitForExit();
                reader.Close();
            }
            p.Close();

            return list_process;
        }
        private void PidKill(Process p, List<int> list_pid)
        {
            p.Start();
            foreach (var item in list_pid)
            {
                p.StandardInput.WriteLine("taskkill /pid " + item + " /f");
                p.StandardInput.WriteLine("exit");
                // showConfigData("taskkill /pid " + item + " /f", richTextBox1);

                //CounterTMT.AppEvents.Instance.OnUpdateScreenRun("taskkill /pid " + item + " /f");
                LogHelper.WriteLog("taskkill /pid " + item + " /f");
            }
            p.Close();
        }
        private void t_btn_kill_Click()
        {
            int port;
            bool b = int.TryParse(txtClearPort.Text.ToString().Trim(), out port);
            if (!b)
            {
                MessageBox.Show("请输入正确的监听端口");
                return;
            }
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            List<int> list_pid = null;
            try
            {
                list_pid = GetPidByPort(p, port);
            }
            catch
            {
                //   showConfigData(" GetPidByPort Expection ", richTextBox1);
                // CounterTMT.AppEvents.Instance.OnUpdateScreenRun(" GetPidByPort Expection ");
                LogHelper.WriteLog(" GetPidByPort Expection ");
            }
            try
            {
                if (list_pid.Count == 0)
                {
                    //MessageBox.Show(VarGlobal.DevicePort + " 无端口占用，操作完成");
                    // showConfigData(VarGlobal.DevicePort + " 无端口占用，操作完成", richTextBox1);
                    //CounterTMT.AppEvents.Instance.OnUpdateScreenRun(" 无端口占用，操作完成 ");
                    LogHelper.WriteLog(" 无端口占用，操作完成 ");
                    return;
                }
                List<string> list_process = GetProcessNameByPid(p, list_pid);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("占用" + port + "端口的进程有:");
                foreach (var item in list_process)
                {
                    sb.Append(item + "\r\n");
                }
                sb.AppendLine("是否要结束这些进程？");

                //if (MessageBox.Show(sb.ToString(), "系统提示", MessageBoxButtons.YesNo) == DialogResult.No)
                //   return;
                PidKill(p, list_pid);
                //showConfigData(VarGlobal.DevicePort + " 已关闭 ", richTextBox1);
                //CounterTMT.AppEvents.Instance.OnUpdateScreenRun(VarGlobal.DevicePort + " 已关闭 ");
                LogHelper.WriteLog(port + " 已关闭 ");
            }
            catch (Exception ex)
            {
                //showConfigData(VarGlobal.DevicePort + " 关闭遇到障碍 " + ex.ToString(), richTextBox1);
                //CounterTMT.AppEvents.Instance.OnUpdateScreenRun(VarGlobal.DevicePort + " 关闭遇到障碍 " + ex.ToString());
                LogHelper.WriteLog(" 关闭遇到障碍 " + ex.ToString());
            }

        }

        private void BtnClearPort_Click(object sender, EventArgs e)
        {

        }

        private void BtnSendValue_Click(object sender, EventArgs e)
        {


            if (client != null && client.IsConnected && txtMsg.Text.Length > 0)
            {

                double data = Convert.ToDouble(txtDouble.Text.ToString().Trim());

                byte[] senddata = ValueToPack(data, txtNickName.Text.ToString().Trim(), txtIDType.Text.ToString().Trim());

                client.Send(senddata);

                LogHelper.WriteLog("发送value数据：" + DataHelper.ByteToHex(senddata.ToArray(), senddata.Length));
            }
        }
        private byte[] ValueToPack(double value, string DeviceID, string DeviceType)
        {
            string strvalue = value.ToString("f2");
            byte[] dataSend = System.Text.Encoding.UTF8.GetBytes("#" + strvalue.PadLeft(8, '0')); //arr.ToArray();
            byte[] dataSendDeviceIDNick = System.Text.Encoding.UTF8.GetBytes("#" + DeviceID.ToString().Trim().PadLeft(10, 'I'));
            byte[] dataSendDeviceType = System.Text.Encoding.UTF8.GetBytes("#" + DeviceType.ToString().Trim().PadLeft(6, 'T'));
            int dtsendLen = dataSend.Length + dataSendDeviceIDNick.Length + dataSendDeviceType.Length + 1 +  /* check */ 4 /* head 5a 5a len */ ;
            var arr = new List<byte> { 0x5A, 0x5A, 0x05 };
            //Head 5A  5A
            //Type  1 int  2 string  3 bool 4 json  5 Double
            //
            //

            if (dtsendLen > 255)
            {
                dtsendLen = dtsendLen & 0xFF;
            }
            arr.Add((byte)dtsendLen);
            arr.AddRange(dataSend);
            arr.AddRange(dataSendDeviceIDNick);
            arr.AddRange(dataSendDeviceType);

            int CheckSum = 0;
            for (int iCnt = 0; iCnt < dataSend.Length; iCnt++)
            {
                CheckSum += (dataSend[iCnt]);
            }
            for (int iCnt = 0; iCnt < dataSendDeviceIDNick.Length; iCnt++)
            {
                CheckSum += (dataSendDeviceIDNick[iCnt]);
            }
            for (int iCnt = 0; iCnt < dataSendDeviceType.Length; iCnt++)
            {
                CheckSum += (dataSendDeviceType[iCnt]);
            }
            CheckSum = CheckSum & 0xFF;
            arr.Add((byte)CheckSum);
            return arr.ToArray();
        }
        private byte[] HeartToPack(string DeviceID, string DeviceType)
        {
            string strvalue = "$$";
            byte[] dataSend = System.Text.Encoding.UTF8.GetBytes("#" + strvalue); //arr.ToArray();
            byte[] dataSendDeviceIDNick = System.Text.Encoding.UTF8.GetBytes("#" + DeviceID.ToString().Trim().PadLeft(10, 'I'));
            byte[] dataSendDeviceType = System.Text.Encoding.UTF8.GetBytes("#" + DeviceType.ToString().Trim().PadLeft(6, 'T'));
            int dtsendLen = dataSend.Length + dataSendDeviceIDNick.Length + dataSendDeviceType.Length + 1 +  /* check */ 4 /* head 5a 5a len */ ;
            var arr = new List<byte> { 0x55, 0x55, 0x40 };
            //Head  0x55 0x55
            //Type   0 Double  1 int  2 string  3 bool 4 json    
            //Type   40 Heart
            //

            if (dtsendLen > 255)
            {
                dtsendLen = dtsendLen & 0xFF;
            }
            arr.Add((byte)dtsendLen);
            arr.AddRange(dataSend);
            arr.AddRange(dataSendDeviceIDNick);
            arr.AddRange(dataSendDeviceType);

            int CheckSum = 0;
            for (int iCnt = 0; iCnt < dataSend.Length; iCnt++)
            {
                CheckSum += (dataSend[iCnt]);
            }
            for (int iCnt = 0; iCnt < dataSendDeviceIDNick.Length; iCnt++)
            {
                CheckSum += (dataSendDeviceIDNick[iCnt]);
            }
            for (int iCnt = 0; iCnt < dataSendDeviceType.Length; iCnt++)
            {
                CheckSum += (dataSendDeviceType[iCnt]);
            }
            CheckSum = CheckSum & 0xFF;
            arr.Add((byte)CheckSum);
            return arr.ToArray();
        }
    }

}
