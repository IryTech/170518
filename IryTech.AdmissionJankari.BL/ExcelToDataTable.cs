using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO; 
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace IryTech.AdmissionJankari.BL
{
  public  class ExcelToDataTable
    {
        #region "Data Members"
        SqlConnection conn = new SqlConnection(Common.CnnString);
        SqlCommand cmd; 
        SqlDataReader sdr;
        DataTable dtb;         
        List<string> _strColumnName;
        #endregion "Data Members"




        #region "Members Functions"
        
        /* Start: Constructor.....*/
        public ExcelToDataTable()
        {
            
            //
            // TODO: Add constructor logic here
            //
        }
        /* End: Constructor.....*/
        
        public DataTable GetRecordsForMappedColumnFromCSVToDataTable(string _strTableName, string _strPrimaryKey, string filePath, string[] SeprateColumns)
        {
            dtb = new DataTable();
            var objClsOledbdatalayer = new ClsOleDBDataWrapper();
            string FileColumnName = "", DBColumnName = "", DBDataType = "",DBSize="",StrColumn=""; 
            foreach (string strColumns in SeprateColumns)
            {
                var arrColumns = strColumns.Split('|'); 
                FileColumnName = Convert.ToString(arrColumns[0]).Trim();
                DBColumnName = Convert.ToString(arrColumns[1]).Trim();
                DBDataType = Convert.ToString(arrColumns[2]).Trim();
                DBSize = Convert.ToString(arrColumns[3]).Trim();
                          
                    StrColumn += FileColumnName + ","; 
            }            
            StrColumn = StrColumn.TrimEnd(',');

            string location = HttpContext.Current.Server.MapPath("~/Resource/ExcelUpload/" + filePath);

          

            try
            {
                  var excelSheets = objClsOledbdatalayer.CountTotalSheets(location);
                  if (excelSheets.Length > 0)
                  {

                      foreach (DataTable dt in excelSheets.Select(t => objClsOledbdatalayer.GetExcelSheetValueBasedOnColumn(location, StrColumn, t)).Where(dt => dt.Rows.Count > 0))
                      {

                          dtb = dt.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull || string.Compare((field as string).Trim(), string.Empty) == 0)).CopyToDataTable();
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
                const string addInfo = "Error while executing GetRecordsForMappedColumnFromCSVToDataTable in ExcelToDataTable.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

           
            return dtb;
            
        }
        
        public List<string> getColumnNameOfCSV(string file)
        { 
            using (StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("~/UserData/Import/" + file)))
            {
                string line = sr.ReadLine();
                _strColumnName = line.Split(',').ToList();
             }
            return _strColumnName;
        }
         
        
        #endregion "Members Functions"
    }
}
