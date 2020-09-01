using System;
using System.Net;

namespace IMESAgent.SocketClientEngine
{
    public class AppEvents
    {
        #region Singleton
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
        #endregion

        public delegate void UpdateScreenEventHandler(string msg);
        public delegate bool FileTransferEventHandler<EventArgs>(EventArgs arg);
        public delegate void TransferErrorEventHandler<TransferErrorEventArgs>(TransferErrorEventArgs arg);
        public delegate IPAddress GetIpAddressHandler();
        public delegate string GetBCRIPHandler();
        public delegate void FlsuhActivedDatetimeEventHandler(string datetime);
        public delegate void ShowDialogEventHandler(string msg, string title);
        public delegate void UpdateConnectionStatusEventHandler(AgentStatus status);
        public delegate void StopAllTasksEventHandler();
        public delegate void FlushBlockingCountEventHandler(string count);

        public UpdateScreenEventHandler OnUpdateScreen;
        public FileTransferEventHandler<EventArgs> OnFileExist;
        public TransferErrorEventHandler<TransferErrorEventArgs> OnErrorOccurred;
        public GetIpAddressHandler OnGetIpAddress;
        public GetBCRIPHandler OnGetBcrIp;
        public FlsuhActivedDatetimeEventHandler OnFlsuhActivedDatetime;
        public ShowDialogEventHandler OnShowDialog;
        public UpdateConnectionStatusEventHandler OnUpdateConnectionStatus;
        public StopAllTasksEventHandler OnStopAllStasks;
        public FlushBlockingCountEventHandler OnFlushBlockingCount;

        public event UpdateScreenEventHandler UpdateScreenEvent
        {
            add
            {
                OnUpdateScreen = (UpdateScreenEventHandler)System.Delegate.Combine(OnUpdateScreen, value);
            }
            remove
            {
                OnUpdateScreen = (UpdateScreenEventHandler)System.Delegate.Remove(OnUpdateScreen, value);
            }
        }

        public event FileTransferEventHandler<EventArgs> FileExistEvent
        {
            add
            {
                OnFileExist = (FileTransferEventHandler<EventArgs>)System.Delegate.Combine(OnFileExist, value);
            }
            remove
            {
                OnFileExist = (FileTransferEventHandler<EventArgs>)System.Delegate.Remove(OnFileExist, value);
            }
        }

        public event TransferErrorEventHandler<TransferErrorEventArgs> ErrorOccurredEvent
        {
            add
            {
                OnErrorOccurred = (TransferErrorEventHandler<TransferErrorEventArgs>)System.Delegate.Combine(OnErrorOccurred, value);
            }
            remove
            {
                OnErrorOccurred = (TransferErrorEventHandler<TransferErrorEventArgs>)System.Delegate.Remove(OnErrorOccurred, value);
            }
        }

        public event GetIpAddressHandler GetIpAddressEvent
        {
            add
            {
                OnGetIpAddress = (GetIpAddressHandler)System.Delegate.Combine(OnGetIpAddress, value);
            }
            remove
            {
                OnGetIpAddress = (GetIpAddressHandler)System.Delegate.Remove(OnGetIpAddress, value);
            }
        }

        public event GetBCRIPHandler GetBcrIpEvent
        {
            add
            {
                OnGetBcrIp = (GetBCRIPHandler)Delegate.Combine(OnGetBcrIp, value);
            }
            remove
            {
                OnGetBcrIp = (GetBCRIPHandler)Delegate.Remove(OnGetBcrIp, value);
            }
        }
        
        public event FlsuhActivedDatetimeEventHandler FlushActivedDatetimeEvent
        {
            add
            {
                OnFlsuhActivedDatetime = (FlsuhActivedDatetimeEventHandler)Delegate.Combine(OnFlsuhActivedDatetime, value);
            }
            remove
            {
                OnFlsuhActivedDatetime = (FlsuhActivedDatetimeEventHandler)Delegate.Remove(OnFlsuhActivedDatetime, value);
            }
        }

        public event ShowDialogEventHandler ShowDialogEvent
        {
            add
            {
                OnShowDialog = (ShowDialogEventHandler)Delegate.Combine(OnShowDialog, value);
            }
            remove
            {
                OnShowDialog = (ShowDialogEventHandler)Delegate.Remove(OnShowDialog, value);
            }
        }

        public event UpdateConnectionStatusEventHandler UpdateConnectionStatusEvent
        {
            add
            {
                OnUpdateConnectionStatus = (UpdateConnectionStatusEventHandler)Delegate.Combine(OnUpdateConnectionStatus, value);
            }
            remove
            {
                OnUpdateConnectionStatus = (UpdateConnectionStatusEventHandler)Delegate.Remove(OnUpdateConnectionStatus, value);
            }
        }

        public event StopAllTasksEventHandler StopAllTasksEvent
        {
            add
            {
                OnStopAllStasks = (StopAllTasksEventHandler)Delegate.Combine(OnStopAllStasks, value);
            }
            remove
            {
                OnStopAllStasks = (StopAllTasksEventHandler)Delegate.Remove(OnStopAllStasks, value);
            }
        }

        public event FlushBlockingCountEventHandler FlushBlockingCountEvent
        {
            add
            {
                OnFlushBlockingCount = (FlushBlockingCountEventHandler)Delegate.Combine(OnFlushBlockingCount, value);
            }
            remove
            {
                OnFlushBlockingCount = (FlushBlockingCountEventHandler)Delegate.Remove(OnFlushBlockingCount, value);
            }
        }
    }
}
