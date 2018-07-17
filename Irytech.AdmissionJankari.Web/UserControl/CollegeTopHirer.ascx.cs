using System;
using System.Collections.Generic;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CollegeTopHirer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pagerHighLightsCollege.PageSize = ApplicationSettings.Instance.CollegePageSize;
            pagerHighLightsCollege.ButtonsCount = ApplicationSettings.Instance.CollegePageCount;
            pagerHighLightsCollege.PagerPageIndexChanged += PagerPageIndexChanged;
            if(!IsPostBack)
            {
                if (Request.QueryString["CollegeBranchCourseId"] != null)
                {
                    BindCollegeHighLights(Request.QueryString["CollegeBranchCourseId"]);
                }
            }
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var topHireer =
                    CollegeProvider.Instance.GetCollegeTopHirer(
                        Convert.ToInt32(Request.QueryString["CollegeBranchCourseId"]));

                if (topHireer.Count > 0)
                {

                    rptTopHirer.Visible = true;

                    pagerHighLightsCollege.BindDataWithPaging(rptTopHirer, Common.ConvertToDataTable(topHireer));
                }
            }
            catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in CollegeTopHirer.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
        }
        public void BindCollegeHighLights(string collegeBranchCourseId)
        {
            try
            {
                var topHireer =
                    CollegeProvider.Instance.GetCollegeTopHirer(
                        Convert.ToInt32(collegeBranchCourseId));

                if (topHireer.Count > 0)
                {
                    divTopHirer.Visible = true;
                    rptTopHirer.Visible = true;

                    pagerHighLightsCollege.BindDataWithPaging(rptTopHirer, Common.ConvertToDataTable(topHireer));
                }else
                {
                    divTopHirer.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindCollegeHighLights in CollegeTopHirer.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}