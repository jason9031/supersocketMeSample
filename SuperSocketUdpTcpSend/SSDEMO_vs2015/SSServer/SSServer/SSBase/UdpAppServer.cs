using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receive.udp
{
    class UdpAppServer : AppServer<UdpSession, MyUdpRequestInfo>
    {
        public static readonly log4net.ILog LOG = log4net.LogManager.GetLogger("VideoSocket");
        public static readonly string PATH = "D:\\ReadVersionD\\video";
        public static Dictionary<string, string> PRIVATE_KEY = new Dictionary<string, string>(); //deviceCode->key
        public static readonly int MIN_IMG_SIZE = 100;



        /// <summary>
        /// 通过配置文件安装服务从这里启动
        /// </summary>
        public UdpAppServer() : base(new DefaultReceiveFilterFactory<MyUDPReceiveFilter, MyUdpRequestInfo>())
        {
            this.NewSessionConnected += MyServer_NewSessionConnected;
            this.SessionClosed += MyServer_SessionClosed;
        }

        /// <summary>
        /// winform启动，不使用这里的事件
        /// </summary>
        public UdpAppServer(SessionHandler<UdpSession> NewSessionConnected, SessionHandler<UdpSession, CloseReason> SessionClosed)
            : base(new DefaultReceiveFilterFactory<MyUDPReceiveFilter, MyUdpRequestInfo>())
        {
            this.NewSessionConnected += NewSessionConnected;
            this.SessionClosed += SessionClosed;
        }



        public static ConcurrentDictionary<string, VideoPackageModel> FILE_WRITERS = new ConcurrentDictionary<string, VideoPackageModel>(); //读取文件

        protected override void OnSystemMessageReceived(string messageType, object messageData)
        {
            LogHelper.WriteLog(string.Format("Udp server Receive：{0}:{1}", messageType, messageData));
            Console.WriteLine(messageType);
            base.OnSystemMessageReceived(messageType, messageData);
        }
        void MyServer_NewSessionConnected(UdpSession session)
        {
            //连接成功
        }

        void MyServer_SessionClosed(UdpSession session, CloseReason value)
        {

        }

        /// <summary>
        /// 过滤器后 进入该方法
        /// </summary>
        /// <param name="session"></param>
        /// <param name="requestInfo"></param>
        protected override void ExecuteCommand(UdpSession session, MyUdpRequestInfo requestInfo)
        {
            if (requestInfo.Key.Equals(HandleUdpUtils.WEB_SESSION_KEY))
            {
                handleWebUdpSocket(requestInfo, session);
                return;
            }
            string privateKey = "";
            if (PRIVATE_KEY.TryGetValue(requestInfo.SessionID, out privateKey) && privateKey.Equals(requestInfo.Key))
            {
                handleDeviceInfo(requestInfo);
                session.Send("receive success");
            }
            else
            {
                LOG.Error(requestInfo.SessionID + "|不存在该设备");
            }

        }





        #region WEB

        /// <summary>
        /// 发送socket 到web客户端
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <param name="session"></param>
        private void handleWebUdpSocket(MyUdpRequestInfo requestInfo, UdpSession session)
        {
            switch (handleWebCommand(requestInfo))
            {
                case 1:
                    session.Send("addOK");
                    break;
                case 0:
                    session.Send("dataError");
                    break;
                case -1:
                    session.Send("alreadyExit");
                    break;
            }
        }


        /// <summary>
        /// body的组成 : 5|DateType
        /// </summary>
        /// <param name="requestInfo"></param>
        private int handleWebCommand(MyUdpRequestInfo requestInfo)
        {
            var startTime = DateTime.Now;
            DateTime endTime;
            int seconds = HandleUdpUtils.getLasingTime(requestInfo.Body, out startTime, out endTime);
            if (seconds == 0)
            {
                LOG.Error(requestInfo.SessionID + "|获取录像持续时间失败！");
                return 0;
            }
            string privateKey = "";
            PRIVATE_KEY.TryGetValue(requestInfo.SessionID, out privateKey);



            if (!addVideoPackageModel(requestInfo.SessionID, privateKey, startTime, endTime, seconds))
            {
                Console.WriteLine(requestInfo.SessionID + "|添加失败！");
                LOG.Error(requestInfo.SessionID + "|添加失败！");//TODO 存在更新的操作
                return -1;
            }
            else
            {
                Console.WriteLine(requestInfo.SessionID + "|添加成功！");
                LOG.Info(requestInfo.SessionID + "|添加成功！");
                return 1;
            }
        }

        /// <summary>
        /// 添加VideoPackageModel
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool addVideoPackageModel(string sessionId, string privateKey, DateTime startTime, DateTime endTime, int seconds)
        {
            VideoPackageModel oldModel;

            if (FILE_WRITERS.TryGetValue(sessionId, out oldModel))
            {
                if (oldModel.Disposed) //可能没删除
                {
                    FILE_WRITERS.TryRemove(sessionId, out oldModel);
                    VideoPackageModel model = new VideoPackageModel(privateKey, sessionId, startTime, endTime, seconds, PATH); //避免计时器出错
                    return FILE_WRITERS.TryAdd(sessionId, model);
                }
                return false;
            }
            else
            {
                VideoPackageModel model = new VideoPackageModel(privateKey, sessionId, startTime, endTime, seconds, PATH);
                return FILE_WRITERS.TryAdd(sessionId, model);
            }
        }


        #endregion



        #region 录像设备
        /// <summary>
        /// 处理设备发来的视频
        /// </summary>
        /// <param name="requestInfo"></param>
        private void handleDeviceInfo(MyUdpRequestInfo requestInfo)
        {
            VideoPackageModel videoModel = getVideoPackage(requestInfo.SessionID);

            if (videoModel == null || requestInfo.Body.Length < MIN_IMG_SIZE)
            {
                return;
            }

            MemoryStream stream = null;
            stream = new MemoryStream(requestInfo.Body);
            Bitmap img = null;
            try
            {
                img = HandleUdpUtils.BytesToBitmap(requestInfo.Body);
                // img.Save("D:\\JGHPCXReadVersionD\\video\\001\\test.jpg");
                videoModel.wirteVideo(img);

            }
            catch (Exception e)
            {
                LOG.Error(requestInfo.SessionID + "|" + e.ToString());
                Console.WriteLine(requestInfo.SessionID + "|" + e.ToString());
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (img != null)
                    img.Dispose();
            }
        }

        /// <summary>
        /// getVideoPackage
        /// </summary>
        /// <param name="machineCode"></param>
        /// <returns></returns>
        private VideoPackageModel getVideoPackage(string sessionId)
        {
            VideoPackageModel video;
            if (FILE_WRITERS.TryGetValue(sessionId, out video))
            {
                LOG.Error(sessionId + "|设备未开启录像");
            }
            return video;
        }


        #endregion


    }

}
