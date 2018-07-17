using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class GoogleMap : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
        }
        public string StateName
        {
            set { txtAddress2.Text = value; }
            get { return txtAddress2.Text; }
          
        }
         public string CityName
        {
            set { txtAddress1.Text = value; }
            get { return txtAddress1.Text; }
          
        }
    }
}