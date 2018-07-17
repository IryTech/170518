using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CollegeContactDetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string ContactEmailId
        {

            set { lblEmailId.InnerText = value; }
        }

        public string ContactWebsites
        {

            set { lblWebSites.InnerText = value; }
        }

        public string ContactPhone
        {

            set { lblPhone.InnerText = value; }
        }

        public string ContactFax
        {

            set { lblFax.InnerText = value; }
        }
        public string ContactAddress
        {

            set { lblAddress.InnerText = value; }
        }
        public string ContactPhone1
        {
            set { lblPhone1.InnerText = value; }
        }
        public bool IsEmailIdVisisble
        {
            set { divEmailId.Visible = value; }
        }
        public bool IsWebSiteVisible
        {
            set { divWebsite.Visible = value; }
        }
        public bool IsVisiblePhone
        {
            set { divPhone.Visible = value; }
        }
        public bool IsVisibleFax
        {
            set { divFax.Visible = value; }
        }
        public bool IsVisibleAddrs
        {
            set { divAdrss.Visible = value; }
        }
        public bool IsVisibleMobile
        {
            set { divMobile.Visible = value; }
        }
       
    }
}