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

namespace CafeF.Redis.UpdateService
{
    public partial class UpdateService : ServiceBase
    {
        #region Properties

        private readonly LogUtils log;
        private readonly LogType logType = (ConfigurationManager.AppSettings["LogType"] ?? "text") == "text" ? LogType.TextLog : LogType.EventLog;
        private readonly bool logEnable = (ConfigurationManager.AppSettings["LogEnable"] ?? "true").ToLower() == "true";
        private readonly string logPath = String.IsNullOrEmpty(ConfigurationManager.AppSettings["LogPath"]) ? AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"log\" : ConfigurationManager.AppSettings["LogPath"].Replace(@"~\", AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
        private PriceData[] priceWorkers;
        private StockData[] stockWorkers;
        private Thread[] workerThreads;
        private StockData ceoWorker;
        private Thread ceoThread;
        #endregion

        public UpdateService()
        {
            InitializeComponent();
            log = logType == LogType.TextLog ? new LogUtils(logType, logEnable, logPath) : new LogUtils(logType, logEnable, eventLog1);
            log.Enabled = logEnable;
            log.LogPath = logPath;
        }

        protected override void OnStart(string[] args)
        {
            log.WriteEntry("Started", EventLogEntryType.Information);
            var noPrice = int.Parse(ConfigurationManager.AppSettings["NoPriceThreads"] ?? "1");
            var noStock = int.Parse(ConfigurationManager.AppSettings["NoStockThreads"] ?? "3");
            workerThreads = new Thread[noPrice + noStock + 4];
            priceWorkers = new PriceData[noPrice + 3];
            stockWorkers = new StockData[noStock + 1];

            for (var i = 0; i < noPrice; i++)
            {
                priceWorkers[i] = new PriceData(i + 1, eventLog1) { ServiceStarted = true };
                workerThreads[i] = new Thread(priceWorkers[0].ExecuteTask);
                workerThreads[i].Start();
            }

            for (var i = noPrice; i < noPrice + noStock; i++)
            {
                stockWorkers[i - noPrice] = new StockData(i + 1, eventLog1) { ServiceStarted = true };
                workerThreads[i] = new Thread(stockWorkers[i - noPrice].ExecuteTask);
                workerThreads[i].Start();
            }

            //Bao cao tai chinh
            stockWorkers[noStock] = new StockData(noPrice + noStock, eventLog1) { ServiceStarted = true };
            workerThreads[noPrice + noStock] = new Thread(stockWorkers[noStock].GetBCTC);
            workerThreads[noPrice + noStock].Start();
            //Giao dich co phieu quy
            priceWorkers[noPrice] = new PriceData(noPrice + noStock + 1, eventLog1) { ServiceStarted = true };
            workerThreads[noPrice + noStock + 1] = new Thread(priceWorkers[noPrice].UpdateFundTransaction);
            workerThreads[noPrice + noStock + 1].Start();
            //Crawler box hang hoa
            priceWorkers[noPrice + 1] = new PriceData(noPrice + noStock + 2, eventLog1) { ServiceStarted = true };
            workerThreads[noPrice + noStock + 2] = new Thread(priceWorkers[noPrice + 1].UpdateBoxHangHoa);
            workerThreads[noPrice + noStock + 2].Start();
            //Update top stock
            priceWorkers[noPrice + 2] = new PriceData(noPrice + noStock + 3, eventLog1) { ServiceStarted = true };
            workerThreads[noPrice + noStock + 3] = new Thread(priceWorkers[noPrice + 2].UpdateTopPrice);
            workerThreads[noPrice + noStock + 3].Start();
            //Update ceo image
            ceoWorker = new StockData(1, eventLog1) {ServiceStarted = true};
            ceoThread = new Thread(ceoWorker.UpdateCeoImage);
            ceoThread.Start();
        }

        protected override void OnStop()
        {
            var noPrice = int.Parse(ConfigurationManager.AppSettings["NoPriceThreads"] ?? "1");
            var noStock = int.Parse(ConfigurationManager.AppSettings["NoStockThreads"] ?? "3");
            for (var i = 0; i < noPrice; i++)
            {
                priceWorkers[i].ServiceStarted = false;
                workerThreads[i].Join(5000);
            }

            for (var i = noPrice; i < noPrice + noStock; i++)
            {
                stockWorkers[i - noPrice].ServiceStarted = false;
                workerThreads[i].Join(5000);
            }

            stockWorkers[noStock].ServiceStarted = false;
            workerThreads[noPrice + noStock].Join(5000);

            priceWorkers[noPrice].ServiceStarted = false;
            workerThreads[noPrice + noStock + 1].Join(5000);

            priceWorkers[noPrice + 1].ServiceStarted = false;
            workerThreads[noPrice + noStock + 2].Join(5000);

            priceWorkers[noPrice + 2].ServiceStarted = false;
            workerThreads[noPrice + noStock + 2].Join(5000);

            ceoWorker.ServiceStarted = false;
            ceoThread.Join(5000);

            log.WriteEntry("Stopped", EventLogEntryType.Information);
        }
    }
}
