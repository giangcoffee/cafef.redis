using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace CafeF.Redis.UpdateService
{
    public class LogUtils
    {
        public EventLog Log;
        private bool isEnabled = false;
        private LogType logType = LogType.TextLog;
        private string logSource = ConfigurationManager.AppSettings["EventLogSource"] ?? "cafef";
        private string logName = ConfigurationManager.AppSettings["EventLog"] ?? "MyService";
        private string logPath = ConfigurationManager.AppSettings["LogPath"] ?? AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"log\";

        public bool Enabled { get { return isEnabled; } set { isEnabled = value; } }
        public LogType LogType { get { return logType; } set { logType = value; } }
        public string LogSource { get { return logSource; } set { logSource = value; } }
        public string LogName { get { return logName; } set { logName = value; } }
        public string LogPath { get { return logPath; } set { logPath = value; } }

        public LogUtils(LogType type, bool enable, EventLog log)
        {
            isEnabled = enable;
            logType = type;

            if (logType == LogType.EventLog)
            {
                Log = log;
                if (!EventLog.SourceExists(logSource))
                {
                    EventLog.CreateEventSource(logSource, logName);
                }
                Log.Source = logSource;
                Log.Log = logName;
            }
        }
        public LogUtils(LogType type, bool enable, string path)
        {
            isEnabled = enable;
            logType = type;
            if (!string.IsNullOrEmpty(path)) logPath = path;
        }
        public void WriteEntry(string message, EventLogEntryType type)
        {
            if (!isEnabled) return;
            switch (logType)
            {
                case LogType.EventLog:
                    Log.WriteEntry(message, type); break;
                case LogType.TextLog:
                    string filename = "log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                    try
                    {
                        if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
                        File.AppendAllText(logPath + filename, DateTime.Now.ToLongTimeString() + " : " + type + " : " + message + Environment.NewLine);
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(new TimeSpan(0, 0, 2));
                        try
                        {
                            if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
                            File.AppendAllText(logPath + filename, DateTime.Now.ToLongTimeString() + " : " + type + " : " + message + Environment.NewLine);
                        }
                        catch (Exception)
                        {

                        }
                    }
                    break;
                default: break;
            }
        }
    }

    public enum LogType
    {
        EventLog,
        TextLog
    }
    public enum EventType
    {
        Error,
        Notification
    }
}
