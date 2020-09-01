using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace TMTDataGen.Supersocket
{
    /// <summary>
    /// 协议过滤
    /// </summary>
    public class ReceiveFilter : BeginEndMarkReceiveFilter<StringPackageInfo>//开头结尾
    {
        //可选继承类：
        //TerminatorReceiveFilter
        //BeginEndMarkReceiveFilter
        //FixedHeaderReceiveFilter
        //FixedSizeReceiveFilter
        //CountSpliterReceiveFilter
        public ReceiveFilter()
            : base(Encoding.ASCII.GetBytes("#"), Encoding.ASCII.GetBytes("$\r\n"))
        {

        }
        /// <summary>
        /// 经过过滤器，收到的字符串会到这个函数
        /// </summary>
        /// <param name="bufferStream"></param>
        /// <returns></returns>
        public override StringPackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            return null;
        }
    }
}
