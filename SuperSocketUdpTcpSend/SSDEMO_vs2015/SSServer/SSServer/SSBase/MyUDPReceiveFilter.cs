using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receive.udp
{

    public class MyUDPReceiveFilter : SuperSocket.SocketBase.Protocol.IReceiveFilter<MyUdpRequestInfo>
    {
        public int LeftBufferSize
        {
            get { return 0; }
        }

        public SuperSocket.SocketBase.Protocol.IReceiveFilter<MyUdpRequestInfo> NextReceiveFilter
        {
            get { return this; }
        }

        public SuperSocket.SocketBase.Protocol.FilterState State
        {
            get; private set;
        }
        /// <summary>
        /// 过滤器接受信息 解析byte[]
        /// </summary>
        /// <param name="readBuffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="toBeCopied"></param>
        /// <param name="rest"></param>
        /// <returns></returns>
        public MyUdpRequestInfo Filter(byte[] readBuffer, int offset, int length, bool toBeCopied, out int rest)
        {
            var endFlag = HandleUdpUtils.getEndFlagStr(readBuffer, length);
            if (!endFlag.Equals(HandleUdpUtils.IMG_END_FLAG) || !endFlag.Equals(HandleUdpUtils.WEB_END_FLAG)) //判断结束标志
            {
                int endFlagSize = 2;
                string privateKey = HandleUdpUtils.getPrivateKey(readBuffer);
                string sesssionId = HandleUdpUtils.getMachineCode(readBuffer); //machineCode 当成sessionId
                rest = 0;
                byte[] body = readBuffer.Skip(
                    HandleUdpUtils.MACHINE_CODE_LENGTH).Take(length - HandleUdpUtils.MACHINE_CODE_LENGTH - endFlagSize).ToArray();
                return new MyUdpRequestInfo(privateKey, sesssionId) { Body = body };
            }
            else
            {
                rest = 0;
                return null;
            }
        }

        public void Reset()
        {

        }
    }
}
