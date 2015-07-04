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
using System.Net;
using System.IO;
using System.Text;
using CafeF.Redis.BO;
using ServiceStack.Redis;
using CafeF.Redis.BL;
using CafeF.BCTC.BO.Utilitis;

namespace CafeF.Redis.Page
{
    public partial class BondChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            int co = Request.QueryString["country"] != null ? int.Parse(Request.QueryString["country"]) : 0;
            int type = Request.QueryString["type"] != null ? int.Parse(Request.QueryString["type"]) : 0;
            LoadImage(co, type, DateTime.Now);
        }

        private string CreatePostData(int country, int type, DateTime da)
        {
            string startStr = "01/01/" + da.Year.ToString();
            DateTime start = DateTime.Parse(startStr);
            string countryStr = ReturnCountryName(country);
            //DataTable dt = BondHelper.GetBondByTimeAndCountryAndType(start, da, countryStr, type);
            var bond = BondBL.GetBond(countryStr, type.ToString());
            string srcImg = "";
            int count = bond.BondValues.Count;
            if (bond != null && count > 0)
            {
                string chd = "";
                string chxl = "";
                string chxp = "";
                
                for (int i = count - 1; i >= 0; i--)
                {
                    chd += String.Format("{0:f}", bond.BondValues[i].ClosePrice) + ",";
                }

                double size = count / 4;
                int its = int.Parse(Math.Floor(size).ToString());
                
                //int day1 = 30 - bond.BondValues[count-1].TradeDate.Day;
                //int day4 = 30 - bond.BondValues[0].TradeDate.Day - 1;
                //chxp += day1 + ",";
                //int temp = count - day1 - day4;
                //int day6 = 30 - bond.BondValues[0].TradeDate.Day - 1;
                //chxl += bond.BondValues[day6].TradeDate.ToString("MM/yy");
                //chxp += (count - day6).ToString();
                for (int j = count-its; j > 0; j = j - its)
                {
                    chxl += "Q" + (bond.BondValues[j].TradeDate.Month + 2) / 3 + "/" + bond.BondValues[j].TradeDate.ToString("yy") +  "|";
                }

                for (int m = its/2; m < count; m = m + its)
                {
                    chxp += m.ToString() + ",";
                }

                string[] range = chd.TrimEnd(',').Split(',');
                bubbleSort(range, range.Length);
                string max = Math.Ceiling(decimal.Parse(range[range.Length-1])).ToString();
                string min = Math.Floor(decimal.Parse(range[0])).ToString();
                srcImg = "http://chart.apis.google.com/chart?chf=a,s,0000009B|bg,s,EFEFEF";
                srcImg += "&chxr=0,1," + bond.BondValues.Count.ToString() + "|1," + min + "," + max;
                srcImg += "&chxt=x,y";
                srcImg += "&chs=260x180";
                srcImg += "&cht=lc";
                srcImg += "&chds=" + min + "," + max;
                srcImg += "&chd=t:" + chd.TrimEnd(',');
                srcImg += "&chg=25,-1,1,1";
                srcImg += "&chls=2";
                srcImg += "&chma=40,20,20,30";
                srcImg += "&chco=FF6D00";
                srcImg += "&chm=B,FFB05D89,0,0,0";
                srcImg += "&chxl=0:|" + chxl.TrimEnd('|');
                srcImg += "&chxp=0," + chxp.TrimEnd(',');
            }
            return srcImg;
        }

        private void LoadImage(int country, int type, DateTime dt)
        {
            //byte[] imgByte;
            //string keyr = String.Format("Cafef.Chart.Bond.BinaryData{0}.{1}.{2}", country, type, dt.ToString("dd/MM/yyyy"));
            //imgByte = this.GetImageFromRedis(keyr, imgByte);
            if (!GetImageFromCache(country, type, dt))
            {
                try
                {
                    Random rnd = new Random();
                    string url = "http://chart.apis.google.com/chart?chid=" + rnd.Next(10000000);
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                    string proxy = null;
                    string postData = this.CreatePostData(country, type, dt);
                    byte[] buffer = Encoding.UTF8.GetBytes(postData);
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded";
                    req.ContentLength = buffer.Length;
                    req.Proxy = new WebProxy(proxy, true); // ignore for local addresses
                    req.CookieContainer = new CookieContainer(); // enable cookies

                    Stream reqst = req.GetRequestStream(); // add form data to request stream
                    reqst.Write(buffer, 0, buffer.Length);
                    reqst.Flush();
                    reqst.Close();
                    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    Stream resst = res.GetResponseStream();
                    Byte[] bf = new byte[2048];
                    Response.Clear();
                    int read = resst.Read(bf, 0, 2048);
                    Byte[] bimg = new byte[20480];
                    int lenimg = 0;
                    if (read > 0)
                    {
                        while (read > 0)
                        {
                            bf.CopyTo(bimg, lenimg);
                            lenimg += read;
                            Response.OutputStream.Write(bf, 0, read);
                            read = resst.Read(bf, 0, 2048);
                        }
                        //save to cache in 1 hour
                        string cacheName = String.Format("Cafef.Chart.Bond.BinaryData.{0}.{1}.{2}", country, type, dt.ToString("dd/MM/yyyy"));
                        Byte[] bimg2 = new byte[lenimg];
                        Array.Copy(bimg, 0, bimg2, 0, lenimg);
                        CacheUtils.AddChartDataToDistributedCache(cacheName, bimg2, 3600);
                        //this.GetImageFromRedis(keyr, bimg2);
                    }
                    resst.Flush();
                    resst.Close();
                    Response.ContentType = "image/png";
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
            //else
            //{
            //    Response.Clear();
            //    Response.OutputStream.Write(imgByte, 0, imgByte.Length);
            //    Response.ContentType = "image/png";
            //}
        }

        private string ReturnCountryName(int c)
        {
            string re = "";
            if (c == 1) { re = "Việt Nam"; }
            else if (c == 2) { re = "Hoa Kỳ"; }
            else if (c == 3) { re = "Anh"; }
            else if (c == 4) { re = "Trung Quốc"; }
            else if (c == 5) { re = "Nhật Bản"; }
            else if (c == 6) { re = "Australia"; }
            else if (c == 7) { re = "Hy Lạp"; }
            else if (c == 8) { re = "Đức"; }
            return re;
        }

        private void bubbleSort(string[] unsortedArray, int length)
        {
            string temp = "";
            int counter, index;
            for (counter = 0; counter < length - 1; counter++)
            { //Loop once for each element in the array.
                for (index = 0; index < length - 1 - counter; index++)
                { //Once for each element, minus the counter.
                    if (double.Parse(unsortedArray[index]) > double.Parse(unsortedArray[index + 1]))
                    { //Test if need a swap or not.
                        temp = unsortedArray[index]; //These three lines just swap the two elements:
                        unsortedArray[index] = unsortedArray[index + 1];
                        unsortedArray[index + 1] = temp;
                    }
                }
            }
        }

        private bool GetImageFromCache(int country, int type, DateTime dt)
        {
            try
            {
                if ((ConfigurationManager.AppSettings["AllowImageCache"] ?? "true") != "true") return false;
                string cacheName = String.Format("Cafef.Chart.Bond.BinaryData.{0}.{1}.{2}", country, type, dt.ToString("dd/MM/yyyy"));
                Byte[] bf = CacheUtils.GetChartDataFromDistributedCache<Byte[]>(cacheName);
                if (bf == null || bf.Length == 0) return false;
                Response.Clear();

                Response.OutputStream.Write(bf, 0, bf.Length);
                Response.ContentType = "image/png";
                return true;

            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        //private byte[] GetImageFromRedis(string keyName, byte[] image)
        //{
        //    byte[] imageBinary;
        //    var redis = new RedisClient(ConfigurationManager.AppSettings["ServerRedisMaster"] ?? "", int.Parse(ConfigurationManager.AppSettings["PortRedisMaster"] ?? "0"));
        //    var key = string.Format("bond:imageBinary:{0}", keyName);
        //    var done = (redis.ContainsKey(key) && !string.IsNullOrEmpty(redis.Get<string>(key) ?? ""));
        //    if (!done)
        //    {
        //        if (redis.ContainsKey(key))
        //            redis.Set(key, image, new TimeSpan(0, 30, 0));
        //        else
        //            redis.Add(key, image, new TimeSpan(0, 30, 0));
        //    }
        //    else
        //    {
        //        imageBinary = redis.Get<byte[]>(key);
        //    }
        //    return imageBinary;
        //}
    }
}
