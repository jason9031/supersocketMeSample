using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace TMTDataGen.Supersocket
{
    public class ClientAdmin
    {
        private EasyClient mClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ClientAdmin()
        {
            mClient = new EasyClient();

            // Initialize the client with the receive filter and request handler
            mClient.Initialize(new ReceiveFilter(), (request) =>
            {
                // handle the received request
                Console.WriteLine(request.Key);
            });


            //// 连接断开事件
            //mClient.Closed += ClientClosed;
            //// 收到服务器数据事件
            //mClient.DataReceived += ClientDataReceived;
            //// 连接到服务器事件
            //mClient.Connected += ClientConnected;
            //// 发生错误的处理
            //mClient.Error += ClientError;
        }
        void ClientError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
        }

        void ClientConnected(object sender, EventArgs e)
        {
            Console.WriteLine("连接成功");
        }

        void ClientDataReceived(object sender, DataEventArgs e)
        {
            string msg = Encoding.Default.GetString(e.Data);
            Console.WriteLine(msg);
        }

        void ClientClosed(object sender, EventArgs e)
        {
            Console.WriteLine("连接断开");
        }




        /// <summary>
        /// 连接到服务器
        /// </summary>
        /// <param name="strIP">IP地址</param>
        /// <param name="iPort">端口</param>
        /// <returns>连接成功返回真</returns>
        public bool Connect(string strIP, int iPort)
        {
            // Connect to the server
            var rst = mClient.ConnectAsync(new IPEndPoint(IPAddress.Parse(strIP), iPort));

            if (rst.Result)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 连接到服务器
        /// </summary>
        /// <param name="strIP">IP地址</param>
        /// <param name="strPort">端口</param>
        /// <returns>连接成功返回真</returns>
        public bool Connect(string strIP, string strPort)
        {
            int iPort = Convert.ToInt32(strPort);
            return Connect(strIP, iPort);
        }
        /// <summary>
        /// 是否连接
        /// </summary>
        /// <returns>true表示连接</returns>
        public bool IsConnected()
        {
            return mClient.IsConnected;
        }


        /// <summary>
        /// 向服务器发命令行协议的数据
        /// </summary>
        /// <param name="key">命令名称</param>
        /// <param name="data">数据</param>
        public void SendCommand(string key, string data)
        {
            if (mClient.IsConnected)
            {
                byte[] arrBytes = Encoding.Default.GetBytes(string.Format("{0} {1}", key, data));
                // Send data to the server
                mClient.Send(arrBytes);
            }
            else
            {
                throw new InvalidOperationException("断开连接");
            }
        }

    }
}
