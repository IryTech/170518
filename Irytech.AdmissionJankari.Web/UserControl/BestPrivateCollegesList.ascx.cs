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
    public partial class BestPrivateCollegesList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)return;
                hdnPrivateCollege.Value = Convert.ToString(ApplicationSettings.Instance.CollegePageSize);
            hdnPrivateCourse.Value = Convert.ToString(new Common().CourseId);
            hdnAssociation.Value = Convert.ToString(ClsSingelton.AssociationId);
            hdnCourseNameAtPrivate.Value = Convert.ToString(new Common().CourseName);
            hdnAppSettingCourseId.Value = Convert.ToString(ApplicationSettings.Instance.CourseId);
        }
    }
}