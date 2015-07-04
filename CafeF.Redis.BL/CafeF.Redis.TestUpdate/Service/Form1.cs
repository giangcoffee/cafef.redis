using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using CafeF.Redis.BL;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;
using ServiceStack.Redis;
using CafeF.Redis.BO;

namespace CafeF.Redis.TestUpdate.Service
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        #region Box hàng hóa
        private void button1_Click(object sender, EventArgs e)
        {
            UpdateBoxHangHoa();
        }

        public void UpdateBoxHangHoa()
        {
            try
            {
                //if ((ConfigurationManager.AppSettings["ProductBoxAllowance"] ?? "") != "TRUE")
                //{
                //    return;
                //}
                var tabVN = new List<string>() { ChiTieuCrawler.TabVietnam.VangTheGioi, ChiTieuCrawler.TabVietnam.VangSJC, ChiTieuCrawler.TabVietnam.USDtudo, ChiTieuCrawler.TabVietnam.EURtudo, ChiTieuCrawler.TabVietnam.USDSIN, ChiTieuCrawler.TabVietnam.USDHKD, ChiTieuCrawler.TabVietnam.CNY, ChiTieuCrawler.TabVietnam.BangAnh, ChiTieuCrawler.TabVietnam.USDVCB, ChiTieuCrawler.TabVietnam.EURVCB };
                var tabTG = new List<string>() { ChiTieuCrawler.TabTheGioi.DowJones, ChiTieuCrawler.TabTheGioi.Nasdaq, ChiTieuCrawler.TabTheGioi.SP500, ChiTieuCrawler.TabTheGioi.FTSE100, ChiTieuCrawler.TabTheGioi.DAX, ChiTieuCrawler.TabTheGioi.Nikkei225, ChiTieuCrawler.TabTheGioi.HangSeng, ChiTieuCrawler.TabTheGioi.StraitTimes };
                var tabHH = new List<string>() { ChiTieuCrawler.TabHangHoa.CrudeOil, ChiTieuCrawler.TabHangHoa.NaturalGas, ChiTieuCrawler.TabHangHoa.Gold, ChiTieuCrawler.TabHangHoa.Copper, ChiTieuCrawler.TabHangHoa.Silver, ChiTieuCrawler.TabHangHoa.Corn, ChiTieuCrawler.TabHangHoa.Sugar, ChiTieuCrawler.TabHangHoa.Coffee, ChiTieuCrawler.TabHangHoa.Cotton, ChiTieuCrawler.TabHangHoa.RoughRice, ChiTieuCrawler.TabHangHoa.Wheat, ChiTieuCrawler.TabHangHoa.Soybean, ChiTieuCrawler.TabHangHoa.Ethanol };
                //while (ServiceStarted)
                //{
                try
                {
                    var dt = SqlDb.GetCrawlerData();
                    var manual = SqlDb.GetManualProductData();
                    var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);

                    #region Tab Viet Nam
                    //tab vietnam
                    var key = string.Format(RedisKey.KeyProductBox, 1);
                    var ls = redis.ContainsKey(key) ? redis.Get<List<ProductBox>>(key) : new List<ProductBox>();
                    var data = new List<ProductBox>();
                    ProductBox box, newbox, otherbox;
                    foreach (var item in tabVN)
                    {
                        if (int.Parse(item) < 0)
                        {
                            newbox = otherbox = null;
                            box = FindBox(item, ls);
                            otherbox = null;
                            if (manual.Rows.Count > 0)
                            {
                                switch (item)
                                {
                                    case ChiTieuCrawler.TabVietnam.EURtudo:
                                        otherbox = new ProductBox() { ProductName = "EUR (tự do)", CurrentPrice = double.Parse(manual.Rows[0]["Price_EURO"].ToString()), OtherPrice = double.Parse(manual.Rows[0]["Price_Euro_Sale"].ToString()), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = "-2" };
                                        break;
                                    case ChiTieuCrawler.TabVietnam.USDtudo:
                                        otherbox = new ProductBox() { ProductName = "USD (tự do)", CurrentPrice = double.Parse(manual.Rows[0]["Price_USD"].ToString()), OtherPrice = double.Parse(manual.Rows[0]["Price_USD_Sale"].ToString()), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = "-1" };
                                        break;
                                }
                            }
                            if (otherbox != null)
                            {
                                otherbox.UpdatePrevPrice(box);
                                data.Add(otherbox);
                            }
                            continue;
                        }
                        var drs = dt.Select("ID=" + item);
                        if (drs.Length == 0) continue;
                        var dr = drs[0];
                        try
                        {
                            newbox = otherbox = null;
                            box = FindBox(dr["ID"].ToString(), ls);
                            switch (dr["ID"].ToString())
                            {
                                case ChiTieuCrawler.TabVietnam.VangTheGioi:
                                    newbox = new ProductBox() { ProductName = "Vàng TG (USD)", CurrentPrice = double.Parse(dr["Gia"].ToString()), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString()) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabVietnam.VangSJC:
                                    newbox = new ProductBox() { ProductName = "Vàng SJC", CurrentPrice = double.Parse(dr["MuaVao"].ToString().Replace(",", "")), OtherPrice = double.Parse(dr["BanRa"].ToString().Replace(",", "")), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabVietnam.USDVCB:
                                    newbox = new ProductBox() { ProductName = "USD (VCB)", CurrentPrice = double.Parse(dr["MuaVao"].ToString().Replace(",", "")), OtherPrice = double.Parse(dr["BanRa"].ToString().Replace(",", "")), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabVietnam.EURVCB:
                                    newbox = new ProductBox() { ProductName = "EUR (VCB)", CurrentPrice = double.Parse(dr["MuaVao"].ToString().Replace(",", "")), OtherPrice = double.Parse(dr["BanRa"].ToString().Replace(",", "")), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabVietnam.CNY:
                                    newbox = new ProductBox() { ProductName = "CNY", CurrentPrice = double.Parse(dr["MuaVao"].ToString().Replace(",", "")), OtherPrice = double.Parse(dr["BanRa"].ToString().Replace(",", "")), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabVietnam.USDSIN:
                                    newbox = new ProductBox() { ProductName = "SGD", CurrentPrice = double.Parse(dr["MuaVao"].ToString().Replace(",", "")), OtherPrice = double.Parse(dr["BanRa"].ToString().Replace(",", "")), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabVietnam.USDHKD:
                                    newbox = new ProductBox() { ProductName = "HKD", CurrentPrice = double.Parse(dr["MuaVao"].ToString().Replace(",", "")), OtherPrice = double.Parse(dr["BanRa"].ToString().Replace(",", "")), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabVietnam.BangAnh:
                                    newbox = new ProductBox() { ProductName = "Bảng Anh", CurrentPrice = double.Parse(dr["MuaVao"].ToString().Replace(",", "")), OtherPrice = double.Parse(dr["BanRa"].ToString().Replace(",", "")), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                default:
                                    newbox = null;
                                    break;
                            }
                            if (newbox != null)
                            {
                                newbox.UpdatePrevPrice(box);
                                data.Add(newbox);
                            }

                        }
                        catch (Exception ex)
                        { //log.WriteEntry("GetBox : " + dr["ID"] + ":" + ex.ToString(), EventLogEntryType.Error); 
                        }
                    }
                    if (data.Count > 0)
                    {
                        if (redis.ContainsKey(key))
                            redis.Set(key, data);
                        else
                            redis.Add(key, data);
                    }
                    #endregion

                    #region Tab The gioi
                    key = string.Format(RedisKey.KeyProductBox, 2);
                    data = new List<ProductBox>();
                    foreach (var item in tabTG)
                    {
                        var drs = dt.Select("ID=" + item);
                        if (drs.Length == 0) continue;
                        var dr = drs[0];
                        try
                        {
                            box = FindBox(dr["ID"].ToString(), ls);
                            switch (dr["ID"].ToString())
                            {
                                case ChiTieuCrawler.TabTheGioi.DowJones:
                                    newbox = new ProductBox() { ProductName = "DowJones", CurrentPrice = double.Parse(dr["Index"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabTheGioi.Nasdaq:
                                    newbox = new ProductBox() { ProductName = "Nasdaq", CurrentPrice = double.Parse(dr["Index"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabTheGioi.SP500:
                                    newbox = new ProductBox() { ProductName = "S&P 500", CurrentPrice = double.Parse(dr["Index"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabTheGioi.FTSE100:
                                    newbox = new ProductBox() { ProductName = "FTSE 100", CurrentPrice = double.Parse(dr["Index"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabTheGioi.DAX:
                                    newbox = new ProductBox() { ProductName = "DAX", CurrentPrice = double.Parse(dr["Index"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabTheGioi.Nikkei225:
                                    newbox = new ProductBox() { ProductName = "Nikkei 225", CurrentPrice = double.Parse(dr["Index"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabTheGioi.HangSeng:
                                    newbox = new ProductBox() { ProductName = "Hang Seng", CurrentPrice = double.Parse(dr["Index"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabTheGioi.StraitTimes:
                                    newbox = new ProductBox() { ProductName = "Strait Times", CurrentPrice = double.Parse(dr["Index"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                default:
                                    newbox = null;
                                    break;
                            }
                            if (newbox != null)
                            {
                                data.Add(newbox);
                            }
                        }
                        catch (Exception ex)
                        { //log.WriteEntry("GetBox : " + dr["ID"] + ":" + ex.ToString(), EventLogEntryType.Error); 
                        }
                    }
                    if (data.Count > 0)
                    {
                        if (redis.ContainsKey(key))
                            redis.Set(key, data);
                        else
                            redis.Add(key, data);
                    }
                    #endregion

                    #region Tab Hàng hóa
                    key = string.Format(RedisKey.KeyProductBox, 3);
                    data = new List<ProductBox>();
                    foreach (var item in tabHH)
                    {
                        if (item == "-1")
                        {
                            switch (item)
                            {

                            }
                            continue;
                        }
                        var drs = dt.Select("ID=" + item);
                        if (drs.Length == 0) continue;
                        var dr = drs[0];
                        try
                        {
                            box = FindBox(dr["ID"].ToString(), ls);
                            switch (dr["ID"].ToString())
                            {
                                case ChiTieuCrawler.TabHangHoa.CrudeOil:
                                    newbox = new ProductBox() { ProductName = "Crude Oil", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabHangHoa.NaturalGas:
                                    newbox = new ProductBox() { ProductName = "Natural Gas", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabHangHoa.Gold:
                                    newbox = new ProductBox() { ProductName = "Gold", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabHangHoa.Copper:
                                    newbox = new ProductBox() { ProductName = "Copper", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabHangHoa.Silver:
                                    newbox = new ProductBox() { ProductName = "Silver", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabHangHoa.Corn:
                                    newbox = new ProductBox() { ProductName = "Corn", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabHangHoa.Sugar:
                                    newbox = new ProductBox() { ProductName = "Sugar", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabHangHoa.Coffee:
                                    newbox = new ProductBox() { ProductName = "Coffee", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabHangHoa.Cotton:
                                    newbox = new ProductBox() { ProductName = "Cotton", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabHangHoa.RoughRice:
                                    newbox = new ProductBox() { ProductName = "Rough rice", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabHangHoa.Wheat:
                                    newbox = new ProductBox() { ProductName = "Wheat", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabHangHoa.Soybean:
                                    newbox = new ProductBox() { ProductName = "Soybean", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                case ChiTieuCrawler.TabHangHoa.Ethanol:
                                    newbox = new ProductBox() { ProductName = "Ethanol", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                    break;
                                default:
                                    newbox = null;
                                    break;
                            }
                            if (newbox != null)
                            {
                                data.Add(newbox);
                            }
                        }
                        catch (Exception ex)
                        { //log.WriteEntry("GetBox : " + dr["ID"] + ":" + ex.ToString(), EventLogEntryType.Error); 
                        }
                    }
                    if (data.Count > 0)
                    {
                        if (redis.ContainsKey(key))
                            redis.Set(key, data);
                        else
                            redis.Add(key, data);
                    }
                    #endregion

                    //Thread.Sleep(crawlerInterval);
                }
                catch (Exception ex)
                {
                    //log.WriteEntry(ex.ToString(), EventLogEntryType.Error);
                }
                //}
            }
            catch (Exception ex)
            {
                //log.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }
        }
        internal class ChiTieuCrawler
        {
            public class TabVietnam
            {
                public const string VangTheGioi = "30";
                public const string VangSJC = "54";
                public const string USDVCB = "48";
                public const string EURVCB = "28";
                public const string CNY = "50";
                public const string USDSIN = "31";
                public const string USDHKD = "26";
                public const string BangAnh = "27";
                public const string USDtudo = "-1";
                public const string EURtudo = "-2";
            }
            public class TabTheGioi
            {
                public const string DowJones = "43";
                public const string Nasdaq = "41";
                public const string SP500 = "38";
                public const string FTSE100 = "39";
                public const string DAX = "33";
                public const string Nikkei225 = "34";
                public const string HangSeng = "35";
                public const string StraitTimes = "36";
            }
            public class TabHangHoa
            {
                public const string CrudeOil = "32";
                public const string NaturalGas = "52";
                public const string Gold = "30";
                public const string Copper = "37";
                public const string Silver = "40";
                public const string Corn = "42";
                public const string Sugar = "44";
                public const string Coffee = "45";
                public const string Cotton = "46";
                public const string RoughRice = "47";
                public const string Wheat = "49";
                public const string Soybean = "51";
                public const string Ethanol = "53";
            }
        }
        private static ProductBox FindBox(string id, List<ProductBox> ls)
        {
            foreach (var box in ls)
            {
                if (box.DbId == id) return box;
            }
            return new ProductBox() { DbId = id };
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var sql = new SqlDb();
            #region FN
            try
            {
                var keylist = string.Format(RedisKey.KeyCompanyNewsByCate, 0); //Tất cả
                var ls = new List<string>();

                var pdt = sql.GetCompanyNews("A", 1000);

                var key1 = string.Format(RedisKey.KeyCompanyNewsByCate, 1); //Tình hình SXKD & Phân tích khác
                var key2 = string.Format(RedisKey.KeyCompanyNewsByCate, 2); // Cổ tức - Chốt quyền
                var key3 = string.Format(RedisKey.KeyCompanyNewsByCate, 3); // Thay đổi nhân sự
                var key4 = string.Format(RedisKey.KeyCompanyNewsByCate, 4); // Tăng vốn - Cổ phiếu quỹ
                var key5 = string.Format(RedisKey.KeyCompanyNewsByCate, 5); // GD cđ lớn & cđ nội bộ
                var cate1 = (redis.ContainsKey(key1)) ? redis.Get<List<string>>(key1) : new List<string>();

                var cate2 = (redis.ContainsKey(key2)) ? redis.Get<List<string>>(key2) : new List<string>();
                var cate3 = (redis.ContainsKey(key3)) ? redis.Get<List<string>>(key3) : new List<string>();

                var cate4 = (redis.ContainsKey(key4)) ? redis.Get<List<string>>(key4) : new List<string>();
                var cate5 = (redis.ContainsKey(key5)) ? redis.Get<List<string>>(key5) : new List<string>();

                foreach (DataRow rdr in pdt.Rows)
                {
                    var compact = new StockNews() { ID = int.Parse(rdr["ID"].ToString()), Body = "", DateDeploy = (DateTime)rdr["PostTime"], Image = "", Title = rdr["title"].ToString(), Sapo = "", TypeID = rdr["ConfigId"].ToString(), Symbol = rdr["StockSymbols"].ToString() };
                    var obj = new StockNews() { ID = int.Parse(rdr["ID"].ToString()), Body = rdr["Content"].ToString(), DateDeploy = (DateTime)rdr["PostTime"], Image = rdr["ImagePath"].ToString(), Title = rdr["title"].ToString(), Sapo = rdr["SubContent"].ToString(), TypeID = rdr["ConfigId"].ToString(), Symbol = rdr["StockSymbols"].ToString() };
                    var key = obj.DateDeploy.ToString("yyyyMMddHHmm") + obj.ID;
                    var compactkey = string.Format(RedisKey.KeyCompanyNewsCompact, obj.ID);
                    if (redis.ContainsKey(compactkey))
                        redis.Set(compactkey, compact);
                    else
                        redis.Add(compactkey, compact);
                    var detailkey = string.Format(RedisKey.KeyCompanyNewsDetail, obj.ID);
                    if (redis.ContainsKey(detailkey))
                        redis.Set(detailkey, obj);
                    else
                        redis.Add(detailkey, obj);

                    if (!ls.Contains(key)) ls.Add(key);
                    #region Update category list
                    if (obj.TypeID.Contains("1"))
                    {
                        if (!cate1.Contains(key)) cate1.Add(key);
                    }
                    else
                    {
                        if (cate1.Contains(key)) cate1.Remove(key);
                    }
                    if (obj.TypeID.Contains("2"))
                    {
                        if (!cate2.Contains(key)) cate2.Add(key);
                    }
                    else
                    {
                        if (cate2.Contains(key)) cate2.Remove(key);
                    }
                    if (obj.TypeID.Contains("3"))
                    {
                        if (!cate3.Contains(key)) cate3.Add(key);
                    }
                    else
                    {
                        if (cate3.Contains(key)) cate3.Remove(key);
                    }
                    if (obj.TypeID.Contains("4"))
                    {
                        if (!cate4.Contains(key)) cate4.Add(key);
                    }
                    else
                    {
                        if (cate4.Contains(key)) cate4.Remove(key);
                    }
                    if (obj.TypeID.Contains("5"))
                    {
                        if (!cate5.Contains(key)) cate5.Add(key);
                    }
                    else
                    {
                        if (cate5.Contains(key)) cate5.Remove(key);
                    }
                    #endregion
                }
                ls.Sort();
                ls.Reverse();
                if (redis.ContainsKey(keylist))
                    redis.Set(keylist, ls);
                else
                    redis.Add(keylist, ls);
                #region Update category list
                cate1.Sort();
                cate1.Reverse();
                if (redis.ContainsKey(key1))
                    redis.Set(key1, cate1);
                else
                    redis.Add(key1, cate1);
                cate2.Sort();
                cate2.Reverse();
                if (redis.ContainsKey(key2))
                    redis.Set(key2, cate2);
                else
                    redis.Add(key2, cate2);
                cate3.Sort();
                cate3.Reverse();
                if (redis.ContainsKey(key3))
                    redis.Set(key3, cate3);
                else
                    redis.Add(key3, cate3);
                cate4.Sort();
                cate4.Reverse();
                if (redis.ContainsKey(key4))
                    redis.Set(key4, cate4);
                else
                    redis.Add(key4, cate4);
                cate5.Sort();
                cate5.Reverse();
                if (redis.ContainsKey(key5))
                    redis.Set(key5, cate5);
                else
                    redis.Add(key5, cate5);
                #endregion
            }
            catch (Exception ex)
            {
                //log.WriteEntry(symbol + " : FN : " + ex.ToString(), EventLogEntryType.Error);
                //ret = false;
            }
            #endregion

            var topkey = RedisKey.KeyTop20News;
            var ndt = sql.GetCompanyNews("A", 100);
            var topls = new List<StockNews>();
            foreach (DataRow ndr in ndt.Rows)
            {
                var da = (DateTime)ndr["PostTime"];
                //if (double.Parse(da.ToString("yyyyMMddHHmm")) > double.Parse(DateTime.Now.ToString("yyyyMMddHHmm"))) continue;
                topls.Add(new StockNews() { ID = int.Parse(ndr["ID"].ToString()), Body = "", DateDeploy = (DateTime)ndr["PostTime"], Image = "", Title = ndr["title"].ToString(), Sapo = "", TypeID = ndr["ConfigId"].ToString(), Symbol = ndr["StockSymbols"].ToString() });
                // if (topls.Count >= 20) break;
            }
            if (redis.ContainsKey(topkey))
                redis.Set(topkey, topls);
            else
                redis.Add(topkey, topls);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var keylist = RedisKey.KeyAnalysisReport;
            var ls = (redis.ContainsKey(keylist)) ? redis.Get<List<string>>(keylist) : new List<string>();

            ls.Sort();
            ls.Reverse();
            if (redis.ContainsKey(keylist))
                redis.Set(keylist, ls);
            else
                redis.Add(keylist, ls);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateStock("VNM");
        }

        private bool UpdateStock(string symbol)
        {
            var ret = true;
            var related = new List<string>();
            var sql = new SqlDb();
            //////log.WriteEntry(symbol + "-1-" + ret, EventLogEntryType.Information);
            try
            {
                var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
                var key = string.Format(RedisKey.Key, symbol.ToUpper());
                var bExisted = false;
                var stock = new Stock() { Symbol = symbol };
                #region Update stock from sql
                //stock.Symbol = "AAA";
                //load stock data 

                //profile
                var profile = bExisted ? stock.CompanyProfile : new CompanyProfile { Symbol = stock.Symbol };
                var basicInfo = bExisted ? profile.basicInfos : new BasicInfo() { Symbol = stock.Symbol };
                var basicCommon = bExisted ? profile.basicInfos.basicCommon : new BasicCommon() { Symbol = stock.Symbol };
                var category = bExisted ? profile.basicInfos.category : new CategoryObject();
                var firstInfo = bExisted ? profile.basicInfos.firstInfo : new FirstInfo() { Symbol = stock.Symbol };
                var commonInfo = bExisted ? profile.commonInfos : new CommonInfo() { Symbol = stock.Symbol };
                //basic information
                if (!bExisted || related.Contains("SB"))
                {
                    try
                    {
                        var dt = sql.GetSymbolData(symbol);
                        if (dt.Rows.Count <= 0) return true;

                        var row = dt.Rows[0];

                        #region StockCompactInfo

                        var compactkey = string.Format(RedisKey.KeyCompactStock, symbol.ToUpper());
                        var compact = new StockCompactInfo() { Symbol = symbol.ToUpper(), TradeCenterId = int.Parse(row["TradeCenterId"].ToString()), CompanyName = row["CompanyName"].ToString(), EPS = double.Parse(row["EPS"].ToString()), FolderChart = row["FolderChart"].ToString(), ShowTradeCenter = row["ShowTradeCenter"].ToString().ToUpper() == "TRUE", IsBank = row["IsBank"].ToString().ToUpper() == "TRUE", IsCCQ = row["IsCCQ"].ToString().ToUpper() == "TRUE" };
                        if (redis.ContainsKey(compactkey))
                            redis.Set(compactkey, compact);
                        else
                            redis.Add(compactkey, compact);
                        #endregion

                        stock.Symbol = row["Symbol"].ToString();
                        stock.TradeCenterId = int.Parse(row["TradeCenterId"].ToString());
                        stock.IsDisabled = row["IsDisabled"].ToString() == "TRUE";
                        stock.StatusText = row["StatusText"].ToString();
                        stock.ShowTradeCenter = row["ShowTradeCenter"].ToString().ToUpper() == "TRUE";
                        stock.FolderImage = row["FolderChart"].ToString();
                        stock.IsBank = row["IsBank"].ToString() == "TRUE";
                        stock.IsCCQ = row["IsCCQ"].ToString().ToUpper() == "TRUE";

                        //profile - basicInfo
                        basicInfo.Name = row["CompanyName"].ToString();
                        basicInfo.TradeCenter = stock.TradeCenterId.ToString();

                        //profile - basicInfo - basicCommon
                        /*PE = double.Parse(row["PE"].ToString()),*/
                        basicCommon.AverageVolume = double.Parse(row["AVG10SS"].ToString());
                        basicCommon.Beta = double.Parse(row["Beta"].ToString());
                        basicCommon.EPS = double.Parse(row["EPS"].ToString());
                        basicCommon.TotalValue = double.Parse(row["MarketCap"].ToString());
                        basicCommon.ValuePerStock = double.Parse(row["BookValue"].ToString());
                        basicCommon.VolumeTotal = double.Parse(row["SLCPNY"].ToString());
                        basicCommon.OutstandingVolume = double.Parse(row["TotalShare"].ToString());
                        basicCommon.PE = basicCommon.EPS != 0 ? (double.Parse(row["LastPrice"].ToString()) / basicCommon.EPS) : 0;
                        basicCommon.EPSDate = row["EPSDate"].ToString();
                        basicCommon.CCQv3 = double.Parse(row["CCQv3"].ToString());
                        basicCommon.CCQv6 = double.Parse(row["CCQv6"].ToString());
                        basicCommon.CCQdate = DateTime.ParseExact(row["CCQdate"].ToString(), "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None);

                        basicInfo.basicCommon = basicCommon;

                        //profile - basicInfo - category
                        category.ID = int.Parse(row["CategoryId"].ToString());
                        category.Name = row["CategoryName"].ToString();
                        basicInfo.category = category;

                        //profile - basicInfo - firstInfo
                        firstInfo.FirstPrice = double.Parse(row["FirstPrice"].ToString());
                        firstInfo.FirstTrade = row["FirstTrade"].Equals(DBNull.Value) ? null : ((DateTime?)row["FirstTrade"]);
                        firstInfo.FirstVolume = double.Parse(row["FirstVolume"].ToString());
                        basicInfo.firstInfo = firstInfo;

                        profile.basicInfos = basicInfo;

                        //profile - commonInfo
                        commonInfo.Capital = double.Parse(row["VonDieuLe"].ToString());
                        commonInfo.Category = row["CategoryName"].ToString();
                        commonInfo.Content = row["About"].ToString();
                        commonInfo.OutstandingVolume = double.Parse(row["TotalShare"].ToString());
                        commonInfo.TotalVolume = double.Parse(row["SLCPNY"].ToString());
                        commonInfo.Content += "<p><b>Địa chỉ:</b> " + row["Address"].ToString() + "</p>";
                        commonInfo.Content += "<p><b>Điện thoại:</b> " + row["Phone"].ToString() + "</p>";
                        commonInfo.Content += "<p><b>Người phát ngôn:</b> " + row["Spokenman"].ToString() + "</p>";
                        if (!string.IsNullOrEmpty(row["Email"].ToString())) commonInfo.Content += "<p><b>Email:</b> <a href='mailto:" + row["Email"] + "'>" + row["Email"] + "</a></p>";
                        if (!string.IsNullOrEmpty(row["Website"].ToString())) commonInfo.Content += "<p><b>Website:</b> <a href='" + row["Website"] + "' target='_blank'>" + row["Website"] + "</a></p>";
                        commonInfo.AuditFirmName = row["AuditName"].ToString();
                        commonInfo.AuditFirmSite = row["AuditSite"].ToString().Trim();
                        commonInfo.ConsultantName = row["ConsultantName"].ToString();
                        commonInfo.ConsultantSite = row["ConsultantSite"].ToString();
                        commonInfo.BusinessLicense = row["BusinessLicense"].ToString();

                        profile.commonInfos = commonInfo;

                        //business plans
                        var plans = new List<BusinessPlan>();
                        if (row["HasPlan"].ToString() == "1")
                        {
                            plans.Add(new BusinessPlan() { Body = row["PlanNote"].ToString(), Date = (DateTime)row["PlanDate"], DividendsMoney = double.Parse(row["Dividend"].ToString()), DividendsStock = double.Parse(row["DivStock"].ToString()), ID = int.Parse(row["PlanId"].ToString()), IncreaseExpected = double.Parse(row["CapitalRaising"].ToString()), ProfitATax = double.Parse(row["NetIncome"].ToString()), ProfitBTax = double.Parse(row["TotalProfit"].ToString()), Revenue = double.Parse(row["TotalIncome"].ToString()), Symbol = stock.Symbol, Year = int.Parse(row["KYear"].ToString()) });
                        }
                        stock.BusinessPlans1 = plans;
                    }
                    catch (Exception ex)
                    {
                        ////log.WriteEntry(symbol + " : BasicInfo : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }

                //profile - subsidiaries
                if (!bExisted || related.Contains("SS"))
                {
                    try
                    {


                        var subsidiaries = new List<OtherCompany>();
                        var associates = new List<OtherCompany>();
                        var cdt = sql.GetChildrenCompany(stock.Symbol);
                        var i = 0;
                        foreach (DataRow cdr in cdt.Rows)
                        {
                            i++;
                            var child = new OtherCompany() { Name = cdr["CompanyName"].ToString(), Note = cdr["NoteInfo"].ToString(), OwnershipRate = double.Parse(cdr["Rate"].ToString()), Order = i, SharedCapital = double.Parse(cdr["TotalShareValue"].ToString()), Symbol = stock.Symbol, TotalCapital = double.Parse(cdr["CharterCapital"].ToString()) };
                            if (cdr["isCongTyCon"].ToString() == "1") subsidiaries.Add(child);
                            else associates.Add(child);
                        }
                        profile.Subsidiaries = subsidiaries;
                        profile.AssociatedCompanies = associates;
                    }
                    catch (Exception ex)
                    {
                        ////log.WriteEntry(symbol + " : Cty con : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }

                //profile - financePeriod
                if (!bExisted || related.Contains("SF"))
                {
                    try
                    {
                        var periods = new List<FinancePeriod>();
                        var financeInfo = new List<FinanceInfo>();

                        var fyt = sql.GetFinancePeriod(stock.Symbol);
                        FinancePeriod period = null;
                        var tmp = 0;
                        foreach (DataRow fyr in fyt.Rows)
                        {
                            if (period == null || tmp != int.Parse(fyr["Year"].ToString()) * 10 + int.Parse(fyr["QuarterType"].ToString()))
                            {
                                if (period != null) { period.UpdateTitle(); periods.Add(period); }
                                period = new FinancePeriod() { Quarter = int.Parse(fyr["QuarterType"].ToString()), Year = int.Parse(fyr["Year"].ToString()) };
                            }
                            tmp = int.Parse(fyr["Year"].ToString()) * 10 + int.Parse(fyr["QuarterType"].ToString());
                            switch (fyr["MaChiTieu"].ToString())
                            {
                                case "Audited":
                                    period.SubTitle = fyr["TieuDeNhom"].ToString();
                                    break;
                                case "QuarterModify":
                                    var qrt = fyr["TieuDeNhom"].ToString();
                                    var qrti = 0;
                                    if (qrt.EndsWith("T")) { period.QuarterTitle = qrt.Remove(qrt.Length - 1) + " tháng"; }
                                    else if (int.TryParse(qrt, out qrti) && qrti >= 1 && qrti < 5)
                                    {
                                        period.QuarterTitle = "Quý " + qrti;
                                    }
                                    else period.QuarterTitle = "";
                                    break;
                                case "YearModify":
                                    if (int.TryParse(fyr["TieuDeNhom"].ToString(), out qrti))
                                    {
                                        period.YearTitle = "Năm " + qrti;
                                    }
                                    else period.YearTitle = "";
                                    break;
                                case "FromDate":
                                    qrt = fyr["TieuDeNhom"].ToString();
                                    if (qrt.Contains("/"))
                                        period.BeginTitle = qrt.Substring(0, qrt.LastIndexOf("/"));
                                    break;
                                case "ToDate":
                                    qrt = fyr["TieuDeNhom"].ToString();
                                    if (qrt.Contains("/"))
                                        period.EndTitle = qrt.Substring(0, qrt.LastIndexOf("/"));
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (period != null) { period.UpdateTitle(); periods.Add(period); }
                        profile.FinancePeriods = periods;

                        //profile - financeInfo

                        var fit = sql.GetChiTieuFinance(stock.Symbol);
                        var fvt = sql.GetFinanceData(stock.Symbol);
                        var groupId = 0;
                        FinanceInfo info = null;
                        foreach (DataRow fir in fit.Rows)
                        {
                            if (info == null || groupId != int.Parse(fir["LoaiChiTieu"].ToString()))
                            {
                                if (info != null) financeInfo.Add(info);
                                info = new FinanceInfo() { NhomChiTieuId = groupId, Symbol = stock.Symbol, TenNhomChiTieu = fir["TenLoaiChiTieu"].ToString() };
                            }
                            groupId = int.Parse(fir["LoaiChiTieu"].ToString());
                            var chiTieu = new FinanceChiTieu() { ChiTieuId = fir["MaChiTieu"].ToString(), TenChiTieu = fir["TieuDeKhac"].ToString() };
                            if (fir["MaChiTieu"].ToString() == "ROA")
                            {
                                int b = 0;
                            }
                            foreach (var financePeriod in periods)
                            {
                                var fvrs = fvt.Select("MaChiTieu = '" + fir["MaChiTieu"] + "' AND Year = " + financePeriod.Year + " AND QuarterType = " + financePeriod.Quarter);
                                if (fvrs.Length > 0)
                                {
                                    chiTieu.Values.Add(new FinanceValue() { Quarter = financePeriod.Quarter, Year = financePeriod.Year, Value = double.Parse(fvrs[0]["FinanceValue"].ToString()) });
                                }
                                else
                                {
                                    chiTieu.Values.Add(new FinanceValue() { Quarter = financePeriod.Quarter, Year = financePeriod.Year, Value = 0 });
                                }
                            }

                            info.ChiTieus.Add(chiTieu);
                        }
                        if (info != null) financeInfo.Add(info);
                        profile.financeInfos = financeInfo;
                    }
                    catch (Exception ex)
                    {
                        ////log.WriteEntry(symbol + " : FinanceInfo : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }

                //profile - leader
                if (!bExisted || related.Contains("SC"))
                {
                    try
                    {
                        var leaders = new List<Leader>();
                        var ldt = sql.GetCeos(stock.Symbol);
                        foreach (DataRow ldr in ldt.Rows)
                        {
                            leaders.Add(new Leader() { GroupID = ldr["ParentId"].ToString(), Name = ldr["FullName"].ToString(), Positions = ldr["TenNhom"].ToString() });
                        }
                        profile.Leaders = leaders;
                    }
                    catch (Exception ex)
                    {
                        ////log.WriteEntry(symbol + " : Leaders : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }

                //profile - owner
                if (!bExisted || related.Contains("SH"))
                {
                    try
                    {
                        var owners = new List<MajorOwner>();
                        var odt = sql.GetShareHolders(stock.Symbol);
                        foreach (DataRow odr in odt.Rows)
                        {
                            owners.Add(new MajorOwner() { Name = odr["FullName"].ToString(), Rate = double.Parse(odr["ShareHoldPct"].ToString()), ToDate = (DateTime)odr["DenNgay"], Volume = double.Parse(odr["SoCoPhieu"].ToString()) });
                        }
                        profile.MajorOwners = owners;
                    }
                    catch (Exception ex)
                    {
                        ////log.WriteEntry(symbol + " : Owners : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }

                //profile - CEO
                if (!bExisted || related.Contains("SCN"))
                {
                    try
                    {
                        var ceos = new List<StockCeo>();
                        var cdt = sql.GetCeosNew(stock.Symbol);
                        foreach (DataRow cdr in cdt.Rows)
                        {
                            var ceo = new StockCeo() { CeoId = int.Parse(cdr["CeoId"].ToString()), CeoCode = cdr["CeoCode"].ToString(), GroupID = int.Parse(cdr["PositionType"].ToString()), Name = cdr["CeoName"].ToString(), Positions = cdr["PositionName"].ToString(), Process = cdr["CeoProfileShort"].ToString(), Age = 0 };
                            var birthday = cdr["CeoBirthday"].ToString();
                            if (birthday.Contains("/"))
                            {
                                int year;
                                if (!int.TryParse(birthday.Substring(birthday.LastIndexOf("/") + 1), out year)) year = 0;
                                if (year > 0)
                                {
                                    if (year < 100) year = 1900 + year;
                                    ceo.Age = year;
                                }
                            }
                            ceos.Add(ceo);
                        }
                        profile.AssociatedCeo = ceos;
                    }
                    catch (Exception ex)
                    {
                        ////log.WriteEntry(symbol + " : CEO New : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }

                stock.CompanyProfile = profile;
                /*================================*/

                //dividend histories
                if (!bExisted || related.Contains("SD"))
                {
                    try
                    {
                        var divs = new List<DividendHistory>();
                        var ddt = sql.GetDividendHistory(stock.Symbol);
                        foreach (DataRow ddr in ddt.Rows)
                        {
                            divs.Add(new DividendHistory() { DonViDoiTuong = ddr["DonViDoiTuong"].ToString(), NgayGDKHQ = (DateTime)ddr["NgayGDKHQ"], GhiChu = ddr["GhiChu"].ToString(), SuKien = ddr["SuKien"].ToString(), Symbol = stock.Symbol, TiLe = ddr["TiLe"].ToString() });
                        }
                        stock.DividendHistorys = divs;
                    }
                    catch (Exception ex)
                    {
                        //log.WriteEntry(symbol + " : Dividend : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }

                //báo cáo phân tích
                if (!bExisted || related.Contains("SA"))
                {
                    try
                    {
                        var reports = new List<Reports>();
                        var rdt = sql.GetAnalysisReports(stock.Symbol);
                        foreach (DataRow rdr in rdt.Rows)
                        {
                            reports.Add(new Reports() { ID = int.Parse(rdr["ID"].ToString()), Title = rdr["title"].ToString(), DateDeploy = (DateTime)rdr["PublishDate"], ResourceCode = rdr["Source"].ToString(), IsHot = rdr["IsHot"].ToString().ToLower() == "true" });
                        }
                        stock.Reports3 = reports;
                    }
                    catch (Exception ex)
                    {
                        //log.WriteEntry(symbol + " : AnalyseReport : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }

                //công ty cùng ngành
                if (!bExisted || related.Contains("SCA"))
                {
                    try
                    {
                        var samecateCompanies = new List<StockShortInfo>();
                        var scdt = sql.GetSameCateCompanies(stock.Symbol);
                        foreach (DataRow scdr in scdt.Rows)
                        {
                            samecateCompanies.Add(new StockShortInfo() { Symbol = scdr["StockSymbol"].ToString(), TradeCenterId = int.Parse(scdr["TradeCenterId"].ToString()), Name = scdr["FullName"].ToString(), EPS = double.Parse(scdr["EPS"].ToString()) });
                        }
                        stock.SameCategory = samecateCompanies;
                    }
                    catch (Exception ex)
                    {
                        //log.WriteEntry(symbol + " : SameCate : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }

                //eps tương đương
                if (!bExisted || related.Contains("SEPS"))
                {
                    try
                    {
                        var sameEPSCompanies = new List<StockShortInfo>();
                        var sedt = sql.GetSameEPSCompanies(stock.Symbol);
                        foreach (DataRow sedr in sedt.Rows)
                        {
                            sameEPSCompanies.Add(new StockShortInfo() { Symbol = sedr["StockSymbol"].ToString(), TradeCenterId = int.Parse(sedr["TradeCenterId"].ToString()), Name = sedr["FullName"].ToString(), EPS = double.Parse(sedr["EPS"].ToString()), MarketValue = double.Parse(sedr["MarketCap"].ToString()) });
                        }
                        stock.SameEPS = sameEPSCompanies;
                    }
                    catch (Exception ex)
                    {
                        //log.WriteEntry(symbol + " : SameEPS : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }

                //pe tương đương
                if (!bExisted || related.Contains("SPE"))
                {
                    try
                    {
                        var samePECompanies = new List<StockShortInfo>();
                        var spdt = sql.GetSamePECompanies(stock.Symbol);
                        foreach (DataRow spdr in spdt.Rows)
                        {
                            samePECompanies.Add(new StockShortInfo() { Symbol = spdr["StockSymbol"].ToString(), TradeCenterId = int.Parse(spdr["TradeCenterId"].ToString()), Name = spdr["FullName"].ToString(), EPS = double.Parse(spdr["EPS"].ToString()), MarketValue = double.Parse(spdr["MarketCap"].ToString()) });
                        }
                        stock.SamePE = samePECompanies;
                    }
                    catch (Exception ex)
                    {
                        ////log.WriteEntry(symbol + " : SamePE : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }

                //stock history
                var history = bExisted ? stock.StockPriceHistory : new StockCompactHistory();

                //stock history - price
                if (!bExisted || related.Contains("SP"))
                {
                    try
                    {
                        var price = new List<PriceCompactHistory>();
                        var pdt = sql.GetPriceHistory(stock.Symbol, 10);
                        foreach (DataRow pdr in pdt.Rows)
                        {
                            price.Add(new PriceCompactHistory() { ClosePrice = double.Parse(pdr["Price"].ToString()), BasicPrice = double.Parse(pdr["BasicPrice"].ToString()), Ceiling = double.Parse(pdr["Ceiling"].ToString()), Floor = double.Parse(pdr["Floor"].ToString()), Volume = double.Parse(pdr["Volume"].ToString()), TotalValue = double.Parse(pdr["TotalValue"].ToString()), TradeDate = (DateTime)pdr["TradeDate"] });
                        }
                        history.Price = price;
                    }
                    catch (Exception ex)
                    {
                        //log.WriteEntry(symbol + " : Price : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }

                //stock history - order
                if (!bExisted || related.Contains("SO"))
                {
                    try
                    {
                        var order = new List<OrderCompactHistory>();
                        var hodt = sql.GetOrderHistory(stock.Symbol, 10);
                        foreach (DataRow hodr in hodt.Rows)
                        {
                            order.Add(new OrderCompactHistory() { AskAverageVolume = double.Parse(hodr["AskAverage"].ToString()), AskLeft = double.Parse(hodr["AskLeft"].ToString()), BidAverageVolume = double.Parse(hodr["BidAverage"].ToString()), BidLeft = double.Parse(hodr["BidLeft"].ToString()), TradeDate = (DateTime)hodr["Trading_Date"] });
                        }
                        history.Orders = order;
                    }
                    catch (Exception ex)
                    {
                        //log.WriteEntry(symbol + " : Order : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }

                //stock history - foreign
                if (!bExisted || related.Contains("SFT"))
                {
                    try
                    {
                        var foreign = new List<ForeignCompactHistory>();
                        var fdt = sql.GetForeignHistory(stock.Symbol, 10);
                        foreach (DataRow fdr in fdt.Rows)
                        {
                            foreign.Add(new ForeignCompactHistory() { BuyPercent = double.Parse(fdr["FBuyPercent"].ToString()), SellPercent = double.Parse(fdr["FSellPercent"].ToString()), NetVolume = double.Parse(fdr["FNetVolume"].ToString()), NetValue = double.Parse(fdr["FNetValue"].ToString()), TradeDate = (DateTime)fdr["Trading_Date"] });
                        }
                        history.Foreign = foreign;
                    }
                    catch (Exception ex)
                    {
                        //log.WriteEntry(symbol + " : Foreign : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }

                stock.StockPriceHistory = history;
                /*====================*/

                //tin tức và sự kiện
                if (!bExisted || related.Contains("SN"))
                {
                    try
                    {
                        //var news = new List<StockNews>();
                        //var ndt = sql.GetCompanyNews(stock.Symbol, -1);
                        //foreach (DataRow ndr in ndt.Rows)
                        //{
                        //    news.Add(new StockNews() { DateDeploy = (DateTime)ndr["PostTime"], ID = int.Parse(ndr["Id"].ToString()), Title = ndr["Title"].ToString(), TypeID = ndr["ConfigId"].ToString() });
                        //}
                        //stock.StockNews = news;
                        var keylist = string.Format(RedisKey.KeyCompanyNewsByStock, stock.Symbol, 0); //Tất cả
                        var ls = new List<string>();
                        var pdt = sql.GetCompanyNews(stock.Symbol, -1);

                        var key1 = string.Format(RedisKey.KeyCompanyNewsByStock, stock.Symbol, 1); //Tình hình SXKD & Phân tích khác
                        var key2 = string.Format(RedisKey.KeyCompanyNewsByStock, stock.Symbol, 2); // Cổ tức - Chốt quyền
                        var key3 = string.Format(RedisKey.KeyCompanyNewsByStock, stock.Symbol, 3); // Thay đổi nhân sự
                        var key4 = string.Format(RedisKey.KeyCompanyNewsByStock, stock.Symbol, 4); // Tăng vốn - Cổ phiếu quỹ
                        var key5 = string.Format(RedisKey.KeyCompanyNewsByStock, stock.Symbol, 5); // GD cđ lớn & cđ nội bộ
                        var cate1 = new List<string>();
                        var cate2 = new List<string>();
                        var cate3 = new List<string>();

                        var cate4 = new List<string>();
                        var cate5 = new List<string>();
                        var stocknews = new List<StockNews>();
                        foreach (DataRow rdr in pdt.Rows)
                        {
                            var compact = new StockNews() { ID = int.Parse(rdr["ID"].ToString()), Body = "", DateDeploy = (DateTime)rdr["PostTime"], Image = "", Title = rdr["title"].ToString(), Sapo = "", TypeID = rdr["ConfigId"].ToString(), Symbol = rdr["StockSymbols"].ToString() };
                            var obj = new StockNews() { ID = int.Parse(rdr["ID"].ToString()), Body = rdr["Content"].ToString(), DateDeploy = (DateTime)rdr["PostTime"], Image = rdr["ImagePath"].ToString(), Title = rdr["title"].ToString(), Sapo = rdr["SubContent"].ToString(), TypeID = rdr["ConfigId"].ToString(), Symbol = rdr["StockSymbols"].ToString() };
                            for (int i = 0xD800; i < 0xDFFF; i++)
                            {
                                obj.Body.Replace((char)i, ' ');
                            }

                            if (stocknews.Count < 6) { stocknews.Add(compact); }
                            var newskey = obj.DateDeploy.ToString("yyyyMMddHHmm") + obj.ID;
                            var compactkey = string.Format(RedisKey.KeyCompanyNewsCompact, obj.ID);
                            var detailkey = string.Format(RedisKey.KeyCompanyNewsDetail, obj.ID);
                            if (redis.ContainsKey(compactkey))
                                redis.Set(compactkey, compact);
                            else
                                redis.Add(compactkey, compact);
                            if (redis.ContainsKey(detailkey))
                                redis.Set(detailkey, obj);
                            else
                                redis.Add(detailkey, obj);

                            if (!ls.Contains(newskey)) ls.Add(newskey);
                            #region Update category list
                            if (obj.TypeID.Contains("1"))
                            {
                                if (!cate1.Contains(newskey)) cate1.Add(newskey);
                            }
                            else
                            {
                                if (cate1.Contains(newskey)) cate1.Remove(newskey);
                            }
                            if (obj.TypeID.Contains("2"))
                            {
                                if (!cate2.Contains(newskey)) cate2.Add(newskey);
                            }
                            else
                            {
                                if (cate2.Contains(newskey)) cate2.Remove(newskey);
                            }
                            if (obj.TypeID.Contains("3"))
                            {
                                if (!cate3.Contains(newskey)) cate3.Add(newskey);
                            }
                            else
                            {
                                if (cate3.Contains(newskey)) cate3.Remove(newskey);
                            }
                            if (obj.TypeID.Contains("4"))
                            {
                                if (!cate4.Contains(newskey)) cate4.Add(newskey);
                            }
                            else
                            {
                                if (cate4.Contains(newskey)) cate4.Remove(newskey);
                            }
                            if (obj.TypeID.Contains("5"))
                            {
                                if (!cate5.Contains(newskey)) cate5.Add(newskey);
                            }
                            else
                            {
                                if (cate5.Contains(newskey)) cate5.Remove(newskey);
                            }
                            #endregion
                        }
                        stock.StockNews = stocknews;

                        ls.Sort();
                        ls.Reverse();
                        if (redis.ContainsKey(keylist))
                            redis.Set(keylist, ls);
                        else
                            redis.Add(keylist, ls);
                        #region Update category list
                        cate1.Sort();
                        cate1.Reverse();
                        if (redis.ContainsKey(key1))
                            redis.Set(key1, cate1);
                        else
                            redis.Add(key1, cate1);
                        cate2.Sort();
                        cate2.Reverse();
                        if (redis.ContainsKey(key2))
                            redis.Set(key2, cate2);
                        else
                            redis.Add(key2, cate2);
                        cate3.Sort();
                        cate3.Reverse();
                        if (redis.ContainsKey(key3))
                            redis.Set(key3, cate3);
                        else
                            redis.Add(key3, cate3);
                        cate4.Sort();
                        cate4.Reverse();
                        if (redis.ContainsKey(key4))
                            redis.Set(key4, cate4);
                        else
                            redis.Add(key4, cate4);
                        cate5.Sort();
                        cate5.Reverse();
                        if (redis.ContainsKey(key5))
                            redis.Set(key5, cate5);
                        else
                            redis.Add(key5, cate5);
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        //log.WriteEntry(symbol + " : News : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                if (redis.ContainsKey(key))
                    redis.Set<Stock>(key, stock);
                else
                    redis.Add<Stock>(key, stock);
            }
            catch (Exception ex)
            {
                ////log.WriteEntry(symbol + ": " + ex.ToString(), EventLogEntryType.Error);
                ret = false;
            }

            return ret;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {


        }

        private void button6_Click(object sender, EventArgs e)
        {
            var symbol = "SSI";
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var ret = new List<StockNews>();
            var key = string.Format(RedisKey.KeyCompanyNewsByStock, symbol, 0);
            var ls = redis.ContainsKey(key) ? redis.Get<List<string>>(key) : new List<string>();
            ls = ls.FindAll(sn => double.Parse(Utility.getDateTime(sn)) <= double.Parse(DateTime.Now.ToString("yyyyMMddHHmm")));
            var lspaging = Utility.GetPaging<string>(ls, 0 + 1, 10);
            for (int i = 0; i < lspaging.Count; i++)
            {
                lspaging[i] = Utility.getNewsID(lspaging[i]);
            }
            var tmp = redis.GetAll<StockNews>(lspaging);

            foreach (StockNews sn in tmp.Values)
            {
                ret.Add(sn);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            var key = "stock:id:37460:compact";
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var a = redis.Get<StockNews>(key);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var ls = new List<string>();
            ls.Add(string.Format(RedisKey.KeyCompanyNewsByCate, "0"));
            ls.Add(string.Format(RedisKey.KeyCompanyNewsByCate, "1"));
            ls.Add(string.Format(RedisKey.KeyCompanyNewsByCate, "2"));
            ls.Add(string.Format(RedisKey.KeyCompanyNewsByCate, "3"));
            ls.Add(string.Format(RedisKey.KeyCompanyNewsByCate, "4"));
            ls.Add(string.Format(RedisKey.KeyCompanyNewsByCate, "5"));

            redis.RemoveAll(ls);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var sql = new SqlDb();
            var keylist = RedisKey.KeyAnalysisReport;
            var ls = new List<string>();
            var pdt = sql.GetAnalysisReports("A", 2000);
            foreach (DataRow rdr in pdt.Rows)
            {
                var key = string.Format(RedisKey.KeyAnalysisReportDetail, rdr["ID"]);
                var obj = new Reports() { ID = int.Parse(rdr["ID"].ToString()), Body = rdr["Des"].ToString(), DateDeploy = (DateTime)rdr["PublishDate"], file = new FileObject() { FileName = rdr["FileName"].ToString(), FileUrl = "http://images1.cafef.vn/Images/Uploaded/DuLieuDownload/PhanTichBaoCao/" + rdr["FileName"] }, IsHot = (rdr["IsHot"].ToString() == "1"), ResourceCode = rdr["Source"].ToString(), Symbol = rdr["Symbol"].ToString(), Title = rdr["title"].ToString(), SourceID = int.Parse(rdr["SourceId"].ToString()), ResourceName = rdr["SourceFullName"].ToString(), ResourceLink = rdr["SourceUrl"].ToString() };
                var id = obj.DateDeploy.ToString("yyyyMMdd") + obj.ID;
                if (redis.ContainsKey(key))
                    redis.Set(key, obj);
                else
                    redis.Add(key, obj);
                if (!ls.Contains(id)) ls.Add(id);
            }
            //ls.Add("201101131222");
            //ls.Add("201103021220");
            ls.Sort();
            ls.Reverse();

            if (redis.ContainsKey(keylist))
                redis.Set(keylist, ls);
            else
                redis.Add(keylist, ls);
            MessageBox.Show(ls[0]);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            /*----- hnx -----*/
            var hnxkey = string.Format(RedisKey.KeyRealTimePrice, 2);
            var hnx = ConvertPrice(Utils.Deserialize(redis.Get<string>(hnxkey))) ?? new DataTable();
            var hnxpu = new List<TopStock>();
            var hnxpd = new List<TopStock>();
            var hnxvd = new List<TopStock>();
            if (hnx.Rows.Count > 0)
            {
                var bAvg = !Utils.InTradingTime(2);
                MessageBox.Show(hnx.Rows[0]["Symbol"].ToString());
                MessageBox.Show(hnx.Rows[0]["PriceChange"].ToString());
                MessageBox.Show(hnx.Rows[0]["AverageChange"].ToString());
                MessageBox.Show(hnx.Rows[0]["DoublePriceChange"].ToString());
                MessageBox.Show(hnx.Rows[0]["DoubleAvgChange"].ToString());
                var hrs = hnx.Select("Symbol <> 'CENTER'", (bAvg ? "DoubleAvgChange" : "DoublePriceChange") + " DESC");
                var key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.PriceUp);
                //var hnxpu = new List<TopStock>();
                for (var i = 0; i < 10; i++)
                {
                    if (hrs.Length <= i || double.Parse(hrs[i][(bAvg ? "DoubleAvgChange" : "DoublePriceChange")].ToString()) <= 0) break;
                    hnxpu.Add(new TopStock() { Symbol = hrs[i]["Symbol"].ToString(), BasicPrice = double.Parse(hrs[i]["Ref"].ToString()), Price = double.Parse(hrs[i][(bAvg ? "AveragePrice" : "TradingPrice")].ToString()), Volume = double.Parse(hrs[i]["TradingVol"].ToString()) });
                }
                if (redis.ContainsKey(key))
                    redis.Set(key, hnxpu);
                else
                    redis.Add(key, hnxpu);
                /****************/
                hrs = hnx.Select("Symbol <> 'CENTER'", (bAvg ? "DoubleAvgChange" : "DoublePriceChange") + " ASC, Symbol");
                key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.PriceDown);
                //var hnxpd = new List<TopStock>();
                for (var i = 0; i < 10; i++)
                {
                    if (hrs.Length <= i || double.Parse(hrs[i][(bAvg ? "DoubleAvgChange" : "DoublePriceChange")].ToString()) >= 0) break;
                    hnxpd.Add(new TopStock() { Symbol = hrs[i]["Symbol"].ToString(), BasicPrice = double.Parse(hrs[i]["Ref"].ToString()), Price = double.Parse(hrs[i][(bAvg ? "AveragePrice" : "TradingPrice")].ToString()), Volume = double.Parse(hrs[i]["TradingVol"].ToString()) });
                }
                if (redis.ContainsKey(key))
                    redis.Set(key, hnxpd);
                else
                    redis.Add(key, hnxpd);
                /****************/
                hrs = hnx.Select("Symbol <> 'CENTER'", "DoubleVolume DESC, Symbol");
                key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.VolumeDown);
                //var hnxvd = new List<TopStock>();
                for (var i = 0; i < 10; i++)
                {
                    if (hrs.Length <= i || double.Parse(hrs[i]["TradingVol"].ToString()) <= 0) break;
                    hnxvd.Add(new TopStock() { Symbol = hrs[i]["Symbol"].ToString(), BasicPrice = double.Parse(hrs[i]["Ref"].ToString()), Price = double.Parse(hrs[i][(bAvg ? "AveragePrice" : "TradingPrice")].ToString()), Volume = double.Parse(hrs[i]["TradingVol"].ToString()) });
                }
                if (redis.ContainsKey(key))
                    redis.Set(key, hnxvd);
                else
                    redis.Add(key, hnxvd);
            }
            /*--------------*/
            /*----- hsx ----*/
            var hsxkey = string.Format(RedisKey.KeyRealTimePrice, 1);
            var hsx = ConvertPrice(Utils.Deserialize(redis.Get<string>(hsxkey))) ?? new DataTable();
            var hsxpu = new List<TopStock>();
            var hsxpd = new List<TopStock>();
            var hsxvd = new List<TopStock>();
            if (hsx.Rows.Count > 0)
            {
                var hrs = hsx.Select("Symbol <> 'CENTER'", "DoublePriceChange DESC, Symbol");
                var key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.PriceUp);
                //var hsxpu = new List<TopStock>();
                for (var i = 0; i < 10; i++)
                {
                    if (hrs.Length <= i || double.Parse(hrs[i]["DoublePriceChange"].ToString()) <= 0) break;
                    hsxpu.Add(new TopStock() { Symbol = hrs[i]["Symbol"].ToString(), BasicPrice = double.Parse(hrs[i]["Ref"].ToString()), Price = double.Parse(hrs[i]["TradingPrice"].ToString()), Volume = double.Parse(hrs[i]["TradingVol"].ToString()) });
                }
                if (redis.ContainsKey(key))
                    redis.Set(key, hsxpu);
                else
                    redis.Add(key, hsxpu);
                /****************/
                hrs = hsx.Select("Symbol <> 'CENTER'", "DoublePriceChange, Symbol");
                key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.PriceDown);
                //var hsxpd = new List<TopStock>();
                for (var i = 0; i < 10; i++)
                {
                    if (hrs.Length <= i || double.Parse(hrs[i]["DoublePriceChange"].ToString()) >= 0) break;
                    hsxpd.Add(new TopStock() { Symbol = hrs[i]["Symbol"].ToString(), BasicPrice = double.Parse(hrs[i]["Ref"].ToString()), Price = double.Parse(hrs[i]["TradingPrice"].ToString()), Volume = double.Parse(hrs[i]["TradingVol"].ToString()) });
                }
                if (redis.ContainsKey(key))
                    redis.Set(key, hsxpd);
                else
                    redis.Add(key, hsxpd);
                /****************/
                hrs = hsx.Select("Symbol <> 'CENTER'", "DoubleVolume DESC, Symbol");
                key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.VolumeDown);
                //var hsxvd = new List<TopStock>();
                for (var i = 0; i < 10; i++)
                {
                    if (hrs.Length <= i || double.Parse(hrs[i]["DoubleVolume"].ToString()) <= 0) break;
                    hsxvd.Add(new TopStock() { Symbol = hrs[i]["Symbol"].ToString(), BasicPrice = double.Parse(hrs[i]["Ref"].ToString()), Price = double.Parse(hrs[i]["TradingPrice"].ToString()), Volume = double.Parse(hrs[i]["TradingVol"].ToString()) });
                }
                if (redis.ContainsKey(key))
                    redis.Set(key, hsxvd);
                else
                    redis.Add(key, hsxvd);
            }
            /*--------------*/
            /*----- all ----*/
            var all = MergeList(hnxpu, hsxpu, RedisKey.KeyTopStockType.PriceUp);
            var allkey = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.PriceUp);
            if (redis.ContainsKey(allkey))
                redis.Set(allkey, all);
            else
                redis.Add(allkey, all);
            all = MergeList(hnxpd, hsxpd, RedisKey.KeyTopStockType.PriceDown);
            allkey = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.PriceDown);
            if (redis.ContainsKey(allkey))
                redis.Set(allkey, all);
            else
                redis.Add(allkey, all);
            all = MergeList(hnxvd, hsxvd, RedisKey.KeyTopStockType.VolumeDown);
            allkey = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.VolumeDown);
            if (redis.ContainsKey(allkey))
                redis.Set(allkey, all);
            else
                redis.Add(allkey, all);
            /*--------------*/
        }
        private static DataTable ConvertPrice(DataTable hnx)
        {
            // var tmp = hnx.Clone();
            // tmp.Columns["AverageChange"].DataType = typeof(double);
            // tmp.Columns["PriceChange"].DataType = typeof(double);
            // tmp.Columns["TradingVol"].DataType = typeof(double);
            //foreach (DataRow dr in hnx.Rows)
            // {
            //     tmp.ImportRow(dr);
            // }
            // return tmp;
            hnx.Columns.Add(new DataColumn("DoubleAvgChange", typeof(double), "Convert(AverageChange, 'System.Double')"));
            hnx.Columns.Add(new DataColumn("DoublePriceChange", typeof(double), "Convert(PriceChange, 'System.Double')"));
            hnx.Columns.Add(new DataColumn("DoubleVolume", typeof(double), "Convert(TradingVol, 'System.Double')"));
            hnx.AcceptChanges();
            return hnx;
        }
        private static List<TopStock> MergeList(List<TopStock> hnx, List<TopStock> hsx, string type)
        {
            var ret = hnx;
            ret.InsertRange(0, hsx);

            switch (type)
            {
                case RedisKey.KeyTopStockType.PriceUp:
                    ret.Sort("ChangePrice DESC"); break;
                case RedisKey.KeyTopStockType.PriceDown:
                    ret.Sort("ChangePrice ASC"); break;
                case RedisKey.KeyTopStockType.VolumeDown:
                    ret.Sort("Volume DESC"); break;
            }
            //foreach (var item in hsx)
            //{
            //    double val = 0;
            //    switch (type)
            //    {
            //        case RedisKey.KeyTopStockType.PriceUp:
            //            val = item.Price - item.BasicPrice; break;
            //        case RedisKey.KeyTopStockType.PriceDown:
            //            val = item.BasicPrice - item.Price; break;
            //        case RedisKey.KeyTopStockType.VolumeDown:
            //            val = item.Volume; break;
            //    }
            //    for (var i = 0; i < ret.Count; i++)
            //    {
            //        double cval = 0;
            //        switch (type)
            //        {
            //            case RedisKey.KeyTopStockType.PriceUp:
            //                cval = item.Price - item.BasicPrice; break;
            //            case RedisKey.KeyTopStockType.PriceDown:
            //                cval = item.BasicPrice - item.Price; break;
            //            case RedisKey.KeyTopStockType.VolumeDown:
            //                cval = item.Volume; break;
            //        }
            //        if (val <= cval) continue;
            //        ret.Insert(i, item);
            //        break;
            //    }
            //}

            while (ret.Count > 10) ret.RemoveAt(10);
            return ret;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var id = "58231";
            var compactkey = string.Format(RedisKey.KeyCompanyNewsCompact, id);
            if (redis.ContainsKey(compactkey))
                redis.Remove(compactkey);
            var detailkey = string.Format(RedisKey.KeyCompanyNewsDetail, id);
            if (redis.ContainsKey(detailkey))
                redis.Remove(detailkey);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var symbol = "CEO_00662";
            var sql = new SqlDb();
            #region CEO

            var ceokey = string.Format(RedisKey.CeoKey, symbol);
            var cdt = sql.GetCeosNew_Profile(symbol);
            if (cdt.Rows.Count == 0)
            {
                if (redis.ContainsKey(ceokey)) redis.Remove(ceokey);
            }
            else
            {
                var ceo = new Ceo();
                ceo.CeoName = cdt.Rows[0]["CeoName"].ToString(); ceo.CeoBirthday = cdt.Rows[0]["CeoBirthday"].ToString(); ceo.CeoIdNo = cdt.Rows[0]["CeoIdNo"].ToString(); ceo.CeoAchievements = cdt.Rows[0]["CeoAchievements"].ToString(); ceo.CeoHomeTown = cdt.Rows[0]["CeoHomeTown"].ToString(); ceo.CeoSchoolDegree = cdt.Rows[0]["CeoLevel"].ToString();
                ceo.CeoCode = cdt.Rows[0]["CeoCode"].ToString();
                if (ceo.CeoBirthday != "" && ceo.CeoBirthday.Contains("/"))
                {
                    ceo.CeoBirthday = ceo.CeoBirthday.Substring(ceo.CeoBirthday.LastIndexOf("/") + 1);
                }
                ceo.CeoImage = "";
                //photo
                //school title
                var css = new List<CeoSchool>();
                var sdt = sql.GetCeosNew_School(symbol);
                foreach (DataRow sdr in sdt.Rows)
                {
                    css.Add(new CeoSchool() { CeoTitle = sdr["CeoTitle"].ToString(), SchoolTitle = sdr["SchoolTitle"].ToString(), SchoolYear = sdr["SchoolYear"].ToString() });
                }
                ceo.CeoSchool = css;

                //ceo position
                var cps = new List<CeoPosition>();
                var pdt = sql.GetCeosNew_Position(symbol);
                foreach (DataRow pdr in pdt.Rows)
                {
                    var cp = new CeoPosition() { PositionTitle = pdr["PositionTitle"].ToString(), PositionCompany = pdr["PositionCompany"].ToString() };
                    if (string.IsNullOrEmpty(cp.PositionTitle)) cp.PositionTitle = pdr["PositionName"].ToString();
                    if (string.IsNullOrEmpty(cp.PositionCompany)) cp.PositionCompany = pdr["FullName"].ToString();

                    //__/01/2007
                    string cpd = pdr["CeoPosDate"].ToString();
                    if (cpd.Contains("/"))
                    {
                        int day, month, year;
                        if (!int.TryParse(cpd.Substring(0, cpd.IndexOf("/")), out day)) day = 0;
                        cpd = cpd.Substring(cpd.IndexOf("/") + 1);
                        if (!int.TryParse(cpd.Substring(0, cpd.IndexOf("/")), out month)) month = 0;
                        cpd = cpd.Substring(cpd.IndexOf("/") + 1);
                        if (!int.TryParse(cpd, out year)) year = 0;
                        if (year == 0)
                        {
                            cp.CeoPosDate = "";
                        }
                        else if (day == 0 && month == 0)
                        {
                            cp.CeoPosDate = "" + year;
                        }
                        else if (month > 0 && day == 0)
                        {
                            cp.CeoPosDate = month + "/" + year;
                        }
                        else if (day > 0 && month > 0)
                        {
                            cp.CeoPosDate = day + "/" + month + "/" + year;
                        }
                        else
                        {
                            cp.CeoPosDate = "";
                        }
                    }

                    cps.Add(cp);
                }
                ceo.CeoPosition = cps;

                //asset
                var cas = new List<CeoAsset>();
                var adt = sql.GetCeosNew_Asset(symbol);
                foreach (DataRow adr in adt.Rows)
                {
                    cas.Add(new CeoAsset() { Symbol = adr["Symbol"].ToString(), AssetVolume = double.Parse(adr["AssetVolume"].ToString()).ToString("#,##0"), UpdatedDate = ((DateTime)adr["UpdatedDate"]).ToString("MM/yyyy") });
                }
                ceo.CeoAsset = cas;

                //relation
                var crs = new List<CeoRelation>();
                var rdt = sql.GetCeosNew_Relation(symbol);
                foreach (DataRow rdr in rdt.Rows)
                {
                    crs.Add(new CeoRelation() { Symbol = rdr["Symbol"].ToString(), AssetVolume = double.Parse(rdr["AssetVolume"].ToString()).ToString("#,##0"), UpdatedDate = ((DateTime)rdr["UpdatedDate"]).ToString("MM/yyyy"), Name = rdr["CeoName"].ToString(), CeoCode = rdr["CeoCode"].ToString(), RelationTitle = rdr["RelationTitle"].ToString() });
                }
                ceo.CeoRelation = crs;

                //process
                var cos = new List<CeoProcess>();
                var odt = sql.GetCeosNew_Process(symbol);
                foreach (DataRow odr in odt.Rows)
                {
                    cos.Add(new CeoProcess() { ProcessBegin = odr["ProcessBegin"].ToString(), ProcessEnd = odr["ProcessEnd"].ToString(), ProcessDesc = odr["ProcessDesc"].ToString(), Symbol = odr["Symbol"].ToString()});
                }
                ceo.CeoRelation = crs;

                //news
                var cns = new List<CeoNews>();
                var wdt = sql.GetCeosNew_News(symbol);
                var ids = "";
                foreach (DataRow ndr in wdt.Rows)
                {
                    ids += "," + ndr["NewsId"];
                }
                var wdt2 = sql.GetCeosNew_NewsDetail(ids);
                foreach (DataRow ndr in wdt2.Rows)
                {
                    cns.Add(new CeoNews() { Title = ndr["News_Title"].ToString(), PublishDate = (DateTime)ndr["News_PublishDate"], NewsLink = string.Format("/{0}CA{1}/{2}.chn", ndr["News_Id"], ndr["Cat_ID"], CafeF.Redis.BL.Utils.UnicodeToKoDauAndGach(ndr["News_Title"].ToString())) });
                }
                ceo.CeoRelation = crs;

                if (redis.ContainsKey(ceokey))
                {
                    redis.Set(ceokey, ceo);
                }
                else
                {
                    redis.Add(ceokey, ceo);
                }
            }
            #endregion
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var full = CafeF.Redis.UpdateService.FinanceReportData.GetAllData();
        }

        #region FTP
        private bool useFtp = (ConfigurationManager.AppSettings["UseFtp"] ?? "").ToUpper() == "TRUE";
        private string ftpAddress = ConfigurationManager.AppSettings["FTPAddressCeo"] ?? "";
        private string ftpUser = ConfigurationManager.AppSettings["FTPUser"] ?? "";
        private string ftpPass = ConfigurationManager.AppSettings["FTPPassword"] ?? "";
        private void UploadCeoPhoto(string srcFile, string desName)
        {
            if (!useFtp) return;

            //Create FTP request
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpAddress + "/" + desName);

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(ftpUser, ftpPass);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            //Load the file
            FileStream stream = File.OpenRead(srcFile);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            //Upload file
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();
        }

        #endregion

        private void button16_Click(object sender, EventArgs e)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var key = string.Format(RedisKey.KeyCompanyNewsDetail, 32639);

            var sql = new SqlDb();
            var pdt = sql.GetCompanyNews("VNM", -1);

            var stocknews = new List<StockNews>();
            foreach (DataRow rdr in pdt.Rows)
            {
                var compact = new StockNews() { ID = int.Parse(rdr["ID"].ToString()), Body = "", DateDeploy = (DateTime)rdr["PostTime"], Image = "", Title = rdr["title"].ToString(), Sapo = "", TypeID = rdr["ConfigId"].ToString(), Symbol = rdr["StockSymbols"].ToString() };
                var obj = new StockNews() { ID = int.Parse(rdr["ID"].ToString()), Body = rdr["Content"].ToString(), DateDeploy = (DateTime)rdr["PostTime"], Image = rdr["ImagePath"].ToString(), Title = rdr["title"].ToString(), Sapo = rdr["SubContent"].ToString(), TypeID = rdr["ConfigId"].ToString(), Symbol = rdr["StockSymbols"].ToString() };


                if (obj.ID == 32639)
                {
                    redis.Set<StockNews>(key, obj);
                }
            }
            var o2 = redis.Get<StockNews>(key);
        }

        public struct FileStruct
        {
            public string Flags;
            public string Owner;
            public string Group;
            public bool IsDirectory;
            public DateTime CreateTime;
            public string Name;
        }
        public enum FileListStyle
        {
            UnixStyle,
            WindowsStyle,
            Unknown
        }

        private void button17_Click(object sender, EventArgs e)
        {

            var symbol = "SCR";
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var sql = new SqlDb();
            var test = redis.Get<TienDoBDS>(string.Format(RedisKey.BDSKey, "SCR_07"));

            //var ls = redis.Get<List<TienDoBDS>>(key) ?? new List<TienDoBDS>();
            var ldt = sql.GetLandProject(symbol);
            var adt = sql.GetLandProject_Area(symbol);
            var pdt = sql.GetLandProject_Profit(symbol);
            foreach (DataRow ldr in ldt.Rows)
            {
                var key = string.Format(RedisKey.BDSKey, ldr["MaTienDo"].ToString());
                var o = new TienDoBDS() { MaCK = ldr["MaCK"].ToString(), MaTienDo = ldr["MaTienDo"].ToString(), TenDuAn = ldr["TenDuAn"].ToString(), HinhThucKinhDoanh = ldr["HinhThucKinhDoanh"].ToString(), DiaDiem = ldr["DiaDiem"].ToString(), ThanhPho = ldr["ThanhPho"].ToString(), TongVon = decimal.Parse(ldr["TongVon"].ToString()), Donvi = ldr["Donvi"].ToString(), TyLeGhopVon = ldr["TyLeGhopVon"].ToString(), TyLeDenBu = ldr["TyLeDenBu"].ToString(), GhiChu = ldr["GhiChu"].ToString(), MoTa = ldr["Mota"].ToString(), URL = ldr["URL"].ToString(), ID = int.Parse(ldr["ID"].ToString()), BDSImages = GetLandImages(ldr["MaTienDo"].ToString()) };
                DateTime d;
                if (DateTime.TryParse(ldr["ViewDate"].ToString(), out d))
                {
                    o.ViewDate = d;      
                }
              
                var adrs = adt.Select("MaTienDo='" + o.MaTienDo + "'");
                var als = new List<TienDoBDSDienTich>();
                foreach(var adr in adrs)
                {
                    als.Add(new TienDoBDSDienTich(){MaTienDo = o.MaTienDo, DienTich = decimal.Parse(adr["DienTich"].ToString()), LoaiDienTich = adr["LoaiDienTich"].ToString()});
                }
                o.DienTichs = als;

                var pdrs = pdt.Select("MaTienDo='" + o.MaTienDo + "'");
                var pls = new List<TienDoBDSLoiNhuan>();
                foreach (var pdr in pdrs)
                {
                    pls.Add(new TienDoBDSLoiNhuan() { MaTienDo = o.MaTienDo, LoiNhuanDoanhThu = decimal.Parse(pdr["LoiNhuanDoanhThu"].ToString()), LoaiLoiNhuan = pdr["LoaiLoiNhuan"].ToString() });
                }
                o.LoiNhuans = pls;

                if (redis.ContainsKey(key))
                    redis.Set(key, o);
                else
                    redis.Add(key, o);

                //ls.Add(o);
            }

            var a = 0;
        }

        private List<string> GetLandImages(string MaTienDo)
        {
            bool useFtp = true; // (ConfigurationManager.AppSettings["UseFtp"] ?? "").ToUpper() == "TRUE";
            string ftpAddress = ConfigurationManager.AppSettings["FTPAddressTienDoBDS"] ?? "ftp://222.255.27.100/Images/Uploaded/DuLieuDownload/RealEstate/";
            string ftpUser = ConfigurationManager.AppSettings["FTPUser"] ?? "";
            string ftpPass = ConfigurationManager.AppSettings["FTPPassword"] ?? "";
            string storageFolder = ConfigurationManager.AppSettings["StorageTienDoBDS"] ?? "Common/TienDoBDS/";
            string webAddress = ConfigurationManager.AppSettings["WebAddressTienDoBDS"] ?? "http://images1.cafef.vn/Images/Uploaded/DuLieuDownload/CEO/";

            var files = StorageUtils.Utils.GetFileList(ftpAddress, ftpUser, ftpPass);
            var ret = new List<string>();
            foreach (var file in files)
            {
                if (file.StartsWith(MaTienDo, true, CultureInfo.InvariantCulture))
                {
                    try
                    {
                        if (StorageUtils.Utils.checkImageExtension(file))
                        {
                            //StorageUtils.Utils.UploadFile(file, storageFolder, ftpAddress, ftpUser, ftpPass);
                           // StorageUtils.Utils.UploadFile(webAddress, file, storageFolder);
                            ret.Add(file);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return ret;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            MessageBox.Show(StorageUtils.Utils.UploadFile("http://images1.cafef.vn/batdongsan/Images/media/", "noimage.jpg", "Common/CEO/"));
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);

            //bool useFtp = true; // (ConfigurationManager.AppSettings["UseFtp"] ?? "").ToUpper() == "TRUE";
            //string ftpAddress = ConfigurationManager.AppSettings["FTPAddressTienDoBDS"] ?? "ftp://222.255.27.100/Images/Uploaded/DuLieuDownload/RealEstate/";
            //string ftpUser = ConfigurationManager.AppSettings["FTPUser"] ?? "";
            //string ftpPass = ConfigurationManager.AppSettings["FTPPassword"] ?? "";
            //string storageFolder = ConfigurationManager.AppSettings["StorageTienDoBDS"] ?? "Common/TienDoBDS/";
            //string webAddress = ConfigurationManager.AppSettings["WebAddressTienDoBDS"] ?? "http://images1.cafef.vn/Images/Uploaded/DuLieuDownload/CEO/";

            //var files = StorageUtils.Utils.GetFileList(ftpAddress, ftpUser, ftpPass);
            //foreach (var photo in files)
            //{
            //    if (!photo.Contains(".")) continue;
            //    var code = photo.Substring(0, photo.IndexOf("."));
            //    //if (redis.ContainsKey(string.Format(RedisKey.BDSImage, code)))
            //    //{
            //    //    redis.Set(string.Format(RedisKey.BDSImage, code), photo);
            //    //}
            //    //else
            //    //{
            //    //    redis.Add(string.Format(RedisKey.BDSImage, code), photo);
            //    //}
            //}

            //var symbol = "SCR";
            //var tds = new List<TienDoBDS>();
            //var tdkeys = redis.SearchKeys(string.Format(RedisKey.BDSKey, "*"));

            //foreach (string hKey in tdkeys)
            //{
            //    if (redis.ContainsKey(hKey))
            //    {
            //        try
            //        {
            //            TienDoBDS td = redis.Get<TienDoBDS>(hKey);
            //            if (td.MaCK.ToUpper() == symbol)
            //                tds.Add(td);
            //        }
            //        catch { }
            //    }
            //}
            //tds.Sort("MaTienDo asc,TenDuAn asc ");

            //var MaTienDos = new List<string>();
            //foreach (var item in tds)
            //{
            //    if (!MaTienDos.Contains(item.MaTienDo)) MaTienDos.Add(item.MaTienDo);
            //}


            //var pattern = "";
            //foreach (var ma in MaTienDos)
            //{
            //    pattern += (pattern == "" ? "" : "|") + ma.Trim();
            //}
            //pattern = "[" + pattern + "]*";
            //var keylist = redis.SearchKeys(string.Format(RedisKey.BDSImage, pattern));
            //var ret = new Dictionary<string, List<string>>();
            //foreach (var ma in MaTienDos)
            //{
            //    var s = string.Format(RedisKey.BDSImage, ma);
            //    ret.Add(ma, keylist.FindAll(k => k.ToUpper().StartsWith(s.ToUpper())));
            //}
            //var b = ret;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                bool useFtp = true; // (ConfigurationManager.AppSettings["UseFtp"] ?? "").ToUpper() == "TRUE";
                string ftpAddress = ConfigurationManager.AppSettings["FTPAddressCeo"] ?? "ftp://222.255.27.100/Images/Uploaded/DuLieuDownload/Ceo";
                string ftpUser = ConfigurationManager.AppSettings["FTPUser"] ?? "";
                string ftpPass = ConfigurationManager.AppSettings["FTPPassword"] ?? "";
                string storageFolder = ConfigurationManager.AppSettings["StorageCeo"] ?? "Common/Ceo";
                string webAddress = ConfigurationManager.AppSettings["WebAddressCeo"] ?? "http://images1.cafef.vn/Images/Uploaded/DuLieuDownload/CEO/";

                var files = StorageUtils.Utils.GetFileList(ftpAddress + "/", ftpUser, ftpPass);
                var ret = new List<string>();
                foreach (var file in files)
                {
                    if (file.StartsWith("CEO_00058", true, CultureInfo.InvariantCulture))
                    {
                    try
                    {
                        if (StorageUtils.Utils.checkImageExtension(file))
                        {
                            //StorageUtils.Utils.UploadFile(file, storageFolder, ftpAddress, ftpUser, ftpPass);
                            StorageUtils.Utils.UploadFile(webAddress, file, storageFolder);
                            ret.Add(file);
                        }
                    }
                    catch (Exception ex)
                    {
                        //log.WriteEntry("GetCeoPhotos - " + file + " - " + ex.ToString(), EventLogEntryType.Error);
                    }
                    }
                }
               // return ret;
            }
            catch (Exception ex)
            {
                //log.WriteEntry("GetCeoPhotos - " + ex.ToString(), EventLogEntryType.Error);
                //return new List<string>();
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var keys = redis.SearchKeys(string.Format(RedisKey.CeoKey, "*"));
            var sql = new SqlDb();
            sql.OpenDb();
            var dt = sql.GetAllCeos();
            var i = 0;
            foreach(var key in keys)
            {
                //ceo:ceocode:{0}:Object
                var t = key.Replace("ceo:ceocode:", "").Replace(":Object","");
                if (dt.Select("CeoCode = '" + t + "'").Length==0)
                {
                    redis.Remove(key);
                    i++;
                }
            }
            sql.CloseDb();

            MessageBox.Show(i.ToString("#,##0"));
        }

        private void button22_Click(object sender, EventArgs e)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var keys = redis.SearchKeys(string.Format(RedisKey.BDSKey, "*"));
            var sql = new SqlDb();
            sql.OpenDb();
            var dt = sql.GetAllLandProjects();
            var i = 0;
            foreach (var key in keys)
            {
                //ceo:ceocode:{0}:Object
                var t = key.Replace("tiendoBDS:tiendocode:", "").Replace(":Object", "");
                if (dt.Select("MaTienDo = '" + t + "'").Length == 0)
                {
                    redis.Remove(key);
                    i++;
                }
            }
            sql.CloseDb();

            MessageBox.Show(i.ToString("#,##0"));
        }

        private void button23_Click(object sender, EventArgs e)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var sql = new SqlDb();
            sql.OpenDb();
            var date = "";
            #region Lịch sự kiện
            var lskdt = sql.GetLichSuKien(date);
            var keys = redis.ContainsKey(RedisKey.KeyLichSuKien) ? redis.Get<List<string>>(RedisKey.KeyLichSuKien) : new List<string>();
            var removals = keys.FindAll(s => s.Substring(0, 8) == date.Replace(".", ""));
            foreach (var removal in removals)
            {
                var key = string.Format(RedisKey.KeyLichSuKienObject, removal.Substring(removal.LastIndexOf(":") + 1));
                if (redis.ContainsKey(key)) redis.Remove(key);
                keys.Remove(removal);
            }
            foreach (DataRow ldr in lskdt.Rows)
            {
                var o = new LichSuKien() { ID = int.Parse(ldr["ID"].ToString()), LoaiSuKien = ldr["EventType_List"].ToString(), MaCK = ldr["StockSymbols"].ToString(), MaSan = 0, News_ID = ldr["News_ID"].ToString(), Title = ldr["EventTitle"].ToString(), NgayBatDau = ldr["NgayBatDau"].ToString(), NgayKetThuc = ldr["NgayKetThuc"].ToString(), NgayThucHien = ldr["NgayThucHien"].ToString(), TenCty = "", TomTat = "", PostDate = (DateTime) ldr["PostDate"]};
                try
                {
                    o.EventDate = (DateTime) ldr["EventDate"];
                }catch(Exception)
                {
                    o.EventDate = DateTime.Parse("2000-01-01");
                }
                var key = string.Format(RedisKey.KeyLichSuKienObject, o.ID);
                if (redis.ContainsKey(key))
                    redis.Set(key, o);
                else
                    redis.Add(key, o);
                key = string.Format(RedisKey.KeyLichSuKienObjectInList, o.EventDate.ToString("yyyyMMdd"), string.IsNullOrEmpty(o.LoaiSuKien.Trim())? "_" : o.LoaiSuKien, o.ID);
                if (!keys.Contains(key)) keys.Add(key);
            }
            if (redis.ContainsKey(RedisKey.KeyLichSuKien))
                redis.Set(RedisKey.KeyLichSuKien, keys);
            else
                redis.Add(RedisKey.KeyLichSuKien, keys);
            #endregion
            #region Lịch sự kiện tóm tắt
            lskdt = sql.GetLichSuKienTomTat();
            var lls = new List<LichSuKien>();
            foreach (DataRow ldr in lskdt.Rows)
            {
                var o = new LichSuKien() { ID = int.Parse(ldr["ID"].ToString()), LoaiSuKien = ldr["EventType_List"].ToString(), MaCK = ldr["StockSymbols"].ToString(), MaSan = 0, News_ID = ldr["News_ID"].ToString(), Title = ldr["EventTitle"].ToString(), NgayBatDau = ldr["NgayBatDau"].ToString(), NgayKetThuc = ldr["NgayKetThuc"].ToString(), NgayThucHien = ldr["NgayThucHien"].ToString(), TenCty = "", TomTat = "", PostDate = (DateTime)ldr["PostDate"] };
                try
                {
                    o.EventDate = (DateTime)ldr["EventDate"];
                }
                catch (Exception)
                {
                    o.EventDate = DateTime.Parse("2000-01-01");
                }
                lls.Add(o);
            }
            if (redis.ContainsKey(RedisKey.KeyLichSuKienTomTat))
                redis.Set(RedisKey.KeyLichSuKienTomTat, lls);
            else
                redis.Add(RedisKey.KeyLichSuKienTomTat, lls);
            #endregion
            sql.CloseDb();
        }


    }
}
