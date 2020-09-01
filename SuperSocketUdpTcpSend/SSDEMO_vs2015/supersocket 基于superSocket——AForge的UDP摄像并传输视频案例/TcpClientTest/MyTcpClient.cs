using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClientTest
{
   
    class MyTcpClient
    {
        private System.Net.Sockets.TcpClient tcpClient;
        public MyTcpClient(string ip, int port)
        {

            tcpClient = new System.Net.Sockets.TcpClient(ip, port);
            byte[] recData = new byte[1024];
            Action a = new Action(() =>
            {
                while (true)
                {
                    tcpClient.Client.Receive(recData);
                    var msg = System.Text.Encoding.UTF8.GetString(recData);
                    Console.WriteLine(msg);
                }
            });
            a.BeginInvoke(null, null);

        }

        public void Send(string message)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(message);
            tcpClient.Client.Send(data);
        }
        public void Send(byte[] message)
        {
            tcpClient.Client.Send(message);
        }
    }
}
