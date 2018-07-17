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
    public partial class UcCourseListForDirectAdmission : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
                BindCourseDetails();
        }

        // Method To Bind The Course Details 
        protected void BindCourseDetails()
        {
            var data = CourseProvider.Instance.GetAllCourseList();
            try
            {
                data = data.Where(course => course.CourseStatus == true).ToList();
                if (data.Count > 0)
                {
                    ddlCourseList.DataSource = data;
                    ddlCourseList.DataBind();
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCourseDetails in UcCourseListForDirectAdmission.axcs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        
    }
}