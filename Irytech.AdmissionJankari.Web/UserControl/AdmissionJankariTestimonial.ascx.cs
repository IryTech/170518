using System;
using System.Data;
using IryTech.AdmissionJankari.BL;


namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class AdmissionJankariTestimonial : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (IsPostBack) return;
            BindTestimonial();
        }
        private void BindTestimonial()
        {
            var testimonial =
                new UserManager().GetadmissionJankriTestimonial(0);
                   
            var data = from a in testimonial.AsEnumerable()
                where a.Field<bool>("AjTestimonialStatus") == true 
                                            select a;
                rptTestimonial.Visible = true;

                rptTestimonial.DataSource = data.AsDataView(); 
            rptTestimonial.DataBind();

            }
        }
    
}