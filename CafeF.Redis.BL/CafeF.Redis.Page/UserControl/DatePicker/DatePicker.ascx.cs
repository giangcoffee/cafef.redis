using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace CafeF.Redis.Page.UserControl.DatePicker
{
    [ValidationProperty("IsValidDate")]
    public partial class DatePicker : System.Web.UI.UserControl
    {
        public string ClientID
        {
            get
            {
                return txtDatePicker.ClientID;
            }
        }
        public string Text
        {
            get
            {
                return txtDatePicker.Text.Trim();
            }
        }
        public Unit Width
        {
            set
            {
                txtDatePicker.Width = value;
            }
            get
            {
                return txtDatePicker.Width;
            }
        }
        public bool IsValidDate
        {
            get
            {
                if (String.IsNullOrEmpty(txtDatePicker.Text.Trim())) return false;

                try
                {
                    string[] month = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                    string[] date = txtDatePicker.Text.Trim().Split('/');

                    DateTime temp = Convert.ToDateTime(date[0] + " " + month[Convert.ToInt32(date[1]) - 1] + " " + date[2]);
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }
        public DateTime SelectedDate
        {
            get
            {
                try
                {
                    string[] month = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                    string[] date = txtDatePicker.Text.Trim().Split('/');

                    return Convert.ToDateTime(date[0] + " " + month[Convert.ToInt32(date[1]) - 1] + " " + date[2]);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                txtDatePicker.Text = value.ToString("dd/MM/yyyy");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtDatePicker.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
        }

        protected void txtDatePicker_Init(object sender, EventArgs e)
        {
            txtDatePicker.Attributes.Add("onKeyDown", "txtDatePicker_KeyDownHandler(event)");
        }
    }
}