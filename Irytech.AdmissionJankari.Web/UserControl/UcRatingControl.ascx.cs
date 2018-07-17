using System;
using System.Web.UI;
using IryTech.AdmissionJankari.BL;
using System.Web;
using Spaanjaars.Toolkit;
namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcRatingControl : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {   var objSecurePage = new SecurePage();
            if (!IsPostBack)
            {
                ContentRating1.ItemId = Guid.NewGuid();
                Rate1 = 1;
                ContentRating1.DataSource = Values;
                ContentRating1.DataBind();
               
            }
           
          
        }
        

        protected void ContentRating1_Rating(object sender, Spaanjaars.Toolkit.RateEventArgs e)
        {
            if (e.HasRated)
            {
                e.Cancel = true;
            }
        }

        protected void ContentRating1_Rated(object sender, Spaanjaars.Toolkit.RateEventArgs e)
        {
            int[] tempValues;
            tempValues = Values;
            Common objCommon = new Common();
            SecurePage objSecurePage = new SecurePage();
            string errMsg = "";


            tempValues[Convert.ToInt32(e.RateValue) - 1] += 1;
            if (objSecurePage.IsLoggedInUSer == true)
            {
                int result = objCommon.InsertUserRating(objSecurePage.LoggedInUserId, RatingType, RatingId, tempValues[0], tempValues[1], tempValues[2], tempValues[3], tempValues[4], out errMsg);//Session["ExamId"]
                Values = tempValues;

                ContentRating1.DataSource = Values;
                ContentRating1.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>MessageRating('" + errMsg + "');</script>", false);
            }
            else
            {
                string url = HttpContext.Current.Request.RawUrl;
                url = url.Substring(1, url.Length-1);
                Response.Redirect(IryTech.AdmissionJankari.Components.Utils.AbsoluteWebRoot+ "account/login?ReturnUrl="+url);
            }
        }

        private  int[] Values
        {
             
            get
            {
                object values = ViewState["Values"];
                if (values != null)
                {
                    return (int[])values;
                }
                else
                {
                    return new int[] { Rate1, Rate2, Rate3, Rate4, Rate5 };
                }
                 return (int[])values;
               
            }
            set
            {
                ViewState["Values"] = value;
            }
            
        }

        public int Rate1
        {
            get;
            set;
        }
        public int Rate2
        {
            get;
            set;
        }
        public int Rate3
        {
            get;
            set;
        }
        public int Rate4
        {
            get;
            set;
        }
        public int Rate5
        {
            get;
            set;
        }


        public string RatingType
        {
            get;
            set;
        }
        public int RatingId
        {
            get;
            set;
        }
     

        public string RatingToolTip
        {
            set { ContentRating1.ToolTip = "Rate " + value; }
        }

        public ContentRating Bind
        {
            get { return ContentRating1; }

        }
    }
}