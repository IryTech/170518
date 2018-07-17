using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Data;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcReportDonationStory : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomCommentPaging.PageSize = 5;
            ucCustomCommentPaging.ButtonsCount = ApplicationSettings.Instance.NewsArticlePageCount;
            ucCustomCommentPaging.PagerPageIndexChanged += PagerPageIndexChanged;
        }
        private void PagerPageIndexChanged(object sender, EventArgs e)
        {
            Common objCommon= new Common();
            BindUserStory(courseId: objCommon.CourseId);
        }

        // Method to Bind The User Story On the Report Donation 
        public void BindUserStory(int courseId = 0, int collegeBranchCourseId=0)
        {
            Common objCommon = new Common();
            DataTable DT= new DataTable();
            try
            {
                
                var data = objCommon.GetReportDonationCollegeList(courseId, collegeBranchCourseId).AsEnumerable().Where(colleges=>colleges.Field<bool>("AjReportStatus")==true).CopyToDataTable();
                if (data != null)
                {
                    if (data.Rows.Count > 0)
                    {
                        rptComment.DataSource = data;
                        rptComment.DataBind();
                        ucCustomCommentPaging.BindDataWithPaging(rptComment, data);
                    }
                    else
                    {
                        divUserDonationStory.Visible = false;
                    }
                }
                else
                {
                    divUserDonationStory.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                divUserDonationStory.Visible = false;
              
            }
        }
    }
}