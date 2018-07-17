using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class ListViewPaging : System.Web.UI.UserControl
    {
        public  event EventHandler PagerPageIndexChanged;
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildPagination();
        }
        #region Pager Creation
        protected void lnkPager_Click(object sender, EventArgs e) //Page index changed function
        {
            LinkButton lnk = (LinkButton)sender;
            CurrentPageIndex = int.Parse(lnk.CommandArgument);

            if (PagerPageIndexChanged != null)
                PagerPageIndexChanged(this, e);
                        
        }

       
        private int PageSize //total display per page
        {
            get;
            set;

        }


     
        private int ButtonsCount //how many total linkbuttons shld be shown
        {
            get;
            set;
        }

        private string FirstPageText
        {
            get { return "&lt;&lt;"; }
        }
        private string LastPageText
        {
            get { return "&gt;&gt;"; }
        }
        public void bindDataWithPaging(int PageSize, int PageCounts) //you can pass either DatList or Repeater to this function
        {
            PageSize = 5;
            ButtonsCount = 3;
            PageCount = 3;
            
            
            //create the linkbuttons for pagination
            BuildPagination();

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

        private LinkButton createButton(string title, int index)
        {
            LinkButton lnk = new LinkButton();
            lnk.ID = index.ToString();
            lnk.Text = title;
            lnk.CssClass = "pageButton";
            lnk.CommandArgument = index.ToString();
            lnk.Click += new EventHandler(lnkPager_Click);
            return lnk;
        }

        //create the linkbuttons for pagination
        protected void BuildPagination()
        {
            pnlPager.Controls.Clear(); //

            if (PageCount <= 1) return; // at least two pages should be there to show the pagination

            //finding the first linkbutton to be shown in the current display
            int start = CurrentPageIndex - (CurrentPageIndex % ButtonsCount);

            //finding the last linkbutton to be shown in the current display
            int end = CurrentPageIndex + (ButtonsCount - (CurrentPageIndex % ButtonsCount));

            //if the start button is more than the number of buttons. If the start button is 11 we have to show the <<First link
            if (start > ButtonsCount - 1)
            {
                pnlPager.Controls.Add(createButton(FirstPageText, 0));
                pnlPager.Controls.Add(createButton("..", start - 1));
            }

            int i = 0, j = 0;

            for (i = start; i < end; i++)
            {
                LinkButton lnk;
                if (i < PageCount)
                {
                    if (i == CurrentPageIndex) //if its the current page
                    {
                        Label lbl = new Label();
                        lbl.Text = (i + 1).ToString();
                        lbl.CssClass = "current";
                        pnlPager.Controls.Add(lbl);
                    }
                    else
                    {
                        pnlPager.Controls.Add(createButton((i + 1).ToString(), i));
                    }
                }
                j++;
            }

            //If the total number of pages are greaer than the end page we have to show Last>> link
            if (PageCount > end)
            {
                pnlPager.Controls.Add(createButton("..", i));
                pnlPager.Controls.Add(createButton("&gt;&gt;", PageCount - 1));
            }


        }
        #endregion

    }
}