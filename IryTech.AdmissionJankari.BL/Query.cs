using System;
using IryTech.AdmissionJankari.BO;
using System.Collections.Generic;
using IryTech.AdmissionJankari.DAL;
using System.Data.SqlClient;
using System.Data;

namespace IryTech.AdmissionJankari.BL
{
    public class Query :QueryProvider
    {
        private DbWrapper _objDataWrapper;
        ClsCrypto _objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
        private int _i;
        private DataSet _dataset;
        public override List<QueryProperty> GetCollegeQuery(int collegebranchId,int queryId,int course)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var listQueryProperty = new List<QueryProperty>();
            _dataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CollegeId", collegebranchId);
                _objDataWrapper.AddParameter("@QueryId", queryId);
                _objDataWrapper.AddParameter("@CourseId", course);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeQuery");
                listQueryProperty = BindQueryObjectList(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeQuery in Query.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return listQueryProperty;
        }
        public override int UpdateQueryReplyStatus(QueryProperty objQuickQuery, out string errMsg)
        {
            errMsg = "";
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _objDataWrapper.AddParameter("@UserId", objQuickQuery.UserId);
                _objDataWrapper.AddParameter("@QueryId", objQuickQuery.StudentQueryId);
                _objDataWrapper.AddParameter("@ReplyStatus", objQuickQuery.ReplyStatus);
                var ObjerrMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_UpdateQyeryStatus");
                if (ObjerrMsg != null && ObjerrMsg.Value != null)
                    errMsg = Convert.ToString(ObjerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateQueryReplyStatus in Query.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }
        public override int InsertCommonQuickQuery(QueryProperty objQuickQuery, out string errMsg,out int UserId)
        {
            errMsg = "";
            UserId = 0;
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _objDataWrapper.AddParameter("@UserEmailId", objQuickQuery.UserEmailId);
                _objDataWrapper.AddParameter("@UserName", objQuickQuery.StudentName);
                _objDataWrapper.AddParameter("@UserMobile", objQuickQuery.UserMobileNo);
                _objDataWrapper.AddParameter("@UserPasseord",_objCrypto.Encrypt(objQuickQuery.UserMobileNo));
                _objDataWrapper.AddParameter("@UserCity", objQuickQuery.StudentCityName);
                _objDataWrapper.AddParameter("@UserQuerySourceTypeId", QueryType.Com);
                _objDataWrapper.AddParameter("@UserCourseId", objQuickQuery.StudentCourseId);
                _objDataWrapper.AddParameter("@UserQuery", objQuickQuery.StudentQuery);

                var ObjerrMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                var ObjUserId =
                   (SqlParameter)(_objDataWrapper.AddParameter("@UserId", SqlDbType.Int, ParameterDirection.Output));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUserQuery");
                if (ObjerrMsg != null && ObjerrMsg.Value != null)
                    errMsg = Convert.ToString(ObjerrMsg.Value);
                if (ObjUserId != null && ObjUserId.Value != null)
                    UserId = Convert.ToInt32(ObjUserId.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertCommonQuickQuery in Query.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int InsertCollegeQuickQuery(QueryProperty objQuickQuery, out string errMsg)
        {
            errMsg = "";
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _objDataWrapper.AddParameter("@UserEmailId", objQuickQuery.UserEmailId);
                _objDataWrapper.AddParameter("@UserName", objQuickQuery.StudentName);
                _objDataWrapper.AddParameter("@UserMobile", objQuickQuery.UserMobileNo);
                _objDataWrapper.AddParameter("@UserPasseord", _objCrypto.Encrypt(objQuickQuery.UserMobileNo));
                _objDataWrapper.AddParameter("@UserCity", objQuickQuery.StudentCityName);
                _objDataWrapper.AddParameter("@UserQuerySourceTypeId", QueryType.Col);
                _objDataWrapper.AddParameter("@UserQuerySourceId",objQuickQuery.StudentSourceId);
                _objDataWrapper.AddParameter("@UserCourseId", objQuickQuery.StudentCourseId);
                _objDataWrapper.AddParameter("@UserQuery", objQuickQuery.StudentQuery);
                var ObjerrMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUserQuery");
                if (ObjerrMsg != null && ObjerrMsg.Value != null)
                    errMsg = Convert.ToString(ObjerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertCommonQuickQuery in Query.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int InsertExamQuickQuery(QueryProperty objQuickQuery, out string errMsg)
        {
            errMsg = "";
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _objDataWrapper.AddParameter("@UserEmailId", objQuickQuery.UserEmailId);
                _objDataWrapper.AddParameter("@UserName", objQuickQuery.StudentName);
                _objDataWrapper.AddParameter("@UserMobile", objQuickQuery.UserMobileNo);
                _objDataWrapper.AddParameter("@UserPasseord", _objCrypto.Encrypt(objQuickQuery.UserMobileNo));
                _objDataWrapper.AddParameter("@UserCity", objQuickQuery.StudentCityName);
                _objDataWrapper.AddParameter("@UserQuerySourceTypeId", QueryType.Exam);
                _objDataWrapper.AddParameter("@UserQuerySourceId", objQuickQuery.StudentSourceId);
                _objDataWrapper.AddParameter("@UserCourseId", objQuickQuery.StudentCourseId);
                _objDataWrapper.AddParameter("@UserQuery", objQuickQuery.StudentQuery);
                var ObjerrMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUserQuery");
                if (ObjerrMsg != null && ObjerrMsg.Value != null)
                    errMsg = Convert.ToString(ObjerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertExamQuickQuery in Query.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int InsertLoanQuickQuery(QueryProperty objQuickQuery, out string errMsg)
        {
            errMsg = "";
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _objDataWrapper.AddParameter("@UserEmailId", objQuickQuery.UserEmailId);
                _objDataWrapper.AddParameter("@UserName", objQuickQuery.StudentName);
                _objDataWrapper.AddParameter("@UserMobile", objQuickQuery.UserMobileNo);
                _objDataWrapper.AddParameter("@UserPasseord", _objCrypto.Encrypt(objQuickQuery.UserMobileNo));
                _objDataWrapper.AddParameter("@UserCity", objQuickQuery.StudentCityName);
                _objDataWrapper.AddParameter("@UserQuerySourceTypeId", QueryType.Loan);
                _objDataWrapper.AddParameter("@UserCourseId", objQuickQuery.StudentCourseId);
                _objDataWrapper.AddParameter("@UserQuery", objQuickQuery.StudentQuery);
                var ObjerrMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUserQuery");
                if (ObjerrMsg != null && ObjerrMsg.Value != null)
                    errMsg = Convert.ToString(ObjerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertLoanQuickQuery in Query.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int InsertQueryThread(QueryProperty objQuickQuery, out string errMsg)
        {
            errMsg = "";
            return _i;
        }

        public override List<QueryProperty> GetAllQueryListByCourse()
        {              
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
             List<QueryProperty> listQueryProperty = new List<QueryProperty>();
           _dataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@case", QueryType.Com);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetStudentQueryList");
              listQueryProperty= BindQueryObjectList(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllQueryListByCourse in Query.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return listQueryProperty;
        }

        public override List<QueryProperty> GetAllQueryListByExam()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            List<QueryProperty> listQueryProperty = new List<QueryProperty>();
            _dataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@case", QueryType.Exam);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetStudentQueryList");
                listQueryProperty = BindQueryObjectList(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllQueryListByExam in Query.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return listQueryProperty;
        }

        public override List<QueryProperty> GetAllQueryListByCollege()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            List<QueryProperty> listQueryProperty = new List<QueryProperty>();
            _dataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@case", QueryType.Col);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetStudentQueryList");
                listQueryProperty = BindQueryObjectList(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllQueryListByCollege in Query.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return listQueryProperty;
        }

        public override List<QueryProperty> GetAllQueryListByLoan()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            List<QueryProperty> listQueryProperty = new List<QueryProperty>();
            _dataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@case", QueryType.Loan );
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetStudentQueryList");
                listQueryProperty = BindQueryObjectList(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllQueryListByLoan in Query.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return listQueryProperty;
        }

        public override int InsertQueryReply(ReplyProperty objReplyProperty, out string errMsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMsg = "";
            try
            {
                _objDataWrapper.AddParameter("@UserId", objReplyProperty.ReplyBy);
                _objDataWrapper.AddParameter("@QueryId", objReplyProperty.QueryId);
                _objDataWrapper.AddParameter("@QueryReply", objReplyProperty.QueryReply);

                var objerrMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertQueryReply");
                if (objerrMsg != null && objerrMsg.Value != null)
                    errMsg = Convert.ToString(objerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertQueryReply in Query.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        // Method to bind the QueryList
        private List<QueryProperty> BindQueryObjectList(DataTable datatable)
        {
            List<QueryProperty> listQueryProperty = new List<QueryProperty>();
           
            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for (int i = 0; i < datatable.Rows.Count; i++)
                    {
                        var objQueryProperty = new QueryProperty
                                {
                                    UserId = Convert.ToInt32(datatable.Rows[i]["AjUserId"]),
                                    UserMobileNo = Convert.ToString(datatable.Rows[i]["AjUserMobile"]),
                                    UserEmailId = Convert.ToString(datatable.Rows[i]["AjUserEmail"]),
                                    StudentSourceId = Convert.ToInt32(datatable.Rows[i]["SourceId"]),
                                    StudentCourseId = Convert.ToInt32(datatable.Rows[i]["AjCourseId"]),
                                    StudentCourseName = Convert.ToString(datatable.Rows[i]["CourseName"]),
                                    StudentName = Convert.ToString(datatable.Rows[i]["AjUserFullName"]),
                                    StudentQuery = Convert.ToString(datatable.Rows[i]["AjStudentQueryText"]),
                                    SourceTypeName = Convert.ToString(datatable.Rows[i]["Source"]),
                                    StudentQueryId = Convert.ToString(datatable.Rows[i]["AjStudentQueryId"]),
                                    StudentCityName = Convert.ToString(datatable.Rows[i]["AjCityName"]),
                                    SourceName = Convert.ToString(datatable.Rows[i]["SourceName"]),
                                    ReplyStatus = Convert.ToBoolean(datatable.Rows[i]["AjStudentQueryReplyStatus"]),
                                    QueryReply = datatable.Columns.Contains("AjQueryReply") == true ? ((datatable.Rows[i]["AjQueryReply"] is DBNull) ? null : Convert.ToString(datatable.Rows[i]["AjQueryReply"])) : null,
                                    CreatedOn=Convert.ToDateTime(datatable.Rows[i]["CreatedOn"]),
                                    QueryStatus = datatable.Columns.Contains("AjQueryStatus") == true ? ((datatable.Rows[i]["AjQueryStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[i]["AjQueryStatus"])) : false,
                                };
                        listQueryProperty.Add(objQueryProperty);

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
                const string addInfo = "Error while executing BindQueryObjectList in Query.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return listQueryProperty;
        }



        public override int ModerateStudentQuery(int queryId,int moderateBy,bool status, out string errMsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMsg = "";
            try
            {
                _objDataWrapper.AddParameter("@queryStatus", status);
                _objDataWrapper.AddParameter("@QueryId", queryId);
                _objDataWrapper.AddParameter("@ModerateBy", moderateBy);
                 var objerrMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@errMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                 _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_ModerateQuery");
                if (objerrMsg != null && objerrMsg.Value != null)
                    errMsg = Convert.ToString(objerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindQueryObjectList in Query.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override string  CheckQueryModerate(int queryId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var isModerated = "";
            try
            {
                _objDataWrapper.AddParameter("@QueryId", queryId);
              
                var objerrMsg =
                   (SqlParameter)(_objDataWrapper.AddParameter("@moderate", "", SqlDbType.NVarChar, ParameterDirection.Output,128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_CheckQueryModerate");
                if (objerrMsg != null && objerrMsg.Value != null)
                    isModerated = Convert.ToString(objerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindQueryObjectList in Query.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return isModerated;
        }
    }
}
