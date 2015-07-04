using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using CafeF.Redis.UpdateService;

namespace CafeF.Redis.KhopLenh
{
    partial class UpdateData : ServiceBase
    {
        #region Properties

        private readonly LogUtils log;
        private readonly LogType logType = (ConfigurationManager.AppSettings["LogType"] ?? "text") == "text" ? LogType.TextLog : LogType.EventLog;
        private readonly bool logEnable = (ConfigurationManager.AppSettings["LogEnable"] ?? "true").ToLower() == "true";
        private readonly string logPath = String.IsNullOrEmpty(ConfigurationManager.AppSettings["LogPath"]) ? AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"log\" : ConfigurationManager.AppSettings["LogPath"].Replace(@"~\", AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
        private PriceData[] priceWorker;
        private Thread[] priceThread;
        private PriceData dbWorker;
        private Thread dbThread;
        #endregion

        #region Events
        public UpdateData()
        {
            InitializeComponent();
            log = logType == LogType.TextLog ? new LogUtils(logType, logEnable, logPath) : new LogUtils(logType, logEnable, eventLog1);
            log.Enabled = logEnable;
            log.LogPath = logPath;
        }

        protected override void OnStart(string[] args)
        {
            log.WriteEntry("Started", EventLogEntryType.Information);
            priceWorker = new PriceData[3];
            priceThread = new Thread[3];

            priceWorker[0] = new PriceData(eventLog1) { ServiceStarted = true, TradeCenterId = 1 }; //hsx
            priceThread[0] = new Thread(priceWorker[0].ExecuteTask);
            priceThread[0].Start();

            priceWorker[1] = new PriceData(eventLog1) { ServiceStarted = true, TradeCenterId = 2 }; //hnx
            priceThread[1] = new Thread(priceWorker[1].ExecuteTask);
            priceThread[1].Start();

            priceWorker[2] = new PriceData(eventLog1) { ServiceStarted = true, TradeCenterId = 9 }; //upcom
            priceThread[2] = new Thread(priceWorker[2].ExecuteTask);
            priceThread[2].Start();

            dbWorker = new PriceData(eventLog1){ServiceStarted =  true};
            dbThread = new Thread(dbWorker.SaveToDb);
            dbThread.Start();
        }

        protected override void OnStop()
        {
            for (var i = 0; i < 3; i++)
            {
                priceWorker[i].ServiceStarted = false;
                priceThread[i].Join(5000);
            }
            dbWorker.ServiceStarted = false;
            dbThread.Join(5000);
            log.WriteEntry("Stopped", EventLogEntryType.Information);
        }
        #endregion
    }
}
