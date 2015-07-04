using System;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
namespace CafeF.Redis.BO
{
    public static class ManagerSetting
    {
        public static int expireHour;
        public static string savedFolder;
        public static string ImgURL;
        public static string ImagesBig;
        static ManagerSetting()
        {
            // Cache all these values in static properties.
            expireHour = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("expireHour"));
            savedFolder = ConfigurationSettings.AppSettings.Get("savedFolder");
            ImgURL = ConfigurationSettings.AppSettings.Get("imageUrl");
            ImagesBig = ConfigurationSettings.AppSettings.Get("ImagesBig");
        }
    }
    public class Const
    {
        //News_Mode
        public const int TIN_THUONG = 0;
        public const int TIN_NOI_BAT_TRANG_CHU = 2;
        public const int TIN_NOI_BAT_TRONG_MUC = 1;
        public const int TIN_TIEU_DIEM = 1;

        //News_Status
        public const int TRANG_THAI_TIN_DUOC_DANG = 3;
        public const int TRANG_THAI_TIN_BI_GO = 5; //TIN BỊ GỠ

        //Header
        public const string MENU_HOME = "home";
        public const string MENU_STAR = "star";
        public const string MENU_LIFE = "life";
        public const string MENU_MUSIK = "music";
        public const string MENU_FASHION = "fashion";
        public const string MENU_LOGIN = "login";
        public const string MENU_REGISTER = "register";
        public const string MENU_CINE = "cine";

        //Content
        public const string CHI_TIET_TIN = "newsdetail";
        //Rank MUSIC
        public const int RANK_MUSIC_LEFT = 1;
        public const int RANK_MUSIC_RIGHT = 2;

        //Status Rank
        public const string GIAM_RANK = "-1";
        public const string GIU_NGUYEN = "0";
        public const string TANG_RANK = "1";
        public const string NEW_RANK = "2";

        public const int CAT_MUSIC = 3;
        public const int CAT_FASION = 5;
        public const int CAT_TIEUDIEM_FASHION = 22;

        public const int NumberOfShoppingFocus = 3;

        public const int NumberOfStyleUpVote = 8;
        public const int NumberOfPreviousMonthStyleUpVote = 3;
        public const int NumberOfShowedCatIcon = 3;

        public const double VOTE_PROGRESS_BAR_LENGTH = 100;

        public static string ImageUploadedPath = System.Configuration.ConfigurationSettings.AppSettings.Get("ImageUploaded");
        //HASTC
        public const string HAMARKET_TOPUP = "HAMARKET_TOPUP";
        public const string HAMARKET_TOPDOWN = "HAMARKET_TOPDOWN";
        public const string HAMARKET_TOPTRANS = "HAMARKET_TOPTRANS";
        //HOSTC
        public const string HOMARKET_TOPUP = "HOMARKET_TOPUP";
        public const string HOMARKET_TOPDOWN = "HOMARKET_TOPDOWN";
        public const string HOMARKET_TOPTRANS = "HOMARKET_TOPTRANS";

        //Portfolio users
        public const string PORTFOLIO_USERS = "PORTFOLIO_USERS";
        //key cho content cua cache
        public const string KEY_CONTENT_MEM_CACHE = "__content";
        //tat ca cac ma cua hai thi truong theo mau rut gon 
        public const string SIMPLE_HA_STOCKSDATA = "sHaStockCache";
        public const string SIMPLE_HO_STOCKSDATA = "sHoStockCache";
        //keyword theo category
        //public const string TCCK_KEYWORD = "chứng khoán, cổ phiếu, trái phiếu, vn index, hastc index, chỉ số, blue-chip, chốt danh sách, giá tham chiếu, giá trần, giá sàn, trực tuyến, ATO, ATC, bán tháo, cổ tức, danh mục, niêm yết, OTC, kết quả kinh doanh, phái sinh, đỏ sàn, thưởng, bán khống, option, futures, STB, FPT, SSI, ACB, VNM, DPM, PVD, PVI, KDC, VCB, REE, SAM, SJS, vietcombank, cổ đông, IPO, sông đà, đa ngành, EPS, P/E, PE, vốn hóa, chia thưởng, cổ phiếu quỹ, nước ngoài, khối ngoại, giao dịch, sàn, Thành Công, hàng khủng, đại hội, đấu giá, dữ liệu, lợi nhuận, đạm phú mỹ, lịch sử giá, bong bóng, cổ phần hóa, nguyễn duy hưng, vũ bằng, đại gia, biên độ";
        public const string TCCK_KEYWORD = "Chứng khoán, giao dịch, cổ phiếu, trái phiếu, niêm yết, otc, kết quả kinh doanh, báo cáo tài chính, lợi nhuận, biên độ giao dịch, room, giao dịch nước ngoài";
        public const string TCCK_DESCRIPTION = "Tin tức thị trường chứng khoán Việt Nam{0}";
        public const string TCCK_TITLE_DES = "Tin tức thị trường chứng khoán Việt Nam{0} | CafeF.vn";
        public const string TCCK_NEWFOCUS_TITLE = "Các bài viết chọn lọc về thị trường chứng khoán";

        //public const string BDS_KEYWORD = "nhà đất, nhà ở, bất động sản, địa ốc, môi giới, bong bóng, chung cư, căn hộ, thuê nhà, cao ốc, giá nhà, biệt thự, Hà Tây, Vista, Ciputra, nền, sổ đỏ, sổ hồng, An Khánh, móng, nền, nhà cho thuê, đô thị, sinh thái, Thủ Thiêm, Phú Mỹ Hưng, Manor, CBRE, Vincom, sacomreal, Phú Mỹ, Quỹ nhà, địa giới, mở rộng, quy hoạch, vật liệu, xây dựng, towers, sàn, đền bù, giải tỏa, Nguyễn Hùng Cường, Hồng Anh, Đặng Văn Thành ";
        public const string BDS_KEYWORD = "Đầu tư, nhà ở, cao ốc, văn phòng,mặt bằng  bán lẻ, cho thuê, khu đất vàng, dự án, sàn giao dịch";
        public const string BDS_DESCRIPTION = "Tin tức cập nhật về thị trường bất động sản Việt Nam{0}";
        public const string BDS_TITLE_DES = "Thông tin thị trường bất động sản Việt Nam{0} | CafeF.vn";
        public const string BDS_NEWFOCUS_TITLE = "Các bài viết chọn lọc về Bất động sản";

        //public const string TCNH_KEYWORD = "sàn vàng, ACB, SJC, Bảo tín, ounce, lượng, nhập khẩu, chế tác, trang sức, vàng miếng, oz, giá vàng, thế giới, trong nước, cầm cố, đặt cọc, tín dụng, lãi suất, lãi suất cơ bản, dư nợ, cho vay, giải ngân, giải chấp, lê xuân nghĩa, thống đốc, Nguyễn Văn Giàu, Lê Đức Thúy, ngân hàng, đầu tư, quỹ, đặng văn thành, sacombank, thương tín, ngân hàng, sài gòn, vietcombank, ngoại thương, kỹ thương, techcombank, quốc doanh, nông thôn, thành thị, thương mại, NHNN, hạn mức, tiết kiệm, tiền gửi, rủi ro, vỡ nợ, lạm phát, đô la, USD, euro, tiền tệ, habubank, chứng khoán, Á Châu, thuế, chính sách tiền tệ ";
        public const string TCNH_KEYWORD = "Ngân hàng, Tín dụng, lãi suất, vàng SJC, ACB, tỷ giá, tiền tệ, thẻ ATM, huy động, cho vay, thế chấp, cầm cố, giải chấp, đầu tư, VND, đô la, dư nợ, vốn điều lệ";
        public const string TCNH_DESCRIPTION = "Thông tin về hệ thống ngân hàng và các tổ chức tín dụng Việt Nam{0}";
        public const string TCNH_TITLE_DES = "Tin tức hoạt động tài chính ngân hàng{0} | CafeF.vn";
        public const string TCNH_NEWFOCUS_TITLE = "Các bài viết chọn lọc tài chính - ngân hàng";

        //public const string TCQT_KEYWORD = "tài chính, Fed, ben bernanke, phố wall, citibank, citigroup, HSBC, merrill lynch, Bear Sterns, trung quốc, chứng khoán, dow, FTSE, nasdaq, S&P 500, thị trường, châu âu, mỹ, khủng hoảng, nợ dưới chuẩn, nhà đất Mỹ, môi giới, cầm cố, lãi suất, cơ bản, chiết khấu, vàng, giá dầu, vùng Vịnh, khai thác, CEO, countrywide, goldman sach, lương thực, lạm phát, thiệt hại, Mỹ, Việt Nam, kinh tế, financial times, bloomberg, thuế, chính sách, USD, đô la, ngân hàng, trung ương, cắt giảm, lãi suất, chiết khấu, thế chấp nhà,";
        public const string TCQT_KEYWORD = "Kinh tế Mỹ, kinh tế châu Á, kinh tế Trung Quốc, Dầu thô, khủng hoảng tài chính, thị trường nhà đất, tín dụng, tăng trưởng, suy thoái, chu kỳ kinh tế";
        public const string TCQT_DESCRIPTION = "Tin tức về diễn biến kinh tế và thị trường tài chính thế giới{0}";
        public const string TCQT_TITLE_DES = "Tin tức tài chính quốc tế{0} | CafeF.vn";
        public const string TCQT_NEWFOCUS_TITLE = "Các bài viết chọn lọc Tài chính quốc tế";

        //public const string DN_KEYWORD = "doanh nghiệp, ngân hàng, tài chính, sản xuất, nông nghiệp, mía đường, đóng tàu, thủy sản, đầu tư, vay vốn, Vafi, dầu tường an, SSI, nguyên liệu, xuất khẩu, nhập khẩu, báo cáo, niêm yết, cổ phần, vốn, phá sản, việt nam, thành lập, bảo hiểm, tập đoàn, công ty, cổ đông, đại hội, quản lý, giấy, gạo, muối, đa ngành, EVN, bảo việt, REE, ngoại thương, FPT, Sudico, Á Châu, sông đà, số liệu, khuyến mãi, giảm giá, tiêu dùng";
        public const string DN_KEYWORD = "Công ty niêm yết, Đối tác chiến lược, kết quả kinh doanh, báo cáo tài chính, IPO, OTC, cổ phiếu quỹ, tỷ lệ sở hữu, kiểm toán";
        public const string DN_DESCRIPTION = "Tin tức sự kiện các công ty niêm yết{0}";
        public const string DN_TITLE_DES = "Thông tin tài chính và hoạt động kinh doanh của Doanh nghiệp{0} | CafeF.vn";
        public const string DN_NEWFOCUS_TITLE = "Các bài viết chọn lọc mục doanh nghiệp";

        //public const string KTDT_KEYWORD = "FDI, ODA, đầu tư, trực tiếp, gián tiếp, Singapore, Nhật Bản, Hàn Quốc, liên hợp quốc, ADB, ngân hàng thế giới, nguyễn tấn dũng, lạm phát, lọc dầu, dung quất, hạ tầng, muối, gạo, cà phê, hồ tiêu, thuế thu nhập, thuế ô tô, nội địa, nước ngoài, than, quảng ninh, nông nghiệp, công nghiệp, thương mại, dịch vụ, giao thông, hàng hóa, thiết yếu, cơ bản, thị trường, khu công nghiệp, cảng biển, nhập siêu, cán cân thương mại, chi tiêu CP, hàng không, vietnam airlines, pacafic, đông á, Asean, bộ trưởng";
        public const string KTDT_KEYWORD = "FDI, GDP, đầu tư nước ngoài,kiều hối, lạm phát, tăng trưởng, thất nghiệp, thuế, dự án đầu tư, ngân sách, xăng dầu";
        public const string KTDT_DESCRIPTION = "Tin tức kinh tế vĩ mô và hoạt động đầu tư tại Việt Nam{0}";
        public const string KTDT_TITLE_DES = "Tin tức hoạt động kinh tế và đầu tư tại Việt Nam{0} | CafeF.vn";
        public const string KTDT_NEWFOCUS_TITLE = "Các bài viết chọn lọc mục kinh tế - đầu tư";
        //thi truong hang hoa
        public const string TTHH_KEYWORD = "nông sản,thống kê,thủy sản,cao su,cafe";
        public const string TTHH_DESCRIPTION = "Tin tức diễn biến thị trường hàng hóa{0}";
        public const string TTHH_TITLE_DES = "Tin tức diễn biến thị trường hàng hóa{0} | CafeF.vn";
        public const string TTHH_NEWFOCUS_TITLE = "Các bài viết chọn lọc mục Thị trường hàng hóa";

        // Thong tin doanh nghiep
        public const string TTDN_KEYWORD = "Tin tức, sự kiện, công ty, niêm yết";
        public const string TTDN_DESCRIPTION = "Tin tức sự kiên công ty niêm yết{0}";
        public const string TTDN_TITLE_DES = "Thông tin các công ty niêm yết{0} | CafeF.vn";
        // Thong tin doanh nghiep (Theo tung cong ty)
        public const string TTDN_BY_COMPANY_KEYWORD = "{0}, {1}, tin tức, sự kiện";
        public const string TTDN_BY_COMPANY_DESCRIPTION = "Tin tức, sự kiện công ty {0}{1}";
        public const string TTDN_BY_COMPANY_TITLE_DES = "Tin tức - sự kiện {0}{1} | CafeF.vn";

        // Y kien chuyen gia
        public const string YKCG_KEYWORD = "ý kiến, nhận định, đánh giá, chuyên gia";
        public const string YKCG_DESCRIPTION = "Ý kiến, nhận định, đánh giá của các chuyên gia tài chính";
        public const string YKCG_TITLE_DES = "Ý kiến chuyên gia | CafeF.vn";

        // Y kien chuyen gia
        public const string KTTC_KEYWORD = "ý kiến, nhận định, đánh giá, chuyên gia,tài chính quốc tế, trong nước,biến động";
        public const string KTTC_DESCRIPTION = "Tình hình kinh tế tài chính năm 2008";
        //public const string KTTC_TITLE_DES = "Sự kiện kinh tế - tài chính 2008 và triển vọng 2009 | CafeF.vn";
        public const string KTTC_TITLE_DES = "CafeF - Kết quả kinh doanh quý II/2009 | CafeF.vn";

        // Du lieu doanh nghiep
        public const string DLDN_KEYWORD = "công ty, mã chứng khoán, download báo cáo tài chính, ngành";
        public const string DLDN_DESCRIPTION = "Ý kiến, nhận định, đánh giá của các chuyên gia tài chính{0}";
        public const string DLDN_TITLE_DES = "Dữ liệu doanh nghiệp{0} | CafeF.vn";

        // Danh sach cong ty niem yet
        public const string DSCTNY_KEYWORD = "công ty, mã chứng khoán, download báo cáo tài chính, ngành";
        public const string DSCTNY_DESCRIPTION = "Danh sách công ty niêm yết{0}{1}";
        public const string DSCTNY_TITLE_DES = "Danh sách công ty niêm yết{0}{1} | CafeF.vn";

        // Phần title_des của category không cần thêm tiếp đầu ngữ cafef.vn - (tên chuyên mục) - (giải thích ngắn ngọn)
        // _TITLE_DES là giải thích ngắn gọn chuyên mục cần nói gì

        public const string TTNY_KEYWORD = "Thị trường, chỉ số Vn-index, chỉ số Hastc-index, công ty niêm yết, dữ liệu";
        //public const string TTNY_DESCRIPTION = "Diễn biến chỉ số Vn-index và Hastc-index  ngày {0} trên 2 sàn Hose và Hastc, dữ liệu các công ty niêm yết trên 2 sàn";
        public const string TTNY_DESCRIPTION = "Diễn biến giao dịch 2 sàn và dữ liệu các công ty niêm yết";
        public const string TTNY_TITLE_DES = "Diễn biến giao dịch và dữ liệu thị trường chứng khoán niêm yết | CafeF.vn";

        // Chi tiet thong tin doanh nghiep (chi tiet thi truong niem yet)
        public const string CTTTNY_KEYWORD = "{1}, {0}, hồ sơ công ty, thông tin giao dịch";
        public const string CTTTNY_DESCRIPTION = "Thông tin giao dịch và hồ sơ {0}";
        public const string CTTTNY_TITLE_DES = "Tin tức và dữ liệu {0} - {1} | CafeF.vn";

        // Danh muc dau tu
        public const string DMDT_KEYWORD = "";
        public const string DMDT_DESCRIPTION = "";
        public const string DMDT_TITLE_DES = "Quản lý danh mục đầu tư | CafeF.vn";

        //mail template
        public const string RESET_PASS = "/MailTemplate/ResetPassword.htm";
        public const string PORTFOLIO_ALER = "";
        public const string NEWS_LETTER = "";
        public const string WELLCOME_PORTFOLIO = "/MailTemplate/WellComePortfolio.htm";


        public const String DATABASE_NAME = "Cafef_Core";
        public const String DATABASE_NAME_MASTER = "FinanceChannel_Cache";
        public const String FINANCE_DATABASE_NAME = "FinanceChannel"; 
        public const String TABLE_NEWSPUBLISHED = "NewsPublished";
        //WellComePortfolio.htm
       
      
        public static string THUMBNAIL_FILE(String ImagePath, int ImageWidthSize)
        {
            if (ImagePath != null && ImagePath != "")
            {
                ImagePath = "/" + ImagePath;
                string thumbServer = ConfigurationSettings.AppSettings.Get("ServerThumbImages");
                string fileNameOrgin = ImagePath.Substring(ImagePath.LastIndexOf('/') + 1); //FileName.Extension
                string[] fileName = fileNameOrgin.Split('.');
                string fileName_Thumb = fileName[0] + "_" + ImageWidthSize + "." + fileName[1];
                //FilenName_Width.Extension
                string fileThum = ImagePath.Replace("/images/", "/" + ManagerSetting.savedFolder + "/").Replace("/Images/", "/" + ManagerSetting.savedFolder + "/").Replace("/" + fileNameOrgin, "/" + fileName_Thumb);
                
                string _path;
                _path = thumbServer + fileThum;
              
                return _path;
            }
            else
            {
                return "";
            }
        }
        public static string THUMBNAIL_LINK(String ImagePath, int ImageWidthSize)
        {
            if (ImagePath != null && ImagePath != "")
            {
                string thumbServer = ConfigurationSettings.AppSettings.Get("ServerGetThumbImages");
                string _path;
                _path = thumbServer + @"/GetThumbNail.ashx?ImgFilePath=" + Const.ImageUploadedPath;
                _path += ImagePath;
                _path += "&width=" + ImageWidthSize;
                return _path;
            }
            else
            {
                return "";
            }
        }
        

       



        //SonPC

        public static void SetDataToCache(DataTable dataTableToCache, string cacheName, string tableNameInDatabase)
        {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(Const.DATABASE_NAME, tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataTableToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }

        public static void SetDataToCache(DataTable dataTableToCache, string cacheName, string tableNameInDatabase, String databaseName)
        {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(databaseName, tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataTableToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }

        public static void SetDataToCache(String dataTableToCache, string cacheName, string tableNameInDatabase, String databaseName)
        {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(databaseName, tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataTableToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }

        public static void SetDataToCache(int dataToCache, string cacheName, string tableNameInDatabase)
        {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(Const.DATABASE_NAME, tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }
        public static void SetDataToCache(string dataToCache, string cacheName, string tableNameInDatabase)
        {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(Const.DATABASE_NAME, tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }
        public static void SetDataToCache(DataTable[] dataToCache, string cacheName, string tableNameInDatabase)
        {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(Const.DATABASE_NAME, tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }
        public static void SetDataToCache(DataSet dataToCache, string cacheName, string tableNameInDatabase)
        {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(Const.DATABASE_NAME, tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }

        public static void SetCache(DataTable dataCache, string cacheName, string[] tableNameInDatabase)
        {
            System.Web.Caching.SqlCacheDependency[] sqlDep = new SqlCacheDependency[tableNameInDatabase.Length];
            for (int i = 0; i < tableNameInDatabase.Length; i++)
                sqlDep[i] = new System.Web.Caching.SqlCacheDependency(Const.DATABASE_NAME, tableNameInDatabase[i]);

            System.Web.Caching.AggregateCacheDependency agg = new System.Web.Caching.AggregateCacheDependency();
            //agg.Add(sqlDep1, sqlDep2);
            agg.Add(sqlDep);
            HttpContext.Current.Cache.Insert(cacheName, dataCache, agg, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }
        public static void SetCache(DataSet dataCache, string cacheName, string[] tableNameInDatabase)
        {
            System.Web.Caching.SqlCacheDependency[] sqlDep = new SqlCacheDependency[tableNameInDatabase.Length];
            for (int i = 0; i < tableNameInDatabase.Length; i++)
                sqlDep[i] = new System.Web.Caching.SqlCacheDependency(Const.DATABASE_NAME, tableNameInDatabase[i]);

            System.Web.Caching.AggregateCacheDependency agg = new System.Web.Caching.AggregateCacheDependency();
            //agg.Add(sqlDep1, sqlDep2);
            agg.Add(sqlDep);
            HttpContext.Current.Cache.Insert(cacheName, dataCache, agg, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }
        /*
           aspnet_regsql.exe -S hostdb -U sa -P sa -d DBName -ed
           aspnet_regsql.exe -S hostdb -U sa -P sa -d DBName -t TableName -et
        */

        /// <summary>
        /// lấy dữ liệu từ cache ra
        /// </summary>
        /// <param name="cacheName">tên cache</param>
        /// <returns></returns>
        public static DataTable GetFromCache(string cacheName)
        {
            return HttpContext.Current.Cache[cacheName] as DataTable;
        }
        public static Int32 GetInt32FromCache(string cacheName)
        {
            if (HttpContext.Current.Cache[cacheName] == null) return 0;
            return (int)HttpContext.Current.Cache[cacheName];
        }
        public static string GetStringFromCache(string cacheName)
        {
            return (string)HttpContext.Current.Cache[cacheName];
        }
        public static DataTable[] GetFromCacheAsTableArray(string cacheName)
        {
            return (DataTable[])HttpContext.Current.Cache[cacheName];
        }
        public static DataSet GetFromCacheAsDataSet(string cacheName)
        {
            return (DataSet)HttpContext.Current.Cache[cacheName];
        }
        //SonPC
    }
}