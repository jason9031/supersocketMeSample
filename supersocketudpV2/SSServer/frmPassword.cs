using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace IMESAgent.GUI
{
    public partial class frmPassword : Form
    {
        //private const string Password = "123";

        public frmPassword()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            btnCancel.Click += btnCancel_Click;
            btnOk.Click += btnOk_Click;
            this.FormClosed += frmPassword_FormClosed;
        }

        private void frmPassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
        }
        static string Reverse(string original)
        {
            char[] arr = original.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            string strPwd = string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now).Substring(2, 7) + "1" + "PH";

            string  sBuffer =  Reverse(strPwd);

            strPwd = sBuffer ;
            WriteLogFileName(txtPassword.Text);
            if (!string.IsNullOrEmpty(txtPassword.Text) && 
                (strPwd.Equals(txtPassword.Text)))
            {
                this.DialogResult = DialogResult.OK;
                WriteLogFileName("exit ok");
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                WriteLogFileName("exit Cancel");
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmPassword_Load(object sender, EventArgs e)
        {
            WriteLogFileName("exit in");
            IPAddress[] ips = Dns.GetHostAddresses(""); //当参数为""时返回本机所有IP
            string netInfo = "通过Dns.GetHostAddresses(\"\")获取本机所有IP信息:\r\n";
            for (int i = 0; i < ips.Length; i++)
            {
                netInfo += string.Format("{0}) [ip:]{1}，  [ip类型:]{2}\r\n", i.ToString(), ips[i].ToString(), ips[i].AddressFamily);
            }
            WriteLogFileName(netInfo);
            if (netInfo.Contains("130."))
            {
                string strPwd = string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now).Substring(2, 7) + "1" + "PH";

                string sBuffer = Reverse(strPwd);

                strPwd = sBuffer;
                this.Text = strPwd;
            }
        }
        static object lockerMemoLog = new object();
        public static string WriteLogFileName(string strLog, string strFileName ="exitlog")
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
