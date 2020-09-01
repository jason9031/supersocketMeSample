namespace IMESAgent.GUI
{
    partial class ucTestMode
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.txtSent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.btnLoadFiles = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMonitor = new System.Windows.Forms.TextBox();
            this.btnBroswer2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDataType = new System.Windows.Forms.TextBox();
            this.cbIgnoreZ = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnClean = new System.Windows.Forms.Button();
            this.btnSendQueue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data File Path";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(113, 43);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(431, 20);
            this.txtPath.TabIndex = 1;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Location = new System.Drawing.Point(569, 41);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(42, 23);
            this.btnBrowser.TabIndex = 2;
            this.btnBrowser.Text = "...";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // txtSent
            // 
            this.txtSent.Location = new System.Drawing.Point(107, 349);
            this.txtSent.Name = "txtSent";
            this.txtSent.Size = new System.Drawing.Size(78, 20);
            this.txtSent.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 349);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Sent";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(122, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(370, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Attention: The file path should follow the format \"..\\\\ParserCode\\\\BCRIP\\\\..\"";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 261);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Interval";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(211, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "(MS)";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(107, 261);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(78, 20);
            this.txtInterval.TabIndex = 10;
            // 
            // btnLoadFiles
            // 
            this.btnLoadFiles.Location = new System.Drawing.Point(387, 282);
            this.btnLoadFiles.Name = "btnLoadFiles";
            this.btnLoadFiles.Size = new System.Drawing.Size(105, 23);
            this.btnLoadFiles.TabIndex = 12;
            this.btnLoadFiles.Text = "Load Files";
            this.btnLoadFiles.UseVisualStyleBackColor = true;
            this.btnLoadFiles.Click += new System.EventHandler(this.btnLoadFiles_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 307);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Total";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(107, 304);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(78, 20);
            this.txtTotal.TabIndex = 13;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(387, 329);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(105, 23);
            this.btnStart.TabIndex = 15;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Monitor Folder";
            // 
            // txtMonitor
            // 
            this.txtMonitor.Location = new System.Drawing.Point(113, 114);
            this.txtMonitor.Name = "txtMonitor";
            this.txtMonitor.Size = new System.Drawing.Size(431, 20);
            this.txtMonitor.TabIndex = 17;
            // 
            // btnBroswer2
            // 
            this.btnBroswer2.Location = new System.Drawing.Point(569, 112);
            this.btnBroswer2.Name = "btnBroswer2";
            this.btnBroswer2.Size = new System.Drawing.Size(42, 23);
            this.btnBroswer2.TabIndex = 18;
            this.btnBroswer2.Text = "...";
            this.btnBroswer2.UseVisualStyleBackColor = true;
            this.btnBroswer2.Click += new System.EventHandler(this.btnBroswer2_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 173);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Choose Data Type";
            // 
            // txtDataType
            // 
            this.txtDataType.Location = new System.Drawing.Point(113, 170);
            this.txtDataType.Name = "txtDataType";
            this.txtDataType.Size = new System.Drawing.Size(241, 20);
            this.txtDataType.TabIndex = 20;
            // 
            // cbIgnoreZ
            // 
            this.cbIgnoreZ.AutoSize = true;
            this.cbIgnoreZ.Checked = true;
            this.cbIgnoreZ.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnoreZ.Location = new System.Drawing.Point(378, 170);
            this.cbIgnoreZ.Name = "cbIgnoreZ";
            this.cbIgnoreZ.Size = new System.Drawing.Size(93, 17);
            this.cbIgnoreZ.TabIndex = 21;
            this.cbIgnoreZ.Text = "Ignore Z Type";
            this.cbIgnoreZ.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(122, 207);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(281, 41);
            this.label9.TabIndex = 22;
            this.label9.Text = "Example : A001*B001, only these two type will be sent.\r\nIf leave the textbox blan" +
    "k, all data will be sent.\r\n";
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(515, 329);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(105, 23);
            this.btnClean.TabIndex = 23;
            this.btnClean.Text = "Clean Queue";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnSendQueue
            // 
            this.btnSendQueue.Location = new System.Drawing.Point(515, 282);
            this.btnSendQueue.Name = "btnSendQueue";
            this.btnSendQueue.Size = new System.Drawing.Size(105, 23);
            this.btnSendQueue.TabIndex = 24;
            this.btnSendQueue.Text = "Dequeu Messages";
            this.btnSendQueue.UseVisualStyleBackColor = true;
            this.btnSendQueue.Click += new System.EventHandler(this.btnSendQueue_Click);
            // 
            // ucTestMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSendQueue);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbIgnoreZ);
            this.Controls.Add(this.txtDataType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnBroswer2);
            this.Controls.Add(this.txtMonitor);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.btnLoadFiles);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSent);
            this.Controls.Add(this.btnBrowser);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label1);
            this.Name = "ucTestMode";
            this.Size = new System.Drawing.Size(670, 404);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.TextBox txtSent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Button btnLoadFiles;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMonitor;
        private System.Windows.Forms.Button btnBroswer2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDataType;
        private System.Windows.Forms.CheckBox cbIgnoreZ;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.Button btnSendQueue;
    }
}
