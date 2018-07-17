using System;
using System.Collections.Generic;
using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CollegeCourseHighLIghts : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public List<CollegeBranchCourseHighlightsProperty> CollegeHighLights
        {
            set
            {
                rptHighLights.DataSource = value;
                rptHighLights.DataBind();
            
            }
        }
        
    }
}