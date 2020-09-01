using IMESAgent.SocketClientEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace IMESServer.Common
{
    public class LogFileTimer
    {
        private Timer timer = null;
        private object lockObject = null;
        private int expiredTime = -1;

        public LogFileTimer(int expired, string logPath, string backupPath = "")
        {
            this.ExpiredTime = expired;
            this.LogPath = logPath;
            this.BackupPath = backupPath;

            lockObject = new object();
        }

        public void Setup()
        {
            timer = new System.Threading.Timer(DeleteExpried, null, Timeout.Infinite, Timeout.Infinite);
            ResetTimer(true);
        }

        private int ExpiredTime
        {
            set
            {
                expiredTime = value < 0 ? 7 : value;
            }
            get
            {
                return expiredTime;
            }
        }

        private void DeleteExpried(object obj)
        {
            try
            {
                lock (lockObject)
                {
                    var lst = GetFiles();

                    foreach (string path in lst)
                    {
                        DirectoryInfo inf = new DirectoryInfo(path);

                        if (inf.Attributes == FileAttributes.Directory)
                        {
                            if (path.DeleteExpiredFolder(ExpiredTime))
                            {
                                
                            }
                        }
                        else if (inf.Attributes == FileAttributes.Archive)
                        {
                            if (path.DeleteExpiredFile(ExpiredTime))
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // nothing will do, just catch the exception
            }
            finally
            {
                ResetTimer(false);
            }
        }

        private void ResetTimer(bool executeNow)
        {
            if (executeNow == true)
            {
                timer.Change(5 * 1000, Timeout.Infinite);
                return;
            }

            TimeSpan now = DateTime.Now.TimeOfDay;
            TimeSpan oneDay = new TimeSpan(24, 0, 0);

            timer.Change(oneDay - now, new TimeSpan(-1));
            //AppEvents.Instance.OnUpdateScreen("ResetTimer : " + (oneDay - now).ToString());
        }

        private IList GetFiles()
        {
            var files = Directory.GetFiles(LogPath).ToList();

            if (!string.IsNullOrEmpty(BackupPath))
            {
                var folders = Directory.GetDirectories(BackupPath).ToList();
                files.AddRange(folders);
            }

            return files;
        }

        private string LogPath
        {
            get;
            set;
        }

        private string BackupPath
        {
            get;
            set;
        }
    }
}
