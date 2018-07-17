using System.Collections.Generic;

namespace IryTech.BaseClassLibraray
{
   public interface ICourse
   {
       int InsertCourseCategory(string courseCategoryName, out string errmsg,int createdBy,bool courseCategoryStatus=false);
       int UpdateCourseCategory(int courseCategoryId, string courseCategoryName, out string errmsg,int modifiedBy,
                                bool courseCategoryStatus = false);
       List<CourseCategoryProperty> GetAllCourseCategoryList();
       List<CourseCategoryProperty> GetCourseCategoryById(int courseCategoryId);



       int InsertCourseEligibilty(string courseEligibiltyName,int createdBy,out string errmsg,bool courseEligibiltyStatus=false);
       int UpdateCourseEligibilty(int courseEligibiltyId, string courseEligibiltyName, int modifiedBy, out string errmsg, bool courseEligibiltyStatus = false);
       List<CourseEligibiltyProperty> GetAllCourseEligibiltyList();
       List<CourseEligibiltyProperty> GetCourseEligibiltyById(int courseeEligibiltyId);


       int InsertCourseMasterDetails(string courseName,string courseUrl,string courseTitle,string courseMetaTag,string courseMetaDesc, string courseDesc, string courseshortName,
                                     string coursePopularName, int courseCategoryId, int courseEligibiltyId,
                                     int createdBy, out string errMsg, string CorseImage, string HelpLineNo, bool courseStatus = false, bool IsBookSeatVisible = true);
       int UpdateCourseMasterDetails(int courseId,string courseName,string courseUrl,string courseTitle,string courseMetaTag,string courseMetaDesc, string courseDesc, string courseshortName,
                                   string coursePopularName, int courseCategoryId, int courseEligibiltyId,
                                   int createdBy, out string errMsg, string CourseImage, string HelpLineNo, bool courseStatus = false, bool IsBookSeatVisible = true);

       List<CourseMasterProperty> GetAllCourseList();
       List<CourseMasterProperty> GetCourseById(int courseId);
       List<CourseMasterProperty> GetCourseByCategory(int courseCategoryId);
       List<CourseMasterProperty> GetCourseByEligibity(int courseEligibityId);



       List<CourseMasterProperty> GetCourseSourceHome();




	   int InsertCoursePaymentMasterDetails( int CourseId, string OnlinePaymentAmount, string OfflinePaymentAmount, out string errMsg, int createdBy );
	   int UpdateCoursePaymentMasterDetails( int PaymentCourseId, int CourseId, string OnlinePaymentAmount, string OfflinePaymentAmount, out string errMsg, int createdBy );
	   List<CoursePaymentMasterProperty> GetCoursePaymentMasterList();
	   List<CoursePaymentMasterProperty> GetCoursePaymentById( int PaymentCourseId );
	   
	   
	  
   }
}
