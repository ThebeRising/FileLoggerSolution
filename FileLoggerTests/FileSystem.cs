using FileLoggerKata;
using NUnit.Framework;
using System;
using System.IO;


namespace FileLoggerTests
{
    public class FileSystem : IFileSystem
    {

        private FileStream writer;
        public void Append(string path, string messsage)
        {

            if (!(new FileInfo(path).Length == 0))
            {
                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteAsync(messsage);

                    w.Close();
                }
            }
            else
                _ = WriteToLog(path, messsage);



        }

        private static async System.Threading.Tasks.Task WriteToLog(string path, string message)
        {

            using (StreamWriter w = File.AppendText(path))
            {
                await w.WriteAsync(message);
            }
      
        }

        public void Create(string path)
        {
            if (path != "")
            {
                writer = File.Create(path);
                writer.Close();
            }


        }

        public bool Exists(string path)
        {
            if (path != "")
            {
                return File.Exists(path);
            }

            return false;
        }

        public void Rename(string currentPath, string newPath)
        {
            if (currentPath != "" && newPath != "")
            {
                File.Move(currentPath, newPath);
            }
        }

        public DateTime GetLastWriteTime(string path)
        {
            if (path != "")
            {
                return File.GetLastWriteTime(path);
            }

            return DateTime.MinValue;
        }

    }
}
