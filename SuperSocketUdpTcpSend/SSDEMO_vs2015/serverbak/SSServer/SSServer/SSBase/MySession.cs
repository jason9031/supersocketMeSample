using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SSServer
{
    public class MySession : AppSession<MySession, MyRequestInfo>
    {
        public MySession()
        {

        }

        public string NickName = "未登录";
        
        protected override void OnSessionStarted()
        {

        }

        protected override void OnInit()
        {
            base.OnInit();
        }

        protected override void HandleUnknownRequest(MyRequestInfo requestInfo)
        {
            LogHelper.WriteLog("Unknown Command!");
        }
        
        protected override void HandleException(Exception e)
        {
            LogHelper.WriteLog("Handle Exception!");
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }
    }
}
