using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Hosting;
using System.IO;
using IryTech.AdmissionJankari.Components.Provider;


namespace IryTech.AdmissionJankari.Components
{
    /// <summary>
    /// Represents the configured settings for the application.
    /// </summary>
    public class ApplicationSettings
    {
        #region PRIVATE/PROTECTED/PUBLIC MEMBERS

        // <summary>
        ///     The application settings singleton.
        /// 
        /// <remarks>
        /// This should be created immediately instead of lazyloaded. It'll reduce the number of null checks that occur
        /// due to heavy reliance on calls to Ramukaka setting.Instance.
        /// </remarks>
        private static readonly Dictionary<Guid, ApplicationSettings> ApplicationSettingsSingleton = new Dictionary<Guid, ApplicationSettings>();

        /// <summary>
        ///     The configured theme.
        /// </summary>
        private string _configuredTheme = String.Empty;

      
        /// <summary>
        ///     The number of comments per page.
        /// </summary>
        private int _commentsPerPage;

        /// <summary>
        ///     The enable http compression.
        /// </summary>
        private bool _enableHttpCompression;

       

        /// <summary>
        /// The sync root.
        /// </summary>
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// The timeout in milliseconds for a remote download. Default is 30 seconds.
        /// </summary>
        private int _remoteDownloadTimeout = DefaultRemoteDownloadTimeout;
        private const int DefaultRemoteDownloadTimeout = 30000;

        private int _maxRemoteFileSize = DefaultMaxRemoteFileSize;
        private const int DefaultMaxRemoteFileSize = 524288;

        #endregion

      
        private ApplicationSettings()
        {
            this.Load();
        }


        /// <summary>
        ///     Gets the singleton instance of the <see>
        ///                                          <cref>ApplicationSetting</cref>
        ///                                        </see>  class.
        /// </summary>
        /// <value>A singleton instance of the <see>
        ///                                      <cref>ApplicationSetting</cref>
        ///                                    </see> class.</value>
        /// <remarks>
        /// </remarks>
        public static ApplicationSettings Instance
        {
            get
            {
                return GetInstanceSettings(Applications.CurrentInstance);
            }
        }
        public static ApplicationSettings GetInstanceSettings(Applications objApplication)
        {
            var objApplicationSerring =new ApplicationSettings();
            
            return objApplicationSerring;
        }

        private IDictionary<String, System.Reflection.PropertyInfo> GetSettingsTypePropertyDict()
        {
            var settingsType = this.GetType();

            var result = new System.Collections.Generic.Dictionary<String, System.Reflection.PropertyInfo>(StringComparer.OrdinalIgnoreCase);

            foreach (var prop in settingsType.GetProperties())
            {
                result[prop.Name] = prop;
            }

            return result;

        }
        private void Load()
        {

            // ------------------------------------------------------------
            // 	Enumerate through individual settings nodes
            // ------------------------------------------------------------
            var dic = ApplicationServices.LoadSettings();
            var settingsProps = GetSettingsTypePropertyDict();

            foreach (System.Collections.DictionaryEntry entry in dic)
            {
                var name = (string)entry.Key;
                System.Reflection.PropertyInfo property = null;

                if (settingsProps.TryGetValue(name, out property))
                {
                    // ------------------------------------------------------------
                    // 	Attempt to apply configured setting
                    // ------------------------------------------------------------
                    try
                    {
                        if (property.CanWrite)
                        {
                            var value = (string)entry.Value;
                            var propType = property.PropertyType;

                            property.SetValue(this,
                                              propType.IsEnum
                                                  ? Enum.Parse(propType, value)
                                                  : Convert.ChangeType(value, propType, CultureInfo.CurrentCulture),
                                              null);
                        }
                    }
                    catch (Exception e)
                    {
                        Utils.Log(string.Format("Error loading application settings: {0}", e.Message));
                    }
                }

            }

        }
        #region Version()

        /// <summary>
        ///     The version.
        /// </summary>
        private static string version;

        /// <summary>
        /// Returns the application version information.
        /// </summary>
        /// <value>
        /// The application major, minor, revision, and build numbers.
        /// </value>
        /// <remarks>
        /// The current version is determined by extracting the build version of the application.Core assembly.
        /// </remarks>
        /// <returns>
        /// The version.
        /// </returns>
        public string Version()
        {
            return version ?? (version = this.GetType().Assembly.GetName().Version.ToString());
        }

        #endregion
        /// <summary>
        /// Returns the settings for the requested application instance.
        /// </summary>

        private bool? _isRazorTheme;
        /// <summary>
        /// Gets whether Theme is a razor theme.
        /// </summary>
        public bool IsRazorTheme
        {
            get
            {
                if (_isRazorTheme.HasValue) { return _isRazorTheme.Value; }

                _isRazorTheme = IsThemeRazor(this.Theme);
                return _isRazorTheme.Value;
            }
        }

        /// <summary>
        /// Determines if themeName is a razor theme.
        /// </summary>
        public static bool IsThemeRazor(string themeName)
        {
            var path = HostingEnvironment.MapPath(string.Format("~/themes/{0}/site.cshtml", themeName));
            return File.Exists(path);
        }

        /// <summary>
        /// Takes into account factors such as if there is a theme override of if
        /// the theme is a Razor theme and returns the actual theme folder name
        /// for the current HTTP request.
        /// </summary>
        public string GetThemeWithAdjustments(string themeOverride)
        {
            var theme = this.Theme;
            var isRazorTheme = _configuredTheme == theme ? IsRazorTheme : IsThemeRazor(theme);
            if (!string.IsNullOrWhiteSpace(themeOverride))
            {
                theme = themeOverride;
                isRazorTheme = IsThemeRazor(theme);
            }
            return isRazorTheme ? "RazorHost" : theme;
        }

        #region ApplicationName

        /// <summary>
        ///     Gets or sets the ApplicationName of the application.
        /// </summary>
        /// <value>A brief synopsis of the application content.</value>
        /// <remarks>
        ///     This value is also used for the description meta tag.
        /// </remarks>
        public string ApplicationName { get; set; }

        #endregion

        #region EnableHttpCompression

        /// <summary>
        ///     Gets or sets a value indicating if HTTP compression is enabled.
        /// </summary>
        /// <value><b>true</b> if compression is enabled, otherwise returns <b>false</b>.</value>
        public bool EnableHttpCompression
        {
            get
            {
                return this._enableHttpCompression && !Utils.IsMono;
            }

            set
            {
                this._enableHttpCompression = value;
            }
        }

        #endregion
          
        #region EnableRating

        /// <summary>
        ///     Gets or sets a value indicating if live preview of post is displayed.
        /// </summary>
        /// <value><b>true</b> if live previews are displayed, otherwise returns <b>false</b>.</value>
        public bool EnableRating { get; set; }

        #endregion
        
        #region DescriptionCharactersOfCollege

        /// <summary>
        ///     Gets or sets a value indicating how many characters should be shown of the description of the college 
        /// </summary>
        public int DescriptionCharactersOfCollege { get; set; }

        #endregion
        
        #region DiscriptionCharacterNewArticle

        /// <summary>
        ///     Gets or sets a value indicating how many characters should be shown of the description in the news atricle 
        /// </summary>
        public int DescriptionCharactersNewsAtricle { get; set; }

        #endregion

        #region DescriptionCharactersOfNewsAtricle

        /// <summary>
        ///     Gets or sets a value indicating how many characters should be shown of the description in the Exam
        /// </summary>
        public int DescriptionCharactersExam { get; set; }

        #endregion

        #region UrlLenght 

        /// <summary>
        ///     Gets or sets a value of url length 
        /// </summary>
        public int UrlLenght { get; set; }

        #endregion

        #region TitleLenght

        /// <summary>
        ///     Gets or sets a value of Title Lenght
        /// </summary>
        public int TitleLenght { get; set; }

        #endregion

        #region MetaTagLenght

        /// <summary>
        ///     Gets or sets a value of Meta Tag Lenght
        /// </summary>
        public int MetaTagLenght { get; set; }

        #endregion

        #region MetaKeyword Length

        /// <summary>
        ///     Gets or sets a value o fMetaKeyword Lenght
        /// </summary>
        public int MetaKeywordLenght { get; set; }

        #endregion

        #region ThemeCookieName

        /// <summary>
        ///     The default theme cookie name.
        /// </summary>
        private const string DefaultThemeCookieName = "theme";

        /// <summary>
        ///     The theme cookie name.
        /// </summary>
        private string _themeCookieName;

        /// <summary>
        ///     Gets or sets the name of the cookie that can override
        ///     the selected theme.
        /// </summary>
        /// <value>The name of the cookie that is checked while determining the theme.</value>
        /// <remarks>
        ///     The default value is "theme".
        /// </remarks>
        public string ThemeCookieName
        {
            get
            {
                return this._themeCookieName ?? DefaultThemeCookieName;
            }

            set
            {
                this._themeCookieName = value != DefaultThemeCookieName ? value : null;
            }
        }

        #endregion

        #region Theme

        /// <summary>
        ///     Gets or sets the current theme applied to the application.
        ///     Default theme can be overridden in the query string
        ///     or cookie to let users select different theme
        /// </summary>
        /// <value>The configured theme for the Order Ramukaka</value>
        public string Theme
        {
            get
            {
                var context = HttpContext.Current;
                if (context != null)
                {
                    var request = context.Request;
                    if (request.QueryString["theme"] != null)
                    {
                        return request.QueryString["theme"];
                    }

                    var cookie = request.Cookies[this.ThemeCookieName];
                    if (cookie != null)
                    {
                        return cookie.Value;
                    }

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       if (Utils.ShouldForceMainTheme(request))
                    {
                        return this._configuredTheme;
                    }
                }

                if (Utils.IsMobile && !string.IsNullOrEmpty(this.MobileTheme))
                {
                    return this.MobileTheme;
                }

                return this._configuredTheme;
            }

            set
            {
                this._configuredTheme = String.IsNullOrEmpty(value) ? String.Empty : value;
            }
        }

        #endregion

        #region MobileTheme

        /// <summary>
        ///     Gets or sets the mobile theme.
        /// </summary>
        /// <value>The mobile theme.</value>
        public string MobileTheme { get; set; }

        #endregion

        #region use application in page table 

        /// <summary>
        ///     Gets or sets the mobile theme.
        /// </summary>
        /// <value>The mobile theme.</value>
        public bool  UseApplicationNameInPageTitles { get; set; }

        #endregion

        public bool AllowServerToDownloadRemoteFiles { get; set; }

        public bool ShowPackage { get; set; }


        #region RemoveWhitespaceInStyleSheets

        /// <summary>
        ///     Gets or sets a value indicating if whitespace in stylesheets should be removed
        /// </summary>
        /// <value><b>true</b> if whitespace is removed, otherwise returns <b>false</b>.</value>
        public bool RemoveWhitespaceInStyleSheets { get; set; }

        #endregion
                 

      

        #region CompressWebResource

        /// <summary>
        ///     Gets or sets a value indicating whether to compress WebResource.axd
        /// </summary>
        /// <value><c>true</c> if [compress web resource]; otherwise, <c>false</c>.</value>
        public bool CompressWebResource { get; set; }

        #endregion
        
        #region TrackingScript

        /// <summary>
        ///     Gets or sets the tracking script used to collect visitor data.
        /// </summary>
        public string TrackingScript { get; set; }

        #endregion

        #region PageSizeOfCollege

        /// <summary>
        ///     Gets or sets the No of Collges will show in the College Serach Page.
        /// </summary>
        public int CollegePageSize { get; set; }

        #endregion

        #region CollegePageCountShow

        /// <summary>
        ///     Gets or sets the No of Page Count Will show in the Page Search.
        /// </summary>
        public int  CollegePageCount { get; set; }

        #endregion

        #region PageSizeOfExam

        /// <summary>
        ///     Gets or sets the No of Exam will show in the Exam List Page.
        /// </summary>
        public int ExamPageSize { get; set; }

        #endregion

        #region DefaultCouseSelection

        /// <summary>
        ///     Gets or sets the No of to set the default Course Selection 
        /// </summary>
        public int CourseId { get; set; }

        #endregion

        #region PageCountOfExam

        /// <summary>
        ///     Gets or sets the No of Exam will show in the Exam List Page.
        /// </summary>
        public int ExamPageCount { get; set; }

        #endregion

        #region PageSizeOfNewsArticle

        /// <summary>
        ///     Gets or sets the No of NewsArticle will show in the NewsArticle List Page.
        /// </summary>
        public int NewsArticlePageSize { get; set; }

        #endregion

        #region PageCountOfNewsArticle

        /// <summary>
        ///     Gets or sets the No of Page Count   will show in the NewsArticle List Page.
        /// </summary>
        public int NewsArticlePageCount { get; set; }

        #endregion

        #region PageSizeOfMostViewdCollege

        /// <summary>
        ///     Gets or sets the No of College will show in the MostViewdCollege List Control.
        /// </summary>
        public int MostViewdCollegePageSize { get; set; }

        #endregion

        #region PageCountOfMostViewdCollege

        /// <summary>
        ///     Gets or sets the No of pages will show in the MostViewdCollege List Control.
        /// </summary>
        public int MostViewdCollegePageCount { get; set; }

        #endregion

        #region PageSizeOfTopRankedCollege

        /// <summary>
        ///     Gets or sets the No of PageSizeOfTopRankedCollege will show in the PageSizeOfTopRankedCollege List Control.
        /// </summary>
        public int TopRankedCollegePageSize { get; set; }

        #endregion

        #region PageCountOfMostViewdCollege

        /// <summary>
        ///     Gets or sets the No of pages Count will show in the TopRankedCollege List Control.
        /// </summary>
        public int TopRankedCollegePageCount { get; set; }

        #endregion

        #region PageSizeOfBestPvtCollege

        /// <summary>
        ///     Gets or sets the No of BestPvtCollege will show in the BestPvtCollege List Control.
        /// </summary>
        public int BestPvtCollegePageSize { get; set; }

        #endregion

        #region PageCountOfBestPvtCollege

        /// <summary>
        ///     Gets or sets the No of pages Count will show in the BestPvtCollege List Control.
        /// </summary>
        public int BestPvtCollegePageCount { get; set; }

        #endregion

        #region Email

        /// <summary>
        ///     Gets or sets the e-mail address notifications are sent to.
        /// </summary>
        /// <value>The e-mail address notifications are sent to.</value>
        public string Email { get; set; }

        #endregion

        #region SendMailOnComment

        /// <summary>
        ///     Gets or sets a value indicating if an enail is sent when a comment is added to a post.
        /// </summary>
        /// <value><b>true</b> if email notification of new comments is enabled, otherwise returns <b>false</b>.</value>
        public bool SendMailOnComment { get; set; }

        #endregion

        #region SmtpPassword

        /// <summary>
        ///     Gets or sets the password used to connect to the SMTP server.
        /// </summary>
        /// <value>The password used to connect to the SMTP server.</value>
        public string SmtpPassword { get; set; }

        #endregion

        #region SmtpServer

        /// <summary>
        ///     Gets or sets the DNS name or IP address of the SMTP server used to send notification emails.
        /// </summary>
        /// <value>The DNS name or IP address of the SMTP server used to send notification emails.</value>
        public string SmtpServer { get; set; }

        #endregion

        #region SmtpServerPort

        /// <summary>
        ///     Gets or sets the DNS name or IP address of the SMTP server used to send notification emails.
        /// </summary>
        /// <value>The DNS name or IP address of the SMTP server used to send notification emails.</value>
        public int SmtpServerPort { get; set; }
        public string  Host { get; set; }

        #endregion

        #region SmtpUsername

        /// <summary>
        ///     Gets or sets the user name used to connect to the SMTP server.
        /// </summary>
        /// <value>The user name used to connect to the SMTP server.</value>
        public string SmtpUserName { get; set; }

        #endregion
            

        #region EmailSubjectPrefix

        /// <summary>
        ///     Gets or sets the email subject prefix.
        /// </summary>
        /// <value>The email subject prefix.</value>
        public string EmailSubjectPrefix { get; set; }

        #endregion

  
        #region EnableCommentsModeration

        /// <summary>
        ///     Gets or sets a value indicating if comments moderation is used .
        /// </summary>
        /// <value><b>true</b> if comments are moderated for posts, otherwise returns <b>false</b>.</value>
        public bool EnableCommentsModeration { get; set; }

        #endregion

  
        #region Comments per page

        /// <summary>
        ///     Number of comments per page displayed in the comments admin section
        /// </summary>
        public int CommentsPerPage
        {
            get { return Math.Max(_commentsPerPage, 10); }
            set { _commentsPerPage = value; }
        }

        #endregion
        #region Page count Show for most view Exam

        /// <summary>
        ///     Number page count will show in the most view exam
        /// </summary>
        public int MostViewExamPageCount
        {
            get;
            set;
        }

        #endregion

        #region Page Size for most view Exam

        /// <summary>
        ///     Page Size  will show in the most view exam
        /// </summary>
        public int MostViewExamPageSize
        {
            get;
            set;
        }

        #endregion

        #region Book your seat 

        /// <summary>
        ///     Its is used to check weather admin want to show book your seat or not
        /// </summary>
        public bool IsVissbibleBookYourSeat
        {
            get;
            set;
        }

        #endregion
     
      
        #region Page Size for most News

        /// <summary>
        ///     Page Size  will show in the most view News
        /// </summary>
        public int MostViewNewsPageSize
        {
            get;
            set;
        }

        #endregion
        #region Page Count for most News

        /// <summary>
        ///     Page Count  will show in the most view News
        /// </summary>
        public int MostViewNewsPageCount
        {
            get;
            set;
        }

        #endregion
     

        #region Language

        /// <summary>
        ///     Gets or sets the language this application  is written in.
        /// </summary>
        /// <value>The language this application is written in.</value>
        /// <remarks>
        ///     Recommended best practice for the values of the Language element is defined by RFC 1766 [RFC1766] which includes a two-letter Language Code (taken from the ISO 639 standard [ISO639]), 
        ///     followed optionally, by a two-letter Country Code (taken from the ISO 3166 standard [ISO3166]).
        /// </remarks>
        /// <example>
        ///     en-US
        /// </example>
        public  string Language { get; set; }

        #endregion

       
        #region ContactFormMessage;

        /// <summary>
        ///     Gets or sets the name of the author of this application.
        /// </summary>
        /// <value>The name of the author of this application.</value>
        public string ContactFormMessage { get; set; }

        #endregion

        #region HtmlHeader

        /// <summary>
        ///     Gets or sets the name of the author of this application.
        /// </summary>
        /// <value>The name of the author of this application.</value>
        public string HtmlHeader { get; set; }

        #endregion

        #region Culture

        /// <summary>
        ///     Gets or sets the name of the author of this application.
        /// </summary>
        /// <value>The name of the author of this application.</value>
        public  string Culture { get; set; }

        #endregion

        #region SearchPriorityListCount

        /// <summary>
        ///     Gets or sets the ApplicationName of the application.
        /// </summary>
        /// <value>A brief synopsis of the application content.</value>
        /// <remarks>
        ///     This value is also used for the description meta tag.
        /// </remarks>
        public string SearchPriorityListCount { get; set; }

        #endregion

  
   

        public int RemoteFileDownloadTimeout
        {
            get
            {
                if (this._remoteDownloadTimeout < 0)
                {
                    this._remoteDownloadTimeout = DefaultRemoteDownloadTimeout;
                }
                return this._remoteDownloadTimeout;
            }
            set
            {
                if (value < 0) { value = DefaultRemoteDownloadTimeout; }
                this._remoteDownloadTimeout = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum allowed file size in bytes that application can download from a remote server. Defaults to 512k.
        /// </summary>
        /// <remarks>
        /// 
        /// Set this value to 0 for unlimited file size.
        /// 
        /// </remarks>
        public int RemoteMaxFileSize
        {
            get
            {
                if (this._maxRemoteFileSize < 0)
                {
                    this._maxRemoteFileSize = DefaultMaxRemoteFileSize;
                }
                return this._maxRemoteFileSize;
            }
            set
            {
                if (value < 0) { value = DefaultMaxRemoteFileSize; }
                this._maxRemoteFileSize = value;
            }
        }

       
     }
}