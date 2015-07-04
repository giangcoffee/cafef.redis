using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeF.Redis.Entity
{
    #region ForeignHistory Models
    [Serializable]
    //key format: "ForeignHistory:{0}:{1}:Symbol:TradeDate" / "foreignhistoryid:{0}:{1}:Object"
    public class ForeignHistory //Lịch sử cung cầu nhà đầu tư nước ngoài - mỗi Object này gắn vào Object History
    {
        public ForeignHistory()
		{
            TradeDate = DateTime.Now;
        }
        //public string Key { get; set; } // key cua object này là Symbol và TradeDate
        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Ngày giao dịch
        /// </summary>
        public DateTime TradeDate { get; set; }
        /// <summary>
        /// Giá trị mua
        /// </summary>
        public double BuyValue { get; set; }
        /// <summary>
        /// Giá trị bán
        /// </summary>
        public double SellValue { get; set; }
        /// <summary>
        /// Khối lượng mua
        /// </summary>
        public double BuyVolume { get; set; }
        /// <summary>
        /// Khối lượng bán
        /// </summary>
        public double SellVolume { get; set; }
        /// <summary>
        /// Room còn lại
        /// </summary>
        public double Room { get; set; }
        /// <summary>
        /// Tổng room
        /// </summary>
        public double TotalRoom { get; set; }
        /// <summary>
        /// Tỉ lệ sở hữu
        /// </summary>
        public double Percent { get; set; }
        /// <summary>
        /// Khối lượng giao dịch ròng
        /// </summary>
        public double NetVolume { get { return BuyVolume - SellVolume; } }
        /// <summary>
        /// Giá trị giao dịch ròng
        /// </summary>
        public double NetValue { get { return BuyValue - SellValue; } }
        /// <summary>
        /// Giá tham chiếu
        /// </summary>
        public double BasicPrice { get; set; }
        /// <summary>
        /// Giá đóng cửa
        /// </summary>
        public double ClosePrice { get; set; }
        /// <summary>
        /// Giá trung bình
        /// </summary>
        public double AveragePrice { get; set; }
    }
	#endregion	
	
}