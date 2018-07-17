using System;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.themes
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
           
         
        }
        protected void lbtnOut_Click(object sender, EventArgs e)
        {
            Session["UID"] = null;
            Session.Abandon();
            Response.Redirect("/", true);
        }

        protected void lnkAdvertise_Click(object sender, EventArgs e)
        {
            if (Session["UTYPE"] != null)
            {
                Response.Redirect(
                    Convert.ToString(Session["UTYPE"]) == "6" ? "/account/college-profile?T=4" : "/account/college-login",
                    true);
            }
            else
            {
                Response.Redirect("/account/college-login", true);

            }
        }

        public bool CheckUserSession
        {
            get { return new SecurePage().IsLoggedInUSer; }
        }


    }
}