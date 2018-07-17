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
    public partial class UcExamRealtedCourse : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Pager.PageSize = ApplicationSettings.Instance.MostViewExamPageSize;
            Pager.ButtonsCount = ApplicationSettings.Instance.MostViewExamPageCount;
            Pager.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindExamList();
        }



        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
                      
            try
            {
                Pager.BindDataWithPaging(rptExamRealtedCourse, Common.ConvertToDataTable(GetExamList()));
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in UcMostViewdExam.ascx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }



        // Method to Get The Exam Realted to Course
        private List<ExamProperty> GetExamList()
        {
            Common objCommon = new Common();
            return ExamProvider.Instance.GetExamListByCourseId(objCommon.CourseId).Where(result => result.ExamName != ExamNames && result.ExamStatus==true).ToList();
        }

        private void BindExamList()
        {
            try
            {
                Pager.BindDataWithPaging(rptExamRealtedCourse, Common.ConvertToDataTable(GetExamList()));
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindExamList in UcExamRealtedCourse.ascx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        public string CourseName
        {
            set{lblCourseName.Text=value;}
        }
        public string ExamNames
        {
            get;
            set;
        }
    }
}