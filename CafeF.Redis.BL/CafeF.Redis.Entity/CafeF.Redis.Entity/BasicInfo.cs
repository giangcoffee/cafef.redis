using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeF.Redis.Entity
{
    #region BasicInfo Models
    [Serializable]
    //key format: "BasicInfo:{0}:Symbol" / "basicinfoid:{0}:Object"
    public class BasicInfo //Thông tin công ty cơ bản - Dùng trong trang Stock 
    {
       // public string Key { get; set; } // key của object này là Symbol
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string TradeCenter { get; set; }
        public CategoryObject category { get; set; }
        public FirstInfo firstInfo { get; set; }
        public BasicCommon basicCommon { get; set; }
    }
    
	#endregion	
	
}