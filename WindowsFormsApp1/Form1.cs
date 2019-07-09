using FluentFTP;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using DIMSClient.Model;
using System.Threading.Tasks;

namespace DIMSClient
{
    public partial class Form1 : Form
    {
        static readonly string configpath = $"{Environment.CurrentDirectory}/Configs.xml";
        static DataTable dataTableMemory = null;
        static int selectRowIndex = -1;//global var for select row of datagridview
        static ILog log = log4net.LogManager.GetLogger("App.Logging");
        private static Dictionary<string, FileSystemSafeWatcher> watchers = new Dictionary<string, FileSystemSafeWatcher>();//global watcher object list for realse

        public Form1()
        {
            InitializeComponent();
            try
            {
                GetData();
                InitEditPanel();
                DoUploadTask();
            }
            catch (Exception err)
            {
                log.Error(err.Message + err.StackTrace);
            }
        }

        private void DoUploadTask()
        {
            if (dataTableMemory != null)
            {
                foreach (DataRow dataRow in dataTableMemory.Rows)
                {
                    if (dataRow["状态"].ToString() == "启用")
                    {
                        WatcherStart(dataRow["本地文件夹"].ToString(), "*.*");
                    }
                }
            }
        }

        #region ConfigTool
        private void BtnSelectLocalFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbLocalFolder.Text = dialog.SelectedPath;
            }
        }

        private void BtnTestFTP_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbFTPFolder.Text.Trim()) && !string.IsNullOrEmpty(tbFTPHost.Text.Trim()) && !string.IsNullOrEmpty(tbFTPUserName.Text.Trim()) && !string.IsNullOrEmpty(tbFTPPwd.Text.Trim()))
            {
                FtpClient conn = new FtpClient();
                try
                {
                    conn.Host = tbFTPHost.Text.Trim();                    
                    conn.Credentials = new NetworkCredential(tbFTPUserName.Text.Trim(), tbFTPPwd.Text.Trim());
                    conn.Connect();
                    if (conn.DirectoryExists(tbFTPFolder.Text.Trim()))
                    {
                        MessageBox.Show("FTP连接测试成功！");
                    }
                    else
                    {
                        MessageBox.Show($"{tbFTPHost.Text.Trim()}{tbFTPFolder.Text.Trim()}无法连接");
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show($"FTP连接测试失败！{error.Message}");
                }
                finally
                {
                    if (conn.IsConnected)
                    {
                        conn.Disconnect();
                        conn.Dispose();
                    }
                }
            }
        }

        private void GetData()
        {
            dataTableMemory = XmlHelper.GetTable(configpath, XmlHelper.XmlType.File, "mapping");
            this.dgvConfigs.DataSource = dataTableMemory;
            //log
            DirectoryInfo directoryInfo = new DirectoryInfo($"{Directory.GetCurrentDirectory()}/logs");
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            foreach (FileInfo fileInfo in fileInfos)
            {
                this.lbLogTxt.Items.Add(fileInfo.FullName);
            }
        }

        private void InitEditPanel()
        {
            if (dgvConfigs.Rows.Count > 0)
            {
                dgvConfigs.Rows[0].Selected = true;
                this.tbLocalFolder.Text = dgvConfigs.Rows[0].Cells["本地文件夹"].Value.ToString();
                this.tbFTPHost.Text = dgvConfigs.Rows[0].Cells["FTP服务器地址"].Value.ToString();
                this.tbFTPFolder.Text = dgvConfigs.Rows[0].Cells["FTP文件夹"].Value.ToString();
                this.tbFTPUserName.Text = dgvConfigs.Rows[0].Cells["FTP账号"].Value.ToString();
                this.tbFTPPwd.Text = dgvConfigs.Rows[0].Cells["FTP密码"].Value.ToString();
                this.cbEnable.Checked = dgvConfigs.Rows[0].Cells["状态"].Value.ToString() == "启用" ? true : false;
            }
        }

        static private Mapping GetMappingByLocalFolder(string localFolder)
        {
            Mapping mapping = null;
            DataRow[] dataRows = dataTableMemory.Select($"本地文件夹='{Path.GetDirectoryName(localFolder)}'");
            if (dataRows.Length > 0)
            {
                mapping = new Mapping(dataRows[0]["序号"].ToString(), dataRows[0]["本地文件夹"].ToString(), dataRows[0]["FTP服务器地址"].ToString(), dataRows[0]["FTP文件夹"].ToString(), dataRows[0]["FTP账号"].ToString(), dataRows[0]["FTP密码"].ToString(), dataRows[0]["状态"].ToString());
            }
            return mapping;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataTableMemory != null)
            {
                if (watchers.ContainsKey(dataTableMemory.Rows[dgvConfigs.CurrentRow.Index]["本地文件夹"].ToString()))
                {
                    watchers.Remove(dataTableMemory.Rows[dgvConfigs.CurrentRow.Index]["本地文件夹"].ToString());
                }
                dataTableMemory.Rows[dgvConfigs.CurrentRow.Index].Delete();
                for ( int i = 0;i < dataTableMemory.Rows.Count;i++ )
                {
                    dataTableMemory.Rows[i]["序号"] = i + 1; //修正序号为唯一自然顺序
                }
                dataTableMemory.WriteXml(configpath);
                GetData();
                
                DoUploadTask();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tbLocalFolder.Text.Trim()) && !string.IsNullOrEmpty(tbFTPHost.Text.Trim()) && !string.IsNullOrEmpty(tbFTPFolder.Text.Trim()) && !string.IsNullOrEmpty(tbFTPUserName.Text.Trim()) && !string.IsNullOrEmpty(tbFTPPwd.Text.Trim()))
                {
                    if (dgvConfigs.Rows[selectRowIndex].IsNewRow)
                    {
                        if (dataTableMemory != null)
                        {
                            DataRow dataRow = dataTableMemory.NewRow();
                            dataRow["序号"] = selectRowIndex + 1;
                            dataRow["本地文件夹"] = tbLocalFolder.Text.Trim();
                            dataRow["FTP服务器地址"] = tbFTPHost.Text.Trim();
                            dataRow["FTP文件夹"] = tbFTPFolder.Text.Trim();
                            dataRow["FTP账号"] = tbFTPUserName.Text.Trim();
                            dataRow["FTP密码"] = tbFTPPwd.Text.Trim();
                            dataRow["状态"] = cbEnable.Checked ? "启用" : "禁用";
                            dataTableMemory.Rows.Add(dataRow);
                        }
                        else
                        {
                            dataTableMemory.Columns.AddRange(new DataColumn[] {
                            new DataColumn("序号",typeof(string)),
                            new DataColumn("本地文件夹",typeof(string)),
                            new DataColumn("FTP服务器地址",typeof(string)),
                            new DataColumn("FTP文件夹",typeof(string)),
                            new DataColumn("FTP账号",typeof(string)),
                            new DataColumn("FTP密码",typeof(string)),
                            new DataColumn("启用",typeof(string)),
                        });
                            DataRow dataRow = dataTableMemory.NewRow();
                            dataRow["序号"] = selectRowIndex + 1;
                            dataRow["本地文件夹"] = tbLocalFolder.Text.Trim();
                            dataRow["FTP服务器地址"] = tbFTPHost.Text.Trim();
                            dataRow["FTP文件夹"] = tbFTPFolder.Text.Trim();
                            dataRow["FTP账号"] = tbFTPUserName.Text.Trim();
                            dataRow["FTP密码"] = tbFTPPwd.Text.Trim();
                            dataRow["状态"] = cbEnable.Checked ? "启用" : "禁用";
                            dataTableMemory.Rows.Add(dataRow);
                        }
                        dataTableMemory.WriteXml(configpath);
                        GetData();
                    }
                    else
                    {
                        if (dgvConfigs.CurrentRow != null)
                        {
                            dgvConfigs.CurrentRow.Cells["本地文件夹"].Value = tbLocalFolder.Text.Trim();
                            dgvConfigs.CurrentRow.Cells["FTP服务器地址"].Value = tbFTPHost.Text.Trim();
                            dgvConfigs.CurrentRow.Cells["FTP文件夹"].Value = tbFTPFolder.Text.Trim();
                            dgvConfigs.CurrentRow.Cells["FTP账号"].Value = tbFTPUserName.Text.Trim();
                            dgvConfigs.CurrentRow.Cells["FTP密码"].Value = tbFTPPwd.Text.Trim();
                            dgvConfigs.CurrentRow.Cells["状态"].Value = cbEnable.Checked ? "启用" : "禁用";

                            DataRow[] dataRows = dataTableMemory.Select($"序号={dgvConfigs.CurrentRow.Cells["序号"].Value.ToString()}");
                            foreach (DataRow dataRow in dataRows)
                            {
                                dataRow["本地文件夹"] = tbLocalFolder.Text.Trim();
                                dataRow["FTP服务器地址"] = tbFTPHost.Text.Trim();
                                dataRow["FTP文件夹"] = tbFTPFolder.Text.Trim();
                                dataRow["FTP账号"] = tbFTPUserName.Text.Trim();
                                dataRow["FTP密码"] = tbFTPPwd.Text.Trim();
                                dataRow["状态"] = cbEnable.Checked ? "启用" : "禁用";
                            }
                            dataTableMemory.WriteXml(configpath);
                            GetData();
                        }
                    }
                }
                MessageBox.Show("保存成功！");
                DoUploadTask();
            }
            catch (Exception error)
            {
                MessageBox.Show($"保存失败，{error.Message}");
            }
        }

        private void DgvConfigs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvConfigs.CurrentRow != null)
            {
                selectRowIndex = dgvConfigs.CurrentRow.Index;
                try
                {
                    this.tbLocalFolder.Text = dgvConfigs.CurrentRow.Cells["本地文件夹"].Value.ToString();
                    this.tbFTPHost.Text = dgvConfigs.CurrentRow.Cells["FTP服务器地址"].Value.ToString();
                    this.tbFTPFolder.Text = dgvConfigs.CurrentRow.Cells["FTP文件夹"].Value.ToString();
                    this.tbFTPUserName.Text = dgvConfigs.CurrentRow.Cells["FTP账号"].Value.ToString();
                    this.tbFTPPwd.Text = dgvConfigs.CurrentRow.Cells["FTP密码"].Value.ToString();
                    this.cbEnable.Checked = dgvConfigs.CurrentRow.Cells["状态"].Value.ToString() == "启用" ? true : false;
                }
                catch (Exception error)
                {
                    MessageBox.Show($"配置文件加载失败，错误信息：{error.Message}");
                }
            }
        }
        #endregion

        #region WatcherFile
        private void UploadFile(string sourcePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(sourcePath);//get fileinfo object for lastwritetime
                if (Path.GetExtension(sourcePath).ToLower() == ".exe" || Path.GetExtension(sourcePath).ToLower() == ".bat")
                {
                    log.Warn($"{sourcePath}不允许上传！");
                    return;
                }
                Mapping mapping = GetMappingByLocalFolder(sourcePath);//get mapping by sourcePath,note!! source path must be unique
                using (FtpClient ftpClient = new FtpClient())
                {
                    ftpClient.Host = mapping.FtpHost;
                    ftpClient.Credentials = new NetworkCredential(mapping.FtpUserName, mapping.FtpPwd);
                    string sourceFileName = Path.GetFileName(sourcePath);//filename with extension
                    string targetFileName = string.IsNullOrEmpty(mapping.FtpFolder) ? $"{sourceFileName}" : $"{mapping.FtpFolder}/{sourceFileName}";//root folder is ""
                    if (ftpClient.FileExists(targetFileName))
                    {
                        //if ftp folder had the same filename,then modify source filename with new filename
                        targetFileName = $"{mapping.FtpFolder}/{Path.GetFileNameWithoutExtension(sourceFileName)}{fileInfo.LastWriteTime.ToString("yyyyMMddHHmmssfff")}{Path.GetExtension(sourceFileName)}";
                    }
                    using (Stream istream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read), ostream = ftpClient.OpenWrite(targetFileName))
                    {
                        byte[] buf = new byte[8192];
                        int read = 0;
                        while ((read = istream.Read(buf, 0, buf.Length)) > 0)
                        {
                            ostream.Write(buf, 0, read);
                        }
                    }
                    ftpClient.SetModifiedTime(targetFileName, fileInfo.LastWriteTime.AddHours(-8));
                    string msg = $"{sourcePath}上传成功，{mapping.FtpHost}/{targetFileName}";
                    log.Info(msg);
                    this.notifyIcon.ShowBalloonTip(5000, "提示", $"{sourcePath}上传成功！", ToolTipIcon.Info);
                }
            }
            catch (Exception error)
            {
                log.Error($"{sourcePath}上传失败，错误原因：{error.Message}");
                MessageBox.Show($"{DateTime.Now.ToString()}:{sourcePath}上传失败，错误原因：{error.Message}");
                this.notifyIcon.ShowBalloonTip(5000, "提示", $"{sourcePath}上传失败！", ToolTipIcon.Info);
            }
        }

        private void WatcherStart(string path, string filter)
        {
            FileSystemSafeWatcher watcher;
            if (watchers.ContainsKey(path))
            {
                watcher = watchers[path];
            }
            else
            {
                watcher = new FileSystemSafeWatcher
                {
                    Path = path,
                    Filter = filter
                };
                watcher.Changed += new FileSystemEventHandler(OnProcess);
                watcher.Created += new FileSystemEventHandler(OnProcess);
                watcher.EnableRaisingEvents = true;
                watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size;
                watcher.IncludeSubdirectories = true;
                watchers.Add(path, watcher);
            }
        }

        private void OnProcess(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                OnCreated(source, e);
            }
            else if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                OnChanged(source, e);
            }
        }
        private void OnCreated(object source, FileSystemEventArgs e)
        {
            this.UploadFile(e.FullPath);
        }
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            this.UploadFile(e.FullPath);
        }

        private void ShowLog(string msg)
        {
            string log = $"{tbLog.Text}\\r\\n{msg}";
            if (log.Length > 4000)
            {
                tbLog.Text = msg;
            }
            else
            {
                tbLog.Text = log;
            }
        }

        #endregion

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示    
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点
                this.Activate();
                //任务栏区显示图标
                this.ShowInTaskbar = true;
                //托盘区图标隐藏
                notifyIcon.Visible = false;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //判断是否选择的是最小化按钮
            if (WindowState == FormWindowState.Minimized)
            {
                //隐藏任务栏区图标
                this.ShowInTaskbar = false;
                //图标显示在托盘区
                notifyIcon.Visible = true;
                this.notifyIcon.ShowBalloonTip(5000, "提示","分析仪器报告上传软件已经最小化至托盘运行！", ToolTipIcon.Info);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
        }

        private void lbLogTxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateLog();
        }

        private void UpdateLog()
        {
            using (FileStream fs = new FileStream($"{lbLogTxt.SelectedItem.ToString()}", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                string line = sr.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    tbLog.Text = string.Empty;
                }
                while (line != null)
                {
                    tbLog.Text = $"{tbLog.Text}{line}\r\n";
                    line = sr.ReadLine();
                }
            }
        }
    }
}
