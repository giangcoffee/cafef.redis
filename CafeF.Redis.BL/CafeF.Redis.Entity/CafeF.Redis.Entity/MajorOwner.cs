using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region MajorOwner Models
    [Serializable]
    //key format: "MajorOwner:{0}:Symbol" / "majorownerid:{0}:Object"
    public class MajorOwner //Thông tin chủ sở hữu - Gắn vào đối tượng CompanyProfile
    {
        /// <summary>
        /// Tên cổ đông
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Số cổ phiếu
        /// </summary>
        public double Volume { get; set; }
        /// <summary>
        /// Tỉ lệ
        /// </summary>
        public double Rate { get; set; }
        /// <summary>
        /// Tính đến ngày
        /// </summary>
        public DateTime ToDate { get; set; }
    }
   
	#endregion	
	
}