using System;
using System.Collections.Generic;
using System.Data;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.DAL;
using System.Data.SqlClient;

namespace IryTech.AdmissionJankari.BL
{
  public   class Zone:ZoneProvider 
  {
      private DbWrapper  _objDataWrapper;
      private DataSet _dataSet;
      private int _i;

      public override  int InsertZoneDetails(string zoneName, int createdBy, out string errMsg)
      {
          _objDataWrapper=new DbWrapper(Common.CnnString,CommandType.StoredProcedure);
          errMsg = string.Empty;
          try
          {
              _objDataWrapper.AddParameter("@ZoneName", zoneName);
              _objDataWrapper.AddParameter("@CreatedBy", createdBy);
              var objErrmsg =
                  (SqlParameter)
                  (_objDataWrapper.AddParameter("@errMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 64));

              _i=_objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateZoneDetails");
              if (objErrmsg != null && objErrmsg.Value != null)
                  errMsg = Convert.ToString(objErrmsg.Value);
          }
          catch (Exception ex)
          {
              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.ToString();
              }
              const string addInfo = "Error while executing InsertZoneDetails in Zone.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);
          }
          return _i;
      }

      public override int UpdateZoneDetails(int zoneId, string zoneName, int modifiedBy, out string errMsg)
      {
          _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
          errMsg = string.Empty;
          try
          {
              _objDataWrapper.AddParameter("@ZoneId", zoneId);
              _objDataWrapper.AddParameter("@ZoneName", zoneName);
              _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
              var objErrmsg =
                  (SqlParameter)
                  (_objDataWrapper.AddParameter("@errMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 64));

              _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateZoneDetails");
              if (objErrmsg != null && objErrmsg.Value != null)
                  errMsg = Convert.ToString(objErrmsg.Value);
          }
          catch (Exception ex)
          {
              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.ToString();
              }
              const string addInfo = "Error while executing UpdateZoneDetails in Zone.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);
          }
          return _i;
      }

      public override List<ZoneProperty> GetAllZoneList()
      {
          _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
          var objZoneList = new List<ZoneProperty>();
          _dataSet=new DataSet();
          try
          {
              _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetZoneList");
              objZoneList = FillZoneObject(_dataSet.Tables[0]);

          }
          catch (Exception ex)
          {

              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.ToString();
              }
              const string addInfo = "Error while executing GetAllZoneList in Zone.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);
          }
          return objZoneList;
      }

      public override List<ZoneProperty> GetZoneListById(int zoneId)
      {
          _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
          var objZoneList = new List<ZoneProperty>();
          _dataSet=new DataSet();
          try
          {
              _objDataWrapper.AddParameter("@ZoneId", zoneId);
              _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetZoneList");
              objZoneList = FillZoneObject(_dataSet.Tables[0]);
          }
          catch (Exception ex)
          {
              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.ToString();
              }
              const string addInfo = "Error while executing GetZoneListById in Zone.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);

          }
          return objZoneList;
      }

      private List<ZoneProperty> FillZoneObject(DataTable dataTable)
      {
          var objZoneObjectList = new List<ZoneProperty>();
          try
          {
              if(dataTable.Rows.Count>0)
              {
                  for (var j = 0; j < dataTable.Rows.Count; j++)
                  {
                      var objZoneObject = new ZoneProperty
                                              {
                                                  ZoneId = Convert.ToInt32(dataTable.Rows[j]["AjZoneId"]),
                                                  ZoneName = Convert.ToString(dataTable.Rows[j]["AjZoneName"])
                                              };
                      objZoneObjectList.Add(objZoneObject);
                  }
              }

          }
          catch (Exception ex )
          {
              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.ToString();
              }
              const string addInfo = "Error while executing FillZoneObject in Zone.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);
              
          }
          return objZoneObjectList;
      }
    }
}



