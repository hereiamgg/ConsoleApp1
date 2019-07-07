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

namespace DIMSClient
{
    public partial class Form1 : Form
    {
        static readonly string configpath = $"{Environment.CurrentDirectory}/Configs.xml";
        static DataTable dataTableMemory = null;
        static int selectRowIndex = -1;//global var for select row of datagridview
        static ILog log = log4net.LogManager.GetLogger("App.Logging");

        public Form1()
        {
            InitializeComponent();
            GetData();
            InitEditPanel();
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
                    conn.Host = tbFTPFolder.Text.Trim();
                    conn.Credentials = new NetworkCredential(tbFTPUserName.Text.Trim(), tbFTPPwd.Text.Trim());
                    conn.Connect();
                    MessageBox.Show("FTP连接测试成功！");
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
                this.cbEnable.Checked = dgvConfigs.Rows[0].Cells["启用"].Value.ToString() == "是" ? true : false;
            }
        }

        static private Mapping GetMappingByLocalFolder(string localFolder)
        {
            Mapping mapping = null;
            DataRow[] dataRows = dataTableMemory.Select($"本地文件夹={localFolder}");
            if (dataRows.Length > 0)
            {
                mapping = new Mapping(dataRows[0]["序号"].ToString(), dataRows[0]["本地文件夹"].ToString(), dataRows[0]["FTP服务器地址"].ToString(), dataRows[0]["FTP文件夹"].ToString(), dataRows[0]["FTP账号"].ToString(), dataRows[0]["FTP密码"].ToString(), dataRows[0]["状态"].ToString());
            }
            return mapping;
        }
        private void BtnGetData_Click(object sender, EventArgs e)
        {
            this.GetData();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataTableMemory != null)
            {
                dataTableMemory.Rows[dgvConfigs.CurrentRow.Index].Delete();
                dataTableMemory.WriteXml(configpath);
                GetData();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
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
                        dataRow["启用"] = cbEnable.Checked ? "启用" : "禁用";
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
                        dataRow["启用"] = cbEnable.Checked ? "启用" : "禁用";
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
                        dgvConfigs.CurrentRow.Cells["启用"].Value = cbEnable.Checked ? "启用" : "禁用";

                        DataRow[] dataRows = dataTableMemory.Select($"序号={dgvConfigs.CurrentRow.Cells["序号"].Value.ToString()}");
                        foreach (DataRow dataRow in dataRows)
                        {
                            dataRow["本地文件夹"] = tbLocalFolder.Text.Trim();
                            dataRow["FTP服务器地址"] = tbFTPHost.Text.Trim();
                            dataRow["FTP文件夹"] = tbFTPFolder.Text.Trim();
                            dataRow["FTP账号"] = tbFTPUserName.Text.Trim();
                            dataRow["FTP密码"] = tbFTPPwd.Text.Trim();
                            dataRow["启用"] = cbEnable.Checked ? "启用" : "禁用";
                        }
                        dataTableMemory.WriteXml(configpath);
                        GetData();
                    }
                }
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
                    this.cbEnable.Checked = dgvConfigs.CurrentRow.Cells["启用"].Value.ToString() == "是" ? true : false;
                }
                catch (Exception error)
                {
                    MessageBox.Show($"配置文件加载失败，错误信息：{error.Message}");
                }
            }
        }
        #endregion

        #region WatcherFile
        private static void Uploadfile(FtpClient ftpClient,string sourcePath)
        {
            try
            {
                using (FtpClient conn = new FtpClient())
                {
                    conn.Host = "10.116.45.146";
                    conn.Credentials = new NetworkCredential("ftpuser", "1qsxdr5thnm,./");
                    string sourceFile1 = Path.GetFileName(sourcePath);
                    FileInfo fi = new FileInfo(sourcePath);

                    //check filename is used in ftp path
                    if (conn.FileExists(targetPath + sourceFile1))
                    {
                        sourceFile1 = $"{Path.GetDirectoryName(sourceFile1)}{Path.GetFileNameWithoutExtension(sourceFile1)}{fi.LastWriteTime.ToString("yyyyMMddHHmmssfff")}{Path.GetExtension(sourceFile1)}";
                    }

                    using (Stream istream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read), ostream = conn.OpenWrite(targetPath + sourceFile1))
                    {

                        byte[] buf = new byte[8192];
                        int read = 0;

                        while ((read = istream.Read(buf, 0, buf.Length)) > 0)
                        {
                            ostream.Write(buf, 0, read);
                        }
                    }
                    conn.SetModifiedTime(targetPath + sourceFile1, fi.LastWriteTime.AddHours(-8), FtpDate.Original);
                    log.Info($"{DateTime.Now.ToString()}:{sourcePath}上传到{}");
                }
            }
            catch (Exception error)
            {
                log.Error($"{DateTime.Now.ToString()}:{sourcePath}上传失败，错误原因：{error.Message}");
            }
        }

        private static void WatcherStart(string path, string filter)
        {
            FileSystemSafeWatcher watcher = new FileSystemSafeWatcher();
            watcher.Path = path;
            watcher.Filter = filter;
            watcher.Changed += new FileSystemEventHandler(OnProcess);
            watcher.Created += new FileSystemEventHandler(OnProcess);
            watcher.EnableRaisingEvents = true;
            watcher.NotifyFilter -= NotifyFilters.Attributes;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size;
            watcher.IncludeSubdirectories = true;
        }

        private static void OnProcess(object source, FileSystemEventArgs e)
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
        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            Uploadfile(e.FullPath);
        }
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Uploadfile(e.FullPath);
        }
        #endregion
    }
}
