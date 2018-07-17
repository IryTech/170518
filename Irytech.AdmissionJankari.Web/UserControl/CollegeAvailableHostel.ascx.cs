using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CollegeAvailableHostel : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public List<CollegeBranchCourseHostelProperty> CollegeHostel
        {
            set
            {
                if (value.Count > 0)
                {
                    divHostel.Visible = true;
                    lblHostel.Visible = false;
                    rptCollegeHostel.DataSource = value;
                    rptCollegeHostel.DataBind();
                }
                else
                {
                    divHostel.Visible = false;
                    lblHostel.CssClass = "info";
                    lblHostel.Visible = true;
                    lblHostel.Text = new Common().GetErrorMessage("noRecords");
                }
            }
        }

        protected string ChangeValue(bool status)
        {
            return status == true ? "Available" : "Not Available";
        }
    }
}