using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region TypeNews Models
    [Serializable]
    //key format: "TypeNews:{0}:ID" / "typenews:{0}:Object"
    public class TypeNews //Thông tin nhóm tin
    {
        //public string Key { get; set; } // key của object này là ID
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
	#endregion	
	
}