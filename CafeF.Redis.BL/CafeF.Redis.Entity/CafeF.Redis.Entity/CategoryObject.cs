using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region CategoryObject Models
    [Serializable]
    //key format: "CategoryObject:{0}:ID" / "categoryobjectid:{0}:Object"
    public class CategoryObject //Thông tin nhóm ngành - Dùng để phân loại Stock thuộc nhóm nào
    {
        //public string Key { get; set; } // key của object này là ID
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order{ get; set; }
        public Boolean IsHidden { get; set; }
        public string Icon { get; set; }
        public List<int> IDs { get; set; } 
    }
	#endregion	
	
}