using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using System.Data;
using System.Web.UI.HtmlControls;
using IryTech.AdmissionJankari.Components.Web.Controls;

namespace IryTech.AdmissionJankari20.Web.College
{
    public partial class CollegeComparison : PageBase
    {
        #region "DataMember"
        Common _objComman;
        DataSet _ds;
        UserManager _objUserManager;


        #endregion


     
        protected void Page_Load(object sender, EventArgs e)
        {
            FirstCollegeAviliableCourse.PageButtonCount = ApplicationSettings.Instance.MostViewdCollegePageCount;
            FirstCollegeAviliableCourse.PageSizeCourse = ApplicationSettings.Instance.MostViewdCollegePageSize;

            SeccondCollegeAviliableCourse.PageButtonCount = ApplicationSettings.Instance.MostViewdCollegePageCount;
            SeccondCollegeAviliableCourse.PageSizeCourse = ApplicationSettings.Instance.MostViewdCollegePageSize;

            CollegeAvailablePlaceMent1.PageButtonCount = ApplicationSettings.Instance.MostViewdCollegePageCount;
            CollegeAvailablePlaceMent1.PageSizeCourse = ApplicationSettings.Instance.MostViewdCollegePageSize;

            CollegeAvailablePlaceMent2.PageButtonCount = ApplicationSettings.Instance.MostViewdCollegePageCount;
            CollegeAvailablePlaceMent2.PageSizeCourse = ApplicationSettings.Instance.MostViewdCollegePageSize;

            if (IsPostBack) return;
           
            _objComman = new Common();
            if (Request.QueryString["CourseId"] != null)
            {
                _objComman.CourseId = Convert.ToInt16(Request.QueryString["CourseId"]);
                var courseDetails = CourseProvider.Instance.GetCourseById(_objComman.CourseId)
                                    .Where(
                                        result => result.CourseId == _objComman.CourseId
                                        )
                                    .ToList().FirstOrDefault();


                if (courseDetails != null) _objComman.CourseName = courseDetails.CourseName;
            }
             hdnCoursePopularName.Value = _objComman.CourseName;
             hndCourseId.Value = Convert.ToString(_objComman.CourseId);


            if (Request.QueryString["CollegeId1"] != null && Request.QueryString["CollegeId2"] != null)
            {

                BindCompare(Convert.ToInt32(Request.QueryString["CollegeId1"]), Convert.ToInt32(Request.QueryString["CollegeId2"]));

            }
            else
            {
                BindPageTitleWithCollege();
            }
           
            
        }
     



        //Event for Compare all college Details
        protected void CompareCollege(object sender, EventArgs e)
        {
            CompaireCollege();
        }


        private void BindPageTitleWithCollege()
        {
            try
            {

                Page.Title = "Compare " + new Common().CourseName + " collges |" + new Common().CourseName + " College Comparisons - Admission Jankari";

                var metadesc = new HtmlMeta();
                metadesc.Attributes.Clear();
                metadesc.Name = "keywords";

                metadesc.Content = new Common().CourseName + " College Comparison, Compare " + new Common().CourseName +
                                   " Colleges,Top " + new Common().CourseName +" Colleges, Private Colleges, Top " + new Common().CourseName
                                       +" Government College, Top Ranked "+ new Common().CourseName+ " Colleges, Best "+ new Common().CourseName +
                                       " Colleges, management, university, exam, hostel, rank, fees, placement and aviliable courses -Admissionjankari";                                       ;
                Page.Header.Controls.Add(metadesc);

                var metaKeywords = new HtmlMeta
                {
                    Name = "description",
                    Content = "Compare two or more " + new Common().CourseName +
                        " SColleges. Compare College on location, management, university, exam, hostel, rank, fees, placement and aviliable courses - Admission Jankari"
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
                const string addInfo = "Error While fetching BindPageTitleAndKeyWordsByCollegeCompare in CollegeComparasion.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        private void BindPageTitleAndKeyWordsByCollegeCompare(string college1,string college2)
        {

            try
            {

                Page.Title = "Compare " + college1 + " Vs. " + college2 + " Admission Jankari";

                var metadesc = new HtmlMeta();
                metadesc.Attributes.Clear();
                metadesc.Name = "description";

                metadesc.Content = "Compare " + college1 + " Vs. " + college2 +
                                   ". Compare colleges on location, management, university, exam, hostel, rank, fees, placement and aviliable courses - Admissionjankari";
                Page.Header.Controls.Add(metadesc);

                var metaKeywords = new HtmlMeta
                {
                    Name = "keywords",
                    Content =college1 +" "+college2 +
                        " ,Compare " + college1 + " Vs. " + college2 +" Top Colleges, Management Colleges, Best Colleges, Top Ranked "
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
                const string addInfo = "Error While fetching BindPageTitleAndKeyWordsByCollegeCompare in CollegeComparasion.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    protected void btnFirstSearchReplace_Click(object sender, EventArgs e)
        {
            txtFirstCollegeName.Text = txtFirstCollegeReplace.Text;
             CompaireCollege();
             txtFirstCollegeReplace.Text = "";
        }
       
        protected void btnSecondSearchReplace_Click(object sender, EventArgs e)
        {
            txtSecondCollegeName.Text = txtSecondCollegeReplace.Text;
           CompaireCollege();
           txtSecondCollegeReplace.Text = "";
        }
       
        private void ShowHideControl(HtmlGenericControl li, Label lblFirst, Label lblSecond)
        {
            if (!string.IsNullOrEmpty(lblFirst.Text) || !string.IsNullOrEmpty(lblSecond.Text))
                li.Visible = true;
            else
            {
                lblFirst.Text = "N/A";
                lblSecond.Text = "N/A";
                li.Visible = true;
            }
        }

        //End Event for ShowHideDetails
        #region "MemberFunction"

        // Method to Compaire The Collegs
        protected void CompaireCollege()
        {
           
            _objComman = new Common();
            var courseId = Convert.ToInt32(hndCourseId.Value);

            try
            {
                
                //To Retrive all CollegeDetails based in First CollegeId and First College Name
                var courseDetails1 = CollegeProvider.Instance.GetAllCollegeBranchNameByCourseIdCollegeName(courseId, txtFirstCollegeName.Text);

              if (courseDetails1.Count > 0)
                {
                    DivCollegeDetails.Focus();
                    var query = courseDetails1.Select(result => new
                    {
                        result.CollegeBranchCityName,
                        CollegeLogo=result.CollegeBranchLogo,
                        result.CollegeManagementType, result.CollegeBranchCourseId,
                        CollegeName = result.CollegeBranchName, result.CourseName,
                        CollegeLocation = result.CollegeBranchCityName,
                        CollegeEstablishment = result.CollegeBranchEst,
                        University = result.UniversityName,
                        Exam = result.ExamName, result.Hostel,

                    }).First();
                    hdnCollegeBranchName1.Value = query.CollegeName;
                    hdnCollegeBranchCourseid1.Value = Convert.ToString(query.CollegeBranchCourseId);
                    hdnCityId1.Value = query.CollegeBranchCityName;
                    lblCollegeName1.Text = query.CollegeName;
                    sndImgCollg1.HRef = Utils.ApplicationRelativeWebRoot + ("College-Details/" + Utils.RemoveIllegealFromCourse(query.CourseName) + "/" + Utils.RemoveIllegalCharacters(query.CollegeName)).ToLower();
                    divFirstCollege.Visible = true;
                    DivCollegeDetails.Visible = true;
                    if (!(query.CourseName.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                    {
                        lblCourseName.Text =
                            !string.IsNullOrEmpty(
                                Convert.ToString(query.CourseName.ToString(CultureInfo.InvariantCulture)))
                                ? query.CourseName
                                : "N/A";
                    }
                    else
                        lblCourseName.Text = "N/A";
                    if (!(query.CollegeLocation.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                    {
                        lblLocation.Text =
                            !string.IsNullOrEmpty(
                                Convert.ToString(query.CollegeLocation.ToString(CultureInfo.InvariantCulture)))
                                ? query.CollegeLocation
                                : "N/A";
                    }
                    else
                        lblLocation.Text = "N/A";
                    if (!(query.CollegeManagementType.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                    {
                        lblManagement.Text =
                            !string.IsNullOrEmpty(
                                Convert.ToString(query.CollegeManagementType.ToString(CultureInfo.InvariantCulture)))
                                ? query.CollegeManagementType
                                : "N/A";
                    }
                    else
                        lblManagement.Text = "N/A";
                    if (!(query.CollegeEstablishment.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                    {
                        lblYOS.Text =
                            !string.IsNullOrEmpty(
                                Convert.ToString(query.CollegeEstablishment.ToString(CultureInfo.InvariantCulture)))
                                ? query.CollegeEstablishment
                                : "N/A";
                    }
                    else
                        lblYOS.Text = "N/A";

                    lblManagement.Text = "N/A";
                    if (!(query.University.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                    {
                        lblUniversity.Text =
                            !string.IsNullOrEmpty(
                                Convert.ToString(query.University.ToString(CultureInfo.InvariantCulture)))
                                ? query.University
                                : "N/A";
                        lblUniversity.ToolTip = !string.IsNullOrEmpty(
                                Convert.ToString(query.University.ToString(CultureInfo.InvariantCulture)))
                                ? query.University
                                : "N/A";
                        if(!string.IsNullOrEmpty(query.University))
                        lblUniversity.NavigateUrl = Utils.ApplicationRelativeWebRoot + ("University-Detail/" +
                                  Utils.RemoveIllegalCharacters(query.University)).ToLower();
                    }
                    else
                        lblUniversity.Text = "N/A";
             
                    //To Retrive all Stream Data based on  CollegeBranchCourseId.
                    var streamData =CollegeProvider.Instance.GetCollegeCourseStreamDetailsByBranchCourseId(
                       Convert.ToInt32(query.CollegeBranchCourseId));
                    FirstCollegeAviliableCourse.CourseStream = streamData;
                    if (streamData.Count > 0)
                    {
                       
                        FirstCollegeAviliableCourse.CourseName = streamData.First().CourseName;
                    }
                    var examData = CollegeProvider.Instance.GetCollegeCourseExamDetailsByBranchCourseId(
                        Convert.ToInt32(query.CollegeBranchCourseId));
                    FirstCollegeExam.CourseExam = examData;
                    //To Retrive all Hostel details based on CollegeBranchCourseId.
                    var hostelData = CollegeProvider.Instance.GetCollegeCourseHostelDetailsByBranchCourseId(Convert.ToInt32(query.CollegeBranchCourseId));
                    SeccondCollegeHostel.CollegeHostel = hostelData;
                    
                   
                    imgFirstCollege.ImageUrl  = String.Format("{0}{1}", "/image.axd?College=", (query.CollegeLogo==null ||string.IsNullOrEmpty(query.CollegeLogo)) ? "NoImage.jpg" : query.CollegeLogo);
                    imgFirstCollege.AlternateText = query.CollegeName;
                    imgFirstCollege.ToolTip = query.CollegeName;
                    var collegeRankData = CollegeProvider.Instance.BindCollegeRankYear(query.CollegeBranchCourseId);
                    CollegeAvailableRank1.CollegeRankData = collegeRankData;

                    var collegePlaced = CollegeProvider.Instance.GetCollegeTopHirer(query.CollegeBranchCourseId);
                    CollegeAvailablePlaceMent1.CollegePlacementData = collegePlaced;
                }
                else
                {


                }
                //To Retrive all College Details based on CourseId and SecondCollegeName
                var corseDetails2 = CollegeProvider.Instance.GetAllCollegeBranchNameByCourseIdCollegeName(courseId, txtSecondCollegeName.Text).ToList();


                if (corseDetails2.Count > 0)
                {

                    var query = corseDetails2.Select(result => new
                    {
                        CollegeLogo = result.CollegeBranchLogo,
                        CollegeBranchCourseId=result.CollegeBranchCourseId ,
                        CollegeName = result.CollegeBranchName,
                        CourseName = result.CourseName,
                        CollegeLocation = result.CollegeBranchCityName,
                        CollegeEstablishment = result.CollegeBranchEst,
                        University = result.UniversityName,
                        Exam = result.ExamName, result.Hostel, result.CollegeManagementType,

                    }).First();
                    hdnCollegeBranchName2.Value = query.CollegeName;
                    hdnCollegeBranchCourseid2.Value = Convert.ToString(query.CollegeBranchCourseId);
                    hdnCityId2.Value = query.CollegeLocation;
                    lblCollegeName2.Text = query.CollegeName;
                    var collegeRankData = CollegeProvider.Instance.BindCollegeRankYear(query.CollegeBranchCourseId);
                    CollegeAvailableRank2.CollegeRankData = collegeRankData;

                    var collegePlaced = CollegeProvider.Instance.GetCollegeTopHirer(query.CollegeBranchCourseId);
                    CollegeAvailablePlaceMent2.CollegePlacementData = collegePlaced;

                    //To Retrive All Stream Name based on CollegeBranchCourseId.
                    var streamData = CollegeProvider.Instance.GetCollegeCourseStreamDetailsByBranchCourseId(
                      Convert.ToInt16(query.CollegeBranchCourseId));
                     SeccondCollegeAviliableCourse.CourseStream = streamData;
                    if (streamData.Count > 0)
                    {
                        SeccondCollegeAviliableCourse.CourseName = streamData.First().CourseName;
                    }
                    //To Retrive all Exam Details based on CollegeBranchCourseId.
                     var examData = CollegeProvider.Instance.GetCollegeCourseExamDetailsByBranchCourseId(
                        Convert.ToInt32(query.CollegeBranchCourseId));
                     SeccondCollegeExam.CourseExam = examData;
                     //To Retrive all hostel Details based on CollegeBranchCourseId.
                     var hostelData = CollegeProvider.Instance.GetCollegeCourseHostelDetailsByBranchCourseId(Convert.ToInt32(query.CollegeBranchCourseId));
                     FirstCollegeHostel.CollegeHostel = hostelData;
                     sndImgCollg2.HRef = Utils.ApplicationRelativeWebRoot + ("College-Details/" + Utils.RemoveIllegealFromCourse(query.CourseName) + "/" + Utils.RemoveIllegalCharacters(query.CollegeName)).ToLower();
                     imgSeccondCollege.ImageUrl = String.Format("{0}{1}", "/image.axd?College=", (query.CollegeLogo == null || string.IsNullOrEmpty(query.CollegeLogo)) ? "NoImage.jpg" : query.CollegeLogo);
                    imgSeccondCollege.AlternateText = query.CollegeName;
                    imgSeccondCollege.ToolTip = query.CollegeName;
                 
                    divSecondCollege.Visible = true;

                    if (!(query.CourseName.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                    {
                        lbl2CourseName.Text =
                            !string.IsNullOrEmpty(
                                Convert.ToString(query.CourseName.ToString(CultureInfo.InvariantCulture)))
                                ? query.CourseName
                                : "N/A";
                    }
                    else
                        lbl2CourseName.Text = "N/A";
                    if (!(query.CollegeLocation.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                    {
                        lbl2Location.Text =
                            !string.IsNullOrEmpty(
                                Convert.ToString(query.CollegeLocation.ToString(CultureInfo.InvariantCulture)))
                                ? query.CollegeLocation 
                                : "N/A";
                    }
                    else
                        lbl2Location.Text = "N/A";
                    if (!(query.CollegeManagementType.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                    {
                        lbl2Managemnt.Text =
                            !string.IsNullOrEmpty(
                                Convert.ToString(query.CollegeManagementType.ToString(CultureInfo.InvariantCulture)))
                                ? query.CollegeManagementType
                                : "N/A";
                    }
                    else
                        lbl2Managemnt.Text = "N/A";
                    if (!(query.CollegeEstablishment.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                    {
                        lbl2YOS.Text =
                            !string.IsNullOrEmpty(
                                Convert.ToString(query.CollegeEstablishment.ToString(CultureInfo.InvariantCulture)))
                                ? query.CollegeEstablishment
                                : "N/A";
                    }
                    else
                        lbl2YOS.Text = "N/A";
                    if (!(query.University.Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                    {
                        lbl2University.Text =
                            !string.IsNullOrEmpty(
                                Convert.ToString(query.University.ToString(CultureInfo.InvariantCulture)))
                                ? query.University
                                : "N/A";
                        lbl2University.ToolTip = !string.IsNullOrEmpty(
                                Convert.ToString(query.University.ToString(CultureInfo.InvariantCulture)))
                                ? query.University
                                : "N/A";
                        if (!string.IsNullOrEmpty(query.University))
                            lbl2University.NavigateUrl = Utils.ApplicationRelativeWebRoot + ("University-Detail/" +
                                      Utils.RemoveIllegalCharacters(query.University)).ToLower();
                    }
                    else
                        lbl2University.Text = "N/A";
  
                    lblCollegeName2.Text = query.CollegeName;
                }
                BindPageTitleAndKeyWordsByCollegeCompare(txtFirstCollegeName.Text, txtSecondCollegeName.Text); 
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  CompaireCollege in CollegeComparison.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            
            
            }

        }

        
        //End Method for ValidationErrorMessages.

        // Method to Bind the College Compare
        protected void BindCompare(int collegeId1, int collegeId2)
        {
            try
            {
                //To Retrive all CollegeName based on collegeID1
                var collegeData = CollegeProvider.Instance.GetCollegeBasicDetailsByBranchCourseId(collegeId1);
                //var data = CollegeProvider.Instance.GetCollegeListByCourse(collegeId1);

                if (collegeData.Count > 0)
                {
                    var query = collegeData.Select(result => new
                                                                 {
                                                                     result.CollegeBranchName,

                                                                 }).First();

                    txtFirstCollegeName.Text = query.CollegeBranchName;
                }

                var data1 = CollegeProvider.Instance.GetCollegeBasicDetailsByBranchCourseId(collegeId2);

                if (data1.Count > 0)
                {
                    var collegeBranch = data1.Select(result => new
                                                                   {
                                                                       result.CollegeBranchName,

                                                                   }).First();

                    txtSecondCollegeName.Text = collegeBranch.CollegeBranchName;
                }
                CompaireCollege();
                BindPageTitleAndKeyWordsByCollegeCompare(txtFirstCollegeName.Text, txtSecondCollegeName.Text); 
            }

            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in ExecutingEvent Page_Load in CollegeComparison.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
        }
        #endregion


    }
}