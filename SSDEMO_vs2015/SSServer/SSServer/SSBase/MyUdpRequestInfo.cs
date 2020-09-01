using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receive.udp
{

    public class MyUdpRequestInfo : UdpRequestInfo, IRequestInfo
    {
        public MyUdpRequestInfo(string key, string sessionID ,WrapReaderData Terminaldata)
           : base(key, sessionID)
        {
            Key = key;
            SessionID = sessionID;
        }
        public string Key { get; set; }
        public string SessionID { get; set; }
        //public MyUdpRequestInfo(byte[] header, byte[] bodyBuffer)
        //{
        //Key = ((header[0] * 256) + header[1]).ToString();
        //Data = bodyBuffer;
        //}
        public byte[] Body { get; set; }
        /// <summary>
        /// 协议号对应自定义命令Name，会触摸自定义命令
        /// </summary>
        //public string Key { get; set; }
        /// <summary>
        /// 正文字节码
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// 正文文本
        /// </summary>
        /// 
        //public string Body
        //{
        //    get
        //    {
        //        return Encoding.UTF8.GetString(Data);
        //    }
        //}

        public WrapReaderData TerminalCounterData { get; set; }
    }

}
