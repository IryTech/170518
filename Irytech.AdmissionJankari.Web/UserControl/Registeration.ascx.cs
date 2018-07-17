using System;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class Registeration : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
           

                  hdnCourseId.Value  = Convert.ToString(new Common().CourseId);
         

        }
    }
}