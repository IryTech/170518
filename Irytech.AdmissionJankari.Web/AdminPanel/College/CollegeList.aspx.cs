using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using System.Web.UI;
namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class CollegeList : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildPagination();
            if (IsPostBack) return;
            PageSize = 10;
            PageNum = 1;
            BindInstituteType();
             BindGroup();
             BindUniversity();
             BindManagement();
             hdnQuery.Value = " and AjCollegeBranchCourseMaster.AjCollegeSponser='0'";
             BindCollegeListByQuery(  hdnQuery.Value);
            BindCourseList();
            BindState(0);
            BindCity(0);
          
        }
        private void BindManagement()
        {
            var dv = ClsSingelton.GetManagement();
    
            ddlManagement.DataSource = dv;
            ddlManagement.DataTextField = "AjMasterValues";
            ddlManagement.DataValueField = "AjMasterValueId";
            ddlManagement.DataBind();
            ddlManagement.Items.Insert(0, new ListItem("Select Management", "0"));

        }
        private void BindInstituteType()
        {
            try
            {
                var data = CollegeProvider.Instance.GetAllInstituteTypeList();
                if (data.Count > 0)
                {
                    ddlInstituteType.DataSource = data;
                    ddlInstituteType.DataTextField = "InstituteType";
                    ddlInstituteType.DataValueField = "InstituteTypeId";
                    ddlInstituteType.DataBind();
                    ddlInstituteType.Items.Insert(0, new ListItem("Select College Type", "0"));
                }
                else
                {
                    ddlInstituteType.Items.Insert(0, new ListItem("Select College Type", "0"));

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindInstituteType in UpdateCollegeDetails  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
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

       
        private void BindCourseList()
        {
            var courseData = CourseProvider.Instance.GetAllCourseList();
            if (courseData.Count > 0)
            {
                ddlCourseList.DataSource = courseData;
                ddlCourseList.DataTextField = "CourseName";
                ddlCourseList.DataValueField = "CourseId";
                ddlCourseList.DataBind();
                ddlCourseList.Items.Insert(0, new ListItem("Select Course", "0"));
                ddlCourse.DataSource = courseData;
                ddlCourse.DataTextField = "CourseName";
                ddlCourse.DataValueField = "CourseId";
                ddlCourse.DataBind();
                ddlCourse.Items.Insert(0, new ListItem("Select Course", "0"));
            }

            else
            {
                ddlCourse.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlCourseList.Items.Insert(0, new ListItem("--Select--", "0"));
                
            }

        }
        private void BindState(int countryId)
        {
            try
            {
                List<StateProperty> data;
                data = countryId == 0 ? StateProvider.Instance.GetAllState() : StateProvider.Instance.GetStateByCountry(countryId);
                if (data.Count > 0)
                {
                    ddlState.DataSource = data;
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateId";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, new ListItem("Select State", "0"));

                
                }
                else
                {
                    ddlState.Items.Insert(0, new ListItem("--Select--", "0")); 
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindState in UpdateCollegeDetails  :: -> ";
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
                    ddlCity.Items.Insert(0, new ListItem("Select City", "0"));
                }
                else
                {
                     ddlCity.Items.Insert(0, new ListItem("Select City", "0"));
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
        private void BindCollegeListByQuery(string query)
        {
            int totalRecords = 0;
            var collegeData = CollegeProvider.Instance.GetCollegeListByDynamicQuery(query, PageSize, PageNum, out totalRecords);
            if (collegeData.Count > 0)
            {
                try
                {
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
               
                lblInfo.Visible = true;
                pnlPager.Visible = false;
                lblInfo.Text = new Common().GetErrorMessage("noRecords");
                lblInfo.CssClass = "info";
                rptCollegeList.Visible = false;

            }
        }

    
        private string Search()
        {
            PageSize = 10;
            PageNum = 1;
            var dbQuery = " and AjCollegeBranchCourseMaster.AjCollegeSponser='" + rbtSponser.SelectedValue + "'";
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
        protected void BtnSearchClick(object sender, EventArgs e)
        {
            var dbQuery = "";
            dbQuery = Search();
            if (!string.IsNullOrEmpty(dbQuery))
            {
                hdnQuery.Value = dbQuery;
                BindCollegeListByQuery(hdnQuery.Value);
            }
            else { BindCollegeListByQuery(""); }

        }

        protected void ddlCourseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dbQuery="";
            PageSize = 10;
            PageNum = 1;
          dbQuery=  Search();
            if (!string.IsNullOrEmpty(dbQuery))
            {
                hdnQuery.Value = dbQuery;
                BindCollegeListByQuery(hdnQuery.Value);
            }
            else { BindCollegeListByQuery(" and AjCollegeBranchCourseMaster.AjCollegeSponser='0'"); } 
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
            else { BindCollegeListByQuery(" and AjCollegeBranchCourseMaster.AjCollegeSponser='0'"); }
            if (ddlState.SelectedIndex > 0)
            {
                BindCity(Convert.ToInt16(ddlState.SelectedValue));
            }
            else { BindCity(0); }
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
            else { BindCollegeListByQuery(" and AjCollegeBranchCourseMaster.AjCollegeSponser='0'"); }

        }

        protected void BtnSubmitCollegeBasicInfoClick(object sender, EventArgs e)
        {
           
            try
            { var fileName ="";
                var imageName = Common.NoImageSubstitute;
                if (FileUpload3.HasFile)
                {
                    fileName = this.FileUpload3.FileName;
                    FileUpload3.SaveAs(Server.MapPath(new Common().GetFilepath("UniversityImage")+fileName));
                }
               
                var objCollegeBranchProperty = new CollegeBranchProperty
                {
                    InstituteTypeId = !string.IsNullOrEmpty(Convert.ToString(ddlInstituteType.SelectedValue)) ? Convert.ToInt16(ddlInstituteType.SelectedValue) : 0,

                    CollegeGroupId = !string.IsNullOrEmpty(Convert.ToString(ddlCollegeGroup.SelectedValue)) ? Convert.ToInt16(ddlCollegeGroup.SelectedValue) : 0,
                    CollegeBranchName =
                        txtCollegeBranch.Text != "" ? txtCollegeBranch.Text : "N/A",
                    CollegePopulaorName =
                        txtCollegePopularName.Text != ""
                            ? txtCollegePopularName.Text
                            : "N/A",
                    CollegeManagementTypeId = Convert.ToInt16(ddlManagement.SelectedValue),
                    CollegeBranchEst =
                        txtCollegeEst.Text != "" ? txtCollegeEst.Text : "N/A",
                    CollegeBranchDesc =
                        txtCollegeDesc.Text != "" ? txtCollegeDesc.Text : "N/A",
                    CollegeBranchAddrs = txtAddress.Text != "" ? txtAddress.Text : "N/A",
                    CollegeBranchMobileNo =
                        txtCollegeMobile.Text != "" ? txtCollegeMobile.Text : "N/A",
                    CollegeBranchPinCode =
                        txtPinCode.Text != "" ? txtPinCode.Text : "N/A",
                    CoillegeBranchEmailId =
                        txtEmailId.Text != "" ? txtEmailId.Text : "N/A",
                    CollegeBranchFax =
                        txtCollegeFax.Text != "" ? txtCollegeFax.Text : "N/A",
                    CollegeBranchWebsite =
                        txtCollegeWebsite.Text != "" ? txtCollegeWebsite.Text : "N/A",
                    CollegeBranchCountryId = Convert.ToInt16(hdnCountryInsert.Value),
                    CollegeBranchStateId = Convert.ToInt16(hdnStateInsert.Value),
                    CollegeBranchCityId = Convert.ToInt16(hdnCityInsert.Value),
                    CollegeBranchStatus = chkCollegeStatus.Checked,
                    CollegeBranchLogo = fileName
                };
                var errMsg = "";
                var collegeBranchId = 0;
                var result = CollegeProvider.Instance.InsertCollegeBranchInfo(objCollegeBranchProperty, new SecurePage().LoggedInUserId, out errMsg,
                                                                              out collegeBranchId);

                if (result > 0)
                {
                    InsertCourse(collegeBranchId);
                    lblResult.Visible = true;
                    lblResult.Text = errMsg;
                    lblResult.CssClass = "success";
                   
                }
                else
                {
                    lblResult.Visible = true;
                    lblResult.Text = errMsg;
                    lblResult.CssClass = "info";
                    
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BtnSubmitCollegeBasicInfoClick  in UpdateCollegeDetails.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


     
        private void BindUniversity()
        {
            var data = UniversityProvider.Instance.GetAllUniversityList();
            if (data != null && data.Count > 0)
            {
                ddlUniversity.DataSource = data;
                ddlUniversity.DataTextField = "UniversityName";
                ddlUniversity.DataValueField = "UniversityId";
                ddlUniversity.DataBind();
                ddlUniversity.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            else { ddlUniversity.Items.Insert(0, new ListItem("Please Select", "0")); }
        }

        private void InsertCourse(int collegeId)
        {try
            {
                var objCollegeBranchCourseProperty = new CollegeBranchCourseProperty
                                                         {
                                                             
                                                             CollegeBranchId =
                                                                 collegeId,
                                                             CourseId = Convert.ToInt16(ddlCourse.SelectedValue),
                                                             UniversityId =
                                                                 Convert.ToInt16(ddlUniversity.SelectedValue),
                                                             HasHostel = chkHasHostel.Checked,
                                                             CollegeBranchCourseDesc =
                                                                 txtCourseDesc.Value != "" ? txtCourseDesc.Value : "N/A",
                                                             CollegeBranchCourseEst =
                                                                 txtCourseEst.Value != ""
                                                                     ? txtCourseEst.Value
                                                                     : "N/A",
                                                             CollegeBranchCourseTitle = txtCourseTitle.Value != ""
                                                                                            ? txtCourseTitle.Value
                                                                                            : "N/A",
                                                             CollegeBranchCourseMetaDesc =
                                                                 txtCourseMetaDesc.Value != ""
                                                                     ? txtCourseMetaDesc.Value
                                                                     : "N/A",
                                                             CollegeBranchCourseMetaTag =
                                                                 txtCourseMetaTag.Value != ""
                                                                     ? txtCourseMetaTag.Value
                                                                     : "N/A",
                                                             CollegeBranchCourseUrl =
                                                                 txtCourseUrl.Value != "" ? txtCourseUrl.Value : "N/A",

                                                             CollegeBranchCourseStatus = chkCollegeCourse.Checked,
                                                             CollegeBranchCourseSponserStatus = chkSponserStatus.Checked

                                                         };
                var errMsg = "";
                var collegeCourseBranchId = 0;
                var result = CollegeProvider.Instance.UpdateCollegeBranchCourseInfo(objCollegeBranchCourseProperty, 1,
                                                                                    out errMsg,
                                                                                    out collegeCourseBranchId);
                if (result > 0)
                {
                    BindCollegeListByQuery("");
                    lblResult.Visible = true;
                    lblResult.Text = errMsg;
                    lblResult.CssClass = "success";
                   
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing CourseUpdate_Click in UpdateCollegeDetails  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            
        }
        protected void rbtSponser_SelectedIndexChanged(object sender, EventArgs e)
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
                BindCollegeListByQuery(" and AjCollegeBranchCourseMaster.AjCollegeSponser='0'");
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