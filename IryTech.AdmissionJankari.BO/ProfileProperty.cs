using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IryTech.AdmissionJankari.BO
{
    [Serializable()]
    public class ExamAppearedProperty
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public int AjStudentExamAppId { get; set; }
        public string AjExamAppRank { get; set; }
        

    }
    [Serializable()]
    public class CollegePrefered
    {
        public int UserId { get; set; }
        public int CollegeBranchCourseId { get; set; }
        public int CollegeBranchId { get; set; }
        public int AjStudentCollegePreferenceId { get; set; }
        public string CollegeName { get; set; }
        public string CollegePopularName { get; set; }
        public string AjCollegeBranchAddress { get; set; }
        public string AjCollegeBranchWebSite { get; set; }


    }
    [Serializable()]
    public class CoursePreffered
    {
        public int UserId { get; set; }
        public int CollegeBranchCourseId { get; set; }
        public int CourseId { get; set; }
        public int AjStudentCoursePreferenceId { get; set; }
        public string CourseName { get; set; }
        public string CoursePopularName { get; set; }
        public string CourseShortName { get; set; }
 
    }
    [Serializable()]
    public class CourseStreamPreffered
    {
        public int UserId { get; set; }
        public int CourseStreamId { get; set; }
        public int StudentStreamPreferenceId { get; set; }
        public string CourseStreamName { get; set; }
        public string CourseStreamCoreCompanies { get; set; }
        public string CourseStreamRelatedIndustry { get; set; }

    }
    [Serializable()]
    public class CityPrefferedProperty
    {
        public int CityPreferId { get; set; }
        public int UserId { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }

    }
    [Serializable()]
    public class StudentQueryProperty
    {
        public int StudentQueryId { get; set; }
        public int UserId { get; set; }
        
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Query { get; set; }
            public string QueryAnswer { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public bool ReplyStatatus { get; set; }
      

    }
    [Serializable()]
    public class StudentHighSchoolProperty
    {
        public int HigherSecondaryScoreCardId { get; set; }
        public int UserId { get; set; }
        public int BoardId { get; set; }
        public string HigherSecondarySchoolName { get; set; }
        public string HigherSecondarySchoolScoreCGPA { get; set; }
        public string HigherSecondarySchoolPassingYear { get; set; }
        public string BoardName { get; set; }
        public int StudyModeId { get; set; }
        public string StudyMode { get; set; }
       

    }
    [Serializable()]
    public class StudentInterSchoolProperty
    {
        public int SeniorSecondaryScoreCardId { get; set; }
        public int UserId { get; set; }
        public int BoardId { get; set; }
        public string SeniorSecondarySchoolName { get; set; }
        public string SeniorSecondarySchoolScoreCgpa { get; set; }
        public string SeniorSecondarySchoolPassingYear { get; set; }
        public string BoardName { get; set; }
        public string SeniorSecondarySchoolSpecialization { get; set; }
        public string SeniorSecondarySchoolSubjectCombination { get; set; }
        public string SeniorSecondarySchoolSubjectMarks { get; set; }
        public string SeniorSecondarySchoolSubjectPercent { get; set; }
        public int StudyModeId { get; set; }
        public string StudyMode { get; set; }

    }
    [Serializable()]
    public class StudentDiplomaProperty
    {
        public int StudentDicScoreCardId { get; set; }
        public int UserId { get; set; }
        public string StudentDicCollegeName { get; set; }
        public string StudentDicCourse { get; set; }
        public string StudentDicPercentage { get; set; }
        public string StudentDicCGPA { get; set; }
        public string StudentDicYOP { get; set; }

    }
    [Serializable()]
    public class StudentGraduationproperty
    {
        public int StudentGrdScorecardId { get; set; }
        public int UserId { get; set; }

        public string StudentGrdCollegeName { get; set; }
        public string StudentGrdSpecialization { get; set; }
        public string StudentGrdPer { get; set; }
        public string StudentGrdCGPA { get; set; }
        public string StudentGrdYOP { get; set; }

    }
    [Serializable()]
    public class SchoolBoardproperty
    {
        public int BoardId { get; set; }
        public int UserId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string BoardFullName { get; set; }
        public string BoardShortName { get; set; }
        public string HeadOffAddrs { get; set; }
        public string Website { get; set; }
        public string BoardLogo { get; set; }
        public string ContactNumber { get; set; }
    }
    [Serializable()]
    public class PaymentMaster
    {
        public int BoardId { get; set; }
        public int UserId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string BoardFullName { get; set; }
        public string BoardShortName { get; set; }
        public string HeadOffAddrs { get; set; }
        public string Website { get; set; }
        public string BoardLogo { get; set; }
        public string ContactNumber { get; set; }
    }
   
}
