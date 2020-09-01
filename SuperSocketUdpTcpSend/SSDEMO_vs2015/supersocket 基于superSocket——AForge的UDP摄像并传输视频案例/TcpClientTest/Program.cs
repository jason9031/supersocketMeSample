using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MyTcpClient c = new MyTcpClient("127.0.0.1", 2020);
            SuperSocketMessage.SSMessage msg = new SuperSocketMessage.SSMessage();
            while (true)
            {
                string m = Console.ReadLine();
                msg.Type = 1;
                msg.Message = m;
                c.Send(msg.ToBytes());
            }
        }
    }
}
