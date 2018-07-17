using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using IryTech.AdmissionJankari.DAL;

using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari.BL
{
    public class CollegeSpeech:CollegeSpeechProvider
    {

        private DbWrapper _objDataWrapper;
        private DataSet _dataSet;
        private int _i;


        public override int InsertCollegeSpeechDetails(CollegeSpeechProperty objCollegeSppechProperty, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@CollegeName", objCollegeSppechProperty.CollegeName);
                _objDataWrapper.AddParameter("@CollegeSpeechPersonDesignation", objCollegeSppechProperty.CollegeSpeechPersonDesignation);
                _objDataWrapper.AddParameter("@CollegeSpeechPersonName", objCollegeSppechProperty.CollegeSpeechPersonName);
                _objDataWrapper.AddParameter("@CollegeSpeechPersonImage", objCollegeSppechProperty.CollegeSpeechPersonImage);
                _objDataWrapper.AddParameter("@CollegeSpeechDesc", objCollegeSppechProperty.CollegeSpeechDesc);
                _objDataWrapper.AddParameter("@AboutKeyPerson", objCollegeSppechProperty.AboutKeyPerson);
                _objDataWrapper.AddParameter("@SpeechStatus", objCollegeSppechProperty.SpeechStatus);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var errMsg =
                     (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeSpeechDetails");
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
                const string addInfo = "Error while executing InsertCollegeSpeechDetails in CollegeSpeech.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }



        public override int UpdateCollegeSpeechDetails(CollegeSpeechProperty objCollegeSppechProperty, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@CollegeSpeechId", objCollegeSppechProperty.CollegeSpeechId);
                _objDataWrapper.AddParameter("@CollegeName", objCollegeSppechProperty.CollegeName);
                _objDataWrapper.AddParameter("@CollegeSpeechPersonDesignation", objCollegeSppechProperty.CollegeSpeechPersonDesignation);
                _objDataWrapper.AddParameter("@CollegeSpeechPersonName", objCollegeSppechProperty.CollegeSpeechPersonName);
                _objDataWrapper.AddParameter("@CollegeSpeechPersonImage", objCollegeSppechProperty.CollegeSpeechPersonImage);
                _objDataWrapper.AddParameter("@CollegeSpeechDesc", objCollegeSppechProperty.CollegeSpeechDesc);
                _objDataWrapper.AddParameter("@AboutKeyPerson", objCollegeSppechProperty.AboutKeyPerson);
                _objDataWrapper.AddParameter("@SpeechStatus", objCollegeSppechProperty.SpeechStatus);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var errMsg =
                     (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCollegeSpeechDetails");
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
                const string addInfo = "Error while executing InsertCollegeSpeechDetails in CollegeSpeech.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }


        public override List<CollegeSpeechProperty> GetAllCollegeSpeechList()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objCollegeGroupList = new List<CollegeSpeechProperty>();
            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetCollegeSpeechList");
                objCollegeGroupList = BindCollegeSpeechObjectList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllCollegeSpeechList in CollegeSpeech.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objCollegeGroupList;
        }


        public override List<CollegeSpeechProperty> GetCollegeSpeechById(int CollegeSpeechID)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objCollegeGroupList = new List<CollegeSpeechProperty>();
            try
            {
                _objDataWrapper.AddParameter("@CollegeSpeechID", CollegeSpeechID);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetCollegeSpeechList");
                objCollegeGroupList = BindCollegeSpeechObjectList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeSpeechById in CollegeSpeech.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objCollegeGroupList;
        }
        public override List<CollegeSpeechProperty> GetCollegeSpeechByCourseId(int courseId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objCollegeGroupList = new List<CollegeSpeechProperty>();
            try
            {
                _objDataWrapper.AddParameter("@CourseId", courseId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetCollegeSpeechList");
                objCollegeGroupList = BindCollegeSpeechObjectList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeSpeechByCourseId in CollegeSpeech.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objCollegeGroupList;
        }
        

        // Private  Method to Bind the College Group Object Details 
        private List<CollegeSpeechProperty> BindCollegeSpeechObjectList(DataTable datatable)
        {
            var objCollegeSpeechList = new List<CollegeSpeechProperty>();
            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {
                        var objCollegeSpeechProperty = new CollegeSpeechProperty
                        {

                            CollegeSpeechId = (datatable.Rows[j]["AjCollegeKeySpeechId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeKeySpeechId"]),
                            CollegeName = (datatable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),
                            CollegeBranhID = (datatable.Rows[j]["AjCollegeBranchId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCollegeBranchId"]),
                            CollegeSpeechPersonName = (datatable.Rows[j]["AjCollegeKeySpeechPersonName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeKeySpeechPersonName"]),
                            CollegeSpeechPersonDesignation = (datatable.Rows[j]["AjCollegeKeySpeechPersonDesignation"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeKeySpeechPersonDesignation"]),
                            CollegeSpeechPersonImage = (datatable.Rows[j]["AjCollegeKeySpeechPersonImage"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeKeySpeechPersonImage"]),
                            CollegeSpeechDesc = (datatable.Rows[j]["AjCollegeKeySpeechDesc"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeKeySpeechDesc"]),
                            AboutKeyPerson = (datatable.Rows[j]["AjAboutKeyPerson"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjAboutKeyPerson"]),
                            SpeechStatus = (datatable.Rows[j]["AjAboutKeySpeechStatus"] is DBNull) ? true : Convert.ToBoolean(datatable.Rows[j]["AjAboutKeySpeechStatus"]),
                        };
                        objCollegeSpeechList.Add(objCollegeSpeechProperty);
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
            return objCollegeSpeechList;
        }

    }
}
