using System;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class Login : System.Web.UI.UserControl
    {
        private Common _objCommon;
        private SecurePage _objSecurePage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            ValidateFields();

        }
        private void ValidateFields()
        {
            _objCommon = new Common();
            rfvUserName.ErrorMessage = _objCommon.GetValidationMessage("rfvEmailId") ?? "N/A";
            rfvPassword.ErrorMessage = _objCommon.GetValidationMessage("rfvPassword") ?? "N/A";
            revEmail.ValidationExpression = ClsSingelton.aRegExpEmail;
            revEmail.ErrorMessage = _objCommon.GetValidationMessage("revEmail") ?? "N/A";
        }

        protected void LoginButtonClick(object sender, EventArgs e)
        {
            _objSecurePage = new SecurePage();
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

                    _objSecurePage.LoggedInUserId = userId;
                    _objSecurePage.LoggedInUserType = userTypeId;
                    _objSecurePage.LoggedInUserEmailId = txtUserName.Text.Trim();
                    _objSecurePage.LoggedInUserName = userName;
                    _objSecurePage.LoggedInUserMobile = mobileNo;
                    _objSecurePage.CanCreateUser = canCreateUser;

                    if (Request.QueryString["ReturnUrl"] == null)
                        Response.Redirect(landingPage, false);
                    else
                    {
                        var landingUri = "";
                        if (Request["SourceId"] != null)
                        {
                            landingUri = Request.QueryString["ReturnUrl"] + "&SourceId=" + Request["SourceId"];

                        }
                        else
                        {
                            landingUri = Request.QueryString["ReturnUrl"];
                        }
                        Response.Redirect(Utils.AbsoluteWebRoot + landingUri,
                                          false);
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
                const string addInfo = "Error while executing DoLogin in Login.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}