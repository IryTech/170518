using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IryTech.AdmissionJankari.DAL;


namespace IryTech.AdmissionJankari.BL
{
   public class Stream:StreamProvider 
    {
        private DbWrapper _objDataWrapper;
        private DataSet _dataSet;
        private int _i;

        public override int InsertCourseStreamDetails(CourseStreamProperty objCourseStreamProperty, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";

            try
            {
                _objDataWrapper.AddParameter("@StreamName", objCourseStreamProperty.CourseStreamName);
                _objDataWrapper.AddParameter("@CourseId", objCourseStreamProperty.CourseId);
                _objDataWrapper.AddParameter("@CourseStreamUrl", objCourseStreamProperty.CourseStreamUrl);
                _objDataWrapper.AddParameter("@CourseStreamTitle", objCourseStreamProperty.CourseStreamName);
                _objDataWrapper.AddParameter("@CourseStreamMetaTag", objCourseStreamProperty.CourseStreamMetaTag);
                _objDataWrapper.AddParameter("@CourseStreamMetaDesc", objCourseStreamProperty.CourseStreamMetaDesc);
                _objDataWrapper.AddParameter("@CourseStreamDesc", objCourseStreamProperty.CourseStreamDesc);
                _objDataWrapper.AddParameter("@CourseStreamHistory", objCourseStreamProperty.CourseStreamHistory);
                _objDataWrapper.AddParameter("@CourseStreamFurture", objCourseStreamProperty.CourseSteamFuture);
                _objDataWrapper.AddParameter("@CourseStreamCoreCompanies", objCourseStreamProperty.CourseStreamCoreCompanies);
                _objDataWrapper.AddParameter("@CourseStreamRelatedIndustry", objCourseStreamProperty.CourseStreamRelatedIndustry);
                _objDataWrapper.AddParameter("@CourseStreamStatus", objCourseStreamProperty.CourseStreamStatus);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objErrmsg = (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCourseStreamDetails");
                if (objErrmsg != null && objErrmsg.Value != null)
                    errmsg = Convert.ToString(objErrmsg.Value);


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertCourseStreamDetails in Stream.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int UpdateCourseStreamDetails(CourseStreamProperty objCourseStreamProperty, int modifiedBy, out string errmsg)
        {

            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";

            try
            {
                _objDataWrapper.AddParameter("@StreamId", objCourseStreamProperty.StreamId);
                _objDataWrapper.AddParameter("@StreamName", objCourseStreamProperty.CourseStreamName);
                _objDataWrapper.AddParameter("@CourseId", objCourseStreamProperty.CourseId);
                _objDataWrapper.AddParameter("@CourseStreamUrl", objCourseStreamProperty.CourseStreamUrl);
                _objDataWrapper.AddParameter("@CourseStreamTitle", objCourseStreamProperty.CourseStreamName);
                _objDataWrapper.AddParameter("@CourseStreamMetaTag", objCourseStreamProperty.CourseStreamMetaTag);
                _objDataWrapper.AddParameter("@CourseStreamMetaDesc", objCourseStreamProperty.CourseStreamMetaDesc);
                _objDataWrapper.AddParameter("@CourseStreamDesc", objCourseStreamProperty.CourseStreamDesc);
                _objDataWrapper.AddParameter("@CourseStreamHistory", objCourseStreamProperty.CourseStreamHistory);
                _objDataWrapper.AddParameter("@CourseStreamFurture", objCourseStreamProperty.CourseSteamFuture);
                _objDataWrapper.AddParameter("@CourseStreamCoreCompanies", objCourseStreamProperty.CourseStreamCoreCompanies);
                _objDataWrapper.AddParameter("@CourseStreamRelatedIndustry", objCourseStreamProperty.CourseStreamRelatedIndustry);
                _objDataWrapper.AddParameter("@CourseStreamStatus", objCourseStreamProperty.CourseStreamStatus);
                _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
                var objErrmsg = (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCourseStreamDetails");
                if (objErrmsg != null && objErrmsg.Value != null)
                    errmsg = Convert.ToString(objErrmsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateCourseStreamDetails in Stream.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override List<CourseStreamProperty> GetAllStreamList()
        {
            var courseStreamObjectList = new List<CourseStreamProperty>();
            _dataSet=new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _dataSet=_objDataWrapper.ExecuteDataSet("Aj_Proc_GetStreamList");
              courseStreamObjectList= BindStreamObjectList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateCourseStreamDetails in Stream.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return courseStreamObjectList;
        }

        public override List<CourseStreamProperty> GetStreamListById(int streamId)
        {
            var courseStreamObjectList = new List<CourseStreamProperty>();
            _dataSet=new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _objDataWrapper.AddParameter("@StreamId",streamId);
                _dataSet=_objDataWrapper.ExecuteDataSet("Aj_Proc_GetStreamList");
              courseStreamObjectList= BindStreamObjectList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetStreamListById in Stream.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return courseStreamObjectList;
        }

        public override List<CourseStreamProperty> GetStreamListByCourse(int courseId)
        {
             var courseStreamObjectList = new List<CourseStreamProperty>();
            _dataSet=new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _objDataWrapper.AddParameter("@CourseId",courseId);
                _dataSet=_objDataWrapper.ExecuteDataSet("Aj_Proc_GetStreamList");
              courseStreamObjectList= BindStreamObjectList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetStreamListByCourse in Stream.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return courseStreamObjectList;

        }

        public override List<CourseStreamProperty> GetStreamListByStreamNameCourseId(int courseId, string streamName)
        {
             var courseStreamObjectList = new List<CourseStreamProperty>();
            _dataSet=new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _objDataWrapper.AddParameter("@CourseId",courseId);
                _objDataWrapper.AddParameter("@StreamName",streamName);
                _dataSet=_objDataWrapper.ExecuteDataSet("Aj_Proc_GetStreamList");
              courseStreamObjectList= BindStreamObjectList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetStreamListByStreamNameCourseId in Stream.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return courseStreamObjectList;
        }

        public override List<CourseStreamProperty> GetStreamListByStreamName(string streamName)
        {
             var courseStreamObjectList = new List<CourseStreamProperty>();
            _dataSet=new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _objDataWrapper.AddParameter("@StreamName",streamName);
                _dataSet=_objDataWrapper.ExecuteDataSet("Aj_Proc_GetStreamList");
              courseStreamObjectList= BindStreamObjectList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetStreamListByStreamName in Stream.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return courseStreamObjectList;
        }


       // This is private member that will bind the Coutse stream Object List
        private List<CourseStreamProperty> BindStreamObjectList(DataTable datatable)
        {
            var courseStreamObjectList = new List<CourseStreamProperty>();


            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for(var j=0;j<datatable.Rows.Count;j++)
                    {
                        var objCourseStream = new CourseStreamProperty
                                            {
                                                CourseId = Convert.ToInt32(datatable.Rows[j]["AjCourseId"]),
                                                CourseName = Convert.ToString(datatable.Rows[j]["AjCourseName"]),
                                                CourseSteamFuture = Convert.ToString(datatable.Rows[j]["AjCourseStreamFuture"]),
                                                CourseStreamCoreCompanies = Convert.ToString(datatable.Rows[j]["AjCourseStreamCoreCompanies"]),
                                                CourseStreamDesc = Convert.ToString(datatable.Rows[j]["AjCourseStreamDesc"]),
                                                CourseStreamHistory = Convert.ToString(datatable.Rows[j]["AjCourseStreamHistory"]),
                                                CourseStreamMetaDesc = Convert.ToString(datatable.Rows[j]["AjCourseStreamMetaDesc"]),
                                                CourseStreamMetaTag = Convert.ToString(datatable.Rows[j]["AjCourseStreamMetaTag"]),
                                                CourseStreamName = Convert.ToString(datatable.Rows[j]["AjCourseStreamName"]),
                                                CourseStreamRelatedIndustry = Convert.ToString(datatable.Rows[j]["AjCourseStreamRelatedIndustry"]),
                                                CourseStreamStatus = Convert.ToBoolean(datatable.Rows[j]["AjCourseStreamStatus"]),
                                                CourseStreamTitle = Convert.ToString(datatable.Rows[j]["AjCourseStreamTitle"]),
                                                CourseStreamUrl = Convert.ToString(datatable.Rows[j]["AjCourseStreamUrl"]),
                                                StreamId = Convert.ToInt32(datatable.Rows[j]["AjCourseStreamId"])


                                            };
                        courseStreamObjectList.Add(objCourseStream);
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
                const string addInfo = "Error while executing BindStreamObjectList in Stream.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return courseStreamObjectList;
        }

    }
}
