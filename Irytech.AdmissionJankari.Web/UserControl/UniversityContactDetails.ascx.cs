using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UniversityContactDetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string PhoneNo
        {
            set
            {
                lblPhoneNo.InnerText = value;
            }
        }
        public string Website
        {
            set
            {
                lblWebsite.InnerText = value;
            }
        }
        public string Fax
        {
            set
            {
                lblFax.InnerText = value;
            }
        }
        public string Address
        {
            set
            {
                lblAddress.InnerText = value;
            }
        }
        public string State
        {
            set
            {
                lblState.InnerText = value;
            }
        }

    }
}