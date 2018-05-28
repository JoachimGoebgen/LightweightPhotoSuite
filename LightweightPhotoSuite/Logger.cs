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
        private string logFileFullPath;
        private Timer fileWriteTimer;

        private Queue<string> entriesToWrite;

        public Logger(string logFolderPath, string logFilePath)
        {
            DateTime dateTime = DateTime.Now;
            string timestamp = dateTime.Year.ToString() + '-' + dateTime.Month.ToString() + '-' + dateTime.Day.ToString() + '_' + dateTime.Hour.ToString() + ':' + dateTime.Minute.ToString() + ':' + dateTime.Second.ToString();
            logFileFullPath = logFolderPath + @"\" + timestamp + "_" + logFilePath + ".txt";
            entriesToWrite = new Queue<string>();
            fileWriteTimer = new Timer(2000);
            fileWriteTimer.Elapsed += write;
            fileWriteTimer.Start();
        }

        public void log(string str)
        {
            DateTime dateTime = DateTime.Now;
            string timestamp = dateTime.Hour.ToString() + ':' + dateTime.Minute.ToString() + ':' + dateTime.Second.ToString() + ' ';

            lock (entriesToWrite)
            {
                entriesToWrite.Enqueue(timestamp + str);
            }
        }

        private void write(object sender, ElapsedEventArgs e)
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
