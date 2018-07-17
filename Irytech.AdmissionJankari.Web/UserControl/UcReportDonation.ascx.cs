using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using System.Net.Mail;
using System.Data;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcReportDonation : System.Web.UI.UserControl
    {
        Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            _objCommon = new Common();
            if (Request.QueryString["collegename"] != null)
                 BindCollege(Convert.ToString(Request.QueryString["collegename"]));
            if (Request.QueryString["CourseId"] != null)
                _objCommon.CourseId = Convert.ToInt16(Request.QueryString["CourseId"]);
            
            hndCourseId.Value = _objCommon.CourseId.ToString();
            ValidateControl();
            BindCourse();
        }




        // method to validate the Control
        protected void ValidateControl()
        {
            revAccusedEmaidId.ValidationExpression = ClsSingelton.aRegExpEmail;
            revAccusedMobileNo.ValidationExpression = ClsSingelton.aRegExpMobile;
            revMobile.ValidationExpression = ClsSingelton.aRegExpMobile;
            revUserEmailId.ValidationExpression = ClsSingelton.aRegExpEmail;
        }

        // Method  To Bind The Course 

        protected void BindCourse()
        {
            try
            {
                var data = CourseProvider.Instance.GetAllCourseList();
                _objCommon = new Common();
                data = data.Where(course => course.CourseStatus == true).ToList();
                if (data.Count > 0)
                {
                    ddlCourse.DataSource = data;
                    ddlCourse.DataTextField = "CourseName";
                    ddlCourse.DataValueField = "CourseId";
                    ddlCourse.DataBind();
                    ddlCourse.SelectedValue = Convert.ToString(_objCommon.CourseId);
                    _objCommon.CourseName = Utils.RemoveIllegealFromCourse(ddlCourse.SelectedItem.Text);
                    ddlCourse.Items.Insert(0, new ListItem("--Select--", "0"));
                 }
                else
                {
                    ddlCourse.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCourse in UcReportDonation.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        

        // Method to Clear The Control

        protected void ClearControl()
        {
            txtAccusedEmailId.Text = "";
            txtAccusedMobileNo.Text = "";
            txtaccusedName.Text = "";
            txtCollegeName.Text = "";
            txtUserEmailId.Text = "";
            txtUserMobile.Text = "";
            txtUserName.Text = "";
            ddlCourse.ClearSelection();
            txtIncident.Text = "";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            InsertUserDonationReqest();
            ClearControl();
        }

        // Method to Insert User Donation Request
        protected void InsertUserDonationReqest()
        {
            _objCommon = new Common();
            string errMsg="";


            try
            {
                if (Page.IsValid)
                {
                    int i = _objCommon.InsertUserDonationRequest(txtUserName.Text, txtUserMobile.Text, txtUserEmailId.Text, txtCollegeName.Text,
                                                            Convert.ToInt32(ddlCourse.SelectedValue), txtaccusedName.Text, txtAccusedEmailId.Text, txtAccusedMobileNo.Text, Convert.ToInt16(IryTech.AdmissionJankari.BO.Usertype.Student),
                                                            txtIncident.Text, out errMsg);
                    if (i > 0)
                    {

                        spnErrMsg.InnerHtml = errMsg;
                        spnErrMsg.Visible = true;
                        spnErrMsg.Focus();
                        MailTemplates _objmailTemplete = new MailTemplates();
                        var mail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "AdmissionJankari: Report Donation"
                        };
                        var body = _objmailTemplete.MailBodyForDonationUser(txtUserName.Text);
                        mail.To.Add(txtUserEmailId.Text);
                        mail.Body = body;
                        Utils.SendMailMessageAsync(mail);

                        mail = new MailMessage
                       {
                           From = new MailAddress(ApplicationSettings.Instance.Email),
                           Subject = "AdmissionJankari:Donation Complain"
                       };
                        body = _objmailTemplete.MailBodyForDonationAdmin(txtUserName.Text, txtUserEmailId.Text, txtUserMobile.Text, ddlCourse.SelectedItem.ToString(),
                                           txtCollegeName.Text, txtaccusedName.Text, txtAccusedMobileNo.Text, txtAccusedEmailId.Text, txtIncident.Text);

                        mail.To.Add(ClsSingelton.donationMailId);
                        mail.Body = body;
                        Utils.SendMailMessageAsync(mail);

                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUserDonationReqest in UcReportDonation.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindCollege(string collegeName)
        {
            Common objCommon = new Common();
            try
            {
                var collegeList = new Common().GetCollegeNameList(objCommon.CourseId);

                var query =
                    collegeList.Tables[0].AsEnumerable().Where(colleges => Utils.RemoveIllegalCharacters(collegeName).Equals(
                        Utils.RemoveIllegalCharacters(colleges.Field<string>("AjCollegeBranchName")),
                        StringComparison.OrdinalIgnoreCase)).Select(colleges => new
                        {
                            collegeBranchName =
                        colleges.Field<string>(
                            "AjCollegeBranchName")
                        }).FirstOrDefault();
                if (query != null)
                {
                    txtCollegeName.Text = query.collegeBranchName;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCollege in UcReportDonation.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

    }
}