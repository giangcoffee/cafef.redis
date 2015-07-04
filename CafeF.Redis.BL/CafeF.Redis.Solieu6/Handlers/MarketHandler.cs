using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
//using CafeF.BO;
using CafeF.Redis.BL;
using CafeF.Redis.Entity;
using CafeF_EmbedData.Common;
using MemcachedProviders.Cache;
using CafeF.Redis.Data;

namespace CafeF_EmbedData.Handlers
{
    public class MarketHandler : CafeF_EmbedData.Common.BaseHandler, IRequiresSessionState
    {
        public MarketHandler(HttpContext context, string method, int cacheExpiration, string[] parameters)
            : base(context, method, cacheExpiration, parameters)
        {
        }

        #region Public methods

        public void GetStockSymbolBySymbolList()
        {
            //if (CurrentContext.Request.QueryString["log"] == "1")
            //{
            //    UpdateLog("thanh_ngang_theo_doi_chung_khoan");
            //}
            //else
            //{
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            //string[] fields = Parameters;

            //string cacheName = "Cache_GetStockSymbolBySymbolList_" + CurrentContext.Request.QueryString["sym"];

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
            //}
        }

        public void GetTopSymbol()
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            TradeCenter tradeCenter = TradeCenter.AllTradeCenter;
            if (this.CurrentContext.Request.QueryString["TradeCenter"] == Lib.Object2Integer(TradeCenter.HaSTC).ToString())
            {
                tradeCenter = TradeCenter.HaSTC;
            }
            else if (this.CurrentContext.Request.QueryString["TradeCenter"] == Lib.Object2Integer(TradeCenter.HoSE).ToString())
            {
                tradeCenter = TradeCenter.HoSE;
            }

            string[] fields = this.Parameters;

            //string cacheName = "Cache_GetTopSymbol_" + this.CurrentContext.Request.QueryString["TradeCenter"];

            //if (this.CacheExists(cacheName))
            //{
            //    this.UpdateOutputInCache(cacheName);
            //}
            //else
            //{
                StringBuilder jsonString = new StringBuilder();
                jsonString.Append("{");
                if (tradeCenter == TradeCenter.HaSTC)
                {
                    jsonString.Append("TopUp:" + jsSerializer.Serialize(StockSymbol_DynamicData.GetTop10Symbols(StockSymbol_DynamicData.TopSymbolBy.TopUp, TradeCenter.HaSTC, fields)) + ",");
                    jsonString.Append("TopDown:" + jsSerializer.Serialize(StockSymbol_DynamicData.GetTop10Symbols(StockSymbol_DynamicData.TopSymbolBy.TopDown, TradeCenter.HaSTC, fields)) + ",");
                    jsonString.Append("TopQuantity:" + jsSerializer.Serialize(StockSymbol_DynamicData.GetTop10Symbols(StockSymbol_DynamicData.TopSymbolBy.TopQuantity, TradeCenter.HaSTC, fields)));
                }
                else if (tradeCenter == TradeCenter.HoSE)
                {
                    jsonString.Append("TopUp:" + jsSerializer.Serialize(StockSymbol_DynamicData.GetTop10Symbols(StockSymbol_DynamicData.TopSymbolBy.TopUp, TradeCenter.HoSE, fields)) + ",");
                    jsonString.Append("TopDown:" + jsSerializer.Serialize(StockSymbol_DynamicData.GetTop10Symbols(StockSymbol_DynamicData.TopSymbolBy.TopDown, TradeCenter.HoSE, fields)) + ",");
                    jsonString.Append("TopQuantity:" + jsSerializer.Serialize(StockSymbol_DynamicData.GetTop10Symbols(StockSymbol_DynamicData.TopSymbolBy.TopQuantity, TradeCenter.HoSE, fields)));
                }
                else
                {
                    jsonString.Append("TopUp:" + jsSerializer.Serialize(StockSymbol_DynamicData.GetTop10Symbols(StockSymbol_DynamicData.TopSymbolBy.TopUp, TradeCenter.AllTradeCenter, fields)) + ",");
                    jsonString.Append("TopDown:" + jsSerializer.Serialize(StockSymbol_DynamicData.GetTop10Symbols(StockSymbol_DynamicData.TopSymbolBy.TopDown, TradeCenter.AllTradeCenter, fields)) + ",");
                    jsonString.Append("TopQuantity:" + jsSerializer.Serialize(StockSymbol_DynamicData.GetTop10Symbols(StockSymbol_DynamicData.TopSymbolBy.TopQuantity, TradeCenter.AllTradeCenter, fields)));
                }
                jsonString.Append("}");

                string output = jsonString.ToString();

                //if (this.CacheExpiration > 0)
                //{
                //    this.UpdateCache(cacheName, this.CacheExpiration, output);
                //}
                this.UpdateOutput(output);
            //}
        }
        //public void GetAllStockSymbol()
        //{
        //    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

        //    string[] fields = this.Parameters;

        //    TradeCenter tradeCenter = TradeCenter.HaSTC;
        //    if (this.CurrentContext.Request["TradeCenter"] == "HoSE")
        //    {
        //        tradeCenter = TradeCenter.HoSE;
        //    }

        //    string cacheName = "Cache_GetAllStockSymbol_" + this.CurrentContext.Request["TradeCenter"];

        //    if (this.CacheExists(cacheName))
        //    {
        //        this.UpdateOutputInCache(cacheName);
        //    }
        //    else
        //    {
        //        string output = jsSerializer.Serialize(StockSymbol_DynamicData.GetSymbolsByTradeCenter(tradeCenter, fields));
        //        if (this.CacheExpiration > 0)
        //        {
        //            this.UpdateCache(cacheName, CacheExpiration, output);
        //        }
        //        this.UpdateOutput(output);
        //    }
        //}
        public void GetTopFinanceStatementData()
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            string[] fields = this.Parameters;

            TradeCenter tradeCenter = TradeCenter.AllTradeCenter;
            if (this.CurrentContext.Request.QueryString["TradeCenter"] == Lib.Object2Integer(TradeCenter.HaSTC).ToString())
            {
                tradeCenter = TradeCenter.HaSTC;
            }
            else if (this.CurrentContext.Request.QueryString["TradeCenter"] == Lib.Object2Integer(TradeCenter.HoSE).ToString())
            {
                tradeCenter = TradeCenter.HoSE;
            }

            //string cacheName = "Cache_GetTopFinanceStatementData_" + this.CurrentContext.Request.QueryString["TradeCenter"];

            //if (this.CacheExists(cacheName))
            //{
            //    this.UpdateOutputInCache(cacheName);
            //}
            //else
            //{
                StringBuilder jsonString = new StringBuilder();
                jsonString.Append("{");
                if (tradeCenter == TradeCenter.HaSTC)
                {
                    jsonString.Append("TopPE:" + jsSerializer.Serialize(FinanceStatement.GetTopFinanceStatement(FinanceStatement.TopFinanceStatementBy.TopPE, TradeCenter.HaSTC)) + ",");
                    jsonString.Append("TopEPS:" + jsSerializer.Serialize(FinanceStatement.GetTopFinanceStatement(FinanceStatement.TopFinanceStatementBy.TopEPS, TradeCenter.HaSTC)) + ",");
                    jsonString.Append("TopCapital:" + jsSerializer.Serialize(FinanceStatement.GetTopFinanceStatement(FinanceStatement.TopFinanceStatementBy.TopCapital, TradeCenter.HaSTC)));
                }
                else if (tradeCenter == TradeCenter.HoSE)
                {
                    jsonString.Append("TopPE:" + jsSerializer.Serialize(FinanceStatement.GetTopFinanceStatement(FinanceStatement.TopFinanceStatementBy.TopPE, TradeCenter.HoSE)) + ",");
                    jsonString.Append("TopEPS:" + jsSerializer.Serialize(FinanceStatement.GetTopFinanceStatement(FinanceStatement.TopFinanceStatementBy.TopEPS, TradeCenter.HoSE)) + ",");
                    jsonString.Append("TopCapital:" + jsSerializer.Serialize(FinanceStatement.GetTopFinanceStatement(FinanceStatement.TopFinanceStatementBy.TopCapital, TradeCenter.HoSE)));
                }
                else
                {
                    jsonString.Append("TopPE:" + jsSerializer.Serialize(FinanceStatement.GetTopFinanceStatement(FinanceStatement.TopFinanceStatementBy.TopPE, TradeCenter.AllTradeCenter)) + ",");
                    jsonString.Append("TopEPS:" + jsSerializer.Serialize(FinanceStatement.GetTopFinanceStatement(FinanceStatement.TopFinanceStatementBy.TopEPS, TradeCenter.AllTradeCenter)) + ",");
                    jsonString.Append("TopCapital:" + jsSerializer.Serialize(FinanceStatement.GetTopFinanceStatement(FinanceStatement.TopFinanceStatementBy.TopCapital, TradeCenter.AllTradeCenter)));
                }
                jsonString.Append("}");

                string output = jsonString.ToString();
                //if (this.CacheExpiration > 0)
                //{
                //    this.UpdateCache(cacheName, CacheExpiration, output);
                //}
                this.UpdateOutput(output);
            //}
        }

        //public void GetRelatedCompany()
        //{
        //    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

        //    string type = this.Parameters[0];
        //    string symbol = this.CurrentContext.Request.QueryString["symbol"];
        //    int pageIndex = Lib.Object2Integer(this.CurrentContext.Request.QueryString["PageIndex"]);
        //    int pageSize = Lib.Object2Integer(this.CurrentContext.Request.QueryString["PageSize"]);

        //    string cacheName = "Cache_GetRelatedCompany_" + type + "_" + symbol + "_" + pageIndex.ToString() + "_" + pageSize.ToString();

        //    if (this.CacheExists(cacheName))
        //    {
        //        this.UpdateOutputInCache(cacheName);
        //    }
        //    else
        //    {
        //        string output = "";

        //        if (type == "EquivalentEPS")
        //        {
        //            output = jsSerializer.Serialize(FinanceStatement.GetRelatedCompany(symbol, FinanceStatement.RelatedCompanyType.EquivalentEPS, pageIndex, pageSize));
        //        }
        //        else if (type == "EquivalentPE")
        //        {
        //            output = jsSerializer.Serialize(FinanceStatement.GetRelatedCompany(symbol, FinanceStatement.RelatedCompanyType.EquivalentPE, pageIndex, pageSize));
        //        }
        //        else
        //        {
        //            output = jsSerializer.Serialize(FinanceStatement.GetRelatedCompany(symbol, FinanceStatement.RelatedCompanyType.SameIndustry, pageIndex, pageSize));
        //        }

        //        if (this.CacheExpiration > 0)
        //        {
        //            this.UpdateCache(cacheName, CacheExpiration, output);
        //        }
        //        this.UpdateOutput(output);
        //    }


        //}

        public void GetCompanyInfo()
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            if (this.CurrentContext.Request.QueryString["ViewAll"] == "1")
            {
                int tradeId = Lib.Object2Integer(this.CurrentContext.Request.QueryString["TradeId"]);
                if (tradeId == 0) tradeId = -1;

                //string cacheName = "Cache_GetCompanyInfo_" + tradeId.ToString();

                //if (this.CacheExists(cacheName))
                //{
                //    this.UpdateOutputInCache(cacheName);
                //}
                //else
                //{
                    string output = "(" + jsSerializer.Serialize(CompanyInfo.GetAllCompanyByTradeId(tradeId)) + ")";
                    //if (this.CacheExpiration > 0)
                    //{
                    //    this.UpdateCache(cacheName, this.CacheExpiration, output);
                    //}
                    this.UpdateOutput(output);
                //}
            }
            else
            {
                int tradeId = Lib.Object2Integer(this.CurrentContext.Request.QueryString["TradeId"]);
                if (tradeId == 0) tradeId = -1;
                int industryId = Lib.Object2Integer(this.CurrentContext.Request.QueryString["IndustryId"]);
                if (industryId == 0) industryId = -1;
                CompanyInfo.FilterType filterType = (this.CurrentContext.Request.QueryString["Type"] == "1" ? CompanyInfo.FilterType.StockSymbol : CompanyInfo.FilterType.NoFilter);
                string keyword = this.CurrentContext.Request.QueryString["Keyword"];
                int pageIndex = Lib.Object2Integer(this.CurrentContext.Request.QueryString["PageIndex"]);
                int pageSize = Lib.Object2Integer(this.CurrentContext.Request.QueryString["PageSize"]);

                //string cacheName = "Cache_GetCompanyInfo_" + tradeId.ToString() + "_" + industryId.ToString() + "_" + filterType.ToString() + "_" + keyword + "_" + pageIndex.ToString() + "_" + pageSize.ToString();

                //if (this.CacheExists(cacheName))
                //{
                //    this.UpdateOutputInCache(cacheName);
                //}
                //else
                //{
                    string output = jsSerializer.Serialize(CompanyInfo.SearchCompany(tradeId, industryId, filterType, keyword, pageIndex, pageSize));
                    //if (this.CacheExpiration > 0)
                    //{
                    //    this.UpdateCache(cacheName, this.CacheExpiration, output);
                    //}
                    this.UpdateOutput(output);
                //}
            }
        }

        //public void GetStockMarketSummary()
        //{
        //    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

        //    string cacheName = "Cache_Cache_GetStockMarketSummary";

        //    if (this.CacheExists(cacheName))
        //    {
        //        this.UpdateOutputInCache(cacheName);
        //        //Lib.WriteLog("UpdateOutputInCache " + cacheName + "|" + this.CacheExpiration.ToString());
        //    }
        //    else
        //    {
        //        string output = jsSerializer.Serialize(StockMarket.GetCurrentData());
        //        if (this.CacheExpiration > 0)
        //        {
        //            this.UpdateCache(cacheName, this.CacheExpiration, output);
        //        }
        //        this.UpdateOutput(output);
        //        //Lib.WriteLog("UpdateOutput " + cacheName + "|" + this.CacheExpiration.ToString());
        //    }
        //}

        //public void GetPriceBoardData()
        //{
        //    StockSymbol_FullDynamicData.TradeCenter tradeCenter = (this.CurrentContext.Request.Params["tc"] == "1" ? StockSymbol_FullDynamicData.TradeCenter.HoSE : StockSymbol_FullDynamicData.TradeCenter.HaSTC);
        //    string symbols = (string.IsNullOrEmpty(this.CurrentContext.Request.Params["ss"]) ? "" : this.CurrentContext.Request.Params["ss"].ToString().ToUpper());
        //    int full = 0;
        //    try
        //    {
        //        full = Convert.ToInt32(this.CurrentContext.Request.Params["full"]);
        //    }
        //    catch
        //    {
        //        full = 0;
        //    }

        //    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

        //    string cacheName = "Cache_GetPriceBoardData_" + tradeCenter.ToString() + "_" + symbols + "_" + full.ToString();

        //    if (this.CacheExists(cacheName))
        //    {
        //        this.UpdateOutputInCache(cacheName);
        //    }
        //    else
        //    {
        //        string output = jsSerializer.Serialize(StockSymbol_FullDynamicData.GetCurrentData(tradeCenter, symbols, full));
        //        if (this.CacheExpiration > 0)
        //        {
        //            this.UpdateCache(cacheName, this.CacheExpiration, output);
        //        }
        //        this.UpdateOutput(output);
        //    }
        //}

        //public void GetStockSymbols()
        //{
        //    string symbols = "";
        //    int tradeCenter = (this.CurrentContext.Request.QueryString["TradeCenter"] == "1" ? 1 : 2);

        //    string cacheName = "Cache_GetStockSymbols" + tradeCenter.ToString();

        //    if (this.CacheExists(cacheName))
        //    {
        //        this.UpdateOutputInCache(cacheName);
        //    }
        //    else
        //    {
        //        using (DataTable dtSymbols = Lib.ExecuteDataTable(ConfigurationManager.ConnectionStrings["FinanceChannelConnectionString"].ConnectionString, "FC_tblStock_ListStockByTradeCenter", CommandType.StoredProcedure, new SqlParameter("@TradeCenterID", tradeCenter)))
        //        {
        //            for (int i = 0; i < dtSymbols.Rows.Count; i++)
        //            {
        //                symbols += ";" + dtSymbols.Rows[i]["Symbol"].ToString();
        //            }
        //            if (symbols != "")
        //            {
        //                symbols = symbols.Substring(1);
        //            }
        //        }

        //        string output = "{\"Symbols\":\"" + symbols + "\"}";
        //        if (this.CacheExpiration > 0)
        //        {
        //            this.UpdateCache(cacheName, this.CacheExpiration, output);
        //        }
        //        this.UpdateOutput(output);
        //    }
        //}

        //public void GetAllStockSymbolsAndIndex()
        //{
        //    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

        //    string cacheName = "Cache_GetAllStockSymbolsAndIndex";

        //    if (this.CacheExists(cacheName))
        //    {
        //        this.UpdateOutputInCache(cacheName);
        //        //Lib.WriteLog("UpdateOutputInCache " + cacheName + "|" + this.CacheExpiration.ToString());
        //    }
        //    else
        //    {
        //        string output = jsSerializer.Serialize(StockSymbol_DynamicData.GetAllSymbols_IncludeIndexValue());
        //        if (this.CacheExpiration > 0)
        //        {
        //            this.UpdateCache(cacheName, this.CacheExpiration, output);
        //        }
        //        this.UpdateOutput(output);
        //        //Lib.WriteLog("UpdateOutput " + cacheName + "|" + this.CacheExpiration.ToString());
        //    }
        //}

        //public void GetTopSymbolForCMS()
        //{
        //    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

        //    StockSymbol_DynamicData.TopSymbolBy type = StockSymbol_DynamicData.TopSymbolBy.TopUp;
        //    if (this.CurrentContext.Request.QueryString["Type"] == Lib.Object2Integer(StockSymbol_DynamicData.TopSymbolBy.TopDown).ToString())
        //    {
        //        type = StockSymbol_DynamicData.TopSymbolBy.TopDown;
        //    }
        //    else if (this.CurrentContext.Request.QueryString["Type"] == Lib.Object2Integer(StockSymbol_DynamicData.TopSymbolBy.TopQuantity).ToString())
        //    {
        //        type = StockSymbol_DynamicData.TopSymbolBy.TopQuantity;
        //    }

        //    TradeCenter tradeCenter = TradeCenter.AllTradeCenter;
        //    if (this.CurrentContext.Request.QueryString["TradeCenter"] == Lib.Object2Integer(TradeCenter.HaSTC).ToString())
        //    {
        //        tradeCenter = TradeCenter.HaSTC;
        //    }
        //    else if (this.CurrentContext.Request.QueryString["TradeCenter"] == Lib.Object2Integer(TradeCenter.HoSE).ToString())
        //    {
        //        tradeCenter = TradeCenter.HoSE;
        //    }

        //    int topSymbol = Lib.Object2Integer(this.CurrentContext.Request.QueryString["TopSymbol"]);
        //    int topSession = Lib.Object2Integer(this.CurrentContext.Request.QueryString["TopSession"]);

        //    string[] fields = this.Parameters;

        //    string cacheName = "Cache_GetTopSymbolForCMS_" + this.CurrentContext.Request.QueryString["Type"] + "_" + this.CurrentContext.Request.QueryString["TradeCenter"] + "_" + this.CurrentContext.Request.QueryString["TopSymbol"] + "_" + this.CurrentContext.Request.QueryString["TopSession"];

        //    if (this.CacheExists(cacheName))
        //    {
        //        this.UpdateOutputInCache(cacheName);
        //    }
        //    else
        //    {
        //        StringBuilder jsonString = new StringBuilder();
        //        jsonString.Append("{");
        //        if (topSession > 0)
        //        {
        //            jsonString.Append("TopSymbols:" + jsSerializer.Serialize(StockSymbol_DynamicData.GetTopSymbolsForCMS(topSymbol, type, tradeCenter, topSession, fields)));
        //        }
        //        else
        //        {
        //            jsonString.Append("TopSymbols:" + jsSerializer.Serialize(StockSymbol_DynamicData.GetTopSymbolsForCMS(topSymbol, type, tradeCenter, fields)));
        //        }
        //        jsonString.Append("}");

        //        string output = jsonString.ToString();

        //        if (this.CacheExpiration > 0)
        //        {
        //            this.UpdateCache(cacheName, this.CacheExpiration, output);
        //        }
        //        this.UpdateOutput(output);
        //    }
        //}

        //public void GetTopSymbolForCMSInSession()
        //{
        //    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

        //    StockSymbol_DynamicData.TopSymbolBy type = StockSymbol_DynamicData.TopSymbolBy.TopUp;
        //    if (this.CurrentContext.Request.QueryString["Type"] == Lib.Object2Integer(StockSymbol_DynamicData.TopSymbolBy.TopDown).ToString())
        //    {
        //        type = StockSymbol_DynamicData.TopSymbolBy.TopDown;
        //    }
        //    else if (this.CurrentContext.Request.QueryString["Type"] == Lib.Object2Integer(StockSymbol_DynamicData.TopSymbolBy.TopQuantity).ToString())
        //    {
        //        type = StockSymbol_DynamicData.TopSymbolBy.TopQuantity;
        //    }

        //    TradeCenter tradeCenter = TradeCenter.AllTradeCenter;
        //    if (this.CurrentContext.Request.QueryString["TradeCenter"] == Lib.Object2Integer(TradeCenter.HaSTC).ToString())
        //    {
        //        tradeCenter = TradeCenter.HaSTC;
        //    }
        //    else if (this.CurrentContext.Request.QueryString["TradeCenter"] == Lib.Object2Integer(TradeCenter.HoSE).ToString())
        //    {
        //        tradeCenter = TradeCenter.HoSE;
        //    }

        //    int topSymbol = Lib.Object2Integer(this.CurrentContext.Request.QueryString["TopSymbol"]);
        //    int session = Lib.Object2Integer(this.CurrentContext.Request.QueryString["Session"]);
        //    string date = this.CurrentContext.Request.QueryString["Date"];

        //    string[] fields = this.Parameters;

        //    string cacheName = "Cache_GetTopSymbolForCMSInSession_" + type.ToString() + "_" + tradeCenter.ToString() + "_" + topSymbol.ToString() + "_" + session.ToString() + "_" + date;

        //    if (this.CacheExists(cacheName))
        //    {
        //        this.UpdateOutputInCache(cacheName);
        //    }
        //    else
        //    {
        //        StringBuilder jsonString = new StringBuilder();
        //        jsonString.Append("{");
        //        jsonString.Append("TopSymbols:" + jsSerializer.Serialize(StockSymbol_DynamicData.GetTopSymbolsForCMS(topSymbol, type, tradeCenter, session, date, fields)));
        //        jsonString.Append("}");

        //        string output = jsonString.ToString();

        //        if (this.CacheExpiration > 0)
        //        {
        //            this.UpdateCache(cacheName, this.CacheExpiration, output);
        //        }
        //        this.UpdateOutput(output);
        //    }
        //}

        //public void WriteGetTopSymbolForCMSInSession()
        //{
        //    try
        //    {
        //        StockSymbol_DynamicData.TopSymbolBy type = StockSymbol_DynamicData.TopSymbolBy.TopUp;
        //        if (this.CurrentContext.Request.QueryString["Type"] == Lib.Object2Integer(StockSymbol_DynamicData.TopSymbolBy.TopDown).ToString())
        //        {
        //            type = StockSymbol_DynamicData.TopSymbolBy.TopDown;
        //        }
        //        else if (this.CurrentContext.Request.QueryString["Type"] == Lib.Object2Integer(StockSymbol_DynamicData.TopSymbolBy.TopQuantity).ToString())
        //        {
        //            type = StockSymbol_DynamicData.TopSymbolBy.TopQuantity;
        //        }

        //        TradeCenter tradeCenter = TradeCenter.AllTradeCenter;
        //        if (this.CurrentContext.Request.QueryString["TradeCenter"] == Lib.Object2Integer(TradeCenter.HaSTC).ToString())
        //        {
        //            tradeCenter = TradeCenter.HaSTC;
        //        }
        //        else if (this.CurrentContext.Request.QueryString["TradeCenter"] == Lib.Object2Integer(TradeCenter.HoSE).ToString())
        //        {
        //            tradeCenter = TradeCenter.HoSE;
        //        }

        //        int topSymbol = Lib.Object2Integer(this.CurrentContext.Request.QueryString["TopSymbol"]);
        //        int session = Lib.Object2Integer(this.CurrentContext.Request.QueryString["Session"]);
        //        string date = this.CurrentContext.Request.QueryString["Date"];

        //        string[] fields = this.Parameters;

        //        string fileName = date + "_" + session.ToString() + "_" + topSymbol.ToString() + "_" + type.ToString() + "_" + tradeCenter.ToString() + ".htm";
        //        string filePath = ConfigurationManager.AppSettings["FilePath_TopSymbolInSession"];
        //        if (!filePath.EndsWith(@"\")) filePath += @"\";
        //        filePath += fileName;

        //        StockSymbol_DynamicData symbols = StockSymbol_DynamicData.GetTopSymbolsForCMS(topSymbol, type, tradeCenter, session, date, fields);
        //        int symbolCount = symbols.Symbols.Length;
        //        bool isAlternation = false;
        //        if (symbolCount > 0)
        //        {
        //            StringBuilder str = new StringBuilder();
        //            str.Append("<html><head><meta http-equiv='content-type' content='text/html;charset=UTF-8' /></head><body>");
        //            str.Append("<table rules='all' align='left' style='border: solid 1px #dadada; border-collapse: collapse;' cellpadding='5' cellspacing='0'>");
        //            str.Append("<tr style='background-color: #eeeeee;'><th style='text-align: left;'>Mã CK</th><th>Khối lượng</th><th>Giá</th><th>+/-</th></tr>");
        //            for (int i = 0; i < symbolCount; i++)
        //            {
        //                str.Append("<tr" + (isAlternation ? " style='background-color: #f9f9f9;'" : "") + ">");
        //                str.Append("<td style='text-align:left;'><a target='_blank' href='http://cafef.vn" + CompanyHelper_Update.GetCompanyInfoLink(symbols.Symbols[i].Symbol) + "'>" + symbols.Symbols[i].Symbol + "</a></td>");

        //                str.Append("<td style='text-align:right;'>" + Lib.FormatDouble(symbols.Symbols[i].Datas[0]) + "</td>");
        //                str.Append("<td style='text-align:right;'>" + Lib.FormatDouble(symbols.Symbols[i].Datas[1]) + "</td>");
        //                if (Lib.Object2Double(symbols.Symbols[i].Datas[0]) > 0)
        //                {
        //                    str.Append("<td style='text-align:right;color: #008000;'>+" + Lib.FormatDouble(symbols.Symbols[i].Datas[2]) + " (+" + Lib.FormatDouble(symbols.Symbols[i].Datas[3]) + "%)</td>");
        //                }
        //                else if (Lib.Object2Double(symbols.Symbols[i].Datas[0]) < 0)
        //                {
        //                    str.Append("<td style='text-align:right;color: #cc0000;'>" + Lib.FormatDouble(symbols.Symbols[i].Datas[2]) + " (" + Lib.FormatDouble(symbols.Symbols[i].Datas[3]) + "%)</td>");
        //                }
        //                else
        //                {
        //                    str.Append("<td style='text-align:right;color: #ff9900;'>0 (0%)</td>");
        //                }
        //                str.Append("</tr>");
        //            }
        //            str.Append("</table>");
        //            str.Append("</body></html>");

        //            using (System.IO.StreamWriter writer = System.IO.File.CreateText(filePath))
        //            {
        //                writer.Write(str.ToString());
        //                writer.Close();
        //            }
        //        }
        //        this.UpdateOutput("{Updated:'" + fileName + "'}");
        //    }
        //    catch
        //    {
        //        this.UpdateOutput("{Updated:''}");
        //    }
        //}


        #region BoxHangHoa
        public void GetBoxHangHoa()
        {
            var tabId = 0;
            if (!int.TryParse(this.CurrentContext.Request["box"] ?? "1", out tabId)) tabId = 1;
            List<ProductBox> ls;
            try
            {
                ls = ProductBoxBL.GetByTab(tabId);
            }
            catch (Exception ex)
            {
                ls = new List<ProductBox>();
            }

            if (ls == null || ls.Count == 0) return;
            var data = new List<HangHoa>();
            var i = 0;
            foreach (var d in ls)
            {
                data.Add(new HangHoa() { ProductName = d.ProductName, CurrentPrice = d.CurrentPrice, OtherPrice = d.OtherPrice, PrevPrice = d.PrevPrice, RowIndex = i, TabId = tabId });
                i++;
            }
            var jsSerializer = new JavaScriptSerializer();
            string output = "(" + jsSerializer.Serialize(data) + ")";
            this.UpdateOutput(output);
        }
        internal class HangHoa
        {
            public string ProductName { get; set; }
            public int TabId { get; set; }
            public double CurrentPrice { get; set; }
            public double OtherPrice { get; set; }
            public double PrevPrice { get; set; }
            public int RowIndex { get; set; }
            //public string ChangeString
            //{
            //    get
            //    {
            //        var format = "#,##0.0";
            //        if (TabId == 1 && !ProductName.ToUpper().Contains("VÀNG TG")) format = "#,##0";
            //        return string.Format("<span style='color:{3}'>{0}{1:" + format + "}" + ((TabId == 1 && !ProductName.ToUpper().Contains("VÀNG TG")) ? "" : " ") + "({0}{2:#,##0.0}%)", CurrentPrice > PrevPrice ? "+" : "", CurrentPrice - PrevPrice, (CurrentPrice - PrevPrice) / PrevPrice * 100, CurrentPrice > PrevPrice ? "#009900" : (CurrentPrice < PrevPrice ? "#CC0000" : "#333"));
            //    }
            //}
            //public string PriceString
            //{
            //    get
            //    {
            //        if (CurrentPrice == 0) return "";
            //        if (TabId != 1) return CurrentPrice.ToString("#,##0.0");
            //        return (TabId == 1 && ProductName.ToUpper().Contains("VÀNG TG")) ? CurrentPrice.ToString("#,##0.0") : CurrentPrice.ToString("#,##0");
            //    }
            //}
            //public string OtherString
            //{
            //    get
            //    {
            //        if (OtherPrice == 0) return "";
            //        if (TabId != 1) return OtherPrice.ToString("#,##0.0");
            //        return (TabId == 1 && ProductName.ToUpper().Contains("VÀNG TG")) ? OtherPrice.ToString("#,##0.0") : OtherPrice.ToString("#,##0");
            //    }
            //}
        }
        #endregion
        #endregion

        #region Private class
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

            //public static StockSymbol GetAllSymbols(TradeCenter tradeCenter)
            //{
            //    StockSymbol currentData = new StockSymbol();

            //    using (DataTable dtStockList = GetStockSymbolData(tradeCenter))
            //    {
            //        List<SymbolItem> items = new List<SymbolItem>();

            //        for (int i = 0; i < dtStockList.Rows.Count; i++)
            //        {
            //            if (Lib.Object2Double(dtStockList.Rows[i]["currentPrice"]) == 0)
            //            {
            //                items.Add(new SymbolItem(dtStockList.Rows[i]["code"].ToString(),
            //                                           Lib.Object2Double(dtStockList.Rows[i]["totalTradingQtty"]),
            //                                           Lib.Object2Double(dtStockList.Rows[i]["basicPrice"]),
            //                                           Lib.Object2Double(dtStockList.Rows[i]["chgIndex"]),
            //                                           Lib.Object2Double(dtStockList.Rows[i]["pctIndex"])));
            //            }
            //            else
            //            {
            //                items.Add(new SymbolItem(dtStockList.Rows[i]["code"].ToString(),
            //                                        Lib.Object2Double(dtStockList.Rows[i]["totalTradingQtty"]),
            //                                        Lib.Object2Double(dtStockList.Rows[i]["currentPrice"]),
            //                                        Lib.Object2Double(dtStockList.Rows[i]["chgIndex"]),
            //                                        Lib.Object2Double(dtStockList.Rows[i]["pctIndex"])));
            //            }
            //        }

            //        currentData.Symbols = items.ToArray();
            //    }
            //    return currentData;
            //}

            //public static StockSymbol GetSymbols(string symbolArray)
            //{
            //    StockSymbol currentData = new StockSymbol();

            //    string filterString = "";
            //    if (!string.IsNullOrEmpty(symbolArray))
            //    {
            //        if (symbolArray.StartsWith(";")) symbolArray = symbolArray.Substring(1);
            //        if (symbolArray.EndsWith(";")) symbolArray = symbolArray.Substring(0, symbolArray.Length - 1);

            //        string[] symbolList = symbolArray.Split(';');

            //        filterString = "";
            //        foreach (string symbol in symbolList)
            //        {
            //            filterString += " OR code='" + symbol + "'";
            //        }

            //        if (filterString != "") filterString = filterString.Substring(4);
            //    }

            //    List<SymbolItem> items = new List<SymbolItem>();

            //    if (filterString != "")
            //    {
            //        DataTable dtHAPriceData = GetStockSymbolData(TradeCenter.HaSTC);
            //        DataRow[] arrHAPriceData = dtHAPriceData.Select(filterString);
            //        for (int i = 0; i < arrHAPriceData.Length; i++)
            //        {
            //            if (Lib.Object2Double(arrHAPriceData[i]["currentPrice"]) == 0)
            //            {
            //                items.Add(new SymbolItem(arrHAPriceData[i]["code"].ToString(),
            //                                           Lib.Object2Double(arrHAPriceData[i]["totalTradingQtty"]),
            //                                           Lib.Object2Double(arrHAPriceData[i]["basicPrice"]),
            //                                           Lib.Object2Double(arrHAPriceData[i]["chgIndex"]),
            //                                           Lib.Object2Double(arrHAPriceData[i]["pctIndex"])));
            //            }
            //            else
            //            {
            //                items.Add(new SymbolItem(arrHAPriceData[i]["code"].ToString(),
            //                                        Lib.Object2Double(arrHAPriceData[i]["totalTradingQtty"]),
            //                                        Lib.Object2Double(arrHAPriceData[i]["currentPrice"]),
            //                                        Lib.Object2Double(arrHAPriceData[i]["chgIndex"]),
            //                                        Lib.Object2Double(arrHAPriceData[i]["pctIndex"])));
            //            }
            //        }
            //        dtHAPriceData.Dispose();

            //        DataTable dtHOPriceData = GetStockSymbolData(TradeCenter.HoSE);
            //        DataRow[] arrHOPriceData = dtHOPriceData.Select(filterString);
            //        for (int i = 0; i < arrHOPriceData.Length; i++)
            //        {
            //            if (Lib.Object2Double(arrHOPriceData[i]["currentPrice"]) == 0)
            //            {
            //                items.Add(new SymbolItem(arrHOPriceData[i]["code"].ToString(),
            //                                           Lib.Object2Double(arrHOPriceData[i]["totalTradingQtty"]),
            //                                           Lib.Object2Double(arrHOPriceData[i]["basicPrice"]),
            //                                           Lib.Object2Double(arrHOPriceData[i]["chgIndex"]),
            //                                           Lib.Object2Double(arrHOPriceData[i]["pctIndex"])));
            //            }
            //            else
            //            {
            //                items.Add(new SymbolItem(arrHOPriceData[i]["code"].ToString(),
            //                                        Lib.Object2Double(arrHOPriceData[i]["totalTradingQtty"]),
            //                                        Lib.Object2Double(arrHOPriceData[i]["currentPrice"]),
            //                                        Lib.Object2Double(arrHOPriceData[i]["chgIndex"]),
            //                                        Lib.Object2Double(arrHOPriceData[i]["pctIndex"])));
            //            }
            //        }
            //        dtHOPriceData.Dispose();
            //    }
            //    currentData.Symbols = items.ToArray();
            //    return currentData;
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

            #region Public methods
            //public static StockSymbol_DynamicData GetSymbolsByTradeCenter(TradeCenter tradeCenter, params string[] fields)
            //{
            //    StockSymbol_DynamicData currentData = new StockSymbol_DynamicData();

            //    using (DataTable dtStockList = GetStockSymbolData(tradeCenter))
            //    {
            //        List<SymbolItem> items = new List<SymbolItem>();

            //        for (int i = 0; i < dtStockList.Rows.Count; i++)
            //        {
            //            object[] datas = new object[fields.Length];

            //            for (int j = 0; j < fields.Length; j++)
            //            {
            //                if (fields[j].ToLower() == "currentprice")
            //                {
            //                    if (Lib.Object2Double(dtStockList.Rows[i][fields[j]]) > 0)
            //                    {
            //                        datas[j] = dtStockList.Rows[i][fields[j]];
            //                    }
            //                    else
            //                    {
            //                        try
            //                        {
            //                            datas[j] = dtStockList.Rows[i]["basicPrice"];
            //                        }
            //                        catch
            //                        {
            //                            datas[j] = 0;
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    datas[j] = dtStockList.Rows[i][fields[j]];
            //                }
            //            }

            //            items.Add(new SymbolItem(dtStockList.Rows[i]["code"].ToString(), datas));
            //        }

            //        currentData.Symbols = items.ToArray();
            //    }
            //    return currentData;
            //}

            public static StockSymbol_DynamicData GetSymbolsBySymbolArray(string symbolArray, params string[] fields)
            {
                StockSymbol_DynamicData currentData = new StockSymbol_DynamicData();

                List<string> symbols = new List<string>();
                try
                {
                    //string filterString = "";
                    if (!string.IsNullOrEmpty(symbolArray))
                    {
                        //if (symbolArray.StartsWith(";")) symbolArray = symbolArray.Substring(1);
                        //if (symbolArray.EndsWith(";")) symbolArray = symbolArray.Substring(0, symbolArray.Length - 1);

                        //string[] symbolList = symbolArray.Split(';');

                        ////filterString = "";
                        //string stock_Inorge = ConfigurationManager.AppSettings["SymbolInorge"];
                        //string[] tem = stock_Inorge.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        //List<string> codeStack = new List<string>();
                        //for (int i = 0, length = tem.Length; i < length; i++)
                        //    codeStack.Add(tem[i].Trim());

                        //foreach (string symbol in symbolList)
                        //{
                        //    //filterString += " OR code='" + symbol + "'";
                        //    if (!codeStack.Contains(symbol))
                        //        symbols.Add(symbol);
                        //}

                        ////if (filterString != "") filterString = filterString.Substring(4);
                        if (symbolArray.StartsWith(";")) symbolArray = symbolArray.Substring(1);
                        if (symbolArray.EndsWith(";")) symbolArray = symbolArray.Substring(0, symbolArray.Length - 1);

                        string[] symbolList = symbolArray.Split(';');

                        foreach (string symbol in symbolList)
                        {
                            if (!symbols.Contains(symbol.ToUpper())) symbols.Add(symbol.ToUpper());
                        }
                    }

                    List<SymbolItem> items = new List<SymbolItem>();

                    //if (filterString != "")
                    //if (symbols.Count > 0)
                    //{
                    //    DataTable dtHAPriceData = GetStockSymbolData(TradeCenter.HaSTC);
                    //    //DataRow[] arrHAPriceData = dtHAPriceData.Select(filterString);
                    //    //for (int i = 0; i < arrHAPriceData.Length; i++)
                    //    for (int i = 0; i < dtHAPriceData.Rows.Count; i++)
                    //    {
                    //        if (IsSymbolSelected(dtHAPriceData.Rows[i]["code"].ToString(), ref symbols))
                    //        {
                    //            object[] datas = new object[fields.Length];

                    //            for (int j = 0; j < fields.Length; j++)
                    //            {
                    //                if (fields[j].ToLower() == "currentprice")
                    //                {
                    //                    if (Lib.Object2Double(dtHAPriceData.Rows[i][fields[j]]) > 0)
                    //                    {
                    //                        datas[j] = dtHAPriceData.Rows[i][fields[j]];
                    //                    }
                    //                    else
                    //                    {
                    //                        try
                    //                        {
                    //                            datas[j] = dtHAPriceData.Rows[i]["basicPrice"];
                    //                        }
                    //                        catch
                    //                        {
                    //                            datas[j] = 0;
                    //                        }
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    datas[j] = dtHAPriceData.Rows[i][fields[j]];
                    //                }
                    //                //hungnd
                    //                try
                    //                {
                    //                    if (fields[j].ToLower() == "ceiling")
                    //                    {
                    //                        datas[j] = dtHAPriceData.Rows[i]["Ceiling"];
                    //                    }
                    //                    if (fields[j].ToLower() == "floor")
                    //                    {
                    //                        datas[j] = dtHAPriceData.Rows[i]["Floor"];
                    //                    }
                    //                }
                    //                catch { }
                    //                //
                    //            }

                    //            items.Add(new SymbolItem(dtHAPriceData.Rows[i]["code"].ToString(), datas));
                    //        }
                    //    }
                    //    dtHAPriceData.Dispose();

                    //    DataTable dtHOPriceData = GetStockSymbolData(TradeCenter.HoSE);
                    //    //DataRow[] arrHOPriceData = dtHOPriceData.Select(filterString);
                    //    //for (int i = 0; i < arrHOPriceData.Length; i++)
                    //    for (int i = 0; i < dtHOPriceData.Rows.Count; i++)
                    //    {
                    //        if (IsSymbolSelected(dtHOPriceData.Rows[i]["code"].ToString(), ref symbols))
                    //        {
                    //            object[] datas = new object[fields.Length];

                    //            for (int j = 0; j < fields.Length; j++)
                    //            {
                    //                if (fields[j].ToLower() == "currentprice")
                    //                {
                    //                    if (Lib.Object2Double(dtHOPriceData.Rows[i][fields[j]]) > 0)
                    //                    {
                    //                        datas[j] = dtHOPriceData.Rows[i][fields[j]];
                    //                    }
                    //                    else
                    //                    {
                    //                        try
                    //                        {
                    //                            datas[j] = dtHOPriceData.Rows[i]["basicPrice"];
                    //                        }
                    //                        catch
                    //                        {
                    //                            datas[j] = 0;
                    //                        }
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    datas[j] = dtHOPriceData.Rows[i][fields[j]];
                    //                }
                    //                //hungnd
                    //                try
                    //                {
                    //                    if (fields[j].ToLower() == "ceiling")
                    //                    {
                    //                        datas[j] = dtHOPriceData.Rows[i]["Ceiling"];
                    //                    }
                    //                    if (fields[j].ToLower() == "floor")
                    //                    {
                    //                        datas[j] = dtHOPriceData.Rows[i]["Floor"];
                    //                    }
                    //                }
                    //                catch { }
                    //                //
                    //            }

                    //            items.Add(new SymbolItem(dtHOPriceData.Rows[i]["code"].ToString(), datas));
                    //        }
                    //    }
                    //    dtHOPriceData.Dispose();
                    //}
                    var ss = new List<string>();
                    foreach (string code in symbols)
                    {
                        if (!ss.Contains(code.ToUpper())) ss.Add(code.ToUpper());
                    }
                    var ts = StockBL.GetStockPriceMultiple(ss);
                    object[] datas;
                    foreach (string code in symbols)
                    {
                        datas = new object[fields.Length];

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
                    currentData.Symbols = items.ToArray();
                }
                catch (Exception ex)
                {
                    //Lib.WriteError(ex.Message);
                }
                return currentData;
            }

            public static StockSymbol_DynamicData GetTop10Symbols(TopSymbolBy type, TradeCenter tradeCenter, params string[] fields)
            {
                StockSymbol_DynamicData currentData = new StockSymbol_DynamicData();

                //string cacheName = "";
                //if (type == TopSymbolBy.TopUp)
                //{
                //    cacheName = (tradeCenter == TradeCenter.AllTradeCenter ? "MemCached_Top10_Tang_All" : (tradeCenter == TradeCenter.HaSTC ? "MemCached_Top10_Tang_HaSTC" : "MemCached_Top10_Tang_HoSE"));
                //}
                //else if (type == TopSymbolBy.TopDown)
                //{
                //    cacheName = (tradeCenter == TradeCenter.AllTradeCenter ? "MemCached_Top10_Giam_All" : (tradeCenter == TradeCenter.HaSTC ? "MemCached_Top10_Giam_HaSTC" : "MemCached_Top10_Giam_HoSE"));
                //}
                //else
                //{
                //    cacheName = (tradeCenter == TradeCenter.AllTradeCenter ? "MemCached_Top10_KhoiLuong_All" : (tradeCenter == TradeCenter.HaSTC ? "MemCached_Top10_KhoiLuong_HaSTC" : "MemCached_Top10_KhoiLuong_HoSE"));
                //}

                //DataTable dtStockList = GetDataTable(cacheName);
                //var dtStockList = new DataTable();
                List<SymbolItem> items = new List<SymbolItem>();
                #region Get From WebService
                //if (dtStockList == null || dtStockList.Rows.Count == 0)
                //{
                    //CafeF_DataService.CafeF_DataService dataServices = new CafeF_EmbedData.CafeF_DataService.CafeF_DataService();
                    //WServices.Function dataServices = new WServices.Function();
                    var key = "";
                    switch (type)
                    {
                        case TopSymbolBy.TopUp:
                            switch (tradeCenter)
                            {
                                case TradeCenter.HaSTC:
                                    //dtStockList = dataServices.GetTopSymbol(10,
                                    //                                        WServices.Function.TopStockSymbol.TopUp,
                                    //                                        WServices.Function.TradeCenter.HaSTC).Tables[0];
                                    key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.PriceUp);
                                    break;
                                case TradeCenter.HoSE:
                                    //dtStockList = dataServices.GetTopSymbol(10,
                                    //                                        WServices.Function.TopStockSymbol.TopUp,
                                    //                                        WServices.Function.TradeCenter.HoSE).Tables[0];
                                    key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.PriceUp);
                                    break;
                                default:
                                    //dtStockList = dataServices.GetTopSymbol(10,
                                    //                                        WServices.Function.TopStockSymbol.TopUp,
                                    //                                        WServices.Function.TradeCenter.All).Tables[0];
                                    key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.PriceUp);
                                    break;
                            }
                            break;
                        case TopSymbolBy.TopDown:
                            switch (tradeCenter)
                            {
                                case TradeCenter.HaSTC:
                                    //dtStockList = dataServices.GetTopSymbol(10,
                                    //                                        WServices.Function.TopStockSymbol.TopDown,
                                    //                                        WServices.Function.TradeCenter.HaSTC).Tables[0];
                                    key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.PriceDown);
                                    break;
                                case TradeCenter.HoSE:
                                    //dtStockList = dataServices.GetTopSymbol(10,
                                    //                                        WServices.Function.TopStockSymbol.TopDown,
                                    //                                        WServices.Function.TradeCenter.HoSE).Tables[0];
                                    key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.PriceDown);
                                    break;
                                default:
                                    //dtStockList = dataServices.GetTopSymbol(10,
                                    //                                        WServices.Function.TopStockSymbol.TopDown,
                                    //                                        WServices.Function.TradeCenter.All).Tables[0];
                                    key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.PriceDown);
                                    break;
                            }
                            break;
                        default:
                            switch (tradeCenter)
                            {
                                case TradeCenter.HaSTC:
                                    //dtStockList = dataServices.GetTopSymbol(10,
                                    //                                        WServices.Function.TopStockSymbol.TopTrade,
                                    //                                        WServices.Function.TradeCenter.HaSTC).Tables[0];
                                    key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.VolumeDown);
                                    break;
                                case TradeCenter.HoSE:
                                    //dtStockList = dataServices.GetTopSymbol(10,
                                    //                                        WServices.Function.TopStockSymbol.TopTrade,
                                    //                                        WServices.Function.TradeCenter.HoSE).Tables[0];
                                    key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.VolumeDown);
                                    break;
                                default:
                                    //dtStockList = dataServices.GetTopSymbol(10,
                                    //                                        WServices.Function.TopStockSymbol.TopTrade,
                                    //                                        WServices.Function.TradeCenter.All).Tables[0];
                                    key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.VolumeDown);
                                    break;
                            }
                            break;
                    }
                    var tops = BLFACTORY.RedisClient.Get<List<TopStock>>(key);
                    //'Quantity':0,'Price':1,'Change':2,'ChangePercent':3
                //}
                foreach (var top in tops)
                {
                    object[] datas = new object[fields.Length];
                    datas[0] = top.Volume;
                    datas[1] = top.Price;
                    datas[2] = top.Price - top.BasicPrice;
                    datas[3] = top.BasicPrice <= 0 ? 0 : (top.Price - top.BasicPrice) / top.BasicPrice * 100;
                    items.Add(new SymbolItem(top.Symbol,datas));
                }
                #endregion

                //if (dtStockList == null || dtStockList.Rows.Count == 0)
                //{
                //    DataTable dtTemp;
                //    if (tradeCenter == TradeCenter.AllTradeCenter)
                //    {
                //        dtTemp = GetStockSymbolData(TradeCenter.HoSE);
                //        dtTemp.Merge(GetStockSymbolData(TradeCenter.HaSTC));
                //    }
                //    else
                //    {
                //        dtTemp = GetStockSymbolData(tradeCenter);
                //    }
                //    using (DataView dvTemp = new DataView(dtTemp))
                //    {
                //        dvTemp.RowFilter = (type == TopSymbolBy.TopUp ? "chgIndex > 0" : (type == TopSymbolBy.TopDown ? "chgIndex < 0" : ""));
                //        dvTemp.Sort = (type == TopSymbolBy.TopUp ? "chgIndex DESC" : (type == TopSymbolBy.TopDown ? "chgIndex ASC" : "totalTradingQtty DESC"));
                //        dtStockList = dvTemp.ToTable();
                //    }
                //    dtTemp.Dispose();
                //}

               

                //for (int i = 0; i < dtStockList.Rows.Count && i < 10; i++)
                //{
                //    object[] datas = new object[fields.Length];

                //    for (int j = 0; j < fields.Length; j++)
                //    {
                //        if (fields[j].ToLower() == "currentprice")
                //        {
                //            if (Lib.Object2Double(dtStockList.Rows[i][fields[j]]) > 0)
                //            {
                //                datas[j] = dtStockList.Rows[i][fields[j]];
                //            }
                //            else
                //            {
                //                try
                //                {
                //                    datas[j] = dtStockList.Rows[i]["basicPrice"];
                //                }
                //                catch
                //                {
                //                    datas[j] = 0;
                //                }
                //            }
                //        }
                //        else
                //        {
                //            datas[j] = dtStockList.Rows[i][fields[j]];
                //        }
                //    }

                //    items.Add(new SymbolItem(dtStockList.Rows[i]["code"].ToString(), datas));
                //}

                //dtStockList.Dispose();

                

                currentData.Symbols = items.ToArray();
                //Lib.WriteLog("Top10CP: " + currentData.Symbols.Length.ToString() + " Items");
                return currentData;
            }

            //public static StockSymbol_DynamicData GetTopSymbolsForCMS(int topSymbol, TopSymbolBy type, TradeCenter tradeCenter, params string[] fields)
            //{
            //    StockSymbol_DynamicData currentData = new StockSymbol_DynamicData();

            //    string cacheName = "";
            //    if (type == TopSymbolBy.TopUp)
            //    {
            //        cacheName = "MemCached_Top" + topSymbol.ToString() + (tradeCenter == TradeCenter.AllTradeCenter ? "_Tang_All" : (tradeCenter == TradeCenter.HaSTC ? "_Tang_HaSTC" : "_Tang_HoSE"));
            //    }
            //    else if (type == TopSymbolBy.TopDown)
            //    {
            //        cacheName = "MemCached_Top" + topSymbol.ToString() + (tradeCenter == TradeCenter.AllTradeCenter ? "_Giam_All" : (tradeCenter == TradeCenter.HaSTC ? "_Giam_HaSTC" : "_Giam_HoSE"));
            //    }
            //    else
            //    {
            //        cacheName = "MemCached_Top" + topSymbol.ToString() + (tradeCenter == TradeCenter.AllTradeCenter ? "_KhoiLuong_All" : (tradeCenter == TradeCenter.HaSTC ? "_KhoiLuong_HaSTC" : "_KhoiLuong_HoSE"));
            //    }

            //    DataTable dtStockList = GetDataTable(cacheName);

            //    #region Get From WebService
            //    if (dtStockList == null || dtStockList.Rows.Count == 0)
            //    {
            //        //CafeF_DataService.CafeF_DataService dataServices = new CafeF_EmbedData.CafeF_DataService.CafeF_DataService();
            //        WServices.Function dataServices = new WServices.Function();

            //        switch (type)
            //        {
            //            case TopSymbolBy.TopUp:
            //                switch (tradeCenter)
            //                {
            //                    case TradeCenter.HaSTC:
            //                        dtStockList = dataServices.GetTopSymbol(topSymbol,
            //                                                                WServices.Function.TopStockSymbol.TopUp,
            //                                                                WServices.Function.TradeCenter.HaSTC).Tables[0];
            //                        break;
            //                    case TradeCenter.HoSE:
            //                        dtStockList = dataServices.GetTopSymbol(topSymbol,
            //                                                                WServices.Function.TopStockSymbol.TopUp,
            //                                                                WServices.Function.TradeCenter.HoSE).Tables[0];
            //                        break;
            //                    default:
            //                        dtStockList = dataServices.GetTopSymbol(topSymbol,
            //                                                                WServices.Function.TopStockSymbol.TopUp,
            //                                                                WServices.Function.TradeCenter.All).Tables[0];
            //                        break;
            //                }
            //                break;
            //            case TopSymbolBy.TopDown:
            //                switch (tradeCenter)
            //                {
            //                    case TradeCenter.HaSTC:
            //                        dtStockList = dataServices.GetTopSymbol(topSymbol,
            //                                                                WServices.Function.TopStockSymbol.TopDown,
            //                                                                WServices.Function.TradeCenter.HaSTC).Tables[0];
            //                        break;
            //                    case TradeCenter.HoSE:
            //                        dtStockList = dataServices.GetTopSymbol(topSymbol,
            //                                                                WServices.Function.TopStockSymbol.TopDown,
            //                                                                WServices.Function.TradeCenter.HoSE).Tables[0];
            //                        break;
            //                    default:
            //                        dtStockList = dataServices.GetTopSymbol(topSymbol,
            //                                                                WServices.Function.TopStockSymbol.TopDown,
            //                                                                WServices.Function.TradeCenter.All).Tables[0];
            //                        break;
            //                }
            //                break;
            //            default:
            //                switch (tradeCenter)
            //                {
            //                    case TradeCenter.HaSTC:
            //                        dtStockList = dataServices.GetTopSymbol(topSymbol,
            //                                                                WServices.Function.TopStockSymbol.TopTrade,
            //                                                                WServices.Function.TradeCenter.HaSTC).Tables[0];
            //                        break;
            //                    case TradeCenter.HoSE:
            //                        dtStockList = dataServices.GetTopSymbol(topSymbol,
            //                                                                WServices.Function.TopStockSymbol.TopTrade,
            //                                                                WServices.Function.TradeCenter.HoSE).Tables[0];
            //                        break;
            //                    default:
            //                        dtStockList = dataServices.GetTopSymbol(topSymbol,
            //                                                                WServices.Function.TopStockSymbol.TopTrade,
            //                                                                WServices.Function.TradeCenter.All).Tables[0];
            //                        break;
            //                }
            //                break;
            //        }
            //    }
            //    #endregion

            //    if (dtStockList == null || dtStockList.Rows.Count == 0)
            //    {
            //        DataTable dtTemp;
            //        if (tradeCenter == TradeCenter.AllTradeCenter)
            //        {
            //            dtTemp = GetStockSymbolData(TradeCenter.HoSE);
            //            dtTemp.Merge(GetStockSymbolData(TradeCenter.HaSTC));
            //        }
            //        else
            //        {
            //            dtTemp = GetStockSymbolData(tradeCenter);
            //        }
            //        using (DataView dvTemp = new DataView(dtTemp))
            //        {
            //            dvTemp.RowFilter = (type == TopSymbolBy.TopUp ? "chgIndex > 0" : (type == TopSymbolBy.TopDown ? "chgIndex < 0" : ""));
            //            dvTemp.Sort = (type == TopSymbolBy.TopUp ? "chgIndex DESC" : (type == TopSymbolBy.TopDown ? "chgIndex ASC" : "totalTradingQtty DESC"));
            //            dtStockList = dvTemp.ToTable();
            //        }
            //        dtTemp.Dispose();
            //    }

            //    List<SymbolItem> items = new List<SymbolItem>();

            //    for (int i = 0; i < dtStockList.Rows.Count; i++)
            //    {
            //        object[] datas = new object[fields.Length];

            //        for (int j = 0; j < fields.Length; j++)
            //        {
            //            if (fields[j].ToLower() == "currentprice")
            //            {
            //                if (Lib.Object2Double(dtStockList.Rows[i][fields[j]]) > 0)
            //                {
            //                    datas[j] = dtStockList.Rows[i][fields[j]];
            //                }
            //                else
            //                {
            //                    try
            //                    {
            //                        datas[j] = dtStockList.Rows[i]["basicPrice"];
            //                    }
            //                    catch
            //                    {
            //                        datas[j] = 0;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                datas[j] = dtStockList.Rows[i][fields[j]];
            //            }
            //        }

            //        items.Add(new SymbolItem(dtStockList.Rows[i]["code"].ToString(), datas));
            //    }

            //    dtStockList.Dispose();

            //    currentData.Symbols = items.ToArray();
            //    //Lib.WriteLog("Top10CP: " + currentData.Symbols.Length.ToString() + " Items");
            //    return currentData;
            //}

            //public static StockSymbol_DynamicData GetTopSymbolsForCMS(int topSymbol, TopSymbolBy type, TradeCenter tradeCenter, int numberOfSession, params string[] fields)
            //{
            //    StockSymbol_DynamicData currentData = new StockSymbol_DynamicData();

            //    string cacheName = "";
            //    if (type == TopSymbolBy.TopUp)
            //    {
            //        cacheName = "MemCached_Top" + topSymbol.ToString() + (tradeCenter == TradeCenter.AllTradeCenter ? "_Tang_All" : (tradeCenter == TradeCenter.HaSTC ? "_Tang_HaSTC" : "_Tang_HoSE")) + numberOfSession.ToString();
            //    }
            //    else if (type == TopSymbolBy.TopDown)
            //    {
            //        cacheName = "MemCached_Top" + topSymbol.ToString() + (tradeCenter == TradeCenter.AllTradeCenter ? "_Giam_All" : (tradeCenter == TradeCenter.HaSTC ? "_Giam_HaSTC" : "_Giam_HoSE")) + numberOfSession.ToString();
            //    }
            //    else
            //    {
            //        cacheName = "MemCached_Top" + topSymbol.ToString() + (tradeCenter == TradeCenter.AllTradeCenter ? "_KhoiLuong_All" : (tradeCenter == TradeCenter.HaSTC ? "_KhoiLuong_HaSTC" : "_KhoiLuong_HoSE")) + numberOfSession.ToString();
            //    }

            //    DataTable dtStockList = GetDataTable(cacheName);

            //    #region Get From WebService
            //    if (dtStockList == null || dtStockList.Rows.Count == 0)
            //    {
            //        //CafeF_DataService.CafeF_DataService dataServices = new CafeF_EmbedData.CafeF_DataService.CafeF_DataService();
            //        WServices.Function dataServices = new WServices.Function();

            //        switch (type)
            //        {
            //            case TopSymbolBy.TopUp:
            //                switch (tradeCenter)
            //                {
            //                    case TradeCenter.HaSTC:
            //                        dtStockList = dataServices.GetTopSymbolsBySessionRange(topSymbol,
            //                                                                WServices.Function.TradeCenter.HaSTC,
            //                                                                numberOfSession,
            //                                                                WServices.Function.TopStockSymbol.TopUp).Tables[0];
            //                        break;
            //                    case TradeCenter.HoSE:
            //                        dtStockList = dataServices.GetTopSymbolsBySessionRange(topSymbol,
            //                                                                WServices.Function.TradeCenter.HoSE,
            //                                                                numberOfSession,
            //                                                                WServices.Function.TopStockSymbol.TopUp).Tables[0];
            //                        break;
            //                    default:
            //                        dtStockList = dataServices.GetTopSymbolsBySessionRange(topSymbol,
            //                                                                WServices.Function.TradeCenter.All,
            //                                                                numberOfSession,
            //                                                                WServices.Function.TopStockSymbol.TopUp).Tables[0];
            //                        break;
            //                }
            //                break;
            //            case TopSymbolBy.TopDown:
            //                switch (tradeCenter)
            //                {
            //                    case TradeCenter.HaSTC:
            //                        dtStockList = dataServices.GetTopSymbolsBySessionRange(topSymbol,
            //                                                                WServices.Function.TradeCenter.HaSTC,
            //                                                                numberOfSession,
            //                                                                WServices.Function.TopStockSymbol.TopDown).Tables[0];
            //                        break;
            //                    case TradeCenter.HoSE:
            //                        dtStockList = dataServices.GetTopSymbolsBySessionRange(topSymbol,
            //                                                                WServices.Function.TradeCenter.HoSE,
            //                                                                numberOfSession,
            //                                                                WServices.Function.TopStockSymbol.TopDown).Tables[0];
            //                        break;
            //                    default:
            //                        dtStockList = dataServices.GetTopSymbolsBySessionRange(topSymbol,
            //                                                                WServices.Function.TradeCenter.All,
            //                                                                numberOfSession,
            //                                                                WServices.Function.TopStockSymbol.TopDown).Tables[0];
            //                        break;
            //                }
            //                break;
            //            default:
            //                switch (tradeCenter)
            //                {
            //                    case TradeCenter.HaSTC:
            //                        dtStockList = dataServices.GetTopSymbolsBySessionRange(topSymbol,
            //                                                                WServices.Function.TradeCenter.HaSTC,
            //                                                                numberOfSession,
            //                                                                WServices.Function.TopStockSymbol.TopTrade).Tables[0];
            //                        break;
            //                    case TradeCenter.HoSE:
            //                        dtStockList = dataServices.GetTopSymbolsBySessionRange(topSymbol,
            //                                                                WServices.Function.TradeCenter.HoSE,
            //                                                                numberOfSession,
            //                                                                WServices.Function.TopStockSymbol.TopTrade).Tables[0];
            //                        break;
            //                    default:
            //                        dtStockList = dataServices.GetTopSymbolsBySessionRange(topSymbol,
            //                                                                WServices.Function.TradeCenter.All,
            //                                                                numberOfSession,
            //                                                                WServices.Function.TopStockSymbol.TopTrade).Tables[0];
            //                        break;
            //                }
            //                break;
            //        }
            //    }
            //    #endregion

            //    if (dtStockList == null || dtStockList.Rows.Count == 0)
            //    {
            //        DataTable dtTemp;
            //        if (tradeCenter == TradeCenter.AllTradeCenter)
            //        {
            //            dtTemp = GetStockSymbolData(TradeCenter.HoSE);
            //            dtTemp.Merge(GetStockSymbolData(TradeCenter.HaSTC));
            //        }
            //        else
            //        {
            //            dtTemp = GetStockSymbolData(tradeCenter);
            //        }
            //        using (DataView dvTemp = new DataView(dtTemp))
            //        {
            //            dvTemp.RowFilter = (type == TopSymbolBy.TopUp ? "chgIndex > 0" : (type == TopSymbolBy.TopDown ? "chgIndex < 0" : ""));
            //            dvTemp.Sort = (type == TopSymbolBy.TopUp ? "chgIndex DESC" : (type == TopSymbolBy.TopDown ? "chgIndex ASC" : "totalTradingQtty DESC"));
            //            dtStockList = dvTemp.ToTable();
            //        }
            //        dtTemp.Dispose();
            //    }

            //    List<SymbolItem> items = new List<SymbolItem>();

            //    for (int i = 0; i < dtStockList.Rows.Count; i++)
            //    {
            //        object[] datas = new object[fields.Length];

            //        for (int j = 0; j < fields.Length; j++)
            //        {
            //            if (fields[j].ToLower() == "currentprice")
            //            {
            //                if (Lib.Object2Double(dtStockList.Rows[i][fields[j]]) > 0)
            //                {
            //                    datas[j] = dtStockList.Rows[i][fields[j]];
            //                }
            //                else
            //                {
            //                    try
            //                    {
            //                        datas[j] = dtStockList.Rows[i]["basicPrice"];
            //                    }
            //                    catch
            //                    {
            //                        datas[j] = 0;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                datas[j] = dtStockList.Rows[i][fields[j]];
            //            }
            //        }

            //        items.Add(new SymbolItem(dtStockList.Rows[i]["code"].ToString(), datas));
            //    }

            //    dtStockList.Dispose();

            //    currentData.Symbols = items.ToArray();
            //    //Lib.WriteLog("Top10CP: " + currentData.Symbols.Length.ToString() + " Items");
            //    return currentData;
            //}

            //public static StockSymbol_DynamicData GetTopSymbols(int top, TopSymbolBy type, TradeCenter tradeCenter, params string[] fields)
            //{
            //    StockSymbol_DynamicData currentData = new StockSymbol_DynamicData();

            //    string cacheName = "";
            //    if (type == TopSymbolBy.TopUp)
            //    {
            //        cacheName = (tradeCenter == TradeCenter.AllTradeCenter ? "MemCached_Top10_Tang_All" : (tradeCenter == TradeCenter.HaSTC ? "MemCached_Top10_Tang_HaSTC" : "MemCached_Top10_Tang_HoSE"));
            //    }
            //    else if (type == TopSymbolBy.TopDown)
            //    {
            //        cacheName = (tradeCenter == TradeCenter.AllTradeCenter ? "MemCached_Top10_Giam_All" : (tradeCenter == TradeCenter.HaSTC ? "MemCached_Top10_Giam_HaSTC" : "MemCached_Top10_Giam_HoSE"));
            //    }
            //    else
            //    {
            //        cacheName = (tradeCenter == TradeCenter.AllTradeCenter ? "MemCached_Top10_KhoiLuong_All" : (tradeCenter == TradeCenter.HaSTC ? "MemCached_Top10_KhoiLuong_HaSTC" : "MemCached_Top10_KhoiLuong_HoSE"));
            //    }

            //    DataTable dtStockList = GetDataTable(cacheName);

            //    #region Get From WebService
            //    if (dtStockList == null || dtStockList.Rows.Count == 0)
            //    {
            //        //CafeF_DataService.CafeF_DataService dataServices = new CafeF_EmbedData.CafeF_DataService.CafeF_DataService();
            //        WServices.Function dataServices = new WServices.Function();

            //        switch (type)
            //        {
            //            case TopSymbolBy.TopUp:
            //                switch (tradeCenter)
            //                {
            //                    case TradeCenter.HaSTC:
            //                        dtStockList = dataServices.GetTopSymbol(top,
            //                                                                WServices.Function.TopStockSymbol.TopUp,
            //                                                                WServices.Function.TradeCenter.HaSTC).Tables[0];
            //                        break;
            //                    case TradeCenter.HoSE:
            //                        dtStockList = dataServices.GetTopSymbol(top,
            //                                                                WServices.Function.TopStockSymbol.TopUp,
            //                                                                WServices.Function.TradeCenter.HoSE).Tables[0];
            //                        break;
            //                    default:
            //                        dtStockList = dataServices.GetTopSymbol(top,
            //                                                                WServices.Function.TopStockSymbol.TopUp,
            //                                                                WServices.Function.TradeCenter.All).Tables[0];
            //                        break;
            //                }
            //                break;
            //            case TopSymbolBy.TopDown:
            //                switch (tradeCenter)
            //                {
            //                    case TradeCenter.HaSTC:
            //                        dtStockList = dataServices.GetTopSymbol(top,
            //                                                                WServices.Function.TopStockSymbol.TopDown,
            //                                                                WServices.Function.TradeCenter.HaSTC).Tables[0];
            //                        break;
            //                    case TradeCenter.HoSE:
            //                        dtStockList = dataServices.GetTopSymbol(top,
            //                                                                WServices.Function.TopStockSymbol.TopDown,
            //                                                                WServices.Function.TradeCenter.HoSE).Tables[0];
            //                        break;
            //                    default:
            //                        dtStockList = dataServices.GetTopSymbol(top,
            //                                                                WServices.Function.TopStockSymbol.TopDown,
            //                                                                WServices.Function.TradeCenter.All).Tables[0];
            //                        break;
            //                }
            //                break;
            //            default:
            //                switch (tradeCenter)
            //                {
            //                    case TradeCenter.HaSTC:
            //                        dtStockList = dataServices.GetTopSymbol(top,
            //                                                                WServices.Function.TopStockSymbol.TopTrade,
            //                                                                WServices.Function.TradeCenter.HaSTC).Tables[0];
            //                        break;
            //                    case TradeCenter.HoSE:
            //                        dtStockList = dataServices.GetTopSymbol(top,
            //                                                                WServices.Function.TopStockSymbol.TopTrade,
            //                                                                WServices.Function.TradeCenter.HoSE).Tables[0];
            //                        break;
            //                    default:
            //                        dtStockList = dataServices.GetTopSymbol(top,
            //                                                                WServices.Function.TopStockSymbol.TopTrade,
            //                                                                WServices.Function.TradeCenter.All).Tables[0];
            //                        break;
            //                }
            //                break;
            //        }
            //    }
            //    #endregion

            //    if (dtStockList == null || dtStockList.Rows.Count == 0)
            //    {
            //        DataTable dtTemp;
            //        if (tradeCenter == TradeCenter.AllTradeCenter)
            //        {
            //            dtTemp = GetStockSymbolData(TradeCenter.HoSE);
            //            dtTemp.Merge(GetStockSymbolData(TradeCenter.HaSTC));
            //        }
            //        else
            //        {
            //            dtTemp = GetStockSymbolData(tradeCenter);
            //        }
            //        using (DataView dvTemp = new DataView(dtTemp))
            //        {
            //            dvTemp.RowFilter = (type == TopSymbolBy.TopUp ? "chgIndex > 0" : (type == TopSymbolBy.TopDown ? "chgIndex < 0" : ""));
            //            dvTemp.Sort = (type == TopSymbolBy.TopUp ? "chgIndex DESC" : (type == TopSymbolBy.TopDown ? "chgIndex ASC" : "totalTradingQtty DESC"));
            //            dtStockList = dvTemp.ToTable();
            //        }
            //        dtTemp.Dispose();
            //    }

            //    List<SymbolItem> items = new List<SymbolItem>();

            //    for (int i = 0; i < dtStockList.Rows.Count && i < 10; i++)
            //    {
            //        object[] datas = new object[fields.Length];

            //        for (int j = 0; j < fields.Length; j++)
            //        {
            //            if (fields[j].ToLower() == "currentprice")
            //            {
            //                if (Lib.Object2Double(dtStockList.Rows[i][fields[j]]) > 0)
            //                {
            //                    datas[j] = dtStockList.Rows[i][fields[j]];
            //                }
            //                else
            //                {
            //                    try
            //                    {
            //                        datas[j] = dtStockList.Rows[i]["basicPrice"];
            //                    }
            //                    catch
            //                    {
            //                        datas[j] = 0;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                datas[j] = dtStockList.Rows[i][fields[j]];
            //            }
            //        }

            //        items.Add(new SymbolItem(dtStockList.Rows[i]["code"].ToString(), datas));
            //    }

            //    dtStockList.Dispose();

            //    currentData.Symbols = items.ToArray();
            //    //Lib.WriteLog("Top10CP: " + currentData.Symbols.Length.ToString() + " Items");
            //    return currentData;
            //}

            //public static StockSymbol_DynamicData GetAllSymbols_IncludeIndexValue()
            //{
            //    StockSymbol_DynamicData hastc = GetSymbolsByTradeCenter(TradeCenter.HaSTC, "currentPrice", "chgIndex");
            //    StockSymbol_DynamicData hose = GetSymbolsByTradeCenter(TradeCenter.HoSE, "currentPrice", "chgIndex");
            //    StockMarket index = StockMarket.GetCurrentData();

            //    StockSymbol_DynamicData currentData = new StockSymbol_DynamicData();

            //    currentData.Symbols = new SymbolItem[hastc.Symbols.Length + hose.Symbols.Length + 2];

            //    int i = 0;

            //    currentData.Symbols[i].Symbol = "VNIndex";
            //    currentData.Symbols[i].Datas = new object[2];
            //    currentData.Symbols[i].Datas[0] = index.VNIndex_Index;
            //    currentData.Symbols[i].Datas[1] = index.VNIndex_Change;
            //    i++;

            //    for (; i < hose.Symbols.Length + 1; i++)
            //    {
            //        currentData.Symbols[i].Symbol = hose.Symbols[i - 1].Symbol;
            //        currentData.Symbols[i].Datas = hose.Symbols[i - 1].Datas;
            //    }

            //    currentData.Symbols[i].Symbol = "HaSTCIndex";
            //    currentData.Symbols[i].Datas = new object[2];
            //    currentData.Symbols[i].Datas[0] = index.HaSTCIndex_Index;
            //    currentData.Symbols[i].Datas[1] = index.HaSTCIndex_Change;
            //    i++;

            //    for (; i < hastc.Symbols.Length + hose.Symbols.Length + 2; i++)
            //    {
            //        currentData.Symbols[i].Symbol = hastc.Symbols[i - hose.Symbols.Length - 2].Symbol;
            //        currentData.Symbols[i].Datas = hastc.Symbols[i - hose.Symbols.Length - 2].Datas;
            //    }

            //    return currentData;
            //}

            //public static StockSymbol_DynamicData GetTopSymbolsForCMS(int topSymbol, TopSymbolBy type, TradeCenter tradeCenter, int session, string date, params string[] fields)
            //{
            //    StockSymbol_DynamicData currentData = new StockSymbol_DynamicData();

            //    string cacheName = "";
            //    if (type == TopSymbolBy.TopUp)
            //    {
            //        cacheName = "MemCached_Top" + topSymbol.ToString() + (tradeCenter == TradeCenter.AllTradeCenter ? "_Tang_All_Session" : (tradeCenter == TradeCenter.HaSTC ? "_Tang_HaSTC_Session" : "_Tang_HoSE_Session")) + session.ToString() + "_Date_" + date;
            //    }
            //    else if (type == TopSymbolBy.TopDown)
            //    {
            //        cacheName = "MemCached_Top" + topSymbol.ToString() + (tradeCenter == TradeCenter.AllTradeCenter ? "_Giam_All_Session" : (tradeCenter == TradeCenter.HaSTC ? "_Giam_HaSTC_Session" : "_Giam_HoSE_Session")) + session.ToString() + "_Date_" + date;
            //    }
            //    else
            //    {
            //        cacheName = "MemCached_Top" + topSymbol.ToString() + (tradeCenter == TradeCenter.AllTradeCenter ? "_KhoiLuong_All_Session" : (tradeCenter == TradeCenter.HaSTC ? "_KhoiLuong_HaSTC_Session" : "_KhoiLuong_HoSE_Session")) + session.ToString() + "_Date_" + date;
            //    }

            //    DataTable dtStockList = GetDataTable(cacheName);

            //    List<SymbolItem> items = new List<SymbolItem>();

            //    for (int i = 0; i < dtStockList.Rows.Count; i++)
            //    {
            //        object[] datas = new object[fields.Length];

            //        for (int j = 0; j < fields.Length; j++)
            //        {
            //            if (fields[j].ToLower() == "currentprice")
            //            {
            //                if (Lib.Object2Double(dtStockList.Rows[i][fields[j]]) > 0)
            //                {
            //                    datas[j] = dtStockList.Rows[i][fields[j]];
            //                }
            //                else
            //                {
            //                    try
            //                    {
            //                        datas[j] = dtStockList.Rows[i]["basicPrice"];
            //                    }
            //                    catch
            //                    {
            //                        datas[j] = 0;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                datas[j] = dtStockList.Rows[i][fields[j]];
            //            }
            //        }

            //        items.Add(new SymbolItem(dtStockList.Rows[i]["code"].ToString(), datas));
            //    }

            //    dtStockList.Dispose();

            //    currentData.Symbols = items.ToArray();
            //    //Lib.WriteLog("Top10CP: " + currentData.Symbols.Length.ToString() + " Items");
            //    return currentData;
            //}
            #endregion

            //private static bool IsSymbolSelected(string symbol, ref List<string> selectedSymbols)
            //{
            //    for (int i = 0; i < selectedSymbols.Count; i++)
            //    {
            //        if (symbol == selectedSymbols[i])
            //        {
            //            selectedSymbols.RemoveAt(i);
            //            return true;
            //        }
            //    }
            //    return false;
            //}

            //private static DataTable GetStockSymbolData(TradeCenter tradeCenter)
            //{
            //    //string cacheName = (tradeCenter == TradeCenter.HaSTC ? CafeF.BO.Const.SIMPLE_HA_STOCKSDATA : CafeF.BO.Const.SIMPLE_HO_STOCKSDATA);
            //    string cacheName = (tradeCenter == TradeCenter.HaSTC ? "MemCached_HaSTC_PriceData" : "MemCached_HoSE_PriceData");
            //    //DataTable dtStockList = MarketHelper.ReadMemCache(cacheName);
            //    DataTable dtStockList = DistCache.Get<DataTable>(cacheName);

            //    if (dtStockList == null || dtStockList.Rows.Count == 0)
            //    {
            //        //CafeF_DataService.CafeF_DataService dataServices = new CafeF_EmbedData.CafeF_DataService.CafeF_DataService();
            //        WServices.Function dataServices = new WServices.Function();
            //        if (tradeCenter == TradeCenter.HaSTC)
            //        {
            //            dtStockList = dataServices.GetHaSTCPriceData().Tables[0].Copy();
            //        }
            //        else if (tradeCenter == TradeCenter.HoSE)
            //        {
            //            dtStockList = dataServices.GetHoSEPriceData().Tables[0].Copy();
            //        }
            //    }
            //    return dtStockList;
            //}

            //private static DataTable GetDataTable(string CacheName)
            //{
            //    //DataTable __tbl = (DataTable)HttpContext.Current.Cache[CacheName + "__web"];
            //    //if (__tbl == null)
            //    //{
            //    //    try
            //    //    {
            //    //        using (CafefShareMemory.CafefCache __cTopUp = new CafefShareMemory.CafefCache(CacheName))
            //    //        {
            //    //            __tbl = (DataTable)__cTopUp.Contents(CacheName + "__content");
            //    //            if (__tbl != null)
            //    //            {
            //    //                HttpContext.Current.Cache.Insert(CacheName + "__web", __tbl, null, DateTime.Now.AddSeconds(30), TimeSpan.Zero);
            //    //                HttpContext.Current.Cache.Insert(CacheName + "__webG", __tbl, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
            //    //            }
            //    //        }
            //    //    }
            //    //    catch (Exception ex)
            //    //    {
            //    //        if (__tbl == null)
            //    //        {
            //    //            __tbl = (DataTable)HttpContext.Current.Cache[CacheName + "__webG"];
            //    //        }
            //    //    }
            //    //}
            //    ////__tbl = MarketHelper.GetStockPriceByMarket("HaSTC");
            //    //return __tbl;

            //    return DistCache.Get<DataTable>(CacheName);
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

        #region Finance statement
        private class FinanceStatement
        {
            public enum TopFinanceStatementBy
            {
                TopPE,
                TopEPS,
                TopCapital
            }
            public enum RelatedCompanyType
            {
                EquivalentEPS,
                EquivalentPE,
                SameIndustry
            }
            public struct FinanceStatementItem
            {
                public FinanceStatementItem(string symbol, double pe, double eps, double capital, double price, double change, double percent, double Ceiling, double Floor)
                {
                    this.Symbol = symbol;
                    this.PE = pe;
                    this.EPS = eps;
                    this.Capital = capital;
                    this.Price = price;
                    this.Change = change;
                    this.Percent = percent;
                    this.Ceiling = Ceiling;
                    this.Floor = Floor;
                }
                public string Symbol;
                public double PE, EPS, Capital, Price, Change, Percent, Ceiling, Floor;
            }

            public static FinanceStatement GetTopFinanceStatement(TopFinanceStatementBy type, TradeCenter tradeCenter)
            {
                FinanceStatement currentData = new FinanceStatement();

                //DataTable dtPriceData = new DataTable("StockPriceData");
                //dtPriceData.Merge(GetStockSymbolData(TradeCenter.HaSTC));
                //dtPriceData.Merge(GetStockSymbolData(TradeCenter.HoSE));

                //string cacheName = "";

                //if (type == TopFinanceStatementBy.TopPE)
                //{
                //    if (tradeCenter == TradeCenter.AllTradeCenter)
                //    {
                //        cacheName += "MemCached_Top10_PE_All";
                //    }
                //    else if (tradeCenter == TradeCenter.HaSTC)
                //    {
                //        cacheName += "MemCached_Top10_PE_HaSTC";
                //    }
                //    else
                //    {
                //        cacheName += "MemCached_Top10_PE_HoSE";
                //    }
                //}
                //else if (type == TopFinanceStatementBy.TopEPS)
                //{
                //    if (tradeCenter == TradeCenter.AllTradeCenter)
                //    {
                //        cacheName += "MemCached_Top10_EPS_All";
                //    }
                //    else if (tradeCenter == TradeCenter.HaSTC)
                //    {
                //        cacheName += "MemCached_Top10_EPS_HaSTC";
                //    }
                //    else
                //    {
                //        cacheName += "MemCached_Top10_EPS_HoSE";
                //    }
                //}
                //else
                //{
                //    if (tradeCenter == TradeCenter.AllTradeCenter)
                //    {
                //        cacheName += "MemCached_Top10_Capital_All";
                //    }
                //    else if (tradeCenter == TradeCenter.HaSTC)
                //    {
                //        cacheName += "MemCached_Top10_Capital_HaSTC";
                //    }
                //    else
                //    {
                //        cacheName += "MemCached_Top10_Capital_HoSE";
                //    }
                //}
                ////using (FinanceStatementData fsdTopData = new FinanceStatementData(cacheName))
                //DataTable fsdTopData = DistCache.Get<DataTable>(cacheName);

                //if (fsdTopData == null || fsdTopData.Rows.Count == 0)
                //{
                    //#region Get From WebService
                    //if (fsdTopData == null || fsdTopData.Rows.Count == 0)
                    //{
                    //    //CafeF_DataService.CafeF_DataService dataServices = new CafeF_EmbedData.CafeF_DataService.CafeF_DataService();
                    //    WServices.Function dataServices = new WServices.Function();
                var key = "";
                switch (type)
                {
                    case TopFinanceStatementBy.TopEPS:
                        switch (tradeCenter)
                        {
                            case TradeCenter.HaSTC:
                                //fsdTopData = dataServices.GetTopCompany(10,
                                //                                        WServices.Function.TopCompany.TopEPS,
                                //                                        WServices.Function.TradeCenter.HaSTC).Tables[0];
                                key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.EPS);
                                break;
                            case TradeCenter.HoSE:
                                //fsdTopData = dataServices.GetTopCompany(10,
                                //                                        WServices.Function.TopCompany.TopEPS,
                                //                                        WServices.Function.TradeCenter.HoSE).Tables[0];
                                key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.EPS);
                                break;
                            default:
                                //fsdTopData = dataServices.GetTopCompany(10,
                                //                                        WServices.Function.TopCompany.TopEPS,
                                //                                        WServices.Function.TradeCenter.All).Tables[0];
                                key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.EPS);
                                break;
                        }
                        break;
                    case TopFinanceStatementBy.TopPE:
                        switch (tradeCenter)
                        {
                            case TradeCenter.HaSTC:
                                //fsdTopData = dataServices.GetTopCompany(10,
                                //                                        WServices.Function.TopCompany.TopPE,
                                //                                        WServices.Function.TradeCenter.HaSTC).Tables[0];
                                key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.PE);
                                break;
                            case TradeCenter.HoSE:
                                //fsdTopData = dataServices.GetTopCompany(10,
                                //                                        WServices.Function.TopCompany.TopPE,
                                //                                        WServices.Function.TradeCenter.HoSE).Tables[0];
                                key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.PE);
                                break;
                            default:
                                //fsdTopData = dataServices.GetTopCompany(10,
                                //                                        WServices.Function.TopCompany.TopPE,
                                //                                        WServices.Function.TradeCenter.All).Tables[0];
                                key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.PE);
                                break;
                        }
                        break;
                    default:
                        switch (tradeCenter)
                        {
                            case TradeCenter.HaSTC:
                                //fsdTopData = dataServices.GetTopCompany(10,
                                //                                        WServices.Function.TopCompany.TopCapital,
                                //                                        WServices.Function.TradeCenter.HaSTC).Tables[0];
                                key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.MarketCap);
                                break;
                            case TradeCenter.HoSE:
                                //fsdTopData = dataServices.GetTopCompany(10,
                                //                                        WServices.Function.TopCompany.TopCapital,
                                //                                        WServices.Function.TradeCenter.HoSE).Tables[0];
                                key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.MarketCap);
                                break;
                            default:
                                //fsdTopData = dataServices.GetTopCompany(10,
                                //                                        WServices.Function.TopCompany.TopCapital,
                                //                                        WServices.Function.TradeCenter.All).Tables[0];
                                key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.MarketCap);
                                break;
                        }
                        break;
                }
                    //}
                    //#endregion
                //}

                //fsdTopData.TableName = "TopData";
                //using (DataSet dsTopData = new DataSet())
                //{
                //    dsTopData.Merge(dtPriceData);
                //    dsTopData.Merge(fsdTopData);

                //    DataColumn parentColumn = dsTopData.Tables["StockPriceData"].Columns["code"];
                //    DataColumn childColumn = dsTopData.Tables["TopData"].Columns[FinanceStatementData.FIELD_SYMBOL];

                //    DataRelation relTopPE_StockPriceData = new DataRelation("TopData_StockPriceData", parentColumn, childColumn, false);

                //    dsTopData.Relations.Add(relTopPE_StockPriceData);

                //    dsTopData.Tables["TopData"].Columns.Add("basicPrice", typeof(double), "Parent.basicPrice");
                //    dsTopData.Tables["TopData"].Columns.Add("currentPrice", typeof(double), "Parent.currentPrice");
                //    dsTopData.Tables["TopData"].Columns.Add("chgIndex", typeof(double), "Parent.chgIndex");
                //    dsTopData.Tables["TopData"].Columns.Add("pctIndex", typeof(double), "Parent.pctIndex");

                //    List<FinanceStatementItem> items = new List<FinanceStatementItem>();

                //    for (int i = 0; i < dsTopData.Tables["TopData"].Rows.Count; i++)
                //    {
                //        if (Lib.Object2Double(dsTopData.Tables["TopData"].Rows[i]["currentPrice"]) == 0)
                //        {
                //            items.Add(new FinanceStatementItem(dsTopData.Tables["TopData"].Rows[i][FinanceStatementData.FIELD_SYMBOL].ToString(),
                //                                        Lib.Object2Double(dsTopData.Tables["TopData"].Rows[i][FinanceStatementData.FIELD_PE]),
                //                                        Lib.Object2Double(dsTopData.Tables["TopData"].Rows[i][FinanceStatementData.FIELD_EPS]),
                //                                        Lib.Object2Double(dsTopData.Tables["TopData"].Rows[i][FinanceStatementData.FIELD_CAPITAL]),
                //                                        Lib.Object2Double(dsTopData.Tables["TopData"].Rows[i]["basicPrice"]),
                //                                        Lib.Object2Double(dsTopData.Tables["TopData"].Rows[i]["chgIndex"]),
                //                                        Lib.Object2Double(dsTopData.Tables["TopData"].Rows[i]["pctIndex"]), 0, 0));
                //        }
                //        else
                //        {
                //            items.Add(new FinanceStatementItem(dsTopData.Tables["TopData"].Rows[i][FinanceStatementData.FIELD_SYMBOL].ToString(),
                //                                        Lib.Object2Double(dsTopData.Tables["TopData"].Rows[i][FinanceStatementData.FIELD_PE]),
                //                                        Lib.Object2Double(dsTopData.Tables["TopData"].Rows[i][FinanceStatementData.FIELD_EPS]),
                //                                        Lib.Object2Double(dsTopData.Tables["TopData"].Rows[i][FinanceStatementData.FIELD_CAPITAL]),
                //                                        Lib.Object2Double(dsTopData.Tables["TopData"].Rows[i]["currentPrice"]),
                //                                        Lib.Object2Double(dsTopData.Tables["TopData"].Rows[i]["chgIndex"]),
                //                                        Lib.Object2Double(dsTopData.Tables["TopData"].Rows[i]["pctIndex"]), 0, 0));
                //        }
                //    }

                //    currentData.FinanceStatements = items.ToArray();
                //}
                var items = new List<FinanceStatementItem>();

                var tops = BLFACTORY.RedisClient.Get<List<TopStock>>(key);
                    //'Quantity':0,'Price':1,'Change':2,'ChangePercent':3
                //}
                foreach (var top in tops)
                {
                    items.Add(new FinanceStatementItem(top.Symbol,top.PE, top.EPS, top.MarketCap, top.Price, top.Price - top.BasicPrice, top.BasicPrice==0?0:((top.Price - top.BasicPrice)/top.BasicPrice*100), 0,0));
                }

                currentData.FinanceStatements = items.ToArray();
                return currentData;
            }
            //public static FinanceStatement GetRelatedCompany(string symbol, RelatedCompanyType type, int pageIndex, int pageSize)
            //{
            //    FinanceStatement currentData = new FinanceStatement();

            //    try
            //    {
            //        DataTable dtPriceData = new DataTable("StockPriceData");
            //        dtPriceData.Merge(GetStockSymbolData(TradeCenter.HaSTC));
            //        dtPriceData.Merge(GetStockSymbolData(TradeCenter.HoSE));
            //        DataTable tbUpCom = GetStockSymbolData(TradeCenter.UpCom);
            //        if (tbUpCom != null && tbUpCom.Rows.Count > 0)
            //            dtPriceData.Merge(tbUpCom);

            //        string cacheName = "";
            //        if (type == RelatedCompanyType.EquivalentEPS)
            //        {
            //            cacheName += "MemCached_EquivalentEPS_" + symbol;
            //        }
            //        else if (type == RelatedCompanyType.EquivalentPE)
            //        {
            //            cacheName += "MemCached_EquivalentPE_" + symbol;
            //        }
            //        else
            //        {
            //            cacheName += "MemCached_SameIndustry_" + symbol;
            //        }
            //        //using (FinanceStatementData fsdData = new FinanceStatementData(cacheName))
            //        DataTable fsdData = DistCache.Get<DataTable>(cacheName);

            //        if (fsdData == null || fsdData.Rows.Count == 0)
            //        {
            //            //CafeF_DataService.CafeF_DataService dataServices = new CafeF_EmbedData.CafeF_DataService.CafeF_DataService();
            //            WServices.Function dataServices = new WServices.Function();

            //            switch (type)
            //            {
            //                case RelatedCompanyType.EquivalentEPS:
            //                    fsdData = dataServices.GetEquivalentEPS(symbol).Tables[0];
            //                    break;
            //                case RelatedCompanyType.EquivalentPE:
            //                    fsdData = dataServices.GetEquivalentPE(symbol).Tables[0];
            //                    break;
            //                default:
            //                    fsdData = dataServices.GetSameIndustry(symbol).Tables[0];
            //                    break;
            //            }
            //        }

            //        fsdData.TableName = "FinanceStatement";
            //        using (DataSet dsData = new DataSet())
            //        {
            //            dsData.Merge(dtPriceData);
            //            dsData.Merge(fsdData);

            //            DataColumn parentColumn = dsData.Tables["StockPriceData"].Columns["code"];
            //            DataColumn childColumn = dsData.Tables["FinanceStatement"].Columns[FinanceStatementData.FIELD_SYMBOL];

            //            DataRelation relTopPE_StockPriceData = new DataRelation("FinanceStatement_StockPriceData", parentColumn, childColumn, false);

            //            dsData.Relations.Add(relTopPE_StockPriceData);

            //            dsData.Tables["FinanceStatement"].Columns.Add("basicPrice", typeof(double), "Parent.basicPrice");
            //            dsData.Tables["FinanceStatement"].Columns.Add("currentPrice", typeof(double), "Parent.currentPrice");
            //            dsData.Tables["FinanceStatement"].Columns.Add("chgIndex", typeof(double), "Parent.chgIndex");
            //            dsData.Tables["FinanceStatement"].Columns.Add("pctIndex", typeof(double), "Parent.pctIndex");

            //            dsData.Tables["FinanceStatement"].Columns.Add("Ceiling", typeof(double), "Parent.Ceiling");
            //            dsData.Tables["FinanceStatement"].Columns.Add("Floor", typeof(double), "Parent.Floor");


            //            List<FinanceStatementItem> items = new List<FinanceStatementItem>();

            //            currentData.m_PageIndex = pageIndex;
            //            currentData.m_RecordCount = dsData.Tables["FinanceStatement"].Rows.Count;
            //            if (pageSize == 0)
            //            {
            //                pageSize = currentData.m_RecordCount;
            //            }
            //            if (pageSize > 0)
            //            {
            //                currentData.m_PageCount = currentData.m_RecordCount / pageSize;
            //                if (currentData.m_RecordCount % pageSize > 0) currentData.m_PageCount++;
            //            }
            //            else
            //            {
            //                currentData.m_PageCount = 0;
            //            }
            //            if (currentData.m_PageIndex > currentData.m_PageCount || currentData.m_PageIndex <= 0) currentData.m_PageIndex = 1;

            //            int startRecord = (currentData.m_PageIndex - 1) * pageSize;
            //            int endRecord = (startRecord + pageSize - 1 <= currentData.m_RecordCount ? startRecord + pageSize - 1 : currentData.m_RecordCount);

            //            for (int i = startRecord; i <= endRecord && i < currentData.m_RecordCount; i++)
            //            {
            //                try
            //                {
            //                    if (dsData.Tables["FinanceStatement"].Rows[i][FinanceStatementData.FIELD_SYMBOL].ToString() != "HASTC-INDEX" ||
            //                        dsData.Tables["FinanceStatement"].Rows[i][FinanceStatementData.FIELD_SYMBOL].ToString() != "VNINDEX")
            //                    {
            //                        if (Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i]["currentPrice"]) == 0)
            //                        {
            //                            items.Add(new FinanceStatementItem(dsData.Tables["FinanceStatement"].Rows[i][FinanceStatementData.FIELD_SYMBOL].ToString(),
            //                                                        Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i][FinanceStatementData.FIELD_PE]),
            //                                                        Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i][FinanceStatementData.FIELD_EPS]),
            //                                                        Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i][FinanceStatementData.FIELD_CAPITAL]),
            //                                                        Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i]["basicPrice"]),
            //                                                        Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i]["chgIndex"]),
            //                                                        Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i]["pctIndex"]),
            //                                                        Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i]["Ceiling"]),
            //                                                        Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i]["Floor"])));
            //                        }
            //                        else
            //                        {
            //                            items.Add(new FinanceStatementItem(dsData.Tables["FinanceStatement"].Rows[i][FinanceStatementData.FIELD_SYMBOL].ToString(),
            //                                                        Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i][FinanceStatementData.FIELD_PE]),
            //                                                        Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i][FinanceStatementData.FIELD_EPS]),
            //                                                        Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i][FinanceStatementData.FIELD_CAPITAL]),
            //                                                        Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i]["currentPrice"]),
            //                                                        Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i]["chgIndex"]),
            //                                                        Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i]["pctIndex"]),
            //                                                         Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i]["Ceiling"]),
            //                                                        Lib.Object2Double(dsData.Tables["FinanceStatement"].Rows[i]["Floor"])));
            //                        }
            //                    }
            //                }
            //                catch { }
            //            }
            //            currentData.FinanceStatements = items.ToArray();
            //            currentData.OtherData = dsData.Tables["FinanceStatement"].Rows[0][FinanceStatementData.FIELD_TRADECENTER].ToString();
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        //Lib.WriteError(ex.Message);
            //    }
            //    if (type == RelatedCompanyType.SameIndustry)
            //    {
            //        currentData.OtherData = CafeF.BO.CompanyHelper_Update.GetInsdustryName(symbol);
            //    }
            //    return currentData;
            //}

            private FinanceStatementItem[] m_FinanceStatements;
            private int m_PageCount, m_PageIndex, m_RecordCount;
            private string m_OtherData;

            public FinanceStatementItem[] FinanceStatements
            {
                get
                {
                    return this.m_FinanceStatements;
                }
                set
                {
                    this.m_FinanceStatements = value;
                }
            }

            public int PageCount
            {
                get
                {
                    return this.m_PageCount;
                }
                set
                {
                    this.m_PageCount = value;
                }
            }
            public int PageIndex
            {
                get
                {
                    return this.m_PageIndex;
                }
                set
                {
                    this.m_PageIndex = value;
                }
            }
            public int RecordCount
            {
                get
                {
                    return this.m_RecordCount;
                }
                set
                {
                    this.m_RecordCount = value;
                }
            }
            public string OtherData
            {
                get
                {
                    return this.m_OtherData;
                }
                set
                {
                    this.m_OtherData = value;
                }
            }
        }
        #endregion

        #region StockMarket summary
        private class StockMarket
        {
            private double m_VNIndex_Index, m_VNIndex_Change, m_VNIndex_ChangePercent, m_VNIndex_TotalVolume, m_VNIndex_TotalValue, m_VNIndex_TotalTrade;
            private double m_HaSTCIndex_Index, m_HaSTCIndex_Change, m_HaSTCIndex_ChangePercent, m_HaSTCIndex_TotalVolume, m_HaSTCIndex_TotalValue, m_HaSTCIndex_TotalTrade;
            private string m_LastTradeDate, m_TradeState, m_TradeStatePrivate;
            //private string 

            //public static StockMarket GetCurrentData()
            //{
            //    StockMarket currentData = new StockMarket();

            //    DataTable dtHaIndex, dtHoIndex;
            //    //CafeF.BO.MarketHelper.GetMarketTradeInfo(out dtHaIndex, out dtHoIndex);

            //    dtHaIndex = DistCache.Get<DataTable>("MemCached_HaSTCIndex");
            //    if (dtHaIndex == null || dtHaIndex.Rows.Count == 0)
            //    {
            //        //CafeF_DataService.CafeF_DataService dataServices = new CafeF_EmbedData.CafeF_DataService.CafeF_DataService();
            //        WServices.Function dataServices = new WServices.Function();
            //        dtHaIndex = dataServices.GetHaSTCIndex().Tables[0];
            //    }
            //    dtHoIndex = DistCache.Get<DataTable>("MemCached_VNIndex");
            //    if (dtHoIndex == null || dtHoIndex.Rows.Count == 0)
            //    {
            //        //CafeF_DataService.CafeF_DataService dataServices = new CafeF_EmbedData.CafeF_DataService.CafeF_DataService();
            //        WServices.Function dataServices = new WServices.Function();
            //        dtHoIndex = dataServices.GetLastVNIndex().Tables[0];
            //    }

            //    if (dtHaIndex != null && dtHaIndex.Rows.Count > 0)
            //    {
            //        currentData.HaSTCIndex_Index = Lib.Object2Double(dtHaIndex.Rows[0]["currentIndex"]);
            //        currentData.HaSTCIndex_Change = Lib.Object2Double(dtHaIndex.Rows[0]["chgIndex"]);
            //        currentData.HaSTCIndex_ChangePercent = Lib.Object2Double(dtHaIndex.Rows[0]["pctIndex"]);
            //        currentData.HaSTCIndex_TotalVolume = Lib.Object2Double(dtHaIndex.Rows[0]["totalQtty"]) * 10;
            //        currentData.HaSTCIndex_TotalValue = Lib.Object2Double(dtHaIndex.Rows[0]["totalValue"]);
            //        currentData.HaSTCIndex_TotalTrade = Lib.Object2Double(dtHaIndex.Rows[0]["totalTrade"]);
            //    }
            //    else
            //    {
            //        currentData.HaSTCIndex_Index = 0;
            //        currentData.HaSTCIndex_Change = 0;
            //        currentData.HaSTCIndex_ChangePercent = 0;
            //        currentData.HaSTCIndex_TotalVolume = 0;
            //        currentData.HaSTCIndex_TotalValue = 0;
            //        currentData.HaSTCIndex_TotalTrade = 0;
            //    }
            //    if (dtHoIndex != null && dtHoIndex.Rows.Count > 0)
            //    {
            //        currentData.VNIndex_Index = Lib.Object2Double(dtHoIndex.Rows[0]["currentIndex"]);
            //        currentData.VNIndex_Change = Lib.Object2Double(dtHoIndex.Rows[0]["chgIndex"]);
            //        currentData.VNIndex_ChangePercent = Lib.Object2Double(dtHoIndex.Rows[0]["pctIndex"]);
            //        currentData.VNIndex_TotalVolume = Lib.Object2Double(dtHoIndex.Rows[0]["totalQtty"]);
            //        currentData.VNIndex_TotalValue = Lib.Object2Double(dtHoIndex.Rows[0]["totalValue"]);
            //        currentData.VNIndex_TotalTrade = Lib.Object2Double(dtHaIndex.Rows[0]["totalTrade"]);
            //    }
            //    else
            //    {
            //        currentData.VNIndex_Index = 0;
            //        currentData.VNIndex_Change = 0;
            //        currentData.VNIndex_ChangePercent = 0;
            //        currentData.VNIndex_TotalVolume = 0;
            //        currentData.VNIndex_TotalValue = 0;
            //        currentData.VNIndex_TotalTrade = 0;
            //    }

            //    currentData.LastTradeDate = MarketHelper.UpdateTimer();
            //    currentData.TradeState = (MarketHelper.isTradingTime() ? "1" : "0");
            //    //currentData.TradeStatePrivate = (MarketHelper.isTradingTime() ? "1" : "0");

            //    return currentData;
            //}

            public double VNIndex_Index
            {
                get { return this.m_VNIndex_Index; }
                set { this.m_VNIndex_Index = value; }
            }
            public double VNIndex_Change
            {
                get { return this.m_VNIndex_Change; }
                set { this.m_VNIndex_Change = value; }
            }
            public double VNIndex_ChangePercent
            {
                get { return this.m_VNIndex_ChangePercent; }
                set { this.m_VNIndex_ChangePercent = value; }
            }
            public double VNIndex_TotalVolume
            {
                get { return this.m_VNIndex_TotalVolume; }
                set { this.m_VNIndex_TotalVolume = value; }
            }
            public double VNIndex_TotalValue
            {
                get { return this.m_VNIndex_TotalValue; }
                set { this.m_VNIndex_TotalValue = value; }
            }
            public double VNIndex_TotalTrade
            {
                get { return this.m_VNIndex_TotalTrade; }
                set { this.m_VNIndex_TotalTrade = value; }
            }

            public double HaSTCIndex_Index
            {
                get { return this.m_HaSTCIndex_Index; }
                set { this.m_HaSTCIndex_Index = value; }
            }
            public double HaSTCIndex_Change
            {
                get { return this.m_HaSTCIndex_Change; }
                set { this.m_HaSTCIndex_Change = value; }
            }
            public double HaSTCIndex_ChangePercent
            {
                get { return this.m_HaSTCIndex_ChangePercent; }
                set { this.m_HaSTCIndex_ChangePercent = value; }
            }
            public double HaSTCIndex_TotalVolume
            {
                get { return this.m_HaSTCIndex_TotalVolume; }
                set { this.m_HaSTCIndex_TotalVolume = value; }
            }
            public double HaSTCIndex_TotalValue
            {
                get { return this.m_HaSTCIndex_TotalValue; }
                set { this.m_HaSTCIndex_TotalValue = value; }
            }
            public double HaSTCIndex_TotalTrade
            {
                get { return this.m_HaSTCIndex_TotalTrade; }
                set { this.m_HaSTCIndex_TotalTrade = value; }
            }


            public string LastTradeDate
            {
                get { return this.m_LastTradeDate; }
                set { this.m_LastTradeDate = value; }
            }
            public string TradeState
            {
                get { return this.m_TradeState; }
                set { this.m_TradeState = value; }
            }
            public string TradeStatePrivate
            {
                get { return this.m_TradeStatePrivate; }
                set { this.m_TradeStatePrivate = value; }
            }
        }
        #endregion

        #region Company info
        public class CompanyInfo
        {
            public enum FilterType
            {
                NoFilter = 0,
                StockSymbol = 1
            }

            public struct CompanyInfoItem
            {
                public CompanyInfoItem(string symbol, string companyName, string tradeCenter, double price, double pe, double eps)
                {
                    this.Symbol = symbol;
                    this.CompanyName = companyName;
                    this.TradeCenter = tradeCenter;
                    this.Price = price;
                    this.PE = pe;
                    this.EPS = eps;
                }
                public string Symbol, CompanyName, TradeCenter;
                public double Price, PE, EPS;
            }

            public static CompanyInfo SearchCompany(int tradeId, int industryId, FilterType filterType, string keyword, int pageIndex, int pageSize)
            {
                CompanyInfo currentData = new CompanyInfo();

                //if (!string.IsNullOrEmpty(keyword))
                //{
                //    keyword = keyword.Replace("'", "");
                //}
                //else
                //{
                //    keyword = "";
                //}

                //CompanyHelper_Update company = new CompanyHelper_Update();
                //currentData.RecordCount = company.Select_Count_TradeCenter(keyword, industryId, tradeId, (int)filterType);
                //currentData.PageIndex = pageIndex;
                //if (currentData.RecordCount > 1)
                //{
                //    currentData.PageCount = ((int)(currentData.RecordCount - 1) / pageSize) + 1;
                //}
                //else
                //{
                //    currentData.PageCount = 1;
                //}

                //if (pageIndex <= 0)
                //{
                //    currentData.PageIndex = 1;
                //}
                //else
                //{
                //    if (pageIndex > currentData.PageCount)
                //    {
                //        currentData.PageIndex = currentData.PageCount;
                //    }
                //    else
                //    {
                //        currentData.PageIndex = pageIndex;
                //    }
                //}

                //using (DataTable dtCompanies = company.GetTradeCenter(tradeId, keyword, industryId, currentData.PageIndex, pageSize, (int)filterType))
                //{
                //    List<CompanyInfoItem> items = new List<CompanyInfoItem>();
                //    if (dtCompanies != null)
                //    {
                //        using (DataTable dtCompaniesIncludePrice = GetOnlineInfo(dtCompanies))
                //        {
                //            for (int i = 0; i < dtCompaniesIncludePrice.Rows.Count; i++)
                //            {
                //                items.Add(new CompanyInfoItem(dtCompaniesIncludePrice.Rows[i]["StockSymbol"].ToString(),
                //                                                dtCompaniesIncludePrice.Rows[i]["Fullname"].ToString(),
                //                                                dtCompaniesIncludePrice.Rows[i]["Symbol"].ToString(),
                //                                                Lib.Object2Double(dtCompaniesIncludePrice.Rows[i]["CurrentPrice"]),
                //                                                Lib.Object2Double(dtCompaniesIncludePrice.Rows[i]["PE"]),
                //                                                Lib.Object2Double(dtCompaniesIncludePrice.Rows[i]["EPS"])
                //                                                ));
                //            }
                //        }
                //    }
                //    currentData.CompanyInfos = items.ToArray();
                //}
                //return currentData;
                var items = new List<CompanyInfoItem>();
                var key = tradeId > 0 ? string.Format(RedisKey.KeyStockListByCenter, tradeId) : RedisKey.KeyStockList;

                var sds = BLFACTORY.RedisClient.Get<List<StockCompact>>(key);
                sds = sds.FindAll(s => s.Symbol.Contains(keyword) && (industryId == -1||s.CategoryId==industryId));
                if(filterType==FilterType.StockSymbol)
                {
                    currentData.RecordCount = sds.Count;
                    //Neu loc theo chu cai
                    sds = sds.GetPaging(pageIndex, pageSize);
                    
                }
                var ss = new List<string>();
                Dictionary<string, StockCompactInfo> infos;
                if (filterType == FilterType.NoFilter && !string.IsNullOrEmpty(keyword))
                {
                    foreach (var sym in sds)
                    {
                        ss.Add(sym.Symbol);
                    }
                    infos = StockBL.GetStockCompactInfoMultiple(ss);
                    foreach (var info in infos)
                    {
                        if (info.Value == null) continue;
                        var sym = info.Value.Symbol;
                        //ss.Remove(info.Value.Symbol);
                        var index = sds.FindIndex(s=>s.Symbol==sym);
                        if(index >0) sds.RemoveAt(index);
                    }
                    currentData.RecordCount = sds.Count;
                    sds = sds.GetPaging(pageIndex, pageSize);
                    
                }
                if (sds.Count > pageSize)
                {
                    currentData.RecordCount = sds.Count;
                    sds = sds.GetPaging(pageIndex, pageSize);
                }
                ss = new List<string>();
                foreach (var sym in sds)
                {
                    ss.Add(sym.Symbol);
                }
                infos = StockBL.GetStockCompactInfoMultiple(ss);
                var prices = StockBL.GetStockPriceMultiple(ss);
                foreach (var sym in sds)
                {
                    var item = new CompanyInfoItem {Symbol = sym.Symbol, TradeCenter = Utils.GetCenterName(sym.TradeCenterId.ToString()), Price = 0, CompanyName = ""};
                    if (prices.ContainsKey(sym.Symbol))
                    {
                        var price = prices[sym.Symbol];
                        if (price != null)
                        {
                            item.Price = price.Price;
                        }
                    }
                    if (infos.ContainsKey(sym.Symbol))
                    {
                        var info = infos[sym.Symbol];
                        if (info != null)
                        {
                            item.CompanyName = info.CompanyName;
                            item.EPS = Math.Round(info.EPS,2);
                            item.PE = (info.EPS == 0) ? 0 : Math.Round(item.Price / info.EPS, 2);
                        }
                    }
                    if(string.IsNullOrEmpty(item.CompanyName)) continue;
                    items.Add(item);
                }
                
                currentData.PageIndex = pageIndex;
                currentData.PageCount = (int)Math.Ceiling((double)currentData.RecordCount / pageSize);
                currentData.CompanyInfos = items.ToArray();
                return currentData;
            }
            public static CompanyInfo GetAllCompanyByTradeId(int tradeId)
            {
                CompanyInfo currentData = new CompanyInfo();

                //CompanyHelper_Update company = new CompanyHelper_Update();
                //currentData.RecordCount = company.Select_Count_TradeCenter("", -1, tradeId, (int)FilterType.NoFilter);
                //currentData.PageIndex = 1;
                //currentData.PageCount = 1;

                //using (DataTable dtCompanies = company.GetTradeCenter(tradeId.ToString()))
                //{
                //    List<CompanyInfoItem> items = new List<CompanyInfoItem>();
                //    if (dtCompanies != null)
                //    {
                //        using (DataTable dtCompaniesIncludePrice = GetOnlineInfo(dtCompanies))
                //        {
                //            for (int i = 0; i < dtCompaniesIncludePrice.Rows.Count; i++)
                //            {
                //                if (Lib.Object2Double(dtCompaniesIncludePrice.Rows[i]["CurrentPrice"]) == 0)
                //                {
                //                    items.Add(new CompanyInfoItem(dtCompaniesIncludePrice.Rows[i]["StockSymbol"].ToString(),
                //                                                       dtCompaniesIncludePrice.Rows[i]["Fullname"].ToString(),
                //                                                       dtCompaniesIncludePrice.Rows[i]["Symbol"].ToString(),
                //                                                       Lib.Object2Double(dtCompaniesIncludePrice.Rows[i]["BasicPrice"]),
                //                                                       Lib.Object2Double(dtCompaniesIncludePrice.Rows[i]["PE"]),
                //                                                       Lib.Object2Double(dtCompaniesIncludePrice.Rows[i]["EPS"])
                //                                                       ));
                //                }
                //                else
                //                {
                //                    items.Add(new CompanyInfoItem(dtCompaniesIncludePrice.Rows[i]["StockSymbol"].ToString(),
                //                                                    dtCompaniesIncludePrice.Rows[i]["Fullname"].ToString(),
                //                                                    dtCompaniesIncludePrice.Rows[i]["Symbol"].ToString(),
                //                                                    Lib.Object2Double(dtCompaniesIncludePrice.Rows[i]["CurrentPrice"]),
                //                                                    Lib.Object2Double(dtCompaniesIncludePrice.Rows[i]["PE"]),
                //                                                    Lib.Object2Double(dtCompaniesIncludePrice.Rows[i]["EPS"])
                //                                                    )
                //                                                    );
                //                }
                //            }
                //        }
                //    }
                //    currentData.CompanyInfos = items.ToArray();
                //}
                var items = new List<CompanyInfoItem>();
                var key = tradeId > 0 ? string.Format(RedisKey.KeyStockListByCenter, tradeId): RedisKey.KeyStockList;
                
                var sds = BLFACTORY.RedisClient.Get<List<StockCompact>>(key);
                var ss = new List<string>();
                foreach (var sym in sds)
                {
                    ss.Add(sym.Symbol);
                }
                var infos = StockBL.GetStockCompactInfoMultiple(ss);
                var prices = StockBL.GetStockPriceMultiple(ss);
                foreach (var sym in sds)
                {
                    var item = new CompanyInfoItem();
                    item.Symbol = sym.Symbol;
                    item.TradeCenter = Utils.GetCenterName(sym.TradeCenterId.ToString());
                    if(prices.ContainsKey(sym.Symbol))
                    {
                        var price = prices[sym.Symbol];
                        if(price!=null)
                        {
                            item.Price = price.Price;
                        }
                    }
                    if(infos.ContainsKey(sym.Symbol))
                    {
                        var info = infos[sym.Symbol];
                        if(info!=null)
                        {
                            item.CompanyName = info.CompanyName;
                            item.EPS = Math.Round(info.EPS, 2);
                            item.PE = (info.EPS == 0) ? 0 : Math.Round(item.Price / info.EPS, 2);
                        }
                    }
                        items.Add(item);    
                    
                }
                currentData.RecordCount = items.Count;
                currentData.PageIndex = 1;
                currentData.PageCount = 1;
                currentData.CompanyInfos = items.ToArray();
                return currentData;
            }

            private CompanyInfoItem[] m_CompanyInfos;
            private int m_PageCount, m_PageIndex, m_RecordCount;

            public CompanyInfoItem[] CompanyInfos
            {
                get
                {
                    return this.m_CompanyInfos;
                }
                set
                {
                    this.m_CompanyInfos = value;
                }
            }
            public int PageCount
            {
                get
                {
                    return this.m_PageCount;
                }
                set
                {
                    this.m_PageCount = value;
                }
            }
            public int PageIndex
            {
                get
                {
                    return this.m_PageIndex;
                }
                set
                {
                    this.m_PageIndex = value;
                }
            }
            public int RecordCount
            {
                get
                {
                    return this.m_RecordCount;
                }
                set
                {
                    this.m_RecordCount = value;
                }
            }

            //#region Các hàm tính toán P/E , Price
            //private static DataTable GetOnlineInfo(DataTable table)
            //{
            //    try
            //    {
            //        table.Columns.Add("CurrentPrice", typeof(double));
            //        table.Columns.Add("PE", typeof(double));
            //        table.Columns.Add("EPS", typeof(double));
            //    }
            //    catch { }
            //    KenhF.Common.FinanceChannelDB.StockTradeInfoDataTable tradeInfo = new KenhF.Common.FinanceChannelDB.StockTradeInfoDataTable();

            //    DataTable dtHASTC = MarketHelper.GetStockTradeInfo("HASTC");
            //    DataTable dtHOSTC = MarketHelper.GetStockTradeInfo("HOSTC");
            //    tradeInfo.Merge(dtHASTC);
            //    tradeInfo.Merge(dtHOSTC);

            //    foreach (DataRow row in table.Rows)
            //    {
            //        double curPrice = GetCurrentPrice(row["StockSymbol"].ToString(), tradeInfo);
            //        row["CurrentPrice"] = curPrice;
            //        row["PE"] = GetPE(row["StockSymbol"].ToString(), curPrice);
            //        row["EPS"] = GetEPS(row["StockSymbol"].ToString(), curPrice);
            //    }
            //    return table;
            //}
            ///// <summary>
            ///// Tinh Pirce
            ///// </summary>
            ///// <param name="StockSymbol"></param>
            ///// <param name="tradeInfo"></param>
            ///// <returns></returns>
            //private static double GetCurrentPrice(string StockSymbol, KenhF.Common.FinanceChannelDB.StockTradeInfoDataTable tradeInfo)
            //{
            //    //stockType=2: hastc; 1: hostc

            //    DataRow[] row = tradeInfo.Select("code='" + StockSymbol + "'");
            //    if (row != null && row.Length > 0)
            //    {
            //        //int stockType = Convert.ToInt32(row[0]["stockType"]);
            //        //if (stockType == 2)//san ha noi
            //        //{
            //        //    return GO.IsInTradingTime ? Math.Round(Convert.ToDouble(row[0]["currentIndex"]), 1) : Math.Round(Convert.ToDouble(row[0]["averagePrice"]), 1);
            //        //}
            //        //else
            //        if (Convert.ToDouble(row[0]["currentPrice"]) == 0)
            //        {
            //            return Math.Round(Convert.ToDouble(row[0]["basicPrice"]), 1);
            //        }
            //        else
            //        {
            //            return Math.Round(Convert.ToDouble(row[0]["currentPrice"]), 1);
            //        }
            //    }
            //    return 0;
            //}

            ///// <summary>
            ///// Tinh P/E
            ///// </summary>
            ///// <param name="StockSymbol"></param>
            ///// <param name="CurrPrice"></param>
            ///// <returns></returns>
            //private static double GetPE(string StockSymbol, double CurrPrice)
            //{
            //    double pe = 0;
            //    double eps = 0;
            //    try
            //    {
            //        DataTable dt = DistCache.Get<DataTable>("MemCached_CompanyInfo_" + StockSymbol);

            //        if (null == dt || dt.Rows.Count == 0)
            //        {
            //            //CafeF_DataService.CafeF_DataService dataServices = new CafeF_EmbedData.CafeF_DataService.CafeF_DataService();
            //            WServices.Function dataServices = new WServices.Function();
            //            DataSet ds = dataServices.GetCompanyInfo(StockSymbol);
            //            if (null != ds && ds.Tables.Count > 0)
            //            {
            //                dt = ds.Tables[0];
            //            }
            //        }

            //        if (null != dt)
            //        {
            //            //eps = Lib.Object2Double(dt.Rows[0]["EPS"]);
            //            pe = Lib.Object2Double(dt.Rows[0]["PE"]);
            //        }
            //        // if (eps > 0) pe = CurrPrice / eps;

            //        //string m_RootFinanceDataPath = ConfigurationManager.AppSettings["FinanceStatementDataFile"];
            //        //if (!m_RootFinanceDataPath.EndsWith(@"\")) m_RootFinanceDataPath += @"\";
            //        //using (FinanceStatementData fsCompany = new FinanceStatementData(m_RootFinanceDataPath + StockSymbol + @"\company.xml"))
            //        //{

            //        //    using (DataView dvCompany = fsCompany.DefaultView)
            //        //    {
            //        //        if (dvCompany != null && dvCompany.Count > 0)
            //        //        {
            //        //            double eps = NewsHelper.ConvertToDouble(dvCompany[0][FinanceStatementData.FIELD_EPS]);
            //        //            if (eps > 0) pe = CurrPrice / eps;
            //        //        }
            //        //    }
            //        //}
            //    }
            //    catch
            //    {
            //    }
            //    return Math.Round(pe, 1);
            //}

            //private static double GetEPS(string StockSymbol, double CurrPrice)
            //{
            //    double pe = 0;
            //    double eps = 0;
            //    try
            //    {
            //        DataTable dt = DistCache.Get<DataTable>("MemCached_CompanyInfo_" + StockSymbol);

            //        if (null == dt || dt.Rows.Count == 0)
            //        {
            //            //CafeF_DataService.CafeF_DataService dataServices = new CafeF_EmbedData.CafeF_DataService.CafeF_DataService();
            //            WServices.Function dataServices = new WServices.Function();
            //            DataSet ds = dataServices.GetCompanyInfo(StockSymbol);
            //            if (null != ds && ds.Tables.Count > 0)
            //            {
            //                dt = ds.Tables[0];
            //            }
            //        }

            //        if (null != dt)
            //        {
            //            //eps = Lib.Object2Double(dt.Rows[0]["EPS"]);
            //            pe = Lib.Object2Double(dt.Rows[0]["EPS"]);
            //        }
            //        // if (eps > 0) pe = CurrPrice / eps;

            //        //string m_RootFinanceDataPath = ConfigurationManager.AppSettings["FinanceStatementDataFile"];
            //        //if (!m_RootFinanceDataPath.EndsWith(@"\")) m_RootFinanceDataPath += @"\";
            //        //using (FinanceStatementData fsCompany = new FinanceStatementData(m_RootFinanceDataPath + StockSymbol + @"\company.xml"))
            //        //{

            //        //    using (DataView dvCompany = fsCompany.DefaultView)
            //        //    {
            //        //        if (dvCompany != null && dvCompany.Count > 0)
            //        //        {
            //        //            double eps = NewsHelper.ConvertToDouble(dvCompany[0][FinanceStatementData.FIELD_EPS]);
            //        //            if (eps > 0) pe = CurrPrice / eps;
            //        //        }
            //        //    }
            //        //}
            //    }
            //    catch
            //    {
            //    }
            //    return Math.Round(pe, 1);
            //}
            //#endregion
        }
        #endregion

        #region StockSymbols Fulldata
        private class StockSymbol_FullDynamicData
        {
            public enum TradeCenter
            {
                HoSE = 1,
                HaSTC = 2
            }
            private enum HoSE_FieldName
            {
                Ref = 0,
                Cei = 1,
                Flo = 2,
                TraP = 3,
                TraV = 4,
                BidP1 = 5,
                BidV1 = 6,
                BidP2 = 7,
                BidV2 = 8,
                BidP3 = 9,
                BidV3 = 10,
                SelP1 = 11,
                SelV1 = 12,
                SelP2 = 13,
                SelV2 = 14,
                SelP3 = 15,
                SelV3 = 16,
                Pri1 = 17,
                Max = 18,
                Min = 19,
                Chg = 20,
                Img = 21
            }
            private enum HaSTC_FieldName
            {
                Ref = 0,
                Cei = 1,
                Flo = 2,
                TraP = 3,
                TraV = 4,
                BidP1 = 5,
                BidV1 = 6,
                BidP2 = 7,
                BidV2 = 8,
                BidP3 = 9,
                BidV3 = 10,
                SelP1 = 11,
                SelV1 = 12,
                SelP2 = 13,
                SelV2 = 14,
                SelP3 = 15,
                SelV3 = 16,
                TTV = 17,
                Max = 18,
                Min = 19,
                Chg = 20,
                Img = 21
            }

            public struct SymbolItem
            {
                public SymbolItem(string symbol, params string[] datas)
                {
                    this.Symbol = symbol;
                    Datas = datas;
                }
                public string Symbol;
                public string[] Datas;
            }
            public struct IndexItem
            {
                public IndexItem(string index, string quantity, string volume, string value)
                {
                    Index = index;
                    Quantity = quantity;
                    Volume = volume;
                    Value = value;
                }
                public string Index, Quantity, Volume, Value;
            }

            public static StockSymbol_FullDynamicData GetCurrentData(TradeCenter tradeCenter, string symbols, int full)
            {
                StockSymbol_FullDynamicData currentData = new StockSymbol_FullDynamicData();
                try
                {
                    bool isHOChange = false, isHAChange = false;
                    int currentHOChangeId = full, currentHAChangeId = full;

                    GetDatabaseState(ref currentHOChangeId, ref currentHAChangeId, ref isHOChange, ref isHAChange);

                    if (tradeCenter == TradeCenter.HoSE)
                    {
                        #region Lay du lieu HoSE
                        DataTable dtPriceData = new DataTable();
                        if (symbols != "" && (isHOChange || full == -1)) dtPriceData = GetHOSEDatatable_MemCache(symbols, (full == -1));

                        DataTable dtIndexData = new DataTable();
                        dtIndexData = GetHOSEIndexDatatable_MemCache();

                        currentData.ChangeId = currentHOChangeId;

                        #region Index
                        currentData.SessionId = 0;
                        currentData.Indexs = new IndexItem[3];

                        if (dtIndexData.Rows.Count > 0)
                        {
                            #region Xác định phiên
                            if (Lib.FormatDouble(dtIndexData.Rows[0]["Index3"]) != "0") // Phiên 3
                            {
                                currentData.SessionId = 3;
                            }
                            else if (Lib.FormatDouble(dtIndexData.Rows[0]["Index2"]) != "0") // Phiên 2
                            {
                                currentData.SessionId = 2;
                            }
                            else if (Lib.FormatDouble(dtIndexData.Rows[0]["Index1"]) != "0") // Phiên 1
                            {
                                currentData.SessionId = 1;
                            }
                            #endregion

                            currentData.PreviousIndex = Lib.FormatDouble(dtIndexData.Rows[0]["PrevIndex"]);

                            currentData.Indexs[0].Index = Lib.FormatDouble(dtIndexData.Rows[0]["Index1"]);
                            currentData.Indexs[0].Quantity = Lib.FormatDouble(dtIndexData.Rows[0]["Quantity1"]);
                            currentData.Indexs[0].Volume = Lib.FormatDouble(dtIndexData.Rows[0]["Vol1"]);
                            currentData.Indexs[0].Value = Lib.FormatDouble(dtIndexData.Rows[0]["Value1"]);

                            currentData.Indexs[1].Index = Lib.FormatDouble(dtIndexData.Rows[0]["Index2"]);
                            currentData.Indexs[1].Quantity = Lib.FormatDouble(dtIndexData.Rows[0]["Quantity2"]);
                            currentData.Indexs[1].Volume = Lib.FormatDouble(dtIndexData.Rows[0]["Vol2"]);
                            currentData.Indexs[1].Value = Lib.FormatDouble(dtIndexData.Rows[0]["Value2"]);

                            currentData.Indexs[2].Index = Lib.FormatDouble(dtIndexData.Rows[0]["Index3"]);
                            currentData.Indexs[2].Quantity = Lib.FormatDouble(dtIndexData.Rows[0]["Quantity3"]);
                            currentData.Indexs[2].Volume = Lib.FormatDouble(dtIndexData.Rows[0]["Vol3"]);
                            currentData.Indexs[2].Value = Lib.FormatDouble(dtIndexData.Rows[0]["Value3"]);
                        }
                        else
                        {
                            currentData.PreviousIndex = "0";

                            currentData.Indexs[0].Index = "0";
                            currentData.Indexs[0].Quantity = "0";
                            currentData.Indexs[0].Volume = "0";
                            currentData.Indexs[0].Value = "0";

                            currentData.Indexs[1].Index = "0";
                            currentData.Indexs[1].Quantity = "0";
                            currentData.Indexs[1].Volume = "0";
                            currentData.Indexs[1].Value = "0";

                            currentData.Indexs[2].Index = "0";
                            currentData.Indexs[2].Quantity = "0";
                            currentData.Indexs[2].Volume = "0";
                            currentData.Indexs[2].Value = "0";
                        }
                        #endregion

                        #region Thông tin giá hiện tại
                        List<SymbolItem> items = new List<SymbolItem>();
                        foreach (DataRow drPriceData in dtPriceData.Rows)
                        {
                            SymbolItem symbol = new SymbolItem();

                            symbol.Symbol = drPriceData["Symbol"].ToString();

                            symbol.Datas = new string[22];

                            symbol.Datas[(int)HoSE_FieldName.Ref] = Lib.FormatDouble(drPriceData["Ref"]);
                            symbol.Datas[(int)HoSE_FieldName.Cei] = Lib.FormatDouble(drPriceData["Ceiling"]);
                            symbol.Datas[(int)HoSE_FieldName.Flo] = Lib.FormatDouble(drPriceData["Floor"]);

                            symbol.Datas[(int)HoSE_FieldName.TraP] = FormatPrice(drPriceData["TradingPrice"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HoSE_FieldName.TraV] = FormatVolume(drPriceData["TradingVol"], drPriceData["TradingPrice"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);

                            string BidPrice1 = (drPriceData["BidPrice1"] == null ? "" : drPriceData["BidPrice1"].ToString());
                            string BidVol1 = (drPriceData["BidVol1"] == null ? "" : drPriceData["BidVol1"].ToString());
                            if (BidPrice1 == "" || BidPrice1 == "0")
                            {
                                if (BidVol1 == "" || BidVol1 == "0")
                                {
                                    BidPrice1 = "";
                                }
                                else
                                {
                                    if (currentData.SessionId == 1)
                                    {
                                        BidPrice1 = "ATO";
                                    }
                                    else if (currentData.SessionId == 3)
                                    {
                                        BidPrice1 = "ATC";
                                    }
                                    else
                                    {
                                        BidPrice1 = "";
                                    }
                                }
                            }
                            else
                            {
                                BidPrice1 = drPriceData["BidPrice1"].ToString();
                            }

                            symbol.Datas[(int)HoSE_FieldName.BidP1] = FormatPrice(BidPrice1, drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HoSE_FieldName.BidV1] = FormatVolume(drPriceData["BidVol1"], BidPrice1, drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HoSE_FieldName.BidP2] = FormatPrice(drPriceData["BidPrice2"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HoSE_FieldName.BidV2] = FormatVolume(drPriceData["BidVol2"], drPriceData["BidPrice2"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HoSE_FieldName.BidP3] = FormatPrice(drPriceData["BidPrice3"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HoSE_FieldName.BidV3] = FormatVolume(drPriceData["BidVol3"], drPriceData["BidPrice3"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);

                            string SellPrice1 = (drPriceData["SellPrice1"] == null ? "" : drPriceData["SellPrice1"].ToString());
                            string SellVol1 = (drPriceData["SellVol1"] == null ? "" : drPriceData["SellVol1"].ToString());
                            if (SellPrice1 == "" || SellPrice1 == "0")
                            {
                                if (SellVol1 == "" || SellVol1 == "0")
                                {
                                    SellPrice1 = "";
                                }
                                else
                                {
                                    if (currentData.SessionId == 1)
                                    {
                                        SellPrice1 = "ATO";
                                    }
                                    else if (currentData.SessionId == 3)
                                    {
                                        SellPrice1 = "ATC";
                                    }
                                    else
                                    {
                                        SellPrice1 = "";
                                    }
                                }
                            }
                            else
                            {
                                SellPrice1 = drPriceData["SellPrice1"].ToString();
                            }

                            symbol.Datas[(int)HoSE_FieldName.SelP1] = FormatPrice(SellPrice1, drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HoSE_FieldName.SelV1] = FormatVolume(drPriceData["SellVol1"], SellPrice1, drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HoSE_FieldName.SelP2] = FormatPrice(drPriceData["SellPrice2"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HoSE_FieldName.SelV2] = FormatVolume(drPriceData["SellVol2"], drPriceData["SellPrice2"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HoSE_FieldName.SelP3] = FormatPrice(drPriceData["SellPrice3"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HoSE_FieldName.SelV3] = FormatVolume(drPriceData["SellVol3"], drPriceData["SellPrice3"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);

                            symbol.Datas[(int)HoSE_FieldName.Pri1] = FormatPrice(drPriceData["TradingPrice1"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);

                            symbol.Datas[(int)HoSE_FieldName.Max] = FormatPrice(drPriceData["TradingPriceMax"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HoSE_FieldName.Min] = FormatPrice(drPriceData["TradingPriceMin"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);

                            FormatChangeValue(out symbol.Datas[(int)HoSE_FieldName.Chg], out symbol.Datas[(int)HoSE_FieldName.Img], drPriceData["TradingPrice"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);

                            items.Add(symbol);
                        }
                        currentData.Symbols = items.ToArray();
                        #endregion
                        dtIndexData.Dispose();
                        dtPriceData.Dispose();
                        #endregion
                    }
                    else
                    {
                        #region Lay du lieu HaSTC
                        DataTable dtPriceData = new DataTable();
                        if (symbols != "" && (isHAChange || full == -1)) dtPriceData = GetHASTCDatatable_MemCache(symbols, (full == -1));

                        DataTable dtIndexData = new DataTable();
                        dtIndexData = GetHASTCIndexDatatable_MemCache();

                        currentData.ChangeId = currentHAChangeId;

                        #region Index
                        currentData.SessionId = 0;
                        currentData.Indexs = new IndexItem[1];

                        if (dtIndexData.Rows.Count > 0)
                        {
                            currentData.PreviousIndex = Lib.FormatDouble(dtIndexData.Rows[0]["PrevIndex"]);

                            currentData.Indexs[0].Index = Lib.FormatDouble(dtIndexData.Rows[0]["Index"]);
                            currentData.Indexs[0].Quantity = Lib.FormatDouble(dtIndexData.Rows[0]["Quantity"]);
                            currentData.Indexs[0].Volume = Lib.FormatDouble(dtIndexData.Rows[0]["Vol"]);
                            currentData.Indexs[0].Value = Lib.FormatDouble(dtIndexData.Rows[0]["Value"]);
                        }
                        else
                        {
                            currentData.PreviousIndex = "0";

                            currentData.Indexs[0].Index = "0";
                            currentData.Indexs[0].Quantity = "0";
                            currentData.Indexs[0].Volume = "0";
                            currentData.Indexs[0].Value = "0";
                        }
                        #endregion

                        #region Thông tin giá hiện tại
                        List<SymbolItem> items = new List<SymbolItem>();
                        foreach (DataRow drPriceData in dtPriceData.Rows)
                        {
                            SymbolItem symbol = new SymbolItem();

                            symbol.Symbol = drPriceData["Symbol"].ToString();

                            symbol.Datas = new string[22];

                            symbol.Datas[(int)HaSTC_FieldName.Ref] = Lib.FormatDouble(drPriceData["Ref"]);
                            symbol.Datas[(int)HaSTC_FieldName.Cei] = Lib.FormatDouble(drPriceData["Ceiling"]);
                            symbol.Datas[(int)HaSTC_FieldName.Flo] = Lib.FormatDouble(drPriceData["Floor"]);

                            symbol.Datas[(int)HaSTC_FieldName.TraP] = FormatPrice(drPriceData["TradingPrice"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HaSTC_FieldName.TraV] = FormatVolume(drPriceData["TradingVol"], drPriceData["TradingPrice"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);

                            symbol.Datas[(int)HaSTC_FieldName.BidP1] = FormatPrice(drPriceData["BidPrice1"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HaSTC_FieldName.BidV1] = FormatVolume(drPriceData["BidVol1"], drPriceData["BidPrice1"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HaSTC_FieldName.BidP2] = FormatPrice(drPriceData["BidPrice2"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HaSTC_FieldName.BidV2] = FormatVolume(drPriceData["BidVol2"], drPriceData["BidPrice2"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HaSTC_FieldName.BidP3] = FormatPrice(drPriceData["BidPrice3"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HaSTC_FieldName.BidV3] = FormatVolume(drPriceData["BidVol3"], drPriceData["BidPrice3"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);

                            symbol.Datas[(int)HaSTC_FieldName.SelP1] = FormatPrice(drPriceData["SellPrice1"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HaSTC_FieldName.SelV1] = FormatVolume(drPriceData["SellVol1"], drPriceData["SellPrice1"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HaSTC_FieldName.SelP2] = FormatPrice(drPriceData["SellPrice2"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HaSTC_FieldName.SelV2] = FormatVolume(drPriceData["SellVol2"], drPriceData["SellPrice2"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HaSTC_FieldName.SelP3] = FormatPrice(drPriceData["SellPrice3"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HaSTC_FieldName.SelV3] = FormatVolume(drPriceData["SellVol3"], drPriceData["SellPrice3"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);

                            symbol.Datas[(int)HaSTC_FieldName.TTV] = Lib.FormatDouble(drPriceData["TotalTradingVolume"]);

                            symbol.Datas[(int)HaSTC_FieldName.Max] = FormatPrice(drPriceData["TradingPriceMax"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);
                            symbol.Datas[(int)HaSTC_FieldName.Min] = FormatPrice(drPriceData["TradingPriceMin"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);

                            FormatChangeValue(out symbol.Datas[(int)HaSTC_FieldName.Chg], out symbol.Datas[(int)HaSTC_FieldName.Img], drPriceData["TradingPrice"], drPriceData["Ceiling"], drPriceData["Floor"], drPriceData["Ref"]);

                            items.Add(symbol);
                        }
                        currentData.Symbols = items.ToArray();
                        #endregion
                        dtIndexData.Dispose();
                        dtPriceData.Dispose();
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    Lib.WriteLog(ex);
                }
                return currentData;
            }

            #region Method lay thong tin chung khoan 2 san
            private static DataTable GetHOSEDatatable_MemCache(string symbols, bool refreshCache)
            {
                try
                {
                    DataTable dtPriceData = new DataTable();
                    if (symbols != "")
                    {
                        if (symbols.StartsWith(";")) symbols = symbols.Substring(1);
                        if (symbols.EndsWith(";")) symbols = symbols.Substring(0, symbols.Length - 1);

                        string[] symbolList = symbols.Split(';');

                        string cacheName = "MemCached_tblHCMPriceBoard";

                        DataTable dtHOPriceData = DistCache.Get<DataTable>(cacheName);

                        dtPriceData = dtHOPriceData.Clone();

                        for (int i = 0; i < dtHOPriceData.Rows.Count; i++)
                        {
                            if (IsValueInArray(dtHOPriceData.Rows[i]["Symbol"].ToString(), symbolList))
                            {
                                dtPriceData.Rows.Add(dtHOPriceData.Rows[i].ItemArray);
                            }
                        }
                    }
                    return dtPriceData;
                }
                catch
                {
                    return new DataTable();
                }
            }
            private static DataTable GetHASTCDatatable_MemCache(string symbols, bool refreshCache)
            {
                try
                {
                    DataTable dtPriceData = new DataTable();
                    if (symbols != "")
                    {
                        if (symbols.StartsWith(";")) symbols = symbols.Substring(1);
                        if (symbols.EndsWith(";")) symbols = symbols.Substring(0, symbols.Length - 1);

                        string[] symbolList = symbols.Split(';');

                        string cacheName = "MemCached_tblHNPriceBoard";

                        DataTable dtHAPriceData = DistCache.Get<DataTable>(cacheName);

                        dtPriceData = dtHAPriceData.Clone();

                        for (int i = 0; i < dtHAPriceData.Rows.Count; i++)
                        {
                            if (IsValueInArray(dtHAPriceData.Rows[i]["Symbol"].ToString(), symbolList))
                            {
                                dtPriceData.Rows.Add(dtHAPriceData.Rows[i].ItemArray);
                            }
                        }
                    }
                    return dtPriceData;
                }
                catch
                {
                    return new DataTable();
                }
            }
            private static DataTable GetHOSEIndexDatatable_MemCache()
            {
                return DistCache.Get<DataTable>("MemCached_tblHCMIndex");
            }
            private static DataTable GetHASTCIndexDatatable_MemCache()
            {
                return DistCache.Get<DataTable>("MemCached_tblHNIndex");
            }

            private static void GetDatabaseState(ref int currentHOChangeId, ref int currentHAChangeId, ref bool isHOChange, ref bool isHAChange)
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["VincomscPriceBoardConnectionString"].ConnectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT tableName, changeId FROM AspNet_SqlCacheTablesForChangeNotification", cnn))
                    {
                        using (DataTable dtDatabaseState = new DataTable())
                        {
                            adapter.Fill(dtDatabaseState);

                            foreach (DataRow drDatabaseState in dtDatabaseState.Rows)
                            {
                                if (drDatabaseState["tableName"].ToString() == "tblHCMPriceBoard")
                                {
                                    isHOChange = (currentHOChangeId.ToString() != drDatabaseState["changeId"].ToString());
                                    currentHOChangeId = Convert.ToInt32(drDatabaseState["changeId"]);
                                }
                                else if (drDatabaseState["tableName"].ToString() == "tblHNPriceBoard")
                                {
                                    isHAChange = (currentHAChangeId.ToString() != drDatabaseState["changeId"].ToString());
                                    currentHAChangeId = Convert.ToInt32(drDatabaseState["changeId"]);
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            private static string FormatPrice(object giaHT, object giatran, object giaSan, object giaTC)
            {
                if (giaHT != null && (giaHT.ToString() == "ATO" || giaHT.ToString() == "ATC")) return giaHT.ToString();
                if (Lib.Object2Float(giaHT) == 0) return "";

                double giaHT1 = Lib.Object2Float(giaHT);
                double giatran1 = Lib.Object2Float(giatran);
                double giaSan1 = Lib.Object2Float(giaSan);
                double giaTC1 = Lib.Object2Float(giaTC);

                string className = "ref";

                if (giaTC1 == 0)
                {
                    className = "ref";
                }
                else
                {
                    if (giaHT1 > giaTC1)
                    {
                        if (giaHT1 == giatran1)
                        {
                            className = "ceiling";
                        }
                        else
                        {
                            className = "up";
                        }
                    }
                    else if (giaHT1 < giaTC1)
                    {
                        if (giaHT1 == giaSan1)
                        {
                            className = "floor";
                        }
                        else
                        {
                            className = "down";
                        }
                    }
                }

                return "<span class='" + className + "'>" + Lib.FormatDouble(giaHT1) + "</span>";
            }

            private static string FormatVolume(object khoiluong, object giaHT, object giatran, object giaSan, object giaTC)
            {
                if (Lib.Object2Float(khoiluong) == 0) return "";

                double giaHT1 = Lib.Object2Float(giaHT);
                double giatran1 = Lib.Object2Float(giatran);
                double giaSan1 = Lib.Object2Float(giaSan);
                double giaTC1 = Lib.Object2Float(giaTC);

                string className = "ref";

                if (giaTC1 == 0)
                {
                    className = "ref";
                }
                else
                {
                    if (giaHT1 > giaTC1)
                    {
                        if (giaHT1 == giatran1)
                        {
                            className = "ceiling";
                        }
                        else
                        {
                            className = "up";
                        }
                    }
                    else if (giaHT1 < giaTC1)
                    {
                        if (giaHT1 == giaSan1)
                        {
                            className = "floor";
                        }
                        else
                        {
                            className = "down";
                        }
                    }
                }
                return "<span class='" + className + "'>" + Lib.FormatDouble(khoiluong) + "</span>";
            }

            private static void FormatChangeValue(out string thayDoi, out string anh, object giaHT, object giatran, object giaSan, object giaTC)
            {
                if (Lib.Object2Float(giaHT) == 0)
                {
                    thayDoi = "";
                    anh = "blank";
                    return;
                }
                double giaHT1 = Lib.Object2Float(giaHT);
                double giatran1 = Lib.Object2Float(giatran);
                double giaSan1 = Lib.Object2Float(giaSan);
                double giaTC1 = Lib.Object2Float(giaTC);

                string sign = "";
                string className = "ref";

                if (giaTC1 == 0)
                {
                    className = "ref";
                }
                else
                {
                    if (giaHT1 > giaTC1)
                    {
                        sign = "+";
                        if (giaHT1 == giatran1)
                        {
                            className = "ceiling";
                        }
                        else
                        {
                            className = "up";
                        }
                    }
                    else if (giaHT1 < giaTC1)
                    {
                        if (giaHT1 == giaSan1)
                        {
                            className = "floor";
                        }
                        else
                        {
                            className = "down";
                        }
                    }
                }
                anh = className;
                thayDoi = "<span class='" + className + "'>" + sign + Lib.FormatDouble(giaHT1 - giaTC1) + "</span>";
            }

            private static bool IsValueInArray(string value, string[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (value == array[i])
                    {
                        return true;
                    }
                }
                return false;
            }


            private SymbolItem[] m_Symbols;
            private IndexItem[] m_Indexs;
            private int m_ChangeId, m_SessionId;
            private string m_PreviousIndex;

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
            public IndexItem[] Indexs
            {
                get
                {
                    return this.m_Indexs;
                }
                set
                {
                    this.m_Indexs = value;
                }
            }
            public int ChangeId
            {
                get
                {
                    return this.m_ChangeId;
                }
                set
                {
                    this.m_ChangeId = value;
                }
            }
            public int SessionId
            {
                get
                {
                    return this.m_SessionId;
                }
                set
                {
                    this.m_SessionId = value;
                }
            }
            public string PreviousIndex
            {
                get
                {
                    return this.m_PreviousIndex;
                }
                set
                {
                    this.m_PreviousIndex = value;
                }
            }
        }
        #endregion

        #endregion

        public enum TradeCenter
        {
            AllTradeCenter = 0,
            HoSE = 1,
            HaSTC = 2,
            UpCom = 9
        }

        //private static DataTable GetStockSymbolData(TradeCenter tradeCenter)
        //{
        //    DataTable dtStockList = new DataTable();

        //    if (tradeCenter == TradeCenter.AllTradeCenter)
        //    {
        //        //CafeF_DataService.CafeF_DataService dataServices = new CafeF_EmbedData.CafeF_DataService.CafeF_DataService();
        //        WServices.Function dataServices = new WServices.Function();

        //        dtStockList = DistCache.Get<DataTable>("MemCached_HaSTC_PriceData");
        //        if (dtStockList == null || dtStockList.Rows.Count == 0)
        //        {
        //            dtStockList = dataServices.GetHaSTCPriceData().Tables[0].Copy();

        //        }
        //        if (dtStockList == null || dtStockList.Rows.Count == 0)
        //        {
        //            dtStockList = DistCache.Get<DataTable>("MemCached_HoSE_PriceData");
        //            if (dtStockList == null || dtStockList.Rows.Count == 0)
        //            {
        //                dtStockList = dataServices.GetHoSEPriceData().Tables[0].Copy();
        //            }
        //        }
        //        //hungnd
        //        if (dtStockList == null || dtStockList.Rows.Count == 0)
        //        {
        //            dtStockList = DistCache.Get<DataTable>("MemCached_UpCom_PriceData");
        //        }//end
        //        else
        //        {
        //            DataTable dtHoStock = DistCache.Get<DataTable>("MemCached_HoSE_PriceData");
        //            if (dtHoStock == null || dtHoStock.Rows.Count == 0)
        //            {
        //                dtHoStock = dataServices.GetHoSEPriceData().Tables[0].Copy();
        //            }
        //            if (dtHoStock != null && dtHoStock.Rows.Count != 0)
        //            {
        //                dtStockList.Merge(dtHoStock);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        string cacheName = string.Empty;
        //        switch (tradeCenter)
        //        { 
        //            case TradeCenter.HaSTC:
        //                cacheName = "MemCached_HaSTC_PriceData";
        //                break;
        //            case TradeCenter.HoSE:
        //                cacheName = "MemCached_HoSE_PriceData";
        //                break;
        //            case TradeCenter.UpCom:
        //                cacheName = "MemCached_UpCom_PriceData";
        //                break;
        //        }
        //        dtStockList = DistCache.Get<DataTable>(cacheName);

        //        if (dtStockList == null || dtStockList.Rows.Count == 0)
        //        {
        //            //CafeF_DataService.CafeF_DataService dataServices = new CafeF_EmbedData.CafeF_DataService.CafeF_DataService();
        //            WServices.Function dataServices = new WServices.Function();

        //            if (tradeCenter == TradeCenter.HaSTC)
        //            {
        //                dtStockList = dataServices.GetHaSTCPriceData().Tables[0].Copy();
        //            }
        //            else
        //            {
        //                dtStockList = dataServices.GetHoSEPriceData().Tables[0].Copy();
        //            }
        //        }
        //    }

        //    return dtStockList;
        //}

        //private void UpdateLog(string group)
        //{
        //    string name = CurrentContext.Request["n"];
        //    string ip = CurrentContext.Request.ServerVariables["REMOTE_ADDR"];
        //    string sql = "tblLog_Insert";

        //    Lib.ExecuteNoneQuery(ConfigurationManager.ConnectionStrings["FinanceChannelConnectionString"].ConnectionString, sql, CommandType.StoredProcedure, new SqlParameter("@IP", ip), new SqlParameter("@Group", group), new SqlParameter("@Name", name));

        //    this.UpdateOutput(1);
        //}
    }
}
