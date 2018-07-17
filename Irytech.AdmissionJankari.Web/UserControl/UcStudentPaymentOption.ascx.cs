using System;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using System.Net.Mail;
using IntegrationKit;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcStudentPaymentOption : System.Web.UI.UserControl
    {
        Consulling _objConsulling;
        Common _ObjCommon;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

              
                 _ObjCommon=new Common();
               BindPaymentAmount(_ObjCommon.CourseId);
               ValidateControl();
               BindUserDetailsForOnlinePayment();
        }


        // Method to Bind Payment Amount
        private void BindPaymentAmount(int courseId)
        {
            _objConsulling = new Consulling();
            var objSecurePage = new SecurePage();


            try
            {
                if (objSecurePage.IsLoggedInUSer)
                {
                    var data = _objConsulling.GetPaymentTransactionStatus(objSecurePage.LoggedInUserId);
                    if (data != null && data.Count > 0)
                    {
                        var data1 = data.FirstOrDefault();
                        if (data1 != null)
                        {
                            lblCheque.Text = data1.PaymentAmount;
                            lblDemand.Text = data1.PaymentAmount;
                            lblCash.Text = data1.PaymentAmount;
                        }
                    }
                    else
                    {
                        lblCheque.Text = _objConsulling.GetPayemntAmountAccordingToCourse(courseId);
                        lblDemand.Text = lblCheque.Text;
                        lblCash.Text = lblCheque.Text;
                    }
                }
                else
                {
                    lblCheque.Text = _objConsulling.GetPayemntAmountAccordingToCourse(courseId);
                    lblDemand.Text = lblCheque.Text;
                    lblCash.Text = lblCheque.Text;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindPaymentAmount in UcStudentPaymentOption.axcs  :: -> ";
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

        protected void btnPayNow_Click(object sender, EventArgs e)
        {
             SecurePage _objSecurePage = new SecurePage();
              _ObjCommon = new Common();
         
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            MailTemplates objMailTemplates = new MailTemplates();
            string formNumber = "ADMJ" + System.DateTime.Now.Year + _ObjCommon.CourseId.ToString() + _objSecurePage.LoggedInUserId.ToString();
            ConsullingCourseAmount = lblCash.Text;
            string TransectionDetails = "You have selected the payment mode through Online Payment of" + " " + "Rs." + " " + lblCash.Text + "/- ";
            var UserDetails = UserManager.Instance.GetUserListById(_objSecurePage.LoggedInUserId);


            var sp = UserDetails.First();
            UserRegistrationProperty _ObjUserRegistrationProperty = new
                  UserRegistrationProperty
                  {
                      UserFullName = _objSecurePage.LoggedInUserName,
                      UserGender = sp.UserGender,
                      UserEmailid = _objSecurePage.LoggedInUserEmailId,
                      MobileNo = _objSecurePage.LoggedInUserMobile,
                      PhoneNo = sp.PhoneNo,
                      UserId = _objSecurePage.LoggedInUserId,
                      CourseId = sp.CourseId,
                      UserCategoryId = _objSecurePage.LoggedInUserType,
                      UserDOB = sp.UserDOB,
                      UserStatus = true,
                      UserPassword = sp.UserPassword,
                      UserPincode = txtPincode.Text.Trim(),
                      UserCorrespondenceAddress = txtAddress.Text.Trim()
                  };
            string ErrMsg = "";
            int i = UserManagerProvider.Instance.UpdateUserInfo(_ObjUserRegistrationProperty, 1, out ErrMsg);
            var mail = new MailMessage
            {
                From = new MailAddress(ApplicationSettings.Instance.Email),
                Subject = "Direct Admission:Form Number" + formNumber
            };
            var body = objMailTemplates.SendValidationMailForTheDirectAdmission("http://www.admissionjankari.com/", _objSecurePage.LoggedInUserName, formNumber, TransectionDetails);
            mail.Body = body;
            mail.To.Add(_objSecurePage.LoggedInUserEmailId);
            mail.Bcc.Add(ClsSingelton.bccDirectAdmission);
                  Utils.SendMailMessageAsync(mail);
                  libfuncs myUtility = new libfuncs();
                  Merchant_Id.Value = "M_shi18022_18022";
                  Amount.Value = lblCash.Text;
                  Order_Id.Value = formNumber + DateTime.Now.ToString("hh:mm:ss");
                  Redirect_Url.Value =Utils.AbsoluteWebRoot+ "ConformationPage.aspx?CID=" + objCrypto.Encrypt(_objSecurePage.LoggedInUserEmailId) + "&frmNumber=" + objCrypto.Encrypt(formNumber) + "&UID=" + objCrypto.Encrypt(_objSecurePage.LoggedInUserId.ToString());
                  string WorkingKey = ClsSingelton.WorkingKey.Trim();
                  Checksum.Value = myUtility.getchecksum(Merchant_Id.Value, Order_Id.Value, Amount.Value, Redirect_Url.Value, WorkingKey);
                  billing_cust_name.Value = _objSecurePage.LoggedInUserName;
                  billing_cust_address.Value = txtAddress.Text.Trim();
                  billing_cust_state.Value = txtState.Text;
                  billing_cust_country.Value = "India";
                  billing_cust_tel.Value = _objSecurePage.LoggedInUserMobile;
                  billing_cust_email.Value = _objSecurePage.LoggedInUserEmailId;
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
                  i = _objConsulling.InsertUpdateUserTransctionalDetails(_objSecurePage.LoggedInUserId, formNumber);
                 // Page.ClientScript.RegisterStartupScript(this.GetType(), "myKey", "PostDFormS();");
                  ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), Guid.NewGuid().ToString(),
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