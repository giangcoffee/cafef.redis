using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    /// <summary>
    /// Các thông tin giá hiện tại, giao dịch phiên gần nhất
    /// </summary>
    public class StockPrice
    {
        public StockPrice() { }

        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Ngày giao dịch (dùng để phân biệt giá realtime - giá import)
        /// </summary>
        public DateTime LastTradeDate { get; set; }
        /// <summary>
        /// Giá hiện tại
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Giá tham chiếu
        /// </summary>
        public double RefPrice { get; set; }
        /// <summary>
        /// Giá sàn
        /// </summary>
        public double FloorPrice { get; set; }
        /// <summary>
        /// Giá trần
        /// </summary>
        public double CeilingPrice { get; set; }
        /// <summary>
        /// Khối lượng giao dịch
        /// </summary>
        public double Volume { get; set; }
        /// <summary>
        /// Giá trị giao dịch
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// Giá cao nhất
        /// </summary>
        public double HighPrice { get; set; }
        /// <summary>
        /// Giá thấp nhất
        /// </summary>
        public double LowPrice { get; set; }
        /// <summary>
        /// Giá trung bình
        /// </summary>
        public double AvgPrice { get; set; }
        /// <summary>
        /// Giá dư mua cao nhất
        /// </summary>
        public double BidPrice01 { get; set; }
        /// <summary>
        /// Giá dư mua cao thứ 2
        /// </summary>
        public double BidPrice02 { get; set; }
        /// <summary>
        /// Giá dư mua cao thứ 3
        /// </summary>
        public double BidPrice03 { get; set; }
        /// <summary>
        /// Khối lượng dư mua cao nhất
        /// </summary>
        public double BidVolume01 { get; set; }
        /// <summary>
        /// Khối lượng dư mua cao thứ 2
        /// </summary>
        public double BidVolume02 { get; set; }
        /// <summary>
        /// Khối lượng dư mua cao thứ 3
        /// </summary>
        public double BidVolume03 { get; set; }
        /// <summary>
        /// Giá dư bán cao nhất
        /// </summary>
        public double AskPrice01 { get; set; }
        /// <summary>
        /// Giá dư bán cao thứ 2
        /// </summary>
        public double AskPrice02 { get; set; }
        /// <summary>
        /// Giá dư bán cao thứ 3
        /// </summary>
        public double AskPrice03 { get; set; }
        /// <summary>
        /// Khối lượng dư bán cao nhất
        /// </summary>
        public double AskVolume01 { get; set; }
        /// <summary>
        /// Khối lượng dư bán cao thứ 2
        /// </summary>}
        public double AskVolume02 { get; set; }
        /// <summary>
        /// Khối lượng dư bán cao thứ 3
        /// </summary>}
        public double AskVolume03 { get; set; }
        /// <summary>
        /// Tổng khối lượng mua
        /// </summary>
        public double BidTotalVolume { get; set; }
        /// <summary>
        /// Tổng lệnh mua
        /// </summary>
        public double BidTotalOrder { get; set; }
        /// <summary>
        /// Tổng khối lượng bán
        /// </summary>
        public double AskTotalVolume { get; set; }
        /// <summary>
        /// Tổng lệnh bán
        /// </summary>
        public double AskTotalOrder { get; set; }
        
        /// <summary>
        /// Giá mở cửa
        /// </summary>
        public double OpenPrice { get; set; }
        /// <summary>
        /// Giá đóng cửa
        /// </summary>
        public double ClosePrice { get; set; }
        /// <summary>
        /// Khối lượng Mua nước ngoài
        /// </summary>
        public double ForeignBuyVolume { get; set; }
        /// <summary>
        /// Giá trị Mua nước ngoài
        /// </summary>
        public double ForeignBuyValue { get; set; }
        /// <summary>
        /// Khối lượng Bán nước ngoài
        /// </summary>
        public double ForeignSellVolume { get; set; }
        /// <summary>
        /// Giá trị Bán nước ngoài
        /// </summary>
        public double ForeignSellValue { get; set; }
        /// <summary>
        /// Giao dịch ròng NĐTNN
        /// </summary>
        public double ForeignNetVolume { get { return ForeignBuyVolume - ForeignSellVolume ; } }
        /// <summary>
        /// Room khối ngoại còn lại
        /// </summary>
        public double ForeignCurrentRoom { get; set; }
        /// <summary>
        /// Tổng room khối ngoại
        /// </summary>
        public double ForeignTotalRoom { get; set; }
    }
}
