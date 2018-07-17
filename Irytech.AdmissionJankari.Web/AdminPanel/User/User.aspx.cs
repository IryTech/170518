using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.User
{
    public partial class User : SecurePage 
    {
        private Common _objCommon;
        private UserCategoryProperty _objUserCategoryProperty;

        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            ValidationErrorMessages();
            BindUser();
        }

        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            var data = UserManagerProvider.Instance.GetAllUserCategoryList();


            if (data.Count > 0)
            {
                try
                {
                    rptUser.Visible = true;
                    rptUser.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptUser, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in User.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptUser.Visible = false;
                rptUser.Visible = true;
                lblError.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }


        private void ValidationErrorMessages()
        {
            _objCommon = new Common();
            rfvUserName.ErrorMessage = _objCommon.GetValidationMessage("rfvUser");
            rfvDashBoardUrl.ErrorMessage = _objCommon.GetValidationMessage("rfvDashBoardUrl");
        }

        private void BindUser()
        {
            _objCommon = new Common();
            var data = UserManagerProvider.Instance.GetAllUserCategoryList();
            if (data.Count > 0)
            {
                lblEditStatus.Visible = true;
                rptUser.Visible = true;
                lblEditStatus.Text = "Edit Course Category";
                rptUser.Visible = true;
                ucCustomPaging.BindDataWithPaging(rptUser, Common.ConvertToDataTable(data));
                //rptUser.DataSource = data;
                //rptUser.DataBind();
            }
            else
            {
                rptUser.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }
        }

        private void InsertUserCategory()
        {
            var errMsg = "";
            var result = UserManagerProvider.Instance.InsertUserCategory(_objUserCategoryProperty, LoggedInUserId, out errMsg);
            if (result > 0)
            {

                lblSuccess.Visible = true;
                lblSuccess.Text = errMsg;
                ClearFileds();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = errMsg;
            }

        }

        private void UpdateUsereCategory()
        {
            _objUserCategoryProperty.UserCategoryId = Convert.ToInt16(hdnUser.Value);
            var errMsg = "";
            var result = UserManagerProvider.Instance.UpdateUserCategory(_objUserCategoryProperty, LoggedInUserId, out errMsg);
            if (result > 0)
            {
                btnSave.Text = "Save";

                lblSuccess.Visible = true;
                lblSuccess.Text = errMsg;
                ClearFileds();
                lblInsertUpdate.Text = "Add User Type";
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = errMsg;
            }

        }

        private void ClearFileds()
        {
            txtUserName.Text = string.Empty;
            txtDashBoardUrl.Text = string.Empty;
            chkStatus.Checked = false;
            rbtCreateUser.SelectedIndex = -1;
        }

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            _objUserCategoryProperty = new UserCategoryProperty
                                           {
                                               UserCategoryDashboard = txtDashBoardUrl.Text.Trim(),
                                               UserCategoryName = txtUserName.Text.Trim(),
                                               UserCategoryStatus = chkStatus.Checked,
                                               CanCreateUser = rbtCreateUser.SelectedValue == "0" ? true : false
                                           };
            switch (btnSave.Text)
            {
                case "Save":
                    InsertUserCategory();
                    break;
                case "Update":
                    UpdateUsereCategory();
                    break;
            }
            BindUser();
        }

        protected void RptUserItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblError.Visible = false;
            lblInform.Visible = false;
            lblSuccess.Visible = false;
            switch (e.CommandName)
            {
                case "Edit":
                    hdnUser.Value = e.CommandArgument.ToString();
                    var data = UserManagerProvider.Instance.GetUserCategoryById(Convert.ToInt16(hdnUser.Value));
                    if (data.Count <= 0) return;
                    {
                        var query = data.Select(result => new
                                                              {
                                                                  userName = result.UserCategoryName,
                                                                  cerateUserStatus = result.CanCreateUser,
                                                                  userStatus = result.UserCategoryStatus,
                                                                  userDashBoardUrl = result.UserCategoryDashboard
                                                              }).First();
                        txtUserName.Text = query != null && query.userName != "" ? query.userName : "N/A";
                        txtDashBoardUrl.Text = query != null && query.userDashBoardUrl != ""
                                                   ? query.userDashBoardUrl
                                                   : "N/A";
                        chkStatus.Checked = query != null && query.userStatus;
                        rbtCreateUser.SelectedValue = query != null && query.cerateUserStatus == true ? "0" : "1";
                        lblInsertUpdate.Text = "Record Update Of " + query.userName;                       
                    }
                    btnSave.Text = "Update";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweqeewqe", "OpenPoup('divUserTypeInsert','650px','sndAddUserType');", true);
              break;

            }
        }
    }
}
    
