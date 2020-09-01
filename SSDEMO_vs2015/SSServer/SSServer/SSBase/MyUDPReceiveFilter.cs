using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receive.udp
{

    public class MyUDPReceiveFilter : SuperSocket.SocketBase.Protocol.IReceiveFilter<MyUdpRequestInfo>
    {
        public int LeftBufferSize
        {
            get { return 0; }
        }

        public SuperSocket.SocketBase.Protocol.IReceiveFilter<MyUdpRequestInfo> NextReceiveFilter
        {
            get { return this; }
        }

        public SuperSocket.SocketBase.Protocol.FilterState State
        {
            get; private set;
        }

        /// <summary>
        /// 过滤器接受信息 解析byte[]
        /// </summary>
        /// <param name="readBuffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="toBeCopied"></param>
        /// <param name="rest"></param>
        /// <returns></returns>
        public MyUdpRequestInfo Filter(byte[] readBuffer, int offset, int length, bool toBeCopied, out int rest)
        {
            string result = string.Empty;
            try
            {
                string msg = Encoding.Default.GetString(readBuffer); //读出的数据为16进制

                for (int iCountRecv = 0; iCountRecv < readBuffer.Length; iCountRecv++)
                {
                    string hexOutput = System.Convert.ToString(readBuffer[iCountRecv], 16);
                    int sLen = Encoding.Default.GetByteCount(hexOutput);
                    if (sLen == 1)
                        hexOutput = '0' + hexOutput;
                    result += hexOutput; //Convert.ToString(byte, 16)把byte转化成十六进制string 
                }
                result = result.ToUpper();

            }
            catch
            {
                rest = 0;
                return null;
            }
            if (result.Length == 10 || result.Length == 46)
            {
                try
                {
                    rest = 0;
                    var body = readBuffer.Skip(offset).Take(length).ToArray();
                    string privateKey = string.Empty;
                    string sesssionId = string.Empty; //machineCode 当成sessionId 

                    WrapReaderData termianlData = new WrapReaderData();
                    if (result.Length == 10)
                    {
                        termianlData.TranType = 10;
                        string str10 = result;// HandleUdpUtils.Ascii2Str(body);
                        privateKey = TermianlID_10(str10);
                        termianlData.ReaderID = privateKey;
                        sesssionId = "TERMINAL_H_ID=" + privateKey;
                        termianlData.sesssionId = sesssionId;
                    }
                    else if (result.Length == 46)
                    {
                        string str46 = result;// HandleUdpUtils.Ascii2Str(body);
                        termianlData = TermianlID_46(str46);
                        termianlData.TranType = 46;
                        if (termianlData.TranResult != 0)
                        {
                            rest = 0;
                            return null;
                        }
                        privateKey = termianlData.ReaderID;
                        sesssionId = "TERMINAL_ID=" + privateKey;
                        termianlData.sesssionId = sesssionId;
                    }
                    else
                    {
                        string strOther = result;// HandleUdpUtils.Ascii2Str(body);
                        termianlData.TranType = 99;
                        if (termianlData.TranResult != 0)
                        {
                            rest = 0;
                            return null;
                        }
                        privateKey = "Error99";
                        sesssionId = "TERMINAL_ID=" + privateKey;
                        termianlData.sesssionId = sesssionId;
                        termianlData.allParams = strOther;
                    }
                    return new MyUdpRequestInfo(privateKey, sesssionId, termianlData) { TerminalCounterData = termianlData };
                }
                catch (Exception ex)
                {
                    rest = 0;
                    return null;
                }
            }
            else
            {
                rest = 0;
                return null;
            }
        }
        #region 10 46 类型数据转换
        public string TermianlID_10(string strRecvData)
        {
            string strReaderCode = strRecvData.Substring(2, 8);
            strReaderCode = "0000000000" + Convert.ToString(Convert.ToInt64(strReaderCode, 16));
            string ReaderID = "T" + strReaderCode.Substring(strReaderCode.Length - 9, 9);
            return ReaderID;
        }
        public WrapReaderData TermianlID_46(string strRecvData)
        {

            WrapReaderData readData = new WrapReaderData();
            readData.TranResult = 0;
            //先判断包头和包尾
            //第1位至2位
            string strCode = strRecvData.Substring(0, 2);
            if (!strCode.Equals("02"))
            {
                readData.TranResult = -1;
                return readData;
            }
            //第29位至30位,结束符
            if (strRecvData.Length == 46)
            {
                strCode = strRecvData.Substring(44, 2);
            }
            if (!strCode.Equals("03"))
            {
                readData.TranResult = -1;
                return readData;
            }
            //第3位至8位,阅读器ID


            //第3位至4位
            strCode = strRecvData.Substring(2, 2);   //阅读器ID头,默认为 54，不用转10进制，直接转为 'T'
            if (strCode.Equals("54"))               //转为'T'
            {
                strCode = "T";
                readData.ReaderID = strCode;
            }
            else
            {
                readData.ReaderID = "X";
            }
            strCode = strRecvData.Substring(4, 8);

            strCode = "0000000000" + Convert.ToString(Convert.ToInt64(strCode, 16));

            readData.ReaderID = readData.ReaderID + strCode.Substring(strCode.Length - 9, 9);

            //第13位至14位
            strCode = strRecvData.Substring(12, 2);   //模智宝ID头,默认为 43，不用转10进制，直接转为 'C'
            if (strCode.Equals("43"))                //转为'C
            {
                strCode = "C";
                readData.PID = strCode;
            }
            else
            {
                readData.PID = "Y";
            }

            strCode = strRecvData.Substring(14, 10);

            strCode = "000000000000" + Convert.ToString(Convert.ToInt64(strCode, 16));

            readData.PID = readData.PID + strCode.Substring(strCode.Length - 11, 11);
            if ("C00000000000".Equals(readData.PID))
            {
                readData.PID = "C20011600001";
            }
            //第9位至14位,模智宝ID
            //wrapReaderData.PID = strRecvData.Substring(8, 6);
            //wrapReaderData.PID = Convert.ToString(Convert.ToInt32(wrapReaderData.PID, 16));
            //第15位至16位 模智宝拆除标志
            if (strRecvData.Substring(24, 2).Equals("00"))
            {
                readData.RemoveTimes = 0;//正常状态
            }
            else
            {
                readData.RemoveTimes = 1;//除拆状态
            }
            //第17位至18位 模智宝电量标志
            if (strRecvData.Substring(26, 2).Equals("00"))
            {
                readData.BatteryAlarm = "H"; //"0";//电池有电
            }
            else
            {
                readData.BatteryAlarm = "E";// "1";//只有10%以下的电量
                                            //模智宝状态
            }
            if (strRecvData.Substring(28, 2).Equals("00"))
            {
                readData.Status = 0;//工作
            }
            else
            {
                readData.Status = 1;//休眠
            }
            //第31位至38位
            strCode = strRecvData.Substring(30, 8);//模次
            strCode = Convert.ToString(Convert.ToInt32(strCode, 16));
            int matchtimes;
            int.TryParse(strCode, out matchtimes);
            readData.MatchTimes = matchtimes;


            //第39位至40位
            strCode = strRecvData.Substring(38, 2);          //成型周期     Close Open
            strCode = Convert.ToString(Convert.ToInt32(strCode, 16));
            int closeTime;
            int.TryParse(strCode, out closeTime);
            string thisCloseTime = closeTime.ToString();
            readData.MatchRemain_s = closeTime;


            //第41位至42位
            strCode = strRecvData.Substring(40, 2);          //平均成型周期  Open Time
            strCode = Convert.ToString(Convert.ToInt32(strCode, 16));
            int OpenTime;
            int.TryParse(strCode, out OpenTime);
            string ThisOpenTime = OpenTime.ToString();
            readData.OpenRemain_s = OpenTime;

            //第43位至44位
            strCode = strRecvData.Substring(42, 2);          //上传失败次数
            strCode = Convert.ToString(Convert.ToInt32(strCode, 16));
            int lostTime;
            int.TryParse(strCode, out lostTime);
            string LostTime = lostTime.ToString();
            readData.LostCnt = lostTime;


            string nowTimeStr = DateTime.Now.ToString("yyyy,MM,dd HH:mm:ss", new System.Globalization.DateTimeFormatInfo());
            string strbuf = ("成功读取时间:" + nowTimeStr);
            readData.allMsg = (String.Format("{0}; Terminal-{1} Counter-{2} SC-{3} REMOVE-{4} Vot-{5}  State-{6} Time-{7} 2MatchTime {8}  OpenTime {9} LostTime {10}" + strRecvData + "\r\n",
                                strRecvData, readData.ReaderID, readData.PID, readData.MatchTimes, readData.RemoveTimes,
                                readData.BatteryAlarm, readData.Status, nowTimeStr, thisCloseTime, ThisOpenTime, LostTime));

            readData.allParams = string.Empty;
          string   strOneIDAllParam = string.Empty;
            strOneIDAllParam += "at=" + "CDATA";//-----1  数据类型 ---
            strOneIDAllParam += "&sn=" + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now).Substring(3, 10);//-----1 通信序列号---
            strOneIDAllParam += "&ti=" + readData.ReaderID;//-----1 reader id----
            strOneIDAllParam += "&tv=" + "1.0";//-----2 终端版本---
            strOneIDAllParam += "&rc=" + "2";//-----1 路由器数 0---
            strOneIDAllParam += "&bs=" + readData.BatteryAlarm;//-----1 电池状态-----
            strOneIDAllParam += "&ci=" + readData.PID;//---1 计数器id--------
            strOneIDAllParam += "&lst=" + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now).Substring(0, 14);//-1 最终shot 时间---
            strOneIDAllParam += "&rt=" + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now).Substring(0, 14);//---1 通信接收时间--生成此命令时间
            strOneIDAllParam += "&ct=" + readData.MatchRemain_s;//-----1 周期时间 -----
            strOneIDAllParam += "&cf=" + "1";//-----1 通信类型 长期  手动---
            strOneIDAllParam += "&sc=" +(readData.MatchTimes.ToString());//-----1 最终shot 数--------
            strOneIDAllParam += "&cs=" + readData.Status;//-----1 计数器状态
            strOneIDAllParam += "&rm=" + readData.RemoveTimes;//-----1 移除状态

            strOneIDAllParam += ("&et=0," + readData.OpenRemain_s.ToString() + "," + readData.OpenRemain_s.ToString());
            readData.allParams = strOneIDAllParam;
            return readData;
        } 
        #endregion
        public void Reset()
        {

        }
    }
}
