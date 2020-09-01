using SuperSocket.SocketBase.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receive.udp.Command
{
    public class CMD_0003_UpText : CommandBase<UdpSession , MyUdpRequestInfo>
    {
        private int Action = 3;
        public override string Name
        {
            get { return Action.ToString(); }
        }
        /// <summary>
        /// 上行
        /// </summary>
        public override void ExecuteCommand(UdpSession  session, MyUdpRequestInfo requestInfo)
        {
            LogHelper.WriteLog(session.NickName + " " + requestInfo.Body.ToString());
            Push(session, "已收到文本");
        }

        /// <summary>
        ///  下行(推送)
        /// </summary>
        public void Push(UdpSession  session, string text) 
        {
            var response = BitConverter.GetBytes((ushort)Action).Reverse().ToList();
            var arr = System.Text.Encoding.UTF8.GetBytes(text);
            response.AddRange(BitConverter.GetBytes((ushort)arr.Length).Reverse().ToArray());
            response.AddRange(arr);

            session.Send(response.ToArray(), 0, response.Count);
        }
        
    }
}
