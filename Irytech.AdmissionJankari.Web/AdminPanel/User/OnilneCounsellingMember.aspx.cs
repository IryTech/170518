using System;
using System.Linq;
using System.Net.Mail;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.User
{
    public partial class OnilneCounsellingMember : SecurePage
    {
        private Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
           CustomPaging1.PageSize = ClsSingelton.PageSize;
           CustomPaging1.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            CustomPaging1.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
           
            BindAllCourseList();
           
            BindAllCityList(); 
            BindUserMasterDetail();
        }
        #region BindAllCourseList Method

        private void BindAllCourseList()
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
                    ddlCourseName.Items.Insert(0, new ListItem("--Select--", "0"));

                }
                else
                {

                    ddlCourseName.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

        }

        #region BindAlCityList Method

        protected void DdlCourseNameSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCourseName.SelectedIndex <= 0) return;
            ddlCityName.ClearSelection();
            txtUserMobileNo.Text = string.Empty;
            txtUserEmailId.Text = string.Empty;
            if (ddlCourseName.SelectedIndex > 0)
                SearchUserDetailByCourseId();
            else
                BindUserMasterDetail();
        }

        protected void DdlCityNameSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCityName.SelectedIndex <= 0) return;
            ddlCourseName.ClearSelection();
            txtUserMobileNo.Text = string.Empty;
            txtUserEmailId.Text = string.Empty;
            if(ddlCityName.SelectedIndex>0)
              SearchUserDetailByCityId();
            else
                BindUserMasterDetail();
        }

        private void BindAllCityList()
        {
            try
            {
                var cityLIst = CityProvider.Instacnce.GetAllCityList();
                if (cityLIst.Count > 0)
                {
                    ddlCityName.Enabled = true;
                    ddlCityName.DataSource = cityLIst;
                    ddlCityName.DataTextField = "CityName";
                    ddlCityName.DataValueField = "CityId";
                    ddlCityName.DataBind();
                    ddlCityName.Items.Insert(0, new ListItem("--Select--","0"));
                }
                else
                {
                
                    ddlCityName.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BindAllCityList in OnilneCounsellingMember.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }

        private void SearchUserDetailByCourseId()
        {
            _objCommon = new Common();
            try
            {
                var userMasterList =
                    UserManagerProvider.Instance.GetUserListByPaymenStatus(
                        Convert.ToBoolean(rbtPaymentMode.SelectedValue));
                userMasterList =
                    userMasterList.Where(result => result.CourseId == Convert.ToInt32(ddlCourseName.SelectedValue))
                                  .ToList();
                if (userMasterList.Count>0)
                {
                    if (rbtPaymentMode.SelectedValue == "true")
                {
                    CustomPaging1.Visible = false;
                    rptUnpaid.Visible = false;
                    ucCustomPaging.Visible = true;
                    rptUserMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMasterList));
                }
                else
                {
                    ucCustomPaging.Visible = false;
                    rptUserMaster.Visible = false;
                    CustomPaging1.Visible = true;
                    rptUnpaid.Visible = true;
                    CustomPaging1.BindDataWithPaging(rptUnpaid, Common.ConvertToDataTable(userMasterList));
                }
            }
        else
                {
                    rptUserMaster.Visible = false; rptUnpaid.Visible = false;
                    lblErorrMsg.Visible = true;

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
                const string addInfo = "Error while executing SearchUserDetailByCourseId in OnilneCounsellingMember.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void SearchUserDetailByCityId()
        {
            _objCommon = new Common();
            try
            {
                var userMasterList = UserManagerProvider.Instance.GetUserListByPaymenStatus(Convert.ToBoolean(rbtPaymentMode.SelectedValue));
                userMasterList =
                    userMasterList.Where(result => result.CityId == Convert.ToInt32(ddlCityName.SelectedValue)).ToList();
                if (userMasterList.Count > 0)
                {

                    lblErorrMsg.Visible = false;

                    if (rbtPaymentMode.SelectedValue == "true")
                    {
                        CustomPaging1.Visible = false;
                        rptUnpaid.Visible = false;
                        ucCustomPaging.Visible = true;
                        rptUserMaster.Visible = true;
                        ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMasterList));
                    }
                    else
                    {
                        ucCustomPaging.Visible = false;
                        rptUserMaster.Visible = false;
                        CustomPaging1.Visible = true;
                        rptUnpaid.Visible = true;
                        CustomPaging1.BindDataWithPaging(rptUnpaid, Common.ConvertToDataTable(userMasterList));
                    }
                }
                else
                {
                    rptUserMaster.Visible = false;
                    rptUnpaid.Visible = false;
                    lblErorrMsg.Visible = true;

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
                const string addInfo = "Error while executing SearchUserDetailByCityId in OnilneCounsellingMember.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
                try
                {
                    rptUserMaster.Visible = true;
                    if (ddlCourseName.SelectedIndex > 0)
                    {
                        var userMasterList = UserManagerProvider.Instance.GetUserListByPaymenStatus(Convert.ToBoolean(rbtPaymentMode.SelectedValue));
                        userMasterList =
                         userMasterList.Where(result => result.CityId == Convert.ToInt32(ddlCourseName.SelectedValue)).ToList();
                        if (userMasterList.Count > 0)
                        {
                            lblErorrMsg.Visible = false;
                            rptUserMaster.Visible = true;
                            ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMasterList));
                        }
                     }

                  

                    else if (ddlCityName.SelectedIndex > 0)
                    {

                        var userMasterList =
                            UserManagerProvider.Instance.GetUserListByPaymenStatus(
                                Convert.ToBoolean(rbtPaymentMode.SelectedValue));
                        userMasterList =
                            userMasterList.Where(result => result.CityId == Convert.ToInt32(ddlCityName.SelectedValue))
                                          .ToList();
                        if (userMasterList.Count > 0)
                        {
                            lblErorrMsg.Visible = false;
                            rptUserMaster.Visible = true;
                            ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMasterList));
                        }
                    }
                    else
                    {
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
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in OnlineCounsellingMember.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }

            }

        private void BindUserMasterDetail()
        {
            _objCommon = new Common();
            try
            {
                var userMasterList = UserManagerProvider.Instance.GetUserListByPaymenStatus(Convert.ToBoolean(rbtPaymentMode.SelectedValue));

                if (userMasterList.Count > 0)
                {
                  

                    if (Convert.ToBoolean(rbtPaymentMode.SelectedValue))
                    {
                        rptUserMaster.Visible = true; rptUnpaid.Visible = false; CustomPaging1.Visible = false; ucCustomPaging.Visible = true;
                        ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMasterList));
                    }
                    else
                    {
                        CustomPaging1.Visible = true; ucCustomPaging.Visible = false;
                        rptUnpaid.Visible = true; rptUserMaster.Visible = false;
                        CustomPaging1.BindDataWithPaging(rptUnpaid, Common.ConvertToDataTable(userMasterList));
                    }

                }
                else
                {
                    rptUserMaster.Visible = false; rptUnpaid.Visible = false;
                    lblErorrMsg.Visible = true;

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
                const string addInfo = "Error while executing BindUserMasterDetail in OnilneCounsellingMember.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        protected void RbtCourseAdmissionEligibiltySelectedIndexChanged(object sender, EventArgs e)
        {
            BindUserMasterDetail();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            _objCommon = new Common();
            try
            {
               
              if ( !string.IsNullOrEmpty(txtUserEmailId.Text) && string.IsNullOrEmpty(txtUserMobileNo.Text))
                {
                  
                    ddlCityName.ClearSelection();
                    ddlCourseName.ClearSelection();
                    txtUserMobileNo.Text = string.Empty;
             
                    SearchUserDetailByEmailId();
                }
                else if ( string.IsNullOrEmpty(txtUserEmailId.Text) && !string.IsNullOrEmpty(txtUserMobileNo.Text))
                {
                  
                    ddlCityName.ClearSelection();
                    ddlCourseName.ClearSelection();
                    txtUserEmailId.Text = string.Empty;
              
                    SearchUserDetailByMobileNo();
                }
                
                else
                {
                    lblErorrMsg.Visible = true;
                    lblErorrMsg.Text = _objCommon.GetErrorMessage("lblCheckField");
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing btnSearch_Click in OnilneCounsellingMember.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        #region Bind Method for Search Users Details by MobileNo

        private void SearchUserDetailByMobileNo()
        {
            _objCommon = new Common();
            try
            {
                var userMasterList = UserManagerProvider.Instance.GetUserListByPaymenStatus(Convert.ToBoolean(rbtPaymentMode.SelectedValue));
               userMasterList =
                    userMasterList.Where(result => result.MobileNo == Convert.ToString(txtUserMobileNo.Text.Trim())).ToList();
                if (userMasterList.Count > 0)
                {

                    lblErorrMsg.Visible = false;

                    if (Convert.ToBoolean(rbtPaymentMode.SelectedValue))
                    {
                        CustomPaging1.Visible = false;
                        rptUnpaid.Visible = false;
                        ucCustomPaging.Visible = false;
                        rptUserMaster.Visible = true;
                     
                        ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMasterList));
                    }
                    else
                    {
                        ucCustomPaging.Visible = false;
                        rptUserMaster.Visible = false;
                        CustomPaging1.Visible = true;
                        rptUnpaid.Visible = true;

                        CustomPaging1.BindDataWithPaging(rptUnpaid, Common.ConvertToDataTable(userMasterList));
                    }
                }
                else
                {
                    rptUserMaster.Visible = false;
                    rptUnpaid.Visible = false;
                    lblErorrMsg.Visible = true;

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
                const string addInfo = "Error while executing SearchUserDetailByMobileNo in OnilneCounsellingMember.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        #endregion

        #region Bind Method for Search Users Details by EmailId

        private void SearchUserDetailByEmailId()
        {
            _objCommon = new Common();
            try
            {
                var userMasterList = UserManagerProvider.Instance.GetUserListByPaymenStatus(Convert.ToBoolean(rbtPaymentMode.SelectedValue));
                userMasterList =
                    userMasterList.Where(result => result.UserEmailid == Convert.ToString(txtUserEmailId.Text.Trim())).ToList();
                if (userMasterList.Count > 0)
                {

                    lblErorrMsg.Visible = false;

                    if (Convert.ToBoolean(rbtPaymentMode.SelectedValue))
                    {
                        CustomPaging1.Visible = false;
                        rptUserMaster.Visible = true;
                        ucCustomPaging.Visible = false;
                        rptUnpaid.Visible = false;
                        ucCustomPaging.BindDataWithPaging(rptUserMaster, Common.ConvertToDataTable(userMasterList));
                    }
                    else
                    {
                        ucCustomPaging.Visible = false;
                        rptUserMaster.Visible = false;
                        CustomPaging1.Visible = true;
                        rptUnpaid.Visible = true;
                        CustomPaging1.BindDataWithPaging(rptUnpaid, Common.ConvertToDataTable(userMasterList));
                    }
                }
                else
                {
                    rptUserMaster.Visible = false;
                    rptUnpaid.Visible = false;
                    lblErorrMsg.Visible = true;
                    CustomPaging1.Visible = false;
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
                string addInfo = "Error while executing SearchUserDetailByEmailId in OnilneCounsellingMember.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            var objConsulling = new Consulling();
            _objCommon = new Common();
            try
            {
                var i = objConsulling.InsertUpdateUserTransctionalDetails(Convert.ToInt32(hdnUserId.Value),
                                                                          hdnForm.Value, chkPaymentStatus.Checked,
                                                                          ddlTransaction.SelectedValue,
                                                                          txtBank.Text,
                                                                          Convert.ToString(hdnForm.Value +
                                                                                           DateTime.Now.ToString(
                                                                                               "hh:mm:ss")),
                                                                          txtPayment.Text);
                if (i > 0)
                {
                    var objmailTemplete = new MailTemplates();


                        var mail = new MailMessage
                                       {
                                           From = new MailAddress(ApplicationSettings.Instance.Email),
                                           Subject = "AdmissionJankari: Payment information"
                                       };
                    var url = Utils.AbsoluteWebRoot + "account/login/";

                        var body = objmailTemplete.MailBodyForPaymentConformation(url, hdnUserName.Value, hdnForm.Value,
                                                                                   ddlTransaction.SelectedValue,
                                                                                   Convert.ToString(hdnForm.Value +
                                                                                                    DateTime.Now
                                                                                                            .ToString(
                                                                                                                "hh:mm:ss")));
                        mail.Body = body;
                        mail.To.Add(hdnEmail.Value);
                        Utils.SendMailMessageAsync(mail);
                        lblErorrMsg.CssClass = "info";
                        lblErorrMsg.Visible = true;
                        lblErorrMsg.Text = string.Format("Transaction status is successfully updated", "ARG0");
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
                const string addInfo =
                   "Error while executing btnUpdateStatus_Click in OnilneCounsellingMember.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        #endregion
        #endregion
        #endregion
    }
}