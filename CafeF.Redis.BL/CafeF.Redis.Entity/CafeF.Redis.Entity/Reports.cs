using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region Reports Models
    /// <summary>
    /// Báo cáo phân tích
    /// </summary>
    [Serializable]
    //key format: "stockid:{0}:reports:List ~ Symbol" / "stockid:{0}:reports:List"
    public class Reports //Thông tin báo cáo phân tích  - Dùng trong trang Stock
    {
        public Reports()
		{
            DateDeploy = DateTime.Now;
		}
        //public string Key { get; set; } // key của object này là Symbol và ID
        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Mã báo cáo
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Ngày nhập
        /// </summary>
        public DateTime DateDeploy { get; set; }
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// File đính kèm
        /// </summary>
        public FileObject file { get; set; }
        /// <summary>
        /// Mã công ty báo cáo
        /// </summary>
        public string ResourceCode { get; set; }
        /// <summary>
        /// Nguồn gốc
        /// </summary>
        public string ResourceName { get; set; }
        /// <summary>
        /// Link
        /// </summary>
        public string ResourceLink { get; set; }
        /// <summary>
        /// Nổi bật ?
        /// </summary>
        public bool IsHot { get; set; }
        /// <summary>
        /// Ngành
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// Nguồn ID?
        /// </summary>
        public int SourceID { get; set; }


    }
    
	#endregion	
	
}