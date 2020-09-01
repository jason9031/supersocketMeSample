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
    public class UdpSession  : AppSession<UdpSession , MyUdpRequestInfo>
    {
            
        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }

        protected override void OnSessionStarted()
        {
            Console.WriteLine("start Session");
            base.OnSessionStarted();
        }

        protected override void HandleUnknownRequest(MyUdpRequestInfo requestInfo)
        {
            Console.WriteLine("unKnownRequest");
            LogHelper.WriteLog("Unknown Command!");
            base.HandleUnknownRequest(requestInfo);
        }

        public UdpSession ()
        {

        }
        public string NickName = "未登录";
        //public string NickName = "未登录";

        //protected override void OnSessionStarted()
        //{

        //}

        protected override void OnInit()
        {
            base.OnInit();
        }

        //protected override void HandleUnknownRequest(MyUdpRequestInfo requestInfo)
        //{
        //    LogHelper.WriteLog("Unknown Command!");
        //}
        
        protected override void HandleException(Exception e)
        {
            LogHelper.WriteLog("Handle Exception!");
        }

        //protected override void OnSessionClosed(CloseReason reason)
        //{
        //    base.OnSessionClosed(reason);
        //}
    }
}
