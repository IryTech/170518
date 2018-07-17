using System;
using System.Web;
using System.Web.Services;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components.Web.Controls;

namespace IryTech.AdmissionJankari20.Web.Account
{
    public partial class PaymentOption : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Session["UTYPE"] != null) return;
            HttpContext.Current.Response.Redirect("/account/college-login");
        }

    }
}