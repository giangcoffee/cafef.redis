using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CafeF.Redis.Page.MasterPage;

namespace CafeF.Redis.Page.UserControl.Ceo
{
    public partial class CeoAchievement : System.Web.UI.UserControl
    {
        private Entity.Ceo GetCeo
        {
            get
            {
                try
                {
                    return (((StockMain) this.Page.Master).GetCeo);
                }
                catch (Exception)
                {
                    return new Entity.Ceo();
                }
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack) return;
            LoadData();
        }

        private void LoadData()
        {
            var ceo = GetCeo;
            if (ceo == null || string.IsNullOrEmpty((ceo.CeoAchievements??"").Trim()))
            {
                this.Visible = false; return;
            }
            ltrCeoAchievements.Text = ("<ul><li>" + ceo.CeoAchievements.Replace(Environment.NewLine, "</li><li>") + "</li></ul>").Replace("<li></li>","");
            this.Visible = true;
        }
    }
}