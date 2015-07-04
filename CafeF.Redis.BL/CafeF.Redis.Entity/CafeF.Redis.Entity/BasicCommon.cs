using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeF.Redis.Entity
{
    #region BasicCommon Models
    [Serializable]
    //key format: "BasicCommon:{0}:Symbol" / "basiccommonid:{0}:Object"
    public class BasicCommon //Thông tin cơ bản chung - Gắn vào đối tượng BasicInfo
    {
        //public string Key { get; set; } // key của object này là Symbol
        public string Symbol { get; set; }
        /// <summary>
        /// Giá trị EPS
        /// </summary>
        public double EPS { get; set; }
        /// <summary>
        /// P/E = Price / EPS
        /// </summary>
        public double PE { get; set; }
        /// <summary>
        /// Giá trị sổ sách / cổ phiếu
        /// </summary>
        public double ValuePerStock { get; set; }
        /// <summary>
        /// Hệ số beta
        /// </summary>
        public double Beta { get; set; }
        /// <summary>
        /// Trung bình khối lượng 10 phiên
        /// </summary>
        public double AverageVolume { get; set; }
        /// <summary>
        /// Khối lượng cổ phiếu niêm yết
        /// </summary>
        public double VolumeTotal { get; set; }
        /// <summary>
        /// Tổng khối lượng cổ phiếu đang lưu hành
        /// </summary>
        public double OutstandingVolume { get; set; }
        /// <summary>
        /// Vốn hóa thị trường (đơn vị : tỷ đồng)
        /// </summary>
        public double TotalValue { get; set; }
        /// <summary>
        /// Thời gian tính EPS
        /// </summary>
        public string EPSDate { get; set; }
        /// <summary>
        /// Thời gian tính EPS bằng chữ
        /// </summary>
        public string EPSFullDate
        {
            get
            {
                const string format = "{0}{1}{2}{3}";
                string[] ss;
                try
                {
                    if (string.IsNullOrEmpty(EPSDate) || !EPSDate.Contains("-")) return "";
                    ss = EPSDate.Split('-');
                }
                catch (Exception) { return ""; }
                int qr; if (!int.TryParse(ss[0], out qr)) qr = 0;
                switch (qr)
                {
                    case 1: return string.Format(format, "Quý ", "I", " năm ", ss[1]);
                    case 2: return string.Format(format, "Quý ", "II", " năm ", ss[1]);
                    case 3: return string.Format(format, "Quý ", "III", " năm ", ss[1]);
                    case 4: return string.Format(format, "Quý ", "IV", " năm ", ss[1]);
                    default: return string.Format(format, "", "", "năm ", ss[1]);
                }

            }
        }
        /// <summary>
        /// Thay đổi giá trị tài sản ròng (tỷ đồng) ==> CCQ
        /// </summary>
        public double CCQv3 { get; set; }
        /// <summary>
        /// Giá trị tài sản ròng/1 CCQ (nghìn đồng) ==> CCQ
        /// </summary>
        public double CCQv6 { get; set; }
        /// <summary>
        /// Ngày cập nhật ==> CCQ
        /// </summary>
        public DateTime CCQdate { get; set; }
    }

    #endregion

}