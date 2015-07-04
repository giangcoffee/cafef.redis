using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;
using CafeF.Redis.UpdateService;
using ServiceStack.Redis;

namespace CafeF.Redis.TestUpdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            var start = DateTime.Now;
            textBox1.Text = start.ToString();

            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var sdt = SqlDb.GetSymbolList(-1);
            foreach (DataRow dr in sdt.Rows)
            {


                Thread.Sleep(1000);
                var start2 = DateTime.Now;

                string symbol = dr["Symbol"].ToString().ToUpper();
                symbol = "SSI";
                try
                {
                    #region Update stock from sql
                    //stock.Symbol = "AAA";
                    //load stock data 
                    var dt = SqlDb.GetSymbolData(symbol);
                    if (dt.Rows.Count <= 0) return;

                    var row = dt.Rows[0];
                    var stock = new Stock() { Symbol = row["Symbol"].ToString(), TradeCenterId = int.Parse(row["TradeCenterId"].ToString()), IsDisabled = row["IsDisabled"].ToString() == "1", StatusText = row["StatusText"].ToString(), ShowTradeCenter = row["ShowTradeCenter"].ToString() == "1", FolderImage = row["FolderChart"].ToString() };

                    //profile
                    var profile = new CompanyProfile { Symbol = stock.Symbol };
                    //profile - basicInfo
                    var basicInfo = new BasicInfo() { Symbol = stock.Symbol, Name = row["CompanyName"].ToString(), TradeCenter = stock.TradeCenterId.ToString() };
                    //profile - basicInfo - basicCommon
                    /*PE = double.Parse(row["PE"].ToString()),*/
                    var basicCommon = new BasicCommon() { AverageVolume = double.Parse(row["AVG10SS"].ToString()), Beta = double.Parse(row["Beta"].ToString()), EPS = double.Parse(row["EPS"].ToString()), Symbol = stock.Symbol, TotalValue = double.Parse(row["MarketCap"].ToString()), ValuePerStock = double.Parse(row["BookValue"].ToString()), VolumeTotal = double.Parse(row["SLCPNY"].ToString()) };
                    basicCommon.PE = basicCommon.EPS != 0 ? (double.Parse(row["LastPrice"].ToString()) / basicCommon.EPS) : 0;

                    basicInfo.basicCommon = basicCommon;
                    //profile - basicInfo - category
                    var category = new CategoryObject() { ID = int.Parse(row["CategoryId"].ToString()), Name = row["CategoryName"].ToString() };
                    basicInfo.category = category;
                    //profile - basicInfo - firstInfo
                    var firstInfo = new FirstInfo() { FirstPrice = double.Parse(row["FirstPrice"].ToString()), FirstTrade = row["FirstTrade"].Equals(DBNull.Value) ? null : ((DateTime?)row["FirstTrade"]), FirstVolume = double.Parse(row["FirstVolume"].ToString()), Symbol = stock.Symbol };
                    basicInfo.firstInfo = firstInfo;

                    profile.basicInfos = basicInfo;

                    //profile - subsidiaries
                    var subsidiaries = new List<OtherCompany>();
                    var associates = new List<OtherCompany>();
                    var cdt = SqlDb.GetChildrenCompany(stock.Symbol);
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

                    //profile - commonInfo
                    var commonInfo = new CommonInfo() { Symbol = stock.Symbol, Capital = double.Parse(row["VonDieuLe"].ToString()), Category = row["CategoryName"].ToString(), Content = row["About"].ToString(), OutstandingVolume = double.Parse(row["TotalShare"].ToString()), TotalVolume = double.Parse(row["SLCPNY"].ToString()) };
                    commonInfo.Content += "<p><b>Địa chỉ:</b> " + row["Address"].ToString() + "</p>";
                    commonInfo.Content += "<p><b>Điện thoại:</b> " + row["Phone"].ToString() + "</p>";
                    commonInfo.Content += "<p><b>Người phát ngôn:</b> " + row["Spokenman"].ToString() + "</p>";
                    if (string.IsNullOrEmpty(row["Email"].ToString())) commonInfo.Content += "<p><b>Email:</b> <a href='mailto:" + row["Email"] + "'>" + row["Email"] + "</a></p>";
                    if (string.IsNullOrEmpty(row["Website"].ToString())) commonInfo.Content += "<p><b>Website:</b> <a href='" + row["Website"] + "'>" + row["Website"] + "</a></p>";

                    profile.commonInfos = commonInfo;


                    //profile - financePeriod
                    var fyt = SqlDb.GetFinancePeriod(stock.Symbol);
                    var periods = new List<FinancePeriod>();
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
                    var financeInfo = new List<FinanceInfo>();
                    var fit = SqlDb.GetChiTieuFinance(stock.Symbol);
                    var fvt = SqlDb.GetFinanceData(stock.Symbol);
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

                    //profile - leader
                    var leaders = new List<Leader>();
                    var ldt = SqlDb.GetCeos(stock.Symbol);
                    foreach (DataRow ldr in ldt.Rows)
                    {
                        leaders.Add(new Leader() { GroupID = ldr["ParentId"].ToString(), Name = ldr["FullName"].ToString(), Positions = ldr["TenNhom"].ToString() });
                    }
                    profile.Leaders = leaders;

                    //profile - owner
                    var owners = new List<MajorOwner>();
                    var odt = SqlDb.GetShareHolders(stock.Symbol);
                    foreach (DataRow odr in odt.Rows)
                    {
                        owners.Add(new MajorOwner() { Name = odr["FullName"].ToString(), Rate = double.Parse(odr["ShareHoldPct"].ToString()), ToDate = (DateTime)odr["DenNgay"], Volume = double.Parse(odr["SoCoPhieu"].ToString()) });
                    }
                    profile.MajorOwners = owners;

                    stock.CompanyProfile = profile;

                    //business plans
                    var plans = new List<BusinessPlan>();
                    if (row["HasPlan"].ToString() == "1")
                    {
                        plans.Add(new BusinessPlan() { Body = row["PlanNote"].ToString(), Date = (DateTime)row["PlanDate"], DividendsMoney = double.Parse(row["Dividend"].ToString()), DividendsStock = double.Parse(row["DivStock"].ToString()), ID = int.Parse(row["PlanId"].ToString()), IncreaseExpected = double.Parse(row["CapitalRaising"].ToString()), ProfitATax = double.Parse(row["NetIncome"].ToString()), ProfitBTax = double.Parse(row["TotalProfit"].ToString()), Revenue = double.Parse(row["TotalIncome"].ToString()), Symbol = stock.Symbol, Year = int.Parse(row["KYear"].ToString()) });
                    }
                    stock.BusinessPlans1 = plans;

                    //dividend histories
                    var divs = new List<DividendHistory>();
                    var ddt = SqlDb.GetDividendHistory(stock.Symbol);
                    foreach (DataRow ddr in ddt.Rows)
                    {
                        divs.Add(new DividendHistory() { DonViDoiTuong = ddr["DonViDoiTuong"].ToString(), NgayGDKHQ = (DateTime)ddr["NgayGDKHQ"], GhiChu = ddr["GhiChu"].ToString(), SuKien = ddr["SuKien"].ToString(), Symbol = stock.Symbol, TiLe = ddr["TiLe"].ToString() });
                    }
                    stock.DividendHistorys = divs;

                    //báo cáo phân tích
                    var reports = new List<Reports>();
                    var rdt = SqlDb.GetAnalysisReports(stock.Symbol);
                    foreach (DataRow rdr in rdt.Rows)
                    {
                        reports.Add(new Reports() { ID = int.Parse(rdr["ID"].ToString()), Title = rdr["title"].ToString(), DateDeploy = (DateTime)rdr["PublishDate"], ResourceCode = rdr["Source"].ToString(), IsHot = rdr["IsHot"].ToString().ToLower() == "true" });
                    }
                    stock.Reports3 = reports;

                    //công ty cùng ngành
                    var samecateCompanies = new List<StockShortInfo>();
                    var scdt = SqlDb.GetSameCateCompanies(stock.Symbol);
                    foreach (DataRow scdr in scdt.Rows)
                    {
                        samecateCompanies.Add(new StockShortInfo() { Symbol = scdr["StockSymbol"].ToString(), TradeCenterId = int.Parse(scdr["TradeCenterId"].ToString()), Name = scdr["FullName"].ToString(), EPS = double.Parse(scdr["EPS"].ToString()) });
                    }
                    stock.SameCategory = samecateCompanies;

                    //eps tương đương
                    var sameEPSCompanies = new List<StockShortInfo>();
                    var sedt = SqlDb.GetSameEPSCompanies(stock.Symbol);
                    foreach (DataRow sedr in sedt.Rows)
                    {
                        sameEPSCompanies.Add(new StockShortInfo() { Symbol = sedr["StockSymbol"].ToString(), TradeCenterId = int.Parse(sedr["TradeCenterId"].ToString()), Name = sedr["FullName"].ToString(), EPS = double.Parse(sedr["EPS"].ToString()), MarketValue = double.Parse(sedr["MarketCap"].ToString()) });
                    }
                    stock.SameEPS = sameEPSCompanies;

                    //pe tương đương
                    var samePECompanies = new List<StockShortInfo>();
                    var spdt = SqlDb.GetSamePECompanies(stock.Symbol);
                    foreach (DataRow spdr in spdt.Rows)
                    {
                        samePECompanies.Add(new StockShortInfo() { Symbol = spdr["StockSymbol"].ToString(), TradeCenterId = int.Parse(spdr["TradeCenterId"].ToString()), Name = spdr["FullName"].ToString(), EPS = double.Parse(spdr["EPS"].ToString()), MarketValue = double.Parse(spdr["MarketCap"].ToString()) });
                    }
                    stock.SamePE = samePECompanies;

                    //stock history
                    var history = new StockCompactHistory();

                    //stock history - price
                    var price = new List<PriceCompactHistory>();
                    var pdt = SqlDb.GetPriceHistory(stock.Symbol, 10);
                    foreach (DataRow pdr in pdt.Rows)
                    {
                        price.Add(new PriceCompactHistory() { ClosePrice = double.Parse(pdr["Price"].ToString()), BasicPrice = double.Parse(pdr["BasicPrice"].ToString()), Ceiling = double.Parse(pdr["Ceiling"].ToString()), Floor = double.Parse(pdr["Floor"].ToString()), Volume = double.Parse(pdr["Volume"].ToString()), TotalValue = double.Parse(pdr["TotalValue"].ToString()), TradeDate = (DateTime)pdr["TradeDate"] });

                    }
                    history.Price = price;

                    //stock history - order
                    var order = new List<OrderCompactHistory>();
                    var hodt = SqlDb.GetOrderHistory(stock.Symbol, 10);
                    foreach (DataRow hodr in hodt.Rows)
                    {
                        order.Add(new OrderCompactHistory() { AskAverageVolume = double.Parse(hodr["AskAverage"].ToString()), AskLeft = double.Parse(hodr["AskLeft"].ToString()), BidAverageVolume = double.Parse(hodr["BidAverage"].ToString()), BidLeft = double.Parse(hodr["BidLeft"].ToString()), TradeDate = (DateTime)hodr["Trading_Date"] });
                    }
                    history.Orders = order;

                    //stock history - foreign
                    var foreign = new List<ForeignCompactHistory>();
                    var fdt = SqlDb.GetForeignHistory(stock.Symbol, 10);
                    foreach (DataRow fdr in fdt.Rows)
                    {
                        foreign.Add(new ForeignCompactHistory() { BuyPercent = double.Parse(fdr["FBuyPercent"].ToString()), SellPercent = double.Parse(fdr["FSellPercent"].ToString()), NetVolume = double.Parse(fdr["FNetVolume"].ToString()), NetValue = double.Parse(fdr["FNetValue"].ToString()), TradeDate = (DateTime)fdr["Trading_Date"] });
                    }
                    history.Foreign = foreign;

                    stock.StockPriceHistory = history;

                    //tin tức và sự kiện
                    var news = new List<StockNews>();
                    var ndt = SqlDb.GetCompanyNews(stock.Symbol, -1);
                    foreach (DataRow ndr in ndt.Rows)
                    {
                        news.Add(new StockNews() { DateDeploy = (DateTime)ndr["PostTime"], ID = int.Parse(ndr["Id"].ToString()), Title = ndr["Title"].ToString(), TypeID = ndr["ConfigId"].ToString() });
                    }
                    stock.StockNews = news;

                    #endregion
                    var end2 = DateTime.Now;

                    //var st = BLFACTORY.RedisClient.Get<Stock>("stock:stockid:ACB:Object");
                    string key = string.Format(RedisKey.Key, symbol);
                    //var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
                    if (redis.ContainsKey(key))
                        redis.Set<Stock>(key, stock);
                    else
                        redis.Add<Stock>(key, stock);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(symbol + " : " + ex.ToString());
                }

            }
            DateTime end = DateTime.Now;
            MessageBox.Show("DOne");


            //var set = DateTime.Now;

            //textBox3.Text = set.ToString();

            ////var st = redis.Get<Stock>(key);
            //var get = DateTime.Now;
            //textBox4.Text = get.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);

            //var pdt = bFirst ? sql.GetAllPrice(centerId) : sql.GetChangedPriceSymbols(centerId);

            DateTime start = DateTime.Now;
            var dt = SqlDb.GetAllPrice(1);
            var allkey = string.Format(RedisKey.KeyRealTimePrice, 1);
            if (redis.ContainsKey(allkey))
                redis.Set(allkey, Lib.Serialize(dt));
            else
                redis.Add(allkey, Lib.Serialize(dt));
            var testdt = Lib.Deserialize(redis.Get<string>(allkey));
            dt.Merge(SqlDb.GetAllPrice(2));
            dt.Merge(SqlDb.GetAllPrice(9));
            foreach (DataRow pdr in dt.Rows)
            {
                try
                {

                    //Thread.Sleep(1000);
                    //update stock price 
                    var symbol = pdr["Symbol"].ToString();

                    #region Get Stock Price Data

                    //var cdt = SqlDb.GetCenterId(symbol);
                    //if (cdt.Rows.Count == 0) continue;
                    var centerId = int.Parse(pdr["centerId"].ToString());
                    //var pdt = SqlDb.GetRealtimePriceData(symbol, centerId);
                    //if (pdt.Rows.Count == 0) continue;
                    //var pdr = pdt.Rows[0];
                    var bAvg = centerId != 1 && !Lib.InTradingTime(centerId);
                    var price = new StockPrice() { Symbol = symbol, LastTradeDate = (DateTime)pdr["TradeDate"], Price = double.Parse(pdr[bAvg ? "AveragePrice" : "TradingPrice"].ToString()), RefPrice = double.Parse(pdr["Ref"].ToString()), CeilingPrice = double.Parse(pdr["Ceiling"].ToString()), FloorPrice = double.Parse(pdr["Floor"].ToString()), Volume = double.Parse(pdr["TradingVol"].ToString()), Value = double.Parse(pdr["TotalTradingValue"].ToString()), HighPrice = double.Parse(pdr["TradingPriceMax"].ToString()), LowPrice = double.Parse(pdr["TradingPriceMin"].ToString()), OpenPrice = double.Parse(pdr["OpenPrice"].ToString()), ClosePrice = double.Parse(pdr["TradingPrice"].ToString()), ForeignBuyValue = double.Parse(pdr["BuyFrValue"].ToString()), ForeignBuyVolume = double.Parse(pdr["BuyFrVolume"].ToString()), ForeignSellValue = double.Parse(pdr["SellFrValue"].ToString()), ForeignSellVolume = double.Parse(pdr["SellFrVolume"].ToString()), ForeignCurrentRoom = double.Parse(pdr["RemainFrRoom"].ToString()), AskPrice01 = double.Parse(pdr["SellPrice1"].ToString()), AskPrice02 = double.Parse(pdr["SellPrice2"].ToString()), AskPrice03 = double.Parse(pdr["SellPrice3"].ToString()), AskVolume01 = double.Parse(pdr["SellVol1"].ToString()), AskVolume02 = double.Parse(pdr["SellVol2"].ToString()), AskVolume03 = double.Parse(pdr["SellVol3"].ToString()), BidPrice01 = double.Parse(pdr["BidPrice1"].ToString()), BidPrice02 = double.Parse(pdr["BidPrice2"].ToString()), BidPrice03 = double.Parse(pdr["BidPrice3"].ToString()), BidVolume01 = double.Parse(pdr["BidVol1"].ToString()), BidVolume02 = double.Parse(pdr["BidVol2"].ToString()), BidVolume03 = double.Parse(pdr["BidVol3"].ToString()) };

                    #endregion

                    var key = string.Format(RedisKey.PriceKey, symbol);

                    if (redis.ContainsKey(key))
                        redis.Set<StockPrice>(key, price);
                    else
                        redis.Add<StockPrice>(key, price);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            //var set = DateTime.Now;

            //textBox3.Text = set.ToString();

            //var st = redis.Get<StockPrice>(key);
            //var get = DateTime.Now;
            //textBox4.Text = get.ToString();
            DateTime end = DateTime.Now;
            MessageBox.Show("Done");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var symbol = "VRC";

            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var keylist = string.Format(RedisKey.PriceHistoryKeys, symbol);
            var ls = redis.ContainsKey(keylist) ? redis.Get<List<String>>(keylist) : new List<string>();

            var pdt = SqlDb.GetPriceHistory(symbol, -1);
            foreach (DataRow pdr in pdt.Rows)
            {
                var key = string.Format(RedisKey.PriceHistory, symbol, ((DateTime)pdr["TradeDate"]).ToString("yyyyMMdd"));

                var price = new StockHistory() { TradeDate = (DateTime)pdr["TradeDate"], ClosePrice = double.Parse(pdr["ClosePrice"].ToString()), AveragePrice = double.Parse(pdr["AveragePrice"].ToString()), BasicPrice = double.Parse(pdr["BasicPrice"].ToString()), Ceiling = double.Parse(pdr["Ceiling"].ToString()), Floor = double.Parse(pdr["Floor"].ToString()), Volume = double.Parse(pdr["Volume"].ToString()), TotalValue = double.Parse(pdr["TotalValue"].ToString()), AgreedValue = double.Parse(pdr["AgreedValue"].ToString()), AgreedVolume = double.Parse(pdr["AgreedVolume"].ToString()), Symbol = symbol, KLGDDot1 = double.Parse(pdr["VolumePhase1"].ToString()), KLGDDot2 = double.Parse(pdr["VolumePhase2"].ToString()), KLGDDot3 = double.Parse(pdr["VolumePhase3"].ToString()), OpenPrice = double.Parse(pdr["OpenPrice"].ToString()), HighPrice = double.Parse(pdr["HighPrice"].ToString()), LowPrice = double.Parse(pdr["LowPrice"].ToString()) };
                if (redis.ContainsKey(key))
                    redis.Set<StockHistory>(key, price);
                else
                    redis.Add<StockHistory>(key, price);

                if (!ls.Contains(key)) ls.Add(key);
            }
            ls.Sort();
            ls.Reverse();
            if (redis.ContainsKey(keylist))
                redis.Set<List<string>>(keylist, ls);
            else
                redis.Add<List<string>>(keylist, ls);

            var test = redis.Get<List<string>>(keylist);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var symbol = "VNINDEX";

            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var keylist = string.Format(RedisKey.OrderHistoryKeys, symbol);
            var ls = redis.ContainsKey(keylist) ? redis.Get<List<String>>(keylist) : new List<string>();

            var pdt = SqlDb.GetOrderHistory(symbol, -1);
            foreach (DataRow pdr in pdt.Rows)
            {
                var key = string.Format(RedisKey.OrderHistory, symbol, ((DateTime)pdr["Trading_Date"]).ToString("yyyyMMdd"));

                var order = new OrderHistory() { TradeDate = (DateTime)pdr["Trading_Date"], BuyOrderCount = double.Parse(pdr["Bid_Order"].ToString()), BuyVolume = double.Parse(pdr["Bid_Volume"].ToString()), SellOrderCount = double.Parse(pdr["Offer_Order"].ToString()), SellVolume = double.Parse(pdr["Offer_Volume"].ToString()), Volume = double.Parse(pdr["Volume"].ToString()), Price = double.Parse(pdr["Price"].ToString()), BasicPrice = double.Parse(pdr["BasicPrice"].ToString()), Ceiling = double.Parse(pdr["Ceiling"].ToString()), Floor = double.Parse(pdr["Floor"].ToString()), Symbol = symbol };
                if (redis.ContainsKey(key))
                    redis.Set<OrderHistory>(key, order);
                else
                    redis.Add<OrderHistory>(key, order);

                if (!ls.Contains(key)) ls.Add(key);
            }
            ls.Sort();
            ls.Reverse();
            if (redis.ContainsKey(keylist))
                redis.Set<List<string>>(keylist, ls);
            else
                redis.Add<List<string>>(keylist, ls);

            var test = redis.Get<List<string>>(keylist);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var symbol = "VNINDEX";

            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var keylist = string.Format(RedisKey.ForeignHistoryKeys, symbol);
            var ls = redis.ContainsKey(keylist) ? redis.Get<List<String>>(keylist) : new List<string>();

            var pdt = SqlDb.GetForeignHistory(symbol, -1);
            foreach (DataRow pdr in pdt.Rows)
            {
                var key = string.Format(RedisKey.ForeignHistory, symbol, ((DateTime)pdr["Trading_Date"]).ToString("yyyyMMdd"));

                var order = new ForeignHistory() { TradeDate = (DateTime)pdr["Trading_Date"], BuyVolume = double.Parse(pdr["Buying_Volume"].ToString()), BuyValue = double.Parse(pdr["Buying_Value"].ToString()), SellVolume = double.Parse(pdr["Selling_Volume"].ToString()), SellValue = double.Parse(pdr["Selling_Value"].ToString()), Room = double.Parse(pdr["CurrentRoom"].ToString()), TotalRoom = double.Parse(pdr["TotalRoom"].ToString()), Percent = double.Parse(pdr["SoHuu"].ToString()), Symbol = symbol };
                if (redis.ContainsKey(key))
                    redis.Set<ForeignHistory>(key, order);
                else
                    redis.Add<ForeignHistory>(key, order);

                if (!ls.Contains(key)) ls.Add(key);
            }
            ls.Sort();
            ls.Reverse();
            if (redis.ContainsKey(keylist))
                redis.Set<List<string>>(keylist, ls);
            else
                redis.Add<List<string>>(keylist, ls);

            var test = redis.Get<List<string>>(keylist);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var symbol = "PTC";

            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var keylist = string.Format(RedisKey.InternalHistoryKeys, symbol);
            //var ls = redis.ContainsKey(keylist) ? redis.Get<List<String>>(keylist) : new List<string>();
            var ls = new List<string>();

            var pdt = SqlDb.GetInternalHistory(symbol, -1);
            foreach (DataRow pdr in pdt.Rows)
            {
                var key = string.Format(RedisKey.InternalHistory, symbol, (((DateTime?)pdr["NgayThongBao"]) ?? DateTime.Now).ToString("yyyyMMdd"), pdr["ID"], pdr["ShareHolder_ID"]);

                var order = new InternalHistory() { Stock = symbol, TransactionMan = pdr["FullName"].ToString(), TransactionManPosition = pdr["ChucVu"].ToString(), RelatedMan = pdr["NguoiLienQuan"].ToString(), RelatedManPosition = pdr["ChucVuNguoiLienQuan"].ToString(), VolumeBeforeTransaction = double.Parse(pdr["SLCPTruocGD"].ToString()), PlanBuyVolume = double.Parse(pdr["DangKy_Mua"].ToString()), PlanSellVolume = double.Parse(pdr["DangKy_Ban"].ToString()), PlanBeginDate = pdr["DangKy_TuNgay"].Equals(DBNull.Value) ? null : (DateTime?)pdr["DangKy_TuNgay"], PlanEndDate = pdr["DangKy_DenNgay"].Equals(DBNull.Value) ? null : (DateTime?)pdr["DangKy_DenNgay"], RealBuyVolume = double.Parse(pdr["ThucHien_Mua"].ToString()), RealSellVolume = double.Parse(pdr["ThucHien_Ban"].ToString()), RealEndDate = pdr["ThucHien_NgayKetThuc"].Equals(DBNull.Value) ? null : (DateTime?)pdr["ThucHien_NgayKetThuc"], PublishedDate = pdr["NgayThongBao"].Equals(DBNull.Value) ? null : (DateTime?)pdr["NgayThongBao"], VolumeAfterTransaction = double.Parse(pdr["SLCPSauGD"].ToString()), TransactionNote = pdr["GhiChu"].ToString() };
                if (redis.ContainsKey(key))
                    redis.Set<InternalHistory>(key, order);
                else
                    redis.Add<InternalHistory>(key, order);

                if (!ls.Contains(key)) ls.Add(key);
            }
            ls.Sort();
            ls.Reverse();
            if (redis.ContainsKey(keylist))
                redis.Set<List<string>>(keylist, ls);
            else
                redis.Add<List<string>>(keylist, ls);

            var test = redis.Get<List<string>>(keylist);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var symbol = "SSI";

            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var keylist = string.Format(RedisKey.FundHistoryKeys, "SSI");
            var ls = redis.ContainsKey(keylist) ? redis.Get<List<String>>(keylist) : new List<string>();

            var pdt = SqlDb.GetFundHistory(symbol, -1);
            foreach (DataRow pdr in pdt.Rows)
            {
                var key = string.Format(RedisKey.FundHistory, "SSI", ((DateTime)pdr["TradingDate"]).ToString("yyyyMMdd"));

                var order = new FundHistory() { Symbol = symbol, TradeDate = (DateTime)pdr["TradingDate"], TransactionType = pdr["Buy_Sale"].ToString() == "s" ? "Bán" : "Mua", PlanVolume = double.Parse(pdr["RegisteredVol"].ToString()), TodayVolume = double.Parse(pdr["TodayTradingVol"].ToString()), AccumulateVolume = double.Parse(pdr["AccumVol"].ToString()), ExpiredDate = (DateTime)pdr["ExpireDate"] };
                if (redis.ContainsKey(key))
                    redis.Set<FundHistory>(key, order);
                else
                    redis.Add<FundHistory>(key, order);

                if (!ls.Contains(key)) ls.Add(key);
            }
            ls.Sort();
            ls.Reverse();
            if (redis.ContainsKey(keylist))
                redis.Set<List<string>>(keylist, ls);
            else
                redis.Add<List<string>>(keylist, ls);

            var test = redis.Get<List<string>>(keylist);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            var symbol = "VIC";

            var key = String.Format(RedisKey.KeyFinanceReport, symbol);

            var obj = new FinanceReport() { HtmlContent = FinanceReportData.ReturnHTML_BCTC("VIC"), Symbol = symbol };

            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            if (redis.ContainsKey(key))
                redis.Set(key, obj);
            else
                redis.Add(key, obj);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var skey = RedisKey.KeyStockList;
            var sdt = SqlDb.GetSymbolList(-1);
            var sls = new List<StockCompact>();
            var hnx = new List<StockCompact>();
            var hsx = new List<StockCompact>();
            var upc = new List<StockCompact>();
            foreach (DataRow sdr in sdt.Rows)
            {
                var stock = new StockCompact() { Symbol = sdr["Symbol"].ToString(), CategoryId = int.Parse(sdr["StockIndustryId"].ToString()), TradeCenterId = int.Parse(sdr["TradeCenterId"].ToString()) };
                sls.Add(stock);
                switch (stock.TradeCenterId)
                {
                    case 1: hsx.Add(stock); break;
                    case 2: hnx.Add(stock); break;
                    case 9: upc.Add(stock); break;
                }
            }
            if (redis.ContainsKey(skey))
                redis.Set(skey, sls);
            else
                redis.Add(skey, sls);
            skey = string.Format(RedisKey.KeyStockListByCenter, "1");
            if (redis.ContainsKey(skey))
                redis.Set(skey, hsx);
            else
                redis.Add(skey, hsx);
            skey = string.Format(RedisKey.KeyStockListByCenter, "2");
            if (redis.ContainsKey(skey))
                redis.Set(skey, hnx);
            else
                redis.Add(skey, hnx);
            skey = string.Format(RedisKey.KeyStockListByCenter, "9");
            if (redis.ContainsKey(skey))
                redis.Set(skey, upc);
            else
                redis.Add(skey, upc);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);

            var keylist = RedisKey.KeyAnalysisReport;
            var ls = new List<string>();
            var rdt = SqlDb.GetAnalysisReports("A", -1);
            foreach (DataRow rdr in rdt.Rows)
            {
                var key = string.Format(RedisKey.KeyAnalysisReportDetail, rdr["ID"]);
                var obj = new Reports() { ID = int.Parse(rdr["ID"].ToString()), Body = rdr["Des"].ToString(), DateDeploy = (DateTime)rdr["PublishDate"], file = new FileObject() { FileName = rdr["FileName"].ToString(), FileUrl = "http://images1.cafef.vn/Images/Uploaded/DuLieuDownload/PhanTichBaoCao/" + rdr["FileName"] }, IsHot = (rdr["IsHot"].ToString() == "1"), ResourceCode = rdr["Source"].ToString(), Symbol = rdr["Symbol"].ToString(), Title = rdr["title"].ToString(), SourceID = int.Parse(rdr["SourceId"].ToString()), ResourceName = rdr["SourceFullName"].ToString(), ResourceLink = rdr["SourceUrl"].ToString() };
                var cateId = 0;
                if (!string.IsNullOrEmpty(obj.Symbol) && !obj.Symbol.Contains(","))
                {
                    var stock = redis.Get<Stock>(string.Format(RedisKey.Key, obj.Symbol));
                    cateId = stock == null ? 0 : stock.CompanyProfile.basicInfos.category.ID;
                }
                obj.CategoryID = cateId;
                if (redis.ContainsKey(key))
                    redis.Set(key, obj);
                else
                    redis.Add(key, obj);
                if (!ls.Contains(key)) ls.Add(key);
            }
            if (redis.ContainsKey(keylist))
                redis.Set(keylist, ls);
            else
                redis.Add(keylist, ls);
        }

        private void button11_Click(object sender, EventArgs e)
        {


            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);

            var hnxkey = string.Format(RedisKey.KeyRealTimePrice, 2);
            var hnx = Lib.Deserialize(redis.Get<string>(hnxkey)) ?? new DataTable();
            var tmp = hnx.Clone();
            tmp.Columns["AverageChange"].DataType = typeof(double);
            tmp.Columns["PriceChange"].DataType = typeof(double);
            foreach (DataRow dr in hnx.Rows)
            {
                tmp.ImportRow(dr);
            }
            var bAvg = !Lib.InTradingTime(2);
            var hrs = tmp.Select((bAvg ? "AverageChange" : "PriceChange") + " > 0", "" + (bAvg ? "AverageChange" : "PriceChange") + " DESC, Symbol");
            var key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.PriceUp);
            var hnxpu = new List<TopStock>();
            for (var i = 0; i < 10; i++)
            {
                if (hrs.Length <= i) break;
                hnxpu.Add(new TopStock() { Symbol = hrs[i]["Symbol"].ToString(), BasicPrice = double.Parse(hrs[i]["Ref"].ToString()), Price = double.Parse(hrs[i][(bAvg ? "AveragePrice" : "TradingPrice")].ToString()), Volume = double.Parse(hrs[i]["TradingVol"].ToString()) });
            }
            if (redis.ContainsKey(key))
                redis.Set(key, hnxpu);
            else
                redis.Add(key, hnxpu);


            //var key1 = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.PriceUp);
            var a = redis.Get<List<TopStock>>(key);


            var pdt = SqlDb.GetTopStock();

            UpdateTopStock(RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.EPS, ref redis, ref pdt);
            UpdateTopStock(RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.EPS, ref redis, ref pdt);
            UpdateTopStock(RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.EPS, ref redis, ref pdt);
            UpdateTopStock(RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.MarketCap, ref redis, ref pdt);
            UpdateTopStock(RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.MarketCap, ref redis, ref pdt);
            UpdateTopStock(RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.MarketCap, ref redis, ref pdt);
            UpdateTopStock(RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.PE, ref redis, ref pdt);
            UpdateTopStock(RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.PE, ref redis, ref pdt);
            UpdateTopStock(RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.PE, ref redis, ref pdt);

            /*==============================*/

            //top stock price
            //pdt = SqlDb.GetTopStockPrice();
            //UpdateTopStock(RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.PriceDown, ref redis, ref pdt);
            //UpdateTopStock(RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.PriceDown, ref redis, ref pdt);
            //UpdateTopStock(RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.PriceDown, ref redis, ref pdt);
            //UpdateTopStock(RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.PriceUp, ref redis, ref pdt);
            //UpdateTopStock(RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.PriceUp, ref redis, ref pdt);
            //UpdateTopStock(RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.PriceUp, ref redis, ref pdt);
            //UpdateTopStock(RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.VolumeDown, ref redis, ref pdt);
            //UpdateTopStock(RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.VolumeDown, ref redis, ref pdt);
            //UpdateTopStock(RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.VolumeDown, ref redis, ref pdt);
        }

        private void UpdateTopStock(string center, string type, ref RedisClient redis, ref DataTable pdt)
        {
            var key = string.Format(RedisKey.KeyTopStock, center, type);
            var pdrs = pdt.Select("Center='" + center + "' AND SType = '" + type + "'");
            var ls = new List<TopStock>();
            foreach (var pdr in pdrs)
            {
                ls.Add(new TopStock() { Symbol = pdr["Symbol"].ToString(), Price = double.Parse(pdr["Price"].ToString()), BasicPrice = double.Parse(pdr["BasicPrice"].ToString()), EPS = double.Parse(pdr["EPS"].ToString()), MarketCap = double.Parse(pdr["MarketCap"].ToString()), PE = double.Parse(pdr["PE"].ToString()), Volume = double.Parse(pdr["Volume"].ToString()) });
            }
            if (redis.ContainsKey(key))
                redis.Set(key, ls);
            else
                redis.Add(key, ls);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var symbol = "SSI";
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var stock = redis.Get<Stock>(string.Format(RedisKey.Key, symbol));
            var history = stock.StockPriceHistory;

            var foreign = new List<ForeignCompactHistory>();
            var fdt = SqlDb.GetForeignHistory(stock.Symbol, 10);
            foreach (DataRow fdr in fdt.Rows)
            {
                foreign.Add(new ForeignCompactHistory() { BuyPercent = double.Parse(fdr["FBuyPercent"].ToString()), SellPercent = double.Parse(fdr["FSellPercent"].ToString()), NetVolume = double.Parse(fdr["FNetVolume"].ToString()), NetValue = double.Parse(fdr["FNetValue"].ToString()), TradeDate = (DateTime)fdr["Trading_Date"] });
            }
            history.Foreign = foreign;

            stock.StockPriceHistory = history;



            var dt = SqlDb.GetSymbolData(symbol);
            if (dt.Rows.Count <= 0) return;

            var row = dt.Rows[0];
            //profile - commonInfo
            var commonInfo = new CommonInfo() { Symbol = stock.Symbol, Capital = double.Parse(row["VonDieuLe"].ToString()), Category = row["CategoryName"].ToString(), Content = row["About"].ToString(), OutstandingVolume = double.Parse(row["TotalShare"].ToString()), TotalVolume = double.Parse(row["SLCPNY"].ToString()) };
            commonInfo.Content += "<p><b>Địa chỉ:</b> " + row["Address"].ToString() + "</p>";
            commonInfo.Content += "<p><b>Điện thoại:</b> " + row["Phone"].ToString() + "</p>";
            commonInfo.Content += "<p><b>Người phát ngôn:</b> " + row["Spokenman"].ToString() + "</p>";
            if (!string.IsNullOrEmpty(row["Email"].ToString())) commonInfo.Content += "<p><b>Email:</b> <a href='mailto:" + row["Email"] + "'>" + row["Email"] + "</a></p>";
            if (!string.IsNullOrEmpty(row["Website"].ToString())) commonInfo.Content += "<p><b>Website:</b> <a href='" + row["Website"] + "'>" + row["Website"] + "</a></p>";

            stock.CompanyProfile.commonInfos = commonInfo;

            redis.Set(string.Format(RedisKey.Key, symbol), stock);
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            string date = "";
            #region FN
            try
            {
                var keylist = RedisKey.KeyCompanyNews;
                var ls = (redis.ContainsKey(keylist)) ? redis.Get<List<string>>(keylist) : new List<string>();
                var pdt = (date == "" || ls.Count == 0) ? SqlDb.GetCompanyNews("A", 1000) : SqlDb.GetCompanyNews("A", date);

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
                    var key = string.Format(RedisKey.KeyCompanyNewsDetail, rdr["ID"]);
                    var obj = new StockNews() { ID = int.Parse(rdr["ID"].ToString()), Body = rdr["Content"].ToString(), DateDeploy = (DateTime)rdr["PostTime"], Image = rdr["ImagePath"].ToString(), Title = rdr["title"].ToString(), Sapo = rdr["SubContent"].ToString(), TypeID = rdr["ConfigId"].ToString(), Symbol = rdr["StockSymbols"].ToString() };
                    if (redis.ContainsKey(key))
                        redis.Set(key, obj);
                    else
                        redis.Add(key, obj);
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
                    if (obj.TypeID.Contains("1"))
                    {
                        if (!cate3.Contains(key)) cate3.Add(key);
                    }
                    else
                    {
                        if (cate3.Contains(key)) cate3.Remove(key);
                    }
                    if (obj.TypeID.Contains("1"))
                    {
                        if (!cate4.Contains(key)) cate4.Add(key);
                    }
                    else
                    {
                        if (cate4.Contains(key)) cate4.Remove(key);
                    }
                    if (obj.TypeID.Contains("1"))
                    {
                        if (!cate5.Contains(key)) cate5.Add(key);
                    }
                    else
                    {
                        if (cate5.Contains(key)) cate5.Remove(key);
                    }
                    #endregion
                }
                if (redis.ContainsKey(keylist))
                    redis.Set(keylist, ls);
                else
                    redis.Add(keylist, ls);
                #region Update category list
                if (redis.ContainsKey(key1))
                    redis.Set(key1, cate1);
                else
                    redis.Add(key1, cate1);
                if (redis.ContainsKey(key2))
                    redis.Set(key2, cate2);
                else
                    redis.Add(key2, cate2);
                if (redis.ContainsKey(key3))
                    redis.Set(key3, cate3);
                else
                    redis.Add(key3, cate3);
                if (redis.ContainsKey(key4))
                    redis.Set(key4, cate4);
                else
                    redis.Add(key4, cate4);
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
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //Việt Nam
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var key = string.Format(RedisKey.KeyProductBox, 1);
            var data = new List<ProductBox>();
            data.Add(new ProductBox() { ProductName = "Vàng SJC", CurrentPrice = 37.330, OtherPrice = 37.420, PrevPrice = 36.95 });
            data.Add(new ProductBox() { ProductName = "USD (VCB)", CurrentPrice = 20785, OtherPrice = 20885, PrevPrice = 20885 });
            data.Add(new ProductBox() { ProductName = "USD (tự do)", CurrentPrice = 20785, OtherPrice = 20885, PrevPrice = 20885 });
            data.Add(new ProductBox() { ProductName = "EUR (VCB)", CurrentPrice = 28494.07, OtherPrice = 29140.67, PrevPrice = 20885 });
            data.Add(new ProductBox() { ProductName = "EUR (tự do)", CurrentPrice = 28494.07, OtherPrice = 29140.67, PrevPrice = 20885 });
            data.Add(new ProductBox() { ProductName = "CNY", CurrentPrice = 3290, OtherPrice = 3310, PrevPrice = 3310 });
            data.Add(new ProductBox() { ProductName = "USD Singapo", CurrentPrice = 16610, OtherPrice = 16839, PrevPrice = 16739 });
            data.Add(new ProductBox() { ProductName = "USD Singapo", CurrentPrice = 16610, OtherPrice = 16839, PrevPrice = 16739 });
            data.Add(new ProductBox() { ProductName = "USD Singapo", CurrentPrice = 16610, OtherPrice = 16839, PrevPrice = 16739 });
            if (redis.ContainsKey(key))
                redis.Set(key, data);
            else
                redis.Add(key, data);
        }

    }
}
