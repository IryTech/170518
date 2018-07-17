using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.User
{
    public partial class EditUserDetails : System.Web.UI.Page
    {
       private readonly SecurePage _objSecurePage = new SecurePage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindCourseList();
            BindUserCategory();
            BindCountry();
            BindState(0);
            BindCity(0);
            BindUserDetails();

        }
        #region courselist
       
        private void BindCourseList()
        {
            var courseData = CourseProvider.Instance.GetAllCourseList();
            if (courseData.Count > 0)
            {
              
                ddlCourse.DataSource = courseData;
                ddlCourse.DataTextField = "CourseName";
                ddlCourse.DataValueField = "CourseId";
                ddlCourse.DataBind();
                ddlCourse.Items.Insert(0, new ListItem("--Select--", "0"));
            }

            else
            {
                ddlCourse.Items.Insert(0, new ListItem("--Select--", "0"));
               

            }

        }
        #endregion

        #region usercategory
        private void BindUserCategory()
        {
            try
            {
                var data = UserManagerProvider.Instance.GetAllUserCategoryList();
                if (data.Count > 0)
                {
                    ddlUserCategory.DataSource = data;
                    ddlUserCategory.DataTextField = "UserCategoryName";
                    ddlUserCategory.DataValueField = "UserCategoryId";
                    ddlUserCategory.DataBind();
                    ddlUserCategory.Items.Insert(0, new ListItem("--Select--", "0"));


                }
                else
                {
                    ddlUserCategory.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindUserCategory in User/EditUserDetails.axpx   :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        #endregion

        #region country,state,city
        private void BindCountry()
        {
            try
            {
                List<CountryProperty> data;
                data = CountryProvider.Instance.GetAllCountry();
                if (data.Count > 0)
                {
                    ddlCountry.DataSource = data;
                    ddlCountry.DataTextField = "CountryName";
                    ddlCountry.DataValueField = "CountryId";
                    ddlCountry.DataBind();
                    ddlCountry.Items.Insert(0, new ListItem("--Select--", "0"));


                }
                else
                {
                    ddlCountry.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCountry in User/EditUserDetails.axpx   :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindState(int countryId)
        {
            try
            {
                List<StateProperty> data;
                data = countryId == 0 ? StateProvider.Instance.GetAllState() : StateProvider.Instance.GetStateByCountry(countryId);
                if (data.Count > 0)
                {
                    ddlState.DataSource = data;
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateId";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, new ListItem("--Select--", "0"));


                }
                else
                {
                    ddlState.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindState in User/EditUserDetails.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindCity(int stateId)
        {
            try
            {
                var data = stateId == 0 ? CityProvider.Instacnce.GetAllCityList() : CityProvider.Instacnce.GetCityListByState(stateId);
                if (data.Count > 0)
                {

                    ddlCity.DataSource = data;
                    ddlCity.DataTextField = "CityName";
                    ddlCity.DataValueField = "CityId";
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCity in User/EditUserDetails.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        #endregion


        #region userdetails......
        private void BindUserDetails()
        {
            try
            {
                if (Request["userId"] != null && !string.IsNullOrEmpty(Request["userId"]))
                {
                    var userDetals = UserManagerProvider.Instance.GetUserListById(Convert.ToInt32(Request["userId"]));
                    if (userDetals.Count > 0)
                    {
                        var userData = userDetals.FirstOrDefault();
                        if (userData != null)
                        {
                            txtUserName.Text = userData.UserFullName;
                            txtEmailId.Text = userData.UserEmailid;
                            txtMobileNo.Text = userData.MobileNo;
                            txtPhoneNo.Text = userData.PhoneNo;
                            txtPassword.Text = userData.UserPassword;
                            txtCorrsepondenceaddress.Text = userData.UserCorrespondenceAddress;
                            txtPopupUserPermanentAddress.Text = userData.UserPermanentAddress;
                            txtPopupUserPinCode.Text = userData.UserPincode;
                            txtDOB.Text = userData.UserDOB.ToString();
                            ddlCourse.SelectedValue = userData.CourseId.ToString();
                            ddlUserCategory.SelectedValue= userData.UserCategoryId.ToString();
                            ddlState.SelectedValue = userData.StateId.ToString();
                            ddlCountry.SelectedValue = userData.CountryCode.ToString();
                            ddlCity.SelectedValue = userData.CityId.ToString();
                            chkPopupUserStatus.Checked = userData.UserStatus;
                            txtFatherName.Text = userData.UserFatherName.Trim();
                            imgUser.ImageUrl = String.Format("{0}{1}", "/image.axd?User=", (userData.UserImage == null || string.IsNullOrEmpty(userData.UserImage)) ? "NoImage.jpg" : userData.UserImage);
                            imgUser.AlternateText = userData.UserFullName;
                            hdnUserImage.Value = userData.UserImage;
                        }
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
                const string addInfo = "Error while executing BindUserDetails  in User/EditUserDetails.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        #endregion

        #region country ,stat selected changed event
        protected void DdlCountrySelectedIndexChanged(object sender, EventArgs e)
        {
            BindState(ddlCountry.SelectedIndex > 0 ? Convert.ToInt16(ddlCountry.SelectedValue) : 0);
        }
        protected void DdlStateSelectedIndexChanged(object sender, EventArgs e)
        {
            BindCity(ddlState.SelectedIndex > 0 ? Convert.ToInt16(ddlState.SelectedValue) : 0);
        }
        #endregion

        #region calender
        protected void Calendar1SelectionChanged(object sender, EventArgs e)
        {
            txtDOB.Text = Calendar1.SelectedDate.ToString();
            Calendar1.Visible = false;
        }
        protected void BtnCalenderClick(object sender, EventArgs e)
        {
            Calendar1.Visible = true;
        }
        #endregion

        #region update userdetails
        protected void BtnSaveClick(object sender, EventArgs e)
        {
            try
            {
                if (flpImage.HasFile)
                {
                    hdnUserImage.Value = flpImage.FileName;
                    flpImage.SaveAs(Server.MapPath(new Common().GetFilepath("UserImage"))+flpImage.FileName);
                }
                var objUserRegistration = new UserRegistrationProperty
                {
                    
                    UserId = Convert.ToInt32(Request["userId"].ToString()),
                    CourseId = Convert.ToInt32(ddlCourse.SelectedValue),
                    CountryCode =
                        Convert.ToInt32(ddlCountry.SelectedValue),
                    StateId = Convert.ToInt32(ddlState.SelectedValue),
                    CityId = Convert.ToInt32(ddlCity.SelectedValue),
                    UserCategoryId =
                        Convert.ToInt32(ddlUserCategory.SelectedValue),
                    UserEmailid = Convert.ToString(txtEmailId.Text),
                    UserFullName =
                        Convert.ToString(
                            txtUserName.Text.Trim()),
                    UserPassword =
                        Convert.ToString(
                            txtPassword.Text.Trim()),
                            MobileNo = txtMobileNo.Text.Trim(),
                    PhoneNo =
                        Convert.ToString(
                            txtPhoneNo.Text.Trim()),
                    UserCorrespondenceAddress =
                        Convert.ToString(
                            txtCorrsepondenceaddress.Text.Trim()),
                    UserPermanentAddress =
                        Convert.ToString(
                            txtPopupUserPermanentAddress.Text.Trim()),
                    UserPincode =
                        Convert.ToString(
                            txtPopupUserPinCode.Text.Trim()),
                    UserStatus = chkPopupUserStatus.Checked,
                    UserGender = rbtGender.SelectedValue,
                    UserDOB = Common.GetDateFromString(txtDOB.Text),
                    UserImage = hdnUserImage.Value,
                    UserFatherName = txtFatherName.Text.Trim()
                };
                var errorMsg = "";
                var update = UserManagerProvider.Instance.UpdateUserInfo(objUserRegistration, _objSecurePage.LoggedInUserId,
                                                                         out errorMsg);
                if (update > 0)
                {

                   Response.Redirect("UserMaster.aspx");
                }
               
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing btnEdit_Click in UserMaster.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }


        }
        #endregion
    }
}