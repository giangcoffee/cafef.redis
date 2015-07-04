<%@ Page Language="C#" %>

<%@ Import Namespace="ServiceStack.Redis" %>
<%@ Import Namespace="CafeF.Redis.Entity" %>
<%@ Import Namespace="CafeF.Redis.Data" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="CafeF.Redis.BL" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Runtime.Serialization.Formatters.Binary" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%
      
            // Convert an object to a byte array
            //private byte[] ObjectToByteArray(Object obj)
            //{
            //if(obj == null)
            //    return null;
            //    var obj = StockBL.getStockBySymbol("SSI");
            //var bf = new BinaryFormatter();
            //var ms = new MemoryStream();
            //bf.Serialize(ms, obj);
            //    Response.Write("Stock : " + ms.ToArray().LongLength + "<br />");
            //    bf = new BinaryFormatter();
            //    ms = new MemoryStream();
            //    bf.Serialize(ms, obj.CompanyProfile);
            //    Response.Write("Stock Profile : " + ms.ToArray().LongLength + "<br />");
            //    bf = new BinaryFormatter();
            //    ms = new MemoryStream();
            //    bf.Serialize(ms, obj.StockNews);
            //    Response.Write("Stock News : " + ms.ToArray().LongLength + "<br />");
            //    bf = new BinaryFormatter();
            //    ms = new MemoryStream();
            //    bf.Serialize(ms, obj.Reports3);
            //    Response.Write("Stock Report : " + ms.ToArray().LongLength + "<br />");
            //    bf = new BinaryFormatter();
            //    ms = new MemoryStream();
            //    bf.Serialize(ms, obj.SameCategory);
            //    Response.Write("Stock SameCate : " + ms.ToArray().LongLength + "<br />");
            //    bf = new BinaryFormatter();
            //    ms = new MemoryStream();
            //    bf.Serialize(ms, obj.SameEPS);
            //    Response.Write("Stock EPS : " + ms.ToArray().LongLength + "<br />");
            //    bf = new BinaryFormatter();
            //    ms = new MemoryStream();
            //    bf.Serialize(ms, obj.SamePE);
            //    Response.Write("Stock PE : " + ms.ToArray().LongLength + "<br />");
            //    bf = new BinaryFormatter();
            //    ms = new MemoryStream();
            //    bf.Serialize(ms, obj.StockPriceHistory);
            //    Response.Write("Stock Price History : " + ms.ToArray().LongLength + "<br />");
            //var pr = StockBL.getStockPriceBySymbol("SSI");
            //var stock = StockBL.getStockBySymbol("SSI");
            //var totalroom = pr.ForeignTotalRoom > 0 ? pr.ForeignTotalRoom : ((stock.IsBank ? 0.3 : 0.49) * stock.CompanyProfile.basicInfos.basicCommon.VolumeTotal);
            //Response.Write(pr.ForeignTotalRoom + "<br />" + totalroom + "<br />" + ((pr.ForeignCurrentRoom / totalroom) * 100).ToString("#,##0.#"));
            //if (totalroom > 0)
            //    ltrRoomNNConlai.Text = pr.ForeignCurrentRoom > totalroom ? "100" : ((pr.ForeignCurrentRoom / totalroom) * 100).ToString("#,##0.#");
            //else
            //    ltrRoomNNConlai.Text = "0";

            //    return ms.ToArray();
            //}
            //var redis1 = new RedisClient("192.168.6.29", 1111);
            //var redis2 = new RedisClient("192.168.8.214", 1111);
            //var key = "test.hieubt.{0}";
            //for (var i = 0; i < 10; i++)
            //{
            //    redis1.Add(string.Format(key, i), i);
            //}
            //for (var i = 0; i < 10; i++)
            //{
            //    if (!redis2.ContainsKey(string.Format(key, i)))
            //        Response.Write("- NotExist : " + i + "<br />");
            //    if (i.ToString() != redis2.Get<int>(string.Format(key, i)).ToString())
            //        Response.Write("- Wrong : " + i + "<br />");
            //}
            //for (var i = 0; i < 10; i++)
            //{
            //    redis1.Remove(string.Format(key, i));
            //}
            //var redis = new RedisClient("192.168.8.214", 1111);
            //var keylist = RedisKey.KeyAnalysisReport;
            //var ls = (redis.ContainsKey(keylist)) ? redis.Get<List<string>>(keylist) : new List<string>();
            //Response.Write(ls[0] + "<br />"  + ls[1]);
            //var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            //var code = "ITA_03266";
            //var codes = new List<string>() { "ITA_03266" };
            //var CeoPhotos = CeoBL.GetCeoImage(ls);
            //Response.Write(CeoPhotos[code]);
            //if (CeoPhotos.ContainsKey(code) && !string.IsNullOrEmpty(CeoPhotos[code]))
            //{
            //    Response.Write(CeoPhotos[code]); 
            //}
            //Response.Write("noimage.jpg");
            //var ss = new List<string>();
            //ss.AddRange(codes);
            //for (var i = 0; i < codes.Count; i++)
            //{
            //    ss[i] = string.Format(RedisKey.CeoImage, codes[i]);
            //}
            //var tmp = BLFACTORY.RedisClient.GetAll<string>(ss);
            //Response.Write(tmp[string.Format(RedisKey.CeoImage, ss[0])]);
            //var t = BLFACTORY.RedisClient.Get<string>(string.Format(RedisKey.CeoImage, code));
            //Response.Write(t);

            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var keys = redis.SearchKeys(string.Format(RedisKey.SessionPrice, "SSI", DateTime.Now.ToString("yyyyMMdd"), "*"));
            Response.Write(keys.Count);
            foreach (var key in keys)
            {
                var o = redis.Get<SessionPriceData>(key);
                Response.Write(o.Symbol + "-" + o.Price + " - " + o.Volume + "-" + o.TradeDate.ToString());
            }
            
        %>
    </div>
    </form>
</body>
</html>
