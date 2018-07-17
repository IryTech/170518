using System;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class ReportAbuse : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
           
                BindAbuseType();
  
        }
        public string AbuseType
        {
            get; set;
        }
        public string AbuseTypeId {
            get; set; 
        }
        private void BindAbuseType()
        {
            var objDataset = new Common().GetAbuseList();
            if (objDataset.Rows.Count > 0)
            {
                ddlReportAbuseList.DataSource = objDataset;
                ddlReportAbuseList.DataTextField = "AjAbuseType";
                ddlReportAbuseList.DataValueField = "AjAbuseId";
                ddlReportAbuseList.DataBind();
                ddlReportAbuseList.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {
                ddlReportAbuseList.Items.Insert(0, new ListItem("Select", "0"));
                
            }
        }

        protected void btnAbuseReport_Click(object sender, EventArgs e)
        {
            var objMailTemplete = new MailTemplates();
            var errMsg = "";
            var objSecure = new SecurePage();
            var result = new Common().InsertAbuseReport(objSecure.LoggedInUserId,
                                                        Convert.ToInt32(ddlReportAbuseList.SelectedValue),
                                                        txtReportAbuseContent.Text.Trim(), AbuseType,
                                                        Convert.ToInt32(AbuseTypeId), out errMsg);

            if (result > 0)
            {

                var objMail = new MailMessage
                {
                    From = new MailAddress(ApplicationSettings.Instance.Email),
                    Subject ="Abuse content information"
                };
                var mailbody = objMailTemplete.MailToUserForAbuse(objSecure.LoggedInUserName, ddlReportAbuseList.SelectedItem.Text);
                objMail.Body = mailbody;
                objMail.To.Add(objSecure.LoggedInUserEmailId);
                objMail.IsBodyHtml = true;
                Utils.SendMailMessageAsync(objMail);

                var mail = new MailMessage
                {
                    From = new MailAddress(ApplicationSettings.Instance.Email),
                    Subject = "Abuse content information by " + objSecure.LoggedInUserName
                };
                var body = objMailTemplete.MailToAdminForAbuse(objSecure.LoggedInUserName, objSecure.LoggedInUserEmailId,
                                                               objSecure.LoggedInUserMobile, txtReportAbuseContent.Text,
                                                               ddlReportAbuseList.SelectedItem.Text);
                mail.Body = body;
                mail.To.Add(ClsSingelton.CommentMailId);
                mail.IsBodyHtml = true;
                Utils.SendMailMessageAsync(mail);  
            }
            ddlReportAbuseList.ClearSelection();
            txtReportAbuseContent.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>Message('" + result + "','" + errMsg + "');</script>", false);

        }
    }
}