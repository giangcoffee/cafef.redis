using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    public class Bond
    {
        public Bond(){ BondValues = new List<BondValue>();}

        /// <summary>
        /// Mã
        /// </summary>
        public string BondCode { get; set; }
        /// <summary>
        /// Tên tiếng Anh
        /// </summary>
        public string BondEnName { get; set; }
        /// <summary>
        /// Tên Tiếng Việt
        /// </summary>
        public string BondVnName { get; set; }
        /// <summary>
        /// Quốc gia
        /// </summary>
        public string BondCountry { get; set; }
        /// <summary>
        /// Loại : 1/3/5/10 năm
        /// </summary>
        public string BondType { get; set; }

        public List<BondValue> BondValues { get; set; }
    }

    public class BondValue
    {
        public BondValue() { TradeDate = DateTime.Now; }

        public DateTime TradeDate { get; set; }
        public double ClosePrice { get; set; }
    }
}
