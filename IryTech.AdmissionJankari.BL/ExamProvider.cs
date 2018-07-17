

using System.Collections.Generic;
using IryTech.AdmissionJankari.BO;
using IryTech.BaseClassLibraray;

namespace IryTech.AdmissionJankari.BL
{
  public abstract  class ExamProvider:IExam
  {
      public static ExamProvider Instance
      {
          get { return new Exam(); }
          
      }
      public abstract int InsertExamDetails(ExamProperty objExamProperty ,int createdBy,out string errmsg );
      public abstract int UpdateExamDetails(ExamProperty objExamProperty ,int modifiedBy,out string errmsg);
      public abstract List<ExamProperty> GetAllExamList();
      public abstract List<ExamProperty> GetExamListById(int examId);
      public abstract List<ExamProperty> GetExamListByCourseId(int courseId);
      public abstract List<ExamProperty> GetExamListByName(string examName);
      public abstract int InsertExamFormDetails(ExamFormProperty objExamFormProperty, int createdBy, out string errmsg);
      public abstract int UpdateExamFormDetails(ExamFormProperty objExamFormProperty, int modifiedBy, out string errmsg);
      public abstract List<ExamFormProperty> GetAllExamFormDetails();
      public abstract List<ExamFormProperty> GetExamFormDeatilsById(int examFormId);
      public abstract List<ExamFormProperty> GetExamFormDetailsByExamId(int examId);
      public abstract List<ExamFormProperty> GetExamFormDetailByCourseId(int courseId);
      public abstract List<ExamFormProperty> GetExamFormDetailsByExamSubject(string subjectName);
      public abstract List<ExamFormProperty> GetExamFormDetailsByExamSubjectCourseId(int courseId, string subjectName);
      public abstract List<ExamProperty> GetMostViewdExamByCourse(int courseId);
      public abstract List<ExamProperty> GetUpComingExamList(int courseId, System.DateTime upComingDate);
     

     


     

     
  }
}
