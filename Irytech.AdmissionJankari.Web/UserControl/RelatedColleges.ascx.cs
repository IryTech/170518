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
    public partial class RelatedColleges : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindRelatedCollege();
        }


        public int CollegeBranchCourseId
        {
            get;
            set;
        }

        // Method to Bind The Related College
        protected void BindRelatedCollege()
        {
            var data = CollegeProvider.Instance.GetCollegeCourseListByBranchCourseId(CollegeBranchCourseId,"rel").Where(result=>result.CollegeBranchCourseId!=CollegeBranchCourseId).ToList();
            try
            {
                if (data.Count > 0)
                {
                    rptRealtedCollege.DataSource = data;
                    rptRealtedCollege.DataBind();
                    CollegeHeader.Visible = true;
                }
                else
                {
                    CollegeHeader.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindRelatedCollege in College.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        public string CollegeName
        {
            set { lblCollegeName.Text = value; }

        }
    }
}