using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CafeF.Redis.Entity
{
    #region StockHistory Models
    //key format: "stockid:{0}:history:List ~ :Symbol" / "stockid:{0}:history:List"
    /// <summary>
    /// Object dùng trong trang lịch sử giao dịch
    /// </summary>
    [Serializable]
    public class StockHistory//Lịch sủ giao dịch - dùng trong trang xem lịch sử giao dịch. N top history đầu được sử dụng trong trang Stock
    {
        public StockHistory()
        {
            TradeDate = DateTime.Now;
        }
        //public string Key { get; set; } // key của object này là Symbol và TradeDate
        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Ngày giao dịch cuối
        /// </summary>
        public DateTime TradeDate { get; set; }
        /// <summary>
        /// Giá tham chiếu
        /// </summary>
        public double BasicPrice { get; set; }
        /// <summary>
        /// Giá mở cửa
        /// </summary>
        public double OpenPrice { get; set; }
        /// <summary>
        /// Giá cao nhất
        /// </summary>
        public double HighPrice { get; set; }
        /// <summary>
        /// Giá thấp nhất
        /// </summary>
        public double LowPrice { get; set; }
        /// <summary>
        /// Giá đóng cửa
        /// </summary>
        public double ClosePrice { get; set; }
        /// <summary>
        /// Giá trung bình
        /// </summary>
        public double AveragePrice { get; set; }
        /// <summary>
        /// Tổng khối lượng khớp lệnh
        /// </summary>
        public double Volume { get; set; }
        public int ID { get; set; }
        /// <summary>
        /// Giá điều chỉnh
        /// </summary>
        public double AdjustPrice { get; set; }
        /// <summary>
        /// Giá trần
        /// </summary>
        public double Ceiling { get; set; }
        /// <summary>
        /// Giá sàn
        /// </summary>
        public double Floor { get; set; }
        /// <summary>
        /// Giá trung bình điều chỉnh
        /// </summary>
        public double AverageAdjustPrice { get; set; }
        /// <summary>
        /// Tổng giá trị khớp lệnh
        /// </summary>
        public double TotalValue { get; set; }
        /// <summary>
        /// Khối lượng giao dịch thỏa thuận
        /// </summary>
        public double AgreedVolume { get; set; }
        /// <summary>
        /// Giá trị giao dịch thỏa thuận
        /// </summary>
        public double AgreedValue { get; set; }
        /// <summary>
        /// Khối lượng giao dịch đợt 1
        /// </summary>
        public double KLGDDot1 { get; set; }
        /// <summary>
        /// Khối lượng giao dịch đợt 2
        /// </summary>
        public double KLGDDot2 { get; set; }
        /// <summary>
        /// Khối lượng giao dịch đợt 3
        /// </summary>
        public double KLGDDot3 { get; set; }

        //public OrderHistory Orders { get; set; }
        //public ForeignHistory ForeignOrders { get; set; }
    }
    #endregion

    #region Lịch sử giá rút gọn - dùng trong trang doanh nghiệp
    [Serializable]
    public class StockCompactHistory
    {
        public StockCompactHistory()
        {
            this.Price = new List<PriceCompactHistory>();
            this.Orders = new List<OrderCompactHistory>();
            this.Foreign = new List<ForeignCompactHistory>();
        }

        public List<PriceCompactHistory> Price { get; set; }
        public List<OrderCompactHistory> Orders { get; set; }
        public List<ForeignCompactHistory> Foreign { get; set; }
    }

    /// <summary>
    /// Lịch sử giá rút gọn - dùng trong trang doanh nghiệp
    /// </summary>
    [Serializable]
    public class PriceCompactHistory
    {

        public string Symbol { get; set; }
        public DateTime TradeDate { get; set; }
        /// <summary>
        /// Giá tham chiếu
        /// </summary>
        public double BasicPrice { get; set; }
        //public double OpenPrice { get; set; }
        //public double HighPrice { get; set; }
        //public double LowPrice { get; set; }
        /// <summary>
        /// Giá đóng cửa (Ho) / Giá trung bình (Ha, Up)
        /// </summary>
        public double ClosePrice { get; set; }
        
        //public double AveragePrice { get; set; }
        /// <summary>
        /// Số cổ phiếu giao dịch
        /// </summary>
        public double Volume { get; set; }
        // public int ID { get; set; }
        /// <summary>
        /// Giá điều chỉnh
        /// </summary>
        public double AdjustPrice { get; set; }
        /// <summary>
        /// Giá trần
        /// </summary>
        public double Ceiling { get; set; }
        /// <summary>
        /// Giá sàn
        /// </summary>
        public double Floor { get; set; }
        //public double AverageAjustPrice { get; set; }
        /// <summary>
        /// Tổng giá trị giao dịch
        /// </summary>
        public double TotalValue { get; set; }
        /// <summary>
        /// Khối lượng Giao dịch thỏa thuận
        /// </summary>
        public double AgreedVolume { get; set; }
        /// <summary>
        /// Giá trị Giao dịch thỏa thuận
        /// </summary>
        public double AgreedValue { get; set; }
    }
    /// <summary>
    /// Cung cầu rút gọn - dùng trong trang doanh nghiệp
    /// </summary>
    [Serializable] 
    public class OrderCompactHistory
    {

        public string Symbol { get; set; }
        public DateTime TradeDate { get; set; }
        /// <summary>
        /// Dư mua
        /// </summary>
        public double BidLeft { get; set; }
        /// <summary>
        /// Dư bán
        /// </summary>
        public double AskLeft { get; set; }
        /// <summary>
        /// Trung bình một lệnh mua
        /// </summary>
        public double BidAverageVolume { get; set; }
        /// <summary>
        /// Trung bình một lệnh bán
        /// </summary>
        public double AskAverageVolume { get; set; }
        public double BidVolume { get; set; }
        public double AskVolume { get; set; }

    }
    /// <summary>
    /// Giao dịch NĐT nước ngoài rút gọn - Dùng trong trang doanh nghiệp
    /// </summary>
    [Serializable] 
    public class ForeignCompactHistory
    {
        public string Symbol { get; set; }
        public DateTime TradeDate { get; set; }

        /// <summary>
        /// Khối lượng giao dịch ròng
        /// </summary>
        public double NetValue { get; set; }
        /// <summary>
        /// Giá trị giao dịch ròng
        /// </summary>
        public double NetVolume { get; set; }
        /// <summary>
        /// Tỉ lệ GD mua toàn thị trường
        /// </summary>
        public double BuyPercent { get; set; }
        /// <summary>
        /// Tỉ lệ GD bán toàn thị trường
        /// </summary>
        public double SellPercent { get; set; }
        public double BuyValue { get; set; }
        public double SellValue { get; set; }
    }
    #endregion

    #region Giao dịch cổ đông nội bộ - dùng trong trang lịch sử giao dịch
    /// <summary>
    /// Giao dịch nội bộ và giao dịch cổ đông lớn
    /// </summary>
    [Serializable]
    public class InternalHistory
    {
        public InternalHistory() { }

        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Stock { get; set; }
        /// <summary>
        /// ID của cá nhân / tổ chức giao dịch
        /// </summary>
        public string HolderID { get; set; }
        /// <summary>
        /// Người / tổ chức giao dịch
        /// </summary>
        public string TransactionMan { get; set; }
        /// <summary>
        /// Vị trí người giao dịch
        /// </summary>
        public string TransactionManPosition { get; set; }
        /// <summary>
        /// Vị trí người liên quan
        /// </summary>
        public string RelatedManPosition { get; set; }
        /// <summary>
        /// Người liên quan
        /// </summary>
        public string RelatedMan { get; set; }
        /// <summary>
        /// Lượng cổ phiếu trước giao dịch
        /// </summary>
        public double VolumeBeforeTransaction { get; set; }
        /// <summary>
        /// Khối lượng đăng ký mua
        /// </summary>
        public double PlanBuyVolume { get; set; }
        /// <summary>
        /// Khối lượng đăng ký bán
        /// </summary>
        public double PlanSellVolume { get; set; }
        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        public DateTime? PlanBeginDate { get; set; }
        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public DateTime? PlanEndDate { get; set; }
        /// <summary>
        /// Khối lượng giao dịch mua
        /// </summary>
        public double RealBuyVolume { get; set; }
        /// <summary>
        /// Khối lượng giao dịch bán
        /// </summary>
        public double RealSellVolume { get; set; }
        /// <summary>
        /// Ngày kết thúc thực hiện giao dịch (ngày thực hiện)
        /// </summary>
        public DateTime? RealEndDate { get; set; }
        /// <summary>
        /// Ngày công bố
        /// </summary>
        public DateTime? PublishedDate { get; set; }
        /// <summary>
        /// Khối lượng sau giao dịch
        /// </summary>
        public double VolumeAfterTransaction { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string TransactionNote { get; set; }
        public double TyLeSoHuu { get; set; }
        public string ShareHolderCode { get; set; }
    }
    #endregion

    #region Giao dịch cổ phiếu quỹ - dùng trong trang lịch sử giao dịch
    [Serializable]
    public class FundHistory
    {
        public FundHistory() { }
        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Ngày giao dịch
        /// </summary>
        public DateTime TradeDate { get; set; }
        /// <summary>
        /// Loại giao dịch (mua / bán)
        /// </summary>
        public string TransactionType { get; set; }
        /// <summary>
        /// Khối lượng đăng ký
        /// </summary>
        public double PlanVolume { get; set; }
        /// <summary>
        /// Khối lượng GD trong ngày
        /// </summary>
        public double TodayVolume { get; set; }
        /// <summary>
        /// Tỉ lệ Khối lượng GD trong ngày / đăng ký
        /// </summary>
        public double TodayPercent { get { return PlanVolume == 0 ? 0 : (TodayVolume / PlanVolume * 100); } }
        /// <summary>
        /// Khối lượng GD tích lũy
        /// </summary>
        public double AccumulateVolume { get; set; }
        /// <summary>
        /// Tỉ lệ Khối lượng GD tích lũy
        /// </summary>
        public double AccumulatePercent { get { return PlanVolume == 0 ? 0 : (AccumulateVolume / PlanVolume * 100); } }
        /// <summary>
        /// Khối lượng còn lại
        /// </summary>
        public double RemainVolume { get { return PlanVolume - AccumulateVolume; } }
        /// <summary>
        /// Tỉ lệ Khối lượng còn lại
        /// </summary>
        public double RemainPercent { get { return 100 - AccumulatePercent; } }
        public DateTime? ExpiredDate { get; set; }
    }
    #endregion
}