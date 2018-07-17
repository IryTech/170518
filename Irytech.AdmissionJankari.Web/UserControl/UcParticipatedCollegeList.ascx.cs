using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcParticipatedCollegeList : System.Web.UI.UserControl
    {
        Consulling _objConsulling;
        readonly Common _objCommon = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCollegeList.PageSize = 5;
            ucCollegeList.ButtonsCount =5;
            ucCollegeList.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            if (Request.QueryString["CollegeName"] != null)
            {
                BindPartiCularCollege(Request.QueryString["CollegeName"]);
                BindPageTitleAndKeyWordsofCollege(Convert.ToString(Request.QueryString["CollegeName"]));
            }
            else
            {
                BindParticipatedCollege();
                BindPageTitleAndKeyWords();
            }
            _objCommon.StateId = 0;
            _objCommon.CityId = 0;
            BindState();
            BindCity();
            BindCourse();

        }

        #region method
        //....method to add page tiltle and keywords by college name..
        public void BindPageTitleAndKeyWordsofCollege(string collegeName)
        {
            try
            {


                Page.Title = "Find complete details of  " + collegeName + " Colleges in India | College Search - Admission Jankari";

                var metadesc = new HtmlMeta();
                metadesc.Attributes.Clear();
                metadesc.Name = "description";

                metadesc.Content =
                    "Find details " + collegeName +
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

        //....method to add page tiltle and keywords by course..
        public void BindPageTitleAndKeyWords()
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
        //...method to bind course..
        private void BindCourse()
        {
            
            try
            {
              
                var data = CourseProvider.Instance.GetAllCourseList();
                data = data.Where(course => course.CourseStatus == true).ToList();
                if (data.Count > 0)
                {
                    ddlCourse.DataSource = data;
                    ddlCourse.DataTextField = "CourseName";
                    ddlCourse.DataValueField = "CourseId";
                    ddlCourse.DataBind();
                    ddlCourse.SelectedValue = Convert.ToString(_objCommon.CourseId);
                    _objCommon.CourseName = Utils.RemoveIllegealFromCourse(ddlCourse.SelectedItem.Text);
                    lblCourse.InnerHtml = ddlCourse.SelectedItem.Text;
                    
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

        // Method to Bind the Participating College..
        protected void BindParticipatedCollege()
        {
            _objConsulling = new Consulling();
            try
            {
                var datalist = GetCollegeListByQuery().ToList();
                BindCollege(datalist);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindParticipatingCollege in UcParticipatedCollegeList.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        //...pager section..
        private void PagerPageIndexChanged(object sender, EventArgs e)
        {
            var datalist = GetCollegeListByQuery().ToList();
            try
            {
                if (datalist.Count > 0)
                {
                    ucCollegeList.Visible = true;
                    ucCollegeList.BindDataWithPaging(rptCollegeDetails, Common.ConvertToDataTable(datalist));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in CollegeList.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        //....method to bind state list
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

        //...method to bind city list..
        private void BindCity()
        {
            try
            {
                var data = CityProvider.Instacnce.GetAllCityList().OrderBy(result => result.CityName).ToList();
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

        protected void DdlStateSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedIndex > 0)
            {
                _objCommon.CityId = 0;

                BindCityByState(Convert.ToInt16(ddlState.SelectedValue));
                _objCommon.StateId = Convert.ToInt16(ddlState.SelectedValue);
                lblState.InnerText = ddlState.SelectedItem.Text;
                var datalist = GetCollegeListByQuery().ToList();

                BindCollege(datalist);


            }
            else
            {
                _objCommon.CityId = 0;
                _objCommon.StateId = Convert.ToInt16(ddlState.SelectedValue);
                ddlCity.Items.Clear();
                ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));

            }
        }

        protected void DdlCitySelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlCity.SelectedIndex > 0)
                {

                    lblCity.InnerText = ddlCity.SelectedItem.Text;
                    _objCommon.CityId = Convert.ToInt16(ddlCity.SelectedValue);
                    var datalist = GetCollegeListByQuery().ToList();
                    BindCollege(datalist);

                }
                else
                {
                    _objCommon.CityId = Convert.ToInt16(ddlCity.SelectedValue);


                }
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

        //....method to bind city by stateid..
        private void BindCityByState(int stateId)
        {
            try
            {
                ddlCity.Items.Clear();
                var data = CityProvider.Instacnce.GetCityListByState(Convert.ToInt16(stateId)).OrderBy(result => result.CityName).ToList();
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

        //....method to get participated college.....
        private IEnumerable<BookSeat> GetCollegeListByQuery()
        {
         
            _objConsulling = new Consulling();
            var objCollegeBranchProperty = new List<BookSeat>();
            try
            {
                objCollegeBranchProperty =
                    _objConsulling.GetBookedCollege().Where(result => result.CourseMaster.CourseId == _objCommon.CourseId &&
                        ((!string.IsNullOrEmpty(result.BookSeatStartDate) ? DateTime.Parse(result.BookSeatStartDate) : DateTime.Now) >= DateTime.Now || (!string.IsNullOrEmpty(result.BookSeatEndDate) ? DateTime.Parse(result.BookSeatEndDate) : DateTime.Now) >= DateTime.Now) && result.BookSeatStatus == true).ToList();
                if (ddlState.SelectedIndex > 0)
                   objCollegeBranchProperty= _objConsulling.GetBookedCollege().Where(result => result.CityMaster.StateId== Convert.ToInt32(ddlState.SelectedValue) &&
                        ((!string.IsNullOrEmpty(result.BookSeatStartDate) ? DateTime.Parse(result.BookSeatStartDate) : DateTime.Now) >= DateTime.Now || (!string.IsNullOrEmpty(result.BookSeatEndDate) ? DateTime.Parse(result.BookSeatEndDate) : DateTime.Now) >= DateTime.Now) && result.BookSeatStatus == true).ToList();
                if(ddlCity.SelectedIndex > 0)
                    objCollegeBranchProperty = _objConsulling.GetBookedCollege().Where(result => result.CityMaster.CityId == Convert.ToInt32(ddlCity.SelectedValue) &&
                       ((!string.IsNullOrEmpty(result.BookSeatStartDate) ? DateTime.Parse(result.BookSeatStartDate) : DateTime.Now) >= DateTime.Now || (!string.IsNullOrEmpty(result.BookSeatEndDate) ? DateTime.Parse(result.BookSeatEndDate) : DateTime.Now) >= DateTime.Now) && result.BookSeatStatus == true).ToList();
                if (objCollegeBranchProperty.Count<=0)
                {
                    objCollegeBranchProperty =
                     _objConsulling.GetBookedCollege().Where(result => result.CourseMaster.CourseId == _objCommon.CourseId &&
                         ((!string.IsNullOrEmpty(result.BookSeatStartDate) ? DateTime.Parse(result.BookSeatStartDate) : DateTime.Now) >= DateTime.Now || (!string.IsNullOrEmpty(result.BookSeatEndDate) ? DateTime.Parse(result.BookSeatEndDate) : DateTime.Now) >= DateTime.Now) && result.BookSeatStatus == true).ToList();

                }



            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindParticipatingCollege in UcParticipatedCollegeList.ascx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objCollegeBranchProperty;
        }

        //...method to bind repeater with data.....
        private void BindCollege(List<BookSeat> datalist)
        {
            if (datalist.Count > 0)
            {
                lblResult.Visible = false;
                rptCollegeDetails.Visible = true;
                ucCollegeList.Visible = true;
                ucCollegeList.BindDataWithPaging(rptCollegeDetails, Common.ConvertToDataTable(datalist));
            }
            else
            {
                lblResult.Visible = true;
                lblResult.CssClass = "info";
                lblResult.Text = "Oops! No Record Found";
                rptCollegeDetails.Visible = false;
                ucCollegeList.Visible = false;
            }
            
        }


        //...method to bind college by college name...
        private void BindPartiCularCollege(string collegeName)
        {
            try
            {
                var collegeDataByName =
                    CollegeProvider.Instance.GetBookedCollegeList(collegeName.Trim())
                                   .ToList();
                                  
                                  
                ucCollegeList.Visible = false;
                if (collegeDataByName.Count > 0)
                {
                    ucCollegeList.Visible = false;
                    rptCollegeDetails.Visible = true;
                    rptCollegeDetails.DataSource = collegeDataByName;
                    rptCollegeDetails.DataBind();

                }
                else
                {
                    rptCollegeDetails.Visible = false;

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

        #endregion
    }
}