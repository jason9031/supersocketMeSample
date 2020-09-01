using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Receive.udp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new MainForm());


            bool running = false;

            try
            {
                // singal program
                Mutex mx = new Mutex(true, Application.ProductName, out running);


                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "MultiOpen"))
                {
                    if (!running)
                    {
                        //ShowDialog(ExceptionMessage.SingleInstance, UserMessage.Error);
                        MessageBox.Show(Application.ProductName + "不允许打开多个，请从资源管理器关闭所有任务后再运行此程序");
                        return;
                    }
                }

                var agentMain = new MainForm();
                Application.Run(agentMain);
            }
            catch (AggregateException ex1)
            {
                MessageBox.Show("打开错误程序将退出" + ex1.ToString());
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开错误程序将退出" + ex.ToString());
                Application.Exit();
            }
            finally
            {
                //RegistryHelper.SetControlPanelPermission("0");
            }

        }
      
    }
}
