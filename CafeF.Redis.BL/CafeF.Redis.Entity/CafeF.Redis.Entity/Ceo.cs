using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    #region Ceo Models
    [Serializable]
    public class Ceo  //Đối tượng này dùng trong trang view ceo
    {
        public Ceo()
        {
            this.CeoPosition = new List<CeoPosition>();
            this.CeoAsset = new List<CeoAsset>();
            this.CeoRelation = new List<CeoRelation>();
            this.CeoSchool = new List<CeoSchool>();
            this.CeoProcess = new List<CeoProcess>();
            this.StockCeo = new List<StockCeo>();
            this.CeoNews = new List<CeoNews>();
        }

        #region basic info
        /// <summary>
        /// Mã ceo
        /// </summary>
        public string CeoCode { get; set; }
        /// <summary>
        /// ảnh
        /// </summary>
        public string CeoImage { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string CeoName { get; set; }
        /// <summary>
        /// Năm sinh
        /// </summary>
        public string CeoBirthday { get; set; }
        /// <summary>
        /// Số CMND
        /// </summary>
        public string CeoIdNo { get; set; }
        /// <summary>
        /// Quê quán
        /// </summary>
        public string CeoHomeTown { get; set; }
        /// <summary>
        /// Thành tích
        /// </summary>
        public string CeoAchievements { get; set; }
        /// <summary>
        /// Thông tin ngắn
        /// </summary>
        public string CeoProfileShort { get; set; }
        /// <summary>
        /// Trình độ
        /// </summary>
        public string CeoSchoolDegree { get; set; }
        #endregion

        public List<CeoPosition> CeoPosition { get; set; }
        public List<CeoAsset> CeoAsset { get; set; }

        public List<CeoRelation> CeoRelation { get; set; }
        public List<CeoSchool> CeoSchool { get; set; }
        public List<CeoProcess> CeoProcess { get; set; }
        public List<CeoNews> CeoNews { get; set; }

        public List<StockCeo> StockCeo { get; set; }

    }
    #endregion	
}
