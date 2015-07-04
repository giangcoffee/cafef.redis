using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    public class SessionPriceData
    {
        public SessionPriceData(){}

        public string Symbol { get; set; }
        public DateTime TradeDate { get; set; }
        public double Price { get; set; }
        public double Volume { get; set; }
        public double TotalVolume { get; set; }
        public double TotalValue { get; set; }
    }
}
