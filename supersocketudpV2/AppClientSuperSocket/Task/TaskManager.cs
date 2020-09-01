using AppClientSuperSocket;
using IMESAgent.Log;
using IMESAgent.SocketClientEngine.CommonInfo;
using IMESAgent.SocketClientEngine.FileLoaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IMESAgent.SocketClientEngine.Watcher
{
    internal class TaskManager
    {
        private List<TaskInfo> allTasks = null;
        private object lockObject = null;
        private Timer folderScannerTimer = null;
        private bool processing = false;

        public TaskManager()
        {
            allTasks = new List<TaskInfo>();
            lockObject = new object();
            Logger = new Log4NetLogFactory().GetLog(this.GetHashCode().ToString());
            AppEvents.Instance.StopAllTasksEvent += StopAll;
        }
        public ILog Logger
        {
            get;
            private set;
        }
        public TaskManager(ConfigInfo info)
            : this()
        {
            this.ConfigInfo = info;
        }

        public TaskManager(ConfigInfo info, FtpInfo ftpInfo)
            : this(info)
        {
            this.FtpInfo = ftpInfo;
        }

        public void InitializeTasks<T>(IList<T> lst) where T : GatheringPointsInfo
        {
            Logger = new Log4NetLogFactory().GetLog(this.GetHashCode().ToString());
            foreach (T info in lst)
            {
                if (info.HeartBeat == true)
                {
                    var taskInfo = CreateTaskInfo(string.Empty, null, WatcherType.HeartBeat);
                    allTasks.Add(taskInfo);

                    continue;
                }

                if (info.Includes == null)
                    continue;

                var filters = info.Includes.Split(':');
                foreach (var st in filters)
                {
                    var taskInfo = CreateTaskInfo(st, info);

                    if (taskInfo == null && info != null)
                    {
                        AppEvents.Instance.OnShowDialog(ExceptionMessage.PathError + info.LogFolder, UserMessage.Error);
                        continue;
                    }

                    allTasks.Add(taskInfo);
                }
            }

            // 06-27-2018 
            // As Ken request, comment this feature temporarily.. 
            //var updatingStatusTaskInfo = CreateTaskInfo(string.Empty, null, WatcherType.AgentStatus);
            //allTasks.Add(updatingStatusTaskInfo);
        }

        private TaskInfo CreateTaskInfo(string filter, GatheringPointsInfo gInfo, WatcherType type = WatcherType.NONE)
        {
            try
            {
                var taskInfo = new TaskInfo();

                if (gInfo != null && ConfigInfo != null)
                {
                    taskInfo.Filter = filter;
                    taskInfo.Index = gInfo.NO;
                }

                taskInfo.Watcher = WatcherFactory.CreateWatcher(gInfo, ConfigInfo, FtpInfo, SetFilter(filter), type);
                taskInfo.ProcessingTask = new Task(StartProcessing, taskInfo, TaskCreationOptions.LongRunning);

                return taskInfo;
            }
            catch (Exception ex)
            {
                AppEvents.Instance.OnUpdateScreen("Error : " + ex.Message);
                return null;
            }
        }

        private string SetFilter(string filter)
        {
            return string.IsNullOrEmpty(filter) ? UserMessage.AllFiles : "*." + filter;
        }

        public FtpInfo FtpInfo
        {
            get;
            private set;
        }

        public ConfigInfo ConfigInfo
        {
            get;
            private set;
        }

        public void SetupFolderScannerTimer()
        {
            //AgencyInfo.FileScanningInterval = 3000.ToString() ;
            var interval = ConfigInfo.FileScanningInterval.ToInt();
            interval = interval < Constant.ScanningInterval ? Constant.ScanningInterval : interval;

            folderScannerTimer = new Timer(OnTick, null, 2000, interval); //sunjie  6
        }

        private void OnTick(object ob)
        {
            Logger.Debug("OnTick start : ");
            string strTaskInfo = string.Empty;
            lock (lockObject)
            {
                try
                {
                    var tmpls = new List<Task>();

                    if (allTasks != null && allTasks.Count > 0)
                    {
                        Array.ForEach(allTasks.ToArray(), info =>
                        {
                            Task t = new Task(StratScanning, info.Watcher); //sunjie  6
                            tmpls.Add(t);
                            if (!string.IsNullOrEmpty(info.Filter))
                                strTaskInfo += ("\r\n----------------------------------------------------------------------------------------------------------"+ info.Filter.ToString() + "  " + info.Watcher.ToString() + "\r\n--- allTasks count = ---" + allTasks.ToString() + allTasks.Count.ToString() + "  t.Id = " + t.Id.ToString() + info.Watcher.LogPath + "\r\n");
                            else
                            {
                                strTaskInfo += ("info.Filter  is empty t.id = " + t.Id.ToString());
                            }
                        });

                        Parallel.ForEach(tmpls, tak =>
                        {
                            tak.Start();
                            strTaskInfo += ("\r\n" + tak.Id.ToString() + "--Start" );
                        });
                        try
                        {
                            strTaskInfo += ("\r\n-Task.WaitAll(tmpls.ToArray())---count" + tmpls.Count.ToString());
                            Logger.Debug("\r\n Debug : OK " + strTaskInfo);
                            if (Task.WaitAll(tmpls.ToArray(), 50000))
                            {
                                Logger.Debug("\r\nDebug : OK  < 10000=============================================================================================");
                            }
                            else
                            {
                                AppEvents.Instance.OnUpdateConnectionStatus(AgentStatus.ERROR);
                                Logger.Debug("Debug : NG  > 10000 AgentStatus.ERROR =============================================================================================" + strTaskInfo);
                                AppEvents.Instance.OnUpdateScreen("AgentStatus.ERROR 请检查目录" );
                            }
                        }
                        catch (AggregateException ex)
                        {
                            AppEvents.Instance.OnUpdateScreen("Debug : " + ex.Message );
                            Logger.Debug("Debug :  Ex " + ex.Message + strTaskInfo);

                            foreach (var item in ex.InnerExceptions)
                            {
                                Logger.Debug("异常类型：" + Environment.NewLine + item.GetType() + "来自：" + item.Source + Environment.NewLine + "异常内容：" + item.Message);
                            }
                        }
                        if (processing == false)
                        {
                            Parallel.ForEach(allTasks, takInfo =>
                            {
                                takInfo.StartTask();
                            });
                            processing = true;
                        }

                        GC.Collect();
                    }
                }
                catch (Exception ex)
                {
                    AppEvents.Instance.OnUpdateScreen("Debug : " + ex.Message);
                    Logger.Debug("Debug : " + ex.Message);
                    var item = ex;
                    {
                        Logger.Debug("异常类型：" + Environment.NewLine+ item.GetType() +"来自："+ item.Source +Environment.NewLine+"异常内容：" + item.Message );
                    }
                }
            }
        }

        private void StratScanning(object ob)
        {
            var watcher = ob as WatcherBase;

            if (watcher != null)
            {
                watcher.ProcessScanning();//sunjie  5
            }
        }

        private void StartProcessing(object ob)
        {
            var info = ob as TaskInfo;

            if (info != null)
            {
                info.Start();
            }
        }

        public void StopAll()
        {
            if (allTasks != null)
            {
                allTasks.ForEach(info =>
                {
                    info.Stop();
                    info.StopTask();
                });

                allTasks.Clear();
            }

            if (folderScannerTimer != null)
            {
                folderScannerTimer.Change(Timeout.Infinite, Timeout.Infinite);
                folderScannerTimer.Dispose();
            }
        }

        private int TotalBlockingCount
        {
            get
            {
                try
                {
                    if (allTasks != null)
                    {
                        var value = (from watcher in (from task in allTasks select task.Watcher) where watcher != null select watcher.BlockingCount).ToArray();
                        return value.Sum();
                    }

                    return 0;
                }
                catch
                {
                    return 0;
                }
            }
        }
    }
}
