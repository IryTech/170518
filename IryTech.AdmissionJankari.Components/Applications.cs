using System;
using System.Collections.Generic;
using System.Web;
using System.Net;


namespace IryTech.AdmissionJankari.Components
{
    /// <summary>
    /// Represents a blog instance.
    /// </summary>
    public class Applications : BusinessBase<Applications, Guid>, IComparable<Applications>
    {
       
        private string _applicationName;
               
        private string _hostname;

        /// <summary>
        ///     Whether any text before the hostname is accepted.
        /// </summary>
        private bool _isAnyTextBeforeHostnameAccepted;

        /// <summary>
        ///     The storage container name of the blog's data
        /// </summary>
        /// /// <summary>
        ///     Whether the blog is the primary blog instance
        /// </summary>
        private bool _isPrimary;

        /// <summary>
        ///     Whether the blog is active
        /// </summary>
        private bool _isActive;

        /// <summary>
        ///     The hostname of the blog instance.
        /// </summary>
        private string _storageContainerName;

        /// <summary>
        ///     The virtual path to the blog instance
        /// </summary>
        private string _virtualPath;

        /// <summary>
        ///     The relative web root.
        /// </summary>
        private string _relativeWebRoot;

        /// <summary>
        ///     Flag used when blog is deleted on whether the storage container will be deleted too.
        /// </summary>
        private bool _deleteStorageContainer;

        /// <summary>
        /// The sync root.
        /// </summary>
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// The blogs.
        /// </summary>
        private static List<Applications> _apps;

        
   

     
        /// <summary>
        ///     Gets the optional hostname of the blog instance.
        /// </summary>
        public string Hostname
        {
            get
            {
                return this._hostname;
            }
            set
            {
                base.SetValue("Hostname", value, ref this._hostname);
            }
        }

        /// <summary>
        ///     Gets whether any text before the hostname is accepted.
        /// </summary>
        public bool IsAnyTextBeforeHostnameAccepted
        {
            get
            {
                return this._isAnyTextBeforeHostnameAccepted;
            }
            set
            {
                base.SetValue("IsAnyTextBeforeHostnameAccepted", value, ref this._isAnyTextBeforeHostnameAccepted);
            }
        }
        /// <summary>
        ///     Gets whether the blog is the primary blog instance.
        /// </summary>
        public bool IsPrimary
        {
            get
            {
                return this._isPrimary;
            }
            internal set
            {
                // SetAsPrimaryInstance() exists as a public method to make
                // a blog instance be the primary one -- which makes sure other
                // instances are no longer primary.

                base.SetValue("IsPrimary", value, ref this._isPrimary);
            }
        }

        /// <summary>
        ///     Gets whether the blog instance is active.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                base.SetValue("IsActive", value, ref this._isActive);
            }
        }

        /// <summary>
        ///     Gets the optional hostname of the blog instance.
        /// </summary>
        /// <summary>
        ///     Gets or sets the blog name.
        /// </summary>
        public string Name
        {
            get
            {
                return this._applicationName;
            }

            set
            {
                base.SetValue("Name", value, ref this._applicationName);
            }
        }

        public static Applications CurrentInstance
        {
            get
            {
                HttpContext context = HttpContext.Current;
                var objApplication = new Applications
                                         {
                                             IsPrimary = true,
                                             _isActive = true,
                                             Name = "Admissionjankari",
                                             _virtualPath = "~/"
                                         };


                return objApplication;
            }
        }
        /// <summary>
        /// Returns a blog based on the specified id.
        /// </summary>
        /// <param name="id">
        /// The blog id.
        /// </param>
        /// <returns>
        /// The selected blog.
        /// </returns>
        public static Applications GetApplication(Guid id)
        {
            return Blogs.Find(b => b.Id == id);
        }


        public static List<Applications> Blogs
        {
            get
            {
                if (_apps == null)
                {
                    lock (SyncRoot)
                    {
                        if (_apps == null)
                        {
                           // create the primary instance
                              // create the primary instance

                                var apps = new Applications
                                               {
                                                   Name = "Primary",
                                                   _hostname = string.Empty,
                                                   VirtualPath = ApplicationConfig.VirtualPath,
                                                   StorageContainerName = string.Empty,
                                                   IsPrimary = true
                                               };
                         
                           

                           
                        }
                    }
                }

                return _apps;
            }
        }
        
        /// <summary>
        ///     Gets or sets the storage container name.
        /// </summary>
        public string StorageContainerName
        {
            get
            {
                return this._storageContainerName;
            }

            set
            {
                base.SetValue("StorageContainerName", value, ref this._storageContainerName);
            }
        }

        /// <summary>
        ///     Gets or sets the virtual path to the blog instance.
        /// </summary>
        public string VirtualPath
        {
            get
            {
                return this._virtualPath;
            }

            set
            {
                // RelativeWebRoot is based on VirtualPath.  Clear relativeWebRoot
                // so RelativeWebRoot is re-generated.
                this._relativeWebRoot = null;

                base.SetValue("VirtualPath", value, ref this._virtualPath);
            }
        }

        /// <summary>
        ///     Flag used when blog is deleted on whether the storage container will be deleted too.
        ///     This property is not peristed.
        /// </summary>
        public bool DeleteStorageContainer
        {
            get
            {
                return this._deleteStorageContainer;
            }

            set
            {
                base.SetValue("DeleteStorageContainer", value, ref this._deleteStorageContainer);
            }
        }

        public bool IsSubfolderOfApplicationWebRoot
        {
            get
            {
                return this.RelativeWebRoot.Length > Utils.ApplicationRelativeWebRoot.Length;
            }
        }

      
        

        /// <summary>
        ///     Initializes a new instance of the <see cref = "Applications" /> class. 
        ///     The default contstructor assign default values.
        /// </summary>
        public Applications()
        {
            this.Id = Guid.NewGuid();
           // this.DateCreated = DateTime.Now;
            //this.DateModified = DateTime.Now;
        }

        

  

     
        [ThreadStatic]
        private static Guid _instanceIdOverride;

        /// <summary>
        /// This is a thread-specific Blog Instance ID to override.
        /// If the current blog instance needs to be overridden,
        /// this property can be used.  A typical use for this is when
        /// using BG/async threads where the current blog instance
        /// cannot be determined since HttpContext will be null.
        /// </summary>
        public static Guid InstanceIdOverride
        {
            get { return _instanceIdOverride; }
            set { _instanceIdOverride = value; }
        }

        


        /// <summary>
        ///     Gets a mappable virtual path to the blog instance's storage folder.
        /// </summary>
        public string StorageLocation
        {
            get
            {
                // only the Primary blog instance should have an empty StorageContainerName
                if (string.IsNullOrWhiteSpace(this.StorageContainerName))
                {
                    return ApplicationConfig.StorageLocation;
                }

                return string.Format("{0}{1}/{2}/", ApplicationConfig.StorageLocation, ApplicationConfig.ApplicationInstancesFolderName, this.StorageContainerName);
            }
        }

        /// <summary>
        ///     Gets the relative root of the blog instance.
        /// </summary>
        /// <value>A string that ends with a '/'.</value>
        public string RelativeWebRoot
        {
            get
            {
                return _relativeWebRoot ??
                       (_relativeWebRoot =
                        VirtualPathUtility.ToAbsolute(VirtualPathUtility.AppendTrailingSlash(this.VirtualPath ?? ApplicationConfig.VirtualPath)));
            }
        }

        /// <summary>
        ///     Gets the absolute root of the blog instance.
        /// </summary>
        public Uri AbsoluteWebRoot
        {
            get
            {
                string contextItemKey = string.Format("{0}-absolutewebroot", this.Id);

                var context = HttpContext.Current;
                if (context == null)
                {
                    throw new WebException("The current HttpContext is null");
                }

                var absoluteWebRoot = context.Items[contextItemKey] as Uri;
                if (absoluteWebRoot != null) { return absoluteWebRoot; }

                var uri = new UriBuilder();
                if (!string.IsNullOrWhiteSpace(this.Hostname))
                    uri.Host = this.Hostname;
                else
                {
                    uri.Host = context.Request.Url.Host;
                    if (!context.Request.Url.IsDefaultPort)
                    {
                        uri.Port = context.Request.Url.Port;
                    }
                }

                string vPath = this.VirtualPath ?? string.Empty;
                if (vPath.StartsWith("~/")) { vPath = vPath.Substring(2); }
                uri.Path = string.Format("{0}{1}", Utils.ApplicationRelativeWebRoot, vPath);
                if (!uri.Path.EndsWith("/")) { uri.Path += "/"; }

                absoluteWebRoot = uri.Uri;
                context.Items[contextItemKey] = absoluteWebRoot;

                return absoluteWebRoot;
            }
        }

        private CacheProvider _cache;
        /// <summary>
        /// blog instance cache
        /// </summary>
        public CacheProvider Cache
        {
            get { return _cache ?? (_cache = new CacheProvider(HttpContext.Current.Cache)); }
        }







        #region IComparable<Applications> Members

        public int CompareTo(Applications other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
