using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
	#region Stock Models	
    [Serializable]
    //key format: "Stock:StockID:{0}:Symbol" / "stock:stockid:{0}:Object" ~symbol
	public class Stock  //Đối tượng này dùng trong trang view Stock
    {
        public Stock()
		{
            this.StockPriceHistory = new StockCompactHistory();
            this.Reports3 = new List<Reports>();
            this.BusinessPlans1 = new List<BusinessPlan>();
            //this.InSameCategorys = new List<string>();
            this.SameCategory = new List<StockShortInfo>();
            this.SameEPS = new List<StockShortInfo>();
            this.SamePE = new List<StockShortInfo>();
            this.DividendHistorys = new List<DividendHistory>();
            this.StockNews = new List<StockNews>();
            this.PrevTradeInfo = new List<StockFirstInfo>();
		}

        //public string Key { get; set; } // key của object này là Symbol
        #region basic info
        public int TradeCenterId { get; set; }
		public string Symbol { get; set; }
        public bool IsDisabled { get; set; }
        public string StatusText { get; set; }
        public bool ShowTradeCenter { get; set; }
        public string FolderImage { get; set; }
        public bool IsBank { get; set; }
        public bool IsCCQ { get; set; }
        public string FirstTradeText { get; set; }
        #endregion

        public List<StockFirstInfo> PrevTradeInfo { get; set; }
        public CompanyProfile CompanyProfile { get; set; }
        //public List<StockHistory> StockHistorys10 { get; set; }
        public StockCompactHistory StockPriceHistory { get; set; }
        public List<Reports> Reports3 { get; set; } 
        public List<BusinessPlan> BusinessPlans1 { get; set; }

        public List<StockShortInfo> SameCategory { get; set; }
        public List<StockShortInfo> SameEPS { get; set; }
        public List<StockShortInfo> SamePE { get; set; }

        public List<DividendHistory> DividendHistorys { get; set; }
        public List<StockNews> StockNews { get; set; }
        
	}
    
    /// <summary>
    /// Đối tượng chứa các thông tin cơ bản : tên, sàn, EPS, folder, ....
    /// </summary>
    [Serializable]
    public class StockCompactInfo
    {
        public StockCompactInfo(){}
        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
        public int TradeCenterId { get; set; }
        public string CompanyName { get; set; }
        public double EPS { get; set; }
        public string FolderChart { get; set; }
        public bool ShowTradeCenter { get; set; }
        public bool IsBank { get; set; }
        public bool IsCCQ { get; set; }
    }

    /// <summary>
    /// Thông tin cơ bản của cổ phiếu - Dùng trong phần danh sách hoặc tra cứu
    /// </summary>
    [Serializable]
    public class StockCompact
    {
        public StockCompact(){}
        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Mã sàn
        /// </summary>
        public int TradeCenterId { get; set; }
        /// <summary>
        /// Mã ngành
        /// </summary>
        public int CategoryId { get; set; }
    }

    [Serializable]
    public class TopStock
    {
        public TopStock(){}
        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Giá phiên gần nhất
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Giá tham chiếu phiên gần nhất
        /// </summary>
        public double BasicPrice { get; set; }
        public double ChangePrice { get { return Price - BasicPrice; } }
        /// <summary>
        /// Khối lượng giao dịch phiên gần nhất
        /// </summary>
        public double Volume { get; set; }
        /// <summary>
        /// EPS
        /// </summary>
        public double EPS { get; set; }
        /// <summary>
        /// PE = Giá / EPS
        /// </summary>
        public double PE { get; set; }
        /// <summary>
        /// Vốn hóa thị trường
        /// </summary>
        public double MarketCap { get; set; }
    }

	#endregion	

	[Serializable]
    public class StockFirstInfo
    {
        public StockFirstInfo()
        {
            
        }

        public string Symbol { get; set; }
        public string Floor { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime EndDate { get; set; }
        public double FirstPrice { get; set; }
        public double FirstVolume { get; set; }
    }
}