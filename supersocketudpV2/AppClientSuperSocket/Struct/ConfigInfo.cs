using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppClientSuperSocket
{
    /// <summary>
    /// load all the information saved in the configruation file.
    /// </summary>
    public class ConfigInfo
    {
        public ConfigInfo()
        {

        }

        // 08/10/2018
        // change to private set
        public string SocketIpAddress
        {
            get;
            private set;
        }

        // 08/10/2018
        // change to private set
        public string SocketPortNumber
        {
            get;
            private set;
        }

        public string BackupDirectory
        {
            get;
            set;
        }

        public string ConfigDirectory
        {
            get;
            set;
        }

        public string LogRetentionPeroid
        {
            get;
            set;
        }

        public string AgentPassword
        {
            get;
            set;
        }

        public string GatheringPointsInfo
        {
            get;
            set;
        }

        public string ParseTypeInfo
        {
            get;
            set;
        }

        public string PollingInterval
        {
            get;
            set;
        }

        public string DateExpired
        {
            get;
            set;
        }

        public string NetworkSegment
        {
            get;
            set;
        }

        public string DelayBeforeMove
        {
            get;
            set;
        }

        public string TcpTimedWaitDelay
        {
            get;
            set;
        }

        public string FileScanningInterval
        {
            get;
            set;
        }
        public string EmptyFolderAndOtherFilesDelInterval
        {
            get;
            set;
        }

        public string TimeServer
        {
            get;
            set;
        }

        public string ServerType
        {
            get;
            set;
        }

        public string SyncInterval
        {
            get;
            set;
        }

        //public string TcpTimedWaitDelay
        //{
        //    get;
        //    set;
        //}

        public string MinimumAvailableSpace
        {
            get;
            set;
        }

        public string UpdateStatusInterval
        {
            get;
            set;
        }
    }
}
