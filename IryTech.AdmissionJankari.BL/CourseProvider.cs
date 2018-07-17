
using System.Collections.Generic;
using IryTech.BaseClassLibraray;

namespace IryTech.AdmissionJankari.BL
{
	public abstract class CourseProvider : ICourse
	{


		public static CourseProvider Instance
		{
			get { return new Course(); }
		}
		public abstract int InsertCourseCategory( string courseCategoryName, out string errmsg, int createdBy,
												 bool courseCategoryStatus = false );

		public abstract int UpdateCourseCategory( int courseCategoryId, string courseCategoryName, out string errmsg,
												 int modifiedBy, bool courseCategoryStatus = false );


		public abstract List<CourseCategoryProperty> GetAllCourseCategoryList();

		public abstract List<CourseCategoryProperty> GetCourseCategoryById( int courseCategoryId );


		public abstract int InsertCourseEligibilty( string courseEligibiltyName, int createdBy, out string errmsg,
												   bool courseEligibiltyStatus = false );


		public abstract int UpdateCourseEligibilty( int courseEligibiltyId, string courseEligibiltyName, int modifiedBy,
												   out string errmsg, bool courseEligibiltyStatus = false );


		public abstract List<CourseEligibiltyProperty> GetAllCourseEligibiltyList();


		public abstract List<CourseEligibiltyProperty> GetCourseEligibiltyById( int courseeEligibiltyId );

		public abstract int InsertCourseMasterDetails( string courseName, string courseUrl, string courseTitle, string courseMetaTag, string courseMetaDesc, string courseDesc, string courseshortName,
													  string coursePopularName, int courseCategoryId,
													  int courseEligibiltyId, int createdBy, out string errMsg, string CourseImage,
                                                      string HelpLineNo, bool courseStatus = false, bool IsBookSeatVisible = true);


		public abstract int UpdateCourseMasterDetails( int courseId, string courseName, string courseUrl, string courseTitle, string courseMetaTag, string courseMetaDesc, string courseDesc,
													  string courseshortName, string coursePopularName,
													  int courseCategoryId, int courseEligibiltyId, int createdBy,
                                                      out string errMsg, string CourseImage, string HelpLineNo, bool courseStatus = false, bool IsBookSeatVisible = true);

		public abstract List<CourseMasterProperty> GetAllCourseList();


		public abstract List<CourseMasterProperty> GetCourseById( int courseId );


		public abstract List<CourseMasterProperty> GetCourseByCategory( int courseCategoryId );


		public abstract List<CourseMasterProperty> GetCourseByEligibity( int courseEligibityId );

		public abstract List<CourseMasterProperty> GetCourseSourceHome();



		public abstract int InsertCoursePaymentMasterDetails( int CourseId, string OnlinePaymentAmount, string OfflinePaymentAmount, out string errMsg, int createdBy );
		public abstract int UpdateCoursePaymentMasterDetails( int PaymentCourseId, int CourseId, string OnlinePaymentAmount, string OfflinePaymentAmount, out string errMsg, int createdBy );
		public abstract List<CoursePaymentMasterProperty> GetCoursePaymentMasterList();
		public abstract List<CoursePaymentMasterProperty> GetCoursePaymentById( int PaymentCourseId );



	}
}
