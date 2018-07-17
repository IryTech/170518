using System;
using System.IO;
using System.Threading;
using System.Web;
using System.Xml;
using System.Globalization;
using System.Net;
using System.Data;
using System.Net.Mail;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using IryTech.AdmissionJankari.DAL;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BO;
using System.Collections;
using System.Text;
using System.Linq;



namespace IryTech.AdmissionJankari.BL
{
    public enum MatchPattern
    {
        First = 1,
        Last = 2
    }


    /// <summary>
    /// Summary description for ClsCommon
    /// </summary>
    public class Common
    {
        public static string OleCnnString;
        public static string OleCnnString2007;
        public static string CnnString;
        public static string StrBaseDirectory;
        public const string INDIA = "91";
        public const string CreatedByOther = "244";
        public static int ImageWidth;
        public static int ImageHeight;
        public static bool StrechImage;
        public const string HelpLineNo = "+91 -  9560 4455 76";
        public static bool ApplyBevel;
        public static string NoImageSubstitute;
        public static string DefaultCulture;
        public static string WebUrl;
        public static int DefaultPageSize;
        public static int PassPhraseOne = 1;
        public static int PassPhraseTwo = 7;

        private DataSet _dataSet;
        private DbWrapper _objDataWrapper;

        // This Method is used to datetime format form the string 
        public static DateTime GetDateFromString(string DateString)
        {
            CultureInfo culture = new CultureInfo(DefaultCulture);
            DateTime dateTimeValue;
            dateTimeValue = Convert.ToDateTime(DateString, culture);
            return dateTimeValue;
        }

        public static string GetStringProperCase(string strTiltle)
        {
            CultureInfo culture = new CultureInfo(DefaultCulture);
            TextInfo textInfo = culture.TextInfo;
            return textInfo.ToTitleCase(strTiltle);
        }

        public static string ConvertRupee(string ammount)
        {

            var parsed = decimal.Parse(ammount, CultureInfo.InvariantCulture);
            var hindi = new CultureInfo("hi-IN");
            return string.Format(hindi, "{0:c}", parsed);
        }

        public static string GetHtmlFromUrl(string url)
        {
            if (url.Length == 0)
                throw new ArgumentException("Invalid URL", "url");

            string html = "";
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            try
            {
                var responseStream = response.GetResponseStream();
                if (responseStream != null)
                {
                    var reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);

                    try
                    {
                        html = reader.ReadToEnd();
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }
            finally
            {
                response.Close();
            }
            return html;
        }

        public string GetErrorMessage(string messageId)
        {
            var flag = 0;
            var message = string.Empty;
            var xdoc = new XmlDocument();
            //get the Xml path
            var objHttp = HttpContext.Current;
            var xmlFilePath = objHttp.Server.MapPath("~") + "/Resource/Messages/Error.xml";
            xdoc.Load(xmlFilePath);
            {
                var nodeList = xdoc.SelectNodes("Root/message");



                for (var j = 0; j < nodeList.Count; j++)
                {
                    var xn = nodeList[j];
                    var attColl = xn.Attributes;

                    if (attColl[0].Value.ToString() == messageId.ToString())
                    {
                        message = attColl[1].Value.ToString();
                        break;
                    }
                }

            }
            return message;

        }

        public string GetValidationMessage(string messageId)
        {
            var flag = 0;
            var message = string.Empty;
            var xdoc = new XmlDocument();
            //get the Xml path
            var objHttp = HttpContext.Current;
            var xmlFilePath = objHttp.Server.MapPath("~") + "/Resource/Messages/Validatorerror.xml";
            xdoc.Load(xmlFilePath);
            {
                var nodeList = xdoc.SelectNodes("Root/message");
                for (var j = 0; j < nodeList.Count; j++)
                {
                    var xn = nodeList[j];
                    var attColl = xn.Attributes;

                    if (attColl[0].Value.ToString() == messageId.ToString())
                    {
                        message = attColl[1].Value.ToString();
                        break;
                    }
                }
            }
            return message;
        }

        public string GetFilepath(string messageId)
        {
            var flag = 0;
            var message = string.Empty;
            var xdoc = new XmlDocument();
            //get the Xml path
            var objHttp = HttpContext.Current;
            var xmlFilePath = objHttp.Server.MapPath("~") + "/Resource/Messages/FilePath.xml";
            xdoc.Load(xmlFilePath);
            {
                var nodeList = xdoc.SelectNodes("Root/FilePathId");
                for (var j = 0; j < nodeList.Count; j++)
                {
                    var xn = nodeList[j];
                    var attColl = xn.Attributes;

                    if (attColl[0].Value.ToString() == messageId.ToString())
                    {
                        message = attColl[1].Value.ToString();
                        break;
                    }
                }

            }
            return message;

        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            var properties =
                TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public DataTable GetApplicationSettingValue(int applicationSettingId = 0)
        {
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _objDataWrapper.AddParameter("@ApplicationSettingId", applicationSettingId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetApplicationSetting");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetApplicationSettingValue in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }

        public void AddAppLog(string origination, string message, DateTime incidentTime)
        {
            try
            {
                _objDataWrapper = new DbWrapper(CnnString, CommandType.StoredProcedure);
                _objDataWrapper.AddParameter("@ORIGINATION", origination);
                _objDataWrapper.AddParameter("@LOG_MESSAGE", message);
                _objDataWrapper.AddParameter("@INCDENT_TIME", incidentTime);
                _objDataWrapper.ExecuteNonQuery("Aj_Proc_ApplicationIncident");
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in GETTING AddAppLog in Common.cs :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        public static void AddAccessLog(string userIP)
        {
            try
            {
                DbWrapper _objDataWrapper = new DbWrapper(CnnString, CommandType.StoredProcedure);
                _objDataWrapper.AddParameter("@UserIp", userIP);
                _objDataWrapper.ExecuteNonQuery("ORK_Proc_InsertUserIpLog");
                _objDataWrapper.CommitTransaction(DbWrapper.CloseConnection.YES);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in GETTING AddAccessLog in Common.cs :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        public int InsertUpdateApplicationSettingValue(string applicationSettingName,
                                                       string applicationSettingValue, int createdBy, out string errMsg,
                                                       int applicationSettingId = 0)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            int _i = 0;
            errMsg = "";
            try
            {
                _objDataWrapper.AddParameter("@ApplicationSettingId", applicationSettingId);
                _objDataWrapper.AddParameter("@ApplicationSettingName", applicationSettingName);
                _objDataWrapper.AddParameter("@ApplicationSettingValue", applicationSettingValue);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objErrmsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateApplicationSetting");
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
                const string addInfo = "Error while executing InsertUpdateApplicationSettingValue in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public DataTable GetXmlMessage(string msgType)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            XmlReader xmlFile;

            try
            {
                switch (msgType)
                {
                    case "Error":
                        {
                            xmlFile =
                                XmlReader.Create(HttpContext.Current.Server.MapPath("~/Resource/Messages/Error.xml"),
                                                 new XmlReaderSettings());
                            _dataSet.ReadXml(xmlFile);
                            xmlFile.Close();
                            break;
                        }
                    case "Validation":
                        {
                            xmlFile =
                                XmlReader.Create(
                                    HttpContext.Current.Server.MapPath("~/Resource/Messages/Validatorerror.xml"),
                                    new XmlReaderSettings());
                            _dataSet.ReadXml(xmlFile);
                            xmlFile.Close();
                            break;
                        }
                    case "FilePath":
                        {
                            xmlFile =
                                XmlReader.Create(
                                    HttpContext.Current.Server.MapPath("~/Resource/Messages/FilePath.xml"),
                                    new XmlReaderSettings());
                            _dataSet.ReadXml(xmlFile);
                            xmlFile.Close();
                            break;
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
                const string addInfo = "Error while executing GetXmlErrorMessage in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }

        public int UpdateMessage(string msgId, string desc, string msgType)
        {
            int flag = 0;
            var xdoc = new XmlDocument();
            string xmlFilePath;
            switch (msgType)
            {
                case "Error":
                    {
                        xmlFilePath = HttpContext.Current.Server.MapPath("~/Resource/Messages/Error.xml");
                        XmlNodeList nodeList = xdoc.SelectNodes("Root/message");
                        xdoc.Load(xmlFilePath);
                        {
                            for (int j = 0; j < nodeList.Count; j++)
                            {
                                XmlNode xn = nodeList[j];
                                XmlAttributeCollection AttColl = xn.Attributes;

                                if (AttColl != null && AttColl[0].Value == msgId)
                                {
                                    AttColl[1].Value = desc;
                                    xdoc.Save(xmlFilePath);
                                    flag = 1;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                case "Validation":
                    {
                        xmlFilePath = HttpContext.Current.Server.MapPath("~/Resource/Messages/Validatorerror.xml");
                        XmlNodeList nodeList = xdoc.SelectNodes("Root/message");
                        xdoc.Load(xmlFilePath);
                        {
                            for (int j = 0; j < nodeList.Count; j++)
                            {
                                XmlNode xn = nodeList[j];
                                XmlAttributeCollection AttColl = xn.Attributes;

                                if (AttColl != null && AttColl[0].Value == msgId)
                                {
                                    AttColl[1].Value = desc;
                                    xdoc.Save(xmlFilePath);
                                    flag = 1;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                case "FilePath":
                    {
                        xmlFilePath = HttpContext.Current.Server.MapPath("~/Resource/Messages/FilePath.xml");
                        XmlNodeList nodeList = xdoc.SelectNodes("Root/FilePathId");
                        xdoc.Load(xmlFilePath);
                        {
                            for (int j = 0; j < nodeList.Count; j++)
                            {
                                XmlNode xn = nodeList[j];
                                XmlAttributeCollection AttColl = xn.Attributes;

                                if (AttColl != null && AttColl[0].Value == msgId)
                                {
                                    AttColl[1].Value = desc;
                                    xdoc.Save(xmlFilePath);
                                    flag = 1;
                                    break;
                                }
                            }
                        }
                        break;
                    }
            }



            return flag;
        }

        // Method to get The List of The Exception of The apllication
        public DataTable GetApplicationExceptionList()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetApplicationException");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetApplicationExceptionList in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];

        }

        // Method To Get The Application error List
        public DataTable GetApplicationErrorList()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetApplicationError");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetApplicationErrorList in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return _dataSet.Tables[0];
        }

        // Method to Delate the Application error List
        public int DeleteApplicationError(int errorId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@ErrorId ", errorId);
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_DeleteApplicationError");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DeleteApplicationError in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }


        // Method to delete The Application Exception 
        public int DeleteApplicationException(int exceptionId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@ExceptionId", exceptionId);
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_DeleteApplicationException");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DeleteApplicationException in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

        // Method to Insert the Click type 
        public int InsertCollegePageClick(int collegeCourseId)
        {
            return InsertPageClick(collegeCourseId, "COL");
        }

        public int InsertExamPageClick(int examId)
        {
            return InsertPageClick(examId, "EXAM");
        }

        public int InsertNewsPageClickType(int newsId)
        {
            return InsertPageClick(newsId, "NEWS");
        }

        public int InsertNoticePageClick(int noticeId)
        {
            return InsertPageClick(noticeId, "NOTICE");
        }

        private int InsertPageClick(int clickTypeId, string clickType)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@ClickType", clickType);
                _objDataWrapper.AddParameter("@ClickTypeId", clickTypeId);
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertPageClickType");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertPageClick in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

        // Method to get  the Click type 
        public int GetCollegePageClick(int collegeCourseId)
        {
            return GetPageClick(collegeCourseId, "COL");
        }

        public int GetExamPageClick(int examId)
        {
            return GetPageClick(examId, "EXAM");
        }

        public int GetNewsPageClickType(int newsId)
        {
            return GetPageClick(newsId, "NEWS");
        }

        public int GetNoticePageClick(int noticeId)
        {
            return GetPageClick(noticeId, "NOTICE");
        }

        public int GetTotalPageClick()
        {
            return GetPageClick(0, "");
        }

        private int GetPageClick(int clickTypeId, string clickType)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var clickCount = 0;
            try
            {
                _objDataWrapper.AddParameter("@ClickType", clickType);
                _objDataWrapper.AddParameter("@ClickTypeId", clickTypeId);

                var objClickCount =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ClickCount", "", SqlDbType.Int, ParameterDirection.Output));
                int i =
                    _objDataWrapper.ExecuteNonQuery("Aj_Proc_GetPageClick");
                if (objClickCount != null && objClickCount.Value != null)
                    clickCount = Convert.ToInt32(objClickCount.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetPageClick in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return clickCount;
        }




        public int InsertUpdateFactor(string factorName, out string errMag, int createdBy, int factorId = 0)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMag = "";
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@FactorId", factorId);
                _objDataWrapper.AddParameter("@FactorName", factorName);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objErrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateFactor");
                if (objErrMsg != null && objErrMsg.Value != null)
                {
                    errMag = Convert.ToString(objErrMsg.Value);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateFactor in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return i;
        }


        public DataTable GetFactor(int factorId = 0)
        {
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _objDataWrapper.AddParameter("@FactorId", factorId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetFactor");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetFactor in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];

        }

        public string CourseName
        {
            get { return Convert.ToString(HttpContext.Current.Session["CourseName"]); }
            set { HttpContext.Current.Session["CourseName"] = value; }
        }

        public int CourseId
        {
            get { return Convert.ToInt32(HttpContext.Current.Session["CourseId"]); }
            set { HttpContext.Current.Session["CourseId"] = value; }
        }

        public int CityId
        {
            get { return Convert.ToInt32(HttpContext.Current.Session["CityId"]); }
            set { HttpContext.Current.Session["CityId"] = value; }
        }

        public int StateId
        {
            get { return Convert.ToInt32(HttpContext.Current.Session["StateId"]); }
            set { HttpContext.Current.Session["StateId"] = value; }
        }

        public int ExamId
        {
            get { return Convert.ToInt32(HttpContext.Current.Session["ExamId"]); }
            set { HttpContext.Current.Session["ExamId"] = value; }
        }

        public int UniversityId
        {
            get { return Convert.ToInt32(HttpContext.Current.Session["UniversityId"]); }
            set { HttpContext.Current.Session["UniversityId"] = value; }
        }


        public DataSet GetCollegeNameList(int courseId = 0,string flag=null, bool sponserStatus = false)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CourseId", courseId);
                _objDataWrapper.AddParameter("@SponserStatus", sponserStatus);
                _objDataWrapper.AddParameter("@Flag", flag);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeNameList");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeNameList in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        public DataSet GetCollegeNameListbyParticipated(int filter = 0, int courseId = 0)
        {
            string filterstr = string.Empty;
            if (filter == 1)
                filterstr = "OnlineParticipated";
            else if (filter == 2)
                filterstr = "Sponsor";
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@Filter", filterstr);
                _objDataWrapper.AddParameter("@CourseId", courseId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeNameListFilterBySponser");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeNameListbyParticipated in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        public DataSet GetCollegeNameListbySponserCourseStateCity(int filter = 0, int courseid = 0, int stateid = 0,
                                                                  int cityid = 0)
        {
            string filterstr = string.Empty;
            if (filter == 1)
                filterstr = "OnlineParticipated";
            else if (filter == 2)
                filterstr = "Sponsor";
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@Filter", filterstr);
                _objDataWrapper.AddParameter("@CourseId", courseid);
                _objDataWrapper.AddParameter("@StateId", stateid);
                _objDataWrapper.AddParameter("@CityId", cityid);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeNameListFilterBySponser");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeNameListbyQuery in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        public DataTable GetCollegeTitleKeyWorDesc(int collegeBranchCourseId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CollegeBranchCourseId", collegeBranchCourseId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeKeyWrdDesc");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeTitleKeyWorDesc in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }

        #region "PageTitle"

        //Get All PageTitleDetails added by Abhishek
        public DataSet GetPageTitle()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetPageTitle");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetPageTitle in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        //Get All PageTitle by Id added by abhishek
        public DataSet GetPageTitleById(int PageId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@PageId", PageId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetPageTitle");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetPageTitleById in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }


        //Function for InsertUpdateAll PageTitleDetails added by abhishek
        public int InsertUpdatePageTitle(string PageName, string PageTitle, string PageKeyWord, string PageDesc,
                                         out string errMag, int createdBy, int PageId = 0)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMag = "";
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@PageId", PageId);
                _objDataWrapper.AddParameter("@PageName", PageName);
                _objDataWrapper.AddParameter("@PageTitle", PageTitle);
                _objDataWrapper.AddParameter("@PageKeyword", PageKeyWord);
                _objDataWrapper.AddParameter("@PageDescription", PageDesc);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objErrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdatePageTitle");
                if (objErrMsg != null && objErrMsg.Value != null)
                {
                    errMag = Convert.ToString(objErrMsg.Value);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdatePageTitle in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return i;
        }




        #endregion




        public DataTable GetPageTitleKeyWordAndDecsription(string optionName)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@OptionName", optionName);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetPageTitle");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetPageTitleKeyWordAndDecsription in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];

        }


        public int UploadCollegeImage(string collegeName, string ImageName)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@CollegeName", collegeName.Replace("`", "'"));
                _objDataWrapper.AddParameter("@ImageName", ImageName);
                i = _objDataWrapper.ExecuteNonQuery("temp_Proc");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UploadCollegeImage in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }


        //method to Insert the User Request on donation 

        public int InsertUserDonationRequest(string userName, string userMobile, string userEmailId,
                                             string userCollegeName,
                                             int userCourseId, string accuasedName, string accusedEmailId,
                                             string accusedMobile, int userCategory, string userStory, out string errMsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var _objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            errMsg = "";
            int i = 0;
            try
            {
                string pwd = _objCrypto.Encrypt(userMobile);
                _objDataWrapper.AddParameter("@UserEmailId", userEmailId);
                _objDataWrapper.AddParameter("@UserName", userName);
                _objDataWrapper.AddParameter("@UserMobile", userMobile);
                _objDataWrapper.AddParameter("@UserCourse", userCourseId);
                _objDataWrapper.AddParameter("@CollegeName", userCollegeName);
                _objDataWrapper.AddParameter("@AccusedName", accuasedName);
                _objDataWrapper.AddParameter("@AccusedMobile", accusedMobile);
                _objDataWrapper.AddParameter("@AccusedEmailId", accusedEmailId);
                _objDataWrapper.AddParameter("@UserStory", userStory);
                _objDataWrapper.AddParameter("@UserCategory", userCategory);
                _objDataWrapper.AddParameter("@UserPassword", _objCrypto.Encrypt(userMobile));
                var ObjerrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 256));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUser_DonationReport");
                if (ObjerrMsg != null && ObjerrMsg.Value != null)
                    errMsg = Convert.ToString(ObjerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUserDonationRequest in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

        // method to get The Course List Having The stream

        public DataTable GetCourseListHavingStream()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();

            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCourseListHavingStream");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCourseListHavingStream in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }

        public DataTable GetHeader(string userId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@AccessLevel", userId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetHeader");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetHeader in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }

        public DataTable GetCourseListForOnlineConsulling()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCourseLisForOnLineConsulling");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCourseListHavingStream in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }

        public DataSet GetInsertionCountByAdmin(int userId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetTotalCount");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetInsertionCountByAdmin in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        public int InsertUserCoomment(int userId, string commentType, string commentTypeId, string comment,
                                      out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            errmsg = "";
            var i = 0;
            try
            {
                _objDataWrapper.AddParameter("@CommentType", commentType);
                _objDataWrapper.AddParameter("@CommentTypeId", commentTypeId);
                _objDataWrapper.AddParameter("@Comment", comment);
                _objDataWrapper.AddParameter("@UserId", userId);

                var objerrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 256));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InserUserComment");
                if (objerrMsg != null && objerrMsg.Value != null)
                    errmsg = Convert.ToString(objerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUserCoomment in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

        public DataSet GetUserComment(string commentType, string commentTypeId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CommentType", commentType);
                _objDataWrapper.AddParameter("@CommentTypeId", commentTypeId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUserComment");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUserComment in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        public DataSet GetUserCommentByCommentType(string commentType)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CommentType", commentType);

                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUserComment");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUserCommentByCommentType in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        public int UpdateCommentStatus(int commentId, bool status, int userId, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            errmsg = "";
            var i = 0;
            try
            {
                _objDataWrapper.AddParameter("@CommentId", commentId);
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@CommentStatus", status);
                var objerrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 256));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InserUserComment");
                if (objerrMsg != null && objerrMsg.Value != null)
                    errmsg = Convert.ToString(objerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateCommentStatus in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

        public DataSet GetBanner()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {


                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetBanner");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetBanner in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        public int InsertBanner(int userId, int courseId, string collegeCourseName, string bannerImage,
                                string bannerToolTip,
                                string bannerUrl, int priority, int bannerPosition, DateTime bannerSratsDate,
                                DateTime bannerEndDate, bool bannerStatus,
                                out string errmsg, int bannerId = 0, int CollegeBranchCourseId = 0)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            errmsg = "";
            var i = 0;
            try
            {
                _objDataWrapper.AddParameter("@BannerId", bannerId);
                _objDataWrapper.AddParameter("@CollegeName", collegeCourseName);
                _objDataWrapper.AddParameter("@CourseId", courseId);
                _objDataWrapper.AddParameter("@BannerImage", bannerImage);
                _objDataWrapper.AddParameter("@Tooltip", bannerToolTip);
                _objDataWrapper.AddParameter("@BannerUrl", bannerUrl);
                _objDataWrapper.AddParameter("@PriorityId", priority);
                _objDataWrapper.AddParameter("@BannerPosition", bannerPosition);
                _objDataWrapper.AddParameter("@CreatedBy", userId);
                _objDataWrapper.AddParameter("@BannerStartDate", bannerSratsDate);
                _objDataWrapper.AddParameter("@BannerEndDate", bannerEndDate);
                _objDataWrapper.AddParameter("@BannerStaus", bannerStatus);

                var objerrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 256));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateBanner");
                if (objerrMsg != null && objerrMsg.Value != null)
                    errmsg = Convert.ToString(objerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertBanner in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

        public DataSet GetBannerById(int courseId = 0, int bannerId = 0, int adsType = 0, string collegeName = null)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CourseId", courseId);
                _objDataWrapper.AddParameter("@adsTypeid", adsType);
                _objDataWrapper.AddParameter("@BannerId", bannerId);
                _objDataWrapper.AddParameter("@collegeName", collegeName);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeBanner");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetBannerById in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }


        // Method to Get The List Of the College Contact Person Details
        public DataTable GetCollegeContactPersonDetailsByCollegeBrnachCourseId(int collegeBranchCourseId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CollegeBranchCourseId", collegeBranchCourseId);

                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeContactPersonDetails");


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeContactPersonDetails in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }

        public DataTable GetCollegeContactPersonDetailsByCollegeBrnachId(int collegeBranchId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CollegeBranchId", collegeBranchId);

                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeContactPersonDetails");


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeContactPersonDetails in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }

        public DataTable GetCollegeContactPersonDetailsByPersonId(int contactPersonId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CollegeContactPersonDetailsId", contactPersonId);

                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeContactPersonDetails");


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeContactPersonDetails in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }

        public int InsertUserRating(int userId, string ratingType, int ratingTypeId, int rate1, int rate2, int rate3,
                                    int rate4, int rate5, out string errMsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMsg = "";
            int i = 0;

            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@RatingType", ratingType);
                _objDataWrapper.AddParameter("@RatingTypeId", ratingTypeId);
                _objDataWrapper.AddParameter("@Rate1", rate1);
                _objDataWrapper.AddParameter("@Rate2", rate2);
                _objDataWrapper.AddParameter("@Rate3", rate3);
                _objDataWrapper.AddParameter("@Rate4", rate4);
                _objDataWrapper.AddParameter("@Rate5", rate5);
                SqlParameter objErrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUserRating");
                if (objErrMsg != null && objErrMsg.Value != null)
                    errMsg = Convert.ToString(objErrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing GetCollegeContactPersonDetailsByPersonId in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }




        // Method to Get the User Rating Details
        public DataTable GetUserRating(string ratingType, int ratingTypeId)
        {
            var ds = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _objDataWrapper.AddParameter("@RatingType", ratingType);
                _objDataWrapper.AddParameter("ratingTypeId", ratingTypeId);
                ds = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUserRating");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeContactPersonDetails in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return ds.Tables[0];
        }


        public DataTable GetAbuseList()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var ds = new DataSet();
            try
            {
                ds = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetAbuseList");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAbuseList in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return ds.Tables[0];
        }


        public int InsertAbuseReport(int userId, int abuseReportTypeId, string abuseText, string abuseType,
                                     int abuseTypeId, out string errMsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMsg = "";
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@AbuseReportText", abuseText);
                _objDataWrapper.AddParameter("@AbuseTypeId", abuseTypeId);
                _objDataWrapper.AddParameter("@AbuseType", abuseType);
                _objDataWrapper.AddParameter("@AbuseReportTypeId", abuseReportTypeId);
                SqlParameter objErrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUserReportAbuse");
                if (objErrMsg != null && objErrMsg.Value != null)
                    errMsg = Convert.ToString(objErrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertAbuseReport in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;

        }

        public int InsertUpdateCollegeContactDetails(int collegeId, int collegeCourseId, int groupId, int userId,
                                                     string personName, string designation, string mobile,
                                                     string phone, string email, string fax, string department,
                                                     bool status, out string errmsg, int contactPersonId = 0)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            errmsg = "";
            var i = 0;
            try
            {
                _objDataWrapper.AddParameter("@CollegeId", collegeId);
                _objDataWrapper.AddParameter("@CollegeCourseId", collegeCourseId);
                _objDataWrapper.AddParameter("@ContactPersonId", contactPersonId);
                _objDataWrapper.AddParameter("@CollegeGroupId", groupId);
                _objDataWrapper.AddParameter("@CollegePersonName", personName);
                _objDataWrapper.AddParameter("@ContactDesignation", designation);
                _objDataWrapper.AddParameter("@Mobile", mobile);
                _objDataWrapper.AddParameter("@Phone", phone);
                _objDataWrapper.AddParameter("@Email", email);
                _objDataWrapper.AddParameter("@Fax", fax);
                _objDataWrapper.AddParameter("@Department", department);
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@Status", status);


                var objerrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 256));
                i = _objDataWrapper.ExecuteNonQuery("AJ_Proc_InsertUpdateCollegeContactDetails");
                if (objerrMsg != null && objerrMsg.Value != null)
                    errmsg = Convert.ToString(objerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateCollegeContactDetails in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }


        // Method to Get The List of the College for Report donation
        public DataTable GetReportDonationCollegeList(int courseId = 0, int CollegeBranchCourseId = 0)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@CourseId", courseId);
                _objDataWrapper.AddParameter("@CollegeBranchCourseId", CollegeBranchCourseId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetReportDonationCollegeList");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetReportDonationCollegeList in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }

        // Method to Update the Report donation status 
        public int UpdateReportDonationStatus(out string errMsg, int collegeBranchCourseId = 0, int reportDonationId = 0)
        {
            int i = 0;
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMsg = "";

            try
            {
                _objDataWrapper.AddParameter("@CollegeBranchCourseId", collegeBranchCourseId);
                _objDataWrapper.AddParameter("@ReportDonationId", reportDonationId);
                var objErrMsg
                    =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_UpdateCollegeReportDonationStatus");
                if (objErrMsg != null && objErrMsg.Value != null)
                    errMsg = Convert.ToString(objErrMsg.Value);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateReportDonationStatus in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

        // Method to get Is donation repoted aginst the college
        public bool IsDonationRepoted(int collegeBranchCourseId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            bool status = false;
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CollegeBranchCourseId", collegeBranchCourseId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_CheckCollegeBlackList");
                if (_dataSet.Tables[0].Rows.Count > 0)
                    status = true;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing IsDonationRepoted in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return status;
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            //Datatable which contains unique records will be return as output.
            return dTable;
        }

        public DataSet GetCollegeByName(string collegeName)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objDataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CollegeName", collegeName);


                objDataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeByName");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeByName in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objDataset;

        }

        public int InsertUpdateCollegeTestimonal(int userId, int collegebranchId, string testimonial, out string errMsg,
                                                 bool testimonialStatus = false, int testimonialId = 0)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMsg = "";
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@CollegeBranchId", collegebranchId);
                _objDataWrapper.AddParameter("@Testimonial", testimonial);
                _objDataWrapper.AddParameter("@TestimonialStatus", testimonialStatus);
                _objDataWrapper.AddParameter("@TestimonialId", testimonialId);
                var objErrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateColegeTestimonialDetails");
                if (objErrMsg != null && objErrMsg.Value != null)
                    errMsg = Convert.ToString(objErrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateCollegeTestimonal in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;

        }

        public DataSet GetTestimonialDetails(int userId = 0, int collegebranchId = 0, int testimonialId = 0)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objDataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@AjUserId", userId);
                _objDataWrapper.AddParameter("@CollegeBranchId", collegebranchId);
                _objDataWrapper.AddParameter("@AjTestimonialId", testimonialId);
                objDataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeTestimonial");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetTestimonialDetails in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objDataset;

        }

        public DataSet GetCollegeRegistered(int collegebranchId = 0, string collegeName = null)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objDataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CollegeBranchId", collegebranchId);
                _objDataWrapper.AddParameter("@CollegeName", collegeName);


                objDataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNewCollegeRegistered");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeRegistered in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objDataset;

        }

        public int UpdateUserStatus(int userId, out string errMsg, int moderatedBy, bool userStatus = false)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMsg = "";
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@Moderatedby", moderatedBy);
                _objDataWrapper.AddParameter("@UserStatus", userStatus);
                var objErrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
                i = _objDataWrapper.ExecuteNonQuery("Aj_proc_UpdateUserLoginStatus");
                if (objErrMsg != null && objErrMsg.Value != null)
                    errMsg = Convert.ToString(objErrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateUserStatus in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;

        }

        public int InsertUserIdAndCollegeId(int userId, int collegebranchId, out string errMsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMsg = "";
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@CollegeBranchId", collegebranchId);

                var objErrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUserIdAndCollegeId");
                if (objErrMsg != null && objErrMsg.Value != null)
                    errMsg = Convert.ToString(objErrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUserIdAndCollegeId in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;

        }

        /// <summary>
        /// Strips all illegal characters from the specified title.
        /// </summary>
        /// <param name="text">
        /// The text to strip.
        /// </param>
        /// <returns>
        /// The remove illegal characters.
        /// </returns>
        public static string RemoveIllegalCharactersBL(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            text = text.Replace(":", string.Empty);
            text = text.Replace("/", string.Empty);
            text = text.Replace("?", string.Empty);
            text = text.Replace("#", string.Empty);
            text = text.Replace("[", string.Empty);
            text = text.Replace("]", string.Empty);
            text = text.Replace("@", string.Empty);
            text = text.Replace("*", string.Empty);
            text = text.Replace(".", string.Empty);
            text = text.Replace(",", string.Empty);
            text = text.Replace("\"", string.Empty);
            text = text.Replace("&", string.Empty);
            text = text.Replace("`", string.Empty);
            text = text.Replace("'", string.Empty);
            text = text.Replace(" ", "-");
            text = RemoveDiacritics(text);
            text = RemoveExtraHyphen(text);

            return HttpUtility.HtmlEncode(text).Replace("%", string.Empty);
        }

        /// <summary>
        /// Removes the diacritics.
        /// </summary>
        /// <param name="text">
        /// The text to remove diacritics from.
        /// </param>
        /// <returns>
        /// The string with the diacritics removed.
        /// </returns>
        private static string RemoveDiacritics(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in
                normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
            {
                sb.Append(c);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Removes the extra hyphen.
        /// </summary>
        /// <param name="text">
        /// The text to remove the extra hyphen from.
        /// </param>
        /// <returns>
        /// The text with the extra hyphen removed.
        /// </returns>
        private static string RemoveExtraHyphen(string text)
        {
            if (text.Contains("--"))
            {
                text = text.Replace("--", "-");
                return RemoveExtraHyphen(text);
            }

            return text;
        }

        public static string RemoveIllegealFromCourseBL(string text)
        {

            text = text.Replace(" ", "-");
            text = text.Replace("/", "-");
            return RemoveExtraHyphen(text);


            return text;

        }

        public DataTable CheckCollegeRegisteration(string collegeName)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);

            var objDataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CollegeName", collegeName.Trim());

                objDataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_CheckCollegeRegisteration");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing CheckCollegeRegisteration in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objDataSet.Tables[0];

        }

        public DataTable GetCollegePlacementYears(int collegeCourseId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objDataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CollegebranchCourseId", collegeCourseId);


                objDataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetColegePlacementYears");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegePlacementYears in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objDataset.Tables[0];

        }

        public int InsertUpdateBookSeat(int bookSeatId, int courseId, string collegeName, string bookSeatPayment,
                                        bool bookSeatStatus, int createdBy, out string errMsg, string instruction,
                                        DateTime bookSeatStartDate, DateTime bookSeatEndDate, string eligiblity10 = null,
                                        string eligiblity12 = null, string eligiblity15 = null)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMsg = "";
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@BookSeatId", bookSeatId);
                _objDataWrapper.AddParameter("@CollegeName", collegeName);
                _objDataWrapper.AddParameter("@CourseId", courseId);
                _objDataWrapper.AddParameter("@BookSeatPayment", bookSeatPayment);
                _objDataWrapper.AddParameter("@BookSeatStatus", bookSeatStatus);
                _objDataWrapper.AddParameter("@Created", createdBy);
                _objDataWrapper.AddParameter("@Instruction", instruction);
                _objDataWrapper.AddParameter("@10Eligiblity", eligiblity10);
                _objDataWrapper.AddParameter("@12Eligiblity", eligiblity12);
                _objDataWrapper.AddParameter("@15Eligiblity", eligiblity15);
                _objDataWrapper.AddParameter("@BookSeatStartDate", bookSeatStartDate);
                _objDataWrapper.AddParameter("@BookSeatEndDate", bookSeatEndDate);
                var objErrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
                i = _objDataWrapper.ExecuteNonQuery("AjProc_InsertUpdateBookSeat");
                if (objErrMsg != null && objErrMsg.Value != null)
                    errMsg = Convert.ToString(objErrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateBookSeat in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;

        }

        public DataTable CheckCollegeBookSeatStatus(int branchCourseId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);

            var objDataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@BranchCourseId", branchCourseId);
                objDataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_CheckCollegeBookSeatStatus");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing CheckCollegeBookSeatStatus in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objDataSet.Tables[0];

        }

        public int InsertUpdateBookSeatStatus(int bracnhCourseId, int userId, out string errMsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMsg = "";
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@CollegeBranchCourseId", bracnhCourseId);
                _objDataWrapper.AddParameter("@Created", userId);
                var objErrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertBookSeatStatus");
                if (objErrMsg != null && objErrMsg.Value != null)
                    errMsg = Convert.ToString(objErrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateBookSeatStatus in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;

        }

        public DataSet CheckUserBookSeatStatus(int userId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);

            var objDataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);

                objDataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_CheckUserBookSeatStatus");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing CheckUserBookSeatStatus in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objDataSet;

        }

        public DataSet GetBookedSeatStudent(int collegeBranchCourseId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);

            var objDataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CollegeBranchCourseId", collegeBranchCourseId);

                objDataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetBookedSeatStudent");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetBookedSeatStudent in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objDataSet;

        }


        public string GetXids(string UserIds)
        {
            string xids = "";
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _objDataWrapper.AddParameter("CustomerID", UserIds);
            var objErrMsg =
                (SqlParameter)
                (_objDataWrapper.AddParameter("@xids", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
            int i = _objDataWrapper.ExecuteNonQuery("sp_GetXidsByCustomerId");
            xids = Convert.ToString(objErrMsg.Value);
            return xids;

        }

        public DataSet GetparticipateCollegeList(string dbQuery)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(dbQuery))
                    _objDataWrapper.AddParameter("@DbQuery", dbQuery);
                _dataSet = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeListByDynamicQuery");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeNameList in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        //method to get sponsered or non sponsered college by course or without course by indu kumar pandey on 17/07/2013...
        public DataSet GetCollegeListBySponserStatus(int courseId = 0, bool sponserStatus = false)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CourseId", courseId);
                _objDataWrapper.AddParameter("@ParticpatingStatus", sponserStatus);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeNameListByStatus");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeListBySponserStatus in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        //method to get sponsered or non sponsered college by course or without course by indu kumar pandey on 17/07/2013...
        public DataSet GetCollegeListByOnilneStatus(int courseId = 0, bool participation = false)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CourseId", courseId);
                _objDataWrapper.AddParameter("@ParticpatingStatus", participation);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeNameListParticipationStatus");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeListByOnilneStatus in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }


        //Method to insert The college Contact Person details

        public int InsertCollegeContactPersonDetails(int userId, string contactPersonName,
                                                     string contactPersonDesgination, string contactPersonMobile,
                                                     string contactPersonEmailId, int collegeBranchId = 0,
                                                     int collegeBranchCourseId = 0)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@collegeBranchId", collegeBranchId);
                _objDataWrapper.AddParameter("@collegeBranchCourseId", collegeBranchCourseId);
                _objDataWrapper.AddParameter("@userId", userId);
                _objDataWrapper.AddParameter("@ContactPersonName", contactPersonName);
                _objDataWrapper.AddParameter("@ContactPersonDesgination", contactPersonDesgination);
                _objDataWrapper.AddParameter("@contactPersonMobile", contactPersonMobile);
                _objDataWrapper.AddParameter("@contactPersonEmailId", contactPersonEmailId);
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertCollegeContact_Person");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertCollegeContactPersonDetails in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }


        // Method to Reply The Query

        public int InsertUserQueryReply(int userId, int queryId, string queryReply)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@QueryId", queryId);
                _objDataWrapper.AddParameter("@QueryReply", queryReply);
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_ReplyUserQuery");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUserQueryReply in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }


        // Method to Get The College Query

        public DataTable GetCollegeQuery(int collegeBranchCourseId, string iCase)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var ds = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@collegeBranchCourseId", collegeBranchCourseId);
                _objDataWrapper.AddParameter("@case", iCase);
                ds = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeRealtedQuery");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeQuery in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return ds.Tables.Count > 0 ? ds.Tables[0] : null;
        }


        // Method to Get The Reply Of the Query

        public DataTable GetQUeryReply(int queryId, bool replyStatus)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var ds = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@QueryId", queryId);
                _objDataWrapper.AddParameter("@ReplyStatus", replyStatus);
                ds = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetQueryReply");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetQUeryReply in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return ds.Tables.Count > 0 ? ds.Tables[0] : null;
        }


        // Method to Get The List Of The College Image Gallaery
        public DataTable GetCollegeImageGallery()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var ds = new DataSet();
            try
            {

                ds = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeGallery");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeImageGallery in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return ds.Tables.Count > 0 ? ds.Tables[0] : null;
        }


        // method to Get The College Contact persond details by User id

        public DataTable GetCollegeContactPerson(int userId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var ds = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@userId", userId);
                ds = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeContactPerson");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeContactPerson in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return ds.Tables.Count > 0 ? ds.Tables[0] : null;
        }


        // Method to Get The Reply Of the Query

        private DataTable GetAllQueryReply(int queryId, string iCase)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var ds = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@QueryId", queryId);
                _objDataWrapper.AddParameter("@case", iCase);
                ds = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetQueryReply");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetQUeryReply in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return ds.Tables.Count > 0 ? ds.Tables[0] : null;
        }

        public List<ReplyProperty> GetReply(int queryId, string iCase)
        {
            var datatable = GetAllQueryReply(queryId, iCase);
            List<ReplyProperty> listQueryProperty = new List<ReplyProperty>();
            if (datatable.Rows.Count > 0)
            {
                for (int i = 0; i < datatable.Rows.Count; i++)
                {
                    var objQueryProperty = new ReplyProperty
                        {
                            QueryId = Convert.ToInt32(datatable.Rows[i]["AjQueryId"]),
                            QueryReply = Convert.ToString(datatable.Rows[i]["AjQueryReply"]),
                            ReplyUserName = Convert.ToString(datatable.Rows[i]["AjUserFullName"]),
                            ReplyUserEmailId = Convert.ToString(datatable.Rows[i]["AjUserEmail"]),
                            QueryReplyId = Convert.ToString(datatable.Rows[i]["AjQueryReplyId"]),
                            ReplyBy = Convert.ToInt32(datatable.Rows[i]["AjUserId"]),
                            ReplyStatus = Convert.ToBoolean(datatable.Rows[i]["QueryReplyStatus"]),

                        };
                    listQueryProperty.Add(objQueryProperty);

                }
            }
            return listQueryProperty;
        }



        // Method to moderate the reply of the query

        public string ModerateReply(int replyId, bool replyStatus, int replyby)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var errMsg = "";
            try
            {
                _objDataWrapper.AddParameter("@replyId", replyId);
                _objDataWrapper.AddParameter("@modifiedBy", replyby);
                _objDataWrapper.AddParameter("@reqplyStatu", replyStatus);
                var objErrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@errMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
                int i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_UpdateReplyStatus");
                if (objErrMsg != null && objErrMsg.Value != null)
                {
                    errMsg = Convert.ToString(objErrMsg.Value);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing ModerateReply in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;
        }


        // Method to get the College spnsoer details

        public DataTable GetCollegeSponser(string collegeName = null, int sponserType = 0, int courseId = 0,
                                           int collegeBranchCourseId = 0)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var ds = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@collegename", collegeName);
                _objDataWrapper.AddParameter("@sponserTypeId", sponserType);
                _objDataWrapper.AddParameter("@courseId", courseId);
                _objDataWrapper.AddParameter("@collegebranchcourseId", collegeBranchCourseId);
                ds = _objDataWrapper.ExecuteDataSet("AJ_Proc_GetCollegeAdvstList");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeSponser in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return ds.Tables.Count > 0 ? ds.Tables[0] : null;
        }

        public DataSet GetCollegeList(int courseId = 0)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CourseId", courseId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeListByCourseForUrlRewrite");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeList in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        public int InsertAdsProduct(string productName, string description, int amount,
                                    int priority, bool displayStatus,
                                    string bannerIds, string associationIds, int createdBy, out string errmsg,
                                    out int productId,bool isBestvalue  )
        {
            productId = 0;
            int i = 0;
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@ProductName", productName);
                _objDataWrapper.AddParameter("@ProductDescription", description);
                _objDataWrapper.AddParameter("@ProductAmount", amount);
                _objDataWrapper.AddParameter("@Productpriority", priority);
                _objDataWrapper.AddParameter("@ProductStatus", displayStatus);
                _objDataWrapper.AddParameter("@ProductBannerId", bannerIds);
                _objDataWrapper.AddParameter("@ProductAssociationId", associationIds);
                _objDataWrapper.AddParameter("@IsBestvalue", isBestvalue);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objProductId =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ProductMasterId", 0, SqlDbType.Int, ParameterDirection.InputOutput));
                var objErrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateAdsProduct");
                if (objProductId != null && objProductId.Value != null)
                    productId = Convert.ToInt32(objProductId.Value);
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
                const string addInfo = "Error while executing InsertAdsProduct in College.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

        public int UpdateAdsProduct(int productMasterId, string productName, string description,
                                    int amount, int priority, bool displayStatus,
                                    string bannerIds, string associationIds, int createdBy, out string errmsg,
                                    out int productId, bool isBestvalue)
        {
            productId = 0;
            int i = 0;
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {

                _objDataWrapper.AddParameter("@ProductName", productName);
                _objDataWrapper.AddParameter("@ProductDescription", description);
                _objDataWrapper.AddParameter("@ProductAmount", amount);
                _objDataWrapper.AddParameter("@Productpriority", priority);
                _objDataWrapper.AddParameter("@ProductStatus", displayStatus);
                _objDataWrapper.AddParameter("@ProductBannerId", bannerIds);
                _objDataWrapper.AddParameter("@ProductAssociationId", associationIds);
                _objDataWrapper.AddParameter("@IsBestvalue", isBestvalue);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objProductId =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ProductMasterId", productMasterId, SqlDbType.Int,
                                                  ParameterDirection.InputOutput));
                var objErrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateAdsProduct");
                if (objProductId != null && objProductId.Value != null)
                    productId = Convert.ToInt32(objProductId.Value);
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
                const string addInfo = "Error while executing UpdateAdsProduct in College.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

        public DataSet GetProductAds()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                //_objDataWrapper.AddParameter("@CourseId", courseId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetProductAds");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetProductAds in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        public DataSet GetProductCategoryByProductId(int productId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@ProductId", productId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetProductCategory");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetProductCategoryByProductId in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        public DataTable GetProductAdsByProductId(int productMasterId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@ProductMasterId", productMasterId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetProductAds");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetProductAdsByProductId in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }

        public DataSet GetProductAdsByProductName(string productName)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@ProductName", productName);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetProductAds");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetProductAdsByProductId in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        public bool CheckModerateReply(int replyId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var errMsg = false;
            try
            {
                _objDataWrapper.AddParameter("@replyId", replyId);

                var objErrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@moderate", "", SqlDbType.Bit, ParameterDirection.Output));
                int i = _objDataWrapper.ExecuteNonQuery("Aj_QueryreplyModerate");
                if (objErrMsg != null && objErrMsg.Value != null)
                {
                    errMsg = Convert.ToBoolean(objErrMsg.Value);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing ModerateReply in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;
        }
        public string CheckModerateComment(int commentId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var errMsg = "";
            try
            {
                _objDataWrapper.AddParameter("@commentId", commentId);

                var objErrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@moderate", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
                int i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_CheckCommentModerate");
                if (objErrMsg != null && objErrMsg.Value != null)
                {
                    errMsg = Convert.ToString(objErrMsg.Value);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing ModerateReply in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;
        }
        public string CheckModerateCollegeUser(int collegeUserId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var errMsg = "";
            try
            {
                _objDataWrapper.AddParameter("@userId", collegeUserId);

                var objErrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@moderate", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
                int i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_GetModerateCollege");
                if (objErrMsg != null && objErrMsg.Value != null)
                {
                    errMsg = Convert.ToString(objErrMsg.Value);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing ModerateReply in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;
        }

        public int DeleteProductFeatures(int productId)
        {
            int i = 0;
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {

                _objDataWrapper.AddParameter("@ProductId", productId);

                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_DeleteProductFeatures");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DeleteProductFeatures in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

        public int InsertProductFeatures(string productFeatures, int productId, int createdBy)
        {
            int i = 0;
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {

                _objDataWrapper.AddParameter("@ProductId", productId);
                _objDataWrapper.AddParameter("@ProductFeatures", productFeatures);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertProductFeatures");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertProductFeatures in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

        public DataSet GetProductFeatures(int productId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@ProductId", productId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetProductFeatures");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetProductFeatures in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        public void AdminUserNotification(out string collegeQuery, out string examQuery, out string loanQuery,
                                          out string commonQuery, out string replyModerate, out string collegeComment,
                                          out string examComment, out string newsComment, out string loanComment,
                                          out string collegeUser)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            collegeQuery = "";
            examQuery = "";
            loanQuery = "";
            commonQuery = "";
            replyModerate = "";
            collegeComment = "";
            examComment = "";
            newsComment = "";
            loanComment = "";
            collegeUser = "";
            try
            {
                var objCollegeQuery =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@collegeQueryNotModerateYet", "", SqlDbType.Int,
                                                  ParameterDirection.Output));
                var objExamQuery =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ExamQueryNotModerateYet", "", SqlDbType.Int,
                                                  ParameterDirection.Output));
                var objLoanQuery =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@LoanQueryNotModerateYet", "", SqlDbType.Int,
                                                  ParameterDirection.Output));
                var objCommonQuery =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@CommonQueryNotModerateYet", "", SqlDbType.Int,
                                                  ParameterDirection.Output));
                var objQueryReply =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ReplyNotModerateYet", "", SqlDbType.Int, ParameterDirection.Output));
                var objCollegeComment =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@CollegeCommentNotModerateYet", "", SqlDbType.Int,
                                                  ParameterDirection.Output));
                var objExamComment =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ExamCommentNotModerateYet", "", SqlDbType.Int,
                                                  ParameterDirection.Output));
                var objNewsComment =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@NewsCommentNotModerateYet", "", SqlDbType.Int,
                                                  ParameterDirection.Output));
                var objLoanComment =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@LoanCommentNotModerateYet", "", SqlDbType.Int,
                                                  ParameterDirection.Output));
                var objCollegeRegistation =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@CollegeNewregistationNotModerateyet", "", SqlDbType.Int,
                                                  ParameterDirection.Output));

                var i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_AdminUserNotification");

                if (objCollegeQuery != null && objCollegeQuery.Value != null)
                {
                    collegeQuery = Convert.ToString(objCollegeQuery.Value);
                }
                if (objExamQuery != null && objExamQuery.Value != null)
                {
                    examQuery = Convert.ToString(objExamQuery.Value);
                }
                if (objLoanQuery != null && objLoanQuery.Value != null)
                {
                    loanQuery = Convert.ToString(objLoanQuery.Value);
                }
                if (objCommonQuery != null && objCommonQuery.Value != null)
                {
                    commonQuery = Convert.ToString(objCommonQuery.Value);
                }
                if (objQueryReply != null && objQueryReply.Value != null)
                {
                    replyModerate = Convert.ToString(objQueryReply.Value);
                }
                if (objCollegeComment != null && objCollegeComment.Value != null)
                {
                    collegeComment = Convert.ToString(objCollegeComment.Value);
                }
                if (objExamComment != null && objExamComment.Value != null)
                {
                    examComment = Convert.ToString(objExamComment.Value);
                }
                if (objNewsComment != null && objNewsComment.Value != null)
                {
                    newsComment = Convert.ToString(objNewsComment.Value);
                }
                if (objLoanComment != null && objLoanComment.Value != null)
                {
                    loanComment = Convert.ToString(objLoanComment.Value);
                }
                if (objCollegeRegistation != null && objCollegeRegistation.Value != null)
                {
                    collegeUser = Convert.ToString(objCollegeRegistation.Value);
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing AdminUserNotification in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        // Method to get the Query Details
        public DataTable GetQueryDetails(int queryId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@queryId", queryId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetQueryDetails");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetQueryDetails in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }
        // Method to get the Query Details
        public DataTable GetCollegeuserDetails(int userid)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@userId", userid);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeUserDetails");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetQueryDetails in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }


        #region CollegeProduct
        public DataSet GetProductForCollege(int collegeId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CollgeId", collegeId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetAdsProductForCollege");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetProductForCollege in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }
        public DataSet GetCourseForProduct(int advertismentDiscountId, int collegeId, int advertismentType)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@AdvertismentDiscountId", advertismentDiscountId);
                _objDataWrapper.AddParameter("@collegeId", collegeId);
                _objDataWrapper.AddParameter("@AdvertismentTypeId", advertismentType);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCoursesForProduct");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCourseForProduct in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }
        public DataSet GetProductForCart(int userId, string orderId, int paymentId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@OrderId", orderId);
                _objDataWrapper.AddParameter("@PaymentId", paymentId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetProductForCart");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetProductForCart in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }
        public int InsertProductForCart(int advertisementDiscountId, string collegeCourseIds, int advertisementTypeId, int createdBy)
        {
            int i = 0;
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
             
                _objDataWrapper.AddParameter("@AdvertisementDiscountId", advertisementDiscountId);
                _objDataWrapper.AddParameter("@CollegeCourseIds", collegeCourseIds);
                _objDataWrapper.AddParameter("@AdvertismentTypeId", advertisementTypeId);
                _objDataWrapper.AddParameter("@UserId", createdBy);
                _objDataWrapper.AddParameter("@CourseCount", collegeCourseIds.Split(',').Length);
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCartProduct");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertProductForCart in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }
        public int DeleteProductFromCart(int productPaymentId, int collegeCourseId)
        {
            int i = 0;
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {

                _objDataWrapper.AddParameter("@ProductPaymentId", productPaymentId);
                _objDataWrapper.AddParameter("@CollegeCourseId", collegeCourseId);
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_DeleteProductFormCart");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DeleteProductFromCart in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }

        public DataSet GetBannerAdsProduct(int collegeId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CollegeId", collegeId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeBannerProduct");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetBannerAdsProduct in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }
        public DataSet GetTextAdsProduct(int collegeId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CollegeId", collegeId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetTextAdsProduct");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetTextAdsProduct in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        public DataTable GetProductCount(int userId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetProductCount");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetProductCount in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }
        public int UpdateOrderIdForProduct(int userId, bool status, string orderNumber=null, string transactionMode = null,int paymentId=0)
        {
            var i = 0;
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {

                _objDataWrapper.AddParameter("@TransactionMode", transactionMode);
                _objDataWrapper.AddParameter("@Status", status);
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@ORDERID", orderNumber);
                _objDataWrapper.AddParameter("@PaymentId", paymentId);
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_UpdateProductOrderId");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateOrderIdForProduct in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }
        // Method To Get The banner
        public DataTable GetBannerListByCollegeId(int collegeId, string flag)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CollegeId", collegeId);
                _objDataWrapper.AddParameter("@Flag", flag);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_CheckBannerCountForCollege");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetBannerListByCollegeId in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return _dataSet.Tables[0];
        }
        public int InsertBannerbyCollegeUser(int collegeId, int userId, int courseId, string bannerImage,
                               string bannerToolTip,
                               string bannerUrl, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            var i = 0;
            try
            {

                _objDataWrapper.AddParameter("@CourseId", courseId);
                _objDataWrapper.AddParameter("@CollegeId", collegeId);
                _objDataWrapper.AddParameter("@BannerImage", bannerImage);
                _objDataWrapper.AddParameter("@BannerToolTip", bannerToolTip);
                _objDataWrapper.AddParameter("@BannerUrl", bannerUrl);
                _objDataWrapper.AddParameter("@UserId", userId);

                var objerrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ERRMSG", "", SqlDbType.VarChar, ParameterDirection.Output, 256));
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_UpdateBannerDataByCollege");
                if (objerrMsg != null && objerrMsg.Value != null)
                    errmsg = Convert.ToString(objerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertBannerbyCollegeUser in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }


        // Method to Get the Product Duration and Details

        public DataTable GetAdvertisementDiscountDetails(int advstType, int advstTypeId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@AdvstType", advstType);
                _objDataWrapper.AddParameter("@AdvstTypeId", advstTypeId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetAdvstDiscountDetails");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAdvertisementDiscountDetails in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return _dataSet.Tables[0];
        }

        // Method to Convert to data table to josn
        public string ConvertDataTabletoString(DataTable dt)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var rows =
                (from DataRow dr in dt.Rows
                 select dt.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col])).ToList();
            return serializer.Serialize(rows);
        }

        // method to Insert The Advertisiment Discount\
        public int InsertAdvstDiscount(int advstType, int advstTypeId, int advstDiscount, int advstMonthValue, DateTime advstValidityStart, DateTime advstValidityEndDate,
                                            bool advstDefaultSelection, bool advstStatus, out string errMsg)
        {

            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMsg = "";
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@AdvstType", advstType);
                _objDataWrapper.AddParameter("@AdvstTypeId", advstTypeId);
                _objDataWrapper.AddParameter("@AdvstDiscount", advstDiscount);
                _objDataWrapper.AddParameter("@AdvstMonthValue", advstMonthValue);
                _objDataWrapper.AddParameter("@AdvstValidityStart", advstValidityStart);
                _objDataWrapper.AddParameter("@AdvstValidityEnd", advstValidityEndDate);
                _objDataWrapper.AddParameter("@AdvstDefaultSelection", advstDefaultSelection);
                _objDataWrapper.AddParameter("@CreatedBy", new SecurePage().LoggedInUserId);
                _objDataWrapper.AddParameter("@AdvstDiscountStatus", advstStatus);
                var objerrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 256));
                i = _objDataWrapper.ExecuteNonQuery("Aj_proc_insertupdateadvstdiscount");

                if (objerrMsg != null && objerrMsg.Value != null)
                    errMsg = Convert.ToString(objerrMsg.Value);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertAdvstDiscount in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }
        // method to Insert The Advertisiment Discount\
        public int UpdateAdvstDiscount(int AdvstDiscountId, int advstType, int advstTypeId, int advstDiscount, int advstMonthValue, DateTime advstValidityStart, DateTime advstValidityEndDate,
                                            bool advstDefaultSelection, bool advstStatus, out string errMsg)
        {

            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMsg = "";
            int i = 0;
            try
            {
                _objDataWrapper.AddParameter("@AdvstDiscountId", AdvstDiscountId);
                _objDataWrapper.AddParameter("@AdvstType", advstType);
                _objDataWrapper.AddParameter("@AdvstTypeId", advstTypeId);
                _objDataWrapper.AddParameter("@AdvstDiscount", advstDiscount);
                _objDataWrapper.AddParameter("@AdvstMonthValue", advstMonthValue);
                _objDataWrapper.AddParameter("@AdvstValidityStart", advstValidityStart);
                _objDataWrapper.AddParameter("@AdvstValidityEnd", advstValidityEndDate);
                _objDataWrapper.AddParameter("@AdvstDefaultSelection", advstDefaultSelection);
                _objDataWrapper.AddParameter("@CreatedBy", new SecurePage().LoggedInUserId);
                _objDataWrapper.AddParameter("@AdvstDiscountStatus", advstStatus);
                var objerrMsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 256));
                i = _objDataWrapper.ExecuteNonQuery("Aj_proc_insertupdateadvstdiscount");

                if (objerrMsg != null && objerrMsg.Value != null)
                    errMsg = Convert.ToString(objerrMsg.Value);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateAdvstDiscount in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }


        // Method to Get The Advst Discount Details 
        public DataTable GetAdvstDiscountDetails(int advstType = 0, int advstDiscountTypeId = 0, int advstDiscountId = 0)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@AdvstType", advstType);
                _objDataWrapper.AddParameter("@AdvstTypeId", advstDiscountTypeId);
                _objDataWrapper.AddParameter("@AdvstDiscountTypeId", advstDiscountId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_proc_GetAdvstDiscount");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAdvstDiscountDetails in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }

        public DataTable CheckProductCountAfterPayment(int userId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_CheckProductCountAfterPayment");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetProductCount in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }

        public DataSet GetProductAfterPayment(int userId, int advertisementType)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@AdvertisementType", advertisementType);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetProductAfterPayment");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetProductAfterPayment in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }

        public DataSet GetProductDiscount(int productId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@ProductId", productId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetProductDiscount");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetProductDiscount in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }
        public DataSet GetBannerProductDiscount(int productId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@AdvertisementTypeId", productId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetBannerProductDiscount");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetBannerProductDiscount in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }
        public DataSet GetTextAdsProductDiscount(int productId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@AdvertisementTypeId", productId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetTextAdsProductDiscount");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetTextAdsProductDiscount in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }
        public DataSet GetCollegeBannerList(int collegeId=0, int courseId=0, int bannerId=0)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@CollegeId", collegeId);
                _objDataWrapper.AddParameter("@CourseId", courseId);
                _objDataWrapper.AddParameter("@BannerId", bannerId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCollegeBannerlIST");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCollegeBannerList in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet;
        }
        // Method To Get The banner
        public DataTable GetOurClientsCollege()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {

                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetOurSponseredColleges");

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetOurClientsCollege in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return _dataSet.Tables[0];
        }
        #endregion


        // Method to update the college logo
        public int UpdateCollegeLogo(int collegebranchId, string collegeLogo)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var i = 0;
            try
            {
                _objDataWrapper.AddParameter("@collegeBranchId", collegebranchId);
                _objDataWrapper.AddParameter("@imageName", collegeLogo);
                i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_UpdateCollegeLogo");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateCollegeLogo in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return i;
        }
        public DataTable GetPaymentedCourse(int paymentId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {

                _objDataWrapper.AddParameter("@PaymentId", paymentId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetPaymentedCourse");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetPaymentedCourse in Common.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];
        }
    }
}
