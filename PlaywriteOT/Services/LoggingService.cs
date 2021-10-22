using PlaywriteOT.Models;
using PlaywriteOT.Utilities;
using System;
using System.IO;

namespace PlaywriteOT.Services
{
    public static class LoggingService
    {
        
        static LoggingService() { }

        /// <summary>
        /// Writes log entry to text file
        /// </summary>
        /// <param name="log"></param>
        public static void WriteLog(Log log)
        {
            using StreamWriter sw = File.AppendText("logs.txt");  //append to log file
            sw.WriteLine(log.ToString());
            sw.Close();
        }

        /*
        //store the logs from the text file in a list
        public static void FetchLogs()
        {
            var file = File.ReadAllLines("logs.txt");
            Log log;

            //read each line in the file
            foreach(string s in file)
            {                
                string []data = s.Trim().Split(" - ");

                log = new Log
                {
                    LogDateTime = Convert.ToDateTime(data[0]),
                    LogLevel = data[1],
                    LogLocation = data[2],
                    LogMessage = data[3]
                };

                LogHistory.Logs.Add(log);
            }

        }
        */
    }
}