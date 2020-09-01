using Microsoft.Win32;

namespace IMESAgent.SocketClientEngine
{
    public class RegistryHelper
    {
        public static void SetupTcpTimeout(string value)
        {
            SetRegistry(Constant.TcpTimeDelayPath, Constant.TcpTimeDelay, value, Registry.LocalMachine, RegistryValueKind.DWord);
        }

        public static void SetDatetimeSynchronization(string server, string type, string interval)
        {
            SetRegistry(Constant.NtpPath, Constant.NtpServer, server, Registry.LocalMachine, RegistryValueKind.String);
            SetRegistry(Constant.NtpPath, Constant.NtpType, type, Registry.LocalMachine, RegistryValueKind.String);
            SetRegistry(Constant.PollingIntervalPath, Constant.PollingInterval, interval, Registry.LocalMachine, RegistryValueKind.DWord);
        }

        public static void SetControlPanelPermission(string permission)
        {
            SetRegistry(Constant.ControlPanelPath, Constant.ControlPanel, permission, Registry.LocalMachine, RegistryValueKind.DWord);
        }

        private static void SetRegistry(string keyPath, string itemName, string value, RegistryKey key, RegistryValueKind kind)
        {
            var subKey = key.OpenSubKey(keyPath, true);
            var subValue = subKey.GetValue(itemName);

            if (subValue == null || !string.Equals(subValue.ToString(), value))
            {
                subKey.SetValue(itemName, value, kind);
            }
        }
    }
}
