using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSocketV1
{
    public class JSocketServerApp : AppServer<JSocketServerSession>
    {

        /// <summary>  
        /// 自定义服务器类MyServer，继承AppServer，并传入自定义连接类MySession  
        /// </summary>  
        //public class MyServer : AppServer<JSocketServerSession>
        //{
        //    protected override void OnStartup()
        //    {
        //        base.OnStartup();
        //        // Console.WriteLine("服务器启动");
        //    }


        //}
        /// <summary>  
        /// 输出新连接信息  
        /// </summary>  
        /// <param name="session"></param>  
        protected override void OnNewSessionConnected(JSocketServerSession session)
        {
            base.OnNewSessionConnected(session);
            //输出客户端IP地址  
            Console.Write("\r\n" + session.LocalEndPoint.Address.ToString() + ":连接");
        }

        /// <summary>  
        /// 输出断开连接信息  
        /// </summary>  
        /// <param name="session"></param>  
        /// <param name="reason"></param>  
        protected override void OnSessionClosed(JSocketServerSession session, CloseReason reason)
        {
            base.OnSessionClosed(session, reason);
            Console.Write("\r\n" + session.LocalEndPoint.Address.ToString() + ":断开连接");
        }

        protected override void OnStopped()
        {
            base.OnStopped();
            Console.WriteLine("服务已停止");
        }
    }
}
