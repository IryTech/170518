using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using IryTech.AdmissionJankari.DAL;

using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari.BL
{
       
   public class Consulling
    {
        private DbWrapper _objDataWrapper;
        private DataSet _dataSet;
        private int _i;

        public DataTable GetBoradList()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetBoardList");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetBoradList in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }

       // Method to Bind The Course Admission Eligibilty
        public DataTable GetCourseAdmissionEligibilty(int courseId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CourseId",courseId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCourseAdmissionEligibilty");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCourseAdmissionEligibilty in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];

        }
        public DataTable GetCourseAdmissionEligibiltyById(int consullingEligibiltyId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@EligibiltyId", consullingEligibiltyId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetAdmissionEligibiltyById");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCourseAdmissionEligibilty in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];

        }


       // Method to get the Payment Amount according to course
        public string GetPayemntAmountAccordingToCourse(int courseId)
        {
             _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            string Amount = "";
            try
            {
                _objDataWrapper.AddParameter("@CourseId", courseId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetPaymentDetails");
                if (_dataSet.Tables[0].Rows.Count > 0)
                {
                    Amount = _dataSet.Tables[0].Rows[0]["AjPaymentAmount"].ToString();
                }
                else
                {
                    Amount = "1100";
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetPayemntAmountAccordingToCourse in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return Amount;
        }


       // Method to Insert The Acedmic Info Of the User
        public int InsertUpdateAcademicInfo(int usreId,string eligibilty ,StudentHighSchoolInfo objStudentHighSchoolInfo,
            StudentIntermidateInfo objStudentIntermidateInfo,StudentDicInfo objStudentDicInfo,StudentGrdInfo objStudentGrdInfo)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            int i = 0;

            try
            {
                _objDataWrapper.AddParameter("@UserId", usreId);
                _objDataWrapper.AddParameter("@Eligibilty",eligibilty);
                _objDataWrapper.AddParameter("@User10SchoolName", objStudentHighSchoolInfo.SchoolName);
                _objDataWrapper.AddParameter("@User10SchoolBoardId", objStudentHighSchoolInfo.SchoolBoard);
                _objDataWrapper.AddParameter("@User10SchoolCGPA", objStudentHighSchoolInfo.SchoolCGPA);
                _objDataWrapper.AddParameter("@User10SchoolYOP", objStudentHighSchoolInfo.SchoolYOP);
                _objDataWrapper.AddParameter("@User12CollegeName", objStudentIntermidateInfo.CollegeName);
                _objDataWrapper.AddParameter("@User12CollegeBoardId", objStudentIntermidateInfo.CollegeBoard);
                _objDataWrapper.AddParameter("@User12CollegeCGPA", objStudentIntermidateInfo.CollegeCGPA);
                _objDataWrapper.AddParameter("@User12CollegeYOP", objStudentIntermidateInfo.CollegeYOP);
                _objDataWrapper.AddParameter("@User12CollegeSpecialization", objStudentIntermidateInfo.CollegeCourseCombination);
                _objDataWrapper.AddParameter("@User12CollegeSpecializationCourseCombination", objStudentIntermidateInfo.CollegeCourseCombination);
                _objDataWrapper.AddParameter("@User12CollegeSpecializationPer", objStudentIntermidateInfo.CollegeCourseCombinationPer);
                _objDataWrapper.AddParameter("@User12CollegePer",objStudentIntermidateInfo.CollegePer);
                _objDataWrapper.AddParameter("@UserDicCollegeName",objStudentDicInfo.DicCollegeName);
                _objDataWrapper.AddParameter("@UserDicCollegeCourse",objStudentDicInfo.DicCourseName);
                _objDataWrapper.AddParameter("@UserDicCollegePer",objStudentDicInfo.DicPer);
                _objDataWrapper.AddParameter("@UserDicCollegeCGPA",objStudentDicInfo.DicCGPA);
                _objDataWrapper.AddParameter("@UserDicCollegeYOP",objStudentDicInfo.DicYOP);
                _objDataWrapper.AddParameter("@UserGrdCollegeName",objStudentGrdInfo.GrdCollegeName);
                _objDataWrapper.AddParameter("@UserGrdSpecialization",objStudentGrdInfo.GrdSpecialization);
                _objDataWrapper.AddParameter("@UserGrdYOP",objStudentGrdInfo.GrdYOP);
                _objDataWrapper.AddParameter("@UserGrdPer",objStudentGrdInfo.GrdPer);
                _objDataWrapper.AddParameter("@UserGrdCGPA",objStudentGrdInfo.GrdCGPA);

                i=_objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateStudentAcademicInfo");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateAcademicInfo in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

       // Method to Insert Exam Appared 

        public int InsertUpdateStudentExamAppeared(int userId, string examName, string examRank)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var i = 0;
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@ExamName", examName);
                _objDataWrapper.AddParameter("@ExamRank", examRank);
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateStudentExamAppared");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateStudentExamAppeared in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }
      
       // Method to Insert student College Prefrance
        public int InsertStudentCollegePrefrance(int userId, string collegeName, int courseId,bool isBookSeatStaus=false)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@CollegeName", collegeName.Trim());
                _objDataWrapper.AddParameter("@CourseId", courseId);
                _objDataWrapper.AddParameter("@IsBookSeatStatus", isBookSeatStaus);
                var errMsg =
                (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateStudentCollegePrefrance");
                if (errMsg != null && errMsg.Value != null)
                {
                    var errmsg = Convert.ToString(errMsg.Value);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertStudentCollegePrefrance in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }
        public int DeleteCollegePreference(int collegePrefernceId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@CollegePrefernceId", collegePrefernceId);
      
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_DeleteStudentCollegePrefrance");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DeleteCollegePreference in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }
       // Method to Insert student College City Prefrance

        public int InsertStudentCityPrefrance(int userId, string cityName)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@CityName", cityName);
            
                var errMsg =
                  (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateStudentCityPrefrance");
                if (errMsg != null && errMsg.Value != null)
                {
                    var  errmsg = Convert.ToString(errMsg.Value);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertStudentCityPrefrance in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

        public int DeleteCityPrefernce(int cityPrefernceId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@CityPrefernceId", cityPrefernceId);

                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_DeleteCityPrefernce");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DeleteCityPrefernce in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }
        public int InsertStudentStreamPrefrance(int userId, int streamId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@StreamId", streamId);
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertStudentStreamPrefrance");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertStudentStreamPrefrance in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }
        public int DeleteStreamPrefernce(int streamPrefernceId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@StreamPrefernceId", streamPrefernceId);

                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_DeleteStreamPrefernce");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DeleteStreamPrefernce in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }
       // Method to get the list of the Online Consulling Partcipaint
        public List<CollegeBranchProperty> GetParticipentCollegeListByCourse(int courseId)
        {

            var objCollegeBranchList = new List<CollegeBranchProperty>();
            try
            {
                objCollegeBranchList = GetCollegeListByCourseId(courseId).Where(result => result.CollegeBranchCourseVirtualOnlineStatus == true).ToList();
                objCollegeBranchList = objCollegeBranchList.OrderByDescending(result => result.CollegeBranchCourseRating).ToList();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetParticipentCollegeListByCourse in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objCollegeBranchList;
        }



       // Method to Get The Participent College List Based on the State id
        public List<CollegeBranchProperty> GetParticipentCollegeListByState(int stateId,int courseId )
        {

            var objCollegeBranchList = new List<CollegeBranchProperty>();
            try
            {
                objCollegeBranchList = GetCollegeListByCourseId(courseId).Where(result => result.CollegeBranchCourseVirtualOnlineStatus == true && result.CollegeBranchStateId == stateId).ToList();
                objCollegeBranchList = objCollegeBranchList.OrderByDescending(result => result.CollegeBranchCourseRating).ToList();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetParticipentCollegeListByState in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objCollegeBranchList;
        }
       // Method to Get The Participent College List Based on the State id
        public List<CollegeBranchProperty> GetParticipentCollegeListByCity(int cityId,int courseId)
        {

            var objCollegeBranchList = new List<CollegeBranchProperty>();
            try
            {
                objCollegeBranchList = GetCollegeListByCourseId(courseId).Where(result => result.CollegeBranchCourseVirtualOnlineStatus == true && result.CollegeBranchCityId == cityId).ToList();
                objCollegeBranchList = objCollegeBranchList.OrderByDescending(result => result.CollegeBranchCourseRating).ToList();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetParticipentCollegeListByCity in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objCollegeBranchList;
        }


        private List<CollegeBranchProperty> GetCollegeListByCourseId(int courseId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objCollegeBranchList = new List<CollegeBranchProperty>();
            try
            {
                _objDataWrapper.AddParameter("@CourseId", courseId);
                _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeListByCourse");
                objCollegeBranchList = BindCollegeListObject(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeListByCourseId in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objCollegeBranchList;
        }
        public List<AccountPaymentMasterProp> GetPaymentTransactionStatus(int userId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objCollegeBranchList = new List<AccountPaymentMasterProp>();
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_proc_GetStudentTrasactionDetails");
                objCollegeBranchList = BindStudentTransactionDetails(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetPaymentTransactionStatus in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objCollegeBranchList;
        }



        private List<CollegeBranchProperty> BindCollegeListObject(DataTable datatable)
        {
            var objCollegeGroupList = new List<CollegeBranchProperty>();
            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {

                        var objCollegeGroupProperty = new CollegeBranchProperty
                        {
                           InstituteTypeId = Convert.ToInt32(datatable.Rows[j]["AjInstituteTypeId"].ToString() != "" ? datatable.Rows[j]["AjInstituteTypeId"] : "0"),
                           CollegeBranchCourseId = Convert.ToInt32(datatable.Columns.Contains("AjCollegeBranchCourseId") ? (datatable.Rows[j]["AjCollegeBranchCourseId"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchCourseId"] : "0") : "0"),
                           CollegeGroupId = Convert.ToInt32(datatable.Rows[j]["AjCollegeGroupId"].ToString() != "" ? datatable.Rows[j]["AjCollegeGroupId"] : "0"),
                           CourseId = (datatable.Rows[j]["AjCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCourseId"]),
                           CourseName = (datatable.Rows[j]["AjCourseName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCourseName"]),
                           CollegePopulaorName = Convert.ToString(datatable.Rows[j]["AjCollegeBranchPopularName"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchPopularName"] : "N/A"),
                           CollegeAssociationId = Convert.ToInt32(datatable.Columns.Contains("AjCollegeAssociationCategoryId") ? (datatable.Rows[j]["AjCollegeAssociationCategoryId"].ToString() != "" ? datatable.Rows[j]["AjCollegeAssociationCategoryId"] : "0") : "0"),
                           CollegeAssociationType = Convert.ToString(datatable.Columns.Contains("AjCollegeAssociationCategoryName") ? (datatable.Rows[j]["AjCollegeAssociationCategoryName"].ToString() != "" ? datatable.Rows[j]["AjCollegeAssociationCategoryName"] : "N/A") : "N/A"),
                           CollegeIdBranchId = Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchId"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchId"] : "N/A"),
                           CollegeBranchEst = (datatable.Rows[j]["AjCollegeBranchCourseEst"] is DBNull) ? "N/A" : Convert.ToString(datatable.Rows[j]["AjCollegeBranchCourseEst"]),
                           CollegeBranchName = Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchName"] : "N/A"),
                           CollegeManagementTypeId = Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchManagementId"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchManagementId"] : "0"),
                           CollegeManagementType = Convert.ToString(datatable.Columns.Contains("AjMasterValues") ? (datatable.Rows[j]["AjMasterValues"].ToString() != "" ? datatable.Rows[j]["AjMasterValues"] : "N/A") : "N/A"),
                           CoillegeBranchEmailId = Convert.ToString(datatable.Rows[j]["AjCollegeBranchEmailId"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchEmailId"] : "N/A"),
                           CollegeBranchDesc = Convert.ToString(datatable.Rows[j]["AjCollegeBranchDesc"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchDesc"] : "N/A"),
                           CollegeBranchAddrs = Convert.ToString(datatable.Rows[j]["AjCollegeBranchAddress"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchAddress"] : "N/A"),
                           CollegeBranchMobileNo = Convert.ToString(datatable.Rows[j]["AjCollegeBranchMobileNo"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchMobileNo"] : "N/A"),
                           CollegeBranchPinCode = Convert.ToString(datatable.Rows[j]["AjCollegeBranchPinCode"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchPinCode"] : "N/A"),
                           CollegeBranchFax = Convert.ToString(datatable.Rows[j]["AjCollegeBranchFax"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchFax"] : "N/A"),
                           CollegeBranchWebsite = Convert.ToString(datatable.Rows[j]["AjCollegeBranchWebSite"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchWebSite"] : "N/A"),
                           CollegeBranchCountryId = Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCountryId"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchCountryId"] : "0"),
                           CollegeBranchStateId = Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchStateId"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchStateId"] : "0"),
                           CollegeBranchCityId = Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCityId"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchCityId"] : "0"),
                           CollegeBranchStatus = Convert.ToBoolean(datatable.Rows[j]["AjCollegeBranchStatus"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchStatus"] : false),
                           CollegeBranchLogo  = (datatable.Rows[j]["AjCollegeBranchLogo"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchLogo"]),
                           CollegeBranchCityName = (datatable.Rows[j]["AjCityName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCityName"]),
                           CollegeBranchStateName = (datatable.Rows[j]["AjStateName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjStateName"]),
                           CollegeBranchCourseOnlineStatus = (datatable.Rows[j]["AjCollegeOnlineParticipate"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeOnlineParticipate"]),
                           CollegeBranchCourseVirtualOnlineStatus = (datatable.Rows[j]["AjCollegeOnlineParticipationVirualStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeOnlineParticipationVirualStatus"]),
                           CollegeBranchCourseRating = (datatable.Rows[j]["AjCollegeOverallRating"] is DBNull) ? 0.0 : Convert.ToDouble(datatable.Rows[j]["AjCollegeOverallRating"]),
                           CollegeBranchCourseAdmissionDate=(datatable.Rows[j]["AjAdmissionOpenDate"] is DBNull) ?DateTime.Now : Convert.ToDateTime(datatable.Rows[j]["AjAdmissionOpenDate"]),
                           CourseIsBookSeatVisible = datatable.Columns.Contains("CourseIsBookSeat") ? (!string.IsNullOrEmpty(Convert.ToString(datatable.Rows[j]["CourseIsBookSeat"])) ? Convert.ToBoolean(datatable.Rows[j]["CourseIsBookSeat"]) : false) : false,
                           CollegeIsBookSeatVisible = datatable.Columns.Contains("CollegeIsBookSeatVisible") ? (!string.IsNullOrEmpty(Convert.ToString(datatable.Rows[j]["CollegeIsBookSeatVisible"])) ? Convert.ToBoolean(datatable.Rows[j]["CollegeIsBookSeatVisible"]) : false) : false,
                        };
                        objCollegeGroupList.Add(objCollegeGroupProperty);
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCollegeListObject in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objCollegeGroupList;
        }

        private List<AccountPaymentMasterProp> BindStudentTransactionDetails(DataTable datatable)
        {
            var objCollegeGroupList = new List<AccountPaymentMasterProp>();
            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {

                        var objCollegeGroupProperty = new AccountPaymentMasterProp
                        {
                            UserTransactionId = (datatable.Rows[j]["AjUserTransactionId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjUserTransactionId"]),
                            CollegeBranchCourseId = (datatable.Rows[j]["AjCollegeBranchCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseId"]),
                            UserLoginId = (datatable.Rows[j]["AjUserLoginId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjUserLoginId"]),

                            AdmissionPriority = (datatable.Rows[j]["AjAdmissionPriority"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjAdmissionPriority"]),

                            AjStudentInterestedCollegeCounsellingId = (datatable.Rows[j]["AjStudentInterestedCollegeCounsellingId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjStudentInterestedCollegeCounsellingId"]),

                            StudentPaymentStatus = (datatable.Rows[j]["AjStudentPaymentStatus"] is DBNull) ?false:Convert.ToBoolean(datatable.Rows[j]["AjStudentPaymentStatus"]),
                            PaymentAmount = (datatable.Rows[j]["AjPaymentAmount"] is DBNull) ? "1100" : Convert.ToString(datatable.Rows[j]["AjPaymentAmount"]),

                           
                        };
                        objCollegeGroupList.Add(objCollegeGroupProperty);
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindStudentTransactionDetails in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objCollegeGroupList;
        }
        public int InsertStudentCollegeInterested(int collegeBranchCourseId, int userId, out string errMsg ,int collegePriorty=0)
        {
            int i = 0;
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMsg = "";
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@CollegeBranchCourseId", collegeBranchCourseId);
                _objDataWrapper.AddParameter("@CollegePriority", collegePriorty);
                var ObjErrMsg = (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                 i= _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateIntertestedCollege");
                if (ObjErrMsg != null && ObjErrMsg.Value != null)
                    errMsg = Convert.ToString(ObjErrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertStudentCollegeInterested in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }
        public DataSet  GetIntertestedCollege(int userId)
        {
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetStudentIntertestedCollegeList");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetIntertestedCollege in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
           return  _dataSet;

        }

       // Delete User Intertested College

        public int  DeleteIntertestedCollege(int intertesredCollegeId,out string errMsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            int i = 0;
            errMsg = "";
            
            try
            {
                _objDataWrapper.AddParameter("@IntertestedCollegeId", intertesredCollegeId);
                var objErrMsg = (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_DeleteUserIntertestedCollege");

                if (objErrMsg != null && objErrMsg.Value != null)
                    errMsg = Convert.ToString(objErrMsg.Value); 
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DeleteIntertestedCollege in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }


       // Method To Add the Intertested Stream List
        public int InsertUserInterestedStream(int userId, int collegeBranchStreamId, out string errMsg,int streamPrioty=0)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMsg="";
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@CollegeBranchStreamId", collegeBranchStreamId);
                _objDataWrapper.AddParameter("@StreamPrioty", streamPrioty);
                var ObjErrMsg = (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateUserStreamPrioty");
                if (ObjErrMsg != null && ObjErrMsg.Value != null)
                    errMsg = Convert.ToString(ObjErrMsg.Value);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUserInterestedStream in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

       // Method to get The Intertested Stream List

        private DataSet GetUserIntertestedStreamList(int userId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetStudentPriotyList");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUserIntertestedStreamList in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

       // Method to Get The Intertested Intertested College Stream List
         public DataTable GetStudentIntertestedStreamList(int collegeBranchCourseId,int userId)
        {
            _dataSet = new DataSet();
            DataTable DT=new DataTable();
            try
            {
                 _dataSet = GetUserIntertestedStreamList(userId);
                DT=  _dataSet.Tables[0].AsEnumerable().Where(result => result.Field<int>("AjCollegeBranchCourseId") == collegeBranchCourseId).CopyToDataTable();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetStudentIntertestedStreamList in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return DT;
        }

       // Method to Update The Update the User Details Transctional 
       public int InsertUpdateUserTransctionalDetails(int userId, string fromNumber, bool paymentStatus = false,
                                                      string transctionMode = "", string bankName = "",
                                                      string transctionId = "", string transAmount = "1100",int ? packageId=null)
       {
           int i = 0;
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);


           try
           {
               _objDataWrapper.AddParameter("@UserId", userId);
               _objDataWrapper.AddParameter("@FromNumber", fromNumber);
               _objDataWrapper.AddParameter("@PaymentStatus", paymentStatus);
               _objDataWrapper.AddParameter("@TransdctionMode", transctionMode);
               _objDataWrapper.AddParameter("@BankName", bankName);
               _objDataWrapper.AddParameter("@TransctionId", transctionId);
               _objDataWrapper.AddParameter("@TransAmount", transAmount);
                _objDataWrapper.AddParameter("@PackageId", packageId);
               i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateUserTransctionalDetails");

           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateUserTransctionalDetails in Consulling.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return i;
       }

       //Method to check the Seat Avialibity
         public DataSet CheckCollegeSeatAvialibity(int collegeBranchCourseId, int marksCheck,out int dataStatus,out string errMsg)
         {
             _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
             errMsg="";
             dataStatus=0;

             try
             {
                 _objDataWrapper.AddParameter("@CollegeBranchCourseId", collegeBranchCourseId);
                 _objDataWrapper.AddParameter("@Marks", marksCheck);
                 var objErrMsg =
                     (SqlParameter)
                     (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                 var objDataStatus =
                     (SqlParameter)
                     (_objDataWrapper.AddParameter("@ContentData", "", SqlDbType.Int, ParameterDirection.Output));
                 _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_CheckCollegeSeatAvailability");
                 if (objErrMsg != null && objErrMsg.Value != null)
                 {
                     errMsg = Convert.ToString(objErrMsg.Value);
                 }
                 if (objDataStatus != null && objDataStatus.Value != null)
                 {
                     dataStatus = Convert.ToInt16(objDataStatus.Value);
                 }
             }
             catch (Exception ex)
             {
                 var err = ex.Message;
                 if (ex.InnerException != null)
                 {
                     err = err + " :: Inner Exception :- " + ex.ToString();
                 }
                 const string addInfo = "Error while executing CheckCollegeSeatAvialibity in Consulling.cs  :: -> ";
                 var objPub = new ClsExceptionPublisher();
                 objPub.Publish(err, addInfo);
             }
             return _dataSet;
         }
         public DataTable GetMaximiumMarksByCourseId(int courseId,int userId)
         {
             _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
             _dataSet = new DataSet();
             try
             {
                 _objDataWrapper.AddParameter("@CourseId", courseId);
                 _objDataWrapper.AddParameter("@UserId", userId);
                 _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetMaximumMarksByCourseId");
             }
             catch (Exception ex)
             {
                 var err = ex.Message;
                 if (ex.InnerException != null)
                 {
                     err = err + " :: Inner Exception :- " + ex.ToString();
                 }
                 const string addInfo = "Error while executing GetMaximiumMarksByCourseId in Consulling.cs  :: -> ";
                 var objPub = new ClsExceptionPublisher();
                 objPub.Publish(err, addInfo);
             }
             return _dataSet.Tables[0];

         }
         // Method to Insert Refund
         public DataSet GetBookedCollegeById(int collegeBranchCourseId)
         {
             _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
             _dataSet = new DataSet();
             try
             {
                 _objDataWrapper.AddParameter("@CollegeBranchCourseId", collegeBranchCourseId);

                 _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetBookedCollegeDetailsById");
             }
             catch (Exception ex)
             {
                 var err = ex.Message;
                 if (ex.InnerException != null)
                 {
                     err = err + " :: Inner Exception :- " + ex.ToString();
                 }
                 const string addInfo = "Error while executing GetBookedCollegeById in Consulling.cs  :: -> ";
                 var objPub = new ClsExceptionPublisher();
                 objPub.Publish(err, addInfo);
             }
             return _dataSet;

         }
         public int InsertUserRefund(string name, string userEmail, string formNo, out string errmsg)
         {
             int i = 0;
             _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
             errmsg = "";

             try
             {
                 _objDataWrapper.AddParameter("@name", name);
                 _objDataWrapper.AddParameter("@email", userEmail);
                 _objDataWrapper.AddParameter("@formno", formNo);
                 var ObjErrMsg =
                       (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                
                 i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateRefund");
                 if (ObjErrMsg != null && ObjErrMsg.Value != null)
                     errmsg = Convert.ToString(ObjErrMsg.Value);
                

             }
             catch (Exception ex)
             {
                 var err = ex.Message;
                 if (ex.InnerException != null)
         

                 {
                     err = err + " :: Inner Exception :- " + ex.ToString();
                 }
                 const string addInfo = "Error while executing InsertUserRefund in Consulling.cs  :: -> ";
                 var objPub = new ClsExceptionPublisher();
                 objPub.Publish(err, addInfo);
             }
             return i;
         }
         public DataTable GetAllRefundRequest()
         {
             _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
             _dataSet = new DataSet();
             try
             {

                 _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetRefundRequest");
             }
             catch (Exception ex)
             {
                 var err = ex.Message;
                 if (ex.InnerException != null)
                 {
                     err = err + " :: Inner Exception :- " + ex.ToString();
                 }
                 const string addInfo = "Error while executing GetAllRefundRequest in Consulling.cs  :: -> ";
                 var objPub = new ClsExceptionPublisher();
                 objPub.Publish(err, addInfo);
             }
             return _dataSet.Tables[0];

         }
         public List<BookSeat> GetBookedCollege()
         {
             _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
             _dataSet = new DataSet();
             var objBookSeatList = new List<BookSeat>();
             try
             {
                 _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetBookedColleges");
                 objBookSeatList = BindBookSeat(_dataSet.Tables[0]);
             }
             catch (Exception ex)
             {
                 var err = ex.Message;
                 if (ex.InnerException != null)
                 {
                     err = err + " :: Inner Exception :- " + ex.ToString();
                 }
                 const string addInfo = "Error while executing GetBookedCollege in Consulling.cs  :: -> ";
                 var objPub = new ClsExceptionPublisher();
                 objPub.Publish(err, addInfo);
             }
             return objBookSeatList;
         }
         public List<BookSeat> GetBookedCollegeByBookSeatId(int bookSeatId)
         {
             _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
             _dataSet = new DataSet();
             var objBookSeatList = new List<BookSeat>();
             try
             {
                 _objDataWrapper.AddParameter("@BookSeatId", bookSeatId);
                 _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetBookedColleges");
                 objBookSeatList = BindBookSeat(_dataSet.Tables[0]);
             }
             catch (Exception ex)
             {
                 var err = ex.Message;
                 if (ex.InnerException != null)
                 {
                     err = err + " :: Inner Exception :- " + ex.ToString();
                 }
                 const string addInfo = "Error while executing GetBookedCollegeByBookSeatId in Consulling.cs  :: -> ";
                 var objPub = new ClsExceptionPublisher();
                 objPub.Publish(err, addInfo);
             }
             return objBookSeatList.ToList();
         }
         private List<BookSeat> BindBookSeat(DataTable datatable)
         {
             var objBookSeatList = new List<BookSeat>();
             try
             {
                 if (datatable.Rows.Count > 0)
                 {
                     for (var j = 0; j < datatable.Rows.Count; j++)
                     {

                         var objBookSeat = new BookSeat
                         {
                             BookSeatId = (datatable.Rows[j]["AjBookSeatId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjBookSeatId"]),
                             BookSeatAmount = (datatable.Rows[j]["AjBookSeatAmount"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjBookSeatAmount"]),
                             BookSeatStatus = (!(datatable.Rows[j]["AjBookSeatStatus"] is DBNull)) && Convert.ToBoolean(datatable.Rows[j]["AjBookSeatStatus"]),

                             Eligibity10 = (datatable.Rows[j]["Aj10Eligibilitypercentage"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["Aj10Eligibilitypercentage"]),

                             Eligibity12 = (datatable.Rows[j]["Aj12Eligibilitypercentage"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["Aj12Eligibilitypercentage"]),

                             Eligibity15 = (datatable.Rows[j]["Aj15Eligibilitypercentage"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["Aj15Eligibilitypercentage"]),
                             Instruction = (datatable.Rows[j]["AjInstruction"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjInstruction"]),
                             BookSeatStartDate = (datatable.Rows[j]["AjBookSeatStartDate"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjBookSeatStartDate"]),
                             BookSeatEndDate = (datatable.Rows[j]["AjBookSeatEndDate"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjBookSeatEndDate"]),
                             CollegeBasicInfo = new CollegeBranchProperty()
                                                    {
                                                        CollegeIdBranchId = (datatable.Rows[j]["AjCollegeBranchId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchId"]),
                                                        CollegeBranchName = (datatable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),
                                                        CollegeBranchLogo = (datatable.Rows[j]["AjCollegeBranchLogo"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchLogo"]),
                                                        CollegeBranchEst= (datatable.Rows[j]["AjCollegeBranchEst"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchEst"]),
                                                    },

                             CollegeBranchCourse = new CollegeBranchCourseProperty()
                             {
                                 CollegeBranchCourseId = (datatable.Rows[j]["AjCollegeBranchCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseId"]),
                                 CollegeBranchCourseSponserStatus = (!(datatable.Rows[j]["AjCollegeSponser"] is DBNull)) && Convert.ToBoolean(datatable.Rows[j]["AjCollegeSponser"]),
                                 CollegeBranchCourseOnlineStatus = (!(datatable.Rows[j]["AjCollegeOnlineParticipate"] is DBNull)) && Convert.ToBoolean(datatable.Rows[j]["AjCollegeOnlineParticipate"]),
                                 IsBookSeatVisible=!string.IsNullOrEmpty(Convert.ToString(datatable.Rows[j]["AjIsBookSeatVisible"]))?Convert.ToBoolean(datatable.Rows[j]["AjIsBookSeatVisible"]):false
                             },
                             CityMaster = new CityProperty()
                                              {
                                                  CityId = (datatable.Rows[j]["AjCityId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCityId"]),
                                                  CityName = (datatable.Rows[j]["AjCityName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCityName"]),
                                                  StateId = (datatable.Rows[j]["AjStateId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjStateId"]),
                                              },
                             CourseMaster = new CourseMasterProperty()
                                              {
                                                  CourseId = (datatable.Rows[j]["AjCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCourseId"]),
                                                  CourseName = (datatable.Rows[j]["AjCourseName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCourseName"])

                                              },
                            CourseEligibity = new CourseEligibiltyProperty()
                           {
                               CourseEligibilityId = (datatable.Rows[j]["AjCollegeCourseEligibiltyId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeCourseEligibiltyId"]),
                               CourseEligibiltyName = (datatable.Rows[j]["AjCollegeCourseEligibiltyName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeCourseEligibiltyName"]),

                           }

                         };
                         objBookSeatList.Add(objBookSeat);
                     }
                 }
             }
             catch (Exception ex)
             {
                 var err = ex.Message;
                 if (ex.InnerException != null)
                 {
                     err = err + " :: Inner Exception :- " + ex.ToString();
                 }
                 const string addInfo = "Error while executing BindBookSeat in Consulling.cs  :: -> ";
                 var objPub = new ClsExceptionPublisher();
                 objPub.Publish(err, addInfo);
             }
             return objBookSeatList;
         }
    }
}
