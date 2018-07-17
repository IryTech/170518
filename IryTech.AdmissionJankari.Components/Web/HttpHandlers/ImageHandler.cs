using System;
using System.IO;
using System.Web;
using System.Xml;


namespace IryTech.AdmissionJankari.Components.Web.HttpHandlers
{
    /// <summary>
    /// The ImageHanlder serves all images that is uploaded from
    ///     the admin pages.
    /// </summary>
    /// <remarks>
    /// By using a HttpHandler to serve images, it is very easy
    ///     to add the capability to stop bandwidth leeching or
    ///     to create a statistics analysis feature upon it.
    /// </remarks>
    public class ImageHandler : IHttpHandler
    {
        #region Events

        /// <summary>
        ///     Occurs when the requested file does not exist.
        /// </summary>
        public static event EventHandler<EventArgs> BadRequest;

        /// <summary>
        ///     Occurs when a file is served.
        /// </summary>
        public static event EventHandler<EventArgs> Served;

        /// <summary>
        ///     Occurs before the requested image is served.
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

        #region Implemented Interfaces

        #region IHttpHandler

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that 
        ///     implements the <see cref="T:System.Web.IHttpHandler"></see> interface.
        /// </summary>
        /// <param name="context">
        /// An <see cref="T:System.Web.HttpContext"></see> object 
        ///     that provides references to the intrinsic server objects 
        ///     (for example, Request, Response, Session, and Server) used to service HTTP requests.
        /// </param>
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["Folder"] == null)      //to check for asynchronous file upload
            {


                switch (context.Request.QueryString.Keys[0])
                {

                    case "Exam":
                        {
                            ImageServe(context, context.Request.QueryString[0], GetFilepath("ExamImg"));
                            break;
                        }
                    case "News":
                        {
                            ImageServe(context, context.Request.QueryString[0], GetFilepath("NewsImage"));
                            break;
                        }
                    case "Notice":
                        {
                            ImageServe(context, context.Request.QueryString[0], GetFilepath("NoticeImage"));
                            break;
                        }
                    case "College":
                        {
                            ImageServe(context, context.Request.QueryString[0], GetFilepath("UniversityImage"));
                            break;
                        }
                    case "User":
                        {
                            ImageServe(context, context.Request.QueryString[0], GetFilepath("UserImage"));
                            break;
                        }
                    case "Common":
                        {
                            ImageServe(context, context.Request.QueryString[0], GetFilepath("CommonImage"));
                            break;
                        }
                    case "Bank":
                        {
                            ImageServe(context, context.Request.QueryString[0], GetFilepath("BankImage"));
                            break;
                        }
                    case "Course":
                        {
                            ImageServe(context, context.Request.QueryString[0], GetFilepath("CourseImage"));
                            break;
                        }
                    case "Banner":
                        {
                            ImageServe(context, context.Request.QueryString[0], GetFilepath("BannerImage"));
                            break;
                        }
                    case "CollegeGallery":
                        {
                            ImageServe(context, context.Request.QueryString[0], GetFilepath("CollegeGallery"));
                            break;
                        }
                    case "CollegeGroup":
                        {
                            ImageServe(context, context.Request.QueryString[0], GetFilepath("UniversityImage"));
                            break;
                        }

                }

            }
            else
            {
            
                 HttpPostedFile postedFile = context.Request.Files["file"];
                 var fileName = postedFile.FileName;
                 var ext=  Path.GetExtension(postedFile.FileName);
                 if (fileName.LastIndexOf("\\", System.StringComparison.Ordinal) != -1)
                 {
                     fileName = fileName.Remove(0, fileName.LastIndexOf("\\", StringComparison.Ordinal)).ToLower();
                 }

                 fileName = GetUniqueFileName(fileName, GetFolderPath(context), ext).ToLower();
                var location = context.Server.MapPath(GetFolderPath(context) + fileName + ext);
                postedFile.SaveAs(location);
                context.Response.Write(fileName + ext);
                context.Response.End();
            }
        }

        #endregion

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
        private static void ImageServe(HttpContext context, string imageName, string folderName)
        {

            var fileName = imageName;
            OnServing(fileName);
            try
            {
                var folder = folderName;

                var fi = new FileInfo(context.Server.MapPath(folder) + fileName);

                if (fi.Directory != null && (fi.Exists))
                {
                    context.Response.Cache.SetCacheability(HttpCacheability.Public);
                    context.Response.Cache.SetExpires(DateTime.Now.AddYears(1));

                    if (Utils.SetConditionalGetHeaders(fi.CreationTimeUtc))
                    {
                        return;
                    }

                    var index = fileName.LastIndexOf(".", System.StringComparison.Ordinal) + 1;
                    var extension = fileName.Substring(index).ToUpperInvariant();

                    // Fix for IE not handling jpg image types
                    context.Response.ContentType = System.String.CompareOrdinal(extension, "JPG") == 0 ? "image/jpeg" : string.Format("image/{0}", extension);

                    context.Response.TransmitFile(fi.FullName);
                    OnServed(fileName);
                }
                else
                {
                    OnBadRequest(fileName);
                    //context.Response.Redirect(string.Format("{0}error404.aspx", Utils.AbsoluteWebRoot));
                }
            }
            catch (Exception ex)
            {
                OnBadRequest(ex.Message);
                //context.Response.Redirect(string.Format("{0}error404.aspx", Utils.AbsoluteWebRoot));
            }
        }

        public static string GetUniqueFileName(string name, string savePath, string ext)
        {

            name = name.Replace(ext, "").Replace(" ", "_");
            name = System.Text.RegularExpressions.Regex.Replace(name, @"[^\w\s]", "");

            var newName = name;
            var i = 0;
            while (File.Exists(savePath + newName + ext))
            {
                i++;
                newName = name + "_" + i;

            }

            return newName;
        }

        //to get folder path.....
        private string GetFolderPath(HttpContext context)
        {
            switch (context.Request.QueryString["Folder"])
            {

                case "Exam":
                    {
                        return GetFilepath("ExamImg");
                    }
                case "News":
                    {
                        return GetFilepath("NewsImage");

                    }
                case "Notice":
                    {
                        return GetFilepath("NoticeImage");

                    }
                case "College":
                    {
                        return GetFilepath("UniversityImage");

                    }
                case "User":
                    {
                        return GetFilepath("UserImage");

                    }
                case "Common":
                    {
                        return GetFilepath("CommonImage");

                    }
                case "Bank":
                    {
                        return GetFilepath("BankImage");

                    }
                case "Course":
                    {
                        return GetFilepath("CourseImage");

                    }
                case "Banner":
                    {
                        return GetFilepath("BannerImage");

                    }
                case "CollegeGallery":
                    {
                        return GetFilepath("CollegeGallery");

                    }
                case "CollegeGroup":
                    {
                        return GetFilepath("UniversityImage");

                    }

            }
            return null;
        }

        #endregion
    }
}