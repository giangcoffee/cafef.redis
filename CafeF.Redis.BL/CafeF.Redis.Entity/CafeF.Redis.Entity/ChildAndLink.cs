using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region ChildAndLink Models
    [Serializable]
    //key format: "ChildAndLink:{0}:Symbol" / "childandlinkid:{0}:Object"
    public class OtherCompany //Thông tin cty con và liên kết - Gắn vào đối tượng CompanyProfile
    {
        //public string Key { get; set; } // key cua object này là Symbol
        /// <summary>
        /// Mã của cty mẹ
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Tên cty
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Vốn điều lệ
        /// </summary>
        public double TotalCapital { get; set; }
        /// <summary>
        /// Vốn góp
        /// </summary>
        public double SharedCapital { get; set; }
        /// <summary>
        /// Tỷ lệ sở hữu
        /// </summary>
        public double OwnershipRate { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// Thứ tự
        /// </summary>
        public int Order { get; set; }
    }

    #endregion

}