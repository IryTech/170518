using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CollegeAvailableRank : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        public List<CollegeBranchRankProperty> CollegeRankData
        {
            set
            {
                try
                {

                    if (value.Count > 0)
                    {

                        rptCollegeRank.DataSource = value;
                        rptCollegeRank.DataBind();
                    }
                    else
                    {
                        divCollegeRank.Visible = false;
                        lblCollegeRankResult.Visible = true;
                        lblCollegeRankResult.CssClass = "info";
                        lblCollegeRankResult.Text = new Common().GetErrorMessage("noRecords");
                        rptCollegeRank.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo =
                        "Error in Executing  BindCollegeRank in CollegeAvailAbleRank.ascx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
        }
    }
}