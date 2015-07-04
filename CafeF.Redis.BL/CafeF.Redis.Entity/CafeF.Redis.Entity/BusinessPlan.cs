using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region BusinessPlan Models
    /// <summary>
    /// Kế hoạch kinh doanh
    /// </summary>
    [Serializable]
    //key format: "BussinessPlan:{0}:bussinessplanid:List ~Symbol" / "stockid:{0}:bussinessplanid:List"
    public class BusinessPlan //Kế hoạch kinh doanh - Dùng trong trang Stock
    {
        public BusinessPlan()
		{
            Date = DateTime.Now;
        }
        //public string Key { get; set; } // key của object này là Symbol và ID
        public string Symbol { get; set; }
        public int ID { get; set; }
        public DateTime Date { get; set; }
        /// <summary>
        /// Doanh thu
        /// </summary>
        public double Revenue { get; set; }
        /// <summary>
        /// Lợi nhuận trước thuế
        /// </summary>
        public double ProfitBTax { get; set; }
        /// <summary>
        /// Lợi nhuận sau thuế
        /// </summary>
        public double ProfitATax { get; set; }
        /// <summary>
        /// Cổ tức bằng tiền mặt
        /// </summary>
        public double DividendsMoney { get; set; }
        /// <summary>
        /// Cổ tức bằng cổ phiếu
        /// </summary>
        public double DividendsStock { get; set; }
        /// <summary>
        /// Dự kiến tăng vốn
        /// </summary>
        public double IncreaseExpected { get; set; }
        /// <summary>
        /// Chi tiết nội dung
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Năm kế hoạch
        /// </summary>
        public int Year { get; set; }
    }
	#endregion	
	
}