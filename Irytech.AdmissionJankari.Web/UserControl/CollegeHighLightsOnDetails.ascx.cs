using System;
using System.Linq;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CollegeHighLightsOnDetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucHighLightPager.PageSize = 2;
            ucHighLightPager.ButtonsCount = ApplicationSettings.Instance.MostViewdCollegePageCount;
            ucHighLightPager.PagerPageIndexChanged += HighLightPaging;
            if (IsPostBack) return;
            BindHighLights();
        }
        protected void HighLightPaging(object sender, EventArgs e)
        {
            try
            {
                var highLights = CollegeProvider.Instance.GetCollegeCourseHighLightsByCollegeBranchCourseId(CollegeBranchCourseId);
                highLights =
                    highLights.Where(highlights => highlights.CollegeBranchCourseHighlightStatus == true)
                              .OrderByDescending(news => news.CollegeBranchCourseHighlightId)
                              .Take(5)
                              .ToList();
                if (highLights.Count > 0)
                {
                    ucHighLightPager.Visible = true;
                    divHighLights.Visible = true;
                    ucHighLightPager.BindDataWithPaging(rptHighLights, Common.ConvertToDataTable(highLights));
                }
                else
                {
                    ucHighLightPager.Visible = false;
                    divHighLights.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindHighLights in CollegeHighLightsOnDetails.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }


        }
        private void BindHighLights()
        {
            try {
                var highLights = CollegeProvider.Instance.GetCollegeCourseHighLightsByCollegeBranchCourseId(CollegeBranchCourseId);
                highLights =
                    highLights.Where(highlights => highlights.CollegeBranchCourseHighlightStatus == true)
                              .OrderByDescending(news => news.CollegeBranchCourseHighlightId)
                              .Take(5)
                              .ToList();
                if (highLights.Count > 0)
                {
                    ucHighLightPager.Visible = true;
                    divHighLights.Visible = true;
                    ucHighLightPager.BindDataWithPaging(rptHighLights, Common.ConvertToDataTable(highLights));
                }
                else
                {
                    ucHighLightPager.Visible = false;
                    divHighLights.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindHighLights in CollegeHighLightsOnDetails.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }
        public int CollegeBranchCourseId
        {
            get { return Convert.ToInt32(ViewState["CollegeCourseId"]); }
            set { ViewState["CollegeCourseId"] = value; }
        }
    }
}