using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region CommonInfo Models
    /// <summary>
    /// Thông tin chung
    /// </summary>
    [Serializable]
    //key format: "CommonInfo:{0}:Symbol" / "commoninfoid:{0}:Object"
    public class CommonInfo //Thông tin chung - Dùng trong trang Stock
    {
        //public string Key { get; set; } // key của object này là Symbol
        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Giới thiệu
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Danh mục
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Vốn điều lệ
        /// </summary>
        public double Capital { get; set; }
        /// <summary>
        /// Tổng số CP niêm yết
        /// </summary>
        public double TotalVolume { get; set; }
        /// <summary>
        /// Tổng số CP lưu hành
        /// </summary>
        public double OutstandingVolume { get; set; }
        /// <summary>
        /// Tên công ty kiểm toán
        /// </summary>
        public string AuditFirmName { get; set; }
        /// <summary>
        /// Website công ty kiểm toán
        /// </summary>
        public string AuditFirmSite { get; set; }
        /// <summary>
        /// Tổ chức tư vấn niêm yết
        /// </summary>
        public string ConsultantName { get; set; }
        /// <summary>
        /// Website tổ chức tư vấn niêm yết
        /// </summary>
        public string ConsultantSite { get; set; }
        /// <summary>
        /// Đăng ký kinh doanh
        /// </summary>
        public string BusinessLicense { get; set; }
        /// <summary>
        /// Mã CK của tổ chức tư vấn niêm yết
        /// </summary>
        public string ConsultantSymbol { get; set; }
    }
    
	#endregion	
	
}