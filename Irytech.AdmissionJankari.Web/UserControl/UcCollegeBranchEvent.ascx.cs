using System;
using System.Linq;
using System.Net.Mail;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using System.Data;
using System.Web.UI;
namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcCollegeBranchEvent : System.Web.UI.UserControl
    {
        private Common _objCommon;
        private UserRegistrationProperty _objUserRegistrationProperty;
        private SecurePage _objSecurePage;
        protected void Page_Load(object sender, EventArgs e)
        {
                     
        }

        // Method to get the College Event 
        public void GetCollegeEventList(int collegeBranchCourseId)
        {
            try
            {
                var data = CollegeProvider.Instance.GetEventByCollege(collegeBranchCourseId).AsEnumerable().Where(row => row.Field<bool>("AjCollegeEventStatus") == true && row.Field<DateTime>("AjCollegeEventDate") >= DateTime.Now);
                if (data.Count() > 0)
                {
                    var eventData = data.CopyToDataTable();
                    rptCollegeEvent.DataSource = eventData;
                    rptCollegeEvent.DataBind();
                    Event.Visible = true;
                    ShowEvent = true;
                }
                else
                {
                    Event.Visible = false;
                    ShowEvent = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  GetCollegeEventList in UcCollegeBranchEvent.asxc :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        public bool ShowEvent
        {
            get;
            set;

        }
        public int CollegeBranchCourseId
        {
            get { return Convert.ToInt32(ViewState["CollegeBranchCourseId"]); }
            set { ViewState["CollegeBranchCourseId"] = value; }
        }
        public int CourseId
        {
            get { return Convert.ToInt32(ViewState["CourseId"]); }
            set { ViewState["CourseId"] = value; }
        }
        public string CityName
        {
            get { return Convert.ToString(ViewState["CityName"]); }
            set { ViewState["CityName"] = value; }
        }
        public string CollegeName
        {
            get { return Convert.ToString(ViewState["CollegeName"]); }
            set { ViewState["CollegeName"] = value; }
        }
        //private string BindPaymentAmount(int courseId)
        //{
        //    Consulling _objConsulling = new Consulling();
        //    var amount = "";
        //    try
        //    {
        //        amount = _objConsulling.GetPayemntAmountAccordingToCourse(courseId);
        //    }
        //    catch (Exception ex)
        //    {
        //        var err = ex.Message;
        //        if (ex.InnerException != null)
        //        {
        //            err = err + " :: Inner Exception :- " + ex.ToString();
        //        }
        //        const string addInfo = "Error while executing BindPaymentAmount in UcCollegeBranchEvent.aspx  :: -> ";
        //        var objPub = new ClsExceptionPublisher();
        //        objPub.Publish(err, addInfo);
        //    }
        //    return amount;
        //}
        protected void BtnRegisterClick(object sender, EventArgs e)
        {
            MailTemplates _objmailTemplete = new MailTemplates();
            _objSecurePage = new SecurePage();

            try
            {
                _objUserRegistrationProperty = new UserRegistrationProperty
                {
                    UserFullName = txtEventUser.Text.Trim(),
                    UserEmailid = txtEmailIdEvent.Text.Trim(),
                    MobileNo = txtMobileEvent.Text.Trim(),
                    UserCategoryId = Convert.ToInt16(Usertype.Student),
                    CourseId = CourseId,
                    UserPassword = txtMobileEvent.Text.Trim()
                };
                var errMsg = "";
                var result = UserManagerProvider.Instance.InsertUserInfo(_objUserRegistrationProperty, Convert.ToInt16(Usertype.Student), out errMsg);
                if (result > 0)
                {
                    var mail = new MailMessage
                    {
                        From = new MailAddress(ApplicationSettings.Instance.Email),
                        Subject = "AdmissionJankari: Registation mail "
                    };
                    var body = _objmailTemplete.MailBodyForRegistation(txtEventUser.Text, txtEmailIdEvent.Text, txtMobileEvent.Text);
                    mail.Body = body;
                    mail.To.Add(_objUserRegistrationProperty.UserEmailid);
                    Utils.SendMailMessageAsync(mail);
                    var result1 = UserManagerProvider.Instance.GetUserListByEmailId(txtEmailIdEvent.Text.Trim());

                    var mail1 = new MailMessage
                    {
                        From = new MailAddress(ApplicationSettings.Instance.Email),
                        Subject = "AdmissionJankari: Event attending by user-"+txtEventUser.Text
                    };
                    var body1 = _objmailTemplete.MailToAdminRegardingEventAttendingByStudent(txtEventUser.Text, txtEmailIdEvent.Text, txtMobileEvent.Text, CollegeName,hdnCollegeEvent.Value,hdnCollegeDate.Value);
                    mail1.Body = body1;
                    mail1.To.Add(ClsSingelton.donationMailId);
                    Utils.SendMailMessageAsync(mail1);
                  
                    if (result1.Count > 0)
                    {
                        var query1 = result1.First();
                        _objSecurePage.LoggedInUserId = query1.UserId;
                        _objSecurePage.LoggedInUserType = query1.UserCategoryId;
                        _objSecurePage.LoggedInUserEmailId = query1.UserEmailid;
                        _objSecurePage.LoggedInUserName = query1.UserFullName;
                        _objSecurePage.LoggedInUserMobile = query1.MobileNo;
                        var collegeInsert = new Consulling().InsertStudentCollegePrefrance(_objSecurePage.LoggedInUserId, CollegeName, CourseId);
                        var cityInsert = new Consulling().InsertStudentCityPrefrance(_objSecurePage.LoggedInUserId, CityName);
                        var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
                      //  var amount = BindPaymentAmount(CourseId);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>EventMessage();</script>", false);
                        //Response.Redirect("/PaymentOptions.aspx?BookSeatPayment=" + objCrypto.Encrypt(amount));
                    }
                }
                else
                {
                    var result1 = UserManagerProvider.Instance.GetUserListByEmailId(txtEmailIdEvent.Text.Trim());

                    if (result1.Count > 0)
                    {

                        var query1 = result1.First();
                        _objSecurePage.LoggedInUserId = query1.UserId;
                        _objSecurePage.LoggedInUserType = query1.UserCategoryId;
                        _objSecurePage.LoggedInUserEmailId = query1.UserEmailid;
                        _objSecurePage.LoggedInUserName = query1.UserFullName;
                        _objSecurePage.LoggedInUserMobile = query1.MobileNo;

                        var collegeInsert = new Consulling().InsertStudentCollegePrefrance(_objSecurePage.LoggedInUserId, CollegeName, CourseId);
                        var cityInsert = new Consulling().InsertStudentCityPrefrance(_objSecurePage.LoggedInUserId, CityName);
                        var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
                       // var amount = BindPaymentAmount(CourseId);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>EventMessage();</script>", false);
                       // Response.Redirect("/PaymentOptions.aspx?BookSeatPayment=" + objCrypto.Encrypt(amount));
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
                const string addInfo = "Error while executing BtnRegisterClick in UcCollegeBranchEvent.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}