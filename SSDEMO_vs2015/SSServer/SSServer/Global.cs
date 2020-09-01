
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Receive.udp
{
    public static  class VarGlobal
    {
        public static string MacType = "";
        public static string SpecUpper = "";
        public static string SpecLower = "";
        public static string Round = "";
        public static string RoundUp = "";
        public static string RoundLow = "";
        public static string ModelCode = "";
        public static string ProdSn = "";
        public static string TestResult = "";

        //组织信息
        public static string CorpCode = "C660";
        public static string GbmCode = "I";
        public static string ProdGrpCode = "IP";
        public static string FctCode = "";
        public static string PlantCode = "";
        public static string LineName = "";
        public static string ProcName = "";
        public static string PartName = "";
        public static string CellName = "";
        public static string PartCode = "";
        public static string LineCode = "";
        public static string ProcCode = "";
        public static string CellCode = "";
        public static string ErpWcCode = "";
        //其他
        public static string ConnKey = "b0yvCio0n/kWgstgPqpLEg==B+F3SEltBa0U+m6zMgzxxA==";    //数据库连接区分
        public static string SysType = "";    //TEST / PRE  / PROD 
        public static string Language = "";
        public static string ScreenId = "";
        public static string FormTitle = "";
        public static string FormSetting = "";
        public static DataTable MSG = new DataTable();  // NG CODE LIST
        public static string WebApiUrl = "";
        public static string SqlDbConstring = "";
        public static string AccessDbConstring = "";
        public static string strDbType = "";
        //用户信息
        public static string UserID = "";
        public static string UserName = "";
        public static string EngUserName = "";
        public static string EMail = "";
        public static string EmpNo = "";
        public static string DeptCode = "";
        public static string DeptName = "";
        public static string EngDeptName = "";
        public static string FinalAccDateTime = "";
        public static string FinalAccIp = "";
        public static string VendorCode = "";
        public static string VendorCode2 = "";
        public static string MfgGrpCode = "";
        public static string MfgTeamCode = "";
        public static string PcIP = "";
        // Command
        public static string StartCmd = "Srart";
        public static string QueryForDataCmd = "";
        public static string DataParam1 = "P06";
        public static string DataParam2 = "P01";
        public static byte[] CmdReadStatus = new byte[3]{82,13,10};
        public static int DataStatusLen = 3;
        public static byte[] CmdReadData = new byte[5] {80,48,54,13,10};
        public static int DataReadDataLen = 5;
        public static string skinName = string.Empty;
        // TOKEN
        public static bool HasToken = false;  
        public static string TokenKey = string.Empty;
        public static Guid SignToken;
        public static DateTime TokenExpireTime;
        public static string LogID = string.Empty;
        public static string DeviceIP = "";
        public static string DevicePort = "";
        public static string exeVersion = "";
        public static string ServerUrl = "";
        public static string SendLimit = "20";
        public static string SendSingleType = "SendMultiIDInOne";
        public static string SendEverySecsWork = "300";
        public static string SendEverySecsOff = "400";
        public static string SendEveryTime = "15";
        public static string NoDataSecsSetOffLine = "1200";
        public static string NoDataSecsSetOffLineCount = "3";

        /// <summary>
        /// 此窗体主要是提示给用户错误信息 或者一些友情提示信息等。支持多语言翻译。
        /// </summary>
        /// <param name="messageboxtype"></param>
        /// <param name="ngcode"></param>
        /// <param name="content"></param>

        /// <summary>
        /// 此弹窗主要是用户操作重要数据前，提示： 是否进行下一步动作用 ，支持多语言翻译
        /// </summary>
        /// <param name="messageboxtype"></param>
        /// <param name="ngcode"></param>
        /// <param name="content"></param>
        /// <returns></returns>
    }
}
