using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using FluentFTP;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            WatcherStart(@"D:\temp2", "*.*");
            Console.ReadKey();
        }

        private static void uploadfile(string sourcePath,string targetPath)
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
                conn.SetModifiedTime(targetPath + sourceFile1, fi.LastWriteTime.AddHours(-8),FtpDate.Original);
            }
        }

        private static void WatcherStart(string path, string filter)
        {
            FileSystemSafeWatcher watcher = new FileSystemSafeWatcher();
            watcher.Path = path;
            watcher.Filter = filter;
            watcher.Changed += new FileSystemEventHandler(OnProcess);
            watcher.Created += new FileSystemEventHandler(OnProcess);
            watcher.Deleted += new FileSystemEventHandler(OnProcess);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
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
            else if (e.ChangeType == WatcherChangeTypes.Deleted)
            {
                OnDeleted(source, e);
            }

        }
        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("文件新建事件处理逻辑 {0}  {1}  {2}", e.ChangeType, e.FullPath, e.Name);
            uploadfile(e.FullPath, "/");
        }
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("文件改变事件处理逻辑{0}  {1}  {2}", e.ChangeType, e.FullPath, e.Name);
            uploadfile(e.FullPath, "/");
        }

        private static void OnDeleted(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("文件删除事件处理逻辑{0}  {1}   {2}", e.ChangeType, e.FullPath, e.Name);
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            Console.WriteLine("文件重命名事件处理逻辑{0}  {1}  {2}", e.ChangeType, e.FullPath, e.Name);
        }
    }
}
