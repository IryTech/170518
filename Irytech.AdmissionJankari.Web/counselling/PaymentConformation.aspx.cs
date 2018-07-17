using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.Components.Web.Controls;
using IryTech.AdmissionJankari.BL;
using System.Net.Mail;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.counselling
{
    public partial class PaymentConformation : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {

                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString[0])))
                {
                    UpdateUserTransctionalDetails(Convert.ToString(Request.QueryString[0]),Convert.ToString(Request.QueryString[1]),Convert.ToString(Request.QueryString[2]));
                }
            }
            else
            {
               // Response.Redirect("~/counselling/StudentCounselling.aspx");
            }
        }


        // Method to Update The Transctionla details 

        protected void UpdateUserTransctionalDetails(string euserEmailId,string efrmNumber,string  euserId)
        {
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            Consulling _objConsulling = new Consulling();
            MailTemplates objMailTemplates = new MailTemplates();
            SecurePage objSecure = new SecurePage();
            euserEmailId = euserEmailId.Replace(" ", "+");
            string emailId = Convert.ToString(objCrypto.Decrypt(euserEmailId));
            try
            {
                euserId = euserId.Replace(" ", "+");
                string userId = Convert.ToString(objCrypto.Decrypt(euserId));
                string frmNumber = efrmNumber.Replace(" ", "+");
                frmNumber = Convert.ToString(objCrypto.Decrypt(frmNumber));
               int  i = _objConsulling.InsertUpdateUserTransctionalDetails(Convert.ToInt32(userId), frmNumber, true, "online", "PNB", Convert.ToString(frmNumber + DateTime.Now.ToString("hh:mm:ss")));
               if (i > 0)
               {
                   var UserDetails = UserManager.Instance.GetUserListById(Convert.ToInt32(userId));
                   var sp = UserDetails.First();
                   objSecure.LoggedInUserEmailId = emailId;
                   objSecure.LoggedInUserId = sp.UserId;
                   objSecure.LoggedInUserName = Common.GetStringProperCase(sp.UserFullName);
                   objSecure.LoggedInUserType = sp.UserCategoryId;
                   objSecure.LoggedInUserMobile = sp.MobileNo;
                   var mail = new MailMessage
                   {
                       From = new MailAddress(ApplicationSettings.Instance.Email),
                       Subject = "AdmissionJankari:Direct Admission Payment confirmation for form number:" + frmNumber
                   };
                   var body = objMailTemplates.MailBodyForPaymentConformation("http://www.admissionjankari.com/", sp.UserFullName, frmNumber, "Online", Convert.ToString(frmNumber + DateTime.Now.ToString("hh:mm:ss")));
                   mail.Body = body;
                   mail.To.Add(emailId);
                   mail.Bcc.Add(ClsSingelton.bccDirectAdmission);
                   Utils.SendMailMessageAsync(mail);
                   lblUserName.InnerHtml = Common.GetStringProperCase(sp.UserFullName);
                   lblFormNumber.InnerHtml = frmNumber;
               }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateUserTransctionalDetails in PaymentConformation.aspx for user :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}