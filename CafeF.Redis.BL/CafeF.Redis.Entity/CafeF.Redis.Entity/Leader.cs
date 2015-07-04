using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region Leader Models
    /// <summary>
    /// Ban lãnh đạo
    /// </summary>
    [Serializable]
    //key format: "LeaderAndOwner:{0}:Symbol" / "leaderid:{0}:Object"
    public class Leader //Thông tin lãnh đạo - Gắn vào đối tượng CompanyProfile
    {
        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Chức vụ
        /// </summary>
        public string Positions { get; set; }
        /// <summary>
        /// =1:Hội đồng quản trị, =2: Ban giám đốc, = 3: Ban kiểm soát
        /// </summary>
        public string GroupID { get; set; }
    }
   //[Serializable]
   // public class LeaderGroup
   //{
   //    public LeaderGroup(){ Leaders = new List<Leader>();}
   //    public string Name { get; set; }
   //    public string Order { get; set; }
   //    public List<Leader> Leaders { get; set; }
   //}
	#endregion	
	
}