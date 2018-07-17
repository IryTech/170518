using System;
using IryTech.AdmissionJankari.BL;
using System.Data;
namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CollegeTestimonialOnDetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack) return;
            BindTestimonial();
        }
        private void BindTestimonial()
        {
            try
            {
                var testimonial = new Common().GetTestimonialDetails(0, CollegeBranchId, 0);
                var dv = testimonial.Tables[0].DefaultView;
                dv.RowFilter = "AjTestimonialStatus=1";
                var objDataSet = new DataSet();
                var objDt = dv.ToTable();
                objDataSet.Tables.Add(objDt);
                if (objDataSet.Tables.Count > 0)
                {
                    if (objDataSet.Tables[0].Rows.Count > 0)
                    {
                        divTestimonial.Visible = true;
                        rptTestimonial.DataSource = objDataSet.Tables[0];
                        rptTestimonial.DataBind();
                    }
                    else
                    {
                        divTestimonial.Visible = false;
                    }
                }
                else
                {
                    divTestimonial.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindTestimonial in CollegeTestimonialOnDetails.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }
        public int CollegeBranchId
        {
            get { return Convert.ToInt32(ViewState["CollegeBranchId"]); }
            set { ViewState["CollegeBranchId"] = value; }
        }
    }
}