using System;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.User
{
    public partial class UserMaster : SecurePage
    {
        private Common _objCommon;

        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
            ucCustomPaging.ButtonsCount = 10;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
           
            BindAllCourseList();
            BindAllUserCatagoryList();
            BindAllStateList();
            BindAllCityList(); BindUserMasterDetail();
        }

        #region Methods

        #region Bind All Records for User Master Details

        protected void BindUserMasterDetail()
        {
            _objCommon = new Common();
            try
            {
                var userMasterList = UserManagerProvider.Instance.GetAllUserList();

                if (userMasterList.Count > 0)
                {
                    lblErorrMsg.Visible = false;
                    rptUserMaster.Visible = true;

                    ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMasterList));

                  }
                else
                {
                    rptUserMaster.Visible = false;
                    lblErorrMsg.Visible = true;
                    lblSeccessMsg.Visible = false;
                    lblErorrMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        #endregion

        #region BindAllCourseList Method

        protected void BindAllCourseList()
        {
            try
            {
                var courseList = CourseProvider.Instance.GetAllCourseList();
                if (courseList.Count > 0)
                {
                    ddlCourseName.Enabled = true;
                    ddlCourseName.DataSource = courseList;
                    ddlCourseName.DataTextField = "CourseName";
                    ddlCourseName.DataValueField = "CourseId";
                    ddlCourseName.DataBind();
                    ddlCourseName.Items.Insert(0, "Select Course");

                }
                else
                {
                    ddlCourseName.Enabled = false;
                    ddlCourseName.Items.Insert(0, "Data not avialable");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

        }

        #endregion

        #region BindAlUserCatagoryList Method

        protected void BindAllUserCatagoryList()
        {
            try
            {
                var categoryList = UserManagerProvider.Instance.GetAllUserCategoryList();
                if (categoryList.Count > 0)
                {
                    ddlUSerCategoryName.Enabled = true;
                    ddlUSerCategoryName.DataSource = categoryList;
                    ddlUSerCategoryName.DataTextField = "UserCategoryName";
                    ddlUSerCategoryName.DataValueField = "UserCategoryId";
                    ddlUSerCategoryName.DataBind();
                    ddlUSerCategoryName.Items.Insert(0, new ListItem("Select","0"));

                }
                else
                {
                    ddlUSerCategoryName.Enabled = false;
                    ddlUSerCategoryName.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

        }

        #endregion

        #region BindAlStateList Method

        protected void BindAllStateList()
        {
            try
            {
                var stateList = StateProvider.Instance.GetAllState();
                if (stateList.Count > 0)
                {
                    ddlStateName.Enabled = true;
                    ddlStateName.DataSource = stateList;
                    ddlStateName.DataTextField = "StateName";
                    ddlStateName.DataValueField = "StateId";
                    ddlStateName.DataBind();
                    ddlStateName.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlStateName.Enabled = false;
                    ddlStateName.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BindAllStateList() in city.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        #endregion

        #region BindAlCityList Method

        protected void BindAllCityList()
        {
            try
            {
                var cityLIst = CityProvider.Instacnce.GetAllCityList().OrderBy(result=>result.CityName).ToList();
                if (cityLIst.Count > 0)
                {
                    ddlCityName.Enabled = true;
                    ddlCityName.DataSource = cityLIst;
                    ddlCityName.DataTextField = "CityName";
                    ddlCityName.DataValueField = "CityId";
                    ddlCityName.DataBind();
                    ddlCityName.Items.Insert(0, new ListItem("Select","0"));
                }
                else
                {
                    ddlCityName.Enabled = false;
                    ddlCityName.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BindAllCityList in UserMaster.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }

        #endregion

        #region Bind Method for Search Users Details by Course Id

        protected void SearchUserDetailByCourseId()
        {
            _objCommon = new Common();
            try
            {
                var userMasterList =
                    UserManagerProvider.Instance.GetUserListByCourseId(
                        Convert.ToInt32(ddlCourseName.SelectedValue.ToString()));
                if (userMasterList.Count > 0)
                {
                    ucCustomPaging.Visible = true;
                    lblErorrMsg.Visible = false;
                    rptUserMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMasterList), true);
                }
                else
                {
                   
                    ucCustomPaging.Visible = false;
                    rptUserMaster.Visible = false;
                    lblErorrMsg.Visible = true;
                    lblSeccessMsg.Visible = false;
                    lblErorrMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        #endregion

        #region Bind Method for Search Users Details by State Id

        protected void SearchUserDetailByStateId()
        {
            _objCommon = new Common();
            try
            {
                var userMasterList =
                    UserManagerProvider.Instance.GetUserListByState(
                        Convert.ToInt32(ddlStateName.SelectedValue.ToString()));
                if (userMasterList.Count > 0)
                {
                    ucCustomPaging.Visible = true;
                    lblErorrMsg.Visible = false;
                    rptUserMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMasterList));
                }
                else
                {
                    
                    ucCustomPaging.Visible = false;
                    rptUserMaster.Visible = false;
                    lblErorrMsg.Visible = true;
                    lblSeccessMsg.Visible = false;
                    lblErorrMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        #endregion

        #region Bind Method for Search Users Details by City Id

        protected void SearchUserDetailByCityId()
        {
            _objCommon = new Common();
            try
            {
                var userMasterList =
                    UserManagerProvider.Instance.GetUserListByCity(Convert.ToInt32(ddlCityName.SelectedValue.ToString()));
                if (userMasterList.Count > 0)
                {
                    ucCustomPaging.Visible = true;
                    lblErorrMsg.Visible = false;
                    rptUserMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMasterList));
                }

                else
                {
                   
                    ucCustomPaging.Visible = false;
                    rptUserMaster.Visible = false;
                    lblErorrMsg.Visible = true;
                    lblSeccessMsg.Visible = false;
                    lblErorrMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        #endregion

        #region Bind Method for Search Users Details by USerCategory Id

        protected void SearchUserDetailByUSerCategoryId()
        {
            _objCommon = new Common();
            try
            {
                var userMasterList =
                    UserManagerProvider.Instance.GetUserListByCategory(
                        Convert.ToInt32(ddlUSerCategoryName.SelectedValue.ToString()));
                if (userMasterList.Count > 0)
                {
                    ucCustomPaging.Visible = true;
                    lblErorrMsg.Visible = false;
                    rptUserMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMasterList));

                }
                else
                {
                  
                    ucCustomPaging.Visible = false;
                    rptUserMaster.Visible = false;
                    lblErorrMsg.Visible = true;
                    lblSeccessMsg.Visible = false;
                    ucCustomPaging.Visible = false;
                    lblErorrMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        #endregion

        #region Bind Method for Search Users Details by UserName

        protected void SearchUserDetailByUserName()
        {
            _objCommon = new Common();
            try
            {
                var userMasterList =
                    UserManagerProvider.Instance.GetUserListByName(Convert.ToString(txtUserName.Text.Trim()));
                if (userMasterList.Count > 0)
                {
                    ucCustomPaging.Visible = true;
                    lblErorrMsg.Visible = false;
                    rptUserMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMasterList));

                }
                else
                {
                    BindUserMasterDetail();
                    ucCustomPaging.Visible = false;
                    rptUserMaster.Visible = false;
                    lblErorrMsg.Visible = true;
                    lblSeccessMsg.Visible = false;
                    lblErorrMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        #endregion

        #region Bind Method for Search Users Details by MobileNo

        protected void SearchUserDetailByMobileNo()
        {
            _objCommon = new Common();
            try
            {
                var userMasterList =
                    UserManagerProvider.Instance.GetUserListByMobile(Convert.ToString(txtUserMobileNo.Text.Trim()));
                if (userMasterList.Count > 0)
                {
                    ucCustomPaging.Visible = true;
                    lblErorrMsg.Visible = false;
                    rptUserMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMasterList));

                }
                else
                {
                    BindUserMasterDetail();
                    ucCustomPaging.Visible = false;
                    rptUserMaster.Visible = false;
                    lblErorrMsg.Visible = true;
                    lblSeccessMsg.Visible = false;
                    lblErorrMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        #endregion

        #region Bind Method for Search Users Details by EmailId

        protected void SearchUserDetailByEmailId()
        {
            _objCommon = new Common();
            try
            {
                var userMasterList =
                    UserManagerProvider.Instance.GetUserListByEmailId(Convert.ToString(txtUserEmailId.Text.Trim()));
                if (userMasterList.Count > 0)
                {
                    ucCustomPaging.Visible = true;
                    lblErorrMsg.Visible = false;
                    rptUserMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMasterList));

                }
                else
                {
                    BindUserMasterDetail();
                    ucCustomPaging.Visible = false;
                    rptUserMaster.Visible = false;
                    lblErorrMsg.Visible = true;
                    lblSeccessMsg.Visible = false;
                    lblErorrMsg.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        #endregion

        #region Bind Method for ClearControls

        protected void ClearControls()
        {
            ddlPopupCityName.ClearSelection();
            ddlPopupCountryName.ClearSelection();
            ddlPopupCourseName.ClearSelection();
            ddlPopupStateName.ClearSelection();
            ddlPopupUSerCategoryName.ClearSelection();
            txtPopupCorresPondence.Text = string.Empty;
            txtPopupUserMobileNo.Text = string.Empty;
            txtPopupUserName.Text = string.Empty;
            txtPopupUserPassword.Text = string.Empty;
            txtPopupUserPermanentAddress.Text = string.Empty;
            txtPopupUserPhoneNo.Text = string.Empty;
            txtPopupUserPinCode.Text = string.Empty;
            chkPopupUserStatus.Checked = false;
            hndCategoryId.Value = "";
            hndCityId.Value = "";
            hndCourseId.Value = "";
            hndCountryId.Value = "";
            hndEmailID.Value = "";
            hndStateId.Value = "";

        }

        #endregion

        #endregion

        protected void ddlCourseName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlUSerCategoryName.ClearSelection();
            ddlStateName.ClearSelection();
            ddlCityName.ClearSelection();
            txtUserName.Text = string.Empty;
            txtUserMobileNo.Text = string.Empty;
            txtUserEmailId.Text = string.Empty;
            if (ddlCourseName.SelectedIndex > 0)
            {
                SearchUserDetailByCourseId();
            }
            else
            {
                BindUserMasterDetail();
            }
        }

        protected void ddlStateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlUSerCategoryName.ClearSelection();
            ddlCourseName.ClearSelection();
            ddlCityName.ClearSelection();
            txtUserName.Text = string.Empty;
            txtUserMobileNo.Text = string.Empty;
            txtUserEmailId.Text = string.Empty;
          
            if (ddlStateName.SelectedIndex > 0)
            {
                SearchUserDetailByStateId();
            }
            else
            {
                BindUserMasterDetail();
            }
        }

        protected void ddlCityName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlUSerCategoryName.ClearSelection();
            ddlCourseName.ClearSelection();
            ddlStateName.ClearSelection();
            txtUserName.Text = string.Empty;
            txtUserMobileNo.Text = string.Empty;
            txtUserEmailId.Text = string.Empty;
            
            if (ddlCityName.SelectedIndex > 0)
            {
                SearchUserDetailByCityId();
            }
            else
            {
                BindUserMasterDetail();
            }
        }
       
        protected void ddlUSerCategoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCityName.ClearSelection();
            ddlCourseName.ClearSelection();
            ddlStateName.ClearSelection();
            txtUserName.Text = string.Empty;
            txtUserMobileNo.Text = string.Empty;
            txtUserEmailId.Text = string.Empty;
            if (ddlUSerCategoryName.SelectedIndex > 0)
            {
                SearchUserDetailByUSerCategoryId();
            }
            else
            {
                BindUserMasterDetail();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            _objCommon = new Common();
            try
            {
                if (!string.IsNullOrWhiteSpace(txtUserName.Text) && string.IsNullOrWhiteSpace(txtUserEmailId.Text) &&
                    string.IsNullOrWhiteSpace(txtUserMobileNo.Text))
                {
                    ddlUSerCategoryName.ClearSelection();
                    ddlStateName.ClearSelection();
                    ddlCityName.ClearSelection();
                    ddlCourseName.ClearSelection();
                    txtUserEmailId.Text = string.Empty;
                    txtUserMobileNo.Text = string.Empty;
                    SearchUserDetailByUserName();
                }
                else if (string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtUserEmailId.Text) &&
                         string.IsNullOrEmpty(txtUserMobileNo.Text))
                {
                    ddlUSerCategoryName.ClearSelection();
                    ddlStateName.ClearSelection();
                    ddlCityName.ClearSelection();
                    ddlCourseName.ClearSelection();
                    txtUserMobileNo.Text = string.Empty;
                    txtUserName.Text = string.Empty;
                    SearchUserDetailByEmailId();
                }
                else if (string.IsNullOrEmpty(txtUserName.Text) && string.IsNullOrEmpty(txtUserEmailId.Text) &&
                         !string.IsNullOrEmpty(txtUserMobileNo.Text))
                {
                    ddlUSerCategoryName.ClearSelection();
                    ddlStateName.ClearSelection();
                    ddlCityName.ClearSelection();
                    ddlCourseName.ClearSelection();
                    txtUserEmailId.Text = string.Empty;
                    txtUserName.Text = string.Empty;
                    SearchUserDetailByMobileNo();
                }
                else
                {
                    ddlUSerCategoryName.ClearSelection();
                    ddlStateName.ClearSelection();
                    ddlCityName.ClearSelection();
                    ddlCourseName.ClearSelection();
                    txtUserEmailId.Text = string.Empty;
                    txtUserName.Text = string.Empty;
                    BindUserMasterDetail();
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BindAllCityList in UserMaster.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var objUserRegistration = new UserRegistrationProperty
                                              {
                                                  UserId = Convert.ToInt32(hndUserId.Value),
                                                  CourseId = Convert.ToInt32(hndCourseId.Value),
                                                  CountryCode =
                                                      Convert.ToInt32(hndCountryId.Value),
                                                  StateId = Convert.ToInt32(hndStateId.Value),
                                                  CityId = Convert.ToInt32(hndCityId.Value),
                                                  UserCategoryId =
                                                      Convert.ToInt32(hndCategoryId.Value),
                                                  UserEmailid = Convert.ToString(hndEmailID.Value),
                                                  UserFullName =
                                                      Convert.ToString(
                                                          txtPopupUserName.Text.Trim()),
                                                  UserPassword =
                                                      Convert.ToString(
                                                          txtPopupUserPassword.Text.Trim()),
                                                  PhoneNo =
                                                      Convert.ToString(
                                                          txtPopupUserPhoneNo.Text.Trim()),
                                                  UserCorrespondenceAddress =
                                                      Convert.ToString(
                                                          txtPopupCorresPondence.Text.Trim()),
                                                  UserPermanentAddress =
                                                      Convert.ToString(
                                                          txtPopupUserPermanentAddress.Text.Trim()),
                                                  UserPincode =
                                                      Convert.ToString(
                                                          txtPopupUserPinCode.Text.Trim()),
                                                  UserStatus = chkPopupUserStatus.Checked,
                                                  UserGender = rbtGender.SelectedValue,
                                                  UserDOB = Common.GetDateFromString(txtDOB.Text)
                                              };
                var errorMsg = "";
                var update = UserManagerProvider.Instance.UpdateUserInfo(objUserRegistration, LoggedInUserId,
                                                                         out errorMsg);
                if (update > 0)
                {

                    lblPopupMessage.Visible = true;
                    lblPopupMessage.Text = errorMsg;
                    ClearControls();
                }
                BindUserMasterDetail();
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

            Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript",
                                                    "close();", true);

        }


        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();

            try
            {
                rptUserMaster.Visible = true;
                lblMsg.Visible = false;
                if (ddlCourseName.SelectedIndex > 0)
                {
                    var userMaster =
                        UserManagerProvider.Instance.GetUserListByCourseId(
                            Convert.ToInt32(ddlCourseName.SelectedValue.ToString()));

                    if (userMaster.Count > 0)
                    {
                        lblErorrMsg.Visible = false;
                        rptUserMaster.Visible = true;
                        ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMaster));
                    }
                }

                else if (ddlStateName.SelectedIndex > 0)
                {
                    var userMaster =
                        UserManagerProvider.Instance.GetUserListByState(
                            Convert.ToInt32(ddlStateName.SelectedValue.ToString()));
                    if (userMaster.Count > 0)
                    {
                        lblErorrMsg.Visible = false;
                        rptUserMaster.Visible = true;
                        ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMaster));
                    }

                }

                else if (ddlCityName.SelectedIndex > 0)
                {

                    var userMaster =
                        UserManagerProvider.Instance.GetUserListByCity(
                            Convert.ToInt32(ddlCityName.SelectedValue.ToString()));
                    if (userMaster.Count > 0)
                    {
                        lblErorrMsg.Visible = false;
                        rptUserMaster.Visible = true;
                        ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMaster));
                    }
                }
                else if (ddlUSerCategoryName.SelectedIndex > 0)
                {
                    var userMaster =
                        UserManagerProvider.Instance.GetUserListByCategory(
                            Convert.ToInt32(ddlUSerCategoryName.SelectedValue.ToString()));
                    if (userMaster.Count > 0)
                    {
                        lblErorrMsg.Visible = false;
                        rptUserMaster.Visible = true;
                        ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMaster));

                    }

                }

                else
                {
                    var userMasterList = UserManagerProvider.Instance.GetAllUserList();
                    if (userMasterList.Count > 0)
                    {
                        lblErorrMsg.Visible = false;
                        rptUserMaster.Visible = true;
                        ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMasterList));
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in UserMaster.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
      
        
    }
}