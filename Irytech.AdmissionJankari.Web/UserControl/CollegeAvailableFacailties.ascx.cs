using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.BL;
namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CollegeAvailableFacailties : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public List<CollegeBranchCourseFacilitiesProperty> CollegeFacalities { set { rptFacilities.DataSource = value;rptFacilities.DataBind(); } }
    }
     
}