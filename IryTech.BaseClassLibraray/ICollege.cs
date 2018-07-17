using IryTech.AdmissionJankari.BO;
using System.Collections.Generic;
namespace IryTech.BaseClassLibraray
{
   public interface ICollege
   {
       List<LeadSourceProperty> GetLeadCollegeList(int courseId, string strStream, string strCities, bool findByCities,
                                                   bool participated);
       List<LeadSourceProperty> GetLeadCollegeNameList(int courseId, string strStream, string strCities, bool findByCities,
                                                 bool participated);
       int InsertCollegeGroupDetails(CollegeGroupProperty objCollegeGroupProperty ,int createdBy,out string errmsg);
       int UpdateCollegeGroupDetails(CollegeGroupProperty objCollegeGroupProperty, int modifiedBy, out string errmsg);
       List<CollegeGroupProperty>GetAllCollegeGroupList();
       List<CollegeGroupProperty>GetCollegeGroupListById(int collegeGriupId);
       List<CollegeGroupProperty> GetCollegeGroupListByGroupName(string collegeGroupName);
       int InsertInstituteTypeDetails(InstituteTypeProperty objInstituteType, int createdBy, out string errmsg);
       int UpdateInstituteTypeDetails(InstituteTypeProperty objInstituteType, int modifiedBy, out string errmsg);
       List<InstituteTypeProperty> GetAllInstituteTypeList();
       List<InstituteTypeProperty> GetInstituteTypeId(int instituteTypeId);
       int InsertCollegeAssociationCategoryType(CollegeAssociationCategoryProperty objCollegeAssociationCategoryProperty ,int createdBy,out string errmsg);
       int UpdateCollegeAssociationCategoryType(CollegeAssociationCategoryProperty objCollegeAssociationCategoryProperty, int modifiedBy, out string errmsg);
       List<CollegeAssociationCategoryProperty> GetAllCollegeAssociationCategoryType(string iCase="T");
       List<CollegeAssociationCategoryProperty> GetCollegeAssociationCategoryTypeById(int categoryId,string iCase="T");
       int InsertHostelCategory(HostelCategoryProperty objHostelCategoryProperty, int createdBy, out string errmsg);
       int UpdateHostelCategory(HostelCategoryProperty objHostelCategoryProperty, int modifiedBy, out string errmsg);
       List<HostelCategoryProperty> GetAllHostelCategory();
       List<HostelCategoryProperty> GetHostelCategoryListById(int hostelCategoryId);
       int InsertCollegeRankSource(CollegeRankSource objCollegeRankSource, int createdBy, out string errmsg);
       int UpdateCollegeRankSource(CollegeRankSource objCollegeRankSource, int modifiedBy, out string errmsg);
       List<CollegeRankSource> GetAllCollegeRankSourceList();
       List<CollegeRankSource> GetCollegeRankSourceListById(int collegeRankSourceId);
       int InsertCollegeLogin(string collegeName, int userId, out string errmsg);
       List<CollegeBranchProperty> GetAllCollegeBranchNameByCourseIdCollegeName(int CourseId, string CollegeName);
       List<CollegeBranchProperty> GetMostViewdCollegeByCourse(int courseId);

       List<CollegeBranchRankProperty> GetCollegeCourseRankByCollegeId(int collegeBranchId);

       // College basic Module for Createtion of the colleges Started 

        #region College Registation Module 

    
       int InsertCollegeBranchInfo(CollegeBranchProperty objCollegeBranchProperty, int createdBy, out string errmsg,
           out int collegeBranchId);
       int UpdateCollegeBranchInfo(CollegeBranchProperty objCollegeBranchProperty, int modifiedBy, out string errmsg, 
            out int collegeBranchId);
       int InsertCollegeBranchCourseInfo(CollegeBranchCourseProperty objCollegeBranchCourseProperty, int createdBy, out string errmsg, 
            out int collegeBranchCourseId);
   
           
       int UpdateCollegeBranchCourseInfo(CollegeBranchCourseProperty objCollegeBranchCourseProperty, int modifiedBy, out string errmsg,
            out int collegeBranchCourseId);
       int InsertCollegeBranchCourseStreamInfo(CollegeBranchCourseStreamProperty objCollegeBranchCourseStreamProperty, int createdBy,
           out string errmsg);
       int InsertCollegeBranchCourseStreamInfoByCollegeId(CollegeBranchCourseStreamProperty objCollegeBranchCourseStreamProperty, int createdBy,
           out string errmsg);
       int UpdateCollegeBranchCourseStreamInfo(CollegeBranchCourseStreamProperty objCollegeBranchCourseStreamProperty, int modifiedBy,
           out string errmsg);
      
       int InsertCollegeBranchCourseExamInfo(CollegeBranchCourseExamProperty objCollegeBranchCourseExamProperty, int createdBy,
           out string errmsg);

       int InsertCollegeBranchCourseExamInfoByCollegeId(CollegeBranchCourseExamProperty objCollegeBranchCourseExamProperty, int createdBy,
           out string errmsg);


       int UpdateCollegeBranchCourseExamInfo(CollegeBranchCourseExamProperty objCollegeBranchCourseExamProperty, int modifiedBy,
           out string errmsg);
      int InsertCollegeBranchCourseHighlightsByCollege(CollegeBranchCourseHighlightsProperty objCollegeBranchCourseHighlightsProperty, int createdBy,
              out string errmsg);

       int InsertCollegeBranchCourseHighlights(CollegeBranchCourseHighlightsProperty objCollegeBranchCourseHighlightsProperty, int createdBy,
           out string errmsg);
       int UpdateCollegeBranchCourseHighlights(CollegeBranchCourseHighlightsProperty objCollegeBranchCourseHighlightsProperty, int modifiedBy,
           out string errmsg);
       int InsertCollegeBranchCourseFacilities(CollegeBranchCourseFacilitiesProperty objCollegeBranchCourseFacilitiesProperty, int createdBy,
           out string errmsg);

       int InsertCollegeBranchCourseFacilitiesByCollege(CollegeBranchCourseFacilitiesProperty objCollegeBranchCourseFacilitiesProperty, int createdBy,
                out string errmsg);

       int UpdateCollegeBranchCourseFacilities(CollegeBranchCourseFacilitiesProperty objCollegeBranchCourseFacilitiesProperty, int modifiedBy,
           out string errmsg);

     int InsertCollegeBranchHostelInfoInsert(CollegeBranchCourseHostelProperty objCollegeBranchCourseHostelProperty, int createdBy,
               out string errmsg);
       int InsertCollegeBranchHostelInfo(CollegeBranchCourseHostelProperty objCollegeBranchCourseHostelProperty, int createdBy,
           out string errmsg);
       int UpdateCollegeBranchHostelInfo(CollegeBranchCourseHostelProperty objCollegeBranchCourseHostelProperty, int modifiedBy,
           out string errmsg);
       int InsertCollegeBranchRank(CollegeBranchRankProperty objCollegeBranchRankProperty, int createdBy,
           out string errmsg);
       int UpdateCollegeBranchRank(CollegeBranchRankProperty objCollegeBranchRankProperty, int modifiedBy,
           out string errmsg);


       int InsertCollegeBranchRankByCollegeId(CollegeBranchRankProperty objCollegeBranchRankProperty, int createdBy,
               out string errmsg);
       int InsertCollegeKeySpeach(CollegeBranchKeySpeech objCollegeBranchKeySpeech, int createdBy,
           out string errmsg);
       int UpdateCollegeKeySpeech(CollegeBranchKeySpeech objCollegeBranchKeySpeech, int modifiedBy,
           out string errmsg);
       int InsertCollegePlacement(CollegeBranchCoursePlacementProperty objCollegeBranchCoursePlacementProperty, int createdBy,
           out string errmsg);
       int UpdateCollegePlacement(CollegeBranchCoursePlacementProperty objCollegeBranchCoursePlacementProperty, int modifiedBy,
           out string errmsg);

       
       int InsertData(string column, string value,string dbColumnActualData, string branchNameValue, string ColumnDataType, out string errMsg);
       int InsertCourseData(string column, string value, string courseName, string dbColumnActualData, string branchNameValue, string universityName, string ColumnDataType, out string errMsg);
       int InsertCourseStreamData(string column, string value, string dbColumnActualData, string branchNameValue, string streamName, string ColumnDataType, out string errMsg);
       int InsertCourseExamData(string column, string value, string dbColumnActualData, string branchNameValue, string examName, string ColumnDataType, out string errMsg);
       int InsertCourseFacalityData(string column, string value, string dbColumnActualData, string branchNameValue, string ColumnDataType, out string errMsg);
       int InsertCourseHighLightsData(string column, string value, string dbColumnActualData, string branchNameValue, string ColumnDataType, out string errMsg);
       int InsertCourseRankSourceData(string column, string value, string dbColumnActualData, string branchNameValue, string ColumnDataType, out string errMsg,string courseName);
       int InsertCourseHostel(string column, string value, string dbColumnActualData, string branchNameValue, string ColumnDataType, out string errMsg,string courseName);
       List<CollegeBranchProperty> GetCollegeListByCourse(int courseId);
       List<CollegeBranchProperty> GetCollegeList();
       List<CollegeBranchProperty> GetCollegeListById(int collegeBranchId);
       List<CollegeBranchCourseProperty> GetCollegeCourseListByCollegeId(int collegeBranchId);

       List<CollegeBranchProperty> GetCollegeCourseListByCollegeName(string collegeName);
       List<CollegeBranchCourseProperty> GetCollegeCourseListByBranchCourseId(int branchCourseId,string type=null);
       List<CollegeBranchCourseStreamProperty> GetCollegeCourseStreamListByCollegeBranchId(int collegeBranchId);
       List<CollegeBranchCourseStreamProperty> GetCollegeCourseStreamListByCollegeCourseStreamId(int collegeCourseStreamId);
       List<CollegeBranchCourseExamProperty> GetCollegeCourseExamListByCollegeBranchId(int collegeBranchId);
       List<CollegeBranchCourseExamProperty> GetCollegeCourseStreamListByCollegeExamId(int collegeExamId);

       List<CollegeBranchCourseFacilitiesProperty> GetCollegeCourseFacalityByCollegeBranchId(int collegeBranchId);
       List<CollegeBranchCourseFacilitiesProperty> GetCollegeCourseFacalityByFacalityId(int facalityId);

       List<CollegeBranchRankProperty> GetCollegeCourseRankByCollegeBranchId(int collegeBranchId);
       List<CollegeBranchRankProperty> GetCollegeCourseRankByRankId(int rankId);

       List<CollegeBranchCourseHighlightsProperty> GetCollegeCourseHighLightsByCollegeBranchId(int collegeBranchId);
       List<CollegeBranchCourseHighlightsProperty> GetCollegeCourseHighLightsByHighLightsId(int highLightsId);


       List<CollegeBranchCourseHostelProperty> GetCollegeCourseHostelByCollegeBranchId(int collegeBranchId);
       List<CollegeBranchCourseHostelProperty> GetCollegeCourseHostelByHostelId(int hostelId);

       List<CollegeBranchProperty> GetCollegeBasicDetailsByBranchCourseId(int branchCourseId);
       List<CollegeBranchCourseStreamProperty> GetCollegeCourseStreamDetailsByBranchCourseId(int branchCourseId);
       List<CollegeBranchCourseExamProperty> GetCollegeCourseExamDetailsByBranchCourseId(int branchCourseId);
       List<CollegeBranchCourseHostelProperty> GetCollegeCourseHostelDetailsByBranchCourseId(int branchCourseId);
       List<CollegeBranchCourseFacilitiesProperty> GetCollegeCourseFacalityDetailsByBranchCourseId(int branchCourseId);
       List<CollegeBranchProperty> GetCollegeListByCityId(int branchCourseId, out string collegeName);
       List<CollegeBranchProperty> GetTopRankedColleges(int courseId, int pageNum, int pageSize, out int totalRecords);
       List<CollegeBranchProperty> GetPrivateCollegeList(int courseId,int pageNum,int pageSize,out int totalRecords);
       List<CollegeBranchProperty> GetCollegeListByDynamicQuery(string dbQuery, int pagesize, int pageNum, out int totalRecords);
       List<CollegeBranchProperty> GetCollegeListByUniversityId(int universityId,int courseId=0);
       int UpdateCollegeAssociation(string collegeName, int courseId, int associationTypeId, System.DateTime advstStartDate, System.DateTime advstEndDate, int advstPriorty, string advstURL, int createdBy, bool advstStatus, out string errmsg);



           
             List<CollegeBranchKeySpeech> GetDirectorSpeechByBranchCourseId(int collegeBranchCourseId);

             List<CollegeBranchCourseHighlightsProperty> GetCollegeHighLightsByBranchCourseId(int collegeBranchCourseId);

             List<CollegeBranchCoursePlacementProperty> GetCollegeTopHirer(int collegeBranchCourseId);

             List<CollegeBranchGallery> GetCollegeImageGallery(int branchCourseId);
             List<CollegeBranchRankProperty> BindCollegeRankYear(int branchCourseId);

             List<CollegeBranchRankProperty> BindCollegeRank(int branchCourseId,int year);

             List<CollegeBranchOnLineCounsellingProperty> GetCollegeForOnlineCounselling(string dbQuery, int pagesize, int pageNum, out int totalRecords);

             int UpdateCollegeCourseOnlineParticipation(int collegebranchCourseId, bool onlineParticipation, bool onlineParticipationVirtual,string AdmissionDate, int createdBy, out string errmsg);


             int UpdateCollegeCourseOnlineParticipationAndRating(int collegebranchCourseId, bool onlineParticipation, bool onlineParticipationVirtual, string factorId, string factorValues, double avgRating, string admissionDate, int createdBy, out string errmsg);


             List<Factor> GetFactorValues(int branchCourseId);

             List<CollegeBranchProperty> GetCollegeListByUserId(int userId);
             int InsertCollegePlacementByCourse(
                  CollegeBranchCoursePlacementProperty objCollegeBranchCoursePlacementProperty, int createdBy,
                  out string errmsg);

       List<CollegeBranchCoursePlacementProperty> GetCollegeCourseTopHirerListByCollegeId(int collegeId);
       List<CollegeBranchCourseHighlightsProperty> GetCollegeCourseHighLightsByCollegeBranchCourseId(int collegeBranchCourseId);

       List<CollegeBranchProperty> GetBookSeatCollege(int courseId, int pageNum, int pageSize, out int totalRecords);
       List<CollegeBranchProperty> GetBookedCollegeList(string collegeName);

       #endregion

   }
}
