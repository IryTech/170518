using System;
using System.Collections.Specialized;
using System.Data;

namespace IryTech.AdmissionJankari.Components.Provider
{
    public class DbApplicationProvider : ApplicationProvider
  {
      #region  Method decleration of the class

        private DataTable _dt;
        private DAL.DbWrapper  _objDataWrapper;
        

         public override StringDictionary LoadSettings()
         {
              var dic = new StringDictionary();
             _dt=new DataTable();
             try
             {
                 _objDataWrapper = new DAL.DbWrapper(ApplicationConfig.ConnectionStraing, CommandType.StoredProcedure);
                 var dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetApplicationSetting");
                 _dt = dataSet.Tables[0];


                 if (_dt.Rows.Count > 0)
                 {
                     for (var i = 0; i < _dt.Rows.Count; i++)
                     {
                         var name = Convert.ToString(_dt.Rows[i]["ApplicationSettingName"]);
                         var value = Convert.ToString(_dt.Rows[i]["ApplicationSettingValue"]);
                         dic.Add(name, value);
                     }
                 }
             }
             catch (Exception ex)
             {
                 var err = ex.Message;
                 //if (ex.InnerException != null)
                 //{
                 //    err = err + " :: Inner Exception :- " + ex.ToString();
                 //}
                 //const string addInfo = "Error while executing LoadSettings in DbApplicationProvider  :: -> ";
                 //var objPub = new IryTech.AdmissionJankari.BL.ClsExceptionPublisher();
                 //objPub.Publish(err, addInfo);
             }
           
            return dic;

         }

      #endregion

    
    }
}
