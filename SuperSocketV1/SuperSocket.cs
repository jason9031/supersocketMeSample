using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SuperSocketV1
{
    public partial class SuperSocket : Form
    {
        JSocketServerApp jSocketServerApp_instance ;
        public SuperSocket()
        {
            InitializeComponent();
        }

        private void SuperSocket_Load(object sender, EventArgs e)
        {

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
             jSocketServerApp_instance = new JSocketServerApp();

            if (!jSocketServerApp_instance.Setup(7007))
            {
                writeLogToText(" Setup 7007 Error");
            }

            if (!jSocketServerApp_instance.Start())
            {
                writeLogToText(" Start 7007 Error ");
            }

            writeLogToText( " Start OK ");

        }

        private void writeLogToText(string strlog = "")
        {
            richLog.Text = richLog.Text +strlog+ "  \r\n";
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            jSocketServerApp_instance.Stop();
        }
    }
}
