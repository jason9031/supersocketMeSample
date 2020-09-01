using CounterTMT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CounterTMT.Enum;

namespace WinFormTaskTest
{
    public partial class WinFormTaskTest : Form
    {
        /// 定义有参无返回值委托   委托 sunjie1
        private delegate void RefreshAgentStatus(AgentStatus status);
        private delegate void FlushOutPut(string msg);

        public WinFormTaskTest()
        {
            InitializeComponent();
            InitializeEvents();
        }
        private void InitializeEvents()
        {
            AppEvents.Instance.UpdateConnectionStatusEvent += UpdateStatus;
            AppEvents.Instance.UpdateScreenEvent += LogToScreen;
        }

        public void UpdateStatus(AgentStatus status)
        {
            if (lbStatus.InvokeRequired)
            {
                var refresh = new RefreshAgentStatus(UpdateStatus);
                this.Invoke(refresh, new object[] { status });
            }
            else
            {
                if (status == AgentStatus.ONLINE)
                {
                    if (!string.Equals(lbStatus.Text, status.ToString()))
                    {
                        lbStatus.Text = status.ToString();
                        lbStatus.BackColor = System.Drawing.Color.LimeGreen;
                        lbStatus.ForeColor = System.Drawing.Color.Black;
                    }
                }
                else
                {
                    if (status == AgentStatus.ERROR || status == AgentStatus.OFFLINE)
                    {
                        if (!string.Equals(lbStatus.Text, status.ToString()))
                        {
                            lbStatus.Text = status.ToString();
                            lbStatus.BackColor = System.Drawing.Color.Red;
                            lbStatus.ForeColor = System.Drawing.Color.White;
                        }
                    }
                    else
                    {
                        //lbStatus.Text = status.ToString();
                        //lbStatus.BackColor = System.Drawing.Color.LimeGreen;
                        //lbStatus.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
        }
        private void LogToScreen(string st)
        {
            if (this.richLog.InvokeRequired)
            {
                var flush = new FlushOutPut(LogToScreen);
                this.Invoke(flush, new object[] { st });
            }
            else
            {
                var msg = string.Format("{0}   {1}", DateTime.Now.ToString(Settings.NormalDateTimeFormatDash), st);

                if (richLog.Text.Length >= 500)
                {
                    var index = richLog.Text.Length;
                    richLog.Text = (st + "\r\n" + richLog.Text).Substring(0, 500);
                }
                else
                {
                    richLog.Text = (st + "\r\n" + richLog.Text);
                }
            }
        }
        private void BtnStart_Click(object sender, EventArgs e)
        {

        }
        private void writeLogToText(string strlog = "")
        {
            AppEvents.Instance.OnUpdateScreenRun(strlog);
            //richLog.Text = richLog.Text + strlog + "  \r\n";
        }

        private void WinFormTaskTest_Load(object sender, EventArgs e)
        {

        }

        private void BtnStartTask_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                writeLogToText("任务开始工作……");
                //模拟工作过程
                Thread.Sleep(5000);
            });
            t.Start();
            t.ContinueWith((task) =>
            {
                string strout = string.Format("IsCanceled={0}\tIsCompleted={1}\tIsFaulted={2}", task.IsCanceled, task.IsCompleted, task.IsFaulted);
                writeLogToText("任务完成，完成时候的状态为：");
                writeLogToText(strout);
            });
            writeLogToText("Task 任务完成");
        }

        private void BtnTaskNoReturn_Click(object sender, EventArgs e)
        {
            //（一）无返回值的方式
            //方式1:
            //　　var t1 = new Task(() => TaskMethod("Task 1"));
            //t1.Start();
            //Task.WaitAll(t1);//等待所有任务结束 
            //注: 任务的状态:
            //Start之前为: Created
            //Start之后为:WaitingToRun
            //方式2:
            //　　Task.Run(() => TaskMethod("Task 2"));
            //方式3:
            //复制代码
            //Task.Factory.StartNew(() => TaskMethod("Task 3")); 直接异步的方法
            ////或者
            //var t3 = Task.Factory.StartNew(() => TaskMethod("Task 3"));
            //Task.WaitAll(t3);//等待所有任务结束
            ////任务的状态:
            //Start之前为: Running
            //Start之后为:Running
        }
        int icount = 0;
        private void BtnTaskWithReturn_Click(object sender, EventArgs e)
        {
            Task<Int32> task = Task.Factory.StartNew(() => fun("Task" + icount++.ToString(), 9));

        }
        private Int32 fun(string s, int m)
        {
            writeLogToText("任务 StartNew" + s);
            return 0;
        }
    }
}
