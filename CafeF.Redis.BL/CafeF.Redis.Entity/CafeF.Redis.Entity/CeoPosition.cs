using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    #region Ceo position Models
    /// <summary>
    /// Vị trí
    /// </summary>
    [Serializable]
    public class CeoPosition //Thông tin chức vụ - Gắn vào đối tượng Ceo
    {
        /// <summary>
        /// Chức vụ
        /// </summary>
        public string PositionTitle { get; set; }
        /// <summary>
        /// Tổ chức
        /// </summary>
        public string PositionCompany { get; set; }
        /// <summary>
        /// Thời gian bổ nhiệm
        /// </summary>
        public string CeoPosDate { get; set; }
        /// <summary>
        /// Mã công ty
        /// </summary>
        public string CeoSymbol { get; set; }
        /// <summary>
        /// Mã sàn của công ty tương ứng
        /// </summary>
        public int CeoSymbolCenterId { get; set; }
    }
    #endregion	
}
