using System;

namespace IMESAgent.SocketClientEngine
{
    public class TransferErrorEventArgs : EventArgs
    {
        public TransferErrorEventArgs(Exception ex, bool showException = false)
        {
            this.Exception = ex;
            this.ShowExcetpion = showException;
        }

        public TransferErrorEventArgs(string errorMessage, bool showException = false)
        {
            this.Exception = new Exception(errorMessage);
            this.ShowExcetpion = showException ;
        }

        public Exception Exception
        {
            get;
            private set;
        }

        public bool ShowExcetpion
        {
            get;
            private set;
        }
    }
}
