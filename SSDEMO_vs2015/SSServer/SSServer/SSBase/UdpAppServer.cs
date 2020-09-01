using com.hp.rtsm.util;
using SSServer;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Receive.udp
{
    class UdpAppServer : AppServer<UdpSession, MyUdpRequestInfo>
    {
      public static readonly log4net.ILog log = log4net.LogManager.GetLogger("Performance");
        //  public static readonly log4net.ILog log = log4net.LogManager.GetLogger("Performance");
        public static readonly string PATH = System.Windows.Forms.Application.StartupPath + @"\Log" + "\\UdpSession";
        public static Dictionary<string, string> PRIVATE_KEY = new Dictionary<string, string>(); //deviceCode->key
        public static readonly int MIN_IMG_SIZE = 100;
        /// <summary>
        /// 通过配置文件安装服务从这里启动
        /// </summary>
        public UdpAppServer() : base(new DefaultReceiveFilterFactory<MyUDPReceiveFilter, MyUdpRequestInfo>())
        {
            this.NewSessionConnected += MyServer_NewSessionConnected;
            this.SessionClosed += MyServer_SessionClosed;
        }
        /// <summary>
        /// winform启动，不使用这里的事件
        /// </summary>
        public UdpAppServer(SessionHandler<UdpSession> NewSessionConnected, SessionHandler<UdpSession, CloseReason> SessionClosed)
            : base(new DefaultReceiveFilterFactory<MyUDPReceiveFilter, MyUdpRequestInfo>())
        {
            this.NewSessionConnected += NewSessionConnected;
            this.SessionClosed += SessionClosed;
        }
        public static ConcurrentDictionary<string, VideoPackageModel> FILE_WRITERS = new ConcurrentDictionary<string, VideoPackageModel>(); //读取文件

        protected override void OnSystemMessageReceived(string messageType, object messageData)
        {
            LogHelper.WriteLog(string.Format("Udp server Receive：{0}:{1}", messageType, messageData));
            Console.WriteLine(messageType);
            base.OnSystemMessageReceived(messageType, messageData);
        }
        void MyServer_NewSessionConnected(UdpSession session)
        {
            //连接成功
        }

        void MyServer_SessionClosed(UdpSession session, CloseReason value)
        {

        }
        protected override void OnStarted()
        {
            //启动成功
            LogHelper.WriteLog(string.Format("UDP Socket启动成功：{0}:{1}", this.Config.Ip, this.Config.Port));
        }

        /// <summary>
        /// 过滤器后 进入该方法
        /// </summary>
        /// <param name="session"></param>
        /// <param name="requestInfo"></param>
        protected override void ExecuteCommand(UdpSession session, MyUdpRequestInfo requestInfo)
        {

            if (requestInfo.Key.Contains(HandleUdpUtils.WEB_SESSION_KEY))
            {
                if (requestInfo.TerminalCounterData.TranType == 10)
                {
                    LogHelper.WriteLog(DateTime.Now.ToString() + "  "+ requestInfo.Key + ": HeartBeat success");
                    string strReaderCode = requestInfo.TerminalCounterData.ReaderID;


                    string SnDate = string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now).Substring(3, 10);

                    string strHeartbeat = string.Empty;
                    strHeartbeat += "at=TDATA";//-----1  数据类型 ---
                    strHeartbeat += ("&sn=" + (SnDate));//-----1 通信序列号---
                    strHeartbeat += ("&ti=" + (strReaderCode));//-----1 reader id----
                    strHeartbeat += ("&lst=" + (string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now).Substring(0, 14)));//-1 最终shot 时间---
                    strHeartbeat += ("&ip=" + (VarGlobal.DeviceIP));//---1 通信接收时间--生成此命令时间
                    LogHelper.WriteLog("Heart " + requestInfo.Key + "~" + requestInfo.SessionID + "~" + strHeartbeat);
                    try
                    {
                        CounterSendMsg cntMsg = new CounterSendMsg();
                        cntMsg.bMultiFlag = false;
                        cntMsg.strSendMsg = strHeartbeat;
                        startThreadSend(cntMsg);//Heart data
                    }
                    catch (Exception ex)
                    {
                        string strex = ex.ToString();
                        AppLog.Error(requestInfo.SessionID + " 10 Code " + strex);
                    }
                }
                else if (requestInfo.TerminalCounterData.TranType == 46)
                {
                    LogHelper.WriteLog("Normal " + requestInfo.Key + "~" + requestInfo.SessionID + "~" + requestInfo.TerminalCounterData.allMsg);
                    try
                    {
                        //strbuf46 = (m_strOneIDAllParam + "=============radio = 3  启动线程=== single ==========");
                        //showApiData(VarGlobal.TokenKey, richApiLog);
                        //showApiData(strbuf46, richApiLog);
                        CounterSendMsg cntMsg = new CounterSendMsg();
                        cntMsg.bMultiFlag = false;
                        cntMsg.strSendMsg = requestInfo.TerminalCounterData.allParams;
                        startThreadSend(cntMsg);//Counter Data
                    }
                    catch (Exception ex)
                    {
                        string strex = ex.ToString();
                        AppLog.Error(requestInfo.SessionID + " 46 Code " + strex);
                        //WriteLogFileName(strex, "StartThreadToSend");
                        //return -5;
                    }

                }
                else
                {
                    LogHelper.WriteLog("Error " + requestInfo.Key + "~" + requestInfo.SessionID + "~" + requestInfo.TerminalCounterData.allParams);
                }
                if (requestInfo.Key.Contains("0123456"))
                {
                    session.Send("Server receive success - " + requestInfo.Key);
                }
            }
            else
            {
                LogHelper.WriteLog(requestInfo.SessionID + "不存在该设备");
            }
        }

        public class CounterSendMsg
        {
            /// <summary>
            /// 阅读器
            /// </summary>
            public bool bMultiFlag = false;
            /// <summary>
            /// Counter ID Old
            /// </summary>
            public string strSendMsg = "";
        }
        static object lockerPost = new object();
        void startThreadSend(CounterSendMsg initMsg)
        {
            Thread threadSend = new Thread(ClientPost);//创建了线程还未开启
            threadSend.Start(initMsg);//用来给函数传递参数，开启线程
        }
        void ClientPost(object Msg)
        {
            CounterSendMsg initMsgNow = (CounterSendMsg)Msg;
            string strEncCode = CryptoModule.encrypt(initMsgNow.strSendMsg.ToString().Trim());
            string strReturnData = string.Empty;
            lock (lockerPost)
            {
                try
                {
                    initMsgNow.strSendMsg = initMsgNow.bMultiFlag ? initMsgNow.strSendMsg : "";
                    strReturnData = Post(VarGlobal.ServerUrl.ToString().Trim(), strEncCode.ToString().Trim(), initMsgNow.strSendMsg.ToString().Trim());
                    LogHelper.WriteLog("SendResult " + strReturnData .Replace("\r\n","-"));
                }
                catch (Exception ex)
                {
                    //string bufret1 = (ex.Message.ToString()
                    //                        + strReturnData + "/r/n"
                    //                        + strEncCode + "/r/n"
                    //                        + m_VtmUrl.ToString().Trim());
                    //string bufret = (ex.Message.ToString() + "/r/n"
                    //                        + strReturnData + "/r/n"
                    //                        + strEncCode + "/r/n"
                    //                        + m_VtmUrl.ToString().Trim());
                    //showApiData(bufret, richApiLog);
                }
            }
        }
        string Post(string url, string postStr, string inteStr = "", Encoding encode = null)
        {
            string result;
            string timeStamp = string.Empty;
            string nonce = string.Empty;
            try
            {
                var webClient = new WebClient { Encoding = Encoding.UTF8 };
                webClient.Proxy = null;
                if (encode != null)
                    webClient.Encoding = encode;
                if (!string.IsNullOrWhiteSpace(VarGlobal.TokenKey))
                    webClient.Headers.Add("Authorization", VarGlobal.TokenKey);
                ServicePointManager.ServerCertificateValidationCallback +=
                    delegate (
                       object sender,
                       X509Certificate certificate,
                       X509Chain chain,
                       SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };
                NameValueCollection markStatus = new NameValueCollection();
                string[] values = null;
                if (string.IsNullOrWhiteSpace(inteStr))
                {
                    markStatus.Add("q", postStr);
                }
                else
                {
                    string[] arryStrParam = inteStr.Split('#');
                    foreach (string istring in arryStrParam)
                    {
                        string strEn = CryptoModule.encrypt(istring.ToString().Trim());
                        markStatus.Add("q", strEn);
                    }
                }
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                var readData = webClient.UploadValues(url, "POST", markStatus);
                result = Encoding.UTF8.GetString(readData);
            }
            catch (Exception ee)
            {
                string buf = ee.Message;
                throw new Exception(buf.ToString());
            }
            return result;
        }
    }

}
