using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace IryTech.AdmissionJankari20.Web.App_Code.Control
{
    public class PagerList : System.Web.UI.Control
    {
        public Literal LitPager { get; set; }

        #region Private Members
        private string ulClassName = string.Empty;
        private string pageSymbol = "p";
        private string baseUrl;
        private string AbsolutePath { get; set; }
        private string Query { get; set; }
        private int _CurrentPageIndex
        {
            get { return ViewState["CurrentPageIndex"] == null ? 0 : (int)ViewState["CurrentPageIndex"]; }
            set { ViewState["CurrentPageIndex"] = value; }
        }
        #endregion

        #region Default Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public void TransTutorPagerControl()
        {
            PageSymbol = string.IsNullOrEmpty(PageSymbol) ? "p" : PageSymbol;
            UlClassName = "transtutor-pagination";
            OutputPageStats = true;
            ShowEdgeEntries = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [output page stats].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [output page stats]; otherwise, <c>false</c>.
        /// </value>
        public bool OutputPageStats { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [Show edge entries].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [Show edge entries]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowEdgeEntries { get; set; }

        #endregion

        #region Public Properties

        public string URL { get; set; }

        [Category("Appearance"), DefaultValue("p"), Description("Paging symbol dispayed in the URL/_links")]
        public string PageSymbol
        {
            get { return pageSymbol; }
            set { pageSymbol = value; }
        }

        [Category("Misc"), Description("The total number of pages in the data source")]
        public int PageCount
        {
            get
            {
                object obj = ViewState["PageCount"];
                if (obj != null) return (int)obj;
                return 25;
            }
            set { ViewState["PageCount"] = value; }
        }

        /// <summary>
        /// The number of items per page. The maximum number of pages is calculated by 
        /// dividing the number of items by items_per_page (rounded up, minimum 1). Default: 25
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize
        {
            get
            {
                object obj = ViewState["PageSize"];
                if (obj != null) return (int)obj;
                return 25;
            }
            set { ViewState["PageSize"] = value; }
        }

        [Category("Appearance"), Description("Gets or sets the base URL")]
        public string BaseUrl
        {
            get { return baseUrl; }
            set { baseUrl = value; }
        }

        [Category("Appearance"), DefaultValue("pagermenu"), Description("Gets or sets the style attribute for the main UL list")]
        public string UlClassName
        {
            get { return ulClassName; }
            set { ulClassName = value; }
        }

        /// <summary>
        /// Gets or sets the offset of the pagination number
        /// </summary>
        public int IndexDisplayOffset
        {
            get { return ViewState["IndexDisplayOffset"] == null ? 1 : (int)ViewState["IndexDisplayOffset"]; }
            set { ViewState["IndexDisplayOffset"] = value; }
        }

        /// <summary>
        /// Gets or sets the total number of rows in the datasource
        /// </summary>
        public int TotalItems
        {
            get { return ViewState["TotalItems"] == null ? 0 : (int)ViewState["TotalItems"]; }
            set { ViewState["TotalItems"] = value; }
        }

        /// <summary>
        /// The page that is selected when the pagination is initialized. Default: 0
        /// </summary>
        public int CurrentPageIndex
        {
            get { return _CurrentPageIndex + 1; }
            set { _CurrentPageIndex = value - 1; }
        }

        /// <summary>
        /// Maximum number of pagination links that are visible. Set to 0 to display a
        /// simple "Previous/Next"-Navigation. Default: 10
        /// </summary>
        public int DisplayEntriesCount
        {
            get { return ViewState["DisplayEntriesCount"] == null ? 10 : (int)ViewState["DisplayEntriesCount"]; }
            set { ViewState["DisplayEntriesCount"] = value; }
        }

        /// <summary>
        /// If this number is set to 1, links to the first and the last page are always shown
        /// , independent of the current position and the visibility constraints set by num_display_entries
        /// . You can set it to bigger numbers to show more links. Default is 2
        /// </summary>
        public int EdgeEntriesCount
        {
            get { return ViewState["EdgeEntriesCount"] == null ? 2 : (int)ViewState["EdgeEntriesCount"]; }
            set { ViewState["EdgeEntriesCount"] = value; }
        }

        /// <summary>
        /// SEO friendly link target of the pagination links when the PageMode = HyperLink. Default is empty
        /// </summary>
        public string TargetLinkFormatString
        {
            get { return ViewState["TargetLinkFormat"] == null ? string.Empty : ViewState["TargetLinkFormat"].ToString(); }
            set { ViewState["TargetLinkFormat"] = value; }
        }

        /// <summary>
        /// Text for the "Previous"-link that decreases the current page number by 1. 
        /// Leave blank to hide the link. Default: « prev
        /// </summary>
        public string PreviousPageText
        {
            get
            {
                return ViewState["PreviousPageText"] == null ? "&laquo; " +
                                                               "Previous" : ViewState["PreviousPageText"].ToString();
            }
            set { ViewState["PreviousPageText"] = value; }
        }

        /// <summary>
        /// Text for the "Next"-link that increases the current page number by 1. 
        /// Leave blank to hide the link. Default: next »
        /// </summary>
        public string NextPageText
        {
            get { return ViewState["NextPageText"] == null ? "Next &raquo;" : ViewState["NextPageText"].ToString(); }
            set { ViewState["NextPageText"] = value; }
        }

        /// <summary>
        /// When there is a gap between the numbers created by EdgeEntriesCount and the displayed number interval
        /// , this text will be inserted into the gap (inside a Label/span tag).
        /// </summary>
        public string EllipseText
        {
            get { return ViewState["EllipseText"] == null ? "..." : ViewState["EllipseText"].ToString(); }
            set { ViewState["EllipseText"] = value; }
        }

        #endregion

        #region On Load
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            if (!string.IsNullOrEmpty(URL))
            {
                Uri uri = new Uri(HttpContext.Current.Request + URL);
                AbsolutePath = uri.AbsolutePath;
                Query = uri.Query;
                BaseUrl = AbsolutePath + "/";
            }


            //if (!Page.IsPostBack)
            DataBind();

            base.OnLoad(e);
        }
        #endregion

        #region Data Bind
        /// <summary>
        /// Binds a data source to the invoked server control and all its child controls.
        /// </summary>
        public override void DataBind()
        {
            this.Controls.Clear();
            CreateChildControls();
            ChildControlsCreated = true;
            base.DataBind();

        }
        #endregion

        #region Create Child Controls
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            BindPager();
        }
        #endregion

        #region Bind Pager
        /// <summary>
        /// Binds the pager.
        /// </summary>
        protected void BindPager()
        {

            LitPager = new Literal();
            LitPager.ID = "litPager";
            this.Controls.Add(LitPager);
            string pagerHTML = string.Empty;
            if (PageCount == 0 || TotalItems == 0)
            {
                pagerHTML = "No data to display";
            }
            else
            {
                pagerHTML = CreateHyperLinkPagination();

            }

            LitPager.Text = pagerHTML;

        }
        #endregion

        #region Total Pages
        /// <summary>
        /// Function to calculate the total number of pages depending on TotalItems and PageSize
        /// </summary>
        /// <returns></returns>
        private int TotalPages()
        {
            return PageCount;
            //return int.Parse(Math.Ceiling(decimal.Parse(TotalItems.ToString()) / decimal.Parse(PageSize.ToString())).ToString());

        }
        #endregion



        #region Create Pagination Hyper Links
        /// <summary>
        /// Creates the hyper link pagination.
        /// </summary>
        /// <returns></returns>
        private string CreateHyperLinkPagination()
        {
            string ellipses = "<li class=\"plain\">" + EllipseText + "</li>";

            //this string builder will hold the pagination string and later we will add this to a div 
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<ul class=\"{0}\" />", UlClassName);
            //finidng the central postion of display entries. The current page will be shown in the center
            int ne_half = int.Parse(Math.Ceiling(decimal.Parse(DisplayEntriesCount.ToString()) / 2).ToString());

            //retrieving the number of pages
            int np = TotalPages();

            int upper_limit = np - DisplayEntriesCount;

            //finding the start position
            int start = _CurrentPageIndex > ne_half
                            ? Math.Max(Math.Min(_CurrentPageIndex - ne_half, upper_limit), 0)
                            : 0;

            //finding the end position
            int end = _CurrentPageIndex > ne_half
                          ? Math.Min(_CurrentPageIndex + ne_half, np)
                          : Math.Min(DisplayEntriesCount, np);

            //Pager Stats
            /*Comment the belw 3 lines based on condition*/
            if (OutputPageStats)
            {
                sb.Append("<div class=\"pagerstats\">");
                sb.AppendFormat("Page {0} of {1} ({2} items) ", CurrentPageIndex, TotalPages(), TotalItems);
                sb.Append("</div><br/>");
            }

            // Begin by creating the 'Previous' Link 
            if (PreviousPageText.Length > 0)
            {
                sb.Append(CreateLink(_CurrentPageIndex - 1, PreviousPageText, _CurrentPageIndex == 0 ? "disabled" : ""))
                    .Append(System.Environment.NewLine);
            }

            // Generate begining edge entries - The first EdgeEntriesCount of page links will be generated 
            if (start > 0 && EdgeEntriesCount > 0)
            {
                //till where the edge entries created
                int edgeEnd = Math.Min(EdgeEntriesCount, start);
                for (int i = 0; i < edgeEnd; i++)
                {
                    sb.Append(CreateLink(i, (i + 1).ToString(), "")).Append(System.Environment.NewLine);
                }
                //if there is a gap between edge entries and start,, ellipse text will be shown bw them
                if (EdgeEntriesCount < start && EllipseText.Length > 0)
                {
                    sb.Append(ellipses).Append(System.Environment.NewLine);
                }
            }

            // Generate interval links - the pagination links based on DisplayEntriesCount will generated here
            //links will be printed from the calculated start and end values
            for (int i = start; i < end; i++)
            {
                sb.Append(CreateLink(i, (i + 1).ToString(), "")).Append(System.Environment.NewLine);
            }

            if (ShowEdgeEntries)
            {
                // Generate ending edge entries - The final EdgeEntriesCount of page links will be generated 
                if (end < np && EdgeEntriesCount > 0)
                {
                    //if there is a gap between end link and edge entries the EllipseText will be shown
                    if (np - EdgeEntriesCount > end && EllipseText.Length > 0)
                    {
                        sb.Append(ellipses).Append(Environment.NewLine);
                    }

                    //from where the edge entries should start
                    int begin = Math.Max(np - EdgeEntriesCount, end);
                    for (int i = begin; i < np; i++)
                    {
                        sb.Append(CreateLink(i, (i + 1).ToString(), "")).Append(Environment.NewLine);
                    }
                }
            }
            // Finish the pagination with 'Next' Link
            if (NextPageText.Length > 0)
            {
                sb.Append(CreateLink(_CurrentPageIndex + 1, NextPageText, _CurrentPageIndex == end - 1 ? "disabled" : ""))
                    .Append(System.Environment.NewLine);
            }

            return sb.ToString();
        }


        /// <summary>
        /// Helper function for generating a single link (or a span tag if it's the current page)
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="displayText">The display text.</param>
        /// <param name="className">Name of the class.</param>
        /// <returns></returns>
        private string CreateLink(int pageNumber, string displayText, string className)
        {
            string link = "";
            //retrive total number of pages available
            int np = TotalPages();

            //if the passed value is < 0 set it as zero.. else if the passed value is greater than total pages set it as total pages - 1
            pageNumber = pageNumber < 0 ? 0 : (pageNumber < np ? pageNumber : np - 1);

            //if its the current page make it a span
            if (pageNumber == _CurrentPageIndex)
            {
                //if no class name is passed, set current as class name
                link = string.Format("<li class='{0}'>{1}</li>"
                                     , className.Length > 0 ? className : "current"
                                     , displayText);
            }
            else //make it as a link
            {
                if (TargetLinkFormatString.Length > 0) //if SEO friendly page target is specified - use it
                {
                    link = string.Format(TargetLinkFormatString, pageNumber + IndexDisplayOffset);
                    link = string.Format("<li><a href='{0}'>{1}</a></li>", link, displayText);
                }
                else
                    link = string.Format("<li><a href='{0}{1}'>{2}</a></li>", BaseUrl,
                                         pageNumber + IndexDisplayOffset, displayText);

            }

            return link;
        }

        #endregion



    }//end class
}
