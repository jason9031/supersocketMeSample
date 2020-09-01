using IMESAgent.SocketClientEngine;
using System;
using System.Windows.Forms;

namespace IMESAgent.GUI
{
    public partial class frmPassword : Form
    {
        //private const string Password = "123";

        public frmPassword()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            btnCancel.Click += btnCancel_Click;
            btnOk.Click += btnOk_Click;
            this.FormClosed += frmPassword_FormClosed;
        }

        private void frmPassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Text) && ClientHelper.Instance.ValidatePassword(txtPassword.Text))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                PopupWindow.ShowDialog(ExceptionMessage.WrongPassword);
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
