using System;
using System.Net.Mail;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using System.Globalization;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class ChangePassword : System.Web.UI.UserControl
    {
       
        private Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if(IsPostBack)return;
            ValidateFields();
            txtEmail.Enabled = false;
            if (Request.QueryString["Email"] != null)
            {
               
                txtEmail.Text = Request.QueryString["Email"];
            }
        }
        private void ValidateFields()
        {
            _objCommon = new Common();
            rfvEmail.ErrorMessage = _objCommon.GetValidationMessage("rfvEmailId") ?? "N/A";
            rfvNewPassword.ErrorMessage = _objCommon.GetValidationMessage("rfvNewPassword") ?? "N/A";
            rfvOldPassword.ErrorMessage = _objCommon.GetValidationMessage("rfvOldPassword") ?? "N/A";
            rfvConfirmPassword.ErrorMessage = _objCommon.GetValidationMessage("rfvConfirmPassword") ?? "N/A";
            revEmail.ValidationExpression = ClsSingelton.aRegExpEmail;
            revEmail.ErrorMessage = _objCommon.GetValidationMessage("revEmail") ?? "N/A";
            confrmPassord.ErrorMessage = _objCommon.GetValidationMessage("cmpPassord") ?? "N/A";
        }

        protected void BtnChangePasswordClick(object sender, EventArgs e)
        {
            MailTemplates _objClsMailTemplete = new MailTemplates();
            
            try
            {
                string errMsg;
                var result = UserManagerProvider.Instance.ChangePassword(1, txtOldPassword.Text.Trim(),
                                                                         txtNewPassword.Text.Trim(), out errMsg);
                if (result > 0)
                {
                    lblSuccess.Visible = true;
                    lblSuccess.Text = errMsg;
                  
                    var mail = new MailMessage
                                   {
                                       From = new MailAddress(ApplicationSettings.Instance.Email),
                                       Subject = "AdmissionJankari: Change passowrd mail "
                                   };

                    var body = "";// _objClsMailTemplete.GetChangeSendPwd("http://www.admissionjankari.com/", txtEmail.Text.Trim(), txtNewPassword.Text);
                    mail.Body = body;
                    mail.To.Add(txtEmail.Text);
                    mail.IsBodyHtml = true;
                    Utils.SendMailMessageAsync(mail);

                }
                else
                {
                    lblInfo.Visible = true;
                    lblInfo.Text = errMsg;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing ChangePassword in ChangePassword.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
           
        }
        
    }
}