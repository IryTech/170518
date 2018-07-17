using System;
using System.Collections.Generic;
using IryTech.AdmissionJankari.BO;
namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CollegeGallery : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public List<CollegeBranchGallery> ImageGallery
        {
            set { 
                rptCollegeImageGallery.DataSource = value;
                rptCollegeImageGallery.DataBind(); 
            }
        }
    }
}