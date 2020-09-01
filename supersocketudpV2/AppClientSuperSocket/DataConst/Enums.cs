namespace IMESAgent.SocketClientEngine
{
    public enum CommonActions
    {
        None = 0,
        Handshake = 1,
        CheckFileExist = 2,
        TransferFileInfo = 3,
        TransferFileBody = 4,
        TransferComplete = 5,
        ImageInfo = 6,
        Cancel = 7,
        Delete = 8,
        Rename = 9,
        HeartBeat = 10
    }
    public enum PlugType
    {
        NormalPlugin = 1,
        ClonePlugin = 2,
        SubdirPlugin = 3
    }

    public enum FileStatus
    {
        Occupied = 0,
        Idle = 1,
        NoFound = 2,
        None = 3
    }

    public enum AgentStatus
    {
        NONE = -1,
        ONLINE = 0,
        OFFLINE = 1,
        ERROR = 2
    }

    public enum WatcherType
    {
        NONE = -1,
        HeartBeat = 0,
        AgentStatus = 1
    }
}
