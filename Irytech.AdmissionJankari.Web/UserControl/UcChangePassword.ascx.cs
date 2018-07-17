using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using System.Net.Mail;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcChangePassword : System.Web.UI.UserControl
    {
        MailTemplates _objMailTemplete;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            ValidateControl();
        }

        #region Method
        // Method To Validate Control
        protected void ValidateControl()
        {
            Common objCommon = new Common();
            rfvEmailId.ErrorMessage = objCommon.GetValidationMessage("rfvEmailId");
            rfvNewPassword.ErrorMessage = objCommon.GetValidationMessage("rfvNewPassword");
            rfvOldPassword.ErrorMessage = objCommon.GetValidationMessage("rfvOldPassword");
            revEmailId.ValidationExpression = ClsSingelton.aRegExpEmail;
            revEmailId.ErrorMessage = objCommon.GetValidationMessage("revEmail");
           
        }

        // Method To Change Passord

        protected int ChangePassword(int userId, string oldPassword, string newPassword)
        {
            _objMailTemplete = new MailTemplates();
           
            string errMsg = "";
            int i = 0;
            try
            {
                 i = UserManagerProvider.Instance.ChangePassword(userId, oldPassword, newPassword, out errMsg);
               
                lblMsg.Visible = true;
                lblMsg.Text = errMsg;
                if (i > 0)
                    lblMsg.CssClass = "Sucess";
                
                else
                    lblMsg.CssClass = "Error";
                
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in GETTING ChangePassword in UcChangePassword.axcs :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

        #endregion

        #region Event
        protected void BtnChangePasswordClick(object sender, EventArgs e)
        {
            SecurePage objSecurePage=new SecurePage();
          
            try
            {
                int i = ChangePassword(objSecurePage.LoggedInUserId, txtOldPassword.Text, txtNewPassword.Text);

                if (i > 0)
                {
                    var mail = new MailMessage
                    {
                        From = new MailAddress(ApplicationSettings.Instance.Email),
                        Subject = "AdmissionJankari: Password reset "
                    };
                    var body = _objMailTemplete.MailBodyForGetPassword(objSecurePage.LoggedInUserEmailId, txtNewPassword.Text, objSecurePage.LoggedInUserName);
                     mail.Body = body;
                    mail.To.Add(txtEmailId.Text);
                    mail.IsBodyHtml = true;
                    Utils.SendMailMessageAsync(mail);
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BtnChangePasswordClick in UcChangePassword.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        #endregion


    }
}