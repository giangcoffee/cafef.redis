using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region StockNews Models
    /// <summary>
    /// Tin tức doanh nghiệp
    /// </summary>
    [Serializable]
    //key format: "stockid:{0}:stocknews:List ~Symbol" / "stockid:{0}:stocknews:List"
    public class StockNews //Thông tin tin tức - Dùng trong trang Stock và News
    {
        public StockNews()
		{
            DateDeploy = DateTime.Now;
        }
        //public string Key { get; set; } // 
        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// ID tin
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Ngày phát hành
        /// </summary>
        public DateTime DateDeploy { get; set; }
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title{ get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Loại tin
        /// </summary>
        public string TypeID { get; set; }
        /// <summary>
        /// Ảnh bài
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Tóm tắt
        /// </summary>
        public string Sapo { get; set; }
    }
   
	#endregion	
	
}