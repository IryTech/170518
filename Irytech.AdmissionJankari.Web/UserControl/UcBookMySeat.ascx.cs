using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web.UI.WebControls;
using IntegrationKit;
using IryTech.AdmissionJankari.BL;
using System.Web.UI.HtmlControls;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcBookMySeat : System.Web.UI.UserControl
    {
        private UserRegistrationProperty _objUserRegistrationProperty;
        private Consulling _objConsulling;
        private SecurePage _objSecurePage;
        private Common _objCommon;

        protected void Page_Load(object sender, EventArgs e)
        {

            wizardApplyForm.PreRender += new EventHandler(WizardApplyFormPreRender);
            
            if (!IsPostBack)
            {
                StudentCourseInfo.CourseVisibility = false;
                var finishButton = wizardApplyForm.FindControl("FinishNavigationTemplateContainerID$FinishButton") as Button;
                if (finishButton != null)
                {
                    finishButton.ValidationGroup = "VldgOnlinePayment";
                    finishButton.CausesValidation = true;
                }
                BindUserDetailsForOnlinePayment();
                
                BindPageTitleAndKeyWords();
                string url = Request.RawUrl;
                if (url.Contains('?'))
                {
                    int id = Convert.ToInt32(url.Substring(url.IndexOf("?") + 1));
                    if (id != 0)
                    {
                        wizardApplyForm.ActiveStepIndex = 1;
                    }
                }

            }
        }

        // to show page title ,keyword and description
        private void BindPageTitleAndKeyWords()
        {
            try
            {


                Page.Title = "";
                Page.Title = "Apply Now! Direct Admission in " + new Common().CourseName +
                             " Colleges  -  Admission Jankari";

                var metadesc = new HtmlMeta();
                metadesc.Attributes.Clear();
                metadesc.Name = "description";

                metadesc.Content =
                    "Apply Now! It will take less than 5 minutes to get Direct Admission in top " +
                    new Common().CourseName +
                    " Colleges in India. Direct Admission! Without donation!! under Management Quota Seats -  Admission Jankari";

                Page.Header.Controls.Add(metadesc);

                var metaKeywords = new HtmlMeta
                                       {
                                           Name = "keywords",
                                           Content =
                                               "Apply Now! Direct Admission in " + new Common().CourseName +
                                               " Colleges  -  Admission Jankari"
                                       };

                Page.Header.Controls.Add(metaKeywords);


            }
            catch (Exception Ex)
            {
                var err = Ex.Message;
                if (Ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + Ex.InnerException.Message;
                }
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in StudentCounselling.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void WizardApplyFormPreRender(object sender, EventArgs e)
        {
            var sideBarList = wizardApplyForm.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
            if (sideBarList == null) return;
            sideBarList.DataSource = wizardApplyForm.WizardSteps;
            sideBarList.DataBind();
        }

        protected string GetClassForWizardStep(object wizardStep)
        {
            var step = wizardStep as WizardStep;

            if (step == null)
            {
                return "";
            }
            int stepIndex = wizardApplyForm.WizardSteps.IndexOf(step);

            if (stepIndex < wizardApplyForm.ActiveStepIndex)
            {
                return "prevStep";
            }
            else if (stepIndex > wizardApplyForm.ActiveStepIndex)
            {
                return "nextStep";
            }
            else
            {
                return "currentStep";
            }
        }


        protected int InsertUserBasicInfo()
        {
            string ErrMsg = "";
            int UserId = 0;

            _objSecurePage = new SecurePage();
            _objUserRegistrationProperty = new UserRegistrationProperty();

            try
            {
                _objUserRegistrationProperty = new
                    UserRegistrationProperty
                                                   {
                                                       UserFullName = studentPerInfo.StudentName,
                                                       UserGender = studentPerInfo.StudentGender,
                                                       UserEmailid = studentPerInfo.StudentEmaiLid,
                                                       MobileNo = studentPerInfo.StudentMobileNo,
                                                       PhoneNo = studentPerInfo.StudentAlternameNo,
                                                       CourseId = StudentCourseInfo.StudentCourse,
                                                       UserCategoryId = Convert.ToInt16(Usertype.Student),
                                                       //UserDOB = Convert.ToDateTime(studentPerInfo.StudentDOB),
                                                       UserDOB = Convert.ToDateTime("01/05/2012"),
                                                       UserFatherName = studentPerInfo.FatherName,
                                                       UserStatus = true,
                                                       UserPassword = studentPerInfo.StudentMobileNo
                                                   };
                int i = UserManagerProvider.Instance.InsertUserInfo(_objUserRegistrationProperty, 1, out ErrMsg);
                UserId = _objUserRegistrationProperty.UserId;
                if (i <= 0)
                {
                    _objUserRegistrationProperty.UserId = UserId;
                    i = UserManagerProvider.Instance.UpdateUserInfo(_objUserRegistrationProperty, 1, out ErrMsg);

                }
                if (i > 0)
                {
                    _objSecurePage = new SecurePage
                                         {
                                             LoggedInUserEmailId = studentPerInfo.StudentEmaiLid,
                                             LoggedInUserId = UserId,
                                             LoggedInUserMobile = studentPerInfo.StudentMobileNo,
                                             LoggedInUserName = studentPerInfo.StudentName,
                                             LoggedInUserType = Convert.ToInt16(Usertype.Student)
                                         };
                }
                _objConsulling = new Consulling();
                _objCommon = new Common();
                string formNumber = "ADMJ" + System.DateTime.Now.Year + _objCommon.CourseId.ToString() +
                                    _objSecurePage.LoggedInUserId.ToString();
                i = _objConsulling.InsertUpdateUserTransctionalDetails(_objSecurePage.LoggedInUserId, formNumber);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUserBasicInfo in StudentCounselling.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return UserId;
        }

        // Method to Insert update The student Acedmic Info
        protected int InsertUpdateAcademicInfo(int userId)
        {
            _objConsulling = new Consulling();
            var i = 0;
            try
            {
                var objStudentHighSchoolInfo = new
                    StudentHighSchoolInfo
                                                   {
                                                       SchoolBoard = StudentAcademicInfo.HighSchoolInfo.SchoolBoard,
                                                       SchoolCGPA = StudentAcademicInfo.HighSchoolInfo.SchoolCGPA,
                                                       SchoolName = StudentAcademicInfo.HighSchoolInfo.SchoolName,
                                                       SchoolYOP = StudentAcademicInfo.HighSchoolInfo.SchoolYOP
                                                   };
                var objStudentIntermidateInfo = new
                    StudentIntermidateInfo
                                                    {
                                                        CollegeBoard = StudentAcademicInfo.IntermediateInfo.CollegeBoard,
                                                        CollegeCGPA = StudentAcademicInfo.IntermediateInfo.CollegeCGPA,
                                                        CollegeCourseCombination =
                                                            StudentAcademicInfo.IntermediateInfo
                                                                               .CollegeCourseCombination,
                                                        CollegeCourseCombinationPer =
                                                            StudentAcademicInfo.IntermediateInfo
                                                                               .CollegeCourseCombinationPer,
                                                        CollegeName = StudentAcademicInfo.IntermediateInfo.CollegeName,
                                                        CollegePer = StudentAcademicInfo.IntermediateInfo.CollegePer,
                                                        CollegeYOP = StudentAcademicInfo.IntermediateInfo.CollegeYOP
                                                    };
                var objStudentDicInfo = new
                    StudentDicInfo
                                            {

                                                DicCGPA = StudentAcademicInfo.DiplomaInfo.DicCGPA,
                                                DicCollegeName = StudentAcademicInfo.DiplomaInfo.DicCollegeName,
                                                DicCourseName = StudentAcademicInfo.DiplomaInfo.DicCourseName,
                                                DicPer = StudentAcademicInfo.DiplomaInfo.DicPer,
                                                DicYOP = StudentAcademicInfo.DiplomaInfo.DicYOP
                                            };
                var objStudentGrdInfo = new
                    StudentGrdInfo
                                            {
                                                GrdCGPA = StudentAcademicInfo.GraduateInfo.GrdCGPA,
                                                GrdCollegeName = StudentAcademicInfo.GraduateInfo.GrdCollegeName,
                                                GrdPer = StudentAcademicInfo.GraduateInfo.GrdPer,
                                                GrdSpecialization = StudentAcademicInfo.GraduateInfo.GrdSpecialization,
                                                GrdYOP = StudentAcademicInfo.GraduateInfo.GrdYOP

                                            };
                i = _objConsulling.InsertUpdateAcademicInfo(userId, StudentAcademicInfo.Eligibilty,
                                                            objStudentHighSchoolInfo, objStudentIntermidateInfo,
                                                            objStudentDicInfo, objStudentGrdInfo);

            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing InsertUpdateAcademicInfo in StudentCounselling.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

        protected void WizardApplyFormActiveStepChanged(object sender, EventArgs e)
        {

            _objSecurePage = new SecurePage();
            int i = 0;
            _objCommon = new Common();
            if (wizardApplyForm.ActiveStep != null)
                switch (wizardApplyForm.ActiveStep.ID)
                {
                    case "Instrucation":
                        {
                            wizardApplyForm.StepNextButtonText = "I Accept »";

                            divSeccondImage.Visible = true;
                            divFirstImage.Visible = false;
                            var collegeData = CollegeProvider.Instance.GetCollegeBasicDetailsByBranchCourseId(
                                Convert.ToInt32(UcSeatAvailiablity.CollegeBranchCourseId != "0"
                                                    ? UcSeatAvailiablity.CollegeBranchCourseId
                                                    : Request.QueryString["collegeId"]));
                            new Consulling().InsertStudentCollegePrefrance(new SecurePage().LoggedInUserId,
                                                                           collegeData.First().CollegeBranchName,
                                                                           new Common().CourseId, true);           /*... TO INSERT COLLEGE PREFERNCE BY INDU KUMAR PANDEY....*/
                         
                            var bookedCollege =
                                 new Consulling().GetBookedCollegeById(
                                     Convert.ToInt32(UcSeatAvailiablity.CollegeBranchCourseId != "0"
                                                         ? UcSeatAvailiablity.CollegeBranchCourseId
                                                         : Request.QueryString["collegeId"]));                    /*.....TO GET BOOKED COLLEGE PAYMENT...................*/
                          
                            var amount = "";
                            if (bookedCollege.Tables.Count > 0)
                            {
                                if (bookedCollege.Tables[0].Rows.Count > 0)
                                {
                                    amount = Convert.ToString(bookedCollege.Tables[0].Rows[0]["AjBookSeatAmount"]);
                                    if (
                                        !string.IsNullOrEmpty(
                                            Convert.ToString(bookedCollege.Tables[0].Rows[0]["AjInstruction"])))
                                    {
                                        ucInstrucation.CollegeVisible = true;
                                        ucInstrucation.CollegeInstuction =
                                            Convert.ToString(bookedCollege.Tables[0].Rows[0]["AjInstruction"]);
                                    }
                                    else
                                        ucInstrucation.CollegeVisible = false;


                                }
                                else
                                    ucInstrucation.CollegeVisible = false;

                            }
                            else
                                ucInstrucation.CollegeVisible = false;

                            this.TotalAmountInserted = !string.IsNullOrEmpty(amount)
                                                           ? amount
                                                           : Convert.ToString(
                                                               ConfigurationManager.AppSettings["BookSeatAmount"]);
                            ucInstrucation.TotalAmountFees = TotalAmountInserted;
                            break;
                        }
                    case "AcedmicInfo":
                        {
                            wizardApplyForm.StepNextButtonText = "Next »";
                            if (string.IsNullOrEmpty(StudentCourseInfo.StudentCourseEligibilty))
                            {
                                StudentAcademicInfo.ValidateAcademicInfo(_objCommon.CourseId);
                                StudentAcademicInfo.BindLateralEntryInfo(_objCommon.CourseId);
                            }
                            else
                            {
                                StudentAcademicInfo.BindLateralEntryInfo(0);
                                StudentAcademicInfo.HideShowAcademicInfo(StudentCourseInfo.StudentCourseEligibilty);
                            }
                            InsertUserBasicInfo();                                                   /*...TO INSERT USER INFORMATION......*/

                            break;
                        }
                    case "wzdPayment":
                        {
                            
                            break;
                        }


                    case "SeatAvailability":
                        {
                            i = InsertUpdateAcademicInfo(_objSecurePage.LoggedInUserId);                 /*...TO INSERT USER STUDENT ACCADEMIC INFORMATION......*/
                            GetMaximumaMarks(new Common().CourseId);                                      /*.....TO CHECK STUDENT SEAT AVAILIBITY....*/
                            wizardApplyForm.StepNextButtonText = "Next »";

                            break;
                        }
                }
        }

        protected void WizardApplyFormFinishButtonClick(object sender, WizardNavigationEventArgs e)
        {

            var objSecurePage = new SecurePage();
            var objConsulling = new Consulling();
         var   objCommon = new Common();
            var objMailTemplates = new MailTemplates();
            var formNum = "ADMJ" + DateTime.Now.Year + objCommon.CourseId.ToString() +
                          objSecurePage.LoggedInUserId.ToString();
            string tranctionDetails;
            if (rbtnPaymentType.SelectedValue == "0")
            {
                tranctionDetails =
                    " You have selected the payment mode through cheque. Please make an account payee cheque of Rs." +
                    lblCash1.Text + " in favour of <b>" + " Admissionjankari.com " + " </b>";
                tranctionDetails = tranctionDetails +
                                   " <br /><br /> Mention your Reference Id(Application form number), Name, Phone No, Email-id, at the back of the cheque.";
                tranctionDetails = tranctionDetails +
                                   "<br /><br />To confirm the payment, please send your cheque at the following address (Via Speed/Registered Post) ";
                tranctionDetails = tranctionDetails + " <br /><br />" + "Admissionjankari.com<br />";
                tranctionDetails = tranctionDetails + "74 Amrit Chamber, 2nd floor, <br />" +
                                   "  202-204 Scindia House Connaught Place, <br />" + " New Delhi-110001. <br />" +
                                   "  Contact us : +91 - 9999 261 633, 9654 722 013 , 011-43391978<br/>";
            }
            else
            {
                if (rbtnPaymentType.SelectedValue == "1")
                {
                    tranctionDetails = "You have selected the payment mode through DD.<br/>  ";
                    tranctionDetails = tranctionDetails + "   <b>Make a single Demand Draft</b> (DD) of Rs." +
                                       lblCash1.Text + " in favour of <b>" + " Admissionjankari com" +
                                       "</b>Payable at <b>Delhi.</b>";
                    tranctionDetails = tranctionDetails +
                                       " <br /><br /> To confirm the payment, please send your  Demand Draft at the following address (Via Speed/Registered Post)";
                    tranctionDetails = tranctionDetails + "<br /><br />" + "Admissionjankari.com <br />";
                    tranctionDetails = tranctionDetails + "74 Amrit Chamber, 2nd floor, <br />" +
                                       "  202-204 Scindia House Connaught Place, <br />" +
                                       " New Delhi-110001. <br />" +
                                       "   Contact us : +91-11-43391978, +91-8800567711, +91-8800567733<br/>";
                }
                else
                {
                    if (rbtnPaymentType.SelectedValue == "2")
                    {
                        const string bankName = "Account Name: Admissionjankari.com";
                        const string ddNumber = "00032 0000 44418";
                        tranctionDetails =
                            "You have selected the payment mode through cash. You will need to deposit Rs." +
                            lblCash1.Text + " in the nearest HDFC Bank in the following account.   <br/><br/> ";
                        tranctionDetails = tranctionDetails + bankName + "<br/>";
                        tranctionDetails = tranctionDetails + "Account Number :" + ddNumber + " <br/>";
                        tranctionDetails = tranctionDetails + "RTGS/IFSC/NEFT Code: HDFC0000003 <br/>";
                        tranctionDetails = tranctionDetails + "Branch:Kasturba Gandhi Marg,New Delhi<br/>";
                        tranctionDetails = tranctionDetails +
                                           " <br /><br /> To confirm the payment, please send your  pay-in-slip at the following address (Via Speed/Registered Post)";
                        tranctionDetails = tranctionDetails + "<br /><br />" + "Admissionjankari.com";
                        tranctionDetails = tranctionDetails + "74 Amrit Chamber, 2nd floor, <br />" +
                                           "  202-204 Scindia House Connaught Place, <br />" +
                                           " New Delhi-110001. <br />" +
                                           "   Contact us : +91-11-43391978, +91-8800567711, +91-8800567733<br/>";
                    }
                    else
                    {

                        tranctionDetails = "You have selected the payment mode through Online Payment of" + " " +
                                           "Rs." + " " + lblCash1.Text + "/- ";
                        OnlinePayment();
                    }
                }

            }
            int i = objConsulling.InsertUpdateUserTransctionalDetails(objSecurePage.LoggedInUserId, formNum, false,
                                                                      rbtnPaymentType.SelectedItem.ToString(), "",
                                                                      "", this.TotalAmountInserted);
            var mail = new MailMessage
            {
                From = new MailAddress(ApplicationSettings.Instance.Email),
                Subject = "Direct Admission:Form Number" + formNum
            };
            var body = objMailTemplates.SendValidationMailForTheDirectAdmission("http://www.admissionjankari.com/",
                                                                                objSecurePage.LoggedInUserName,
                                                                                formNum, tranctionDetails);
            mail.Body = body;
            mail.To.Add(objSecurePage.LoggedInUserEmailId);
            mail.Bcc.Add(ClsSingelton.bccDirectAdmission);
            Utils.SendMailMessageAsync(mail);
            if (rbtnPaymentType.SelectedValue != "OnPayment")
                Response.Redirect(Utils.AbsoluteWebRoot + "ConformationPage.aspx", true);
        }


         //Method to Insert The College Details for book your Seat 
        protected void InsertUserCollegePrefrance(int userId, string collegeName, int courseId)
        {
            _objConsulling = new Consulling();
            var i = _objConsulling.InsertStudentCollegePrefrance(userId, collegeName, courseId);
        }


        private void GetMaximumaMarks(int courseId)
        {
            var objSecurePage = new SecurePage();
            DataSet collegeData = null;
            var dataStatus = 0;
            var errMsg = "";
            var objConsulling = new Consulling();
            var objDataTable = objConsulling.GetMaximiumMarksByCourseId(courseId, objSecurePage.LoggedInUserId);
            if (objDataTable.Rows.Count > 0)
            {
                collegeData = objConsulling.CheckCollegeSeatAvialibity(
                    Convert.ToInt32(Request.QueryString["collegeId"]),
                    Convert.ToInt32(
                        objDataTable.Rows[0]["TotalMarks"].ToString()), out dataStatus, out errMsg);
            }
            switch (dataStatus)
            {
                case -1:
                    UcSeatAvailiablity.CollegeBranchCourseId = Request.QueryString["collegeId"];
                    UcSeatAvailiablity.DataStatus = errMsg;
                    break;
                case 1:
                    UcSeatAvailiablity.DataStatus = errMsg;
                    UcSeatAvailiablity.BindRepesater = collegeData;
                    break;

            }
        }

        protected void WizardApplyFormNextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (e.CurrentStepIndex == 2)
            {
                if (UcSeatAvailiablity.CollegeBranchCourseId == "0")
                {
                    e.Cancel = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Call Message",
                                                            "alert('Please select a college to proceed')", true);

                }
            }
            
        }
        public string TotalAmountInserted
        {
            set { lblCash1.Text = value; lblCash2.Text = value; lblCash3.Text = value; }
            get
            {
                return lblCash1.Text;
            }
        }
        private void OnlinePayment()
        {
            var objSecurePage = new SecurePage();
            var objCommon = new Common();

            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
          var formNumber = "ADMJ" + System.DateTime.Now.Year + objCommon.CourseId.ToString() + objSecurePage.LoggedInUserId.ToString();

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
                Subject = "Direct Admission:Form Number" + formNumber
            };
            var amount = lblCash1.Text;
            var myUtility = new libfuncs();
            Merchant_Id.Value = "M_shi18022_18022";
            Amount.Value = amount;
            Order_Id.Value = formNumber + DateTime.Now.ToString("hh:mm:ss");
            Redirect_Url.Value = Utils.AbsoluteWebRoot + "ConformationPage.aspx?CID=" +
                                 objCrypto.Encrypt(objSecurePage.LoggedInUserEmailId) + "&frmNumber=" +
                                 objCrypto.Encrypt(formNumber) + "&UID=" +
                                 objCrypto.Encrypt(objSecurePage.LoggedInUserId.ToString() + "&Amount=" + amount);
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
           // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "YourUniqueScriptKey", "<script type='text/javascript'>PostDForm();</script>", false);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "YourUniqueScriptKey",
                                                            "<script type='text/javascript'>PostDForm();</script>", false);
           

        }

        public RadioButtonList GetPaymentMode
        {
            get { return rbtnPaymentType; }
        }

        protected void RbtnPaymentTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnPaymentType.SelectedValue == "OnPayment")
            {
                var finishButton = wizardApplyForm.FindControl("FinishNavigationTemplateContainerID$FinishButton") as Button;
                if (finishButton != null)
                {
                    finishButton.ValidationGroup = "VldgOnlinePayment";
                    finishButton.CausesValidation = true;
                }
            }
            else
            {    var finishButton = wizardApplyForm.FindControl("FinishNavigationTemplateContainerID$FinishButton") as Button;
                if (finishButton != null)
                {
                    finishButton.CausesValidation = false;
                }
            }
        }

        // Method to Bind The User Details if User Want to make the payment
        protected void GetUserDetails(string courseId, string userId)
        {
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            var objCommon = new Common();
            var objSecurePage = new SecurePage();
            try
            {
                courseId = objCrypto.Decrypt(courseId);
                userId = objCrypto.Decrypt(userId);
                objCommon.CourseId = Convert.ToInt16(courseId);
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
        private void BindUserDetailsForOnlinePayment()
        {
            var objSecurePage = new SecurePage();
            try
            {
                if (objSecurePage.IsLoggedInUSer)
                {
                    var userDetails = UserManagerProvider.Instance.GetUserListById(objSecurePage.LoggedInUserId).FirstOrDefault();
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
