using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Receive.udp
{
    public class UdpSession : AppSession<UdpSession, MyUdpRequestInfo>
    {
        public UdpSession()
        {
        }
        public string NickName = "未登录";
        protected override void OnInit()
        {
            base.OnInit();
        }
        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }

        //public virtual void Initialize(IAppServer<TAppSession, TRequestInfo> appServer, ISocketSession socketSession);

        public override void Initialize(IAppServer<UdpSession, MyUdpRequestInfo> appServer, ISocketSession socketSession)
        {
            NickName = (appServer.SessionCount + 1).ToString() + "=" + socketSession.SessionID;
             //   + "=" + socketSession.AppSession.SocketSession.LocalEndPoint.Address??"init " + "=" + socketSession.AppSession.SocketSession.LocalEndPoint.Port ?? "init ";
            LogHelper.WriteLog(NickName);
            base.Initialize(appServer, socketSession);
        }
        protected override void OnSessionStarted()
        {
            Console.WriteLine("start Session");
            base.OnSessionStarted();
        }
        protected override void HandleUnknownRequest(MyUdpRequestInfo requestInfo)
        {
            Console.WriteLine("unKnownRequest");
            LogHelper.WriteLog("Unknown Command! HandleUnknownRequest ");
            base.HandleUnknownRequest(requestInfo);
        }
        protected override void HandleException(Exception e)
        {
            LogHelper.WriteLog("Handle Exception! HandleException");
        }
    }
}
