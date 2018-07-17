using System;
using System.Collections.Generic;
using System.Data;
using IryTech.AdmissionJankari.DAL;
using System.Data.SqlClient;

namespace IryTech.AdmissionJankari.BL
{
    public class University :UniversityProvider
    {
        private DbWrapper _objDataWrapper;
        private DataSet _dataSet;
        private int _i;

        public override int InsertUniversityCategoryDetails(BO.UniversityCategoryProperty objUniversityCategory, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";

            try
            {
                _objDataWrapper.AddParameter("@UniversityCategoryName", objUniversityCategory.UniversityCategoryName);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objErrMsg=
                        (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg","",SqlDbType.VarChar,ParameterDirection.Output ,128));
                    _i=_objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateUniversityCategoryDetails");
                if(objErrMsg!=null && objErrMsg.Value!=null)
                        errmsg=Convert.ToString(objErrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUniversityCategoryDetails in University.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int UpdateUnivesityCategoryDetails(BO.UniversityCategoryProperty objUniversityCategory, int modifiedBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";

            try
            {
                _objDataWrapper.AddParameter("@UniversityCategoryId", objUniversityCategory.UniversityCategoryId);
                _objDataWrapper.AddParameter("@UniversityCategoryName", objUniversityCategory.UniversityCategoryName);
                _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
                var objErrMsg=
                        (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg","",SqlDbType.VarChar,ParameterDirection.Output ,128));
                    _i=_objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateUniversityCategoryDetails");
                if(objErrMsg!=null && objErrMsg.Value!=null)
                        errmsg=Convert.ToString(objErrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateUnivesityCategoryDetails in University.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override List<BO.UniversityCategoryProperty> GetAllUniversityCategoryList()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objUniversityCategoryList = new List<BO.UniversityCategoryProperty>();
            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUniversityCategoryList");
               objUniversityCategoryList= BindUniversityCategoryObject(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllUniversityCategoryList in University.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objUniversityCategoryList;
        }

        public override List<BO.UniversityCategoryProperty> GetUniversityCategoryById(int universityCategoryId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objUniversityCategoryList = new List<BO.UniversityCategoryProperty>();
            try
            {
                _objDataWrapper.AddParameter("@UniversityCategoryId", universityCategoryId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUniversityCategoryList");
                objUniversityCategoryList = BindUniversityCategoryObject(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUniversityCategoryById in University.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objUniversityCategoryList;
        }

        public override int InsertUniversityDetails(BO.UniversityProperty objUniversityProerty, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";

            try
            {
                _objDataWrapper.AddParameter("@UniversityName", objUniversityProerty.UniversityName);
                _objDataWrapper.AddParameter("@UniversityUrl", objUniversityProerty.UniversityUrl);
                _objDataWrapper.AddParameter("@UniversityTitle", objUniversityProerty.UniversityTitle);
                _objDataWrapper.AddParameter("@UniversityMetaDesc", objUniversityProerty.UniversityMetaDesc);
                _objDataWrapper.AddParameter("@UniversityCategoryId", objUniversityProerty.UniversityCategoryId);
                _objDataWrapper.AddParameter("@UniversityMetaTag", objUniversityProerty.UniversityMetaTag);
                _objDataWrapper.AddParameter("@UniversityDesc", objUniversityProerty.UniversityDesc);
                _objDataWrapper.AddParameter("@UniversityLogo", objUniversityProerty.UniversityLogo);
                _objDataWrapper.AddParameter("@UniversityWebsite", objUniversityProerty.UniversityWebsite);
                _objDataWrapper.AddParameter("@UniversityEst", objUniversityProerty.UniversityEst);
                _objDataWrapper.AddParameter("@UniversityPhone", objUniversityProerty.UniversityPhoneNo);
                _objDataWrapper.AddParameter("@UniversityMobile", objUniversityProerty.UniversityMobile);
                _objDataWrapper.AddParameter("@UniversityEmailId", objUniversityProerty.UniversityEmailId);
                _objDataWrapper.AddParameter("@UniversityPopularName", objUniversityProerty.UniversityPopularName);
                _objDataWrapper.AddParameter("@UniversityShortName", objUniversityProerty.UniversityshortName);
                _objDataWrapper.AddParameter("@UniversityFax", objUniversityProerty.UniversityFax);
                _objDataWrapper.AddParameter("@UniversityAddrs", objUniversityProerty.UniversityAddrs);
                _objDataWrapper.AddParameter("@UniversityCountryId", objUniversityProerty.UniversityCountryId);
                _objDataWrapper.AddParameter("@UniversityStateId", objUniversityProerty.UniversityStateId);
                _objDataWrapper.AddParameter("@UniversityCityId", objUniversityProerty.UniversityCityId);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objErrMsg=
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg","",SqlDbType.VarChar,ParameterDirection.Output ,128));

                    _i=_objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateUniversityDetails");
                    if (objErrMsg!=null && objErrMsg.Value!=null)
                            errmsg=Convert.ToString(objErrMsg.Value);
                
                

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUniversityDetails in University.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int UpdateUniversityDetails(BO.UniversityProperty objUniversityProerty, int modifiedBy, out string errmsg)
        {
             _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";

            try
            {
                _objDataWrapper.AddParameter("@UniversityId", objUniversityProerty.UniversityId);
                _objDataWrapper.AddParameter("@UniversityName", objUniversityProerty.UniversityName);
                _objDataWrapper.AddParameter("@UniversityUrl", objUniversityProerty.UniversityUrl);
                _objDataWrapper.AddParameter("@UniversityTitle", objUniversityProerty.UniversityTitle);
                _objDataWrapper.AddParameter("@UniversityMetaDesc", objUniversityProerty.UniversityMetaDesc);
                _objDataWrapper.AddParameter("@UniversityCategoryId", objUniversityProerty.UniversityCategoryId);
                _objDataWrapper.AddParameter("@UniversityMetaTag", objUniversityProerty.UniversityMetaTag);
                _objDataWrapper.AddParameter("@UniversityDesc", objUniversityProerty.UniversityDesc);
                _objDataWrapper.AddParameter("@UniversityLogo", objUniversityProerty.UniversityLogo);
                _objDataWrapper.AddParameter("@UniversityWebsite", objUniversityProerty.UniversityWebsite);
                _objDataWrapper.AddParameter("@UniversityEst", objUniversityProerty.UniversityEst);
                _objDataWrapper.AddParameter("@UniversityPhone", objUniversityProerty.UniversityPhoneNo);
                _objDataWrapper.AddParameter("@UniversityMobile", objUniversityProerty.UniversityMobile);
                _objDataWrapper.AddParameter("@UniversityEmailId", objUniversityProerty.UniversityEmailId);
                _objDataWrapper.AddParameter("@UniversityPopularName", objUniversityProerty.UniversityPopularName);
                _objDataWrapper.AddParameter("@UniversityShortName", objUniversityProerty.UniversityshortName);
                _objDataWrapper.AddParameter("@UniversityFax", objUniversityProerty.UniversityFax);
                _objDataWrapper.AddParameter("@UniversityAddrs", objUniversityProerty.UniversityAddrs);
                _objDataWrapper.AddParameter("@UniversityCountryId", objUniversityProerty.UniversityCountryId);
                _objDataWrapper.AddParameter("@UniversityStateId", objUniversityProerty.UniversityStateId);
                _objDataWrapper.AddParameter("@UniversityCityId", objUniversityProerty.UniversityCityId);
                _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
                var objErrMsg=
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg","",SqlDbType.VarChar,ParameterDirection.Output ,128));

                    _i=_objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateUniversityDetails");
                    if (objErrMsg!=null && objErrMsg.Value!=null)
                            errmsg=Convert.ToString(objErrMsg.Value);
                
                

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateUniversityDetails in University.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }
        public override int InsertUniversityDetailsUpload(BO.UniversityProperty objUniversityProerty, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";

            try
            {
                 _objDataWrapper.AddParameter("@UniversityName", objUniversityProerty.UniversityName);
                 _objDataWrapper.AddParameter("@UniversityCategoryName", objUniversityProerty.UniversityCategoryName);
                _objDataWrapper.AddParameter("@UniversityDesc", objUniversityProerty.UniversityDesc);
                _objDataWrapper.AddParameter("@UniversityWebsite", objUniversityProerty.UniversityWebsite);
                _objDataWrapper.AddParameter("@UniversityEst", objUniversityProerty.UniversityEst);
                _objDataWrapper.AddParameter("@UniversityPhone", objUniversityProerty.UniversityPhoneNo);
                _objDataWrapper.AddParameter("@UniversityMobile", objUniversityProerty.UniversityMobile);
                _objDataWrapper.AddParameter("@UniversityEmailId", objUniversityProerty.UniversityEmailId);
                _objDataWrapper.AddParameter("@UniversityPopularName", objUniversityProerty.UniversityPopularName);
                _objDataWrapper.AddParameter("@UniversityShortName", objUniversityProerty.UniversityshortName);
                _objDataWrapper.AddParameter("@UniversityFax", objUniversityProerty.UniversityFax);
                _objDataWrapper.AddParameter("@UniversityAddrs", objUniversityProerty.UniversityAddrs);
                _objDataWrapper.AddParameter("@UniversityCountryName", objUniversityProerty.UniversityCountryName);
                _objDataWrapper.AddParameter("@UniversityStateName", objUniversityProerty.UniversityStateName);
                _objDataWrapper.AddParameter("@UniversityCityName", objUniversityProerty.UniversityCityName);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateUniversityDataUpload");
              
                

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing Upload universityData in University.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }
        public override List<BO.UniversityProperty> GetAllUniversityList()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objUniversityList = new List<BO.UniversityProperty>();
            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUniversityList");
                objUniversityList = BindUniversityObject(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllUniversityList in University.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objUniversityList;
        }

        public override List<BO.UniversityProperty> GetUniversityListById(int universityId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objUniversityList = new List<BO.UniversityProperty>();
            try
            {
                _objDataWrapper.AddParameter("@UniversityId", universityId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUniversityList");
                objUniversityList = BindUniversityObject(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUniversityListById in University.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objUniversityList;
        }

        public override List<BO.UniversityProperty> GetUniversityListByName(string universityName)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objUniversityList = new List<BO.UniversityProperty>();
            try
            {
                _objDataWrapper.AddParameter("@UniversityName", universityName);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUniversityList");
                objUniversityList = BindUniversityObject(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUniversityListByName in University.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objUniversityList;
        }
        public override List<BO.UniversityProperty> GetUniversityListByCategory(int universityCategoryId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objUniversityList = new List<BO.UniversityProperty>();
            try
            {
                _objDataWrapper.AddParameter("@UniversityCategoryId", universityCategoryId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUniversityList");
                objUniversityList = BindUniversityObject(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUniversityListByCategory in University.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objUniversityList;
        }

        public override List<BO.UniversityProperty> GetUniversityListByCategoryUniversityName(int universityCategoryId, string universityName)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objUniversityList = new List<BO.UniversityProperty>();
            try
            {
                _objDataWrapper.AddParameter("@UniversityName", universityName);
                _objDataWrapper.AddParameter("@UniversityCategoryId", universityCategoryId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUniversityList");
                objUniversityList = BindUniversityObject(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUniversityListByCategoryUniversityName in University.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objUniversityList;
        }
        // private method to bind The Object List Of The university Categoty object
        private List<BO.UniversityCategoryProperty> BindUniversityCategoryObject(DataTable datatable)
        {
            var objUniversityCategoryList=new List<BO.UniversityCategoryProperty>();
            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)   
                    {
                        var objUniversityCategory = new BO.UniversityCategoryProperty
                                        {
                                            UniversityCategoryId = Convert.ToInt32(datatable.Rows[j]["AjUniversityCategoryId"]),
                                            UniversityCategoryName = Convert.ToString(datatable.Rows[j]["AjUniversityCategoryName"])
                                        };
                        objUniversityCategoryList.Add(objUniversityCategory);
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
                const string addInfo = "Error while executing BindUniversityCategoryObject in University.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objUniversityCategoryList;

        }

        private List<BO.UniversityProperty> BindUniversityObject(DataTable datatable)
        {
            var objUniversityList = new List<BO.UniversityProperty>();
            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {
                        var objUniversity = new BO.UniversityProperty
                                    {
                                        
                                        UniversityAddrs= (datatable.Rows[j]["AjUniversityAddress"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUniversityAddress"]) ,
                                        UniversityCategoryId = (datatable.Rows[j]["AjUniversityCategoryId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjUniversityCategoryId"]),
                                        UniversityCategoryName = (datatable.Rows[j]["AjUniversityCategoryName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUniversityCategoryName"]),
                                        UniversityCityId = (datatable.Rows[j]["AjUniversityCityId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjUniversityCityId"]),
                                        UniversityCityName = (datatable.Rows[j]["AjCityName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCityName"]),
                                        UniversityCountryId = (datatable.Rows[j]["AjUniversityCountryId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjUniversityCountryId"]),
                                        UniversityCountryName = (datatable.Rows[j]["AjCountryName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCountryName"]),
                                        UniversityDesc =(datatable.Rows[j]["AjUniversityDesc"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUniversityDesc"]) ,
                                        UniversityEmailId =(datatable.Rows[j]["AjUniversityEmailId"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUniversityEmailId"]),
                                        UniversityEst = (datatable.Rows[j]["AjUniversityEst"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjUniversityEst"]),
                                        UniversityFax =(datatable.Rows[j]["AjUniversityFax"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUniversityFax"]) ,
                                        UniversityId =(datatable.Rows[j]["AjUniversityId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjUniversityId"]) ,
                                        UniversityLogo =(datatable.Rows[j]["AjUniversityLogo"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUniversityLogo"]),
                                        UniversityMetaDesc = (datatable.Rows[j]["AjUniversityMetaDesc"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUniversityMetaDesc"]),
                                        UniversityMetaTag = (datatable.Rows[j]["AjUniversityMetaTag"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUniversityMetaTag"]) ,
                                        UniversityMobile =(datatable.Rows[j]["AjUniversityMobile"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUniversityMobile"]),
                                        UniversityName =(datatable.Rows[j]["AjUniversityName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUniversityName"]),
                                        UniversityPhoneNo =(datatable.Rows[j]["AJuniversityPhone"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AJuniversityPhone"]),
                                        UniversityPopularName =(datatable.Rows[j]["AjUniversityPopularName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUniversityPopularName"]) ,
                                        UniversityshortName = (datatable.Rows[j]["AjUniversityShortName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUniversityShortName"]),
                                        UniversityStateId =(datatable.Rows[j]["AjUniversityStateId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjUniversityStateId"]),
                                        UniversityStateName =(datatable.Rows[j]["AjStateName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjStateName"]),
                                        UniversityTitle = (datatable.Rows[j]["AjUniversityTitle"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUniversityTitle"]),
                                        UniversityUrl = (datatable.Rows[j]["AjUniversityUrl"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUniversityUrl"]),
                                        UniversityWebsite=(datatable.Rows[j]["AjUniversityWebsite"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUniversityWebsite"])
                                        

                                    };
                        objUniversityList.Add(objUniversity);
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
                const string addInfo = "Error while executing BindUniversityObject in University.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objUniversityList;
        }





       
    }
}
