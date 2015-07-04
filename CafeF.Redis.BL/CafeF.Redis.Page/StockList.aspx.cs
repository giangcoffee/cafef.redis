using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CafeF.Redis.BL;

namespace CafeF.Redis.Page
{
    public partial class StockList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ret = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine;
            ret += "<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">" + Environment.NewLine;
            var template = "<url><loc>{0}</loc><lastmod>{1}</lastmod><changefreq>{2}</changefreq><priority>{3}</priority></url>" + Environment.NewLine;
           
                ret += string.Format(template, "http://cafef.vn/du-lieu.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/tin-doanh-nghiep.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/tin-doanh-nghiep/trang-2.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/lich-su-kien.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");

                ret += string.Format(template, "http://cafef.vn/home.chn", DateTime.Now.ToString("yyyy-MM-dd"), "always", "1.0");
                ret += string.Format(template, "http://cafef.vn/doc-nhanh.chn", DateTime.Now.ToString("yyyy-MM-dd"), "always", "1.0");
                ret += string.Format(template, "http://cafef.vn/thi-truong-chung-khoan.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/doanh-nghiep.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/tai-chinh-ngan-hang.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/tai-chinh-quoc-te.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/vi-mo-dau-tu.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/hang-hoa-nguyen-lieu.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/doanh-nhan.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/doanh-nghiep-gioi-thieu.chn", DateTime.Now.ToString("yyyy-MM-dd"), "always", "1.0");
                ret += string.Format(template, "http://cafef.vn/thi-truong-chung-khoan/trang-2.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/doanh-nghiep/trang-2.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/tai-chinh-ngan-hang/trang-2.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/tai-chinh-quoc-te/trang-2.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/vi-mo-dau-tu/trang-2.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/hang-hoa-nguyen-lieu/trang-2.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/doanh-nhan/trang-2.chn", DateTime.Now.ToString("yyyy-MM-dd"), "hourly", "1.0");
                ret += string.Format(template, "http://cafef.vn/doanh-nghiep-gioi-thieu/trang-2.chn", DateTime.Now.ToString("yyyy-MM-dd"), "always", "1.0");

            var stocks = StockBL.GetStockList(0);
            var ss =  new List<string>();
            foreach (var stock in stocks)
            {
                ss.Add(stock.Symbol);
            }
            var infos = StockBL.GetStockCompactInfoMultiple(ss);
            foreach (var info in infos)
            {
                ret += string.Format(template, "http://cafef.vn" + Utils.GetSymbolLink(info.Value.Symbol, info.Value.CompanyName, info.Value.TradeCenterId.ToString()), DateTime.Now.ToString("yyyy-MM-dd"), "daily", "1.0");
            }

            ret += "</urlset>";
            Response.Clear();
            Response.Write(ret);
            Response.ContentType = "text/xml";
        }
    }
}
