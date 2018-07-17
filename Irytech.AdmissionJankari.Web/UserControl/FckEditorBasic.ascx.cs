using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class FckEditorBasic : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected string Value
        {
            get  {return txtFckEditorBasic.Text;}
            set { txtFckEditorBasic.Text = Value; }
        }
    }
}