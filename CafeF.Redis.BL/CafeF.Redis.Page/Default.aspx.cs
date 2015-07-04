using System;
using System.Collections;
using System.Collections.Generic;
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

using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Data;
namespace CafeF.Redis.Page
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(("," + "1,2,3,4" + ",").IndexOf("," + "5" + ","));
            //Stock myStock = new Stock();
            //myStock.Symbol = "SHB";
            //CompanyProfile profile = new CompanyProfile();
            //CommonInfo info = new CommonInfo();
            //info.Content = "Ngân hàng thương mại cổ phần SHB";
            //profile.commonInfos = info;
            //myStock.companyProfile = profile;
            //// Serialize
            //string json = Utility.Serialize<Stock>(myStock);
            //// Deserialize
            //BLFACTORY.RedisClient.Set(String.Format("Stock:{0}:Symbol", "SHB"), "SHB");
            //BLFACTORY.RedisClient.Set(String.Format("stockid:{0}:Object", "SHB"), json);
            //string objValue = BLFACTORY.RedisClient.GetString(String.Format("stockid:{0}:Object", "SHB"));

            //Stock myDeStock = (Stock)Utility.Deserialize<Stock>(objValue);
            //ltr.Text = myDeStock.companyProfile.commonInfos.Content;

           // Response.Write(BLFACTORY.RedisClient.GetString("stock:stockid:SHB:Object"));
            //string objValue = BLFACTORY.RedisClient.GetString("newsid:ACB:6:6:Object");
            //News myN = (News)Utility.Deserialize<News>(objValue);
            //Response.Write(myN.Title);

            //BLFACTORY.RedisClient.Remove("newsid:ACB:6:6:Object");
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           Stock st =  BLFACTORY.RedisClient.Get<Stock>("stock:stockid:ACB:Object");
           DateTime dt = DateTime.Now;
            dt = dt.AddDays(-10);
            StockCompactHistory sch = new StockCompactHistory();
            List<PriceCompactHistory> Price  = new List<PriceCompactHistory>();
            List<OrderCompactHistory> Orders = new List<OrderCompactHistory>();
            List<ForeignCompactHistory> Foreign = new List<ForeignCompactHistory>();
            int i = 10;
            Random r = new Random();
            while (dt.CompareTo(DateTime.Now) < 0)
            {
                PriceCompactHistory pc = new PriceCompactHistory();
                pc.Symbol = "ACB";
                pc.BasicPrice = i;
                pc.Ceiling = i * 1.05;
                pc.ClosePrice = (i % 2 == 0 ? i + r.NextDouble() : i - r.NextDouble());
                pc.Floor = i*0.95;
                pc.TotalValue = 10000000000;
                pc.TradeDate = dt;
                pc.Volume = i * 100;
                Price.Add(pc);

                OrderCompactHistory od = new OrderCompactHistory();
                od.Symbol = "ACB";
                od.TradeDate = dt;
                od.AskAverageVolume = i * 1500;
                od.AskLeft = i * 49;
                od.BidAverageVolume = i * 2000;
                od.BidLeft = i * 50;
                Orders.Add(od);

                ForeignCompactHistory fr = new ForeignCompactHistory();
                fr.Symbol = "ACB";
                fr.TradeDate = dt;
                fr.BuyPercent = (double)i / r.Next(i,100);
                fr.NetVolume = i * 300;
                fr.SellPercent = (double)i / r.Next(i, 100);
                Foreign.Add(fr);

                dt = dt.AddDays(1);
                i++;
            }
            sch.Foreign = Foreign;
            sch.Price = Price;
            sch.Orders = Orders;
            st.StockPriceHistory = sch;
           BLFACTORY.RedisClient.Set("stock:stockid:ACB:Object", st);
        }
    }
}
