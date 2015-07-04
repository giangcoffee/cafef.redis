using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    #region Stock ceo Models
    /// <summary>
    /// Ban lãnh đạo liên quan
    /// </summary>
    [Serializable]
    public class StockCeo //Thông tin lãnh đạo - Gắn vào đối tượng CompanyProfile và Ceo
    {
        /// <summary>
        /// Chức vụ
        /// </summary>
        public string Positions { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Tuổi
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// Quá trình công tác
        /// </summary>
        public string Process { get; set; }
        /// <summary>
        /// =1:Hội đồng quản trị, =2: Ban giám đốc/kế toán, = 3: Ban kiểm soát
        /// </summary>
        public int GroupID { get; set; }
        public string CeoCode { get; set; }
        public int CeoId { get; set; }
    }    
    #endregion	
}
