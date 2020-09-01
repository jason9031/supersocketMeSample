namespace Receive.udp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkCntLog = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.chkUpdateConfigLog = new System.Windows.Forms.CheckBox();
            this.btnSendmsg2 = new System.Windows.Forms.Button();
            this.chkUpdateApiLog = new System.Windows.Forms.CheckBox();
            this.chkUpGrid = new System.Windows.Forms.CheckBox();
            this.txt_msg2 = new System.Windows.Forms.TextBox();
            this.chkTiming = new System.Windows.Forms.CheckBox();
            this.btnSendmsg1 = new System.Windows.Forms.Button();
            this.txt_msg1 = new System.Windows.Forms.TextBox();
            this.btnUDPServer = new System.Windows.Forms.Button();
            this.btnTcpServer = new System.Windows.Forms.Button();
            this.btnSendmsg = new System.Windows.Forms.Button();
            this.txt_msg = new System.Windows.Forms.TextBox();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btnGetLocation = new System.Windows.Forms.Button();
            this.txtAll = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.chkCntLog);
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.chkUpdateConfigLog);
            this.groupBox1.Controls.Add(this.btnSendmsg2);
            this.groupBox1.Controls.Add(this.chkUpdateApiLog);
            this.groupBox1.Controls.Add(this.chkUpGrid);
            this.groupBox1.Controls.Add(this.txt_msg2);
            this.groupBox1.Controls.Add(this.chkTiming);
            this.groupBox1.Controls.Add(this.btnSendmsg1);
            this.groupBox1.Controls.Add(this.txt_msg1);
            this.groupBox1.Controls.Add(this.btnUDPServer);
            this.groupBox1.Controls.Add(this.btnTcpServer);
            this.groupBox1.Controls.Add(this.btnSendmsg);
            this.groupBox1.Controls.Add(this.txt_msg);
            this.groupBox1.Controls.Add(this.btn_clear);
            this.groupBox1.Controls.Add(this.btnGetLocation);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(5, 227);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(975, 373);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // chkCntLog
            // 
            this.chkCntLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCntLog.AutoSize = true;
            this.chkCntLog.Location = new System.Drawing.Point(852, 354);
            this.chkCntLog.Name = "chkCntLog";
            this.chkCntLog.Size = new System.Drawing.Size(104, 17);
            this.chkCntLog.TabIndex = 12;
            this.chkCntLog.Text = "CounterDataLog";
            this.chkCntLog.UseVisualStyleBackColor = true;
            this.chkCntLog.Visible = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(6, 105);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(829, 268);
            this.richTextBox1.TabIndex = 31;
            this.richTextBox1.Text = "";
            // 
            // chkUpdateConfigLog
            // 
            this.chkUpdateConfigLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUpdateConfigLog.AutoSize = true;
            this.chkUpdateConfigLog.Checked = true;
            this.chkUpdateConfigLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateConfigLog.Location = new System.Drawing.Point(852, 334);
            this.chkUpdateConfigLog.Name = "chkUpdateConfigLog";
            this.chkUpdateConfigLog.Size = new System.Drawing.Size(109, 17);
            this.chkUpdateConfigLog.TabIndex = 11;
            this.chkUpdateConfigLog.Text = "UpdateConfigLog";
            this.chkUpdateConfigLog.UseVisualStyleBackColor = true;
            this.chkUpdateConfigLog.Visible = false;
            // 
            // btnSendmsg2
            // 
            this.btnSendmsg2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendmsg2.Location = new System.Drawing.Point(881, 73);
            this.btnSendmsg2.Name = "btnSendmsg2";
            this.btnSendmsg2.Size = new System.Drawing.Size(75, 25);
            this.btnSendmsg2.TabIndex = 30;
            this.btnSendmsg2.Text = "Send";
            this.btnSendmsg2.UseVisualStyleBackColor = true;
            this.btnSendmsg2.Visible = false;
            // 
            // chkUpdateApiLog
            // 
            this.chkUpdateApiLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUpdateApiLog.AutoSize = true;
            this.chkUpdateApiLog.Location = new System.Drawing.Point(852, 314);
            this.chkUpdateApiLog.Name = "chkUpdateApiLog";
            this.chkUpdateApiLog.Size = new System.Drawing.Size(94, 17);
            this.chkUpdateApiLog.TabIndex = 10;
            this.chkUpdateApiLog.Text = "UpdateApiLog";
            this.chkUpdateApiLog.UseVisualStyleBackColor = true;
            this.chkUpdateApiLog.Visible = false;
            // 
            // chkUpGrid
            // 
            this.chkUpGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUpGrid.AutoSize = true;
            this.chkUpGrid.Location = new System.Drawing.Point(852, 291);
            this.chkUpGrid.Name = "chkUpGrid";
            this.chkUpGrid.Size = new System.Drawing.Size(83, 17);
            this.chkUpGrid.TabIndex = 9;
            this.chkUpGrid.Text = "Update Grid";
            this.chkUpGrid.UseVisualStyleBackColor = true;
            this.chkUpGrid.Visible = false;
            // 
            // txt_msg2
            // 
            this.txt_msg2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_msg2.Location = new System.Drawing.Point(559, 74);
            this.txt_msg2.Name = "txt_msg2";
            this.txt_msg2.Size = new System.Drawing.Size(317, 20);
            this.txt_msg2.TabIndex = 29;
            this.txt_msg2.Text = "02540BF367244304A9FD088D0000010001F55207FFA203";
            // 
            // chkTiming
            // 
            this.chkTiming.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTiming.AutoSize = true;
            this.chkTiming.Checked = true;
            this.chkTiming.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTiming.Location = new System.Drawing.Point(852, 268);
            this.chkTiming.Name = "chkTiming";
            this.chkTiming.Size = new System.Drawing.Size(57, 17);
            this.chkTiming.TabIndex = 8;
            this.chkTiming.Text = "Timing";
            this.chkTiming.UseVisualStyleBackColor = true;
            this.chkTiming.Visible = false;
            // 
            // btnSendmsg1
            // 
            this.btnSendmsg1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendmsg1.Location = new System.Drawing.Point(881, 45);
            this.btnSendmsg1.Name = "btnSendmsg1";
            this.btnSendmsg1.Size = new System.Drawing.Size(75, 25);
            this.btnSendmsg1.TabIndex = 28;
            this.btnSendmsg1.Text = "Send";
            this.btnSendmsg1.UseVisualStyleBackColor = true;
            this.btnSendmsg1.Click += new System.EventHandler(this.btnSendmsg1_Click);
            // 
            // txt_msg1
            // 
            this.txt_msg1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_msg1.Location = new System.Drawing.Point(559, 47);
            this.txt_msg1.Name = "txt_msg1";
            this.txt_msg1.Size = new System.Drawing.Size(317, 20);
            this.txt_msg1.TabIndex = 27;
            this.txt_msg1.Text = "02540BF236794304A9FD09CF0000010004129C6AFF3F03";
            // 
            // btnUDPServer
            // 
            this.btnUDPServer.Location = new System.Drawing.Point(28, 15);
            this.btnUDPServer.Name = "btnUDPServer";
            this.btnUDPServer.Size = new System.Drawing.Size(207, 79);
            this.btnUDPServer.TabIndex = 26;
            this.btnUDPServer.Text = "UDP Start";
            this.btnUDPServer.UseVisualStyleBackColor = true;
            this.btnUDPServer.Click += new System.EventHandler(this.BtnUDPServer_Click);
            // 
            // btnTcpServer
            // 
            this.btnTcpServer.Location = new System.Drawing.Point(850, 168);
            this.btnTcpServer.Name = "btnTcpServer";
            this.btnTcpServer.Size = new System.Drawing.Size(88, 34);
            this.btnTcpServer.TabIndex = 25;
            this.btnTcpServer.Text = "TCP Server";
            this.btnTcpServer.UseVisualStyleBackColor = true;
            this.btnTcpServer.Visible = false;
            // 
            // btnSendmsg
            // 
            this.btnSendmsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendmsg.Location = new System.Drawing.Point(881, 17);
            this.btnSendmsg.Name = "btnSendmsg";
            this.btnSendmsg.Size = new System.Drawing.Size(75, 25);
            this.btnSendmsg.TabIndex = 19;
            this.btnSendmsg.Text = "Send";
            this.btnSendmsg.UseVisualStyleBackColor = true;
            this.btnSendmsg.Visible = false;
            this.btnSendmsg.Click += new System.EventHandler(this.Btn_sendmsg_Click);
            // 
            // txt_msg
            // 
            this.txt_msg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_msg.Location = new System.Drawing.Point(559, 20);
            this.txt_msg.Name = "txt_msg";
            this.txt_msg.Size = new System.Drawing.Size(317, 20);
            this.txt_msg.TabIndex = 18;
            this.txt_msg.Text = "02540BF222E44304A9FD03BC00000100064FFE1D0BFF03";
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(850, 208);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(88, 34);
            this.btn_clear.TabIndex = 24;
            this.btn_clear.Text = "Empty All";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Visible = false;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btnGetLocation
            // 
            this.btnGetLocation.Location = new System.Drawing.Point(850, 125);
            this.btnGetLocation.Name = "btnGetLocation";
            this.btnGetLocation.Size = new System.Drawing.Size(88, 37);
            this.btnGetLocation.TabIndex = 0;
            this.btnGetLocation.Text = "Query";
            this.btnGetLocation.UseVisualStyleBackColor = true;
            this.btnGetLocation.Visible = false;
            // 
            // txtAll
            // 
            this.txtAll.BackColor = System.Drawing.Color.White;
            this.txtAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAll.Location = new System.Drawing.Point(5, 5);
            this.txtAll.MaxLength = 50;
            this.txtAll.Multiline = true;
            this.txtAll.Name = "txtAll";
            this.txtAll.ReadOnly = true;
            this.txtAll.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAll.Size = new System.Drawing.Size(975, 222);
            this.txtAll.TabIndex = 0;
            this.txtAll.TextChanged += new System.EventHandler(this.TxtAll_TextChanged);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(281, 15);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(247, 79);
            this.btnStop.TabIndex = 27;
            this.btnStop.Text = "UDP Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 605);
            this.Controls.Add(this.txtAll);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "SuperSocket Server";
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
        private System.Windows.Forms.Button btnSendmsg;
        private System.Windows.Forms.TextBox txt_msg;
        private System.Windows.Forms.Button btnUDPServer;
        private System.Windows.Forms.Button btnTcpServer;
        private System.Windows.Forms.Button btnSendmsg2;
        private System.Windows.Forms.TextBox txt_msg2;
        private System.Windows.Forms.Button btnSendmsg1;
        private System.Windows.Forms.TextBox txt_msg1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox chkCntLog;
        private System.Windows.Forms.CheckBox chkUpdateConfigLog;
        private System.Windows.Forms.CheckBox chkUpdateApiLog;
        private System.Windows.Forms.CheckBox chkUpGrid;
        private System.Windows.Forms.CheckBox chkTiming;
        private System.Windows.Forms.Button btnStop;
    }
}