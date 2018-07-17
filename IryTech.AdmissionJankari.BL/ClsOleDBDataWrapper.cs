using System;
using System.Data;

using System.Data.OleDb;

/// <summary>
/// Summary description for ClsOleDBDataWrapper
/// </summary>
/// 
namespace IryTech.AdmissionJankari.BL
{
    public class ClsOleDBDataWrapper
    {
        public ClsOleDBDataWrapper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string[] CountTotalSheets(string file)
        {
            int i = 0;
            OleDbConnection consrc;
            String[] excelSheets = new String[0];
            string consrcstr = file.EndsWith(".xls") ? Common.OleCnnString : Common.OleCnnString2007;
            consrcstr=consrcstr +file;
            consrc = new OleDbConnection(consrcstr);
            DataSet ds = new DataSet();
            try
            {

                if (consrc.State == ConnectionState.Closed)
                {
                    consrc.Open();
                }

                DataTable dt = consrc.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                excelSheets = new String[dt.Rows.Count];

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }
                consrc.Close();
            }
            catch (Exception ex)
            {

                consrc.Close();
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while CountTotalSheets in ClsOleDBDataWrapper  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            finally
            {
                consrc.Close();
            }
            return excelSheets;
        }

        public DataSet getdata(string file, string sheetname)
        {
            OleDbConnection consrc;
            string consrcstr = file.EndsWith(".xls")? Common.OleCnnString:Common.OleCnnString2007;
            consrcstr=consrcstr+file;
            consrc = new OleDbConnection(consrcstr);
            DataSet ds = new DataSet();
            try
            {
                
                consrc.Open();

                string cmd = "Select * from [" + sheetname + "]"; //[Sheet1$]";

                OleDbDataAdapter cmdsrc = new OleDbDataAdapter(cmd, consrcstr);
                cmdsrc.TableMappings.Clear();
                cmdsrc.TableMappings.Add("Table", "Data");

                cmdsrc.Fill(ds);
                consrc.Close();
            }
            catch (Exception ex)
            {
                consrc.Close();
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                string addInfo = "Error :: -> ";
                ClsExceptionPublisher objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            finally
            {
                consrc.Close();
            }
            return ds;
        }

        public DataTable GetExcelColumnHeader(string file, string sheetname)
        {
            OleDbConnection consrc;
            string consrcstr = file.EndsWith(".xls") ? Common.OleCnnString : Common.OleCnnString2007;
            consrcstr = consrcstr + file;
            consrc = new OleDbConnection(consrcstr);
            DataSet ds = new DataSet();
            try
            {

                if (consrc.State == ConnectionState.Closed)
                {
                    consrc.Open();
                }

                string cmd = "Select top 1 * from  [" + sheetname + "]" ; //[Sheet1$]";

                OleDbDataAdapter cmdsrc = new OleDbDataAdapter(cmd, consrc);
                cmdsrc.TableMappings.Clear();
                cmdsrc.TableMappings.Add("Table", "Data");

                cmdsrc.Fill(ds);
                consrc.Close();
            }
            catch (Exception ex)
            {
                consrc.Close();
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                string addInfo = "Error :: -> ";
                ClsExceptionPublisher objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            finally
            {
                consrc.Close();
            }
            return ds.Tables[0];
        }



        // Method To get The Column Data from Excel

        public DataTable GetExcelSheetValueBasedOnColumn(string fileName, string columnName, string sheetname)
        {
            OleDbConnection consrc;
            string consrcstr = fileName.EndsWith(".xls") ? Common.OleCnnString : Common.OleCnnString2007;
            consrcstr = consrcstr + fileName;
            consrc = new OleDbConnection(consrcstr);
            DataSet ds = new DataSet();
            try
            {
                if (consrc.State == ConnectionState.Closed)
                {
                    consrc.Open();
                }

                string cmd = "select   " + columnName + "   from [" + sheetname + "]"; 

                OleDbDataAdapter cmdsrc = new OleDbDataAdapter(cmd, consrc);
                cmdsrc.TableMappings.Clear();
                cmdsrc.TableMappings.Add("Table", "Data");

                cmdsrc.Fill(ds);
                consrc.Close();
            }
            catch (Exception ex)
            {
                consrc.Close();
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                string addInfo = "Error while executing GetExcelSheetValueBasedOnColumn in OleDbConnection.cs :: -> ";
                ClsExceptionPublisher objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            finally
            {
                consrc.Close();
            }
            return ds.Tables[0];
        }
    }

}