using System;
using System.Linq;
using System.Net.Mail;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class Register : System.Web.UI.UserControl
    {
        private Common _objCommon;
        private UserRegistrationProperty _objUserRegistrationProperty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindUserCategory();
            BindCourseMaster();
            BindCity();
            
        }
        
        private void BindUserCategory()
        {
           var data = UserManagerProvider.Instance.GetAllUserCategoryList().Where(resut=>resut.UserCategoryStatus==true).ToList();
            if (data.Count <= 0) return;
            ddlUserCategory.DataSource = data;
            ddlUserCategory.DataTextField = "UserCategoryName";
            ddlUserCategory.DataValueField = "UserCategoryId";
            ddlUserCategory.DataBind();
            ddlUserCategory.Items.Insert(0,new ListItem("Select","0"));
        }
        private void BindCity()
        {
            var data = CityProvider.Instacnce.GetAllCityList();
            if (data.Count <= 0) return;
            ddlCity.DataSource = data;
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "CityId";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0,new ListItem("Select City","0"));
        }
        private void BindCourseMaster()
        {
          var data = CourseProvider.Instance.GetAllCourseList().Where(result=>result.CourseStatus==true).ToList();
            if (data.Count <= 0) return;
            ddlCourse.DataSource = data;
            ddlCourse.DataTextField = "CourseName";
            ddlCourse.DataValueField = "CourseId";
            ddlCourse.DataBind();
            ddlCourse.Items.Insert(0, new ListItem("Select Course", "0"));
        }
        protected void BtnRegisterClick(object sender, EventArgs e)
        {
            var objmailTemplete = new MailTemplates();
           try
            {
                _objUserRegistrationProperty = new UserRegistrationProperty
                                                           {
                                                               UserFullName = txtUserName.Text.Trim(),
                                                               UserEmailid = txtEmailId.Text.Trim(),
                                                               MobileNo = txtMobile.Text.Trim(),
                                                               UserCategoryId = Convert.ToInt16(ddlUserCategory.SelectedValue),
                                                               CourseId = ddlCourse.SelectedIndex>0?Convert.ToInt16(ddlCourse.SelectedValue):0,
                                                              UserPassword=txtMobile.Text.Trim()
                                                           };
               
                var errMsg = "";
                var result = UserManagerProvider.Instance.InsertUserInfo(_objUserRegistrationProperty, 1, out errMsg);
                hdnUserId.Value = Convert.ToString(_objUserRegistrationProperty.UserId);
                hdnUserCategory.Value = Convert.ToString(ddlUserCategory.SelectedValue);
                hdnPassword.Value = _objUserRegistrationProperty.UserPassword;
                if (result > 0)
                {
                    switch (ddlUserCategory.SelectedItem.Text)
                    {
                        case "Student":
                            Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript",
                                                                    "OpenPoup('popCollege,400,'btnRegister');", true);
                            break;
                        default:
                            Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript",
                                                                    "OpenPoup('popCity',400,'btnRegister');", true);
                            break;
                    }
                    var mail = new MailMessage
                    {
                        From = new MailAddress(ApplicationSettings.Instance.Email),
                        Subject = "AdmissionJankari: Registation mail "
                    };

                    var body = objmailTemplete.MailBodyForRegistation(txtUserName.Text, txtEmailId.Text, txtMobile.Text);
                    mail.Body = body;
                    mail.To.Add(_objUserRegistrationProperty.UserEmailid);
                    Utils.SendMailMessageAsync(mail);
                    SetStatus("CommentSuccess", errMsg);
                    ClearFields();
                  
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
                const string addInfo = "Error while executing InsertUserInfo in Register.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private  void ClearFields()
        {
            txtUserName.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmailId.Text = string.Empty;
            ddlUserCategory.ClearSelection();
            ddlCourse.ClearSelection();
        }

        protected void BtnCityRegisterClick(object sender, EventArgs e)
        {
            var errMsg="";
            if (ddlCity.SelectedIndex <= 0) return;
            var cityData = CityProvider.Instacnce.GetCityById(Convert.ToInt16(ddlCity.SelectedValue));
            var query = cityData.Select(result => new
                                                      {
                                                          countryId=result.CountryId,
                                                          stateId=result.StateId,
                                                          stateName=result.StateName
                                                      }).First();
           _objUserRegistrationProperty = new UserRegistrationProperty
                                               {
                                                   UserCategoryId =Convert.ToInt16( hdnUserCategory.Value),
                                                   UserFullName = txtUserName.Text.Trim(),
                                                   UserEmailid =txtEmailId.Text.Trim(),
                                                   MobileNo =txtMobile.Text.Trim(),
                                                   UserPassword =  hdnPassword.Value,
                                                   UserId = Convert.ToInt16(hdnUserId.Value),
                                                   CityId = Convert.ToInt16(ddlCity.SelectedValue),
                                                   StateId = query.stateId,
                                                   CountryCode = query.countryId
                                               };
            var status = UserManagerProvider.Instance.UpdateUserInfo(_objUserRegistrationProperty, 1, out errMsg);
            if(status>0)
            {
                SetStatus("CommentSuccess",errMsg);
                ClearFields();
            }
            else
            {
                SetStatus("CommentError", errMsg);
            
            }
        }
        protected void BtnCollegeRegisterClick(object sender, EventArgs e)
        {
            ClearFields();

        }
        public void SetStatus(string status, string msg)
        {
            lblInfo.Attributes.Clear();
            lblInfo.Attributes.Add("class", status);
            lblInfo.Visible = true;
            lblInfo.InnerHtml =
                string.Format(
                    "{0}<a href=\"javascript:HideMessageStatus(" + lblInfo.ClientID + ")\" style=\"width:20px;float:right\">X</a>",
                    Server.HtmlEncode(msg));
        }
    }
}