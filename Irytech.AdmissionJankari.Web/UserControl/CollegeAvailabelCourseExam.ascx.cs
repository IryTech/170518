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
    public partial class CollegeAvailabelCourseExam : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         

        }

        public List<CollegeBranchCourseExamProperty> CourseExam
        {
           
            set
            {
                try
                {
                    if (value.Count > 0)
                    {
                        divExamResult.Visible = true;
                        lblExmResult.Visible = false;
                        rptEntranceExam.DataSource = value;
                        rptEntranceExam.DataBind();
                    }
                    else
                    {
                        divExamResult.Visible = false;
                        lblExmResult.Visible = true;
                        lblExmResult.CssClass = "info";
                        lblExmResult.Text = new Common().GetErrorMessage("noRecords");
                        
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
                        "Error in Executing  Pager_PageIndexChanged in CollegeAvailAbleCourseExam.ascx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
                
            }
        }
       
    }
}