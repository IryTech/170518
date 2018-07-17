using System;
using System.Linq;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class AdminProfile : System.Web.UI.UserControl
    {
        private readonly SecurePage _objSecurePage = new SecurePage();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            {
                GetAdminDetails();
            }
        }

        public void GetAdminDetails()
        {
            var userData = UserManagerProvider.Instance.GetUserListById(_objSecurePage.LoggedInUserId);
            if (userData.Count > 0)
            {
                
             lblName.Text=   userData.First().UserFullName;
             lblMobile.Text=   userData.First().MobileNo;
             lblEmailId.Text = userData.First().UserEmailid;
             imgAdmin.ImageUrl = String.Format("{0}{1}", "/image.axd?User=", (userData.First().UserImage == null || string.IsNullOrEmpty(userData.First().UserImage)) ? "NoImage.jpg" : userData.First().UserImage);
             imgAdmin.AlternateText = userData.First().UserFullName;
            }
        }
    }
}
