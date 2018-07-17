using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UniversityBasicDetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string UniversityName
        {
            set 
            {
                lblUniversityName.InnerText =value;
            }
        }
        public string UniversityCategoryName
        {
            set
            {
                lblUniversityCategoryName.InnerText = value;
            }
        }
        public string PopularName
        {
            set 
            {
                lblPopularName.InnerText = value;
            }
        }
        public string Establishment
        {
            set
            {
                lblEstablishment.InnerText = value;
            }
        }
        
    }
}