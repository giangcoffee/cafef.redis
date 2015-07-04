using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region UserCategoryConfig Models
    [Serializable]
    //key format: "UserCategoryConfig:{0}:ID" / "usercategoryconfigid:{0}:Object"
    public class UserCategoryConfig //Thông tin nhóm ngành - Dùng trong trang User, danh mục đầu tư, gắn với User
    {
        //public string Key { get; set; } // key cua object này là Name
        public string Name { get; set; }
        public string Description { get; set; }
    }
    
	#endregion	
	
}