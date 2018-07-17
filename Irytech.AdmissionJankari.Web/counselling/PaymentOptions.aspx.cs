using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.Components.Web.Controls;
using IryTech.AdmissionJankari.BL;
using System.Net.Mail;
using IntegrationKit;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.counselling
{
    public partial class PaymentOptions : PageBase
    {
      
        Common _ObjCommon;
        Consulling _objConsulling;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request.QueryString.Count > 1)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString[0])) && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString[1])))
                {
                    GetUserDetails(Convert.ToString(Request.QueryString[0]), Convert.ToString(Request.QueryString[1]));
                }
            }
                
            btnFinish.ValidationGroup = "VldgOnlinePayment";
            btnFinish.CausesValidation = true;
            _ObjCommon = new Common();
            BindPaymentAmount(_ObjCommon.CourseId);
            ValidateControl();
            //Method to Bind the details for the online Payment 
            BindUserDetailsForOnlinePayment();
           
        }
        // Method to Bind Payment Amount
        private void BindPaymentAmount(int courseId)
        {
            _objConsulling = new Consulling();


            try
            {
                lblCheque.Text = _objConsulling.GetPayemntAmountAccordingToCourse(courseId);
                lblDemand.Text = lblCheque.Text;
                lblCash.Text = lblCheque.Text;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindPaymentAmount in PaymentOption.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
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
            _ObjCommon = new Common();

            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            var objMailTemplates = new MailTemplates();
            var formNumber = "ADMJ" + DateTime.Now.Year + _ObjCommon.CourseId.ToString(CultureInfo.InvariantCulture) + objSecurePage.LoggedInUserId.ToString(CultureInfo.InvariantCulture);
            ConsullingCourseAmount = lblCash.Text;
            string transectionDetails = "You have selected the payment mode through Online Payment of" + " " + "Rs." + " " + lblCash.Text + "/- ";
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
            var i = UserManagerProvider.Instance.UpdateUserInfo(objUserRegistrationProperty, 1, out errMsg);
            var mail = new MailMessage
            {
                From = new MailAddress(ApplicationSettings.Instance.Email),
                Subject = "Direct Admission:Form Number " + formNumber
            };
            var body = objMailTemplates.SendValidationMailForTheDirectAdmission("http://www.admissionjankari.com/", objSecurePage.LoggedInUserName, formNumber, transectionDetails);
            mail.Body = body;
            mail.To.Add(objSecurePage.LoggedInUserEmailId);
            mail.Bcc.Add(ClsSingelton.bccDirectAdmission);
            Utils.SendMailMessageAsync(mail);
            libfuncs myUtility = new libfuncs();
            Merchant_Id.Value = "M_shi18022_18022";
            Amount.Value =lblCash.Text;
            Order_Id.Value = formNumber + DateTime.Now.ToString("hh:mm:ss");
            Redirect_Url.Value = Utils.AbsoluteWebRoot+ "ConformationPage.aspx?CID=" + objCrypto.Encrypt(objSecurePage.LoggedInUserEmailId) + "&frmNumber=" + objCrypto.Encrypt(formNumber) + "&UID=" + objCrypto.Encrypt(objSecurePage.LoggedInUserId.ToString());
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
            i = _objConsulling.InsertUpdateUserTransctionalDetails(objSecurePage.LoggedInUserId, formNumber);
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
            string BankName = string.Empty;
            SecurePage _objSecurePage = new SecurePage();
            Consulling _ObjConsulling = new Consulling();
            _ObjCommon = new Common();
            MailTemplates objMailTemplates = new MailTemplates();
            string DDNumber = string.Empty;
            string FormNum = "ADMJ" + System.DateTime.Now.Year + _ObjCommon.CourseId.ToString() + _objSecurePage.LoggedInUserId.ToString();
            string TranctionDetails = string.Empty;
            if (rbtnPaymentType.SelectedValue == "0")
            {
                BankName = "";
                DDNumber = "";
                TranctionDetails = " You have selected the payment mode through cheque. Please make an account payee cheque of Rs. " + lblCash.Text + " in favour of <b>" + "Admissionjankari.com " + " </b><br/><br/>";
                TranctionDetails = TranctionDetails + "  Mention your Reference id (Application form number), Name, Phone No and Email id at the back of the cheque.<br/><br/>";
                TranctionDetails = TranctionDetails + " To confirm the payment, please send your cheque at the following address (Via Speed/Registered Post): ";
                TranctionDetails = TranctionDetails + " <br /><br />" + " Admissionjankari.com";
                TranctionDetails = TranctionDetails + "<br/>74 Amrit Chamber, 2nd floor, <br />" + "  202-204 Scindia House Connaught Place, <br />" + " New Delhi-110001. <br />" + "  Contact us : +91-11-43391978, +91-8800567711, +91-8800567733";
            }
            else
            {
                if (rbtnPaymentType.SelectedValue == "1")
                {
                    BankName = "";
                    DDNumber = "";
                    TranctionDetails = "You have selected the payment mode through DD.<br/>  ";
                    TranctionDetails = TranctionDetails + "   <b>Make a single Demand Draft</b> (DD) of Rs.  " + lblCash.Text + " in favour of <b>" + " Admissionjankari.com" + "</b> Payable at <b>New Delhi.</b><br/><br/>";
                    TranctionDetails = TranctionDetails + "  Mention your Reference id (Application form number), Name, Phone No and Email id at the back of the Demand draft.<br/><br/>";
                    TranctionDetails = TranctionDetails + "To confirm the payment, please send your Demand Draft at the following address (Via Speed/Registered Post):";
                    TranctionDetails = TranctionDetails + "<br /><br />  Admissionjankari.com<br/>";
                    TranctionDetails = TranctionDetails + "74 Amrit Chamber, 2nd floor, <br />" + "  202-204 Scindia House Connaught Place, <br />" + " New Delhi-110001. <br />" + "   Contact us : +91-11-43391978, +91-8800567711, +91-8800567733<br/>";
                }
                else
                {
                    if (rbtnPaymentType.SelectedValue == "2")
                    {
                        const string bankName = "<b>Account Name :</b> Admissionjankari.com";
                        const string ddNumber = "00032 0000 44418";
                        TranctionDetails = "You have selected the payment mode through cash. You will need to deposit Rs." + lblCash.Text + " in the nearest HDFC Bank in the following account.   <br/><br/> ";
                        TranctionDetails = TranctionDetails + bankName + "<br/>";
                        TranctionDetails = TranctionDetails + "<b>Account Number :<b>" + ddNumber + " <br/>";
                        TranctionDetails = TranctionDetails + "<b>RTGS/IFSC/NEFT Code :</b> HDFC0000003 <br/>";
                        TranctionDetails = TranctionDetails + "<b>Branch : </b>Kasturba Gandhi Marg,New Delhi<br/>";
                        TranctionDetails = TranctionDetails + " <br /><br /> To confirm the payment, please send your pay-in-slip at the following address (Via Speed/Registered Post):";
                        TranctionDetails = TranctionDetails + "<br /><br />" + "Admissionjankari.com<br/>";
                        TranctionDetails = TranctionDetails + "74 Amrit Chamber, 2nd floor, <br />" + "  202-204 Scindia House Connaught Place, <br />" + " New Delhi-110001. <br />" + "   Contact us : +91-11-43391978, +91-8800567711, +91-8800567733<br/>";
                    }
                    else
                    {

                        TranctionDetails = "You have selected the payment mode through Online Payment of" + " " + "Rs." + " " + lblCash.Text + "/- ";
                        OnlinePayment();
                    }
                }

            }
            int i = _ObjConsulling.InsertUpdateUserTransctionalDetails(_objSecurePage.LoggedInUserId, FormNum, false, rbtnPaymentType.SelectedItem.ToString());
            var mail = new MailMessage
            {
                From = new MailAddress(ApplicationSettings.Instance.Email),
                Subject = "Direct Admission:Form Number" + FormNum
            };
            var body = objMailTemplates.SendValidationMailForTheDirectAdmission("http://www.admissionjankari.com/", _objSecurePage.LoggedInUserName, FormNum, TranctionDetails);
            mail.Body = body;
            mail.To.Add(_objSecurePage.LoggedInUserEmailId);
            mail.Bcc.Add(ClsSingelton.bccDirectAdmission);
            Utils.SendMailMessageAsync(mail);
            if(rbtnPaymentType.SelectedValue != "OnPayment")
                Response.Redirect(Utils.AbsoluteWebRoot+Utils.RemoveIllegealFromCourse(new Common().CourseName.ToLower())+"/counselling/thankyou/");
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
        protected void GetUserDetails(string courseId,string userId)
        {
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            _ObjCommon = new Common();
            SecurePage objSecurePage = new SecurePage();
            try
            {
                courseId = courseId.Replace(" ", "+");
                courseId = objCrypto.Decrypt(courseId);
                userId = userId.Replace(" ", "+");
                userId = objCrypto.Decrypt(userId);
                _ObjCommon.CourseId = Convert.ToInt16(courseId);
                var UserDetails = UserManager.Instance.GetUserListById(Convert.ToInt32(userId)).FirstOrDefault();
                if (UserDetails != null)
                {
                    objSecurePage.LoggedInUserEmailId = UserDetails.UserEmailid;
                    objSecurePage.LoggedInUserId = UserDetails.UserId;
                    objSecurePage.LoggedInUserMobile = UserDetails.MobileNo;
                    objSecurePage.LoggedInUserName = UserDetails.UserFullName;
                    objSecurePage.LoggedInUserType = UserDetails.UserCategoryId;
                    
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

        // Method to Bind the details for the online pyamnet 

        private void BindUserDetailsForOnlinePayment()
        {
            SecurePage objSecurePage = new SecurePage();
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