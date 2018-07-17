using System;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.Components.Web.Controls;

namespace IryTech.AdmissionJankari20.Web.Account
{
    public partial class CollegeLogin : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            ValidateFields();
        }
        private void ValidateFields()
        {
            try
            {
                var objCommon = new Common();
                rfvUserName.ErrorMessage = objCommon.GetValidationMessage("rfvEmailId") ?? "N/A";
                rfvPassword.ErrorMessage = objCommon.GetValidationMessage("rfvPassword") ?? "N/A";
                revEmail.ValidationExpression = ClsSingelton.aRegExpEmail;
                revEmail.ErrorMessage = objCommon.GetValidationMessage("revEmail") ?? "N/A";
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing ValidateFields in CollegeLogin.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void LoginButtonClick(object sender, EventArgs e)
        {
            var objSecurePage = new SecurePage();
            try
            {
                var canCreateUser = false;
                var userStatus = false;
                var errMsg = "";
                var mobileNo = "";
                var landingPage = "";
                var userName = "";
                int userId;
                int userTypeId;
                var result = UserManagerProvider.Instance.DoLogin(txtUserName.Text.Trim(), txtPassword.Text.Trim(),
                                                                  out userTypeId, out userId, out userName,
                                                                  out landingPage,
                                                                  out mobileNo, out canCreateUser, out errMsg, out userStatus);
                if (result)
                {
                     if (userStatus)
                        {
                            if (Request.QueryString["ReturnUrl"] == null)
                            {
                                objSecurePage.LoggedInUserId = userId;
                                objSecurePage.LoggedInUserType = userTypeId;
                                objSecurePage.LoggedInUserEmailId = txtUserName.Text.Trim();
                                objSecurePage.LoggedInUserName = userName;
                                objSecurePage.LoggedInUserMobile = mobileNo;
                                objSecurePage.CanCreateUser = canCreateUser;
                                Response.Redirect(landingPage, true);
                            }

                            else
                                Response.Redirect(Utils.AbsoluteWebRoot + Convert.ToString(Request.QueryString["ReturnUrl"]));
                        }

                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = string.Format("Your account is not active");
                        }
                   
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = errMsg;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing LoginButtonClick in CollegeLogin.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}