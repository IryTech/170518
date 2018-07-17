using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.BL;
using System.IO;

namespace IryTech.AdmissionJankari20.Web
{
    public class Global : System.Web.HttpApplication
    {
        private System.ComponentModel.IContainer components = null;
        Common _ObjClsCommon = new Common();

        public Global()
        {
            InitializeComponent();
        }
        private void WriteLogFile(string str)
        {
            string strLogFile = Common.StrBaseDirectory + "Admissionjankari.log";
            TextWriter tw = new StreamWriter(strLogFile, true);
            tw.WriteLine("Step:- " + str + "\n\r");
            tw.Close();

        }
        void Application_BeginRequest(object source, EventArgs e)
        {
            var app = (HttpApplication)source;
            var context = app.Context;

            // Attempt to perform first request initialization
            FirstRequestInitialization.Initialize(context);
        }
        void Application_Start(object sender, EventArgs e)
        {
            Common.OleCnnString = System.Configuration.ConfigurationManager.AppSettings["OLE_CON_STRING"];
            Common.OleCnnString2007 = System.Configuration.ConfigurationManager.AppSettings["OLE_CON_STRING2007"]; 
            Common.CnnString = System.Configuration.ConfigurationManager.AppSettings["DB_CON_STRING"];
            Common.DefaultCulture = System.Configuration.ConfigurationManager.AppSettings["DefaultCulture"];

           _ObjClsCommon.AddAppLog("AdmissionJankari", "Application Started ", DateTime.Now);

        }

        void Application_End(object sender, EventArgs e)
        {
            try
            {
                string shutDownMessage = "";
                string shutDownStack = "";

                HttpRuntime runtime = (HttpRuntime)typeof(System.Web.HttpRuntime).InvokeMember("_theRuntime",
                    BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField,
                    null, null, null);
                if (runtime != null)
                {
                    shutDownMessage = shutDownMessage + "  ...  " + (string)runtime.GetType().InvokeMember("_shutDownMessage",
                        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField,
                        null, runtime, null);

                    shutDownStack = (string)runtime.GetType().InvokeMember("_shutDownStack",
                        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField,
                        null, runtime, null);
                }

                _ObjClsCommon.AddAppLog("Admissionjankari " + shutDownMessage, shutDownStack, DateTime.Now);

            }
            catch (Exception ex)
            {
                _ObjClsCommon.AddAppLog(ex.Message, ex.InnerException.Message, DateTime.Now);
            }

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            _ObjClsCommon.CourseId = ApplicationSettings.Instance.CourseId;
            var courseMasterProperty = CourseProvider.Instance.GetCourseById(_ObjClsCommon.CourseId).FirstOrDefault();
            if (courseMasterProperty != null)
                _ObjClsCommon.CourseName = Utils.RemoveIllegealFromCourse(courseMasterProperty.CourseName);

            //ApplicationSettings.Instance.CourseId;
            //SecurePage objSecurePage = new SecurePage();
            //objSecurePage.LoggedInUserId = 1;
        }

        void Session_End(object sender, EventArgs e)
        {
            

        }
        /// <summary>
        /// Sets the culture based on the language selection in the settings.
        /// </summary>
        void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            var culture = ApplicationSettings.Instance.Culture;
            if (!string.IsNullOrEmpty(culture) && !culture.Equals("Auto"))
            {
                CultureInfo defaultCulture = Utils.GetDefaultCulture();
                Thread.CurrentThread.CurrentUICulture = defaultCulture;
                Thread.CurrentThread.CurrentCulture = defaultCulture;
            }
        }
        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }
        #endregion

        private class FirstRequestInitialization
        {
            private static bool _initializedAlready = false;
            private static readonly object _SyncRoot = new Object();

            // Initialize only on the first request
            public static void Initialize(HttpContext context)
            {
                if (_initializedAlready)
                {
                    return;
                }

                lock (_SyncRoot)
                {
                    if (_initializedAlready)
                    {
                        return;
                    }

                     Utils.LoadExtensions();
                    _initializedAlready = true;
                }
            }
        }
    }
}
