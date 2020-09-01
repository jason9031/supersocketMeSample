namespace TCPUDPClient
{
    partial class SSClient
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SSClient));
            this.btn_clear = new System.Windows.Forms.Button();
            this.btnConn = new System.Windows.Forms.Button();
            this.txtAll = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClearPort = new System.Windows.Forms.Button();
            this.txtClearPort = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSendUdp = new System.Windows.Forms.Button();
            this.txtUDPIP = new System.Windows.Forms.TextBox();
            this.btnUDPPath = new System.Windows.Forms.Button();
            this.btnUDPConnect = new System.Windows.Forms.Button();
            this.btnUDPFile = new System.Windows.Forms.Button();
            this.btnUDPMsg = new System.Windows.Forms.TextBox();
            this.txtUDPPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUDPPort = new System.Windows.Forms.TextBox();
            this.btnUDPTxt = new System.Windows.Forms.Button();
            this.btnUDPClear = new System.Windows.Forms.Button();
            this.chkUDPHeart = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSendTCP = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnSendPathAllFiles = new System.Windows.Forms.Button();
            this.btnSendAllPath = new System.Windows.Forms.Button();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnclear = new System.Windows.Forms.Button();
            this.cbHeart = new System.Windows.Forms.CheckBox();
            this.TimerTcp = new System.Windows.Forms.Timer(this.components);
            this.TimerUdp = new System.Windows.Forms.Timer(this.components);
            this.txtNickName = new System.Windows.Forms.TextBox();
            this.Nick = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.Label();
            this.txtIDType = new System.Windows.Forms.TextBox();
            this.Param4 = new System.Windows.Forms.Label();
            this.txtParam4 = new System.Windows.Forms.TextBox();
            this.Setting = new System.Windows.Forms.Label();
            this.txtSetting3 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSendValue = new System.Windows.Forms.Button();
            this.txtDouble = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(931, 127);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 25);
            this.btn_clear.TabIndex = 22;
            this.btn_clear.Text = "清空文本框";
            this.btn_clear.UseVisualStyleBackColor = true;
            // 
            // btnConn
            // 
            this.btnConn.Location = new System.Drawing.Point(398, 19);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(75, 25);
            this.btnConn.TabIndex = 2;
            this.btnConn.Text = "连接";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // txtAll
            // 
            this.txtAll.BackColor = System.Drawing.Color.White;
            this.txtAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAll.Location = new System.Drawing.Point(0, 0);
            this.txtAll.Multiline = true;
            this.txtAll.Name = "txtAll";
            this.txtAll.ReadOnly = true;
            this.txtAll.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAll.Size = new System.Drawing.Size(758, 154);
            this.txtAll.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btn_clear);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 154);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(758, 433);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作";
            // 
            // btnClearPort
            // 
            this.btnClearPort.Location = new System.Drawing.Point(554, 42);
            this.btnClearPort.Name = "btnClearPort";
            this.btnClearPort.Size = new System.Drawing.Size(75, 25);
            this.btnClearPort.TabIndex = 46;
            this.btnClearPort.Text = "清空端口";
            this.btnClearPort.UseVisualStyleBackColor = true;
            this.btnClearPort.Click += new System.EventHandler(this.BtnClearPort_Click);
            // 
            // txtClearPort
            // 
            this.txtClearPort.Location = new System.Drawing.Point(554, 14);
            this.txtClearPort.MaxLength = 15;
            this.txtClearPort.Name = "txtClearPort";
            this.txtClearPort.Size = new System.Drawing.Size(75, 20);
            this.txtClearPort.TabIndex = 45;
            this.txtClearPort.Text = "9006";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSendUdp);
            this.groupBox3.Controls.Add(this.txtUDPIP);
            this.groupBox3.Controls.Add(this.btnUDPPath);
            this.groupBox3.Controls.Add(this.btnUDPConnect);
            this.groupBox3.Controls.Add(this.btnUDPFile);
            this.groupBox3.Controls.Add(this.btnUDPMsg);
            this.groupBox3.Controls.Add(this.txtUDPPath);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtUDPPort);
            this.groupBox3.Controls.Add(this.btnUDPTxt);
            this.groupBox3.Controls.Add(this.btnUDPClear);
            this.groupBox3.Controls.Add(this.chkUDPHeart);
            this.groupBox3.Location = new System.Drawing.Point(27, 267);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(638, 125);
            this.groupBox3.TabIndex = 44;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "UDP";
            // 
            // btnSendUdp
            // 
            this.btnSendUdp.Location = new System.Drawing.Point(522, 92);
            this.btnSendUdp.Name = "btnSendUdp";
            this.btnSendUdp.Size = new System.Drawing.Size(107, 25);
            this.btnSendUdp.TabIndex = 44;
            this.btnSendUdp.Text = "发送UDP";
            this.btnSendUdp.UseVisualStyleBackColor = true;
            this.btnSendUdp.Click += new System.EventHandler(this.BtnSendUdp_Click);
            // 
            // txtUDPIP
            // 
            this.txtUDPIP.Location = new System.Drawing.Point(21, 19);
            this.txtUDPIP.MaxLength = 15;
            this.txtUDPIP.Name = "txtUDPIP";
            this.txtUDPIP.Size = new System.Drawing.Size(195, 20);
            this.txtUDPIP.TabIndex = 38;
            this.txtUDPIP.Text = "127.0.0.1";
            // 
            // btnUDPPath
            // 
            this.btnUDPPath.Location = new System.Drawing.Point(398, 52);
            this.btnUDPPath.Name = "btnUDPPath";
            this.btnUDPPath.Size = new System.Drawing.Size(134, 25);
            this.btnUDPPath.TabIndex = 42;
            this.btnUDPPath.Text = "发送路径所有文件";
            this.btnUDPPath.UseVisualStyleBackColor = true;
            this.btnUDPPath.Click += new System.EventHandler(this.BtnUDPPath_Click);
            // 
            // btnUDPConnect
            // 
            this.btnUDPConnect.Enabled = false;
            this.btnUDPConnect.Location = new System.Drawing.Point(398, 19);
            this.btnUDPConnect.Name = "btnUDPConnect";
            this.btnUDPConnect.Size = new System.Drawing.Size(75, 25);
            this.btnUDPConnect.TabIndex = 2;
            this.btnUDPConnect.Text = "连接";
            this.btnUDPConnect.UseVisualStyleBackColor = true;
            this.btnUDPConnect.Click += new System.EventHandler(this.BtnUDPConnect_Click);
            // 
            // btnUDPFile
            // 
            this.btnUDPFile.Location = new System.Drawing.Point(554, 52);
            this.btnUDPFile.Name = "btnUDPFile";
            this.btnUDPFile.Size = new System.Drawing.Size(75, 25);
            this.btnUDPFile.TabIndex = 41;
            this.btnUDPFile.Text = "发送文件";
            this.btnUDPFile.UseVisualStyleBackColor = true;
            this.btnUDPFile.Click += new System.EventHandler(this.BtnUDPFile_Click);
            // 
            // btnUDPMsg
            // 
            this.btnUDPMsg.Location = new System.Drawing.Point(78, 97);
            this.btnUDPMsg.MaxLength = 15;
            this.btnUDPMsg.Name = "btnUDPMsg";
            this.btnUDPMsg.Size = new System.Drawing.Size(179, 20);
            this.btnUDPMsg.TabIndex = 31;
            this.btnUDPMsg.Text = "9638520147";
            // 
            // txtUDPPath
            // 
            this.txtUDPPath.Location = new System.Drawing.Point(21, 57);
            this.txtUDPPath.MaxLength = 15;
            this.txtUDPPath.Name = "txtUDPPath";
            this.txtUDPPath.Size = new System.Drawing.Size(348, 20);
            this.txtUDPPath.TabIndex = 40;
            this.txtUDPPath.Text = "C:\\Users\\sujie\\Desktop\\send";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "文本消息";
            // 
            // txtUDPPort
            // 
            this.txtUDPPort.Location = new System.Drawing.Point(260, 19);
            this.txtUDPPort.MaxLength = 15;
            this.txtUDPPort.Name = "txtUDPPort";
            this.txtUDPPort.Size = new System.Drawing.Size(109, 20);
            this.txtUDPPort.TabIndex = 39;
            this.txtUDPPort.Text = "6000";
            // 
            // btnUDPTxt
            // 
            this.btnUDPTxt.Location = new System.Drawing.Point(271, 94);
            this.btnUDPTxt.Name = "btnUDPTxt";
            this.btnUDPTxt.Size = new System.Drawing.Size(75, 25);
            this.btnUDPTxt.TabIndex = 35;
            this.btnUDPTxt.Text = "发送文本";
            this.btnUDPTxt.UseVisualStyleBackColor = true;
            this.btnUDPTxt.Click += new System.EventHandler(this.BtnUDPTxt_Click);
            // 
            // btnUDPClear
            // 
            this.btnUDPClear.Location = new System.Drawing.Point(398, 92);
            this.btnUDPClear.Name = "btnUDPClear";
            this.btnUDPClear.Size = new System.Drawing.Size(75, 25);
            this.btnUDPClear.TabIndex = 36;
            this.btnUDPClear.Text = "清空文本框";
            this.btnUDPClear.UseVisualStyleBackColor = true;
            // 
            // chkUDPHeart
            // 
            this.chkUDPHeart.AutoSize = true;
            this.chkUDPHeart.Location = new System.Drawing.Point(505, 19);
            this.chkUDPHeart.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chkUDPHeart.Name = "chkUDPHeart";
            this.chkUDPHeart.Size = new System.Drawing.Size(86, 17);
            this.chkUDPHeart.TabIndex = 37;
            this.chkUDPHeart.Text = "发送心跳包";
            this.chkUDPHeart.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSendTCP);
            this.groupBox2.Controls.Add(this.txtIP);
            this.groupBox2.Controls.Add(this.btnSendPathAllFiles);
            this.groupBox2.Controls.Add(this.btnConn);
            this.groupBox2.Controls.Add(this.btnSendAllPath);
            this.groupBox2.Controls.Add(this.txtMsg);
            this.groupBox2.Controls.Add(this.txtPath);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtPort);
            this.groupBox2.Controls.Add(this.btnSend);
            this.groupBox2.Controls.Add(this.btnclear);
            this.groupBox2.Controls.Add(this.cbHeart);
            this.groupBox2.Location = new System.Drawing.Point(36, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(638, 125);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "TCP";
            // 
            // btnSendTCP
            // 
            this.btnSendTCP.Location = new System.Drawing.Point(522, 92);
            this.btnSendTCP.Name = "btnSendTCP";
            this.btnSendTCP.Size = new System.Drawing.Size(107, 25);
            this.btnSendTCP.TabIndex = 43;
            this.btnSendTCP.Text = "发送TCP";
            this.btnSendTCP.UseVisualStyleBackColor = true;
            this.btnSendTCP.Click += new System.EventHandler(this.BtnSendTCP_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(21, 19);
            this.txtIP.MaxLength = 15;
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(195, 20);
            this.txtIP.TabIndex = 38;
            this.txtIP.Text = "127.0.0.1";
            // 
            // btnSendPathAllFiles
            // 
            this.btnSendPathAllFiles.Location = new System.Drawing.Point(398, 52);
            this.btnSendPathAllFiles.Name = "btnSendPathAllFiles";
            this.btnSendPathAllFiles.Size = new System.Drawing.Size(134, 25);
            this.btnSendPathAllFiles.TabIndex = 42;
            this.btnSendPathAllFiles.Text = "发送路径所有文件";
            this.btnSendPathAllFiles.UseVisualStyleBackColor = true;
            this.btnSendPathAllFiles.Click += new System.EventHandler(this.BtnSendPathAllFiles_Click);
            // 
            // btnSendAllPath
            // 
            this.btnSendAllPath.Location = new System.Drawing.Point(554, 52);
            this.btnSendAllPath.Name = "btnSendAllPath";
            this.btnSendAllPath.Size = new System.Drawing.Size(75, 25);
            this.btnSendAllPath.TabIndex = 41;
            this.btnSendAllPath.Text = "发送文件";
            this.btnSendAllPath.UseVisualStyleBackColor = true;
            this.btnSendAllPath.Click += new System.EventHandler(this.BtnSendOneFiles_Click);
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(78, 97);
            this.txtMsg.MaxLength = 15;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(179, 20);
            this.txtMsg.TabIndex = 31;
            this.txtMsg.Text = "0123456789";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(21, 57);
            this.txtPath.MaxLength = 15;
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(348, 20);
            this.txtPath.TabIndex = 40;
            this.txtPath.Text = "C:\\Users\\sujie\\Desktop\\send";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "文本消息";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(260, 19);
            this.txtPort.MaxLength = 15;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(109, 20);
            this.txtPort.TabIndex = 39;
            this.txtPort.Text = "9009";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(271, 94);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 25);
            this.btnSend.TabIndex = 35;
            this.btnSend.Text = "发送文本";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnclear
            // 
            this.btnclear.Location = new System.Drawing.Point(398, 92);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(75, 25);
            this.btnclear.TabIndex = 36;
            this.btnclear.Text = "清空文本框";
            this.btnclear.UseVisualStyleBackColor = true;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // cbHeart
            // 
            this.cbHeart.AutoSize = true;
            this.cbHeart.Location = new System.Drawing.Point(505, 19);
            this.cbHeart.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbHeart.Name = "cbHeart";
            this.cbHeart.Size = new System.Drawing.Size(86, 17);
            this.cbHeart.TabIndex = 37;
            this.cbHeart.Text = "发送心跳包";
            this.cbHeart.UseVisualStyleBackColor = true;
            // 
            // TimerTcp
            // 
            this.TimerTcp.Interval = 3000;
            this.TimerTcp.Tick += new System.EventHandler(this.TimerTcp_Tick);
            // 
            // TimerUdp
            // 
            this.TimerUdp.Interval = 3000;
            this.TimerUdp.Tick += new System.EventHandler(this.TimerUdp_Tick);
            // 
            // txtNickName
            // 
            this.txtNickName.Location = new System.Drawing.Point(6, 42);
            this.txtNickName.MaxLength = 15;
            this.txtNickName.Name = "txtNickName";
            this.txtNickName.Size = new System.Drawing.Size(73, 20);
            this.txtNickName.TabIndex = 47;
            this.txtNickName.Text = "TEMP0001";
            // 
            // Nick
            // 
            this.Nick.AutoSize = true;
            this.Nick.Location = new System.Drawing.Point(27, 21);
            this.Nick.Name = "Nick";
            this.Nick.Size = new System.Drawing.Size(29, 13);
            this.Nick.TabIndex = 48;
            this.Nick.Text = "Nick";
            // 
            // ID
            // 
            this.ID.AutoSize = true;
            this.ID.Location = new System.Drawing.Point(108, 21);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(18, 13);
            this.ID.TabIndex = 50;
            this.ID.Text = "ID";
            // 
            // txtIDType
            // 
            this.txtIDType.Location = new System.Drawing.Point(87, 42);
            this.txtIDType.MaxLength = 15;
            this.txtIDType.Name = "txtIDType";
            this.txtIDType.Size = new System.Drawing.Size(73, 20);
            this.txtIDType.TabIndex = 49;
            this.txtIDType.Text = "16";
            // 
            // Param4
            // 
            this.Param4.AutoSize = true;
            this.Param4.Location = new System.Drawing.Point(266, 21);
            this.Param4.Name = "Param4";
            this.Param4.Size = new System.Drawing.Size(43, 13);
            this.Param4.TabIndex = 54;
            this.Param4.Text = "Param4";
            // 
            // txtParam4
            // 
            this.txtParam4.Location = new System.Drawing.Point(245, 42);
            this.txtParam4.MaxLength = 15;
            this.txtParam4.Name = "txtParam4";
            this.txtParam4.Size = new System.Drawing.Size(73, 20);
            this.txtParam4.TabIndex = 53;
            this.txtParam4.Text = "56.3";
            // 
            // Setting
            // 
            this.Setting.AutoSize = true;
            this.Setting.Location = new System.Drawing.Point(187, 21);
            this.Setting.Name = "Setting";
            this.Setting.Size = new System.Drawing.Size(40, 13);
            this.Setting.TabIndex = 52;
            this.Setting.Text = "Setting";
            // 
            // txtSetting3
            // 
            this.txtSetting3.Location = new System.Drawing.Point(166, 42);
            this.txtSetting3.MaxLength = 15;
            this.txtSetting3.Name = "txtSetting3";
            this.txtSetting3.Size = new System.Drawing.Size(73, 20);
            this.txtSetting3.TabIndex = 51;
            this.txtSetting3.Text = "Auto";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSendValue);
            this.groupBox4.Controls.Add(this.txtDouble);
            this.groupBox4.Controls.Add(this.Nick);
            this.groupBox4.Controls.Add(this.btnClearPort);
            this.groupBox4.Controls.Add(this.Param4);
            this.groupBox4.Controls.Add(this.txtClearPort);
            this.groupBox4.Controls.Add(this.txtNickName);
            this.groupBox4.Controls.Add(this.txtParam4);
            this.groupBox4.Controls.Add(this.txtIDType);
            this.groupBox4.Controls.Add(this.Setting);
            this.groupBox4.Controls.Add(this.ID);
            this.groupBox4.Controls.Add(this.txtSetting3);
            this.groupBox4.Location = new System.Drawing.Point(27, 166);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(638, 80);
            this.groupBox4.TabIndex = 55;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "设置";
            // 
            // btnSendValue
            // 
            this.btnSendValue.Location = new System.Drawing.Point(466, 42);
            this.btnSendValue.Name = "btnSendValue";
            this.btnSendValue.Size = new System.Drawing.Size(75, 25);
            this.btnSendValue.TabIndex = 56;
            this.btnSendValue.Text = "发送值";
            this.btnSendValue.UseVisualStyleBackColor = true;
            this.btnSendValue.Click += new System.EventHandler(this.BtnSendValue_Click);
            // 
            // txtDouble
            // 
            this.txtDouble.Location = new System.Drawing.Point(466, 14);
            this.txtDouble.MaxLength = 15;
            this.txtDouble.Name = "txtDouble";
            this.txtDouble.Size = new System.Drawing.Size(75, 20);
            this.txtDouble.TabIndex = 55;
            this.txtDouble.Text = "32.06";
            // 
            // SSClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 587);
            this.Controls.Add(this.txtAll);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "SSClient";
            this.Text = "Socket客户端";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.TextBox txtAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.Button btnclear;
        private System.Windows.Forms.CheckBox cbHeart;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnSendAllPath;
        private System.Windows.Forms.Button btnSendPathAllFiles;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtUDPIP;
        private System.Windows.Forms.Button btnUDPPath;
        private System.Windows.Forms.Button btnUDPConnect;
        private System.Windows.Forms.Button btnUDPFile;
        private System.Windows.Forms.TextBox btnUDPMsg;
        private System.Windows.Forms.TextBox txtUDPPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUDPPort;
        private System.Windows.Forms.Button btnUDPTxt;
        private System.Windows.Forms.Button btnUDPClear;
        private System.Windows.Forms.CheckBox chkUDPHeart;
        private System.Windows.Forms.Button btnSendTCP;
        private System.Windows.Forms.Button btnClearPort;
        private System.Windows.Forms.TextBox txtClearPort;
        private System.Windows.Forms.Timer TimerTcp;
        private System.Windows.Forms.Timer TimerUdp;
        private System.Windows.Forms.Button btnSendUdp;
        private System.Windows.Forms.Label Param4;
        private System.Windows.Forms.TextBox txtParam4;
        private System.Windows.Forms.Label Setting;
        private System.Windows.Forms.TextBox txtSetting3;
        private System.Windows.Forms.Label ID;
        private System.Windows.Forms.TextBox txtIDType;
        private System.Windows.Forms.Label Nick;
        private System.Windows.Forms.TextBox txtNickName;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSendValue;
        private System.Windows.Forms.TextBox txtDouble;
    }
}

