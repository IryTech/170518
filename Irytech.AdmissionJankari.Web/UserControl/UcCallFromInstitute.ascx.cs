using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcCallFromInstitute : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            
                hndCourseid.Value= new IryTech.AdmissionJankari.BL.Common().CourseId.ToString();

        }

        public string CollegeName
        {
            set { hndCollegeName.Value = value; }
        }
        public string CollegeBranchCourseId
        {
            set { hndCollegeBranchCourseId.Value = value; }

        }
        public string CityName
        {
            set { hndCityName.Value = value; }
        }

    }
}