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
    public partial class CollegeDetails : PageBase
    {
        Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCollegeBasicCourse.PageButtonCount = ApplicationSettings.Instance.MostViewdCollegePageCount;
           ucCollegeBasicCourse.PageSizeCourse = ApplicationSettings.Instance.MostViewdCollegePageSize;
           ucCollegeChart.CollegeBranchCourseId = Convert.ToInt32(Request.QueryString["CollegeBranchCourseId"]);
           ADMJReportAbuse.AbuseType = Convert.ToString(CommentType.Col);
           ADMJReportAbuse.AbuseTypeId = Request.QueryString["CollegeBranchCourseId"];
           UcRating.RatingId = Convert.ToInt32(Request.QueryString["CollegeBranchCourseId"]);
           UcRating.RatingType = CommentType.Col.ToString();
           BindUserRating();
            if (Request.QueryString["CollegeBranchCourseId"] != null)
            {
                UcComment.CommentType = Convert.ToString(CommentType.Col);
                UcComment.CommentTypeId = Request.QueryString["CollegeBranchCourseId"];
                GetUserComment();
            }
            if (IsPostBack) return;
          
            if (Request.QueryString["CollegeBranchCourseId"] == null) return;
            ucCollegeEvent.GetCollegeEventList(Convert.ToInt32(Request.QueryString["CollegeBranchCourseId"]));
            if (ucCollegeEvent.ShowEvent == true)
                liEvent.Visible = true;
            UcReportDonation.BindUserStory(collegeBranchCourseId: Convert.ToInt32(Request.QueryString["CollegeBranchCourseId"]));
            GetPageClickCount(Convert.ToInt32(Request.QueryString["CollegeBranchCourseId"]));
            BindCollegeBasicDetails(Request.QueryString["CollegeBranchCourseId"]);
            BindCollegeCourseStreamDetails(Request.QueryString["CollegeBranchCourseId"]);
            BindCollegeCourseExamDetails(Request.QueryString["CollegeBranchCourseId"]);
            BindCOllegeHostelDetails(Request.QueryString["CollegeBranchCourseId"]);
            BindCollegeFacalityDetails(Request.QueryString["CollegeBranchCourseId"]);
            ucCollegeQuery.CollegeBranchCourseId = Convert.ToInt32(Request.QueryString["CollegeBranchCourseId"]);
            ucRelatedCollege.CollegeBranchCourseId = Convert.ToInt32(Request.QueryString["CollegeBranchCourseId"]);
            _objCommon = new Common();
            var i = _objCommon.InsertCollegePageClick(Convert.ToInt32(Request.QueryString["CollegeBranchCourseId"]));
           
        }
        private void GetUserComment()
        {
            var lblCount = UcComment.FindControl("lblCount") as Label;
            var dataset = new Common().GetUserComment(UcComment.CommentType, UcComment.CommentTypeId);
            lblCount.Text = "0";
            ADMJCommentCount.TotalCommentCount = "0";
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

                        var repeater = control as Repeater;
                        if (repeater != null)
                        {
                            if (divUserComment != null) divUserComment.Visible = true;
                            repeater.DataSource = rowResults.ToList();
                            repeater.DataBind();
                            if (lblCount != null)
                                lblCount.Text = !string.IsNullOrEmpty(rowResults.Count().ToString()) ? Convert.ToString(rowResults.Count()) : "0";
                            ADMJCommentCount.TotalCommentCount = !string.IsNullOrEmpty(rowResults.Count().ToString()) ? Convert.ToString(rowResults.Count()) : "0";
                        }
                    }
                }
            }

        }
        private void BindPageTitleAndKeyWords(string city, string state, string course, string college, string collegePopularName)
        {

          
            try
            {
                    Page.Title = "";
                    Page.Title = college+" | "+city+" | "+ state+" | "+ course+" - Admission Jankari";

                    var metadesc = new HtmlMeta();
                    metadesc.Attributes.Clear();
                    metadesc.Name = "description";

                metadesc.Content = "Find "+ course  +" in "  + college + ", " + city + ", " + state +
                                   " India. Get  proper information on " + course +  " courses offered, Admissions in " + college +
                                   ", Fees, Placements, Admission Criteria, Facilities, Contact Details, Rankings, History, Hostel, Map, Compare similar colleges with " +
                                   college + " - Admission Jankari";

                    Page.Header.Controls.Add(metadesc);

                var metaKeywords = new HtmlMeta
                                       {
                                           Name = "keywords",
                                           Content =
                                               college + " , " + collegePopularName + " , Admission in " +
                                            course + " in " +   college + " ," + course + " Courses offered by " + college + ", Fees of " + college +
                                               " , Address of  " + college + " , Rank of " + college +
                                               " , Placement of " + college + ", Admission criteria of " + college +
                                                "," + course + " colleges " + city + " in " + state
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
        private void BindCollegeBasicDetails(string collegeBranchCourseId)
        {
            try
            {
                var basicDetails =
                    CollegeProvider.Instance.GetCollegeBasicDetailsByBranchCourseId(
                        Convert.ToInt16(collegeBranchCourseId));
                _objCommon = new Common();
                if (basicDetails.Count > 0)
                {
                    ucCollegeBasicDetails.Visible = true;
                    var query = basicDetails.Select(result => new
                                                                  {
                                                                      result.CollegeBranchName,
                                                                      result.CollegeIdBranchId,
                                                                      result.CollegeBranchCityName,
                                                                      result.CollegeBranchCityId,
                                                                      result.CollegePopulaorName,
                                                                      result.CollegeBranchEst,
                                                                      result.CollegeManagementType,
                                                                      result.CourseId,
                                                                      result.CourseName,
                                                                      Decsription = result.CollegeBranchDesc,
                                                                      result.CoillegeBranchEmailId,
                                                                      result.CollegeBranchAddrs,
                                                                      result.CollegeBranchWebsite,
                                                                      result.CollegeBranchMobileNo,
                                                                      result.CollegeBranchFax,
                                                                      CollegeCityId = result.CollegeBranchCityId,
                                                                      CollegeLogo = result.CollegeBranchLogo,
                                                                      CollegeUniversity = result.UniversityName,
                                                                      CollegeUniversityId = result.UniversityId,
                                                                      OnlineConsullingStatus =
                                                                  result.CollegeBranchCourseOnlineStatus,
                                                                      result.CollegePhoneNo,
                                                                      CityName = result.CollegeBranchCityName,
                                                                      StateName = result.CollegeBranchStateName, result.HelpLineNumber, result.CollegeBranchCourseHelplineNo,
                                                                      result.CollegeIsBookSeatVisible,
                                                                      result.CourseIsBookSeatVisible,
                                                                       result.CollegeBranchCourseOnlineStatus
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
                    ucCollegeEvent.CollegeName = query.CollegeBranchName;
                    ucCollegeEvent.CityName = query.CollegeBranchCityName;
                    ucCollegeEvent.CourseId = query.CourseId;
                    ucCollegeEvent.CollegeBranchCourseId = Convert.ToInt32(collegeBranchCourseId);
                    ADMJCommentCount.TotalCommentTooltip = query.CollegeBranchName;
                    ADMJTotalViews.TotalViewsTooltip = query.CollegeBranchName;
                    UcRating.RatingToolTip = query.CollegeBranchName;
                    ucCall.CollegeName = query.CollegeBranchName;
                    ucCall.CollegeBranchCourseId = collegeBranchCourseId;
                    CollegeNotice.CollegeBranchId = Convert.ToInt32(query.CollegeIdBranchId);
                    ucTestimonial.CollegeBranchId = Convert.ToInt32(query.CollegeIdBranchId);
                    ucPlacement.CollegeBranchCourseId = Convert.ToInt32(collegeBranchCourseId);
                    UcCollegeHighLights.CollegeBranchCourseId = Convert.ToInt32(collegeBranchCourseId);
                    ucCall.CityName = query.CityName;
                    ucRelatedCollege.CollegeName = query.CollegeBranchName;
                    ucCollegeBasicDetails.BranchName = query.CollegeBranchName != "" ? query.CollegeBranchName : "N/A";
                    ucCollegeBasicDetails.PopularName =  string.IsNullOrEmpty(query.CollegePopulaorName)  ? query.CollegePopulaorName : "N/A";
                    ucCollegeBasicDetails.CourseName = query.CourseName != "" ? query.CourseName : "N/A";
                    ucCollegeBasicDetails.SetBalackListed=new Common().IsDonationRepoted(Convert.ToInt32(collegeBranchCourseId))==true?"blacklisted":"";
                    ucCollegeBasicDetails.BlackListedTitle = new Common().IsDonationRepoted(Convert.ToInt32(collegeBranchCourseId)) == true ? "Donation reported against the " + query.CollegeBranchName : "";
                    ucCollegeBasicDetails.Establishment =  string.IsNullOrEmpty(query.CollegeBranchEst) ? query.CollegeBranchEst : "N/A";
                    ucCollegeBasicDetails.Management = query.CollegeManagementType != "" ? query.CollegeManagementType+" College" : "N/A";
                    ucCollegeBasicDetails.UniversityName = query.CollegeUniversity != "" && query.CollegeUniversity !="null"? query.CollegeUniversity : "N/A";
                    ucCollegeBasicDetails.UniversityLink =Utils.AbsoluteWebRoot+"University-Detail/" + Utils.RemoveIllegalCharacters(query.CollegeUniversity);
                    _objCommon.CourseId = query.CourseId;
                    ucCollegeQuery.CousreId = query.CourseId;
                    ucCollegeQuery.CollegeBranchId = Convert.ToInt32(query.CollegeIdBranchId);
                    ucCollegeQuery.CollegeBranchName = query.CollegeBranchName;
                    ucCollegeQuery.CourseName = query.CourseName;
                    UcComment.CourseId = Convert.ToString(query.CourseId);
                    _objCommon.CourseName = Utils.RemoveIllegealFromCourse(query.CourseName);
                    lnkReportdonation.HRef = Utils.AbsoluteWebRoot + ("ReportDonation/" + Utils.RemoveIllegealFromCourse(query.CourseName)+"/"+ Utils.RemoveIllegalCharacters(query.CollegeBranchName)).ToLower();
                    spnText.InnerHtml = query.CollegeManagementType == "Government"
                        ? "Apply for this and other top pvt colleges" : query.OnlineConsullingStatus == true ? "Apply for this  and other similar colleges" : "Apply for this and other top pvt colleges";

                    if (!string.IsNullOrEmpty(query.Decsription))
                    {
                        if (!(query.Decsription.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                        {
                            if (!string.IsNullOrEmpty(query.Decsription))
                            {
                                string str = System.Text.RegularExpressions.Regex.Replace(query.Decsription, "<[^>]*>", "", System.Text.RegularExpressions.RegexOptions.Multiline | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                                ucCollegeDescription.Description = str;

                                liDescription.Visible = true;
                            }
                            else
                                ucCollegeDescription.Visible = false;

                        }
                        else
                            ucCollegeDescription.Visible = false;
                    }
                    else
                    {
                        ucCollegeDescription.Visible = false;
                    }
                    liContactDetails.Visible = true;
                    liOverview.Visible = true;
                    if (!string.IsNullOrEmpty(query.CoillegeBranchEmailId))
                    {
                        if (!(query.CoillegeBranchEmailId.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                        {
                            if (!string.IsNullOrEmpty(query.CoillegeBranchEmailId))
                                ucColegeContactsDetails.ContactEmailId = query.CoillegeBranchEmailId;
                            else
                                ucColegeContactsDetails.IsEmailIdVisisble = false;
                        }
                        else
                            ucColegeContactsDetails.IsEmailIdVisisble = false;
                    }
                    else
                    {
                        ucColegeContactsDetails.IsEmailIdVisisble = false;
                    }
                    if (!string.IsNullOrEmpty(query.CollegePhoneNo))
                    {
                        if (!(query.CollegePhoneNo.Equals("null", StringComparison.OrdinalIgnoreCase)))
                        {
                            if (!string.IsNullOrEmpty(query.CollegePhoneNo))
                                ucColegeContactsDetails.ContactPhone1 = query.CollegePhoneNo;
                            else
                                ucColegeContactsDetails.IsVisiblePhone = false;
                        }
                        else
                            ucColegeContactsDetails.IsVisiblePhone = false;
                    }
                    else
                    {
                        ucColegeContactsDetails.IsVisiblePhone = false;
                    }
                    if (!string.IsNullOrEmpty(query.CollegeBranchWebsite))
                    {
                        if (!(query.CollegeBranchWebsite.Equals("null", StringComparison.OrdinalIgnoreCase)))
                        {
                            if (!string.IsNullOrEmpty(query.CollegeBranchWebsite))
                                ucColegeContactsDetails.ContactWebsites = query.CollegeBranchWebsite;
                            else
                                ucColegeContactsDetails.IsWebSiteVisible = false;
                        }
                        else
                            ucColegeContactsDetails.IsWebSiteVisible = false;

                    }
                    else
                    {
                        ucColegeContactsDetails.IsWebSiteVisible = false;
                    }
                    if (!string.IsNullOrEmpty(query.CollegeBranchMobileNo))
                    {
                        if (!(query.CollegeBranchMobileNo.Equals("null", StringComparison.OrdinalIgnoreCase)))
                        {
                            if (!string.IsNullOrEmpty(query.CollegeBranchMobileNo))
                                ucColegeContactsDetails.ContactPhone = query.CollegeBranchMobileNo;
                            else
                                ucColegeContactsDetails.IsVisibleMobile = false;
                        }
                        else
                            ucColegeContactsDetails.IsVisibleMobile = false;
                    }
                    else
                    {
                        ucColegeContactsDetails.IsVisibleMobile = false;
                    }
                    if (!string.IsNullOrEmpty(query.CollegeBranchFax))
                    {
                        if (!(query.CollegeBranchFax.Equals("null", StringComparison.OrdinalIgnoreCase)))
                            if (!string.IsNullOrEmpty(query.CollegeBranchFax))
                                ucColegeContactsDetails.ContactFax = query.CollegeBranchFax;
                            else
                                ucColegeContactsDetails.IsVisibleFax = false;
                        else
                            ucColegeContactsDetails.IsVisibleFax = false;

                    }
                    else
                    {
                        ucColegeContactsDetails.IsVisibleFax = false;
                    }
                    if (!string.IsNullOrEmpty(query.CollegeBranchAddrs))
                    {
                        if (!(query.CollegeBranchAddrs.Equals("null", StringComparison.OrdinalIgnoreCase)))
                            ucColegeContactsDetails.ContactAddress = !string.IsNullOrEmpty(query.CollegeBranchAddrs) ? query.CollegeBranchAddrs + "," + query.CityName + "," + query.StateName : query.CityName + "," + query.StateName;
                        else
                            ucColegeContactsDetails.IsVisibleAddrs = false;
                    }
                    else
                    {
                        ucColegeContactsDetails.IsVisibleAddrs = false;
                    }
                  
                  
                  
                    ucGoogleMap.StateName = query.StateName;
                    ucGoogleMap.CityName = query.CollegeBranchAddrs + "," + query.CityName;
                    lblHeaderCollegeName.Text = query.CollegeBranchName;

                    if (!string.IsNullOrEmpty(query.CollegeBranchCourseHelplineNo))
                          txtHelpLineNo.Text = query.CollegeBranchCourseHelplineNo;
                        else
                            txtHelpLineNo.Text = query.HelpLineNumber;



                    CollegeImageHeader.ImageUrl = String.Format("{0}{1}", "/image.axd?College=", string.IsNullOrEmpty(query.CollegeLogo) ? "NoImage.jpg" : query.CollegeLogo);
                    CollegeImageHeader.AlternateText = query.CollegeBranchName;
                    ucQuickQUery.BranchCourseId = Convert.ToInt32(Request.QueryString["CollegeBranchCourseId"]);
                    ucQuickQUery.CityName = query.CollegeBranchCityName;
                    ucQuickQUery.CollegeEmailId = query.CoillegeBranchEmailId;
                    ucQuickQUery.CollegeName = query.CollegeBranchName;
                    ucQuickQUery.CourseId = query.CourseId;
                    BindPageTitleAndKeyWords(query.CityName, query.StateName, query.CourseName, query.CollegeBranchName, query.CollegePopulaorName);
                }
                else
                {
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
        private void BindCollegeCourseStreamDetails(string collegeBranchCourseId)
        {
            try
            {
               
                var streamData =
                    CollegeProvider.Instance.GetCollegeCourseStreamDetailsByBranchCourseId(
                        Convert.ToInt32(collegeBranchCourseId)).Where(result => result.CollegeBranchCourseStreamStatus == true).ToList();
                ucQuickQUery.CourseStream = streamData;
                if (streamData.Count > 0)
                {
                
                   ucCollegeBasicCourse.CourseName= streamData.First().CourseName;
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
        {try
        {
            var examData =
                CollegeProvider.Instance.GetCollegeCourseExamDetailsByBranchCourseId(
                    Convert.ToInt32(collegeBranchCourseId)).Where(result => result.CollegeCourseExamStatus == true).ToList();
            if (examData.Count>0)
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
                    Convert.ToInt32(collegeBranchCourseId))
                               .Where(result => result.CollegeBranchCourseHostelStatus == true)
                               .ToList();
            if(hostelData.Count>0)
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
                        Convert.ToInt16(collegeBranchCourseId))
                                   .Where(result => result.CollegeBranchCourseFacilitieStatus == true)
                                   .ToList();
                if(facalityData.Count>0)
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
            catch(Exception ex)
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

        // method to get the page view
        private void GetPageClickCount(int collegeBranchCourseId)
        {
             var count = new Common().GetCollegePageClick(collegeBranchCourseId);
            ADMJTotalViews.TotalViewCount = !string.IsNullOrEmpty(count.ToString(System.Globalization.CultureInfo.InvariantCulture)) ? count.ToString(System.Globalization.CultureInfo.InvariantCulture) : "0";
        }
        // Method to Bind The Rating 
        protected void BindUserRating()
        {
            Common objCommon = new Common();
            System.Data.DataTable DT = new System.Data.DataTable();
            try
            {
                DT = objCommon.GetUserRating(Convert.ToString(CommentType.Col), Convert.ToInt32(Request.QueryString["CollegeBranchCourseId"]));
                if (DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        UcRating.Rate1 = UcRating.Rate1 + Convert.ToInt16(DT.Rows[i]["AjRating1"]);
                        UcRating.Rate2 = UcRating.Rate2 + Convert.ToInt16(DT.Rows[i]["AjRating2"]);
                        UcRating.Rate3 = UcRating.Rate3 + Convert.ToInt16(DT.Rows[i]["AjRating3"]);
                        UcRating.Rate4 = UcRating.Rate4 + Convert.ToInt16(DT.Rows[i]["AjRating4"]);
                        UcRating.Rate5 = UcRating.Rate5 + Convert.ToInt16(DT.Rows[i]["AjRating5"]);

                    }

                }
                else
                {
                    UcRating.Rate1 = 0;
                    UcRating.Rate2 = 0;
                    UcRating.Rate3 = 0;
                    UcRating.Rate4 = 0;
                    UcRating.Rate5 = 0;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex;
                }
                const string addInfo = "Error while executing BindUserRating() in CollegeDetails.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}