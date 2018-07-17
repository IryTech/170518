using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components.Web.Controls;
using IryTech.AdmissionJankari.BO;
using System.Web.UI.HtmlControls;
using System.Globalization;
namespace IryTech.AdmissionJankari20.Web.Exam
{
    public partial class ExamDetails : PageBase
    {
        Common _ObjCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ExamId"] != null)
            {
                UcComment.CommentType = Convert.ToString(CommentType.Exam);
                UcComment.CommentTypeId = Request.QueryString["ExamId"];
            }
            if (IsPostBack) return;
            if (Request.QueryString["ExamId"] != null)
            {
                BindExamDetails(Request.QueryString["ExamId"]);
                BindExamFormDetails(Request.QueryString["ExamId"]);
                _ObjCommon = new Common();
                int i = _ObjCommon.InsertExamPageClick(Convert.ToInt32(Request.QueryString["ExamId"]));
               
            }
           
            

        }
        private void BindExamDetails(string examId)
        {
            var data = ExamProvider.Instance.GetExamListById(Convert.ToInt32(examId));
            if (data.Count <= 0) return;
            BindPageTitleAndKeyWords(data);
            rptViewExam.DataSource = data;
            rptViewExam.DataBind();
            var query = data.Select(result => new
            {
                examLogo = result.ExamLogo,
                examFullName = result.ExamFullName,
                examName=result.ExamName,
                courseName = result.CourseName,
                courseId = result.CourseId,
                HelpLineNumber=result.HelpLineNumber
               
            }).First();
            ucExamQuery.ExamId = examId;
            ucExamQuery.ExamName = query.examName;
            ucRealatedCourse.CourseName = query.courseName;
            ucRealatedCourse.ExamNames = query.examName;
            ucCollegeRealtedRealatedExam.CourseId = query.courseId;
            ucCollegeRealtedRealatedExam.ExamId = Convert.ToInt32(examId);
            ucCollegeRealtedRealatedExam.ExamName = query.examName;
                imgExam.ImageUrl = String.Format("{0}{1}", "/image.axd?Exam=", string.IsNullOrEmpty(query.examLogo.ToString()) ? "NoImage.jpg" : query.examLogo);
                imgExam.AlternateText = query.examFullName;
                            
            if (!string.IsNullOrEmpty(query.examFullName))
            {
                lblHeader.Text = query.examFullName;
                txtHelpLineNo.Text = Convert.ToString(query.HelpLineNumber);

            }
        }
        private void BindExamFormDetails(string examId)
        {
            var data = ExamProvider.Instance.GetExamFormDetailsByExamId(Convert.ToInt16(examId)).Where(a=>Convert.ToInt16(a.ExamFormYear)>=DateTime.Now.Year && a.ExamFormStatus==true).ToList();

            if (data.Count > 0)
            {

                lblExamFormHeader.Visible = true;
                lblExamFormHeader.Text = data.FirstOrDefault().ExamName + " " + "Exam Form Details";
                rptExamForm.DataSource = data;
                rptExamForm.DataBind();
                            
            }
            else
            {
                lblExamFormHeader.Visible = false;
            
                ExamForm.Visible = false;
            }
        }
        private void BindPageTitleAndKeyWords(List<ExamProperty> objExam)
        {

            if (objExam.Count > 0)
            {
                Page.Title = "";
                 Page.Title = objExam.First().ExamName + "-" +System.DateTime.Now.Year +", Eligibility, Syllabus, Exam Pattern, Exam Analysis and Cut off's, Tips for preparation," +
                            "Marking Scheme,"+
                            "Negative Marking, Mock Test, Old Paper-Admissionjankari";  
                           
                var metadesc = new HtmlMeta();
                metadesc.Attributes.Clear();
                metadesc.Name = "description";

                metadesc.Content = objExam.First().ExamName+ " " +System.DateTime.Now.Year+"," + objExam.First().ExamFullName + "," +
                                  objExam.First().ExamName+" "+"Exam Pattern"+","+   objExam.First().ExamName+ " " +"Mock Test"+
                                  ","+objExam.First().ExamName+ " "+"Syllabus Exam Details,"+ objExam.First().ExamName+" " +"Exam Center"+
                                  objExam.First().ExamName+" " +"Exam Center"+"," + objExam.First().ExamName+ " " +"Total Question, Question paper format, Question types, Multiple Choice, Multiple Response, Integer Type,  Books, entrance, date, notification, subjects, topics, mock test, old paper, exam analyser, Solved Question Papers, Free Study Tips, Quotes, Admission Notification " + DateTime.Now.Year +", " +DateTime.Now.Year+ ", India, Education, Admission, Result, Guide, Question bank, test preparation, Forms, Application form, recent notification, Free, Registration, Register Free, Free Trial, Sample Papers, Download"+
                                  "-Admissionjankari";

                Page.Header.Controls.Add(metadesc);

                var metaKeywords = new HtmlMeta
                                       {
                                           Name = "keywords",
                                           Content =
                                               objExam.First().ExamName + " "+System.DateTime.Now.Year+"," + objExam.First().ExamFullName + "," +
                                  objExam.First().ExamName+" "+"Exam Pattern"+","+   objExam.First().ExamName+ " " +"Mock Test"+
                                  ","+objExam.First().ExamName+ " "+"Syllabus Exam Details,"+ objExam.First().ExamName+" " +"Exam Center"+
                                  objExam.First().ExamName+" " +"Exam Center"+"," + objExam.First().ExamName+ " " +"Total Question, Question paper format, Question types, Multiple Choice, Multiple Response, Integer Type,  Books, entrance, date, notification, subjects, topics, mock test, old paper, exam analyser, Solved Question Papers, Free Study Tips, Quotes, Admission Notification " + DateTime.Now.Year +", " +DateTime.Now.Year+ ", India, Education, Admission, Result, Guide, Question bank, test preparation, Forms, Application form, recent notification, Free, Registration, Register Free, Free Trial, Sample Papers, Download"
                                  +"-Admissionjankari"
                                       };


                Page.Header.Controls.Add(metaKeywords);

            }
        }
        public string GetMonth(object examStartDate)
        {
            string examMonth = "";
            
            try
            {
                if(!string.IsNullOrEmpty(Convert.ToString(examStartDate)))
                {
                    var examDate = Convert.ToString(examStartDate).Split(',');
                    examMonth = new DateTime(Common.GetDateFromString(examDate[0]).Year, Common.GetDateFromString(examDate[0]).Month, Common.GetDateFromString(examDate[0]).Day).ToString("MMMM", CultureInfo.InvariantCulture); ;
                }
                else
                {
                    examMonth= new DateTime(DateTime.Now.Year, DateTime.Now.Month,DateTime.Now.Day)
                            .ToString("MMM", CultureInfo.InvariantCulture);

                }

            }
            catch (Exception ex)
            {

            }
            return examMonth;
        }

    }
}