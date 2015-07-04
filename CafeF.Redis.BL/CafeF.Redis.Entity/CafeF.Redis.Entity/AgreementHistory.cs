using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CafeF.Redis.Entity
{
    #region AgreementHistory Models
    [Serializable]
    //key format: "stockid:{0}:agreementhistory:{1}:Object ~ :Symbol:Trading_Date(yyyymmdd)" / "stockid:{0}:agreementhistory:{1}:Object"
	public class AgreementHistory//Lịch sủ giao dịch thỏa thuận - dùng trong trang xem lịch sử giao dịch
	{
        public AgreementHistory()
		{
            Trading_Date = DateTime.Now;
		}
        //public string Key { get; set; } // key của object này là Symbol và TradeDate
        public string Symbol { get; set; }
        public DateTime Trading_Date { get; set; }
        public double Trans_Volumn { get; set; }
        public double Trans_Value { get; set; }
       
	}
	#endregion	
	
}