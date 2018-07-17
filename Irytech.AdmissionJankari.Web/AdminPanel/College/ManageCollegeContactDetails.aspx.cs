using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;


namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class ManageCollegeContactDetails : System.Web.UI.Page
    {
        private Common _objCommon;

        protected void Page_Load(object sender, EventArgs e)
        {
            BuildPagination();
            if (IsPostBack) return;
            PageSize = 10;
            PageNum = 1;
            BindGroup();
            BindCourseList();
            BindStateList();
            BindCity(0);
            BindCollegeListByQuery("");

        }

      
        // Method to Get The List The Course
        private void BindCourseList()
        {
            _objCommon = new Common();
            try
            {
                // PageNum = 1;
                //CurrentPageIndex = 0;
                var data = CourseProvider.Instance.GetAllCourseList();
                data = data.Where(course => course.CourseStatus == true).ToList();
                if (data.Count > 0)
                {
                    ddlCourse.DataSource = data;
                    ddlCourse.DataTextField = "CourseName";
                    ddlCourse.DataValueField = "CourseId";
                    ddlCourse.DataBind();
                    ddlCourse.Items.Insert(0, new ListItem("--Select Course--", "0"));


                }
                else
                {
                    ddlCourse.Items.Insert(0, new ListItem("--Select Course--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCourse in ManageCollegeContactDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void BindCollegeListByQuery(string query)
        {
            int totalRecords = 0;
             var collegeData = CollegeProvider.Instance.GetCollegeForOnlineCounselling(query, PageSize, PageNum, out totalRecords);
            if (collegeData.Count > 0)
            {
                try
                {
                    pnlPager.Visible = true;
                    rptCollegeContactDetails.Visible = true;
                    if (totalRecords == 0)
                    {
                        totalRecords = collegeData.Count;
                    }
                    rptCollegeContactDetails.DataSource = collegeData;
                    rptCollegeContactDetails.DataBind();
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

                lblInfo.Visible = true;
                pnlPager.Visible = false;
                lblInfo.Text = new Common().GetErrorMessage("noRecords");
                lblInfo.CssClass = "info";
                rptCollegeContactDetails.Visible = false;

            }
        }

        protected void rptCollegeContactDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                _objCommon = new Common();
                var rptContactDetails = (Repeater) e.Item.FindControl("rptContactDetails");
                var hndCollegeBranchCourseId = (HiddenField) (e.Item.FindControl("hndCollegeBranchCourseId"));
                if (rptContactDetails != null)
                {
                    
                        var dt =
                            _objCommon.GetCollegeContactPersonDetailsByCollegeBrnachCourseId(
                                Convert.ToInt32(hndCollegeBranchCourseId.Value));
                        if (dt.Rows.Count > 0)
                        {
                            rptContactDetails.Visible = true;
                            rptContactDetails.DataSource = dt;
                            rptContactDetails.DataBind();

                        }
                        else
                        {
                            rptContactDetails.Visible = false;
                            var lblMsg = (Label) (e.Item.FindControl("lblMsg"));
                            lblMsg.Visible = true;
                            lblMsg.Text = string.Format("No Contact Details find for the college");
                        }
                    

                }
            }
        }


        private void BindGroup()
        {
            try
            {
                var data = CollegeProvider.Instance.GetAllCollegeGroupList();
                if (data.Count > 0)
                {
                    ddlCollegeGroup.DataSource = data;
                    ddlCollegeGroup.DataTextField = "CollegeGroupName";
                    ddlCollegeGroup.DataValueField = "CollegeGroupId";
                    ddlCollegeGroup.DataBind();
                    ddlCollegeGroup.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlCollegeGroup.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindGroup in UpdateCollegeDetails  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        // Method to Bind the State List
        private void BindStateList()
        {
            var state = StateProvider.Instance.GetAllState();
            try
            {
                
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
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindStateList in ManageCollegeContactDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindCity(int stateId)
        {
            try
            {
                List<CityProperty> data;
                data = stateId == 0 ? CityProvider.Instacnce.GetAllCityList() : CityProvider.Instacnce.GetCityListByState(stateId);
                if (data.Count > 0)
                {

                    ddlCity.DataSource = data;
                    ddlCity.DataTextField = "CityName";
                    ddlCity.DataValueField = "CityId";
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, new ListItem("--Select City--", "0"));
                }
                else
                {
                    ddlCity.Items.Insert(0, new ListItem("--Select City--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCity in UpdateCollegeDetails  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private string Search()
        {
            lblInfo.Visible = false;
            var dbQuery = "";
            if (ddlCourse.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(dbQuery))
                {
                    dbQuery += " and AjCollegeBranchCourseMaster.AjCourseId= " +
                               ddlCourse.SelectedValue;
                }
                else
                {
                    dbQuery += " and  AjCollegeBranchCourseMaster.AjCourseId=" +
                               ddlCourse.SelectedValue;

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
            if (ddlState.SelectedIndex > 0)
            {
                ddlCity.Items.Clear();
                BindCity(Convert.ToInt32(ddlState.SelectedValue));
            }
            var dbQuery = "";
            dbQuery = Search();
            if (!string.IsNullOrEmpty(dbQuery))
            {
                hdnQuery.Value = dbQuery;
                BindCollegeListByQuery(hdnQuery.Value);
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

        protected void rptCollegeContactDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                btnContactInsert.Text = "Insert";
                if (e.CommandName == "Insert")
                {
                    hdnCollegeCourseId.Value = e.CommandArgument.ToString();
                    var basicDetails =
                        CollegeProvider.Instance.GetCollegeBasicDetailsByBranchCourseId(
                            Convert.ToInt32(hdnCollegeCourseId.Value));
                    if (basicDetails.Count > 0)
                    {
                        var query = basicDetails.Select(result => new
                        {
                            result.CollegeBranchName,
                            result.CollegeIdBranchId,
                            result.CollegeGroupId,

                        }).First();
                        collegeName.InnerText = query.CollegeBranchName;
                        hdnCollegeId.Value = query.CollegeIdBranchId.ToString(CultureInfo.InvariantCulture);
                        ddlCollegeGroup.SelectedValue = query.CollegeGroupId.ToString(CultureInfo.InvariantCulture);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>OpenPopForContact();</script>", false);
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error While fetching rptCollegeContactDetails_ItemCommand in ManageCollegeContactDetails.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void btnContactInsert_Click(object sender, EventArgs e)
        {
            var errMsg = "";
            var objCommon = new Common();
            var result = 0;
            try
            {
                if (btnContactInsert.Text == "Insert")
                {
                    result = objCommon.InsertUpdateCollegeContactDetails(Convert.ToInt32(hdnCollegeId.Value),
                                                                             Convert.ToInt32(hdnCollegeCourseId.Value),
                                                                             Convert.ToInt32(
                                                                                 ddlCollegeGroup.SelectedValue),
                                                                             new SecurePage().LoggedInUserId,
                                                                             txtName.Text.Trim(),
                                                                             txtDesignation.Text.Trim(),
                                                                             txtCollegeMobile.Text.Trim(),
                                                                             txtCollegePhone.Text.Trim(),
                                                                             txtEmailId.Text.Trim(),
                                                                             txtCollegeFax.Text.Trim(),
                                                                             txtDepartment.Text.Trim(),
                                                                             chkStatus.Checked,
                                                                             out errMsg, 0);
                }
                else
                {
                 result = objCommon.InsertUpdateCollegeContactDetails(Convert.ToInt32(hdnCollegeId.Value),
                                                                               Convert.ToInt32(hdnCollegeCourseId.Value),
                                                                               Convert.ToInt32(
                                                                                   ddlCollegeGroup.SelectedValue),
                                                                               new SecurePage().LoggedInUserId,
                                                                               txtName.Text.Trim(),
                                                                               txtDesignation.Text.Trim(),
                                                                               txtCollegeMobile.Text.Trim(),
                                                                               txtCollegePhone.Text.Trim(),
                                                                               txtEmailId.Text.Trim(),
                                                                               txtCollegeFax.Text.Trim(),
                                                                               txtDepartment.Text.Trim(),
                                                                               chkStatus.Checked,
                                                                               out errMsg, Convert.ToInt32(hdnFacalityId.Value));
                    
                }
                if (result > 0)
                {
                    lblInfo.CssClass = "success";
                    lblInfo.Visible = true;
                    lblInfo.Text = errMsg;
                    BindCollegeListByQuery(hdnQuery.Value);
                }
                else
                {
                    lblInfo.CssClass = "info";
                    lblInfo.Visible = true;
                    lblInfo.Text = errMsg; 
                }
                CLearFields();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>Close()</script>", false);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  btnContactInsert_Click in ManageCollegeContactDetails.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        protected void rptContactDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var objCommon = new Common();
            try
            {
                if (e.CommandName == "Edit")
                {
                    hdnFacalityId.Value = e.CommandArgument.ToString();
                    var basicDetails =
                        objCommon.GetCollegeContactPersonDetailsByPersonId(
                            Convert.ToInt32(hdnFacalityId.Value));
                    if (basicDetails.Rows.Count > 0)
                    {

                        collegeName.InnerText = Convert.ToString(basicDetails.Rows[0]["AjCollegeBranchName"].ToString());
                        hdnCollegeCourseId.Value = Convert.ToString(basicDetails.Rows[0]["AjCollegeBranchCourseId"].ToString());
                        hdnCollegeId.Value = Convert.ToString(basicDetails.Rows[0]["AjCollegeBranchId"].ToString());
                        txtName.Text = Convert.ToString(basicDetails.Rows[0]["AJCollegeContactPersonName"].ToString());
                        txtDesignation.Text = Convert.ToString(basicDetails.Rows[0]["AjCollegePersonDegisnation"].ToString());
                        txtCollegeMobile.Text = Convert.ToString(basicDetails.Rows[0]["AjCollegeContactPersonMobile"].ToString());

                        txtCollegePhone.Text = Convert.ToString(basicDetails.Rows[0]["AjCollegeContactPersonPhoneNo"].ToString());
                        txtEmailId.Text = Convert.ToString(basicDetails.Rows[0]["AjCollegeContactPersonEmail"].ToString());
                        txtCollegeFax.Text = Convert.ToString(basicDetails.Rows[0]["AjCollegeContactPersonFax"].ToString());
                        txtDepartment.Text = Convert.ToString(basicDetails.Rows[0]["AjCollegeContactPersonDept"].ToString());
                        chkStatus.Checked = Convert.ToBoolean(basicDetails.Rows[0]["AjCollegeContactPersonStatus"].ToString());
                        ddlCollegeGroup.SelectedValue =
                            !string.IsNullOrEmpty(basicDetails.Rows[0]["AjCollegeGroupId"].ToString())
                                ? Convert.ToString(basicDetails.Rows[0]["AjCollegeGroupId"].ToString())
                                : "0";
                        btnContactInsert.Text = "Update";
                   
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>OpenPopForContact();</script>", false);
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error While fetching rptCollegeContactDetails_ItemCommand in ManageCollegeContactDetails.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void CLearFields()
        {
            btnContactInsert.Text = "Insert";
            txtName.Text = string.Empty;
            txtDesignation.Text = string.Empty;
            txtCollegeFax.Text = string.Empty; txtCollegeMobile.Text = string.Empty;
            txtCollegePhone.Text = string.Empty;
            txtDepartment.Text = string.Empty;
            txtEmailId.Text = string.Empty;
            ddlCollegeGroup.ClearSelection();
            chkStatus.Checked = false;
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
        #region Pager Creation
        protected void lnkPager_Click(object sender, EventArgs e) //Page index changed function
        {

            var ObjClsCommon = new Common();
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
      

    }
}