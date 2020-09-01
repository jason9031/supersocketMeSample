using System.Threading.Tasks;

namespace IMESAgent.SocketClientEngine.Watcher
{
    internal class TaskInfo
    {
        public Task ProcessingTask
        {
            get;
            set;
        }

        public WatcherBase Watcher
        {
            get;
            set;
        }

        public string Filter
        {
            get;
            set;
        }

        public string Index
        {
            get;
            set;
        }

        public void Start()
        {
            if (Watcher != null)
            {
                Watcher.Start();
                Watcher.Process();
            }
        }

        public void Stop()
        {
            if (Watcher != null)
            {
                Watcher.Stop();
            }
        }

        public void StartTask()
        {
            if (ProcessingTask != null)
                ProcessingTask.Start();
        }

        public void StopTask()
        {
            if (ProcessingTask != null)
                ProcessingTask.Wait();
        }
    }
}
