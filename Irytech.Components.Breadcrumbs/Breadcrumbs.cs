using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using System.Collections.Generic;


namespace Irytech.Components.Breadcrumbs
{
    namespace Bread
    {
        public class BreadCrumbControl : System.Web.UI.WebControls.WebControl
        {
            readonly StringBuilder _sbResult = new StringBuilder();				// Holds the Breadcrumb HTML.
            readonly StringBuilder _sbBcUrl = new StringBuilder();				// Holds the URL of the breadcrumb.  Directories are appended in succession to the root.
            private readonly HybridDictionary _labels = new HybridDictionary();	// Holds the "friendly" directory names.


            /*
             *	Constructor
             *	Hook up the Control_Load event handler.
             */
            public BreadCrumbControl()
            {
                Load += new EventHandler(Control_Load);

                //
                // Initialize properties to default values.
                //
                ShowFileName = false;
                Separator = ">";
                RootUrl = "/";
                RootName = "Home";

                //
                // Give the directories "friendly" names.  Given the dynamic nature of directory structures, list them 
                // alphabetically by key.
                //
                _labels.Add("subdir", "Sub Directory");
                _labels.Add("subsubdir", "Sub Sub Directory");
                _labels.Add("subsubsubdir", "Sub Sub sub  Directory");
            }


            // PROPERTIES

            // ShowFileName.  Set to true if you want to append an extra separator character and
            // the current file's name; false if not.  Default is false.  
            public bool ShowFileName { get; set; }

            // Separator.  Contains the character(s) that separate each directory and/or file name
            // in the breadcrumb HTML.  Default is ">".
            public string Separator { get; set; }

            // RootUrl.  URL of the root directory.  Default is "/".
            public string RootUrl { get; set; }

            // RootName.  "Friendly" name of the root directory.  Default is "Home".
            public string RootName { get; set; }


            // EVENT HANDLERS

            protected override void Render(HtmlTextWriter output)
            {
                output.Write(_sbResult + "<br/>");
            }

            /*
             *	EventHandler: Control_Load
             *	This event handler contains the meat of the control's functionality.
             */

            protected void Control_Load(Object sender, EventArgs e)
            {
                var urlList = new List<Url>();
                //
                // Create the root breadcrumb (corresponds to the root directory).
                //

                _sbResult.Append("<a href=\"" + RootUrl + "\" title='" + RootName + "'>" + RootName + "</a>");
                //
                // Get the site URL.  Use a StringBuilder to hold the URL so that we can append 
                // directory names in succession.
                //
                var strHostUrl = "http://" + Page.Request.ServerVariables["HTTP_HOST"] + "/";
                _sbBcUrl.Append(strHostUrl);

                //
                // Break up the path parts into an array (directory name(s) and/or file name).
                //
                var scriptName = Page.Request.RawUrl;
                var strScriptDir = Path.GetDirectoryName(scriptName);
               if (strScriptDir != null)
                {
                    if (scriptName.Contains("/City/".ToLower()))
                    {
                        urlList = RewriteCollegeSearchByCity(scriptName);
                    }
                    else if (scriptName.Contains("/College-Details/".ToLower()))
                    {
                        urlList = RewriteCollegeDetails(scriptName);
                    }
                    else if (scriptName.Contains("/Stream-Detail/".ToLower()))
                    {
                        urlList = RewriteStreamDetails(scriptName);
                    }
                    else if (scriptName.Contains("/University-Detail".ToLower()))
                    {
                        urlList = RewriteUniversityDetails(scriptName);
                    }
                    else if (scriptName.Contains("/Course".ToLower()))
                    {
                        urlList = RewriteCollegeSearchByCourse(scriptName);
                    }
                    else if (scriptName.Contains("/Admission-Notices".ToLower()))
                    {
                        urlList = RewriteForNotice(scriptName);
                    }
                    else if (scriptName.Contains("/Notice-Details".ToLower()))
                    {
                        urlList = RewriteForNoticeDetails(scriptName);
                    }
                    else if (scriptName.Contains("/Latest-News".ToLower()))
                    {
                        urlList = RewriteForNewsList(scriptName);
                    }
                    else if (scriptName.Contains("/News-Details".ToLower()))
                    {
                        urlList = RewriteForNewsDetails(scriptName);
                    }
                    else if (scriptName.Contains("/Education-Loan".ToLower()))
                    {
                        urlList = RewriteForBankList(scriptName);
                    }
                    else if (scriptName.Contains("/Loan-Details".ToLower()))
                    {
                        urlList = RewriteForLoanDetails(scriptName);
                    }
                    else if (scriptName.Contains("/Get-Direct-Admission".ToLower()))
                    {
                        urlList = RewriteForAdmission(scriptName);
                    }
                    else if (scriptName.Contains("/Online-Counselling".ToLower()))
                    {
                        urlList = RewriteForAdmissionCounselling(scriptName);
                    }
                    else if (scriptName.Contains("/Account".ToLower()))
                    {
                        urlList = RewriteForAccount(scriptName);
                    }
                    else if (scriptName.Contains("/Exams".ToLower()))
                    {
                        urlList = RewriteForExamList(scriptName);
                    }
                    else if (scriptName.Contains("/Exam-Details".ToLower()))
                    {
                        urlList = RewriteForExamDetails(scriptName);
                    }
                    else if (scriptName.Contains("/Compare-Colleges".ToLower()))
                    {

                        urlList = RewriteForCollegeCompare(scriptName);
                    }
                    else if (scriptName.Contains("/college/".ToLower()))
                    {

                        urlList = RewriteForCollegeExamList(scriptName);
                    }

                    else if (scriptName.Contains("/Compare-Streams".ToLower()))
                    {
                        urlList = RewriteForStreamCompare(scriptName);
                    }
                    else if (scriptName.Contains("/CollegeSearch".ToLower()))
                    {
                        urlList = RewriteForCollegeSearchByName(scriptName);
                    }
                    else if (scriptName.Contains("/ExamSearch".ToLower()))
                    {
                        urlList = RewriteForExamSearchByName(scriptName);
                    }

                  
                    else if (scriptName.Contains("/counselling/instruction".ToLower()))
                    {
                        urlList = RewriteForCollegeCounsellingInstruction(scriptName);
                    }
                    else if (scriptName.Contains("/counselling/onlineapplicationform".ToLower()))
                    {
                        urlList = RewriteForCollegeCounsellingStudentCounselling(scriptName);
                    }
                    else if (scriptName.Contains("/counselling/OnlineTransaction".ToLower()))
                    {
                        urlList = RewriteForCollegeCounsellingStudentPayment(scriptName);
                    }
                    else if (scriptName.Contains("/counselling/thankyou".ToLower()))
                    {
                        urlList = RewriteForCollegeCounsellingThankYou(scriptName);
                    }
                    else if (!scriptName.Contains("Default.aspx".ToLower()))
                    {
                        urlList = RewriteForStaticPages(scriptName);
                    }


                    if (urlList.Count > 0)
                    {
                        foreach (var strDirName in urlList)
                        {
                            _sbResult.Append("<a href=\"" + Utils.AbsoluteWebRoot + strDirName.urlRel + "\">" +
                                             strDirName.Title + "</a>");
                            _sbBcUrl.Append(strDirName + "/");
                        }
                    }


                    if (ShowFileName)
                    {
                        _sbResult.Append(String.Format(" {0} {1}", Separator, Path.GetFileName(scriptName)));
                    }

                }
            }

            private List<Url> RewriteCollegeSearchByCity(string url)
            {
                var itemList=new List<Url>();
                var objUrl2 = new Url();
                var objUrl3 = new Url();
                var cityName = ExtractTitle(url);
                var i = url.LastIndexOf("/city/", StringComparison.Ordinal);
                if (i > 0)
                {
                    var course="";
                    var courseName = url.Substring(1, i - 1);
                    var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                      .Where(
                                          result =>
                                          courseName.Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                            StringComparison.OrdinalIgnoreCase))
                                      .ToList().FirstOrDefault();
                    if (courseDetails != null) course = courseDetails.CourseName;
                    objUrl2.Title = course;
                    objUrl2.urlRel = "course/" + Utils.RemoveIllegealFromCourse(courseName);
                     itemList.Add(objUrl2);
                     objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase(cityName));
                     objUrl3.urlRel = Utils.RemoveIllegealFromCourse(courseName) + "/city/" + Common.GetStringProperCase(Utils.RemoveIllegalCharacters(cityName));
                      itemList.Add(objUrl3);
                   
                     
                }
                return itemList;
            }
            private List<Url> RewriteCollegeDetails(string url)
            {
                var course = "";
                var itemList = new List<Url>();
                var objUrl2 = new Url();
                var objUrl3 = new Url();
                var i = url.LastIndexOf("/", StringComparison.Ordinal);
                var collegeName = url.Substring(i + 1, url.Length - (i + 1));
                var courseName = url.Substring(17, i - 17);
                var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                     .Where(
                                         result =>
                                         courseName.Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                           StringComparison.OrdinalIgnoreCase))
                                     .ToList().FirstOrDefault();
                if (courseDetails != null) course = courseDetails.CourseName;
                objUrl2.Title = course;
                objUrl2.urlRel = "course/" + Utils.RemoveIllegealFromCourse(courseName);
                itemList.Add(objUrl2);
                objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase(collegeName));
                objUrl3.urlRel = "college-details/" + Utils.RemoveIllegealFromCourse(courseName) + "/" + Utils.RemoveIllegalCharacters(collegeName);
                itemList.Add(objUrl3);
                return itemList;

            }

            private List<Url> RewriteStreamDetails(string url)
            {
                var itemList = new List<Url>();
                var objUrl2 = new Url();
                var urlSplit = url.Split('/');
                var courseName = urlSplit[urlSplit.Length - 2];
                var stream1 = urlSplit[urlSplit.Length - 1].Replace("?", string.Empty).Trim();
                objUrl2.Title = Utils.RemoveHyphen(Common.GetStringProperCase(stream1));
                objUrl2.urlRel = "stream-detail/" + Utils.RemoveIllegealFromCourse(courseName) + "/" + Utils.RemoveIllegalCharacters(stream1);
                itemList.Add(objUrl2);
                return itemList;
            }


            private List<Url> RewriteUniversityDetails(string url)
            {
                var itemList = new List<Url>();
                var objUrl2 = new Url();
                var urlSplit = url.Split('/');
                objUrl2.Title = Utils.RemoveHyphen(Common.GetStringProperCase("university detail"));
                objUrl2.urlRel = "university-detail/" + urlSplit[urlSplit.Length-1];
                itemList.Add(objUrl2);
                return itemList;
            }
            private static string ExtractTitle(string url)
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

                   
                }
                return url;
            }
            private List<Url> RewriteCollegeSearchByCourse(string url)
            {
                var course="";
                var itemList = new List<Url>();
                var objUrl2 = new Url();
                    var courseName = url.Trim().Split('/');
                    var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                       .Where(
                                           result =>
                                           courseName[2].Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                             StringComparison.OrdinalIgnoreCase))
                                       .ToList().FirstOrDefault();
                    if (courseDetails != null) course = courseDetails.CourseName;
                    objUrl2.Title = course;
                    objUrl2.urlRel = "course/" + Utils.RemoveIllegealFromCourse(courseName[2]);
                    itemList.Add(objUrl2);
                
                return itemList;
            }

            private List<Url> RewriteForNotice(string url)
            {
                var itemList = new List<Url>();
                var objUrl2 = new Url(); var objUrl3 = new Url();
                
                    objUrl2.Title = Utils.RemoveHyphen(Common.GetStringProperCase(url.Split('/')[1]));
                    objUrl2.urlRel = "admission-notices/";
                    itemList.Add(objUrl2);
                return itemList;
            }

            private List<Url> RewriteForNoticeDetails(string url)
            {
                var itemList = new List<Url>();
                var objUrl2 = new Url(); var objUrl3 = new Url();

                objUrl2.Title = Utils.RemoveHyphen(Common.GetStringProperCase("admission-notices")); 
                    objUrl2.urlRel = "admission-notices/";
                    itemList.Add(objUrl2);
                    var noticeTitle = ExtractTitle(url);
                    objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase(noticeTitle));
                    objUrl3.urlRel = "notice-details/" + Utils.RemoveIllegalCharacters(noticeTitle);
                    itemList.Add(objUrl3);
               
                return itemList;
            }
            private List<Url> RewriteForNewsList(string url)
            {
                var itemList = new List<Url>();
                var objUrl2 = new Url {Title = Utils.RemoveHyphen(Common.GetStringProperCase(url.Split('/')[1])), urlRel = "latest-news/"};

                itemList.Add(objUrl2);


                return itemList;
            }

            private List<Url> RewriteForNewsDetails(string url)
            {
                var itemList = new List<Url>();
                var objUrl2 = new Url(); var objUrl3 = new Url();

                objUrl2.Title = Utils.RemoveHyphen(Common.GetStringProperCase("Latest-News"));
                objUrl2.urlRel = "latest-news/";
                itemList.Add(objUrl2);
                var newsTitle = ExtractTitle(url);
                objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase(newsTitle));
                objUrl3.urlRel = "news-details/" + Utils.RemoveIllegalCharacters(newsTitle);
                itemList.Add(objUrl3);

                return itemList;
            }


            private List<Url> RewriteForBankList(string url)
            {
                var itemList = new List<Url>();
          
                var objUrl2 = new Url
                                  {
                                      Title = Utils.RemoveHyphen(Common.GetStringProperCase(url.Split('/')[1])),
                                      urlRel = "Education-Loan".ToLower()
                                  };

                itemList.Add(objUrl2);
                return itemList;
            }
            private List<Url> RewriteForLoanDetails(string url)
            {
                var itemList = new List<Url>();
                var objUrl2 = new Url(); var objUrl3 = new Url();
                objUrl2.Title = Utils.RemoveHyphen(Common.GetStringProperCase(url.Split('/')[1]));
                objUrl2.urlRel = "education-Loan/";
                itemList.Add(objUrl2);
                var loan = ExtractTitle(url);
                objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase(loan));
                objUrl3.urlRel = "loan-details/" + Utils.RemoveIllegalCharacters(loan);
                itemList.Add(objUrl3);
                return itemList;
            }
            private List<Url> RewriteForAdmission(string url)
            {
                var itemList = new List<Url>();
                var objUrl2 = new Url();
                var objUrl3 = new Url();
                var course = "";
                var courseName = url.Split('/');
                var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                      .Where(
                                          result =>
                                          courseName[1].Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                            StringComparison.OrdinalIgnoreCase))
                                      .ToList().FirstOrDefault();
                if (courseDetails != null) course = courseDetails.CourseName;
                objUrl2.Title = course;
                objUrl2.urlRel = "course/" + Utils.RemoveIllegealFromCourse(courseName[1]);
                    itemList.Add(objUrl2);
                    objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase(courseName[2]));
                    objUrl3.urlRel = Utils.RemoveIllegealFromCourse(courseName[1]) + "/get-direct-admission";
                    itemList.Add(objUrl3);

                return itemList;
            }
            private List<Url> RewriteForAdmissionCounselling(string url)
            {
                var itemList = new List<Url>();
                var objUrl2 = new Url();
                var objUrl3 = new Url();
                var objUrl4 = new Url();
                var courseName = url.Split('/');
                var course = "";
                var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                     .Where(
                                         result =>
                                         courseName[1].Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                           StringComparison.OrdinalIgnoreCase))
                                     .ToList().FirstOrDefault();
                if (courseDetails != null) course = courseDetails.CourseName;
                objUrl2.Title = course;
                objUrl2.urlRel = "course/" + Utils.RemoveIllegealFromCourse(courseName[1]);
                    itemList.Add(objUrl2);
                    objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase("get-direct-admission"));
                    objUrl3.urlRel = Utils.RemoveIllegealFromCourse(courseName[1]) + "/get-direct-admission";
                    itemList.Add(objUrl3);
                    objUrl4.Title = Utils.RemoveHyphen(Common.GetStringProperCase(courseName[3]));
                    objUrl4.urlRel = Utils.RemoveIllegealFromCourse(courseName[1]) + "/get-direct-admission/online-counselling";
                    itemList.Add(objUrl4);

                return itemList;
            }
            private List<Url> RewriteForAccount(string url)
            {
                var itemList = new List<Url>();
                var objUrl2 = new Url();
             var accountName = url.Split('/');
             switch (accountName[accountName.Length - 1])
             {
                 case "login":
                     {
                         objUrl2.Title = Utils.RemoveHyphen(Common.GetStringProperCase(accountName[accountName.Length-1]));
                         objUrl2.urlRel = "account/" + Utils.RemoveIllegalCharacters(accountName[accountName.Length - 1]);
                         itemList.Add(objUrl2);
                         break;
                     }
                 case "new-user-registration-form":
                     {
                         objUrl2.Title = Utils.RemoveHyphen(Common.GetStringProperCase(accountName[accountName.Length - 1]));
                         objUrl2.urlRel = "account/" + Utils.RemoveIllegalCharacters(accountName[accountName.Length - 1]);
                         itemList.Add(objUrl2);
                         break;
                     }
                 case "college-profile":
                     {
                         objUrl2.Title = Utils.RemoveHyphen(Common.GetStringProperCase(accountName[accountName.Length - 1]));
                         objUrl2.urlRel = "account/" + Utils.RemoveIllegalCharacters(accountName[accountName.Length - 1]);
                         itemList.Add(objUrl2);
                         break;
                     }
                 case "college-login":
                     {
                         objUrl2.Title = Utils.RemoveHyphen(Common.GetStringProperCase(accountName[accountName.Length - 1]));
                         objUrl2.urlRel = "account/" + Utils.RemoveIllegalCharacters(accountName[accountName.Length - 1]);
                         itemList.Add(objUrl2);
                         break;
                     }
                        
                }
                return itemList;
            }
            private List<Url> RewriteForStaticPages(string url)
            {
                var itemList = new List<Url>();

                var objUrl2 = new Url
                {
                    Title = Utils.RemoveHyphen(Common.GetStringProperCase(url.Split('/')[1])),
                    urlRel = url.Split('/')[1]
                };

                itemList.Add(objUrl2);
                return itemList;
            }
            private List<Url> RewriteForExamList(string url)
            {
                var itemList = new List<Url>();
                var urlSplit = url.Split('/');
                var course = "";
                var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                   .Where(
                                       result =>
                                       urlSplit[urlSplit.Length - 1].Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                         StringComparison.OrdinalIgnoreCase))
                                   .ToList().FirstOrDefault();
                if (courseDetails != null) course = courseDetails.CourseName;
                var objUrl2 = new Url
                                  {
                                      Title = course,
                                      urlRel = "exams/" + Utils.RemoveIllegealFromCourse(urlSplit[urlSplit.Length - 1])
                                  };

                itemList.Add(objUrl2);

                return itemList;
            }
            private List<Url> RewriteForExamDetails(string url)
            {
                var itemList = new List<Url>();
                var objUrl2 = new Url();
                var objUrl3 = new Url();
                var objUrl4 = new Url();
                var courseName = url.Split('/');
                var course = "";
                var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                     .Where(
                                         result =>
                                         courseName[1].Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                           StringComparison.OrdinalIgnoreCase))
                                     .ToList().FirstOrDefault();
                if (courseDetails != null) course = courseDetails.CourseName;
                objUrl2.Title = course;
                objUrl2.urlRel = "course/" + Utils.RemoveIllegealFromCourse(courseName[1]);
                itemList.Add(objUrl2);
                objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase("exams"));
                objUrl3.urlRel = "exams/" + Utils.RemoveIllegealFromCourse(courseName[1]);
                itemList.Add(objUrl3);
                objUrl4.Title = Utils.RemoveHyphen(Common.GetStringProperCase(courseName[courseName.Length - 1]).ToUpper());
                objUrl4.urlRel = Utils.RemoveIllegealFromCourse(courseName[1]) + "/exam-details/year/" + courseName[4] + "/" + Utils.RemoveIllegalCharacters(courseName[5]);
                itemList.Add(objUrl4);
                return itemList;
            }
            private List<Url> RewriteForCollegeCompare(string url)
            {
                var itemList = new List<Url>();
                var objUrl2 = new Url();
                var objUrl3 = new Url();
                var objUrl4 = new Url(); 
                var objUrl5 = new Url();
                var index = url.LastIndexOf('?');
                if (index > 0)
                {
                    var splitUrl = url.Split('/');
                   var courseName = splitUrl[1];
                   var course = "";
                   var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                        .Where(
                                            result =>
                                            courseName.Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                              StringComparison.OrdinalIgnoreCase))
                                        .ToList().FirstOrDefault();
                   if (courseDetails != null) course = courseDetails.CourseName;
                    splitUrl = splitUrl[splitUrl.Length - 1].Split('=');
                    var college2 = splitUrl[splitUrl.Length - 1];
                    splitUrl = splitUrl[1].Split('&');
                    var college1 = splitUrl[0];
                    objUrl2.Title = course;
                    objUrl2.urlRel = "course/" + Utils.RemoveIllegealFromCourse(courseName);
                    itemList.Add(objUrl2);
                    objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase("compare-college"));
                    objUrl3.urlRel = Utils.RemoveIllegealFromCourse(courseName) + "/compare-colleges";
                    itemList.Add(objUrl3);
                    objUrl4.Title = Utils.RemoveHyphen(Common.GetStringProperCase(college1));
                    objUrl4.urlRel = "college-details/" + Utils.RemoveIllegealFromCourse(courseName) + "/" +
                                     Utils.RemoveIllegalCharacters(college1);
                                    
                    itemList.Add(objUrl4);
                    objUrl5.Title = Utils.RemoveHyphen(Common.GetStringProperCase(college2));
                    objUrl5.urlRel = "college-details/" + Utils.RemoveIllegealFromCourse(courseName) + "/" +
                                     Utils.RemoveIllegalCharacters(college2);

                    itemList.Add(objUrl5);
                }
                else
                {
                    var courseName = url.Split('/');
                    var course = "";
                    var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                         .Where(
                                             result =>
                                             courseName[1].Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                               StringComparison.OrdinalIgnoreCase))
                                         .ToList().FirstOrDefault();
                    if (courseDetails != null) course = courseDetails.CourseName;
                    objUrl2.Title = course;
                    objUrl2.urlRel = "course/" + Utils.RemoveIllegealFromCourse(courseName[1]);
                    itemList.Add(objUrl2);
                    objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase("compare-colleges"));
                    objUrl3.urlRel = Utils.RemoveIllegealFromCourse(courseName[1]) + "/compare-colleges";
                    itemList.Add(objUrl3);
                }
                return itemList;
            }
            private List<Url> RewriteForStreamCompare(string url)
            {
                var itemList = new List<Url>();
                var objUrl2 = new Url();
                var objUrl3 = new Url();
                var objUrl4 = new Url(); var objUrl5 = new Url();
                var index = url.LastIndexOf('?');
                if (index > 0)
                {
                    var splitUrl = url.Split('/');
                    var courseName = splitUrl[1];
                    var course = "";
                    var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                         .Where(
                                             result =>
                                             courseName.Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                               StringComparison.OrdinalIgnoreCase))
                                         .ToList().FirstOrDefault();
                    if (courseDetails != null) course = courseDetails.CourseName;
                    splitUrl = splitUrl[splitUrl.Length - 1].Split('=');
                    var stream2 = splitUrl[splitUrl.Length - 1];
                    splitUrl = splitUrl[1].Split('&');
                    var stream1 = splitUrl[0];
                    objUrl2.Title = course;
                    objUrl2.urlRel = "course/" + Utils.RemoveIllegealFromCourse(courseName);
                    itemList.Add(objUrl2);
                    objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase("Compare-Streams"));
                    objUrl3.urlRel = Utils.RemoveIllegealFromCourse(courseName) + "/compare-streams";
                    itemList.Add(objUrl3);
                    objUrl4.Title = Utils.RemoveHyphen(Common.GetStringProperCase(stream1));
                    objUrl4.urlRel = "stream-detail/" + Utils.RemoveIllegealFromCourse(courseName) + "/" +
                                     Utils.RemoveIllegalCharacters(stream1);

                    itemList.Add(objUrl4);
                    objUrl5.Title = Utils.RemoveHyphen(Common.GetStringProperCase(stream2));
                    objUrl5.urlRel = "stream-detail/" + Utils.RemoveIllegealFromCourse(courseName) + "/" +
                                     Utils.RemoveIllegalCharacters(stream2);

                    itemList.Add(objUrl5);
                }
                else
                {
                    var courseName = url.Split('/');
                    var course = "";
                    var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                         .Where(
                                             result =>
                                             courseName[1].Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                               StringComparison.OrdinalIgnoreCase))
                                         .ToList().FirstOrDefault();
                    if (courseDetails != null) course = courseDetails.CourseName;
                    objUrl2.Title = course;
                    objUrl2.urlRel = "course/" + Utils.RemoveIllegealFromCourse(courseName[1]);
                    itemList.Add(objUrl2);
                    objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase("compare-streams"));
                    objUrl3.urlRel = Utils.RemoveIllegealFromCourse(courseName[1]) + "/compare-streams";
                    itemList.Add(objUrl3);
                }
                return itemList;
            }
            private List<Url> RewriteForCollegeSearchByName(string url)
            {
                var itemList = new List<Url>();
                var objUrl2 = new Url();
             
                var collegeName = url.Split('=');
                objUrl2.Title = Utils.RemoveHyphen(Common.GetStringProperCase(collegeName[collegeName.Length - 1]));
                objUrl2.urlRel = "collegesearch?collegeName=" + Utils.RemoveIllegalCharacters(collegeName[collegeName.Length - 1]);
                itemList.Add(objUrl2);
                return itemList;
            }
            private List<Url> RewriteForExamSearchByName(string url)
            {
                var itemList = new List<Url>();
                var objUrl2 = new Url();
             
                var examName = url.Split('=');
                objUrl2.Title = Utils.RemoveHyphen(Common.GetStringProperCase(examName[examName.Length - 1]).ToUpper());
                objUrl2.urlRel = "examsearch?exam=" + Utils.RemoveIllegalCharacters(examName[examName.Length - 1]);
                itemList.Add(objUrl2);
                return itemList;
            }


             private List<Url> RewriteForCollegeExamList(string url)
            {
                var itemList = new List<Url>();
                var objUrl2 = new Url(); var objUrl3 = new Url();
                var urlSplit = url.Split('/');
                 var courseName = urlSplit[1];
                 var course = "";
                 var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                      .Where(
                                          result =>
                                          courseName.Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                            StringComparison.OrdinalIgnoreCase))
                                      .ToList().FirstOrDefault();
                 if (courseDetails != null) course = courseDetails.CourseName;
                 objUrl2.Title = course;
                 objUrl2.urlRel = "course/" + Utils.RemoveIllegealFromCourse(courseName);
                itemList.Add(objUrl2);
                objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase(urlSplit[urlSplit.Length-1]));
                objUrl3.urlRel = "examsearch?exam=" + Utils.RemoveIllegalCharacters(urlSplit[urlSplit.Length - 1]);
                itemList.Add(objUrl3);
                return itemList;
            }
       
             private List<Url> RewriteForCollegeCounsellingInstruction(string url)
             {
                 var itemList = new List<Url>();
                 var objUrl2 = new Url(); var objUrl3 = new Url();  var objUrl5 = new Url();
                 var urlSplit = url.Split('/');
                 var courseName = urlSplit[1];

                 var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                         .Where(
                                             result =>
                                             courseName.Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                               StringComparison.OrdinalIgnoreCase))
                                         .ToList().FirstOrDefault();
                 if (courseDetails != null) courseName = courseDetails.CourseName;

                 objUrl2.Title = courseName;
                            objUrl2.urlRel = "course/" + Utils.RemoveIllegealFromCourse(courseName);
                             itemList.Add(objUrl2);

                             objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase("get-direct-admission"));
                             objUrl3.urlRel = Utils.RemoveIllegealFromCourse(courseName.ToLower()) + "/get-direct-admission/";
                            itemList.Add(objUrl3);


                            objUrl5.Title = Utils.RemoveHyphen(Common.GetStringProperCase("instruction"));
                            objUrl5.urlRel = Utils.RemoveIllegealFromCourse(courseName.ToLower()) + "/counselling/instruction/";
                            itemList.Add(objUrl5);

                 return itemList;
             }
             private List<Url> RewriteForCollegeCounsellingStudentCounselling(string url)
             {
                 var itemList = new List<Url>();
                 var objUrl2 = new Url(); var objUrl3 = new Url();  var objUrl5 = new Url(); var objUrl6 = new Url();
                 var urlSplit = url.Split('/');
                 var courseName = urlSplit[1];

                 var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                         .Where(
                                             result =>
                                             courseName.Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                               StringComparison.OrdinalIgnoreCase))
                                         .ToList().FirstOrDefault();
                 if (courseDetails != null) courseName = courseDetails.CourseName;

                 objUrl2.Title = courseName;
                 objUrl2.urlRel = "course/" + Utils.RemoveIllegealFromCourse(courseName);
                 itemList.Add(objUrl2);

                 objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase("get-direct-admission"));
                 objUrl3.urlRel = Utils.RemoveIllegealFromCourse(courseName.ToLower()) + "/get-direct-admission/";
                 itemList.Add(objUrl3);

                 objUrl5.Title = Utils.RemoveHyphen(Common.GetStringProperCase("online-application-form"));
                 objUrl5.urlRel = Utils.RemoveIllegealFromCourse(courseName.ToLower()) + "/counselling/OnlineApplicationForm/";
                 itemList.Add(objUrl5);

                 return itemList;
             }
             private List<Url> RewriteForCollegeCounsellingStudentPayment(string url)
             {
                 var itemList = new List<Url>();
                 var objUrl2 = new Url(); var objUrl3 = new Url();  var objUrl5 = new Url();   var objUrl6 = new Url();
                 var objUrl7 = new Url();
                 var urlSplit = url.Split('/');
                 var courseName = urlSplit[1];

                 var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                         .Where(
                                             result =>
                                             courseName.Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                               StringComparison.OrdinalIgnoreCase))
                                         .ToList().FirstOrDefault();
                 if (courseDetails != null) courseName = courseDetails.CourseName;

                 objUrl2.Title = courseName;
                 objUrl2.urlRel = "course/" + Utils.RemoveIllegealFromCourse(courseName);
                 itemList.Add(objUrl2);

                 objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase("get-direct-admission"));
                 objUrl3.urlRel = Utils.RemoveIllegealFromCourse(courseName.ToLower()) + "/get-direct-admission/";
                 itemList.Add(objUrl3);


                 objUrl6.Title = Utils.RemoveHyphen(Common.GetStringProperCase("online-application-form"));
                 objUrl6.urlRel = Utils.RemoveIllegealFromCourse(courseName.ToLower()) + "/counselling/OnlineApplicationForm/";
                 itemList.Add(objUrl6);

                 objUrl7.Title = Utils.RemoveHyphen(Common.GetStringProperCase("Online-Transaction"));
                 objUrl7.urlRel = Utils.RemoveIllegealFromCourse(courseName.ToLower()) + "/counselling/onlinetransaction/";
                 itemList.Add(objUrl7);
                 return itemList;
             }
             private List<Url> RewriteForCollegeCounsellingThankYou(string url)
             {
                 var itemList = new List<Url>();
                 var objUrl2 = new Url(); var objUrl3 = new Url(); var objUrl5 = new Url(); var objUrl6 = new Url();
                 var objUrl7 = new Url(); var objUrl8 = new Url();
                 var urlSplit = url.Split('/');
                 var courseName = urlSplit[1];

                 var courseDetails = CourseProvider.Instance.GetAllCourseList()
                                         .Where(
                                             result =>
                                             courseName.Equals(Utils.RemoveIllegealFromCourse(result.CourseName),
                                                               StringComparison.OrdinalIgnoreCase))
                                         .ToList().FirstOrDefault();
                 if (courseDetails != null) courseName = courseDetails.CourseName;

                 objUrl2.Title = courseName;
                 objUrl2.urlRel = "course/" + Utils.RemoveIllegealFromCourse(courseName);
                 itemList.Add(objUrl2);

                 objUrl3.Title = Utils.RemoveHyphen(Common.GetStringProperCase("get-direct-admission"));
                 objUrl3.urlRel = Utils.RemoveIllegealFromCourse(courseName.ToLower()) + "/get-direct-admission/";
                 itemList.Add(objUrl3);

                 objUrl6.Title = Utils.RemoveHyphen(Common.GetStringProperCase("online-application-form"));
                 objUrl6.urlRel = Utils.RemoveIllegealFromCourse(courseName.ToLower()) + "/counselling/OnlineApplicationForm/";
                 itemList.Add(objUrl6);

                 objUrl7.Title = Utils.RemoveHyphen(Common.GetStringProperCase("Online-Transaction"));
                 objUrl7.urlRel = Utils.RemoveIllegealFromCourse(courseName.ToLower()) + "/counselling/onlinetransaction/";
                 itemList.Add(objUrl7);
                     objUrl8.Title = Utils.RemoveHyphen(Common.GetStringProperCase("Thank-You"));
                     objUrl8.urlRel = Utils.RemoveIllegealFromCourse(courseName.ToLower()) + "/counselling/thankyou/";
                     itemList.Add(objUrl8);
                 return itemList;
             }
            class Url
            {
               public string Title;
               public  string urlRel;
            }
        
        }
    }
}
