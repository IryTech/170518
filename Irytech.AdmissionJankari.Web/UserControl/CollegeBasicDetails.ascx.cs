using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CollegeBasicDetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        public string BranchName
        {
            
            set { lblCollegebranchName.InnerText = value; }
        }
       
        public string CourseName
        {
          
            set { lblCourseName.InnerText = value; }
        }
        
        public string Establishment
        {
            
            set { lblEstYear.InnerText = value; }
        }
        
       
        public string Management
        {

            set { lblMgtValue.InnerText = value; }
        }
        public string PopularName
        {
           
            set { lblPopularNameValue.InnerText = value; }
        }
        public string UniversityName
        {

            set { lblUniversityName.InnerText = value; }
        }
        public string  UniversityLink
        {

            set { lnkUniversity.HRef = value; }
        }
        public string SetBalackListed
        {
            set { blackListed.Attributes["class"] = value; }
        }
        public string BlackListedTitle
        {
            set { blackListed.Attributes["title"] = value; }
        }
        
    
    }
}