using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region FinanceInfo Models
    /// <summary>
    /// Thông tin tài chính - Nhóm chỉ tiêu
    /// </summary>
    [Serializable]
    //key format: "FinanceInfo:{0}:{1}:Symbol:Date" / "financeinfoid:{0}:{1}:Object"
    public class FinanceInfo //Thông tin tài chính - Dùng trong trang Stock
    {
        public FinanceInfo()
        {
            ChiTieus = new List<FinanceChiTieu>();
        }
        //public string Key { get; set; } // key của object này là Symbol và Date
        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Mã nhóm
        /// </summary>
        public int NhomChiTieuId { get; set; }
        /// <summary>
        /// Tên nhóm
        /// </summary>
        public string TenNhomChiTieu { get; set; }
        /// <summary>
        /// Danh sách chỉ tiêu
        /// </summary>
        public List<FinanceChiTieu> ChiTieus { get; set; }

        // Nhóm chỉ tiêu -- chỉ tiêu -- danh sách giá trị theo từng quý / năm
    }

    /// <summary>
    /// Chỉ tiêu
    /// </summary>
    [Serializable]
    public class FinanceChiTieu
    {
        public FinanceChiTieu()
        {
            Values = new List<FinanceValue>();
        }
        /// <summary>
        /// Mã chỉ tiêu
        /// </summary>
        public string ChiTieuId { get; set; }
        /// <summary>
        /// Tên chỉ tiêu
        /// </summary>
        public string TenChiTieu { get; set; }
        /// <summary>
        /// Danh sách giá trị 
        /// </summary>
        public List<FinanceValue> Values { get; set; }
        public int MinQuarter
        {
            get
            {
                var tmp = 0;
                foreach (var value in Values)
                {
                    if (value.Quarter < 5 && (tmp == 0 || tmp > value.Order)) tmp = value.Order;
                }
                return tmp;
            }
        }
        public int MaxQuarter
        {
            get
            {
                var tmp = 0;
                foreach (var value in Values)
                {
                    if (value.Quarter < 5 && (tmp == 0 || tmp < value.Order)) tmp = value.Order;
                }
                return tmp;
            }
        }
        public bool HasQuarterValue
        {
            get
            {
                foreach (var value in Values)
                {
                    if (value.Quarter < 5) return true;
                } return false;
            }
        }
    }

    /// <summary>
    /// Giá trị từng chỉ tiêu theo từng kỳ
    /// </summary>
    [Serializable]
    public class FinanceValue
    {
        public FinanceValue()
        {

        }
        public int Year { get; set; }
        public int Quarter { get; set; }
        public double Value { get; set; }
        public int Order { get { return Year * 10 + Quarter; } }
    }
    #endregion

    #region Finance Periods
    /// <summary>
    /// Niên độ kế toán
    /// </summary>
    [Serializable]
    public class FinancePeriod
    {
        public FinancePeriod()
        {

        }
        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Năm kế toán
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Quý : 1, 2, 3, 4 | Cả năm : 5
        /// </summary>
        public int Quarter { get; set; }
        /// <summary>
        /// Chú thích khác
        /// </summary>
        public string SubTitle { get; set; }
        /// <summary>
        /// Tiêu đề quý
        /// </summary>
        public string QuarterTitle { get; set; }
        /// <summary>
        /// Tiêu đề năm
        /// </summary>
        public string YearTitle { get; set; }
        /// <summary>
        /// Chú thích ngày bắt đầu
        /// </summary>
        public string BeginTitle { get; set; }
        /// <summary>
        /// Chú thích ngày kết thúc
        /// </summary>
        public string EndTitle { get; set; }
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; }
        public string UpdateTitle()
        {
            var ret = "";
            if (string.IsNullOrEmpty(QuarterTitle) && Quarter < 5) ret += "Quý " + Quarter; else ret += QuarterTitle;
            if (string.IsNullOrEmpty(YearTitle)) ret += (string.IsNullOrEmpty(ret) ? "Năm " : "-") + Year; else ret += (string.IsNullOrEmpty(ret) ? "" : "-") + YearTitle;
            if (ret.Contains("-Năm")) ret.Replace("-Năm", "-");
            if (!string.IsNullOrEmpty(SubTitle)) ret += "<br /><span style='color:#CC0000;'>(" + SubTitle + ")</span>";
            if (string.IsNullOrEmpty(BeginTitle))
                if (!string.IsNullOrEmpty(EndTitle)) ret += "<br /><span style='color:#838383;'>(đến " + EndTitle + ")</span>";
                else ret += "";
            else if (!string.IsNullOrEmpty(EndTitle)) ret += "<br /><span style='color:#838383;'>(" + BeginTitle + " - " + EndTitle + ")</span>";
            else ret += "<br /><span style='color:#838383;'>(từ " + BeginTitle + ")</span>";
            Title = ret;
            return ret;
        }
    }
    #endregion

}