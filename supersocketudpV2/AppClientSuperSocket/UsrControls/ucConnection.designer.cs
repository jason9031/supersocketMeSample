namespace IMESAgent.GUI
{
    partial class ucConnection
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.gbServerSetting = new System.Windows.Forms.GroupBox();
            this.btnApplyServerSetting = new System.Windows.Forms.Button();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.lbServerPort = new System.Windows.Forms.Label();
            this.txtServerAddress = new System.Windows.Forms.TextBox();
            this.lbServerAddress = new System.Windows.Forms.Label();
            this.gbLocalSetting = new System.Windows.Forms.GroupBox();
            this.btnGetLog = new System.Windows.Forms.Button();
            this.btnApplyLocalSetting = new System.Windows.Forms.Button();
            this.btnBroswer = new System.Windows.Forms.Button();
            this.lbDay = new System.Windows.Forms.Label();
            this.cbRetentionPeriod = new System.Windows.Forms.ComboBox();
            this.lbRetentionPeriod = new System.Windows.Forms.Label();
            this.txtBackDir = new System.Windows.Forms.TextBox();
            this.lbDataLogDir = new System.Windows.Forms.Label();
            this.gbOutput = new System.Windows.Forms.GroupBox();
            this.tlpOutput = new System.Windows.Forms.TableLayoutPanel();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.tlpMain.SuspendLayout();
            this.gbServerSetting.SuspendLayout();
            this.gbLocalSetting.SuspendLayout();
            this.gbOutput.SuspendLayout();
            this.tlpOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.gbServerSetting, 0, 0);
            this.tlpMain.Controls.Add(this.gbLocalSetting, 0, 1);
            this.tlpMain.Controls.Add(this.gbOutput, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(583, 590);
            this.tlpMain.TabIndex = 4;
            // 
            // gbServerSetting
            // 
            this.gbServerSetting.Controls.Add(this.btnApplyServerSetting);
            this.gbServerSetting.Controls.Add(this.txtServerPort);
            this.gbServerSetting.Controls.Add(this.lbServerPort);
            this.gbServerSetting.Controls.Add(this.txtServerAddress);
            this.gbServerSetting.Controls.Add(this.lbServerAddress);
            this.gbServerSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbServerSetting.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbServerSetting.Location = new System.Drawing.Point(3, 3);
            this.gbServerSetting.Name = "gbServerSetting";
            this.gbServerSetting.Size = new System.Drawing.Size(577, 96);
            this.gbServerSetting.TabIndex = 1;
            this.gbServerSetting.TabStop = false;
            this.gbServerSetting.Text = "Server Setting";
            // 
            // btnApplyServerSetting
            // 
            this.btnApplyServerSetting.Location = new System.Drawing.Point(472, 56);
            this.btnApplyServerSetting.Name = "btnApplyServerSetting";
            this.btnApplyServerSetting.Size = new System.Drawing.Size(82, 32);
            this.btnApplyServerSetting.TabIndex = 8;
            this.btnApplyServerSetting.Text = "Apply";
            this.btnApplyServerSetting.UseVisualStyleBackColor = true;
            this.btnApplyServerSetting.Click += new System.EventHandler(this.btnApplyServerSetting_Click);
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(191, 55);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(190, 22);
            this.txtServerPort.TabIndex = 4;
            // 
            // lbServerPort
            // 
            this.lbServerPort.Location = new System.Drawing.Point(57, 56);
            this.lbServerPort.Name = "lbServerPort";
            this.lbServerPort.Size = new System.Drawing.Size(114, 20);
            this.lbServerPort.TabIndex = 3;
            this.lbServerPort.Text = "Server Port Number";
            this.lbServerPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServerAddress
            // 
            this.txtServerAddress.Location = new System.Drawing.Point(191, 26);
            this.txtServerAddress.Name = "txtServerAddress";
            this.txtServerAddress.Size = new System.Drawing.Size(190, 22);
            this.txtServerAddress.TabIndex = 1;
            // 
            // lbServerAddress
            // 
            this.lbServerAddress.Location = new System.Drawing.Point(54, 27);
            this.lbServerAddress.Name = "lbServerAddress";
            this.lbServerAddress.Size = new System.Drawing.Size(117, 20);
            this.lbServerAddress.TabIndex = 0;
            this.lbServerAddress.Text = "Data Server Address";
            this.lbServerAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbLocalSetting
            // 
            this.gbLocalSetting.Controls.Add(this.btnGetLog);
            this.gbLocalSetting.Controls.Add(this.btnApplyLocalSetting);
            this.gbLocalSetting.Controls.Add(this.btnBroswer);
            this.gbLocalSetting.Controls.Add(this.lbDay);
            this.gbLocalSetting.Controls.Add(this.cbRetentionPeriod);
            this.gbLocalSetting.Controls.Add(this.lbRetentionPeriod);
            this.gbLocalSetting.Controls.Add(this.txtBackDir);
            this.gbLocalSetting.Controls.Add(this.lbDataLogDir);
            this.gbLocalSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLocalSetting.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.gbLocalSetting.Location = new System.Drawing.Point(3, 105);
            this.gbLocalSetting.Name = "gbLocalSetting";
            this.gbLocalSetting.Size = new System.Drawing.Size(577, 117);
            this.gbLocalSetting.TabIndex = 2;
            this.gbLocalSetting.TabStop = false;
            this.gbLocalSetting.Text = "Local Setting";
            // 
            // btnGetLog
            // 
            this.btnGetLog.Location = new System.Drawing.Point(472, 73);
            this.btnGetLog.Name = "btnGetLog";
            this.btnGetLog.Size = new System.Drawing.Size(87, 32);
            this.btnGetLog.TabIndex = 9;
            this.btnGetLog.Text = "Get Data Log";
            this.btnGetLog.UseVisualStyleBackColor = true;
            this.btnGetLog.Click += new System.EventHandler(this.btnGetLog_Click);
            // 
            // btnApplyLocalSetting
            // 
            this.btnApplyLocalSetting.Location = new System.Drawing.Point(409, 73);
            this.btnApplyLocalSetting.Name = "btnApplyLocalSetting";
            this.btnApplyLocalSetting.Size = new System.Drawing.Size(57, 32);
            this.btnApplyLocalSetting.TabIndex = 7;
            this.btnApplyLocalSetting.Text = "Apply";
            this.btnApplyLocalSetting.UseVisualStyleBackColor = true;
            this.btnApplyLocalSetting.Click += new System.EventHandler(this.btnApplyLocalSetting_Click);
            // 
            // btnBroswer
            // 
            this.btnBroswer.Location = new System.Drawing.Point(523, 24);
            this.btnBroswer.Name = "btnBroswer";
            this.btnBroswer.Size = new System.Drawing.Size(31, 23);
            this.btnBroswer.TabIndex = 6;
            this.btnBroswer.Text = "...";
            this.btnBroswer.UseVisualStyleBackColor = true;
            this.btnBroswer.Click += new System.EventHandler(this.btnBroswer_Click);
            // 
            // lbDay
            // 
            this.lbDay.AutoSize = true;
            this.lbDay.Location = new System.Drawing.Point(339, 62);
            this.lbDay.Name = "lbDay";
            this.lbDay.Size = new System.Drawing.Size(36, 16);
            this.lbDay.TabIndex = 5;
            this.lbDay.Text = "(Day)";
            // 
            // cbRetentionPeriod
            // 
            this.cbRetentionPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRetentionPeriod.FormattingEnabled = true;
            this.cbRetentionPeriod.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cbRetentionPeriod.Location = new System.Drawing.Point(191, 58);
            this.cbRetentionPeriod.Name = "cbRetentionPeriod";
            this.cbRetentionPeriod.Size = new System.Drawing.Size(121, 24);
            this.cbRetentionPeriod.TabIndex = 4;
            // 
            // lbRetentionPeriod
            // 
            this.lbRetentionPeriod.Location = new System.Drawing.Point(21, 60);
            this.lbRetentionPeriod.Name = "lbRetentionPeriod";
            this.lbRetentionPeriod.Size = new System.Drawing.Size(150, 20);
            this.lbRetentionPeriod.TabIndex = 3;
            this.lbRetentionPeriod.Text = "Data Log Retention Period";
            this.lbRetentionPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBackDir
            // 
            this.txtBackDir.Location = new System.Drawing.Point(191, 25);
            this.txtBackDir.Name = "txtBackDir";
            this.txtBackDir.Size = new System.Drawing.Size(320, 22);
            this.txtBackDir.TabIndex = 2;
            // 
            // lbDataLogDir
            // 
            this.lbDataLogDir.Location = new System.Drawing.Point(21, 25);
            this.lbDataLogDir.Name = "lbDataLogDir";
            this.lbDataLogDir.Size = new System.Drawing.Size(150, 20);
            this.lbDataLogDir.TabIndex = 1;
            this.lbDataLogDir.Text = "Data Log Backup Directory";
            this.lbDataLogDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbOutput
            // 
            this.gbOutput.Controls.Add(this.tlpOutput);
            this.gbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOutput.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.gbOutput.Location = new System.Drawing.Point(3, 228);
            this.gbOutput.Name = "gbOutput";
            this.gbOutput.Size = new System.Drawing.Size(577, 359);
            this.gbOutput.TabIndex = 3;
            this.gbOutput.TabStop = false;
            this.gbOutput.Text = "Output";
            // 
            // tlpOutput
            // 
            this.tlpOutput.ColumnCount = 1;
            this.tlpOutput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOutput.Controls.Add(this.lstOutput, 0, 1);
            this.tlpOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpOutput.Location = new System.Drawing.Point(3, 18);
            this.tlpOutput.Name = "tlpOutput";
            this.tlpOutput.RowCount = 2;
            this.tlpOutput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpOutput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOutput.Size = new System.Drawing.Size(571, 338);
            this.tlpOutput.TabIndex = 1;
            // 
            // lstOutput
            // 
            this.lstOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstOutput.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.HorizontalScrollbar = true;
            this.lstOutput.ItemHeight = 15;
            this.lstOutput.Location = new System.Drawing.Point(3, 11);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.ScrollAlwaysVisible = true;
            this.lstOutput.Size = new System.Drawing.Size(565, 324);
            this.lstOutput.TabIndex = 0;
            // 
            // ucConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tlpMain);
            this.Name = "ucConnection";
            this.Size = new System.Drawing.Size(583, 590);
            this.tlpMain.ResumeLayout(false);
            this.gbServerSetting.ResumeLayout(false);
            this.gbServerSetting.PerformLayout();
            this.gbLocalSetting.ResumeLayout(false);
            this.gbLocalSetting.PerformLayout();
            this.gbOutput.ResumeLayout(false);
            this.tlpOutput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.GroupBox gbServerSetting;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Label lbServerPort;
        private System.Windows.Forms.TextBox txtServerAddress;
        private System.Windows.Forms.Label lbServerAddress;
        private System.Windows.Forms.GroupBox gbLocalSetting;
        private System.Windows.Forms.Button btnGetLog;
        private System.Windows.Forms.Button btnApplyLocalSetting;
        private System.Windows.Forms.Button btnBroswer;
        private System.Windows.Forms.Label lbDay;
        private System.Windows.Forms.ComboBox cbRetentionPeriod;
        private System.Windows.Forms.Label lbRetentionPeriod;
        private System.Windows.Forms.TextBox txtBackDir;
        private System.Windows.Forms.Label lbDataLogDir;
        private System.Windows.Forms.GroupBox gbOutput;
        private System.Windows.Forms.TableLayoutPanel tlpOutput;
        private System.Windows.Forms.ListBox lstOutput;
        private System.Windows.Forms.Button btnApplyServerSetting;
    }
}
