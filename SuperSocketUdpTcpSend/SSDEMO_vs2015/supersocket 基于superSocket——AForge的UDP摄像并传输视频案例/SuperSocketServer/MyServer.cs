using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketServer
{
    public class MyServer : AppServer<MySession, BinaryRequestInfo>
    {
        public MyServer()
            : base(new DefaultReceiveFilterFactory<MyReceiveFilter, BinaryRequestInfo>()) //使用默认的接受过滤器工厂 (DefaultReceiveFilterFactory)
        {
        }
    }

    public class MySession : AppSession<MySession, BinaryRequestInfo>
    {
    }
}
