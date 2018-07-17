

using System;
namespace IryTech.AdmissionJankari.BO
{
   [Serializable()] 
    public class ExamProperty
    {

        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public string ExamFullName { get; set; }
        public string ExamPopularName { get; set; }
        public string ExamEligiblityCriteria { get; set; }
        public string ExamLogo { get; set; }
        public string ExamDesc { get; set; }
        public string ExamWebSite { get; set; }
        public string CourseName { get; set; }
        public int CourseId { get; set; }
        public bool ExamStatus { get; set; }
        public string HelpLineNumber { get; set; }
    }
    [Serializable()] 
    public class ExamFormProperty
    {
        public int ExamFormId { get; set; }
        public int ExamId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set;}
        public string ExamName { get; set; }
        public string ExamFormUrl { get; set; }
        public string ExamFormTitle { get; set; }
        public string ExamFormKeywords { get; set; }
        public string ExamFormMetaDesc { get; set;}
        public string ExamFormSubject { get; set; }
        public string ExamFormYear { get; set; }
        public string ExamFormWebsite { get; set; }
        public string ExamFormSaleStartDate { get; set; }
        public string ExamFromSaleStartDateRemark { get; set; }
        public string ExamFormSaleEndDate { get; set; }
        public string ExamFormSaleEndDateRemark { get; set; }
        public string ExamFormSubmitDate { get; set; }
        public string ExamFormSubmitDateRemark { get; set; }
        public string ExamFormResultDate { get; set; }
        public string ExamFormResultDateReamrk { get; set; }
        public string ExamFormResultWebsite { get; set; }
        public string ExamFormPrice { get; set; }
        public string ExamFormStore { get; set; }
        public string ExamFormCenter { get; set; }
        public string ExamFormDd { get; set; }
        public string ExamFormSyllabus { get; set; }
        public bool ExamFormStatus { get; set; }
    }
}