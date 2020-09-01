using CounterTMT;
using IMESAgent.GUI;
using SSServer;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Receive.udp
{
    public partial class MainForm : Form
    {
        public const string m_strRegProject = "Vtm", m_strRegFrom = "FrmVtm";
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger("Performance");
        //  public static readonly log4net.ILog log = log4net.LogManager.GetLogger("Performance");
        //public static readonly string PATH = "F:\\Debug\\Log\\LogMain";
        public static readonly string PATH = System.Windows.Forms.Application.StartupPath + @"\Log" + "\\LogMain";
        string m_UdpIP = string.Empty;
        string m_UdpPort = string.Empty;
        string m_VtmUrl = string.Empty;
        int m_iRefeshTime = 20;
        public static string m_strMdbConnectString = string.Empty;
        public static string m_strAllIDCounterParam = string.Empty;
        public static string m_strOneIDAllParam = string.Empty;
        UdpAppServer udpApp = null;
        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeData();
            LoadToken();
            this.Text = version();
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
                    Ip = VarGlobal.PcIP,
                    Port = int.Parse(VarGlobal.DevicePort),
                    ClearIdleSession = true,
                    ClearIdleSessionInterval = 20,
                    IdleSessionTimeOut = 1800,
                    SendingQueueSize = 100,
                    ReceiveBufferSize = 1000
                };
            }
        }



        void app_SessionClosed(UdpSession session, SuperSocket.SocketBase.CloseReason value)
        {
            LogHelper.WriteLog("链接断开：" + session.SessionID);
        }

        void app_NewSessionConnected(UdpSession session)
        {
            LogHelper.WriteLog("新链接：" + session.SessionID);
        }

        internal static bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();


            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }


            return inUse;
        }
        private void t_btn_kill_Click(object sender, EventArgs e)
        {
            int port;
            bool b = int.TryParse(VarGlobal.DevicePort, out port);
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
                showConfigData(" GetPidByPort Expection ", richTextBox1);
            }
            try
            {
                if (list_pid.Count == 0)
                {
                    //MessageBox.Show(VarGlobal.DevicePort + " 无端口占用，操作完成");
                    showConfigData(VarGlobal.DevicePort + " 无端口占用，操作完成", richTextBox1);
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

                if (MessageBox.Show(sb.ToString(), "系统提示", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                PidKill(p, list_pid);
                showConfigData(VarGlobal.DevicePort + " 已关闭 ", richTextBox1);
            }
            catch (Exception ex)
            {
                showConfigData(VarGlobal.DevicePort + " 关闭遇到障碍 " + ex.ToString(), richTextBox1);
            }

        }

        private void PidKill(Process p, List<int> list_pid)
        {
            p.Start();
            foreach (var item in list_pid)
            {
                p.StandardInput.WriteLine("taskkill /pid " + item + " /f");
                p.StandardInput.WriteLine("exit");
                showConfigData("taskkill /pid " + item + " /f", richTextBox1);
            }
            p.Close();
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
        //根据端口号，查找该端口所在的进程，并结束该进程
        private void LookAndStop(int port)
        {
            Process pro = new Process();
            try
            {
                // 设置命令行、参数
                pro.StartInfo.FileName = "cmd.exe";
                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.RedirectStandardInput = true;
                pro.StartInfo.RedirectStandardOutput = true;
                pro.StartInfo.RedirectStandardError = true;
                pro.StartInfo.CreateNoWindow = true;
                // 启动CMD
                pro.Start();
                // 运行端口检查命令
                pro.StandardInput.WriteLine("netstat -ano");
                pro.StandardInput.WriteLine("exit");
                // 获取结果
                Regex reg = new Regex("\\s+", RegexOptions.Compiled);
                string line = null;
                string endStr = ":" + Convert.ToString(port);
                int iTcpUdpCount = 0;
                while ((line = pro.StandardOutput.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.StartsWith("TCP", StringComparison.OrdinalIgnoreCase))
                    {
                        showConfigData(line, richTextBox1);
                        line = reg.Replace(line, ",");
                        string[] arr = line.Split(',');
                        if (arr[1].EndsWith(endStr))
                        {
                            iTcpUdpCount++;
                            //int pid = Int32.Parse(arr[4]); //Console.WriteLine("4001端口的进程ID：{0}", arr[4]);
                            //Process pro80 = Process.GetProcessById(pid);
                            //pro80.Kill();// 处理该进程
                            break;
                        }
                    }
                    else if (line.StartsWith("UDP", StringComparison.OrdinalIgnoreCase))
                    {
                        //showConfigData(line, richTextBox1);

                        line = reg.Replace(line, ",");
                        string[] arr = line.Split(',');
                        if (arr[1].EndsWith(endStr))
                        {
                            iTcpUdpCount = iTcpUdpCount + 2;
                            //int pid = Int32.Parse(arr[3]);   //Console.WriteLine("4001端口的进程ID：{0}", arr[3]);
                            //Process pro80 = Process.GetProcessById(pid);                        // 处理该进程
                            //pro80.Kill();
                            break;
                        }
                    }
                }
                if (iTcpUdpCount > 0)
                {
                    if (MessageBox.Show("Close all Program Using this port ?", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

                    {

                        while ((line = pro.StandardOutput.ReadLine()) != null)
                        {
                            //showConfigData(line + " ======== =all ", richTextBox1);
                            line = line.Trim();
                            if (line.StartsWith("TCP", StringComparison.OrdinalIgnoreCase))
                            {
                                //showConfigData(line, richTextBox1);
                                iTcpUdpCount++;
                                line = reg.Replace(line, ",");
                                string[] arr = line.Split(',');
                                if (arr[1].EndsWith(endStr) || line.Contains(endStr))
                                {
                                    int pid = Int32.Parse(arr[4]); //Console.WriteLine("4001端口的进程ID：{0}", arr[4]);
                                    Process pro80 = Process.GetProcessById(pid);
                                    pro80.Kill();// 处理该进程
                                    showConfigData(line + " Close Success ", richTextBox1);
                                    break;
                                }
                            }
                            else if (line.StartsWith("UDP", StringComparison.OrdinalIgnoreCase))
                            {
                                //showConfigData(line, richTextBox1);
                                iTcpUdpCount++;
                                line = reg.Replace(line, ",");
                                string[] arr = line.Split(',');
                                if (arr[1].EndsWith(endStr) || line.Contains(endStr))
                                {
                                    int pid = Int32.Parse(arr[3]);   //Console.WriteLine("4001端口的进程ID：{0}", arr[3]);
                                    Process pro80 = Process.GetProcessById(pid);                        // 处理该进程
                                    pro80.Kill();
                                    showConfigData(line + " Close Success ", richTextBox1);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        showConfigData(line + " Close Cancel ", richTextBox1);
                    }
                }
            }
            catch (Exception ex)
            {
                AppLog.Error("LookAndStop = " + ex.ToString() + " Fail");
            }
            finally
            {
                pro.Close();
            }
        }
        private void BtnUDPServer_Click(object sender, EventArgs e)
        {
            try
            {
                t_btn_kill_Click(null, null);
                //if (PortInUse(int.Parse(VarGlobal.DevicePort)))
                //{
                //    AppLog.Error("Start App Port = " + VarGlobal.DevicePort.ToString() + " Fail");

                //    //LookAndStop(int.Parse(VarGlobal.DevicePort));
                //    t_btn_kill_Click(null,null);
                //    if (PortInUse(int.Parse(VarGlobal.DevicePort)))
                //    {
                //        AppLog.Error("Second Start App Port = " + VarGlobal.DevicePort.ToString() + " Fail");
                //        return;
                //    }

                //}
                //else
                //{

                //}

                SERVER_CONFIG = DefaultServerConfig;
                ROOT_CONFIG = new RootConfig();
                udpApp = new UdpAppServer(app_NewSessionConnected, app_SessionClosed);

                LogHelper.SetOnLog(new LogHelper.LogEvent((m) =>
                {
                    txtAll.Text = string.Join(" ", m, "\r\n");
                    txtAll.Select(txtAll.TextLength, 0);
                    txtAll.ScrollToCaret();
                }));

                udpApp.Setup(ROOT_CONFIG, SERVER_CONFIG);

                if (!udpApp.Start())
                {
                    LogHelper.WriteLog(string.Format("UDP  {0}:{1}启动失败，请检查权限或端口是否被占用！", SERVER_CONFIG.Ip, SERVER_CONFIG.Port), new Exception("启动失败"));
                }
                else
                {
                    LogHelper.WriteLog(string.Format("UDP  {0}:{1}启动 成功！", SERVER_CONFIG.Ip, SERVER_CONFIG.Port));
                    btnUDPServer.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                AppLog.Error("Start App" + ex.ToString());
            }

        }
        #endregion

        private void Btn_sendmsg_Click(object sender, EventArgs e)
        {
            if (udpApp != null && udpApp.State == SuperSocket.SocketBase.ServerState.Running && txt_msg.Text.Length > 0)
            {
                foreach (var item in udpApp.GetAllSessions())
                {
                    new CommandSendToClient().Push(item, txt_msg.Text);
                }
            }
        }

        private void TxtAll_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadToken()
        {
            string strStatusPath = System.Windows.Forms.Application.StartupPath + @"\Token\Token.mmt";
            try
            {
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\Token"))
                {
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\Token");
                    VarGlobal.TokenKey = "Token Loss";
                    return;
                }
                else
                {
                    if (File.Exists(strStatusPath))
                    {
                        string strStart = ReadFromLogFileUsingPwd("Token", "Token.mmt", 1);
                        if (strStart.Length > 10)
                            VarGlobal.TokenKey = strStart;
                        else
                            VarGlobal.TokenKey = "Token Loss";
                    }
                    else
                    {
                        VarGlobal.TokenKey = "Token Loss";
                    }

                    showConfigData(" Token " + VarGlobal.TokenKey.ToString() + "\r\n", richTextBox1);
                }
            }
            catch (Exception ex)
            {
                VarGlobal.TokenKey = "Token Loss" + ex.ToString();
            }
        }
        public static string ReadFromLogFileUsingPwd(string Folder, string strLogFile, int ReadNum)//  读取文件 jjjsss 目录 + 内容
        {
            string strPath = System.Windows.Forms.Application.StartupPath + @"\" + Folder + @"\";
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            string ReturnStrMsg = string.Empty;
            if (!File.Exists(strPath + strLogFile))
            {
                ReturnStrMsg = "NoFile";
            }
            else
            {
                int ReadLineCount = 0;
                /**////从指定的目录以打开或者创建的形式读取日志文件
                FileStream fs = new FileStream(strPath + strLogFile, FileMode.OpenOrCreate, FileAccess.Read);// (Server.MapPath("upedFile") + "\\logfile.txt", FileMode.OpenOrCreate, FileAccess.Read);
                /**////定义输出字符串
                StringBuilder output = new StringBuilder();
                /**////初始化该字符串的长度为0
                output.Length = 0;
                /**////为上面创建的文件流创建读取数据流
                StreamReader read = new StreamReader(fs);
                /**////设置当前流的起始位置为文件流的起始点
                read.BaseStream.Seek(0, SeekOrigin.Begin);
                /**////读取文件
                while ((read.Peek() > -1) && (ReadLineCount < ReadNum))
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
        /// <summary>
        /// 窗体初始数据加载
        /// </summary>
        private void InitializeData()
        {
            string strBgImg = Application.StartupPath + @"\Image\Login\login.png";
            if (File.Exists(strBgImg))
                this.BackgroundImage = Image.FromFile(strBgImg);

            this.BackgroundImageLayout = ImageLayout.Stretch;
            string xmlfile = Application.StartupPath + "\\Setting.xml";
            if (File.Exists(xmlfile))
            {
                XmlHelper localxml = new XmlHelper(xmlfile);
                VarGlobal.ConnKey = localxml.GetNodeValue("//KEY");
                string strPC = localxml.GetNodeValue("//PC");
                if (strPC.Contains("p"))//post
                {
                    chkTiming.Checked = true;
                }
                else
                {
                    chkTiming.Checked = false;
                }
                VarGlobal.SysType = localxml.GetNodeValue("//FLAG");
                VarGlobal.FormSetting = localxml.GetNodeValue("//FORM");
                VarGlobal.DeviceIP = localxml.GetNodeValue("//DeviceIP");
                m_UdpIP = VarGlobal.DeviceIP;
                VarGlobal.DevicePort = localxml.GetNodeValue("//DevicePort");
                m_UdpPort = VarGlobal.DevicePort;
                VarGlobal.ServerUrl = localxml.GetNodeValue("//ServerURL");
                m_VtmUrl = VarGlobal.ServerUrl;
                VarGlobal.SendLimit = localxml.GetNodeValue("//SendLimit");
                VarGlobal.SendSingleType = localxml.GetNodeValue("//SendSingleType");
                // lblSendMultiI.Text = "Send Type : " + VarGlobal.SendSingleType;
                VarGlobal.SendEverySecsWork = localxml.GetNodeValue("//SendEverySecsWork");
                VarGlobal.SendEverySecsOff = localxml.GetNodeValue("//SendEverySecsOff");
                VarGlobal.SendEveryTime = localxml.GetNodeValue("//SendEveryTime");
                if (int.TryParse(VarGlobal.SendEveryTime, out m_iRefeshTime))
                {
                    // timShowCnt.Interval = m_iRefeshTime * 1000;
                }
                else
                {
                    //  timShowCnt.Interval = 20 * 1000;
                }
                //lblTick.Text = "Update Grid Tick : " + (timShowCnt.Interval / 1000).ToString();
                VarGlobal.NoDataSecsSetOffLine = localxml.GetNodeValue("//NoDataSecsSetOffLine");
                VarGlobal.NoDataSecsSetOffLineCount = localxml.GetNodeValue("//NoDataSecsSetOffLineCount");


                showConfigData(" SendSingleType " + VarGlobal.SendSingleType + "\r\n" +
                                " grid Update Time " + m_iRefeshTime.ToString() + "\r\n" +
                                " Pc IP " + m_UdpIP.ToString() + "\r\n" +
                                " Pc Port  " + m_UdpPort.ToString() + "\r\n" +
                                " Api Url " + m_VtmUrl.ToString() + "\r\n", richTextBox1
                                );
            }
            else
            {
                throw new ArrayTypeMismatchException("Setting.xml 文件不存在或者损坏");
            }
        }
        static object lockerMemoShow = new object();
        private void showConfigData(string strShowData, RichTextBox memtxt)
        {
            WriteLogFileName(strShowData, "ConfigLog");
            if (!chkUpdateConfigLog.Checked)
            {
                return;
            }
            lock (lockerMemoShow)
            {
                try
                {
                    memtxt.Text = strShowData + "\r\n----------------------------------\r\n" + memtxt.Text;
                    if (memtxt.Text.ToString().Trim().Length > 50000)
                    {
                        memtxt.Text = memtxt.Text.Substring(0, 50000);
                    }
                }
                catch (Exception ex)
                {
                    string strLog = ex.ToString();
                }
            }
        }
        /// <summary>
        /// =======================================Log 日志写入=========================================
        /// </summary>
        /// <param name="strComType">接收参数</param>
        /// <returns name="DoData">返会处理参数的方法</returns>
        static object lockerMemoLog = new object();

        private void btnSendmsg1_Click(object sender, EventArgs e)
        {
            AppLog.Error("test1");
            AppLog.Info("test2");
            AppLog.Warn("test3");
            AppLog.Fatal("test4");

            if (udpApp != null && udpApp.State == SuperSocket.SocketBase.ServerState.Running && txt_msg.Text.Length > 0)
            {
                richTextBox1.Text = string.Empty;
                foreach (var item in udpApp.GetAllSessions())
                {
                    //new CommandSendToClient().Push(item, txt_msg.Text);
                    AppLog.Info("Session List All : " + udpApp.SessionCount.ToString().PadLeft(4, ' ') + item.SessionID + " " + item.NickName + " " + item.LocalEndPoint.Address + " " + item.LocalEndPoint.Port);
                    showConfigData(item.SessionID + " " + item.NickName + " " + item.LocalEndPoint.Address + " " + item.LocalEndPoint.Port, richTextBox1);
                }
            }


        }

        public string version()
        {
            try
            {

                string FileVersions = "";
                // Get the file version for the notepad. 
                string filePath = this.GetType().Assembly.Location;


                System.Diagnostics.FileVersionInfo file1 = System.Diagnostics.FileVersionInfo.GetVersionInfo(filePath);
                FileVersions = file1.FileVersion;
                if (FileVersions != "")
                {
                    string[] strVer = FileVersions.Split('.');
                    if (strVer.Length == 2)
                    {
                        FileVersions = strVer[0] + ".00.0000";
                    }

                }
                string versions = "TMT : " + FileVersions;
                return versions;
            }
            catch (Exception ex)
            {
                return "TMT : " + "FileVersions Exception error";
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            try
            {
                udpApp.Stop();
                btnUDPServer.Enabled = true;
            }
            catch (Exception ex)
            {
                AppLog.Error("Stop App" + ex.ToString());
            }
        }

        private void BtnbtnCmd_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            //设置要启动的应用程序
            p.StartInfo.FileName = "cmd.exe";
            //是否使用操作系统shell启动
            p.StartInfo.UseShellExecute = false;
            // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardInput = true;
            //输出信息
            p.StartInfo.RedirectStandardOutput = true;
            // 输出错误
            p.StartInfo.RedirectStandardError = true;
            //不显示程序窗口
            p.StartInfo.CreateNoWindow = true;
            //启动程序
            p.Start();
        }

        private void BtnCommadn_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            //设置要启动的应用程序
            p.StartInfo.FileName = "cmd.exe";
            //是否使用操作系统shell启动
            p.StartInfo.UseShellExecute = false;
            // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardInput = true;
            //输出信息
            p.StartInfo.RedirectStandardOutput = true;
            // 输出错误
            p.StartInfo.RedirectStandardError = true;
            //不显示程序窗口
            p.StartInfo.CreateNoWindow = false;
            //启动程序
            p.Start();
            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine("netstat -ano|findstr " + VarGlobal.DevicePort + "    &exit");//+ "&exit"

            p.StandardInput.AutoFlush = true;

            //获取输出信息
            string strOuput = p.StandardOutput.ReadToEnd();
            //等待程序执行完退出进程
            p.WaitForExit();
            p.Close();
            showConfigData(strOuput, richTextBox1);
            //Console.ReadKey();
        }

        private void BtnPing_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            //设置要启动的应用程序
            p.StartInfo.FileName = "cmd.exe";
            //是否使用操作系统shell启动
            p.StartInfo.UseShellExecute = false;
            // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardInput = true;
            //输出信息
            p.StartInfo.RedirectStandardOutput = true;
            // 输出错误
            p.StartInfo.RedirectStandardError = true;
            //不显示程序窗口
            p.StartInfo.CreateNoWindow = false;
            //启动程序
            p.Start();
            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine("ping  www.baidu.com   &exit");

            p.StandardInput.AutoFlush = true;

            //获取输出信息
            string strOuput = p.StandardOutput.ReadToEnd();
            //等待程序执行完退出进程
            p.WaitForExit();
            p.Close();
            showConfigData(strOuput, richTextBox1);
            //Console.ReadKey();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确认退出吗? 退出 Counter 就掉线了\r\n确认退出请联系惠普系统担当", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                using (var dialog = new frmPassword())
                {
                    //DialogResult result = dialog.ShowDialog();

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Dispose();
                        Application.Exit();
                    }
                    else
                    {
                        e.Cancel = true;
                        //托盘显示图标等于托盘图标对象 
                        //注意notifyIcon1是控件的名字而不是对象的名字 
                        notifyIcon1.Icon = (System.Drawing.Icon)Properties.Resources.ResourceManager.GetObject("pacs");
                        //隐藏任务栏区图标 
                        this.ShowInTaskbar = false;
                        //图标显示在托盘区 
                        notifyIcon1.Visible = true;
                        this.Hide();                      //隐藏窗体  
                    }
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();                                //窗体显示  
            this.WindowState = FormWindowState.Normal;  //窗体状态默认大小  
            this.Activate();                            //激活窗体给予焦点  
        }

        public static string WriteLogFileName(string strLog, string strFileName = m_strRegProject + m_strRegFrom)
        {
            string strPath = System.Windows.Forms.Application.StartupPath + @"\Log\";
            string strLogPathName = strPath + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now).Substring(0, 9) + strFileName + ".log";
            lock (lockerMemoLog)
            {
                try
                {
                    if (!Directory.Exists(strPath))
                    {
                        Directory.CreateDirectory(strPath);
                    }
                    string strLogMessage = string.Empty;
                    StreamWriter swLog;
                    {
                        strLogMessage = string.Format("{0}:#{1}", DateTime.Now, (strLog));
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
                    return "0";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
        }
    }
}
