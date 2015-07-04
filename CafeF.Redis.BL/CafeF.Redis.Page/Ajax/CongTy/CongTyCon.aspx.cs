﻿using System;
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
using System.Text;
using CafeF.Redis.BL;
using CafeF.Redis.Page;
using CafeF.Redis.Data;
namespace CafeF.Redis.Page.Ajax.CongTy
{
    public partial class CongTyCon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack) return;
            LoadData();
        }
        private void LoadData()
        {
            var stock = StockBL.getStockBySymbol((Request["sym"] ?? "").ToUpper());
            if(stock==null) return;
            ucCongTyCon1.LoadData(stock);
        }
    }
}
