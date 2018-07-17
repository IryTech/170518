using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcStudentPersonelInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindUserPersonelInfo();
        }

        // Method to Bind The User Personel Info

        public void BindUserPersonelInfo()
        {
            SecurePage _objSecurePage = new SecurePage();
            try
            {
                var UserDetails = UserManager.Instance.GetUserListById(_objSecurePage.LoggedInUserId);
                var sp = UserDetails.First();
                lblName.Text = _objSecurePage.LoggedInUserName;
                lblEmailId.Text = _objSecurePage.LoggedInUserEmailId;
                lblMobile.Text = _objSecurePage.LoggedInUserMobile;
                ConsullingCourseId = sp.CourseId;
                var CourseDetails = CourseProvider.Instance.GetCourseById(sp.CourseId);
                var CourseInfo = CourseDetails.First();
                lblCourse.Text = CourseInfo.CourseName;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindUserPersonelInfo in UcStudentPersonelInfo.axcs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        public static int ConsullingCourseId
        {
            get;
            set;
        }
    }
}