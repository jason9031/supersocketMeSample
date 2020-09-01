namespace IMESAgent.SocketClientEngine
{
    public class UserMessage
    {
        public const string SectionName = "server";

        public const string Warning = "Warning";
        public const string Question = "Question";
        public const string Error = "Error";
        public const string Exclamation = "Exclamation";
        public const string Information = "Information";

        public const string GatheringPointsCsv = @"GatheringPointsInfo.csv";
        public const string ParseTypeInfoCsv = @"ParseTypeInfo.csv";
        public const string Config = @"Config";
        public const string ConfigFile = @"config.xml";
        public const string FtpConfigFile = @"ftpConfig.xml";
        public const string ApplicationName = "Agency";
        public const string DecryptFailed = "Decrypt failed...";

        public const string FtpPath = @"/interface/AGENT_user";
        public const string FtpPath1 = @"IMES/share/C660A/eqpImgFile";
        public const string JPG = "jpg";
        
        public const string AllFiles = "*.*";
        public const string BlankCsv = "no,line,agentIp,alias,bcrIp,logFolder,parseTypeCode,parseTypeName,parseGroupCode,parseGroupName,plugin,includes,excludes,prefixs,interval,fileNameFilter";

        public const string TimeSync = "TimeSync.bat";
        public const string CloneTimestamp = "CloneTimestamp.agent";
        public const string AgentPassword = "agent123";
    }

    public class ExceptionMessage
    {
        public const string WrongPassword = "The input password was incorrect.";
        public const string NotMatched = "The Insp Route and the Insp Type didn't match.";
        public const string NoIpAddress = "Can't find out the system ip address.";

        public const string SingleInstance = "Can't open multiply IMES Agent.";
        public const string ConfirmIPModification = "Are you sure to modify the ip address and port? It will be activated at once.";
        public const string NothingChanged = "Nothing has been changed, please check.";
        public const string ConfirmModification = "Please confirm your modification for the BackupDirectory and the RetentionDays. It will be activated at once.";
        public const string ConfigNoFound = " The configuration file can NOT be found.";
        public const string ReadFailed = "Read file failed.";
        public const string PathError = "The location can NOT be reach. Path = ";
    }

    public class ClientMessage
    {
        public const string ActivateModification = "保存成功，重启客户端后新的配置才会生效。";
        public const string SaveFirst = "请先保存之前修改。";
        public const string Discard = "是否放弃当前修改操作？";
        public const string SaveFailed = "保存失败";
    }

    public class FieldName
    {
        public const string ParseTypeCode = "parseTypeCode";
        public const string ParseTypeName = "parseTypeName";
        public const string ParseGroupCode = "parseGroupCode";
        public const string ParseGroupName = "parseGroupName";
        public const string Plugin = "plugin";
        public const string Includes = "includes";
        public const string Excludes = "excludes";
        public const string Prefixs = "prefixs";
        public const string Interval = "interval";

        public const string No = "no";
        public const string Line = "line";
        public const string AgentIp = "agentIp";
        public const string Alias = "alias";
        public const string BcrIp = "bcrIp";
        public const string LogFolder = "logFolder";
        public const string FileNameFilter = "fileNameFilter";

        public const string LogFilePath = "LogFilePath";
        public const string ModifiedDate = "ModifiedDateTime";
        public const string Persistence = "Persistence_";
        public const string ItemName = "item";
    }

    public class XmlElementName
    {
        public const string Basic = "basic";
        public const string SocketIpAddress = "socketIpAddress";
        public const string SocketPortNumber = "socketPortNumber";
        public const string BackupDirectory = "backupDirectory";
        public const string ConfigDirectory = "configDirectory";
        public const string LogRetentionPeroid = "logRetentionPeroid";
        public const string AgentVersion = "agentVersion";
        public const string AgentPassword = "agentPassword";
        public const string DateExpired = "dateExpired";

        public const string Archive = "archive";
        public const string ArchiveDirectory = "archiveDirectory";
        public const string ArchiveSubDirDateFormatString = "archiveSubDirDateFormatString";

        public const string GatheringPointsInfo = "gatheringPointsInfo";
        public const string ParseTypeInfo = "parseTypeInfo";

        public const string Relationship = "relationship";
        public const string OneToOne = "oneToOne";
        public const string OneToClone = "oneToClone";
        public const string ManyToOne = "manyToOne";

        public const string PullingInterval = "pullingInterval";
        public const string PollingInterval = "pollingInterval";
        public const string LoadHistory = "loadHistory";
        public const string FileScanningInterval = "fileScanningInterval";
        public const string EmptyFolderAndOtherFilesDelInterval = "EmptyFolderAndOtherFilesDelInterval";
        public const string NetworkSegment = "networkSegment";
        public const string BasicField = "basic";
        public const string DelayBeforMove = "delayBeforeMove";
        public const string TcpTimedWaitDelay = "tcpTimedWaitDelay";
        public const string MinimumAvailableSpace = "minimumAvailableSpace";

        public const string TimeServer = "timeServer";
        public const string ServerType = "serverType";
        public const string SyncInterval = "syncInterval";

        public const string UpdateStatusInterval = "updateStatusInterval";

        public const string Yes = "YES";
        public const string No = "NO";
        public const string Clone = "Clone";
        public const string Plugin = "Plugin";
    }

    public class Settings
    {
        public const string NormalDateTimeFormat = "yyyyMMdd HH:mm:ss.ff";
        public const string DashDateFormat = "yyyy-MM-dd";
        public const string DateTimeFormatMsec = "yyyyMMddHHmmssfff";
        public const string DateTimeFormatSec = "yyyyMMddHHmmss";
        public const string DateTimeFormatMsec2 = "yyyyMMddHHmmssff";
        public const string NormalDateTimeFormatDash = "yyyy-MM-dd HH:mm:ss.ff";
    }

    public class Constant
    {
        // the minimum value for scanning  
        public const int ScanningInterval = 50 * 6 * 1000;

        // the minimum value for updating 
        public const int UpdatingInterval = 15 * 1000;

        // the minimum value for heartbeat 
        public const int HeartBeatInterval = 5 * 30 * 1000;

        public const int DeleteTotalMinutesInterval = 12 * 60;

        public const string TcpTimeDelayPath = @"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters";
        public const string NtpPath = @"SYSTEM\CurrentControlSet\Services\W32Time\Parameters";
        public const string PollingIntervalPath = @"SYSTEM\CurrentControlSet\Services\W32Time\TimeProviders\NtpClient";
        public const string ControlPanelPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer";
        
        public const string TcpTimeDelay = "TcpTimedWaitDelay";
        public const string NtpServer = "NtpServer";
        public const string NtpType = "Type";
        public const string ControlPanel = "NoControlPanel";
        public const string PollingInterval = "SpecialPollInterval";
    }
}
