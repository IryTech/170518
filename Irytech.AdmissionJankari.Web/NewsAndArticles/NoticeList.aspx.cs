using System;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.Components.Web.Controls;
using System.Web.UI.HtmlControls;
namespace IryTech.AdmissionJankari20.Web.NewsAndArticles
{
    public partial class NoticeList : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildPagination();
            if (IsPostBack) return;
            PageSize = ApplicationSettings.Instance.NewsArticlePageSize;
            PageNum = 1;
            BindNoticeList(); 
            BindPageTitleAndKeyWords();
        }
        public int PageNum
        {
            get { return Convert.ToInt16(ViewState["PageNum"]); }
            set { ViewState["PageNum"] = value; }
        }

        public int PageSize
        {
            get { return Convert.ToInt16(ViewState["PageSize"]); }
            set { ViewState["PageSize"] = value; }
        }
        // to show page title ,keyword and description
        private void BindPageTitleAndKeyWords()
        {
            var objPage=new Common().GetPageTitleKeyWordAndDecsription("Notice");
       
            try
            {
                if (objPage.Rows.Count > 0)
                {

                    Page.Title = "";
                    Page.Title = Convert.ToString(objPage.Rows[0]["AjPageTitle"].ToString());

                    var metadesc = new HtmlMeta();
                    metadesc.Attributes.Clear();
                    metadesc.Name = "description";

                    metadesc.Content = Convert.ToString(objPage.Rows[0]["AjPageDescription"].ToString());

                    Page.Header.Controls.Add(metadesc);

                    var metaKeywords = new HtmlMeta();
                    metaKeywords.Name = "keywords";

                    metaKeywords.Content = Convert.ToString(objPage.Rows[0]["AjPageKeyword"].ToString());

                    Page.Header.Controls.Add(metaKeywords);
                }

            }
            catch (Exception Ex)
            {
                string err = Ex.Message;
                if (Ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + Ex.InnerException.Message;
                }
                string addInfo = "Error While fetching BindPageTitleAndKeyWords in NoticeList.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
       
        private void BindNoticeList()
        {
            try
            {
                var recordsCount = 0;
                var data = NewsArticleNoticeProvider.Instance.GetAllNoticeListByPaging(out recordsCount, PageNum, PageSize);
                data = data.Where(notice => notice.NoticeStatus == true).ToList();
                if (data.Count > 0)
                {
                    rptNotice.Visible = true;

                    pnlPager.Visible = true;
                    if (recordsCount == 0)
                    {
                        recordsCount = data.Count;
                    }
                    rptNotice.DataSource = data;
                    rptNotice.DataBind();
                        TotalRecord = recordsCount;
                        PageCount = TotalRecord / PageSize;
                        int temp = TotalRecord % PageSize;
                        if (temp > 0)
                            PageCount = PageCount + 1;

                        BuildPagination();

                    }

                    else
                    {

                        pnlPager.Visible = false;
                        PageNum = 1;
                        CurrentPageIndex = 0;
                    }
                
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindNoticeList in NoticelIst.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected string GetShortDesc(object desc)
        {
            if(!string.IsNullOrEmpty(desc.ToString()))
            {
                if (desc.ToString().Length > ApplicationSettings.Instance.DescriptionCharactersNewsAtricle)
                {
                    return desc.ToString().Substring(0, ApplicationSettings.Instance.DescriptionCharactersNewsAtricle);
                }
                else
                {
                    return desc.ToString();
                }
            }
            else
            {
                return "N/A";
            }
        }
        #region Pager Creation
        protected void lnkPager_Click(object sender, EventArgs e) //Page index changed function
        {

          
            var lnk = (LinkButton)sender;

            CurrentPageIndex = int.Parse(lnk.CommandArgument);
            if (CurrentPageIndex == 0)
                PageNum = 1;
            else
                PageNum = CurrentPageIndex + 1;

            BindNoticeList();
            BindPageTitleAndKeyWords();
        
        }

        private int ButtonsCount //how many total linkbuttons shld be shown
        {
            get { return ApplicationSettings.Instance.NewsArticlePageCount; }
        }

        private string FirstPageText
        {
            get { return "First"; }
        }

        private int CurrentPageIndex //to store the current page index
        {
            get { return ViewState["CurrentPageIndex"] == null ? 0 : int.Parse(ViewState["CurrentPageIndex"].ToString()); }
            set { ViewState["CurrentPageIndex"] = value; }
        }
        private int PageCount  //total number of pages needed to display the data
        {
            get { return ViewState["PageCount"] == null ? 0 : int.Parse(ViewState["PageCount"].ToString()); }
            set { ViewState["PageCount"] = value; }
        }

        private LinkButton CreateButton(string title, int index)
        {
            var lnk = new LinkButton();
            lnk.ID ="Notice"+index.ToString();
            lnk.Text = title;
            lnk.CommandArgument = index.ToString();
            lnk.Click += new EventHandler(lnkPager_Click);
            lnk.CssClass = "pager";
          
            return lnk;
        }

        //create the linkbuttons for pagination
        protected void BuildPagination()
        {
            pnlPager.Controls.Clear(); //

            if (PageCount <= 1) return; // at least two pages should be there to show the pagination

            //finding the first linkbutton to be shown in the current display
            var start = CurrentPageIndex - (CurrentPageIndex % ButtonsCount);

            //finding the last linkbutton to be shown in the current display
            var end = CurrentPageIndex + (ButtonsCount - (CurrentPageIndex % ButtonsCount));

            //if the start button is more than the number of buttons. If the start button is 11 we have to show the <<First link
            if (start > ButtonsCount - 1)
            {
                pnlPager.Controls.Add(CreateButton(FirstPageText, 0));
                pnlPager.Controls.Add(CreateButton("..", start - 1));

            }

            int i = 0, j = 0;

            for (i = start; i < end; i++)
            {

                if (i < PageCount)
                {
                    if (i == CurrentPageIndex) //if its the current page
                    {
                        var lbl = new Label
                        {
                            Text = (i + 1).ToString()
                        };
                        pnlPager.Controls.Add(lbl);

                    }
                    else
                    {
                        pnlPager.Controls.Add(CreateButton((i + 1).ToString(), i));

                    }
                }
                j++;
            }

            //If the total number of pages are greaer than the end page we have to show Last>> link
            if (PageCount > end)
            {
                pnlPager.Controls.Add(CreateButton("..", i));
                pnlPager.Controls.Add(CreateButton("&raquo;&raquo;", PageCount - 1));

            }


        }
        #endregion

        private int TotalRecord
        {
            get { return Convert.ToInt16(ViewState["TotalRecords"]); }
            set { ViewState["TotalRecords"] = value; }
        }

    }
}