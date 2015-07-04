using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CafeF.Redis.Page
{
    public partial class Screener : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Bộ lọc chứng khoán | CafeF.vn";
            var ctlMetaDesc = this.Header.FindControl("description") as HtmlMeta;
            if (ctlMetaDesc != null)
            {
                ctlMetaDesc.Attributes["content"] = "Tìm kiếm, lọc cổ phiếu niêm yết theo gần 30 tiêu chí khác nhau theo giá, vốn hóa và các thông tin tài chính khác";
            }
        }
    }
}
