using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class UpdateCollegeDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
            ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += CoursePagerPageIndexChanged;
            CustomPaging1.PageSize = ClsSingelton.PageSize;
            CustomPaging1.ButtonsCount = ClsSingelton.PageButtonCount;
            CustomPaging1.PagerPageIndexChanged += StreamPagerPageIndexChanged;
            FileUpload1.uploadToDirectory = new Common().GetFilepath("UniversityImage");
            if (IsPostBack) return;
            BindInstituteType();
            BindGroup();
            BindCountry(); 
            BindStream();
            BindStreamMode(); 
            BindExam();
            BindState(0);
            BindCity(0); 
            BindCourse();
            BindHostel();
            BindUniversity();
            BindManagement(); 
            BindRank();
            if (Request.QueryString["CollegeBranchId"] != null)
            {
                BindCollegeList(Request.QueryString["CollegeBranchId"]);
                BindCollegeCourseList(Request.QueryString["CollegeBranchId"]);
                BindCollegeCourseStreamList(Request.QueryString["CollegeBranchId"]);
                BindCollegeCourseExamList(Request.QueryString["CollegeBranchId"]);
                BindCollegeCourseFacalityList(Request.QueryString["CollegeBranchId"]);
                BindCollegeCourseRankList(Request.QueryString["CollegeBranchId"]);
                BindCollegeCourseHighLights(Request.QueryString["CollegeBranchId"]);
                BindCollegeCourseHostel(Request.QueryString["CollegeBranchId"]);
            }
        }
        protected void CoursePagerPageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var data = CollegeProvider.Instance.GetCollegeCourseListByCollegeId(Convert.ToInt16(Request.QueryString["CollegeBranchId"]));
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
                const string addInfo = "Error while executing CoursePagerPageIndexChanged in UpdateCollegeDetails  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void StreamPagerPageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var data =
                    CollegeProvider.Instance.GetCollegeCourseStreamListByCollegeBranchId(Convert.ToInt16(Request.QueryString["CollegeBranchId"]));
                if (data.Count > 0)
                {
                 
                    CustomPaging1.BindDataWithPaging(rptCourseStream, Common.ConvertToDataTable(data));

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing StreamPagerPageIndexChanged in UpdateCollegeDetails  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindManagement()
        {
            var dv = ClsSingelton.GetManagement();
            rbtManagement.DataSource = dv;
            rbtManagement.DataTextField = "AjMasterValues";
            rbtManagement.DataValueField = "AjMasterValueId";
            rbtManagement.DataBind();
           
        }
        private void BindCollegeCourseList(string collegeId)
        {
            try
            {
                var data = CollegeProvider.Instance.GetCollegeCourseListByCollegeId(Convert.ToInt16(collegeId));
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
                const string addInfo = "Error while executing BindCollegeCourseList in UpdateCollegeDetails  :: -> ";
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
                    
                    CustomPaging1.BindDataWithPaging(rptCourseStream, Common.ConvertToDataTable(data));

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCollegeCourseStreamList in UpdateCollegeDetails  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindCollegeCourseExamList(string collegeId)
        {
            try
            {
                var data = CollegeProvider.Instance.GetCollegeCourseExamListByCollegeBranchId(Convert.ToInt16(collegeId));
                if (data.Count > 0)
                {
                    rptExam.DataSource = data;
                    rptExam.DataBind();
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCollegeCourseExamList in UpdateCollegeDetails  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindCollegeCourseFacalityList(string collegeId)
        {
            try
            {
                var data = CollegeProvider.Instance.GetCollegeCourseFacalityByCollegeBranchId(Convert.ToInt16(collegeId));
                if (data.Count > 0)
                {
                    rptCourseFacality.DataSource = data;
                    rptCourseFacality.DataBind();
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCollegeCourseFacalityList in UpdateCollegeDetails  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
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
                const string addInfo = "Error while executing BindCollegeCourseRankList in UpdateCollegeDetails  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
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
                const string addInfo = "Error while executing BindCollegeCourseHighLights in UpdateCollegeDetails  :: -> ";
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
                const string addInfo = "Error while executing BindCollegeCourseHostel in UpdateCollegeDetails  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
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
                    ddlInstituteType.Items.Insert(0, new ListItem("Please Select", "0"));
                }
                else
                {
                    ddlInstituteType.Items.Insert(0, new ListItem("Please Select", "0"));

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
                    ddlCollegeGroup.Items.Insert(0, new ListItem("Please Select", "0"));
                }
                else
                {
                    ddlCollegeGroup.Items.Insert(0, new ListItem("Please Select", "0"));
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
        private void BindCountry()
        {
            var data = CountryProvider.Instance.GetAllCountry();
            if (data.Count > 0)
            {
                ddlCountry.DataSource = data;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            else
            {ddlCountry.Items.Insert(0,new ListItem("Please Select","0"));
                
            }
        }

        private void BindState(int countryId)
        {
            try
            {
                List<StateProperty> data;
                if (countryId == 0)
                {
                    data = StateProvider.Instance.GetAllState();
                }
                else
                {
                    data = StateProvider.Instance.GetStateByCountry(countryId);

                }
                if (data.Count > 0)
                {
                    ddlState.DataSource = data;
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateId";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, new ListItem("Please Select", "0"));
                }
                else
                {
                    ddlState.Items.Insert(0, new ListItem("Please Select", "0"));
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
                if (stateId == 0)
                {
                    data = CityProvider.Instacnce.GetAllCityList();
                }
                else
                {
                    data = CityProvider.Instacnce.GetCityListByState(stateId);

                }
                if (data.Count > 0)
                {
                    ddlCollegeCity.DataSource = data;
                    ddlCollegeCity.DataTextField = "CityName";
                    ddlCollegeCity.DataValueField = "CityId";
                    ddlCollegeCity.DataBind();
                    ddlCollegeCity.Items.Insert(0, new ListItem("Please Select", "0"));
                }
                else
                {
                    ddlCollegeCity.Items.Insert(0, new ListItem("Please Select", "0"));
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

       
        private void BindCollegeList(string collegeBranchId)
        {
            try
            {
                var collegeBasicData = CollegeProvider.Instance.GetCollegeListById(Convert.ToInt16(collegeBranchId));
                if (collegeBasicData.Count > 0)
                {
                    var query = collegeBasicData.Select(result => new
                                                                      {
                                                                          InstituteTypeId = result.InstituteTypeId,
                                                                        CollegeGroupId = result.CollegeGroupId,
                                                                          CollegeBranchName = result.CollegeBranchName,
                                                                          CollegePopulaorName =
                                                                      result.CollegePopulaorName,
                                                                          CollegeManagementTypeId =
                                                                      result.CollegeManagementTypeId,
                                                                          CollegeBranchEst = result.CollegeBranchEst,
                                                                          CollegeBranchDesc = result.CollegeBranchDesc,
                                                                          CollegeBranchAddrs = result.CollegeBranchAddrs,
                                                                          CollegeBranchMobileNo =
                                                                      result.CollegeBranchMobileNo,
                                                                          CollegeBranchPinCode =
                                                                      result.CollegeBranchPinCode,
                                                                          CoillegeBranchEmailId =
                                                                      result.CoillegeBranchEmailId,
                                                                          CollegeBranchFax = result.CollegeBranchFax,
                                                                          CollegeBranchWebsite =
                                                                      result.CollegeBranchWebsite,
                                                                          CollegeBranchCountryId =
                                                                      result.CollegeBranchCountryId,
                                                                          CollegeBranchStateId =
                                                                      result.CollegeBranchStateId,
                                                                          CollegeBranchCityId =
                                                                      result.CollegeBranchCityId,
                                                                          CollegeBranchStatus =
                                                                      result.CollegeBranchStatus,
                                                                          CollegeBranchLogo = result.CollegeBranchLogo
                                                                        
                                                                      }).First();
                    ddlInstituteType.SelectedValue = query.InstituteTypeId.ToString() != ""
                                                         ? query.InstituteTypeId.ToString()
                                                         : "0";

                    ddlCollegeGroup.SelectedValue = query.CollegeGroupId.ToString() != ""
                                                        ? query.CollegeGroupId.ToString()
                                                        : "0";
                    ddlCountry.SelectedValue = query.CollegeBranchCountryId.ToString() != ""
                                                   ? query.CollegeBranchCountryId.ToString()
                                                   : "0";
                    ddlState.SelectedValue = query.CollegeBranchStateId.ToString() != ""
                                                 ? query.CollegeBranchStateId.ToString()
                                                 : "0";
                    ddlCollegeCity.SelectedValue = query.CollegeBranchCityId.ToString() != ""
                                                       ? query.CollegeBranchCityId.ToString()
                                                       : "0";
                    txtCollegeBranch.Text = query.CollegeBranchName != ""
                                                ? query.CollegeBranchName
                                                : "N/A";

                    txtCollegePopularName.Text = query.CollegePopulaorName != ""
                                                     ? query.CollegePopulaorName
                                                     : "N/A";
                    txtAddress.Text = query.CollegeBranchAddrs != ""
                                          ? query.CollegeBranchAddrs
                                          : "N/A";
                   fckCourseDecsription.FckValue = query.CollegeBranchDesc != ""
                                              ? query.CollegeBranchDesc
                                              : "N/A";
                    txtCollegeEst.Text = query.CollegeBranchEst != ""
                                             ? query.CollegeBranchEst
                                             : "N/A";
                    txtCollegeFax.Text = query.CollegeBranchFax != ""
                                             ? query.CollegeBranchFax
                                             : "N/A";
                    txtCollegeMobile.Text = query.CollegeBranchMobileNo != ""
                                                ? query.CollegeBranchMobileNo
                                                : "N/A";
                    txtCollegeWebsite.Text = query.CollegeBranchWebsite != ""
                                                 ? query.CollegeBranchWebsite
                                                 : "N/A";
                    txtEmailId.Text = query.CoillegeBranchEmailId != ""
                                          ? query.CoillegeBranchEmailId
                                          : "N/A";
                    txtPinCode.Text = query.CollegeBranchPinCode != ""
                                          ? query.CollegeBranchPinCode
                                          : "N/A";
                    chkCollegeStatus.Checked = query.CollegeBranchStatus && query.CollegeBranchStatus;
                    rbtManagement.SelectedValue = query.CollegeManagementTypeId.ToString(CultureInfo.InvariantCulture);
                    var img = query.CollegeBranchLogo != "" ? query.CollegeBranchLogo : "N/A";
                    imgCollege.ImageUrl = String.Format("{0}{1}", "/image.axd?College=", string.IsNullOrEmpty(query.CollegeBranchLogo) ? "NoImage.jpg" : query.CollegeBranchLogo);
                   imgCollege.AlternateText = query.CollegeBranchName;
                    if (img.Equals("N/A"))
                    {
                        FileUpload1.SetImgUrl = "";
                    }
                    else
                    {
                        FileUpload1.SetImgUrl = new Common().GetFilepath("UniversityImage") + img;
                        hdnImageFile.Value = img;
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
                const string addInfo = "Error while executing BindCollegeList in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void BtnSubmitCollegeBasicInfoClick(object sender, EventArgs e)
        {
            try
            {
                string ImageName = Common.NoImageSubstitute;

                var fileName = this.FileUpload1.UploadedImageName;
                if (fileName != null)
                {
                    hdnImageFile.Value = fileName;
                }
                var objCollegeBranchProperty = new CollegeBranchProperty
                                                   {
                                                       CollegeIdBranchId = Convert.ToInt16(Request.QueryString["CollegeBranchId"]),
                                                       InstituteTypeId =!string.IsNullOrEmpty(Convert.ToString(ddlInstituteType.SelectedValue))? Convert.ToInt16(ddlInstituteType.SelectedValue):0 ,
                                                      
                                                       CollegeGroupId =!string.IsNullOrEmpty(Convert.ToString(ddlCollegeGroup.SelectedValue))? Convert.ToInt16(ddlCollegeGroup.SelectedValue):0 ,
                                                       CollegeBranchName =
                                                           txtCollegeBranch.Text != "" ? txtCollegeBranch.Text : "N/A",
                                                       CollegePopulaorName =
                                                           txtCollegePopularName.Text != ""
                                                               ? txtCollegePopularName.Text
                                                               : "N/A",
                                                       CollegeManagementTypeId = Convert.ToInt16(rbtManagement.SelectedValue),
                                                       CollegeBranchEst =
                                                           txtCollegeEst.Text != "" ? txtCollegeEst.Text : "N/A",
                                                       CollegeBranchDesc =
                                                        fckCourseDecsription.FckValue != "" ?fckCourseDecsription.FckValue : "N/A",
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
                                                       CollegeBranchCountryId = Convert.ToInt16(ddlCountry.SelectedValue),
                                                       CollegeBranchStateId = Convert.ToInt16(ddlState.SelectedValue),
                                                       CollegeBranchCityId = Convert.ToInt16(ddlCollegeCity.SelectedValue),
                                                       CollegeBranchStatus = chkCollegeStatus.Checked,
                                                    CollegeBranchLogo= hdnImageFile.Value,
                                                      
                                                   };
                var errMsg = "";
                var collegeBranchId = 0;
                var result = CollegeProvider.Instance.UpdateCollegeBranchInfo(objCollegeBranchProperty, new SecurePage().LoggedInUserId, out errMsg,
                                                                              out collegeBranchId);
          
                if (result > 0)
                {
                    lblResult.Visible = true;
                    lblResult.Text = errMsg;
                    lblResult.CssClass = "success";
                    BindCollegeList(Request.QueryString["CollegeBranchId"]);
                   


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

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedIndex > 0)
            {
                BindState(Convert.ToInt16(ddlCountry.SelectedValue));
            }

        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedIndex > 0)
            {
                BindCity(Convert.ToInt16(ddlState.SelectedValue));
            }

        }

        protected void rptCourse_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    hdnCollegeCourse.Value = e.CommandArgument.ToString();
                    var courseData =
                        CollegeProvider.Instance.GetCollegeCourseListByBranchCourseId(Convert.ToInt16(e.CommandArgument));
                    if (courseData.Count > 0)
                    {
                        var query = courseData.Select(result => new
                                                                    {
                                                                        result.CourseId,
                                                                        result.UniversityId,
                                                                        result.HasHostel,
                                                                        result.CollegeBranchCourseDesc,
                                                                        result.CollegeBranchCourseEst,
                                                                        result.CollegeBranchCourseTitle,
                                                                        result.CollegeBranchCourseMetaDesc,
                                                                        result.CollegeBranchCourseMetaTag,
                                                                        result.CollegeBranchCourseUrl,
                                                                        result.CollegeBranchCourseStatus,
                                                                        result.CollegeBranchCourseSponserStatus,
                                                                        result.CollegeBranchCourseHelplineNo,
                                                                        result.IsBookSeatVisible


                                                                    }).First();
                       
                        ddlCourse.SelectedValue =
                            !string.IsNullOrEmpty(Convert.ToString(query.CourseId.ToString(CultureInfo.InvariantCulture)))
                                ? query.CourseId.ToString(CultureInfo.InvariantCulture)
                                : "0";
                        ddlUniversity.SelectedValue =
                            !string.IsNullOrEmpty(Convert.ToString(query.UniversityId.ToString()))
                                ? query.UniversityId.ToString()
                                : "0";
                        txtCourseTitle.Value = !string.IsNullOrEmpty(Convert.ToString(query.CollegeBranchCourseTitle))
                                                   ? query.CollegeBranchCourseTitle
                                                   : "N/A";
                        txtCourseMetaTag.Value =
                            !string.IsNullOrEmpty(Convert.ToString(query.CollegeBranchCourseMetaTag))
                                ? query.CollegeBranchCourseMetaTag
                                : "N/A";
                        txtCourseUrl.Value = !string.IsNullOrEmpty(Convert.ToString(query.CollegeBranchCourseUrl))
                                                 ? query.CollegeBranchCourseUrl
                                                 : "N/A";
                        txtCourseMetaDesc.Value =
                            !string.IsNullOrEmpty(Convert.ToString(query.CollegeBranchCourseMetaDesc))
                                ? query.CollegeBranchCourseMetaDesc
                                : "N/A";
                        txtCourseEst.Value = !string.IsNullOrEmpty(Convert.ToString(query.CollegeBranchCourseEst))
                                                 ? query.CollegeBranchCourseEst
                                                 : "N/A";
                        txtCourseDesc.Value = !string.IsNullOrEmpty(Convert.ToString(query.CollegeBranchCourseDesc))
                                                  ? query.CollegeBranchCourseDesc
                                                  : "N/A";
                        chkSponserStatus.Checked = query.CollegeBranchCourseSponserStatus;
                        chkCollegeCourse.Checked = query.CollegeBranchCourseStatus;
                        txtHelplineNoEdit.Value = !string.IsNullOrEmpty(Convert.ToString(query.CollegeBranchCourseHelplineNo)) ? query.CollegeBranchCourseHelplineNo : "N/A";
                        chkHasHostel.Checked = query.HasHostel;
                        chkIsBookSeatVisibleEdit.Checked = query.IsBookSeatVisible;
                    }
                    Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript",
                                                            "openpop('coursePop');", true);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing rptCourse_ItemCommand in UpdateCollegeDetails  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindCourse()
        {
            var data = CourseProvider.Instance.GetAllCourseList();
            if (data.Count > 0)
            {
                ddlCourse.DataSource = data;
                ddlCourse.DataTextField = "CourseName";
                ddlCourse.DataValueField = "CourseId";
                ddlCourse.DataBind();
                ddlCourse.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlCourseInsert.DataSource = data;
                ddlCourseInsert.DataTextField = "CourseName";
                ddlCourseInsert.DataValueField = "CourseId";
                ddlCourseInsert.DataBind();
                ddlCourseInsert.Items.Insert(0, new ListItem("Please Select", "0"));

                ddlCourses.DataSource = data;
                ddlCourses.DataTextField = "CourseName";
                ddlCourses.DataValueField = "CourseId";
                ddlCourses.DataBind();
                ddlCourses.Items.Insert(0, new ListItem("Please Select", "0"));

                ddlCoursesExam.DataSource = data;
                ddlCoursesExam.DataTextField = "CourseName";
                ddlCoursesExam.DataValueField = "CourseId";
                ddlCoursesExam.DataBind();
                ddlCoursesExam.Items.Insert(0, new ListItem("Please Select", "0"));


                
                ddlCoursesFacality.DataSource = data;
                ddlCoursesFacality.DataTextField = "CourseName";
                ddlCoursesFacality.DataValueField = "CourseId";
                ddlCoursesFacality.DataBind();
                ddlCoursesFacality.Items.Insert(0, new ListItem("Please Select", "0"));

                ddlCoursesRank.DataSource = data;
                ddlCoursesRank.DataTextField = "CourseName";
                ddlCoursesRank.DataValueField = "CourseId";
                ddlCoursesRank.DataBind();
                ddlCoursesRank.Items.Insert(0, new ListItem("Please Select", "0"));

                ddlCoursesHigh.DataSource = data;
                ddlCoursesHigh.DataTextField = "CourseName";
                ddlCoursesHigh.DataValueField = "CourseId";
                ddlCoursesHigh.DataBind();
                ddlCoursesHigh.Items.Insert(0, new ListItem("Please Select", "0"));

                ddlCoursesHostel.DataSource = data;
                ddlCoursesHostel.DataTextField = "CourseName";
                ddlCoursesHostel.DataValueField = "CourseId";
                ddlCoursesHostel.DataBind();
                ddlCoursesHostel.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            else
            {
                ddlCoursesHostel.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlCoursesHigh.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlCoursesRank.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlCoursesFacality.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlCourse.Items.Insert(0,new ListItem("Please Select","0"));
                ddlCoursesExam.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlCourses.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlCourseInsert.Items.Insert(0, new ListItem("Please Select", "0"));
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

                ddlUniversityInsert.DataSource = data;
                ddlUniversityInsert.DataTextField = "UniversityName";
                ddlUniversityInsert.DataValueField = "UniversityId";
                ddlUniversityInsert.DataBind();
                ddlUniversityInsert.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            else
            {
                ddlUniversityInsert.Items.Insert(0, new ListItem("Please Select", "0"));

                ddlUniversityInsert.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        protected void btnCourseInsert_Click(object sender, EventArgs e)
        {
            try
            {
                
                var objCollegeBranchCourseProperty = new CollegeBranchCourseProperty
                {
                   
                    CollegeBranchId =
                        Convert.ToInt32(Request.QueryString["CollegeBranchId"]),
                    CourseId = Convert.ToInt16(ddlCourseInsert.SelectedValue),
                    UniversityId =
                        Convert.ToInt16(ddlUniversityInsert.SelectedValue),
                    HasHostel = chkHasHostelInsert.Checked,
                    CollegeBranchCourseDesc =
                        txtDescInsert.Value != "" ? txtDescInsert.Value : "N/A",
                    CollegeBranchCourseEst =
                        txtEstInsert.Value != ""
                            ? txtEstInsert.Value
                            : "N/A",
                    CollegeBranchCourseTitle = txtTitleInsert.Value != ""
                                                   ? txtTitleInsert.Value
                                                   : "N/A",
                    CollegeBranchCourseMetaDesc =
                        txtMetaDescInsert.Value != ""
                            ? txtMetaDescInsert.Value
                            : "N/A",
                    CollegeBranchCourseMetaTag =
                        txtMetaTagInsert.Value != ""
                            ? txtMetaTagInsert.Value
                            : "N/A",
                    CollegeBranchCourseUrl =
                        txtUrlInsert.Value != "" ? txtUrlInsert.Value : "N/A",

                    CollegeBranchCourseStatus = chkCollegeCourseInsert.Checked,
                    CollegeBranchCourseSponserStatus = chkSponserStatusInsert.Checked,
                    CollegeBranchCourseHelplineNo = txtHelplineNoInsert.Value != "" ? txtHelplineNoInsert.Value : "N/A"  ,
                    IsBookSeatVisible=chkIsBookSeatVisibleInsert.Checked

                };
                var errMsg = "";
                var collegeCourseBranchId = 0;
                var result = CollegeProvider.Instance.InsertCollegeBranchCourseInfo(objCollegeBranchCourseProperty, new SecurePage().LoggedInUserId,
                                                                                    out errMsg,
                                                                                    out collegeCourseBranchId);
                lblResult.Visible = true;
                lblResult.Text = errMsg;
                if (result > 0)
                {
                    ClearInsertCourse();
                    lblResult.CssClass = "success";
                    BindCollegeCourseList(Request.QueryString["CollegeBranchId"]);

                }
                else
                {
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
                const string addInfo = "Error while executing CourseUpdate_Click in UpdateCollegeDetails  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        private void ClearInsertCourse()
        {
            ddlCourseInsert.ClearSelection();
            ddlUniversityInsert.ClearSelection();
            chkHasHostelInsert.Checked = false;
            txtDescInsert.Value = string.Empty;
            txtEstInsert.Value = string.Empty;
            txtTitleInsert.Value = string.Empty;
            txtMetaDescInsert.Value = string.Empty;
            txtMetaTagInsert.Value = string.Empty;
            txtUrlInsert.Value = string.Empty;
            txtHelplineNoInsert.Value = string.Empty;
            chkCollegeCourseInsert.Checked = false;
            chkSponserStatusInsert.Checked = false;
            chkIsBookSeatVisibleInsert.Checked = false;

        }
        protected void CourseUpdate_Click(object sender, EventArgs e)
        {
            try
            {
              
                var objCollegeBranchCourseProperty = new CollegeBranchCourseProperty
                                                         {
                                                             CollegeBranchCourseId =
                                                                 Convert.ToInt32(hdnCollegeCourse.Value),
                                                             CollegeBranchId =
                                                                 Convert.ToInt32(Request.QueryString["CollegeBranchId"]),
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
                                                             CollegeBranchCourseSponserStatus = chkSponserStatus.Checked,
                                                             CollegeBranchCourseHelplineNo = txtHelplineNoEdit.Value != "" ? txtHelplineNoEdit.Value : "",
                                                             IsBookSeatVisible=chkIsBookSeatVisibleEdit.Checked

                                                         };
                var errMsg = "";
                var collegeCourseBranchId = 0;
                var result = CollegeProvider.Instance.UpdateCollegeBranchCourseInfo(objCollegeBranchCourseProperty, new SecurePage().LoggedInUserId,
                                                                                    out errMsg,
                                                                                    out collegeCourseBranchId);
                if (result > 0)
                {
                    lblResult.Visible = true;
                    lblResult.Text = errMsg;
                    lblResult.CssClass = "success";
                    BindCollegeCourseList(Request.QueryString["CollegeBranchId"]);

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

        protected void rptCourseStream_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    hdnCourseStream.Value = e.CommandArgument.ToString();
                    var streamData =
                        CollegeProvider.Instance.GetCollegeCourseStreamListByCollegeCourseStreamId(
                            Convert.ToInt16(hdnCourseStream.Value));
                    if (streamData.Count > 0)
                    {
                        var query = streamData.Select(result => new
                                                                    {
                                                                        result.CollegeBranchCourseId,
                                                                        result.StreamId,
                                                                        result.StreamName,
                                                                        result.CourseId,
                                                                        result.CourseName,
                                                                        result.CollegeBranchCourseStreamSeat,
                                                                        result.CollegeBranchCourseStreamDuration,
                                                                        result.CollegeBranchCourseStreamFees,
                                                                        result.CollegeBranchCourseStreamModeId,
                                                                        result.CollegeBranchCourseStreamEligibity,
                                                                        result.CollegeBranchCourseStreamDesc,
                                                                        result.CollegeBranchCourseStreamManagementQuotaSeat,
                                                                        result.CollegeBranchCourseStreamLateralEntrySeat,
                                                                        result.CollegeBranchCourseStreamStatus,


                                                                    }).First();

                       
                        hdnCollegeBranchCourseId.Value = Convert.ToString(query.CollegeBranchCourseId);
                        ddlCourseStream.SelectedValue =
                            !string.IsNullOrEmpty(Convert.ToString(query.StreamId.ToString()))
                                ? query.StreamId.ToString()
                                : "0";
                        rbtCourseStream.SelectedValue =
                            !string.IsNullOrEmpty(Convert.ToString(query.CollegeBranchCourseStreamModeId.ToString()))
                                ? query.CollegeBranchCourseStreamModeId.ToString()
                                : "1";
                        txtStreamSeat.Text =
                            !string.IsNullOrEmpty(Convert.ToString(query.CollegeBranchCourseStreamSeat.ToString()))
                                ? query.CollegeBranchCourseStreamSeat.ToString()
                                : "N/A";
                        txtStreamDuration.Text =
                            !string.IsNullOrEmpty(Convert.ToString(query.CollegeBranchCourseStreamDuration.ToString()))
                                ? query.CollegeBranchCourseStreamDuration.ToString()
                                : "N/A";
                        txtStreamFees.Text =
                            !string.IsNullOrEmpty(Convert.ToString(query.CollegeBranchCourseStreamFees.ToString()))
                                ? query.CollegeBranchCourseStreamFees.ToString()
                                : "N/A";
                        txtStreamEligibilty.Text =
                            !string.IsNullOrEmpty(Convert.ToString(query.CollegeBranchCourseStreamEligibity.ToString()))
                                ? query.CollegeBranchCourseStreamEligibity.ToString()
                                : "N/A";
                        txtStreamQuotaSeat.Text =
                            !string.IsNullOrEmpty(
                                Convert.ToString(query.CollegeBranchCourseStreamManagementQuotaSeat.ToString()))
                                ? query.CollegeBranchCourseStreamManagementQuotaSeat.ToString()
                                : "N/A";
                        txtLateralSeat.Text =
                            !string.IsNullOrEmpty(
                                Convert.ToString(query.CollegeBranchCourseStreamLateralEntrySeat.ToString()))
                                ? query.CollegeBranchCourseStreamLateralEntrySeat.ToString()
                                : "N/A";
                        txtStreamDesc.Value =
                            !string.IsNullOrEmpty(Convert.ToString(query.CollegeBranchCourseStreamDesc.ToString()))
                                ? query.CollegeBranchCourseStreamDesc.ToString()
                                : "N/A";
                        chkStreamStatus.Checked = query.CollegeBranchCourseStreamStatus;
                        Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript",
                                                                "openpop('divStream');", true);
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
                const string addInfo = "Error while executing rptCourseStream_ItemCommand in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        private void BindStream()
        {
            var data = StreamProvider.Instance.GetAllStreamList();
            if (data != null && data.Count > 0)
            {
                ddlCourseStream.DataSource = data;
                ddlCourseStream.DataTextField = "CourseStreamName";
                ddlCourseStream.DataValueField = "StreamId";
                ddlCourseStream.DataBind();
                ddlCourseStream.Items.Insert(0, new ListItem("Please Select", "0"));

                ddlCourseStreamInsert.DataSource = data;
                ddlCourseStreamInsert.DataTextField = "CourseStreamName";
                ddlCourseStreamInsert.DataValueField = "StreamId";
                ddlCourseStreamInsert.DataBind();
                ddlCourseStreamInsert.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            else
            {
                ddlCourseStream.Items.Insert(0, new ListItem("Please Select", "0"));

                ddlCourseStreamInsert.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        private void BindStreamMode()
        {
            var dv = ClsSingelton.GetMode();

            if (dv.Table.Rows.Count > 0)
            {
                rbtCourseStream.DataSource = dv;
                rbtCourseStream.DataTextField = "AjMasterValues";
                rbtCourseStream.DataValueField = "AjMasterValueId";
                rbtCourseStream.DataBind();

                rbtCourseStreamInsert.DataSource = dv;
                rbtCourseStreamInsert.DataTextField = "AjMasterValues";
                rbtCourseStreamInsert.DataValueField = "AjMasterValueId";
                rbtCourseStreamInsert.DataBind();
            }

        }
        protected void BtnStreamUpdateClick(object sender, EventArgs e)
        {
            try
            {
                var objCollegeBranchCourseStreamProperty = new CollegeBranchCourseStreamProperty
                                                               {
                                                                   CollegeBranchCourseId =
                                                                       Convert.ToInt16(hdnCollegeBranchCourseId.Value),
                                                                   CollegeBranchCourseStreamId =
                                                                       Convert.ToInt32(hdnCourseStream.Value),
                                                                   StreamId =
                                                                       Convert.ToInt32(ddlCourseStream.SelectedValue),
                                                                   CollegeBranchCourseStreamModeId =
                                                                       Convert.ToInt32(rbtCourseStream.SelectedValue),
                                                                   CollegeBranchCourseStreamSeat =
                                                                       txtStreamSeat.Text != ""
                                                                           ? txtStreamSeat.Text
                                                                           : "N/A",
                                                                   CollegeBranchCourseStreamDuration =
                                                                       txtStreamDuration.Text != ""
                                                                           ? txtStreamDuration.Text
                                                                           : "N/A",
                                                                   CollegeBranchCourseStreamFees =
                                                                       txtStreamFees.Text != ""
                                                                           ? txtStreamFees.Text
                                                                           : "N/A",
                                                                   CollegeBranchCourseStreamEligibity =
                                                                       txtStreamEligibilty.Text != ""
                                                                           ? txtStreamEligibilty.Text
                                                                           : "N/A",
                                                                   CollegeBranchCourseStreamDesc =
                                                                       txtStreamDesc.Value != ""
                                                                           ? txtStreamDesc.Value
                                                                           : "N/A",
                                                                   CollegeBranchCourseStreamManagementQuotaSeat =
                                                                       txtStreamQuotaSeat.Text != ""
                                                                           ? txtStreamQuotaSeat.Text
                                                                           : "N/A",
                                                                   CollegeBranchCourseStreamLateralEntrySeat =
                                                                       txtLateralSeat.Text != ""
                                                                           ? txtLateralSeat.Text
                                                                           : "N/A",
                                                                   CollegeBranchCourseStreamStatus =
                                                                       chkStreamStatus.Checked
                                                               };
                var errMsg = "";

                var result =
                    CollegeProvider.Instance.UpdateCollegeBranchCourseStreamInfo(objCollegeBranchCourseStreamProperty, 1,
                                                                                 out errMsg);

                if (result > 0)
                {
                    lblResult.Visible = true;
                    lblResult.Text = "Updated Successfully";
                    lblResult.CssClass = "success";
                    BindCollegeCourseStreamList(Request.QueryString["CollegeBranchId"]);

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BtnStreamUpdateClick in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        protected void BtnStreamInsertClick(object sender, EventArgs e)
        {
            try
            {
              

                var objCollegeBranchCourseStreamProperty = new CollegeBranchCourseStreamProperty
                {
                    CollegeBranchId = Convert.ToInt32(Request.QueryString["CollegeBranchId"]),
                    CourseId = Convert.ToInt32(ddlCourses.SelectedValue),
                    StreamId =
                        Convert.ToInt32(ddlCourseStreamInsert.SelectedValue),
                    CollegeBranchCourseStreamModeId =
                        Convert.ToInt32(rbtCourseStreamInsert.SelectedValue),
                    CollegeBranchCourseStreamSeat =
                        txtStreamSeatInsert.Text != ""
                            ? txtStreamSeatInsert.Text
                            : "N/A",
                    CollegeBranchCourseStreamDuration =
                        txtStreamDurationInsert.Text != ""
                            ? txtStreamDurationInsert.Text
                            : "N/A",
                    CollegeBranchCourseStreamFees =
                        txtStreamFeesInsert.Text != ""
                            ? txtStreamFeesInsert.Text
                            : "N/A",
                    CollegeBranchCourseStreamEligibity =
                        txtStreamEligibiltyINsert.Text != ""
                            ? txtStreamEligibiltyINsert.Text
                            : "N/A",
                    CollegeBranchCourseStreamDesc =
                        txtStreamInsertDesc.Value != ""
                            ? txtStreamInsertDesc.Value
                            : "N/A",
                    CollegeBranchCourseStreamManagementQuotaSeat =
                        txtStreamQuotaSeatInsert.Text != ""
                            ? txtStreamQuotaSeatInsert.Text
                            : "N/A",
                    CollegeBranchCourseStreamLateralEntrySeat =
                        txtLateralSeatInsert.Text != ""
                            ? txtLateralSeatInsert.Text
                            : "N/A",
                    CollegeBranchCourseStreamStatus =
                        chkStreamStatusInsert.Checked
                };
                var errMsg = "";

                var result =
                    CollegeProvider.Instance.InsertCollegeBranchCourseStreamInfoByCollegeId(objCollegeBranchCourseStreamProperty, 1,
                                                                                 out errMsg);

                if (result > 0)
                {
                    lblResult.Visible = true;
                    lblResult.Text = errMsg;
                    lblResult.CssClass = "success";
                    BindCollegeCourseStreamList(Request.QueryString["CollegeBranchId"]);

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BtnStreamUpdateClick in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        protected void DdlCourseStreamSelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlCourses.SelectedIndex > 0)
            {
                BindStreamById(ddlCourses.SelectedValue);
            }
            else
            {
                ddlCourseStreamInsert.Items.Clear();
                ddlCourseStreamInsert.Enabled = false;
                ddlCourseStreamInsert.Items.Insert(0, new ListItem("Select", "0"));

            }

        }
        private void BindStreamById(string courseId)
        {
            var data = StreamProvider.Instance.GetStreamListByCourse(Convert.ToInt32(courseId)).OrderBy(x=>x.CourseStreamName).ToList();
            if (data.Count > 0)
            {
                ddlCourseStreamInsert.ClearSelection();
                ddlCourseStreamInsert.Enabled = true;
                ddlCourseStreamInsert.DataSource = data;
                ddlCourseStreamInsert.DataTextField = "CourseStreamName";
                ddlCourseStreamInsert.DataValueField = "StreamId";
                ddlCourseStreamInsert.DataBind();
                ddlCourseStreamInsert.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCourseStreamInsert.ClearSelection();
                ddlCourseStreamInsert.Enabled = false;
                ddlCourseStreamInsert.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        protected void RptExamItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    hdnExamId.Value = e.CommandArgument.ToString();
                    var examData =
                        CollegeProvider.Instance.GetCollegeCourseStreamListByCollegeExamId(
                            Convert.ToInt16(hdnExamId.Value));
                    if (examData.Count > 0)
                    {
                        var query = examData.Select(result => new
                                                                  {
                                                                      CollegeBranchCourseId =
                                                                  result.CollegeBranchCourseId,
                                                                      ExamId = result.ExamId,
                                                                      CollegeCourseExamStatus =
                                                                  result.CollegeCourseExamStatus,
                                                                      CourseId = result.CourseId,
                                                                      CollegeBranchId = result.CollegeBranchId,
                                                                      CollegeExamEligibity = result.CollegeExamEligibilty,
                                                                  }).First();
                     
                        hdnCourseExamId.Value = Convert.ToString(query.CollegeBranchCourseId);
                        ddlExam.SelectedValue = query.ExamId.ToString() != "" ? query.ExamId.ToString() : "0";
                        chkCourseExam.Checked = query.CollegeCourseExamStatus;
                        txtCollegeExamEligibilty.Text = query.CollegeExamEligibity;
                        Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript",
                                                                "openpop('divExam');", true);


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
                const string addInfo = "Error while executing RptExamItemCommand in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
          
        private void BindExam()
        {
            var data = ExamProvider.Instance.GetAllExamList();
            if (data.Count > 0)
            {

                ddlExam.DataSource = data;
                ddlExam.DataTextField = "ExamName";
                ddlExam.DataValueField = "ExamId";
                ddlExam.DataBind();
                ddlExam.Items.Insert(0, new ListItem("Please Select", "0"));


                ddlExamInsert.DataSource = data;
                ddlExamInsert.DataTextField = "ExamName";
                ddlExamInsert.DataValueField = "ExamId";
                ddlExamInsert.DataBind();
                ddlExamInsert.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            else
            { ddlExamInsert.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlExam.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        protected void btnExam_Click(object sender, EventArgs e)
        {
            try
            {
                var objCollegeBranchCourseExamProperty = new CollegeBranchCourseExamProperty
                                                             {
                                                                 CollegeBranchCourseId =
                                                                     Convert.ToInt16(hdnCourseExamId.Value),
                                                                 CollegeBranchCourseExamId =
                                                                     Convert.ToInt16(hdnExamId.Value),
                                                                 ExamId =
                                                                     Convert.ToInt32(ddlExam.SelectedValue),
                                                                 CollegeCourseExamStatus = chkCourseExam.Checked,
                                                                 CollegeExamEligibilty=txtCollegeExamEligibity.Text
                                                             };
                var errMsg = "";

                var result =
                    CollegeProvider.Instance.UpdateCollegeBranchCourseExamInfo(objCollegeBranchCourseExamProperty, new SecurePage().LoggedInUserId,
                                                                               out errMsg);

                if (result > 0)
                {
                    lblResult.Visible = true;
                    lblResult.Text = errMsg;
                    lblResult.CssClass = "success";
                    BindCollegeCourseExamList(Request.QueryString["CollegeBranchId"]);

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnExam_Click in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        protected void BtnExamInsertClick(object sender, EventArgs e)
        {
            try
            {
                var objCollegeBranchCourseExamProperty = new CollegeBranchCourseExamProperty
                {
                    CollegeBranchId =
                        Convert.ToInt16(Request.QueryString["CollegeBranchId"]),
                    CourseId = 
                   Convert.ToInt16(ddlCoursesExam.SelectedValue),
             
                    ExamId =
                        Convert.ToInt32(ddlExamInsert.SelectedValue),
                    CollegeCourseExamStatus = chkCourseExam.Checked,
                    CollegeExamEligibilty = txtCollegeExamEligibilty.Text
                };
                var errMsg = "";

                var result =
                    CollegeProvider.Instance.InsertCollegeBranchCourseExamInfoByCollegeId(objCollegeBranchCourseExamProperty, 1,
                                                                               out errMsg);

                if (result > 0)
                {
                    lblResult.Visible = true;
                    lblResult.Text = errMsg;
                    lblResult.CssClass = "success";
                    BindCollegeCourseExamList(Request.QueryString["CollegeBranchId"]);

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnExam_Click in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        private void BindExamById(int courseId)
        {
            try
            {
                var data = courseId == 0
                               ? ExamProvider.Instance.GetAllExamList()
                               : ExamProvider.Instance.GetExamListByCourseId(courseId).OrderBy(p => p.ExamName).ToList();
                if (data.Count > 0)
                {
                    ddlExamInsert.ClearSelection();
                    ddlExamInsert.Enabled = true;
                    ddlExamInsert.DataSource = data;
                    ddlExamInsert.DataTextField = "ExamName";
                    ddlExamInsert.DataValueField = "ExamId";
                    ddlExamInsert.DataBind();
                    ddlExamInsert.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlExamInsert.ClearSelection();
                    ddlExamInsert.Enabled = false;
                    ddlExamInsert.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex;
                }
                const string addInfo = "Error while executing BindExam in CollegeProfile.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void DdlCourseExamSelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlCoursesExam.SelectedIndex > 0)
            {
                BindExamById(Convert.ToInt32(ddlCoursesExam.SelectedValue));
            }
            else
            {
                ddlExamInsert.ClearSelection();
                ddlExamInsert.Items.Clear();
                ddlExamInsert.Enabled = false;
                ddlExamInsert.Items.Insert(0, new ListItem("Select", "0"));

            }

        }
        protected void RptCourseFacalityItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    hdnFacalityId.Value = e.CommandArgument.ToString();
                    var facalityData =
                        CollegeProvider.Instance.GetCollegeCourseFacalityByFacalityId(
                            Convert.ToInt16(hdnFacalityId.Value));
                    if (facalityData.Count > 0)
                    {
                        var query = facalityData.Select(result => new
                                                                      {
                                                                          CollegeBranchCourseFacilitieName =
                                                                      result.CollegeBranchCourseFacilitieName,
                                                                          CollegeBranchCourseId =
                                                                      result.CollegeBranchCourseId,
                                                                          CollegeBranchCourseFacilitieDesc =
                                                                      result.CollegeBranchCourseFacilitieDesc,
                                                                          CollegeBranchCourseFacilitieStatus =
                                                                      result.CollegeBranchCourseFacilitieStatus,


                                                                      }).First();
                        BindExam();
                        hdnCourseFacalityId.Value = Convert.ToString(query.CollegeBranchCourseId);
                        txtCourseFacality.Text = query.CollegeBranchCourseFacilitieName.ToString() != ""
                                                     ? query.CollegeBranchCourseFacilitieName.ToString()
                                                     : "N/A";
                        txtFacalityDesc.Text = query.CollegeBranchCourseFacilitieDesc.ToString() != ""
                                                   ? query.CollegeBranchCourseFacilitieDesc.ToString()
                                                   : "N/A";
                        chkFacalityStatus.Checked = query.CollegeBranchCourseFacilitieStatus;
                        Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript",
                                                                "openpop('divFacality');", true);


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
                const string addInfo = "Error while executing RptCourseFacalityItemCommand in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }
        protected void btnCourseFacality_Click(object sender, EventArgs e)
        {
            try
            {
                var objCollegeBranchCourseFacilitiesProperty = new CollegeBranchCourseFacilitiesProperty
                                                                   {
                                                                       CollegeBranchCourseId =
                                                                           Convert.ToInt16(hdnCourseFacalityId.Value),
                                                                       CollegeBranchCourseFacilitieId =
                                                                           Convert.ToInt16(hdnFacalityId.Value),
                                                                       CollegeBranchCourseFacilitieName =
                                                                           txtCourseFacality.Text != ""
                                                                               ? txtCourseFacality.Text
                                                                               : "N/A",
                                                                       CollegeBranchCourseFacilitieDesc =
                                                                           txtFacalityDesc.Text != ""
                                                                               ? txtFacalityDesc.Text
                                                                               : "N/A",
                                                                       CollegeBranchCourseFacilitieStatus =
                                                                           chkFacalityStatus.Checked,
                                                                   };
                var errMsg = "";

                var result =
                    CollegeProvider.Instance.UpdateCollegeBranchCourseFacilities(
                        objCollegeBranchCourseFacilitiesProperty, new SecurePage().LoggedInUserId,
                        out errMsg);

                if (result > 0)
                {
                    lblResult.Visible = true;
                    lblResult.Text = errMsg;
                    lblResult.CssClass = "success";
                    BindCollegeCourseFacalityList(Request.QueryString["CollegeBranchId"]);

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnCourseFacality_Click in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }
          protected void BtnCourseFacalityInsertClick(object sender, EventArgs e)
        {
            try
            {
                var objCollegeBranchCourseFacilitiesProperty = new CollegeBranchCourseFacilitiesProperty
                                                                   {
                                                                       CollegeBranchId = 
                                                                           Convert.ToInt16(Request.QueryString["CollegeBranchId"]),
                                                                           CourseId = Convert.ToInt16(ddlCoursesFacality.SelectedValue),
                                                                  CollegeBranchCourseFacilitieName =
                                                                           txtCourseFacalityInsert.Text != ""
                                                                               ? txtCourseFacalityInsert.Text
                                                                               : "N/A",
                                                                       CollegeBranchCourseFacilitieDesc =
                                                                           txtCourseFacalityInsert.Text != ""
                                                                               ? txtCourseFacalityInsert.Text
                                                                               : "N/A",
                                                                       CollegeBranchCourseFacilitieStatus =
                                                                           chkFacalityStatusInsert.Checked,
                                                                   };
                var errMsg = "";

                var result =
                    CollegeProvider.Instance.InsertCollegeBranchCourseFacilitiesByCollege(
                        objCollegeBranchCourseFacilitiesProperty, new SecurePage().LoggedInUserId,
                        out errMsg);

                if (result > 0)
                {
                    lblResult.Visible = true;
                    lblResult.Text = errMsg;
                    lblResult.CssClass = "success";
                    BindCollegeCourseFacalityList(Request.QueryString["CollegeBranchId"]);

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BtnCourseFacalityInsertClick in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }

        protected void RptRankSourceItemCommand(object source, RepeaterCommandEventArgs e)
        {
       
            try
            {
                if (e.CommandName == "Edit")
                {
                    hdnRankId.Value = e.CommandArgument.ToString();
                    var data = CollegeProvider.Instance.GetCollegeCourseRankByRankId(Convert.ToInt16(hdnRankId.Value));

                    if (data.Count > 0)
                    {
                        var query = data.Select(result => new
                        {
                            CollegeRankSourceId = result.CollegeRankSourceId,
                            CollegeBranchCourseId = result.CollegeBranchCourseId,
                            CollegeOverAllRank = result.CollegeOverAllRank,
                            CollegeRankYear = result.CollegeRankYear,
                            CollegeRankStatus = result.CollegeRankStatus,


                        }).First();
                        
                        hdnCourseRankId.Value = Convert.ToString(query.CollegeBranchCourseId);
                        ddlRankSource.SelectedValue = query.CollegeRankSourceId.ToString() != ""
                                                     ? query.CollegeRankSourceId.ToString()
                                                     : "0";
                        txtRankOverall.Text = query.CollegeOverAllRank.ToString() != ""
                                                   ? query.CollegeOverAllRank.ToString()
                                                   : "N/A";
                        txtRanKYear.Text = query.CollegeRankYear.ToString() != ""
                                                 ? query.CollegeRankYear.ToString()
                                                 : "N/A";
                        chkRankStatus.Checked = query.CollegeRankStatus;
                        Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript",
                                                                "openpop('divRank');", true);


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
                const string addInfo = "Error while executing RptCourseFacalityItemCommand in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }
        private void BindRank()
        {
            var data = CollegeProvider.Instance.GetAllCollegeRankSourceList();
            if (data.Count > 0)
            {

                ddlRankSource.DataSource = data;
                ddlRankSource.DataTextField = "CollegeRankSourceName";
                ddlRankSource.DataValueField = "CollegeRankSourceId";
                ddlRankSource.DataBind();
                ddlRankSource.Items.Insert(0, new ListItem("Please Select", "0"));

                 ddlRankSourceInsert.DataSource = data;
                ddlRankSourceInsert.DataTextField = "CollegeRankSourceName";
                ddlRankSourceInsert.DataValueField = "CollegeRankSourceId";
                ddlRankSourceInsert.DataBind();
                ddlRankSourceInsert.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            else
            {   ddlRankSourceInsert.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlRankSource.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        protected void btnRankOverAll_Click(object sender, EventArgs e)
        {
            try
            {
                var objCollegeBranchRankProperty = new CollegeBranchRankProperty
                                                       {
                                                           CollegeBranchCourseId =
                                                               Convert.ToInt16(hdnCourseRankId.Value),

                                                           CollegeRankSourceId =
                                                               Convert.ToInt16(ddlRankSource.SelectedValue),
                                                           CollegeRankId = Convert.ToInt16(hdnRankId.Value),
                                                           CollegeOverAllRank =
                                                               txtRankOverall.Text != "" ? txtRankOverall.Text : "N/A",
                                                           CollegeRankYear =
                                                               txtRanKYear.Text != ""
                                                                   ? Convert.ToInt16(txtRanKYear.Text)
                                                                   : 0,
                                                           CollegeRankStatus = chkRankStatus.Checked,
                                                       };
                var errMsg = "";

                var result =
                    CollegeProvider.Instance.UpdateCollegeBranchRank(objCollegeBranchRankProperty, new SecurePage().LoggedInUserId,
                                                                     out errMsg);

                if (result > 0)
                {
                    lblResult.Visible = true;
                    lblResult.Text = errMsg;
                    lblResult.CssClass = "success";
                    BindCollegeCourseRankList(Request.QueryString["CollegeBranchId"]);

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnRankOverAll_Click in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }
        protected void btnRankOverAllInsert_Click(object sender, EventArgs e)
        {
            try
            {
                var objCollegeBranchRankProperty = new CollegeBranchRankProperty
                {
                    CollegeBranchId =
                        Convert.ToInt32(Request.QueryString["CollegeBranchId"]),

                    CourseId =
                        Convert.ToInt32(ddlCoursesRank.SelectedValue),
                    CollegeRankSourceId =
                Convert.ToInt32(ddlRankSourceInsert.SelectedValue),
                CollegeOverAllRank =
                        txtRankOverallInsert.Text != "" ? txtRankOverallInsert.Text : "N/A",
                    CollegeRankYear =
                        txtRanKYearInsert.Text != ""
                            ? Convert.ToInt16(txtRanKYearInsert.Text)
                            : 0,
                    CollegeRankStatus = chkRankStatusInsert.Checked,
                };
                var errMsg = "";

                var result =
                    CollegeProvider.Instance.InsertCollegeBranchRankByCollegeId(objCollegeBranchRankProperty, new SecurePage().LoggedInUserId,
                                                                     out errMsg);

                if (result > 0)
                {
                    lblResult.Visible = true;
                    lblResult.Text = errMsg;
                    lblResult.CssClass = "success";
                    BindCollegeCourseRankList(Request.QueryString["CollegeBranchId"]);

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnRankOverAll_Click in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }

        protected void rptHighLights_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
             try
            {
                if (e.CommandName == "Edit")
                {
                    hdnHighLights.Value = e.CommandArgument.ToString();
                    var data = CollegeProvider.Instance.GetCollegeCourseHighLightsByHighLightsId(Convert.ToInt16(hdnHighLights.Value));

                    if (data.Count > 0)
                    {
                        var query = data.Select(result => new
                        {
                            result.CollegeBranchCourseHighlight, result.CollegeBranchCourseHighlightStatus,
                           result.CollegeBranchCourseId,

                        }).First();
                        BindRank();
                        hdnCourseHighLightsId.Value = Convert.ToString(query.CollegeBranchCourseId);
                        txtCourseHighLights.Text = query.CollegeBranchCourseHighlight.ToString() != ""
                                                       ? query.CollegeBranchCourseHighlight.ToString()
                                                       : "N/A";
                      chkHighLights.Checked = query.CollegeBranchCourseHighlightStatus;
                        Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript",
                                                                "openpop('divHighLights');", true);


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
                 const string addInfo = "Error while executing rptHighLights_ItemCommand in UpdateCollegeDetails.aspx  :: -> ";
                 var objPub = new ClsExceptionPublisher();
                 objPub.Publish(err, addInfo);

             }
}
        protected void btnHighLights_Click(object sender, EventArgs e)
        {
            try
            {
                var objCollegeBranchCourseHighlightsProperty = new CollegeBranchCourseHighlightsProperty
                                                                   {
                                                                       CollegeBranchCourseId =
                                                                           Convert.ToInt16(hdnCourseHighLightsId.Value),

                                                                       CollegeBranchCourseHighlightId =
                                                                           Convert.ToInt16(hdnHighLights.Value),
                                                                       CollegeBranchCourseHighlight =
                                                                           txtCourseHighLights.Text != ""
                                                                               ? txtCourseHighLights.Text
                                                                               : "N/A",
                                                                       CollegeBranchCourseHighlightStatus =
                                                                           chkHighLights.Checked,
                                                                   };
                var errMsg = "";

                var result =
                    CollegeProvider.Instance.UpdateCollegeBranchCourseHighlights(
                        objCollegeBranchCourseHighlightsProperty, 1,
                        out errMsg);

                if (result > 0)
                {
                    lblResult.Visible = true;
                    lblResult.Text = errMsg;
                    lblResult.CssClass = "success";
                    BindCollegeCourseHighLights(Request.QueryString["CollegeBranchId"]);

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnHighLights_Click in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }
        protected void btnHighLightsInsertClick(object sender, EventArgs e)
        {
            try
            {
                var objCollegeBranchCourseHighlightsProperty = new CollegeBranchCourseHighlightsProperty
                {
                    CollegeBranchId =
                        Convert.ToInt16(Request.QueryString["CollegeBranchId"]),
                    CourseId = Convert.ToInt16(ddlCoursesHigh.SelectedValue), 
                    CollegeBranchCourseHighlight =
                        txtCourseHighLightsInsert.Text != ""
                            ? txtCourseHighLightsInsert.Text
                            : "N/A",
                    CollegeBranchCourseHighlightStatus =
                        chkHighLightsInsert.Checked,
                };
                var errMsg = "";

                var result =
                    CollegeProvider.Instance.InsertCollegeBranchCourseHighlightsByCollege(
                        objCollegeBranchCourseHighlightsProperty, new SecurePage().LoggedInUserId,
                        out errMsg);

                if (result > 0)
                {
                    lblResult.Visible = true;
                    lblResult.Text = errMsg;
                    lblResult.CssClass = "success";
                    BindCollegeCourseHighLights(Request.QueryString["CollegeBranchId"]);

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnHighLights_Click in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }
        protected void rptHostel_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    hdnHostelId.Value = e.CommandArgument.ToString();
                    var data = CollegeProvider.Instance.GetCollegeCourseHostelByHostelId(Convert.ToInt16(hdnHostelId.Value));

                    if (data.Count > 0)
                    {
                        var query = data.Select(result => new
                        {
                            CollegeBranchCourseHostelLocation = result.CollegeBranchCourseHostelLocation,
                            CollegeBranchCourseHostelCharge = result.CollegeBranchCourseHostelCharge,
                            CollegeBranchCourseId = result.CollegeBranchCourseId,
                            HostelCategoryId = result.HostelCategoryId,
                            CollegeBranchCourseHostelStatus = result.CollegeBranchCourseHostelStatus,
                            IsCollegeBranchCourseHostelHasAC = result.IsCollegeBranchCourseHostelHasAC,
                            IsCollegeBranchCourseHostelHasInternet = result.IsCollegeBranchCourseHostelHasInternet,
                            IsCollegeBranchCourseHostelHasLoundry = result.IsCollegeBranchCourseHostelHasLoundry,
                            IsCollegeBranchCourseHostelHasPowerBackup = result.IsCollegeBranchCourseHostelHasPowerBackup,

                        }).First();
                       
                        hdnHostelCourseId.Value = Convert.ToString(query.CollegeBranchCourseId);
                        ddlHostelMaster.SelectedValue = query.HostelCategoryId.ToString() != ""
                                                            ? query.HostelCategoryId.ToString()
                                                            : "0";
                        txtHostelLocation.Text = query.CollegeBranchCourseHostelLocation.ToString() != ""
                                                       ? query.CollegeBranchCourseHostelLocation.ToString()
                                                       : "N/A";
                        txtHostelCharge.Text = query.CollegeBranchCourseHostelCharge.ToString() != ""
                                                     ? query.CollegeBranchCourseHostelCharge.ToString()
                                                     : "N/A";
                        rbtAc.SelectedValue = query.IsCollegeBranchCourseHostelHasAC.ToString() != ""
                                                       ? query.IsCollegeBranchCourseHostelHasAC.ToString()
                                                       :"0";
                        rbtLoundary.SelectedValue = query.IsCollegeBranchCourseHostelHasLoundry.ToString() != ""
                                                     ? query.IsCollegeBranchCourseHostelHasLoundry.ToString()
                                                     : "0";
                        rbtPower.SelectedValue = query.IsCollegeBranchCourseHostelHasPowerBackup.ToString() != ""
                                                     ? query.IsCollegeBranchCourseHostelHasPowerBackup.ToString()
                                                     : "0";
                        rbtInternet.SelectedValue = query.IsCollegeBranchCourseHostelHasInternet.ToString() != ""
                                                     ? query.IsCollegeBranchCourseHostelHasInternet.ToString()
                                                     : "0";
                        chkHostel.Checked = query.CollegeBranchCourseHostelStatus;
                        Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript",
                                                                "openpop('divHostel');", true);


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
                const string addInfo = "Error while executing rptHostel_ItemCommand in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }
        private void BindHostel()
        {
            var data = CollegeProvider.Instance.GetAllHostelCategory();
            if (data.Count > 0)
            {

                ddlHostelMaster.DataSource = data;
                ddlHostelMaster.DataTextField = "HostelCategoryType";
                ddlHostelMaster.DataValueField = "HostelCategoryId";
                ddlHostelMaster.DataBind();
                ddlHostelMaster.Items.Insert(0, new ListItem("Please Select", "0"));

                ddlHostelMasterInsert.DataSource = data;
                ddlHostelMasterInsert.DataTextField = "HostelCategoryType";
                ddlHostelMasterInsert.DataValueField = "HostelCategoryId";
                ddlHostelMasterInsert.DataBind();
                ddlHostelMasterInsert.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            else
            {
                ddlHostelMasterInsert.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlHostelMaster.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        protected void BtnHostelClick(object sender, EventArgs e)
        {
            try
            {
                var objCollegeBranchCourseHostelProperty = new CollegeBranchCourseHostelProperty
                                                               {
                                                                   CollegeBranchCourseId =
                                                                       Convert.ToInt16(hdnHostelCourseId.Value),

                                                                   CollegeBranchCourseHostelCharge =
                                                                       txtHostelCharge.Text.Trim() != ""
                                                                           ? txtHostelCharge.Text.Trim()
                                                                           : "N/A",
                                                                   CollegeBranchCourseHostelLocation =
                                                                       txtHostelLocation.Text.Trim() != ""
                                                                           ? txtHostelLocation.Text.Trim()
                                                                           : "N/A",
                                                                   HostelCategoryId =
                                                                       Convert.ToInt16(ddlHostelMaster.SelectedValue),
                                                                   IsCollegeBranchCourseHostelHasInternet =
                                                                       rbtInternet.SelectedValue == "0" ? true : false,
                                                                   IsCollegeBranchCourseHostelHasAC =
                                                                       rbtAc.SelectedValue == "0" ? true : false,
                                                                   IsCollegeBranchCourseHostelHasLoundry =
                                                                       rbtLoundary.SelectedValue == "0" ? true : false,
                                                                   IsCollegeBranchCourseHostelHasPowerBackup =
                                                                       rbtPower.SelectedValue == "0" ? true : false,
                                                                   CollegeBranchCourseHostelStatus = chkHostel.Checked,
                                                                   CollegeBranchCourseHostelId =
                                                                       Convert.ToInt16(hdnHostelId.Value)

                                                               };
                var errMsg = "";

                var result =
                    CollegeProvider.Instance.UpdateCollegeBranchHostelInfo(objCollegeBranchCourseHostelProperty, new SecurePage().LoggedInUserId,
                                                                           out errMsg);

                if (result > 0)
                {
                    lblResult.Visible = true;
                    lblResult.Text = errMsg;
                    lblResult.CssClass = "success";
                    BindCollegeCourseHostel(Request.QueryString["CollegeBranchId"]);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BtnHostelClick in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }

        protected void btnHostelInsertClick(object sender, EventArgs e)
        {
            try
            {
                var objCollegeBranchCourseHostelProperty = new CollegeBranchCourseHostelProperty
                {
                    CollegeBranchId =
                        Convert.ToInt16(Request.QueryString["CollegeBranchId"]),

                    CollegeBranchCourseHostelCharge =
                        txtHostelChargeInsert.Text.Trim() != ""
                            ? txtHostelChargeInsert.Text.Trim()
                            : "N/A",
                    CollegeBranchCourseHostelLocation =
                        txtHostelLocationInsert.Text.Trim() != ""
                            ? txtHostelLocationInsert.Text.Trim()
                            : "N/A",
                    HostelCategoryId =
                        Convert.ToInt16(ddlHostelMasterInsert.SelectedValue),
                    IsCollegeBranchCourseHostelHasInternet =
                        rbtInternetInsert.SelectedValue == "0",
                    IsCollegeBranchCourseHostelHasAC =
                        rbtAcInsert.SelectedValue == "0",
                    IsCollegeBranchCourseHostelHasLoundry =
                        rbtLoundaryInsert.SelectedValue == "0",
                    IsCollegeBranchCourseHostelHasPowerBackup =
                        rbtPowerInsert.SelectedValue == "0",
                    CollegeBranchCourseHostelStatus = chkHostelInsert.Checked,
                    CourseId =
                        Convert.ToInt16(ddlCoursesHostel.SelectedValue)

                };
                var errMsg = "";

                var result =
                    CollegeProvider.Instance.InsertCollegeBranchHostelInfoInsert(objCollegeBranchCourseHostelProperty, new SecurePage().LoggedInUserId,
                                                                           out errMsg);

                if (result > 0)
                {
                    lblResult.Visible = true;
                    lblResult.Text = errMsg;
                    lblResult.CssClass = "success";
                    BindCollegeCourseHostel(Request.QueryString["CollegeBranchId"]);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BtnHostelClick in UpdateCollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }

        
    }
}
