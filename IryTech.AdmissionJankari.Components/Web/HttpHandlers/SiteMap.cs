using System;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Xml;
using IryTech.AdmissionJankari.BL;
using System.Linq;

namespace IryTech.AdmissionJankari.Components.Web.HttpHandlers
{
    /// <summary>
    /// A application  sitemap suitable for Google Sitemap as well as
    ///     other big search engines such as MSN/Live, Yahoo and Ask.
    /// </summary>
    public class SiteMap : IHttpHandler
    {
        #region Properties

        /// <summary>
        ///     Gets a value indicating whether another request can use the <see cref = "T:System.Web.IHttpHandler"></see> instance.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref = "T:System.Web.IHttpHandler"></see> instance is reusable; otherwise, false.</returns>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Implemented Interfaces

        #region IHttpHandler

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that 
        ///     implements the <see cref="T:System.Web.IHttpHandler"></see> interface.
        /// </summary>
        /// <param name="context">
        /// An <see cref="T:System.Web.HttpContext"></see> 
        ///     object that provides references to the intrinsic server objects 
        ///     (for example, Request, Response, Session, and Server) used to service HTTP requests.
        /// </param>
        public void ProcessRequest(HttpContext context)
        {
            using (var writer = XmlWriter.Create(context.Response.OutputStream))
            {
                writer.WriteStartElement("urlset", "http://www.google.com/schemas/sitemap/0.84");

                //// Courses
                foreach (var courseList in CourseProvider.Instance.GetAllCourseList().Where(course => course.CourseStatus == true).ToList())
                {
                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", string.Format("{0}{1}", Utils.AbsoluteWebRoot,"Course/"+ Utils.RemoveIllegealFromCourse(courseList.CourseName)));
                    writer.WriteElementString("lastmod", "2013-01-01");
                    writer.WriteElementString("changefreq", "monthly");
                    writer.WriteEndElement();
                }
                // College Search According to state
                foreach (var courseList in CourseProvider.Instance.GetAllCourseList().Where(course => course.CourseStatus == true).ToList())
                {
                    foreach (var cityList in StateProvider.Instance.GetAllState())
                    {
                        writer.WriteStartElement("url");
                        writer.WriteElementString("loc", string.Format("{0}{1}", Utils.AbsoluteWebRoot, Utils.RemoveIllegealFromCourse(courseList.CourseName) + "/State/" + Utils.RemoveIllegalCharacters(cityList.StateName)));
                        writer.WriteElementString("lastmod", "2013-01-01");
                        writer.WriteElementString("changefreq", "monthly");
                        writer.WriteEndElement();
                    }
                }
                // College Search According to City
                foreach (var courseList in CourseProvider.Instance.GetAllCourseList().Where(course => course.CourseStatus == true).ToList())
                {
                    foreach (var cityList in CityProvider.Instacnce.GetAllCityList())
                    {
                        writer.WriteStartElement("url");
                        writer.WriteElementString("loc", string.Format("{0}{1}", Utils.AbsoluteWebRoot, Utils.RemoveIllegealFromCourse(courseList.CourseName) + "/City/" + Utils.RemoveIllegalCharacters(cityList.CityName)));
                        writer.WriteElementString("lastmod", "2013-01-01");
                        writer.WriteElementString("changefreq", "monthly");
                        writer.WriteEndElement();
                    }
                }
                // College Search According to Exam
               
                    foreach (var examList in ExamProvider.Instance.GetAllExamList().Where(examList => examList != null))
                    {
                        writer.WriteStartElement("url");
                        writer.WriteElementString("loc",
                            string.Format("{0}{1}", Utils.AbsoluteWebRoot,
                                Utils.RemoveIllegealFromCourse(examList.CourseName) + "/college/" +
                                Utils.RemoveIllegalCharacters(examList.ExamName)));
                        writer.WriteElementString("lastmod", "2013-01-01");
                        writer.WriteElementString("changefreq", "monthly");
                        writer.WriteEndElement();
                    }
               
                //Collges

                foreach (var college in CollegeProvider.Instance.GetCollegeList().Where(resut=>resut.CollegeBranchStatus==true).ToList())
                {
                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", string.Format("{0}{1}", Utils.AbsoluteWebRoot, "College-Details/" + Utils.RemoveIllegealFromCourse(college.CourseName) + "/" + Utils.RemoveIllegalCharacters(college.CollegeBranchName)));
                    writer.WriteElementString("lastmod", "2013-01-01");
                    writer.WriteElementString("changefreq", "monthly");
                    writer.WriteEndElement();
                }
                // Exam

                foreach (var courseList in CourseProvider.Instance.GetAllCourseList().Where(course => course.CourseStatus == true).ToList())
                {
                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", string.Format("{0}{1}", Utils.AbsoluteWebRoot, "Exams/" + Utils.RemoveIllegealFromCourse(courseList.CourseName)));
                    writer.WriteElementString("lastmod", "2013-01-01");
                    writer.WriteElementString("changefreq", "monthly");
                    writer.WriteEndElement();
                }
                  
                // Exam Details
                  foreach (var exam in ExamProvider.Instance.GetAllExamList().Where(resut => resut.ExamStatus == true).ToList())
                  {
                      writer.WriteStartElement("url");
                      writer.WriteElementString("loc", string.Format("{0}{1}", Utils.AbsoluteWebRoot, Utils.RemoveIllegealFromCourse(exam.CourseName) + "/Exam-Details/Year/" + System.DateTime.Now.Year.ToString() + "-" + (System.DateTime.Now.Year + 1).ToString() + "/" + Utils.RemoveIllegalCharacters(exam.ExamName)));
                      writer.WriteElementString("lastmod", "2013-01-01");
                      writer.WriteElementString("changefreq", "monthly");
                      writer.WriteEndElement();
                  }

            // Course Compaire
                  foreach (var courseList in CourseProvider.Instance.GetAllCourseList().Where(course => course.CourseStatus == true).ToList())
                  {
                      writer.WriteStartElement("url");
                      writer.WriteElementString("loc", string.Format("{0}{1}", Utils.AbsoluteWebRoot, Utils.RemoveIllegealFromCourse(courseList.CourseName) + "/Compare-Streams"));
                      writer.WriteElementString("lastmod", "2013-01-01");
                      writer.WriteElementString("changefreq", "monthly");
                      writer.WriteEndElement();
                  }

            // College Compaire 
                  foreach (var courseList in CourseProvider.Instance.GetAllCourseList().Where(course => course.CourseStatus == true).ToList())
                  {
                      writer.WriteStartElement("url");
                      writer.WriteElementString("loc", string.Format("{0}{1}", Utils.AbsoluteWebRoot, Utils.RemoveIllegealFromCourse(courseList.CourseName) + "/Compare-Colleges/"));
                      writer.WriteElementString("lastmod", "2013-01-01");
                      writer.WriteElementString("changefreq", "monthly");
                      writer.WriteEndElement();
                  }
           // Direct Admission Page
                  foreach (var courseList in CourseProvider.Instance.GetAllCourseList().Where(course => course.CourseStatus == true).ToList())
                  {
                      writer.WriteStartElement("url");
                      writer.WriteElementString("loc", string.Format("{0}{1}", Utils.AbsoluteWebRoot, Utils.RemoveIllegealFromCourse(courseList.CourseName) + "/Get-Direct-Admission"));
                      writer.WriteElementString("lastmod", "2013-01-01");
                      writer.WriteElementString("changefreq", "monthly");
                      writer.WriteEndElement();
                  }

            // Consulling Page 
                  foreach (var courseList in CourseProvider.Instance.GetAllCourseList().Where(course => course.CourseStatus == true).ToList())
                  {
                      writer.WriteStartElement("url");
                      writer.WriteElementString("loc", string.Format("{0}{1}", Utils.AbsoluteWebRoot, Utils.RemoveIllegealFromCourse(courseList.CourseName) + "/Direct-Admission/Online-Counselling"));
                      writer.WriteElementString("lastmod", "2013-01-01");
                      writer.WriteElementString("changefreq", "monthly");
                      writer.WriteEndElement();
                  }

            // News And Atricle Page
                  foreach (var newsList in NewsArticleNoticeProvider.Instance.GetAllNewsArticleList().Where(news => news.NewsStatus== true).ToList())
                  {
                      writer.WriteStartElement("url");
                      writer.WriteElementString("loc", string.Format("{0}{1}", Utils.AbsoluteWebRoot, "News-Details/" + Utils.RemoveIllegalCharacters(newsList.NewsSubject)));
                      writer.WriteElementString("lastmod", "2013-01-01");
                      writer.WriteElementString("changefreq", "monthly");
                      writer.WriteEndElement();
                  }
            // Notice Details
                  foreach (var noticeList in NewsArticleNoticeProvider.Instance.GetAllNoticeList().Where(notice => notice.NoticeStatus == true).ToList())
                  {
                      writer.WriteStartElement("url");
                      writer.WriteElementString("loc", string.Format("{0}{1}", Utils.AbsoluteWebRoot, "News-Deatils/" + Utils.RemoveIllegalCharacters(noticeList.NoticeSubject)));
                      writer.WriteElementString("lastmod", "2013-01-01");
                      writer.WriteElementString("changefreq", "monthly");
                      writer.WriteEndElement();
                  }
          // bank List 
                  foreach (var bankList in BankProvider.Instance.GetAllBankList())
                  {
                      writer.WriteStartElement("url");
                      writer.WriteElementString("loc", string.Format("{0}{1}", Utils.AbsoluteWebRoot, "Loan-Details/" + Utils.RemoveIllegalCharacters(bankList.BankName)));
                      writer.WriteElementString("lastmod", "2013-01-01");
                      writer.WriteElementString("changefreq", "monthly");
                      writer.WriteEndElement();
                  }
       /**********************************************************************Static Pages********************************************************************/
                //// Content Writer 
                 writer.WriteStartElement("url");
                 writer.WriteElementString("loc", Utils.AbsoluteWebRoot.ToString() + "Jobs-in-Delhi/Content-Writer");
                 writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                 writer.WriteElementString("changefreq", "daily");
                 writer.WriteEndElement();

                // Notice List
                 writer.WriteStartElement("url");
                 writer.WriteElementString("loc", Utils.AbsoluteWebRoot.ToString() + "Admission-Notices");
                 writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                 writer.WriteElementString("changefreq", "daily");
                 writer.WriteEndElement();

                // News List
                 writer.WriteStartElement("url");
                 writer.WriteElementString("loc", Utils.AbsoluteWebRoot.ToString() + "Latest-News/");
                 writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                 writer.WriteElementString("changefreq", "daily");
                 writer.WriteEndElement();

                // Bank List
                 writer.WriteStartElement("url");
                 writer.WriteElementString("loc", Utils.AbsoluteWebRoot.ToString() + "Education-Loan/");
                 writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                 writer.WriteElementString("changefreq", "daily");
                 writer.WriteEndElement();



                 // Counselor
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", Utils.AbsoluteWebRoot.ToString() + "Jobs-in-Delhi/Counselor");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "daily");
                writer.WriteEndElement();

                // codeing
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", Utils.AbsoluteWebRoot.ToString() + "Jobs-in-Delhi/Programming");
                writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", "daily");
                writer.WriteEndElement();

                // About Us
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", Utils.AbsoluteWebRoot.ToString() + "About-Us");
                writer.WriteElementString("lastmod", "2013-01-01");
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();


                // Privacy Policy
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", Utils.AbsoluteWebRoot.ToString() + "Privacy-Policy");
                writer.WriteElementString("lastmod", "2013-01-01");
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();

                // Terms & Condition
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", Utils.AbsoluteWebRoot.ToString() + "Terms-and-Conditions");
                writer.WriteElementString("lastmod", "2013-01-01");
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();

                // Contact US
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", Utils.AbsoluteWebRoot.ToString() + "Contact-Us");
                writer.WriteElementString("lastmod", "2013-01-01");
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();

                // Login 
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", Utils.AbsoluteWebRoot.ToString() + "Account/Login");
                writer.WriteElementString("lastmod", "2013-01-01");
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();

                // Registation
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", Utils.AbsoluteWebRoot.ToString() + "Account/New-User-Registration-Form");
                writer.WriteElementString("lastmod", "2013-01-01");
                writer.WriteElementString("changefreq", "monthly");
                writer.WriteEndElement();
                

                writer.WriteEndElement();
            }

            context.Response.ContentType = "text/xml";
        }

        #endregion

        #endregion
    }
}