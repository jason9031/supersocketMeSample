using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketMessage
{
   public  class SSMessage
    {
        public byte[] Start;//4个字节
        public ushort Type;//2个字节, 1表示文字消息,2表示图片消息,其他表示未知,不能解析
        public int Len;//4个字节
        public string Message;
        public byte[] Tail;

        public SSMessage()
        {
            Start = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
            Tail = new byte[] { 0x1F, 0x1F, 0x1F, 0x1F };
        }

        public byte[] ToBytes()
        {
            List<byte> list = new List<byte>();
            list.AddRange(Start);
            var t = BitConverter.GetBytes(Type);
            list.AddRange(t);

            var t3 = System.Text.Encoding.UTF8.GetBytes(Message);
            var t2 = BitConverter.GetBytes(t3.Length);//注意,这里不是Message.Length,而是Message转化成字节数组后的Lenght

            list.AddRange(t2);
            list.AddRange(t3);

            return list.ToArray();
        }
    }
}
