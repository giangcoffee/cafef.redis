using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    #region Ceo process Models
    /// <summary>
    /// Quá trình công tác
    /// </summary>
    [Serializable]
    public class CeoProcess //Thông tin quá trình công tác - Gắn vào đối tượng Ceo
    {
        /// <summary>
        /// Quá trình
        /// </summary>
        public string ProcessDesc { get; set; }
        /// <summary>
        /// Bắt đầu
        /// </summary>
        public string ProcessBegin { get; set; }
        /// <summary>
        /// Kết thúc
        /// </summary>
        public string ProcessEnd { get; set; }
        /// <summary>
        /// Mã cty (nếu có)
        /// </summary>
        public string Symbol { get; set; }
    }
    #endregion	
}
