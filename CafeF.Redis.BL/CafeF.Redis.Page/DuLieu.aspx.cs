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
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
namespace CafeF.Redis.Page
{
    public partial class DuLieu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
        //protected override void Render(HtmlTextWriter writer)
        //{
        //    using (HtmlTextWriter htmlwriter = new HtmlTextWriter(new System.IO.StringWriter()))
        //    {
        //        base.Render(htmlwriter);
        //        string html = htmlwriter.InnerWriter.ToString();

        //        //if ((ConfigurationManager.AppSettings.Get("RemoveWhitespace") + string.Empty).Equals("true", StringComparison.OrdinalIgnoreCase))
        //        //{
        //            html = Regex.Replace(html, @"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}", string.Empty);
        //            //html = Regex.Replace(html, @"[ \f\r\t\v]?([\n\xFE\xFF/{}[\];,<>*%&|^!~?:=])[\f\r\t\v]?", "$1");
        //            html = Regex.Replace(html, @"^\s+<", " <", RegexOptions.Multiline);
        //            html = Regex.Replace(html, @">\s+<", "> <", RegexOptions.Multiline) ;
        //            html = html.Replace(";\n", ";");
        //        //}

        //        writer.Write(html.Trim());
        //    }
        //}

        //protected override void Render(HtmlTextWriter writer)
        //{
            //if (this.Request.Headers["X-MicrosoftAjax"] != "Delta=true")
            //{
            //    Regex reg = new Regex(@"<script[^>]*>[\w|\t|\r|\W]*?</script>");
            //    StringBuilder sb = new StringBuilder();
            //    StringWriter sw = new StringWriter(sb);
            //    HtmlTextWriter hw = new HtmlTextWriter(sw);
            //    base.Render(hw);
            //    string html = sb.ToString();
            //    MatchCollection mymatch = reg.Matches(html);
            //    html = reg.Replace(html, string.Empty);
            //    reg = new Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}|(?=[\r])\s{2,}");
            //    html = reg.Replace(html, string.Empty);
            //    reg = new Regex(@"</body>");
            //    string str = string.Empty;
            //    foreach (Match match in mymatch)
            //    {
            //        str += match.ToString();
            //    }
            //    html = reg.Replace(html, str + "</body>");
            //    writer.Write(html);
            //}
            //else
            //    base.Render(writer);
        //}
    }
}
