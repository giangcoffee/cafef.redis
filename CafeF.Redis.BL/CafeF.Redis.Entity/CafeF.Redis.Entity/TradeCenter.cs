using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    public class TradeCenter
    {
        public int TradeCenterId { get; set; }
        public string CenterName { get; set; }

        public static string GetCenterName(int id)
        {
            switch (id)
            {
                case 1: return "HoSE";
                case 2: return "HASTC";
                case 8: return "OTC";
                case 9: return "UpCOM";
                default: return "";
            }
        }
        public static string GetShortName(int id)
        {
            switch (id)
            {
                case 1: return "HSX";
                case 2: return "HNX";
                case 8: return "OTC";
                case 9: return "UPCOM";
                default: return "";
            }
        }
    }

    public class TradeCenterStats
    {
        public int TradeCenterId { get; set;}

        public int Ceiling { get; set; }
        public int Up { get; set; }
        public int Normal { get; set; }
        public int Down { get; set; }
        public int Floor { get; set; }

        public string ChartFolder { get; set; }

        #region Dữ liệu chỉ số sàn
        public double PrevIndex { get; set; }
        public DateTime CurrentDate { get; set; }
        public double CurrentIndex { get; set; }
        public double CurrentQuantity { get; set; }
        public double CurrentVolume { get; set; }
        public double CurrentValue { get; set; }
        public double ForeignBuyVolume { get; set; }
        public double ForeignBuyValue { get; set; }
        public double ForeignSellVolume { get; set; }
        public double ForeignSellValue { get; set; }
        public double ForeignNetVolume
        {
            get
            {
                return (ForeignBuyVolume - ForeignSellVolume);
            }
        }
        #endregion

        #region Dữ liệu theo đợt (dùng cho VNINDEX)
        public double Index1 { get; set; }
        public double Volume1 { get; set; }
        public double Value1 { get; set; }
        public double Index2 { get; set; }
        public double Volume2 { get; set; }
        public double Value2 { get; set; }
        public double Index3 { get; set; }
        public double Volume3 { get; set; }
        public double Value3 { get; set; }
        #endregion
    }

}
