using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.Components.Web.Controls;
using System.Web.UI.HtmlControls;
using System.Globalization;

using System.Net.Mail;

namespace IryTech.AdmissionJankari20.Web.counselling
{
    public partial class StudentCounselling : PageBase
    {
        UserRegistrationProperty _ObjUserRegistrationProperty;
        ClsCrypto _ObjClsCrypto;
        Consulling _ObjConsulling;
        SecurePage _objSecurePage;
        Common _ObjCommon;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            wizardApplyForm.PreRender += new EventHandler(wizardApplyForm_PreRender);
            if(IsPostBack)return;
           BindPageTitleAndKeyWords();
        }
        // to show page title ,keyword and description
        private void BindPageTitleAndKeyWords()
        {
           
            try
            {
               

                    Page.Title = "";
                    Page.Title = "Apply Now! Direct Admission in " + new Common().CourseName + " Colleges  -  Admission Jankari";

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
                                                   "Apply Now! Direct Admission in " + new Common().CourseName + " Colleges  -  Admission Jankari"
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

        protected void wizardApplyForm_PreRender(object sender, EventArgs e)
        {
            Repeater SideBarList = wizardApplyForm.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
            SideBarList.DataSource = wizardApplyForm.WizardSteps;
            SideBarList.DataBind();

        }
      
        protected string GetClassForWizardStep(object wizardStep)
        {
            WizardStep step = wizardStep as WizardStep;

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
       

        protected int  InsertUserBasicInfo()
        {
           string ErrMsg="";
            int UserId=0;
          
            _objSecurePage = new SecurePage();
            _ObjUserRegistrationProperty = new UserRegistrationProperty();
           
            try
            {
                 _ObjUserRegistrationProperty = new
                        UserRegistrationProperty{
                            UserFullName = studentPerInfo.StudentName,
                            UserGender = studentPerInfo.StudentGender,
                            UserEmailid = studentPerInfo.StudentEmaiLid,
                            MobileNo = studentPerInfo.StudentMobileNo,
                            PhoneNo = studentPerInfo.StudentAlternameNo,
                            CourseId = StudentCourseInfo.StudentCourse,
                            UserCategoryId=Convert.ToInt16(Usertype.Student),
                            UserDOB = Convert.ToDateTime(studentPerInfo.StudentDOB),
                           UserFatherName=studentPerInfo.FatherName,
                            UserStatus=true,
                            UserPassword = studentPerInfo.StudentMobileNo
                         };
                 int i = UserManagerProvider.Instance.InsertUserInfo(_ObjUserRegistrationProperty, 1, out ErrMsg);
                 UserId = _ObjUserRegistrationProperty.UserId;
                 if (i <= 0)
                 {
                     _ObjUserRegistrationProperty.UserId = UserId;
                     i = UserManagerProvider.Instance.UpdateUserInfo(_ObjUserRegistrationProperty, 1, out ErrMsg);
                     
                 }
                 if (i > 0)
                 {
                     _objSecurePage = new SecurePage();
                     _objSecurePage.LoggedInUserEmailId = studentPerInfo.StudentEmaiLid;
                     _objSecurePage.LoggedInUserId = UserId;
                     _objSecurePage.LoggedInUserMobile=studentPerInfo.StudentMobileNo;
                     _objSecurePage.LoggedInUserName=studentPerInfo.StudentName;
                     _objSecurePage.LoggedInUserType = Convert.ToInt16(Usertype.Student);
                     
                 }
                 _ObjConsulling = new Consulling();
                _ObjCommon=new Common();
                 string formNumber = "ADMJ" + System.DateTime.Now.Year + _ObjCommon.CourseId.ToString() + _objSecurePage.LoggedInUserId.ToString();
                 i = _ObjConsulling.InsertUpdateUserTransctionalDetails(_objSecurePage.LoggedInUserId, formNumber);
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
            _ObjConsulling = new Consulling();
             int i=0;
            try
            {
                StudentHighSchoolInfo objStudentHighSchoolInfo = new
                            StudentHighSchoolInfo
                            {
                                SchoolBoard = StudentAcademicInfo.HighSchoolInfo.SchoolBoard,
                                SchoolCGPA = StudentAcademicInfo.HighSchoolInfo.SchoolCGPA,
                                SchoolName = StudentAcademicInfo.HighSchoolInfo.SchoolName,
                                SchoolYOP = StudentAcademicInfo.HighSchoolInfo.SchoolYOP
                            };
                StudentIntermidateInfo objStudentIntermidateInfo = new
                            StudentIntermidateInfo
                            {
                                CollegeBoard = StudentAcademicInfo.IntermediateInfo.CollegeBoard,
                                CollegeCGPA = StudentAcademicInfo.IntermediateInfo.CollegeCGPA,
                                CollegeCourseCombination = StudentAcademicInfo.IntermediateInfo.CollegeCourseCombination,
                                CollegeCourseCombinationPer = StudentAcademicInfo.IntermediateInfo.CollegeCourseCombinationPer,
                                CollegeName = StudentAcademicInfo.IntermediateInfo.CollegeName,
                                CollegePer = StudentAcademicInfo.IntermediateInfo.CollegePer,
                                CollegeYOP = StudentAcademicInfo.IntermediateInfo.CollegeYOP
                            };
                StudentDicInfo objStudentDicInfo = new
                            StudentDicInfo
                            {

                                DicCGPA = StudentAcademicInfo.DiplomaInfo.DicCGPA,
                                DicCollegeName = StudentAcademicInfo.DiplomaInfo.DicCollegeName,
                                DicCourseName = StudentAcademicInfo.DiplomaInfo.DicCourseName,
                                DicPer = StudentAcademicInfo.DiplomaInfo.DicPer,
                                DicYOP = StudentAcademicInfo.DiplomaInfo.DicYOP
                            };
                StudentGrdInfo objStudentGrdInfo = new
                        StudentGrdInfo
                        {
                            GrdCGPA = StudentAcademicInfo.GraduateInfo.GrdCGPA,
                            GrdCollegeName = StudentAcademicInfo.GraduateInfo.GrdCollegeName,
                            GrdPer = StudentAcademicInfo.GraduateInfo.GrdPer,
                            GrdSpecialization = StudentAcademicInfo.GraduateInfo.GrdSpecialization,
                            GrdYOP = StudentAcademicInfo.GraduateInfo.GrdYOP

                        };
              i  = _ObjConsulling.InsertUpdateAcademicInfo(userId, StudentAcademicInfo.Eligibilty, objStudentHighSchoolInfo, objStudentIntermidateInfo,
                                                            objStudentDicInfo, objStudentGrdInfo);

            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateAcademicInfo in StudentCounselling.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }


        // method to Insert Update Exam Appared
        protected int InsertUpdateExamApp(int userId)
        {
            _ObjConsulling = new Consulling();
            int i = 0;
            try
            {
                if (!String.IsNullOrEmpty(StudentExamInfo.ExamApp1))
                {
                    i = _ObjConsulling.InsertUpdateStudentExamAppeared(userId, StudentExamInfo.ExamApp1, StudentExamInfo.ExamApp1Rank1);
                }
                if (!String.IsNullOrEmpty(StudentExamInfo.ExamApp2))
                {
                    i = _ObjConsulling.InsertUpdateStudentExamAppeared(userId, StudentExamInfo.ExamApp2, StudentExamInfo.ExamApp2Rank2);
                }
                if (!String.IsNullOrEmpty(StudentExamInfo.ExamApp3))
                {
                    i = _ObjConsulling.InsertUpdateStudentExamAppeared(userId, StudentExamInfo.ExamApp3, StudentExamInfo.ExamApp3Rank3);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateExamApp in StudentCounselling.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }


        //// Method to Insert City Prefrance
        protected int InsertCityPrefrance(int userId)
        {
            _ObjConsulling = new Consulling();
            int i = 0;
            try
            {
                if (!String.IsNullOrEmpty(UcICityPrefrance.StudentCityPrefrance1))
                {
                    i = _ObjConsulling.InsertStudentCityPrefrance(userId, UcICityPrefrance.StudentCityPrefrance1);
                }
                if (!String.IsNullOrEmpty(UcICityPrefrance.StudentCityPrefrance2))
                {
                    i = _ObjConsulling.InsertStudentCityPrefrance(userId, UcICityPrefrance.StudentCityPrefrance2);
                }
                if (!String.IsNullOrEmpty(UcICityPrefrance.StudentCityPrefrance2))
                {
                    i = _ObjConsulling.InsertStudentCityPrefrance(userId, UcICityPrefrance.StudentCityPrefrance2);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertCityPrefrance in StudentCounselling.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;

        }

        // Method to Insert student College prefrance
        protected int InsertCollegePrefrance(int userId, int courseId)
        {
            _ObjConsulling = new Consulling();
            int i = 0;
            try
            {
                if (!String.IsNullOrEmpty(UcCollegePrefrance.StudentCollegePrefrance1))
                {
                    i = _ObjConsulling.InsertStudentCollegePrefrance(userId, UcCollegePrefrance.StudentCollegePrefrance1, courseId);
                }
                if (!String.IsNullOrEmpty(UcCollegePrefrance.StudentCollegePrefrance2))
                {
                    i = _ObjConsulling.InsertStudentCollegePrefrance(userId, UcCollegePrefrance.StudentCollegePrefrance2, courseId);
                }
                if (!String.IsNullOrEmpty(UcCollegePrefrance.StudentCollegePrefrance3))
                {
                    i = _ObjConsulling.InsertStudentCollegePrefrance(userId, UcCollegePrefrance.StudentCollegePrefrance3, courseId);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateExamApp in StudentCounselling.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }


        protected void wizardApplyForm_ActiveStepChanged(object sender, EventArgs e)
        {
           
            _objSecurePage = new SecurePage();
            int i = 0;
            _ObjCommon=new Common();
            switch(wizardApplyForm.ActiveStep.ID)
            {
                case "Instrucation":
                    {
                        wizardApplyForm.StepNextButtonText = "I Accept »";
                        i = InsertUserBasicInfo();
                        divSeccondImage.Visible = true;
                        divFirstImage.Visible = false;
                     break;
                    }
                case "AcedmicInfo":
                    {
                        wizardApplyForm.StepNextButtonText = "Next »";
                        if (string.IsNullOrEmpty(StudentCourseInfo.StudentCourseEligibilty))
                        {
                            StudentAcademicInfo.ValidateAcademicInfo(_ObjCommon.CourseId);
                            StudentAcademicInfo.BindLateralEntryInfo(_ObjCommon.CourseId);
                        }
                        else
                        {
                            StudentAcademicInfo.BindLateralEntryInfo(0);
                            StudentAcademicInfo.HideShowAcademicInfo(StudentCourseInfo.StudentCourseEligibilty);
                        }
                        
                            break;
                    }
                case "Interested":
                    {
                        wizardApplyForm.StepNextButtonText = "Next »";
                         i = InsertUpdateAcademicInfo(_objSecurePage.LoggedInUserId);
                       
                        break;
                    }

                case "ExamAppeared":
                    {
                        wizardApplyForm.StepNextButtonText = "Next »";
                        i = InsertCollegePrefrance(_objSecurePage.LoggedInUserId, _ObjCommon.CourseId);
                        i = InsertCityPrefrance(_objSecurePage.LoggedInUserId);
                        i = InsertUpdateExamApp(_objSecurePage.LoggedInUserId);

                        break;
                    }
            }



        }

        protected void wizardApplyForm_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            _objSecurePage = new SecurePage();
          // int  i = InsertUpdateExamApp(_objSecurePage.LoggedInUserId);
           Response.Redirect(Utils.ApplicationRelativeWebRoot + (Utils.RemoveIllegealFromCourse(new Common().CourseName) + "/Counselling/OnlineTransaction/").ToLower());
            //RadioButtonList rbtnPaymentType = StudentPaymentOptions.GetPaymentMode;
            //string BankName = string.Empty;
            //_objSecurePage = new SecurePage();
            //_ObjConsulling = new Consulling();
            //_ObjCommon = new Common();
            //MailTemplates objMailTemplates = new MailTemplates();
            //string DDNumber = string.Empty;
            //string FormNum = "ADMJ" + System.DateTime.Now.Year + _ObjCommon.CourseId.ToString() + _objSecurePage.LoggedInUserId.ToString();
            //string TranctionDetails = string.Empty;
            //if (rbtnPaymentType.SelectedValue == "0")
            //{
            //    BankName = "";
            //    DDNumber = "";
            //    TranctionDetails = " You have selected the payment mode through cheque. Please make an account payee cheque of " + StudentPaymentOptions.ConsullingCourseAmount + " in favour of <b>" + " Iry Tech Pvt Ltd " + " </b>" + " payable at <b> " + "New Delhi</b> ";
            //    TranctionDetails = TranctionDetails + "  Mention your Reference Id, Name, Phone No, Email-id, at the back of the cheque.";
            //    TranctionDetails = TranctionDetails + "Please send Cheque via courier to this address. ";
            //    TranctionDetails = TranctionDetails + " <br /><br />" + " Iry Tech Pvt Ltd.";
            //    TranctionDetails = TranctionDetails + "74 Amrit Chamber, 2nd floor, <br />" + "  202-204 Scindia House Connaught Place, <br />" + " New Delhi-110001. <br />" + "  Contact us : +91 - 9999 261 633, 9654 722 013 , 011-43391978";
            //}
            //else
            //{
            //    if (rbtnPaymentType.SelectedValue == "1")
            //    {
            //        BankName = "";
            //        DDNumber = "";
            //        TranctionDetails = "You have selected the payment mode through DD.<br/>  ";
            //        TranctionDetails = TranctionDetails + "   <b>Make a single Demand Draft</b> (DD) of " + StudentPaymentOptions.ConsullingCourseAmount + " in favour of <b>" + " IryTech Pvt Ltd" + "</b> Payble at <b>Delhi.</b>";
            //        TranctionDetails = TranctionDetails + " <br /><br /> Please send Demand Draft via courier to this address.";
            //        TranctionDetails = TranctionDetails + "<br /><br />  Iry Tech Pvt Ltd.";
            //        TranctionDetails = TranctionDetails + "74 Amrit Chamber, 2nd floor, <br />" + "  202-204 Scindia House Connaught Place, <br />" + " New Delhi-110001. <br />" + "   Contact us : +91 - 9999 261 633, 9654 722 013 , 011-43391978";
            //    }
            //    else
            //    {
            //        if (rbtnPaymentType.SelectedValue == "2")
            //        {
            //            BankName = "Iry Tech pvt ltd";
            //            DDNumber = "1846 0021 0027 0120";
            //            TranctionDetails = "You have selected the payment mode through cash. You will need to deposit " + StudentPaymentOptions.ConsullingCourseAmount + " in the nearest Punjab National Bank in the following account.   <br/><br/> ";
            //            TranctionDetails = TranctionDetails + BankName;
            //            TranctionDetails = TranctionDetails + "<br/>";
            //            TranctionDetails = TranctionDetails + "Account Number :" + DDNumber + " <br/> <br/>";
            //        }
            //        else
            //        {
            //            TranctionDetails = "You have selected the payment mode through Online Payment of" + " " + "Rs." + " " + StudentPaymentOptions.ConsullingCourseAmount + "/- ";
            //        }
            //    }

            //}
            //int i = _ObjConsulling.InsertUpdateUserTransctionalDetails(_objSecurePage.LoggedInUserId, FormNum, false, rbtnPaymentType.SelectedItem.ToString());
            //var mail = new MailMessage
            //{
            //    From = new MailAddress(ApplicationSettings.Instance.Email),
            //    Subject = "Direct Admission:Form Number" + FormNum
            //};
            //var body = objMailTemplates.SendValidationMailForTheDirectAdmission("http://www.admissionjankari.com/", _objSecurePage.LoggedInUserName, FormNum, TranctionDetails);
            //mail.Body = body;
            //mail.To.Add(_objSecurePage.LoggedInUserEmailId);
            //mail.Bcc.Add(ClsSingelton.bccDirectAdmission);
            //Utils.SendMailMessageAsync(mail);
            //Response.Redirect("~/counselling/ChooseCollege.aspx");
        }

        

    }
}