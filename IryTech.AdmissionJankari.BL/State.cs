using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IryTech.AdmissionJankari.DAL;

namespace IryTech.AdmissionJankari.BL
{
  public class State:StateProvider 
  {
      private DbWrapper _objDataWrapper;
      private DataSet _dataSet;
      private int _i;
      public  override int InsertStateDetails(string stateName, int zoneId, int countryId, int createdBy, out string errmsg)
      {
          _objDataWrapper=new DbWrapper(Common.CnnString,CommandType.StoredProcedure);
          errmsg = string.Empty;
          try
          {
              _objDataWrapper.AddParameter("@StateName", stateName);
              _objDataWrapper.AddParameter("@ZoneId", zoneId);
              _objDataWrapper.AddParameter("@CountryId", countryId);
              _objDataWrapper.AddParameter("@CreatedBy", createdBy);
              var objerrMsg =
                  (SqlParameter)
                  (_objDataWrapper.AddParameter("@errmsg", "", SqlDbType.VarChar, ParameterDirection.Output, 64));
              _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateState");
              if (objerrMsg != null && objerrMsg.Value != null)
                  errmsg = Convert.ToString(objerrMsg.Value);

          }
          catch (Exception ex)
          {

              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.InnerException.Message;
              }
              const string addInfo = "Error while executing InsertStateDetails in State.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);
          }
          return _i;
      }

      public override int UpdateStateDetails(int stateId, string stateName, int zoneId, int countryId, int modofiedBy, out string errmsg)
      {
          _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
          errmsg = string.Empty;
          try
          {
              _objDataWrapper.AddParameter("@StateId", stateId);
              _objDataWrapper.AddParameter("@StateName", stateName);
              _objDataWrapper.AddParameter("@ZoneId", zoneId);
              _objDataWrapper.AddParameter("@CountryId", countryId);
              _objDataWrapper.AddParameter("@CreatedBy", modofiedBy);
              var objerrMsg =
                  (SqlParameter)
                  (_objDataWrapper.AddParameter("@errmsg", "", SqlDbType.VarChar, ParameterDirection.Output, 64));
              _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateState");
              if (objerrMsg != null && objerrMsg.Value != null)
                  errmsg = Convert.ToString(objerrMsg.Value);

          }
          catch (Exception ex)
          {

              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.InnerException.Message;
              }
              const string addInfo = "Error while executing UpdateStateDetails in State.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);
          }
          return _i;
      }

      public override List<StateProperty> GetAllState()
      {
          _objDataWrapper=new DbWrapper(Common.CnnString,CommandType.StoredProcedure);
          var objStateList = new List<StateProperty>();
          _dataSet=new DataSet();
          try
          {
              _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetStateList");
              objStateList = BindStateObject(_dataSet.Tables[0]);
          }
          catch (Exception ex)
          {
              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.InnerException.Message;
              }
              const string addInfo = "Error while executing GetAllState in State.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);
              
          }
          return objStateList;
      }

      public override List<StateProperty> GetStateByStateId(int stateId)
      {
          _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
          var objStateList = new List<StateProperty>();
          _dataSet = new DataSet();
          try
          {
              _objDataWrapper.AddParameter("@Stateid", stateId);
              _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetStateList");
              objStateList = BindStateObject(_dataSet.Tables[0]);
          }
          catch (Exception ex)
          {
              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.InnerException.Message;
              }
              const string addInfo = "Error while executing GetStateByStateId in State.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);

          }
          return objStateList;
      }

      public override List<StateProperty> GetStateByCountry(int countryId)
      {
          _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
          var objStateList = new List<StateProperty>();
          _dataSet = new DataSet();
          try
          {
              _objDataWrapper.AddParameter("@CountryId", countryId);
              _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetStateListByCountry");
              objStateList = BindStateObject(_dataSet.Tables[0]);
          }
          catch (Exception ex)
          {
              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.InnerException.Message;
              }
              const string addInfo = "Error while executing GetStateByStateId in State.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);

          }
          return objStateList;
      }

      public override List<StateProperty> GetStateByZone(int zoneId)
      {
          _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
          var objStateList = new List<StateProperty>();
          _dataSet = new DataSet();
          try
          {
              _objDataWrapper.AddParameter("@ZoneId", zoneId);
              _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetStateListByZone");
              objStateList = BindStateObject(_dataSet.Tables[0]);
          }
          catch (Exception ex)
          {
              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.InnerException.Message;
              }
              const string addInfo = "Error while executing GetStateByStateId in State.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);

          }
          return objStateList;
      }

      private List<StateProperty>BindStateObject(DataTable dataTable)
      {
          var objStateList = new List<StateProperty>();
         
          try
          {
              if(dataTable.Rows.Count>0)
              {
                  for(var j=0;j<dataTable.Rows.Count;j++)
                  {
                      var objState = new StateProperty
                                         {
                                             StateName = (dataTable.Rows[j]["AjStateName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjStateName"]),
                                             StateId = (dataTable.Rows[j]["AjStateId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjStateId"]),
                                             CountryId = (dataTable.Rows[j]["AjCountryId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjCountryId"]),
                                             CountryName = (dataTable.Rows[j]["AjCountryName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCountryName"]),
                                             ZoneId =(dataTable.Rows[j]["AjZoneId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjZoneId"]) ,
                                             ZoneName = (dataTable.Rows[j]["AjZoneName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjZoneName"]) 
                                         };
                      objStateList.Add(objState);
                  }
              }
          }
          catch (Exception ex)
          {

              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.InnerException.Message;
              }
              const string addInfo = "Error while executing BindStateObject in State.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);
          }
          return objStateList;
      }
    }
}
