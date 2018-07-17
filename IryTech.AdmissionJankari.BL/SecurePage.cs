using System;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web;
namespace IryTech.AdmissionJankari.BL
{
  public class SecurePage : Page
    {



      #region "Init"
      protected override void InitializeCulture()
      {
          CultureInfo CI = new CultureInfo("en-US");
          CI.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";

          Thread.CurrentThread.CurrentCulture = CI;
          Thread.CurrentThread.CurrentUICulture = CI;
          base.InitializeCulture();
      }
      #endregion "Init"




        public SecurePage()
        {
            this.PreRender += new EventHandler(SecurePage_PreRender);

        }

        void SecurePage_PreRender(object sender, EventArgs e)
        {
            if (Session["UID"] == null)
            {
                HttpContext.Current.Response.Redirect("/Account/Login"); 
            }
        }
        public bool IsLoggedInUSer
        {

            get
            {
                if (Session["UID"] == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }
        public int LoggedInUserId
        {
            get
            {
                if (Session["UID"] == null)
                {
                    HttpContext.Current.Response.Redirect("/Account/Login"); 
                    
                }
                return Convert.ToInt32 (Session["UID"]);
            }
            set
            {
                Session["UID"] = value;
            }
        }
        public string LoggedInUserEmailId
        {
            get
            {
                if (Session["EmailId"] == null)
                {
                    HttpContext.Current.Response.Redirect("/Account/Login"); 
                }
                return Session["EmailId"].ToString();
            }
            set
            {
                Session["EmailId"] = value;
            }
        }


        public string LoggedInUserName
        {
            get
            {
                if (Session["LoginUserName"] == null)
                {
                    HttpContext.Current.Response.Redirect("/Account/Login"); 
                }
                return  Convert.ToString(Session["LoginUserName"]);
            }
            set
            {
                Session["LoginUserName"] = value;
            }
        }

        public int  LoggedInUserType
        {
            get
            {
                if (Session["UTYPE"] == null)
                {
                    HttpContext.Current.Response.Redirect("/Account/Login"); 
                }
                return Convert.ToInt32(Session["UTYPE"]);
            }
            set
            {
                Session["UTYPE"] = value;
            }
        }
        public bool CanCreateUser
        {
            get
            {
                if (Session["CanCreateUser"] == null)
                {
                    HttpContext.Current.Response.Redirect("/Account/Login"); 
                }
                return Convert.ToBoolean(Session["CanCreateUser"]);
            }
            set
            {
                Session["CanCreateUser"] = value;
            }
        }
        public string  LoggedInUserMobile
        {
            get
            {
                if (Session["UserMobile"] == null)
                {
                    HttpContext.Current.Response.Redirect("/Account/Login"); 
                }
                return Convert.ToString(Session["UserMobile"]);
            }
            set
            {
                Session["UserMobile"] = value;
            }
        }
    }
}
