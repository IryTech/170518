using System;
using System.Net.Mail;
using System.Data;
using System.Linq;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.Components.Web.Controls;
using System.Web.UI.HtmlControls;

namespace IryTech.AdmissionJankari20.Web
{
    public partial class Refund : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            ValidateControl();
        }
        protected void ValidateControl()
        {
            validateEmail.ValidationExpression = ClsSingelton.aRegExpEmail;
        }
        protected void sendMailtoUser(string courseName)
        {
           
                var objMailTemplete = new MailTemplates();
                var objMail = new MailMessage
                {
                    From = new MailAddress(ApplicationSettings.Instance.Email),
                    Subject = "Refund Application Received"
                };
                var mailbody = objMailTemplete.MailToUserforRefundSent(txtRefundName.Text.Trim().ToString(), txtRefundForm.Text.Trim().ToString(), DateTime.Today.ToString("dd/MM/yyyy"), courseName);

                objMail.Body = mailbody;
                
                objMail.To.Add(txtRefundEmailId.Text.Trim().ToString());
                objMail.IsBodyHtml = true;
                Utils.SendMailMessageAsync(objMail);
           
        }
        protected void sendMailtoAdmin(string courseName)
        {
            var objMailTemplete = new MailTemplates();
            var objMail = new MailMessage
            {
                From = new MailAddress(ApplicationSettings.Instance.Email),
                Subject = "Request the refund for the form number " + txtRefundForm.Text.Trim().ToString()
            };
            var mailbody = objMailTemplete.MailToAdminforRefundSent(txtRefundName.Text.Trim().ToString(), txtRefundEmailId.Text.Trim().ToString(), txtRefundForm.Text.Trim().ToString(), DateTime.Today.ToString("dd/MM/yyyy"), courseName);

            objMail.Body = mailbody;
            
            objMail.To.Add(ClsSingelton.donationMailId);
            
            objMail.IsBodyHtml = true;
            Utils.SendMailMessageAsync(objMail);

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            string Errormsg = "";
            var insert = 0;
            
            try
            {
               
                insert = new Consulling().InsertUserRefund(txtRefundName.Text.Trim().ToString(), txtRefundEmailId.Text.Trim().ToString(), txtRefundForm.Text.Trim().ToString(), out Errormsg);
                if (insert > 0)
                {
                    var userData = UserManagerProvider.Instance.GetUserListByEmailId(txtRefundEmailId.Text.Trim().ToString());
                    userData = userData.Where(result => result.UserEmailid == txtRefundEmailId.Text.Trim().ToString()).ToList();
                    sendMailtoUser(userData.First().CourseName.Trim());
                    sendMailtoAdmin(userData.First().CourseName.Trim());
                }
                lblMsg.CssClass=insert>0?"success":"error";
                lblMsg.Text=Errormsg;
                clearForm();
                lblMsg.Visible=true;
            }
            catch(Exception ex)
            {
                 var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  btnSubmit_Click in Refund.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearForm();
        }
        protected void clearForm()
        {
            txtRefundEmailId.Text = "";
            txtRefundForm.Text = "";
            txtRefundName.Text = "";
            lblMsg.Visible = false;
        }
    }
}