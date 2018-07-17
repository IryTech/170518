
using System.Collections.Generic;
using IryTech.BaseClassLibraray;
using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari.BL
{
    public abstract class CollegeProvider : ICollege
    {


        public static CollegeProvider Instance
        {
            get
            {

                return new College();
            }
        }


        #region ICollege Group  Members

        public abstract int InsertCollegeGroupDetails(CollegeGroupProperty objCollegeGroupProperty, int createdBy, out string errmsg);
        public abstract int UpdateCollegeGroupDetails(CollegeGroupProperty objCollegeGroupProperty, int modifiedBy, out string errmsg);
        public abstract List<CollegeGroupProperty> GetAllCollegeGroupList();
        public abstract List<CollegeBranchGallery> GetCollegeGalleryById(int CollegeGalleryID);
        public abstract List<CollegeBranchGallery> GetCollegeGalleryList();
        public abstract List<CollegeGroupProperty> GetCollegeGroupListById(int collegeGriupId);
        public abstract List<CollegeGroupProperty> GetCollegeGroupListByGroupName(string collegeGroupName);

        public abstract int InsertUpdateCollegeEvent( string collegeName, int courseId, string eventName, string eventLocation, System.DateTime eventDate, string eventDesc,bool eventStatus, out string errMsg,int eventId=0);
        public abstract System.Data.DataTable GetAllEvent();
        public abstract System.Data.DataTable GetEventById(int eventId);
        public abstract System.Data.DataTable GetEventByCollege(int collegeBranchCourseId);


        #endregion

        #region ICollege Members of Institute Type


        public abstract int InsertInstituteTypeDetails(InstituteTypeProperty objInstituteType, int createdBy, out string errmsg);
        public abstract int UpdateInstituteTypeDetails(InstituteTypeProperty objInstituteType, int modifiedBy, out string errmsg);
        public abstract List<InstituteTypeProperty> GetAllInstituteTypeList();
        public abstract List<InstituteTypeProperty> GetInstituteTypeId(int instituteTypeId);
        public abstract List<CollegeBranchProperty> GetAllSponserCollegeList();
        public abstract List<CollegeBranchProperty> GetAllSponserCollegeListbyCourseID(int CourseID);
        public abstract List<CollegeBranchProperty> GetAllCollegeBranchNameByCourseIdCollegeName(int CourseId, string CollegeName);

        #endregion

        #region ICollege Members of College Assocation Type category 


        public abstract int InsertCollegeAssociationCategoryType(CollegeAssociationCategoryProperty objCollegeAssociationCategoryProperty, int createdBy, out string errmsg);
        public abstract  int UpdateCollegeAssociationCategoryType(CollegeAssociationCategoryProperty objCollegeAssociationCategoryProperty, int modifiedBy, out string errmsg);
        public abstract List<CollegeAssociationCategoryProperty> GetAllCollegeAssociationCategoryType(string iCase="T");
        public abstract List<CollegeAssociationCategoryProperty> GetCollegeAssociationCategoryTypeById(int categoryId,string iCase="T");
        

        #endregion
        
        #region ICollege Hostel Category
            public abstract int InsertHostelCategory(HostelCategoryProperty objHostelCategoryProperty, int createdBy, out string errmsg);
            public abstract  int UpdateHostelCategory(HostelCategoryProperty objHostelCategoryProperty, int modifiedBy, out string errmsg);
            public abstract List<HostelCategoryProperty> GetAllHostelCategory();
            public abstract List<HostelCategoryProperty> GetHostelCategoryListById(int hostelCategoryId);
        #endregion
        
        #region ICollege Rank Source

            public abstract int InsertCollegeRankSource(CollegeRankSource objCollegeRankSource, int createdBy, out string errmsg);
            public abstract int UpdateCollegeRankSource(CollegeRankSource objCollegeRankSource, int modifiedBy, out string errmsg);
            public abstract List<CollegeRankSource> GetAllCollegeRankSourceList();
            public abstract List<CollegeRankSource> GetCollegeRankSourceListById(int collegeRankSourceId);
            
            #endregion

        #region ICollege Members for College Login Related 
                public  abstract int InsertCollegeLogin(string collegeName, int userId, out string errmsg);
        #endregion

        #region ICollege Members for college Registation
            
                public abstract int InsertCollegeBranchInfo(CollegeBranchProperty objCollegeBranchProperty, int createdBy, out string errmsg,
                out int collegeBranchId);
        
            public abstract int UpdateCollegeBranchInfo(CollegeBranchProperty objCollegeBranchProperty, int modifiedBy, out string errmsg,
                out int collegeBranchId);
        
            public abstract int InsertCollegeBranchCourseInfo(CollegeBranchCourseProperty objCollegeBranchCourseProperty, int createdBy, out string errmsg,
                out int collegeBranchCourseId);
          
            public abstract int UpdateCollegeBranchCourseInfo(CollegeBranchCourseProperty objCollegeBranchCourseProperty, int modifiedBy, out string errmsg,
                out int collegeBranchCourseId);
        
            public abstract int InsertCollegeBranchCourseStreamInfo(CollegeBranchCourseStreamProperty objCollegeBranchCourseStreamProperty, int createdBy,
                out string errmsg);

            public abstract int InsertCollegeBranchCourseStreamInfoByCollegeId(CollegeBranchCourseStreamProperty objCollegeBranchCourseStreamProperty, int createdBy,
           out string errmsg);
            public abstract int UpdateCollegeBranchCourseStreamInfo(CollegeBranchCourseStreamProperty objCollegeBranchCourseStreamProperty, int modifiedBy,
                out string errmsg);
            
            public abstract int InsertCollegeBranchCourseExamInfo(CollegeBranchCourseExamProperty objCollegeBranchCourseExamProperty, int createdBy,
                out string errmsg);

            public abstract int InsertCollegeBranchCourseExamInfoByCollegeId(CollegeBranchCourseExamProperty objCollegeBranchCourseExamProperty, int createdBy,
              out string errmsg);

            public abstract int UpdateCollegeBranchCourseExamInfo(CollegeBranchCourseExamProperty objCollegeBranchCourseExamProperty, int modifiedBy,
                out string errmsg);
           
            public abstract int InsertCollegeBranchCourseHighlights(CollegeBranchCourseHighlightsProperty objCollegeBranchCourseHighlightsProperty, int createdBy,
                out string errmsg);


            public abstract int InsertCollegeBranchCourseHighlightsByCollege(CollegeBranchCourseHighlightsProperty objCollegeBranchCourseHighlightsProperty, int createdBy,
                out string errmsg);
           
            public abstract int UpdateCollegeBranchCourseHighlights(CollegeBranchCourseHighlightsProperty objCollegeBranchCourseHighlightsProperty, int modifiedBy,
                out string errmsg);
            
            public abstract int InsertCollegeBranchCourseFacilities(CollegeBranchCourseFacilitiesProperty objCollegeBranchCourseFacilitiesProperty, int createdBy,
                out string errmsg);

            public abstract int InsertCollegeBranchCourseFacilitiesByCollege(CollegeBranchCourseFacilitiesProperty objCollegeBranchCourseFacilitiesProperty, int createdBy,
                out string errmsg);

            public abstract int UpdateCollegeBranchCourseFacilities(CollegeBranchCourseFacilitiesProperty objCollegeBranchCourseFacilitiesProperty, int modifiedBy,
                out string errmsg);
        
            public abstract int InsertCollegeBranchHostelInfo(CollegeBranchCourseHostelProperty objCollegeBranchCourseHostelProperty, int createdBy,
                out string errmsg);
            public abstract int InsertCollegeBranchHostelInfoInsert(CollegeBranchCourseHostelProperty objCollegeBranchCourseHostelProperty, int createdBy,
                out string errmsg);
        
            public abstract int UpdateCollegeBranchHostelInfo(CollegeBranchCourseHostelProperty objCollegeBranchCourseHostelProperty, int modifiedBy,
                out string errmsg);

            public abstract int InsertCollegeBranchRank(CollegeBranchRankProperty objCollegeBranchRankProperty, int createdBy,
                out string errmsg);

            public abstract int InsertCollegeBranchRankByCollegeId(CollegeBranchRankProperty objCollegeBranchRankProperty, int createdBy,
                 out string errmsg);

            public abstract int UpdateCollegeBranchRank(CollegeBranchRankProperty objCollegeBranchRankProperty, int modifiedBy,
                out string errmsg);

            public abstract int InsertCollegeKeySpeach(CollegeBranchKeySpeech objCollegeBranchKeySpeech, int createdBy,
                out string errmsg);

            public abstract int UpdateCollegeKeySpeech(CollegeBranchKeySpeech objCollegeBranchKeySpeech, int modifiedBy,
                out string errmsg);

            public abstract int InsertCollegePlacement(CollegeBranchCoursePlacementProperty objCollegeBranchCoursePlacementProperty, int createdBy,
                out string errmsg);

            public abstract int UpdateCollegePlacement(CollegeBranchCoursePlacementProperty objCollegeBranchCoursePlacementProperty, int modifiedBy,
                out string errmsg);
            public abstract int InsertData(string column, string value,string dbColumnActualData, string branchNameValue, string ColumnDataType, out string errMsg);
            public abstract int InsertCourseData(string column, string value,string courseName, string dbColumnActualData, string branchNameValue,string universityName, string ColumnDataType, out string errMsg);
            public abstract int InsertCourseStreamData(string column, string value, string dbColumnActualData, string branchNameValue, string streamName, string ColumnDataType, out string errMsg);
            public abstract int InsertCourseExamData(string column, string value, string dbColumnActualData, string branchNameValue, string examName, string ColumnDataType, out string errMsg);
            public abstract int InsertCourseFacalityData(string column, string value, string dbColumnActualData, string branchNameValue,  string ColumnDataType, out string errMsg);
            public abstract int InsertCourseHighLightsData(string column, string value, string dbColumnActualData, string branchNameValue, string ColumnDataType, out string errMsg);
            public abstract int InsertCourseRankSourceData(string column, string value, string dbColumnActualData, string branchNameValue, string ColumnDataType, out string errMsg,string courseName);
            public abstract int InsertCourseHostel(string column, string value, string dbColumnActualData, string branchNameValue, string ColumnDataType, out string errMsg,string courseName);
            public abstract List<CollegeBranchProperty> GetCollegeListByCourse(int courseId);
            public abstract List<CollegeBranchProperty> GetCollegeList();
            public abstract List<CollegeBranchProperty> GetCollegeListById(int collegeBranchId);
            public abstract List<CollegeBranchCourseProperty> GetCollegeCourseListByCollegeId(int collegeBranchId);
            public abstract List<CollegeBranchCourseProperty> GetCollegeCourseListByBranchCourseId(int branchCourseId,string type=null);

            public abstract List<CollegeBranchCourseStreamProperty> GetCollegeCourseStreamListByCollegeBranchId(int collegeBranchId);

            public abstract List<CollegeBranchCourseStreamProperty> GetCollegeCourseStreamListByCollegeCourseStreamId(int collegeCourseStreamId);

            public abstract List<CollegeBranchProperty> GetCollegeCourseListByCollegeName(string collegeName);

            public abstract List<CollegeBranchCourseExamProperty> GetCollegeCourseExamListByCollegeBranchId(int collegeBranchId);
            public abstract List<CollegeBranchCourseExamProperty> GetCollegeCourseStreamListByCollegeExamId(int collegeExamId);

            public abstract List<CollegeBranchCourseFacilitiesProperty> GetCollegeCourseFacalityByCollegeBranchId(int collegeBranchId);
            public abstract List<CollegeBranchCourseFacilitiesProperty> GetCollegeCourseFacalityByFacalityId(int facalityId);
            public abstract List<CollegeBranchRankProperty> GetCollegeCourseRankByCollegeBranchId(int collegeBranchId);

            public abstract List<CollegeBranchRankProperty> GetCollegeCourseRankByCollegeId(int collegeBranchId);
            public abstract List<CollegeBranchRankProperty> GetCollegeCourseRankByRankId(int rankId);

            public abstract List<CollegeBranchCourseHighlightsProperty> GetCollegeCourseHighLightsByCollegeBranchId(int collegeBranchId);
            public abstract List<CollegeBranchCourseHighlightsProperty> GetCollegeCourseHighLightsByHighLightsId(int highLightsId);
            public abstract List<CollegeBranchCourseHighlightsProperty> GetCollegeCourseHighLightsByCollegeBranchCourseId(int collegeBranchCourseId);


            public abstract List<CollegeBranchCourseHostelProperty> GetCollegeCourseHostelByCollegeBranchId(int collegeBranchId);
            public abstract List<CollegeBranchCourseHostelProperty> GetCollegeCourseHostelByHostelId(int hostelId);


            public abstract List<CollegeBranchProperty> GetCollegeBasicDetailsByBranchCourseId(int branchCourseId);
            public abstract List<CollegeBranchCourseStreamProperty> GetCollegeCourseStreamDetailsByBranchCourseId(int branchCourseId);
            public abstract List<CollegeBranchCourseExamProperty> GetCollegeCourseExamDetailsByBranchCourseId(int branchCourseId);
            public abstract List<CollegeBranchCourseHostelProperty> GetCollegeCourseHostelDetailsByBranchCourseId(int branchCourseId);
            public abstract List<CollegeBranchCourseFacilitiesProperty> GetCollegeCourseFacalityDetailsByBranchCourseId(int branchCourseId);

            public abstract List<CollegeBranchProperty> GetCollegeListByCityId(int branchCourseId, out string collegeName);

            public abstract List<CollegeBranchProperty> GetMostViewdCollegeByCourse(int courseId);

            public abstract List<CollegeBranchCoursePlacementProperty> GetCollegeCourseTopHirerListByCollegeName(string collegeName);

            public abstract List<CollegeBranchProperty> GetTopRankedColleges(int courseId, int pageNum, int pageSize, out int totalRecords);

            public abstract List<CollegeBranchProperty> GetPrivateCollegeList(int courseId,int pageNum,int pageSize,out int totalRecords);


            public abstract List<CollegeBranchProperty> GetCollegeListByDynamicQuery(string dbQuery, int pagesize, int pageNum, out int totalRecords);

            public abstract List<CollegeBranchCoursePlacementProperty> GetCollegeTopHirerByDynamicQuery(string dbQuery);
            public abstract List<CollegeBranchCoursePlacementProperty> GetCollegeCourseTopHirerList();


            public abstract int UpdateCollegeAssociation(string collegeName, int courseId, int associationTypeId, System.DateTime advstStartDate, System.DateTime advstEndDate, int advstPriorty, string advstURL, int createdBy, bool advstStatus, out string errmsg);

            public abstract List<CollegeBranchKeySpeech> GetDirectorSpeechByBranchCourseId(int collegeBranchCourseId);

            public abstract List<CollegeBranchCourseHighlightsProperty> GetCollegeHighLightsByBranchCourseId(int collegeBranchCourseId);

            public abstract List<CollegeBranchCoursePlacementProperty> GetCollegeTopHirer(int collegeBranchCourseId);

            public abstract List<CollegeBranchCoursePlacementProperty> GetCollegeTopHirerByPlacementID(int collegePlacementID);

            public abstract List<CollegeBranchGallery> GetCollegeImageGallery(int branchCourseId);


            public abstract List<CollegeBranchRankProperty> BindCollegeRankYear(int branchCourseId);

            public abstract List<CollegeBranchRankProperty> BindCollegeRank(int branchCourseId, int year);




            public abstract int UpdateCollegeCourseOnlineParticipation(int collegebranchCourseId, bool onlineParticipation, bool onlineParticipationVirtual,string AdmissionDate, int createdBy, out string errmsg);



            public abstract int UpdateCollegeCourseOnlineParticipationAndRating(int collegebranchCourseId, bool onlineParticipation, bool onlineParticipationVirtual, string factorId, string factorValues, double avgRating,string admissionDate, int createdBy, out string errmsg);
            public abstract List<CollegeBranchOnLineCounsellingProperty> GetCollegeForOnlineCounselling(string dbQuery, int pagesize, int pageNum, out int totalRecords);


            public abstract List<Factor> GetFactorValues(int branchCourseId);



            public abstract List<CollegeBranchCourseStreamProperty> GetCollegeCourseStreamDetails();

            public abstract List<LeadSourceProperty> GetLeadCollegeList(int courseId, string strStream, string strCities,
                                                            bool findByCities, bool participated);


            public abstract List<LeadSourceProperty> GetLeadCollegeNameList(int courseId, string strStream, string strCities, bool findByCities,
                                                     bool participated);

          public abstract  List<CollegeBranchProperty> GetCollegeListByUserId(int userId);

        public abstract int InsertCollegePlacementByCourse(
            CollegeBranchCoursePlacementProperty objCollegeBranchCoursePlacementProperty, int createdBy,
            out string errmsg);

        public abstract int UpdateCollegePlacementByCourse(
          CollegeBranchCoursePlacementProperty objCollegeBranchCoursePlacementProperty, int createdBy,
          out string errmsg);

        public abstract List<CollegeBranchCoursePlacementProperty> GetCollegeCourseTopHirerListByCollegeId(int collegeId);
        public abstract List<CollegeBranchProperty> GetBookSeatCollege(int courseId, int pageNum, int pageSize, out int totalRecords);
        #endregion

        #region ICollege Members

            public abstract List<CollegeBranchProperty> GetCollegeListByUniversityId(int universityId,int courseId=0);
            
            public abstract List<CollegeBranchProperty> GetCollegeListByCourseExamStateCIty(int cityId, int stateId, int examId, int courseId, int collegeManagemnet, out int ErrorCount, out string Searchpattern, int PageNum, out int TotalRecords, int PageSize);
            public abstract int InsertCollegeGallery(CollegeBranchGallery objCollegeGalleryProperty, int createdBy, out string errmsg);
            public abstract int UpdateCollegeGallery(CollegeBranchGallery objCollegeGalleryProperty, int createdBy, out string errmsg);
            public abstract List<CollegeBranchProperty> GetBookedCollegeList(string collegeName);
            #endregion

        public abstract List<CollegeBranchOnLineCounsellingProperty> GetCollegeForOnline(int collegeBranchCourseId);
        public abstract List<CollegeBranchProperty> GetBookedCollegeByCourse(int collegeCourseId);


        #region SearchPriorityListingCollege
        public abstract List<SearchPriorityListingCollege> GetSearchPriorityListingCollege(int cityId, int stateId, int examId, int courseId, int collegeManagemnet,  out string searchPattern);
        #endregion
    }



}