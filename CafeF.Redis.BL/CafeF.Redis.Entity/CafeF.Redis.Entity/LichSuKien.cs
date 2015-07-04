using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CafeF.Redis.Entity
{
    #region LichSuKien Models
    //key format: "stockid:{0}:lichsukien:List ~ :Symbol" / "stockid:{0}:lichsukien:List"
    /// <summary>
    /// Object dùng trong trang lịch sự kiện
    /// </summary>
    [Serializable]
    public class LichSuKien
    {
        public LichSuKien()
        {
            EventDate = DateTime.Now;
            PostDate = DateTime.Now;
        }
        //public string Key { get; set; } // key của object này là MaCK, NgayBatDau(ngay gd ko huong quyen), LoaiSuKien
        public int ID { get; set; }
        /// <summary>
        /// Mã CK
        /// </summary>
        public string MaCK { get; set; }
        /// <summary>
        /// Ngày bắt đầu(ngay gd ko huong quyen)
        /// </summary>
        public DateTime EventDate { get; set; }
        /// <summary>
        /// Loại sự kiện
        /// </summary>
        public string LoaiSuKien { get; set; }
        /// <summary>
        /// Tên cty ck
        /// </summary>
        public string TenCty { get; set; }
        /// <summary>
        /// Ngày bắt đầu 
        /// </summary>
        public string NgayBatDau { get; set; }
        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public string NgayKetThuc { get; set; }
        /// <summary>
        /// Ngày thực hiện
        /// </summary>
        public string NgayThucHien { get; set; }
        /// <summary>
        /// Mã sản CK
        /// </summary>
        public int MaSan { get; set; }
        /// <summary>
        /// Tóm tắt
        /// </summary>
        public string TomTat { get; set; }
        /// <summary>
        /// Mã tin tức từ bảng tblNews
        /// </summary>
        public string News_ID { get; set; }
        /// <summary>
        /// Title tin
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Ngày đăng
        /// </summary>
        public DateTime PostDate { get; set; }
    }
    #endregion

    
}
