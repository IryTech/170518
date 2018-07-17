using System;
namespace IryTech.AdmissionJankari.BO
{
    #region College class  Property

    [Serializable()]
    public class CollegeBranchProperty
    {
        public int InstituteTypeId { get; set; }
        public string InstituteType { get; set; }
        public string CollegePopulaorName { get; set; }
        public int CollegeGroupId { get; set; }
        public string CollegeGroupName { get; set; }
        public int CollegeIdBranchId { get; set; }
        public string CollegeBranchName { get; set; }
        public int CollegeManagementTypeId { get; set; }
        public string CollegeManagementType { get; set; }
        public string CollegeBranchEst { get; set; }
        public string CollegeBranchDesc { get; set; }
        public string CollegeBranchAddrs { get; set; }
        public string CollegeBranchMobileNo { get; set; }
        public string CollegeBranchPinCode { get; set; }
        public string CoillegeBranchEmailId { get; set; }
        public string CollegeBranchFax { get; set; }
        public string CollegeBranchWebsite { get; set; }
        public int CollegeBranchCountryId { get; set; }
        public string CollegeBranchLogo { get; set; }
        public string CollegeBranchCountryName { get; set; }
        public int CollegeBranchStateId { get; set; }
        public string CollegeBranchStateName { get; set; }
        public int CollegeBranchCityId { get; set; }
        public string CollegeBranchCityName { get; set; }
        public bool CollegeBranchStatus { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public bool Hostel { get; set; }
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public int CollegeBranchCourseId { get; set; }
        public string CourseStreamName { get; set; }
        public string Placement { get; set; }
        public string Fees { get; set; }
        public string Rank { get; set; }
        public string CollegeAssociationType { get; set; }
        public int CollegeAssociationId { get; set; }
        public bool CollegeBranchCourseSponserStatus { get; set; }
        public bool CollegeBranchCourseOnlineStatus { get; set; }
        public bool CollegeBranchCourseVirtualOnlineStatus { get; set; }
        public double CollegeBranchCourseRating { get; set; }
        public DateTime CollegeBranchCourseAdmissionDate { get; set; }
        public string CollegePhoneNo { get; set; }
        public string CollegeUrl { get; set; }
        public string UniversityUrl { get; set; }
        public string HelpLineNumber { get; set; }
        public bool CourseIsBookSeatVisible { get; set; }
        public bool CollegeIsBookSeatVisible { get; set; }
        public string CollegeBranchCourseHelplineNo { get; set; }
        public string CollegeCourseEligibiltyName { get; set; }
        public string CollegeCoures10EligibilityPer { get;set; }
        public string CollegeCoures12EligibilityPer { get;set; }
        public string CollegeCoures15EligibilityPer { get; set; }
        public string CollegeInstruction { get; set; }
        public bool CollegeOnlineParticipateStatus { get; set; }
        public string CollegeOverallRating { get; set; }
        public string CollegeAssociationCategory { get; set; }
    }
    public class LeadSourceProperty
    {
        public string CollegeBranchName { get; set; }
        public int CollegeBranchId { get; set; }
        public int StreamId { get; set; }
        public string StreamName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CollegeBranchCourseStreamSeat { get; set; }
        public int CollegeBranchCityId { get; set; }
        public string CollegeBranchCityName { get; set; }
        public bool CheckParticipatedColleges { get; set; }
        public string CollegeBranchCourseStreamFees { get; set; }
    }
    
    
    [Serializable()]
    public class CollegeBranchCourseProperty
    {
        public int CollegeBranchCourseId { get; set; }
        public int CollegeBranchId { get; set; }
        public string CollegeBranchName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public bool HasHostel { get; set; }
        public string CollegeBranchCourseDesc { get; set; }
        public string CollegeBranchCourseEst { get; set; }
        public string CollegeBranchCourseTitle { get; set; }
        public string CollegeBranchCourseMetaDesc { get; set; }
        public string CollegeBranchCourseMetaTag { get; set; }
        public string CollegeBranchCourseUrl { get; set; }
        public bool CollegeBranchCourseStatus { get; set; }
        public bool CollegeBranchCourseSponserStatus { get; set; }
        public string CollegeBranchCourseLogo { get; set; }
        public string CollegeBranchCourseManagement { get; set; }
        public bool CollegeBranchCourseOnlineStatus { get; set; }
        public string CollegeBranchCourseHelplineNo { get; set; }
        public bool IsBookSeatVisible { get; set; }
    }
    [Serializable()]
    public class CollegeBranchCourseStreamProperty
    {
        public int CollegeBranchCourseId { get; set; }
        public int CollegeBranchId { get; set; }
        public string CollegeBranchName { get; set; }
        public int CollegeBranchCourseStreamId { get; set; }
        public int StreamId { get; set; }
        public string StreamName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CollegeBranchCourseStreamSeat { get; set; }
        public string CollegeBranchCourseStreamDuration { get; set; }
        public string CollegeBranchCourseStreamFees { get; set; }
        public int CollegeBranchCourseStreamModeId { get; set; }
        public string CollegeBranchCourseStreamModeName { get; set; }
        public string CollegeBranchCourseStreamEligibity { get; set; }
        public string CollegeBranchCourseStreamDesc { get; set; }
        public string CollegeBranchCourseStreamManagementQuotaSeat { get; set; }
        public string CollegeBranchCourseStreamLateralEntrySeat { get; set; }
        public bool CollegeBranchCourseStreamStatus { get; set; }

    }
    [Serializable()]
    public class CollegeBranchCourseExamProperty
    {
        public int CollegeBranchCourseExamId { get; set; }
        public int CollegeBranchCourseId { get; set; }
        public int CollegeBranchId { get; set; }
        public string CollegeBranchName { get; set; }
        public bool CollegeCourseExamStatus { get; set; }
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public string ExamPopularName { get; set; }
        public string ExamEligibilty { get; set; }
        public string CollegeExamEligibilty { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
    [Serializable()]
    public class CollegeBranchCourseHighlightsProperty
    {
        public int CollegeBranchCourseId { get; set; }
        public int CollegeBranchId { get; set; }
        public string CollegeBranchName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int CollegeBranchCourseHighlightId { get; set; }
        public string CollegeBranchCourseHighlight { get; set; }
        public bool CollegeBranchCourseHighlightStatus { get; set; }

    }
    [Serializable()]
    public class CollegeBranchCourseFacilitiesProperty
    {
        public int CollegeBranchCourseId { get; set; }
        public int CollegeBranchId { get; set; }
        public string CollegeBranchName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int CollegeBranchCourseFacilitieId { get; set; }
        public string CollegeBranchCourseFacilitieName { get; set; }
        public string CollegeBranchCourseFacilitieDesc { get; set; }
        public bool CollegeBranchCourseFacilitieStatus { get; set; }

    }
    [Serializable()]
    public class CollegeBranchCoursePlacementProperty
    {
        public int CollegeBranchCourseId { get; set; }
        public int CollegeBranchId { get; set; }
        public string CollegeBranchName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int CollegeBranchCoursePlacementId { get; set; }
        public string CollegeBranchCoursePlacementCompanyName { get; set; }
        public string CollegeBranchCoursePlacementYear { get; set; }
        public string CollegeBranchCoursePlacementNoOfStudentHired { get; set; }
        public string CollegeBranchCoursePlacementAvgSalaryOffered { get; set; }
        public bool CollegeBranchCoursePlacementStatus { get; set; }

    }
    [Serializable()]
    public class CollegeBranchCourseDownLoadProperty
    {
        public int CollegeBranchCourseId { get; set; }
        public int CollegeBranchId { get; set; }
        public string CollegeBranchName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int CollegeBranchCourseDownloadId { get; set; }
        public string CollegeBranchCourseDownloadType { get; set; }
        public string CollegeBranchCourseDownloadName { get; set; }
        public bool CollegeBranchCourseDownloadStatus { get; set; }

    }
    [Serializable()]
    public class CollegeBranchCourseHostelProperty
    {
        public int CollegeBranchCourseId { get; set; }
        public int CollegeBranchId { get; set; }
        public string CollegeBranchName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int HostelCategoryId { get; set; }
        public string HostelCategoryName { get; set; }
        public int CollegeBranchCourseHostelId { get; set; }
        public string CollegeBranchCourseHostelLocation { get; set; }
        public bool IsCollegeBranchCourseHostelHasInternet { get; set; }
        public bool IsCollegeBranchCourseHostelHasLoundry { get; set; }
        public bool IsCollegeBranchCourseHostelHasPowerBackup { get; set; }
        public bool IsCollegeBranchCourseHostelHasAC { get; set; }
        public string CollegeBranchCourseHostelCharge { get; set; }
        public bool CollegeBranchCourseHostelStatus { get; set; }
    }
    [Serializable()]
    public class CollegeBranchGallery
    {
        public int CollegeBranchId { get; set; }
        public string CollegeBranchName { get; set; }
        public int CollegeBranchGalleryId { get; set; }
        public string CollegeBranchGalleryImageTitle { get; set; }
        public string CollegeBranchGalleryImageName { get; set; }
        public bool CollegeBranchGalleryImageStatus { get; set; }
    }
    [Serializable()]
    public class CollegeBranchKeySpeech
    {
        public int CollegeBranchId { get; set; }
        public string CollegeBranchName { get; set; }
        public int CollegeBranchKeySpeechId { get; set; }
        public string CollegeBranchKeySpeechPersonDesignation { get; set; }
        public string CollegeBranchKeySpeechPersonName { get; set; }
        public string CollegeBranchKeySpeechPersonImage { get; set; }
        public string CollegeBranchKeySpeechDesc { get; set; }
        public string CollegeBranchKeySpeechPersonAbout { get; set; }
        public bool CollegeBranchKeySpeechStatus { get; set; }
    }
    [Serializable()]
    public class CollegeBranchRankProperty
    {
        public int CollegeRankId { get; set; }
        public int CollegeBranchId { get; set; }
        public string CollegeBranchName { get; set; }
        public string CourseName { get; set; }

        public int CourseId { get; set; }
        public int CollegeBranchCourseId { get; set; }
        public int CollegeRankSourceId { get; set; }
        public string RankSourceName { get; set; }
        public int CollegeRankYear { get; set; }
        public string CollegeOverAllRank { get; set; }
        public bool CollegeRankStatus { get; set; }
    }

    #endregion


    [Serializable()]
    public class CollegeGroupProperty
    {
        public int CollegeGroupId { get; set; }
        public string CollegeGroupName { get; set; }
        public bool CollegeGropuStatus { get; set; }
        public string CollegeGroupLogo { get; set; }

    }
    [Serializable()]
    public class InstituteTypeProperty
    {
        public int InstituteTypeId { get; set; }
        public string InstituteType { get; set; }

    }
    [Serializable()]
    public class CollegeAssociationCategoryProperty
    {
        public int AssociationCategoryTypeId { get; set; }
        public string AssociationCategoryType { get; set; }
        public string AssociationCategoryAmount { get; set; }
        public string AssociationCategoryDescription{ get; set; }
        public bool AssociationCategoryStatus { get; set; }
        public string AssociationType { get; set; }
    }

    [Serializable()]
    public class HostelCategoryProperty
    {

        public int HostelCategoryId { get; set; }
        public string HostelCategoryType { get; set; }
        public bool HostelCategoryStatus { get; set; }

    }
    [Serializable()]
    public class CollegeRankSource
    {

        public int CollegeRankSourceId { get; set; }
        public string CollegeRankSourceName { get; set; }
        public bool CollegeRankSourceStatus { get; set; }

    }
    [Serializable()]
    public class CollegeBranchDetails
    {
        public int CollegeBranchName { get; set; }
         public int CourseId { get; set; 
        }
    }
   

    [Serializable()]
    public class CollegeBranchOnLineCounsellingProperty
    {
        public int CollegeBranchCourseId { get; set; }
        public int CollegeBranchId { get; set; }
        public string CollegeBranchName { get; set; }
        public bool CollegeBranchCourseSponserStatus { get; set; }
        public int FactorId { get; set; }
        public string FactoName { get; set; }
        public int CollegeFactorId { get; set; }
        public string CollegeFactorValue { get; set; }
        public string CollegeOverallRating { get; set; }
        public bool CollegeOnlineParticipateStatus { get; set; }
        public bool CollegeOnlineParticipationVirualStatus { get; set; }
        public DateTime AdmissionDate { get; set; }
        
    }
    public class Factor
    {
        public int FactorId { set; get; }
        public string FactorName { set; get; }
        public double FactorValues { set; get; }
        public int BranchCourseId { set; get; }
    }

    public class CollegeBanner
    {
        public int BannerId { set; get; }
        public string TooTip { set; get; }
        public string BannerUrl { set; get; }
        public DateTime BannerStartDate { set; get; }
        public DateTime BannerEndDate { set; get; }
        public int BannerPriority { set; get; }
        public bool BannerStatus { set; get; }
    }


    public class SearchPriorityListingCollege
    {
        public CollegeBanner CollegeBannerList { get; set; }
        public CollegeBranchProperty CollegeBasicInfo { get; set; }
        public CollegeBranchCourseProperty CollegeBranchCourse { get; set; }
        public CourseMasterProperty CourseMaster { get; set; }
        public CityProperty CityMaster { get; set; }

    }


}
   
