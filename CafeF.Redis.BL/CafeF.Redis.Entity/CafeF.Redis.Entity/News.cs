using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region News Models
    [Serializable]
    //key format: "News:{0}:{1}:{2}:Symbol:TypeID:Incre" / "newsid:{0}:{1}:{2}:Object"
    public class News //Thông tin tin tức - Dùng trong trang Stock và News
    {
        //public string Key { get; set; } // key của object này là Symbol và Date va IncreID
        public string Symbol { get; set; }
        public DateTime DateDeploy { get; set; }
        public string Title{ get; set; }
        public string Body { get; set; }
        public int TypeID { get; set; }
    }
   
	#endregion	
	
}