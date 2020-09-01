using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketServer
{
    public class MyReceiveFilter : FixedHeaderReceiveFilter<BinaryRequestInfo>
    {

        public MyReceiveFilter()
            : base(10)//消息头部长度
        {        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="header">*byte[] header * 缓存的数据，这个并不是单纯只包含协议头的数据,有时候tcp协议长度为409600,很多</param>
        /// <param name="offset">头部数据从 缓存的数据 中开始的索引,一般为0.(tcp协议有可能从405504之类的一个很大数据开始)</param>
        /// <param name="length">这个length和base(10)中的参数相等</param>
        /// <returns></returns>
        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            return GetBodyLengthFromHeader(header, offset, length, 6, 4);//6表示第几个字节开始表示长度.4:由于是int来表示长度,int占用4个字节
        }
        protected override BinaryRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {

            byte[] body = new byte[length];
            Array.Copy(bodyBuffer, offset, body, 0, length);

            Int16 type = BitConverter.ToInt16(header.ToArray(), 4);

            BinaryRequestInfo r = new BinaryRequestInfo(type.ToString(), body);
            return r;


            //byte[] h = header.ToArray();
            //byte[] full = new byte[h.Count()+length];
            //Array.Copy(h, full, h.Count());
            //Array.Copy(body, 0, full, h.Count(), body.Length );
            //BinaryRequestInfo r = new BinaryRequestInfo("",full);
            //return r;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header">需要解析的数据</param>
        /// <param name="offset">头部数据从header中开始的索引,一般为0,也可能不是0</param>
        /// <param name="length">这个length和base(10)中的参数相等</param>
        /// <param name="lenStartIndex">表示长度的字节从第几个开始</param>
        /// <param name="lenBytesCount">几个字节来表示长度:4个字节=int,2个字节=int16,1个字节=byte</param>
        /// <returns></returns>
        private int GetBodyLengthFromHeader(byte[] header, int offset, int length, int lenStartIndex, int lenBytesCount)
        {
            var headerData = new byte[lenBytesCount];
            Array.Copy(header, offset + lenStartIndex, headerData, 0, lenBytesCount);//
            if (lenBytesCount == 1)
            {
                int i = headerData[0];
                return i;
            }
            else if (lenBytesCount == 2)
            {
                int i = BitConverter.ToInt16(headerData, 0);
                return i;
            }
            else //  if (lenBytesCount == 4)
            {
                int i = BitConverter.ToInt32(headerData, 0);
                return i;
            }
        }
    }
}
