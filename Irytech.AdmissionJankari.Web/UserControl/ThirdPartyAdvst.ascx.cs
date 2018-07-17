using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.Components;
using System.Web.UI.HtmlControls;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class ThirdPartyAdvst : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            System.Web.UI.Page UC = new Page();
            ThirdPartyAdvst UC1 = new ThirdPartyAdvst();
            //var deferAttr = "defer=\"defer\"";
            //var script = string.Format("<script type=\"text/javascript\"{0} src=\"{1}\"></script>", deferAttr, "http://www.hotelscombined.com/SearchBox/89975");
            ////System.Web.UI.Page.ScriptManager.RegisterStartupScript(typeof(UserControl), "myscript", script);
            //if (!Page.ClientScript.IsClientScriptBlockRegistered("ThirdPartyAdvst.ascx.css"))
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ThirdPartyAdvst.ascx.css", script);
            //}
            var script = new HtmlGenericControl("script");
            script.Attributes["type"] = "text/javascript";
            script.Attributes["src"] = "http://www.hotelscombined.com/SearchBox/89975";
            script.Attributes["defer"] = "defer";
            divScript.Controls.Add(script);

        }
        
    }
}