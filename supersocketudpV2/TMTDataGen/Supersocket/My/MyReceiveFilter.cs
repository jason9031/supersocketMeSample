using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMTDataGen.Supersocket.My
{
    public class MyReceiveFilter : FixedHeaderReceiveFilter<MyPackageInfo>
    {
        /// +-------+---+-------------------------------+
        /// |request| l |                               |
        /// | name  | e |    request body               |
        /// |  (2)  | n |                               |
        /// |       |(2)|                               |
        /// +-------+---+-------------------------------+
        public MyReceiveFilter()
        : base(4)
        {

        }
        protected override int GetBodyLengthFromHeader(IBufferStream bufferStream, int length)
        {
            ArraySegment<byte> buffers = bufferStream.Buffers[0];
            byte[] array = buffers.Array;
            int len = array[length - 2] * 256 + array[length - 1];
            //int len = (int)array[buffers.Offset + 2] * 256 + (int)array[buffers.Offset + 3];
            return len;
        }
        public override MyPackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            //第三个参数用0,1都可以
            byte[] header = bufferStream.Buffers[0].Array;
            byte[] bodyBuffer = bufferStream.Buffers[1].Array;
            byte[] allBuffer = bufferStream.Buffers[0].Array.CloneRange(0, (int)bufferStream.Length);
            return new MyPackageInfo(header, bodyBuffer);
        }
    }
}
