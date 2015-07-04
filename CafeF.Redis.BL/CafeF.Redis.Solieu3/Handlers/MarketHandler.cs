using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using CafeF.Redis.BL;
using CafeF.Redis.Entity;
using CafeF_EmbedData.Common;

namespace CafeF_EmbedData.Handlers
{
    public class MarketHandler : CafeF_EmbedData.Common.BaseHandler, IRequiresSessionState
    {
        public const string PortSolieu = "22";

        public MarketHandler(HttpContext context, string method, int cacheExpiration, string[] parameters)
            : base(context, method, cacheExpiration, parameters)
        {
        }

        #region Public methods

        public void GetStockSymbolBySymbolList()
        {
            try
            {
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

                string cacheName = "Cache_GetStockSymbolBySymbolList_" + CurrentContext.Request.QueryString["sym"];

                //if (this.CacheExists(cacheName))
                //{
                //    this.UpdateOutputInCache(cacheName);
                //}
                //else
                //{
                string output = "";
                try
                {
                    //hungnd
                    string[] fields = new string[Parameters.Length + 2];
                    for (int i = 0; i < Parameters.Length; i++)
                    {
                        fields[i] = Parameters[i];
                    }
                    fields[Parameters.Length] = "ceiling";
                    fields[Parameters.Length + 1] = "floor";
                    //
                    output = jsSerializer.Serialize(StockSymbol_DynamicData.GetSymbolsBySymbolArray(this.CurrentContext.Request.QueryString["sym"], fields));
                }
                catch (Exception ex)
                {
                    Lib.WriteLog(ex);
                }
                //if (this.CacheExpiration > 0)
                //{
                //    this.UpdateCache(cacheName, this.CacheExpiration, output);
                //}
                this.UpdateOutput(output);
                //}
            }
            catch (Exception ex) { Lib.WriteError("GetStockSymbolBySymbolList: " + ex.Message); }
        }

        #endregion

        #region StockSymbols entity

        private class StockSymbol
        {
            public struct SymbolItem
            {
                public SymbolItem(string symbol, double volume, double price, double change, double pchange)
                {
                    this.Symbol = symbol;
                    this.Volume = volume;
                    this.Price = price;
                    this.Change = change;
                    this.PChange = pchange;
                }
                public string Symbol;
                public double Volume, Price, Change, PChange;
            }


            private SymbolItem[] m_Symbols;

            public SymbolItem[] Symbols
            {
                get
                {
                    return this.m_Symbols;
                }
                set
                {
                    this.m_Symbols = value;
                }
            }
        }

        private class StockSymbol_DynamicData
        {
            public enum TopSymbolBy
            {
                TopUp = 0,
                TopDown = 1,
                TopQuantity = 2
            }
            public struct SymbolItem
            {
                public SymbolItem(string symbol, params object[] datas)
                {
                    this.Symbol = symbol;
                    Datas = datas;
                }
                public string Symbol;
                public object[] Datas;
            }

            public static StockSymbol_DynamicData GetSymbolsBySymbolArray(string symbolArray, params string[] fields)
            {
                StockSymbol_DynamicData currentData = new StockSymbol_DynamicData();
                try
                {
                    List<string> symbols = new List<string>();

                    if (!string.IsNullOrEmpty(symbolArray))
                    {
                        if (symbolArray.StartsWith(";")) symbolArray = symbolArray.Substring(1);
                        if (symbolArray.EndsWith(";")) symbolArray = symbolArray.Substring(0, symbolArray.Length - 1);

                        string[] symbolList = symbolArray.Split(';');

                        foreach (string symbol in symbolList)
                        {
                            if (!symbols.Contains(symbol.ToUpper())) symbols.Add(symbol.ToUpper());
                        }

                    }
                    var hsx = TradeCenterBL.getByTradeCenter((int)TradeCenter.HoSE);
                    var hnx = TradeCenterBL.getByTradeCenter((int)TradeCenter.HaSTC);
                    List<SymbolItem> items = new List<SymbolItem>();
                    DataTable tblSymbol;
                    if (symbols.Count > 0)
                    {
                        var ss = new List<string>();
                        foreach (string code in symbols)
                        {
                            if (!ss.Contains(code.ToUpper())) ss.Add(code.ToUpper());
                        }
                        var ts = PriceRedisBL.GetStockPriceMultiple(ss);
                        object[] datas;
                        foreach (string code in symbols)
                        {
                            //tblSymbol = GetSymbolDataByCache(code, TradeCenter.HaSTC);
                            //if (tblSymbol == null || tblSymbol.Rows.Count < 1)
                            //    tblSymbol = GetSymbolDataByCache(code, TradeCenter.HoSE);
                            //if (tblSymbol == null || tblSymbol.Rows.Count < 1)
                            //    tblSymbol = GetSymbolDataByCache(code, TradeCenter.UpCom);


                            //datas = new object[fields.Length];
                            ////currentPrice','chgIndex','pctIndex
                            ////this.Fields = {'Price':0,'Change':1,'ChangePercent':2,'ceiling':3,'floor':4};
                            //if (tblSymbol != null && tblSymbol.Rows.Count > 0)
                            //{
                            //    if (Lib.Object2Double(tblSymbol.Rows[0][1]) > 0)
                            //        datas[0] = tblSymbol.Rows[0][1];//currentprice
                            //    else
                            //        datas[0] = tblSymbol.Rows[0][10];//currentprice
                            //    datas[1] = tblSymbol.Rows[0][2];//chgIndex
                            //    datas[2] = tblSymbol.Rows[0][6];//pctIndex
                            //    try
                            //    {
                            //        datas[3] = tblSymbol.Rows[0][17];//ceiling
                            //        datas[4] = tblSymbol.Rows[0][18];//Floor
                            //    }
                            //    catch { }
                            //    items.Add(new SymbolItem(code, datas));
                            //}
                            //else
                            //{
                            //    datas[0] = "0";
                            //    datas[1] = "0";
                            //    datas[2] = "0";
                            //    datas[3] = "0";
                            //    datas[4] = "0";
                            //    items.Add(new SymbolItem(code, datas));
                            //}
                            datas = new object[5];
                            if (code.ToUpper() == "HSXD")
                            {
                                datas[0] = hsx.CurrentVolume.ToString("#,##0");
                                datas[1] = hsx.ForeignNetVolume.ToString("#,##0");
                                datas[2] = hsx.Ceiling + "|" + hsx.Up + "|" + hsx.Normal + "|" + hsx.Down + "|" + hsx.Floor;
                                datas[3] = 0;
                                datas[4] = 0;
                                items.Add(new SymbolItem(code, datas));
                                continue;
                            }
                            if (code.ToUpper() == "HNXD")
                            {
                                datas[0] = hnx.CurrentVolume.ToString("#,##0");
                                datas[1] = hnx.ForeignNetVolume.ToString("#,##0");
                                datas[2] = hnx.Ceiling + "|" + hnx.Up + "|" + hnx.Normal + "|" + hnx.Down + "|" + hnx.Floor;
                                datas[3] = 0;
                                datas[4] = 0;
                                items.Add(new SymbolItem(code, datas));
                                continue;
                            }

                            StockPrice price = null;
                            bool updated = false;
                            try
                            {
                                price = ts[code];
                                datas[0] = price.Price;
                                datas[1] = price.Price - price.RefPrice;
                                datas[2] = price.RefPrice <= 0 ? 0 : (price.Price - price.RefPrice) / price.RefPrice * 100;
                                datas[3] = price.CeilingPrice;
                                datas[4] = price.FloorPrice;
                                updated = true;
                            }
                            catch (Exception)
                            {
                                updated = false;
                            }
                            if (!updated)
                            {
                                datas[0] = "0";
                                datas[1] = "0";
                                datas[2] = "0";
                                datas[3] = "0";
                                datas[4] = "0";
                            }
                            items.Add(new SymbolItem(code, datas));
                        }
                    }
                    #region Header index

                    var san = new object[5];
                    san[0] = hsx.CurrentIndex.ToString("#,##0.00");
                    san[1] = (hsx.CurrentIndex > hsx.PrevIndex ? "+" : "") + (hsx.CurrentIndex - hsx.PrevIndex).ToString("#,##0.00");
                    san[2] = (hsx.CurrentIndex > hsx.PrevIndex ? "+" : "") + (hsx.PrevIndex <= 0 ? 0 : (hsx.CurrentIndex - hsx.PrevIndex) / hsx.PrevIndex * 100).ToString("#,##0.00");
                    san[3] = hsx.CurrentValue.ToString("#,##0.0");
                    san[4] = (Utils.InTradingTime(1) ? hsx.CurrentDate : Utils.GetCloseTime(1)).ToString("HH:mm");
                    items.Add(new SymbolItem("HSX", san));

                    san = new object[5];
                    san[0] = hnx.CurrentIndex.ToString("#,##0.00");
                    san[1] = (hnx.CurrentIndex > hnx.PrevIndex ? "+" : "") + (hnx.CurrentIndex - hnx.PrevIndex).ToString("#,##0.00");
                    san[2] = (hnx.CurrentIndex > hnx.PrevIndex ? "+" : "") + (hnx.PrevIndex <= 0 ? 0 : (hnx.CurrentIndex - hnx.PrevIndex) / hnx.PrevIndex * 100).ToString("#,##0.00");
                    san[3] = hnx.CurrentValue.ToString("#,##0.0");
                    san[4] = (Utils.InTradingTime(2) ? hnx.CurrentDate : Utils.GetCloseTime(2)).ToString("HH:mm");
                    items.Add(new SymbolItem("HNX", san));
                    #endregion
                    currentData.Symbols = items.ToArray();
                }
                catch (Exception ex) { Lib.WriteError(fields.Length + ":GetSymbolsBySymbolArray: " + symbolArray + " : " + ex.Message); }
                return currentData;
            }

            private static bool IsSymbolSelected(string symbol, ref List<string> selectedSymbols)
            {
                for (int i = 0; i < selectedSymbols.Count; i++)
                {
                    if (symbol == selectedSymbols[i])
                    {
                        selectedSymbols.RemoveAt(i);
                        return true;
                    }
                }
                return false;
            }
            //private static DataTable GetSymbolDataByCache(string Symbol,TradeCenter tc )
            //{
            //    DataTable dtStockList = null;
            //    switch (tc)
            //    { 
            //        case TradeCenter.HaSTC:
            //            if (CacheController.IsCacheExists(PortSolieu, string.Format("MemCached_HaSTC_PriceData_Symbol{0}", Symbol)))
            //                dtStockList = CacheController.Get<DataTable>(PortSolieu, string.Format("MemCached_HaSTC_PriceData_Symbol{0}", Symbol));
            //            break;
            //        case TradeCenter.HoSE:
            //            if (CacheController.IsCacheExists(PortSolieu, string.Format("MemCached_HoSE_PriceData_Symbol{0}", Symbol)))
            //                dtStockList = CacheController.Get<DataTable>(PortSolieu, string.Format("MemCached_HoSE_PriceData_Symbol{0}", Symbol));
            //            break;
            //        case TradeCenter.UpCom:
            //            if (CacheController.IsCacheExists(PortSolieu, string.Format("MemCached_UpCom_PriceData_Symbol{0}", Symbol)))
            //                dtStockList = CacheController.Get<DataTable>(PortSolieu, string.Format("MemCached_UpCom_PriceData_Symbol{0}", Symbol));
            //            break;
            //    }

            //    return dtStockList;
            //}

            private SymbolItem[] m_Symbols;

            public SymbolItem[] Symbols
            {
                get
                {
                    return this.m_Symbols;
                }
                set
                {
                    this.m_Symbols = value;
                }
            }
        }
        #endregion


        public enum TradeCenter
        {
            AllTradeCenter = 0,
            HoSE = 1,
            HaSTC = 2,
            UpCom = 9
        }


    }
}
