using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LightweightPhotoSuite
{
    class Logger
    {
        private static string logFolderPath;
        private static string logFileFullPath;
        private static Timer fileWriteTimer;

        private static Queue<string> entriesToWrite;

        public static void init()
        {
            DateTime dateTime = DateTime.Now;
            string timestamp = dateTime.Year.ToString() + '-' + dateTime.Month.ToString() + '-' + dateTime.Day.ToString() + '_' + dateTime.Hour.ToString() + ':' + dateTime.Minute.ToString() + ':' + dateTime.Second.ToString();
            logFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            logFileFullPath = logFolderPath + @"\" + timestamp + "_LPS-log.txt";
            fileWriteTimer = new Timer(2000);
            fileWriteTimer.Elapsed += write;
        }

        public static void log(string str)
        {
            DateTime dateTime = DateTime.Now;
            string timestamp = dateTime.Hour.ToString() + ':' + dateTime.Minute.ToString() + ':' + dateTime.Second.ToString() + ' ';

            lock (entriesToWrite)
            {
                entriesToWrite.Enqueue(timestamp + str);
            }
        }

        private static void write(object sender, ElapsedEventArgs e)
        {
            lock (entriesToWrite)
            {
                using (StreamWriter sw = new StreamWriter(logFileFullPath))
                {
                    while (entriesToWrite.Peek() != null)
                    {
                        sw.WriteLine(entriesToWrite.Dequeue());
                    }
                }
            }
        }
    }
}
