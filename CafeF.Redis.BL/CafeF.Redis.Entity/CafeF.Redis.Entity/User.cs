using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region User Models
    [Serializable]
    //key format: "User:{0}:ID" / "userid:{0}:Object"
    public class User//Người dùng  - Dùng trong trang User, danh mục đầu tư
    {
       // public string Key { get; set; } // key của object này là ID
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Symbols { get; set; }
        public List<string> UserCategoryConfigs { get; set; }
       
    }
    
	#endregion	
	
}