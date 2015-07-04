using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    #region TienDoBDS Models
    [Serializable]
    public class TienDoBDS  //Đối tượng này dùng trong trang view tiến độ BDS
    {
        public TienDoBDS()
        {
            this.BDSImages = new List<string>();
            this.DienTichs = new List<TienDoBDSDienTich>();
            this.LoiNhuans = new List<TienDoBDSLoiNhuan>();
        }

        #region basic info
        /// <summary>
        /// Mã ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Mã tiến độ
        /// </summary>
        public string MaTienDo { get; set; }
        /// <summary>
        /// Mã chứng khoán
        /// </summary>
        public string MaCK { get; set; }
        /// <summary>
        /// Tên dự án
        /// </summary>
        public string TenDuAn { get; set; }
        /// <summary>
        /// Địa điểm
        /// </summary>
        public string DiaDiem { get; set; }
        /// <summary>
        /// Thành phố
        /// </summary>
        public string ThanhPho { get; set; }
        /// <summary>
        /// Hình thức kinh doanh
        /// </summary>
        public string HinhThucKinhDoanh { get; set; }
       
        /// <summary>
        /// Tổng vốn
        /// </summary>
        public decimal TongVon { get; set; }
        /// <summary>
        /// Đơn vị
        /// </summary>
        public string Donvi { get; set; }       
        /// <summary>
        /// Tỷ lệ ghóp vốn
        /// </summary>
        public string TyLeGhopVon { get; set; }
        /// <summary>
        /// Tỷ lệ đền bù
        /// </summary>
        public string TyLeDenBu { get; set; }
      
      
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string GhiChu { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string MoTa { get; set; }
        /// <summary>
        /// Link liên kết
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Ngay cap nhat
        /// </summary>
        public DateTime ViewDate { get; set; }
        /// <summary>
        /// Danh sách ảnh dự án
        /// </summary>
        public List<string> BDSImages { get; set; }

        public List<TienDoBDSDienTich> DienTichs { get; set; }
        public List<TienDoBDSLoiNhuan> LoiNhuans { get; set; }
        #endregion      

    }

    [Serializable]
    public class TienDoBDSDienTich
    {
        /// <summary>
        /// Mã tiến độ
        /// </summary>
        public string MaTienDo { get; set; }
        /// <summary>
        /// Diện tích
        /// </summary>
        public decimal DienTich { get; set; }
        /// <summary>
        /// Loại diện tích
        /// </summary>
        public string LoaiDienTich { get; set; }
    }

    [Serializable]
    public class TienDoBDSLoiNhuan
    {
        /// <summary>
        /// Mã tiến độ
        /// </summary>
        public string MaTienDo { get; set; }
        /// <summary>
        /// Lợi nhuận doanh thu
        /// </summary>
        public decimal LoiNhuanDoanhThu { get; set; }
        /// <summary>
        /// Loại lợi nhuận
        /// </summary>
        public string LoaiLoiNhuan { get; set; }
    }
    #endregion	
}
