using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using IryTech.AdmissionJankari.DAL;
using System.Data.OleDb;
using System.Web;
/// <summary>
/// Summary description for ClsDBTablesAttributes
/// </summary>
/// 


namespace IryTech.AdmissionJankari.BL
{
    public class ClsDBTablesAttributes
    {
        #region "Data Members"
        SqlConnection conn = new SqlConnection(Common.CnnString);
        SqlCommand cmd;
        SqlDataReader sdr;
        DataTable dtb;

        #endregion "Data Members"




        /* Default Constructor.............*/
        public ClsDBTablesAttributes() { }



        /*--------------------------------------------------------------*/

        /// <summary>
        /// Property for Get And Set Column Name...
        /// </summary>
        public global::System.String strColumnName;
        public global::System.String PropColumnName
        {
            get { return strColumnName; }
            set { strColumnName = value; }
        }

        /// <summary>
        /// Property  for Get And Set Column's Datatype...
        /// </summary>
        public global::System.String strDataType;
        public global::System.String PropDataType
        {
            get { return strDataType; }
            set { strDataType = value; }
        }

        /// <summary>
        /// Property  for Get And Set Column's Datatype Size...
        /// </summary>
        public global::System.String strColumnDTCharLength;
        public global::System.String PropColumnDTCharLength
        {
            get { return strColumnDTCharLength; }
            set { strColumnDTCharLength = value; }
        }


        /*--------------------------------------------------------------*/

        /// <summary>
        /// Property  for Get And Set Table's Name...
        /// </summary>
        public global::System.String strTABLENAME;
        public global::System.String PropTABLENAME
        {
            get { return strTABLENAME; }
            set { strTABLENAME = value; }
        }


        /// <summary>
        /// Property  for Get And Set PrimaryKey Column's Names...
        /// </summary>
        public global::System.String strPK_COLUMNNAME;
        public global::System.String PropPK_COLUMNNAME
        {
            get { return strPK_COLUMNNAME; }
            set { strPK_COLUMNNAME = value; }
        }












        public List<ClsDBTablesAttributes> GetAllColumnsOfTable(string _strTableName, string AutoIncrementedColumnName)
        {
            DbWrapper objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            DataSet ds = new DataSet();

            List<ClsDBTablesAttributes> _objListClsDBTables = new List<ClsDBTablesAttributes>();
            objDataWrapper.AddParameter("@TableName", _strTableName);
            objDataWrapper.AddParameter("@AutoIncrementedColumnName", AutoIncrementedColumnName);
            ds = objDataWrapper.ExecuteDataSet("Aj_Proc_GetColumnsOfTable");


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ClsDBTablesAttributes _objClsDBTablesAttributes = new ClsDBTablesAttributes();
                _objClsDBTablesAttributes.PropColumnName = ds.Tables[0].Rows[i]["column_name"].ToString();
                _objClsDBTablesAttributes.PropDataType = ds.Tables[0].Rows[i]["data_type"].ToString();
                _objClsDBTablesAttributes.PropColumnDTCharLength = ds.Tables[0].Rows[i]["charLength"].ToString();
                _objListClsDBTables.Add(_objClsDBTablesAttributes);
            }


            return _objListClsDBTables;
        }







        public List<ClsDBTablesAttributes> GetAllTablesNPrimaryColumns(string tblName)
        {
            DbWrapper objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            DataSet ds = new DataSet();

            List<ClsDBTablesAttributes> _objListClsDBTablesAttributes = new List<ClsDBTablesAttributes>();

            objDataWrapper.AddParameter("@tblName", tblName);
            ds = objDataWrapper.ExecuteDataSet("Aj_Proc_GetAllTables");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ClsDBTablesAttributes _objClsDBTablesAttributes = new ClsDBTablesAttributes();

                _objClsDBTablesAttributes.PropTABLENAME = ds.Tables[0].Rows[i]["TABLE_NAME"].ToString();
                _objClsDBTablesAttributes.PropPK_COLUMNNAME = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                _objListClsDBTablesAttributes.Add(_objClsDBTablesAttributes);
            }

            return _objListClsDBTablesAttributes;
        }



        // Method to Get The List Of the column name from the excel File 
        public List<string> GetColumnListofExcel(string fileName)
        {
            ClsOleDBDataWrapper _objClsOleDbDataWrapper = new ClsOleDBDataWrapper();
            Common objCommon = new Common();
            List<string> _strColumnName = new List<string>();
            var path = HttpContext.Current.Server.MapPath("~/" + objCommon.GetFilepath("ExcelUpload") + fileName);
            var excelSheets = _objClsOleDbDataWrapper.CountTotalSheets(path);
            foreach (var t in excelSheets)
            {
                dtb = _objClsOleDbDataWrapper.GetExcelColumnHeader(path, t);
                foreach (DataColumn dc in dtb.Columns)
                {
                    _strColumnName.Add(dc.ColumnName);
                }
            }
            return _strColumnName;

        }



        public DataTable GetTableSchema(string _strTableName, string _strPrimaryKey)
        {
            dtb = new DataTable();
            cmd = new SqlCommand("Crm_Proc_GetTableSchema", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TableName", _strTableName);
            cmd.Parameters.AddWithValue("@AutoIncrementedColumnName", _strPrimaryKey);
            conn.Open();
            sdr = cmd.ExecuteReader();
            dtb.Load(sdr);
            conn.Dispose();
            return dtb;
        }
    }


}
