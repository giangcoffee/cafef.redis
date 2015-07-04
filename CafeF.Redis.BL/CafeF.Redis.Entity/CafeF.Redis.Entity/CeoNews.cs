using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    #region Ceo News Models
    /// <summary>
    /// Tin tức doanh nghiệp
    /// </summary>
    [Serializable]
    public class CeoNews //Thông tin tin tức - Dùng trong trang Ceo và News
    {
        public CeoNews()
        {
            PublishDate = DateTime.Now;
        }
        //public string Key { get; set; } // 
        /// <summary>
        /// Mã ceo
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// ID tin
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Ngày phát hành
        /// </summary>
        public DateTime PublishDate { get; set; }
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; }        
        /// <summary>
        /// Ảnh bài
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Tóm tắt
        /// </summary>
        public string Sapo { get; set; }
        public string NewsLink { get; set; }
    }

    #endregion	
}
