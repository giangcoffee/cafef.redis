using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeF.Redis.Entity
{
    #region OrderHistory Models
    /// <summary>
    /// Thống kê đặt lệnh
    /// </summary>
    [Serializable]
    //key format: "OrderHistory:{0}:{1}:Symbol:TradeDate" / "orderhistoryid:{0}:{1}:Object"
    public class OrderHistory //Lịch sử cung cầu - mỗi Object này gắn vào Object History
    {
        public OrderHistory()
        {
            TradeDate = DateTime.Now;
        }
        // public string Key { get; set; } // key cua object này là Symbol va TradeDate
        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Ngày giao dịch
        /// </summary>
        public DateTime TradeDate { get; set; }
        /// <summary>
        /// Tổng lệnh mua
        /// </summary>
        public double BuyOrderCount { get; set; } // so lenh
        /// <summary>
        /// Tổng lệnh bán
        /// </summary>
        public double SellOrderCount { get; set; }// so lenh
        /// <summary>
        /// Tổng khối lượng đặt mua
        /// </summary>
        public double BuyVolume { get; set; }
        /// <summary>
        /// Tổng khối lượng đặt bán
        /// </summary>
        public double SellVolume { get; set; }
        /// <summary>
        /// Tổng khối lượng dư mua
        /// </summary>
        public double BuyLeft { get { return BuyVolume - Volume; } }
        /// <summary>
        /// Tổng khối lượng dư bán
        /// </summary>
        public double SellLeft { get { return SellVolume - Volume; } }
        /// <summary>
        /// Trung bình một lệnh mua
        /// </summary>
        public double BuyAverage { get { return BuyVolume / BuyOrderCount; } }
        /// <summary>
        /// Trung bình một lệnh bán
        /// </summary>
        public double SellAverage { get { return SellVolume / SellOrderCount; } }
        /// <summary>
        /// Chênh lệch lượng mua - bán
        /// </summary>
        public double BuySellDiff { get { return BuyVolume - SellVolume; } }

        #region Price
        /// <summary>
        /// Giá cổ phiếu
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Giá tham chiếu
        /// </summary>
        public double BasicPrice { get; set; }
        /// <summary>
        /// Giá trần
        /// </summary>
        public double Ceiling { get; set; }
        /// <summary>
        /// Giá sàn
        /// </summary>
        public double Floor { get; set; }
        /// <summary>
        /// Khối lượng khớp lệnh
        /// </summary>
        public double Volume { get; set; }
        #endregion

        //Dư mua = Tổng KL mua - Tổng KL GD
        //Dư bán = Tổng KL bán - Tổng KL GD
        //KLTB 1 lệnh mua = Tổng KL mua / Số lệnh mua
        //KLTB 1 lệnh bán = Tổng KL bán / Số lệnh bán
        //Chênh lệch KL đặt mua - đặt bán = Tổng KL mua - Tổng KL bán

    }

    #endregion

}