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
    public partial class TopRankedColleges : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hdnTopColleges.Value = Convert.ToString(ApplicationSettings.Instance.CollegePageSize);
            hdnTopCourse.Value = Convert.ToString(new Common().CourseId);
            hdnTopAssociation.Value = Convert.ToString(ClsSingelton.AssociationId);
            hdnTopCourseInAppSetting.Value = Convert.ToString(ApplicationSettings.Instance.CourseId);
            hdnTopCourseName.Value = new Common().CourseName;
        }
    }
}