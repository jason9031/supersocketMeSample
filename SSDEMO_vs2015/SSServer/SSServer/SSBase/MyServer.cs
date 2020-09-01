using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSServer
{
    public class MyServer : AppServer<MySession, MyRequestInfo>
    {
        /// <summary>
        /// 通过配置文件安装服务从这里启动
        /// </summary>
        public MyServer()
            : base(new DefaultReceiveFilterFactory<MyReceiveFilter, MyRequestInfo>())
        {
            this.NewSessionConnected += MyServer_NewSessionConnected;
            this.SessionClosed += MyServer_SessionClosed;
        }
        /// <summary>
        /// winform启动，不使用这里的事件
        /// </summary>
        public MyServer(SessionHandler<MySession> NewSessionConnected, SessionHandler<MySession, CloseReason> SessionClosed)
            : base(new DefaultReceiveFilterFactory<MyReceiveFilter, MyRequestInfo>())
        {
            this.NewSessionConnected += NewSessionConnected;
            this.SessionClosed += SessionClosed;
        }

        protected override void OnStarted()
        {
            //启动成功
            LogHelper.WriteLog(string.Format("Socket启动成功：{0}:{1}", this.Config.Ip, this.Config.Port));
        }

        void MyServer_NewSessionConnected(MySession session)
        {
            //连接成功
        }

        void MyServer_SessionClosed(MySession session, CloseReason value)
        {

        }
    }
}
