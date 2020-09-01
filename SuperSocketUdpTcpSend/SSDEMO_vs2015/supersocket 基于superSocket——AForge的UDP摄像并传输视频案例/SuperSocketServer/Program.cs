using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            MyServer appServer = new MyServer();
            var se = new SuperSocket.SocketBase.Config.ServerConfig();
            se.TextEncoding = "Unicode";// System.Text.Encoding.
            se.TextEncoding = "gbk";// System.Text.Encoding.
            se.Ip = "127.0.0.1";
            se.Port = 2020;
            se.Mode = SocketMode.Tcp;

            System.Threading.Thread thSend = new System.Threading.Thread(SendMsgToClient);
            thSend.Start();
            //Setup the appServer
            if (!appServer.Setup(se)) //Setup with listening port
            {
                Console.WriteLine("Failed to setup!");
                Console.ReadKey();
                return;
            }
            Console.WriteLine();
            //Try to start the appServer
            if (!appServer.Start())
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }


            appServer.NewSessionConnected += appServer_NewSessionConnected;
            appServer.SessionClosed += appServer_SessionClosed;
            appServer.NewRequestReceived += new RequestHandler<MySession, SuperSocket.SocketBase.Protocol.BinaryRequestInfo>(appServer_NewRequestReceived);


            //// appServer.NewRequestReceived += appServer_NewRequestReceived;
            //Console.WriteLine("The server started successfully, press key 'q' to stop it!");

            //while (Console.ReadKey().KeyChar != 'q')
            //{
            //    Console.WriteLine();
            //    continue;
            //}

            ////Stop the appServer
            //appServer.Stop();
            //Console.WriteLine("The server was stopped!");
            Console.ReadKey();
        }

        static List<MySession> listSession = new List<MySession>();
        static void appServer_NewSessionConnected(MySession session)
        {
            Console.WriteLine("Session:" + session.RemoteEndPoint + " connect");
            listSession.Add(session);

        }

        static void appServer_NewRequestReceived(MySession session, SuperSocket.SocketBase.Protocol.BinaryRequestInfo requestInfo)
        {
            string key = requestInfo.Key;
            switch (key)
            {
                case "1":
                    Console.WriteLine("Get message from " + session.RemoteEndPoint.ToString() + ":" + System.Text.Encoding.UTF8.GetString(requestInfo.Body));
                    break;
                case "2":
                    Console.WriteLine("Get image");
                    break;
                default:
                    Console.WriteLine("Get unknown message.");
                    break;
            }
        }
        static void appServer_SessionClosed(MySession session, CloseReason value)
        {
            Console.WriteLine("Session:" + session.RemoteEndPoint + " SessionClosed");
            var temp = listSession.Find(f => f.SessionID == session.SessionID);
            if (temp != null)
            {
                listSession.Remove(temp);
            }

        }
        static void SendMsgToClient()
        {

            while (true)
            {
                if (listSession.Count > 0)
                {
                    var input = Console.ReadLine();
                    if (input.Length == 0)
                    {
                        continue;
                    }
                    var data = System.Text.Encoding.UTF8.GetBytes(input);
                    for (int i = 0; i < listSession.Count; i++)
                    {
                        listSession[i].Send(data, 0, data.Length);
                    }
                }
                else
                {
                    Console.WriteLine("No client.");
                    System.Threading.Thread.Sleep(5000);
                }
            }

        }
    }
}
