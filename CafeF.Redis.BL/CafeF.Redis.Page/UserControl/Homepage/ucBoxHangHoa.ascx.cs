using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CafeF.Redis.BL;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Page.UserControl.Homepage
{
    public partial class ucBoxHangHoa : System.Web.UI.UserControl
    {
        #region Properties

        private int tabId;
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            GetParams();
            if (IsPostBack) return;
            LoadData();
        }
        #endregion

        #region Business
        private void GetParams()
        {
            if (!int.TryParse(Request["tab"] ?? "1", out tabId)) tabId = 1;
        }

        private void LoadData()
        {
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
            repVietnam.Visible = repTheGioi.Visible = repHangHoa.Visible = false;
            if (tabId == 4)
            {
                divBond.Visible = true;
            }
            else
            {
                divBond.Visible = false;
                Repeater rep;
                switch (tabId)
                {
                    case 1:
                        rep = repVietnam; break;
                    case 2:
                        rep = repTheGioi; break;
                    case 3:
                        rep = repHangHoa; break;
                    default:
                        rep = repVietnam; break;
                }
                rep.DataSource = data;
                rep.DataBind();
                rep.Visible = true;
            }
        }
        protected string DisplayPrice(object price)
        {
            var p = double.Parse(price.ToString());
            return p > 0 ? p.ToString("#,##0.0") : "";
        }
        #endregion
    }
    internal class HangHoa
    {
        public string ProductName { get; set; }
        public int TabId { get; set; }
        public double CurrentPrice { get; set; }
        public double OtherPrice { get; set; }
        public double PrevPrice { get; set; }
        public int RowIndex { get; set; }
        public string ChangeString
        {
            get
            {
                var format = "#,##0.0";
                if (TabId == 1 && !ProductName.ToUpper().Contains("VÀNG TG")) format = "#,##0";
                return string.Format("<span style='color:{3}'>{0}{1:" + format + "}" + ((TabId == 1 && !ProductName.ToUpper().Contains("VÀNG TG")) ? "" : " ") + "({0}{2:#,##0.0}%)", CurrentPrice > PrevPrice ? "+" : "", CurrentPrice - PrevPrice, (CurrentPrice - PrevPrice) / PrevPrice * 100, CurrentPrice > PrevPrice ? "#009900" : (CurrentPrice < PrevPrice ? "#CC0000" : "#333"));
            }
        }
        public string PriceString
        {
            get
            {
                if (CurrentPrice == 0) return "";
                if (TabId != 1) return CurrentPrice.ToString("#,##0.0");
                return (TabId == 1 && ProductName.ToUpper().Contains("VÀNG TG")) ? CurrentPrice.ToString("#,##0.0") : CurrentPrice.ToString("#,##0");
            }
        }
        public string OtherString
        {
            get
            {
                if (OtherPrice == 0) return "";
                if (TabId != 1) return OtherPrice.ToString("#,##0.0");
                return (TabId == 1 && ProductName.ToUpper().Contains("VÀNG TG")) ? OtherPrice.ToString("#,##0.0") : OtherPrice.ToString("#,##0");
            }
        }
    }
}