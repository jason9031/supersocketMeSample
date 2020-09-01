using CounterTMT;
using SSServer;
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
                    Port =int.Parse( VarGlobal.DevicePort),
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


        private void BtnUDPServer_Click(object sender, EventArgs e)
        {
            try
            {
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
