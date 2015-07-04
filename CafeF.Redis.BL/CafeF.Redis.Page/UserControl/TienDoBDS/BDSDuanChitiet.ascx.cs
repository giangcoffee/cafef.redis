using System;
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
namespace CafeF.Redis.Page.UserControl.TienDoBDS
{
    public partial class BDSDuanChitiet : System.Web.UI.UserControl
    {
        private int pcount = 5;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                
            }
        }

        private List<CafeF.Redis.Entity.TienDoBDS> GetStockTienDoBDS
        {

            get
            {
                try
                {
                    return ((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetStockTienDoBDS;
                }
                catch
                {
                    return new List<CafeF.Redis.Entity.TienDoBDS>();
                }
            }
        }

        protected void BindData()
        {
            object objSymbol = Request.QueryString["symbol"];
            if (objSymbol == null || objSymbol.ToString().Trim() == "")
                return;
            int idx = 1;
            if (Request.QueryString["pageidx"] != null)
            {
                try
                {
                    idx = Convert.ToInt32(Request.QueryString["pageidx"].ToString());
                }
                catch { }
            }
            if (idx < 1) idx = 1;
           
            List<CafeF.Redis.Entity.TienDoBDS> tblResult = GetStockTienDoBDS;
            if (tblResult != null && tblResult.Count > 0)
            {
                if (tblResult.Count > 0)
                {
                    int count = (tblResult.Count % pcount == 0) ? (tblResult.Count / pcount) : (tblResult.Count / pcount + 1);
                    if (idx > count) idx = count;
                    getGenPaging(tblResult.Count,idx);
                    rpData.DataSource = tblResult.GetPaging(idx, pcount);
                    rpData.DataBind();
                }
            }
        }

        protected void rpData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CafeF.Redis.Entity.TienDoBDS data = (CafeF.Redis.Entity.TienDoBDS)e.Item.DataItem;
                Literal ltName = (Literal)e.Item.FindControl("ltName");
                Literal ltTongVon = (Literal)e.Item.FindControl("ltTongVon");
                Literal ltTyLeGhopVon = (Literal)e.Item.FindControl("ltTyLeGhopVon");
                Literal ltLoiNhuanDoanhThu = (Literal)e.Item.FindControl("ltLoiNhuanDoanhThu");
                Literal ltHinhThucKinhDoanh = (Literal)e.Item.FindControl("ltHinhThucKinhDoanh");
                Literal ltDiaDiem = (Literal)e.Item.FindControl("ltDiaDiem");
                Literal ltDienTich = (Literal)e.Item.FindControl("ltDienTich");
                Literal ltGhiChu = (Literal)e.Item.FindControl("ltGhiChu");
                Literal ltMota = (Literal)e.Item.FindControl("ltMota");
                HtmlAnchor aName = (HtmlAnchor)e.Item.FindControl("aName");
                HtmlAnchor aAll = (HtmlAnchor)e.Item.FindControl("aAll");
                if (!string.IsNullOrEmpty(data.URL))
                    aAll.HRef = data.URL;
                else
                    aAll.Visible = false;
                ltName.Text = data.TenDuAn;
                ltTongVon.Text =string.Format("{0} {1}", String.Format("{0:#,##0}",data.TongVon),data.Donvi) ;
                ltTyLeGhopVon.Text = data.TyLeGhopVon.ToString();
                //ltLoiNhuanDoanhThu.Text =  String.Format("{0:#,##0}", data.LoiNhuanDoanhThu);
                ltHinhThucKinhDoanh.Text = data.HinhThucKinhDoanh;
                ltDiaDiem.Text = data.DiaDiem;
                //ltDienTich.Text = string.Format("{0} {1}", data.DienTich, data.LoaiDienTich);
                string strgc = "";
                //if (string.IsNullOrEmpty(data.URL ))
                strgc = "<br><a href=\"javascript:void(0); \" class=\"tttiendo\" style=\"color:#003366;float: right;\">Xem thêm<span class=\"tooltip\"><span class=\"top\"></span><span class=\"middle\" >" + data.GhiChu + "</span><span class=\"bottom\" ></span></span></a>";

                ltGhiChu.Text = CafeF.Redis.BL.Utils.SubStringSpace(data.GhiChu, 50, strgc); 
               
                if ((data.ViewDate != null) && (data.ViewDate != DateTime.MinValue))
                    ltGhiChu.Text += " (Tính đến " + data.ViewDate.ToString("dd/MM/yyyy") + ")" ;
                string str = "";
                //if (string.IsNullOrEmpty(data.URL ))
                    str = "<br><a href=\"javascript:void(0); \" class=\"tttiendo\" style=\"color:#003366;float: right;\">Xem thêm<span class=\"tooltip\"><span class=\"top\"></span><span class=\"middle\" >" + data.MoTa + "</span><span class=\"bottom\" ></span></span></a>";
                //else
                    //str = "<br><a href=\"javascript:void(0); window.open('" + data.URL + "');\" class=\"tttiendo\" style=\"color:#003366;float: right;\">Xem thêm<span class=\"tooltip\"><span class=\"top\"></span><span class=\"middle\" >" + data.MoTa + "</span><span class=\"bottom\" ></span></span></a>";
                ltMota.Text = CafeF.Redis.BL.Utils.SubStringSpace(data.MoTa, 50, str);

                BDSImages BDSImages1 = (BDSImages)e.Item.FindControl("BDSImages1");
                BDSImages1.BDSImagesList = data.BDSImages;
                BDSImages1.ImageID = data.MaTienDo;
                aName.Attributes.Add("name", data.MaTienDo);

                Repeater rpDT = (Repeater)e.Item.FindControl("rpDT");
                rpDT.DataSource = data.DienTichs;
                rpDT.DataBind();

                Repeater rpLN = (Repeater)e.Item.FindControl("rpLN");
                rpLN.DataSource = data.LoiNhuans;
                rpLN.DataBind();
            }
        }

        private void getGenPaging(int totalrow,int idx)
        {
            int count = (totalrow % pcount == 0) ? (totalrow / pcount) : (totalrow / pcount + 1);
            string host = Request.RawUrl.Replace(".chn", "");
            if (host.IndexOf("/trang-") > 0)
                host = host.Substring(0, host.IndexOf("/trang-"));
            string sformat = "{0}/trang-{1}.chn";
            if (idx > 1)
                paging.InnerHtml = @"<a style='padding-left=5px' href='" + string.Format(sformat, host, (idx-1).ToString()) + "'>&lt;</a>&nbsp;";
            else
                paging.InnerHtml = @"<a style='padding-left=5px' href=''>&lt;</a>&nbsp;";
            for (int i = 1; i <= count; i++)
            {
                if (i == idx)
                    paging.InnerHtml += "<a class='current' id='aSamePE" + i.ToString() + "' href='" + string.Format(sformat, host ,i.ToString()) + "'>" + i.ToString() + "</a>&nbsp;";
                else
                    paging.InnerHtml += "<a id='aSamePE" + i.ToString() + "' href='" + string.Format(sformat, host, i.ToString()) + "'>" + i.ToString() + "</a>&nbsp;";
            }
            if (idx < count)
                paging.InnerHtml += "<a href='" + string.Format(sformat, host, (idx + 1).ToString()) + "'>&gt;</a>&nbsp;";
            else
                paging.InnerHtml += "<a href=''>&gt;</a>&nbsp;";
        }

        protected void rpData_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
             if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rpDT = (Repeater)e.Item.FindControl("rpDT");
                rpDT.ItemDataBound +=new RepeaterItemEventHandler(rpDT_ItemDataBound);
                Repeater rpLN = (Repeater)e.Item.FindControl("rpLN");
                rpLN.ItemDataBound +=new RepeaterItemEventHandler(rpLN_ItemDataBound);
            }
        }

        protected void rpDT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CafeF.Redis.Entity.TienDoBDSDienTich data = (CafeF.Redis.Entity.TienDoBDSDienTich)e.Item.DataItem;
                Literal ltHeader = (Literal)e.Item.FindControl("ltHeader");
                ltHeader.Text = getDienTichText(data.LoaiDienTich);
            }
        }

        protected void rpLN_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CafeF.Redis.Entity.TienDoBDSLoiNhuan data = (CafeF.Redis.Entity.TienDoBDSLoiNhuan)e.Item.DataItem;
                Literal ltHeader = (Literal)e.Item.FindControl("ltHeader");
                ltHeader.Text = getLoiNhuanText(data.LoaiLoiNhuan);
            }
        }

        private string getDienTichText(string key)
        {
            switch (key)
            {
                case "DTT":
                    return "Tổng diện tích:";
                case "DTS":
                    return "Diện tích sàn:";
            }
            return "Tổng diện tích:";
        }
        private string getLoiNhuanText(string key)
        {
            switch (key)
            {
                case "TDT":
                    return "Doanh thu dự kiến:";
                case "LNTT":
                    return "Lợi nhuận dự kiến:";
                case "LNTT50":
                    return "Lợi nhuận dự kiến 50 năm:";
                case "TDT50":
                    return "Doanh thu dự kiến 50 năm:";
            }
            return "Doanh thu dự kiến:";
        }
    }
}