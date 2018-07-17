using System;
using System.Linq;
using IryTech.AdmissionJankari.Components.Web.Controls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using System.Net.Mail;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web
{
    public partial class Testimonial : PageBase
    {
        #region "DataMembers"

        readonly SecurePage _objSecurePage = new SecurePage();
        #endregion

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UID"] != null)
            {
                FillControls();
            }
            ValidateControls();
        }
        protected void BtnSaveClick(object sender, EventArgs e)
        {
            InsertUserTestimonial();
            ClearControls();
        }        
        #endregion

        #region "Method"

        private void InsertUserTestimonial()
        {
            try
            {
                if (Page.IsValid)
                {
                    var objmailTemplete = new MailTemplates();
                    var objTestimonialProperty = new TestimonialProperty();
                    int i,insert = 0; string errMsg = "";
                    if (string.IsNullOrWhiteSpace(txtTesimonial.FckValue)) { RfvFck.Text = "Testimonial cannot be blank"; RfvFck.Visible = true; return; }
                    if (Session["UID"] == null)
                    {
                        var objUserRegistrationProperty = new UserRegistrationProperty
                        {
                            UserFullName = txtUserName.Text.Trim(),
                            UserEmailid = txtUserEmailId.Text.Trim(),
                            MobileNo = txtUserMobile.Text.Trim(),
                            UserCategoryId = Convert.ToInt16(Usertype.Student),
                            UserPassword = txtUserMobile.Text.Trim(),
                            CourseId = Convert.ToInt32(new Common().CourseId),
                        };

                        i = UserManagerProvider.Instance.InsertUserInfo(objUserRegistrationProperty, Convert.ToInt16(Usertype.Student), out errMsg);

                        if (i > 0)
                        {
                            objTestimonialProperty.UserID = Session["UID"] == null ? Convert.ToInt32(objUserRegistrationProperty.UserId) : _objSecurePage.LoggedInUserId;
                            objTestimonialProperty.Testimonials = Convert.ToString(txtTesimonial.FckValue.Trim());
                            insert = NewsArticleNoticeProvider.Instance.InsertTestimonilasDetails(objTestimonialProperty, 1, out errMsg);
                            var mail = new MailMessage
                            {
                                From = new MailAddress(ApplicationSettings.Instance.Email),
                                Subject = "AdmissionJankari: Registration mail "
                            };
                            var body = objmailTemplete.MailBodyForRegistation(txtUserName.Text.Trim(), txtUserEmailId.Text.Trim(), txtUserMobile.Text.Trim());
                            mail.Body = body;
                            mail.To.Add(objUserRegistrationProperty.UserEmailid);
                            Utils.SendMailMessageAsync(mail);
                        }
                        else
                        {
                            var userDetails = UserManagerProvider.Instance.GetUserListByEmailId(txtUserEmailId.Text.Trim());
                            if (userDetails != null)
                            {                                
                                    var query1 = userDetails.First();
                                    _objSecurePage.LoggedInUserId = query1.UserId;
                                    _objSecurePage.LoggedInUserType = query1.UserCategoryId;
                                    _objSecurePage.LoggedInUserEmailId = query1.UserEmailid;
                                    _objSecurePage.LoggedInUserName = query1.UserFullName;
                                    _objSecurePage.LoggedInUserMobile = query1.MobileNo;                                

                                objTestimonialProperty.UserID = _objSecurePage.LoggedInUserId;
                                objTestimonialProperty.Testimonials = Convert.ToString(txtTesimonial.FckValue.Trim());
                                insert = NewsArticleNoticeProvider.Instance.InsertTestimonilasDetails(objTestimonialProperty, 1, out errMsg);
                            }
                        }
                    }
                    else
                    {
                        objTestimonialProperty.UserID = Convert.ToInt32(Session["UID"]);
                        objTestimonialProperty.Testimonials = Convert.ToString(txtTesimonial.FckValue.Trim());
                        insert = NewsArticleNoticeProvider.Instance.InsertTestimonilasDetails(objTestimonialProperty, 1, out errMsg);
                    }
                   
                    RfvFck.Visible = false;
                    spnErrMsg.InnerHtml = "Your testimonial is successfully submitted";
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing InsertUserInfo in Testimonial.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void ValidateControls()
        {
            revUserEmailId.ValidationExpression = ClsSingelton.aRegExpEmail;
            revMobile.ValidationExpression = ClsSingelton.aRegExpMobile;
        }
        protected void BtnClearClick(object sender, EventArgs e)
        { ClearControls(); spnErrMsg.Visible = false; }

        private void ClearControls()
        {
            txtTesimonial.FckValue = "";
            spnErrMsg.Visible = true;          
        }

        private void FillControls()
        {
            txtUserName.Text = _objSecurePage.LoggedInUserName;
            txtUserEmailId.Text = _objSecurePage.LoggedInUserEmailId;
            txtUserMobile.Text = _objSecurePage.LoggedInUserMobile;
            txtUserName.ReadOnly = true;
            txtUserEmailId.ReadOnly=true;
            txtUserMobile.ReadOnly = true;
        }
        #endregion
    }
}