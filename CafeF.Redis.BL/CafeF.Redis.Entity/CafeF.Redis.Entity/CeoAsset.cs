using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    #region Ceo asset Models
    /// <summary>
    /// Cổ phiếu nắm giữ
    /// </summary>
    [Serializable]
    public class CeoAsset //Thông tin cổ phiếu nắm giữ - Gắn vào đối tượng Ceo
    {
        /// <summary>
        /// Tổ chức/mã cp
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public string AssetVolume { get; set; }
        /// <summary>
        /// Tính đến thời điểm
        /// </summary>
        public string UpdatedDate { get; set; }
    }
    #endregion	
}
