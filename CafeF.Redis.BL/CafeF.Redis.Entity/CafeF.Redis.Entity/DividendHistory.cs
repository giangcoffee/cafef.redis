using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeF.Redis.Entity
{
    #region DividendHistory Models
    /// <summary>
    /// Lịch trả cổ tức và chia thưởng
    /// </summary>
    [Serializable]
    //key format: "DividendHistory:{0}:{1}:Symbol:EffectiveDate" / "dividenhistoryid:{0}:{1}:Object"
    public class DividendHistory //Lịch sử chia tách thưởng - Dùng trong trang Stock
    {
        public DividendHistory()
		{
            //ExRightsDate = DateTime.Now;
            //FixDate = DateTime.Now;
            //EffectiveDate = DateTime.Now;
            //NoticeDate = DateTime.Now;
        }
        ////public string Key { get; set; } // key của object này là Symbol và EffectiveDate
        //public string Symbol { get; set; }
        //public string Title { get; set; }
        //public double Value { get; set; }
        //public double Rate { get; set; }
        //public DateTime ExRightsDate { get; set; }
        //public DateTime FixDate { get; set; }
        //public double Year { get; set; }
        //public double Order { get; set; }
        //public DateTime EffectiveDate { get; set; }
        //public string Notes { get; set; }
        //public DateTime NoticeDate { get; set; }
        //public double ExRightsPrice { get; set; }
        //public double PriceBeforeRights { get; set; }
        //public string Donvi { get; set; }
        /// <summary>
        /// Ngày giao dịch không hưởng quyền
        /// </summary>
        public DateTime NgayGDKHQ { get; set; }
        /// <summary>
        /// Tên sự kiện
        /// </summary>
        public string SuKien { get; set; }
        /// <summary>
        /// Đơn vị
        /// </summary>
        public string DonViDoiTuong { get; set; }
        /// <summary>
        /// Tỉ lệ
        /// </summary>
        public string TiLe { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string GhiChu { get; set; }
        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
    }

	#endregion	
	
}