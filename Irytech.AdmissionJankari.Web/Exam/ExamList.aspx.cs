using System;
using System.Globalization;
using System.Linq;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components.Web.Controls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IryTech.AdmissionJankari20.Web.Exam
{
    public partial class ExamList : PageBase
    {
        Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            Pager.PageSize = ApplicationSettings.Instance.ExamPageSize;
            Pager.ButtonsCount = ApplicationSettings.Instance.ExamPageCount;
            Pager.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            if (Request.QueryString["CourseId"] != null)
            {
                _objCommon = new Common();
                _objCommon.CourseId = Convert.ToInt16(Request.QueryString["CourseId"]);
              
            }
            BindCourse();
           
            if (Request.QueryString["Exam"] != null)
                  BindExam();
            else
                BindExamList();
                             
           
           // GetMyMonthList();
        }
        // to show page title ,keyword and description
         private void BindPageTitleAndKeyWords()
         {
             var courseSubject = "";
             var courseData =
                 CourseProvider.Instance.GetAllCourseList().Where(result => result.CourseStatus == true).ToList();
            var courseSubjectLists = (from test in courseData select test.CourseName).ToArray();
            courseSubject =String.Join("|", courseSubjectLists);
            var objPage=new Common().GetPageTitleKeyWordAndDecsription("Exam");
        
            try
            {
                if (objPage.Rows.Count > 0)
                {

                    Page.Title = "";
                   // Page.Title = Convert.ToString(objPage.Rows[0]["AjPageTitle"].ToString());
                    Page.Title = new Common().CourseName + " Entrance Exam " + DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) + " " + (DateTime.Now.Year+1)+"" +"| " + new Common().CourseName + " Exams Results -  Admission Jankari";
                    var metadesc = new HtmlMeta();
                    metadesc.Attributes.Clear();
                    metadesc.Name = "description";
                    metadesc.Content = courseSubject + " Entrance Exam " + DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) + " " + (DateTime.Now.Year+1)+"" +", View top " +
                                       new Common().CourseName +
                                       Convert.ToString(objPage.Rows[0]["AjPageDescription"].ToString());
                   // metadesc.Content = ;

                    Page.Header.Controls.Add(metadesc);

                    var metaKeywords = new HtmlMeta
                                           {
                                               Name = "keywords",
                                               Content = new Common().CourseName + "Entrance  Exam," + " Top" + new Common().CourseName + " Exam," + courseSubject + " Entrance Exams, " + new Common().CourseName + " exams in India"
                                                   //Convert.ToString(objPage.Rows[0]["AjPageKeyword"].ToString())
                                           };

                    Page.Header.Controls.Add(metaKeywords);
                }

            }
            catch (Exception Ex)
            {
                string err = Ex.Message;
                if (Ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + Ex.InnerException.Message;
                }
                string addInfo = "Error While fetching BindPageTitleAndKeyWords in CollegeSearch.aspx :: -> ";
                ClsExceptionPublisher objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        // Method to Binb the Exam List
        private void BindExamList(int courseId=0)
        {
            _objCommon = new Common();
            if(courseId!=0)
                _objCommon.CourseId = courseId;
            var data = ExamProvider.Instance.GetExamListByCourseId(_objCommon.CourseId).Where(d=>d.ExamStatus==true).ToList();
            try
            {
                if (data.Count > 0)
                {
                    rptEntranceExam.Visible = true;
                    Pager.BindDataWithPaging(rptEntranceExam, Common.ConvertToDataTable(data));
                }
                else
                {
                    rptEntranceExam.Visible = false;
                    Pager.Visible = false;

                }
                BindPageTitleAndKeyWords();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in ExamList.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
           
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            var data = ExamProvider.Instance.GetExamListByCourseId(_objCommon.CourseId);
            data = data.Where(exam => exam.ExamStatus == true).ToList();
            if (data.Count <= 0) return;
            try
            {
                rptEntranceExam.Visible = true;
                Pager.BindDataWithPaging(rptEntranceExam, Common.ConvertToDataTable(data));
                BindPageTitleAndKeyWords();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in ExamList.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindExam()
        {
            _objCommon = new Common();
            var data = ExamProvider.Instance.GetExamListByName(Request.QueryString["Exam"].ToString());
            try
            {
                if (data.Count > 0)
                {
                    rptEntranceExam.Visible = true;
                    rptEntranceExam.DataSource = data;
                    rptEntranceExam.DataBind();
                }
                else
                {
                    rptEntranceExam.Visible = false;
                  

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindExam in ExamList.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }


        protected void BindCourse()
        {
            _objCommon = new Common();
            try
            {
                var data = CourseProvider.Instance.GetAllCourseList();
                data = data.Where(course => course.CourseStatus == true).ToList();
                if (data.Count > 0)
                {
                    ddlCourse.DataSource = data;
                    ddlCourse.DataTextField = "CourseName";
                    ddlCourse.DataValueField = "CourseId";
                    ddlCourse.DataBind();
                    ddlCourse.SelectedValue = Convert.ToString(_objCommon.CourseId);
                    lblCourse.InnerText = ddlCourse.SelectedItem.ToString();
                    _objCommon.CourseName = Utils.RemoveIllegealFromCourse(lblCourse.InnerText);
                    //lblCourse1.InnerText = lblCourse.InnerText;
                    //lblCourse2.InnerText = lblCourse.InnerText;
                    //lblCourse3.InnerText = lblCourse.InnerText;
                  
                }
                else
                {
                    ddlCourse.Items.Insert(0, new ListItem("Select Course", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCourse in ExamList.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void ddlCourseSelectedIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            _objCommon.CourseId = Convert.ToInt16(ddlCourse.SelectedValue);
            _objCommon.CourseName = Utils.RemoveIllegealFromCourse(ddlCourse.SelectedItem.ToString());
             BindExamList();
            //lblCourse.InnerText = ddlCourse.SelectedItem.ToString();
            //lblCourse1.InnerText = lblCourse.InnerText;
            //lblCourse2.InnerText = lblCourse.InnerText;
            //lblCourse3.InnerText = lblCourse.InnerText;
            ucMostviewdExam.BindMostViewdExam();

        }
        public void GetMyMonthList()
        {
            


        }

        protected void ddlmonthSelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}