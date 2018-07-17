using System;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Xml;


namespace IryTech.AdmissionJankari.Components.Web.HttpHandlers
{
    /// <summary>
    /// The ExcelImportHandler serves all Excel  that is uploaded from
    ///     the admin pages used Insert The Data into the table
    /// </summary>
    /// <remarks>
    /// By using a HttpHandler to serve files, it is very easy
    ///     to add the capability to stop bandwidth leeching or
    ///     to create a statistics analysis feature upon it.
    /// </remarks>

  public class ExcelImportHandler : IHttpHandler
    {

        #region Events

        /// <summary>
        ///     Occurs when the requested file does not exist;
        /// </summary>
        public static event EventHandler<EventArgs> BadRequest;

        /// <summary>
        ///     Occurs when a file is served;
        /// </summary>
        public static event EventHandler<EventArgs> Served;

        // <summary>
        ///     Occurs when a file is Saving to Desired Location;
        /// </summary>
        public static event EventHandler<EventArgs> Saving;

        /// <summary>
        ///     Occurs when the requested file does not exist;
        /// </summary>
        public static event EventHandler<EventArgs> Serving;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether another request can use the <see cref = "T:System.Web.IHttpHandler"></see> instance.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref = "T:System.Web.IHttpHandler"></see> instance is reusable; otherwise, false.</returns>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region IHttpHandler

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that 
        ///     implements the <see cref="T:System.Web.IHttpHandler"></see> interface.
        /// </summary>
        /// <param name="context">
        /// An <see cref="T:System.Web.HttpContext"></see> 
        ///     object that provides references to the intrinsic server objects 
        ///     (for example, Request, Response, Session, and Server) used to service HTTP requests.
        /// </param>
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString.Count <= 0) return;
            string temp = context.Request.QueryString[0].ToString();
            string fileName = "";
                string uploadFilePath = context.Request.PhysicalApplicationPath;
                uploadFilePath = uploadFilePath + GetFilepath("ExcelUpload");
               try
                {
                    if (context.Request.Files.Count > 0)
                    {
                        HttpPostedFile file = context.Request.Files[0];
                        file.SaveAs(Path.Combine(uploadFilePath, file.FileName));
                        if (HttpContext.Current.Request.QueryString.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(HttpContext.Current.Request["tblName"]))
                            {
                                context.Response.Write("<script type='text/javascript'>window.parent.location='/AdminPanel/College/CollegeImport/FieldMapping.aspx?tblName=" + HttpContext.Current.Request["tblName"] + "&file=" + file.FileName + "';</script>");
                            }

                        }
                        else
                        {
                            context.Response.Write("<script type='text/javascript'>window.parent.location='/ImportUtility/FieldMapping.aspx?file=" + file.FileName + "';</script>");
                        }

                    }
                    else
                    {
                        OnBadRequest(fileName);
                        //context.Response.Redirect(string.Format("{0}error404.aspx", Utils.AbsoluteWebRoot));
                    }
                }
                catch (Exception)
                {
                    OnBadRequest(fileName);
                    //context.Response.Redirect(string.Format("{0}error404.aspx", Utils.AbsoluteWebRoot));
                }

          

        }
        #endregion

        #region Methods

        /// <summary>
        /// Called when [bad request].
        /// </summary>
        /// <param name="file">The file name.</param>
        private static void OnBadRequest(string file)
        {
            if (BadRequest != null)
            {
                BadRequest(file, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when [served].
        /// </summary>
        /// <param name="file">The file name.</param>
        private static void OnServed(string file)
        {
            if (Served != null)
            {
                Served(file, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when [serving].
        /// </summary>
        /// <param name="file">The file name.</param>
        private static void OnServing(string file)
        {
            if (Serving != null)
            {
                Serving(file, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when [serving].
        /// </summary>
        /// <param name="file">The file name.</param>
        private static void OnSaving(string filePath, HttpContext context)
        {
            if (Saving != null)
            {
                HttpPostedFile file = context.Request.Files[0];
                file.SaveAs(Path.Combine(filePath, file.FileName));
                Saving(filePath,EventArgs.Empty);
            }
        }

        private static string GetFilepath(string messageId)
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

                    if (attColl != null && attColl[0].Value.ToString() == messageId.ToString())
                    {
                        message = attColl[1].Value.ToString();
                        break;
                    }
                }

            }
            return message;

        }
        #endregion
    }
}
