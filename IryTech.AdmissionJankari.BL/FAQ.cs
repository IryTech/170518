using System;
using System.Collections.Generic;
using IryTech.AdmissionJankari.DAL;
using System.Data;
using IryTech.AdmissionJankari.BO;
using System.Data.SqlClient;
namespace IryTech.AdmissionJankari.BL
{
  public class FAQ:FAQProvider 
    {
        private DbWrapper _objDataWrapper;
        private DataSet _dataSet;
        private int _i;

        public override int InsertFAQCategory(FAQCategoryProperty objFAQCategoryProperty, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@FAQCategoryName", objFAQCategoryProperty.FAQCategoryName );
                _objDataWrapper.AddParameter("@FAQCategoryStatus", objFAQCategoryProperty.FAQCategoryStatus);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var errMsg =
                     (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateFAQCategory");
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
                const string addInfo = "Error while executing InsertFAQCategory in FAQ.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int UpdtaeFAQCategory(FAQCategoryProperty objFAQCategoryProperty, int modifiedBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@FAQCategoryId", objFAQCategoryProperty.FAQCategoryId);
                _objDataWrapper.AddParameter("@FAQCategoryName", objFAQCategoryProperty.FAQCategoryName);
                _objDataWrapper.AddParameter("@FAQCategoryStatus", objFAQCategoryProperty.FAQCategoryStatus);
                _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
                var errMsg =
                     (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateFAQCategory");
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
                const string addInfo = "Error while executing UpdtaeFAQCategory in FAQ.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override List<FAQCategoryProperty> GetAllFAQCategoryList()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objFAQCategoryList = new List<FAQCategoryProperty>();
            _dataSet = new DataSet();

            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetFAQCategoryList");
                objFAQCategoryList = BindFAQCategoryObjectList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllFAQCategoryList in FAQ.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objFAQCategoryList;
        }

        public override List<FAQCategoryProperty> GetAllFAQCategoryById(int faqCategotryId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objFAQCategoryList = new List<FAQCategoryProperty>();
            _dataSet = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@FAQCategoryId", faqCategotryId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetFAQCategoryList");
                objFAQCategoryList = BindFAQCategoryObjectList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllFAQCategoryById in FAQ.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objFAQCategoryList;
        }
        public override int InsertFAQDetails(FAQDetailsProperty objFAQDetailsProperty, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@FAQQuestion", objFAQDetailsProperty.FAQDetailsQuestion);
                _objDataWrapper.AddParameter("@FAQCategoryId", objFAQDetailsProperty.FAQCategoryId);
                _objDataWrapper.AddParameter("@FAQAnswer", objFAQDetailsProperty.FAQDetailsAnswer);
                _objDataWrapper.AddParameter("@FAqQDetailsStatus", objFAQDetailsProperty.FAQDetailsStatus);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var errMsg =
                     (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateFAQDetails");
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
                const string addInfo = "Error while executing InsertFAQDetails in FAQ.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int UpdateFAQDetails(FAQDetailsProperty objFAQDetailsProperty, int modifiedBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@FAQDetailsId", objFAQDetailsProperty.FAQDetailsId );
                _objDataWrapper.AddParameter("@FAQQuestion", objFAQDetailsProperty.FAQDetailsQuestion);
                _objDataWrapper.AddParameter("@FAQCategoryId", objFAQDetailsProperty.FAQCategoryId);
                _objDataWrapper.AddParameter("@FAQAnswer", objFAQDetailsProperty.FAQDetailsAnswer);
                _objDataWrapper.AddParameter("@FAqQDetailsStatus", objFAQDetailsProperty.FAQDetailsStatus);
                _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
                var errMsg =
                     (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateFAQDetails");
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
                const string addInfo = "Error while executing UpdateFAQDetails in FAQ.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override List<FAQDetailsProperty> GetAllFAQDetailsList()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objFAQDetailsList = new List<FAQDetailsProperty>();
            _dataSet = new DataSet();

            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetFAQDetails");
                objFAQDetailsList = BindFAQDetailsObjectList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllFAQDetailsList in FAQ.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objFAQDetailsList;
        }

        public override List<FAQDetailsProperty> GetFAQDetailsById(int faqDetailsId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objFAQDetailsList = new List<FAQDetailsProperty>();
            _dataSet = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@FAQDetailsId", faqDetailsId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetFAQDetails");
                objFAQDetailsList = BindFAQDetailsObjectList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetFAQDetailsById in FAQ.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objFAQDetailsList;
           
        }

        public override List<FAQDetailsProperty> GetFAQDetailsByName(string faqDetailsName)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objFAQDetailsList = new List<FAQDetailsProperty>();
            _dataSet = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@FAQFAQQestionName", faqDetailsName);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetFAQDetails");
                objFAQDetailsList = BindFAQDetailsObjectList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetFAQDetailsByName in FAQ.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objFAQDetailsList;
        }

        public override List<FAQDetailsProperty> GetFAQDetailsByFAQCategory(int faqCategoryId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objFAQDetailsList = new List<FAQDetailsProperty>();
            _dataSet = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@FAQCategoryId", faqCategoryId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetFAQDetails");
                objFAQDetailsList = BindFAQDetailsObjectList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetFAQDetailsByFAQCategory in FAQ.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objFAQDetailsList;
        }
      // Method to Bind The  private member of the the Class
        private List<FAQCategoryProperty> BindFAQCategoryObjectList(DataTable datatable)
        {
            var objFAQCategoryList = new List<FAQCategoryProperty>();

            try
            {

                if (datatable.Rows.Count > 0)
                {
                    for(var j=0;j<datatable.Rows.Count;j++)
                    {
                        var objFAQCategory = new FAQCategoryProperty
                                            {
                                                FAQCategoryId = Convert.ToInt32(datatable.Rows[j]["AjFaqCategoryId"]),
                                                FAQCategoryName = Convert.ToString(datatable.Rows[j]["AjFaqCategoryName"]),
                                                FAQCategoryStatus = Convert.ToBoolean(datatable.Rows[j]["AjFaqStatus"])


                                            };
                        objFAQCategoryList.Add(objFAQCategory);
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
                const string addInfo = "Error while executing BindFAQCategoryObjectList in FAQ.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objFAQCategoryList;
        }

        private List<FAQDetailsProperty> BindFAQDetailsObjectList(DataTable datatable)
        {
            var objFAQDetailsList = new List<FAQDetailsProperty>();

            try
            {

                if (datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {
                        var objFAQDetails = new FAQDetailsProperty
                        {
                            FAQCategoryId = Convert.ToInt32(datatable.Rows[j]["AjFaqCategoryId"]),
                            FAQCategoryName = Convert.ToString(datatable.Rows[j]["AjFaqCategoryName"]),
                            FAQDetailsAnswer = Convert.ToString(datatable.Rows[j]["AjFaqAnswer"]),
                            FAQDetailsId = Convert.ToInt32(datatable.Rows[j]["AjFaqId"]),
                            FAQDetailsQuestion = Convert.ToString(datatable.Rows[j]["AjFaqName"]),
                            FAQDetailsStatus = Convert.ToBoolean(datatable.Rows[j]["AjFaqDetailsStatus"])


                        };
                        objFAQDetailsList.Add(objFAQDetails);
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
                const string addInfo = "Error while executing BindFAQDetailsObjectList in FAQ.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objFAQDetailsList;
        }


        
    }
}
