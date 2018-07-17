using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.Components.Web.Controls;
using System.Web.UI;
using System.Data;
using System.Web.UI.HtmlControls;

namespace IryTech.AdmissionJankari20.Web.Account
{
    public partial class CollegeProfile : PageBase
    {
        private readonly SecurePage _objSecurePage = new SecurePage();
        private readonly Common _objCommon = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            noticePaging.PageSize = ClsSingelton.PageSize;
            noticePaging.ButtonsCount = ClsSingelton.PageButtonCount;
            noticePaging.PagerPageIndexChanged += NoticePagerPageIndexChanged;
            CustompagingEvent.PageSize = ApplicationSettings.Instance.CollegePageSize;
            CustompagingEvent.ButtonsCount = ApplicationSettings.Instance.CollegePageCount;
            CustompagingEvent.PagerPageIndexChanged += EventPagerPageIndexChanged;
            TestimonialPager.PageSize = ApplicationSettings.Instance.CollegePageSize;
            TestimonialPager.ButtonsCount = ApplicationSettings.Instance.CollegePageCount;
            TestimonialPager.PagerPageIndexChanged += TestiPagerPageIndexChanged;
            CustomPaging2.PageSize = ClsSingelton.PageSize;
            CustomPaging2.ButtonsCount = ApplicationSettings.Instance.CollegePageCount;
            CustomPaging2.PagerPageIndexChanged += StreamPagerPageIndexChanged;
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
            ucCustomPaging.ButtonsCount = ApplicationSettings.Instance.CollegePageCount;
            ucCustomPaging.PagerPageIndexChanged += CoursePagerPageIndexChanged;

            if (!IsPostBack)
            {
                txtTestimonial.Fckrfv = "testmonial";
                txtCourseHighLightsInsert.Fckrfv = "highLightsinsert";
                txtCollegeNotice.Fckrfv = "notice";
                ValidateFields();
                BindState(0);
                BindCity(0);
                BindManagement();
                BindCollegeList();
                BindCourse();
                BindUniversity();
                BindCollegeCourseList(hdnCollegeId.Value);
                addProduct.CollegeId = Convert.ToInt32(hdnCollegeId.Value);
                BindStreamMode();
                BindCollegeCourseStreamList(hdnCollegeId.Value);
                BindCollegeCourseExamList(hdnCollegeId.Value);
                BindHostelCategory();
                BindCollegeCourseHostel(hdnCollegeId.Value);
                BindRank();
                BindCollegeCourseRankList(hdnCollegeId.Value);
                BindCollegeCourseHighLights(hdnCollegeId.Value);
                BindPlacemntByCollegeId(hdnCollegeId.Value);
                BindNoticeCategory();
                BindEventList();
                BindNoticeDetails();
                BindTestimonialDetails();
                CheckBannerCount();
                CheckProductCountAfterPayment();
                GetCollegeBannerList();
                BindPageTitleAndKeyWords();
            }
            if (!string.IsNullOrEmpty(Convert.ToString(ddlCourseForVisitors.SelectedValue)))
                GetColLegeVisiotrsCount(Convert.ToInt32(ddlCourseForVisitors.SelectedValue));
        }

        private void BindPageTitleAndKeyWords()
        {

            try
            {
                Page.Title = "Manage details of " +lblCollegeName.Text +"  -  Admission Jankari";
                var metadesc = new HtmlMeta();
                metadesc.Attributes.Clear();
                metadesc.Name = "description";
                metadesc.Content = "Manage the details of " + lblCollegeName.Text + "," + ddlCollegeCity.SelectedItem + ", " + ddlState.SelectedItem +
                                 " in India. Manage your courses offered, Admissions in " + lblCollegeName.Text +
                                 ", Fees, Placements, Admission Criteria, Facilities, Contact Details, Rankings, History, Notices, News ,Events Hostel, Map, Compare similar colleges with - Admission Jankari " ;
                             
                Page.Header.Controls.Add(metadesc);
                var metaKeywords = new HtmlMeta
                {
                    Name = "keywords",
                    Content = "Manage the " + " details of " + lblCollegeName.Text + "," + ddlCollegeCity.SelectedItem + ", " + ddlState.SelectedItem +
                                 " in India. Manage your  courses offered, Admissions in " + lblCollegeName.Text +
                                 ", Fees, Placements, Admission Criteria, Facilities, Contact Details, Rankings, History, Notices, News ,Events Hostel, Map, Compare similar colleges with " +
                                lblCollegeName.Text + " - Admission Jankari"
                       
                };
                Page.Header.Controls.Add(metaKeywords);
            }
            catch (Exception Ex)
            {
                var err = Ex.Message;
                if (Ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + Ex.InnerException.Message;
                }
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in Default.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        #region CollegeBasicInfo

        private void BindState(int countryId)
        {
            try
            {
                List<StateProperty> data;
                data = countryId == 0
                           ? StateProvider.Instance.GetAllState()
                           : StateProvider.Instance.GetStateByCountry(countryId);
                if (data.Count > 0)
                {
                    ddlState.DataSource = data;
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateId";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, new ListItem("Select", "0"));
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
                const string addInfo = "Error while executing BindState in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
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
                    ddlCollegeCity.DataSource = data;
                    ddlCollegeCity.DataTextField = "CityName";
                    ddlCollegeCity.DataValueField = "CityId";
                    ddlCollegeCity.DataBind();
                    ddlCollegeCity.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlCollegeCity.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCity in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void BindManagement()
        {
            var dv = ClsSingelton.GetManagement();
            if (dv.Count > 0)
            {
                ddlCollegeMgt.DataSource = dv;
                ddlCollegeMgt.DataTextField = "AjMasterValues";
                ddlCollegeMgt.DataValueField = "AjMasterValueId";
                ddlCollegeMgt.DataBind();
                ddlCollegeMgt.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
                ddlCollegeMgt.Items.Insert(0, new ListItem("Select", "0"));
        }

        // Method to validate fields
        private void ValidateFields()
        {
            try
            {
                var objCommon = new Common();
                rfvCollegeMgt.Value = objCommon.GetValidationMessage("rfvCollegeMgt");
                rfvCollegeEst.Value = objCommon.GetValidationMessage("rfvCollegeEst");
                revCollegeEst.Value = objCommon.GetValidationMessage("revCollegeEst");
                revStudentHired.ValidationExpression = ClsSingelton.aRegExpInteger;
                rfvCity.Value = objCommon.GetValidationMessage("rfvCity");
                rfvState.Value = objCommon.GetValidationMessage("rfvState");
                rfvEmailId.Value = objCommon.GetValidationMessage("rfvEmailId");
                revEmailId.Value = objCommon.GetValidationMessage("revEmail");

                rfvMobile.Value = objCommon.GetValidationMessage("rfvMobile");
                revMobile.Value = objCommon.GetValidationMessage("revMobile");
                revCollegePinCode.Value = objCommon.GetValidationMessage("revPinCode");
                revFax.Value = objCommon.GetValidationMessage("revFax");
                revCourseEst.ValidationExpression = ClsSingelton.aRegExpInteger;
                revCourseEst.ErrorMessage = objCommon.GetValidationMessage("revCollegeEst");


                revStreamSeat.ValidationExpression = ClsSingelton.aRegExpInteger;
                revLateralSeat.ValidationExpression = ClsSingelton.aRegExpInteger;
                revQuotaSeat.ValidationExpression = ClsSingelton.aRegExpInteger;

                revRanRankYear.ValidationExpression = ClsSingelton.aRegExpInteger;
                revRanRankYear.ErrorMessage = objCommon.GetValidationMessage("revCollegeEst");
                revRanRankOverAll.ValidationExpression = ClsSingelton.aRegExpInteger;
                revRanRankOverAll.ErrorMessage = objCommon.GetValidationMessage("revCollegeEst");
                rgYear.MaximumValue = DateTime.Now.Year.ToString();
                rgCourseEst.MaximumValue = DateTime.Now.Year.ToString();

                // revEventCalender.ValidationExpression = ClsSingelton.aRegExpDate;

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing ValidateFields in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        //to get college data...
        private void BindCollegeList()
        {
            var objSecurePage = new SecurePage();
            try
            {
                var collegeBasicData = CollegeProvider.Instance.GetCollegeListByUserId(objSecurePage.LoggedInUserId);
                if (collegeBasicData.Count > 0)
                {
                    var query = collegeBasicData.Select(result => new
                    {
                        result.CollegeIdBranchId,
                        result.InstituteTypeId,
                        result.CollegeGroupId,
                        result.CollegeBranchName,
                        result.CollegePopulaorName,
                        result.CollegeManagementTypeId,
                        result.CollegeBranchEst,
                        result.CollegeBranchDesc,
                        result.CollegeBranchAddrs,
                        result.CollegeBranchMobileNo,
                        result.CollegeBranchPinCode,
                        result.CoillegeBranchEmailId,
                        result.CollegeBranchFax,
                        result.CollegeBranchWebsite,
                        result.CollegeBranchCountryId,
                        result.CollegeBranchStateId,
                        result.CollegeBranchCityId,
                        result.CollegeBranchStatus,
                        result.CollegeBranchLogo
                    }).First();
                    hdnCollegeId.Value = Convert.ToString(query.CollegeIdBranchId);
                    countryId.Value = Convert.ToString(query.CollegeBranchCountryId);
                    hdnCollegeName.Value = query.CollegeBranchName;
                    ddlState.SelectedValue = query.CollegeBranchStateId.ToString() != ""
                                                 ? query.CollegeBranchStateId.ToString()
                                                 : "0";
                    ddlCollegeCity.SelectedValue = query.CollegeBranchCityId.ToString() != ""
                                                       ? query.CollegeBranchCityId.ToString()
                                                       : "0";
                    lblCollegeName.Text = !string.IsNullOrEmpty(query.CollegeBranchName)
                                              ? query.CollegeBranchName
                                              : "N/A";
                    lblCollegeName.ToolTip = lblCollegeName.Text;
                    txtCollegePopularName.Text = !string.IsNullOrEmpty(query.CollegePopulaorName)
                                                     ? query.CollegePopulaorName
                                                     : "N/A";
                    txtAddress.Text = query.CollegeBranchAddrs != ""
                                          ? query.CollegeBranchAddrs
                                          : "N/A";
                    txtCollegeDesc.Text = query.CollegeBranchDesc != ""
                                              ? query.CollegeBranchDesc
                                              : "N/A";
                    txtCollegeEst.Text = query.CollegeBranchEst;
                    txtCollegeFax.Text = query.CollegeBranchFax;
                    txtCollegeMobile.Text = query.CollegeBranchMobileNo;
                    txtCollegeWebsite.Text = query.CollegeBranchWebsite;
                    txtEmailId.Text = query.CoillegeBranchEmailId;
                    txtPinCode.Text = query.CollegeBranchPinCode;
                    ddlCollegeMgt.SelectedValue = query.CollegeManagementTypeId.ToString(CultureInfo.InvariantCulture);
                    var img = !String.IsNullOrEmpty(query.CollegeBranchLogo) ? query.CollegeBranchLogo : null;
                    imgCollege.ImageUrl = String.Format("{0}{1}", "/image.axd?College=",
                                                        string.IsNullOrEmpty(query.CollegeBranchLogo)
                                                            ? "NoImage.jpg"
                                                            : query.CollegeBranchLogo);
                    imgCollege.AlternateText = query.CollegeBranchName;


                    Session["Image"] = img;

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCollegeList in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

       

        private void CollegeData()
        {
            try
            {
                var collegeImage = "";

                if (Session["Image"] != null)
                {
                    collegeImage = Session["Image"].ToString();
                }
                var objCollegeBranchProperty = new CollegeBranchProperty
                {
                    CollegeIdBranchId = Convert.ToInt16(hdnCollegeId.Value),
                    InstituteTypeId = 0,

                    CollegeGroupId = 0,
                    CollegeBranchName =
                        lblCollegeName.Text != "" ? lblCollegeName.Text : "N/A",
                    CollegePopulaorName =
                        txtCollegePopularName.Text != ""
                            ? txtCollegePopularName.Text
                            : "N/A",
                    CollegeManagementTypeId =
                        Convert.ToInt32(ddlCollegeMgt.SelectedValue),
                    CollegeBranchEst =
                        txtCollegeEst.Text,
                    CollegeBranchDesc =
                        txtCollegeDesc.Text != "" ? txtCollegeDesc.Text : "N/A",
                    CollegeBranchAddrs =
                        txtAddress.Text != "" ? txtAddress.Text : "N/A",
                    CollegeBranchMobileNo =
                        txtCollegeMobile.Text != "" ? txtCollegeMobile.Text : "N/A",
                    CollegeBranchPinCode =
                        txtPinCode.Text,
                    CoillegeBranchEmailId =
                        txtEmailId.Text,
                    CollegeBranchFax =
                        txtCollegeFax.Text,
                    CollegeBranchWebsite =
                        txtCollegeWebsite.Text != "" ? txtCollegeWebsite.Text : "N/A",
                    CollegeBranchCountryId = Convert.ToInt32(countryId.Value),
                    CollegeBranchStateId = Convert.ToInt16(ddlState.SelectedValue),
                    CollegeBranchCityId =
                        Convert.ToInt16(ddlCollegeCity.SelectedValue),
                    CollegeBranchLogo = collegeImage
                };
                var errMsg = "";
                var collegeBranchId = 0;
                var result = CollegeProvider.Instance.UpdateCollegeBranchInfo(objCollegeBranchProperty,
                                                                              _objSecurePage.LoggedInUserId,
                                                                              out errMsg,
                                                                              out collegeBranchId);

                if (result > 0)
                {
                    lblBasicDetailsMsg.Visible = true;
                    lblBasicDetailsMsg.CssClass = "success";
                    lblBasicDetailsMsg.Text = errMsg;
                    BindCollegeList();
                }
                else
                {
                    lblBasicDetailsMsg.Visible = true;
                    lblBasicDetailsMsg.CssClass = "info";
                    lblBasicDetailsMsg.Text = errMsg;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing BtnSubmitCollegeBasicInfoClick  in CollegeProfile.axpx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void BtnSubmitCollegeBasicInfoClick(object sender, EventArgs e)
        {
            CollegeData();
        }

        #endregion

        #region course


        private void CoursePagerPageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var data = CollegeProvider.Instance.GetCollegeCourseListByCollegeId(Convert.ToInt16(hdnCollegeId.Value));
                if (data.Count > 0)
                {

                    ucCustomPaging.BindDataWithPaging(rptCourse, Common.ConvertToDataTable(data));

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing CoursePagerPageIndexChanged in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void BindCollegeCourseList(string collegeId)
        {

            try
            {
                var data = CollegeProvider.Instance.GetCollegeCourseListByCollegeId(Convert.ToInt16(collegeId));
                if (data.Count > 0)
                {
                    liTab.Visible = true;
                    tabAdvertise.Visible = true;
                    BindCourseForOthers(data);
                    ucCustomPaging.BindDataWithPaging(rptCourse, Common.ConvertToDataTable(data), false, false);

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCollegeCourseList in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }



        private void BindCourse()
        {
            lblCourseMsg.Visible = false;
            var data = CourseProvider.Instance.GetAllCourseList();
            if (data.Count > 0)
            {

                ddlCourseInsert.DataSource = data;
                ddlCourseInsert.DataTextField = "CourseName";
                ddlCourseInsert.DataValueField = "CourseId";
                ddlCourseInsert.DataBind();
                ddlCourseInsert.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {

                ddlCourseInsert.Items.Insert(0, new ListItem("Select", "0"));

            }
        }

        private void BindUniversity()
        {
            var data = UniversityProvider.Instance.GetAllUniversityList();
            if (data != null && data.Count > 0)
            {

                ddlUniversityInsert.DataSource = data;
                ddlUniversityInsert.DataTextField = "UniversityName";
                ddlUniversityInsert.DataValueField = "UniversityId";
                ddlUniversityInsert.DataBind();
                ddlUniversityInsert.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlUniversityInsert.Items.Insert(0, new ListItem("Select", "0"));


            }
        }

        protected void BtnCourseInsertClick(object sender, EventArgs e)
        {
            lblCourseMsg.Visible = false;
            try
            {
                var result = 0;
                var objCollegeBranchCourseProperty = new CollegeBranchCourseProperty
                {

                    CourseId = Convert.ToInt16(ddlCourseInsert.SelectedValue),
                    UniversityId =
                        Convert.ToInt16(ddlUniversityInsert.SelectedValue),
                    HasHostel = chkHasHostelInsert.Checked,
                    CollegeBranchCourseEst = txtEstInsert.Text,
                    CollegeBranchCourseStatus = chkCourseStatus.Checked,
                };
                var errMsg = "";
                var collegeCourseBranchId = 0;
                if (string.IsNullOrEmpty(hdnCollegeCourseId.Value))
                {
                    objCollegeBranchCourseProperty.CollegeBranchId = Convert.ToInt32(hdnCollegeId.Value);
                    result = CollegeProvider.Instance.InsertCollegeBranchCourseInfo(objCollegeBranchCourseProperty,
                                                                                    _objSecurePage.LoggedInUserId,
                                                                                    out errMsg,
                                                                                    out collegeCourseBranchId);

                }
                else
                {
                    objCollegeBranchCourseProperty.CollegeBranchCourseId = Convert.ToInt32(hdnCollegeCourseId.Value);
                    objCollegeBranchCourseProperty.CollegeBranchId = Convert.ToInt32(hdnCollegeId.Value);
                    result = CollegeProvider.Instance.UpdateCollegeBranchCourseInfo(objCollegeBranchCourseProperty,
                                                                                    _objSecurePage.LoggedInUserId,
                                                                                    out errMsg,
                                                                                    out collegeCourseBranchId);
                }
                lblCourseMsg.Visible = true;
                lblCourseMsg.Text = errMsg;
                if (result > 0)
                {
                    lblCourseMsg.CssClass = "success";
                    BindCollegeCourseList(hdnCollegeId.Value);
                    btnCourseInsert.Text = "Insert";
                    ClearCoursesFields();
                }
                else
                    lblCourseMsg.CssClass = "info";

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnCourseInsert_Click in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        private void ClearCoursesFields()
        {
            txtEstInsert.Text = string.Empty;
            ddlCourseInsert.ClearSelection();
            ddlUniversityInsert.ClearSelection();
            chkHasHostelInsert.Checked = false;
            chkCourseStatus.Checked = false;
            hdnCollegeCourseId.Value = "";
        }

        private void BindCourseForOthers(List<CollegeBranchCourseProperty> objCollegeBranchCourse)
        {
            if (objCollegeBranchCourse.Count > 0)
            {
                ddlCourseStream.DataSource = objCollegeBranchCourse;
                ddlCourseStream.DataTextField = "CourseName";
                ddlCourseStream.DataValueField = "CourseId";
                ddlCourseStream.DataBind();
                ddlCourseStream.Items.Insert(0, new ListItem("Select", "0"));

                ddlCoursesExam.DataSource = objCollegeBranchCourse;
                ddlCoursesExam.DataTextField = "CourseName";
                ddlCoursesExam.DataValueField = "CourseId";
                ddlCoursesExam.DataBind();
                ddlCoursesExam.Items.Insert(0, new ListItem("Select", "0"));

                ddlCoursesHostel.DataSource = objCollegeBranchCourse;
                ddlCoursesHostel.DataTextField = "CourseName";
                ddlCoursesHostel.DataValueField = "CourseId";
                ddlCoursesHostel.DataBind();
                ddlCoursesHostel.Items.Insert(0, new ListItem("Select", "0"));

                ddlCoursesRank.DataSource = objCollegeBranchCourse;
                ddlCoursesRank.DataTextField = "CourseName";
                ddlCoursesRank.DataValueField = "CourseId";
                ddlCoursesRank.DataBind();
                ddlCoursesRank.Items.Insert(0, new ListItem("Select", "0"));

                ddlCoursesHigh.DataSource = objCollegeBranchCourse;
                ddlCoursesHigh.DataTextField = "CourseName";
                ddlCoursesHigh.DataValueField = "CourseId";
                ddlCoursesHigh.DataBind();
                ddlCoursesHigh.Items.Insert(0, new ListItem("Select", "0"));

                ddlCoursesPlacement.DataSource = objCollegeBranchCourse;
                ddlCoursesPlacement.DataTextField = "CourseName";
                ddlCoursesPlacement.DataValueField = "CourseId";
                ddlCoursesPlacement.DataBind();
                ddlCoursesPlacement.Items.Insert(0, new ListItem("Select", "0"));


                ddlCourseEvent.DataSource = objCollegeBranchCourse;
                ddlCourseEvent.DataTextField = "CourseName";
                ddlCourseEvent.DataValueField = "CourseId";
                ddlCourseEvent.DataBind();
                ddlCourseEvent.Items.Insert(0, new ListItem("Select", "0"));

                ddlCourseForVisitors.DataSource = objCollegeBranchCourse;
                ddlCourseForVisitors.DataTextField = "CourseName";
                ddlCourseForVisitors.DataValueField = "CourseId";
                ddlCourseForVisitors.DataBind();
                //ddlCourseForVisitors.Items.Insert(0, new ListItem("Select", "0"));
                ddlCourseForQuery.DataSource = objCollegeBranchCourse;
                ddlCourseForQuery.DataTextField = "CourseName";
                ddlCourseForQuery.DataValueField = "CourseId";
                ddlCourseForQuery.DataBind();



            }
            else
            {

                ddlCourseInsert.Items.Insert(0, new ListItem("Select", "0"));
                ddlCourseStream.Items.Insert(0, new ListItem("Select", "0"));
                ddlCoursesExam.Items.Insert(0, new ListItem("Select", "0"));
                ddlCoursesHostel.Items.Insert(0, new ListItem("Select", "0"));
                ddlCoursesRank.Items.Insert(0, new ListItem("Select", "0"));
                ddlCoursesHigh.Items.Insert(0, new ListItem("Select", "0"));
                ddlCoursesPlacement.Items.Insert(0, new ListItem("Select", "0"));
                ddlCourseEvent.Items.Insert(0, new ListItem("Select", "0"));

                //ddlCourseForVisitors.Items.Insert(0, new ListItem("Select", "0"));
            }


        }

        #endregion

        #region stream

        //private void BindStream(string courseId)
        //{
        //    var data = StreamProvider.Instance.GetStreamListByCourse(Convert.ToInt32(courseId));
        //    if (data != null && data.Count > 0)
        //    {
        //        ddlStream.Enabled = true;
        //        ddlStream.DataSource = data;
        //        ddlStream.DataTextField = "CourseStreamName";
        //        ddlStream.DataValueField = "StreamId";
        //        ddlStream.DataBind();
        //        ddlStream.Items.Insert(0, new ListItem("Select", "0"));
        //    }
        //    else
        //    {
        //        ddlStream.ClearSelection();
        //        ddlStream.Enabled = false;
        //        ddlStream.Items.Insert(0, new ListItem("Select", "0"));
        //    }
        //}

        private void BindStreamMode()
        {
            var dv = ClsSingelton.GetMode();

            if (dv.Table.Rows.Count > 0)
            {
                ddlStreammode.DataSource = dv;
                ddlStreammode.DataTextField = "AjMasterValues";
                ddlStreammode.DataValueField = "AjMasterValueId";
                ddlStreammode.DataBind();
                ddlStreammode.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
                ddlStreammode.Items.Insert(0, new ListItem("Select", "0"));
        }


        protected void BtnStreamInsertClick(object sender, EventArgs e)
        {
            lblCourseStreamMsg.Visible = false;
            try
            {

                var result = 0;

                var objCollegeBranchCourseStreamProperty = new CollegeBranchCourseStreamProperty
                {

                    CourseId =
                        Convert.ToInt32(ddlCourseStream.SelectedValue),
                    StreamId =
                        Convert.ToInt32(hndStreamId.Value),
                    CollegeBranchCourseStreamModeId =
                        Convert.ToInt32(ddlStreammode.SelectedValue),
                    CollegeBranchCourseStreamSeat =
                        txtStreamSeatInsert.Text.Trim(),
                    CollegeBranchCourseStreamDuration =
                        txtStreamDurationInsert.Text.Trim(),
                    CollegeBranchCourseStreamFees =
                        txtStreamFeesInsert.Text.Trim(),
                    CollegeBranchCourseStreamEligibity =
                        txtStreamEligibiltyINsert.Text.Trim(),
                    CollegeBranchCourseStreamManagementQuotaSeat =
                        txtStreamQuotaSeatInsert.Text.Trim(),
                    CollegeBranchCourseStreamLateralEntrySeat =
                        txtLateralSeatInsert.Text.Trim(),
                    CollegeBranchCourseStreamStatus =
                        chkStreamStatus.Checked
                };
                var errMsg = "";
                if (string.IsNullOrEmpty(hdnCollegeCourseStreamId.Value))
                {
                    objCollegeBranchCourseStreamProperty.CollegeBranchId = Convert.ToInt32(hdnCollegeId.Value);

                    result = CollegeProvider.Instance.InsertCollegeBranchCourseStreamInfoByCollegeId(
                        objCollegeBranchCourseStreamProperty, _objSecurePage.LoggedInUserId,
                        out errMsg);
                }
                else
                {
                    objCollegeBranchCourseStreamProperty.CollegeBranchCourseId =
                        Convert.ToInt32(hdnCollegeCourseId.Value);
                    objCollegeBranchCourseStreamProperty.CollegeBranchCourseStreamId =
                        Convert.ToInt32(hdnCollegeCourseStreamId.Value);
                    result =
                        CollegeProvider.Instance.UpdateCollegeBranchCourseStreamInfo(
                            objCollegeBranchCourseStreamProperty, _objSecurePage.LoggedInUserId,
                            out errMsg);
                }
                lblCourseStreamMsg.Visible = true;
                lblCourseStreamMsg.Text = errMsg;
                if (result > 0)
                {
                    lblCourseStreamMsg.CssClass = "success";
                    ClearStreamFields();
                    hdnCollegeCourseId.Value = "0";
                    btnStreamInsert.Text = "Insert";
                    BindCollegeCourseStreamList(hdnCollegeId.Value);

                }
                else
                    lblCourseStreamMsg.CssClass = "info";
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BtnStreamInsertClick in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        private void BindCollegeCourseStreamList(string collegeId)
        {
            try
            {

                var data =
                    CollegeProvider.Instance.GetCollegeCourseStreamListByCollegeBranchId(Convert.ToInt16(collegeId));
                if (data.Count > 0)
                {
                    CustomPaging2.Visible = true;
                    rptCourseStream.Visible = true;
                    CustomPaging2.BindDataWithPaging(rptCourseStream, Common.ConvertToDataTable(data), false, false);

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing BindCollegeCourseStreamList in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void StreamPagerPageIndexChanged(object sender, EventArgs e)
        {
            try
            {

                var data =
                    CollegeProvider.Instance.GetCollegeCourseStreamListByCollegeBranchId(
                        Convert.ToInt16(hdnCollegeId.Value));
                if (data.Count > 0)
                {
                    CustomPaging2.Visible = true;
                    rptCourseStream.Visible = true;
                    CustomPaging2.BindDataWithPaging(rptCourseStream, Common.ConvertToDataTable(data));

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing BindCollegeCourseStreamList in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }


        private void ClearStreamFields()
        {
            txtStreamDurationInsert.Text = string.Empty;
            txtStreamEligibiltyINsert.Text = string.Empty;
            txtStreamFeesInsert.Text = string.Empty;
            txtStreamQuotaSeatInsert.Text = string.Empty;
            txtStreamSeatInsert.Text = string.Empty;
            ddlCourseStream.ClearSelection();
            ddlStreammode.ClearSelection();
            ddlStream.ClearSelection();
            txtLateralSeatInsert.Text = string.Empty;
            chkStreamStatus.Checked = false;
            hdnCollegeCourseStreamId.Value = "";
        }

        #endregion

        #region exam

        private void BindCollegeCourseExamList(string collegeId)
        {
            try
            {
                var data = CollegeProvider.Instance.GetCollegeCourseExamListByCollegeBranchId(Convert.ToInt16(collegeId));
                if (data.Count <= 0) return;
                rptExam.DataSource = data;
                rptExam.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCollegeCourseExamList in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }



        protected void BtnExamClick(object sender, EventArgs e)
        {
            try
            {
                var result = 0;
                var objCollegeBranchCourseExamProperty = new CollegeBranchCourseExamProperty
                {

                    ExamId =
                        Convert.ToInt32(hndSelectedExam.Value),
                    CollegeCourseExamStatus = chkExamStatus.Checked

                };
                var errMsg = "";
                if (!string.IsNullOrEmpty(hdnExamId.Value))
                {
                    objCollegeBranchCourseExamProperty.CollegeBranchCourseId = Convert.ToInt32(hdnCourseExamId.Value);
                    objCollegeBranchCourseExamProperty.CollegeBranchCourseExamId = Convert.ToInt32(hdnExamId.Value);
                    result =
                        CollegeProvider.Instance.UpdateCollegeBranchCourseExamInfo(objCollegeBranchCourseExamProperty,
                                                                                   new SecurePage().LoggedInUserId,
                                                                                   out errMsg);
                }
                else
                {
                    objCollegeBranchCourseExamProperty.CollegeBranchId = Convert.ToInt32(hdnCollegeId.Value);
                    objCollegeBranchCourseExamProperty.CourseId = Convert.ToInt32(ddlCoursesExam.SelectedValue);
                    result =
                        CollegeProvider.Instance.InsertCollegeBranchCourseExamInfoByCollegeId(
                            objCollegeBranchCourseExamProperty, new SecurePage().LoggedInUserId,
                            out errMsg);
                }
                lblExamMsg.Visible = true;
                lblExamMsg.Text = errMsg;
                if (result > 0)
                {
                    lblExamMsg.CssClass = "success";
                    ClearExamFields();
                    btnExam.Text = "Insert";
                    BindCollegeCourseExamList(hdnCollegeId.Value);
                }
                else
                    lblExamMsg.CssClass = "info";
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnExam_Click in account/collegeprofile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        private void ClearExamFields()
        {

            ddlCoursesExam.ClearSelection();
            ddlExam.ClearSelection();
            chkExamStatus.Checked = false;
            hdnExamId.Value = "";
        }

        #endregion

        #region hostel

        private void BindHostelCategory()
        {
            var data =
                CollegeProvider.Instance.GetAllHostelCategory()
                               .Where(result => result.HostelCategoryStatus == true)
                               .ToList();
            if (data.Count > 0)
            {

                ddlHostelMasterInsert.DataSource = data;
                ddlHostelMasterInsert.DataTextField = "HostelCategoryType";
                ddlHostelMasterInsert.DataValueField = "HostelCategoryId";
                ddlHostelMasterInsert.DataBind();
                ddlHostelMasterInsert.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlHostelMasterInsert.Items.Insert(0, new ListItem("Select", "0"));
            }
        }



        protected void BtnHostelClick(object sender, EventArgs e)
        {
            try
            {
                var result = 0;

                var objCollegeBranchCourseHostelProperty = new CollegeBranchCourseHostelProperty
                {


                    CollegeBranchCourseHostelCharge =
                        txtHostelChargeInsert.Text.Trim() != ""
                            ? txtHostelChargeInsert.Text.Trim()
                            : "N/A",
                    CollegeBranchCourseHostelLocation =
                        txtHostelLocationInsert.Text.Trim() != ""
                            ? txtHostelLocationInsert.Text.Trim()
                            : "N/A",
                    HostelCategoryId =
                        Convert.ToInt16(
                            ddlHostelMasterInsert.SelectedValue),
                    IsCollegeBranchCourseHostelHasInternet =
                        rbtInternetInsert.SelectedValue == "0"
                            ? true
                            : false,
                    IsCollegeBranchCourseHostelHasAC =
                        rbtAcInsert.SelectedValue == "0"
                            ? true
                            : false,
                    IsCollegeBranchCourseHostelHasLoundry =
                        rbtLoundaryInsert.SelectedValue == "0"
                            ? true
                            : false,
                    IsCollegeBranchCourseHostelHasPowerBackup =
                        rbtPowerInsert.SelectedValue == "0"
                            ? true
                            : false,
                    CollegeBranchCourseHostelStatus = chkHostelStatus.Checked,


                };
                var errMsg = "";
                if (!string.IsNullOrEmpty(hdnHostelId.Value))
                {

                    objCollegeBranchCourseHostelProperty.CollegeBranchCourseHostelId = Convert.ToInt16(hdnHostelId.Value);
                    objCollegeBranchCourseHostelProperty.CollegeBranchCourseId = Convert.ToInt16(hdnHostelCourseId.Value);
                    result =
                        CollegeProvider.Instance.UpdateCollegeBranchHostelInfo(objCollegeBranchCourseHostelProperty,
                                                                               new SecurePage().LoggedInUserId,
                                                                               out errMsg);
                }
                else
                {
                    objCollegeBranchCourseHostelProperty.CollegeBranchId = Convert.ToInt32(hdnCollegeId.Value);
                    objCollegeBranchCourseHostelProperty.CourseId = Convert.ToInt32(ddlCoursesHostel.SelectedValue);
                    result =
                        CollegeProvider.Instance.InsertCollegeBranchHostelInfoInsert(
                            objCollegeBranchCourseHostelProperty, new SecurePage().LoggedInUserId,
                            out errMsg);
                }
                lblHostelMsg.Visible = true;
                lblHostelMsg.Text = errMsg;
                if (result > 0)
                {
                    lblHostelMsg.CssClass = "success";
                    ClearHostelFields();
                    btnHostelInsert.Text = "Insert";
                    BindCollegeCourseHostel(hdnCollegeId.Value);
                }
                else
                    lblHostelMsg.CssClass = "info";
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BtnHostelClick in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }

        private void BindCollegeCourseHostel(string collegeId)
        {
            try
            {
                var data = CollegeProvider.Instance.GetCollegeCourseHostelByCollegeBranchId(Convert.ToInt16(collegeId));
                if (data.Count > 0)
                {
                    rptHostel.DataSource = data;
                    rptHostel.DataBind();
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCollegeCourseHostel in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void ClearHostelFields()
        {
            txtHostelChargeInsert.Text = string.Empty;
            txtHostelLocationInsert.Text = string.Empty;
            ddlHostelMasterInsert.ClearSelection();
            ddlCoursesHostel.ClearSelection();
            rbtAcInsert.SelectedValue = "0";
            rbtInternetInsert.SelectedValue = "0";
            rbtLoundaryInsert.SelectedValue = "0";
            rbtPowerInsert.SelectedValue = "0";
            chkHostelStatus.Checked = false;
            hdnHostelId.Value = "";
        }

        #endregion

        #region Rank

        private void BindRank()
        {
            var data = CollegeProvider.Instance.GetAllCollegeRankSourceList();
            if (data.Count > 0)
            {
                ddlRankSourceInsert.DataSource = data;
                ddlRankSourceInsert.DataTextField = "CollegeRankSourceName";
                ddlRankSourceInsert.DataValueField = "CollegeRankSourceId";
                ddlRankSourceInsert.DataBind();
                ddlRankSourceInsert.Items.Insert(0, new ListItem("Select", "0"));
                if (!string.IsNullOrEmpty(txtRankSource.Text))
                {
                    ddlRankSourceInsert.SelectedItem.Text = txtRankSource.Text.Trim();
                }
            }
            else
            {
                ddlRankSourceInsert.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        private void BindCollegeCourseRankList(string collegeId)
        {
            try
            {
                var data = CollegeProvider.Instance.GetCollegeCourseRankByCollegeId(Convert.ToInt16(collegeId));
                if (data.Count > 0)
                {
                    rptRankSource.DataSource = data;
                    rptRankSource.DataBind();
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCollegeCourseRankList in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }



        protected void BtnRankSourceInsertClick(object sender, EventArgs e)
        {
            try
            {
                var errMsg = "";
                var objRankSource = new CollegeRankSource()
                {
                    CollegeRankSourceName = txtRankSource.Text.Trim(),
                    CollegeRankSourceStatus = true,
                };
                var result = CollegeProvider.Instance.InsertCollegeRankSource(objRankSource,
                                                                              _objSecurePage.LoggedInUserId, out errMsg);
                if (result > 0)
                {
                    BindRank();
                    txtRankSource.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnRankSourceInsert_Click in collegeprofile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }

        protected void BtnRankOverAllInsertClick(object sender, EventArgs e)
        {
            try
            {
                var result = 0;
                var objCollegeBranchRankProperty = new CollegeBranchRankProperty
                {
                    CollegeRankSourceId =
                        Convert.ToInt32(ddlRankSourceInsert.SelectedValue),
                    CollegeOverAllRank =
                        txtRankOverallInsert.Text,
                    CollegeRankYear =
                        !string.IsNullOrEmpty(txtRanKYearInsert.Text)
                            ? Convert.ToInt32(txtRanKYearInsert.Text)
                            : 0,
                    CollegeRankStatus = chkRankStatus.Checked

                };
                var errMsg = "";
                if (string.IsNullOrEmpty(hdnCourseRankId.Value))
                {
                    objCollegeBranchRankProperty.CollegeRankStatus = false;
                    objCollegeBranchRankProperty.CollegeBranchId = Convert.ToInt32(hdnCollegeId.Value);
                    objCollegeBranchRankProperty.CourseId = Convert.ToInt32(ddlCoursesRank.SelectedValue);
                    result =
                        CollegeProvider.Instance.InsertCollegeBranchRankByCollegeId(objCollegeBranchRankProperty,
                                                                                    _objSecurePage.LoggedInUserId,
                                                                                    out errMsg);
                }
                else
                {
                    objCollegeBranchRankProperty.CollegeBranchCourseId = Convert.ToInt16(hdnCourseRankId.Value);
                    objCollegeBranchRankProperty.CollegeRankId = Convert.ToInt16(hdnRankId.Value);
                    result =
                        CollegeProvider.Instance.UpdateCollegeBranchRank(objCollegeBranchRankProperty,
                                                                         _objSecurePage.LoggedInUserId,
                                                                         out errMsg);
                }
                lblRankMsg.Visible = true;
                lblRankMsg.Text = errMsg;
                if (result > 0)
                {
                    lblRankMsg.CssClass = "success";
                    ClearRankField();
                    btnRankOverAllInsert.Text = "Insert";
                    BindCollegeCourseRankList(hdnCollegeId.Value);
                }
                else
                    lblRankMsg.CssClass = "info";
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnRankOverAll_Click in collegeprofile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }

        private void ClearRankField()
        {
            txtRanKYearInsert.Text = string.Empty;
            txtRankOverallInsert.Text = string.Empty;
            ddlCoursesRank.ClearSelection();
            ddlRankSourceInsert.ClearSelection();
            ddlRankSourceInsert.SelectedValue = "0";
            chkRankStatus.Checked = false;
            hdnCourseRankId.Value = "";

        }

        #endregion

        #region HighLights

        private void BindCollegeCourseHighLights(string collegeId)
        {
            try
            {
                var data =
                    CollegeProvider.Instance.GetCollegeCourseHighLightsByCollegeBranchId(Convert.ToInt16(collegeId));
                if (data.Count > 0)
                {
                    rptHighLights.DataSource = data;
                    rptHighLights.DataBind();
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing BindCollegeCourseHighLights in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void RptHighLightsItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    lblHighLightsMsg.Visible = false;
                    hdnHighLights.Value = e.CommandArgument.ToString();
                    var data =
                        CollegeProvider.Instance.GetCollegeCourseHighLightsByHighLightsId(
                            Convert.ToInt16(hdnHighLights.Value));

                    if (data.Count > 0)
                    {
                        var query = data.Select(result => new
                        {
                            result.CollegeBranchCourseHighlight,
                            result.CollegeBranchCourseHighlightStatus,
                            result.CollegeBranchCourseId,
                            result.CourseId

                        }).First();

                        hdnCourseHighLightsId.Value = Convert.ToString(query.CollegeBranchCourseId);
                        txtCourseHighLightsInsert.FckValue = query.CollegeBranchCourseHighlight.ToString();
                        ddlCoursesHigh.SelectedValue = !string.IsNullOrEmpty(query.CourseId.ToString())
                                                           ? Convert.ToString(query.CourseId)
                                                           : "0";
                        chkHighlightsStatus.Checked = query.CollegeBranchCourseHighlightStatus;
                        btnHighLightsInsert.Text = "Upadate";

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
                const string addInfo =
                    "Error while executing rptHighLights_ItemCommand in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }

        protected void BtnHighLightsInsertClick(object sender, EventArgs e)
        {
            try
            {
                var CollegeId = hdnCollegeCourseId.Value;
                if (!string.IsNullOrEmpty(txtCourseHighLightsInsert.FckValue))
                {
                    var result = 0;
                    var objCollegeBranchCourseHighlightsProperty = new CollegeBranchCourseHighlightsProperty
                    {

                        CollegeBranchCourseHighlight =
                            txtCourseHighLightsInsert.FckValue,
                        CollegeBranchCourseHighlightStatus =
                            chkHighlightsStatus.Checked,

                    };
                    var errMsg = "";
                    if (string.IsNullOrEmpty(hdnCourseHighLightsId.Value) || hdnCourseHighLightsId.Value.Equals("0"))
                    {
                        objCollegeBranchCourseHighlightsProperty.CollegeBranchId =
                            Convert.ToInt16(Convert.ToInt32(hdnCollegeId.Value));
                        objCollegeBranchCourseHighlightsProperty.CourseId = Convert.ToInt16(ddlCoursesHigh.SelectedValue);
                        result =
                            CollegeProvider.Instance.InsertCollegeBranchCourseHighlightsByCollege(
                                objCollegeBranchCourseHighlightsProperty, _objSecurePage.LoggedInUserId,
                                out errMsg);
                    }
                    else
                    {
                        objCollegeBranchCourseHighlightsProperty.CollegeBranchCourseId =
                            Convert.ToInt16(hdnCourseHighLightsId.Value);

                        

                        objCollegeBranchCourseHighlightsProperty.CollegeBranchCourseHighlightId =
                            Convert.ToInt16(hdnHighLights.Value);
                        result =
                            CollegeProvider.Instance.UpdateCollegeBranchCourseHighlights(
                                objCollegeBranchCourseHighlightsProperty, _objSecurePage.LoggedInUserId,
                                out errMsg);

                    }
                    lblHighLightsMsg.Visible = true;
                    lblHighLightsMsg.Text = errMsg;
                    if (result > 0)
                    {
                        ClearHighLightS();
                        lblHighLightsMsg.CssClass = "success";
                        btnHighLightsInsert.Text = "Insert";
                        BindCollegeCourseHighLights(hdnCollegeId.Value);
                    }
                    else
                        lblHighLightsMsg.CssClass = "info";
                }
                else
                {
                    spnHighError.Visible = true;
                    spnHighError.InnerHtml = "Field highlights cannot be blank";
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnHighLights_Click in collegeprofile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }

        private void ClearHighLightS()
        {
            txtCourseHighLightsInsert.FckValue = string.Empty;
            ddlCoursesHigh.ClearSelection();
            chkHighlightsStatus.Checked = false;
            hdnCourseHighLightsId.Value = "";
        }

        #endregion

        #region Placement

        protected void BtnPlacementInsertClick(object sender, EventArgs e)
        {
            try
            {
                var result = 0;
                var objCollegeBranchCoursePlacementProperty = new CollegeBranchCoursePlacementProperty
                {
                    CollegeBranchCoursePlacementAvgSalaryOffered =
                        txtStudentSalary.Text.Trim(),
                    CollegeBranchCoursePlacementYear =
                        txtCompanyNameyear.Text.Trim(),
                    CollegeBranchCoursePlacementNoOfStudentHired =
                        txtStudentHired.Text.Trim(),
                    CollegeBranchCoursePlacementCompanyName =
                        txtCompanyName.Text.Trim(),
                    CollegeBranchCoursePlacementStatus = chkPlacement.Checked,
                    CourseId = Convert.ToInt16(ddlCoursesPlacement.SelectedValue)


                };
                var errMsg = "";
                if (string.IsNullOrEmpty(hdnPlacementCourseiD.Value) || hdnPlacementCourseiD.Value.Equals("0"))
                {
                    objCollegeBranchCoursePlacementProperty.CollegeBranchName = hdnCollegeName.Value;

                    result =
                        CollegeProvider.Instance.InsertCollegePlacementByCourse(
                            objCollegeBranchCoursePlacementProperty, _objSecurePage.LoggedInUserId,
                            out errMsg);
                }
                else
                {
                    objCollegeBranchCoursePlacementProperty.CollegeBranchCourseId =
                        Convert.ToInt32(hdnPlacementCourseiD.Value);

                    objCollegeBranchCoursePlacementProperty.CollegeBranchCoursePlacementId =
                        Convert.ToInt32(hdnPlacementId.Value);
                    result =
                        CollegeProvider.Instance.UpdateCollegePlacement(
                            objCollegeBranchCoursePlacementProperty, _objSecurePage.LoggedInUserId,
                            out errMsg);

                }
                lblPlacementMsg.Visible = true;
                lblPlacementMsg.Text = errMsg;
                if (result > 0)
                {
                    ClearPlacement();
                    lblPlacementMsg.CssClass = "success";
                    btnPlacementInsert.Text = "Insert";
                    BindPlacemntByCollegeId(hdnCollegeId.Value);
                }
                else
                    lblPlacementMsg.CssClass = "info";
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnPlacementInsert in collegeprofile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }

        private void ClearPlacement()
        {
            txtStudentSalary.Text = string.Empty;
            txtCompanyNameyear.Text = string.Empty;
            txtStudentHired.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            ddlCoursesPlacement.ClearSelection();
            chkPlacement.Checked = false;
            hdnPlacementCourseiD.Value = "";
        }

        private void BindPlacemntByCollegeId(string collegeId)
        {
            try
            {
                var data =
                    CollegeProvider.Instance.GetCollegeCourseTopHirerListByCollegeId(Convert.ToInt16(collegeId));
                if (data.Count > 0)
                {
                    rptPlacemnet.DataSource = data;
                    rptPlacemnet.DataBind();
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing BindCollegeCourseHighLights in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }



        #endregion

        #region Event

        protected void TxtSaveClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(hndEventId.Value)))
                InsertUpdateCollegeEvent(0, Convert.ToInt16(ddlCourseEvent.SelectedValue), lblCollegeName.Text.Trim(),
                                         txtEventName.Text, txtEventLocation.Text,
                                         txtEventDate.Text, chkEvent.Checked, txtEventDesc.Text);
            else
            {
                InsertUpdateCollegeEvent(Convert.ToInt32(hndEventId.Value),
                                         Convert.ToInt16(ddlCourseEvent.SelectedValue), lblCollegeName.Text.Trim(),
                                         txtEventName.Text, txtEventLocation.Text,
                                         txtEventDate.Text, chkEvent.Checked, txtEventDesc.Text);
                btnSave.Text = "Save";
                hndEventId.Value = "";
            }

        }

        // Method to Insert Update The College event
        private void InsertUpdateCollegeEvent(int evnetId, int courseId, string collegeName, string eventName,
                                              string eventLocation, string eventDateTime, bool evenetStatus,
                                              string eventDesc)
        {
            try
            {
                string errMsg;
                var i = CollegeProvider.Instance.InsertUpdateCollegeEvent(collegeName, courseId, eventName,
                                                                          eventLocation,
                                                                          Common.GetDateFromString(eventDateTime),
                                                                          eventDesc,
                                                                          evenetStatus, out errMsg, evnetId);
                lblMsg.Visible = true;
                lblMsg.Text = errMsg;
                if (i > 0)
                {
                    lblMsg.CssClass = "success";
                    ClearControl();
                    BindEventList();
                }

                else
                    lblMsg.CssClass = "info";
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateCollegeEvent in collegeprofile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        // Method to clear The Control
        private void ClearControl()
        {
            txtEventDate.Text = string.Empty;
            txtEventLocation.Text = string.Empty;
            txtEventName.Text = string.Empty;
            txtEventDesc.Text = string.Empty;
            ddlCourseEvent.ClearSelection();
            chkEvent.Checked = false;
        }

        private void EventPagerPageIndexChanged(object sender, EventArgs e)
        {
            var dt = CollegeProvider.Instance.GetAllEvent();
            CustompagingEvent.BindDataWithPaging(rptEventList, dt);
        }

        // Method to Bind The Event List
        private void BindEventList()
        {
            try
            {
                var objDataSet = new DataSet();
                var dt = CollegeProvider.Instance.GetAllEvent();
                var dv = dt.DefaultView;
                dv.RowFilter = " AjCollegeBranchId=" + hdnCollegeId.Value;
                var objDt = dv.ToTable();
                objDataSet.Tables.Add(objDt);
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    CustompagingEvent.BindDataWithPaging(rptEventList, objDataSet.Tables[0], false, false);
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindEventList in collegeprofile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }



        #endregion

        #region Notice

        private void BindNoticeCategory()
        {
            try
            {
                var noticeData =
                    NewsArticleNoticeProvider.Instance.GetAllNoticeCategoryList()
                                             .Where(result => result.NoticeCategoryStatus)
                                             .ToList();
                if (noticeData.Count <= 0)
                {
                    ddlNoticeCategory.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlNoticeCategory.DataSource = noticeData;
                    ddlNoticeCategory.DataTextField = "NoticeCategoryName";
                    ddlNoticeCategory.DataValueField = "NoticecategoryId";
                    ddlNoticeCategory.DataBind();
                    ddlNoticeCategory.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindNoticeCategory in CollegeProfile.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void btnUpload1_Click(object sender, EventArgs e)
        {
            try
            {
                var filename = System.IO.Path.GetFileName(FileUpload2.FileName);
                var path = new Common().GetFilepath("NoticeImage");
                FileUpload2.SaveAs(Server.MapPath(path) + filename);

                if (filename != null)
                {
                    Session["NoticeImage"] = filename;
                    imgNoticeImage.ImageUrl = String.Format("{0}{1}", "/image.axd?Notice=",
                                                            string.IsNullOrEmpty(filename)
                                                                ? "NoImage.jpg"
                                                                : filename);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp",
                                                        "<script type='text/javascript'>SetTabOnPartialUpdate();</script>",
                                                        false);

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  NoticeImagUpload in CollegeProfile.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void BindNoticeDetails()
        {

            try
            {
                var data =
                    NewsArticleNoticeProvider.Instance.GetNoticeListOfParticulerCollege(
                        Convert.ToInt32(hdnCollegeId.Value));
                if (data.Count > 0)
                {
                    noticePaging.Visible = true;
                    rptNoticeDetails.Visible = true;
                    noticePaging.BindDataWithPaging(rptNoticeDetails, Common.ConvertToDataTable(data), false, false);
                }
                else
                {
                    noticePaging.Visible = false;
                    rptNoticeDetails.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindNoticeDetails in CollegeProfile.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void NoticePagerPageIndexChanged(object sender, EventArgs e)
        {
            lblNoticeMsg.Visible = false;

            try
            {
                var data =
                    NewsArticleNoticeProvider.Instance.GetNoticeListOfParticulerCollege(
                        Convert.ToInt32(hdnCollegeId.Value));
                if (data.Count > 0)
                {
                    noticePaging.Visible = true;
                    rptNoticeDetails.Visible = true;
                    noticePaging.BindDataWithPaging(rptNoticeDetails, Common.ConvertToDataTable(data));
                }
                else
                {
                    noticePaging.Visible = false;
                    rptNoticeDetails.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  NoticePagerPageIndexChanged in CollegeProfile.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void BtnSaveNoticeClick(object sender, EventArgs e)
        {
            InsertUpdateNotice();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp",
                                                "<script type='text/javascript'>SetTabOnPartialUpdate();</script>",
                                                false);
        }

        private void InsertUpdateNotice()
        {
            lblNoticeMsg.Visible = false;
            try
            {
                if (!string.IsNullOrEmpty(txtCollegeNotice.FckValue))
                {
                    var filename = "";
                    var result = 0;
                    var errMsg = "";
                    if (FileUpload2.HasFile)
                    {
                        filename = System.IO.Path.GetFileName(FileUpload2.FileName);

                        var path = new Common().GetFilepath("NoticeImage");
                        FileUpload2.SaveAs(Server.MapPath(path) + filename);
                    }

                    var objNoticeDetails = new NoticeDetails
                    {

                        NoticeSubject = txtNoticeSubject.Text.Trim(),
                        NoticeDesc = txtCollegeNotice.FckValue.Trim(),
                        RealtedCollegeId = Convert.ToInt16(hdnCollegeId.Value),
                        NoticeTypeId = Convert.ToInt16(ddlNoticeCategory.SelectedValue),
                        NoticeShortDesc = txtNoticeShortDesc.Text.Trim(),
                        NoticeStatus = chkNotice.Checked

                    };
                    if (string.IsNullOrEmpty(hdnNoticeId.Value))
                    {
                        objNoticeDetails.NoticeImage = filename;
                        result = NewsArticleNoticeProvider.Instance.InsertNoticeDetails(objNoticeDetails,
                                                                                        _objSecurePage.LoggedInUserId,
                                                                                        out errMsg);

                    }
                    else
                    {
                        objNoticeDetails.NoticeImage = !string.IsNullOrEmpty(filename) ? filename : hdnNoticeImage.Value;
                        objNoticeDetails.NoticeId = Convert.ToInt16(hdnNoticeId.Value);
                        result = NewsArticleNoticeProvider.Instance.UpdateNoticeDetails(objNoticeDetails,
                                                                                        _objSecurePage.LoggedInUserId,
                                                                                        out errMsg);
                    }
                    lblNoticeMsg.Visible = true;
                    lblNoticeMsg.Text = errMsg;
                    if (result > 0)
                    {
                        imgNoticeImage.ImageUrl = "";
                        lblNoticeMsg.CssClass = "success";
                        ClearNoticeFields();
                        btnSaveNotice.Text = "Insert";
                        BindNoticeDetails();
                    }
                    else
                        lblNoticeMsg.CssClass = "info";
                }
                else
                {
                    errNotice.InnerHtml = "Field notice description cannot be blank";
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex;
                }
                const string addInfo =
                    "Error while executing  in InsertUpdateNotice at page CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }



        private void ClearNoticeFields()
        {
            txtCollegeNotice.FckValue = string.Empty;
            txtNoticeShortDesc.Text = string.Empty;
            txtNoticeSubject.Text = string.Empty;
            ddlNoticeCategory.ClearSelection();
            imgNoticeImage.AlternateText = "";
            hdnNoticeId.Value = "";
            imgNoticeImage.CssClass = "fleft hide";
            chkNotice.Checked = false;
        }

        #endregion Notice

        #region testimonial

        private void BindTestimonialDetails()
        {
            try
            {
                var data =
                    new Common().GetTestimonialDetails(0, Convert.ToInt32(hdnCollegeId.Value), 0);

                if (data.Tables.Count > 0)
                {
                    if (data.Tables[0].Rows.Count > 0)
                    {
                        grdTestomonial.Visible = true;
                        TestimonialPager.Visible = true;
                        TestimonialPager.BindDataWithPaging(rptTestimonial, data.Tables[0], false, false);
                    }


                }
                else
                {
                    grdTestomonial.Visible = false;
                    TestimonialPager.Visible = false;
                    rptTestimonial.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo =
                    "Error in Executing  BindTestimonialDetails in CollegeProfile.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }


        private void TestiPagerPageIndexChanged(object sender, EventArgs e)
        {

            try
            {
                lblMsgTestimonial.Visible = false;
                var data =
                    new Common().GetTestimonialDetails(0, Convert.ToInt32(hdnCollegeId.Value), 0);

                if (data.Tables.Count > 0)
                {
                    if (data.Tables[0].Rows.Count > 0)
                    {

                        TestimonialPager.Visible = true;
                        TestimonialPager.BindDataWithPaging(rptTestimonial, data.Tables[0]);
                    }


                }
                else
                {
                    TestimonialPager.Visible = false;
                    rptTestimonial.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo =
                    "Error in Executing  TestiPagerPageIndexChanged in CollegeProfile.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void BtnSaveTestimonialClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(hdnTestimonial.Value)))
                InsertUpdateCollegeTestimonial(0, txtTestimonial.FckValue, chkTestimonialStatus.Checked);
            else
            {
                InsertUpdateCollegeTestimonial(Convert.ToInt32(hdnTestimonial.Value), txtTestimonial.FckValue.Trim(),
                                               chkTestimonialStatus.Checked);
                btnSaveTestimonial.Text = "Insert";
                hdnTestimonial.Value = "";
            }
        }

        private void InsertUpdateCollegeTestimonial(int testimonialId, string testimonial, bool testimonialStatus)
        {
            lblMsgTestimonial.Visible = false;
            try
            {
                if (!string.IsNullOrEmpty(testimonial))
                {
                    var errMsg = "";
                    var i = new Common().InsertUpdateCollegeTestimonal(_objSecurePage.LoggedInUserId,
                                                                       Convert.ToInt32(hdnCollegeId.Value),
                                                                       testimonial.Trim(), out errMsg,
                                                                       testimonialStatus, testimonialId);
                    lblMsgTestimonial.Visible = true;
                    lblMsgTestimonial.Text = errMsg;
                    if (i > 0)
                    {
                        btnSaveTestimonial.Text = "Save";
                        lblMsgTestimonial.CssClass = "success";
                        txtTestimonial.FckValue = string.Empty;
                        chkTestimonialStatus.Checked = false;
                        BindTestimonialDetails();
                    }

                    else
                        lblMsgTestimonial.CssClass = "info";
                }
                else
                {
                    chkTestimonialStatus.Checked = false;
                    lblMsgTestimonial.CssClass = "error";
                    lblMsgTestimonial.Visible = true;
                    lblMsgTestimonial.Text = "Field Testimonial cannot be blank";
                }
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing InsertUpdateCollegeTestimonial in collegeprofile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }



        #endregion

        #region Visitors

        private void GetColLegeVisiotrsCount(int courseId)
        {
            try
            {
                var data = CollegeProvider.Instance.GetCollegeCourseListByCollegeId(Convert.ToInt16(hdnCollegeId.Value));
                data = data.Where(result => result.CourseId == courseId).ToList();
                if (data.Count > 0)
                {
                    var collegeCourseId = data.First().CollegeBranchCourseId;
                    var pageCount = new Common().GetCollegePageClick(collegeCourseId);
                    lblCollegeVisitors.Text = pageCount > 0 ? pageCount.ToString(CultureInfo.InvariantCulture) : "0";

                }
                var totalData = new Common().GetTotalPageClick();
                if (totalData > 0)
                {
                    lblTotalVisitors.Text = totalData.ToString();
                    CollegePlacementChart(lblCollegeVisitors.Text, lblTotalVisitors.Text);
                }
                else
                {
                    lblTotalVisitors.Text = "0";
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing  in GetColLegeVisiotrsCount at page CollegeProfile.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void DdlCoursVisitorsSelectedIndexChanged(object sender, EventArgs e)
        {
            GetColLegeVisiotrsCount(Convert.ToInt32(ddlCourseForVisitors.SelectedValue));

        }

        #endregion

        #region PlacementChart
        private void CollegePlacementChart(string collegeVisitor, string totalvisitor)
        {
            try
            {
                var dt = new DataTable();
                dt.Columns.Add("CompareText", typeof(string));
                dt.Columns.Add("CompareValue", typeof(string));

                var dtrow = dt.NewRow(); // Create New Row
                dtrow["CompareText"] = "AdmissionJankari"; //Bind Data to Columns
                dtrow["CompareValue"] = totalvisitor;
                dt.Rows.Add(dtrow);
                dtrow = dt.NewRow();
                dtrow["CompareText"] = lblCollegeName.Text.Trim(); //Bind Data to Columns
                dtrow["CompareValue"] = collegeVisitor;
                dt.Rows.Add(dtrow);
                rankChart.DataSource = dt;
                rankChart.ChartAreas["ChartArea2"].AxisX.Title = "AdmissionJankari Vs. " + lblCollegeName.Text;
                // here i am giving the title of the y-axis
                rankChart.ChartAreas["ChartArea2"].AxisY.Title = "Total Visitor";
                rankChart.Series["rankSeries"].XValueMember = "CompareText";
                // here i am binding the y-axisvalue with the chart control
                rankChart.Series["rankSeries"].YValueMembers = "CompareValue";
                rankChart.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing  in CollegePlacementChart at page CollegeProfile.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        #endregion

        #region banner
        private void CheckBannerCount()
        {
            try
            {
                var objDataSet = new Common().GetBannerListByCollegeId(Convert.ToInt32(hdnCollegeId.Value), "COUNT");
                if (objDataSet.Rows.Count > 0)
                {

                    var query1 = objDataSet.AsEnumerable().Select(result => new
                    {
                        CourseName = result.Field<string>("AjCourseName"),
                        CourseId = result.Field<Int32>("AjCourseId"),
                    }).Distinct().ToList();
                    ddlBannerCourse.DataSource = query1;
                    ddlBannerCourse.DataTextField = "CourseName";
                    ddlBannerCourse.DataValueField = "CourseId";
                    ddlBannerCourse.DataBind();
                    liBanner.Visible = true;
                    tabBanner.Visible = true;
                }
                else
                {
                    liBanner.Visible = false;
                    tabBanner.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  CheckBannerCount in CollegeProfile.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void BtnBannerSubmitClick(object sender, EventArgs e)
        {
            try
            {
                var errMsg = "";
                var result = new Common().InsertBannerbyCollegeUser(Convert.ToInt32(hdnCollegeId.Value),
                                                                    new SecurePage().LoggedInUserId,
                                                                    Convert.ToInt32(ddlBannerCourse.SelectedValue),
                                                                    hdnBanner.Value,
                                                                    txtToolTip.Text.Trim(), txtUrl.Text.Trim(),
                                                                    out errMsg);
                lblBannerResult.Visible = true;
                lblBannerResult.Text = errMsg;
                if (result > 0)
                {
                    lblBannerResult.CssClass = "success";
                    BannerClearControl();
                }
                else
                {
                    lblBannerResult.CssClass = "error";

                }
                GetCollegeBannerList();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp",
                                                    "<script type='text/javascript'>SetTabBanner();</script>",
                                                    false);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BtnBannerSubmitClick in CollegeProfile.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void BannerClearControl()
        {
            txtUrl.Text = string.Empty;
            txtToolTip.Text = string.Empty;
            ddlBannerCourse.ClearSelection();
            bannerImage.Src = "";
        }

        private void CheckProductCountAfterPayment()
        {
            try
            {
                var objDataSet = _objCommon.CheckProductCountAfterPayment(new SecurePage().LoggedInUserId);
                if (objDataSet.Rows.Count > 0)
                {
                    if (Convert.ToString(objDataSet.Rows[0][0]) == "0")
                    {
                        liAdvertiseList.Visible = false;
                        divAdvertiseContainer.Visible = false;
                    }
                    else
                    {
                        liAdvertiseList.Visible = true;
                        divAdvertiseContainer.Visible = true;

                    }
                }
                else
                {
                    liAdvertiseList.Visible = false;
                    divAdvertiseContainer.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  CheckProductCountAfterPayment in CollegeProfile.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void GetCollegeBannerList()
        {
            try
            {
                var objDataSet = _objCommon.GetCollegeBannerList(Convert.ToInt32(hdnCollegeId.Value));
                if (objDataSet.Tables[0].Rows.Count <= 0) return;
                rptBannerList.DataSource = objDataSet.Tables[0];
                rptBannerList.DataBind();

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  GetCollegeBannerList in CollegeProfile.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        #endregion
    }
}