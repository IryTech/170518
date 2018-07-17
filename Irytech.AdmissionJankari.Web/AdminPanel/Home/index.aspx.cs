using System;
using System.Web;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;


namespace IryTech.AdmissionJankari20.Web.AdminPanel.Home
{
    public partial class index : SecurePage
    {
        private Common _objCommon;
        private SecurePage _objSecurePage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            ValidateFields();
            if (Request.QueryString["logout"] != null)
                Session.Abandon();
         

        }
        private void ValidateFields()
        {
            _objCommon = new Common();
            rfvUserName.ErrorMessage = _objCommon.GetValidationMessage("rfvEmailId") ?? "N/A";
            rfvPassword.ErrorMessage = _objCommon.GetValidationMessage("rfvPassword") ?? "N/A";
            revUserName.ValidationExpression = ClsSingelton.aRegExpEmail;
            revUserName.ErrorMessage = _objCommon.GetValidationMessage("rfvEmailId") ?? "N/A";
        }

        protected void LoginButtonClick(object sender, EventArgs e)
        {
            _objSecurePage = new SecurePage();
            try
            {
                var canCreateUser = false; var userStatus = false;
                var errMsg = "";
                var mobileNo = "";
                var landingPage = "";
                var userName = "";
                int userId;
                int userTypeId;
                bool UserStatus;
                var result = UserManagerProvider.Instance.DoLogin(txtUserName.Text.Trim(), txtPassword.Text.Trim(),
                                                                  out userTypeId, out userId, out userName,
                                                                  out landingPage,
                                                                  out mobileNo, out canCreateUser, out errMsg, out UserStatus);
                if (result)
                {
                    _objSecurePage.LoggedInUserId = userId;
                    _objSecurePage.LoggedInUserType = userTypeId;
                    _objSecurePage.LoggedInUserEmailId = txtUserName.Text;
                    _objSecurePage.LoggedInUserName = userName;
                    _objSecurePage.LoggedInUserMobile = mobileNo;
                    _objSecurePage.CanCreateUser = canCreateUser;
                    Response.Redirect(landingPage);
                }
                else
                {
                    lblInfo.Visible = true;
                    lblInfo.Text = errMsg;
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