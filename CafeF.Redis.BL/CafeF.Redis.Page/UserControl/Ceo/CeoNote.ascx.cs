using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CafeF.Redis.BL;

namespace CafeF.Redis.Page.UserControl.Ceo
{
    public partial class CeoNote : System.Web.UI.UserControl
    {
        private Entity.Ceo GetCeo
        {
            get
            {
                try
                {
                    return ((MasterPage.StockMain)this.Page.Master).GetCeo;
                }
                catch
                {
                    return new Entity.Ceo();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadData();
        }

        private void LoadData()
        {
            this.Visible = false;
            var ceo = GetCeo;
            if (ceo != null && (ceo.CeoAsset.Count > 0 || ceo.CeoRelation.Count > 0))
            {
                var center = TradeCenterBL.getByTradeCenter(1);
                ltrTradeDate.Text = center.CurrentDate.ToString("dd/MM/yyyy");
                // +" " + (Utils.InTradingTime(1) ? Utils.GetCloseTime(1).ToString("HH:mm") : center.CurrentDate.ToString("HH:mm"));
                this.Visible = true;
            }

        }
    }
}