using System.Collections.Generic;
using IryTech.AdmissionJankari.BO;
using System;

namespace IryTech.BaseClassLibraray
{
   public  interface IExam
   {
       #region Defined Method for the insert data into Exam Master Details 
           int InsertExamDetails( ExamProperty objExamProperty ,int createdBy,out string errmsg);
           int UpdateExamDetails(ExamProperty objExamProperty, int modified,out string errmsg);
           List<ExamProperty> GetAllExamList();
           List<ExamProperty> GetExamListById(int examId);
           List<ExamProperty> GetExamListByCourseId(int courseId );
           List<ExamProperty> GetExamListByName(string examName);
           List<ExamProperty> GetMostViewdExamByCourse(int courseId);
           List<ExamProperty> GetUpComingExamList(int courseId, DateTime upComingDate); 
       #endregion

       #region Defined Method for the insert data into Exam Form for the AjExamFormMaster

       int InsertExamFormDetails(ExamFormProperty objExamFormProperty,int createdBy,out string errmsg);
       int UpdateExamFormDetails(ExamFormProperty objExamFormProperty, int modifiedBy, out string errmsg);

       List<ExamFormProperty> GetAllExamFormDetails();
       List<ExamFormProperty> GetExamFormDeatilsById(int examFormId);
       List<ExamFormProperty> GetExamFormDetailsByExamId(int examId);
       List<ExamFormProperty> GetExamFormDetailByCourseId(int courseId);
       List<ExamFormProperty> GetExamFormDetailsByExamSubject(string subjectName);
       List<ExamFormProperty> GetExamFormDetailsByExamSubjectCourseId(int courseId, string subjectName);
     



       #endregion
   }
}
