using AppClientSuperSocket;

namespace IMESAgent.SocketClientEngine.FileLoaders
{
    public class GatheringPointsInfo : ParserTypeInfo
    {
        private int index = 0;

        public GatheringPointsInfo()
        {
            HeartBeat = false;
        }

        public GatheringPointsInfo(ParserTypeInfo info)
        {
            UpdateParserInfo(info);
        }

        public void UpdateParserInfo(ParserTypeInfo info)
        {
            base.ParseTypeCode = info.ParseTypeCode;
            base.ParseTypeName = info.ParseTypeName;
            base.ParseGroupCode = info.ParseGroupCode;
            base.ParseGroupName = info.ParseGroupName;
            base.Plugin = info.Plugin;
            base.Includes = info.Includes;
            base.Excludes = info.Excludes;
            base.Interval = info.Interval;
            base.Prefixs = info.Prefixs;
        }

        [Name(FieldName.No)]
        public string NO
        {
            get;
            set;
        }

        [Name(FieldName.Line)]
        public string Line
        {
            get;
            set;
        }

        [Name(FieldName.AgentIp, false)]
        public string AgentIp
        {
            get;
            set;
        }

        [Name(FieldName.Alias)]
        public string Alias
        {
            get;
            set;
        }

        [Name(FieldName.BcrIp)]
        public string BcrIp
        {
            get;
            set;
        }

        [Name(FieldName.LogFolder)]
        public string LogFolder
        {
            get;
            set;
        }

        public int Index
        {
            get
            {
                if (index > 999)
                    index = 0;

                return ++index;
            }
        }

        public string Path
        {
            get;
            set;
        }

        public bool HeartBeat
        {
            get;
            set;
        }
    }
}
