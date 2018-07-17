using System;
using System.Linq;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.Components.Web.Controls;

namespace IryTech.AdmissionJankari20.Web.Account
{
    public partial class CollegeRegisteration : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack) return;
            var objCommon = new Common();
            hdnCollegeSuccess.Value = objCommon.GetErrorMessage("lblCollegeRegisaterSuccess");
            ValidateFields();
            BindStateList();
            BindCourse();
        }
        // Method to vali8date fields
        private void ValidateFields()
        {
            try
            {
                var objCommon = new Common();
                rfvCollegeName.ErrorMessage = objCommon.GetValidationMessage("rfvCollegeName");
                rfvCollegePersonName.ErrorMessage = objCommon.GetValidationMessage("rfvCollegeContactPersonName");
                rfvState.ErrorMessage = objCommon.GetValidationMessage("rfvState");
                rfvCity.ErrorMessage = objCommon.GetValidationMessage("rfvCity");
                rfvEmailId.ErrorMessage = objCommon.GetValidationMessage("rfvEmailId");
                rfvMobile.ErrorMessage = objCommon.GetValidationMessage("rfvMobile");
                revEmailId.ValidationExpression = ClsSingelton.aRegExpEmail;
                revEmailId.ErrorMessage = objCommon.GetValidationMessage("revEmail");
                revMobile.ValidationExpression = ClsSingelton.aRegExpMobile;
                revMobile.ErrorMessage = objCommon.GetValidationMessage("revMobile");
                revContactMobile.ValidationExpression = ClsSingelton.aRegExpMobile;
                revContactMobile.ErrorMessage = objCommon.GetValidationMessage("revMobile");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing ValidateFields in ColleRegisteration.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        // Method to Bind the State List
        private void BindStateList()
        {
            var state = StateProvider.Instance.GetAllState();
            try
            {

                if (state.Count > 0)
                {
                    ddlState.DataSource = state;
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateId";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlState.Items.Insert(0, new ListItem("Select", "0"));

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindStateList in CollegeRegisteration.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);


            }
        }

    
        protected void BtnRegisterClick(object sender, EventArgs e)
        {
            lblInfo.Visible = false;
            var objmailTemplete = new MailTemplates();
           
            try
            {
                   var collegeData =
                        new Common().CheckCollegeRegisteration(txtCollegeName.Text);
                    if (Convert.ToInt16(collegeData.Rows[0]["TotalCount"]) <= 0)
                    {
                        var stateList =
                            StateProvider.Instance.GetStateByStateId(Convert.ToInt32(ddlState.SelectedValue)).ToList().First();
                        var objUserRegistrationProperty = new UserRegistrationProperty
                                                              {
                                                                  UserFullName = txtCollegeContactPersonName.Text.Trim(),
                                                                  UserEmailid = txtEmailId.Text.Trim(),
                                                                  MobileNo = txtMobile.Text.Trim(),
                                                                  UserCategoryId = Convert.ToInt32(Usertype.College),
                                                                  UserPassword = txtMobile.Text.Trim()
                                                              };
                        var errMsg = "";
                        var result = UserManagerProvider.Instance.InsertUserInfo(objUserRegistrationProperty,
                                                                      Convert.ToInt32(Usertype.College), out errMsg);
                        var userList = UserManagerProvider.Instance.GetUserListByEmailId(txtEmailId.Text.Trim());
                        if (result > 0)
                        {
                            DoCollegeRegister(userList.First().UserId, stateList.CountryId);
                        }
                        else
                        {

                            if (userList.Count > 0)
                            {

                                spnEmailError.Visible = true;
                                spnEmailError.CssClass = "error";
                                spnEmailError.Text =
                                    string.Format(
                                        "{0} ,You are registered as " + userList.First().UserCategoryName + ". Please login or use different emailId",
                                        Common.GetStringProperCase(txtCollegeContactPersonName.Text.Trim()));
                                spnEmailError.Focus();

                            }
                        }
                    }
                    else
                    {
                        spnEmailError.Visible = true;
                        spnEmailError.CssClass = "error";
                        spnEmailError.Text = "The college is registered with us. Please <a href='/account/college-login'> login. </a> or <a href='/contact-us'> contact us.</a></span>";
                        spnEmailError.Focus();
                    }
               

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BtnRegisterClick in CollegeRegisteration.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        //method to college register in collegemaster by indu kumar pandey
        private void DoCollegeRegister(int userId, int countryId)
        {
            try
            {
                var errMsg = "";
                var collegeId = 0;
                var objCommon = new Common();
                var objCollegeBranchProperty = new CollegeBranchProperty()
                                                   {
                                                       CollegeBranchName = txtCollegeName.Text,
                                                       CollegeBranchMobileNo = txtCollegePhone.Text.Trim(),
                                                       CollegeBranchCountryId = countryId,
                                                       CollegeBranchStateId = Convert.ToInt32(ddlState.SelectedValue),
                                                       CollegeBranchCityId = Convert.ToInt32(hndCityId.Value),
                                                   };
                CollegeProvider.Instance.InsertCollegeBranchInfo(objCollegeBranchProperty, userId,
                                                                 out errMsg, out collegeId);

                var data = hndCollegeCourse.Value.Split(',');
                for (var i = 0; i < data.Count(); i++)
                {
                    var collegebranchCourseId=0;
                    
                    CollegeBranchCourseProperty objCollegeBranchCourseProperty = new CollegeBranchCourseProperty();
                    objCollegeBranchCourseProperty.CollegeBranchCourseId = 0;
                    objCollegeBranchCourseProperty.CollegeBranchId = collegeId;
                    objCollegeBranchCourseProperty.CollegeBranchCourseStatus = true;
                    objCollegeBranchCourseProperty.CourseId = Convert.ToInt32(data[i]);
                    CollegeProvider.Instance.InsertCollegeBranchCourseInfo(objCollegeBranchCourseProperty, userId, out errMsg, out collegebranchCourseId);
                }
                objCommon.InsertCollegeContactPersonDetails(userId, txtCollegeContactPersonName.Text, txtContactDesignation.Text,
                                                            txtMobile.Text, txtEmailId.Text, collegeId);
                if (collegeId > 0)
                {

                    InsertUserIdAndCollegeId(userId, collegeId);
                }
                else
                {
                    var collegeData =
                        CollegeProvider.Instance.GetCollegeCourseListByCollegeName(txtCollegeName.Text.Trim());
                    InsertUserIdAndCollegeId(userId, collegeData.First().CollegeIdBranchId);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing DoCollegeRegister in CollegeRegisteration.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        private void InsertUserIdAndCollegeId(int userId, int collegeId)
        {
            try
            {
                var errMsg = "";
                var objCommon = new Common();
                var result = objCommon.InsertUserIdAndCollegeId(userId, collegeId, out errMsg);

                if (result > 0)
                {
                    var objMailTemplete = new MailTemplates();

                    var objMail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "College Registration:" + txtCollegeName.Text.Trim()
                        };
                    var mailbody = objMailTemplete.MailToCollegeUser(txtCollegeContactPersonName.Text.Trim(),
                                                                     txtCollegeName.Text.Trim(),
                                                                     ddlState.SelectedItem.Text,
                                                                     ddlCity.SelectedItem.Text,
                                                                     txtCollegePhone.Text.Trim(),
                                                                     txtEmailId.Text.Trim(), txtMobile.Text.Trim());
                    objMail.Body = mailbody;
                    objMail.To.Add(txtEmailId.Text.Trim());
                    objMail.IsBodyHtml = true;
                    Utils.SendMailMessageAsync(objMail);

                    var objMail1 = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "College Registration:" + txtCollegeName.Text.Trim()
                        };
                    var mailbody1 =
                        objMailTemplete.MailBodyForCollegeRegisterToAdmin(txtCollegeContactPersonName.Text.Trim(),
                                                                          txtCollegeName.Text.Trim(),
                                                                          ddlState.SelectedItem.Text,
                                                                          ddlCity.SelectedItem.Text,
                                                                          txtCollegePhone.Text.Trim(),
                                                                          txtEmailId.Text.Trim());
                    objMail1.Body = mailbody1;
                    objMail1.To.Add(ClsSingelton.CollegeRegisterationMailId);
                    objMail1.IsBodyHtml = true;
                    Utils.SendMailMessageAsync(objMail1);
                    SetStatus("CommentSuccess", errMsg);
                    ClearForm();
                    spnEmailError.Visible = false;
                }
                else
                {
                    SetStatus("CommentError", errMsg);
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo =
                    "Error while executing InsertUserIdAndCollegeId in CollegeRegisteration.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        // to clear form..
        private void ClearForm()
        {
            txtCollegeName.Text = string.Empty;
            txtContactDesignation.Text = string.Empty;
            txtCollegePhone.Text = string.Empty;
            txtCollegeContactPersonName.Text = string.Empty;
            txtEmailId.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtCollegePhone.Text = string.Empty;
            ddlState.ClearSelection();
            ddlCity.ClearSelection();
        }

        // Method to Bind The Course 

        private void BindCourse()
        {
            ddlCollegeCourse.DataSource = CourseProvider.Instance.GetAllCourseList().Where(a => a.CourseStatus == true).ToList();
            ddlCollegeCourse.DataTextField = "CourseName";
            ddlCollegeCourse.DataValueField = "CourseId";
            ddlCollegeCourse.DataBind();
          
        }
        public void SetStatus(string status, string msg)
        {
            lblInfo.Attributes.Clear();
            lblInfo.Attributes.Add("class", status);
            lblInfo.Visible = true;
            lblInfo.InnerHtml =
                string.Format(
                    "{0}<a href=\"javascript:HideMessageStatus()\" style=\"width:20px;float:right\">X</a>",
                    Server.HtmlEncode(msg));
        }

      
    }
}