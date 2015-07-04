using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region LeaderAndOwner Models
    [Serializable]
    //key format: "LeaderAndOwner:{0}:Symbol" / "leaderandownerid:{0}:Object"
    public class LeaderAndOwner //Thông tin lãnh đạo và chủ sở hữu - Gắn vào đối tượng CompanyProfile
    {
        //public string Key { get; set; } // key của object này là Symbol
        public string Symbol { get; set; }
        public string CategoryName { get; set; }
        public string Content { get; set; }
    }
   
	#endregion	
	
}