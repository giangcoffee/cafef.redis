using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    /// <summary>
    /// Dữ liệu box hàng hóa ở trang chủ
    /// </summary>
    [Serializable]
    public class ProductBox
    {
        public ProductBox()
        {
            UpdateDate = DateTime.Now;
        }
        /// <summary>
        /// Tên item
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Giá mua hoặc Giá hiện tại
        /// </summary>
        public double CurrentPrice { get; set; }
        /// <summary>
        /// Giá bán (box Việt Nam)
        /// </summary>
        public double OtherPrice { get; set; }
        /// <summary>
        /// Giá kỳ trước
        /// </summary>
        public double PrevPrice { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// ID Trong db
        /// </summary>
        public string DbId { get; set; }

        public void UpdatePrevPrice(ProductBox lastbox)
        {
            if (lastbox == null) { this.PrevPrice = this.CurrentPrice; return; }
            if (int.Parse(lastbox.UpdateDate.ToString("HH")) < 22 && (int.Parse(this.UpdateDate.ToString("HH")) > 22 || double.Parse(this.UpdateDate.ToString("yyyyMMdd")) > double.Parse(lastbox.UpdateDate.ToString("yyyyMMdd"))))
            {
                this.PrevPrice = lastbox.CurrentPrice;
            }else
            {
                this.PrevPrice = lastbox.PrevPrice;
            }
            if (this.PrevPrice == 0) {this.PrevPrice = this.CurrentPrice; }
        }
    }

}
