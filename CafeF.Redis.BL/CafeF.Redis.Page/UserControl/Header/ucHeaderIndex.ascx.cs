using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CafeF.Redis.BL;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Page.UserControl.Header
{
    public partial class ucHeaderIndex : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            GetData();
        }
        private void GetData()
        {
            var ls = new List<MyIndex>();
            List<ProductBox> p;
            try
            {
                p = ProductBoxBL.GetByTab(2);
                foreach (var box in p)
                {
                    if (box.ProductName != "DowJones") continue;
                    ls.Add(new MyIndex() { Name = box.ProductName, Index = box.CurrentPrice.ToString("#,##0.0"), Change = (box.CurrentPrice > box.PrevPrice ? "+" : "") + (box.CurrentPrice - box.PrevPrice).ToString("#,##0.0"), ChangeClass = (box.CurrentPrice > box.PrevPrice ? "up" : (box.CurrentPrice < box.PrevPrice ? "down" : "")), ChangePercent = (box.CurrentPrice > box.PrevPrice ? "+" : "") + (box.PrevPrice == 0 ? 0 : (box.CurrentPrice / box.PrevPrice * 100 - 100)).ToString("#0.0") });
                    break;
                }
            }
            catch (Exception ex) { }
            try
            {
                p = ProductBoxBL.GetByTab(3);
                foreach (var box in p)
                {
                    if (box.ProductName != "Gold") continue;
                    ls.Add(new MyIndex() { Name = box.ProductName, Index = box.CurrentPrice.ToString("#,##0.0"), Change = (box.CurrentPrice > box.PrevPrice ? "+" : "") + (box.CurrentPrice - box.PrevPrice).ToString("#,##0.0"), ChangeClass = (box.CurrentPrice > box.PrevPrice ? "up" : (box.CurrentPrice < box.PrevPrice ? "down" : "")), ChangePercent = (box.CurrentPrice > box.PrevPrice ? "+" : "") + (box.PrevPrice == 0 ? 0 : (box.CurrentPrice / box.PrevPrice * 100 - 100)).ToString("#0.0") });
                    break;
                }

                //p = ProductBoxBL.GetByTab(3);
                foreach (var box in p)
                {
                    if (box.ProductName != "Crude Oil") continue;
                    ls.Add(new MyIndex() { Name = box.ProductName, Index = box.CurrentPrice.ToString("#,##0.0"), Change = (box.CurrentPrice > box.PrevPrice ? "+" : "") + (box.CurrentPrice - box.PrevPrice).ToString("#,##0.0"), ChangeClass = (box.CurrentPrice > box.PrevPrice ? "up" : (box.CurrentPrice < box.PrevPrice ? "down" : "")), ChangePercent = (box.CurrentPrice > box.PrevPrice ? "+" : "") + (box.PrevPrice == 0 ? 0 : (box.CurrentPrice / box.PrevPrice * 100 - 100)).ToString("#0.0") });
                    break;
                }
            }
            catch (Exception ex) { }
            repIndex.DataSource = ls;
            repIndex.DataBind();
        }
    }
    internal class MyIndex
    {
        public MyIndex() { }
        public string Name { get; set; }
        public string Index { get; set; }
        public string Change { get; set; }
        public string ChangeClass { get; set; }
        public string ChangePercent { get; set; }
    }
}