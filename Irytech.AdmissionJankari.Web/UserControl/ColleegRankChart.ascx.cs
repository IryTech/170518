using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class ColleegRankChart : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
                BindYear();
           
        }
        private void BindCollegeRankDetails(int year)
        {

            try
            {
                var rank = CollegeProvider.Instance.BindCollegeRank(CollegeBranchCourseId, year);
                if (rank.Count > 0)
                {
                    CollegeRank.Visible = true;
                    rankChart.DataSource = rank;
                    rankChart.ChartAreas["ChartArea2"].AxisX.Title = "RankSourceName";
                    // here i am giving the title of the y-axis
                    rankChart.ChartAreas["ChartArea2"].AxisY.Title = "CollegeRankYear";
                    rankChart.Series["rankSeries"].XValueMember = "RankSourceName";
                    // here i am binding the y-axisvalue with the chart control
                    rankChart.Series["rankSeries"].YValueMembers = "CollegeOverAllRank";
                    rankChart.DataBind();

                }
                else
                {
                    CollegeRank.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindYear in ColleegRankChart.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }
        private void BindYear()
        {
            try
            {
                var year = CollegeProvider.Instance.BindCollegeRankYear(CollegeBranchCourseId);
                var distinctResult = from c in year
                                     group c by c.CollegeRankYear into uniqueIds
                                     select uniqueIds.FirstOrDefault();
                if (distinctResult.Count() > 0)
                {
                    CollegeRank.Visible = true;
                    ddlYear.DataSource = distinctResult;
                    ddlYear.DataTextField = "CollegeRankYear";
                    ddlYear.DataValueField = "CollegeRankYear";
                    ddlYear.DataBind();
                    BindCollegeRankDetails(Convert.ToInt16(ddlYear.SelectedValue.ToString()));
                }
                else
                {
                    CollegeRank.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindYear in ColleegRankChart.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCollegeRankDetails(Convert.ToInt16(ddlYear.SelectedValue));
        }
        public int CollegeBranchCourseId
        {
            get{ return Convert.ToInt32(ViewState["CollegeCourseId"]); }
            set { ViewState["CollegeCourseId"] = value; }
        }
    }
}