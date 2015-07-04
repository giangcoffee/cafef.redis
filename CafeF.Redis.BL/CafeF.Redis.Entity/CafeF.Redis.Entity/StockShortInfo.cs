using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    [Serializable]
    public class StockShortInfo
    {
        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Tên công ty
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Mã sàn
        /// </summary>
        public int TradeCenterId { get; set; }
        /// <summary>
        /// Giá trị EPS
        /// </summary>
        public double EPS { get; set; }
        /// <summary>
        /// Vốn hóa thị trường
        /// </summary>
        public double MarketValue { get; set; }
    }
}
