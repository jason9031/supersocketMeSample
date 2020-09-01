using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSocketV1
{
    public class JSocketACommand: CommandBase<JSocketServerSession, StringRequestInfo>
    {

        public override void ExecuteCommand(JSocketServerSession session, StringRequestInfo requestInfo)
        {
            session.CustomID = new Random().Next(10000, 99999);
            session.CustomName = "hello word";
            Console.WriteLine(session.CustomID);
            Console.WriteLine(session.CustomName);

            var key = requestInfo.Key;
            var param = requestInfo.Parameters;
            var body = requestInfo.Body;
            Console.WriteLine(key);
            Console.WriteLine(param);
            Console.WriteLine(body);

            session.Send(session.CustomID.ToString() + session.Config.Ip);
        }

    }
}
