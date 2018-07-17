using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CourseStreamBasicDetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string CourseStreamName
        {
            set
            {
                lblCourseStreamName.InnerText = value;
            }
        }
        public string CourseName
        {
            set
            {
                lblCourseName.InnerText = value;
            }
        }
    }
}