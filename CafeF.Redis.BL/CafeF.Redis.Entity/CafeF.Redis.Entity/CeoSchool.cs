using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    #region Ceo school Models
    /// <summary>
    /// Thành tích
    /// </summary>
    [Serializable]
    public class CeoSchool //Thông tin thành tích học tập - Gắn vào đối tượng Ceo
    {
        /// <summary>
        /// Học vị đạt đc
        /// </summary>
        public string CeoTitle { get; set; }
        /// <summary>
        /// Tên trường
        /// </summary>
        public string SchoolTitle { get; set; }
        /// <summary>
        /// Năm tốt nghiệp
        /// </summary>
        public string SchoolYear { get; set; }
    }
    #endregion	
}
