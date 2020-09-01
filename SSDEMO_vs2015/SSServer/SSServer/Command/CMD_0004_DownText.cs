using SuperSocket.SocketBase.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSServer.Command
{
    public class CMD_0004_DownText : CommandBase<MySession, MyRequestInfo>
    {
        private int Action = 4;
        public override string Name
        {
            get { return Action.ToString(); }
        }
        /// <summary>
        /// 上行
        /// </summary>
        public override void ExecuteCommand(MySession session, MyRequestInfo requestInfo)
        {

            LogHelper.WriteLog(session.NickName + " 已确认客户端收到文本");
        }

        /// <summary>
        ///  下行(推送)
        /// </summary>
        public void Push(MySession session, string text)
        {
            LogHelper.WriteLog("服务器发送文本：" + text);


            var response = BitConverter.GetBytes((ushort)Action).Reverse().ToList();
            var arr = System.Text.Encoding.UTF8.GetBytes(text);
            response.AddRange(BitConverter.GetBytes((ushort)arr.Length).Reverse().ToArray());
            response.AddRange(arr);

            session.Send(response.ToArray(), 0, response.Count);
        }
        
    }
}
