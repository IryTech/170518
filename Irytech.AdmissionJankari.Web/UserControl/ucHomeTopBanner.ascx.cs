using System;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class ucHomeTopBanner : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            
                hdnCourseBanner.Value = Utils.RemoveIllegealFromCourse(new Common().CourseName);
                hdnCourseBannerId.Value = Convert.ToString(new Common().CourseId);
              

        }
       
    }
}