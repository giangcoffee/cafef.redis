using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region InvestorList Models
    [Serializable]
    //key format: "InvestorList:{0}:{1}:{2}:UserID:Symbol:TradeDate" / "investorlistid:{0}:{1}:{2}:Object"
    public class InvestorList//Danh mục đầu tư - Dùng trong trang User
    {
        public InvestorList()
		{
            TradeDate = DateTime.Now;
        }
       // public string Key { get; set; } // key của object này là UserID, Symbol + TradeDate
        public int UserId { get; set; }
        public string Symbol { get; set; }
        public DateTime TradeDate { get; set; }
        public int Type { get; set; }
        public double Volume { get; set; }
        public double Price { get; set; }
        public double Fee { get; set; }
        public double Profit { get; set; }

    } 
    
	#endregion	
	
}