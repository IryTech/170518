using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class SponseredColleges : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected override void OnLoad(EventArgs e)
        {
            if (IsPostBack) return;
            BindSponseredColleges();
        }
        public void BindSponseredColleges()
        {
            try
            {
                var client = new Common().GetOurClientsCollege();
                {
                    if (client.Rows.Count > 0)
                    {
                        dtlSponseredColleges.Visible = true;
                        dtlSponseredColleges.DataSource = client;
                        dtlSponseredColleges.DataBind();
                        headerSponseredColleges.Visible = true;
                        divSponseredCollege.Visible = true;
                    }
                    else
                    {
                        dtlSponseredColleges.Visible = false;
                        headerSponseredColleges.Visible = false;
                        divSponseredCollege.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindSponseredColleges in SponseredColleges.ascx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }
    }
}