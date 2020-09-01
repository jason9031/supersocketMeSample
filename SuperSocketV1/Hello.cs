using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSocketV1
{
    public class Hello : CommandBase<JSocketServerSession, StringRequestInfo>
    {
        /// <summary>  
        /// 自定义执行命令方法，注意传入的变量session类型为MySession  
        /// </summary>  
        /// <param name="session">会话</param>  
        /// <param name="requestInfo">请求数据信息</param>  
        public override void ExecuteCommand(JSocketServerSession session, StringRequestInfo requestInfo)
        {
            session.Send(string.Format("Hello {0}:{1}   {2}", session.Config.Ip, session.Config.Port, requestInfo.Body));
        }
    }
}
