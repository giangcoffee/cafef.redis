using System;
using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Page;
using CafeF.Redis.Data;
namespace CafeF.Redis.Page.MasterPage
{
    public partial class StockMain : System.Web.UI.MasterPage
    {
        private string symbol = "";
        private string ceocode = "";
        private string symbolCeo = "";
        private Stock stock = new Stock();
        private CafeF.Redis.Entity.Ceo ceo = new Entity.Ceo();
        private Stock stockForCeo = new Stock();
        private List<CafeF.Redis.Entity.TienDoBDS> stockTienDoBDS = new List<Entity.TienDoBDS>();
        public IDictionary<string, string> CeoPhotos { get; set; }
        public IDictionary<string, StockCompactInfo> CeoStocks { get; set; }
        public IDictionary<string, StockPrice> CeoStockPrices { get; set; }
        public Stock GetStock
        {
            get
            {
                return stock;
            }
        }

        public Stock GetStockForCeo
        {
            get
            {
                return stockForCeo;
            }
        }

        public CafeF.Redis.Entity.Ceo GetCeo
        {
            get
            {
                return ceo;
            }
        }

        public List<CafeF.Redis.Entity.TienDoBDS> GetStockTienDoBDS
        {
            get
            {
                return stockTienDoBDS;
            }
        }
        public string SymbolForCEO { get { return symbolCeo; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            form1.Action = Request.RawUrl;
            symbol = (Request.Params["symbol"] ?? "").Trim().ToUpper();
            ceocode = (Request.Params["ceocode"] ?? "").Trim().ToUpper();
            symbol = symbol.ToUpper();
            ceocode = ceocode.ToUpper();
            CeoPhotos = new Dictionary<string, string>();
            CeoStocks = new Dictionary<string, StockCompactInfo>();
            CeoStockPrices = new Dictionary<string, StockPrice>();
            if (symbol != "")
            {
                stock = StockBL.getStockBySymbol(symbol);
                if (stock != null)
                {
                    var tab = "";
                    switch ((Request["tabid"] ?? "").ToLower())
                    {
                        case "1":
                            tab = "Thông tin tài chính";
                            break;
                        case "2":
                            tab = "Thông tin công ty";
                            break;
                        case "3":
                            tab = ((Request["Tab"] ?? "").ToLower() != "co-dong-lon") ? "Ban lãnh đạo" : "Cổ đông lớn";
                            break;
                        case "4":
                            tab = "Công ty con - Công ty liên kết";
                            break;
                        default:
                            tab = "Tin tức và dữ liệu doanh nghiệp";
                            break;
                    }
                    this.Page.Title = string.Format("{1} : {0} | {2}CafeF.vn", stock.CompanyProfile.basicInfos.Name, stock.Symbol, tab + (tab != "" ? " | " : ""));
                    AddMetaTag("Keywords", string.Format("{0}, {1}, hồ sơ công ty, thông tin giao dịch{2}", stock.Symbol, stock.CompanyProfile.basicInfos.Name, (tab != "" ? " , " : "") + tab));
                    AddMetaTag("Description", string.Format("Thông tin giao dịch và hồ sơ {0}{1}", stock.CompanyProfile.basicInfos.Name, (tab != "" ? " - " : "") + tab));
                }
                stockTienDoBDS = TienDoBDSBL.get_TienDoBDSBySymbol(symbol);
            }
            else
            {
                if (ceocode != "")
                {
                    ceo = CeoBL.getCeoByCode(ceocode);
                    var path = HttpContext.Current.Items["VirtualUrl"] ?? "";
                    symbolCeo = path.ToString().Contains("?cs=") ? (path.ToString().Substring(path.ToString().IndexOf("?cs=") + 4)) : "";

                    if (ceo != null)
                    {
                        string pos = "";
                        if (symbolCeo == "")
                        {
                            foreach (var ceopos in ceo.CeoPosition)
                            {
                                if (ceopos.CeoSymbol != "")
                                {
                                    symbolCeo = ceopos.CeoSymbol;
                                    pos = ceopos.PositionTitle;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (var ceopos in ceo.CeoPosition)
                            {
                                if (ceopos.CeoSymbol == symbolCeo)
                                {
                                    pos = ceopos.PositionTitle;
                                    break;
                                }
                            }
                            if (pos == "")
                            {
                                if (ceo.CeoPosition.Count > 0)
                                {
                                    pos = ceo.CeoPosition[0].PositionTitle;
                                    symbolCeo = ceo.CeoPosition[0].CeoSymbol;
                                }
                            }
                        }
                        this.Page.Title = string.Format("{1} : {0} - {2} | CafeF.vn", ceo.CeoName, string.IsNullOrEmpty(symbolCeo) ? "CEO" : symbolCeo.ToUpper(), pos);
                        AddMetaTag("Keywords", string.Format("{0}, {1}, hồ sơ ceo, thông tin ceo", string.IsNullOrEmpty(symbolCeo) ? "CEO" : symbolCeo.ToUpper(), ceo.CeoName));
                        AddMetaTag("Description", string.Format("Thông tin và hồ sơ {0}", ceo.CeoName));
                        if (!string.IsNullOrEmpty(symbolCeo))
                        {
                            stockForCeo = StockBL.getStockBySymbol(symbolCeo.ToUpper());
                            if (stockForCeo != null)
                            {
                                var ls = new List<string> {ceocode};
                                foreach (var tmp in stockForCeo.CompanyProfile.AssociatedCeo)
                                {
                                    if (!ls.Contains(tmp.CeoCode)) ls.Add(tmp.CeoCode);
                                }
                                CeoPhotos = CeoBL.GetCeoImage(ls);
                            }
                        }
                        var ss = new List<string>();
                        foreach (var asset in ceo.CeoAsset)
                        {
                            if(asset.Symbol!="" && !ss.Contains(asset.Symbol)) ss.Add(asset.Symbol);
                        }
                        foreach (var relation in ceo.CeoRelation)
                        {
                            if(relation.Symbol!="" && !ss.Contains(relation.Symbol)) ss.Add(relation.Symbol);
                        }
                        
                        CeoStocks = StockBL.GetStockCompactInfoMultiple(ss);
                        CeoStockPrices = StockBL.GetStockPriceMultiple(ss);
                    }
                }

            }
        }
        private void AddMetaTag(string name, string value)
        {
            var head = Page.Header;
            foreach (Control ctrl in head.Controls)
            {
                if (ctrl.GetType() != typeof(HtmlMeta)) continue;
                if (((HtmlMeta)ctrl).Name.ToUpper() != name.ToUpper()) continue;
                ((HtmlMeta)ctrl).Content = value;
                return;
            }
            head.Controls.Add(new HtmlMeta { Name = name, Content = value });
        }

    }
}