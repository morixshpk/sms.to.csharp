using System.IO;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace sms.to.csharp.Services
{
    public class Logger
    {
        private static readonly object _sync;

        private static int _versioningNo;

        private static readonly long _maxSize;

        private static DateTime _date;

        private static string _dir;

        private static int _noDays;

        public static string Dir
        {
            get
            {
                return _dir;
            }
            set
            {
                if (!Directory.Exists(value))
                {
                    Directory.CreateDirectory(value);
                    if (!Directory.Exists(value))
                    {
                        throw new Exception("Directory " + value + " can't be created!");
                    }
                }
                _dir = value;
            }
        }

        public static int NoDays
        {
            get
            {
                return _noDays;
            }
            set
            {
                if (value > 0)
                {
                    _noDays = value;
                }
                else
                {
                    _noDays = 30;
                }
            }
        }

        static Logger()
        {
            _sync = new object();
            _versioningNo = 0;
            _maxSize = 50000000L;
            _date = DateTime.Now.Date;
            _dir = "";
            _noDays = 30;
            string path = Uri.UnescapeDataString(new UriBuilder(Directory.GetCurrentDirectory()).Path);
            Dir = Path.Combine(Path.GetDirectoryName(path), "Logs");
            NoDays = 30;
            string path3 = $"{_dir}\\log{DateTime.Now.Ticks}.txt";
            try
            {
                File.AppendAllText(path3, string.Format("Test", Environment.NewLine));
            }
            catch
            {
            }
            if (File.Exists(path3))
            {
                File.Delete(path3);
            }

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    DeleteOldFiles();
                    Thread.Sleep(1000 * 60);
                }
            });
        }

        private static void DeleteOldFiles()
        {
            try
            {
                string[] files = Directory.GetFiles(_dir);
                DateTime dateTime = DateTime.Today.AddDays(-NoDays);
                int num = 0;
                string[] array = files;
                foreach (string text in array)
                {
                    var fileInfo = new FileInfo(text);
                    if (fileInfo.LastAccessTime < dateTime)
                    {
                        fileInfo.Delete();
                        num++;
                    }
                }
            }
            catch { }
        }

        public static void Custom(string filename, string content, bool sameLine = false)
        {
            lock (_sync)
            {
                try
                {
                    if (_date != DateTime.Now.Date)
                    {
                        _versioningNo = 0;
                        _date = DateTime.Now.Date;
                    }
                    if (!Directory.Exists($"{_dir}\\{filename.ToUpper()}"))
                    {
                        Directory.CreateDirectory($"{_dir}\\{filename.ToUpper()}");
                    }
                    string text = $"{_dir}\\{filename.ToUpper()}\\{filename}-{_date:yyyy-MM-dd}-{_versioningNo}.txt";
                    var fileInfo = new FileInfo(text);
                    while (fileInfo.Exists && fileInfo.Length > _maxSize)
                    {
                        _versioningNo++;
                        text = $"{_dir}\\{filename}-{_date:yyyy-MM-dd}-{_versioningNo}.txt";
                        fileInfo = new FileInfo(text);
                        _ = fileInfo.Exists;
                    }
                    var stringBuilder = new StringBuilder();
                    if (sameLine)
                    {
                        stringBuilder.AppendLine(DateTime.Now.ToString("HH:mm:ss.fff") + ":" + content);
                    }
                    else
                    {
                        stringBuilder.AppendLine("----------" + DateTime.Now.ToString("HH:mm:ss.fff") + "----------");
                        stringBuilder.AppendLine(content);
                    }
                    using (StreamWriter streamWriter = File.AppendText(text))
                    {
                        streamWriter.Write(stringBuilder.ToString());
                        streamWriter.Close();
                    }
                }
                catch { }
            }
        }
    }
}
