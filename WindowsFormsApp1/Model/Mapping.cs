using System;
using System.Collections.Generic;
using System.Text;

namespace DIMSClient.Model
{
    public class Mapping
    {
        private string number, localFolder,ftpHost, ftpFolder, ftpUserName, ftpPwd, enable;

        public Mapping(string number, string localFolder,string ftpHost, string ftpFolder, string ftpUserName, string ftpPwd, string enable)
        {
            this.number = number;
            this.localFolder = localFolder;
            this.ftpHost = ftpHost;
            this.ftpFolder = ftpFolder;
            this.ftpUserName = ftpUserName;
            this.ftpPwd = ftpPwd;
            this.enable = enable;
        }

        /// <summary>
        /// 序号
        /// </summary>
        public string Number { get => number; set => number = value; }
        /// <summary>
        /// 本地文件夹
        /// </summary>
        public string LocalFolder { get => localFolder; set => localFolder = value; }
        /// <summary>
        /// FTP文件夹
        /// </summary>
        public string FtpFolder { get => ftpFolder; set => ftpFolder = value; }
        /// <summary>
        /// FTP用户名
        /// </summary>
        public string FtpUserName { get => ftpUserName; set => ftpUserName = value; }
        /// <summary>
        /// FTP密码
        /// </summary>
        public string FtpPwd { get => ftpPwd; set => ftpPwd = value; }
        /// <summary>
        /// 状态：启用\禁用
        /// </summary>
        public string Enable { get => enable; set => enable = value; }
        public string FtpHost { get => ftpHost; set => ftpHost = value; }
    }
}
