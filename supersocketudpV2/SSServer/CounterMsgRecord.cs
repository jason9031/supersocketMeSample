using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Specialized;
using com.hp.rtsm.util;
using Newtonsoft.Json;
using System.Globalization;

namespace Receive.udp
{
    public   class DoData
    {
        public static string Post(string url, string postStr, string inteStr = "", Encoding encode = null)
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
        public static string Get(string url, string query, Encoding encode = null)
        {
            string result;
            string timeStamp = string.Empty;
            string nonce = string.Empty;
            url = url.Replace("+", "%2B");
            try
            {
                var webClient = new WebClient { Encoding = Encoding.UTF8 };
                if (encode != null)
                    webClient.Encoding = encode;
                webClient.Proxy = null;
                webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                webClient.Headers.Add(HttpRequestHeader.Accept, "application/json");
                ServicePointManager.ServerCertificateValidationCallback +=
                                delegate (
                                   object sender,
                                   X509Certificate certificate,
                                   X509Chain chain,
                                   SslPolicyErrors sslPolicyErrors)
                                {
                                    return true;
                                };
                result = webClient.DownloadString(url);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public static T PostNew<T>(string url, string data, string staffId)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            string timeStamp = GetTimeStamp();
            string nonce = GetRandom();
            //加入头信息
            request.Headers.Add("staffid", staffId.ToString()); //当前请求用户StaffId
            request.Headers.Add("timestamp", timeStamp); //发起请求时的时间戳（单位：毫秒）
            request.Headers.Add("nonce", nonce); //发起请求时的时间戳（单位：毫秒）
            request.Headers.Add("signature", GetSignature(timeStamp, nonce, staffId, data)); //当前请求内容的数字签名
            //写数据
            request.Method = "POST";
            request.ContentLength = bytes.Length;
            request.ContentType = "application/json";
            Stream reqstream = request.GetRequestStream();
            reqstream.Write(bytes, 0, bytes.Length);
            //读数据
            request.Timeout = 300000;
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(streamReceive, Encoding.UTF8);
            string strResult = streamReader.ReadToEnd();
            //关闭流
            reqstream.Close();
            streamReader.Close();
            streamReceive.Close();
            request.Abort();
            response.Close();
            return JsonConvert.DeserializeObject<T>(strResult);
        }
        /// <summary>
        /// 计算签名
        /// </summary>
        /// <returns></returns>
        private static string GetSignature(string timeStamp, string nonce, string staffId, string data)
        {
            var hash = System.Security.Cryptography.MD5.Create();
            //拼接签名数据
            var signStr = timeStamp + nonce + staffId + VarGlobal.SignToken.ToString() + data;
            //将字符串中字符按升序排序
            var sortStr = string.Concat(signStr.OrderBy(c => c));
            var bytes = Encoding.UTF8.GetBytes(sortStr);
            //使用MD5加密
            var md5Val = hash.ComputeHash(bytes);
            //把二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            foreach (var c in md5Val)
            {
                result.Append(c.ToString("X2"));
            }
            return result.ToString().ToUpper();
        }
        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        private static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
        /// <summary>  
        /// 获取随机数
        /// </summary>  
        /// <returns></returns>  
        private static string GetRandom()
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int i = rd.Next(0, int.MaxValue);
            return i.ToString();
        }
        /// <summary>
        /// 拼接get参数
        /// </summary>
        /// <param name="parames"></param>
        /// <returns></returns>
        public static Tuple<string, string> GetQueryString(Dictionary<string, string> parames)
        {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parames);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();
            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder("");  //签名字符串
            StringBuilder queryStr = new StringBuilder(""); //url参数
            if (parames == null || parames.Count == 0)
                return new Tuple<string, string>("", "");
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key))
                {
                    query.Append(key).Append(value);
                    queryStr.Append("&").Append(key).Append("=").Append(value);
                }
            }
            return new Tuple<string, string>(query.ToString(), queryStr.ToString().Substring(1, queryStr.Length - 1));
        }
        public static void GetAsync(string url, DownloadStringCompletedEventHandler downLoadCompleted = null, Encoding encode = null)
        {
            var webClient = new WebClient { Encoding = Encoding.UTF8 };
            webClient.Proxy = null;
            if (encode != null)
                webClient.Encoding = encode;
            if (downLoadCompleted != null)
                webClient.DownloadStringCompleted += downLoadCompleted;
            webClient.DownloadStringAsync(new Uri(url));
        }
        public static void PostAsync(string url, string postStr = "", UploadDataCompletedEventHandler uploadDataCompleted = null, Encoding encode = null)
        {
            var webClient = new WebClient { Encoding = Encoding.UTF8 };
            webClient.Proxy = null;
            if (encode != null)
                webClient.Encoding = encode;
            var sendData = Encoding.GetEncoding("GB2312").GetBytes(postStr);
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            webClient.Headers.Add("ContentLength", sendData.Length.ToString(CultureInfo.InvariantCulture));
            if (uploadDataCompleted != null)
                webClient.UploadDataCompleted += uploadDataCompleted;
            webClient.UploadDataAsync(new Uri(url), "POST", sendData);
        }

    }
    public class WrapReaderData
    {
        public string allMsg;
        public string allParams;
        public string ReaderID;
        public string CounterID;
        public string PID;
        public int RemoveTimes;
        public Int64 MatchTimes;
        public Int64 curMatchTimes;
        public string BatteryAlarm;
        public int Status;
        public int MatchRemain_s;
        public int OpenRemain_s;
        public int LostCnt;
        public int TranResult;
        public int TranType;
        public string sesssionId;
    }
    public class CounterMsgRecord
    {
        /// <summary>
        /// 阅读器
        /// </summary>
        public string ReaderID = "";
        /// <summary>
        /// Counter ID Old
        /// </summary>
        public string CounterIDOld = "";
        /// <summary>
        /// Counter ID New
        /// </summary>
        public string CounterIDMap = "";
        /// <summary>
        /// Counter Reauest Api when ervery times 
        /// </summary>
        public int CounterSendLimit = 0;
        /// <summary>
        /// Send Seq (SN)
        /// </summary>
        public int SnSeq = 0;
        /// <summary>
        /// Send Date Seq (Date + SN)
        /// </summary>
        public string SnDate = "";
        /// <summary>
        /// shot Cnt Success last request
        /// </summary>
        public long LastSendShotCnt = 0;
        /// <summary>
        /// Now Shot Count
        /// </summary>
        public long ShotCnt = 0;
        /// <summary>
        /// every time list 
        /// </summary>
        public string SendEveryTime = "";
        /// <summary>
        /// every time Sum 
        /// </summary>
        public long SumEveryTime = 0;
        /// <summary>
        /// Last Time Send 
        /// </summary>
        public string LastSendTime = "";
        /// <summary>
        /// Last Time Send 
        /// </summary>
        public DateTime SendTime;
        /// <summary>
        /// Last Time Send 
        /// </summary>
        public string SendReason = "init";
        /// <summary>
        /// Last Time Recv 
        /// </summary>
        public DateTime RecvTime;
        /// <summary>
        /// Battery Flag 
        /// </summary>
        public string BatteryAlarm = "";
        /// <summary>
        /// State Flag 
        /// </summary>
        public string StateFlag = "Work";
        /// <summary>
        /// Remove Flag 
        /// </summary>
        public string RemoveFlag = "On";
        /// <summary>
        /// 未收到数据 定时任务发送次数
        /// </summary>
        public int iCntNoData = 0;
        /// <summary>
        /// 未收到数据 标志
        /// </summary>
        public string stCntNoDataFlag = "0";
        /// <summary>
        /// CT 时间
        /// </summary>
        public string strThisCT = "5";
        /// <summary>
        /// ET 时间
        /// </summary>
        public string strETList = "";
        /// <summary>
        /// ET Sum
        /// </summary>
        public int iETSum = 0;
        /// <summary>
        /// init data from reader 
        /// </summary>
        public string strInitData = "Helloworld";
        /// <summary>
        /// org string
        /// </summary>
        public string strOrg;
        /// <summary>
        /// org string
        /// </summary>
        public string SubMatchTimeBetweenTwo;
        /// <summary>
        /// org string
        /// </summary>
        public string OpenTime;
        /// <summary>
        /// org string
        /// </summary>
        public string LostTime;

        //nowTimeStr, CycleTime, AvgCycleTime, LostTime

        public string DataType = "CDATA";

        public string m_cmbat_Text = "CDATA";
        public string m_cmbdn_Text = string.Empty;
        public string m_cmbsn_Text = string.Empty;
        public string m_cmbgw_Text = string.Empty;
        public string m_cmbip_Text = string.Empty;
        public string m_cmbti_Text = string.Empty;
        public string m_cmbdh_Text = string.Empty;
        public string m_cmbtv_Text = string.Empty;
        public string m_cmbrc_Text = string.Empty;
        public string m_cmbbs_Text = string.Empty;
        public string m_cmbci_Text = string.Empty;
        public string m_cmblst_Text = string.Empty;
        public string m_cmbrt_Text = string.Empty;
        public string m_cmbct_Text = string.Empty;
        public string m_cmbcf_Text = string.Empty;
        public string m_cmbsc_Text = string.Empty;
        public string m_cmbCS_Text = string.Empty;
        public string m_cmbRM_Text = string.Empty;
        public string m_cmbEtCount_Text = string.Empty;
        public string m_cmbID1_Text = string.Empty;
        public string m_cmbID2_Text = string.Empty;


        public bool m_cmbdh_Enabled = false;
        public bool m_cmbip_Enabled = false;
        public bool m_cmbgw_Enabled = false;
        public bool m_cmbdn_Enabled = false;
        public bool m_cmblst_Enabled = true;
        public bool m_cmbrt_Enabled = true;
        public bool m_cmbcf_Enabled = true;
        public bool m_cmbct_Enabled = true;
        public bool m_cmbbs_Enabled = true;
        public bool m_cmbrc_Enabled = true;
        public bool m_cmbsn_Enabled = true;
        public bool m_cmbEtCount_Enabled = true;
        public bool m_cmbat_Enabled = true;
        public bool m_cmbci_Enabled = true;
        public bool m_cmbsc_Enabled = true;
        public bool m_cmbti_Enabled = true;
        public bool m_cmbtv_Enabled = true;
        public bool m_cmbRM_Enabled = true;
        public bool m_cmbCS_Enabled = true;

        //cmbat.Text = "CDATA";
        //m_cmbat_Text = "CDATA";
        //m_cmbdh_Enabled = false; m_cmbip_Enabled = false; m_cmbgw_Enabled = false; m_cmbdn_Enabled = false;

        //m_cmbti_Enabled = true; m_cmbci_Enabled = true; m_cmbsc_Enabled = true; m_cmblst_Enabled = true;
        //m_cmbrt_Enabled = true; m_cmbcf_Enabled = true; m_cmbct_Enabled = true; m_cmbbs_Enabled = true;
        //m_cmbrc_Enabled = true; m_cmbtv_Enabled = true; m_cmbsn_Enabled = true; m_cmbEtCount_Enabled = true;
        //m_cmbat_Enabled = true; m_cmbRM_Enabled = true; m_cmbCS_Enabled = true;



    }



}
