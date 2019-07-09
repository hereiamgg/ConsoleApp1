namespace DIMSClient
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tbLog = new System.Windows.Forms.TextBox();
            this.dgvConfigs = new System.Windows.Forms.DataGridView();
            this.panEdit = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbFTPHost = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSelectLocalFile = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnTestFTP = new System.Windows.Forms.Button();
            this.cbEnable = new System.Windows.Forms.CheckBox();
            this.tbFTPPwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFTPUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFTPFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbLocalFolder = new System.Windows.Forms.TextBox();
            this.lblLocalFolder = new System.Windows.Forms.Label();
            this.panLog = new System.Windows.Forms.Panel();
            this.panDataGrid = new System.Windows.Forms.Panel();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panLogList = new System.Windows.Forms.Panel();
            this.lbLogTxt = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfigs)).BeginInit();
            this.panEdit.SuspendLayout();
            this.panLog.SuspendLayout();
            this.panDataGrid.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panLogList.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLog
            // 
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLog.Location = new System.Drawing.Point(0, 0);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(529, 381);
            this.tbLog.TabIndex = 0;
            this.tbLog.WordWrap = false;
            // 
            // dgvConfigs
            // 
            this.dgvConfigs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvConfigs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConfigs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConfigs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvConfigs.Location = new System.Drawing.Point(0, 0);
            this.dgvConfigs.Name = "dgvConfigs";
            this.dgvConfigs.RowTemplate.Height = 23;
            this.dgvConfigs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConfigs.Size = new System.Drawing.Size(526, 197);
            this.dgvConfigs.TabIndex = 1;
            this.dgvConfigs.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvConfigs_CellClick);
            // 
            // panEdit
            // 
            this.panEdit.Controls.Add(this.label8);
            this.panEdit.Controls.Add(this.label7);
            this.panEdit.Controls.Add(this.label6);
            this.panEdit.Controls.Add(this.label5);
            this.panEdit.Controls.Add(this.tbFTPHost);
            this.panEdit.Controls.Add(this.label4);
            this.panEdit.Controls.Add(this.btnSelectLocalFile);
            this.panEdit.Controls.Add(this.btnDelete);
            this.panEdit.Controls.Add(this.btnSave);
            this.panEdit.Controls.Add(this.btnTestFTP);
            this.panEdit.Controls.Add(this.cbEnable);
            this.panEdit.Controls.Add(this.tbFTPPwd);
            this.panEdit.Controls.Add(this.label3);
            this.panEdit.Controls.Add(this.tbFTPUserName);
            this.panEdit.Controls.Add(this.label1);
            this.panEdit.Controls.Add(this.tbFTPFolder);
            this.panEdit.Controls.Add(this.label2);
            this.panEdit.Controls.Add(this.tbLocalFolder);
            this.panEdit.Controls.Add(this.lblLocalFolder);
            this.panEdit.Location = new System.Drawing.Point(547, 216);
            this.panEdit.Name = "panEdit";
            this.panEdit.Size = new System.Drawing.Size(526, 303);
            this.panEdit.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label8.Location = new System.Drawing.Point(151, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "填写示例：ftpuser1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label7.Location = new System.Drawing.Point(151, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "填写示例：Report\\色谱1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(151, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "填写示例：192.168.1.1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Location = new System.Drawing.Point(151, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "填写示例：D:\\Report";
            // 
            // tbFTPHost
            // 
            this.tbFTPHost.Location = new System.Drawing.Point(151, 61);
            this.tbFTPHost.Name = "tbFTPHost";
            this.tbFTPHost.Size = new System.Drawing.Size(253, 21);
            this.tbFTPHost.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "FTP服务器地址";
            // 
            // btnSelectLocalFile
            // 
            this.btnSelectLocalFile.Location = new System.Drawing.Point(411, 16);
            this.btnSelectLocalFile.Name = "btnSelectLocalFile";
            this.btnSelectLocalFile.Size = new System.Drawing.Size(35, 23);
            this.btnSelectLocalFile.TabIndex = 13;
            this.btnSelectLocalFile.Text = "...";
            this.btnSelectLocalFile.UseVisualStyleBackColor = true;
            this.btnSelectLocalFile.Click += new System.EventHandler(this.BtnSelectLocalFile_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(362, 267);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 23);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "删除配置";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(207, 267);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "保存配置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnTestFTP
            // 
            this.btnTestFTP.Location = new System.Drawing.Point(52, 267);
            this.btnTestFTP.Name = "btnTestFTP";
            this.btnTestFTP.Size = new System.Drawing.Size(100, 23);
            this.btnTestFTP.TabIndex = 10;
            this.btnTestFTP.Text = "测试FTP连接";
            this.btnTestFTP.UseVisualStyleBackColor = true;
            this.btnTestFTP.Click += new System.EventHandler(this.BtnTestFTP_Click);
            // 
            // cbEnable
            // 
            this.cbEnable.AutoSize = true;
            this.cbEnable.Checked = true;
            this.cbEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEnable.Location = new System.Drawing.Point(151, 239);
            this.cbEnable.Name = "cbEnable";
            this.cbEnable.Size = new System.Drawing.Size(72, 16);
            this.cbEnable.TabIndex = 9;
            this.cbEnable.Text = "是否启用";
            this.cbEnable.UseVisualStyleBackColor = true;
            // 
            // tbFTPPwd
            // 
            this.tbFTPPwd.Location = new System.Drawing.Point(151, 196);
            this.tbFTPPwd.Name = "tbFTPPwd";
            this.tbFTPPwd.Size = new System.Drawing.Size(253, 21);
            this.tbFTPPwd.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(86, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "FTP密码";
            // 
            // tbFTPUserName
            // 
            this.tbFTPUserName.Location = new System.Drawing.Point(151, 151);
            this.tbFTPUserName.Name = "tbFTPUserName";
            this.tbFTPUserName.Size = new System.Drawing.Size(253, 21);
            this.tbFTPUserName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "FTP用户名";
            // 
            // tbFTPFolder
            // 
            this.tbFTPFolder.Location = new System.Drawing.Point(151, 106);
            this.tbFTPFolder.Name = "tbFTPFolder";
            this.tbFTPFolder.Size = new System.Drawing.Size(253, 21);
            this.tbFTPFolder.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "FTP文件夹";
            // 
            // tbLocalFolder
            // 
            this.tbLocalFolder.Location = new System.Drawing.Point(151, 16);
            this.tbLocalFolder.Name = "tbLocalFolder";
            this.tbLocalFolder.Size = new System.Drawing.Size(253, 21);
            this.tbLocalFolder.TabIndex = 1;
            // 
            // lblLocalFolder
            // 
            this.lblLocalFolder.AutoSize = true;
            this.lblLocalFolder.Location = new System.Drawing.Point(68, 20);
            this.lblLocalFolder.Name = "lblLocalFolder";
            this.lblLocalFolder.Size = new System.Drawing.Size(65, 12);
            this.lblLocalFolder.TabIndex = 0;
            this.lblLocalFolder.Text = "本地文件夹";
            // 
            // panLog
            // 
            this.panLog.Controls.Add(this.tbLog);
            this.panLog.Location = new System.Drawing.Point(12, 138);
            this.panLog.Name = "panLog";
            this.panLog.Size = new System.Drawing.Size(529, 381);
            this.panLog.TabIndex = 3;
            // 
            // panDataGrid
            // 
            this.panDataGrid.Controls.Add(this.dgvConfigs);
            this.panDataGrid.Location = new System.Drawing.Point(547, 13);
            this.panDataGrid.Name = "panDataGrid";
            this.panDataGrid.Size = new System.Drawing.Size(526, 197);
            this.panDataGrid.TabIndex = 4;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "分析仪器报告自动采集上传软件";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // 显示ToolStripMenuItem
            // 
            this.显示ToolStripMenuItem.Name = "显示ToolStripMenuItem";
            this.显示ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.显示ToolStripMenuItem.Text = "显示";
            this.显示ToolStripMenuItem.Click += new System.EventHandler(this.显示ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // panLogList
            // 
            this.panLogList.Controls.Add(this.lbLogTxt);
            this.panLogList.Location = new System.Drawing.Point(13, 13);
            this.panLogList.Name = "panLogList";
            this.panLogList.Size = new System.Drawing.Size(528, 119);
            this.panLogList.TabIndex = 6;
            // 
            // lbLogTxt
            // 
            this.lbLogTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLogTxt.FormattingEnabled = true;
            this.lbLogTxt.ItemHeight = 12;
            this.lbLogTxt.Location = new System.Drawing.Point(0, 0);
            this.lbLogTxt.Name = "lbLogTxt";
            this.lbLogTxt.Size = new System.Drawing.Size(528, 119);
            this.lbLogTxt.TabIndex = 0;
            this.lbLogTxt.SelectedIndexChanged += new System.EventHandler(this.lbLogTxt_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 531);
            this.Controls.Add(this.panLogList);
            this.Controls.Add(this.panDataGrid);
            this.Controls.Add(this.panLog);
            this.Controls.Add(this.panEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "色谱分析报告自动采集上传软件V1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfigs)).EndInit();
            this.panEdit.ResumeLayout(false);
            this.panEdit.PerformLayout();
            this.panLog.ResumeLayout(false);
            this.panLog.PerformLayout();
            this.panDataGrid.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panLogList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.DataGridView dgvConfigs;
        private System.Windows.Forms.Panel panEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnTestFTP;
        private System.Windows.Forms.CheckBox cbEnable;
        private System.Windows.Forms.TextBox tbFTPPwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFTPUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFTPFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbLocalFolder;
        private System.Windows.Forms.Label lblLocalFolder;
        private System.Windows.Forms.Button btnSelectLocalFile;
        private System.Windows.Forms.Panel panLog;
        private System.Windows.Forms.Panel panDataGrid;
        private System.Windows.Forms.TextBox tbFTPHost;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.Panel panLogList;
        private System.Windows.Forms.ListBox lbLogTxt;
    }
}

