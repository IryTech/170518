
using System.Collections.Generic;
using System.Data;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari.Components.Web.HttpModules
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.IO;
  

    /// <summary>
    /// Handles pretty URL's and redirects them to the permalinks.
    /// </summary>
    public class UrlRewrite : IHttpModule
    {
        #region Constants and Fields

        /// <summary>
        /// The Year Regex.
        /// </summary>
        private static readonly Regex YearRegex = new Regex(
            "/([0-9][0-9][0-9][0-9])/",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// The Year Month Regex.
        /// </summary>
        private static readonly Regex YearMonthRegex = new Regex(
            "/([0-9][0-9][0-9][0-9])/([0-1][0-9])/",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// The Year Month Day Regex.
        /// </summary>
        private static readonly Regex YearMonthDayRegex = new Regex(
            "/([0-9][0-9][0-9][0-9])/([0-1][0-9])/([0-3][0-9])/",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        #endregion

        #region Implemented Interfaces

        #region IHttpModule

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
            // Nothing to dispose
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += ContextBeginRequest;
        }

        #endregion

        #endregion

        #region Methods

        private static string GetUrlWithQueryString(HttpContext context)
        {
            return string.Format(
                "{0}?{1}", context.Request.Path, context.Request.QueryString.ToString());
        }

        /// <summary>
        /// Extracts the year and month from the requested URL and returns that as a DateTime.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="year">
        /// The year number.
        /// </param>
        /// <param name="month">
        /// The month number.
        /// </param>
        /// <param name="day">
        /// The day number.
        /// </param>
        /// <returns>
        /// Whether date extraction succeeded.
        /// </returns>
        private static bool ExtractDate(HttpContext context, out int year, out int month, out int day)
        {
            year = 0;
            month = 0;
            day = 0;



            var match = YearMonthDayRegex.Match(GetUrlWithQueryString(context));
            if (match.Success)
            {
                year = int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                month = int.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
                day = int.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);
                return true;
            }

            match = YearMonthRegex.Match(GetUrlWithQueryString(context));
            if (match.Success)
            {
                year = int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                month = int.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Extracts the title from the requested URL.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="url">
        /// The url string.
        /// </param>
        /// <returns>
        /// The extract title.
        /// </returns>
        private static string ExtractTitle(HttpContext context, string url)
        {
            url = url.ToLowerInvariant().Replace("---", "-");

            if (!string.IsNullOrEmpty(ApplicationConfig.FileExtension))
            {
                if (url.Contains("?"))
                    url = url.Substring(0, url.LastIndexOf("?", System.StringComparison.Ordinal));

                if (url.EndsWith("/"))
                    url = url.Substring(0, url.Length - 1);

                if (url.Contains("/"))
                    url = url.Substring(url.LastIndexOf("/", System.StringComparison.Ordinal) + 1);

                return context.Server.HtmlEncode(url);
            }

            if (url.Contains(ApplicationConfig.FileExtension) && url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
                context.Response.AppendHeader("location", url);
                context.Response.StatusCode = 301;
            }

            if (url.Contains(ApplicationConfig.FileExtension))
                url = url.Substring(0, url.IndexOf(ApplicationConfig.FileExtension, System.StringComparison.Ordinal));

            var index = url.LastIndexOf("/", System.StringComparison.Ordinal) + 1;
            var title = url.Substring(index);
            return context.Server.HtmlEncode(title);
        }

        /// <summary>
        /// Gets the query string from the requested URL.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The query string.
        /// </returns>
        private static string GetQueryString(HttpContext context)
        {
            var query = context.Request.QueryString.ToString();
            return !string.IsNullOrEmpty(query) ? string.Format("&{0}", query) : string.Empty;
        }





        /// <summary>
        /// The rewrite default.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private static void RewriteDefault(HttpContext context)
        {
            var url = GetUrlWithQueryString(context);
               string newUrl = url.Replace("Default.aspx", "default.aspx");  // fixes a casing oddity on Mono
              int defaultStart = url.IndexOf("default.aspx", StringComparison.OrdinalIgnoreCase);
              newUrl = Utils.ApplicationRelativeWebRoot + url.Substring(defaultStart);
             context.RewritePath(newUrl);
           
        }

        //method to rewrite search college by city....
        
        private static void RewriteCollegeSearchByCity(HttpContext context, string url)
        {
            if (url.Contains("/JS/")) return;
            var cityName = ExtractTitle(context, url);
            int i = url.LastIndexOf("/City/".ToUpperInvariant());
            if (i > 0)
            {
                var courseName = url.Substring(1, i - 1);
                var CourseList = CourseProvider.Instance.GetAllCourseList().Where(result =>Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName), StringComparison.OrdinalIgnoreCase)).ToList().FirstOrDefault();
                if (CourseList != null)
                {
                    var city = CityProvider.Instacnce.GetAllCityList().Find(
                        p =>

                        (cityName.Equals(Utils.RemoveIllegalCharacters(p.CityName.Trim()), StringComparison.OrdinalIgnoreCase)));

                    if (city == null)
                    {
                        return;
                    }


                    context.RewritePath(Utils.ApplicationRelativeWebRoot + "College/CollegeSearch.aspx?CityId=" + city.CityId + "&CourseId=" + CourseList.CourseId,
                                        false);
                }
            }
        }
        //method to rewrite search college by city....

        private static void RewriteCollegeSearchByState(HttpContext context, string url)
        {
            if (url.Contains("/JS/")) return;
            var stateName = ExtractTitle(context, url);
            stateName = Utils.RemoveIllegalCharacters(stateName);
            var i = url.LastIndexOf("/State/".ToUpperInvariant(), System.StringComparison.Ordinal);
            if (i <= 0) return;
            var courseName = url.Substring(1, i - 1);
            var courseList = CourseProvider.Instance.GetAllCourseList().Where(result => Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName), StringComparison.OrdinalIgnoreCase)).ToList().FirstOrDefault();
            if (courseList == null) return;
            var state = StateProvider.Instance.GetAllState().Find(
                p =>

                    (stateName.Equals(Utils.RemoveIllegalCharacters(p.StateName.Trim()), StringComparison.OrdinalIgnoreCase)));

            if (state == null)
            {
                return;
            }


            context.RewritePath(Utils.ApplicationRelativeWebRoot + "College/CollegeSearch.aspx?StateId=" + state.StateId + "&CourseId=" + courseList.CourseId,
                false);
        }
        private static void RewriteCollegeSearchByExam(HttpContext context, string url)
        {
            if (url.Contains("/JS/")) return;
            var examName = ExtractTitle(context, url);
            var i = url.LastIndexOf("/college/".ToUpperInvariant(), StringComparison.Ordinal);
            if (i <= 0) return;
            var courseName = url.Substring(1, i - 1);
            var courseList = CourseProvider.Instance.GetAllCourseList().Where(result => Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName), StringComparison.OrdinalIgnoreCase)).ToList().FirstOrDefault();
            if (courseList != null)
            {
                var exam = ExamProvider.Instance.GetAllExamList().Find(
                    p =>

                    (examName.Equals(Utils.RemoveIllegalCharacters(p.ExamName), StringComparison.OrdinalIgnoreCase)));

                if (exam == null)
                {
                    return;
                }


                context.RewritePath(Utils.ApplicationRelativeWebRoot + "College/CollegeSearch.aspx?ExamId=" + exam.ExamId + "&CourseId=" + courseList.CourseId,
                                    false);
            }
        }

        private static void RewriteCollegeSearchByCollegeName(HttpContext context, string url)
        {
            var collegeName = url.Split('=');
            if (!url.Contains(".aspx".ToUpperInvariant()))
            {
                var college = collegeName[1].Replace("_", " ").Replace("%U2019", "’").Trim();

                context.RewritePath(Utils.ApplicationRelativeWebRoot + "College/CollegeSearch.aspx?CollegeName=" + college,

                                    false);
            }
            else
            {
                context.RewritePath(Utils.ApplicationRelativeWebRoot + "College/CollegeSearch.aspx",

                                  false);
            }

        }

        private static void RewriteExamSearchByExamName(HttpContext context, string url)
        {
            var examName = url.Split('=');
            var exam = examName[1].Replace("_", " ").Replace("%U2019", "’").Trim();

            context.RewritePath(Utils.ApplicationRelativeWebRoot + "Exam/ExamList.aspx?Exam=" + exam,
                                false);

        }

        private static void RewriteNewPage(HttpContext context, string url)
        {

            var newsTitle = ExtractTitle(context, url);
            var newsData = NewsArticleNoticeProvider.Instance.GetAllNewsList().Find(
                p =>

                (newsTitle.Equals(Utils.RemoveIllegalCharacters(p.NewsSubject), StringComparison.OrdinalIgnoreCase)));
            if (newsData != null)
                context.RewritePath(
                    Utils.ApplicationRelativeWebRoot + "NewsAndArticles/NewsDetails.aspx?NewsId=" + newsData.NewsId,
                    false);


        }

        private static void RewriteNoticePage(HttpContext context, string url)
        {
            var noticeTitle = ExtractTitle(context, url);
            var noticeData = NewsArticleNoticeProvider.Instance.GetAllNoticeList().Find(
                p =>

                (noticeTitle.Equals(Utils.RemoveIllegalCharacters(p.NoticeSubject), StringComparison.OrdinalIgnoreCase)));
            if (noticeData != null)
                context.RewritePath(
                    Utils.ApplicationRelativeWebRoot + "NewsAndArticles/NoticeDetails.aspx?NoticeId=" +
                    noticeData.NoticeId,
                    false);

        }

        private static void RewriteLoanPage(HttpContext context, string url)
        {

            var bankTitle = ExtractTitle(context, url);
            var bankData = BankProvider.Instance.GetAllBankList().Find(
                p =>

                (bankTitle.Equals(Utils.RemoveIllegalCharacters(p.BankName), StringComparison.OrdinalIgnoreCase)));
            if (bankData != null)
                context.RewritePath(
                    Utils.ApplicationRelativeWebRoot + "BankLoan/BankLoanDetails.aspx?BankId=" + bankData.BankId,
                    false);


        }

        private static void RewriteExamPage(HttpContext context, string url)
        {

            var examTitle = ExtractTitle(context, url);
            var examData = ExamProvider.Instance.GetAllExamList().Find(
                p =>

                (examTitle.Equals(Utils.RemoveIllegalCharacters(p.ExamName), StringComparison.OrdinalIgnoreCase)));
            if (examData != null)
                context.RewritePath(
                    Utils.ApplicationRelativeWebRoot + "Exam/ExamDetails.aspx?ExamId=" + examData.ExamId,
                    false);


        }

        private static void RewriteCollegeComparePage(HttpContext context, string url)
        {

            if (!url.Contains('&'))
            {
                var i = url.LastIndexOf("Compare-Colleges/".ToUpperInvariant(), StringComparison.Ordinal);

                var courseName = url.Split('/')[1];
                var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                        .Where(
                                            result =>
                                           Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                              StringComparison.OrdinalIgnoreCase))
                                        .ToList().FirstOrDefault();
                if(courseDetails!=null)
                    RewritePhysicalPageGeneric(context, url, "College/CollegeComparison.aspx?CourseId=" + courseDetails.CourseId);
            }
            else
            {

                var splitUrl = url.Split('&');
                var i = splitUrl[0].LastIndexOf("/", StringComparison.Ordinal);
                var courseName = splitUrl[0].Substring(1, i - 1);
                i = splitUrl[0].LastIndexOf("=", System.StringComparison.Ordinal);
                var college1 = splitUrl[0].Substring(i + 1, splitUrl[0].Length - (i + 1));
                if (college1 != null)
                {
                    i = splitUrl[1].LastIndexOf("=", StringComparison.Ordinal);
                    var college2 = splitUrl[1].Substring(i + 1, splitUrl[1].Length - (i + 1));
                    var courseList =
                        CourseProvider.Instance.GetAllCourseList()
                                      .Where(
                                          result =>
                                         Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                            StringComparison.OrdinalIgnoreCase))
                                      .ToList();
                    var courseDetails = courseList.First();
                    var collegeList = new Common().GetCollegeNameList(courseDetails.CourseId);

                    var query =
                        collegeList.Tables[0].AsEnumerable().Where(colleges => college1.Equals(
                            Utils.RemoveIllegalCharacters(colleges.Field<string>("AjCollegeBranchName")),
                            StringComparison.OrdinalIgnoreCase)).Select(colleges => new
                                                                                        {
                                                                                            collegeBranchCourseId =
                                                                                        colleges.Field<int>(
                                                                                            "AjCollegeBranchCourseId")
                                                                                        });
                    var collegeDetails1 = query.First();
                    var query1 =
                        collegeList.Tables[0].AsEnumerable().Where(colleges => college2.Equals(
                            Utils.RemoveIllegalCharacters(colleges.Field<string>("AjCollegeBranchName")),
                            StringComparison.OrdinalIgnoreCase)).Select(colleges => new
                                                                                        {
                                                                                            collegeBranchCourseId =
                                                                                        colleges.Field<int>(
                                                                                            "AjCollegeBranchCourseId")
                                                                                        });
                    var collegeDetails2 = query1.First();
                    if (collegeDetails1 != null && collegeDetails2 != null)
                        context.RewritePath(Utils.ApplicationRelativeWebRoot +
                                            "College/CollegeComparison.aspx?CollegeId1=" +
                                            collegeDetails1.collegeBranchCourseId + "&" + "CollegeId2=" +
                                            collegeDetails2.collegeBranchCourseId);
                }


            }

        }

        private static void RewriteStreamComparePage(HttpContext context, string url)
        {

            if (!url.Contains('&'))
            {


                string courseName = url.Split('/')[1];
             var courseDetails= CourseProvider.Instance.GetAllCourseList()
                                     .Where(
                                         result =>
                                        Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                           StringComparison.OrdinalIgnoreCase))
                                     .ToList().FirstOrDefault();
                if(courseDetails!=null)
                    RewritePhysicalPageGeneric(context, url, "College/CourseComparison.aspx?CourseId="+courseDetails.CourseId);
            }
            else
            {

                var splitUrl = url.Split('&');
                var i = splitUrl[0].LastIndexOf("/", StringComparison.Ordinal);
                var courseName = splitUrl[0].Substring(1, i - 1);
                i = splitUrl[0].LastIndexOf("=", StringComparison.Ordinal);
                var stream1 = splitUrl[0].Substring(i + 1, splitUrl[0].Length - (i + 1));
                if (stream1 != null)
                {
                    i = splitUrl[1].LastIndexOf("=", StringComparison.Ordinal);
                    var stream2 = splitUrl[1].Substring(i + 1, splitUrl[1].Length - (i + 1));
                    var courseList =
                        CourseProvider.Instance.GetAllCourseList()
                                      .Where(
                                          result =>
                                          Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                            StringComparison.OrdinalIgnoreCase))
                                      .ToList();
                    var streamData1 = StreamProvider.Instance.GetStreamListByCourse(courseList.First().CourseId).Find(
                        p =>

                        (stream1.Equals(Utils.RemoveIllegalCharacters(p.CourseStreamName),
                                        StringComparison.OrdinalIgnoreCase)));

                    var streamData2 = StreamProvider.Instance.GetStreamListByCourse(courseList.First().CourseId).Find(
                        p =>

                        (stream2.Equals(Utils.RemoveIllegalCharacters(p.CourseStreamName),
                                        StringComparison.OrdinalIgnoreCase)));

                    if (streamData1 != null && streamData2 != null)
                        context.RewritePath(Utils.ApplicationRelativeWebRoot +
                                            "College/CourseComparison.aspx?StreamId1=" +
                                            streamData1.StreamId + "&" + "StreamId2=" + streamData2.StreamId);
                }


            }

        }

        //for profile to be redirected..
        private static void RewriteToProfile(HttpContext context, string urlRel)
        {
            var queryString = "";
            if (urlRel.Contains("?T="))
            {
                queryString ="?"+urlRel.Split('?')[1];
            }
            var key = ExtractTitle(context,urlRel);

            switch (key)
            {
                case "login":
                    RewritePhysicalPageGeneric(context, urlRel, "Account/Login.aspx");

                    break;
                case "new-user-registration-form":
                    RewritePhysicalPageGeneric(context, urlRel, "Account/Register.aspx");

                    break;
                case "collegelogin.aspx":
                    RewritePhysicalPageGeneric(context, urlRel, "Account/collegelogin.aspx");

                    break;
                case "college-registeration":
                    RewritePhysicalPageGeneric(context, urlRel, "Account/CollegeRegisteration.aspx");

                    break;
                case "college-login":
                    RewritePhysicalPageGeneric(context, urlRel, "account/collegelogin.aspx");

                    break;
                case "college-profile":
                    RewritePhysicalPageGeneric(context, urlRel, "account/collegeprofile.aspx" + queryString);

                    break;
                case "paymentoption.aspx":
                    RewritePhysicalPageGeneric(context, urlRel, "account/paymentoption.aspx");

                    break;
                case "paymentconfirmation.aspx":
                    RewritePhysicalPageGeneric(context, urlRel, "account/paymentconfirmation.aspx");

                    break;
                default:
                    RewritePhysicalPageGeneric(context, urlRel, "Account/UserProfile.aspx");

                    break;
            }

        }
        //
        private static void RewriteExams(HttpContext context, string url)
        {
           
            var courseName = ExtractTitle(context, url);
            var courseList = CourseProvider.Instance.GetAllCourseList().Where(result => Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName), StringComparison.OrdinalIgnoreCase)).ToList();
            var courseDetails = courseList.First();
            if (courseDetails != null)
                context.RewritePath(Utils.ApplicationRelativeWebRoot + "Exam/ExamList.aspx?CourseId="+courseDetails.CourseId,
                                  false);
        }
        private static void RewriteReportdonation(HttpContext context, string url)
        {
            var courseName = ExtractTitle(context, url);
            if(courseName.Equals("report-donation"))
            {     context.RewritePath(Utils.ApplicationRelativeWebRoot + "ReportDonation.aspx",
                            false);
            }
            else
            {
                int i = url.ToLower().LastIndexOf("report-donation/");
                int j = url.LastIndexOf("/");
                var course = url.Substring(i, j-1);
                course = ExtractTitle(context, course);
                var courseList = CourseProvider.Instance.GetAllCourseList().Where(result => course.Equals(Utils.RemoveIllegealFromCourse(result.CourseName), StringComparison.OrdinalIgnoreCase)).ToList();
                 var courseDetails = courseList.First();
                if (courseDetails != null)
                    context.RewritePath(Utils.ApplicationRelativeWebRoot + "ReportDonation.aspx?collegename=" + courseName.Replace("-", " ") + "&CourseId=" + courseDetails.CourseId,
                            false);
            }
            
        }
        private static void ContextBeginRequest(object sender, EventArgs e)
        {
            var context = ((HttpApplication) sender).Context;
            var path = context.Request.Path.ToUpperInvariant();
            var url = GetUrlWithQueryString(context).ToUpperInvariant();
            var temp = context.Request.RawUrl;
            path = path.Replace(".ASPX.CS", string.Empty);
            url = url.Replace(".ASPX.CS", string.Empty);

            // to prevent XSS
            url = HttpUtility.HtmlEncode(url);

            var applicationInstance = Applications.CurrentInstance;

            var urlContainsFileExtension =
                url.IndexOf(ApplicationConfig.FileExtension, StringComparison.OrdinalIgnoreCase) != -1;

            if (url.Contains("/State/".ToUpperInvariant()))
            {
                RewriteCollegeSearchByState(context, url);
            }
            else if (url.Contains("/College-Details/".ToUpperInvariant()))
            {
                RewriteCollegeDetails(context, url);
            }
            else if (url.Contains("/college-query/".ToUpperInvariant()))
            {
                RewriteCollegeQuery(context, url);
            }
            else if (url.Contains("/Stream-Detail/".ToUpperInvariant()))
            {
                RewriteStreamDetails(context, url);
            }
            else if (url.Contains("/University-Detail/".ToUpperInvariant()))
            {
                RewriteUniversityDetails(context, url);
            }
            else if (url.Contains("/About-Us".ToUpperInvariant()))
            {
                RewritePhysicalPageGeneric(context, url, "About.aspx");

            }
            else if (url.Contains("/Terms-and-Conditions".ToUpperInvariant()))
            {
                RewritePhysicalPageGeneric(context, url, "TermAndConditions.aspx");

            }
            else if (url.Contains("/report-donation".ToUpperInvariant()))
            {
                RewriteReportdonation(context, url);

            }
            else if (url.Contains("/Account".ToUpperInvariant()))
            {
                RewriteToProfile(context, url);

            }
            else if (url.Contains("/Privacy-Policy".ToUpperInvariant()))
            {
                RewritePhysicalPageGeneric(context, url, "PrivacyPolicy.aspx");

            }
            else if (url.Contains("/Coding".ToUpperInvariant()))
            {
                RewritePhysicalPageGeneric(context, url, "Coding.aspx");

            }
            else if (url.Contains("/Online-College-Advertisement".ToUpperInvariant()))
            {
                RewritePhysicalPageGeneric(context, url, "AdvertiseWithUs.aspx");

            }
            else if (url.Contains("/CollegeSearch".ToUpperInvariant()))
            {
                RewriteCollegeSearchByCollegeName(context, url);

            }
            else if (url.Contains("/ExamSearch".ToUpperInvariant()))
            {
                RewriteExamSearchByExamName(context, url);

            }
            else if (url.Contains("/Get-Direct-Admission".ToUpperInvariant()))
            {
               
                RewriteDirectAdmission(context, url);

            }
            else if (url.Contains("/Direct-Admission".ToUpperInvariant()))
            {
                RewriteOldDirectAdmission(context, url);

            }
            else if (url.Contains("/Direct-Admission/Online-Counselling".ToUpperInvariant()))
            {
                RewritePhysicalPageGeneric(context, url, "counselling/StudentCounselling.aspx");

            }
            else if (url.Contains("/Contact-Us".ToUpperInvariant()))
            {

                RewritePhysicalPageGeneric(context, url, "ContactUs.aspx");
            }
            else if (url.Contains("/College/".ToUpperInvariant()))
            {
                RewriteCollegeSearchByExam(context, url);
               
            }
            else if (url.Contains("/Course/".ToUpperInvariant()))
            {

                RewriteCollegeSearchCourse(context, url);
            }
            else if (url.Contains("/City/".ToUpperInvariant()))
            {
                RewriteCollegeSearchByCity(context, url);
            }

            else if (url.Contains("/CollegeSearch".ToUpperInvariant()))
            {
                string collegeName = GetQueryString(context);
                context.RewritePath(
                    Utils.ApplicationRelativeWebRoot + "College/CollegeSearch.aspx?CollegeName=" + collegeName,
                    false);
            }
            else if (url.Contains("/Latest-News".ToUpperInvariant()))
            {

                RewritePhysicalPageGeneric(context, url, "NewsAndArticles/NewsAndArticles.aspx");
            }
            else if (url.Contains("/Admission-Notices".ToUpperInvariant()))
            {
                RewritePhysicalPageGeneric(context, url, "NewsAndArticles/NoticeList.aspx");

            }
            else if (url.Contains("/Notice-Details".ToUpperInvariant()) )
            {
                RewriteNoticePage(context, url);
            }
            else if (url.Contains("/Exams".ToUpperInvariant()))
            {
                RewriteExams(context, url);
            }
            else if (url.Contains("/Exam-Details".ToUpperInvariant()))
            {
                RewriteExamPage(context, url);
            }
            else if (url.Contains("/News-Details".ToUpperInvariant()) || url.Contains("/news-deatils".ToUpperInvariant()))
            {
                RewriteNewPage(context, url);
            }

            else if (url.Contains("/Education-Loan".ToUpperInvariant()))
            {
                RewritePhysicalPageGeneric(context, url, "BankLoan/BankList.aspx");

            }
            else if (url.Contains("/Loan-Details".ToUpperInvariant()))
            {
                RewriteLoanPage(context, url);
            }
            else if (url.Contains("/Compare-Colleges".ToUpperInvariant()))
            {

                RewriteCollegeComparePage(context, url);
            }
            else if (url.Contains("/Compare-Streams".ToUpperInvariant()))
            {

                RewriteStreamComparePage(context, url);
            }
           
             else if (url.Contains("/Counselling/Instruction".ToUpperInvariant()))
            {

                RewriteCounsellingInstruction(context, url);
            }
            else if (url.Contains("/Counselling/Onlinetransaction".ToUpperInvariant()))
            {

                RewriteCounsellingPayment(context, url);
            }
           else if (url.Contains("/Counselling/OnlineApplicationForm".ToUpperInvariant()))
            {

                RewriteStudentCounselling(context, url);
            }
            
            else if (url.Contains("/Counselling/Thankyou".ToUpperInvariant()))
            {

                RewriteCounsellingThankYou(context, url);
            }
            else if (url.Contains("/bookseat".ToUpperInvariant()) && !url.Contains("/bookseat.aspx".ToUpperInvariant()))
            {
                url = context.Request.Path.ToUpperInvariant();
                RewritebookSeat(context, url);
            }
            else if (url.Contains("mba/colleges/".ToUpperInvariant()) || url.Contains("medical/colleges/".ToUpperInvariant()) || url.Contains("dental/colleges/".ToUpperInvariant()) || url.Contains("mca/colleges/".ToUpperInvariant()) || url.Contains("mds/colleges/".ToUpperInvariant()) || url.Contains("b.pharma/colleges/".ToUpperInvariant()) || url.Contains("engineering/colleges/".ToUpperInvariant())) 
            {
                MapOldURLtoNewURL(context, url);
            }


            else if (urlContainsFileExtension &&
                     url.Contains(string.Format("/DEFAULT{0}", ApplicationConfig.FileExtension.ToUpperInvariant())))
            {
                RewriteDefault(context);
            }
            else
            {
                // If this is blog instance that is in a virtual sub-folder, we will
                // need to rewrite the path for URL to a physical file.  This includes
                // requests such as the homepage (default.aspx), contact.aspx, archive.aspx,
                // any of the admin pages, etc, etc.

                if (applicationInstance.IsSubfolderOfApplicationWebRoot &&
                    VirtualPathUtility.AppendTrailingSlash(path)
                                      .IndexOf(applicationInstance.RelativeWebRoot,
                                               StringComparison.OrdinalIgnoreCase) != -1)
                {
                    bool skipRewrite = false;
                    string rewriteQs = string.Empty;
                    string rewriteUrl = GetUrlWithQueryString(context);

                    int qsStart = rewriteUrl.IndexOf("?", System.StringComparison.Ordinal);
                    if (qsStart != -1) // remove querystring.
                    {
                        rewriteQs = rewriteUrl.Substring(qsStart);
                        rewriteUrl = rewriteUrl.Substring(0, qsStart);
                    }

                    // Want to see if a specific page/file is being requested (something with a . (dot) in it).
                    // Because Utils.ApplicationRelativeWebRoot may contain a . (dot) in it, pathAfterAppWebRoot
                    // tells us if the actual path (after the AppWebRoot) contains a dot.
                    string pathAfterAppWebRoot = rewriteUrl.Substring(Utils.ApplicationRelativeWebRoot.Length);

                    if (!pathAfterAppWebRoot.Contains("."))
                    {
                        if (!rewriteUrl.EndsWith("/"))
                            rewriteUrl += "/";

                        rewriteUrl += "default.aspx";
                    }
                    else
                    {
                        var extension = Path.GetExtension(pathAfterAppWebRoot);
                        if (extension != null && extension.ToUpperInvariant() == ".AXD")
                            skipRewrite = true;
                    }

                    if (!skipRewrite)
                    {
                        // remove the subfolder portion.  so /subfolder/ becomes /.
                        rewriteUrl =
                            new Regex(Regex.Escape(applicationInstance.RelativeWebRoot), RegexOptions.IgnoreCase)
                                .Replace(rewriteUrl, Utils.ApplicationRelativeWebRoot);

                        context.RewritePath(rewriteUrl + rewriteQs, false);
                    }

                    return;
                }
               
            }
        }
        
        private static void RewriteDirectAdmission(HttpContext context, string url)
        {
            var courseName = url.Split('/')[1];
            var courseList =
                       CourseProvider.Instance.GetAllCourseList()
                                     .Where(
                                         result =>
                                        Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                           StringComparison.OrdinalIgnoreCase))
                                     .ToList().FirstOrDefault();
            if (courseList != null)
            {
                RewritePhysicalPageGeneric(context, url, "counselling/DirectAdmission.aspx?CourseId="+courseList.CourseId);
            }
        }
        private static void RewritebookSeat(HttpContext context, string url)
        {
            if (!url.Equals("/bookseat", StringComparison.OrdinalIgnoreCase))
            {

                var courseName = url.Split('/')[2];
                courseName = courseName.Replace("?", "");
                var courseList =
               CourseProvider.Instance.GetAllCourseList()
                             .Where(
                                 result =>
                                Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                   StringComparison.OrdinalIgnoreCase))
                             .ToList().FirstOrDefault();
                if (!Utils.RemoveIllegalCharacters(courseName).Equals(Utils.RemoveIllegalCharacters(ExtractTitle(context, url)), StringComparison.OrdinalIgnoreCase))
                {
                    var collegeName = url.Split('/')[3];
                    if (courseList != null)
                    {
                        var collegeList = new Common().GetCollegeNameList(courseList.CourseId);
                        var query =
                               collegeList.Tables[0].AsEnumerable().Where(colleges => Utils.RemoveIllegalCharacters(collegeName).Equals(
                                   Utils.RemoveIllegalCharacters(colleges.Field<string>("AjCollegeBranchName")),
                                   StringComparison.OrdinalIgnoreCase)).Select(colleges => new
                                   {
                                       collegeBranchCourseId =
                                   colleges.Field<int>(
                                       "AjCollegeBranchCourseId"),
                                       CollegeName = colleges.Field<string>(
                                       "AjCollegeBranchName")
                                   }).FirstOrDefault();
                        if (query != null)
                        {
                            RewritePhysicalPageGeneric(context, url, "counselling/BookYourSeat.aspx?CourseId=" + courseList.CourseId + "&collegeId=" + query.collegeBranchCourseId + "&CollegeName=" + query.CollegeName);
                        }
                    }
                }
                else
                {
                    RewritePhysicalPageGeneric(context, url, "College/ParticipatedCollegeList.aspx?CourseId=" + courseList.CourseId);
                }
            }
            else
            {
                RewritePhysicalPageGeneric(context, url, "College/ParticipatedCollegeList.aspx?CollegeName=" + GetQueryString(context));
            }
        }
        private static void RewriteOldDirectAdmission(HttpContext context, string url)
        {
            var courseId = ApplicationSettings.Instance.CourseId;

            RewritePhysicalPageGeneric(context, url, "counselling/DirectAdmission.aspx?CourseId=" + courseId);
         }

        private static void RewriteCollegeSearchCourse(HttpContext context, string url)
        {
            var CourseName = ExtractTitle(context, url);
            var CourseList = CourseProvider.Instance.GetAllCourseList().Where(result => CourseName.Equals(Utils.RemoveIllegealFromCourse(result.CourseName), StringComparison.OrdinalIgnoreCase)).ToList();
            if (CourseList.Count>0 )
            {
                var courseDetails = CourseList.First();
                context.RewritePath(Utils.ApplicationRelativeWebRoot + "College/CollegeSearch.aspx?CourseId=" + courseDetails.CourseId,
                                       false);
            }
        }
        private static void RewritePhysicalPageGeneric(HttpContext context, string url, string relativePath)
        {
            var query = GetQueryString(context);
            if (query.Length > 0 && query.StartsWith("&"))
            {
                query = "?" + query.Substring(1);
            }
            context.RewritePath(string.Format("{0}{1}{2}", Utils.ApplicationRelativeWebRoot, relativePath, query), false);
        }


        private static void RewriteCollegeQuery(HttpContext context, string url)
        {
            if (url.Contains(".AXD")) return;
            var rewriteUrl = GetUrlWithQueryString(context);
            var pathAfterAppWebRoot = rewriteUrl.Substring(Utils.ApplicationRelativeWebRoot.Length);
            var extension = Path.GetExtension(pathAfterAppWebRoot);
            if (extension == null || extension.ToUpperInvariant() == ".AXD") return;
            var i = url.LastIndexOf("/", StringComparison.Ordinal);
            var collegeName = url.Substring(i + 1, url.Length - (i + 2));
            var courseName = url.Substring(15, i - 15);
            var courseList =
                CourseProvider.Instance.GetAllCourseList()
                              .Where(
                                  result =>
                                 Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                    StringComparison.OrdinalIgnoreCase))
                              .ToList();
            var courseDetails = courseList.FirstOrDefault();
            if (courseDetails == null)
                return;
            var collegeList = new Common().GetCollegeNameList(courseDetails.CourseId);

            var query =
                from colleges in collegeList.Tables[0].AsEnumerable()
                where
                    collegeName.Equals(
                        Utils.RemoveIllegalCharacters(colleges.Field<string>("AjCollegeBranchName")),
                        StringComparison.OrdinalIgnoreCase)
                select new
                {
                    collegeBranchCourseId = colleges.Field<int>("AjCollegeBranchCourseId"),
                };
            if (query.Any())
            {
                var collegeDetails = query.First();
               
                    context.RewritePath(Utils.ApplicationRelativeWebRoot +
                                        "College/CollegeQuery.aspx?CollegeBranchCourseId=" +
                                        collegeDetails.collegeBranchCourseId);
            }
        }

        private static void RewriteCollegeDetails(HttpContext context, string url)
        {
            if (url.Contains(".AXD")) return;
            var rewriteUrl = GetUrlWithQueryString(context);
            var pathAfterAppWebRoot = rewriteUrl.Substring(Utils.ApplicationRelativeWebRoot.Length);
            var extension = Path.GetExtension(pathAfterAppWebRoot);
            if (extension == null || extension.ToUpperInvariant() == ".AXD") return;
            var i = url.LastIndexOf("/", StringComparison.Ordinal);
            var collegeName = url.Substring(i + 1, url.Length - (i + 2));
            var courseName = url.Substring(17, i - 17);
            var courseList =
                CourseProvider.Instance.GetAllCourseList()
                              .Where(
                                  result =>
                                 Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                    StringComparison.OrdinalIgnoreCase))
                              .ToList();
            var courseDetails = courseList.FirstOrDefault();
            if (courseDetails == null)
                return;
            var collegeList = new Common().GetCollegeList(courseDetails.CourseId);

            var query =
                from colleges in collegeList.Tables[0].AsEnumerable()
                where
                    collegeName.Equals(
                        Utils.RemoveIllegalCharacters(colleges.Field<string>("AjCollegeBranchName")),
                        StringComparison.OrdinalIgnoreCase)
                select colleges;
            IEnumerable<DataRow> dataRows = query as IList<DataRow> ?? query.ToList();
            var cmsStatus = false;
            var branchCourseId = 0;
            if (dataRows.Any())
            {
                foreach (var row in dataRows)
                {
                    if (row.Field<string>("AjCollegeAssociationCategoryName") == "CMS")
                    {
                        var startDate = row.Field<DateTime>("AjAdsBannerStartDate");
                        var endDate = row.Field<DateTime>("AjAdsBannerEndDate");
                        var currentDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                        cmsStatus = startDate <= currentDate && currentDate <= endDate;
                       

                    }
                    branchCourseId = row.Field<int>("AjCollegeBranchCourseId");
                }
                if (cmsStatus)
                    context.RewritePath(Utils.ApplicationRelativeWebRoot +
                                        "College/SponserCollegeDetails.aspx?CollegeBranchCourseId=" +
                                        branchCourseId);
                else
                    context.RewritePath(Utils.ApplicationRelativeWebRoot +
                                        "College/CollegeDetails.aspx?CollegeBranchCourseId=" +
                                        branchCourseId);
            }
            else
            {
                if (courseName.Equals("MBA-PGDM"))
                {
                    courseList =
                CourseProvider.Instance.GetAllCourseList()
                              .Where(
                                  result =>
                                "PGDM".Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                    StringComparison.OrdinalIgnoreCase))
                              .ToList();
                     courseDetails = courseList.FirstOrDefault();
                    if (courseDetails == null)
                        return;
                    collegeList = new Common().GetCollegeList(courseDetails.CourseId);

                     query =
                        from colleges in collegeList.Tables[0].AsEnumerable()
                        where
                            collegeName.Equals(
                                Utils.RemoveIllegalCharacters(colleges.Field<string>("AjCollegeBranchName")),
                                StringComparison.OrdinalIgnoreCase)
                        select colleges;
                    dataRows = query as IList<DataRow> ?? query.ToList();
                    branchCourseId = 0;
                   
                    if (dataRows.Any())
                    {
                        foreach (var row in dataRows)
                        {
                            if (row.Field<string>("AjCollegeAssociationCategoryName") == "CMS")
                            {
                                var startDate = row.Field<DateTime>("AjAdsBannerStartDate");
                                var endDate = row.Field<DateTime>("AjAdsBannerEndDate");
                                var currentDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                                cmsStatus = startDate <= currentDate && currentDate <= endDate;


                            } 
                            branchCourseId = row.Field<int>("AjCollegeBranchCourseId");
                        }
                        if (cmsStatus)
                            context.RewritePath(Utils.ApplicationRelativeWebRoot +
                                                "College/SponserCollegeDetails.aspx?CollegeBranchCourseId=" +
                                                branchCourseId);
                        else
                            context.RewritePath(Utils.ApplicationRelativeWebRoot +
                                                "College/CollegeDetails.aspx?CollegeBranchCourseId=" +
                                                branchCourseId);
                    }
                    else
                    {
                        courseList =
                CourseProvider.Instance.GetAllCourseList()
                              .Where(
                                  result =>
                                "MBA".Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                    StringComparison.OrdinalIgnoreCase))
                              .ToList();
                        courseDetails = courseList.FirstOrDefault();
                        if (courseDetails == null)
                            return;
                        collegeList = new Common().GetCollegeList(courseDetails.CourseId);

                        query =
                           from colleges in collegeList.Tables[0].AsEnumerable()
                           where
                               collegeName.Equals(
                                   Utils.RemoveIllegalCharacters(colleges.Field<string>("AjCollegeBranchName")),
                                   StringComparison.OrdinalIgnoreCase)
                           select colleges;
                        dataRows = query as IList<DataRow> ?? query.ToList();
                    
                        branchCourseId = 0;
                       
                        if (dataRows.Any())
                        {
                            foreach (var row in dataRows)
                            {
                                if (row.Field<string>("AjCollegeAssociationCategoryName") == "CMS")
                                {
                                    var startDate = row.Field<DateTime>("AjAdsBannerStartDate");
                                    var endDate = row.Field<DateTime>("AjAdsBannerEndDate");
                                    var currentDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                                    cmsStatus = startDate <= currentDate && currentDate <= endDate;


                                } 
                                branchCourseId = row.Field<int>("AjCollegeBranchCourseId");
                            }

                            if (cmsStatus)
                                context.RewritePath(Utils.ApplicationRelativeWebRoot +
                                                    "College/SponserCollegeDetails.aspx?CollegeBranchCourseId=" +
                                                    branchCourseId);
                            else
                                context.RewritePath(Utils.ApplicationRelativeWebRoot +
                                                    "College/CollegeDetails.aspx?CollegeBranchCourseId=" +
                                                    branchCourseId);
                        }
                    }
                   
                }
            }
        }

        private static void RewriteStreamDetails(HttpContext context, string url)
        {
             if(url.Contains(".AXD")) return;

            var urlSplit = url.Split('/');
            var courseName = urlSplit[urlSplit.Length - 2];
            var stream1 = urlSplit[urlSplit.Length - 1].Replace("?",string.Empty).Trim();
            if (stream1 == null) return;
            var courseList =
                CourseProvider.Instance.GetAllCourseList()
                              .Where(
                                  result =>
                                 Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                    StringComparison.OrdinalIgnoreCase))
                              .ToList();
            //new Common().CourseId = courseList.First().CourseId;
            var streamData1 = StreamProvider.Instance.GetStreamListByCourse(courseList.First().CourseId).Find(
                p =>
                (stream1.Equals(Utils.RemoveIllegalCharacters(p.CourseStreamName), StringComparison.OrdinalIgnoreCase)));
            if (streamData1 != null)
                context.RewritePath(Utils.ApplicationRelativeWebRoot + "Course/StreamDecsription.aspx?StreamId=" +
                                    streamData1.StreamId);
        }
        private static void RewriteUniversityDetails(HttpContext context, string url)
        {
             if(url.Contains(".AXD")) return;

            var urlSplit = url.Split('/');
            var universityName = urlSplit[urlSplit.Length - 1].Replace("?",string.Empty);
   
      var universityList = UniversityProvider.Instance.GetAllUniversityList().Find(
                p =>
                (universityName.Equals(Utils.RemoveIllegalCharacters(p.UniversityName), StringComparison.OrdinalIgnoreCase)));
      if (universityList != null)
          context.RewritePath(Utils.ApplicationRelativeWebRoot + "Course/UniversityDescription.aspx?UniversityId=" +
                                    universityList.UniversityId);
        }

        private static List<CourseMasterProperty> Course(string course)
        {
          var objCorseList=new List<CourseMasterProperty>();
          var objCourse = new CourseMasterProperty();
            var courseList =
               CourseProvider.Instance.GetAllCourseList()
                             .Where(
                                 result =>
                                 course.Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                   StringComparison.OrdinalIgnoreCase))
                             .ToList();
            var courseName = courseList.First().CourseName;
            objCourse.CourseId = courseList.First().CourseId; objCourse.CourseName = courseList.First().CourseName;
            objCorseList.Add(objCourse);
            return objCorseList.ToList();

        }
       
        private static void RewriteCounsellingInstruction(HttpContext context, string url)
        {

            var courseName = url.Split('/')[1];
            var courseList = CourseProvider.Instance.GetAllCourseList().Where(result => Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName), StringComparison.OrdinalIgnoreCase)).ToList();
            if (courseList.Count > 0)
            {
                var courseDetails = courseList.First();
                context.RewritePath(Utils.ApplicationRelativeWebRoot + "counselling/OnlineApplicationInstrucation.aspx?CourseId=" + courseDetails.CourseId,
                                       false);
            }
        }
        private static void RewriteCounsellingPayment(HttpContext context, string url)
        {

            var courseName =  url.Split('/')[1];
            var courseList = CourseProvider.Instance.GetAllCourseList().Where(result =>Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName), StringComparison.OrdinalIgnoreCase)).ToList();
            if (courseList.Count > 0)
            {
                var courseDetails = courseList.First();
                context.RewritePath(Utils.ApplicationRelativeWebRoot + "counselling/PaymentOptions.aspx?CourseId=" + courseDetails.CourseId,
                                       false);
            }
        }
        private static void RewriteStudentCounselling(HttpContext context, string url)
        {

            var courseName = url.Split('/')[1];
            var courseList = CourseProvider.Instance.GetAllCourseList().Where(result =>Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName), StringComparison.OrdinalIgnoreCase)).ToList();
            if (courseList.Count > 0)
            {
                var courseDetails = courseList.First();
                context.RewritePath(Utils.ApplicationRelativeWebRoot + "counselling/StudentCounselling.aspx?CourseId=" + courseDetails.CourseId,
                                       false);
            }
        }
        private static void RewriteCounsellingThankYou(HttpContext context, string url)
        {

            var courseName = url.Split('/')[1];
            var courseList = CourseProvider.Instance.GetAllCourseList().Where(result => Utils.RemoveIllegealFromCourse(courseName).Equals(Utils.RemoveIllegealFromCourse(result.CourseName), StringComparison.OrdinalIgnoreCase)).ToList();
            if (courseList.Count > 0)
            {
                var courseDetails = courseList.First();
                context.RewritePath(Utils.ApplicationRelativeWebRoot + "counselling/Confirmation.aspx?CourseId=" + courseDetails.CourseId,
                                       false);
            }
        }


        private static void MapOldURLtoNewURL(HttpContext context, string url)
        {
            url=url.ToLower();
            
            if(url.Contains("mba/colleges/"))
                RoutetoNewUrl(url, 2, context);
            else if(url.Contains("medical/colleges/"))
                RoutetoNewUrl(url, 3, context);
            else if(url.Contains("dental/colleges/"))
                RoutetoNewUrl(url, 4, context);
            else if(url.Contains("mca/colleges/"))
                     RoutetoNewUrl(url, 7, context);
            else if(url.Contains("mds/colleges/"))
                    RoutetoNewUrl(url, 8, context);
            else if(url.Contains("b.pharma/colleges/"))
                      RoutetoNewUrl(url, 9, context);
            else if(url.Contains("engineering/colleges/"))
                     RoutetoNewUrl(url, 1, context);
             
            
        }
        private static void RoutetoNewUrl(string collegeName, int courseId, HttpContext context)
        {
            int d = collegeName.LastIndexOf("/", StringComparison.Ordinal);
            collegeName = collegeName.Substring(d, collegeName.Length - d);
            collegeName = Utils.RemoveIllegalCharacters(collegeName);
            collegeName = collegeName.Replace("-", " ").Trim();
            collegeName = collegeName.Replace("=", "-").Trim();
            var collegeList = new Common().GetCollegeList(courseId);

            var query =
                from colleges in collegeList.Tables[0].AsEnumerable()
                where
                    collegeName.Equals(
                        Utils.RemoveIllegalCharacters(colleges.Field<string>("AjCollegeBranchName")),
                        StringComparison.OrdinalIgnoreCase)
                select colleges;
            IEnumerable<DataRow> dataRows = query as IList<DataRow> ?? query.ToList();
            var branchCourseId = 0;
        

            if (dataRows.Any())
            {
                foreach (var row in dataRows)
                {
                   
                        branchCourseId = row.Field<int>("AjCollegeBranchCourseId");

                  

                }
              
                    context.RewritePath(Utils.ApplicationRelativeWebRoot +
                                        "College/CollegeDetails.aspx?CollegeBranchCourseId=" +
                                        branchCourseId);
            }
            else
            {
                var city =
                    CityProvider.Instacnce.GetAllCityList()
                                .FirstOrDefault(
                                    p =>
                                    (Utils.RemoveIllegalCharacters(collegeName)
                                          .Equals(Utils.RemoveIllegalCharacters(p.CityName),
                                                  StringComparison.OrdinalIgnoreCase)));

                if (city == null)
                       return;
                context.RewritePath(
                    Utils.ApplicationRelativeWebRoot + "College/CollegeSearch.aspx?CityId=" + city.CityId + "&CourseId=" +
                    courseId,
                    false);
            }
        }
        #endregion
    }
}