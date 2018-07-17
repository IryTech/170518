using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IryTech.AdmissionJankari.DAL;


namespace IryTech.AdmissionJankari.BL
{
  public   class Country: CountryProvider 
  {
      private DbWrapper _objDataWrapper;
      private int _i ;
      private DataSet _dataSet;

      // method to Insert the country 
      public  override   int InsertCountry(string countryname,int createdby, out string errMsg, string countrycode = null )
      {
          _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
          errMsg = string.Empty;
          try
          {
              _objDataWrapper.AddParameter("@CountryName", countryname);
              _objDataWrapper.AddParameter("@CountryCode", countrycode);
              _objDataWrapper.AddParameter("@createdBy", createdby);
            var objErrMsg =
                  (SqlParameter)
                  (_objDataWrapper.AddParameter("@errmsg", "", SqlDbType.VarChar, ParameterDirection.Output, 64));
              _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCountryDetails");
              if (objErrMsg != null && objErrMsg.Value != null)
                  errMsg = Convert.ToString(objErrMsg.Value);

          }
          catch (Exception ex)
          {
              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.InnerException.Message;
              }
              const string addInfo = "Error while executing InsertCountry in Country.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);
          }
          return _i;
      }

      public override int UpdateCountry(int countryid, string countryname, int createdby, out string errmsg, string countrycode = null)
      {
          _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
          errmsg = string.Empty;
          try
          {
              _objDataWrapper.AddParameter("@CountryId", countryid);
              _objDataWrapper.AddParameter("@CountryName", countryname);
              _objDataWrapper.AddParameter("@CountryCode", countrycode);
              _objDataWrapper.AddParameter("@createdBy", createdby);
            var objErrMsg =
                  (SqlParameter)
                  (_objDataWrapper.AddParameter("@errmsg", "", SqlDbType.VarChar, ParameterDirection.Output, 64));
              _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCountryDetails");
              if (objErrMsg != null && objErrMsg.Value != null)
                  errmsg = Convert.ToString(objErrMsg.Value);

          }
          catch (Exception ex)
          {
              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.ToString();
              }
              const string addInfo = "Error while executing UpdateCountry in Country.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);
          }
          return _i;
      }

      public override List<CountryProperty> GetAllCountry()
      {   _dataSet=new DataSet();
          _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
          var objCountryList = new List<CountryProperty>();
          try
          {
             _dataSet=_objDataWrapper.ExecuteDataSet("Aj_Proc_GetCountryList");
              objCountryList = BindCountryObject(_dataSet.Tables[0]);

          }
          catch (Exception ex)
          {
              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.ToString();
              }
              const string addInfo = "Error while executing GetAllCountry in Country.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);
          }
          return objCountryList;
      }

      public override List<CountryProperty> GetCountryById(int countryid)
      {
          _dataSet = new DataSet();
          _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
          var objCountryList = new List<CountryProperty>();
          try
          {
              _objDataWrapper.AddParameter("@CountryId", countryid);
              _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCountryList");
              objCountryList = BindCountryObject(_dataSet.Tables[0]);

          }
          catch (Exception ex)
          {
              var err = ex.Message;
              if (ex.InnerException != null)
              {
                  err = err + " :: Inner Exception :- " + ex.ToString();
              }
              const string addInfo = "Error while executing GetAllCountry in Country.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);
          }
          return objCountryList;
      }

      private List <CountryProperty> BindCountryObject(DataTable dataTable )
      {
          if (dataTable == null) throw new ArgumentNullException("dataTable");
          var objCountryList = new List<CountryProperty>();
          
          
        try
          {
              if(dataTable.Rows.Count>0 )
              {
                  
                  for(var j=0;j<dataTable.Rows.Count;j++)
                  {
                      var objCountry = new CountryProperty
                                           {
                                               CountryId = Convert.ToInt32(dataTable.Rows[j]["AjCountryId"]),
                                               CountryCode = Convert.ToString(dataTable.Rows[j]["AjCountryCode"]),
                                               CountryName = Convert.ToString(dataTable.Rows[j]["AjCountryName"])
                                           };
                      objCountryList.Add(objCountry);
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
              const string addInfo = "Error while executing BindCountryObject in Country.cs  :: -> ";
              var objPub = new ClsExceptionPublisher();
              objPub.Publish(err, addInfo);
              
          }
          return objCountryList;
      }
  }
}
