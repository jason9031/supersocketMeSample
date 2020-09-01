using SSServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receive.udp
{
    public class LogHelper
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger("Performance");
        //  public static readonly log4net.ILog log = log4net.LogManager.GetLogger("Performance");
        public static readonly string PATH = System.Windows.Forms.Application.StartupPath + @"\Log" + "\\log";
        public delegate void LogEvent(string msg);
        public static List<string> allLines = new List<string>();
        public static int displayLength = 0;
        private static LogEvent OnLog { get; set; }

        /// <summary>
        /// winform使用后台线程写显示Log
        /// </summary>
        public static void SetOnLog(LogEvent e)
        {
            OnLog = e;
            //每100毫秒重新渲染到界面
            var timer = new System.Timers.Timer(100);
            timer.Elapsed += new System.Timers.ElapsedEventHandler((s, x) => 
            {
                try
                {
                    int count = allLines.Count;
                    if (displayLength == count)
                        return;
                    //最多保留Log行数
                    if (allLines.Count > 5000) 
                        allLines.RemoveRange(0, 20);

                    displayLength = allLines.Count;
                    OnLog(string.Join("\r\n", allLines));
                }
                catch { }
            });
            timer.Enabled = true;
            timer.Start();
        }
        /// <summary>
        /// 普通的文件记录日志
        /// </summary>
        /// <param name="info"></param>
        public static void WriteLog(string info)
        {
            if (OnLog != null) 
            {
                allLines.Add(string.Join(" ", DateTime.Now.ToString("HH:mm:ss"), info));
                WriteLogFileName(info);
            }
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="se"></param>
        public static void WriteLog(string info, Exception se)
        {
            if (OnLog != null)
            {
                allLines.Add(info);
                AppLog.Error(info);
            }
        }
        /// <summary>
        /// =======================================Log 日志写入=========================================
        /// </summary>
        /// <param name="strComType">接收参数</param>
        /// <returns name="DoData">返会处理参数的方法</returns>
        static object lockerMemoLog = new object();
        public static string WriteLogFileName(string strLog, string strFileName = "Debug")
        {
            string strPath = System.Windows.Forms.Application.StartupPath + @"\Log\";
            string strLogPathName = strPath + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now).Substring(0, 10) + strFileName + ".log";
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
