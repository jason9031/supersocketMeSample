using IMESAgent.SocketClientEngine;
using System.Windows.Forms;

namespace IMESAgent.GUI
{
    public class PopupWindow
    {
        public static DialogResult ShowDialog(string msg, string title)
        {
            MessageBoxIcon icon = MessageBoxIcon.None;
            MessageBoxButtons btn = MessageBoxButtons.OK;

            if (UserMessage.Warning == title)
                icon = MessageBoxIcon.Warning;
            else if (UserMessage.Error == title)
                icon = MessageBoxIcon.Error;
            else if (UserMessage.Question == title)
            {
                icon = MessageBoxIcon.Question;
                btn = MessageBoxButtons.YesNo;
            }
            else if (UserMessage.Information == title)
                icon = MessageBoxIcon.Information;

            return MessageBox.Show(msg, title, btn, icon);
        }

        public static DialogResult ShowDialog(string msg)
        {
            return MessageBox.Show(msg, UserMessage.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
