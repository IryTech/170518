using System.Configuration;
using System.Data;
using IryTech.AdmissionJankari.DAL;
using System.Collections;
using System;
namespace IryTech.AdmissionJankari.BL
{
    /// <summary>
    /// Summary description for ClsSingelton
    /// </summary>
    public sealed class ClsSingelton
    {
        /// <summary>
        /// Regular Expressions
        /// </summary>
        /// 

        //public variables that will store regular expression string
        public static string aRegExpPhone;
        public static string aRegExpExtn;
        public static string aRegExpFax;
        public static string aRegExpZip;
        public static string aRegExpMobile;
        public static string aRegExpEmail;
        public static string aRegExpEmailList;
        public static string aRegExpAlpha;
        public static string aRegExpAlphaAdd;//FOR ADDRESS FIELDS IT ACCEPT [,-,0-9,A-Z,a-z]
        public static string aRegExpAlphaNum;
        public static string aRegExpAlphaNumStrict;
        public static string aRegExpDecimal;
        public static string aRegExpAlphaNumSpaceStrict;
        public static string aRegExpInteger;
        public static string aRegExpIntegerSign;
        public static string aRegExpDouble;
        public static string aRegExpCurrency;
        public static string aRegExpDate;
        public static string aRegExpShortDate;
        public static string aRegExpCurrencyformat;
        public static string aRegExpWebSiteUrl;
        public static string aRegExpPhoneAll;
        public static string aRegExpImageFile;
        public static string aRegExpPassword;
        public static string aRegExpExcelUpload;
        public static string bccDirectAdmission;
        public static string bccSponserCollegeQuery;
        public static int PageButtonCount;
        public static int PageSize;
        public static string WorkingKey;
        static DataTable dtMasterValues;
        static Hashtable htGender;
        public static string AssociationId;
        public static string donationMailId;
        public static string queryReplyMailId;

        public static string CommentMailId;

        public static string CollegeRegisterationMailId;
        static readonly ClsSingelton objSingletonDataSet = new ClsSingelton();
        
        static ClsSingelton()
        {
           
            InitialiseRegularExpressions();
            LoadData();
        }

      

   

       
        private static void InitialiseRegularExpressions()
        {
            aRegExpPassword = ConfigurationManager.AppSettings["RegPassword"];
            aRegExpPhone = ConfigurationManager.AppSettings["RegExpPhone"];
            aRegExpZip = ConfigurationManager.AppSettings["RegExpZip"];
            aRegExpExtn = ConfigurationManager.AppSettings["RegExpExtn"];
            aRegExpFax = ConfigurationManager.AppSettings["RegExpPhone"];
            aRegExpMobile = ConfigurationManager.AppSettings["RegMobile"];
            aRegExpEmail = ConfigurationManager.AppSettings["RegExpEmail"];
            aRegExpEmailList = ConfigurationManager.AppSettings["RegExpEmailList"];
            aRegExpAlpha = ConfigurationManager.AppSettings["RegExpAlpha"];
            aRegExpAlphaAdd = ConfigurationManager.AppSettings["RegExpAlphaAdd"];
            aRegExpAlphaNum = ConfigurationManager.AppSettings["RegExpAlphaNum"];
            aRegExpAlphaNumStrict = ConfigurationManager.AppSettings["RegExpAlphaNumStrict"];
            aRegExpDecimal = ConfigurationManager.AppSettings["RegExpDecimal"];
            aRegExpAlphaNumSpaceStrict = ConfigurationManager.AppSettings["RegExpAlphaNumSpaceStrict"];
            aRegExpInteger = ConfigurationManager.AppSettings["RegExpInteger"];
            aRegExpIntegerSign = ConfigurationManager.AppSettings["RegExpIntegerSign"];
            aRegExpDouble = ConfigurationManager.AppSettings["RegExpDouble"];
            aRegExpCurrency = ConfigurationManager.AppSettings["RegExpCurrency"];
            aRegExpDate = ConfigurationManager.AppSettings["RegExpDate"];
            aRegExpShortDate = ConfigurationManager.AppSettings["RegExpShortDate"];
            aRegExpCurrencyformat = ConfigurationManager.AppSettings["RegExpCurrencyFormat"];
            aRegExpWebSiteUrl = ConfigurationManager.AppSettings["RegExpWebSiteUrl"];
            aRegExpPhoneAll = ConfigurationManager.AppSettings["RegExpPhoneAll"];
            aRegExpImageFile = ConfigurationManager.AppSettings["RegExpImageFile"];
            aRegExpExcelUpload = ConfigurationManager.AppSettings["RegExpexcel"];
            PageButtonCount= Convert.ToInt16(ConfigurationManager.AppSettings["AdminButton_Count"]);
            PageSize= Convert.ToInt16(ConfigurationManager.AppSettings["PAGE_SIZE"]);
            bccDirectAdmission = ConfigurationManager.AppSettings["BCCDirectAdmission"];
            bccSponserCollegeQuery = ConfigurationManager.AppSettings["BCCSponserCollege"];
            WorkingKey = ConfigurationManager.AppSettings["WorkingKey"];
            AssociationId = ConfigurationManager.AppSettings["HomeBasedAssociationId"];
            donationMailId = ConfigurationManager.AppSettings["DonationMailId"];
            CommentMailId = ConfigurationManager.AppSettings["CommentMailId"];
            CollegeRegisterationMailId = ConfigurationManager.AppSettings["CollegeRegisterationMailId"];
            queryReplyMailId = ConfigurationManager.AppSettings["QueryReply"];
        }

        public static Hashtable GetGenders()
        {
            return htGender;
        }

        // This Method to is used to get the mode of the Like Reguler ,Private amd many more like that 
        public static DataView GetMode()
        {
            var dv = new DataView(dtMasterValues) {RowFilter = "AjMasterTitle = 'Mode'", Sort = "AjMasterValues"};
            return dv;
        }

        public static string DirectAdmissionNote
        {
            get { return BindDirectAdmissionNote(); }
            
        }

        private static string BindDirectAdmissionNote()
        {
            var dv = new DataView(dtMasterValues) {RowFilter = "AjMasterTitle = 'DirectADMJ'", Sort = "AjMasterValues"};
            if (dv.Count > 0)
            {
              return   dv[0]["AjMasterValues"]as string;
            }
            else
            {
              return   "";
            }
        }
        // this method ti used to get the Managemnet type like Private or goverment like this 
        public static DataView GetManagement()
        {
            var dv = new DataView(dtMasterValues) {RowFilter = "AjMasterTitle = 'Mangt'", Sort = "AjMasterValues"};
            return dv;

        }
        private static void LoadData()
        {
            try
            {
                var objWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
                var dsTemp = objWrapper.ExecuteDataSet("Aj_Proc_GetMasterValues");
                dtMasterValues = dsTemp.Tables[0];
                htGender = new Hashtable {{"1", "Male"}, {"2", "Female"}, {"0", "Select"}};
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing LoadData in ClsSingelton  .cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
    }

}