﻿using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace IryTech.AdmissionJankari.Components.Web.Controls 
{
    /// <summary>
    /// All pages in the custom themes as well as pre-defined pages in the root
    ///     must inherit from this class.
    /// </summary>
    /// <remarks>
    /// The class is responsible for assigning the theme to all
    ///     derived pages as well as adding RSS, RSD, tracking script
    ///     and a whole lot more.
    /// </remarks>
   public  abstract class  PageBase:Page
    {
        #region Constants and Fields

        /// <summary>
        /// The theme.
        /// </summary>
        private readonly string _theme = ApplicationSettings .Instance.Theme;

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the generic link to the header.
        /// </summary>
        /// <param name="relation">
        /// The relation string.
        /// </param>
        /// <param name="title">
        /// The title string.
        /// </param>
        /// <param name="href">
        /// The href string.
        /// </param>
        public virtual void AddGenericLink(string relation, string title, string href)
        {
            this.AddGenericLink(null, relation, title, href);
        }

        /// <summary>
        /// Adds the generic link to the header.
        /// </summary>
        /// <param name="type">
        /// The type string.
        /// </param>
        /// <param name="relation">
        /// The relation string.
        /// </param>
        /// <param name="title">
        /// The title string.
        /// </param>
        /// <param name="href">
        /// The href string.
        /// </param>
        public virtual void AddGenericLink(string type, string relation, string title, string href)
        {
            this.Page.Header.Controls.Add(GetGenericLink(type, relation, title, href));
        }

        /// <summary>
        /// Adds a Stylesheet reference to the HTML head tag.
        /// </summary>
        /// <param name="url">
        /// The relative URL.
        /// </param>
        /// <param name="insertAtFront">
        /// If true, inserts in beginning of HTML head tag.
        /// </param>
        public virtual void AddStylesheetInclude(string url, bool insertAtFront)
        {
            var link = new HtmlLink();
            link.Attributes["type"] = "text/css";
            link.Attributes["href"] = url;
            link.Attributes["rel"] = "stylesheet";

            if (insertAtFront)
            {
                this.Page.Header.Controls.AddAt(0, link);
            }
            else
            {
                this.Page.Header.Controls.Add(link);
            }
        }

        /// <summary>
        /// Adds a Stylesheet reference to the HTML head tag.
        /// </summary>
        /// <param name="url">
        /// The relative URL.
        /// </param>
        public virtual void AddStylesheetInclude(string url)
        {
            this.AddStylesheetInclude(url, false);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds code to the HTML head section.
        /// </summary>
        protected virtual void AddCustomCodeToHead()
        {
            var code = string.Format(
                CultureInfo.InvariantCulture,
                "{0}<!-- Start custom code -->{0}{1}{0}<!-- End custom code -->{0}",
                Environment.NewLine,
                ApplicationSettings.Instance.HtmlHeader);
            var control = new LiteralControl(code);
            this.Page.Header.Controls.Add(control);
        }

        /// <summary>
        /// Adds the default stylesheet language
        /// </summary>
        protected virtual void AddDefaultLanguages()
        {
            this.Response.AppendHeader("Content-Style-Type", "text/css");
            this.Response.AppendHeader("Content-Script-Type", "text/javascript");
        }

        /// <summary>
        /// Add global style sheets before any custom css
        /// </summary>
        protected virtual void AddGlobalStyles()
        {
            // add styles in the ~/Styles folder to the page header
            var s = Path.Combine(HttpContext.Current.Server.MapPath(Utils.ApplicationRelativeWebRoot), "Styles");
            var fileEntries = Directory.GetFiles(s);
            foreach (var fileName in
                fileEntries.Where(fileName => fileName.EndsWith(".css", StringComparison.OrdinalIgnoreCase)))
            {
                this.AddStylesheetInclude(
                    string.Format("{0}Styles/{1}", Utils.ApplicationRelativeWebRoot, Utils.ExtractFileNameFromPath(fileName)), true);
            }
        }

        /// <summary>
        /// Adds the content-type meta tag to the header.
        /// </summary>
        protected virtual void AddMetaContentType()
        {
            var meta = new HtmlMeta
            {
                HttpEquiv = "content-type",
                Content =
                    string.Format(
                        "{0}; charset={1}", this.Response.ContentType, this.Response.ContentEncoding.HeaderName)
            };
            this.Page.Header.Controls.Add(meta);
        }

        /// <summary>
        /// Add a meta tag to the page's header.
        /// </summary>
        /// <param name="name">
        /// The tag name.
        /// </param>
        /// <param name="value">
        /// The tag value.
        /// </param>
        protected virtual void AddMetaTag(string name, string value)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
            {
                return;
            }

            var meta = new HtmlMeta { Name = name, Content = value };
            this.Page.Header.Controls.Add(meta);
        }

        /// <summary>
        /// Adds a JavaScript to the bottom of the page at runtime.
        /// </summary>
        /// <remarks>
        /// You must add the script tags to the ApplicationSettings.Instance.TrackingScript.
        /// </remarks>
        protected virtual void AddTrackingScript()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(ApplicationSettings.Instance.TrackingScript))
            {
                sb.Append(ApplicationSettings.Instance.TrackingScript);
            }

            var s = sb.ToString();
            if (!string.IsNullOrEmpty(s))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "tracking", string.Format("\n{0}", s), false);
            }
        }

        /// <summary>
        /// Finds all stylesheets in the header and changes the 
        ///     path so it points to css.axd which removes the whitespace.
        /// </summary>
        protected virtual void CompressCss()
        {
            if (this.Request.QueryString["theme"] != null)
            {
                return;
            }

            foreach (Control control in this.Page.Header.Controls)
            {
                var c = control as HtmlControl;
                if (c == null || c.Attributes["type"] == null ||
                    !c.Attributes["type"].Equals("text/css", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                // if a CSS filename has ".min.css" in it, it is probably an already
                // minified CSS file -- skip these.
                if (c.Attributes["href"].StartsWith("http://") ||
                    c.Attributes["href"].IndexOf(".min.css", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    continue;
                }

                var url = string.Format("{0}themes/{1}/css.axd?name={2}", Utils.ApplicationRelativeWebRoot, this._theme, c.Attributes["href"]);
                c.Attributes["href"] = url;
                c.EnableViewState = false;
            }
        }


        /// <summary>
        /// Creates and returns a generic link control.
        /// </summary>
        /// <param name="type">
        /// The HtmlLink's "type" attribute value.
        /// </param>
        /// <param name="relation">
        /// The HtmlLink's "rel" attribute value.
        /// </param>
        /// <param name="title">
        /// The HtmlLink's "title" attribute value.
        /// </param>
        /// <param name="href">
        /// The HtmlLink's "href" attribute value.
        /// </param>
        private static HtmlLink GetGenericLink(string type, string relation, string title, string href)
        {
            var link = new HtmlLink();

            if (type != null) { link.Attributes["type"] = type; }
            link.Attributes["rel"] = relation;
            link.Attributes["title"] = title;
            link.Attributes["href"] = href;
            return link;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.TemplateControl.Error"></see> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="T:System.EventArgs"></see> that contains the event data.
        /// </param>
        protected override void OnError(EventArgs e)
        {
            var ctx = HttpContext.Current;
            var exception = ctx.Server.GetLastError();

            if (exception != null && exception.Message.Contains("callback"))
            {
                // This is a robot spam attack so we send it a 404 status to make it go away.
                ctx.Response.StatusCode = 404;
                ctx.Server.ClearError();
               // Comment.OnSpamAttack();
            }

            base.OnError(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
        /// Adds links and javascript to the HTML header tag.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string relativeWebRoot = Utils.RelativeWebRoot;
            Uri absoluteWebRoot = Utils.AbsoluteWebRoot;
            string instanceName = ApplicationSettings.Instance.ApplicationName;

            if (!this.Page.IsCallback)
            {
              this.AddMetaContentType();

                this.AddDefaultLanguages();

                //   this.AddLocalizationKeys();

                this.AddGlobalStyles();

                Utils.AddFolderJavaScripts(this, "Scripts", true);
               // Utils.AddJavaScriptResourcesToPage(this);
                Utils.AddFolderJavaScripts(this, string.Format("themes/{0}", this._theme), true);

                
                if (!string.IsNullOrEmpty(ApplicationSettings.Instance.HtmlHeader))
                {
                    this.AddCustomCodeToHead();
                }

                this.AddTrackingScript();
            }


            if (ApplicationSettings.Instance.RemoveWhitespaceInStyleSheets)
            {
                this.CompressCss();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Page.PreInit"/> event at the beginning of page initialization.
        /// Assignes the selected theme to the pages.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnPreInit(EventArgs e)
        {
            bool allowViewing = true ;

            // - To prevent authenticated users from accessing the site, you would assign
            //   that user to a role that does not have the right to ViewPublicPosts.
            // - To prevent unauthenticated users from accessing the site, remove
            //   the ViewPublicPosts from the Anonymous role.
            // - If the user is authenticated, but hasn't been assigned to any roles, allow
            //   them to access the site.
            // - Even though we allow authenticated users without any roles to access the
            //   site, the user will still usually not be able to view any published posts.
            //   It is ideal that all users are assigned to a role, even if that role has
            //   minimal rights such as ViewPublicPosts.

           
         

            this.MasterPageFile = string.Format("{0}themes/{1}/site.master", Utils.ApplicationRelativeWebRoot, ApplicationSettings.Instance.GetThemeWithAdjustments(null));

            base.OnPreInit(e);

            if (this.Page.IsPostBack)
            {
                return;
            }

          
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Page.PreRenderComplete"></see> event after 
        ///     the <see cref="M:System.Web.UI.Page.OnPreRenderComplete(System.EventArgs)"></see> event and before the page is rendered.
        /// </summary>
        /// <param name="e">
        /// An <see cref="T:System.EventArgs"></see> that contains the event data.
        /// </param>
        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);
            if (ApplicationSettings.Instance.UseApplicationNameInPageTitles)
            {
                this.Page.Title = string.Format("{0} | {1}", ApplicationSettings.Instance.ApplicationName, this.Page.Title);
            }
        }

        /// <summary>
        /// Initializes the <see cref="T:System.Web.UI.HtmlTextWriter"></see> object and calls on the child
        ///     controls of the <see cref="T:System.Web.UI.Page"></see> to render.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="T:System.Web.UI.HtmlTextWriter"></see> that receives the page content.
        /// </param>
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(new RewriteFormHtmlTextWriter(writer));
        }

        #endregion
    }
}