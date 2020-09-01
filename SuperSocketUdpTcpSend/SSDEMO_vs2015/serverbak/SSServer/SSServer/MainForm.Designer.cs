namespace SSServer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_sendmsg = new System.Windows.Forms.Button();
            this.txt_msg = new System.Windows.Forms.TextBox();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btnGetLocation = new System.Windows.Forms.Button();
            this.txtAll = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_sendmsg);
            this.groupBox1.Controls.Add(this.txt_msg);
            this.groupBox1.Controls.Add(this.btn_clear);
            this.groupBox1.Controls.Add(this.btnGetLocation);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(7, 594);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1118, 139);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作";
            // 
            // btn_sendmsg
            // 
            this.btn_sendmsg.Location = new System.Drawing.Point(402, 28);
            this.btn_sendmsg.Margin = new System.Windows.Forms.Padding(4);
            this.btn_sendmsg.Name = "btn_sendmsg";
            this.btn_sendmsg.Size = new System.Drawing.Size(100, 29);
            this.btn_sendmsg.TabIndex = 19;
            this.btn_sendmsg.Text = "1发送文本";
            this.btn_sendmsg.UseVisualStyleBackColor = true;
            this.btn_sendmsg.Click += new System.EventHandler(this.btn_sendmsg_Click);
            // 
            // txt_msg
            // 
            this.txt_msg.Location = new System.Drawing.Point(157, 29);
            this.txt_msg.Margin = new System.Windows.Forms.Padding(4);
            this.txt_msg.Name = "txt_msg";
            this.txt_msg.Size = new System.Drawing.Size(241, 25);
            this.txt_msg.TabIndex = 18;
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(1010, 98);
            this.btn_clear.Margin = new System.Windows.Forms.Padding(4);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(100, 29);
            this.btn_clear.TabIndex = 24;
            this.btn_clear.Text = "清空文本框";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btnGetLocation
            // 
            this.btnGetLocation.Location = new System.Drawing.Point(39, 25);
            this.btnGetLocation.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetLocation.Name = "btnGetLocation";
            this.btnGetLocation.Size = new System.Drawing.Size(100, 29);
            this.btnGetLocation.TabIndex = 0;
            this.btnGetLocation.Text = "查询";
            this.btnGetLocation.UseVisualStyleBackColor = true;
            // 
            // txtAll
            // 
            this.txtAll.BackColor = System.Drawing.Color.White;
            this.txtAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAll.Location = new System.Drawing.Point(7, 6);
            this.txtAll.Margin = new System.Windows.Forms.Padding(4);
            this.txtAll.Multiline = true;
            this.txtAll.Name = "txtAll";
            this.txtAll.ReadOnly = true;
            this.txtAll.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAll.Size = new System.Drawing.Size(1118, 588);
            this.txtAll.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 739);
            this.Controls.Add(this.txtAll);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Text = "Socket服务";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAll;
        private System.Windows.Forms.Button btnGetLocation;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_sendmsg;
        private System.Windows.Forms.TextBox txt_msg;
    }
}