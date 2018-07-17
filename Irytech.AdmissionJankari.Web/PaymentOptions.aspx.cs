using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.Components.Web.Controls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using System.Net.Mail;
using IntegrationKit;


namespace IryTech.AdmissionJankari20.Web
{
    public partial class PaymentOptions : PageBase
    {
        private Common _objCommon;
        private Consulling _objConsulling;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack) return;

            btnFinish.ValidationGroup = "VldgOnlinePayment";
            btnFinish.CausesValidation = true;
            ValidateControl();
            
            UpdateUserTranscationalDetails();
            BindUserDetailsForOnlinePayment();
            
        }

        
        // Method to Validate The Control
        private void ValidateControl()
        {
            revAddress.ValidationExpression = ClsSingelton.aRegExpAlphaNumSpaceStrict;
            revPincode.ValidationExpression = ClsSingelton.aRegExpZip;
        }


        private void OnlinePayment()
        {
            var objSecurePage = new SecurePage();
            _objCommon = new Common();

            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            var objMailTemplates = new MailTemplates();
            var formNumber = "ADMJ" + System.DateTime.Now.Year + _objCommon.CourseId.ToString() + objSecurePage.LoggedInUserId.ToString();
            var userDetails = UserManagerProvider.Instance.GetUserListById(objSecurePage.LoggedInUserId);
            
            var sp = userDetails.First();
            var objUserRegistrationProperty = new
                  UserRegistrationProperty
            {
                UserFullName = objSecurePage.LoggedInUserName,
                UserGender = sp.UserGender,
                UserEmailid = objSecurePage.LoggedInUserEmailId,
                MobileNo = objSecurePage.LoggedInUserMobile,
                PhoneNo = sp.PhoneNo,
                UserId = objSecurePage.LoggedInUserId,
                CourseId = sp.CourseId,
                UserCategoryId = objSecurePage.LoggedInUserType,
                UserDOB = sp.UserDOB,
                UserStatus = true,
                UserPassword = sp.UserPassword,
                UserPincode = txtPincode.Text.Trim(),
                UserCorrespondenceAddress = txtAddress.Text.Trim()
            };
            var errMsg = "";
            int i = UserManagerProvider.Instance.UpdateUserInfo(objUserRegistrationProperty, 1, out errMsg);
            var myUtility = new libfuncs();
            Merchant_Id.Value = "M_shi18022_18022";
            var bookSeatPayment="";
            if (Request.QueryString["BookSeatPayment"] != null)
            {
                bookSeatPayment = objCrypto.Decrypt(Request.QueryString["BookSeatPayment"].ToString());
            }
            Amount.Value =!string.IsNullOrEmpty(bookSeatPayment)?bookSeatPayment:"26100";
            Order_Id.Value = formNumber + DateTime.Now.ToString("hh:mm:ss");
            Redirect_Url.Value = "http://admissionjankari.com/ConformationPage.aspx?CID=" + objCrypto.Encrypt(objSecurePage.LoggedInUserEmailId) + "&frmNumber=" + objCrypto.Encrypt(formNumber) + "&UID=" + objCrypto.Encrypt(objSecurePage.LoggedInUserId.ToString());
            var workingKey = ClsSingelton.WorkingKey.Trim();
            Checksum.Value = myUtility.getchecksum(Merchant_Id.Value, Order_Id.Value, Amount.Value, Redirect_Url.Value, workingKey);
            billing_cust_name.Value = objSecurePage.LoggedInUserName;
            billing_cust_address.Value = txtAddress.Text.Trim();
            billing_cust_state.Value = txtState.Text;
            billing_cust_country.Value = "India";
            billing_cust_tel.Value = objSecurePage.LoggedInUserMobile;
            billing_cust_email.Value = objSecurePage.LoggedInUserEmailId;
            delivery_cust_name.Value = "";
            delivery_cust_address.Value = "";
            delivery_cust_state.Value = "";
            delivery_cust_country.Value = "";
            delivery_cust_tel.Value = "";
            billing_cust_city.Value = txtCity.Text;
            billing_zip_code.Value = txtPincode.Text.Trim();
            delivery_cust_city.Value = "";
            delivery_zip_code.Value = "";
            _objConsulling = new Consulling();
           
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey",
                                               "PostDForm();", true);

        }

        public RadioButtonList GetPaymentMode
        {
            get { return rbtnPaymentType; }
        }

        public string ConsullingCourseAmount
        {
            get;
            set;
        }
        protected void Finsh(object sender, EventArgs e)
        {
            var objSecurePage = new SecurePage();
            var objConsulling = new Consulling();
            _objCommon = new Common();
            var objMailTemplates = new MailTemplates();
            var ddNumber = string.Empty;
            var formNum = "ADMJ" + DateTime.Now.Year + _objCommon.CourseId.ToString() + objSecurePage.LoggedInUserId.ToString();
            var tranctionDetails = string.Empty;
            if (rbtnPaymentType.SelectedValue == "OnPayment")
                 OnlinePayment();
         
            int i = objConsulling.InsertUpdateUserTransctionalDetails(objSecurePage.LoggedInUserId, formNum, false, rbtnPaymentType.SelectedItem.ToString(),"","","26100");
            var mail = new MailMessage
            {
                From = new MailAddress(ApplicationSettings.Instance.Email),
                Subject = "Direct Admission:Form Number" + formNum
            };

            var body = objMailTemplates.SendValidationMailForTheBookSeat("http://www.admissionjankari.com/", objSecurePage.LoggedInUserName, formNum, rbtnPaymentType.SelectedItem.ToString(), _objCommon.CourseName);
            mail.Body = body;
            mail.To.Add(objSecurePage.LoggedInUserEmailId);
            mail.Bcc.Add(ClsSingelton.bccDirectAdmission);
            Utils.SendMailMessageAsync(mail);
            if (rbtnPaymentType.SelectedValue != "OnPayment")
                Response.Redirect(Utils.AbsoluteWebRoot + "ConformationPage.aspx/");
        }

        protected void rbtnPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnPaymentType.SelectedValue == "OnPayment")
            {
                btnFinish.ValidationGroup = "VldgOnlinePayment";
                btnFinish.CausesValidation = true;
            }
            else
            {
                btnFinish.CausesValidation = false;
            }
        }

        // Method to Bind The User Details if User Want to make the payment
        protected void GetUserDetails(string courseId, string userId)
        {
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            _objCommon = new Common();
            var objSecurePage = new SecurePage();
            try
            {
                courseId = objCrypto.Decrypt(courseId);
                userId = objCrypto.Decrypt(userId);
                _objCommon.CourseId = Convert.ToInt16(courseId);
                var userDetails = UserManagerProvider.Instance.GetUserListById(Convert.ToInt32(userId)).FirstOrDefault();
                if (userDetails != null)
                {
                    objSecurePage.LoggedInUserEmailId = userDetails.UserEmailid;
                    objSecurePage.LoggedInUserId = userDetails.UserId;
                    objSecurePage.LoggedInUserMobile = userDetails.MobileNo;
                    objSecurePage.LoggedInUserName = userDetails.UserFullName;
                    objSecurePage.LoggedInUserType = userDetails.UserCategoryId;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUserDetails in PaymentOptions.aspx for user :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void UpdateUserTranscationalDetails()
        {
            var objSecurePage = new SecurePage();
            _objCommon = new Common();

            _objConsulling = new Consulling();
            string formNum = "ADMJ" + DateTime.Now.Year + _objCommon.CourseId.ToString() + objSecurePage.LoggedInUserId.ToString();
            try
            {
                int i = _objConsulling.InsertUpdateUserTransctionalDetails(objSecurePage.LoggedInUserId, formNum,false, "", "", " ", "26100");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateUserTranscationalDetails in PaymentOptions.aspx for user :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        private void BindUserDetailsForOnlinePayment()
        {
            var objSecurePage = new SecurePage();
            try
            {
                if (objSecurePage.IsLoggedInUSer)
                {
                    var userDetails = UserManager.Instance.GetUserListById(objSecurePage.LoggedInUserId).FirstOrDefault();
                    if (userDetails != null)
                    {
                        txtAddress.Text = userDetails.UserCorrespondenceAddress;
                        txtCity.Text = userDetails.CityName;
                        txtState.Text = userDetails.StateName;
                        txtPincode.Text = userDetails.UserPincode;
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
                const string addInfo = "Error while executing BindUserDetailsForOnlinePayment in PaymentOptions.aspx for user :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}