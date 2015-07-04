<%@ Page Language="C#" ClassName="ViewStatepage" %>

<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI.WebControls.WebParts" %>
<%@ Import Namespace="System.Web.UI.HtmlControls" %>
<%@ Import Namespace="MemcachedProviders.Cache" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        //DistCache.Remove("MemCached_tblHCMIndex");
    
        ltrThongbao.Text = "";
        
        string cacheName = Request.QueryString["cache"];
        if (!string.IsNullOrEmpty(cacheName))
        {
            using (DataTable dt = GetCache(cacheName))
            {
                if (dt != null)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    string s = GetCacheAsString(cacheName);
                    if (!string.IsNullOrEmpty(s))
                    {
                        ltrThongbao.Text = s;
                    }
                    else
                    {
                        ltrThongbao.Text = "Khong co du lieu";
                    }
                }
            }
        }
        else
        {
            ltrThongbao.Text = "QueryString khong hop le";
        }
    }

    private static DataTable GetCache(string key)
    {
        return DistCache.Get<DataTable>(key);
    }

    private static string GetCacheAsString(string key)
    {
        object value = DistCache.Get(key);

        return (value == null ? "" : value.ToString());
    }

    private static void UpdateCache(string key, object value)
    {
        if (DistCache.Get(key) != null) DistCache.Remove(key);

        DistCache.Add(key, value);
    }
</script>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    <asp:Literal runat="server" ID="ltrThongbao"></asp:Literal>
    </form>
</body>
</html>
