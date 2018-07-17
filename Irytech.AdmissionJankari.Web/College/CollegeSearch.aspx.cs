using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.Components.Web.Controls;
using System.Web.UI.HtmlControls;



namespace IryTech.AdmissionJankari20.Web.College
{
    public partial class ColegeSearch : PageBase
    {
        
        private Common _objCommon=new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildPagination();
            if (IsPostBack) return;
            {
                hdnSearchPriorityListCount.Value = ApplicationSettings.Instance.SearchPriorityListCount;
                PageSize = ApplicationSettings.Instance.CollegePageSize;
                PageNum = 1;
                _objCommon.StateId = 0;
                _objCommon.ExamId = 0;
                _objCommon.CityId = 0;
                if (Request.QueryString["CourseId"] != null)
                    _objCommon.CourseId = Convert.ToInt16(Request.QueryString["CourseId"]);

                BindCourse();
                
                BindExam(Convert.ToInt16(_objCommon.CourseId));
                BindState();
                BindManagent();
                BindCity();
                if (Request.QueryString["CityId"] != null)
                {
                    _objCommon.CityId = Convert.ToInt32(Request.QueryString["CityId"]);
                    ddlCity.SelectedValue = Request.QueryString["CityId"];
                    var cityProperty = CityProvider.Instacnce.GetCityById(Convert.ToInt32(Request.QueryString["CityId"])).FirstOrDefault();
                    if (
                        cityProperty != null)
                        ddlState.SelectedValue = Convert.ToString(cityProperty.StateId);
                    BindPageTitleAndKeyWordsByCity(ddlCity.SelectedItem.Text);
                }
                if (Request.QueryString["ExamId"] != null)
                {
                    _objCommon.ExamId = Convert.ToInt32(Request.QueryString["ExamId"]);
                    ddlExam.SelectedValue = Request.QueryString["ExmId"];

                    BindPageTitleAndKeyWordsByExam(ddlExam.SelectedItem.Text);
                }
                if (Request.QueryString["StateId"] != null)
                {
                    _objCommon.StateId = Convert.ToInt32(Request.QueryString["StateId"]);
                    ddlState.SelectedValue = Request.QueryString["StateId"];
                    BindPageTitleAndKeyWordsByCity(ddlState.SelectedItem.Text);
                }
                if (Request.QueryString["CollegeName"] != null)
                {
                    BindPartiCularCollege(Request.QueryString["CollegeName"]);
                    BindPageTitleAndKeyWordsofCollege(Convert.ToString(Request.QueryString["CollegeName"]));
                }
                else
                {

                    FillCollegeSearch(_objCommon.CityId, _objCommon.StateId, _objCommon.ExamId, _objCommon.CourseId, 0);

                    if (_objCommon.CityId == 0 && _objCommon.ExamId == 0 && _objCommon.StateId == 0)
                        BindPageTitleAndKeyWords();
                   


                }

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
      
        public  void BindPageTitleAndKeyWords()
        {
         try
            {
                   Page.Title = "Top  " + new Common().CourseName + " Colleges in India | College Search - Admission Jankari";
               
                    var metadesc = new HtmlMeta();
                    metadesc.Attributes.Clear();
                    metadesc.Name = "description";

                    metadesc.Content =
                        "Find top  " + new Common().CourseName +
                        " college in india, admission criteria, helpline number, MQS/NRI seats availability, fee, rank, placement, contact details, available courses, " +
                        " hostel info etc - Admission Jankari";
                        Page.Header.Controls.Remove(metadesc);
                        Page.Header.Controls.Add(metadesc);

                    var metaKeywords = new HtmlMeta
                                           {
                                               Name = "keywords",
                                               Content =
                                                   "Top  " + new Common().CourseName + " Colleges, Best  " +
                                                   new Common().CourseName + " Colleges,   " +
                                                   new Common().CourseName + " College Search, Search  " +
                                                   new Common().CourseName + " Colleges, Top Private " +
                                                   new Common().CourseName + " College, Top Government  " +
                                                   new Common().CourseName + " Colleges, Top Ranked " +
                                                   new Common().CourseName + " College, Best Private " +
                                                   new Common().CourseName + " College, Best Government " +
                                                   new Common().CourseName + " College, Management Quota Seats"
                                           };

                    Page.Header.Controls.Remove(metaKeywords);
                    Page.Header.Controls.Add(metaKeywords);
           
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in CollegeSearch.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        public void BindPageTitleAndKeyWordsofCollege(string collegeName)
        {
            try
            {


                Page.Title = "Find complete details of  " + collegeName + " Colleges in India | College Search - Admission Jankari";

                var metadesc = new HtmlMeta();
                metadesc.Attributes.Clear();
                metadesc.Name = "description";

                metadesc.Content =
                    "Find details " + collegeName  +
                    " college in india, admission criteria, helpline number, MQS/NRI seats availability, fee, rank, placement, contact details, available courses, " +
                    " hostel info etc - Admission Jankari";
               
                Page.Header.Controls.Add(metadesc);

                var metaKeywords = new HtmlMeta
                {
                    Name = "keywords",
                    Content =
                        "Find  " + collegeName + " Colleges, Get   " +
                        collegeName + "Colleges courses , Get Fees " +
                        collegeName + " College Search, " +
                       collegeName + " College, Management Quota Seats,MQS/NRI seats availability, fee, rank, placement, contact details"
                };

                Page.Header.Controls.Remove(metaKeywords);
                Page.Header.Controls.Add(metaKeywords);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in CollegeSearch.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        public void BindPageTitleAndKeyWordsByCity(string city)
        {
            var objCommon = new Common();
            try
            {


                Page.Title = "Top  " + objCommon.CourseName + " Colleges in  " + city + "- Admission Jankari";

                var metadesc = new HtmlMeta();
                metadesc.Attributes.Clear();
                metadesc.Name = "description";

                metadesc.Content =
                    "Find " + city + " top " + objCommon.CourseName +
                    " college, admission criteria, helpline number, MQS/NRI seats availability, fee, rank, placement, contact details, available courses, hostel" +
                    " info etc - Admission Jankari";

                Page.Header.Controls.Add(metadesc);

                var metaKeywords = new HtmlMeta
                {
                    Name = "keywords",
                    Content =
                        "" + city + " Top " + objCommon.CourseName +
                    " Colleges, " + city + " Best " + objCommon.CourseName +
                    " Colleges, " + city + " " + objCommon.CourseName +
                    " College Search, Search " + city + " " + objCommon.CourseName +
                    " Colleges, Top Private " + objCommon.CourseName +
                    " College in " + city + " Top Government " + objCommon.CourseName +
                    " Colleges in  " + city + ", Top Ranked " + objCommon.CourseName +
                    " Colleges, Best Private " + objCommon.CourseName +
                    " College, Best Government " + objCommon.CourseName +
                    " College"
                };


                Page.Header.Controls.Add(metaKeywords);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in CollegeSearch.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        public void BindPageTitleAndKeyWordsByExam(string exam)
        {
            var objCommon = new Common();
            try
            {


                Page.Title = "Top " + exam + " Colleges for  " + objCommon.CourseName + "-Admission Jankari";

                var metadesc = new HtmlMeta();
                metadesc.Attributes.Clear();
                metadesc.Name = "description";

                metadesc.Content =
                    "Find " + exam + " top " + objCommon.CourseName +
                    " colleges, admission criteria, helpline number, MQS/NRI seats availability, fee, rank, placement, contact details, available courses, hostel" +
                    " info etc - Admission Jankari";

                Page.Header.Controls.Add(metadesc);

                var metaKeywords = new HtmlMeta
                {
                    Name = "keywords",
                    Content =
                        "" + exam + " Top " + objCommon.CourseName +
                    " Colleges, " + exam + " Best " + objCommon.CourseName +
                    " Colleges, " + exam + " " + objCommon.CourseName +
                    " College Search, Search " + exam + " " + objCommon.CourseName +
                    " Colleges, Top Private " + objCommon.CourseName +
                    " College for " + exam + " exam Top Government " + objCommon.CourseName +
                    " Colleges for  " + exam + " exam,  Top Ranked " + objCommon.CourseName +
                    " Colleges, Best Private " + exam +
                    " Colleges, Best Government " + exam +
                    " College"
                };


                Page.Header.Controls.Add(metaKeywords);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in CollegeSearch.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindCourse()
        {
            _objCommon = new Common();
            try
            {
                PageNum = 1;
                CurrentPageIndex = 0;
                var data = CourseProvider.Instance.GetAllCourseList();
                data = data.Where(course => course.CourseStatus == true).ToList();
                if (data.Count > 0)
                {
                    ddlCourse.DataSource = data;
                    ddlCourse.DataTextField = "CourseName";
                    ddlCourse.DataValueField = "CourseId";
                    ddlCourse.DataBind();
                   ddlCourse.SelectedValue = Convert.ToString(_objCommon.CourseId);
                   _objCommon.CourseName =  Utils.RemoveIllegealFromCourse(ddlCourse.SelectedItem.Text);
                   lblCourse.InnerHtml = ddlCourse.SelectedItem.Text;
                   hdnCourseId.Value = Convert.ToString(_objCommon.CourseId);
                   hdnCourseName.Value = Utils.RemoveIllegealFromCourse(Convert.ToString(_objCommon.CourseName));
                   
                }
                else
                {
                    ddlCourse.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCourse in CollegeSearch.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindState()
        {
            try
            {
                var data = StateProvider.Instance.GetAllState();

                if (data.Count > 0)
                {
                    ddlState.DataSource = data;
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateId";
                    ddlState.DataBind(); ddlState.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlState.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindState in CollegeSearch.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindCity()
        {
            try
            {
                var data = CityProvider.Instacnce.GetAllCityList().OrderBy(result=>result.CityName).ToList();
                if (data.Count > 0)
                {
                    ddlCity.DataSource = data;
                    ddlCity.DataTextField = "CityName";
                    ddlCity.DataValueField = "CityId";
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlCity.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCity in CollegeSearch.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindExam(int courseId)
        {
            try
            {
                var data = ExamProvider.Instance.GetExamListByCourseId(Convert.ToInt32(courseId)).OrderBy(result=>result.ExamName).ToList();
                if (data.Count > 0)
                {
                    ddlExam.DataSource = data;
                    ddlExam.DataTextField = "ExamName";
                    ddlExam.DataValueField = "ExamId";
                    ddlExam.DataBind();
                    ddlExam.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlExam.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindExam in CollegeSearch.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindManagent()
        {
            try
            {
                var dv = ClsSingelton.GetManagement();

                if (dv.Count > 0)
                {
                    ddlManagement.DataSource = dv;
                    ddlManagement.DataTextField = "AjMasterValues";
                    ddlManagement.DataValueField = "AjMasterValueId";
                    ddlManagement.DataBind();
                    ddlManagement.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlManagement.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindManagent in CollegeSearch.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        
        private void BindPartiCularCollege(string collegeName)
        {
            try
            {
                var collegeDataByName =
                    CollegeProvider.Instance.GetCollegeCourseListByCollegeName(collegeName.Trim()).Where(college=>college.CollegeBranchStatus==true).ToList();

                if (collegeDataByName.Count > 0)
                {
                    rptCollegeDetails.Visible = true;
                    rptCollegeDetails.DataSource = collegeDataByName;
                    rptCollegeDetails.DataBind();

                }
                else
                {
                    lblSuccess.Visible = true;
                    lblSuccess.CssClass = "info";
                    lblSuccess.Text = new Common().GetErrorMessage("noRecords");
                    rptCollegeDetails.Visible = false;
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
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindPartiCularCollege in CollegeSearch.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        protected void DdlExamSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PageNum = 1;
                CurrentPageIndex = 0;
                if (ddlExam.SelectedIndex > 0)
                {

                    lblExam.InnerText = ddlExam.SelectedItem.Text;
                    _objCommon.ExamId = Convert.ToInt16(ddlExam.SelectedValue);
                    FillCollegeSearch(_objCommon.CityId, _objCommon.StateId, _objCommon.ExamId, _objCommon.CourseId,
                                      Convert.ToInt32(ddlManagement.SelectedValue));
                    BindPageTitleAndKeyWordsByCity(ddlExam.SelectedItem.Text);

                }
                else
                {
                    _objCommon.ExamId = Convert.ToInt16(ddlExam.SelectedValue);
                    FillCollegeSearch(_objCommon.CityId, _objCommon.StateId, _objCommon.ExamId, _objCommon.CourseId,
                                      Convert.ToInt32(ddlManagement.SelectedValue));
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp",
                                                    "<script type='text/javascript'>SearchPriorityListCollege(" +
                                                    _objCommon.CourseId + "," + _objCommon.StateId + "," +
                                                    _objCommon.CityId + "," + _objCommon.ExamId + "," +
                                                    Convert.ToInt32(ddlManagement.SelectedValue) + ");</script>", false);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DdlExamSelectedIndexChanged in CollegeSearch.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void DdlStateSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PageNum = 1;
                CurrentPageIndex = 0;
                if (ddlState.SelectedIndex > 0)
                {
                    _objCommon.CityId = 0;

                    BindCityByState(Convert.ToInt16(ddlState.SelectedValue));
                    _objCommon.StateId = Convert.ToInt16(ddlState.SelectedValue);
                    lblState.InnerText = ddlState.SelectedItem.Text;
                    FillCollegeSearch(_objCommon.CityId, _objCommon.StateId, _objCommon.ExamId, _objCommon.CourseId,
                                      Convert.ToInt32(ddlManagement.SelectedValue));
                    BindPageTitleAndKeyWordsByCity(ddlState.SelectedItem.Text);


                }
                else
                {
                    _objCommon.CityId = 0;
                    _objCommon.StateId = Convert.ToInt16(ddlState.SelectedValue);
                    ddlCity.Items.Clear();
                    ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));
                    FillCollegeSearch(_objCommon.CityId, _objCommon.StateId, _objCommon.ExamId, _objCommon.CourseId,
                                      Convert.ToInt32(ddlManagement.SelectedValue));
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp",
                                                    "<script type='text/javascript'>SearchPriorityListCollege(" +
                                                    _objCommon.CourseId + "," + _objCommon.StateId + "," +
                                                    _objCommon.CityId +
                                                    "," + _objCommon.ExamId + "," +
                                                    Convert.ToInt32(ddlManagement.SelectedValue) + ");</script>", false);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DdlStateSelectedIndexChanged in CollegeSearch.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void DdlCitySelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PageNum = 1;
                CurrentPageIndex = 0;
                if (ddlCity.SelectedIndex > 0)
                {

                    lblCity.InnerText = ddlCity.SelectedItem.Text;
                    _objCommon.CityId = Convert.ToInt16(ddlCity.SelectedValue);
                    FillCollegeSearch(_objCommon.CityId, _objCommon.StateId, _objCommon.ExamId, _objCommon.CourseId,
                                      Convert.ToInt32(ddlManagement.SelectedValue));
                    BindPageTitleAndKeyWordsByCity(ddlCity.SelectedItem.Text);
                }
                else
                {
                    _objCommon.CityId = Convert.ToInt16(ddlCity.SelectedValue);
                    FillCollegeSearch(_objCommon.CityId, _objCommon.StateId, _objCommon.ExamId, _objCommon.CourseId,
                                      Convert.ToInt32(ddlManagement.SelectedValue));


                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp",
                                                    "<script type='text/javascript'>SearchPriorityListCollege(" +
                                                    _objCommon.CourseId + "," + _objCommon.StateId + "," +
                                                    _objCommon.CityId + "," + _objCommon.ExamId + "," +
                                                    Convert.ToInt32(ddlManagement.SelectedValue) + ");</script>", false);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DdlCitySelectedIndexChanged in CollegeSearch.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        protected void DdlManagementSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PageNum = 1;
                CurrentPageIndex = 0;
                if (ddlManagement.SelectedIndex > 0)
                {

                    lblManagement.InnerText = ddlManagement.SelectedItem.Text;

                    FillCollegeSearch(_objCommon.CityId, _objCommon.StateId, _objCommon.ExamId, _objCommon.CourseId,
                                      Convert.ToInt32(ddlManagement.SelectedValue));
                    BindPageTitleAndKeyWordsByCity(lblManagement.InnerText);
                }
                else
                {
                    FillCollegeSearch(_objCommon.CityId, _objCommon.StateId, _objCommon.ExamId, _objCommon.CourseId,
                                      Convert.ToInt32(ddlManagement.SelectedValue));
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp",
                                                    "<script type='text/javascript'>SearchPriorityListCollege(" +
                                                    _objCommon.CourseId + "," + _objCommon.StateId + "," +
                                                    _objCommon.CityId +
                                                    "," + _objCommon.ExamId + "," +
                                                    Convert.ToInt32(ddlManagement.SelectedValue) + ");</script>", false);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing DdlManagementSelectedIndexChanged in CollegeSearch.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        

        protected void FillCollegeSearch(int cityId, int statId, int examId, int courseId, int managementType)
        {
            lblResult.Text = string.Empty;
            try
            {
              
                var searchPattern = "";
                int errorCount;
                var recordsCount = 0;
                var collegeData = CollegeProvider.Instance.GetCollegeListByCourseExamStateCIty(cityId, statId, examId,
                                                                                               courseId, managementType,
                                                                                               out errorCount,
                                                                                                                                          out searchPattern, PageNum, out recordsCount, PageSize).Where(college=>college.CollegeBranchStatus==true).ToList();
              
                   
                    if (collegeData.Count > 0)
                    {
                        pnlPager.Visible = true;
                        lblResult.Visible = true;
                        lblResult.Text = "Showing results for: " + searchPattern;
                        lblSuccess.Text = string.Empty;
                        lblSuccess.Visible = false;
                        rptCollegeDetails.Visible = true;
                        if (recordsCount == 0)
                        {
                            recordsCount = collegeData.Count;
                        }
                        rptCollegeDetails.DataSource = collegeData;
                        rptCollegeDetails.DataBind();
                        TotalRecord = recordsCount;
                        PageCount = TotalRecord / PageSize;
                        int temp = TotalRecord % PageSize;
                        if(temp>0)
                            PageCount = PageCount + 1;
                        
                        BuildPagination();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp",
                                                       "<script type='text/javascript'>SearchPriorityListCollege(" +
                                                      courseId + "," + statId + "," +
                                                      cityId + "," + examId + "," +
                                                      managementType + ");</script>",
                                                       false);

                    }
                    else
                    {
                        lblSuccess.Visible = true;
                        lblSuccess.CssClass = "info";
                        lblSuccess.Text = new Common().GetErrorMessage("noRecords");
                        rptCollegeDetails.Visible = false;
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
                    const string addInfo = "Error in Executing  FillCollegeSearch in CollegeSerach.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
        }

        private void BindCityByState(int stateId)
        {
            try
            {
                ddlCity.Items.Clear();
                var data = CityProvider.Instacnce.GetCityListByState(Convert.ToInt16(stateId)).OrderBy(result=>result.CityName).ToList();
                if (data.Count > 0)
                {
                    ddlCity.DataSource = data;
                    ddlCity.DataTextField = "CityName";
                    ddlCity.DataValueField = "CityId";
                    ddlCity.DataBind(); 
                    ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCityByState in CollegeSearch.aspx  :: -> ";
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
                  
            FillCollegeSearch(_objCommon.CityId, _objCommon.StateId, _objCommon.ExamId, _objCommon.CourseId,
                                      Convert.ToInt32(ddlManagement.SelectedValue));

            BindPageTitleAndKeyWords();
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

        private int TotalRecord
        {
            get { return Convert.ToInt16(ViewState["TotalRecords"]); }
            set { ViewState["TotalRecords"] = value; }
        }

        public string SPonserValue;
         public string Sponser
        {
            get
            {
                return SPonserValue;
               
            }
            set { SPonserValue= value; }
        }
         public string UserLogin
         {
             get
             {
                 return new SecurePage().IsLoggedInUSer==true?"true":"false";

             }
            
         }
         public string LoginUserName
         {
             get
             {
                 return new SecurePage().IsLoggedInUSer ? new SecurePage().LoggedInUserName: "N/A";

             }

         }
    }
    

    }
