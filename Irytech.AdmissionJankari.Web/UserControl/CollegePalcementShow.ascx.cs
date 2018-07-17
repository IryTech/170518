using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using AjaxControlToolkit;
namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CollegePalcementShow : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindYear();
                
        }
        private void BindYear()
        {
            try
            {
                var year = new Common().GetCollegePlacementYears(CollegeBranchCourseId);

                if (year.Rows.Count > 0)
                {
                    divPlcement.Visible = true;
                 
                    ddlPlacementYear.DataSource = year;
                    ddlPlacementYear.DataTextField = "AjCollegeBranchPlacementYear";
                    ddlPlacementYear.DataValueField = "AjCollegeBranchPlacementYear";
                    ddlPlacementYear.DataBind();
                    ListItem match = ddlPlacementYear.Items.FindByText(DateTime.Now.Year.ToString());
                    if (match != null)
                        match.Selected = true;
                    BindCollegePlacementDetails(Convert.ToInt16(ddlPlacementYear.SelectedValue.ToString()));
                }
                else
                {
                    divPlcement.Visible = false;
                   
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindYear in CollegePlacementShow.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }
        private void BindCollegePlacementDetails(int year)
        {

            try
            {
                var rank = CollegeProvider.Instance.GetCollegeTopHirer(CollegeBranchCourseId);
                rank = rank.Where(result => result.CollegeBranchCoursePlacementYear == Convert.ToString(year) && result.CollegeBranchCoursePlacementStatus==true).ToList();
                if (rank.Count > 0)
                {
                    CollegePlacementChart(rank);
                    rptPalcement.Visible = true;
                    rptPalcement.DataSource = rank;
                    rptPalcement.DataBind();
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCollegePlacementDetails in CollegePlacementShow.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }

        private void CollegePlacementChart(List<CollegeBranchCoursePlacementProperty> rank)
        {
            if (rank.Count > 0)
            {
               
                rankChart.DataSource = rank;
                rankChart.ChartAreas["ChartArea2"].AxisX.Title = "Company Name";
                // here i am giving the title of the y-axis
                rankChart.ChartAreas["ChartArea2"].AxisY.Title = "Student Hired";
                rankChart.Series["rankSeries"].XValueMember = "CollegeBranchCoursePlacementCompanyName";
                // here i am binding the y-axisvalue with the chart control
                rankChart.Series["rankSeries"].YValueMembers = "CollegeBranchCoursePlacementNoOfStudentHired";
                rankChart.DataBind();

            }
        }
        protected void ddlPlacementYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCollegePlacementDetails(Convert.ToInt16(ddlPlacementYear.SelectedValue));
        }
        public int CollegeBranchCourseId
        {
            get { return Convert.ToInt32(ViewState["CollegeCourseId"]); }
            set { ViewState["CollegeCourseId"] = value; }
        }
    }
}