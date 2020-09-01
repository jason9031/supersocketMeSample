using SuperSocket.SocketBase.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSServer.Command
{
    public class CMD_0001_Login : CommandBase<MySession, MyRequestInfo>
    {
        private int Action = 1;
        public override string Name
        {
            get { return Action.ToString(); }
        }
        /// <summary>
        /// 上行
        /// </summary>
        public override void ExecuteCommand(MySession session, MyRequestInfo requestInfo)
        {
            //当做登陆成功
            var body = requestInfo.Body;
            session.NickName = body;
            LogHelper.WriteLog(session.SessionID + "登录名" + session.NickName);


            Push(session, 1);
        }

        /// <summary>
        ///  下行(推送)
        /// </summary>
        public void Push(MySession session, byte status) 
        {
            var response = new byte[] { 0, 1, 0, 2, 79, 75 }; //OK
            session.Send(response, 0, response.Length);
        }
        
    }
}
