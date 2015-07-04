using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region FirstInfo Models
    [Serializable]
    //key format: "FirstInfo:{0}:Symbol" / "firstinfoid:{0}:Object"
    public class FirstInfo //Thong tin co ban ban dau - Gắn vào đối tượng BasicInfo
    {
        public FirstInfo()
		{
            FirstTrade = DateTime.Now;
            EndTrade = DateTime.Now;
        }
        //public string Key { get; set; } // key cua object này là Symbol
        public string Symbol { get; set; }
        public DateTime? FirstTrade { get; set; }
        public DateTime? EndTrade { get; set; }
        public double FirstPrice { get; set; }
        public double FirstVolume { get; set; }
    }
    
	#endregion	
	
}