using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class ucFontTestimonialsDetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindFrontTestmonialDetails();
            
            }
        }
        private void BindFrontTestmonialDetails()
        {
            try
            {
                var testimonilData = NewsArticleNoticeProvider.Instance.GetTestimonialsDetails();
                testimonilData = testimonilData.Where(result => result.TestimonilaStatus == true).ToList();
                if (testimonilData.Count > 0)
                {

                    dlFontTestmonialsDetails.DataSource = testimonilData;
                    dlFontTestmonialsDetails.DataBind();

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  GetAllTestimonials in ucTestmonialsDetails.ascx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        
        }


        public int TestimonilasCount
        {
            get;
            set;

        }
    }
}