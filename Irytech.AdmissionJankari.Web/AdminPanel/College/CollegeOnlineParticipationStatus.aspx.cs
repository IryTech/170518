using System;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Linq;
using System.Collections.Generic;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class CollegeOnlineParticipationStatus : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildPagination();
     
            var args = Request["__EVENTARGUMENT"];
            if (!string.IsNullOrEmpty(args) && args == "reload")
            {
                lblSuccess.Visible = true;
                lblSuccess.CssClass = "success";
                lblSuccess.Text = string.Format("Participation is updated successfully");
                BindCollegeListByQuery(hdnQuery.Value);
            }

            if (IsPostBack) return;
            PageSize = 10;
            PageNum = 1;
            BindCollegeListByQuery(hdnQuery.Value);
            BindCourseList();
            BindState();
            BindCity(0);


        }



        private void BindCourseList()
        {
            var courseData = CourseProvider.Instance.GetAllCourseList();
            if (courseData.Count > 0)
            {
                ddlCourseList.DataSource = courseData;
                ddlCourseList.DataTextField = "CourseName";
                ddlCourseList.DataValueField = "CourseId";
                ddlCourseList.DataBind();
                ddlCourseList.Items.Insert(0, new ListItem("--Select Course--", "0"));
            }
            else
            {
                ddlCourseList.Items.Insert(0, new ListItem("--Select Course--", "0"));

            }

        }

        private void BindState()
        {
            var state = StateProvider.Instance.GetAllState();
            if (state.Count > 0)
            {
                ddlState.DataSource = state;
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "StateId";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("--Select State--", "0"));
            }
            else
            {
                ddlState.Items.Insert(0, new ListItem("--Select State--", "0"));

            }
        }

        private void BindCity(int stateId)
        {
            try
            {
                var data = stateId == 0
                               ? CityProvider.Instacnce.GetAllCityList()
                               : CityProvider.Instacnce.GetCityListByState(stateId);
                if (data.Count > 0)
                {
                    ddlCity.DataSource = data;
                    ddlCity.DataTextField = "CityName";
                    ddlCity.DataValueField = "CityId";
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, new ListItem("--Select  City--", "0"));
                }
                else
                {
                    ddlCity.Items.Insert(0, new ListItem("--Select  City--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCity in CollegeOnLineParticipation.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }



        protected void RptCollegeListItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblSuccess.Visible = false;
            try
            {
                if (e.CommandName == "Edit")
                {
                    Response.Redirect("UpdateCollegeDetails.aspx?CollegeBranchId=" + e.CommandArgument, true);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  RptCollegeListItemCommand in CollegeList.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

       
        private void BindCollegeListByQuery(string query)
        {
            int totalRecords = 0;
            var collegeData = CollegeProvider.Instance.GetCollegeListByDynamicQuery(query, PageSize, PageNum, out totalRecords);
            if (collegeData.Count > 0)
            {
                try
                {
                    lblInfo.Visible = false;
                    pnlPager.Visible = true;
                    rptCollegeList.Visible = true;
                    if (totalRecords == 0)
                    {
                        totalRecords = collegeData.Count;
                    }
                    rptCollegeList.DataSource = collegeData;
                    rptCollegeList.DataBind();
                    TotalRecord = totalRecords;
                    PageCount = TotalRecord / PageSize;
                    int temp = TotalRecord % PageSize;
                    if (temp > 0)
                        PageCount = PageCount + 1;

                    BuildPagination();
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  BindCollegeListByQuery in CollegeList.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                pnlPager.Visible = false;

                lblInfo.Visible = true;
                lblInfo.Text = new Common().GetErrorMessage("noRecords");
                lblInfo.CssClass = "info";
                rptCollegeList.Visible = false;

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


            BindCollegeListByQuery(hdnQuery.Value);

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
        private string Search()
        {
            lblSuccess.Visible = false;
            var dbQuery = "";
            if (ddlCourseList.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(dbQuery))
                {
                    dbQuery += " and AjCollegeBranchCourseMaster.AjCourseId= " +
                               ddlCourseList.SelectedValue;
                }
                else
                {
                    dbQuery += " and  AjCollegeBranchCourseMaster.AjCourseId=" +
                               ddlCourseList.SelectedValue;

                }

            }

            if (ddlState.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(dbQuery))
                {
                    dbQuery += " and AjCollegeBranchMaster.AjCollegeBranchStateId= " +
                               ddlState.SelectedValue;
                }
                else
                {
                    dbQuery += " and  AjCollegeBranchMaster.AjCollegeBranchStateId=" +
                               ddlState.SelectedValue;

                }
            }

            if (ddlCity.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(dbQuery))
                {
                    dbQuery += " and AjCollegeBranchMaster.AjCollegeBranchCityId= " +
                               ddlCity.SelectedValue;
                }
                else
                {
                    dbQuery += " and  AjCollegeBranchMaster.AjCollegeBranchCityId=" +
                               ddlCity.SelectedValue;

                }

            }
            if (!string.IsNullOrEmpty(txtCollegeName.Text.Trim()))
            {
                if (!string.IsNullOrEmpty(dbQuery))
                {
                    dbQuery += " and AjCollegeBranchMaster.AjCollegeBranchName='" + txtCollegeName.Text.Trim() + "'";

                }
                else
                {
                    dbQuery += " and  AjCollegeBranchMaster.AjCollegeBranchName= '" + txtCollegeName.Text.Trim() + "'";

                }

            }


            return dbQuery;

        }

        protected void ddlCourseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dbQuery = "";
            dbQuery = Search();
            if (!string.IsNullOrEmpty(dbQuery))
            {
                hdnQuery.Value = dbQuery;
                BindCollegeListByQuery(hdnQuery.Value);
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dbQuery = "";
            dbQuery = Search();
            if (!string.IsNullOrEmpty(dbQuery))
            {
                hdnQuery.Value = dbQuery;
                BindCollegeListByQuery(hdnQuery.Value);
            }
            if (ddlState.SelectedIndex > 0)
            {
                BindCity(Convert.ToInt16(ddlState.SelectedValue));
            }
            else
            {
                BindCity(0);
            }
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dbQuery = "";
            dbQuery = Search();
            if (!string.IsNullOrEmpty(dbQuery))
            {
                hdnQuery.Value = dbQuery;
                BindCollegeListByQuery(hdnQuery.Value);
            }

        }

        protected void BtnSearchClick(object sender, EventArgs e)
        {
            var dbQuery = "";
            dbQuery = Search();
            if (!string.IsNullOrEmpty(dbQuery))
            {
                hdnQuery.Value = dbQuery;
                BindCollegeListByQuery(hdnQuery.Value);
            }
            else
            {
                BindCollegeListByQuery("");
            }

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
        private int TotalRecord
        {
            get { return Convert.ToInt16(ViewState["TotalRecords"]); }
            set { ViewState["TotalRecords"] = value; }
        }
    }
}