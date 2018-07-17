using System;
namespace IryTech.AdmissionJankari.BO
{
   public class StudentHighSchoolInfo
    {
       public string SchoolName { get; set; }
       public int SchoolBoard { get; set; }
       public string SchoolYOP { get; set; }
       public string SchoolCGPA { get; set; }

    }

   public class StudentIntermidateInfo
   {
       public string CollegeName { get; set; }
       public int CollegeBoard { get; set; }
       public string CollegeYOP { get; set; }
       public string CollegePer { get; set; }
       public string CollegeCGPA { get; set; }
       public string CollegeCourseCombination { get; set; }
       public string CollegeCourseCombinationPer { get; set; }
   }

   public class StudentDicInfo
   {
       public string DicCollegeName { get; set; }
       public string DicCourseName { get; set; }
       public string DicPer { get; set; }
       public string DicYOP { get; set; }
       public string DicCGPA { get; set; }
   }

   public class StudentGrdInfo
   {
       public string GrdCollegeName { get; set; }
       public string GrdSpecialization { get; set; }
       public string GrdYOP { get; set; }
       public string GrdPer { get; set; }
       public string GrdCGPA { get; set; }
   }
   public class AccountPaymentMasterProp
   {
       public int UserTransactionId { get; set; }
       public int CollegeBranchCourseId { get; set; }
        public string CollegeName { get; set; }
       public int UserLoginId { get; set; }
       public string UserTransactionMode { get; set; }
       public int UserBankId { get; set; }
       public int TransactionId { get; set; }
       public string StudentFormNumber { get; set; }
       public bool StudentPaymentStatus { get; set; }
       public string AdmissionPriority { get; set; }
       public int AjStudentInterestedCollegeCounsellingId { get; set; }
       public string PaymentAmount { get; set; }
    
       
   }
   public class RefundRequestProperty
   {
       public int RefundId { get; set; }
      
       public string EmailId { get; set; }
       public string FormNo { get; set; }
       public bool RefundStatus { get; set; }
      
   }
   public class BookSeat
   {
       public int? BookSeatId { get; set; }
       public CollegeBranchProperty CollegeBasicInfo { get; set; }
       public CollegeBranchCourseProperty CollegeBranchCourse { get; set; }
       public CourseMasterProperty CourseMaster { get; set; }
       public CityProperty CityMaster { get; set; }
       public CourseEligibiltyProperty CourseEligibity { get; set; }
       public string BookSeatAmount { get; set; }
       public string Eligibity10 { get; set; }
       public string Eligibity12 { get; set; }
       public string Eligibity15 { get; set; }
       public string Instruction { get; set; }
       public bool BookSeatStatus { get; set; }
       public string  BookSeatStartDate { get; set; }
       public string BookSeatEndDate { get; set; }


   }


}
