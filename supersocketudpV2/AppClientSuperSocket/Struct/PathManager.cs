using IMESAgent.SocketClientEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppClientSuperSocket
{
    public class PathManager
    {
        private static PathManager pm = null;
        private string startupPath = string.Empty;
        private ConfigInfo info = null;

        private PathManager(string startupPath)
        {
            this.startupPath = startupPath;
        }

        public static PathManager Initialize(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;
            if (pm == null) pm = new PathManager(path);

            return pm;
        }

        public static PathManager Instance
        {
            get
            {
                return pm;
            }
        }

        //public string ArchiveFolder
        //{
        //    get;
        //    private set;
        //}

        public string BackupFolder
        {
            get;
            private set;
        }

        public string ConfigFilePath
        {
            get
            {
                return string.Format("{0}\\{1}\\{2}", startupPath, UserMessage.Config, UserMessage.ConfigFile);
            }
        }

        public string ParserTypeInfoPath
        {
            get;
            private set;
        }

        public string GatheringInfoPath
        {
            get;
            private set;
        }

        public ConfigInfo confinfo //Agency
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
                InitializePaths(value);
            }
        }

        public string FtpConfigPath
        {
            get
            {
                return string.Format("{0}\\{1}\\{2}", startupPath, UserMessage.Config, UserMessage.FtpConfigFile);
            }
        }

        public string TimeSyncBat
        {
            get
            {
                return string.Format("{0}\\{1}", startupPath, UserMessage.TimeSync);
            }
        }

        private void InitializePaths(ConfigInfo info)
        {
            if (string.IsNullOrEmpty(startupPath) || info == null) return;

            // the absolute path should be loaded from the configuration file.
            this.BackupFolder = info.BackupDirectory;
            //this.ArchiveFolder = info.ArchiveDirectory;

            this.ParserTypeInfoPath = string.Format("{0}\\{1}\\{2}", startupPath, info.ConfigDirectory, info.ParseTypeInfo);

#if TestMode
            this.GatheringInfoPath = string.Format("{0}\\{1}\\Test_{2}", startupPath, info.ConfigDirectory, info.GatheringPointsInfo);
#else
            this.GatheringInfoPath = string.Format("{0}\\{1}\\{2}", startupPath, info.ConfigDirectory, info.GatheringPointsInfo);
#endif
        }
    }
}
