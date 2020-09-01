using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receive.udp
{
    //handle byte 
    public class HandleUdpUtils
    {
        public static readonly string IMG_END_FLAG = "##"; //设备传来的结束标志

        public static readonly string WEB_END_FLAG = "**"; //WEB传来的结束标志

        public static readonly string WEB_SESSION_KEY = "f21c2a0689443179082e02f8f44079";

        public static readonly int KEY_LENGTH = 30;

        public static readonly int MACHINE_CODE_LENGTH = 80;

        public enum TimeTypeEnum { Day, Hour, Minute, Second }


        /// <summary>
        /// 读取byte[]并转化为图片
        /// </summary>
        /// <param name="bytes">byte[]</param>
        /// <returns>Image</returns>
        public static Bitmap BytesToBitmap(byte[] Bytes)
        {
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(Bytes);
                return new Bitmap((Image)new Bitmap(stream));
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
        }

        /// <summary>
        /// 得到传输来的结束标志
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string getEndFlagStr(byte[] bytes, int length)
        {
            int begin = length - 2;
            int size = 2;
            string endFlag = Encoding.ASCII.GetString(bytes, begin, size);
            return String.IsNullOrEmpty(endFlag) ? "" : endFlag;
        }

        /// <summary>
        /// 根据标志判断客户端类型
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static int getClientType(byte[] bytes, int length)
        {
            var endFlagStr = getEndFlagStr(bytes, length);
            if (endFlagStr.Equals(WEB_END_FLAG)) //web
                return 1;
            else if (endFlagStr.Equals(IMG_END_FLAG)) //device
                return 2;
            else
                return 0;

        }

        /// <summary>
        /// 长度为40的密钥
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string getPrivateKey(byte[] bytes)
        {
            string privateKey = "";
            if (bytes.Length <= KEY_LENGTH)
                return privateKey;
            privateKey = Encoding.ASCII.GetString(bytes, 0, KEY_LENGTH).TrimEnd('\0'); //取掉休止符

            return privateKey;
        }

        /// <summary>
        /// 得到机器码
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string getMachineCode(byte[] bytes)
        {
            string machineCode = "";
            if (bytes.Length <= MACHINE_CODE_LENGTH)
                return machineCode;
            machineCode = Encoding.ASCII.GetString(bytes, KEY_LENGTH, MACHINE_CODE_LENGTH - KEY_LENGTH).TrimEnd
                ('\0');

            return machineCode;
        }


        /// <summary>
        /// 密钥构造成字节数组 （客户端）
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static byte[] InitPrivateKeyBytes(string privateKey)
        {
            byte[] fixedBytes = new byte[40];
            if (String.IsNullOrEmpty(privateKey))
                return fixedBytes;
            var bytes = Encoding.Default.GetBytes(privateKey);

            for (int i = 0; i < bytes.Length; i++)
                fixedBytes[i] = bytes[i];


            return fixedBytes;

        }


        /// <summary>
        /// web取截止时间
        /// </summary>
        /// <param name="body"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static bool StrToTime(byte[] body, out DateTime endTime)
        {
            string str = Encoding.Default.GetString(body);
            endTime = DateTime.Now;
            try
            {
                DateTime dateTime = Convert.ToDateTime(str);
                endTime = dateTime;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 得到计时时间
        /// </summary>
        /// <param name="body"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int getLasingTime(byte[] body, out DateTime startTime, out DateTime endTime)
        {

            string str = Encoding.Default.GetString(body);
            int index = str.IndexOf("|");
            startTime = DateTime.Now;
            endTime = DateTime.Now;
            if (index == -1)
                return 0;
            var timeType = str.Substring(index + 1); //5|Second
            var num = 0;
            try
            {
                num = Convert.ToInt32(str.Substring(0, index));
            }
            catch (Exception)
            {
                return 0;
            }

            switch (timeType)
            {
                case nameof(TimeTypeEnum.Day):
                    num *= 12 * 60 * 60;
                    break;
                case nameof(TimeTypeEnum.Hour):
                    num *= 60 * 60;
                    break;
                case nameof(TimeTypeEnum.Minute):
                    num *= 60;
                    break;
                case nameof(TimeTypeEnum.Second):

                    break;
                default:
                    num = 0;
                    break;
            }
            endTime = startTime.AddSeconds(num);
            return num;
        }
    }
}
