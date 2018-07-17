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

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class Testimonials : System.Web.UI.UserControl
    {
        SecurePage objSecure = new SecurePage();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (objSecure.LoggedInUserId == 2)
            {
                BindTestimonialsData();
                txtTestimonial.Visible = false;
                chkStatus.Visible = false;
              btnAdd.Visible = false;

            }
            else
            {

                txtTestimonial.Visible = true;
                chkStatus.Visible = true;
                btnAdd.Visible = true;
                
            
            }
        if (IsPostBack) return;



            UcCustomPaging.PageSize = ApplicationSettings.Instance.CollegePageSize;
            UcCustomPaging.ButtonsCount = ApplicationSettings.Instance.CollegePageCount;
            UcCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;

        }


        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            lblSeccessMsg.Visible = false;
            var data = NewsArticleNoticeProvider.Instance.GetTestimonialsByUserId(objSecure.LoggedInUserId);
            if (data.Count > 0)
            {
                try
                {
                    rptTestimonilas.Visible = true;

                    UcCustomPaging.BindDataWithPaging(rptTestimonilas, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in Testimonilas.ascx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptTestimonilas.Visible = false;

            }
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string ErrorMsg = "";
            var insert = 0;
            try
            {
               
                TestimonialProperty objTestimonialProperty = new TestimonialProperty();

                if (objSecure.LoggedInUserId > 0)
                {
                    BindTestimonialsData();

                    objTestimonialProperty.UserID = objSecure.LoggedInUserId;
                    objTestimonialProperty.Testimonials = Convert.ToString(txtTestimonial.FckValue.Trim());
                    objTestimonialProperty.TestimonilaStatus = chkStatus.Checked;

                    if (btnAdd.Text == "Add")
                    {

                        insert = NewsArticleNoticeProvider.Instance.InsertTestimonilasDetails(objTestimonialProperty, objSecure.LoggedInUserId, out ErrorMsg);


                        // insert = ExamProvider.Instance.InsertExamDetails(objCollegeSpeech, LoggedInUserId, out ErrorMsg);
                        //BindExamMasterDetail();
                        //ClearFields();
                    }
                    else
                    {
                        objTestimonialProperty.TestimonialID = Convert.ToInt32(Request.QueryString["CollegeSpeechId"]);

                        insert = NewsArticleNoticeProvider.Instance.UpdateTestimonilasDetails(objTestimonialProperty, objSecure.LoggedInUserId, out ErrorMsg);

                        //BindExamMasterDetail();
                        //ClearFields();
                        btnAdd.Text = "Add";
                        //lblRecordsInserted.Text = "Insert";
                        //lblHeader.Text = "Add Exam Master";
                        lblInsertUpdate.Text = "Insert";
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

                else
                {

                    lblInsertUpdate.Text = "Please Login To Insert Testimonials";


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
        }
        private void ClearFields()
        {
            txtTestimonial.FckValue = string.Empty;
            chkStatus.Checked = false;
        
        }

        private void BindTestimonialsData()
        {
            var data = NewsArticleNoticeProvider.Instance.GetTestimonialsByUserId(objSecure.LoggedInUserId);
            if (data.Count > 0)
            {

                UcCustomPaging.BindDataWithPaging(rptTestimonilas, Common.ConvertToDataTable(data));


            
            }
        
        }

        protected void rptTestimonilas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblSeccessMsg.Visible = false;
            try
            {
                if (e.CommandName == "Edit")
                {
                    txtTestimonial.Visible = true;
                    chkStatus.Visible = true;
                    btnAdd.Visible = true;
                    hndCollegeTopHirer.Value = e.CommandArgument.ToString();
                   

                    var data = NewsArticleNoticeProvider.Instance.GetTestimonialsDetailsById(Convert.ToInt32(hndCollegeTopHirer.Value));


                    if (data.Count > 0)
                    {

                        var query = from result in data
                                    select new
                                    {
                                        UserId = result.UserID,
                                        Testimonial = result.Testimonials,
                                        TestimonialStatus = result.TestimonilaStatus,

                                    };
                        var sp = query.First();

                        txtTestimonial.FckValue = sp.Testimonial;


                        if (sp.TestimonialStatus == true)
                        {
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            chkStatus.Checked = false;
                        }
                        Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript",
                                                                 "OpenPoup('divAssociation',400,'lnkUpdate');", true);

                        //lblHeader.Text = "Add Exam Form Master";
                        btnAdd.Text = "Update";
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