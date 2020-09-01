namespace IMESAgent.GUI
{
    partial class ucConfiguration
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
            this.gbConfiguration = new System.Windows.Forms.GroupBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lbWarning = new System.Windows.Forms.Label();
            this.txtLogFolder = new System.Windows.Forms.TextBox();
            this.lbLogFolder = new System.Windows.Forms.Label();
            this.lbBCRIP = new System.Windows.Forms.Label();
            this.txtBCRIP = new System.Windows.Forms.TextBox();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.lbInterval = new System.Windows.Forms.Label();
            this.cbInspType = new System.Windows.Forms.ComboBox();
            this.lbInspType = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnBroswer = new System.Windows.Forms.Button();
            this.lbMs = new System.Windows.Forms.Label();
            this.cbInspRoute = new System.Windows.Forms.ComboBox();
            this.lbRoute = new System.Windows.Forms.Label();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.lbAlias = new System.Windows.Forms.Label();
            this.gridConfigruation = new System.Windows.Forms.DataGridView();
            this.tlpMain.SuspendLayout();
            this.gbConfiguration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridConfigruation)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.gbConfiguration, 0, 0);
            this.tlpMain.Controls.Add(this.gridConfigruation, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 207F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(569, 560);
            this.tlpMain.TabIndex = 0;
            // 
            // gbConfiguration
            // 
            this.gbConfiguration.BackColor = System.Drawing.Color.Transparent;
            this.gbConfiguration.Controls.Add(this.btnApply);
            this.gbConfiguration.Controls.Add(this.btnDelete);
            this.gbConfiguration.Controls.Add(this.btnUpdate);
            this.gbConfiguration.Controls.Add(this.lbWarning);
            this.gbConfiguration.Controls.Add(this.txtLogFolder);
            this.gbConfiguration.Controls.Add(this.lbLogFolder);
            this.gbConfiguration.Controls.Add(this.lbBCRIP);
            this.gbConfiguration.Controls.Add(this.txtBCRIP);
            this.gbConfiguration.Controls.Add(this.txtInterval);
            this.gbConfiguration.Controls.Add(this.lbInterval);
            this.gbConfiguration.Controls.Add(this.cbInspType);
            this.gbConfiguration.Controls.Add(this.lbInspType);
            this.gbConfiguration.Controls.Add(this.btnAdd);
            this.gbConfiguration.Controls.Add(this.btnBroswer);
            this.gbConfiguration.Controls.Add(this.lbMs);
            this.gbConfiguration.Controls.Add(this.cbInspRoute);
            this.gbConfiguration.Controls.Add(this.lbRoute);
            this.gbConfiguration.Controls.Add(this.txtAlias);
            this.gbConfiguration.Controls.Add(this.lbAlias);
            this.gbConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbConfiguration.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.gbConfiguration.Location = new System.Drawing.Point(3, 3);
            this.gbConfiguration.Name = "gbConfiguration";
            this.gbConfiguration.Size = new System.Drawing.Size(563, 201);
            this.gbConfiguration.TabIndex = 0;
            this.gbConfiguration.TabStop = false;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(479, 153);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(66, 32);
            this.btnApply.TabIndex = 29;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(403, 153);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(59, 32);
            this.btnDelete.TabIndex = 28;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(315, 153);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(66, 32);
            this.btnUpdate.TabIndex = 27;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lbWarning
            // 
            this.lbWarning.AutoSize = true;
            this.lbWarning.ForeColor = System.Drawing.Color.Red;
            this.lbWarning.Location = new System.Drawing.Point(103, 142);
            this.lbWarning.Name = "lbWarning";
            this.lbWarning.Size = new System.Drawing.Size(117, 16);
            this.lbWarning.TabIndex = 26;
            this.lbWarning.Text = "Don\'t allow / * ? < > | ";
            // 
            // txtLogFolder
            // 
            this.txtLogFolder.Location = new System.Drawing.Point(106, 117);
            this.txtLogFolder.Name = "txtLogFolder";
            this.txtLogFolder.Size = new System.Drawing.Size(395, 22);
            this.txtLogFolder.TabIndex = 25;
            // 
            // lbLogFolder
            // 
            this.lbLogFolder.Location = new System.Drawing.Point(12, 117);
            this.lbLogFolder.Name = "lbLogFolder";
            this.lbLogFolder.Size = new System.Drawing.Size(74, 20);
            this.lbLogFolder.TabIndex = 24;
            this.lbLogFolder.Text = "Log Folder";
            this.lbLogFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbBCRIP
            // 
            this.lbBCRIP.Location = new System.Drawing.Point(314, 25);
            this.lbBCRIP.Name = "lbBCRIP";
            this.lbBCRIP.Size = new System.Drawing.Size(56, 20);
            this.lbBCRIP.TabIndex = 23;
            this.lbBCRIP.Text = "BCR IP";
            this.lbBCRIP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBCRIP
            // 
            this.txtBCRIP.Location = new System.Drawing.Point(384, 25);
            this.txtBCRIP.Name = "txtBCRIP";
            this.txtBCRIP.Size = new System.Drawing.Size(161, 22);
            this.txtBCRIP.TabIndex = 22;
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(384, 54);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(117, 22);
            this.txtInterval.TabIndex = 21;
            // 
            // lbInterval
            // 
            this.lbInterval.Location = new System.Drawing.Point(314, 54);
            this.lbInterval.Name = "lbInterval";
            this.lbInterval.Size = new System.Drawing.Size(56, 20);
            this.lbInterval.TabIndex = 20;
            this.lbInterval.Text = "Interval";
            this.lbInterval.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbInspType
            // 
            this.cbInspType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInspType.FormattingEnabled = true;
            this.cbInspType.Location = new System.Drawing.Point(106, 84);
            this.cbInspType.Name = "cbInspType";
            this.cbInspType.Size = new System.Drawing.Size(185, 24);
            this.cbInspType.TabIndex = 19;
            // 
            // lbInspType
            // 
            this.lbInspType.Location = new System.Drawing.Point(9, 85);
            this.lbInspType.Name = "lbInspType";
            this.lbInspType.Size = new System.Drawing.Size(77, 20);
            this.lbInspType.TabIndex = 18;
            this.lbInspType.Text = "Insp. Type";
            this.lbInspType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(231, 153);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(66, 32);
            this.btnAdd.TabIndex = 17;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnBroswer
            // 
            this.btnBroswer.Location = new System.Drawing.Point(514, 117);
            this.btnBroswer.Name = "btnBroswer";
            this.btnBroswer.Size = new System.Drawing.Size(31, 23);
            this.btnBroswer.TabIndex = 15;
            this.btnBroswer.Text = "...";
            this.btnBroswer.UseVisualStyleBackColor = true;
            this.btnBroswer.Click += new System.EventHandler(this.btnBroswer_Click);
            // 
            // lbMs
            // 
            this.lbMs.AutoSize = true;
            this.lbMs.Location = new System.Drawing.Point(512, 57);
            this.lbMs.Name = "lbMs";
            this.lbMs.Size = new System.Drawing.Size(33, 16);
            this.lbMs.TabIndex = 14;
            this.lbMs.Text = "(MS)";
            // 
            // cbInspRoute
            // 
            this.cbInspRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInspRoute.FormattingEnabled = true;
            this.cbInspRoute.Location = new System.Drawing.Point(106, 54);
            this.cbInspRoute.Name = "cbInspRoute";
            this.cbInspRoute.Size = new System.Drawing.Size(185, 24);
            this.cbInspRoute.TabIndex = 13;
            // 
            // lbRoute
            // 
            this.lbRoute.Location = new System.Drawing.Point(9, 55);
            this.lbRoute.Name = "lbRoute";
            this.lbRoute.Size = new System.Drawing.Size(80, 20);
            this.lbRoute.TabIndex = 12;
            this.lbRoute.Text = "Insp. Route";
            this.lbRoute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAlias
            // 
            this.txtAlias.Location = new System.Drawing.Point(106, 25);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(185, 22);
            this.txtAlias.TabIndex = 11;
            // 
            // lbAlias
            // 
            this.lbAlias.Location = new System.Drawing.Point(15, 25);
            this.lbAlias.Name = "lbAlias";
            this.lbAlias.Size = new System.Drawing.Size(71, 20);
            this.lbAlias.TabIndex = 10;
            this.lbAlias.Text = "Alias";
            this.lbAlias.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gridConfigruation
            // 
            this.gridConfigruation.AllowUserToAddRows = false;
            this.gridConfigruation.AllowUserToDeleteRows = false;
            this.gridConfigruation.AllowUserToResizeRows = false;
            this.gridConfigruation.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridConfigruation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridConfigruation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridConfigruation.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridConfigruation.Location = new System.Drawing.Point(3, 210);
            this.gridConfigruation.MultiSelect = false;
            this.gridConfigruation.Name = "gridConfigruation";
            this.gridConfigruation.ReadOnly = true;
            this.gridConfigruation.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridConfigruation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridConfigruation.Size = new System.Drawing.Size(563, 347);
            this.gridConfigruation.TabIndex = 1;
            // 
            // ucConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "ucConfiguration";
            this.Size = new System.Drawing.Size(569, 560);
            this.tlpMain.ResumeLayout(false);
            this.gbConfiguration.ResumeLayout(false);
            this.gbConfiguration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridConfigruation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.GroupBox  gbConfiguration;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnBroswer;
        private System.Windows.Forms.ComboBox cbInspRoute;
        private System.Windows.Forms.Label lbRoute;
        private System.Windows.Forms.TextBox txtAlias;
        private System.Windows.Forms.Label lbAlias;
        private System.Windows.Forms.Label lbInspType;
        private System.Windows.Forms.Label lbBCRIP;
        private System.Windows.Forms.TextBox txtBCRIP;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label lbInterval;
        private System.Windows.Forms.ComboBox cbInspType;
        private System.Windows.Forms.Label lbMs;
        private System.Windows.Forms.TextBox txtLogFolder;
        private System.Windows.Forms.Label lbLogFolder;
        private System.Windows.Forms.Label lbWarning;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridView gridConfigruation;
        private System.Windows.Forms.Button btnApply;
    }
}
