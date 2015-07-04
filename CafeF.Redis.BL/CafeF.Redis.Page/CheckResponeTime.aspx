<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="CafeF.Redis.Data"%>
<%@ Import Namespace="ServiceStack.Redis"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ Import Namespace="System.Net"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%
            //var client = new WebClient();
            //var t1 = DateTime.Now;
            //var t2 = DateTime.Now;
            //TimeSpan totalTime;
            //t1 = DateTime.Now;
            //client.DownloadData("http://192.168.7.23:8080/home.chn");
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.7.23, ");
            //Response.Write("Site:home.chn, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<br />");
            //t1 = DateTime.Now;
            //client.DownloadData("http://192.168.7.23:8080/du-lieu.chn");
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.7.23, ");
            //Response.Write("Site:du-lieu.chn, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<hr />");

            //t1 = DateTime.Now;
            //client.DownloadData("http://192.168.3.192:8080/home.chn");
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.3.192, ");
            //Response.Write("Site:home.chn, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<br />");
            //t1 = DateTime.Now;
            //client.DownloadData("http://192.168.3.192:8080/du-lieu.chn");
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.3.192, ");
            //Response.Write("Site:du-lieu.chn, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<hr />");

            //t1 = DateTime.Now;
            //client.DownloadData("http://192.168.4.182:8080/home.chn");
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.4.182, ");
            //Response.Write("Site:home.chn, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<br />");
            //t1 = DateTime.Now;
            //client.DownloadData("http://192.168.4.182:8080/du-lieu.chn");
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.4.182, ");
            //Response.Write("Site:du-lieu.chn, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<hr />");

            //t1 = DateTime.Now;
            //client.DownloadData("http://192.168.5.93:8080/home.chn");
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.5.93, ");
            //Response.Write("Site:home.chn, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<br />");
            //t1 = DateTime.Now;
            //client.DownloadData("http://192.168.5.93:8080/du-lieu.chn");
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.5.93, ");
            //Response.Write("Site:du-lieu.chn, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<hr />");

            //t1 = DateTime.Now;
            //client.DownloadData("http://192.168.7.23:8080/home.chn");
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.7.23, ");
            //Response.Write("Site:home.chn, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<br />");
            //t1 = DateTime.Now;
            //client.DownloadData("http://192.168.7.23:8080/du-lieu.chn");
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.7.23, ");
            //Response.Write("Site:du-lieu.chn, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<hr />");

            //t1 = DateTime.Now;
            //client.DownloadData("http://192.168.7.53:8080/home.chn");
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.7.53, ");
            //Response.Write("Site:home.chn, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<br />");
            //t1 = DateTime.Now;
            //client.DownloadData("http://192.168.7.53:8080/du-lieu.chn");
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.7.53, ");
            //Response.Write("Site:du-lieu.chn, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<hr />");

            //t1 = DateTime.Now;
            //client.DownloadData("http://192.168.6.42:8080/home.chn");
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.6.42, ");
            //Response.Write("Site:home.chn, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<br />");
            //t1 = DateTime.Now;
            //client.DownloadData("http://192.168.6.42:8080/du-lieu.chn");
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.6.42, ");
            //Response.Write("Site:du-lieu.chn, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<hr />");

            //t1 = DateTime.Now;
            //client.DownloadData("http://192.168.6.43:8080/home.chn");
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.6.43, ");
            //Response.Write("Site:home.chn, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<br />");
            //t1 = DateTime.Now;
            //client.DownloadData("http://192.168.6.43:8080/du-lieu.chn");
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.6.43, ");
            //Response.Write("Site:du-lieu.chn, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<hr />");

            //SqlConnection sql;
            //SqlCommand cmd;
            //DateTime t1, t2;
            //TimeSpan totalTime;
            //var server = "Data Source={0};User ID=cafef;Password=cafeF123$%^;Initial Catalog=CafeF_Core";

            //t1 = DateTime.Now;
            //sql = new SqlConnection(string.Format(server, "192.168.9.116"));
            //sql.Open();
            //cmd = new SqlCommand("SELECT TOP 10 * FROM NewsPublished ORDER BY News_PublishDate DESC", sql);
            //cmd.ExecuteNonQuery();
            //sql.Close();
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.9.116, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<hr />");

            //t1 = DateTime.Now;
            //sql = new SqlConnection(string.Format(server, "192.168.5.103"));
            //sql.Open();
            //cmd = new SqlCommand("SELECT TOP 10 * FROM NewsPublished ORDER BY News_PublishDate DESC", sql);
            //cmd.ExecuteNonQuery();
            //sql.Close();
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.5.103, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<hr />");

            //t1 = DateTime.Now;
            //sql = new SqlConnection(string.Format(server, "192.168.7.151"));
            //sql.Open();
            //cmd = new SqlCommand("SELECT TOP 10 * FROM NewsPublished ORDER BY News_PublishDate DESC", sql);
            //cmd.ExecuteNonQuery();
            //sql.Close();
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.7.151, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<hr />");

            //t1 = DateTime.Now;
            //sql = new SqlConnection(string.Format(server, "192.168.9.101"));
            //sql.Open();
            //cmd = new SqlCommand("SELECT TOP 10 * FROM NewsPublished ORDER BY News_PublishDate DESC", sql);
            //cmd.ExecuteNonQuery();
            //sql.Close();
            //t2 = DateTime.Now;
            //totalTime = t2 - t1;
            //Response.Write("IP:192.168.9.101, ");
            //Response.Write("Total time:" + totalTime.ToString());
            //Response.Write("<hr />");

            var redis = new RedisClient("192.168.6.29", 1111);
            Response.Write("192.168.6.29:1111  : " + redis.Get<string>(RedisKey.KeyKby) + "<br />");

            redis = new RedisClient("192.168.6.29", 1112);
            Response.Write("192.168.6.29:1112  : " + redis.Get<string>(RedisKey.KeyKby) + "<br />");

            redis = new RedisClient("192.168.6.32", 1111);
            Response.Write("192.168.6.32:1111  : " + redis.Get<string>(RedisKey.KeyKby) + "<br />");

            redis = new RedisClient("192.168.6.32", 1112);
            Response.Write("192.168.6.32:1112  : " + redis.Get<string>(RedisKey.KeyKby) + "<br />");
        %>
    </div>
    </form>
</body>
</html>
