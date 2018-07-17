using System.Web.Configuration;

namespace IryTech.AdmissionJankari.Components
{
    public class ApplicationConfig
    {

        #region FileExtension

        /// <summary>
        ///     The  file extension used for aspx pages
        /// </summary>
        public static string FileExtension
        {
            get
            {
                return WebConfigurationManager.AppSettings["Apps.FileExtension"] ?? ".aspx";
            }
        }

        #endregion

        #region VirtualPath

        public static string VirtualPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["Apps.VirtualPath"] ?? "~/";
            }
        }
        #endregion

        #region Coonection String Name

        public static string ConnectionStraing
        {
            get
            {
                return WebConfigurationManager.AppSettings["DB_CON_STRING"];
            }
        }
        #endregion

        #region MobileServices

        public static string MobileServices
        {
            get
            {
                return WebConfigurationManager.AppSettings["Apps.MobileDevices"];
            }
        }

        #endregion

        #region StorageLocation

        /// <summary>
        /// Storage location on web server
        /// </summary>
        /// <returns>
        /// string with virtual path to storage
        /// </returns>
        public static string StorageLocation
        {
            get
            {
                return string.IsNullOrEmpty(WebConfigurationManager.AppSettings["StorageLocation"])
                           ? @"~/App_Data/"
                           : WebConfigurationManager.AppSettings["StorageLocation"];
            }
        }

        #endregion

        #region StorageLocation

        /// <summary>
        /// Gets name of the folder application instances are stored in.
        /// </summary>
        public static string ApplicationInstancesFolderName
        {
            get
            {
                return WebConfigurationManager.AppSettings["ApplicationConfigFolderName"] ?? "Application";
            }
        }

        #endregion


    }
}
