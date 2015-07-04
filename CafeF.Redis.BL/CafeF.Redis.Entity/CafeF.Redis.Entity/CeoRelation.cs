using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    #region Ceo relation Models
    /// <summary>
    /// Cá nhân liên quan
    /// </summary>
    [Serializable]
    public class CeoRelation //Thông tin cá nhân liên quan - Gắn vào đối tượng Ceo
    {
        /// <summary>
        /// Họ tên
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Mối quan hệ
        /// </summary>
        public string RelationTitle { get; set; }
        /// <summary>
        /// Cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Số cổ phiếu đang nắm giữ
        /// </summary>
        public string AssetVolume { get; set; }
        /// <summary>
        /// Tính đến thời điểm
        /// </summary>
        public string UpdatedDate { get; set; }
        public string CeoCode { get; set; }
    }
    #endregion	
}
