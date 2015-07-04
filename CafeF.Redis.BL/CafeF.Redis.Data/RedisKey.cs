using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Data
{
    public class RedisKey
    {
        #region Trang doanh nghiệp
        /// <summary>
        /// Trang doanh nghiệp - object Stock
        /// </summary>
        public const string Key = "stock:stockid:{0}:Object";
        public const string KeyHistory = "stockid:{0}:history:List";
        public const string KeyReports = "stockid:{0}:reports:List";
        public const string KeyBusinessPlans = "stockid:{0}:BusinessPlanid:List";
        public const string KeyAgreementHistory = "stockid:{0}:agreementhistory:{1}:Object";
        /// <summary>
        /// Key chứa Thông tin cơ bản của cổ phiếu
        /// </summary>
        public const string KeyCompactStock = "stock:{0}:compact";

        /// <summary>
        /// folder tương ứng với file kby.js
        /// </summary>
        public const string KeyKby = "stock:kby";
        /// <summary>
        /// Chứa ds bctc trong trang doanh nghiệp - object - FinanceReport
        /// </summary>
        public const string KeyFinanceReport = "stock:stockid:{0}:StockFinance";
        
        // <summary>
        /// Danh sách mã - object StockCompact
        /// </summary>
        public const string KeyStockList = "stock:list";
        /// <summary>
        /// Danh sách mã theo sàn - object StockCompact
        /// Mã sàn : Ho = 1; Ha = 2; Upcom = 9;
        /// </summary>
        public const string KeyStockListByCenter = "stock:center:{0}";
        #endregion

        #region Báo cáo phân tích
        /// <summary>
        /// Danh sách tất cả các key báo cáo phân tích - object List string 
        /// </summary>
        public const string KeyAnalysisReport = "stock:report:list";
        /// <summary>
        /// Chi tiết báo cáo phân tích - object Reports
        /// </summary>
        public const string KeyAnalysisReportDetail = "analysisreport:{0}:detail";
        #endregion

        #region Trang lịch sử giá
        /// <summary>
        /// Giá cổ phiếu - object StockPrice
        /// </summary>
        public const string PriceKey = "stock:stockid:{0}:RealtimePrice";
        /// <summary>
        /// Lịch sử giá - object StockHistory
        /// </summary>
        public const string PriceHistory = "stock:stockid:{0}:PriceHistory:{1}";
        public const string PriceHistoryHeader = "stock:stockid";
        public const string PriceHistoryFooter = ":PriceHistory:";
        /// <summary>
        /// Các key của object Lịch sử giá - object List string
        /// </summary>
        public const string PriceHistoryKeys = "stock:stockid:{0}:PriceHistory:Keys";
        /// <summary>
        /// Thống kê đặt lệnh - object OrderHistory
        /// </summary>
        public const string OrderHistory = "stock:stockid:{0}:OrderHistory:{1}";
        /// <summary>
        /// Các key của object Thống kê đặt lệnh - object List string
        /// </summary>
        public const string OrderHistoryKeys = "stock:stockid:{0}:OrderHistory:Keys";
        /// <summary>
        /// Giao dịch NĐT NN - object ForeignHistory
        /// </summary>
        public const string ForeignHistory = "stock:stockid:{0}:ForeignHistory:{1}";
        /// <summary>
        /// Các key của object Giao dịch NĐT NN - object List string 
        /// </summary>
        public const string ForeignHistoryKeys = "stock:stockid:{0}:ForeignHistory:Keys";
        /// <summary>
        /// Giao dịch cổ đông lớn và nội bộ - object InternalHistory
        /// </summary>
        public const string InternalHistory = "stock:stockid:{0}:InternalHistory:{1}:{2}:Man:{3}";
        /// <summary>
        /// Các key của object Giao dịch cổ đông lớn và nội bộ - object List string
        /// </summary>
        public const string InternalHistoryKeys = "stock:stockid:{0}:InternalHistory:Keys";
        /// <summary>
        /// Giao dịch cổ phiếu quỹ - object FundHistory
        /// </summary>
        public const string FundHistory = "stock:stockid:{0}:FundHistory:{1}";
        /// <summary>
        /// Các key của object Giao dịch cổ phiếu quỹ - object List string
        /// </summary>
        public const string FundHistoryKeys = "stock:stockid:{0}:FundHistory:Keys";
        #endregion

        #region Trang dữ liệu
        /// <summary>
        /// Top 10 công ty : Tăng giá (PU) / Giảm giá (PD) / KLGD (VD) / PE (PE) / EPS (EPS) / Vốn hóa (MC)
        /// Object List TopStock
        /// </summary>
        public const string KeyTopStock = "stock:center:{0}:type:{1}";
        /// <summary>
        /// Dữ liệu giá hiện tại của các mã theo dữ liệu sàn - object DataTable
        /// </summary>
        public const string KeyRealTimePrice = "stock:realtimeprice:center:{0}";
        /// <summary>
        /// Dữ liệu giá có điều chỉnh do đội data import - object DataTable
        /// </summary>
        public const string KeyPriceData = "stock:price:center:{0}";

        public class KeyTopStockCenter
        {
            public const string All = "ALL";
            public const string Ho = "HO";
            public const string Ha = "HA";
        }
        public class KeyTopStockType
        {
            /// <summary>
            /// Giá tăng
            /// </summary>
            public const string PriceUp = "PU";
            /// <summary>
            /// Giá giảm
            /// </summary>
            public const string PriceDown = "PD";
            /// <summary>
            /// KLGD giảm
            /// </summary>
            public const string VolumeDown = "VD";
            /// <summary>
            /// P/E thấp nhất
            /// </summary>
            public const string PE = "PE";
            /// <summary>
            /// EPS cao nhất
            /// </summary>
            public const string EPS = "EPS";
            /// <summary>
            /// Vốn hóa
            /// </summary>
            public const string MarketCap = "MC";
        }
        #endregion

        #region Trang tin tức công ty
        /// <summary>
        /// Danh sách tất cả tin công ty - object List string - Cấu trúc (yyyyMMddHHmm)(ID tin) - VD : 20110225151354908
        /// </summary>
        public const string KeyCompanyNewsByCate = "stocknews:cate:{0}:list";
        /// <summary>
        /// Danh sách tin theo mã - object List string - Cấu trúc (yyyyMMddHHmm)(ID tin) - VD : 20110225151354908
        /// </summary>
        public const string KeyCompanyNewsByStock = "stockid:{0}:cate:{1}:list";
        /// <summary>
        /// Chi tiết tin công ty - object StockNews
        /// </summary>
        public const string KeyCompanyNewsCompact = "stock:id:{0}:compact";
        /// <summary>
        /// Chi tiết tin công ty - object StockNews
        /// </summary>
        public const string KeyCompanyNewsDetail = "stocknews:id:{0}:detail";
        /// <summary>
        /// Top 20 tin công ty mới nhất object List StockNews
        /// </summary>
        public const string KeyTop20News = "stocknews:top";
        #endregion

        #region Chỉ số sàn
        /// <summary>
        /// Thông tin chỉ số sàn - object TradeCenterStats
        /// </summary>
        public static string KeyCenterIndex = "center:{0}:index";
        #endregion

        #region Box hàng hóa
        /// <summary>
        /// Danh sách các key chứa nội dung crawl được - chia theo tab - List ProductBox
        /// </summary>
        public const string KeyProductBox = "boxhanghoa:tab:{0}:list";
        #endregion

        #region Trang Ceo
        /// <summary>
        /// Trang ceo - object Stock
        /// </summary>
        public const string CeoKey = "ceo:ceocode:{0}:Object";
        public const string CeoImage = "ceo:ceocode:{0}:Image";
        #endregion

        #region Trang Tiến độ BDS
        /// <summary>
        /// Trang BDS - object BDS: mã dự án
        /// </summary>
        public const string BDSKey = "tiendoBDS:sym:{0}:tiendocode:{1}:Object";
        #endregion

        #region Google Tag
        /// <summary>
        /// Google keywords
        /// </summary>
        public const string GoogleTag = "news:keyword:google";
        #endregion

        #region Khớp lệnh theo thời gian
        public const string SessionPrice = "khoplenh:{0}:{1}:{2}"; //khoplenh:symbol:yyyyMMdd:HHmmss
        public const string RealtimeSessionPrice = "khoplenhrealtime:{0}"; //khoplenhrealtime:symbol
        #endregion

        #region Trang Lich su kien
        public const string KeyLichSuKien = "lsk:list";
        public const string KeyLichSuKienTomTat = "lsk:shortlist";
        public const string KeyLichSuKienObject = "lsk:obj:{0}"; //lsk:obj:id
        public const string KeyLichSuKienObjectInList = "{0}:{1}:{2}"; //yyyyMMdd:type:id
        #endregion

        #region Du lieu vi mo
        public const string KeyDLVMImageSrc = "dlvm:imagesrc:{0}"; //yyyyMMdd:type:id
        #endregion

        #region Bond
        public const string BondKey = "bond:{0}:{1}"; //country : type
        #endregion

        #region BCTC Full
        public const string BCTCKey = "bctc:full:{0}:{1}:{2}:{3}:{4}"; //symbol : type : quartertype (= 1 if quarter > 0 | =0 if quarter=0 ) : year : quarter
        #endregion
    }
}
