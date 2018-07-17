using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using AjaxControlToolkit;
using System.Web.UI;
namespace IryTech.AdmissionJankari20.Web.AdminPanel
{
    public partial class AdmissionjankariTestimonial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
            ucCustomPaging.ButtonsCount = 10;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            FileUpload1.uploadToDirectory = new Common().GetFilepath("UniversityImage");
            if (IsPostBack) return;
            BindTestimonial();
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            var testimonial = new UserManager().GetadmissionJankriTestimonial(0);
            if (testimonial.Rows.Count > 0)
            {
                rptTestimonial.Visible = true;
                ucCustomPaging.BindDataWithPaging(rptTestimonial, testimonial);

            }
        }
        private void BindTestimonial()
        {
            var testimonial = new UserManager().GetadmissionJankriTestimonial(0);
            if (testimonial.Rows.Count > 0)
            {
                rptTestimonial.Visible = true;
                ucCustomPaging.BindDataWithPaging(rptTestimonial, testimonial);
            
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
              var fileName = this.FileUpload1.UploadedImageName;
                if (fileName != null)
                {
                    hdnImageFile.Value = fileName;
                }
            if (string.IsNullOrEmpty(Convert.ToString(hdnTestimonialId.Value)))
                InsertUpdateCollegeTestimonial(0);
            else
            {
                InsertUpdateCollegeTestimonial(Convert.ToInt32(hdnTestimonialId.Value));
                btnSave.Text = "Insert";
                hdnTestimonialId.Value = "";
            }
          

        }
        private void InsertUpdateCollegeTestimonial(int testimonialId)
        {
            try
            {
                var errMsg = "";
                var objAdmissionJankariTestimonial = new AdmissionJankariTestimonial
                {
                    TestimonialId = testimonialId,
                    TestimonialPersonName = txtPersonName.Text.Trim(),
                    TestimonialPeronDesignation = txtPersondesig.Text.Trim(),
                    TestimonialImage = hdnImageFile.Value.Trim(),
                    TestimonialPriority = txtPriority.Text,
                    TestimonialStatus = chkTestimonialStatus.Checked,
                    TestimonialText=txtTestimonial.FckValue

                };
                var result = UserManagerProvider.Instance.InsertUpdateTestimonialAdmssionjankari(objAdmissionJankariTestimonial, new SecurePage().LoggedInUserId, out errMsg);
                lblResult.Text = errMsg;
                lblResult.Visible = true;
                if (result > 0) {
                    ClearFields();
                    BindTestimonial();
                    lblResult.CssClass = "success";
                }
                else
                    lblResult.CssClass = "error";
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnSave_Click in AdminPanel/AdmissionjanklariTestimonial.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        private void ClearFields()
        {
            txtPersondesig.Text = string.Empty;
            txtPersonName.Text = string.Empty;
            txtPriority.Text = string.Empty;
            chkTestimonialStatus.Checked = false;
            txtTestimonial.FckValue = string.Empty;
            imgCollege.ImageUrl = "";


        }
        protected void RptTestimonialItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            try
            {
                var testimonial = new UserManager().GetadmissionJankriTestimonial(Convert.ToInt32(e.CommandArgument.ToString()));
                hdnTestimonialId.Value = e.CommandArgument.ToString();
                if (testimonial.Rows.Count > 0)
                {
                    txtPersonName.Text = Convert.ToString(testimonial.Rows[0]["AjTestimonialPersonName"].ToString());
                    txtPersondesig.Text = Convert.ToString(testimonial.Rows[0]["AjTestimonialPersonDesignation"].ToString());
                    txtPriority.Text = Convert.ToString(testimonial.Rows[0]["AjTestimonialPriority"].ToString());
                    txtTestimonial.FckValue = Convert.ToString(testimonial.Rows[0]["AjTestimonialText"].ToString());
                    chkTestimonialStatus.Checked = !string.IsNullOrEmpty(testimonial.Rows[0]["AjTestimonialStatus"].ToString()) && Convert.ToBoolean(testimonial.Rows[0]["AjTestimonialStatus"].ToString());
                    var img= Convert.ToString(testimonial.Rows[0]["AjTestimonialPersonImage"].ToString());
                  
                    imgCollege.ImageUrl = String.Format("{0}{1}", "/image.axd?College=", string.IsNullOrEmpty(hdnImageFile.Value) ? "NoImage.jpg" : hdnImageFile.Value);
                    imgCollege.AlternateText = txtPersonName.Text;
                    if (img.Equals("N/A"))
                    {
                        FileUpload1.SetImgUrl = "";
                    }
                    else
                    {
                        FileUpload1.SetImgUrl = new Common().GetFilepath("UniversityImage") + img;
                        hdnImageFile.Value = img;
                    }
                    lblResult.Visible = false;
                    btnSave.Text = "Update";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweqeewqe", "OpenPoup('divUniversityCategoryInsert','650px','sndAddCollegeTestomonial');return false;", true);
                }
               
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing RptTestimonialItemCommand in  AdminPanel/AdmissionjanklariTestimonial.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}