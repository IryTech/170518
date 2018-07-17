using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.Components.Web.Controls;
using System.Web.UI.HtmlControls;
namespace IryTech.AdmissionJankari20.Web.College
{
    public partial class SponserCollegeDetails :PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Request.QueryString["CollegeBranchCourseId"] != null)
            {
                ucCollegeChart.CollegeBranchCourseId = Convert.ToInt32(Request.QueryString["CollegeBranchCourseId"]);
                UcComment.CommentType = Convert.ToString(CommentType.Col);
                UcComment.CommentTypeId = Request.QueryString["CollegeBranchCourseId"];
                GetUserComment();
            }

          ucCollegeBasicCourse.PageButtonCount = ApplicationSettings.Instance.MostViewdCollegePageCount;
           ucCollegeBasicCourse.PageSizeCourse = ApplicationSettings.Instance.MostViewdCollegePageSize;
           if (IsPostBack) return;
            lnkBookSeat.Visible = ApplicationSettings.Instance.IsVissbibleBookYourSeat;
            if (Request.QueryString["CollegeBranchCourseId"] == null) return;
            BindCollegeBasicDetails(Request.QueryString["CollegeBranchCourseId"]);
            BindCollegeCourseStreamDetails(Request.QueryString["CollegeBranchCourseId"]);
            BindCollegeCourseExamDetails(Request.QueryString["CollegeBranchCourseId"]);
            BindCOllegeHostelDetails(Request.QueryString["CollegeBranchCourseId"]);
            BindCollegeFacalityDetails(Request.QueryString["CollegeBranchCourseId"]);
            BindCollegePresidentSpeech(Request.QueryString["CollegeBranchCourseId"]);
            BindCollegeHighLights(Request.QueryString["CollegeBranchCourseId"]);
            BindCollegeGallery(Request.QueryString["CollegeBranchCourseId"]);
            ucCollegeQuery.CollegeBranchCourseId = Convert.ToInt32(Request.QueryString["CollegeBranchCourseId"]);
            ucRelatedCollege.CollegeBranchCourseId = Convert.ToInt32(Request.QueryString["CollegeBranchCourseId"]);
          var objCommon = new Common();
            var i = objCommon.InsertCollegePageClick(Convert.ToInt32(Request.QueryString["CollegeBranchCourseId"]));
          
        }
        private void GetUserComment()
        {
            var dataset = new Common().GetUserComment(UcComment.CommentType, UcComment.CommentTypeId);

            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    var rowResults = from result in dataset.Tables[0].AsEnumerable()
                                     where result.Field<bool>("AjCommentStatus") == true
                                     select new
                                     {
                                         AjUserFullName = result.Field<string>("AjUserFullName"),
                                         AjUserImage = result.Field<string>("AjUserImage"),
                                         Comment = result.Field<string>("Comment"),
                                         CreatedOn = result.Field<DateTime>("CreatedOn"),

                                     };

                    if (rowResults.Count() > 0)
                    {
                        var control = UcComment.FindControl("rptComment");
                        var divUserComment = UcComment.FindControl("divUserComment") as HtmlGenericControl;
                        var lblCount = UcComment.FindControl("lblCount") as Label;
                        var repeater = control as Repeater;
                        if (repeater != null)
                        {
                            if (divUserComment != null) divUserComment.Visible = true;
                            repeater.DataSource = rowResults.ToList();
                            repeater.DataBind();
                            if (lblCount != null)
                                lblCount.Text = !string.IsNullOrEmpty(rowResults.Count().ToString()) ? Convert.ToString(rowResults.Count()) : "0";
                           
                        }
                    }
                }
            }

        }

        private void BindCollegeBasicDetails(string collegeBranchCourseId)
        {
            try
            {
                var basicDetails =
                    CollegeProvider.Instance.GetCollegeBasicDetailsByBranchCourseId(
                        Convert.ToInt16(collegeBranchCourseId));
                if (basicDetails.Count > 0)
                {
                    liContactDetails.Visible = true;
                    liOverview.Visible = true;
                    ucCollegeBasicDetails.Visible = true;
                    var query = basicDetails.Select(result => new
                    {
                        result.CollegeIdBranchId,
                        CollegeBranchName = result.CollegeBranchName,
                        CollegeBranchCityName = result.CollegeBranchCityName,
                        CollegeBranchCityId = result.CollegeBranchCityId,
                        CollegePopulaorName = result.CollegePopulaorName,
                        CollegeBranchEst = result.CollegeBranchEst,
                        CollegeManagementType = result.CollegeManagementType,
                        CourseName = result.CourseName,
                        CourseId = result.CourseId,
                        Decsription = result.CollegeBranchDesc,
                        CoillegeBranchEmailId = result.CoillegeBranchEmailId,
                        CollegeBranchAddrs = result.CollegeBranchAddrs,
                        CollegeBranchWebsite = result.CollegeBranchWebsite,
                        CollegeBranchMobileNo = result.CollegeBranchMobileNo,
                        CollegeBranchFax = result.CollegeBranchFax,
                        CollegeCityId = result.CollegeBranchCityId,
                        CollegeLogo = result.CollegeBranchLogo,
                        CollegeUniversity = result.UniversityName,
                        CollegeUniversityId = result.UniversityId,
                        CollegeBranchStateName = result.CollegeBranchStateName,
                        CollegeBranchCourseOnlineStatus=result.CollegeBranchCourseOnlineStatus,
                        HelpLineNumber = result.HelpLineNumber, result.CollegeBranchCourseHelplineNo,
                        result.CollegeIsBookSeatVisible,
                        result.CourseIsBookSeatVisible,
                        
                    }).First();
                    if (ApplicationSettings.Instance.IsVissbibleBookYourSeat)
                    {
                        if (query.CourseIsBookSeatVisible)
                        {
                            if (query.CollegeBranchCourseOnlineStatus)
                            {
                            if (query.CollegeIsBookSeatVisible)
                            {
                               
                                    lnkBookSeat.Visible = true;
                                    lnkBookSeat.HRef =
                                        (Utils.ApplicationRelativeWebRoot + "bookseat/" +
                                         Utils.RemoveIllegealFromCourse(query.CourseName) + "/" +
                                         Utils.RemoveIllegalCharacters(
                                             query.CollegeBranchName)).ToLower();
                                }
                            }
                        }
                    }
                    hdnCollegeCourseIdBook.Value = collegeBranchCourseId;
                    hdnCourseIdBook.Value = Convert.ToString(query.CourseId);
                    hdnCourseBook.Value = query.CourseName;
                    hdnCollegeIdBook.Value = Convert.ToString(query.CollegeIdBranchId);
                    hdnCityIdBook.Value = Convert.ToString(query.CollegeBranchCityId);
                    ucCollegeBasicDetails.BranchName = query.CollegeBranchName != "" ? query.CollegeBranchName : "N/A";
                    ucCollegeBasicDetails.PopularName = query.CollegePopulaorName != "" ? query.CollegePopulaorName : "N/A";
                    ucCollegeBasicDetails.CourseName = query.CourseName != "" ? query.CourseName : "N/A";
                    ucCollegeBasicDetails.Establishment = query.CollegeBranchEst != "" ? query.CollegeBranchEst : "N/A";
                    ucCollegeBasicDetails.Management = query.CollegeManagementType != "" ? (query.CollegeManagementType + " College" ): "N/A";
                    ucCollegeBasicDetails.UniversityName = query.CollegeUniversity != "" && query.CollegeUniversity != "null" ? query.CollegeUniversity : "N/A";
                    ucCollegeBasicDetails.UniversityLink = Utils.AbsoluteWebRoot + "University-Detail/" + Utils.RemoveIllegalCharacters(query.CollegeUniversity);
                    sndOnLineCounselling.Visible = query.CollegeBranchCourseOnlineStatus;

                    ucPlacement.CollegeBranchCourseId = Convert.ToInt32(collegeBranchCourseId);
                    ucCall.CityName = query.CollegeBranchCityName;
                    if (query.Decsription != "" && query.Decsription != "null")
                    {
                        ucCollegeDescription.Description = query.Decsription;
                        liDescription.Visible = true;
                    }
                    else
                        ucCollegeDescription.Visible = false;

                    ucColegeContactsDetails.ContactEmailId = (query.CoillegeBranchEmailId != "" || query.CoillegeBranchEmailId != null) ? query.CoillegeBranchEmailId : "N/A";
                    ucColegeContactsDetails.ContactWebsites = (query.CollegeBranchWebsite != "" || query.CollegeBranchWebsite != null) ? query.CollegeBranchWebsite : "N/A";
                    ucColegeContactsDetails.ContactPhone = (query.CollegeBranchMobileNo != "" || query.CollegeBranchMobileNo != null) ? query.CollegeBranchMobileNo : "N/A";
                    ucColegeContactsDetails.ContactFax = (query.CollegeBranchFax != "" || query.CollegeBranchFax != null) ? query.CollegeBranchFax : "N/A";
                    ucColegeContactsDetails.ContactAddress = (query.CollegeBranchAddrs != "" || query.CollegeBranchAddrs != null) ? query.CollegeBranchAddrs : "N/A";
                    lblHeaderCollegeName.Text = query.CollegeBranchName;
                    if (!string.IsNullOrEmpty(query.CollegeBranchCourseHelplineNo))
                        txtHelpLineNo.Text = query.CollegeBranchCourseHelplineNo;
                    else
                        txtHelpLineNo.Text = query.HelpLineNumber;

                    CollegeImageHeader.ImageUrl = String.Format("{0}{1}", "/image.axd?College=", (query.CollegeLogo == null || string.IsNullOrEmpty(query.CollegeLogo)) ? "NoImage.jpg" : query.CollegeLogo);
                    CollegeImageHeader.AlternateText = query.CollegeBranchName;
                    ucGoogleMap.StateName = query.CollegeBranchStateName;
                    ucGoogleMap.CityName = query.CollegeBranchCityName;
                    ucQuickQUery.BranchCourseId = Convert.ToInt32(Request.QueryString["CollegeBranchCourseId"]);
                    ucQuickQUery.CollegeEmailId = query.CoillegeBranchEmailId;
                    UcComment.CourseId = Convert.ToString(query.CourseId);
                    ucQuickQUery.CollegeName = query.CollegeBranchName;
                    ucQuickQUery.CourseId = query.CourseId;
                    ucQuickQUery.CityName = query.CollegeBranchCityName;
                    BindPageTitleAndKeyWords(query.CollegeBranchCityName, query.CollegeBranchStateName, query.CourseName, query.CollegeBranchName, query.CollegePopulaorName);
                }
                else
                {
                    ucGoogleMap.Visible = false;
                    ucCollegeBasicDetails.Visible = false;
                    ucColegeContactsDetails.Visible = false;
                    ucCollegeDescription.Visible = false;
                    CollegeImageHeader.Visible = false;
                    lblHeaderCollegeName.Visible = false;
                    txtHelpLineNo.Visible = false;

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindCollegeBasicDetails in CollegeDetails.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindPageTitleAndKeyWords(string city, string state, string course, string college, string collegePopularName)
        {


            try
            {

                Page.Title = "";
                Page.Title = college + " | " + city + " | " + state + " | " + course + " - Admission Jankari";

                var metadesc = new HtmlMeta();
                metadesc.Attributes.Clear();
                metadesc.Name = "description";

                metadesc.Content = "Find " + college + ",   " + city + ",  " + state +
                                   " India. Get  proper information on courses offered, Admissions in " + college +
                                   ", Fees, Placements, Admission Criteria, Facilities, Contact Details, Rankings, History, Hostel, Map, Compare similar colleges with " +
                                   college + " - Admission Jankari";

                Page.Header.Controls.Add(metadesc);

                var metaKeywords = new HtmlMeta
                {
                    Name = "keywords",
                    Content =
                        college + " ,  " + collegePopularName + " , Admission in  " +
                        college + " , Courses offered by  " + college + " , Fees of  " + college +
                        " , Address of  " + college + " , Rank of  " + college +
                        " , Placement of  " + college + " , Admission criteria of  " + college +
                        " , Colleges at  " + city + " in " + state
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
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in CollegeDetails.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindCollegeCourseStreamDetails(string collegeBranchCourseId)
        {
            try
            {
           
                var streamData =
                    CollegeProvider.Instance.GetCollegeCourseStreamDetailsByBranchCourseId(
                        Convert.ToInt16(collegeBranchCourseId)).Where(result=>result.CollegeBranchCourseStreamStatus==true).ToList();
               
                if (streamData.Count > 0)
                {
                    ucQuickQUery.CourseStream = streamData;
                    ucCollegeBasicCourse.CourseName = streamData.First().CourseName;
                    liCourse.Visible = true;
                    ucCollegeBasicCourse.Visible = true;
                    ucCollegeBasicCourse.CourseStream = streamData;
                 

                }
                else
                {
                    ucCollegeBasicCourse.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindCollegeCourseStreamDetails in CollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }
        private void BindCollegeCourseExamDetails(string collegeBranchCourseId)
        {
            try
            {
                var examData =
                    CollegeProvider.Instance.GetCollegeCourseExamDetailsByBranchCourseId(
                        Convert.ToInt16(collegeBranchCourseId)).Where(result => result.CollegeCourseExamStatus == true).ToList();
                if (examData.Count > 0)
                {
                    liExam.Visible = true;
                    ucCollegeCourseExam.Visible = true;
                    ucCollegeCourseExam.CourseExam = examData;
                }
                else
                {
                    ucCollegeCourseExam.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindCollegeCourseExamDetails in CollegeDetails.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }
        private void BindCOllegeHostelDetails(string collegeBranchCourseId)
        {
            var hostelData =
                CollegeProvider.Instance.GetCollegeCourseHostelDetailsByBranchCourseId(
                    Convert.ToInt16(collegeBranchCourseId))
                               .Where(result => result.CollegeBranchCourseHostelStatus == true)
                               .ToList();
            if (hostelData.Count > 0)
            {
                liHostel.Visible = true;
                ucCollegeCourseHostel.Visible = true;
                ucCollegeCourseHostel.CollegeHostel = hostelData;


            }
            else
            {
                ucCollegeCourseHostel.Visible = false;
            }

        }
        private void BindCollegeFacalityDetails(string collegeBranchCourseId)
        {
            try
            {
                var facalityData =
                    CollegeProvider.Instance.GetCollegeCourseFacalityDetailsByBranchCourseId(
                        Convert.ToInt16(collegeBranchCourseId)).Where(result => result.CollegeBranchCourseFacilitieStatus == true)
                               .ToList();
                if (facalityData.Count > 0)
                {
                    liFacality.Visible = true;
                    ucCollegeCourseFacality.Visible = true;
                    ucCollegeCourseFacality.CollegeFacalities = facalityData;

                }
                else
                {
                    ucCollegeCourseFacality.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindCollegeFacalityDetails in CollegeDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindCollegePresidentSpeech(string collegeBranchCourseId)
        {
            try
            {
                var speech =
                    CollegeProvider.Instance.GetDirectorSpeechByBranchCourseId(
                        Convert.ToInt16(collegeBranchCourseId)).Where(result => result.CollegeBranchKeySpeechStatus == true)
                               .ToList();
                if (speech.Count > 0)
                {
                    liPresidentSpeech.Visible = true;
                    ucPresidentSpeech.Visible = true;
                    var query = speech.Select(result => new
                                                                  {
                                                                      CollegeBranchName = result.CollegeBranchName,
                                                                      CollegeBranchKeySpeechPersonDesignation =
                                                                  result.CollegeBranchKeySpeechPersonDesignation,
                                                                      CollegeBranchKeySpeechPersonName =
                                                                  result.CollegeBranchKeySpeechPersonName,
                                                                      CollegeBranchKeySpeechDesc =
                                                                  result.CollegeBranchKeySpeechDesc,
                                                                      CollegeBranchKeySpeechPersonImage =
                                                               result.CollegeBranchKeySpeechPersonImage,


                                                                  }).First();
                    ucPresidentSpeech.Designation = query.CollegeBranchKeySpeechPersonDesignation != ""
                                                        ? query.CollegeBranchKeySpeechPersonDesignation
                                                        : "N/A";

                    ucPresidentSpeech.DirectorName = query.CollegeBranchKeySpeechPersonName != ""
                                                        ? query.CollegeBranchKeySpeechPersonName
                                                        : "N/A";

                    var desc=query.CollegeBranchKeySpeechDesc.ToString();

                    ucPresidentSpeech.DirectorSpeech = ((desc.Length > 50) ? desc.Substring(0, 47) + "..." :(string.IsNullOrEmpty(desc) ? "NA" : desc));
                    ucPresidentSpeech.DirectorSpeechFull = desc;                           
                  
                   
                    ucPresidentSpeech.DirectorImage = String.Format("{0}{1}", "/image.axd?College=", string.IsNullOrEmpty(query.CollegeBranchKeySpeechPersonImage.ToString()) ? "NoImage.jpg" : query.CollegeBranchKeySpeechPersonImage);
                   
                    ucPresidentSpeech.DirectorImageAltText = query.CollegeBranchKeySpeechPersonName != ""
                                                             ? query.CollegeBranchKeySpeechPersonName
                                                             : "N/A";
                }
                else
                {
                    ucPresidentSpeech.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindCollegeBasicDetails in CollegeDetails.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindCollegeHighLights(string collegeBranchCourseId)

        {
            try
            {
                var highLights =
                    CollegeProvider.Instance.GetCollegeHighLightsByBranchCourseId(
                        Convert.ToInt16(collegeBranchCourseId)).Where(result => result.CollegeBranchCourseHighlightStatus == true)
                               .ToList();
                if (highLights.Count > 0)
                {
                    ucHighLights.Visible = true;
                    ucHighLights.CollegeHighLights = highLights;
                }
                else
                {
                    ucHighLights.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindCollegeBasicDetails in CollegeDetails.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindCollegeGallery(string collegeBranchCourseId)
        {
            try
            {
                var gallery =
                    CollegeProvider.Instance.GetCollegeImageGallery(
                        Convert.ToInt16(collegeBranchCourseId));
                if (gallery.Count > 0)
                {
                    liGallery.Visible = true;
                    ucCollegeBranchGallery.Visible = true;
                    ucCollegeBranchGallery.ImageGallery = gallery;
                }
                else
                {
                    ucCollegeBranchGallery.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindCollegeGallery in CollegeDetails.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}