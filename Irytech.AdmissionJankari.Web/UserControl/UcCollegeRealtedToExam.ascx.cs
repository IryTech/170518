using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcCollegeRealtedToExam : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (IsPostBack) return;
            PageSize = ApplicationSettings.Instance.CollegePageSize;
            PageNum = 1;
            BindCollegeDetails();
        }



        private void BindCollegeDetails()
        {
            try
            {
                string searchPattern = "";
               int    errorCount=0;
                int recordsCount = 0;
              var collegeData = CollegeProvider.Instance.GetCollegeListByCourseExamStateCIty(0, 0, ExamId, CourseId, 0, out errorCount, out searchPattern, PageNum, out recordsCount, PageSize);
              if (collegeData.Count > 0)
              {
                  CollegeRealtedToExam.Visible = true;
                  rptCollegeRealtedToExam.DataSource = collegeData;
                  rptCollegeRealtedToExam.DataBind();
                  BuildPagination();           
              }
              else
              {
                  CollegeRealtedToExam.Visible = false;
              }
                                                                                             
                                                                                              
                                                                       
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindCollegeDetails in UcCollegeRealtedToExam.axcx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        public int CourseId
        {
            get;
            set;
        }
        public string ExamName
        {
            set { lblExamName.Text = value; }
        }
        public int ExamId
        {
            get;
            set;
        }
        private int PageNum
        {
            get { return Convert.ToInt16(ViewState["PageNum"]); }
            set { ViewState["PageNum"] = value; }
        }

        private int PageSize
        {
            get { return Convert.ToInt16(ViewState["PageSize"]); }
            set { ViewState["PageSize"] = value; }
        }
        #region Pager Creation
        protected void lnkPager_Click(object sender, EventArgs e) //Page index changed function
        {

            var ObjClsCommon = new Common();
            var lnk = (LinkButton)sender;

            CurrentPageIndex = int.Parse(lnk.CommandArgument);
            if (CurrentPageIndex == 0)
            {
                PageNum = 1;
            }
            else
            {
                PageNum = CurrentPageIndex + 1;
            }
            BindCollegeDetails();


        }

        private int ButtonsCount //how many total linkbuttons shld be shown
        {
            get { return 10; }
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
            lnk.ID = index.ToString();
            lnk.Text = title;
            lnk.CommandArgument = index.ToString();
            lnk.Click += new EventHandler(lnkPager_Click);
            lnk.CssClass = "pager";
            //lnk.Width = 8;
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
                        var lbl = new Label { Text = (i + 1).ToString() };
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

       
    }
}