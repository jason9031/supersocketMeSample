using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSServer
{
    /// <summary>
    /// 使用Log4net插件的log日志对象
    /// </summary>
    public static class AppLog
    {
        private static ILog log;

        static AppLog()
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
            log = LogManager.GetLogger(typeof(AppLog));
        }

        public static void Debug(object message)
        {
            log.Debug(message);
        }

        public static void DebugFormatted(string format, params object[] args)
        {
            log.DebugFormat(format, args);
        }

        public static void Info(object message)
        {
            if (message == null)
            {
                log.Info("null");
            }
            else
            {
                log.Info(message);
            }
        }

        public static void InfoFormatted(string format, params object[] args)
        {
            log.InfoFormat(format, args);
        }

        public static void Warn(object message)
        {
            if (message == null)
            {
                log.Warn("null");
            }
            else
            {
                log.Warn(message);
            }
        }

        public static void Warn(object message, Exception exception)
        {
            log.Warn(message, exception);
        }

        public static void WarnFormatted(string format, params object[] args)
        {
            log.WarnFormat(format, args);
        }

        public static void Error(object message)
        {
            if (message == null)
            {
                log.Error("null");
            }
            else
            {
                log.Error(message);
            }
        }

        public static void Error(object message, Exception exception)
        {
            log.Error(message, exception);
        }

        public static void ErrorFormatted(string format, params object[] args)
        {
            log.ErrorFormat(format, args);
        }

        public static void Fatal(object message)
        {
            if (message == null)
            {
                log.Fatal("null");
            }
            else
            {
                log.Fatal(message);
            }
        }

        public static void Fatal(object message, Exception exception)
        {
            log.Fatal(message, exception);
        }

        public static void FatalFormatted(string format, params object[] args)
        {
            log.FatalFormat(format, args);
        }

    }
}
