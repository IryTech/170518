using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CustomPaging : System.Web.UI.UserControl
    {
        public event EventHandler PagerPageIndexChanged;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //pager_PageIndexChanged(this, e);
            }
            else
            {
                BuildPagination();
            }

        }

        #region Pager Creation

        private void LnkPagerClick(object sender, EventArgs e) //Page index changed function
        {
            var lnk = (LinkButton)sender;
            lnk.Focus();
            CurrentPageIndex = int.Parse(lnk.CommandArgument);

            if (PagerPageIndexChanged != null)
                PagerPageIndexChanged(this, e);

            //bindData();
        }

        public int TotalPageCount;

        public int PageSize //total display per page
        {
            get { return TotalPageCount; }
            set { TotalPageCount = value; }

        }


        public int TotalButtonCount;

        public int ButtonsCount //how many total linkbuttons shld be shown
        {
            get { return TotalButtonCount; }
            set { TotalButtonCount = value; }
        }

        public string FirstPageText
        {
            get { return "&laquo;&laquo;"; }
        }

        public string LastPageText
        {
            get { return "&raquo;&raquo;"; }
        }


        public void BindDataWithPaging(Control bindControl, DataTable dt, bool search = false, bool serialNo = true)
        //you can pass either DatList or Repeater to this function
        {
            if (serialNo)
            {
                if (!dt.Columns.Contains("SrNo"))
                {

                    var col = dt.Columns.Add("SrNo", Type.GetType("System.Int32"));
                    col.SetOrdinal(0); // to put the column in position 0;
                    var i = 1;
                    foreach (DataRow drow in dt.Rows)
                    {
                        drow["SrNo"] = i;
                        i++;
                    }
                }
            }


            // var dv = data.DefaultView;
            //int a = CurrentPageIndex;
            CurrentPageIndex = !search ? CurrentPageIndex : 0;
            var dsP = new PagedDataSource
                          {
                              AllowPaging = true,
                              DataSource = dt.DefaultView,
                              CurrentPageIndex = !search ? this.CurrentPageIndex : 0,
                              PageSize = PageSize
                          };
            //  !search ? CurrentPageIndex : 

            //  a = CurrentPageIndex;

            //Binding data to the controls
            var dataList = bindControl as DataList;
            if (dataList != null)
            {
                dataList.DataSource = dsP;
                bindControl.DataBind();
            }
            else
            {
                var repeater = bindControl as Repeater;
                if (repeater != null)
                {
                    repeater.DataSource = dsP;
                    bindControl.DataBind();

                }

                else
                {
                    var gridView = bindControl as GridView;
                    if (gridView != null)
                    {
                        gridView.DataSource = dsP;
                        bindControl.DataBind();

                    }
                }
            }
            PageSize = PageSize == 0 ? 5 : PageSize;
            //saving the total page count in Viewstate for later use
            //PageCount = dt.Rows.Count/PageSize;
            //var temp = dt.Rows.Count / PageSize;
            //if (temp > 0)
            //    PageCount = PageCount + 1;
            PageCount = dt.Rows.Count % PageSize == 0 ? dt.Rows.Count / PageSize : dt.Rows.Count / PageSize + 1;
            //create the linkbuttons for pagination
            BuildPagination();


        }

        public int CurrentPageIndex //to store the current page index
        {
            get { return ViewState["CurrentPageIndex"] == null ? 0 : int.Parse(ViewState["CurrentPageIndex"].ToString()); }
            set { ViewState["CurrentPageIndex"] = value; }
        }

        private int PageCount //total number of pages needed to display the data
        {
            get { return ViewState["PageCount"] == null ? 0 : int.Parse(ViewState["PageCount"].ToString()); }
            set { ViewState["PageCount"] = value; }
        }

        private LinkButton CreateButton(string title, int index)
        {
            var lnk = new LinkButton
                          {
                              ID = index.ToString(CultureInfo.InvariantCulture),
                              Text = title,
                              CommandArgument = index.ToString(CultureInfo.InvariantCulture)
                          };
            lnk.Click += new EventHandler(LnkPagerClick);
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
            var end = CurrentPageIndex +
                      (ButtonsCount - (CurrentPageIndex % ButtonsCount));

            //if the start button is more than the number of buttons. If the start button is 11 we have to show the <<First link
            if (start > ButtonsCount - 1)
            {
                pnlPager.Controls.Add(CreateButton(FirstPageText, 0));

                pnlPager.Controls.Add(CreateButton("..", start - 1));
            }

            int i, j = 0;

            for (i = start; i < end; i++)
            {
                if (i < PageCount)
                {
                    if (i == CurrentPageIndex) //if its the current page
                    {
                        var lbl = new Label
                                      {
                                          Text = (i + 1).ToString(CultureInfo.InvariantCulture),
                                          CssClass = "current"
                                      };
                        pnlPager.Controls.Add(lbl);
                    }
                    else
                    {
                        pnlPager.Controls.Add(CreateButton((i + 1).ToString(CultureInfo.InvariantCulture), i));
                    }
                }
                j++;
            }

            //If the total number of pages are greaer than the end page we have to show Last >> link
            if ((PageCount - 1) >= end)
            {
                if ((PageCount - 1) > end)
                    pnlPager.Controls.Add(CreateButton("..", i));
                pnlPager.Controls.Add(CreateButton(LastPageText, PageCount - 1));


            }


        }


        #endregion


    }
}
