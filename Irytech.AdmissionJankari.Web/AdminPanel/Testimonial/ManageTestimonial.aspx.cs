using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.Components.Web.Controls;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Testimonial
{
    public partial class ManageTestimonial : SecurePage
    {
        Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
            ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            GetAllTestimonials();
        }

        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            var TestimonilData = NewsArticleNoticeProvider.Instance.GetTestimonialsDetails();


            if (TestimonilData.Count > 0)
            {
                try
                {
                    rptTestimonilas.Visible = true;
                    rptTestimonilas.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptTestimonilas, Common.ConvertToDataTable(TestimonilData));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in ManageTestimonilas.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptTestimonilas.Visible = false;
                rptTestimonilas.Visible = true;
                lblErorrMsg.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string ErrorMsg = "";
            var insert = 0;
            try
            {

                var fileName = this.FIleUploder.UploadedImageName;
                if (fileName != null)
                {
                    hdnImageFile.Value = fileName;
                }

                TestimonialProperty objTestimonialProperty = new TestimonialProperty();


                objTestimonialProperty.UserID = LoggedInUserId;
                objTestimonialProperty.UserName = txtName.Text.Trim();
                objTestimonialProperty.Testimonials = Convert.ToString(txtTesimonial.FckValue.Trim());
                objTestimonialProperty.TestimonilaStatus = chkStatus.Checked;
                objTestimonialProperty.UserImage = hdnImageFile.Value;


                if (btnSubmit.Text == "Submit")
                {

                    insert = NewsArticleNoticeProvider.Instance.InsertTestimonilasDetails(objTestimonialProperty, LoggedInUserId, out ErrorMsg);
                    ClearControls();
                }
                else
                {
                    objTestimonialProperty.TestimonialID = Convert.ToInt32(hndCollegeTopHirer.Value);

                    insert = NewsArticleNoticeProvider.Instance.UpdateTestimonilasDetails(objTestimonialProperty, LoggedInUserId, out ErrorMsg);

                    //BindExamMasterDetail();
                    ClearControls();
                    btnSubmit.Text = "Submit";
                }
                if (insert > 0)
                {
                    lblSeccessMsg.Visible = true;
                    lblSeccessMsg.Text = ErrorMsg;
                }
                else
                {
                    lblErorrMsg.Visible = true;
                    lblErorrMsg.Text = ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                string addInfo = "Error  :: -> ";
                ClsExceptionPublisher objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            GetAllTestimonials();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                var TestimonilData = NewsArticleNoticeProvider.Instance.GetTestimonialsDetails();
                TestimonilData = TestimonilData.Where(result => result.UserName == txtSearch.Text.Trim()).ToList();
                if (TestimonilData.Count > 0)
                {

                    ucCustomPaging.BindDataWithPaging(rptTestimonilas, Common.ConvertToDataTable(TestimonilData));

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

            ClearControls();
        }
        private void ClearControls()
        {
            txtTesimonial.FckValue = string.Empty;
            chkStatus.Checked = false;
            txtName.Text = "";


        }

        public void GetAllTestimonials()
        {
            try
            {
                var TestimonilData = NewsArticleNoticeProvider.Instance.GetTestimonialsDetails();
                if (TestimonilData.Count > 0)
                {

                    ucCustomPaging.BindDataWithPaging(rptTestimonilas, Common.ConvertToDataTable(TestimonilData));

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  GetAllTestimonials in Testimonial.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }

        protected void rptTestimonilas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblSeccessMsg.Visible = false;
            _objCommon = new Common();
            try
            {
                if (e.CommandName == "Edit")
                {
                    hndCollegeTopHirer.Value = e.CommandArgument.ToString();
                    HiddenField hndCollegeBranchCourseID = (HiddenField)e.Item.FindControl("hndCollegeBranchCourseID");

                    var data = NewsArticleNoticeProvider.Instance.GetTestimonialsDetailsById(Convert.ToInt32(hndCollegeTopHirer.Value));


                    if (data.Count > 0)
                    {

                        var query = from result in data
                                    select new
                                    {
                                        UserId = result.UserID,
                                        Testimonial = result.Testimonials,
                                        TestimonialStatus = result.TestimonilaStatus,
                                        UserName = result.UserName,
                                        UserImage = result.UserImage,

                                    };
                        var sp = query.First();

                        txtTesimonial.FckValue = sp.Testimonial;
                        txtName.Text = sp.UserName;
                        string Img = sp.UserImage != "" ? sp.UserImage : "N/A";
                        var path = _objCommon.GetFilepath("NewsArticle");
                        hdnImageFile.Value = Img;

                        if (sp.TestimonialStatus == true)
                        {
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            chkStatus.Checked = false;
                        }


                        //lblHeader.Text = "Add Exam Form Master";
                        btnSubmit.Text = "Update";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweqeewqe", "OpenPoup('divStudentTestomonialInsert','650px','sndAddTestomonual');", true);
                    }


                }


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  rptCollegeList_ItemCommand in Testimonial.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


    }
}