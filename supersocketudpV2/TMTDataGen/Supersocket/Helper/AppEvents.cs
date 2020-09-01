using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CounterTMT.Enum;

namespace CounterTMT
{
    public class AppEvents
    {

        private static AppEvents instance = null;
        private AppEvents()
        {
        }
        public static AppEvents Instance
        {
            get
            {
                if (instance == null)
                    instance = new AppEvents();

                return instance;
            }
        }
        public delegate void UpdateScreenEventHandler(string msg);  //委托声明 sunjie 1
        public delegate void UpdateLogEventHandler(byte[] msg);  //委托声明 sunjie 1-1
        public delegate void UpdateConnectionStatusEventHandler(AgentStatus status);

        public UpdateScreenEventHandler OnUpdateScreenRun;   //创建委托对象 sunjie 2
        public UpdateLogEventHandler OnUpdateLogRun;   //创建委托对象 sunjie 2-2
        public UpdateConnectionStatusEventHandler OnUpdateConnectionStatusRun;

        public event UpdateScreenEventHandler UpdateScreenEvent
        {
            add
            {
                OnUpdateScreenRun = (UpdateScreenEventHandler)System.Delegate.Combine(OnUpdateScreenRun, value);
            }
            remove
            {
                OnUpdateScreenRun = (UpdateScreenEventHandler)System.Delegate.Remove(OnUpdateScreenRun, value);
            }
        }

        public event UpdateLogEventHandler UpdateLogEvent
        {
            add
            {
                OnUpdateLogRun = (UpdateLogEventHandler)System.Delegate.Combine(OnUpdateLogRun, value);
            }
            remove
            {
                OnUpdateLogRun = (UpdateLogEventHandler)System.Delegate.Remove(OnUpdateLogRun, value);
            }
        }


        public event UpdateConnectionStatusEventHandler UpdateConnectionStatusEvent
        {
            add
            {
                OnUpdateConnectionStatusRun = (UpdateConnectionStatusEventHandler)Delegate.Combine(OnUpdateConnectionStatusRun, value);
            }
            remove
            {
                OnUpdateConnectionStatusRun = (UpdateConnectionStatusEventHandler)Delegate.Remove(OnUpdateConnectionStatusRun, value);
            }
        }
    }
}
