using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using IryTech.AdmissionJankari.DAL;

using IryTech.AdmissionJankari.BO;
using System.Collections.Generic;
using System.IO;

namespace IryTech.AdmissionJankari.BL
{
   public class College:CollegeProvider
   {
       private DbWrapper _objDataWrapper;
       private DataSet _dataSet;
       private int _i;
       public override List<CollegeBranchProperty> GetBookedCollegeList(string collegeName)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeName", collegeName);
               _objDataWrapper.AddParameter("@CourseId",0);
               _objDataWrapper.AddParameter("@BranchCourseId",0);
               _objDataWrapper.AddParameter("@case",null);
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeListByCourse");
               objCollegeBranchList = BindCollegeListByName(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetBookedCollegeList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }
       public override List<CollegeBranchProperty> GetBookSeatCollege(int courseId, int pageNum, int pageSize, out int totalRecords)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           totalRecords = 0;
           var objCollegeBranchList = new List<CollegeBranchProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CourseId", courseId);
               _objDataWrapper.AddParameter("@PageNum", pageNum);
               _objDataWrapper.AddParameter("@PageSize", pageSize);
               var objRoralRecords = (SqlParameter)
                   (_objDataWrapper.AddParameter("@TotalRecords", "", SqlDbType.Int, ParameterDirection.Output));
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_CheckCollegeBookSeatStatus");
               if (objRoralRecords != null && objRoralRecords.Value != null)
                   totalRecords = Convert.ToInt32(objRoralRecords.Value);
               objCollegeBranchList = BindCollegeList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetBookSeatCollege in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }
       public override List<CollegeBranchCoursePlacementProperty> GetCollegeCourseTopHirerListByCollegeId(int collegeId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchCoursePlacementProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchId", collegeId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeTopHirer");
               objCollegeBranchList = BindTopHirer(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseTopHirerListByCollegeId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }

       public override int UpdateCollegePlacementByCourse(CollegeBranchCoursePlacementProperty objCollegeBranchCoursePlacementProperty, int modifiedby, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@PlacementId", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementId);
               _objDataWrapper.AddParameter("@CollegeName", objCollegeBranchCoursePlacementProperty.CollegeBranchName);
               _objDataWrapper.AddParameter("@CourseId", objCollegeBranchCoursePlacementProperty.CourseId);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementCompanyName", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementCompanyName);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementYear", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementYear);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementNoOfStudentHired", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementNoOfStudentHired);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementAvgSalary", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementAvgSalaryOffered);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementStatus", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementStatus);
               _objDataWrapper.AddParameter("@modifiedBy", modifiedby);

               var ObjErrMsg =
                       (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegePlacementByCourse");
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
               const string addInfo = "Error while executing InsertCollegePlacementByCourse in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }
       public override int InsertCollegePlacementByCourse(CollegeBranchCoursePlacementProperty objCollegeBranchCoursePlacementProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {

               _objDataWrapper.AddParameter("@CollegeName", objCollegeBranchCoursePlacementProperty.CollegeBranchName);
               _objDataWrapper.AddParameter("@CourseId", objCollegeBranchCoursePlacementProperty.CourseId);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementCompanyName", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementCompanyName);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementYear", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementYear);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementNoOfStudentHired", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementNoOfStudentHired);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementAvgSalary", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementAvgSalaryOffered);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementStatus", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);

               var ObjErrMsg =
                       (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegePlacementByCourse");
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
               const string addInfo = "Error while executing InsertCollegePlacementByCourse in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }
       public override List<CollegeBranchProperty> GetCollegeListByUserId(int userId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchProperty>();
           try
           {
               _objDataWrapper.AddParameter("@UserId", userId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeDetailsByUserId");
               objCollegeBranchList = BindCollegeListByName(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeListByUserId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }
       public override List<LeadSourceProperty> GetLeadCollegeNameList(int courseId, string strStream, string strCities, bool findByCities, bool participated)
       {
           var objRankSourcePropertyList = new List<LeadSourceProperty>();
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           try
           {
               _objDataWrapper.AddParameter("@CourseId", courseId);
               _objDataWrapper.AddParameter("@Stream", strStream);
               _objDataWrapper.AddParameter("@PreferedCities", strCities);
               _objDataWrapper.AddParameter("@CheckPreferedCities", findByCities);
               _objDataWrapper.AddParameter("@CheckParticipatedColleges", participated);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeNameByQuery");
               objRankSourcePropertyList = BindLeadCollegeNameList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetLeadCollegeNameList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objRankSourcePropertyList;
       }

       public override List<LeadSourceProperty> GetLeadCollegeList(int courseId, string strStream, string strCities, bool findByCities, bool participated)
       {
           var objRankSourcePropertyList = new List<LeadSourceProperty>();
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           try
           {
               _objDataWrapper.AddParameter("@CourseId", courseId);
               _objDataWrapper.AddParameter("@Stream", strStream);
               _objDataWrapper.AddParameter("@PreferedCities", strCities);
               _objDataWrapper.AddParameter("@CheckPreferedCities", findByCities);
               _objDataWrapper.AddParameter("@CheckParticipatedColleges", participated);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeListByQuery");
               objRankSourcePropertyList = BindLeadCollegeList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeRankSourceListById in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objRankSourcePropertyList;
       }

       public override List<Factor> GetFactorValues(int branchCourseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<Factor>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", branchCourseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetFactorValues");
               objCollegeBranchList = BindFactorValues(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetFactorValues in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }
       public override int UpdateCollegeCourseOnlineParticipationAndRating(int collegebranchCourseId, bool onlineParticipation, bool onlineParticipationVirtual, string factorId, string factorValues, double avgRating, string admissionDate, int modifiedBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@OnlineParticipation", onlineParticipation);
               _objDataWrapper.AddParameter("@OnlineParticipationVirtual", onlineParticipationVirtual);
               _objDataWrapper.AddParameter("@CollegebranchCourseId", collegebranchCourseId);
               _objDataWrapper.AddParameter("@FactorId", factorId);
               _objDataWrapper.AddParameter("@FactorValues", factorValues);
               _objDataWrapper.AddParameter("@avgRating", avgRating);
               _objDataWrapper.AddParameter("@AdmissionDate", Common.GetDateFromString(admissionDate));
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
               var errMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_UpdateRankingAndOnlineStatus");
               if (errMsg != null && errMsg.Value != null)
                   errmsg = Convert.ToString(errMsg.Value);


           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateCollegeCourseOnlineParticipation in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }
       public override int UpdateCollegeCourseOnlineParticipation(int collegebranchCourseId, bool onlineParticipation, bool onlineParticipationVirtual,string admissionDate ,int modifiedBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@OnlineParticipation", onlineParticipation);
               _objDataWrapper.AddParameter("@AdmissionDate", Common.GetDateFromString(admissionDate));
               _objDataWrapper.AddParameter("@OnlineParticipationVirtual", onlineParticipationVirtual);
               _objDataWrapper.AddParameter("@CollegebranchCourseId", collegebranchCourseId);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
               var errMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_UpdateCollegeOnlineCounsellingParticipation");
               if (errMsg != null && errMsg.Value != null)
                   errmsg = Convert.ToString(errMsg.Value);


           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateCollegeCourseOnlineParticipation in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }
       public override List<CollegeBranchRankProperty> BindCollegeRank(int branchCourseId, int year)
       {

           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCoursePlacementProperty = new List<CollegeBranchRankProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", branchCourseId);
               _objDataWrapper.AddParameter("@Year", year);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_BindCollegeRank");
               objCollegeBranchCoursePlacementProperty = BindRankProperty(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing BindCollegeRank in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCoursePlacementProperty;
       }
       public override  List<CollegeBranchRankProperty> BindCollegeRankYear(int branchCourseId)
       {

           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCoursePlacementProperty = new List<CollegeBranchRankProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", branchCourseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_BindCollegeRankYear");
               objCollegeBranchCoursePlacementProperty = BindRankProperty(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing BindCollegeRankYear in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCoursePlacementProperty;
       }
       public override   List<CollegeBranchGallery> GetCollegeImageGallery(int branchCourseId)
       {

           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCoursePlacementProperty = new List<CollegeBranchGallery>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", branchCourseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeImageGallery");
               objCollegeBranchCoursePlacementProperty = BindCollegeGallery(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeImageGallery in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCoursePlacementProperty;
       }
       public override List<CollegeBranchCoursePlacementProperty> GetCollegeTopHirer(int collegeBranchCourseId)
       {

           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCoursePlacementProperty = new List<CollegeBranchCoursePlacementProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", collegeBranchCourseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeTopHirer");
               objCollegeBranchCoursePlacementProperty = BindTopHirer(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeTopHirer in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCoursePlacementProperty;
       }




       public override List<CollegeBranchCoursePlacementProperty> GetCollegeTopHirerByPlacementID(int collegePlacementID)
       {

           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCoursePlacementProperty = new List<CollegeBranchCoursePlacementProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchPlacementId", collegePlacementID);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeTopHirer");
               objCollegeBranchCoursePlacementProperty = BindTopHirer(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeTopHirer in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCoursePlacementProperty;
       }


       public override List<CollegeBranchKeySpeech> GetDirectorSpeechByBranchCourseId(int collegeBranchCourseId)
       {

           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchKeySpeech = new List<CollegeBranchKeySpeech>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", collegeBranchCourseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetDirecotrSpeechByBranchCourseId");
               objCollegeBranchKeySpeech = BindCollegeDirectorSpeech(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetDirectorSpeechByBranchCourseId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchKeySpeech;
       }
       public override List<CollegeBranchCourseHighlightsProperty> GetCollegeHighLightsByBranchCourseId(int collegeBranchCourseId)
       {

           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseHighlightsProperty = new List<CollegeBranchCourseHighlightsProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", collegeBranchCourseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeHighLightsByBranchCourseId");
               objCollegeBranchCourseHighlightsProperty = BindHighLights(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeHighLightsByBranchCourseId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseHighlightsProperty;
       }

       public override int InsertCollegeGroupDetails(CollegeGroupProperty objCollegeGroupProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CollegeGroupName", objCollegeGroupProperty.CollegeGroupName);
               _objDataWrapper.AddParameter("@CollegeGroupLogo", objCollegeGroupProperty.CollegeGroupLogo);
               _objDataWrapper.AddParameter("@CollegeGroupStatus", objCollegeGroupProperty.CollegeGropuStatus );
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);
               var errMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeGroup");
               if (errMsg != null && errMsg.Value != null)
                   errmsg = Convert.ToString(errMsg.Value);


           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing InsertCollegeGroupDetails in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }
       public override int UpdateCollegeAssociation(string collegeName, int courseId,int associationTypeId,System.DateTime advstStartDate,System.DateTime advstEndDate,int advstPriorty,string advstURL, int createdBy,bool advstStatus, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@courseId", courseId);
               _objDataWrapper.AddParameter("@AssociationId", associationTypeId);
               _objDataWrapper.AddParameter("@collegeBranchName", collegeName);
               _objDataWrapper.AddParameter("@ModifiedBy", createdBy);
               _objDataWrapper.AddParameter("@AdvstStartDate", advstStartDate);
               _objDataWrapper.AddParameter("@AdvstEndDate", advstEndDate);
               _objDataWrapper.AddParameter("@AdvstPrioty", advstPriorty);
               _objDataWrapper.AddParameter("@AdvstURL", advstURL);
               _objDataWrapper.AddParameter("@AdvstStatus", advstStatus);
               var errMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_UpdateCollegeAssociation");
               if (errMsg != null && errMsg.Value != null)
                   errmsg = Convert.ToString(errMsg.Value);


           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateCollegeAssociation in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }
       public override int UpdateCollegeGroupDetails(CollegeGroupProperty objCollegeGroupProperty, int modifiedBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CollegeGroupId", objCollegeGroupProperty.CollegeGroupId);
               _objDataWrapper.AddParameter("@CollegeGroupName", objCollegeGroupProperty.CollegeGroupName);
               _objDataWrapper.AddParameter("@CollegeGroupLogo", objCollegeGroupProperty.CollegeGroupLogo);
               _objDataWrapper.AddParameter("@CollegeGroupStatus", objCollegeGroupProperty.CollegeGropuStatus);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
               var errMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeGroup");
               if (errMsg != null && errMsg.Value != null)
                   errmsg = Convert.ToString(errMsg.Value);


           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateCollegeGroupDetails in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override List<CollegeGroupProperty> GetAllCollegeGroupList()
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeGroupList = new List<CollegeGroupProperty>();
           try
           {
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeGroupList");
               objCollegeGroupList = BindCollegeGroupObjectList(_dataSet.Tables[0]);
           }
           catch(Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetAllCollegeGroupList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }

       public override List<CollegeGroupProperty> GetCollegeGroupListById(int collegeGroupId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeGroupList = new List<CollegeGroupProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeGroupId", collegeGroupId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeGroupList");
               objCollegeGroupList = BindCollegeGroupObjectList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeGroupListById in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }

       public override List<CollegeGroupProperty> GetCollegeGroupListByGroupName(string collegeGroupName)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeGroupList = new List<CollegeGroupProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeGroupName", collegeGroupName);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeGroupList");
               objCollegeGroupList = BindCollegeGroupObjectList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeGroupListByGroupName in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }

       public override int InsertInstituteTypeDetails(InstituteTypeProperty objInstituteType, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@InstituteType", objInstituteType.InstituteType);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);
               var errMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateInstituteType");
               if (errMsg != null && errMsg.Value != null)
                   errmsg = Convert.ToString(errMsg.Value);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing InsertInstituteTypeDetails in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int UpdateInstituteTypeDetails(InstituteTypeProperty objInstituteType, int modifiedBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@InstituteTypeId", objInstituteType.InstituteTypeId);
               _objDataWrapper.AddParameter("@InstituteType", objInstituteType.InstituteType);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
               var errMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateInstituteType");
               if (errMsg != null && errMsg.Value != null)
                   errmsg = Convert.ToString(errMsg.Value);


           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateInstituteTypeDetails in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override List<InstituteTypeProperty> GetAllInstituteTypeList()
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objInstituteTypeList = new List<InstituteTypeProperty>();
           try
           {
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetInstituteTypeList");
               objInstituteTypeList = BindInstituteTypeObjectList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetAllInstituteTypeList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objInstituteTypeList;
       }
       //Code added by Abhishek
       public override List<CollegeBranchProperty> GetAllSponserCollegeList()
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objSponserCollegelist = new List<CollegeBranchProperty>();
           try
           {
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetSponserCollegeName");
               objSponserCollegelist = BindSponserCollegeObjectList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetAllSponserCollegeList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objSponserCollegelist;
       }


       //Code added by Abhishek
       public override List<CollegeBranchProperty> GetAllSponserCollegeListbyCourseID(int CourseID)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objSponserCollegelist = new List<CollegeBranchProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CourseId", CourseID);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetSponserCollegeName");
               objSponserCollegelist = BindSponserCollegeObjectListbyCourseId(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetAllSponserCollegeList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objSponserCollegelist;
       }





       public override List<InstituteTypeProperty> GetInstituteTypeId(int instituteTypeId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objInstituteTypeList = new List<InstituteTypeProperty>();
           try
           {
               _objDataWrapper.AddParameter("@InstituteTypeId", instituteTypeId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetInstituteTypeList");
               objInstituteTypeList = BindInstituteTypeObjectList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetInstituteTypeId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objInstituteTypeList;
       }

       public override int InsertCollegeAssociationCategoryType(CollegeAssociationCategoryProperty objCollegeAssociationCategoryProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@AssociationCategoryType", objCollegeAssociationCategoryProperty.AssociationCategoryType);
               _objDataWrapper.AddParameter("@AssociationCategoryTypeStatus", objCollegeAssociationCategoryProperty.AssociationCategoryStatus);
               _objDataWrapper.AddParameter("@AssociationCategoryAmount", objCollegeAssociationCategoryProperty.AssociationCategoryAmount);
               _objDataWrapper.AddParameter("@AssociationACategoryDesc", objCollegeAssociationCategoryProperty.AssociationCategoryDescription);
               _objDataWrapper.AddParameter("@AssCase", objCollegeAssociationCategoryProperty.AssociationType);

               _objDataWrapper.AddParameter("@CreatedBy", createdBy);
               var errMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeAssociationCategoryType");
               if (errMsg != null && errMsg.Value != null)
                   errmsg = Convert.ToString(errMsg.Value);


           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing InsertCollegeAssociationCategoryType in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int UpdateCollegeAssociationCategoryType(CollegeAssociationCategoryProperty objCollegeAssociationCategoryProperty, int modifiedBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@AssociationCategoryTypeId", objCollegeAssociationCategoryProperty.AssociationCategoryTypeId);
               _objDataWrapper.AddParameter("@AssociationCategoryType", objCollegeAssociationCategoryProperty.AssociationCategoryType);
               _objDataWrapper.AddParameter("@AssociationCategoryTypeStatus", objCollegeAssociationCategoryProperty.AssociationCategoryStatus);
               _objDataWrapper.AddParameter("@AssociationCategoryAmount", objCollegeAssociationCategoryProperty.AssociationCategoryAmount);
               _objDataWrapper.AddParameter("@AssociationACategoryDesc", objCollegeAssociationCategoryProperty.AssociationCategoryDescription);
               _objDataWrapper.AddParameter("@AssCase", objCollegeAssociationCategoryProperty.AssociationType);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
               var errMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeAssociationCategoryType");
               if (errMsg != null && errMsg.Value != null)
                   errmsg = Convert.ToString(errMsg.Value);


           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateCollegeAssociationCategoryType in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }
      

       public override List<CollegeAssociationCategoryProperty> GetAllCollegeAssociationCategoryType(string iCase="T")
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objAssociationCategoryList = new List<CollegeAssociationCategoryProperty>();
           try
           {
               _objDataWrapper.AddParameter("@iCase", iCase);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeAssociationCategoryTypeList");

               objAssociationCategoryList = iCase.Equals("T") ? BindAssociationCategoryObjectList(_dataSet.Tables[0]) : BindAssociationDisplayObjectList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetAllCollegeAssociationCategoryType in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objAssociationCategoryList;

       }

       public override List<CollegeAssociationCategoryProperty> GetCollegeAssociationCategoryTypeById(int categoryId,string iCase="T")
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objAssociationCategoryList = new List<CollegeAssociationCategoryProperty>();
           try
           {
               _objDataWrapper.AddParameter("@iCase", iCase);
               _objDataWrapper.AddParameter("@AssociationCategoryId", categoryId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeAssociationCategoryTypeList");
               objAssociationCategoryList = iCase.Equals("T") ? BindAssociationCategoryObjectList(_dataSet.Tables[0]) : BindAssociationDisplayObjectList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeAssociationCategoryTypeById in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objAssociationCategoryList;
       }

       public override int InsertHostelCategory(HostelCategoryProperty objHostelCategoryProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@HostelCategoryName", objHostelCategoryProperty.HostelCategoryType);
               _objDataWrapper.AddParameter("@HostelCategoryStatus", objHostelCategoryProperty.HostelCategoryStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);
               var errMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateHostelCategory");
               if (errMsg != null && errMsg.Value != null)
                   errmsg = Convert.ToString(errMsg.Value);


           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing InsertHostelCategory in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int UpdateHostelCategory(HostelCategoryProperty objHostelCategoryProperty, int modifiedBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@HostelCategoryId", objHostelCategoryProperty.HostelCategoryId);
               _objDataWrapper.AddParameter("@HostelCategoryName", objHostelCategoryProperty.HostelCategoryType);
               _objDataWrapper.AddParameter("@HostelCategoryStatus", objHostelCategoryProperty.HostelCategoryStatus);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
               var errMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateHostelCategory");
               if (errMsg != null && errMsg.Value != null)
                   errmsg = Convert.ToString(errMsg.Value);


           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateHostelCategory in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override List<HostelCategoryProperty> GetAllHostelCategory()
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objHostelCategoryPropertyList = new List<HostelCategoryProperty>();
           try
           {
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetHostelCategoryList");
               objHostelCategoryPropertyList=BindHostelCategoryObjectList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetAllHostelCategory in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objHostelCategoryPropertyList;
                 
          
       }

       public override List<HostelCategoryProperty> GetHostelCategoryListById(int hostelCategoryId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objHostelCategoryPropertyList = new List<HostelCategoryProperty>();
           try
           {
               _objDataWrapper.AddParameter("@HostelCategoryId", hostelCategoryId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetHostelCategoryList");
               objHostelCategoryPropertyList = BindHostelCategoryObjectList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetAllHostelCategory in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objHostelCategoryPropertyList;
       }

       public override int InsertCollegeRankSource(CollegeRankSource objCollegeRankSource, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@RankSourceName", objCollegeRankSource.CollegeRankSourceName);
               _objDataWrapper.AddParameter("@RankSourceStatus", objCollegeRankSource.CollegeRankSourceStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);
               var errMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateRankSource");
               if (errMsg != null && errMsg.Value != null)
                   errmsg = Convert.ToString(errMsg.Value);


           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing InsertCollegeRankSource in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int UpdateCollegeRankSource(CollegeRankSource objCollegeRankSource, int modifiedBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@RankSourceId", objCollegeRankSource.CollegeRankSourceId);
               _objDataWrapper.AddParameter("@RankSourceName", objCollegeRankSource.CollegeRankSourceName);
               _objDataWrapper.AddParameter("@RankSourceStatus", objCollegeRankSource.CollegeRankSourceStatus);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
               var errMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateRankSource");
               if (errMsg != null && errMsg.Value != null)
                   errmsg = Convert.ToString(errMsg.Value);


           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateCollegeRankSource in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override List<CollegeRankSource> GetAllCollegeRankSourceList()
       {
           var objRankSourcePropertyList = new List<CollegeRankSource>();
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           try
           {
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetRankSourceList");
               objRankSourcePropertyList = BindRankSourceObjectList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetAllCollegeRankSourceList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objRankSourcePropertyList;

       }

       public override List<CollegeRankSource> GetCollegeRankSourceListById(int collegeRankSourceId)
       {
           var objRankSourcePropertyList = new List<CollegeRankSource>();
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           try
           {
               _objDataWrapper.AddParameter("@RankSourceId", collegeRankSourceId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetRankSourceList");
               objRankSourcePropertyList = BindRankSourceObjectList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeRankSourceListById in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objRankSourcePropertyList;
       }

       public override int InsertCollegeLogin(string collegeName, int userId, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg="";
           try
           {
               _objDataWrapper.AddParameter("@CollegeName", collegeName);
               _objDataWrapper.AddParameter("@UserId", userId);
               var ObjErrMsg = 
                        (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i=_objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertCollegeLogin");

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
               const string addInfo = "Error while executing InsertCollegeLogin in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }

           return _i;
       }

       public override int InsertCollegeBranchInfo(CollegeBranchProperty objCollegeBranchProperty, int createdBy, out string errmsg, out int collegeBranchId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           collegeBranchId=0;
           try
           {
               _objDataWrapper.AddParameter("@InstituteTypeId", objCollegeBranchProperty.InstituteTypeId);
              
               _objDataWrapper.AddParameter("@CollegeGroupId", objCollegeBranchProperty.CollegeGroupId );
               _objDataWrapper.AddParameter("@CollegeBranchName", objCollegeBranchProperty.CollegeBranchName);
               _objDataWrapper.AddParameter("@CollegeBranchPopularName", objCollegeBranchProperty.CollegePopulaorName);
               _objDataWrapper.AddParameter("@ManagementId", objCollegeBranchProperty.CollegeManagementTypeId);
               _objDataWrapper.AddParameter("@CollegeBranchEst", objCollegeBranchProperty.CollegeBranchEst);
               _objDataWrapper.AddParameter("@CollegeBranchDesc", objCollegeBranchProperty.CollegeBranchDesc);
               _objDataWrapper.AddParameter("@CollegeBranchCountryId", objCollegeBranchProperty.CollegeBranchCountryId);
               _objDataWrapper.AddParameter("@CollegeBranchStateId", objCollegeBranchProperty.CollegeBranchStateId);
               _objDataWrapper.AddParameter("@CollegeBranchCityID", objCollegeBranchProperty.CollegeBranchCityId);
               _objDataWrapper.AddParameter("@CollegeBranchAddrs", objCollegeBranchProperty.CollegeBranchAddrs);
               _objDataWrapper.AddParameter("@CollegeBranchPinCode", objCollegeBranchProperty.CollegeBranchPinCode);
               _objDataWrapper.AddParameter("@CollegeBranchMobileNo", objCollegeBranchProperty.CollegeBranchMobileNo);
               _objDataWrapper.AddParameter("@CollegeBranchFax", objCollegeBranchProperty.CollegeBranchFax);
               _objDataWrapper.AddParameter("@CollegeBranchEmailId", objCollegeBranchProperty.CoillegeBranchEmailId);
               _objDataWrapper.AddParameter("@CollegeBranchWebsite", objCollegeBranchProperty.CollegeBranchWebsite);
               _objDataWrapper.AddParameter("@CollegeBranchStatus", objCollegeBranchProperty.CollegeBranchStatus);
               _objDataWrapper.AddParameter("@CollegeBranchLogo", objCollegeBranchProperty.CollegeBranchLogo);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);
              
               var ObjCollegeId = 
                        (SqlParameter)(_objDataWrapper.AddParameter("@CollegeBranchId", 0, SqlDbType.Int, ParameterDirection.InputOutput));
               var ObjErrMsg=
                        (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg","",SqlDbType.VarChar,ParameterDirection.Output,128));

                _i=_objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeBasicInfo");
               if(ObjCollegeId!=null && ObjCollegeId.Value!=null)
                    collegeBranchId=Convert.ToInt32(ObjCollegeId.Value);
               if(ObjErrMsg!=null && ObjErrMsg.Value!=null)
                    errmsg=Convert.ToString(ObjErrMsg.Value);
                
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing InsertCollegeBranchInfo in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int UpdateCollegeBranchInfo(CollegeBranchProperty objCollegeBranchProperty, int modifiedBy, out string errmsg, out int collegeBranchId)
       {
          _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           collegeBranchId=0;
           try
           {
                _objDataWrapper.AddParameter("@InstituteTypeId", objCollegeBranchProperty.InstituteTypeId);
             _objDataWrapper.AddParameter("@CollegeGroupId", objCollegeBranchProperty.CollegeGroupId );
               _objDataWrapper.AddParameter("@CollegeBranchName", objCollegeBranchProperty.CollegeBranchName);
               _objDataWrapper.AddParameter("@CollegeBranchPopularName", objCollegeBranchProperty.CollegePopulaorName);
               _objDataWrapper.AddParameter("@ManagementId", objCollegeBranchProperty.CollegeManagementTypeId);
               _objDataWrapper.AddParameter("@CollegeBranchEst", objCollegeBranchProperty.CollegeBranchEst);
               _objDataWrapper.AddParameter("@CollegeBranchDesc", objCollegeBranchProperty.CollegeBranchDesc);
               _objDataWrapper.AddParameter("@CollegeBranchCountryId", objCollegeBranchProperty.CollegeBranchCountryId);
               _objDataWrapper.AddParameter("@CollegeBranchStateId", objCollegeBranchProperty.CollegeBranchStateId);
               _objDataWrapper.AddParameter("@CollegeBranchCityID", objCollegeBranchProperty.CollegeBranchCityId);
               _objDataWrapper.AddParameter("@CollegeBranchAddrs", objCollegeBranchProperty.CollegeBranchAddrs);
               _objDataWrapper.AddParameter("@CollegeBranchPinCode", objCollegeBranchProperty.CollegeBranchPinCode);
               _objDataWrapper.AddParameter("@CollegeBranchMobileNo", objCollegeBranchProperty.CollegeBranchMobileNo);
               _objDataWrapper.AddParameter("@CollegeBranchFax", objCollegeBranchProperty.CollegeBranchFax);
               _objDataWrapper.AddParameter("@CollegeBranchLogo", objCollegeBranchProperty.CollegeBranchLogo);
               _objDataWrapper.AddParameter("@CollegeBranchEmailId", objCollegeBranchProperty.CoillegeBranchEmailId);
               _objDataWrapper.AddParameter("@CollegeBranchWebsite", objCollegeBranchProperty.CollegeBranchWebsite);
               _objDataWrapper.AddParameter("@CollegeBranchStatus", objCollegeBranchProperty.CollegeBranchStatus);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);

               var ObjCollegeId = 
                        (SqlParameter)(_objDataWrapper.AddParameter("@CollegeBranchId",objCollegeBranchProperty.CollegeIdBranchId, SqlDbType.Int, ParameterDirection.InputOutput));
               var ObjErrMsg=
                        (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg","",SqlDbType.VarChar,ParameterDirection.Output,128));

                _i=_objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeBasicInfo");
               if(ObjCollegeId!=null && ObjCollegeId.Value!=null)
                    collegeBranchId=Convert.ToInt32(ObjCollegeId.Value);
               if(ObjErrMsg!=null && ObjErrMsg.Value!=null)
                    errmsg=Convert.ToString(ObjErrMsg.Value);

           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateCollegeBranchInfo in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int InsertCollegeBranchCourseInfo(CollegeBranchCourseProperty objCollegeBranchCourseProperty,
                                                         int createdBy, out string errmsg, out int collegeBranchCourseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           collegeBranchCourseId = 0;
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchId", objCollegeBranchCourseProperty.CollegeBranchId);
               _objDataWrapper.AddParameter("@CourseId", objCollegeBranchCourseProperty.CourseId);
               _objDataWrapper.AddParameter("@UniversityId", objCollegeBranchCourseProperty.UniversityId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseDesc",
                                            objCollegeBranchCourseProperty.CollegeBranchCourseDesc);
               _objDataWrapper.AddParameter("@CollegeBranchCourseEst",
                                            objCollegeBranchCourseProperty.CollegeBranchCourseEst);
               _objDataWrapper.AddParameter("@CollegeBranchHasHostel", objCollegeBranchCourseProperty.HasHostel);
               _objDataWrapper.AddParameter("@CollegeBranchUrl", objCollegeBranchCourseProperty.CollegeBranchCourseUrl);
               _objDataWrapper.AddParameter("@CollegeBranchTitle",
                                            objCollegeBranchCourseProperty.CollegeBranchCourseTitle);
               _objDataWrapper.AddParameter("@CollegeMetaDesc",
                                            objCollegeBranchCourseProperty.CollegeBranchCourseMetaDesc);
               _objDataWrapper.AddParameter("@CollegeMetaTag", objCollegeBranchCourseProperty.CollegeBranchCourseMetaTag);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStatus",
                                            objCollegeBranchCourseProperty.CollegeBranchCourseStatus);
               _objDataWrapper.AddParameter("@CollegeBranchCourseSponserStatus",
                                            objCollegeBranchCourseProperty.CollegeBranchCourseSponserStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);
               _objDataWrapper.AddParameter("@AjCollegeBranchCourseHelplineNo",
                                            objCollegeBranchCourseProperty.CollegeBranchCourseHelplineNo);
               _objDataWrapper.AddParameter("@AjIsBookSeatVisible", objCollegeBranchCourseProperty.IsBookSeatVisible);
               var ObjErrMsg =
                   (SqlParameter)
                   (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               var objCollegeBranchCourseId =
                   (SqlParameter)
                   (_objDataWrapper.AddParameter("@CollegeBranchCourseId", 0, SqlDbType.Int,
                                                 ParameterDirection.InputOutput));

               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeCourse");

               if (ObjErrMsg != null && ObjErrMsg.Value != null)
                   errmsg = Convert.ToString(ObjErrMsg.Value);
               if (objCollegeBranchCourseId != null && objCollegeBranchCourseId.Value != null)
                   collegeBranchCourseId = Convert.ToInt32(objCollegeBranchCourseId.Value);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing InsertCollegeBranchCourseInfo in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int UpdateCollegeBranchCourseInfo(CollegeBranchCourseProperty objCollegeBranchCourseProperty,
                                                         int modifiedBy, out string errmsg,
                                                         out int collegeBranchCourseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           collegeBranchCourseId = 0;
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchId", objCollegeBranchCourseProperty.CollegeBranchId);
               _objDataWrapper.AddParameter("@CourseId", objCollegeBranchCourseProperty.CourseId);
               _objDataWrapper.AddParameter("@UniversityId", objCollegeBranchCourseProperty.UniversityId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseDesc",
                                            objCollegeBranchCourseProperty.CollegeBranchCourseDesc);
               _objDataWrapper.AddParameter("@CollegeBranchCourseEst",
                                            objCollegeBranchCourseProperty.CollegeBranchCourseEst);
               _objDataWrapper.AddParameter("@CollegeBranchHasHostel", objCollegeBranchCourseProperty.HasHostel);
               _objDataWrapper.AddParameter("@CollegeBranchUrl", objCollegeBranchCourseProperty.CollegeBranchCourseUrl);
               _objDataWrapper.AddParameter("@CollegeBranchTitle",
                                            objCollegeBranchCourseProperty.CollegeBranchCourseTitle);
               _objDataWrapper.AddParameter("@CollegeMetaDesc",
                                            objCollegeBranchCourseProperty.CollegeBranchCourseMetaDesc);
               _objDataWrapper.AddParameter("@CollegeMetaTag", objCollegeBranchCourseProperty.CollegeBranchCourseMetaTag);
               _objDataWrapper.AddParameter("@CollegeBranchCourseSponserStatus",
                                            objCollegeBranchCourseProperty.CollegeBranchCourseSponserStatus);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStatus",
                                            objCollegeBranchCourseProperty.CollegeBranchCourseStatus);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
               _objDataWrapper.AddParameter("@AjCollegeBranchCourseHelplineNo",
                                            objCollegeBranchCourseProperty.CollegeBranchCourseHelplineNo);
                 _objDataWrapper.AddParameter("@AjIsBookSeatVisible", objCollegeBranchCourseProperty.IsBookSeatVisible);
               var ObjErrMsg =
                   (SqlParameter)
                   (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               var objCollegeBranchCourseId =
                   (SqlParameter)
                   (_objDataWrapper.AddParameter("@CollegeBranchCourseId",
                                                 objCollegeBranchCourseProperty.CollegeBranchCourseId, SqlDbType.Int,
                                                 ParameterDirection.InputOutput));

               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeCourse");

               if (ObjErrMsg != null && ObjErrMsg.Value != null)
                   errmsg = Convert.ToString(ObjErrMsg.Value);
               if (objCollegeBranchCourseId != null && objCollegeBranchCourseId.Value != null)
                   collegeBranchCourseId = Convert.ToInt32(objCollegeBranchCourseId.Value);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateCollegeBranchCourseInfo in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int InsertCollegeBranchCourseStreamInfo(CollegeBranchCourseStreamProperty objCollegeBranchCourseStreamProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@StreamId", objCollegeBranchCourseStreamProperty.StreamId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", objCollegeBranchCourseStreamProperty.CollegeBranchCourseId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamSeat", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamSeat);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamDuration", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamDuration);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamFees", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamFees);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamModeId", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamModeId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamEligibilty", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamEligibity );
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamDesc", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamDesc);
               _objDataWrapper.AddParameter("@CollegeBranchCourseMamagementQuotaSeat", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamManagementQuotaSeat);
               _objDataWrapper.AddParameter("@CollegeBranchCourseLateralEntrySeat", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamLateralEntrySeat);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamStatus", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);
               var ObjErrMsg =(SqlParameter)
                        (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeCourseStream");
               if (ObjErrMsg != null && ObjErrMsg.Value!=null)
                   errmsg = Convert.ToString(ObjErrMsg.Value);


           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing InsertCollegeBranchCourseStreamInfo in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }
       public override int InsertCollegeBranchCourseStreamInfoByCollegeId(CollegeBranchCourseStreamProperty objCollegeBranchCourseStreamProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CourseId", objCollegeBranchCourseStreamProperty.CourseId);
               _objDataWrapper.AddParameter("@StreamId", objCollegeBranchCourseStreamProperty.StreamId);
               _objDataWrapper.AddParameter("@CollegeId", objCollegeBranchCourseStreamProperty.CollegeBranchId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamSeat", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamSeat);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamDuration", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamDuration);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamFees", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamFees);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamModeId", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamModeId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamEligibilty", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamEligibity);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamDesc", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamDesc);
               _objDataWrapper.AddParameter("@CollegeBranchCourseMamagementQuotaSeat", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamManagementQuotaSeat);
               _objDataWrapper.AddParameter("@CollegeBranchCourseLateralEntrySeat", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamLateralEntrySeat);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamStatus", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);
               var ObjErrMsg = (SqlParameter)
                        (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeCourseStreamByCourseAndCollgeeId");
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
               const string addInfo = "Error while executing InsertCollegeBranchCourseStreamInfo in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int UpdateCollegeBranchCourseStreamInfo(CollegeBranchCourseStreamProperty objCollegeBranchCourseStreamProperty, int modifiedBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamId", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamId);
               _objDataWrapper.AddParameter("@StreamId", objCollegeBranchCourseStreamProperty.StreamId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", objCollegeBranchCourseStreamProperty.CollegeBranchCourseId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamSeat", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamSeat);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamDuration", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamDuration);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamFees", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamFees);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamModeId", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamModeId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamEligibilty", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamEligibity);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamDesc", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamDesc);
               _objDataWrapper.AddParameter("@CollegeBranchCourseMamagementQuotaSeat", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamManagementQuotaSeat);
               _objDataWrapper.AddParameter("@CollegeBranchCourseLateralEntrySeat", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamLateralEntrySeat);
               _objDataWrapper.AddParameter("@CollegeBranchCourseStreamStatus", objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamStatus);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
               var ObjErrMsg = (SqlParameter)
                        (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeCourseStream");
               if (ObjErrMsg != null && ObjErrMsg.Value !=null)
                   errmsg = Convert.ToString(ObjErrMsg.Value);


           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateCollegeBranchCourseStreamInfo in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int InsertCollegeBranchCourseExamInfo(CollegeBranchCourseExamProperty objCollegeBranchCourseExamProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@ExamId", objCollegeBranchCourseExamProperty.ExamId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", objCollegeBranchCourseExamProperty.CollegeBranchCourseId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseExamStatus", objCollegeBranchCourseExamProperty.CollegeCourseExamStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);

               var ObjErrMsg =
                       (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeCoureseExam");
               if (ObjErrMsg != null && ObjErrMsg.Value!=null)
                   errmsg = Convert.ToString(ObjErrMsg.Value);


           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing InsertCollegeBranchCourseExamInfo in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;

       }
       public override int InsertCollegeBranchCourseExamInfoByCollegeId(CollegeBranchCourseExamProperty objCollegeBranchCourseExamProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CourseId", objCollegeBranchCourseExamProperty.CourseId);
               _objDataWrapper.AddParameter("@ExamId", objCollegeBranchCourseExamProperty.ExamId);
               _objDataWrapper.AddParameter("@CollegeId", objCollegeBranchCourseExamProperty.CollegeBranchId);
                _objDataWrapper.AddParameter("@CollegeBranchCourseExamStatus", objCollegeBranchCourseExamProperty.CollegeCourseExamStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);

               var ObjErrMsg =
                       (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeCoureseExamInsert");
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
               const string addInfo = "Error while executing InsertCollegeBranchCourseExamInfoByCollegeId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;

       }
       public override int UpdateCollegeBranchCourseExamInfo(CollegeBranchCourseExamProperty objCollegeBranchCourseExamProperty, int modifiedBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CollegeCourseExamId", objCollegeBranchCourseExamProperty.CollegeBranchCourseExamId);
               _objDataWrapper.AddParameter("@ExamId", objCollegeBranchCourseExamProperty.ExamId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", objCollegeBranchCourseExamProperty.CollegeBranchCourseId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseExamStatus", objCollegeBranchCourseExamProperty.CollegeCourseExamStatus);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);

               var ObjErrMsg =
                       (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeCoureseExam");
               if (ObjErrMsg != null && ObjErrMsg.Value!=null)
                   errmsg = Convert.ToString(ObjErrMsg.Value);


           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateCollegeBranchCourseExamInfo in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int InsertCollegeBranchCourseHighlights(CollegeBranchCourseHighlightsProperty objCollegeBranchCourseHighlightsProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", objCollegeBranchCourseHighlightsProperty.CollegeBranchCourseId);
               _objDataWrapper.AddParameter("@CollegeHighlights", objCollegeBranchCourseHighlightsProperty.CollegeBranchCourseHighlight);
               _objDataWrapper.AddParameter("@CollegeHighlightsStatus", objCollegeBranchCourseHighlightsProperty.CollegeBranchCourseHighlightStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);
               var ObjErrMsg =
                            (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeHighlights");
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
               const string addInfo = "Error while executing InsertCollegeBranchCourseHighlights in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }
       public override int InsertCollegeBranchCourseHighlightsByCollege(CollegeBranchCourseHighlightsProperty objCollegeBranchCourseHighlightsProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CollegeId", objCollegeBranchCourseHighlightsProperty.CollegeBranchId);
               _objDataWrapper.AddParameter("@CourseId", objCollegeBranchCourseHighlightsProperty.CourseId);
               _objDataWrapper.AddParameter("@CollegeHighlights", objCollegeBranchCourseHighlightsProperty.CollegeBranchCourseHighlight);
               _objDataWrapper.AddParameter("@CollegeHighlightsStatus", objCollegeBranchCourseHighlightsProperty.CollegeBranchCourseHighlightStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);
               var ObjErrMsg =
                            (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeHighlightsInsert");
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
               const string addInfo = "Error while executing InsertCollegeBranchCourseHighlightsByCollege in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int UpdateCollegeBranchCourseHighlights(CollegeBranchCourseHighlightsProperty objCollegeBranchCourseHighlightsProperty, int modifiedBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CollegeHighlightsId", objCollegeBranchCourseHighlightsProperty.CollegeBranchCourseHighlightId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", objCollegeBranchCourseHighlightsProperty.CollegeBranchCourseId);
               _objDataWrapper.AddParameter("@CollegeHighlights", objCollegeBranchCourseHighlightsProperty.CollegeBranchCourseHighlight);
               _objDataWrapper.AddParameter("@CollegeHighlightsStatus", objCollegeBranchCourseHighlightsProperty.CollegeBranchCourseHighlightStatus);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
               var ObjErrMsg =
                            (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeHighlights");
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
               const string addInfo = "Error while executing UpdateCollegeBranchCourseHighlights in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int InsertCollegeBranchCourseFacilities(CollegeBranchCourseFacilitiesProperty objCollegeBranchCourseFacilitiesProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", objCollegeBranchCourseFacilitiesProperty.CollegeBranchCourseId);
               _objDataWrapper.AddParameter("@CollegeFacilitiesName", objCollegeBranchCourseFacilitiesProperty.CollegeBranchCourseFacilitieName );
               _objDataWrapper.AddParameter("@CollegeFacilitiesDesc", objCollegeBranchCourseFacilitiesProperty.CollegeBranchCourseFacilitieDesc);
               _objDataWrapper.AddParameter("@CollegeFacilitiesStatus", objCollegeBranchCourseFacilitiesProperty.CollegeBranchCourseFacilitieStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);
               var ObjErrMsg =
                           (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeFacilities");
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
               const string addInfo = "Error while executing InsertCollegeBranchCourseFacilities in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }
       public override int InsertCollegeBranchCourseFacilitiesByCollege(CollegeBranchCourseFacilitiesProperty objCollegeBranchCourseFacilitiesProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CollegeId", objCollegeBranchCourseFacilitiesProperty.CollegeBranchId);
               _objDataWrapper.AddParameter("@CourseId", objCollegeBranchCourseFacilitiesProperty.CourseId);
               _objDataWrapper.AddParameter("@CollegeFacilitiesName", objCollegeBranchCourseFacilitiesProperty.CollegeBranchCourseFacilitieName);
               _objDataWrapper.AddParameter("@CollegeFacilitiesDesc", objCollegeBranchCourseFacilitiesProperty.CollegeBranchCourseFacilitieDesc);
               _objDataWrapper.AddParameter("@CollegeFacilitiesStatus", objCollegeBranchCourseFacilitiesProperty.CollegeBranchCourseFacilitieStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);
               var ObjErrMsg =
                           (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeFacilitiesInsert");
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
               const string addInfo = "Error while executing InsertCollegeBranchCourseFacilitiesByCollege in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int UpdateCollegeBranchCourseFacilities(CollegeBranchCourseFacilitiesProperty objCollegeBranchCourseFacilitiesProperty, int modifiedBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";

           try
           {
               _objDataWrapper.AddParameter("@CollegeFacilitiesId", objCollegeBranchCourseFacilitiesProperty.CollegeBranchCourseFacilitieId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", objCollegeBranchCourseFacilitiesProperty.CollegeBranchCourseId);
               _objDataWrapper.AddParameter("@CollegeFacilitiesName", objCollegeBranchCourseFacilitiesProperty.CollegeBranchCourseFacilitieName);
               _objDataWrapper.AddParameter("@CollegeFacilitiesDesc", objCollegeBranchCourseFacilitiesProperty.CollegeBranchCourseFacilitieDesc);
               _objDataWrapper.AddParameter("@CollegeFacilitiesStatus", objCollegeBranchCourseFacilitiesProperty.CollegeBranchCourseFacilitieStatus);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
               var ObjErrMsg =
                           (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeFacilities");
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
               const string addInfo = "Error while executing UpdateCollegeBranchCourseFacilities in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }
       
       public override int InsertCollegeBranchHostelInfo(CollegeBranchCourseHostelProperty objCollegeBranchCourseHostelProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@HostelCategoryId", objCollegeBranchCourseHostelProperty.HostelCategoryId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", objCollegeBranchCourseHostelProperty.CollegeBranchCourseId);
               _objDataWrapper.AddParameter("@CollegeHostelLocation", objCollegeBranchCourseHostelProperty.CollegeBranchCourseHostelLocation);
               _objDataWrapper.AddParameter("@CollegeHasInternet", objCollegeBranchCourseHostelProperty.IsCollegeBranchCourseHostelHasInternet);
               _objDataWrapper.AddParameter("@CollegeHostelHasLoundry", objCollegeBranchCourseHostelProperty.IsCollegeBranchCourseHostelHasLoundry);
               _objDataWrapper.AddParameter("@CollegeHostelHasPowerBackup", objCollegeBranchCourseHostelProperty.IsCollegeBranchCourseHostelHasPowerBackup);
               _objDataWrapper.AddParameter("@CollegeHostelHasAC", objCollegeBranchCourseHostelProperty.IsCollegeBranchCourseHostelHasAC);
               _objDataWrapper.AddParameter("@CollegeHostelCharge", objCollegeBranchCourseHostelProperty.CollegeBranchCourseHostelCharge);
               _objDataWrapper.AddParameter("@CollegeHostelStatus", objCollegeBranchCourseHostelProperty.CollegeBranchCourseHostelStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);

               var ObjErrMsg =
                          (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeHostelInfo");
               if (ObjErrMsg != null && ObjErrMsg.Value != null)
                   errmsg = Convert.ToString(ObjErrMsg.Value);
           }
           catch(Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing InsertCollegeBranchHostelInfo in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }



           return _i;
       }
       public override int InsertCollegeBranchHostelInfoInsert(CollegeBranchCourseHostelProperty objCollegeBranchCourseHostelProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@HostelCategoryId", objCollegeBranchCourseHostelProperty.HostelCategoryId);
               _objDataWrapper.AddParameter("@CollegeId", objCollegeBranchCourseHostelProperty.CollegeBranchId);
               _objDataWrapper.AddParameter("@CourseId", objCollegeBranchCourseHostelProperty.CourseId);
               _objDataWrapper.AddParameter("@CollegeHostelLocation", objCollegeBranchCourseHostelProperty.CollegeBranchCourseHostelLocation);
               _objDataWrapper.AddParameter("@CollegeHasInternet", objCollegeBranchCourseHostelProperty.IsCollegeBranchCourseHostelHasInternet);
               _objDataWrapper.AddParameter("@CollegeHostelHasLoundry", objCollegeBranchCourseHostelProperty.IsCollegeBranchCourseHostelHasLoundry);
               _objDataWrapper.AddParameter("@CollegeHostelHasPowerBackup", objCollegeBranchCourseHostelProperty.IsCollegeBranchCourseHostelHasPowerBackup);
               _objDataWrapper.AddParameter("@CollegeHostelHasAC", objCollegeBranchCourseHostelProperty.IsCollegeBranchCourseHostelHasAC);
               _objDataWrapper.AddParameter("@CollegeHostelCharge", objCollegeBranchCourseHostelProperty.CollegeBranchCourseHostelCharge);
               _objDataWrapper.AddParameter("@CollegeHostelStatus", objCollegeBranchCourseHostelProperty.CollegeBranchCourseHostelStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);

               var ObjErrMsg =
                          (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeHostelInfoInsert");
               if (ObjErrMsg != null && ObjErrMsg.Value != null)
                   errmsg = Convert.ToString(ObjErrMsg.Value);
           }
           catch(Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing InsertCollegeBranchHostelInfo in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }



           return _i;
       }
       public override int UpdateCollegeBranchHostelInfo(CollegeBranchCourseHostelProperty objCollegeBranchCourseHostelProperty, int modifiedBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CollegeHostelId", objCollegeBranchCourseHostelProperty.CollegeBranchCourseHostelId);
               _objDataWrapper.AddParameter("@HostelCategoryId", objCollegeBranchCourseHostelProperty.HostelCategoryId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", objCollegeBranchCourseHostelProperty.CollegeBranchCourseId);
               _objDataWrapper.AddParameter("@CollegeHostelLocation", objCollegeBranchCourseHostelProperty.CollegeBranchCourseHostelLocation);
               _objDataWrapper.AddParameter("@CollegeHasInternet", objCollegeBranchCourseHostelProperty.IsCollegeBranchCourseHostelHasInternet);
               _objDataWrapper.AddParameter("@CollegeHostelHasLoundry", objCollegeBranchCourseHostelProperty.IsCollegeBranchCourseHostelHasLoundry);
               _objDataWrapper.AddParameter("@CollegeHostelHasPowerBackup", objCollegeBranchCourseHostelProperty.IsCollegeBranchCourseHostelHasPowerBackup);
               _objDataWrapper.AddParameter("@CollegeHostelHasAC", objCollegeBranchCourseHostelProperty.IsCollegeBranchCourseHostelHasAC);
               _objDataWrapper.AddParameter("@CollegeHostelCharge", objCollegeBranchCourseHostelProperty.CollegeBranchCourseHostelCharge);
               _objDataWrapper.AddParameter("@CollegeHostelStatus", objCollegeBranchCourseHostelProperty.CollegeBranchCourseHostelStatus);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);

               var ObjErrMsg =
                          (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeHostelInfo");
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
               const string addInfo = "Error while executing UpdateCollegeBranchHostelInfo in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }



           return _i;
       }
       
       public override int InsertCollegeBranchRank(CollegeBranchRankProperty objCollegeBranchRankProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", objCollegeBranchRankProperty.CollegeBranchCourseId);
               _objDataWrapper.AddParameter("@CollegeRanksourceId", objCollegeBranchRankProperty.CollegeRankSourceId);
               _objDataWrapper.AddParameter("@CollegeRankSourceYear", objCollegeBranchRankProperty.CollegeRankYear);
               _objDataWrapper.AddParameter("@CollegeRankOverAll", objCollegeBranchRankProperty.CollegeOverAllRank);
               _objDataWrapper.AddParameter("@CollegeRankStatus", objCollegeBranchRankProperty.CollegeRankStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);

               var ObjErrMsg =
                          (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeRank");
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
               const string addInfo = "Error while executing InsertCollegeBranchRank in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
                      
           return _i;
       }
       public override int InsertCollegeBranchRankByCollegeId(CollegeBranchRankProperty objCollegeBranchRankProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CollegeId", objCollegeBranchRankProperty.CollegeBranchId);
               _objDataWrapper.AddParameter("@CourseId", objCollegeBranchRankProperty.CourseId);
               _objDataWrapper.AddParameter("@CollegeRanksourceId", objCollegeBranchRankProperty.CollegeRankSourceId);
               _objDataWrapper.AddParameter("@CollegeRankSourceYear", objCollegeBranchRankProperty.CollegeRankYear);
               _objDataWrapper.AddParameter("@CollegeRankOverAll", objCollegeBranchRankProperty.CollegeOverAllRank);
               _objDataWrapper.AddParameter("@CollegeRankStatus", objCollegeBranchRankProperty.CollegeRankStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);

               var ObjErrMsg =
                          (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeRankInsert");
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
               const string addInfo = "Error while executing InsertCollegeBranchRankByCollegeId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
                      
           return _i;
       }

       public override int UpdateCollegeBranchRank(CollegeBranchRankProperty objCollegeBranchRankProperty, int modifiedBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CollegeRankId", objCollegeBranchRankProperty.CollegeRankId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", objCollegeBranchRankProperty.CollegeBranchCourseId);
               _objDataWrapper.AddParameter("@CollegeRanksourceId", objCollegeBranchRankProperty.CollegeRankSourceId);
               _objDataWrapper.AddParameter("@CollegeRankSourceYear", objCollegeBranchRankProperty.CollegeRankYear);
               _objDataWrapper.AddParameter("@CollegeRankOverAll", objCollegeBranchRankProperty.CollegeOverAllRank);
               _objDataWrapper.AddParameter("@CollegeRankStatus", objCollegeBranchRankProperty.CollegeRankStatus);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);

               var ObjErrMsg =
                          (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeRank");
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
               const string addInfo = "Error while executing UpdateCollegeBranchRank in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }

           return _i;
       }

       public override int InsertCollegeKeySpeach(CollegeBranchKeySpeech objCollegeBranchKeySpeech, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";

           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchId", objCollegeBranchKeySpeech.CollegeBranchId);
               _objDataWrapper.AddParameter("@CollegeSpeechPersonDesignation", objCollegeBranchKeySpeech.CollegeBranchId);
               _objDataWrapper.AddParameter("@CollegeSpeechPersonName", objCollegeBranchKeySpeech.CollegeBranchId);
               _objDataWrapper.AddParameter("@CollegeSpeechPersonImage", objCollegeBranchKeySpeech.CollegeBranchId);
               _objDataWrapper.AddParameter("@CollegeSpeechDesc", objCollegeBranchKeySpeech.CollegeBranchId);
               _objDataWrapper.AddParameter("@CollegeSpeechPersonAbout", objCollegeBranchKeySpeech.CollegeBranchId);
               _objDataWrapper.AddParameter("@CollegeSpeechPersonStatus", objCollegeBranchKeySpeech.CollegeBranchId);
               _objDataWrapper.AddParameter("@CreatedBy", objCollegeBranchKeySpeech.CollegeBranchId);
               var ObjErrMsg =
                         (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeKeySpeach");
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
               const string addInfo = "Error while executing InsertCollegeKeySpeach in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int UpdateCollegeKeySpeech(CollegeBranchKeySpeech objCollegeBranchKeySpeech, int modifiedBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";

           try
           {
               _objDataWrapper.AddParameter("@CollegeKeySpeachId", objCollegeBranchKeySpeech.CollegeBranchKeySpeechId);
               _objDataWrapper.AddParameter("@CollegeBranchId", objCollegeBranchKeySpeech.CollegeBranchId);
               _objDataWrapper.AddParameter("@CollegeSpeechPersonDesignation", objCollegeBranchKeySpeech.CollegeBranchId);
               _objDataWrapper.AddParameter("@CollegeSpeechPersonName", objCollegeBranchKeySpeech.CollegeBranchId);
               _objDataWrapper.AddParameter("@CollegeSpeechPersonImage", objCollegeBranchKeySpeech.CollegeBranchId);
               _objDataWrapper.AddParameter("@CollegeSpeechDesc", objCollegeBranchKeySpeech.CollegeBranchId);
               _objDataWrapper.AddParameter("@CollegeSpeechPersonAbout", objCollegeBranchKeySpeech.CollegeBranchId);
               _objDataWrapper.AddParameter("@CollegeSpeechPersonStatus", objCollegeBranchKeySpeech.CollegeBranchId);
               _objDataWrapper.AddParameter("@CreatedBy", objCollegeBranchKeySpeech.CollegeBranchId);
               var ObjErrMsg =
                         (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeKeySpeach");
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
               const string addInfo = "Error while executing UpdateCollegeKeySpeech in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int InsertCollegeGallery(CollegeBranchGallery objCollegeGalleryProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CollegeGalleryId", objCollegeGalleryProperty.CollegeBranchGalleryId);
                _objDataWrapper.AddParameter("@CollegeBranchName", objCollegeGalleryProperty.CollegeBranchName);
               _objDataWrapper.AddParameter("@CollegeGalleryImageTitle", objCollegeGalleryProperty.CollegeBranchGalleryImageTitle);
               _objDataWrapper.AddParameter("@CollegeGalleryImageName", objCollegeGalleryProperty.CollegeBranchGalleryImageName);
               _objDataWrapper.AddParameter("@CollegeGalleryStatus", objCollegeGalleryProperty.CollegeBranchGalleryImageStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);

               var ObjErrMsg =
                       (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeGallery");
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
               const string addInfo = "Error while executing InsertCollegeGallery in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }



       public override int UpdateCollegeGallery(CollegeBranchGallery objCollegeGalleryProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CollegeGalleryId", objCollegeGalleryProperty.CollegeBranchGalleryId);
               _objDataWrapper.AddParameter("@CollegeBranchName", objCollegeGalleryProperty.CollegeBranchName);
               _objDataWrapper.AddParameter("@CollegeGalleryImageTitle", objCollegeGalleryProperty.CollegeBranchGalleryImageTitle);
               _objDataWrapper.AddParameter("@CollegeGalleryImageName", objCollegeGalleryProperty.CollegeBranchGalleryImageName);
               _objDataWrapper.AddParameter("@CollegeGalleryStatus", objCollegeGalleryProperty.CollegeBranchGalleryImageStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);

               var ObjErrMsg =
                       (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeGallery");
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
               const string addInfo = "Error while executing UpdateCollegeGallery in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int InsertCollegePlacement(CollegeBranchCoursePlacementProperty objCollegeBranchCoursePlacementProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", objCollegeBranchCoursePlacementProperty.CollegeBranchCourseId);
              _objDataWrapper.AddParameter("@CollegeBranchPlacementCompanyName", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementCompanyName);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementYear", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementYear);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementNoOfStudentHired", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementNoOfStudentHired);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementAvgSalary", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementAvgSalaryOffered);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementStatus", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);

               var ObjErrMsg =
                       (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegePlacement");
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
               const string addInfo = "Error while executing InsertCollegePlacement in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       public override int UpdateCollegePlacement(CollegeBranchCoursePlacementProperty objCollegeBranchCoursePlacementProperty, int modifiedBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = "";
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchPlacementId", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", objCollegeBranchCoursePlacementProperty.CollegeBranchCourseId);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementCompanyName", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementCompanyName);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementYear", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementYear);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementNoOfStudentHired", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementNoOfStudentHired);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementAvgSalary", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementAvgSalaryOffered);
               _objDataWrapper.AddParameter("@CollegeBranchPlacementStatus", objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementStatus);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);

               var ObjErrMsg =
                       (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegePlacement");
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
               const string addInfo = "Error while executing UpdateCollegePlacement in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;
       }

       // Private  Method to Bind the College Group Object Details 
       private List<CollegeGroupProperty> BindCollegeGroupObjectList(DataTable datatable)
       {
           var objCollegeGroupList = new List<CollegeGroupProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objCollegeGroupProperty = new CollegeGroupProperty
                            {
                                CollegeGroupId = (datatable.Rows[j]["AjCollegeGroupId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeGroupId"]),
                                CollegeGroupName = (datatable.Rows[j]["AjCollegeGroupName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeGroupName"]),
                                CollegeGroupLogo = (datatable.Rows[j]["AjCollegeGroupLogo"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeGroupLogo"]),
                                CollegeGropuStatus = (datatable.Rows[j]["AjCollegeGroupStatus"] is DBNull) ? true : Convert.ToBoolean(datatable.Rows[j]["AjCollegeGroupStatus"])

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
               const string addInfo = "Error while executing BindCollegeGroupObjectList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }

       private List<CollegeBranchProperty> BindSponserCollegeObjectList(DataTable datatable)
       {
           var objInstituteTypeList = new List<CollegeBranchProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objSponserCollegeProperty = new CollegeBranchProperty
                       {
                           CollegeIdBranchId = (datatable.Rows[j]["AjCollegeBranchId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchId"]),
                           CollegeBranchName = (datatable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),
                           
                       };
                       objInstituteTypeList.Add(objSponserCollegeProperty);
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
               const string addInfo = "Error while executing BindInstituteTypeObjectList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objInstituteTypeList;
       }

       private List<CollegeBranchProperty> BindSponserCollegeObjectListbyCourseId(DataTable datatable)
       {
           var objInstituteTypeList = new List<CollegeBranchProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objSponserCollegeProperty = new CollegeBranchProperty
                       {
                           CollegeIdBranchId = (datatable.Rows[j]["AjCollegeBranchId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchId"]),
                           CollegeBranchName = (datatable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),

                       };
                       objInstituteTypeList.Add(objSponserCollegeProperty);
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
               const string addInfo = "Error while executing BindInstituteTypeObjectList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objInstituteTypeList;
       }





       private List<InstituteTypeProperty> BindInstituteTypeObjectList(DataTable datatable)
       {
           var objInstituteTypeList = new List<InstituteTypeProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objInstituteTypeProperty = new InstituteTypeProperty
                       {
                           InstituteTypeId = (datatable.Rows[j]["AjInstituteTypeId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjInstituteTypeId"]),
                           InstituteType = (datatable.Rows[j]["AjInstituteType"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjInstituteType"])                           

                       };
                       objInstituteTypeList.Add(objInstituteTypeProperty);
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
               const string addInfo = "Error while executing BindInstituteTypeObjectList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objInstituteTypeList;
       }

       private List<CollegeAssociationCategoryProperty> BindAssociationCategoryObjectList(DataTable datatable)
       {
           var objAssociationCategoryList = new List<CollegeAssociationCategoryProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objAssociationCategoryProperty = new CollegeAssociationCategoryProperty
                       {
                           AssociationCategoryTypeId =(datatable.Rows[j]["AjCollegeAssociationCategoryId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeAssociationCategoryId"]),
                           AssociationCategoryType = (datatable.Rows[j]["AjCollegeAssociationCategoryName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeAssociationCategoryName"]),
                           AssociationCategoryAmount = (datatable.Rows[j]["AjAmount"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjAmount"]),
                           AssociationCategoryDescription = (datatable.Rows[j]["AjDescription"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjDescription"]),
                           AssociationCategoryStatus = (!(datatable.Rows[j]["AjCollegeAssociationCategoryStatus"] is DBNull)) && Convert.ToBoolean(datatable.Rows[j]["AjCollegeAssociationCategoryStatus"]),

                       };
                       objAssociationCategoryList.Add(objAssociationCategoryProperty);
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
               const string addInfo = "Error while executing BindAssociationCategoryObjectList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objAssociationCategoryList;
       }
       private List<CollegeAssociationCategoryProperty> BindAssociationDisplayObjectList(DataTable datatable)
       {
           var objAssociationCategoryList = new List<CollegeAssociationCategoryProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objAssociationCategoryProperty = new CollegeAssociationCategoryProperty
                       {
                           AssociationCategoryTypeId = (datatable.Rows[j]["AjBannerPositionId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjBannerPositionId"]),
                           AssociationCategoryType = (datatable.Rows[j]["AjBannerPosition"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjBannerPosition"]),
                           AssociationCategoryAmount = (datatable.Rows[j]["AjBannerPostAmount"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjBannerPostAmount"]),
                           AssociationCategoryDescription = (datatable.Rows[j]["AjBannerPostDescription"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjBannerPostDescription"]),
                           AssociationCategoryStatus = (!(datatable.Rows[j]["AjBannerPostionStatus"] is DBNull)) && Convert.ToBoolean(datatable.Rows[j]["AjBannerPostionStatus"]),

                       };
                       objAssociationCategoryList.Add(objAssociationCategoryProperty);
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
               const string addInfo = "Error while executing BindAssociationDisplayObjectList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objAssociationCategoryList;
       }

       private List<HostelCategoryProperty> BindHostelCategoryObjectList(DataTable datatable)
       {
           var objHostelCategoryPropertyList = new List<HostelCategoryProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objHostelCategoryPropertyProperty = new HostelCategoryProperty
                       {
                           HostelCategoryId = (datatable.Rows[j]["AjHostelCategoryId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjHostelCategoryId"]),
                           HostelCategoryType = (datatable.Rows[j]["AjHostelCategoryName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjHostelCategoryName"]),
                           HostelCategoryStatus = (datatable.Rows[j]["AjHostelCategoryStatus"] is DBNull) ? true : Convert.ToBoolean(datatable.Rows[j]["AjHostelCategoryStatus"]) 

                       };
                       objHostelCategoryPropertyList.Add(objHostelCategoryPropertyProperty);
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
               const string addInfo = "Error while executing BindHostelCategoryObjectList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objHostelCategoryPropertyList;
       }

       private List<CollegeRankSource> BindRankSourceObjectList(DataTable datatable)
       {
           var objRankSourcePropertyList = new List<CollegeRankSource>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objRankSourceProperty = new CollegeRankSource
                       {
                           CollegeRankSourceId =(datatable.Rows[j]["AjCollegeRankSourceId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeRankSourceId"]),
                           CollegeRankSourceName = (datatable.Rows[j]["AjCollegeRankSourceName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeRankSourceName"]),
                           CollegeRankSourceStatus = (datatable.Rows[j]["AjCollegeRankStatus"] is DBNull) ? true : Convert.ToBoolean(datatable.Rows[j]["AjCollegeRankStatus"]) 

                       };
                       objRankSourcePropertyList.Add(objRankSourceProperty);
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
               const string addInfo = "Error while executing BindHostelCategoryObjectList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objRankSourcePropertyList;
       }
       
        //to insert college details using excel
       public override int InsertData(string column, string value,string dbColumnActualData, string branchNameValue,string ColumnDataType, out string errMsg)
       {
           errMsg = "";
           int i = 0;
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
         
          try
           {
               _objDataWrapper.AddParameter("@ColumnName", column);
               _objDataWrapper.AddParameter("@dbColumnActualData", dbColumnActualData);
               _objDataWrapper.AddParameter("@ColumnDataType", ColumnDataType);
               _objDataWrapper.AddParameter("@BranchName", branchNameValue);
               _objDataWrapper.AddParameter("@ColumnValue", value.Replace("'","`"));
               var ObjErrMsg = (SqlParameter)
                       (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               i = _objDataWrapper.ExecuteNonQuery("AJ_Proc_DynamicData");
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
               const string addInfo = "Error while executing InsertData in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           } return i;
           
       }
       public override int InsertCourseData(string column, string value,string courseName, string dbColumnActualData, string branchNameValue,string universityName ,  string ColumnDataType, out string errMsg)
       {
           errMsg = "";
           int i = 0;
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);

           try
           {
               _objDataWrapper.AddParameter("@ColumnName", column);
               _objDataWrapper.AddParameter("@UniversityName", universityName);
               _objDataWrapper.AddParameter("@dbColumnActualData", dbColumnActualData);
               _objDataWrapper.AddParameter("@ColumnDataType", ColumnDataType);
               _objDataWrapper.AddParameter("@BranchName", branchNameValue.Replace("'", "`"));
               _objDataWrapper.AddParameter("@CourseName", courseName.Replace("'","`"));
               _objDataWrapper.AddParameter("@ColumnValue", value.Replace("'", "`"));
               var ObjErrMsg = (SqlParameter)
                       (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_DynamicCollegeCourseInsert");
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
               const string addInfo = "Error while executing InsertCourseData in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           } return i;

       }
       public override int InsertCourseStreamData(string column, string value, string dbColumnActualData, string branchNameValue, string streamName, string ColumnDataType, out string errMsg)
       {
           errMsg = "";
           int i = 0;
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);

           try
           {
               _objDataWrapper.AddParameter("@ColumnName", column);
               _objDataWrapper.AddParameter("@StreamName", streamName.Replace("'","`"));
               _objDataWrapper.AddParameter("@dbColumnActualData", dbColumnActualData);
               _objDataWrapper.AddParameter("@ColumnDataType", ColumnDataType);
               _objDataWrapper.AddParameter("@BranchName", branchNameValue.Replace("'","`"));
               _objDataWrapper.AddParameter("@ColumnValue", value.Replace("'", "`"));
               var ObjErrMsg = (SqlParameter)
                       (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_DynamicCollegeCourseStreamInsert");
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
               const string addInfo = "Error while executing InsertCourseStreamData in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           } return i;

       }
       public override int InsertCourseExamData(string column, string value, string dbColumnActualData, string branchNameValue, string examName, string ColumnDataType, out string errMsg)
       {
           errMsg = "";
           int i = 0;
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);

           try
           {
               _objDataWrapper.AddParameter("@ColumnName", column);
               _objDataWrapper.AddParameter("@ExamName", examName);
               _objDataWrapper.AddParameter("@dbColumnActualData", dbColumnActualData);
               _objDataWrapper.AddParameter("@ColumnDataType", ColumnDataType);
               _objDataWrapper.AddParameter("@BranchName", branchNameValue);
               _objDataWrapper.AddParameter("@ColumnValue", value);
               var ObjErrMsg = (SqlParameter)
                       (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               i = _objDataWrapper.ExecuteNonQuery("AJ_Proc_InsertCollegeCourseExam");
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
               const string addInfo = "Error while executing InsertCourseStreamData in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           } return i;

       }
       public override int InsertCourseFacalityData(string column, string value, string dbColumnActualData, string branchNameValue,string ColumnDataType, out string errMsg)
       {
           errMsg = "";
           int i = 0;
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);

           try
           {
               _objDataWrapper.AddParameter("@ColumnName", column);
               _objDataWrapper.AddParameter("@dbColumnActualData", dbColumnActualData);
               _objDataWrapper.AddParameter("@ColumnDataType", ColumnDataType);
               _objDataWrapper.AddParameter("@BranchName", branchNameValue);
               _objDataWrapper.AddParameter("@ColumnValue", value);
               var ObjErrMsg = (SqlParameter)
                       (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               i = _objDataWrapper.ExecuteNonQuery("AJ_Proc_InsertCollegeCourseFacality");
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
               const string addInfo = "Error while executing InsertCourseStreamData in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           } return i;

       }
       public override int InsertCourseHighLightsData(string column, string value, string dbColumnActualData, string branchNameValue, string ColumnDataType, out string errMsg)
       {
           errMsg = "";
           int i = 0;
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);

           try
           {
               _objDataWrapper.AddParameter("@ColumnName", column);
               _objDataWrapper.AddParameter("@dbColumnActualData", dbColumnActualData);
               _objDataWrapper.AddParameter("@ColumnDataType", ColumnDataType);
               _objDataWrapper.AddParameter("@BranchName", branchNameValue);
               _objDataWrapper.AddParameter("@ColumnValue", value);
               var ObjErrMsg = (SqlParameter)
                       (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               i = _objDataWrapper.ExecuteNonQuery("AJ_Proc_InsertCollegeCourseHighLights");
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
               const string addInfo = "Error while executing InsertCourseHighLightsData in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           } return i;

       }
       public override int InsertCourseRankSourceData(string column, string value, string dbColumnActualData, string branchNameValue, string ColumnDataType, out string errMsg,string courseName)
       {
           errMsg = "";
           int i = 0;
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);

           try
           {
               _objDataWrapper.AddParameter("@ColumnName", column);
               _objDataWrapper.AddParameter("@dbColumnActualData", dbColumnActualData);
               _objDataWrapper.AddParameter("@ColumnDataType", ColumnDataType);
               _objDataWrapper.AddParameter("@BranchName", branchNameValue);
               _objDataWrapper.AddParameter("@CourseName", courseName);
               _objDataWrapper.AddParameter("@ColumnValue", value);
               var ObjErrMsg = (SqlParameter)
                       (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               i = _objDataWrapper.ExecuteNonQuery("AJ_Proc_InsertCourseRankSourceData");
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
               const string addInfo = "Error while executing InsertCourseRankSourceData in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           } return i;

       }
       public override int InsertCourseHostel(string column, string value, string dbColumnActualData, string branchNameValue, string ColumnDataType, out string errMsg,string courseName)
       {
           errMsg = "";
           int i = 0;
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);

           try
           {
               _objDataWrapper.AddParameter("@ColumnName", column);
               _objDataWrapper.AddParameter("@dbColumnActualData", dbColumnActualData);
               _objDataWrapper.AddParameter("@ColumnDataType", ColumnDataType);
               _objDataWrapper.AddParameter("@BranchName", branchNameValue);
               _objDataWrapper.AddParameter("@ColumnValue", value);
               _objDataWrapper.AddParameter("@CourseName", courseName);
               var ObjErrMsg = (SqlParameter)
                       (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               i = _objDataWrapper.ExecuteNonQuery("AJ_Proc_InsertCourseHostel");
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
               const string addInfo = "Error while executing InsertCourseRankSourceData in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           } return i;

       }
       public override List<CollegeBranchProperty> GetCollegeListByCourse(int courseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CourseId", courseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeListByCourse");
               objCollegeBranchList = BindCollegeList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetAllCollegeGroupList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }


       public override List<CollegeBranchProperty> GetCollegeList()
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchProperty>();
           try
           {
               
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeListByCourse");
               objCollegeBranchList = BindCollegeListByName(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }



       public override List<CollegeBranchGallery> GetCollegeGalleryList()
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchGallery>();
           try
           {

               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeImageGallery");
               objCollegeBranchList = BindCollegeGallery(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeGalleryList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }


       public override List<CollegeBranchGallery> GetCollegeGalleryById( int CollegeGalleryID)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchGallery>();
           try
           {
               _objDataWrapper.AddParameter("@COllegeGalleryId", CollegeGalleryID);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeImageGallery");
               objCollegeBranchList = BindCollegeGallery(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeGalleryList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }
       public override List<CollegeBranchProperty> GetCollegeListByDynamicQuery(string dbQuery, int pagesize,int pageNum,out int totalRecords)
     {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           totalRecords = 0;
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchProperty>();
           try
           {
               if(!string.IsNullOrEmpty(dbQuery))
                    _objDataWrapper.AddParameter("@DbQuery", dbQuery);
               _objDataWrapper.AddParameter("@PageSize", pagesize);
               _objDataWrapper.AddParameter("@PageNum", pageNum);
               var objTotalRecordParameter =
                   (SqlParameter)
                       _objDataWrapper.AddParameter("@TotalRecord", " ", SqlDbType.Int, ParameterDirection.Output);
             _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeListByDynamicQuery");
               if (objTotalRecordParameter != null && objTotalRecordParameter.Value != null)
                   totalRecords = Convert.ToInt32(objTotalRecordParameter.Value);
               objCollegeBranchList = BindCollegeList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeListByDynamicQuery in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }

       public override List<CollegeBranchProperty> GetBookedCollegeByCourse(int collegeCourseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
         
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchProperty>();
           try
           {

               _objDataWrapper.AddParameter("@CollegeBrcnahCourseId", collegeCourseId);
             
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetBookedCollegeByCourse");
              
               objCollegeBranchList = BindCollegeList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetBookedCollegeByCourse in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }
       public override List<CollegeBranchCoursePlacementProperty> GetCollegeTopHirerByDynamicQuery(string dbQuery)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCoursePlacementProperty = new List<CollegeBranchCoursePlacementProperty>();
           try
           {
               _objDataWrapper.AddParameter("@DbQuery", dbQuery);
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeTopHirerByDynamicQuery");
               objCollegeBranchCoursePlacementProperty = BindTopHirer(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeTopHirerByDynamicQuery in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCoursePlacementProperty;
       }



       public override List<CollegeBranchOnLineCounsellingProperty> GetCollegeForOnlineCounselling(string dbQuery, int pagesize, int pageNum, out int totalRecords)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           totalRecords = 0;
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchOnLineCounsellingProperty>();
           try
           {
                 _objDataWrapper.AddParameter("@DbQuery", dbQuery);
               _objDataWrapper.AddParameter("@PageSize", pagesize);
               _objDataWrapper.AddParameter("@PageNum", pageNum);
               var objTotalRecordParameter =
                   (SqlParameter)
                       _objDataWrapper.AddParameter("@TotalRecord", " ", SqlDbType.Int, ParameterDirection.Output);
      
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeListByDynamicQuery");
               objCollegeBranchList = BindOnlineCollegeList(_dataSet.Tables[0]);
               if (objTotalRecordParameter != null && objTotalRecordParameter.Value != null)
                   totalRecords = Convert.ToInt32(objTotalRecordParameter.Value);
           
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeForOnlineCounselling in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }
       public override List<CollegeBranchOnLineCounsellingProperty> GetCollegeForOnline(int collegeBranchCourseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchOnLineCounsellingProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", collegeBranchCourseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetOnlineCollegeList");
               objCollegeBranchList = BindOnlineCollege(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeForOnline in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }
       public override List<CollegeBranchProperty> GetCollegeListById(int collegeBranchId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchId", collegeBranchId);
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeListId");
               objCollegeBranchList = BindCollegeListByCollege(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeListById in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }
       public override List<CollegeBranchProperty> GetCollegeBasicDetailsByBranchCourseId(int branchCourseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchProperty>();
           try
           {
               _objDataWrapper.AddParameter("@BranchCourseId", branchCourseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeListByCourse");
               objCollegeBranchList = BindCollegeListByName(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeBasicDetailsByBranchCourseId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }
       public override List<CollegeBranchProperty> GetCollegeListByCityId(int branchCourseId,out string collegeName)
       {
           collegeName = "";
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchProperty>();
           try
           {
               _objDataWrapper.AddParameter("@BranchCourseId", branchCourseId); 
               var objCollegeName = (SqlParameter)
                (_objDataWrapper.AddParameter("@CollegeName ", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetColegeListByCityId");


               if (objCollegeName != null && objCollegeName.Value != null)
                   collegeName = Convert.ToString(objCollegeName.Value);
               objCollegeBranchList = BindCollegeListByName(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeListByCityId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }
       public override List<CollegeBranchProperty> GetTopRankedColleges(int courseId,int pageNum, int pageSize, out int totalRecords)
       {
          _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchProperty>();
           totalRecords = 0;
           try
           {
               _objDataWrapper.AddParameter("@CourseId", courseId);
               _objDataWrapper.AddParameter("@PageNum", pageNum);
               _objDataWrapper.AddParameter("@PageSize", pageSize);
               var objTotalRecords = 
                   (SqlParameter)(_objDataWrapper.AddParameter("@TotalRecords", "", SqlDbType.Int, ParameterDirection.Output));
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GettopRankedCollege");
               if (objTotalRecords != null && objTotalRecords.Value != null)
                   totalRecords = Convert.ToInt32(objTotalRecords.Value);

                 objCollegeBranchList = BindTopRankedCollege(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetTopRankedColleges in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }
       public override List<CollegeBranchProperty> GetPrivateCollegeList(int courseId, int pageNum, int pageSize, out int totalRecords)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           totalRecords = 0;
           var objCollegeBranchList = new List<CollegeBranchProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CourseId", courseId);
               _objDataWrapper.AddParameter("@PageNum", pageNum);
               _objDataWrapper.AddParameter("@PageSize", pageSize);
               var objRoralRecords = (SqlParameter)
                   (_objDataWrapper.AddParameter("@TotalRecords", "", SqlDbType.Int, ParameterDirection.Output));
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetPrivateCollegeListByCourseId");
               if (objRoralRecords != null && objRoralRecords.Value != null)
                   totalRecords = Convert.ToInt32(objRoralRecords.Value);
               objCollegeBranchList = BindTopRankedCollege(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetPrivateCollegeList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }
       public override List<CollegeBranchProperty> GetCollegeCourseListByCollegeName(string collegeName)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeName", collegeName);
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeListByCourse");
               objCollegeBranchList = BindCollegeListByName(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseListByCollegeName in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }

       public override List<CollegeBranchCoursePlacementProperty> GetCollegeCourseTopHirerListByCollegeName(string collegeName)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchCoursePlacementProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchName", collegeName);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeTopHirer");
               objCollegeBranchList = BindTopHirer(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseTopHirerListByCollegeName in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }


       public override List<CollegeBranchCoursePlacementProperty> GetCollegeCourseTopHirerList()
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchCoursePlacementProperty>();
           try
           {
               
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeTopHirer");
               
               objCollegeBranchList = BindTopHirer(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseTopHirerListByCollegeName in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }






       public override List<CollegeBranchProperty> GetCollegeListByCourseExamStateCIty(int cityId, int stateId,
                                                                                       int examId, int courseId,
                                                                                       int collegeManagemnet,
                                                                                       out int ErrorCount,
                                                                                       out string Searchpattern,
                                                                                       int PageNum, out int TotalRecords,
                                                                                       int PageSize)
       {
           TotalRecords = 0;
           ErrorCount = 0;
           Searchpattern = "";
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CityId", cityId);
               _objDataWrapper.AddParameter("@StateId", stateId);
               _objDataWrapper.AddParameter("@ExamId", examId);
               _objDataWrapper.AddParameter("@CourseId", courseId);
               _objDataWrapper.AddParameter("@PageNum", PageNum);
               _objDataWrapper.AddParameter("@PageSize", PageSize);
               _objDataWrapper.AddParameter("@CollegeManagement", collegeManagemnet);
               var ObjErrorCount =
                   (SqlParameter)
                   (_objDataWrapper.AddParameter("@ErrorCount", "", SqlDbType.Int, ParameterDirection.Output));
               var ObjTotalRecords =
                   (SqlParameter)
                   (_objDataWrapper.AddParameter("@TotalRowsNum", "", SqlDbType.Int, ParameterDirection.Output));
               var ObjSearchPattern =
                   (SqlParameter)
                   (_objDataWrapper.AddParameter("@SearchPattern", "", SqlDbType.VarChar, ParameterDirection.Output, 200));
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeSearch");
               objCollegeBranchList = BindCollegeListByName(_dataSet.Tables[0]);
               if (ObjErrorCount.Value != DBNull.Value || ObjErrorCount != null)
               {
                   ErrorCount = Convert.ToInt32(ObjErrorCount.Value);
               }

               if (ObjSearchPattern.Value != DBNull.Value || ObjSearchPattern != null)
               {
                   Searchpattern = Convert.ToString(ObjSearchPattern.Value);
               }
               if (ObjTotalRecords.Value != DBNull.Value || ObjTotalRecords != null)
               {
                   TotalRecords = Convert.ToInt32(ObjTotalRecords.Value);
               }
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeListByCourseExamStateCIty in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }

       public override List<SearchPriorityListingCollege> GetSearchPriorityListingCollege(int cityId, int stateId,
                                                                                          int examId, int courseId,
                                                                                          int collegeManagemnet,
                                                                                          out string searchPattern)
       {

           searchPattern = "";
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objSearchPriorityListingCollegeList = new List<SearchPriorityListingCollege>();
           try
           {
               _objDataWrapper.AddParameter("@CityId", cityId);
               _objDataWrapper.AddParameter("@StateId", stateId);
               _objDataWrapper.AddParameter("@ExamId", examId);
               _objDataWrapper.AddParameter("@CourseId", courseId);
               _objDataWrapper.AddParameter("@MngtId", collegeManagemnet);
               var objSearchPattern =
                   (SqlParameter)
                   (_objDataWrapper.AddParameter("@SearchPattern", "", SqlDbType.VarChar, ParameterDirection.Output, 4000));
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_PROC_GetSearchPriorityListingCollege");
               objSearchPriorityListingCollegeList = BindSearchPriorityListCollege(_dataSet.Tables[0]);

               searchPattern = Convert.ToString(objSearchPattern.Value);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetSearchPriorityListingCollege in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objSearchPriorityListingCollegeList;
       }

       public override List<CollegeBranchCourseProperty> GetCollegeCourseListByCollegeId(int collegeBranchId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchId", collegeBranchId);
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeCourseListByCollegeId");
               objCollegeBranchCourseProperty = BindCollegeCourseList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseListByCollegeId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseProperty> GetCollegeCourseListByBranchCourseId(int branchCourseId,string type=null)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseProperty>();
           try
           {
               _objDataWrapper.AddParameter("@BranchCourseId", branchCourseId);
               _objDataWrapper.AddParameter("@case", type);
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeListByCourse");
               objCollegeBranchCourseProperty = BindCollegeCourseList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseListByBranchCourseId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseStreamProperty> GetCollegeCourseStreamListByCollegeBranchId(int collegeBranchId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseStreamProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchId", collegeBranchId);
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeCourseStreamListByCollegeBranchId");
               objCollegeBranchCourseProperty = BindCollegeCourseStreamList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseStreamListByCollegeBranchId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseStreamProperty> GetCollegeCourseStreamDetailsByBranchCourseId(int branchCourseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseStreamProperty>();
           try
           {
               _objDataWrapper.AddParameter("@BranchCourseId", branchCourseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeCourseStreamListByCollegeBranchId");
               objCollegeBranchCourseProperty = BindCollegeCourseStreamList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseStreamDetailsByBranchCourseId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseStreamProperty> GetCollegeCourseStreamListByCollegeCourseStreamId(int collgeCourseStreamId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseStreamProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CourseStreamId", collgeCourseStreamId);
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeCourseStreamListByCollegeBranchId");
               objCollegeBranchCourseProperty = BindCollegeCourseStreamList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseStreamListByCollegeCourseStreamId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseStreamProperty> GetCollegeCourseStreamDetails()
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseStreamProperty>();
           try
           {
              
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeCourseStreamListByCollegeBranchId");
               objCollegeBranchCourseProperty = BindCollegeCourseStreamList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseStreamListByCollegeCourseStreamId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseExamProperty> GetCollegeCourseExamListByCollegeBranchId(int collegeBranchId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseExamProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchId", collegeBranchId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeCourseExamListById");
               objCollegeBranchCourseProperty = BindCollegeCourseExam(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseExamListByCollegeBranchId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseExamProperty> GetCollegeCourseExamDetailsByBranchCourseId(int branchCourseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseExamProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", branchCourseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeCourseExamListById");
               objCollegeBranchCourseProperty = BindCollegeCourseExam(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseExamDetailsByBranchCourseId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseExamProperty> GetCollegeCourseStreamListByCollegeExamId(int collegeExamId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseExamProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchCourseExamId", collegeExamId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeCourseExamListById");
               objCollegeBranchCourseProperty = BindCollegeCourseExam(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseStreamListByCollegeExamId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseFacilitiesProperty> GetCollegeCourseFacalityByCollegeBranchId(int collegeBranchId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseFacilitiesProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchId", collegeBranchId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeCourseFacalityId");
               objCollegeBranchCourseProperty = BindCollegeCourseFacality(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseFacalityByCollegeBranchId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseFacilitiesProperty> GetCollegeCourseFacalityDetailsByBranchCourseId(int branchCourseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseFacilitiesProperty>();
           try
           {
               _objDataWrapper.AddParameter("@BranchCourseId", branchCourseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeCourseFacalityId");
               objCollegeBranchCourseProperty = BindCollegeCourseFacality(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseFacalityDetailsByBranchCourseId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseFacilitiesProperty> GetCollegeCourseFacalityByFacalityId(int facalityId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseFacilitiesProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchCourseFacalityId", facalityId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeCourseFacalityId");
               objCollegeBranchCourseProperty = BindCollegeCourseFacality(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseFacalityByFacalityId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchRankProperty> GetCollegeCourseRankByCollegeBranchId(int collegeBranchId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchRankProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchId", collegeBranchId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeRankListByCollegeId");
               objCollegeBranchCourseProperty = BindCollegeCourseRank(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseRankByCollegeBranchId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchRankProperty> GetCollegeCourseRankByCollegeId(int collegeId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchRankProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchId", collegeId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_getCollegeRankById");
               objCollegeBranchCourseProperty = BindCollegeCourseRank(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseRankByCollegeId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchRankProperty> GetCollegeCourseRankByRankId(int rankId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchRankProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeRankId", rankId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeRankListByCollegeId");
               objCollegeBranchCourseProperty = BindCollegeCourseRank(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseRankByRankId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseHighlightsProperty> GetCollegeCourseHighLightsByCollegeBranchId(int collegeBranchId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseHighlightsProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchId", collegeBranchId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeHighLightsListByCollegeId");
               objCollegeBranchCourseProperty = BindCollegeCourseHighLights(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseHighLightsByCollegeBranchId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseHighlightsProperty> GetCollegeCourseHighLightsByCollegeBranchCourseId(int collegeBranchCourseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseHighlightsProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", collegeBranchCourseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeHighLightsListByCollegeId");
               objCollegeBranchCourseProperty = BindCollegeCourseHighLights(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseHighLightsByCollegeBranchCourseId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseHighlightsProperty> GetCollegeCourseHighLightsByHighLightsId(int highLightsId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseHighlightsProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegehighLightsId", highLightsId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeHighLightsListByCollegeId");
               objCollegeBranchCourseProperty = BindCollegeCourseHighLights(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseHighLightsByHighLightsId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseHostelProperty> GetCollegeCourseHostelByCollegeBranchId(int collegeBranchId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseHostelProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeBranchId", collegeBranchId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeCourseHostelByCollegeBranchId");
               objCollegeBranchCourseProperty = BindCollegeCourseHostel(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseHighLightsByHighLightsId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseHostelProperty> GetCollegeCourseHostelByHostelId(int hostelId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseHostelProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeHostelId", hostelId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeCourseHostelByCollegeBranchId");
               objCollegeBranchCourseProperty = BindCollegeCourseHostel(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseHighLightsByHighLightsId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchCourseHostelProperty> GetCollegeCourseHostelDetailsByBranchCourseId(int branchCourseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchCourseProperty = new List<CollegeBranchCourseHostelProperty>();
           try
           {
               _objDataWrapper.AddParameter("@BranchCourseId", branchCourseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeCourseHostelByCollegeBranchId");
               objCollegeBranchCourseProperty = BindCollegeCourseHostel(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseHighLightsByHighLightsId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchCourseProperty;
       }
       public override List<CollegeBranchProperty> GetAllCollegeBranchNameByCourseIdCollegeName(int CourseId, string CollegeName)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchProperty = new List<CollegeBranchProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CollegeName", CollegeName);
               _objDataWrapper.AddParameter("@CourseId", CourseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeListByCourse");
               objCollegeBranchProperty = GetAllCollegeBranchNameByCourseIdCollegeName(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeCourseHighLightsByHighLightsId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchProperty;
       }
       public override List<CollegeBranchProperty> GetMostViewdCollegeByCourse(int courseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchProperty = new List<CollegeBranchProperty>();
           try
           {
               _objDataWrapper.AddParameter("@CourseId", courseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetMostViewdCollege");
               objCollegeBranchProperty = BindMostViewdObject(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetMostViewdCollegeByCourse in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }

           return objCollegeBranchProperty;
       }
       public override List<CollegeBranchProperty> GetCollegeListByUniversityId(int universityId, int courseId=0)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var objCollegeBranchList = new List<CollegeBranchProperty>();
           try
           {
               _objDataWrapper.AddParameter("@UniversityId", universityId);
               _objDataWrapper.AddParameter("@CourseId", courseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeListByUniversity");
               objCollegeBranchList = BindCollegeListForUniversityList(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetCollegeListByUniversityId in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchList;
       }


       private List<CollegeBranchProperty> BindCollegeListForUniversityList(DataTable datatable)
       {
           var objCollegeBranchPropertyList = new List<CollegeBranchProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {

                       var objCollegeBranchProperty = new CollegeBranchProperty
                       {
                          
                           CollegeBranchCourseId = (datatable.Rows[j]["AjCollegeBranchCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseId"]),
                           CollegePopulaorName = (datatable.Rows[j]["AjCollegeBranchPopularName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchPopularName"]),
                           CollegeIdBranchId = (datatable.Rows[j]["AjCollegeBranchId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchId"]),
                           CollegeBranchName = (datatable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),
                           CollegeManagementTypeId = (datatable.Rows[j]["AjCollegeBranchManagementId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchManagementId"]),
                           CollegeManagementType = (datatable.Rows[j]["AjMasterValues"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjMasterValues"]),
                           CollegeBranchEst = (datatable.Rows[j]["AjCollegeBranchCourseEst"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchCourseEst"]),
                           CollegeBranchStateId = (datatable.Rows[j]["AjCollegeBranchStateId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchStateId"]),
                           CollegeBranchStateName = (datatable.Rows[j]["AjStateName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjStateName"]),
                           CollegeBranchCityId = (datatable.Rows[j]["AjCityId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCityId"]),
                           CollegeBranchCityName = (datatable.Rows[j]["AjCityName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCityName"]),
                           CollegeBranchStatus = (datatable.Rows[j]["AjCollegeBranchStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeBranchStatus"]),
                           CollegeBranchLogo = (datatable.Rows[j]["AjCollegeBranchLogo"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchLogo"]),
                           CourseId = (datatable.Rows[j]["AjCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCourseId"]),
                           CourseName = (datatable.Rows[j]["AjCourseName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCourseName"]),
                           CollegeBranchCourseSponserStatus = (datatable.Rows[j]["AjCollegeSponser"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeSponser"]),
                           CollegeBranchCourseOnlineStatus = (datatable.Rows[j]["AjCollegeOnlineParticipate"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeOnlineParticipate"])

                       };
                       objCollegeBranchPropertyList.Add(objCollegeBranchProperty);
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
               const string addInfo = "Error while executing BindCollegeListForUniversityList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchPropertyList;
       }
       private List<CollegeBranchProperty> BindMostViewdObject(DataTable datatable)
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
                           
                           CollegeBranchCourseId =(datatable.Rows[j]["AjCollegeBranchCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseId"]),
                           CollegePopulaorName =(datatable.Rows[j]["AjCollegeBranchPopularName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchPopularName"]),
                           CollegeIdBranchId =(datatable.Rows[j]["AjCollegeBranchId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchId"]),
                           CollegeBranchName =(datatable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),
                           CollegeManagementTypeId =(datatable.Rows[j]["AjCollegeBranchManagementId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchManagementId"]),
                           CollegeBranchEst =(datatable.Rows[j]["AjCollegeBranchEst"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchEst"]),
                           CollegeBranchAddrs =(datatable.Rows[j]["AjCollegeBranchAddress"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchAddress"]),
                           CollegeBranchMobileNo =(datatable.Rows[j]["AjCollegeBranchMobileNo"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchMobileNo"]) ,
                           CollegeBranchPinCode =(datatable.Rows[j]["AjCollegeBranchPinCode"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchPinCode"]),
                           CollegeBranchCountryId =(datatable.Rows[j]["AjCollegeBranchCountryId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCountryId"]) ,
                           CollegeBranchStateId =(datatable.Rows[j]["AjCollegeBranchStateId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchStateId"]) ,
                           CollegeBranchCityId =(datatable.Rows[j]["AjCollegeBranchCityId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCityId"]) ,
                           CollegeBranchLogo = (datatable.Rows[j]["AjCollegeBranchLogo"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchLogo"]),
                           CollegeManagementType =(datatable.Rows[j]["AjMasterValues"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjMasterValues"]) 

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
               const string addInfo = "Error while executing BindCollegeList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }


       private List<CollegeBranchGallery> BindCollegeGallery(DataTable datatable)
       { 
           var collegeGallerlist=new List<CollegeBranchGallery>();
            try
           {
               if (datatable.Rows.Count > 0)

               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {

                       var objCollegeGalleryProperty = new CollegeBranchGallery
                       {

                           CollegeBranchGalleryId = (datatable.Rows[j]["AjCollegeGalleryId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeGalleryId"]),
                           CollegeBranchGalleryImageName = (datatable.Rows[j]["AjCollegeGalleryImageName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeGalleryImageName"]),
                           CollegeBranchGalleryImageTitle = (datatable.Rows[j]["AjCollegeGalleryImageTitle"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeGalleryImageTitle"]),
                           CollegeBranchGalleryImageStatus = (datatable.Rows[j]["AjCollegeGalleryStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeGalleryStatus"]),
                           CollegeBranchId = (datatable.Rows[j]["AjCollegeBranchId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchId"]),
                           CollegeBranchName = (datatable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),
                          
                       };

                       collegeGallerlist.Add(objCollegeGalleryProperty);
                   }
               }
            }
           catch(Exception ex)
            {
           
           
           }
       return collegeGallerlist;
       
       }
       private List<CollegeBranchProperty> BindCollegeListByCollege(DataTable datatable)
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
                           InstituteTypeId = (datatable.Rows[j]["AjInstituteTypeId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjInstituteTypeId"]),
                           
                           CollegeGroupId = (datatable.Rows[j]["AjCollegeGroupId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeGroupId"]),
                           CollegePopulaorName = (datatable.Rows[j]["AjCollegeBranchPopularName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchPopularName"]),
                           
                           CollegeIdBranchId = (datatable.Rows[j]["AjCollegeBranchId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchId"]),
                           CollegeBranchName = (datatable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),
                           CollegeManagementTypeId = (datatable.Rows[j]["AjCollegeBranchManagementId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchManagementId"]),
                           
                           CollegeBranchEst = (datatable.Rows[j]["AjCollegeBranchEst"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchEst"]),
                           CoillegeBranchEmailId = (datatable.Rows[j]["AjCollegeBranchEmailId"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchEmailId"]),
                           CollegeBranchDesc = (datatable.Rows[j]["AjCollegeBranchDesc"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchDesc"]),
                           CollegeBranchAddrs = (datatable.Rows[j]["AjCollegeBranchAddress"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchAddress"]),
                           CollegeBranchMobileNo = (datatable.Rows[j]["AjCollegeBranchMobileNo"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchMobileNo"]),
                           CollegeBranchPinCode = (datatable.Rows[j]["AjCollegeBranchPinCode"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchPinCode"]),
                           CollegeBranchFax = (datatable.Rows[j]["AjCollegeBranchFax"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchFax"]),
                           CollegeBranchWebsite = (datatable.Rows[j]["AjCollegeBranchWebSite"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchWebSite"]),
                           CollegeBranchCountryId = (datatable.Rows[j]["AjCollegeBranchCountryId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCountryId"]),
                           CollegeBranchStateId = (datatable.Rows[j]["AjCollegeBranchStateId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchStateId"]),
                           CollegeBranchCityId = (datatable.Rows[j]["AjCollegeBranchCityId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCityId"]),
                           CollegeBranchStatus = (datatable.Rows[j]["AjCollegeBranchStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeBranchStatus"]),
                           CollegeBranchLogo = (datatable.Rows[j]["AjCollegeBranchLogo"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchLogo"]),
                           
                     

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
               const string addInfo = "Error while executing BindCollegeList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }

       private  List<CollegeBranchProperty> BindCollegeList(DataTable datatable)
       {
           var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
          
           var objCollegeGroupList = new List<CollegeBranchProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       
                       var objCollegeGroupProperty = new CollegeBranchProperty
                       {

                           CollegeAssociationType = datatable.Columns.Contains("AjCollegeAssociationCategoryName") ? ((datatable.Rows[j]["AjCollegeAssociationCategoryName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeAssociationCategoryName"])) : null,
                    

                           InstituteTypeId = (datatable.Rows[j]["AjInstituteTypeId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjInstituteTypeId"]),
                           CollegeBranchCourseId = (datatable.Rows[j]["AjCollegeBranchCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseId"]),

                           //CollegeBranchCoursePlacementAvgSalaryOffered = (datatable.Rows[j]["AjCollegeBranchPlacementAvgSalaryOffered"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchPlacementAvgSalaryOffered"]),


                           CollegeGroupId = (datatable.Rows[j]["AjCollegeGroupId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeGroupId"]),
                           CollegePopulaorName = (datatable.Rows[j]["AjCollegeBranchPopularName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchPopularName"]),
                           CollegeAssociationId = (datatable.Rows[j]["AjCollegeAssociationCategoryId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeAssociationCategoryId"]),
                           CollegeIdBranchId = (datatable.Rows[j]["AjCollegeBranchId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchId"]),
                           CollegeBranchName = (datatable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),
                           CollegeManagementTypeId = (datatable.Rows[j]["AjCollegeBranchManagementId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchManagementId"]),
                           CollegeManagementType = (datatable.Rows[j]["AjMasterValues"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjMasterValues"]),
                           CollegeBranchEst = (datatable.Rows[j]["AjCollegeBranchEst"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchEst"]),
                           CoillegeBranchEmailId = (datatable.Rows[j]["AjCollegeBranchEmailId"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchEmailId"]),
                           CollegeBranchDesc = (datatable.Rows[j]["AjCollegeBranchDesc"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchDesc"]),
                           CollegeBranchAddrs = (datatable.Rows[j]["AjCollegeBranchAddress"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchAddress"]),
                           CollegeBranchMobileNo = (datatable.Rows[j]["AjCollegeBranchMobileNo"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchMobileNo"]),
                           CollegeBranchPinCode = (datatable.Rows[j]["AjCollegeBranchPinCode"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchPinCode"]),
                           CollegeBranchFax = (datatable.Rows[j]["AjCollegeBranchFax"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchFax"]),
                           CollegeBranchWebsite = (datatable.Rows[j]["AjCollegeBranchWebSite"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchWebSite"]),
                           CollegeBranchCountryId =(datatable.Rows[j]["AjCollegeBranchCountryId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCountryId"]),
                           CollegeBranchStateId = (datatable.Rows[j]["AjCollegeBranchStateId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchStateId"]),
                           CollegeBranchCityId = (datatable.Rows[j]["AjCollegeBranchCityId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCityId"]),
                           CollegeBranchStatus = (datatable.Rows[j]["AjCollegeBranchStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeBranchStatus"]),
                           CollegeBranchLogo = (datatable.Rows[j]["AjCollegeBranchLogo"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchLogo"]),
                           CourseName = (datatable.Rows[j]["AjCourseName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCourseName"]),
                           CourseId = (datatable.Rows[j]["AjCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCourseId"]),
                           CollegeBranchCityName = (datatable.Rows[j]["AjCityName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCityName"]),
                           CollegeBranchStateName = (datatable.Rows[j]["AjStateName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjStateName"]),
                           CollegeUrl = "/college-details/" + Common.RemoveIllegealFromCourseBL(Convert.ToString(datatable.Rows[j]["AjCourseName"])).ToLower() + "/" + Common.RemoveIllegalCharactersBL(Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"])).ToLower(),

                           //CollegeCourseEligibiltyName = Convert.ToString(datatable.Rows[j]["AjCollegeCourseEligibiltyName"]),
                           CollegeCourseEligibiltyName = datatable.Columns.Contains("AjCollegeCourseEligibiltyName") ? ((datatable.Rows[j]["AjCollegeCourseEligibiltyName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeCourseEligibiltyName"])) : null,
                           CollegeCoures10EligibilityPer = datatable.Columns.Contains("Aj10Eligibilitypercentage") ? ((datatable.Rows[j]["Aj10Eligibilitypercentage"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["Aj10Eligibilitypercentage"])) : null,
                           CollegeCoures12EligibilityPer = datatable.Columns.Contains("Aj12Eligibilitypercentage") ? ((datatable.Rows[j]["Aj12Eligibilitypercentage"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["Aj12Eligibilitypercentage"])) : null,
                           CollegeCoures15EligibilityPer = datatable.Columns.Contains("Aj15Eligibilitypercentage") ? ((datatable.Rows[j]["Aj15Eligibilitypercentage"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["Aj15Eligibilitypercentage"])) : null,
                           CollegeInstruction = datatable.Columns.Contains("AjInstruction") ? ((datatable.Rows[j]["AjInstruction"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjInstruction"])) : null,
                           CollegeOnlineParticipateStatus = datatable.Columns.Contains("AjCollegeOnlineParticipate") && ((!(datatable.Rows[j]["AjCollegeOnlineParticipate"] is DBNull)) && Convert.ToBoolean(datatable.Rows[j]["AjCollegeOnlineParticipate"])),
                           CollegeBranchCourseVirtualOnlineStatus = datatable.Columns.Contains("AjCollegeOnlineParticipationVirualStatus") && ((!(datatable.Rows[j]["AjCollegeOnlineParticipationVirualStatus"] is DBNull)) && Convert.ToBoolean(datatable.Rows[j]["AjCollegeOnlineParticipationVirualStatus"])),

                           CollegeOverallRating = datatable.Columns.Contains("AjCollegeOverallRating") ? ((datatable.Rows[j]["AjCollegeOverallRating"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeOverallRating"])) : null,
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
               const string addInfo = "Error while executing BindCollegeList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }

       private List<CollegeBranchCourseProperty> BindCollegeCourseList(DataTable datatable)
       {

           var objCollegeGroupList = new List<CollegeBranchCourseProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {

                       var objCollegeGroupProperty = new CollegeBranchCourseProperty
                                                         {
                                                             CollegeBranchCourseId =
                                                                 (datatable.Rows[j]["AjCollegeBranchCourseId"] is DBNull)
                                                                     ? 0
                                                                     : Convert.ToInt32(
                                                                         datatable.Rows[j]["AjCollegeBranchCourseId"]),
                                                             CourseId =
                                                                 (datatable.Rows[j]["AjCourseId"] is DBNull)
                                                                     ? 0
                                                                     : Convert.ToInt32(datatable.Rows[j]["AjCourseId"]),
                                                             UniversityId =
                                                                 (datatable.Rows[j]["AjUniversityId"] is DBNull)
                                                                     ? 0
                                                                     : Convert.ToInt32(
                                                                         datatable.Rows[j]["AjUniversityId"]),
                                                             HasHostel =
                                                                 (datatable.Rows[j]["AjHasHostel"] is DBNull)
                                                                     ? false
                                                                     : Convert.ToBoolean(
                                                                         datatable.Rows[j]["AjHasHostel"]),
                                                             CollegeBranchCourseDesc =
                                                                 (datatable.Rows[j]["AjCollegeBranchCourseDesc"] is
                                                                  DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         datatable.Rows[j]["AjCollegeBranchCourseDesc"]),
                                                             CourseName =
                                                                 (datatable.Rows[j]["AjCourseName"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         datatable.Rows[j]["AjCourseName"]),
                                                             CollegeBranchCourseEst =
                                                                 (datatable.Rows[j]["AjCollegeBranchCourseEst"] is
                                                                  DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         datatable.Rows[j]["AjCollegeBranchCourseEst"]),
                                                             CollegeBranchCourseTitle =
                                                                 (datatable.Rows[j]["AjCollegeTitle"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         datatable.Rows[j]["AjCollegeTitle"]),
                                                             CollegeBranchCourseMetaDesc =
                                                                 (datatable.Rows[j]["AjCollegeMetaDesc"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         datatable.Rows[j]["AjCollegeMetaDesc"]),
                                                             CollegeBranchCourseMetaTag =
                                                                 (datatable.Rows[j]["AjCollegeMetaTag"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         datatable.Rows[j]["AjCollegeMetaTag"]),
                                                             CollegeBranchCourseUrl =
                                                                 (datatable.Rows[j]["AjCollegeUrl"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         datatable.Rows[j]["AjCollegeUrl"]),
                                                             CollegeBranchCourseStatus =
                                                                 (datatable.Rows[j]["AjCollegeBranchCourseStatus"] is
                                                                  DBNull)
                                                                     ? false
                                                                     : Convert.ToBoolean(
                                                                         datatable.Rows[j]["AjCollegeBranchCourseStatus"
                                                                             ]),
                                                             CollegeBranchCourseSponserStatus =
                                                                 (datatable.Rows[j]["AjCollegeSponser"] is DBNull)
                                                                     ? false
                                                                     : Convert.ToBoolean(
                                                                         datatable.Rows[j]["AjCollegeSponser"]),
                                                             CollegeBranchCourseLogo =
                                                                 (datatable.Rows[j]["AjCollegeBranchLogo"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         datatable.Rows[j]["AjCollegeBranchLogo"]),
                                                             CollegeBranchCourseManagement =
                                                                 (datatable.Rows[j]["AjMasterValues"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         datatable.Rows[j]["AjMasterValues"]),
                                                             CollegeBranchName =
                                                                 (datatable.Rows[j]["AjCollegeBranchName"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         datatable.Rows[j]["AjCollegeBranchName"]),
                                                             CollegeBranchCourseHelplineNo = (datatable.Columns.Contains("AjCollegeBranchCourseHelplineNo")?(
                                                                 (datatable.Rows[j]["AjCollegeBranchCourseHelplineNo"]
                                                                  is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         datatable.Rows[j][
                                                                             "AjCollegeBranchCourseHelplineNo"])):null),
                                                             IsBookSeatVisible =
                                                                 (datatable.Rows[j]["AjIsBookSeatVisible"] is DBNull)
                                                                     ? true
                                                                     : Convert.ToBoolean(
                                                                         datatable.Rows[j]["AjIsBookSeatVisible"])
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
               const string addInfo = "Error while executing BindCollegeCourseList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }

       private List<CollegeBranchCourseStreamProperty> BindCollegeCourseStreamList(DataTable datatable)
       {

           var objCollegeGroupList = new List<CollegeBranchCourseStreamProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objCollegeGroupProperty = new CollegeBranchCourseStreamProperty
                       {
                           CollegeBranchCourseStreamId=(datatable.Rows[j]["AjCollegeBranchCourseStreamId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseStreamId"]),
                           CollegeBranchCourseId =(datatable.Rows[j]["AjCollegeBranchCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseId"]),
                           StreamId = (datatable.Rows[j]["AjCourseStreamId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCourseStreamId"]),
                           CourseId = (datatable.Rows[j]["AjCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCourseId"]),
                           CollegeBranchCourseStreamModeId = (datatable.Rows[j]["AjCollegeBranchCourseStreamModeId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseStreamModeId"]),
                           CollegeBranchCourseStreamLateralEntrySeat = (datatable.Rows[j]["AjCollegeBranchCourseStreamLateralEntrySeat"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchCourseStreamLateralEntrySeat"]),

                           CollegeBranchCourseStreamSeat = (datatable.Rows[j]["AjCollegeBranchCourseStreamSeat"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchCourseStreamSeat"]),
                           CourseName = (datatable.Rows[j]["AjCourseName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCourseName"]),
                           StreamName = (datatable.Rows[j]["AjCourseStreamName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCourseStreamName"]),
                           CollegeBranchCourseStreamDuration = (datatable.Rows[j]["AJCollegeBranchCourseStreamDuration"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AJCollegeBranchCourseStreamDuration"]),
                           CollegeBranchCourseStreamFees = (datatable.Rows[j]["AjCollegeBranchCourseStreamFees"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchCourseStreamFees"]),
                           CollegeBranchCourseStreamEligibity = (datatable.Rows[j]["AJCollegeBranchCourseStreamEligibilty"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AJCollegeBranchCourseStreamEligibilty"]),
                           CollegeBranchCourseStreamDesc = (datatable.Rows[j]["AjCollegeBranchCourseStreamDesc"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchCourseStreamDesc"]),
                           CollegeBranchCourseStreamManagementQuotaSeat = (datatable.Rows[j]["AjCollegeBranchCourseStreamManagementQuotaSeat"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchCourseStreamManagementQuotaSeat"]),
                           CollegeBranchCourseStreamStatus = (datatable.Rows[j]["AjCollegeBranchCourseStreamStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeBranchCourseStreamStatus"]),
                           CollegeBranchCourseStreamModeName = (datatable.Rows[j]["AjMasterValues"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjMasterValues"]),
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
               const string addInfo = "Error while executing BindCollegeCourseList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }
       private List<CollegeBranchCourseExamProperty> BindCollegeCourseExam(DataTable datatable)
       {

           var objCollegeGroupList = new List<CollegeBranchCourseExamProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objCollegeGroupProperty = new CollegeBranchCourseExamProperty
                       {
                           CollegeBranchCourseExamId = (datatable.Rows[j]["AjCollegeCourseExamId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeCourseExamId"]),

                           CollegeBranchCourseId = (datatable.Rows[j]["AjCollegeBranchCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseId"]),

                           ExamId = (datatable.Rows[j]["AjExamId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjExamId"]),

                           CourseId = (datatable.Rows[j]["AjCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCourseId"]),

                           CourseName = (datatable.Rows[j]["AjCourseName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCourseName"]),

                           ExamName = (datatable.Rows[j]["AjExamName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjExamName"]),

                           CollegeCourseExamStatus = (datatable.Rows[j]["AjCollegeBranchCourseExamStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeBranchCourseExamStatus"]),

                           ExamEligibilty = (datatable.Rows[j]["AjExamEligiblityCriteria"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjExamEligiblityCriteria"]),

                           ExamPopularName = (datatable.Rows[j]["AjExamPopularName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjExamPopularName"]),
                           
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
               const string addInfo = "Error while executing BindCollegeCourseList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }
       private List<CollegeBranchCourseFacilitiesProperty> BindCollegeCourseFacality(DataTable datatable)
       {

           var objCollegeGroupList = new List<CollegeBranchCourseFacilitiesProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objCollegeGroupProperty = new CollegeBranchCourseFacilitiesProperty
                       {
                           CourseId =(datatable.Columns.Contains("AjCourseId")==true)?(datatable.Rows[j]["AjCourseId"] is DBNull) ?0: Convert.ToInt32(datatable.Rows[j]["AjCourseId"]):0,
                          
                           
                           CourseName = (datatable.Columns.Contains("AjCourseName")==true)? (datatable.Rows[j]["AjCourseName"] is DBNull) ?null: Convert.ToString(datatable.Rows[j]["AjCourseName"]) :null,



                           CollegeBranchCourseFacilitieId = (datatable.Rows[j]["AjCollegeFacilitiesId"] is DBNull)? 0:Convert.ToInt32(datatable.Rows[j]["AjCollegeFacilitiesId"]),
                           CollegeBranchCourseId = (datatable.Rows[j]["AjCollegeBranchCourseId"] is DBNull) ?0:Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseId"]),
                            
                           
                           CollegeBranchCourseFacilitieName =(datatable.Rows[j]["AjCollegeFacilitiesName"] is DBNull) ?null: Convert.ToString(datatable.Rows[j]["AjCollegeFacilitiesName"]),
                           CollegeBranchCourseFacilitieDesc = (datatable.Rows[j]["AjCollegeFacilitiesDesc"] is DBNull) ? null:Convert.ToString(datatable.Rows[j]["AjCollegeFacilitiesDesc"]),
                           CollegeBranchCourseFacilitieStatus = Convert.ToBoolean(datatable.Rows[j]["AjCollegeFacilitiesStatus"] is DBNull)?false: Convert.ToBoolean(datatable.Rows[j]["AjCollegeFacilitiesStatus"])
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
               const string addInfo = "Error while executing BindCollegeCourseList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }
       private List<CollegeBranchRankProperty> BindCollegeCourseRank(DataTable datatable)
       {

           var objCollegeGroupList = new List<CollegeBranchRankProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objCollegeGroupProperty = new CollegeBranchRankProperty
                       {
                           CollegeRankId = (datatable.Rows[j]["AjCollegeRankId"]  is DBNull) ?0: Convert.ToInt32(datatable.Rows[j]["AjCollegeRankId"]) ,
                           CollegeRankSourceId = (datatable.Rows[j]["AjCollegeRankSourceId"] is DBNull) ?0: Convert.ToInt32(datatable.Rows[j]["AjCollegeRankSourceId"]) ,
                           CollegeBranchCourseId = (datatable.Rows[j]["AjCollegeBranchCourseId"]  is DBNull) ?0: Convert.ToInt32( datatable.Rows[j]["AjCollegeBranchCourseId"]),
                           CourseId = (datatable.Rows[j]["AjCourseId"] is DBNull) ?0: Convert.ToInt32( datatable.Rows[j]["AjCourseId"]),
                           CourseName = (datatable.Rows[j]["AjCourseName"]   is DBNull) ?null: Convert.ToString(datatable.Rows[j]["AjCourseName"] ),
                           CollegeRankYear = (datatable.Rows[j]["AjCollegeRankYear"] is DBNull) ?0: Convert.ToInt32(datatable.Rows[j]["AjCollegeRankYear"] ),
                           CollegeOverAllRank = (datatable.Rows[j]["AjCollegeRankOverall"] is DBNull) ?null: Convert.ToString( datatable.Rows[j]["AjCollegeRankOverall"]),
                           CollegeRankStatus = (datatable.Rows[j]["AjCollegeRankStatus"] is DBNull) ?false: Convert.ToBoolean( datatable.Rows[j]["AjCollegeRankStatus"]),
                           RankSourceName =(datatable.Rows[j]["AjCollegeRankSourceName"] is DBNull) ?null: Convert.ToString( datatable.Rows[j]["AjCollegeRankSourceName"] ),
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
               const string addInfo = "Error while executing BindCollegeCourseList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }
       private List<CollegeBranchCourseHighlightsProperty> BindCollegeCourseHighLights(DataTable datatable)
       {

           var objCollegeGroupList = new List<CollegeBranchCourseHighlightsProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objCollegeGroupProperty = new CollegeBranchCourseHighlightsProperty
                       {
                            CollegeBranchCourseHighlightId= Convert.ToInt32(datatable.Rows[j]["AjCollegeHighlightsId"].ToString() != "" ? datatable.Rows[j]["AjCollegeHighlightsId"] : "0"),
                           
                           CollegeBranchCourseId = Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseId"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchCourseId"] : "0"),
                           CourseId = Convert.ToInt32(datatable.Rows[j]["AjCourseId"].ToString() != "" ? datatable.Rows[j]["AjCourseId"] : "0"),
                           CourseName = Convert.ToString(datatable.Rows[j]["AjCourseName"].ToString() != "" ? datatable.Rows[j]["AjCourseName"] : "N/A"),
                            CollegeBranchCourseHighlight = Convert.ToString(datatable.Rows[j]["AjCollegeHighlights"].ToString() != "" ? datatable.Rows[j]["AjCollegeHighlights"] : "N/A"),
                           
                            CollegeBranchCourseHighlightStatus= Convert.ToBoolean(datatable.Rows[j]["AjCollegeHighlightStatus"].ToString() != "" ? datatable.Rows[j]["AjCollegeHighlightStatus"] : false),
                         
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
               const string addInfo = "Error while executing BindCollegeCourseHighLights in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }
       private static List<CollegeBranchCourseHostelProperty> BindCollegeCourseHostel(DataTable datatable)
       {

           var objCollegeGroupList = new List<CollegeBranchCourseHostelProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objCollegeGroupProperty = new CollegeBranchCourseHostelProperty
                       {
                           CollegeBranchCourseHostelId = (datatable.Rows[j]["AjCollegeHostelId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeHostelId"]),
                           CollegeBranchCourseId = (datatable.Rows[j]["AjCollegeBranchCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseId"]),
                           CourseId = (datatable.Rows[j]["AjCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCourseId"]),
                           CourseName =(datatable.Rows[j]["AjCourseName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCourseName"]),
                           HostelCategoryId = (datatable.Rows[j]["AjHostelCategoryId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjHostelCategoryId"]),
                           HostelCategoryName = (datatable.Rows[j]["AjHostelCategoryName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjHostelCategoryName"]),
                           CollegeBranchCourseHostelLocation = (datatable.Rows[j]["AjCollegeHostelLocation"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeHostelLocation"]),
                           IsCollegeBranchCourseHostelHasInternet = (datatable.Rows[j]["AjCollegeHostelHasInternet"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeHostelHasInternet"]),
                           IsCollegeBranchCourseHostelHasLoundry = (datatable.Rows[j]["AjCollegeHostelHasLoundry"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeHostelHasLoundry"]),
                           IsCollegeBranchCourseHostelHasPowerBackup = (datatable.Rows[j]["AjCollegeHostelHasPowerBackup"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeHostelHasPowerBackup"]),
                           CollegeBranchCourseHostelCharge = (datatable.Rows[j]["AjCollegeHostelCharge"] is DBNull) ? "NA" : Convert.ToString(datatable.Rows[j]["AjCollegeHostelCharge"]),
                           IsCollegeBranchCourseHostelHasAC = (datatable.Rows[j]["AjCollegeHostelHasAC"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeHostelHasAC"]),
                           CollegeBranchCourseHostelStatus = (datatable.Rows[j]["AjCollegeHostelStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeHostelStatus"])


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
               const string addInfo = "Error while executing BindCollegeCourseHighLights in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }

       private List<CollegeBranchProperty> GetAllCollegeBranchNameByCourseIdCollegeName(DataTable datatable)
       {
           var objCollegeDetails = new List<CollegeBranchProperty>();

           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var collegedetails = new CollegeBranchProperty
                       {
                           CollegeBranchLogo = (datatable.Rows[j]["AjCollegeBranchLogo"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchLogo"]),
                           CollegeBranchCourseId = Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseId"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchCourseId"] : "0"),
                           CollegeBranchName = Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchName"] : "N/A"),
                           CourseName = Convert.ToString(datatable.Rows[j]["AjCourseName"].ToString() != "" ? datatable.Rows[j]["AjCourseName"] : "N/A"),
                           CollegeBranchCityName = Convert.ToString(datatable.Rows[j]["AjCityName"].ToString() != "" ? datatable.Rows[j]["AjCityName"] : "N/A"),
                           CollegeManagementType = Convert.ToString(datatable.Rows[j]["AjMasterValues"].ToString() != "" ? datatable.Rows[j]["AjMasterValues"] : "N/A"),
                           CollegeBranchEst = Convert.ToString(datatable.Rows[j]["AjCollegeBranchEst"].ToString() != "" ? datatable.Rows[j]["AjCollegeBranchEst"] : "N/A"),
                            Hostel = Convert.ToBoolean(datatable.Rows[j]["AjHasHostel"].ToString() != "" ? datatable.Rows[j]["AjHasHostel"] : "N/A"),
                           UniversityName = Convert.ToString(datatable.Rows[j]["AjUniversityName"].ToString() != "" ? datatable.Rows[j]["AjUniversityName"] : "N/A"),
                          CourseId = Convert.ToInt32(datatable.Rows[j]["AjCourseId"].ToString() != "" ? datatable.Rows[j]["AjCourseId"] : "N/A"),
                       
                       };
                       objCollegeDetails.Add(collegedetails);
                   }
               }

           }
           catch(Exception ex)
           {

               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetAllCollegeBranchNameByCourseIdCollegeName in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           
           }

           return objCollegeDetails;
       }
       private List<CollegeBranchProperty> BindCollegeListByName(DataTable dataTable)
       {

           var objCollegeGroupList = new List<CollegeBranchProperty>();
           try
           {
               if (dataTable.Rows.Count > 0)
               {
                   for (var j = 0; j < dataTable.Rows.Count; j++)
                   {

                       var objCollegeGroupProperty = new CollegeBranchProperty
                                                         {
                                                             InstituteTypeId =
                                                                 (dataTable.Rows[j]["AjInstituteTypeId"] is DBNull)
                                                                     ? 0
                                                                     : Convert.ToInt32(
                                                                         dataTable.Rows[j]["AjInstituteTypeId"]),
                                                             UniversityId =
                                                                 (dataTable.Rows[j]["AjUniversityId"] is DBNull)
                                                                     ? 0
                                                                     : Convert.ToInt32(
                                                                         dataTable.Rows[j]["AjUniversityId"]),
                                                             CollegeGroupId =
                                                                 (dataTable.Rows[j]["AjCollegeGroupId"] is DBNull)
                                                                     ? 0
                                                                     : Convert.ToInt32(
                                                                         dataTable.Rows[j]["AjCollegeGroupId"]),
                                                             UniversityName =
                                                                 (dataTable.Rows[j]["AjUniversityName"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         dataTable.Rows[j]["AjUniversityName"]),
                                                             CollegePopulaorName =
                                                                 (dataTable.Rows[j]["AjCollegeBranchPopularName"] is
                                                                  DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         dataTable.Rows[j]["AjCollegeBranchPopularName"]),
                                                             CollegeIdBranchId =
                                                                 (dataTable.Rows[j]["AjCollegeBranchId"] is DBNull)
                                                                     ? 0
                                                                     : Convert.ToInt32(
                                                                         dataTable.Rows[j]["AjCollegeBranchId"]),
                                                             CollegeBranchName =
                                                                 (dataTable.Rows[j]["AjCollegeBranchName"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         dataTable.Rows[j]["AjCollegeBranchName"]),
                                                             CollegeManagementTypeId =
                                                                 (dataTable.Rows[j]["AjCollegeBranchManagementId"] is
                                                                  DBNull)
                                                                     ? 0
                                                                     : Convert.ToInt32(
                                                                         dataTable.Rows[j]["AjCollegeBranchManagementId"
                                                                             ]),
                                                             CollegeBranchEst =
                                                                 (dataTable.Rows[j]["AjCollegeBranchEst"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         dataTable.Rows[j]["AjCollegeBranchEst"]),
                                                             CollegeBranchDesc =
                                                                 (dataTable.Rows[j]["AjCollegeBranchDesc"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         dataTable.Rows[j]["AjCollegeBranchDesc"]),
                                                             CollegeBranchAddrs =
                                                                 (dataTable.Rows[j]["AjCollegeBranchAddress"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         dataTable.Rows[j]["AjCollegeBranchAddress"]),
                                                             CoillegeBranchEmailId =
                                                                 (dataTable.Rows[j]["AjCollegeBranchEmailId"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         dataTable.Rows[j]["AjCollegeBranchEmailId"]),
                                                             CollegeBranchMobileNo =
                                                                 (dataTable.Rows[j]["AjCollegeBranchMobileNo"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         dataTable.Rows[j]["AjCollegeBranchMobileNo"]),
                                                             CollegeBranchPinCode =
                                                                 (dataTable.Rows[j]["AjCollegeBranchPinCode"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         dataTable.Rows[j]["AjCollegeBranchPinCode"]),
                                                             CollegeBranchFax =
                                                                 (dataTable.Rows[j]["AjCollegeBranchFax"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         dataTable.Rows[j]["AjCollegeBranchFax"]),
                                                             CollegeBranchWebsite =
                                                                 (dataTable.Rows[j]["AjCollegeBranchWebSite"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         dataTable.Rows[j]["AjCollegeBranchWebSite"]),
                                                             CollegeBranchCountryId =
                                                                 (dataTable.Rows[j]["AjCollegeBranchCountryId"] is
                                                                  DBNull)
                                                                     ? 0
                                                                     : Convert.ToInt32(
                                                                         dataTable.Rows[j]["AjCollegeBranchCountryId"]),
                                                             CollegeBranchStateId =
                                                                 (dataTable.Rows[j]["AjCollegeBranchStateId"] is DBNull)
                                                                     ? 0
                                                                     : Convert.ToInt32(
                                                                         dataTable.Rows[j]["AjCollegeBranchStateId"]),
                                                             CollegeBranchCityId =
                                                                 (dataTable.Rows[j]["AjCollegeBranchCityId"] is DBNull)
                                                                     ? 0
                                                                     : Convert.ToInt32(
                                                                         dataTable.Rows[j]["AjCollegeBranchCityId"]),
                                                             CollegeBranchStatus =
                                                                 (dataTable.Rows[j]["AjCollegeBranchStatus"] is DBNull) || Convert.ToBoolean(
                                                                     dataTable.Rows[j]["AjCollegeBranchStatus"]),
                                                             CourseId =
                                                                 (dataTable.Rows[j]["AjCourseId"] is DBNull)
                                                                     ? 0
                                                                     : Convert.ToInt32(dataTable.Rows[j]["AjCourseId"]),
                                                             CourseName =
                                                                 (dataTable.Rows[j]["AjCourseName"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         dataTable.Rows[j]["AjCourseName"]),
                                                             CollegeBranchLogo =
                                                                 (dataTable.Rows[j]["AjCollegeBranchLogo"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         dataTable.Rows[j]["AjCollegeBranchLogo"]),
                                                             CollegeBranchCityName =
                                                                 (dataTable.Rows[j]["AjCityName"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(dataTable.Rows[j]["AjCityName"]),
                                                             CollegeBranchStateName =
                                                                 (dataTable.Rows[j]["AjStateName"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(dataTable.Rows[j]["AjStateName"]),
                                                             CollegeManagementType =
                                                                 (dataTable.Rows[j]["AjMasterValues"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         dataTable.Rows[j]["AjMasterValues"]),
                                                             CollegeBranchCourseId =
                                                                 (dataTable.Rows[j]["AjCollegeBranchCourseId"] is DBNull)
                                                                     ? 0
                                                                     : Convert.ToInt32(
                                                                         dataTable.Rows[j]["AjCollegeBranchCourseId"]),
                                                             CollegeBranchCourseSponserStatus =
                                                                 (!(dataTable.Rows[j]["AjCollegeSponser"] is DBNull)) && Convert.ToBoolean(
                                                                     dataTable.Rows[j]["AjCollegeSponser"]),
                                                             CollegeBranchCourseOnlineStatus =
                                                                 (!(dataTable.Rows[j]["AjCollegeOnlineParticipate"] is
                                                                    DBNull)) && Convert.ToBoolean(
                                                                        dataTable.Rows[j]["AjCollegeOnlineParticipate"]),
                                                             CollegePhoneNo =
                                                                 (dataTable.Rows[j]["AjCollegeBranchPhoneNo"] is DBNull)
                                                                     ? null
                                                                     : Convert.ToString(
                                                                         dataTable.Rows[j]["AjCollegeBranchPhoneNo"]),
                                                             CollegeBranchCourseHelplineNo =
                                                                 Convert.ToString(
                                                                     dataTable.Columns.Contains(
                                                                         "AjCollegeBranchCourseHelplineNo")
                                                                         ? (dataTable.Rows[j][
                                                                             "AjCollegeBranchCourseHelplineNo"].ToString
                                                                                () != ""
                                                                                ? dataTable.Rows[j][
                                                                                    "AjCollegeBranchCourseHelplineNo"]
                                                                                : null)
                                                                         : null),
                                                             HelpLineNumber =
                                                                 Convert.ToString(
                                                                     dataTable.Columns.Contains("AjHelplineNo")
                                                                         ? (dataTable.Rows[j]["AjHelplineNo"].ToString() !=
                                                                            ""
                                                                                ? dataTable.Rows[j]["AjHelplineNo"]
                                                                                : null)
                                                                         : null),
                                                             CourseIsBookSeatVisible =
                                                                 dataTable.Columns.Contains("CourseIsBookSeat") && (!string.IsNullOrEmpty(
                                                                     Convert.ToString(
                                                                         dataTable.Rows[j]["CourseIsBookSeat"])) && Convert.ToBoolean(
                                                                             dataTable.Rows[j]["CourseIsBookSeat"])),
                                                             CollegeIsBookSeatVisible =
                                                                 dataTable.Columns.Contains("CollegeIsBookSeatVisible") && (!string.IsNullOrEmpty(
                                                                     Convert.ToString(
                                                                         dataTable.Rows[j][
                                                                             "CollegeIsBookSeatVisible"])) && Convert.ToBoolean(
                                                                                 dataTable.Rows[j][
                                                                                     "CollegeIsBookSeatVisible"])),

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
               const string addInfo = "Error while executing BindCollegeListByName in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }

       private List<CollegeBranchProperty> BindTopRankedCollege(DataTable datatable)
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
                                                             CollegeAssociationId =
                                                    Convert.ToInt32(
                                                        datatable.Columns.Contains("AjCollegeAssociationCategoryId")
                                                            ? (!string.IsNullOrEmpty(datatable.Rows[j]["AjCollegeAssociationCategoryId"].ToString())
                                                               ? datatable.Rows[j]["AjCollegeAssociationCategoryId"]
                                                                   : 0)
                                                            : 0),

                                                             CollegePopulaorName =
                                                                 Convert.ToString(
                                                                     datatable.Rows[j]["AjCollegeBranchPopularName"]
                                                                         .ToString() != ""
                                                                         ? datatable.Rows[j][
                                                                             "AjCollegeBranchPopularName"]
                                                                         : "N/A"),
                                                             CollegeIdBranchId =
                                                                 Convert.ToInt32(
                                                                     datatable.Rows[j]["AjCollegeBranchId"].ToString() !=
                                                                     ""
                                                                         ? datatable.Rows[j]["AjCollegeBranchId"]
                                                                         : "N/A"),
                                                             CollegeBranchName =
                                                                 Convert.ToString(
                                                                     datatable.Rows[j]["AjCollegeBranchName"].ToString() !=
                                                                     ""
                                                                         ? datatable.Rows[j]["AjCollegeBranchName"]
                                                                         : "N/A"),
                                                             CollegeBranchEst =
                                                                 Convert.ToString(
                                                                     datatable.Rows[j]["AjCollegeBranchEst"].ToString() !=
                                                                     ""
                                                                         ? datatable.Rows[j]["AjCollegeBranchEst"]
                                                                         : "N/A"),
                                                             CollegeBranchDesc =
                                                                 Convert.ToString(
                                                                     datatable.Rows[j]["AjCollegeBranchDesc"].ToString() !=
                                                                     ""
                                                                         ? datatable.Rows[j]["AjCollegeBranchDesc"]
                                                                         : "N/A"),
                                                             CollegeBranchWebsite =
                                                                 Convert.ToString(
                                                                     datatable.Rows[j]["AjCollegeBranchWebSite"]
                                                                         .ToString() != ""
                                                                         ? datatable.Rows[j]["AjCollegeBranchWebSite"]
                                                                         : "0"),
                                                             CourseName =
                                                                 Convert.ToString(
                                                                     datatable.Columns.Contains("AjCourseName")
                                                                         ? (!String.IsNullOrEmpty(datatable.Rows[j]["AjCourseName"].ToString())
                                                                            ? datatable.Rows[j]["AjCourseName"]
                                                                                : null)
                                                                         : null),
                                                             CollegeBranchLogo =
                                                                 Convert.ToString(
                                                                     datatable.Rows[j]["AjCollegeBranchLogo"].ToString()),
                                                             CollegeBranchCityName =
                                                                 Convert.ToString(
                                                                     datatable.Rows[j]["AjCityName"].ToString() != ""
                                                                         ? datatable.Rows[j]["AjCityName"]
                                                                         : null),
                                                             
                                                             CollegeManagementType =
                                                    Convert.ToString(
                                                        datatable.Columns.Contains("AjMasterValues")
                                                            ? (!String.IsNullOrEmpty(datatable.Rows[j]["AjMasterValues"].ToString())
                                                               ? datatable.Rows[j]["AjMasterValues"]
                                                                   : null)
                                                            : null),
                                                             CollegeBranchCourseId =
                                                                 Convert.ToInt16(
                                                                     datatable.Rows[j]["AjCollegeBranchCourseId"]
                                                                         .ToString() != ""
                                                                         ? datatable.Rows[j]["AjCollegeBranchCourseId"]
                                                                               .ToString()
                                                                         : "0"),
                                                             UniversityId =
                                                                 Convert.ToInt32(
                                                                     datatable.Rows[j]["AjUniversityId"].ToString() !=
                                                                     ""
                                                                         ? datatable.Rows[j]["AjUniversityId"]
                                                                         : "0"),
                                                             UniversityName =
                                                                 Convert.ToString(
                                                                     datatable.Rows[j]["AjUniversityName"].ToString() !=
                                                                     ""
                                                                         ? datatable.Rows[j]["AjUniversityName"]
                                                                         : null),
                                                             CollegeBranchCourseOnlineStatus =
                                                                 (datatable.Rows[j]["AjCollegeOnlineParticipate"] is
                                                                  DBNull)
                                                                     ? true
                                                                     : Convert.ToBoolean(
                                                                         datatable.Rows[j]["AjCollegeOnlineParticipate"]),
                                                             CollegeBranchCourseSponserStatus =
                                                                 (datatable.Rows[j]["AjCollegeSponser"] is DBNull)
                                                                     ? false
                                                                     : Convert.ToBoolean(
                                                                         datatable.Rows[j]["AjCollegeSponser"]),

                                                             CollegeUrl =
                                                                 "/college-details/" +
                                                                 Common.RemoveIllegealFromCourseBL(
                                                                     Convert.ToString(datatable.Rows[j]["AjCourseName"]))
                                                                       .ToLower() + "/" +
                                                                 Common.RemoveIllegalCharactersBL(
                                                                     Convert.ToString(
                                                                         datatable.Rows[j]["AjCollegeBranchName"]))
                                                                       .ToLower(),
                                                             UniversityUrl =
                                                                 "/university-detail/" +
                                                                 Common.RemoveIllegalCharactersBL(
                                                                     Convert.ToString(
                                                                         datatable.Rows[j]["AjUniversityName"]))
                                                                       .ToLower()
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
               const string addInfo = "Error while executing BindCollegeListByName in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }

       private List<CollegeBranchKeySpeech> BindCollegeDirectorSpeech(DataTable datatable)
       {
           var objCollegeGroupList = new List<CollegeBranchKeySpeech>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objCollegeGroupProperty = new CollegeBranchKeySpeech
                       {
                           CollegeBranchKeySpeechId = (datatable.Rows[j]["AjCollegeKeySpeechId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeKeySpeechId"]),
                           CollegeBranchKeySpeechDesc = (datatable.Rows[j]["AjCollegeKeySpeechDesc"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeKeySpeechDesc"]),
                           CollegeBranchId = (datatable.Rows[j]["AjCollegeBranchId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchId"]),
                           CollegeBranchKeySpeechPersonImage = (datatable.Rows[j]["AjCollegeKeySpeechPersonImage"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeKeySpeechPersonImage"]),
                           CollegeBranchKeySpeechPersonAbout = (datatable.Rows[j]["AjAboutKeyPerson"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjAboutKeyPerson"]),

                           CollegeBranchKeySpeechPersonDesignation = (datatable.Rows[j]["AjCollegeKeySpeechPersonDesignation"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeKeySpeechPersonDesignation"]),
                           CollegeBranchKeySpeechPersonName = (datatable.Rows[j]["AjCollegeKeySpeechPersonName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeKeySpeechPersonName"]),
                           CollegeBranchKeySpeechStatus = (datatable.Rows[j]["AjAboutKeySpeechStatus"] is DBNull) ? true : Convert.ToBoolean(datatable.Rows[j]["AjAboutKeySpeechStatus"]),
                           CollegeBranchName = (datatable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),
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
               const string addInfo = "Error while executing BindCollegeDirectorSpeech in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }
       private List<CollegeBranchCourseHighlightsProperty> BindHighLights(DataTable datatable)
       {
           var objCollegeGroupList = new List<CollegeBranchCourseHighlightsProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objCollegeGroupProperty = new CollegeBranchCourseHighlightsProperty
                       {
                           CollegeBranchCourseHighlight = (datatable.Rows[j]["AjCollegeHighlights"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeHighlights"]),
                           CollegeBranchCourseHighlightStatus = (datatable.Rows[j]["AjCollegeHighlightStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeHighlightStatus"]),
                           CollegeBranchCourseHighlightId = (datatable.Rows[j]["AjCollegeHighlightsId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeHighlightsId"]),
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
               const string addInfo = "Error while executing BindHighLights in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }
       private List<CollegeBranchCoursePlacementProperty> BindTopHirer(DataTable datatable)
       {
           var objCollegeGroupList = new List<CollegeBranchCoursePlacementProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objCollegeGroupProperty = new CollegeBranchCoursePlacementProperty
                       {
                           CollegeBranchName = (datatable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),
                           CollegeBranchCoursePlacementCompanyName = (datatable.Rows[j]["AjCollegeBranchPlacementCompanyName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchPlacementCompanyName"]),
                           CollegeBranchCoursePlacementYear = (datatable.Rows[j]["AjCollegeBranchPlacementYear"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchPlacementYear"]),
                           CollegeBranchCoursePlacementNoOfStudentHired = (datatable.Rows[j]["AjCollegeBranchPlacementNoOfStudentHired"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchPlacementNoOfStudentHired"]),
                           CollegeBranchCoursePlacementAvgSalaryOffered = (datatable.Rows[j]["AjCollegeBranchPlacementAvgSalaryOffered"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchPlacementAvgSalaryOffered"]),
                           CollegeBranchCoursePlacementStatus = (datatable.Rows[j]["AjCollegeBranchPlacementStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeBranchPlacementStatus"]),
                           CollegeBranchCoursePlacementId = (datatable.Rows[j]["AjCollegeBranchPlacementId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchPlacementId"]),
                           CollegeBranchId = (datatable.Rows[j]["AjCollegeBranchId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchId"]),
                           CollegeBranchCourseId = (datatable.Rows[j]["AjCollegeBranchCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseId"]),
                           CourseId = datatable.Columns.Contains("AjCourseId") ? (datatable.Rows[j]["AjCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCourseId"]) : 0,
                           CourseName = datatable.Columns.Contains("AjCourseName") ? (datatable.Rows[j]["AjCourseName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCourseName"]) : null
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
               const string addInfo = "Error while executing BindTopHirer in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeGroupList;
       }
      
       private List<CollegeBranchRankProperty> BindRankProperty(DataTable datatable)
       {
           var objCollegeBranchRankProperty = new List<CollegeBranchRankProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objCollegeGroupProperty = new CollegeBranchRankProperty
                       {
                           CollegeRankId = (datatable.Rows[j]["AjCollegeRankId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeRankId"]),
                           CollegeRankYear = (datatable.Rows[j]["AjCollegeRankYear"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeRankYear"]),
                           CollegeOverAllRank = (datatable.Rows[j]["AjCollegeRankOverall"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeRankOverall"]),
                           CollegeRankStatus = (datatable.Rows[j]["AjCollegeRankStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeRankStatus"]),
                           RankSourceName = (datatable.Rows[j]["AjCollegeRankSourceName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeRankSourceName"]),
                           
                       };
                       objCollegeBranchRankProperty.Add(objCollegeGroupProperty);
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
               const string addInfo = "Error while executing BindTopHirer in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchRankProperty;
       }
       private List<CollegeBranchOnLineCounsellingProperty> BindOnlineCollegeList(DataTable datatable)
       {
           var objCollegeBranchOnLineCounsellingPropertyList = new List<CollegeBranchOnLineCounsellingProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objCollegeBranchOnLineCounsellingProperty = new CollegeBranchOnLineCounsellingProperty
                       {
                           CollegeBranchCourseId = (datatable.Rows[j]["AjCollegeBranchCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseId"]),
                           CollegeBranchName = (datatable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),
                           CollegeBranchCourseSponserStatus = (datatable.Rows[j]["AjCollegeBranchCourseStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeBranchCourseStatus"]),
                           CollegeOnlineParticipateStatus = (datatable.Rows[j]["AjCollegeOnlineParticipate"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeOnlineParticipate"]),
                           CollegeOnlineParticipationVirualStatus = (datatable.Rows[j]["AjCollegeOnlineParticipationVirualStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeOnlineParticipationVirualStatus"]),
                           CollegeOverallRating = (datatable.Rows[j]["AjCollegeOverallRating"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeOverallRating"]),
                           AdmissionDate = (datatable.Rows[j]["AjAdmissionOpenDate"] is DBNull) ? System.DateTime.Now : Convert.ToDateTime(datatable.Rows[j]["AjAdmissionOpenDate"]),
                       };
                       objCollegeBranchOnLineCounsellingPropertyList.Add(objCollegeBranchOnLineCounsellingProperty);
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
               const string addInfo = "Error while executing BindOnlineCollegeList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchOnLineCounsellingPropertyList;
       }
       private List<CollegeBranchOnLineCounsellingProperty> BindOnlineCollege(DataTable datatable)
       {
           var objCollegeBranchOnLineCounsellingPropertyList = new List<CollegeBranchOnLineCounsellingProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objCollegeBranchOnLineCounsellingProperty = new CollegeBranchOnLineCounsellingProperty
                       {
                           CollegeBranchCourseId = (datatable.Rows[j]["AjCollegeBranchCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseId"]),
                           CollegeBranchName = (datatable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),
                           CollegeBranchCourseSponserStatus = (datatable.Rows[j]["AjCollegeBranchCourseStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeBranchCourseStatus"]),
                           CollegeOnlineParticipateStatus = (datatable.Rows[j]["AjCollegeOnlineParticipate"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeOnlineParticipate"]),
                           CollegeOnlineParticipationVirualStatus = (datatable.Rows[j]["AjCollegeOnlineParticipationVirualStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeOnlineParticipationVirualStatus"]),
                           CollegeOverallRating = (datatable.Rows[j]["AjCollegeOverallRating"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeOverallRating"]),
                           AdmissionDate = (datatable.Rows[j]["AjAdmissionOpenDate"] is DBNull) ? System.DateTime.Now : Convert.ToDateTime(datatable.Rows[j]["AjAdmissionOpenDate"]),
                       };
                       objCollegeBranchOnLineCounsellingPropertyList.Add(objCollegeBranchOnLineCounsellingProperty);
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
               const string addInfo = "Error while executing BindOnlineCollegeList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchOnLineCounsellingPropertyList;
       }
       private List<Factor> BindFactorValues(DataTable datatable)
       {
           var objFactorList = new List<Factor>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objFactor = new Factor
                       {
                           BranchCourseId = (datatable.Rows[j]["AjCollegeBranchCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseId"]),
                           FactorId = (datatable.Rows[j]["AjFactorId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjFactorId"]),
                           FactorValues = (datatable.Rows[j]["AjCollegeFactorValue"] is DBNull) ? 0 : Convert.ToDouble(datatable.Rows[j]["AjCollegeFactorValue"]),
                         
                           
                       };
                       objFactorList.Add(objFactor);
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
               const string addInfo = "Error while executing BindFactorValues in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objFactorList;
       }
       private List<LeadSourceProperty> BindLeadCollegeList(DataTable datatable)
       {
           var objCollegeBranchOnLineCounsellingPropertyList = new List<LeadSourceProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objCollegeBranchOnLineCounsellingProperty = new LeadSourceProperty
                       {
                           CollegeBranchId = (datatable.Rows[j]["AjCollegeBranchId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchId"]),
                           CollegeBranchName = (datatable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),
                           CollegeBranchCityId = (datatable.Rows[j]["AjCityId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCityId"]),
                           CollegeBranchCityName = (datatable.Rows[j]["AjCityName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCityName"]),
                           CollegeBranchCourseStreamFees = (datatable.Rows[j]["AjCollegeBranchCourseStreamFees"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchCourseStreamFees"]),
                           CollegeBranchCourseStreamSeat = (datatable.Rows[j]["AjCollegeBranchCourseStreamSeat"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchCourseStreamSeat"]),

                           CheckParticipatedColleges = (datatable.Rows[j]["AjCollegeOnlineParticipate"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjCollegeOnlineParticipate"]),
                           StreamId = (datatable.Rows[j]["AjCourseStreamId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCourseStreamId"]),
                           StreamName = (datatable.Rows[j]["AjCourseStreamName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCourseStreamName"]),
                       
                       };
                       objCollegeBranchOnLineCounsellingPropertyList.Add(objCollegeBranchOnLineCounsellingProperty);
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
               const string addInfo = "Error while executing BindLeadCollegeList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchOnLineCounsellingPropertyList;
       }
       private List<LeadSourceProperty> BindLeadCollegeNameList(DataTable datatable)
       {
           var objCollegeBranchOnLineCounsellingPropertyList = new List<LeadSourceProperty>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {
                       var objCollegeBranchOnLineCounsellingProperty = new LeadSourceProperty
                       {
                           CollegeBranchId = (datatable.Rows[j]["AjCollegeBranchId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchId"]),
                           CollegeBranchName = (datatable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),
                          

                       };
                       objCollegeBranchOnLineCounsellingPropertyList.Add(objCollegeBranchOnLineCounsellingProperty);
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
               const string addInfo = "Error while executing BindLeadCollegeNameList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return objCollegeBranchOnLineCounsellingPropertyList;
       }

       public override int InsertUpdateCollegeEvent(string collegeName, int courseId, string eventName, string eventLocation, DateTime eventDate, string eventDesc, bool eventStatus, out string errMsg,int eventId = 0)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           int i = 0;
           errMsg = "";
           try
           {
               SecurePage objPage= new SecurePage();
               _objDataWrapper.AddParameter("@EventId", eventId);
               _objDataWrapper.AddParameter("@CollegeName", collegeName);
               _objDataWrapper.AddParameter("@CourseId", courseId);
               _objDataWrapper.AddParameter("@EventName", eventName);
               _objDataWrapper.AddParameter("@EventDate", eventDate);
               _objDataWrapper.AddParameter("@EventLocation", eventLocation);
               _objDataWrapper.AddParameter("@EventDesc", eventDesc);
               _objDataWrapper.AddParameter("@EventStatus", eventStatus);
               _objDataWrapper.AddParameter("@CreatedBy", objPage.LoggedInUserId);
                
            var objErrMsg=(SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg","",SqlDbType.NVarChar,ParameterDirection.Output,128));
               i=_objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertCollegeBranchEvent");
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
               const string addInfo = "Error while executing InsertCollegeEvent in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return i;
       }

       
       public override DataTable GetAllEvent()
       {
           return GetEventList(0, 0);
       }

       public override DataTable GetEventById(int eventId)
       {
           return GetEventList(eventId, 0);
       }

       public override DataTable GetEventByCollege(int collegeBranchCourseId)
       {
           return GetEventList(0, collegeBranchCourseId);
       }


       //public override DataTable GetCollegeSeatAValibity(int collegeBranchCourseId)
       //{
       //    _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
       //    try
       //    {
       //        _objDataWrapper.AddParameter("
       //    }
       //    catch (Exception ex)
       //    {
       //        var err = ex.Message;
       //        if (ex.InnerException != null)
       //        {
       //            err = err + " :: Inner Exception :- " + ex.ToString();
       //        }
       //        const string addInfo = "Error while executing GetCollegeSeatAValibity in College.cs  :: -> ";
       //        var objPub = new ClsExceptionPublisher();
       //        objPub.Publish(err, addInfo);
       //    }

       //}


       private DataTable GetEventList(int eventId, int collegeBranchCourseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           try
           {
               _objDataWrapper.AddParameter("@EventId", eventId);
               _objDataWrapper.AddParameter("@CollegeBranchCourseId", collegeBranchCourseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetEventList");
           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetEventList in College.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _dataSet.Tables[0];
       }
       private List<SearchPriorityListingCollege> BindSearchPriorityListCollege(DataTable datatable)
       {
           var objSearchPriorityListingCollegeList = new List<SearchPriorityListingCollege>();
           try
           {
               if (datatable.Rows.Count > 0)
               {
                   for (var j = 0; j < datatable.Rows.Count; j++)
                   {

                       var objSearchPriorityListingCollege = new SearchPriorityListingCollege
                           {
                               CollegeBannerList = new CollegeBanner()
                               {
                                   BannerId =
                                       (datatable.Rows[j]["AjBannerId"] is DBNull)
                                           ? 0
                                           : Convert.ToInt32(datatable.Rows[j]["AjBannerId"]),
                                   TooTip =
                                       (datatable.Rows[j]["AjToolTip"] is DBNull)
                                           ? null
                                           : Convert.ToString(datatable.Rows[j]["AjToolTip"]),
                                   BannerUrl =
                                       (string.IsNullOrEmpty(Convert.ToString(datatable.Rows[j]["AjBannerUrl"])))
                                           ? "/college-details/" +
                                                                 Common.RemoveIllegealFromCourseBL(
                                                                     Convert.ToString(datatable.Rows[j]["AjCourseName"]))
                                                                       .ToLower() + "/" +
                                                                 Common.RemoveIllegalCharactersBL(
                                                                     Convert.ToString(
                                                                         datatable.Rows[j]["AjCollegeBranchName"]))
                                                                       .ToLower()
                                           : Convert.ToString(datatable.Rows[j]["AjBannerUrl"]),
                                   BannerStartDate =
                            (datatable.Rows[j]["AjAdsBannerStartDate"] is DBNull)
                                ? DateTime.Now
                                : Convert.ToDateTime(datatable.Rows[j]["AjAdsBannerStartDate"]),
                                   BannerEndDate =
                                       (datatable.Rows[j]["AjAdsBannerEndDate"] is DBNull)
                                           ? DateTime.Now
                                           : Convert.ToDateTime(datatable.Rows[j]["AjAdsBannerEndDate"]),
                                   BannerPriority =
                                       (datatable.Rows[j]["AjPriorityId"] is DBNull)
                                           ? 0
                                           : Convert.ToInt32(datatable.Rows[j]["AjPriorityId"]),
                               },

                               CollegeBasicInfo = new CollegeBranchProperty()
                                   {
                                       CollegeIdBranchId =
                                           (datatable.Rows[j]["AjCollegeBranchId"] is DBNull)
                                               ? 0
                                               : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchId"]),
                                       CollegeBranchName =
                                           (datatable.Rows[j]["AjCollegeBranchName"] is DBNull)
                                               ? null
                                               : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),
                                       CollegeBranchLogo =
                                           (datatable.Rows[j]["AjCollegeBranchLogo"] is DBNull)
                                               ? null
                                               : Convert.ToString(datatable.Rows[j]["AjCollegeBranchLogo"]),

                                   },

                               CollegeBranchCourse = new CollegeBranchCourseProperty()
                                   {
                                       CollegeBranchCourseId =
                                           (datatable.Rows[j]["AjCollegeBranchCourseId"] is DBNull)
                                               ? 0
                                               : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchCourseId"]),
                                      
                                   },
                               CityMaster = new CityProperty()
                                   {
                                       CityId =
                                           (datatable.Rows[j]["AjCityId"] is DBNull)
                                               ? 0
                                               : Convert.ToInt32(datatable.Rows[j]["AjCityId"]),
                                       CityName =
                                           (datatable.Rows[j]["AjCityName"] is DBNull)
                                               ? null
                                               : Convert.ToString(datatable.Rows[j]["AjCityName"]),
                                       StateId =
                                           (datatable.Rows[j]["AjStateId"] is DBNull)
                                               ? 0
                                               : Convert.ToInt32(datatable.Rows[j]["AjStateId"]),
                                   },
                               CourseMaster = new CourseMasterProperty()
                                   {
                                       CourseId =
                                           (datatable.Rows[j]["AjCourseId"] is DBNull)
                                               ? 0
                                               : Convert.ToInt32(datatable.Rows[j]["AjCourseId"]),
                                       CourseName =
                                           (datatable.Rows[j]["AjCourseName"] is DBNull)
                                               ? null
                                               : Convert.ToString(datatable.Rows[j]["AjCourseName"]),

                                   },

                           };
                       objSearchPriorityListingCollegeList.Add(objSearchPriorityListingCollege);
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
           return objSearchPriorityListingCollegeList;
       }
   }
}
