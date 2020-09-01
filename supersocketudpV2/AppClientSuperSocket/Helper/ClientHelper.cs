using IMESAgent.SocketClientEngine;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using ParserTypes = System.Collections.Generic.List<AppClientSuperSocket.ParserTypeInfo>;
using GatheringPoints = System.Collections.Generic.List<IMESAgent.SocketClientEngine.FileLoaders.GatheringPointsInfo>;
using MachineLearning.SocketClientEngine.Info;
using System.Net;
using System.Net.Sockets;
using IMESAgent.SocketClientEngine.Watcher;
using IMESServer.Common;
using IMESAgent.SocketClientEngine.FileLoaders;

namespace AppClientSuperSocket
{

    public class ClientHelper
    {
        private PathManager pathManager = null;
        private DataTable table = null;
        private ParserTypes parsers = null;
        private const int CsvVersion = 16;
        private static ClientHelper client = null;
        private TaskManager taskManager = null;
        private LogFileTimer logTimer = null;
        private GatheringPoints gatheringPoints = null;
        private ClientHelper()
        {
            //AppEvents.Instance.GetIpAddressEvent += GetIpAddress;
            //AppEvents.Instance.GetBcrIpEvent += GetBcrIp;
        }

        /// <summary>
        /// Load CSV files 
        /// </summary>
        /// <param name="path">CSV file path</param>

        private ClientHelper(string path)
            : this()
        {
            pathManager = PathManager.Instance;

            LoadConfiguration();
            //InitializeFolders();
            //SaveToGatheringPoints();
            UpdateGatheringInfos(pathManager.GatheringInfoPath);

            //InitializeRegistry();
            //InitializeTasks();
        }
        private void UpdateGatheringInfos(string path)
        {
            string ip = GetIpAddress().ToString();
            bool refresh = false;

            foreach (GatheringPointsInfo info in gatheringPoints)
            {
                if (info.AgentIp != ip)
                {
                    info.AgentIp = ip;
                    refresh = true;
                }
            }

            if (refresh)
            {
                table = RemoveFakeRecord();
                SaveToCsv(path, table);
            }
        }
        private DataTable RemoveFakeRecord()
        {
            var result = (from g in gatheringPoints where g.HeartBeat == false select g).ToList<GatheringPointsInfo>();
            return result.ToDataTable();
        }

        public static ClientHelper InitializeClientHelper(string appPath)
        {
            if (client == null) client = new ClientHelper(appPath);

            return client;
        }
        public DataTable UpdateGatheringInfos(string index, string alias, string bcrIp, string logPath, string route, string type)
        {
            var gatherInfo = (from g in gatheringPoints where g.NO.ToString() == index select g).First();
            var parserInfo = GetSpecifiedPaser(route, type);

            if (gatherInfo != null && parserInfo != null)
            {
                gatherInfo.UpdateParserInfo(parserInfo);

                gatherInfo.Alias = alias;
                gatherInfo.BcrIp = bcrIp;
                gatherInfo.LogFolder = logPath;
                gatherInfo.Line = alias;
            }

            return RemoveFakeRecord();
        }

        public ParserTypeInfo GetSpecifiedPaser(string routeName, string typeName)
        {
            var info = parsers.GetParserInfo(routeName, typeName);

            return info;
        }
        public DataTable SaveToDatatable(string alias, string bcrip, string logPath, string route, string type)
        {
            ParserTypeInfo info = GetSpecifiedPaser(route, type);

            if (info == null)
                throw new Exception(ExceptionMessage.NotMatched);

            var gatherInfo = CreateGatheringPointsInfo(info, alias, bcrip, logPath);

            if (gatherInfo != null)
                gatheringPoints.Add(gatherInfo);

            return RemoveFakeRecord();
        }
        private GatheringPointsInfo CreateGatheringPointsInfo(ParserTypeInfo parserInfo, string alias, string bcrip, string logPath)
        {
            var gatherInfo = new GatheringPointsInfo(parserInfo);

            if (gatherInfo != null)
            {
                gatherInfo.AgentIp = GetIpAddress().ToString();
                gatherInfo.Alias = alias;
                gatherInfo.BcrIp = bcrip;
                gatherInfo.LogFolder = logPath;
                gatherInfo.Line = alias;
                gatherInfo.NO = (GetMaxNO() + 1).ToString();
            }

            return gatherInfo;
        }
        private int GetMaxNO()
        {
            List<string> stList = gatheringPoints.Select(p => p.NO).ToList<string>();
            List<int> lst = stList.Select(p => int.Parse(p)).ToList<int>();

            int max = lst.Max();
            return max == -999 ? 1 : max;
        }
        public static ClientHelper Instance
        {
            get
            {
                return client;
            }
        }
        public void SaveLogConfig(string path, string days)
        {
            ConfigInfo.BackupDirectory = path;
            ConfigInfo.LogRetentionPeroid = days;

            XmlHelper<ConfigInfo>.UpdateXmlElement(pathManager.ConfigFilePath, XmlElementName.BackupDirectory, path);
            XmlHelper<ConfigInfo>.UpdateXmlElement(pathManager.ConfigFilePath, XmlElementName.LogRetentionPeroid, days);
        }
        private void LoadConfiguration()
        {
            ConfigInfo = LoadAgencyInfo(pathManager.ConfigFilePath);
           // FtpInfo = LoadFtpInfo(pathManager.FtpConfigPath);
            pathManager.confinfo = ConfigInfo;

            parsers = LoadParserInfo(pathManager.ParserTypeInfoPath);
            table = LoadGatheringPointsInfo(pathManager.GatheringInfoPath);
        }
        private ParserTypes LoadParserInfo(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                throw new Exception(UserMessage.ParseTypeInfoCsv + ExceptionMessage.ConfigNoFound);
            }

            var table = CsvFileHelper.LoadCsvFile(path);
            return table.ToList<ParserTypeInfo>();
        }
        public string[] GetInspTypes(string routeName)
        {
            var ls = parsers.ToParserTypeList(routeName);

            return ls.ToArray<string>();
        }
        public string[] GetInspRoutes()
        {
            var ls = parsers.ToParserGroupList().ToList<string>();

            return ls.ToArray<string>();
        }
        public DataTable GatheringPointTable
        {
            get
            {
                return table;
            }
        }
        public bool RemoveGatheringInfo(string index, DataRow row)
        {
            table.Rows.Remove(row);
            return gatheringPoints.Remove(gatheringPoints.Where(g => g.NO.ToString() == index).FirstOrDefault());
        }
        public DataTable LoadGatheringPointsInfo(string path)
        {
            if (ConfigInfo == null)
                return null;

            var versionNumber = CsvFileHelper.GetCsvVersion(path);

            if (!File.Exists(path) || (File.Exists(path) && versionNumber == -1))
            {
                CsvFileHelper.CreateBlankCsv(path, UserMessage.BlankCsv);
            }
            else
            {
                // if the feild item didn't find, the file should be updated.
                if (versionNumber != CsvVersion)
                {
                    table = CsvFileHelper.LoadCsvFile(path, FieldName.FileNameFilter);
                    SaveToCsv(path, table);
                }
            }

            return CsvFileHelper.LoadCsvFile(path);
        }
        public bool SaveToCsv(string path, DataTable table)
        {
            return CsvFileHelper.SaveToCsvFile(path, table);
        }
        public ConfigInfo ConfigInfo
        {
            get;
            private set;
        }
        private ConfigInfo LoadAgencyInfo(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                throw new Exception(UserMessage.ConfigFile + ExceptionMessage.ConfigNoFound);
            }
            InitializeXmlSetting(path);
            return XmlHelper<ConfigInfo>.Load(path);
        }
        public IPAddress GetIpAddress()
        {
            IPAddress[] address = Dns.GetHostAddresses(Dns.GetHostName()).Where(ip => (ip.AddressFamily == AddressFamily.InterNetwork)).ToArray();

            if (ValidateNetworkSegment())
            {
                foreach (IPAddress ip in address)
                {
                    if (ip.ToString().StartsWith(ConfigInfo.NetworkSegment))
                        return ip;
                }
            }

            return address.First();
        }
        public void SetupTasks(string path)
        {
            if (taskManager != null)
                taskManager.SetupFolderScannerTimer(); //sunjie  7

            if (!string.IsNullOrEmpty(path))
            {
                logTimer = new LogFileTimer(ConfigInfo.LogRetentionPeroid.ToInt(), path + "\\Logs", ConfigInfo.BackupDirectory);
                logTimer.Setup();
            }
        }
        private bool ValidateNetworkSegment()
        {
            if (ConfigInfo == null) return false;
            if (string.IsNullOrEmpty(ConfigInfo.NetworkSegment)) return false;
            if (ConfigInfo.NetworkSegment == "0") return false;

            return true;
        }
        public void SaveIPAddressInfo(string ip, string port)
        {
            // 08/10/2018 
            // Don't allow to active the new IP and port until restart program
            //AgencyInfo.SocketIpAddress = ip;
            //AgencyInfo.SocketPortNumber = port;

            XmlHelper<ConfigInfo>.UpdateXmlElement(pathManager.ConfigFilePath, XmlElementName.SocketIpAddress, ip);
            XmlHelper<ConfigInfo>.UpdateXmlElement(pathManager.ConfigFilePath, XmlElementName.SocketPortNumber, port.ToString());
        }
        private void InitializeXmlSetting(string path)
        {
            XmlHelper<ConfigInfo>.CreateXmlElement(path, XmlElementName.BasicField, XmlElementName.NetworkSegment, "109");
            XmlHelper<ConfigInfo>.CreateXmlElement(path, XmlElementName.BasicField, XmlElementName.DelayBeforMove, "2000");

            // correct the spelling mistake 
            XmlHelper<ConfigInfo>.UpdateFieldName(path, XmlElementName.BasicField, XmlElementName.PullingInterval, XmlElementName.PollingInterval);

            // force to set the port to 4105
            //XmlHelper<ConfigInfo>.UpdateXmlElement(path, XmlElementName.SocketPortNumber, "4105");

            // force to set the polling interval to 300000, do a heartbeat every 5 minutes.
            XmlHelper<ConfigInfo>.UpdateXmlElement(path, XmlElementName.PollingInterval, "300000");

            // remove the obsolete emlement
            XmlHelper<ConfigInfo>.RemoveXmlElement(path, XmlElementName.Archive);
            XmlHelper<ConfigInfo>.RemoveXmlElement(path, XmlElementName.Relationship);
            XmlHelper<ConfigInfo>.RemoveXmlElement(path, XmlElementName.Basic, XmlElementName.LoadHistory);

            // set the interval to 600,000 ms
            XmlHelper<ConfigInfo>.CreateXmlElement(path, XmlElementName.BasicField, XmlElementName.FileScanningInterval, "300000");
            //XmlHelper<ConfigInfo>.CreateXmlElement(path, XmlElementName.BasicField, XmlElementName.EmptyFolderAndOtherFilesDelInterval, "1440");

            XmlHelper<ConfigInfo>.CreateXmlElement(path, XmlElementName.BasicField, XmlElementName.SyncInterval, "3600");
            XmlHelper<ConfigInfo>.CreateXmlElement(path, XmlElementName.BasicField, XmlElementName.TimeServer, "109.116.6.57,0x9");
            XmlHelper<ConfigInfo>.CreateXmlElement(path, XmlElementName.BasicField, XmlElementName.ServerType, "NTP");

            XmlHelper<ConfigInfo>.CreateXmlElement(path, XmlElementName.BasicField, XmlElementName.TcpTimedWaitDelay, "20");
            XmlHelper<ConfigInfo>.CreateXmlElement(path, XmlElementName.BasicField, XmlElementName.MinimumAvailableSpace, "100");

            XmlHelper<ConfigInfo>.CreateXmlElement(path, XmlElementName.BasicField, XmlElementName.UpdateStatusInterval, "30000");
        }
    }

}
