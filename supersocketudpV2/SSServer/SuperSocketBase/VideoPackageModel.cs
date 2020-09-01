using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using AForge;

namespace Receive.udp
{
    public class VideoPackageModel : IDisposable
    {
        //校验码
        public string PrivateKey { get; }
        //机器码
        public string SessionId { get; }
        //录制开始时间
        public DateTime StartTime { get; }
        //录制结束时间
        public DateTime EndTime { get; }
        //视频路径
        public string VideoPath { get; }
        //计时器
        private System.Timers.Timer Timer = new System.Timers.Timer();
        //视频写入(32位的FFMPEG)
        // private VideoFileWriter Writer = new VideoFileWriter();

        //是否释放资源

        public bool Disposed = false;
        //avi读取
        /////////////////////////// private AVIWriter aviWriter = new AVIWriter("wmv3");

        //VideoFileWriter 所需的时间戳
        DateTime _firstFrameTime;

        public VideoPackageModel(string privateKey, string sessionId, DateTime startTime, DateTime endTime, int second, string path)
        {
            SessionId = sessionId;
            PrivateKey = privateKey;
            StartTime = startTime;
            EndTime = endTime;
            _firstFrameTime = DateTime.Now;
            int width = 848;    //录制视频的宽度
            int height = 480;   //录制视频的高度
            int fps = 20;
            path = getTimeStamp(path);


            ///////////////////////////aviWriter.Open(path, Convert.ToInt32(width), Convert.ToInt32(height));
            //  Writer.Open(path, width, height, fps, VideoCodec.Default);

            int lastTime = second * 1000;
            Console.WriteLine("持续时间" + lastTime);

            Timer.Elapsed += new System.Timers.ElapsedEventHandler(endTimeEvent);   //到达时间的时候执行事件；
            Timer.AutoReset = false;   //设置是执行一次（false）还是一直执行(true)；
            Timer.Interval = lastTime;//设置定时间隔(毫秒为单位)
            Timer.Enabled = true;

        }

        /// <summary>
        /// video 存储路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string getTimeStamp(string path)
        {
            System.DateTime originTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            long timeStamp = (long)(StartTime - originTime).TotalMilliseconds;
            string filePath = path + "\\" + SessionId;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            return filePath + "\\" + timeStamp + ".avi";
        }



        /// <summary>
        /// 结束资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endTimeEvent(object sender, ElapsedEventArgs e)
        {
            using (this)
            {

                VideoPackageModel videoModel;
                UdpAppServer.FILE_WRITERS.TryRemove(SessionId, out videoModel);
                Console.WriteLine("endWriters");
            }
        }

        /// <summary>
        /// 写入视频
        /// </summary>
        /// <param name="img"></param>
        public void wirteVideo(Bitmap img)
        {
            try
            {
                //////////////////////lock (aviWriter)
                //////////////////////{
                //////////////////////    if (aviWriter != null)
                //////////////////////        aviWriter.AddFrame(img);
                //////////////////////}


                //    if(Writer != null && Writer.IsOpen)
                //    {
                //        lock (Writer)
                //        {
                //            if (_firstFrameTime != null)
                //            {

                //                Writer.WriteVideoFrame(img, TimeSpan.FromMilliseconds(DateTime.Now.ToUniversalTime().Subtract(
                //new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                //).TotalMilliseconds - _firstFrameTime.ToUniversalTime().Subtract(
                //new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                //).TotalMilliseconds));
                //            }
                //            else
                //            {
                //                Writer.WriteVideoFrame(img);
                //                _firstFrameTime = DateTime.Now;
                //            }
                //        }
                //    }

            }
            catch (Exception e)
            {
                _firstFrameTime = DateTime.Now;
                Console.WriteLine(e.ToString());
            }

        }

        #region Dispose 操作

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    ////////////////////////if (aviWriter != null)
                    ////////////////////////{
                    ////////////////////////    try
                    ////////////////////////    {
                    ////////////////////////        aviWriter.Close();
                    ////////////////////////        aviWriter.Dispose();
                    ////////////////////////    }
                    ////////////////////////    catch (Exception e)
                    ////////////////////////    {
                    ////////////////////////        Console.WriteLine(e);

                    ////////////////////////    }
                    ////////////////////////    finally
                    ////////////////////////    {
                    ////////////////////////        aviWriter = null;
                    ////////////////////////    }


                    ////////////////////////}
                    if (Timer != null)
                    {
                        Timer.Dispose();
                        Timer = null;
                    }
                }
                //处理非托管
                Disposed = true;
            }
        }




        ~VideoPackageModel()
        {
            Dispose(false);
        }


        #endregion

    }
}
