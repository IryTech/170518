using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using IryTech.AdmissionJankari.DAL;


namespace IryTech.AdmissionJankari.BL
{
    public class City : CityProvider 
    {
        private DbWrapper  _objDataWrapper;
        private DataSet _dataSet;
        private int _i;

        
        public override  int InsertCityDetails(string cityName, int stateId, int createdby, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = string.Empty;
            try
            {
                _objDataWrapper.AddParameter("@CityName", cityName);
                _objDataWrapper.AddParameter("@StateId", stateId);
                _objDataWrapper.AddParameter("@CreatedBy", createdby);
                var objErrmsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@errMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 64));

                _i = _objDataWrapper.ExecuteNonQuery("AJ_Proc_InsertUpdateCityDetails");
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
                const string addInfo = "Error while executing InsertCityDetails in City.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return _i;
        }

        public override int UpdateCityDetails(int cityId, string cityName, int stateId, int modifiedBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = string.Empty;
            try
            {
                _objDataWrapper.AddParameter("@CityId", cityId);
                _objDataWrapper.AddParameter("@CityName", cityName);
                _objDataWrapper.AddParameter("@StateId", stateId);
                _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
                var objErrmsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@errMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 64));

                _i = _objDataWrapper.ExecuteNonQuery("AJ_Proc_InsertUpdateCityDetails");
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
                const string addInfo = "Error while executing UpdateCityDetails in City.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return _i;
        }

        public override List<CityProperty> GetAllCityList()
        {
            _dataSet=new DataSet();
            _objDataWrapper=new DbWrapper(Common.CnnString,CommandType.StoredProcedure);
            var objCityList = new List<CityProperty>();

            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetCityList");
                objCityList = BindCityObject(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllCityList in City.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objCityList;
        }
        public override List<CityProperty> GetCityDetailsByName(string cityName)
        {
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objCityList = new List<CityProperty>();

            try
            {
                _objDataWrapper.AddParameter("@CityName", cityName);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetCityList");
                objCityList = BindCityObject(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCityDetailsByName in City.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objCityList;
        }

        public override List<CityProperty> GetCityById(int cityId)
        {
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objCityList = new List<CityProperty>();

            try
            {
                _objDataWrapper.AddParameter("@CityId", cityId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetCityList");
                objCityList = BindCityObject(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCityById in City.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objCityList;
        }

        public override List<CityProperty> GetCityListByState(int stateId)
        {
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objCityList = new List<CityProperty>();

            try
            {
                _objDataWrapper.AddParameter("@StateId", stateId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetCityList");
                objCityList = BindCityObject(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCityListByState in City.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objCityList;
        }

        public override List<CityProperty> GetCityListByCountry(int countryId)
        {
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objCityList = new List<CityProperty>();

            try
            {
                _objDataWrapper.AddParameter("@CountryId", countryId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetCityList");
                objCityList = BindCityObject(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCityListByCountry in City.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objCityList;
        }

        public override List<CityProperty> GetCityListByZone(int zoneId)
        {
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objCityList = new List<CityProperty>();

            try
            {
                _objDataWrapper.AddParameter("@ZoneId", zoneId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetCityList");
                objCityList = BindCityObject(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCityListByZone in City.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objCityList;
        }



        private List<CityProperty> BindCityObject(DataTable dataTable)
        {
            var objCityList = new List<CityProperty>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {

                    for (var j = 0; j < dataTable.Rows.Count; j++)
                    {

                        var objCity = new CityProperty
                                          {
                                              CityId = (dataTable.Rows[j]["AjCityId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjCityId"]),
                                              CityName = (dataTable.Rows[j]["AjCityName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCityName"]),
                                              CountryId = (dataTable.Rows[j]["AjCountryId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjCountryId"]),
                                              CountryName = (dataTable.Rows[j]["AjCountryName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCountryName"]),
                                              StateId = (dataTable.Rows[j]["AjStateId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjStateId"]),
                                              StateName = (dataTable.Rows[j]["AjStateName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjStateName"]),
                                              ZoneId = (dataTable.Rows[j]["AjZoneId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjZoneId"]),
                                              ZoneName = (dataTable.Rows[j]["AjZoneName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjZoneName"])
                                          };
                        objCityList.Add(objCity);

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
                const string addInfo = "Error while executing BindCityObject in City.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objCityList;
        }
    }
}
