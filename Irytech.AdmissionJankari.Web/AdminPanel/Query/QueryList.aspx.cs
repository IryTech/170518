using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Globalization;
using System.Data;
namespace IryTech.AdmissionJankari20.Web.AdminPanel.Query
{
    public partial class QueryList : System.Web.UI.Page
    {
        List<QueryProperty> _listQueryProperty = new List<QueryProperty>();
        Common _objCommon;
           
        protected void Page_Load(object sender, EventArgs e)
        {
          ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            if (Request.QueryString["N"] != null)
            {
                rbtSearchQuery.ClearSelection();
                rbtSearchQuery.Items.FindByValue(Convert.ToString(Request.QueryString["N"])).Selected = true;
            }
            BindCourseList();
            BindConditionalQuery();
        }

        protected void rbtSearchQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindConditionalQuery();
        }


        // Method to Bind The Condtional Query
        private void BindConditionalQuery()
        {
            switch (rbtSearchQuery.SelectedValue)
            {
                case "1":
                    BindQueryList(BindCollegeQueryList());
                    break;
                case "2":
                    BindQueryList(BindExamQueryList());
                    break;
                case "3":
                    BindQueryList(BindLoanQueryList());
                    break;
                case "4":
                    BindQueryList(BindCourseQueryList());
                    break;
            }
        }

        // Method to Bind the Query List according to college
        private List<QueryProperty> BindCollegeQueryList()
        {
            try
            {
                 _listQueryProperty = QueryProvider.Instance.GetAllQueryListByCollege();
                
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindQueryList in QueryList.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _listQueryProperty;
        }


        // Method to get the query list according to course
        private List<QueryProperty> BindCourseQueryList()
        {
            try
            {
                _listQueryProperty = QueryProvider.Instance.GetAllQueryListByCourse();

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCourseQueryList in QueryList.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _listQueryProperty;
        }
        // Method to get the query list according to Exam
        private List<QueryProperty> BindExamQueryList()
        {
            try
            {
                _listQueryProperty = QueryProvider.Instance.GetAllQueryListByExam();

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindExamQueryList in QueryList.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _listQueryProperty;
        }
        // Method to get the query list according to Loan
        private List<QueryProperty> BindLoanQueryList()
        {
            try
            {
                _listQueryProperty = QueryProvider.Instance.GetAllQueryListByLoan();

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindLoanQueryList in QueryList.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _listQueryProperty;
        }


        private void BindQueryList(List<QueryProperty> objQueryList)
        {
          
            try
            {
                if (objQueryList.Count > 0)
                {
                    rptQuery.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptQuery, Common.ConvertToDataTable(objQueryList));
                    ucCustomPaging.Visible = true;

                }
                else
                {
                    rptQuery.Visible = false;
                    ucCustomPaging.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindQueryList in QueryList.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        private void PagerPageIndexChanged(object sender, EventArgs e)
        {
            BindConditionalQuery();

        }

        private void DownloadQuery(List<QueryProperty> objQueryList)
        {
            try
            {
                if (objQueryList.Count > 0)
                {
                    DateTime Time;
                    if (!string.IsNullOrEmpty(txtFromdate.Text))
                    {
                         Time = DateTime.ParseExact(txtFromdate.Text, "dd/MM/yyyy", null);
                        txtFromdate.Text = (Time.ToString("MM/dd/yyyy hh:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")));
                    }
                    if (!string.IsNullOrEmpty(txtTodate.Text))
                    {
                         Time = DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", null);
                        txtTodate.Text = (Time.ToString("MM/dd/yyyy hh:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")));
                    }


                    if (!string.IsNullOrEmpty(txtFromdate.Text) && string.IsNullOrEmpty(txtTodate.Text))
                    {
                        objQueryList = objQueryList.Where(data => Convert.ToDateTime(data.CreatedOn) >= Convert.ToDateTime(txtFromdate.Text) && Convert.ToDateTime(data.CreatedOn) <= System.DateTime.Now).ToList();
                            
                    }
                    else if(string.IsNullOrEmpty(txtFromdate.Text) && !string.IsNullOrEmpty(txtTodate.Text))
                    {
                        objQueryList = objQueryList.Where(data => Convert.ToDateTime(data.CreatedOn) <= Convert.ToDateTime(txtTodate.Text)).ToList();

                    }
                    else if (!string.IsNullOrEmpty(txtFromdate.Text) && !string.IsNullOrEmpty(txtTodate.Text))
                    {

                        objQueryList = objQueryList.Where(data => Convert.ToDateTime(data.CreatedOn) >= Convert.ToDateTime(txtFromdate.Text) && Convert.ToDateTime(data.CreatedOn) <= Convert.ToDateTime(txtTodate.Text).AddDays(1)).ToList();
                        
                    }

                    else if (ddlCourseList.SelectedIndex > 0)
                    {
                        objQueryList = objQueryList.Where(data => data.StudentCourseId == Convert.ToInt32(ddlCourseList.SelectedValue)).ToList();

                    }
                    
                    string path= "QueryList" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + ".xls";
                    var fi = new FileInfo(path);
                    var stringWriter = new StringWriter();
                    var htmlWrite = new HtmlTextWriter(stringWriter);
                    var queryList = objQueryList.Select(data => new
                    {
                        Course = data.StudentCourseName,
                        Name = data.StudentName,
                        Mobile = data.UserMobileNo,
                        EmailId = data.UserEmailId,
                        CityName = data.StudentCityName,
                        Source = data.SourceName,
                        Query = data.StudentQuery,
                        QueryDate = data.CreatedOn.ToString("dd/MM/yyyy")
                    });

                    if (queryList.Any())
                    {
                        var dataGrd = new GridView {DataSource = queryList};
                        dataGrd.DataBind();
                        Page.Controls.Add(dataGrd);
                        ExportToExcel(path, dataGrd);
                        ddlCourseList.SelectedValue = "0";
                    }
                }
               
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DownloadQuery in QueryList.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            switch (rbtQueryDownloadType.SelectedValue)
            {
                case "1":
                    DownloadQuery(BindCollegeQueryList());
                    break;
                case "2":
                    DownloadQuery(BindExamQueryList());
                    break;
                case "3":
                    DownloadQuery(BindLoanQueryList());
                    break;
                case "4":
                    DownloadQuery(BindCourseQueryList());
                    break;
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        private void ExportToExcel(string strFileName, GridView gv)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + strFileName);
            Response.ContentType = "application/excel";
            var sw = new StringWriter();
            var htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
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
                ddlCourseListSearch.DataSource = courseData;
                ddlCourseListSearch.DataTextField = "CourseName";
                ddlCourseListSearch.DataValueField = "CourseId";
                ddlCourseListSearch.DataBind();
                ddlCourseListSearch.Items.Insert(0, new ListItem("Select Course", "0"));
              
            }

            else
            {
                 ddlCourseList.Items.Insert(0, new ListItem("--Select--", "0"));
                 ddlCourseListSearch.Items.Insert(0, new ListItem("Select Course", "0"));

            }

        }
        protected void ddlCourseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCourseList.SelectedIndex > 0)
            {
                switch (rbtQueryDownloadType.SelectedValue)
                {
                    case "1":
                        DownloadQuery(BindCollegeQueryList());
                        break;
                    case "2":
                        DownloadQuery(BindExamQueryList());
                        break;
                    case "3":
                        DownloadQuery(BindLoanQueryList());
                        break;
                    case "4":
                        DownloadQuery(BindCourseQueryList());
                        break;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int courseId = 0;
            if (ddlCourseListSearch.SelectedIndex > 0)
            {
                courseId=Convert.ToInt32(ddlCourseListSearch.SelectedValue);
            }
            switch (rbtQueryDownloadType.SelectedValue)
            {
                    
                case "1":
                    BindQuerySearch(BindCollegeQueryList(),courseId);
                    break;
                case "2":
                    BindQuerySearch(BindExamQueryList(),courseId);
                    break;
                case "3":
                    BindQuerySearch(BindLoanQueryList(),courseId);
                    break;
                case "4":
                    BindQuerySearch(BindCourseQueryList(),courseId);
                    break;
            }
        }

       
        protected void ddlCourseListSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCourseListSearch.SelectedIndex > 0)
            {
                var courseId = Convert.ToInt32(ddlCourseListSearch.SelectedValue);
                switch (rbtQueryDownloadType.SelectedValue)
                {
                    case "1":
                        BindCourseQuery(BindCollegeQueryList(), courseId);
                        break;
                    case "2":
                        BindCourseQuery(BindExamQueryList(), courseId);
                        break;
                    case "3":
                        BindCourseQuery(BindLoanQueryList(), courseId);
                        break;
                    case "4":
                        BindCourseQuery(BindCourseQueryList(), courseId);
                        break;
                }
            }

        }
        
        public string GetReplyCount(int queryId)
        {
            Common _objCommon = new Common();
            var data = _objCommon.GetReply(Convert.ToInt32(queryId), "A");
            return data.Count.ToString();
        }


        // Method to Bind The Query List According To Course
        private void BindCourseQuery(List<QueryProperty> objQueryList, int courseId)
        {
            objQueryList = objQueryList.Where(data => data.StudentCourseId == courseId).ToList();
            if (objQueryList.Count > 0)
            {
                rptQuery.Visible = true;
                ucCustomPaging.BindDataWithPaging(rptQuery, Common.ConvertToDataTable(objQueryList));
                ucCustomPaging.Visible = true;

            }
            else
            {
                rptQuery.Visible = false;
                ucCustomPaging.Visible = false;
            }
        }

        // Method to Bind The QUery Search
        private void BindQuerySearch(List<QueryProperty> objQueryList, int courseId = 0)
        {
            DateTime Time;
            if (courseId > 0)
            {
                objQueryList = objQueryList.Where(data => data.StudentCourseId == courseId).ToList();
            }

            if (!string.IsNullOrEmpty(txtFromdateSearch.Text))
            {
                Time = DateTime.ParseExact(txtFromdateSearch.Text, "dd/MM/yyyy", null);
                txtFromdateSearch.Text = (Time.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")));
            }
            if (!string.IsNullOrEmpty(txtTodateSearch.Text))
            {
                Time = DateTime.ParseExact(txtTodateSearch.Text, "dd/MM/yyyy", null);
                txtTodateSearch.Text = (Time.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")));
            }


            if (!string.IsNullOrEmpty(txtFromdateSearch.Text) && string.IsNullOrEmpty(txtTodateSearch.Text))
            {
                objQueryList = objQueryList.Where(data => Convert.ToDateTime(data.CreatedOn) >= Convert.ToDateTime(txtFromdate.Text) && Convert.ToDateTime(data.CreatedOn) <= System.DateTime.Now).ToList();

            }
            else if (string.IsNullOrEmpty(txtFromdateSearch.Text) && !string.IsNullOrEmpty(txtTodateSearch.Text))
            {
                objQueryList = objQueryList.Where(data => Convert.ToDateTime(data.CreatedOn) <= Convert.ToDateTime(txtTodateSearch.Text)).ToList();

            }
            else if (!string.IsNullOrEmpty(txtFromdateSearch.Text) && !string.IsNullOrEmpty(txtTodateSearch.Text))
            {

                objQueryList = objQueryList.Where(data => Convert.ToDateTime(data.CreatedOn) >= Convert.ToDateTime(txtFromdateSearch.Text) && Convert.ToDateTime(data.CreatedOn) <= Convert.ToDateTime(txtTodateSearch.Text).AddDays(1)).ToList();
                
            }

            if (objQueryList.Count > 0)
            {
                rptQuery.Visible = true;
                ucCustomPaging.BindDataWithPaging(rptQuery, Common.ConvertToDataTable(objQueryList));
                ucCustomPaging.Visible = true;

            }
            else
            {
                rptQuery.Visible = false;
                ucCustomPaging.Visible = false;
            }
        }

        // method to find the query has been moderated or not
        public string GetModerateQueryClass(object queryId)
        {
            return  QueryProvider.Instance.CheckQueryModerate(Convert.ToInt32(queryId));
        }
    }
}