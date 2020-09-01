using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Messaging;
using IMESAgent.SocketClientEngine;

namespace IMESAgent.GUI
{
    public partial class ucTestMode : UserControl
    {
        private List<Timer> timerLst = new List<Timer>();
        private List<Timer> loadTimes = new List<Timer>();

        private SortedList<string, Queue<string>> allQueues = null;

        private string destFolder = string.Empty;
        private string sourceFolder = string.Empty;

        public const string PrivateQueue = @".\Private$\";

        public ucTestMode()
        {
            InitializeComponent();
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = dialog.SelectedPath;
                    sourceFolder = txtPath.Text;
                }
            }
        }

        int index = 0;
        long totalCount = 0;

        private void Tmr_Tick(object sender, EventArgs e)
        {
            MessageQueue queue = null;
            Timer tm = (Timer)sender;

            string queueName = PrivateQueue + tm.Tag.ToString();

            if (queueName.Contains("imesqueue")) return;
            string source = destFolder + "\\" + tm.Tag.ToString();

            if (MessageQueue.Exists(queueName))
            {
                queue = new MessageQueue(queueName);
            }
            else
            {
                queue = MessageQueue.Create(queueName);
            }

            if (IsEmpty(queue))
            {
                tm.Enabled = false;
                return;
            }

            System.Messaging.Message msg = queue.Receive(new TimeSpan(0, 0, 2));
            msg.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            string path = msg.Body.ToString();

            if (!Directory.Exists(destFolder + "\\" + tm.Tag.ToString()))
                Directory.CreateDirectory(destFolder + "\\" + tm.Tag.ToString());

            string name = path.Substring(path.LastIndexOf("\\") + 1);
            string destPath = destFolder + "\\" + tm.Tag.ToString() + "\\" + name;

            if (File.Exists(destPath))
            {
                destPath = destFolder + "\\" + tm.Tag.ToString() + "\\" + "Duplicate" + index.ToString() + name;
                index++;
            }

            File.Copy(path, destPath);

            totalCount += 1;
            LogToScreen();
        }

        private void btnLoadFiles_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This Operation will delete all data in the MSMQ, are you sure to process?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            DirectoryInfo dInfo = new DirectoryInfo(txtPath.Text);
            DirectoryInfo[] subInfo = dInfo.GetDirectories();
            string[] filters = GetParserFilter();

            long count = 0;
            foreach (DirectoryInfo info in subInfo)
            {
                if (IsZType(info.Name)) continue;

                if (filters != null)
                {
                    if (!filters.Contains(info.Name))
                        continue;
                }

                Timer tm = SetupTimer1(info.Name, 100);
                loadTimes.Add(tm);

                Timer tm2 = SetupTimer2(info.Name, int.Parse(txtInterval.Text.TrimEnd()));
                timerLst.Add(tm2);

                count += info.GetFiles(UserMessage.AllFiles, SearchOption.AllDirectories ).Length;
            }

            txtTotal.Text = count.ToString();
        }

        private void btnBroswer2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtMonitor.Text = dialog.SelectedPath;
                    destFolder = dialog.SelectedPath;
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            foreach (Timer t in timerLst)
            {
                t.Enabled = true;
            }
        }

        private delegate void FlushOutPut();
        private void LogToScreen()
        {
            if (txtTotal.InvokeRequired)
            {
                var flush = new FlushOutPut(LogToScreen);
                this.Invoke(flush);
            }
            else
            {
                txtSent.Text = totalCount.ToString();
            }
        }

        private bool IsZType(string name)
        {
            if (name == "ZZZZ" || name == "Z001" || name == "Z003") return true;

            return false;
        }

        private string[] GetParserFilter()
        {
            if (string.IsNullOrEmpty(txtDataType.Text)) return null;
            else
            {
                string[] st = txtDataType.Text.Split('*');

                return st;
            }
        }

        private Timer SetupTimer1(string parserCode, int interval)
        {
            //int interval = int.Parse(txtInterval.Text.TrimEnd());
            Timer tmr = new Timer();

            tmr.Tag = parserCode;
            tmr.Tick += LoadFileTimer;
            tmr.Interval = interval;
            tmr.Enabled = true;

            return tmr;
        }

        private Timer SetupTimer2(string parserCode, int interval)
        {
            //int interval = int.Parse(txtInterval.Text.TrimEnd());
            Timer tmr = new Timer();

            tmr.Tag = parserCode;
            tmr.Tick += Tmr_Tick;
            tmr.Interval = interval;
            tmr.Enabled = false;

            return tmr;
        }

        private void LoadFileTimer(object sender, EventArgs e)
        {
            MessageQueue queue = null;
            Timer tm = (Timer)sender;
            string queueName = PrivateQueue + tm.Tag.ToString();
            string source = sourceFolder + "\\" + tm.Tag.ToString();

            if (MessageQueue.Exists(queueName))
            {
                queue = new MessageQueue(queueName);
                queue.Purge();
            }
            else
            {
                queue = MessageQueue.Create(queueName);
            }

            DirectoryInfo info = new DirectoryInfo(source);
            FileInfo[] fInfo = info.GetFiles(UserMessage.AllFiles, SearchOption.AllDirectories);

            foreach (FileInfo f in fInfo)
            {
                Enqueue(queue, f.FullName, tm.Tag.ToString());
            }

            tm.Enabled = false;
        }

        public void Enqueue(MessageQueue queue, string fullPath, string parser)
        {
            if (queue != null)
            {
                System.Messaging.Message msg = new System.Messaging.Message();

                msg.Body = fullPath.ToString();
                msg.Label = parser;

                queue.Send(msg);
            }
        }

        public bool IsEmpty(MessageQueue queue)
        {
            bool isEmpty = false;
            try
            {
                if (queue == null) return true;

                queue.Peek(new TimeSpan(0));

                // If an IOTimeout was not thrown, there is a message 
                // in the queue.
                isEmpty = false;

            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
                {
                    isEmpty = true;
                }
            }

            return isEmpty;
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            //string machineName = System.Net.Dns.GetHostName();
            //MessageQueue[] qs = MessageQueue.GetPrivateQueuesByMachine(machineName);

            //foreach (MessageQueue q in qs)
            //{
            //    q.Purge();
            //    MessageQueue.Delete(q.QueueName);
            //}
        }

        private void btnSendQueue_Click(object sender, EventArgs e)
        {
            if (txtTotal.Text.TrimEnd() != txtSent.Text.TrimEnd()) return;

            timerLst = new List<Timer>();
            string machineName = System.Net.Dns.GetHostName();
            MessageQueue[] qs = MessageQueue.GetPrivateQueuesByMachine(machineName);


            long length = 0;
            foreach (MessageQueue q in qs)
            {
                bool b = Contain(q.QueueName);
                bool b1 = q.QueueName.Contains("imesqueue");

                if (b1 == true) continue;

                if (b == true)
                {
                    length += q.GetAllMessages().Length;
                    Timer tm2 = SetupTimer2(GetParserName(q.QueueName), int.Parse(txtInterval.Text.TrimEnd()));
                    timerLst.Add(tm2);
                }
            }

            txtTotal.Text = length.ToString();

            foreach (Timer t in timerLst)
            {
                t.Start();
            }
        }

        private string GetParserName(string queueName)
        {
            return queueName.Substring(queueName.LastIndexOf('\\') + 1);
        }

        private bool Contain(string queueName)
        {
            string[] filters = GetParserFilter();

            if (filters == null) return true;

            foreach (string s in filters)
            {
                if (queueName.ToUpper().Contains(s.ToUpper())) return true;
            }

            return false;
        }
    }
}
