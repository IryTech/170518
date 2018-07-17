using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using System.Web.Services;
using System.Net.Mail;
using System.IO;
using System.Text.RegularExpressions;

namespace IryTech.AdmissionJankari20.Web.WebServices
{
    /// <summary>
    /// Summary description for CommonWEServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    [Serializable]
    public class CommonWeServices : System.Web.Services.WebService
    {
        private SecurePage objSecurePage;
        private Common _objCommon;
        private string[] CountingRecords = new string[3] {"", "", ""};

        [WebMethod ]
        public ListItem[] GetCountryList()
        {
            var data = CountryProvider.Instance.GetAllCountry();
            if (data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.CountryName,
                        Value =
                            result.CountryId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;
        }

        [WebMethod]
        public ListItem[] GetState(string countryId)
        {
            var data = StateProvider.Instance.GetStateByCountry(Convert.ToInt16(countryId));

            if (data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.StateName,
                        Value =
                            result.StateId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;
        }

        //Method for bind all State list
        [WebMethod]
        public ListItem[] GetAllState()
        {
            var data = StateProvider.Instance.GetAllState();
            if (data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.StateName,
                        Value =
                            result.StateId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;
        }

        //Web method for city
        [WebMethod]
        public ListItem[] GetAllCityWithoutId()
        {
            var data = CityProvider.Instacnce.GetAllCityList().OrderBy(result => result.CityName).ToList();
            if (data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.CityName.Trim(),
                        Value =
                            result.CityId.ToString(CultureInfo.InvariantCulture)

                    }).OrderBy(m=>m.Text).ToArray();
            return null;
        }

        //web Method for Get All city list
        [WebMethod]
        public ListItem[] GetCity(string stateId)
        {
            var data = CityProvider.Instacnce.GetCityListByState(Convert.ToInt32(stateId));
            if (data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.CityName,
                        Value =
                            result.CityId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;
        }

        //web Method for Get All city list
        [WebMethod]
        public string GetAllCity()
        {
            string citySeperatedList = "";
            var data = CityProvider.Instacnce.GetAllCityList();
            if (data.Count > 0)
            {
                var cityLists = (from test in data select test.CityName).ToArray();
                citySeperatedList = String.Join(",", cityLists);
            }
            return citySeperatedList;
        }

        //web Method for Get All State list
        [WebMethod]
        public string GetSearchAllState()
        {
            string StateSeperatedList = "";
            var data = StateProvider.Instance.GetAllState();
            if (data.Count > 0)
            {
                var StateLists = (from test in data select test.StateName).ToArray();
                StateSeperatedList = String.Join(",", StateLists);
            }

            return StateSeperatedList;
        }

        [WebMethod]
        public string GetNewsDetails()
        {
            var newsSeperatedList = "";
            var data = NewsArticleNoticeProvider.Instance.GetAllNewsList();
            if (data.Count > 0)
            {
                var newsList = (from test in data select test.NewsSubject).ToArray();
                newsSeperatedList = String.Join(",", newsList);
            }
            return newsSeperatedList;
        }

        [WebMethod]
        public string GetCollegeGroupDetails()
        {
            var collegeGroupSeperatedList = "";
            var data = CollegeProvider.Instance.GetAllCollegeGroupList();
            if (data.Count > 0)
            {
                var collegeGroupList = (from test in data select test.CollegeGroupName).ToArray();
                collegeGroupSeperatedList = String.Join(",", collegeGroupList);
            }
            return collegeGroupSeperatedList;
        }

        [WebMethod]
        public string GetInstituteNameDetails()
        {
            var collegeGroupSeperatedList = "";
            var data = CollegeProvider.Instance.GetAllCollegeGroupList();
            if (data.Count > 0)
            {
                var collegeGroupList = (from test in data select test.CollegeGroupName).ToArray();
                collegeGroupSeperatedList = String.Join(",", collegeGroupList);
            }
            return collegeGroupSeperatedList;
        }

        [WebMethod]
        public string GetCollegeDetails()
        {

            var collegeSeperatedList = "";
            _objCommon = new Common();
            var ds = new DataSet();

            ds = _objCommon.GetCollegeNameList();
            if (ds.Tables.Count > 1)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var query = ds.Tables[0].AsEnumerable().ToList();
                    var query1 = ds.Tables[1].AsEnumerable();
                    collegeSeperatedList = string.Join(",",
                                                       from k in query select k.Field<string>("AjCollegeBranchName"));
                    collegeSeperatedList = collegeSeperatedList + "," +
                                           string.Join(",",
                                                       from k in query1
                                                       select k.Field<string>("AjCollegeBranchPopularName"));
                }
            }

            return collegeSeperatedList;
        }

        [WebMethod]
        public string GetCollegeDetailsbyCourseID(int courseid)
        {
            var collegeSeperatedList = "";
            _objCommon = new Common();
            DataSet ds = new DataSet();

            ds = _objCommon.GetCollegeNameList(courseid);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var query = ds.Tables[0].AsEnumerable().ToList();
                    //var query1 = ds.Tables[1].AsEnumerable();
                    collegeSeperatedList = string.Join(",",
                                                       from k in query select k.Field<string>("AjCollegeBranchName"));
                    //collegeSeperatedList = collegeSeperatedList + "," +
                    //                       string.Join(",",
                    //                                   from k in query1
                    //                                   select k.Field<string>("AjCollegeBranchPopularName"));
                    //var result = numbers.Aggregate("", (current, item) => current + item.ToString() + ",");
                    //collegeSeperatedList.Append(ds.Tables[0].Rows[0][0].ToString() + "," + ds.Tables[1].Rows[0][0].ToString());
                }
            }

            return collegeSeperatedList;
        }

        [WebMethod]
        public string GetCollegeDetailsbyCourseIDParticipated(int filter, int courseid)
        {
            var collegeSeperatedList = "";
            _objCommon = new Common();
            DataSet ds = new DataSet();

            ds = _objCommon.GetCollegeNameListbyParticipated(filter, courseid);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var query = ds.Tables[0].AsEnumerable().ToList();
                    //var query1 = ds.Tables[1].AsEnumerable();
                    collegeSeperatedList = string.Join(",",
                                                       from k in query select k.Field<string>("AjCollegeBranchName"));
                    //collegeSeperatedList = collegeSeperatedList + "," +
                    //                       string.Join(",",
                    //                                   from k in query1
                    //                                   select k.Field<string>("AjCollegeBranchPopularName"));
                    //var result = numbers.Aggregate("", (current, item) => current + item.ToString() + ",");
                    //collegeSeperatedList.Append(ds.Tables[0].Rows[0][0].ToString() + "," + ds.Tables[1].Rows[0][0].ToString());
                }
            }

            return collegeSeperatedList;
        }

        [WebMethod]
        public string GetCollegeDetailsbySponserCourseStateCity(int filter, int courseid, int stateid, int cityid)
        {
            var collegeSeperatedList = "";
            _objCommon = new Common();
            var ds = new DataSet();

            ds = _objCommon.GetCollegeNameListbySponserCourseStateCity(filter, courseid, stateid, cityid);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var query = ds.Tables[0].AsEnumerable().ToList();
                    collegeSeperatedList = string.Join(",",
                                                       from k in query select k.Field<string>("AjCollegeBranchName"));

                }
            }

            return collegeSeperatedList;
        }

        [WebMethod]
        public string GetUniversityMaster()
        {
            var universitySeperatedList = "";
            var data = UniversityProvider.Instance.GetAllUniversityList();
            if (data.Count > 0)
            {
                var universityList = (from test in data select test.UniversityName).ToArray();
                universitySeperatedList = String.Join(",", universityList);
            }
            return universitySeperatedList;
        }

        [WebMethod]
        public string GetStreamByCourseSearch(string courseId)
        {
            var streamSeperatedList = "";
            var data = StreamProvider.Instance.GetStreamListByCourse(Convert.ToInt16(courseId));
            if (data != null && data.Count > 0)
            {
                var streamList = (from test in data select test.CourseStreamName).ToArray();
                streamSeperatedList = String.Join(",", streamList);
            }
            return streamSeperatedList;
        }

        [WebMethod]
        public string GetNoticeDetails()
        {
            var noticeSeperatedList = "";
            var data = NewsArticleNoticeProvider.Instance.GetAllNoticeList();
            if (data.Count > 0)
            {
                var noticeList = (from test in data select test.NoticeSubject).ToArray();
                noticeSeperatedList = String.Join(",", noticeList);
            }
            return noticeSeperatedList;
        }

        [WebMethod]
        public string GetStreamList()
        {
            var streamSeperatedList = "";
            var data = StreamProvider.Instance.GetAllStreamList();
            if (data.Count > 0)
            {
                var streamList = (from test in data select test.CourseStreamName).ToArray();
                streamSeperatedList = String.Join(",", streamList);
            }
            return streamSeperatedList;
        }

        [WebMethod]
        public ListItem[] BindUniversityCategory()
        {
            var data =
                UniversityProvider.Instance.GetAllUniversityCategoryList()
                                  .OrderBy(x => x.UniversityCategoryName)
                                  .ToList();
            if (data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.UniversityCategoryName,
                        Value =
                            result.UniversityCategoryId.ToString(
                                CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;
        }

        //web Method for Get All Exam list
        [WebMethod]
        public string GetAllExamList()
        {
            string ExamSeperatedList = "";
            var data = ExamProvider.Instance.GetAllExamList();
            if (data.Count > 0)
            {
                var ExamLists = (from test in data select test.ExamName).ToArray();
                ExamSeperatedList = String.Join(",", ExamLists);
            }

            return ExamSeperatedList;
        }

        [WebMethod(EnableSession = true)]
        public string GetAllExamListFront()
        {
            string ExamSeperatedList = "";
            //  (course => course.CourseStatus == true)
            _objCommon = new Common();
            var data =
                ExamProvider.Instance.GetExamListByCourseId(_objCommon.CourseId)
                            .Where(exam => exam.ExamStatus == true)
                            .ToList();
            if (data.Count > 0)
            {
                var ExamLists = (from test in data select test.ExamName).ToArray();
                ExamSeperatedList = String.Join(",", ExamLists);
            }

            return ExamSeperatedList;
        }


        //web Method for Get All user name List For User Master in backend 
        [WebMethod]
        public string GetUserManagerList()
        {
            string userNameSeperatedList = "";
            var data = UserManagerProvider.Instance.GetAllUserList();
            if (data.Count > 0)
            {
                var userNameLists = (from test in data select test.UserFullName).ToArray();
                userNameSeperatedList = String.Join(",", userNameLists);
            }
            return userNameSeperatedList;
        }

        //web Method for Get All user EmailId List For User Master in backend 
        [WebMethod]
        public string GetUserEmailIDList()
        {
            string userEmailIdSeperatedList = "";
            var data = UserManagerProvider.Instance.GetAllUserList();
            if (data.Count > 0)
            {
                var userEmailIdLists = (from test in data select test.UserEmailid).ToArray();
                userEmailIdSeperatedList = String.Join(",", userEmailIdLists);
            }
            return userEmailIdSeperatedList;
        }

        //web Method for Get All user Mobile No. List For User Master in backend 
        [WebMethod]
        public string GetMobileNoList()
        {
            string userMobileNoSeperatedList = "";
            var data = UserManagerProvider.Instance.GetAllUserList();
            if (data.Count > 0)
            {
                var userMobileNoLists = (from test in data select test.MobileNo).ToArray();
                userMobileNoSeperatedList = String.Join(",", userMobileNoLists);
            }
            return userMobileNoSeperatedList;
        }

        //web method for user category
        [WebMethod]
        public ListItem[] GetAllUserCategory()
        {
            var data = UserManagerProvider.Instance.GetAllUserCategoryList();
            if (data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.UserCategoryName,
                        Value =
                            result.UserCategoryId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;
        }

        //Method for bind all Course list
        [WebMethod]
        public ListItem[] GetAllCourseList()
        {
            var data = CourseProvider.Instance.GetAllCourseList();
            if (data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.CourseName,
                        Value =
                            result.CourseId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;
        }

        //web Method for Get All Exam Subject Name list
        [WebMethod]
        public string GetAllExamSubjectNameList()
        {
            string ExamSubjectSeperatedList = "";
            var data = ExamProvider.Instance.GetAllExamFormDetails();
            if (data.Count > 0)
            {
                var ExamSubjectLists = (from test in data select test.ExamFormSubject).ToArray();
                ExamSubjectSeperatedList = String.Join(",", ExamSubjectLists);
            }

            return ExamSubjectSeperatedList;
        }

        //Method for bind all Zone list
        [WebMethod]
        public ListItem[] GetAllZone()
        {
            var data = ZoneProvider.Instance.GetAllZoneList();
            if (data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.ZoneName,
                        Value =
                            result.ZoneId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;
        }

        //Method for bind all Zone list by Zone Id
        [WebMethod]
        public ListItem[] GetZoneByZoneId(string ZoneId)
        {
            var data = ZoneProvider.Instance.GetZoneListById(Convert.ToInt16(ZoneId));
            if (data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.ZoneName,
                        Value =
                            result.ZoneId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;
        }

        //method to get institute type added by indu kumar pandey on 31/10/2012...............
        [WebMethod]
        public ListItem[] GetInstituteType()
        {
            var data = CollegeProvider.Instance.GetAllInstituteTypeList();
            if (data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.InstituteType,
                        Value =
                            result.InstituteTypeId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;
        }

        //method to get CollegeAssociate  added by indu kumar pandey on 31/10/2012...............
        [WebMethod]
        public ListItem[] GetCollegeAssociate()
        {
            var data = CollegeProvider.Instance.GetAllCollegeAssociationCategoryType();
            if (data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.AssociationCategoryType,
                        Value =
                            result.AssociationCategoryTypeId.ToString(
                                CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;
        }

        //method to get CollegeGroup  added by indu kumar pandey on 31/10/2012...............
        [WebMethod]
        public ListItem[] GetCollegeGroup()
        {
            var data = CollegeProvider.Instance.GetAllCollegeGroupList();
            if (data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.CollegeGroupName,
                        Value =
                            result.CollegeGroupId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;
        }

        //method to get CollegeHostel  added by indu kumar pandey on 31/10/2012...............
        [WebMethod]
        public ListItem[] GetCollegeHostel()
        {
            var data = CollegeProvider.Instance.GetAllHostelCategory();
            if (data != null && data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.HostelCategoryType,
                        Value =
                            result.HostelCategoryId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;
        }

        //method to get UniversityMaster  added by indu kumar pandey on 31/10/2012...............
        [WebMethod]
        public ListItem[] GetUniversityList()
        {
            var data = UniversityProvider.Instance.GetAllUniversityList();
            if (data != null && data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.UniversityName,
                        Value =
                            result.UniversityId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;
        }


        // method to Show the front end Course Details 
        [WebMethod]
        public ListItem[] BindFrontCourse()
        {
            var data = CourseProvider.Instance.GetAllCourseList();
            if (data.Count > 0)
                data = data.Where(course => course.CourseStatus == true).OrderBy(course => course.CourseName).ToList();
            return data.Select(result => new ListItem()
                {
                    Text = result.CourseName,
                    Value = Convert.ToString(result.CourseId)

                }).ToArray();
           
        }

        [WebMethod(EnableSession = true)]
        public string SaveStudentCommonQuery(string name, string emailId, string mobileNo, string cityName,
                                             int courseId, string query, string studentcoursename)
        {


            _objCommon = new Common();
            var Msg = "";
            var UserId = 0;
            if (Utils.IsEmailValid(emailId))
            {
                var objMailTemplete = new MailTemplates();
                if (Utils.IsMobileValid(mobileNo))
                {
                    var StudeName = Common.GetStringProperCase(Regex.Replace(name.Trim(),
                                                                         "\\s+", " "));
                    var objQueryProperty = new QueryProperty
                        {
                            StudentName =
                                StudeName,
                            UserEmailId = emailId,
                            UserMobileNo = mobileNo,
                            StudentCityName = cityName,
                            StudentCourseId = courseId,
                            StudentQuery = query,
                            StudentCourseName = studentcoursename,
                        };
                    int i = QueryProvider.Instance.InsertCommonQuickQuery(objQueryProperty, out Msg,out UserId);
                    //new com.admissionjankari.crm.CommonServices().ImportAdmissionJankariLeads(0, mobileNo,
                    //                                                                          name,
                    //                                                                          emailId,
                    //                                                                          studentcoursename,
                    //                                                                          null, null,
                    //                                                                          cityName
                    //                                                                          , null,
                    //                                                                          query, true);

                    SecurePage _objSecurePage = new SecurePage();
                    _objSecurePage.LoggedInUserId = UserId;
                    _objSecurePage.LoggedInUserType = 1;
                    _objSecurePage.LoggedInUserEmailId = emailId.Trim();
                    _objSecurePage.LoggedInUserName = StudeName;
                    _objSecurePage.LoggedInUserMobile = mobileNo;
                    if (i == 2)
                    {
                       


                        var objMail = new MailMessage
                            {
                                From = new MailAddress(ApplicationSettings.Instance.Email),
                                Subject = "AdmissionJankari:Registration"
                            };
                        var mailbody = objMailTemplete.MailBodyForRegistation(name, emailId, mobileNo);
                        objMail.Body = mailbody;
                        objMail.To.Add(objQueryProperty.UserEmailId);
                        objMail.IsBodyHtml = true;
                        Utils.SendMailMessageAsync(objMail);

                    }
                    var mail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "AdmissionJankari:Query"
                        };
                    var body = objMailTemplete.MailBodyForCommonQuery(name, studentcoursename, query, mobileNo);
                    mail.Body = body;
                    mail.To.Add(objQueryProperty.UserEmailId);
                    mail.IsBodyHtml = true;
                    Utils.SendMailMessageAsync(mail);

                    return Msg;

                }
                else
                {
                    return Msg = _objCommon.GetValidationMessage("revContactNo");
                }
            }
            else
            {
                return Msg = _objCommon.GetValidationMessage("revEmail");
            }
        }

        [WebMethod]
        public string SaveStudentLoanQuery(string name, string emailId, string mobileNo, string cityName,
                                           string courseId, string query, string Amount, string realQuery,
                                           string bankName)
        {
            realQuery = System.Uri.UnescapeDataString(realQuery);
            _objCommon = new Common();
            MailTemplates _objMailTemplete = new MailTemplates();
            string Msg = "";
            int userId =0;
            if (Utils.IsEmailValid(emailId))
            {
                if (Utils.IsMobileValid(mobileNo))
                {
                    QueryProperty objQueryProperty = new QueryProperty
                        {
                            StudentName =
                                Common.GetStringProperCase(Regex.Replace(name.Trim(),
                                                                         "\\s+", " ")),
                            UserEmailId = emailId,
                            UserMobileNo = mobileNo,
                            StudentCityName = cityName,
                            StudentCourseId = Convert.ToInt32(courseId),
                            StudentQuery = query
                        };
                    int i = QueryProvider.Instance.InsertCommonQuickQuery(objQueryProperty, out Msg,out userId);
                    var courseData = CourseProvider.Instance.GetCourseById(Convert.ToInt32(courseId));
                    new com.admissionjankari.crm.CommonServices().ImportAdmissionJankariLeads(0, mobileNo,
                                                                                              name,
                                                                                              emailId,
                                                                                              courseData.First()
                                                                                                        .CourseName,
                                                                                              null, null,
                                                                                              cityName
                                                                                              , null,
                                                                                              "Bank Loan Query : " +
                                                                                              bankName + "-" + query,
                                                                                              true);
                    if (i == 2)
                    {
                       
                        var ObjMail = new MailMessage
                            {
                                From = new MailAddress(ApplicationSettings.Instance.Email),
                                Subject = "AdmissionJankari:Registration"
                            };
                        var mailbody = _objMailTemplete.MailBodyForRegistation(name, emailId, mobileNo);
                        ObjMail.Body = mailbody;
                        ObjMail.To.Add(objQueryProperty.UserEmailId);
                        ObjMail.IsBodyHtml = true;
                        Utils.SendMailMessageAsync(ObjMail);
                    }

                    var courseDetails = CourseProvider.Instance.GetCourseById(Convert.ToInt16(courseId));
                    var courseQuery = courseDetails.First();
                    var mail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "AdmissionJankari:Education Loan"
                        };
                    var body = _objMailTemplete.MailBodyForBankQuery(name, courseQuery.CourseName,
                                                                     realQuery.Replace("%20", " "), mobileNo,
                                                                     bankName.Replace("%20", " "), Amount);
                    mail.Body = body;
                    mail.To.Add(objQueryProperty.UserEmailId);
                    mail.IsBodyHtml = true;
                    Utils.SendMailMessageAsync(mail);
                    return Msg;

                }
                else
                {
                    return Msg = _objCommon.GetValidationMessage("revContactNo");
                }
            }
            else
            {
                return Msg = _objCommon.GetValidationMessage("revEmail");
            }
        }

        //method to get ExamList  added by indu kumar pandey on 31/10/2012...............
        [WebMethod]
        public ListItem[] GetExamList()
        {
            var data = ExamProvider.Instance.GetAllExamList();
            if (data != null && data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.ExamName,
                        Value =
                            result.ExamId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;

        }

        //method to get rank source  added by indu kumar pandey on 31/10/2012...............
        [WebMethod]
        public ListItem[] GetRankSourceList()
        {
            var data = CollegeProvider.Instance.GetAllCollegeRankSourceList();
            if (data != null && data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.CollegeRankSourceName,
                        Value =
                            result.CollegeRankSourceId.ToString(
                                CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;

        }

        //method to get stream By course  added by indu kumar pandey on 1/11/2012...............
        [WebMethod]
        public ListItem[] GetStreamListByCourseId(string courseId)
        {
            var data =
                StreamProvider.Instance.GetStreamListByCourse(Convert.ToInt16(courseId))
                              .OrderBy(stream => stream.CourseStreamName)
                              .ToList();
            if (data != null && data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.CourseStreamName,
                        Value =
                            result.StreamId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;

        }

        [WebMethod]
        public ListItem[] GetCourseListHavingStream()
        {
            _objCommon = new Common();
            var data = _objCommon.GetCourseListHavingStream().AsEnumerable().ToList();
            if (data != null && data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.Field<string>("AjCourseName"),
                        Value = result.Field<int>("ajcourseid").ToString()

                    }).ToArray();
            return null;
        }

        //method to get stream By course  added by indu kumar pandey on 1/11/2012...............
        [WebMethod]
        public ListItem[] GetExamListByCourseId(string courseId)
        {
            var data = ExamProvider.Instance.GetExamListByCourseId(Convert.ToInt32(courseId));
            if (data != null && data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.ExamName,
                        Value =
                            result.ExamId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;

        }



        [WebMethod(EnableSession = true)]
        public CollegeData[] InserCollegeDetails(string colegeInstitute, string collegeGroup, string collegeAssociate,
                                                 string collegeName, string manageMent, string collegeEst,
                                                 string branchDesc, string popualrName, int cityId, string webSite,
                                                 string status, string emailId, string mobileNo, string pinCode,
                                                 string fax, string address)
        {
            var countryId = 0;
            var stateId = 0;
            var objSecurePage = new SecurePage();
            var cityData = CityProvider.Instacnce.GetCityById(cityId);
            if (cityData.Count > 0)
            {
                var query = cityData.Select(result => new
                    {
                        countryId = result.CountryId,
                        stateId = result.StateId,
                        stateName = result.StateName
                    }).First();
                countryId = query.countryId;
                stateId = query.stateId;
            }

            var objCollegeBranchProperty = new CollegeBranchProperty
                {
                    InstituteTypeId = Convert.ToInt16(colegeInstitute),
                    CollegeGroupId = Convert.ToInt16(collegeGroup),
                    CollegeBranchName = collegeName.Trim(),
                    CollegePopulaorName = popualrName,
                    CollegeManagementTypeId = Convert.ToInt16(manageMent),
                    CollegeBranchEst = collegeEst,
                    CollegeBranchDesc = branchDesc,
                    CollegeBranchCityId = cityId,
                    CollegeBranchCountryId = countryId,
                    CollegeBranchStateId = stateId,
                    CollegeBranchAddrs = address,
                    CollegeBranchPinCode = pinCode,
                    CollegeBranchMobileNo = mobileNo,
                    CollegeBranchFax = fax,
                    CollegeBranchStatus = status == "true" ? true : false,
                    CollegeBranchWebsite = webSite,
                    CoillegeBranchEmailId = emailId,
                };
            var errMsg = "";
            var collegeBranchId = 0;
            var objCollegeData = new List<CollegeData>();
            var objCollege = new CollegeData();
            var collegeStatus = CollegeProvider.Instance.InsertCollegeBranchInfo(objCollegeBranchProperty, 1, out errMsg,
                                                                                 out collegeBranchId);
            if (collegeStatus <= 0) return objCollegeData.ToArray();
            objCollege.CollegeId = collegeBranchId;
            objCollege.ErrMsg = errMsg;
            objCollegeData.Add(objCollege);
            return objCollegeData.ToArray();
        }

        [WebMethod]
        public ClsDBTablesAttributes[] GetTableNamesNPrimaryField(string tblName)
        {
            var objClsDbTablesAttributes = new ClsDBTablesAttributes();
            return objClsDbTablesAttributes.GetAllTablesNPrimaryColumns(tblName).ToArray();
        }

        [WebMethod]
        public ClsDBTablesAttributes[] GetColumns(string TableName, string AutoIncrementedColumnName)
        {
            var objClsDbTablesAttributes = new ClsDBTablesAttributes();
            return objClsDbTablesAttributes.GetAllColumnsOfTable(TableName, AutoIncrementedColumnName).ToArray();
        }

        [WebMethod]
        public List<string> GetCSVColumns(string FilePath)
        {
            var objClsDbTablesAttributes = new ClsDBTablesAttributes();
            return objClsDbTablesAttributes.GetColumnListofExcel(FilePath);
        }

        [WebMethod]
        public List<SucessCount> ImportCollegeBranchBasicInfo(string _strTableName, string _strPrimaryKey,
                                                              string fileName, string Columns)
        {
            var collegeBranchName = "";
            var listSucessCount = new List<SucessCount>();
            var sucess = 0;
            string failureCount = "";
            var objClsCsvToDatatable = new ExcelToDataTable();
            new DataTable();
            string strColumn = "",
                   dynamicValueData = "",
                   columnDataType = "",
                   dbColumnActualData = "";
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {

                    var seprateColumns = Columns.Split(',');
                    var dtbFinalTable = objClsCsvToDatatable.GetRecordsForMappedColumnFromCSVToDataTable(_strTableName,
                                                                                                         _strPrimaryKey,
                                                                                                         fileName,
                                                                                                         seprateColumns);

                    foreach (string strColumns in seprateColumns)
                    {
                        var arrColumns = strColumns.Split('|');
                        var dbColumnName = Convert.ToString(arrColumns[1]).Trim();
                        var dbDataType = Convert.ToString(arrColumns[2]).Trim();
                        //StrColumn += DBColumnName + "*";
                        columnDataType += dbDataType + "*";
                        dbColumnActualData += dbColumnName + ",";
                    }
                    var splitDbColumn = dbColumnActualData.Split(',');
                    var columnDataTyepSplit = columnDataType.Split('*');
                    //StrColumn = StrColumn.TrimEnd('*');

                    for (var i = 0; i < dtbFinalTable.Rows.Count; i++)
                    {
                        for (var j = 0; j < dtbFinalTable.Columns.Count; j++)
                        {
                            if (splitDbColumn[j] == "AjCollegeBranchName")
                            {
                                collegeBranchName = dtbFinalTable.Rows[i][j].ToString();

                            }
                            if (columnDataTyepSplit[j] == "int")
                            {
                                dynamicValueData += !string.IsNullOrEmpty(dtbFinalTable.Rows[i][j].ToString())
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "null";
                                strColumn += splitDbColumn[j];
                            }
                            else if (columnDataTyepSplit[j] == "bit")
                            {
                                if (!string.IsNullOrEmpty(dtbFinalTable.Rows[i][j].ToString()))
                                {
                                    dynamicValueData += dtbFinalTable.Rows[i][j].ToString() == "True"
                                                            ? dtbFinalTable.Rows[i][j].ToString()
                                                            : "false";
                                }
                                else
                                {
                                    dynamicValueData += false;
                                }
                                strColumn += splitDbColumn[j];
                            }
                            else
                            {
                                dynamicValueData += !string.IsNullOrEmpty(dtbFinalTable.Rows[i][j].ToString())
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "null";
                                strColumn += splitDbColumn[j];
                            }
                            dynamicValueData += "*";
                            strColumn += "*";
                        }
                        dbColumnActualData = dbColumnActualData.TrimEnd(',');
                        dynamicValueData = dynamicValueData.TrimEnd('*');
                        columnDataType = columnDataType.TrimEnd('*');
                        dynamicValueData = dynamicValueData.Trim();
                        var errMsg = "";
                        var result = CollegeProvider.Instance.InsertData(strColumn, dynamicValueData, dbColumnActualData,
                                                                         collegeBranchName.Replace("'", "`"),
                                                                         columnDataType, out errMsg);
                        if (result > 0)
                            sucess++;
                        else
                            failureCount = failureCount + "<br/>Row Number Of Failure Count: " + i + " " + "Error: " +
                                           errMsg;



                        collegeBranchName = "";
                        dynamicValueData = "";

                    }

                    listSucessCount.Add(new SucessCount {TotalNoRows = dtbFinalTable.Rows.Count.ToString()});
                    listSucessCount.Add(new SucessCount {TotalFailureCount = failureCount.ToString()});
                    listSucessCount.Add(new SucessCount {TotalSucessCount = sucess.ToString()});

                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertCollegeBranchInfoByExcel in College.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return listSucessCount;
        }

        [WebMethod]
        public List<SucessCount> ImportCourse(string _strTableName, string _strPrimaryKey, string fileName,
                                              string Columns)
        {
            List<SucessCount> listSucessCount = new List<SucessCount>();
            int Sucess = 0;
            string FailureCount = "";
            ExcelToDataTable _objClsCSVToDatatable = new ExcelToDataTable();
            DataTable sourceTable = new DataTable();
            string FileColumnName = "",
                   collegeBranchName = "",
                   universityName = "",
                   DBColumnName = "",
                   courseName = "",
                   DBDataType = "",
                   StrColumn = "",
                   dynamicValueData = "",
                   ColumnDataType = "",
                   dbColumnActualData = "";
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {

                    var SeprateColumns = Columns.Split(',');
                    DataTable dtbFinalTable = new DataTable();
                    dtbFinalTable = _objClsCSVToDatatable.GetRecordsForMappedColumnFromCSVToDataTable(_strTableName,
                                                                                                      _strPrimaryKey,
                                                                                                      fileName,
                                                                                                      SeprateColumns);
                    foreach (string strColumns in SeprateColumns)
                    {
                        var arrColumns = strColumns.Split('|');
                        FileColumnName = Convert.ToString(arrColumns[0]).Trim();
                        DBColumnName = Convert.ToString(arrColumns[1]).Trim();
                        DBDataType = Convert.ToString(arrColumns[2]).Trim();
                        StrColumn += DBColumnName + "*";
                        ColumnDataType += DBDataType + "*";
                        dbColumnActualData += DBColumnName + ",";
                    }

                    StrColumn = StrColumn.TrimEnd('*');
                    var columnDataTyepSplit = ColumnDataType.Split('*');
                    var splitDbColumn = dbColumnActualData.Split(',');
                    for (int i = 0; i < dtbFinalTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtbFinalTable.Columns.Count; j++)
                        {
                            if (splitDbColumn[j] == "AjCollegeBranchId")
                            {
                                collegeBranchName = dtbFinalTable.Rows[i][j].ToString();
                            }
                            else if (splitDbColumn[j] == "AjUniversityId")
                            {
                                universityName = dtbFinalTable.Rows[i][j].ToString();
                            }
                            else if (splitDbColumn[j] == "AjCourseId")
                            {
                                courseName = dtbFinalTable.Rows[i][j].ToString();
                            }
                            if (columnDataTyepSplit[j] == "int")
                            {
                                dynamicValueData += !string.IsNullOrEmpty(dtbFinalTable.Rows[i][j].ToString())
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "null";

                            }
                            else if (columnDataTyepSplit[j] == "bit")
                            {
                                if (!string.IsNullOrEmpty(dtbFinalTable.Rows[i][j].ToString()))
                                {
                                    dynamicValueData += dtbFinalTable.Rows[i][j].ToString() == "True"
                                                            ? dtbFinalTable.Rows[i][j].ToString()
                                                            : "false";
                                }
                                else
                                {
                                    dynamicValueData += false;
                                }
                            }
                            else
                            {
                                dynamicValueData += !string.IsNullOrEmpty(dtbFinalTable.Rows[i][j].ToString())
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "null";
                            }
                            dynamicValueData += "*";
                        }
                        dbColumnActualData = dbColumnActualData.TrimEnd(',');
                        dynamicValueData = dynamicValueData.TrimEnd('*');
                        ColumnDataType = ColumnDataType.TrimEnd('*');
                        dynamicValueData = dynamicValueData.Trim();
                        var errMsg = "";
                        var result = CollegeProvider.Instance.InsertCourseData(StrColumn, dynamicValueData, courseName,
                                                                               dbColumnActualData, collegeBranchName,
                                                                               universityName, ColumnDataType,
                                                                               out errMsg);

                        if (result > 0)
                            Sucess++;
                        else
                            FailureCount = FailureCount + "<br/>Row Number Of Failure Count: " + i + "Error: " + errMsg;

                        collegeBranchName = "";
                        dynamicValueData = "";
                        universityName = "";
                    }
                    listSucessCount.Add(new SucessCount {TotalNoRows = dtbFinalTable.Rows.Count.ToString()});
                    listSucessCount.Add(new SucessCount {TotalFailureCount = FailureCount});
                    listSucessCount.Add(new SucessCount {TotalSucessCount = Sucess.ToString()});

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing ImportCourse in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return listSucessCount;
        }

        [WebMethod]
        public List<SucessCount> ImportCourseStream(string _strTableName, string _strPrimaryKey, string fileName,
                                                    string Columns)
        {
            List<SucessCount> listSucessCount = new List<SucessCount>();
            int Sucess = 0;
            string FailureCount = "";
            ExcelToDataTable _objClsCSVToDatatable = new ExcelToDataTable();
            DataTable sourceTable = new DataTable();
            string FileColumnName = "",
                   collegeBranchName = "",
                   streamName = "",
                   DBColumnName = "",
                   DBDataType = "",
                   StrColumn = "",
                   dynamicValueData = "",
                   ColumnDataType = "",
                   dbColumnActualData = "";
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {

                    var SeprateColumns = Columns.Split(',');
                    DataTable dtbFinalTable = new DataTable();
                    dtbFinalTable = _objClsCSVToDatatable.GetRecordsForMappedColumnFromCSVToDataTable(_strTableName,
                                                                                                      _strPrimaryKey,
                                                                                                      fileName,
                                                                                                      SeprateColumns);
                    foreach (string strColumns in SeprateColumns)
                    {
                        var arrColumns = strColumns.Split('|');
                        FileColumnName = Convert.ToString(arrColumns[0]).Trim();
                        DBColumnName = Convert.ToString(arrColumns[1]).Trim();
                        DBDataType = Convert.ToString(arrColumns[2]).Trim();
                        StrColumn += DBColumnName + "*";
                        ColumnDataType += DBDataType + "*";
                        dbColumnActualData += DBColumnName + ",";
                    }

                    StrColumn = StrColumn.TrimEnd('*');
                    var columnDataTyepSplit = ColumnDataType.Split('*');
                    var splitDbColumn = dbColumnActualData.Split(',');
                    for (int i = 0; i < dtbFinalTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtbFinalTable.Columns.Count; j++)
                        {
                            if (splitDbColumn[j] == "AjCollegeBranchCourseId")
                            {
                                collegeBranchName = dtbFinalTable.Rows[i][j].ToString();
                            }
                            if (splitDbColumn[j] == "AjStreamId")
                            {
                                streamName = dtbFinalTable.Rows[i][j].ToString();
                            }
                            if (columnDataTyepSplit[j] == "int")
                            {
                                dynamicValueData += !string.IsNullOrEmpty(dtbFinalTable.Rows[i][j].ToString())
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "null";
                            }
                            else if (columnDataTyepSplit[j] == "bit")
                            {
                                if (!string.IsNullOrEmpty(dtbFinalTable.Rows[i][j].ToString()))
                                {
                                    dynamicValueData += dtbFinalTable.Rows[i][j].ToString() == "True"
                                                            ? dtbFinalTable.Rows[i][j].ToString()
                                                            : "false";
                                }
                                else
                                {
                                    dynamicValueData += false;
                                }
                            }
                            else
                            {
                                dynamicValueData += dtbFinalTable.Rows[i][j].ToString() != ""
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "null";
                            }
                            dynamicValueData += "*";
                        }
                        dbColumnActualData = dbColumnActualData.TrimEnd(',');
                        dynamicValueData = dynamicValueData.TrimEnd('*');
                        ColumnDataType = ColumnDataType.TrimEnd('*');
                        dynamicValueData = dynamicValueData.Trim();
                        var errMsg = "";
                        var result = CollegeProvider.Instance.InsertCourseStreamData(StrColumn, dynamicValueData,
                                                                                     dbColumnActualData,
                                                                                     collegeBranchName, streamName,
                                                                                     ColumnDataType, out errMsg);


                        if (result > 0)
                            Sucess++;
                        else
                            FailureCount = FailureCount + "<br/>Row Number Of Failure Count: " + i + "Error: " + errMsg;

                        collegeBranchName = "";
                        dynamicValueData = "";
                        streamName = "";
                    }
                    listSucessCount.Add(new SucessCount {TotalNoRows = dtbFinalTable.Rows.Count.ToString()});
                    listSucessCount.Add(new SucessCount {TotalFailureCount = FailureCount.ToString()});
                    listSucessCount.Add(new SucessCount {TotalSucessCount = Sucess.ToString()});

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing ImportCourse in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return listSucessCount;
        }

        [WebMethod]
        public List<SucessCount> ImportCourseExam(string _strTableName, string _strPrimaryKey, string fileName,
                                                  string Columns)
        {
            List<SucessCount> listSucessCount = new List<SucessCount>();
            int Sucess = 0;
            int FailureCount = 0;
            ExcelToDataTable _objClsCSVToDatatable = new ExcelToDataTable();
            DataTable sourceTable = new DataTable();
            string FileColumnName = "",
                   collegeBranchName = "",
                   examName = "",
                   DBColumnName = "",
                   DBDataType = "",
                   StrColumn = "",
                   dynamicValueData = "",
                   ColumnDataType = "",
                   dbColumnActualData = "";
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {

                    var SeprateColumns = Columns.Split(',');

                    DataTable dtbFinalTable = new DataTable();
                    dtbFinalTable = _objClsCSVToDatatable.GetRecordsForMappedColumnFromCSVToDataTable(_strTableName,
                                                                                                      _strPrimaryKey,
                                                                                                      fileName,
                                                                                                      SeprateColumns);
                    foreach (string strColumns in SeprateColumns)
                    {
                        var arrColumns = strColumns.Split('|');
                        FileColumnName = Convert.ToString(arrColumns[0]).Trim();
                        DBColumnName = Convert.ToString(arrColumns[1]).Trim();
                        DBDataType = Convert.ToString(arrColumns[2]).Trim();
                        StrColumn += DBColumnName + "*";
                        ColumnDataType += DBDataType + "*";
                        dbColumnActualData += DBColumnName + ",";
                    }

                    StrColumn = StrColumn.TrimEnd('*');
                    var columnDataTyepSplit = ColumnDataType.Split('*');
                    var splitDbColumn = dbColumnActualData.Split(',');
                    for (int i = 0; i < dtbFinalTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtbFinalTable.Columns.Count; j++)
                        {
                            if (splitDbColumn[j] == "AjCollegeBranchCourseId")
                            {
                                collegeBranchName = dtbFinalTable.Rows[i][j].ToString();
                            }
                            if (splitDbColumn[j] == "AjExamId")
                            {
                                examName = dtbFinalTable.Rows[i][j].ToString();
                            }
                            if (columnDataTyepSplit[j] == "int")
                            {
                                dynamicValueData += dtbFinalTable.Rows[i][j].ToString() != ""
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "null";
                            }
                            else if (columnDataTyepSplit[j] == "bit")
                            {
                                if (!string.IsNullOrEmpty(dtbFinalTable.Rows[i][j].ToString()))
                                {
                                    dynamicValueData += dtbFinalTable.Rows[i][j].ToString() == "True"
                                                            ? dtbFinalTable.Rows[i][j].ToString()
                                                            : "false";
                                }
                                else
                                {
                                    dynamicValueData += false;
                                }

                            }
                            else
                            {
                                dynamicValueData += dtbFinalTable.Rows[i][j].ToString() != ""
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "null";
                            }
                            dynamicValueData += "*";
                        }
                        dbColumnActualData = dbColumnActualData.TrimEnd(',');
                        dynamicValueData = dynamicValueData.TrimEnd('*');
                        ColumnDataType = ColumnDataType.TrimEnd('*');
                        dynamicValueData = dynamicValueData.Trim();
                        var errMsg = "";
                        var result = CollegeProvider.Instance.InsertCourseExamData(StrColumn, dynamicValueData,
                                                                                   dbColumnActualData, collegeBranchName,
                                                                                   examName, ColumnDataType, out errMsg);
                        int tempCount = result > 0 ? Sucess++ : FailureCount++;
                        listSucessCount.Add(new SucessCount {TotalNoRows = dtbFinalTable.Rows.Count.ToString()});
                        listSucessCount.Add(new SucessCount {TotalFailureCount = FailureCount.ToString()});
                        listSucessCount.Add(new SucessCount {TotalSucessCount = Sucess.ToString()});
                        collegeBranchName = "";
                        dynamicValueData = "";
                        examName = "";
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
                const string addInfo = "Error while executing ImportCourse in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return listSucessCount;
        }

        [WebMethod]
        public List<SucessCount> ImportCourseFacality(string _strTableName, string _strPrimaryKey, string fileName,
                                                      string Columns)
        {

            List<SucessCount> listSucessCount = new List<SucessCount>();
            int Sucess = 0;
            int FailureCount = 0;
            ExcelToDataTable _objClsCSVToDatatable = new ExcelToDataTable();
            DataTable sourceTable = new DataTable();
            string FileColumnName = "",
                   collegeBranchName = "",
                   examName = "",
                   DBColumnName = "",
                   DBDataType = "",
                   StrColumn = "",
                   dynamicValueData = "",
                   ColumnDataType = "",
                   dbColumnActualData = "";
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {

                    var SeprateColumns = Columns.Split(',');
                    DataTable dtbFinalTable = new DataTable();
                    dtbFinalTable = _objClsCSVToDatatable.GetRecordsForMappedColumnFromCSVToDataTable(_strTableName,
                                                                                                      _strPrimaryKey,
                                                                                                      fileName,
                                                                                                      SeprateColumns);
                    foreach (string strColumns in SeprateColumns)
                    {
                        var arrColumns = strColumns.Split('|');
                        FileColumnName = Convert.ToString(arrColumns[0]).Trim();
                        DBColumnName = Convert.ToString(arrColumns[1]).Trim();
                        DBDataType = Convert.ToString(arrColumns[2]).Trim();
                        StrColumn += DBColumnName + "*";
                        ColumnDataType += DBDataType + "*";
                        dbColumnActualData += DBColumnName + ",";
                    }

                    StrColumn = StrColumn.TrimEnd('*');
                    var columnDataTyepSplit = ColumnDataType.Split('*');
                    for (int i = 0; i < dtbFinalTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtbFinalTable.Columns.Count; j++)
                        {
                            if (columnDataTyepSplit[j] == "int")
                            {
                                dynamicValueData += dtbFinalTable.Rows[i][j].ToString() != ""
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "0";
                            }
                            else if (columnDataTyepSplit[j] == "bit")
                            {
                                dynamicValueData += dtbFinalTable.Rows[i][j].ToString() == "True"
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "false";
                            }
                            else
                            {
                                dynamicValueData += dtbFinalTable.Rows[i][j].ToString() != ""
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "N/A";
                            }
                            dynamicValueData += "*";
                        }
                        dbColumnActualData = dbColumnActualData.TrimEnd(',');
                        collegeBranchName = dtbFinalTable.Rows[i][0].ToString();
                        examName = dtbFinalTable.Rows[i][2].ToString();
                        dynamicValueData = dynamicValueData.TrimEnd('*');
                        ColumnDataType = ColumnDataType.TrimEnd('*');
                        dynamicValueData = dynamicValueData.Trim();
                        var errMsg = "";
                        var result = CollegeProvider.Instance.InsertCourseFacalityData(StrColumn, dynamicValueData,
                                                                                       dbColumnActualData,
                                                                                       collegeBranchName, ColumnDataType,
                                                                                       out errMsg);
                        int tempCount = result > 0 ? Sucess++ : FailureCount++;
                        listSucessCount.Add(new SucessCount {TotalNoRows = dtbFinalTable.Rows.Count.ToString()});
                        listSucessCount.Add(new SucessCount {TotalFailureCount = FailureCount.ToString()});
                        listSucessCount.Add(new SucessCount {TotalSucessCount = Sucess.ToString()});
                        collegeBranchName = "";
                        dynamicValueData = "";
                        examName = "";
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
                const string addInfo = "Error while executing ImportCourse in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return listSucessCount;
        }

        [WebMethod]
        public List<SucessCount> ImportCourseHighLights(string _strTableName, string _strPrimaryKey, string fileName,
                                                        string Columns)
        {
            List<SucessCount> listSucessCount = new List<SucessCount>();
            int Sucess = 0;
            int FailureCount = 0;
            ExcelToDataTable _objClsCSVToDatatable = new ExcelToDataTable();
            DataTable sourceTable = new DataTable();
            string FileColumnName = "",
                   collegeBranchName = "",
                   DBColumnName = "",
                   DBDataType = "",
                   StrColumn = "",
                   dynamicValueData = "",
                   ColumnDataType = "",
                   dbColumnActualData = "";
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {

                    var SeprateColumns = Columns.Split(',');
                    DataTable dtbFinalTable = new DataTable();
                    dtbFinalTable = _objClsCSVToDatatable.GetRecordsForMappedColumnFromCSVToDataTable(_strTableName,
                                                                                                      _strPrimaryKey,
                                                                                                      fileName,
                                                                                                      SeprateColumns);
                    foreach (string strColumns in SeprateColumns)
                    {
                        var arrColumns = strColumns.Split('|');
                        FileColumnName = Convert.ToString(arrColumns[0]).Trim();
                        DBColumnName = Convert.ToString(arrColumns[1]).Trim();
                        DBDataType = Convert.ToString(arrColumns[2]).Trim();
                        StrColumn += DBColumnName + "*";
                        ColumnDataType += DBDataType + "*";
                        dbColumnActualData += DBColumnName + ",";
                    }

                    StrColumn = StrColumn.TrimEnd('*');
                    var columnDataTyepSplit = ColumnDataType.Split('*');
                    for (int i = 0; i < dtbFinalTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtbFinalTable.Columns.Count; j++)
                        {
                            if (columnDataTyepSplit[j] == "int")
                            {
                                dynamicValueData += dtbFinalTable.Rows[i][j].ToString() != ""
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "0";
                            }
                            else if (columnDataTyepSplit[j] == "bit")
                            {
                                dynamicValueData += dtbFinalTable.Rows[i][j].ToString() == "True"
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "false";
                            }
                            else
                            {
                                dynamicValueData += dtbFinalTable.Rows[i][j].ToString() != ""
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "N/A";
                            }
                            dynamicValueData += "*";
                        }
                        dbColumnActualData = dbColumnActualData.TrimEnd(',');
                        collegeBranchName = dtbFinalTable.Rows[i][0].ToString();
                        dynamicValueData = dynamicValueData.TrimEnd('*');
                        ColumnDataType = ColumnDataType.TrimEnd('*');
                        dynamicValueData = dynamicValueData.Trim();
                        var errMsg = "";
                        var result = CollegeProvider.Instance.InsertCourseHighLightsData(StrColumn, dynamicValueData,
                                                                                         dbColumnActualData,
                                                                                         collegeBranchName,
                                                                                         ColumnDataType, out errMsg);

                        int tempCount = result > 0 ? Sucess++ : FailureCount++;
                        listSucessCount.Add(new SucessCount {TotalNoRows = dtbFinalTable.Rows.Count.ToString()});
                        listSucessCount.Add(new SucessCount {TotalFailureCount = FailureCount.ToString()});
                        listSucessCount.Add(new SucessCount {TotalSucessCount = Sucess.ToString()});
                        collegeBranchName = "";
                        dynamicValueData = "";

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
                const string addInfo = "Error while executing ImportCourse in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return listSucessCount;
        }

        [WebMethod]
        public List<SucessCount> ImportCourseRankSource(string _strTableName, string _strPrimaryKey, string fileName,
                                                        string Columns)
        {

            List<SucessCount> listSucessCount = new List<SucessCount>();
            int Sucess = 0;
            int FailureCount = 0;
            ExcelToDataTable _objClsCSVToDatatable = new ExcelToDataTable();
            DataTable sourceTable = new DataTable();
            string CourseName = "";
            string FileColumnName = "",
                   collegeBranchName = "",
                   DBColumnName = "",
                   DBDataType = "",
                   StrColumn = "",
                   dynamicValueData = "",
                   ColumnDataType = "",
                   dbColumnActualData = "";
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {

                    var SeprateColumns = Columns.Split(',');
                    DataTable dtbFinalTable = new DataTable();
                    dtbFinalTable = _objClsCSVToDatatable.GetRecordsForMappedColumnFromCSVToDataTable(_strTableName,
                                                                                                      _strPrimaryKey,
                                                                                                      fileName,
                                                                                                      SeprateColumns);
                    foreach (string strColumns in SeprateColumns)
                    {
                        var arrColumns = strColumns.Split('|');
                        FileColumnName = Convert.ToString(arrColumns[0]).Trim();
                        DBColumnName = Convert.ToString(arrColumns[1]).Trim();
                        DBDataType = Convert.ToString(arrColumns[2]).Trim();
                        StrColumn += DBColumnName + "*";
                        ColumnDataType += DBDataType + "*";
                        dbColumnActualData += DBColumnName + ",";
                    }

                    StrColumn = StrColumn.TrimEnd('*');
                    var columnDataTyepSplit = ColumnDataType.Split('*');
                    var splitDbColumn = dbColumnActualData.Split(',');
                    for (int i = 0; i < dtbFinalTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtbFinalTable.Columns.Count; j++)
                        {
                            if (splitDbColumn[j] == "AjCollegeBranchCourseId")
                            {
                                collegeBranchName = dtbFinalTable.Rows[i][j].ToString();
                            }
                            if (splitDbColumn[j] == "AjCourseId")
                            {
                                CourseName = dtbFinalTable.Rows[i][j].ToString();
                            }
                            if (columnDataTyepSplit[j] == "int")
                            {
                                dynamicValueData += dtbFinalTable.Rows[i][j].ToString() != ""
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "0";
                            }
                            else if (columnDataTyepSplit[j] == "bit")
                            {
                                dynamicValueData += dtbFinalTable.Rows[i][j].ToString() == "True"
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "false";
                            }
                            else
                            {
                                dynamicValueData += dtbFinalTable.Rows[i][j].ToString() != ""
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "N/A";
                            }
                            dynamicValueData += "*";
                        }
                        dbColumnActualData = dbColumnActualData.TrimEnd(',');
                        dynamicValueData = dynamicValueData.TrimEnd('*');
                        ColumnDataType = ColumnDataType.TrimEnd('*');
                        dynamicValueData = dynamicValueData.Trim();
                        var errMsg = "";
                        var result = CollegeProvider.Instance.InsertCourseRankSourceData(StrColumn, dynamicValueData,
                                                                                         dbColumnActualData,
                                                                                         collegeBranchName,
                                                                                         ColumnDataType, out errMsg,
                                                                                         CourseName);
                        int tempCount = result > 0 ? Sucess++ : FailureCount++;
                        listSucessCount.Add(new SucessCount {TotalNoRows = dtbFinalTable.Rows.Count.ToString()});
                        listSucessCount.Add(new SucessCount {TotalFailureCount = FailureCount.ToString()});
                        listSucessCount.Add(new SucessCount {TotalSucessCount = Sucess.ToString()});
                        collegeBranchName = "";
                        dynamicValueData = "";

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
                const string addInfo = "Error while executing ImportCourse in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return listSucessCount;
        }

        [WebMethod]
        public List<SucessCount> ImportCourseHostel(string _strTableName, string _strPrimaryKey, string fileName,
                                                    string Columns)
        {
            List<SucessCount> listSucessCount = new List<SucessCount>();
            int Sucess = 0;
            int FailureCount = 0;
            ExcelToDataTable _objClsCSVToDatatable = new ExcelToDataTable();
            DataTable sourceTable = new DataTable();
            string CourseName = "";
            string FileColumnName = "",
                   collegeBranchName = "",
                   DBColumnName = "",
                   DBDataType = "",
                   StrColumn = "",
                   dynamicValueData = "",
                   ColumnDataType = "",
                   dbColumnActualData = "";
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {

                    var SeprateColumns = Columns.Split(',');
                    DataTable dtbFinalTable = new DataTable();
                    dtbFinalTable = _objClsCSVToDatatable.GetRecordsForMappedColumnFromCSVToDataTable(_strTableName,
                                                                                                      _strPrimaryKey,
                                                                                                      fileName,
                                                                                                      SeprateColumns);
                    foreach (string strColumns in SeprateColumns)
                    {
                        var arrColumns = strColumns.Split('|');
                        FileColumnName = Convert.ToString(arrColumns[0]).Trim();
                        DBColumnName = Convert.ToString(arrColumns[1]).Trim();
                        DBDataType = Convert.ToString(arrColumns[2]).Trim();
                        StrColumn += DBColumnName + "*";
                        ColumnDataType += DBDataType + "*";
                        dbColumnActualData += DBColumnName + ",";
                    }

                    StrColumn = StrColumn.TrimEnd('*');
                    var columnDataTyepSplit = ColumnDataType.Split('*');
                    var splitDbColumn = dbColumnActualData.Split(',');
                    for (int i = 0; i < dtbFinalTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtbFinalTable.Columns.Count; j++)
                        {
                            if (splitDbColumn[j] == "AjCollegeBranchCourseId")
                            {
                                collegeBranchName = dtbFinalTable.Rows[i][j].ToString();
                            }
                            if (splitDbColumn[j] == "AjCourseId")
                            {
                                CourseName = dtbFinalTable.Rows[i][j].ToString();
                            }
                            if (columnDataTyepSplit[j] == "int")
                            {
                                dynamicValueData += dtbFinalTable.Rows[i][j].ToString() != ""
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "0";
                            }
                            else if (columnDataTyepSplit[j] == "bit")
                            {
                                dynamicValueData += dtbFinalTable.Rows[i][j].ToString() == "True"
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "false";
                            }
                            else
                            {
                                dynamicValueData += dtbFinalTable.Rows[i][j].ToString() != ""
                                                        ? dtbFinalTable.Rows[i][j].ToString()
                                                        : "N/A";
                            }
                            dynamicValueData += "*";
                        }
                        dbColumnActualData = dbColumnActualData.TrimEnd(',');
                        dynamicValueData = dynamicValueData.TrimEnd('*');
                        ColumnDataType = ColumnDataType.TrimEnd('*');
                        dynamicValueData = dynamicValueData.Trim();
                        var errMsg = "";
                        var result = CollegeProvider.Instance.InsertCourseHostel(StrColumn, dynamicValueData,
                                                                                 dbColumnActualData, collegeBranchName,
                                                                                 ColumnDataType, out errMsg, CourseName);
                        int tempCount = result > 0 ? Sucess++ : FailureCount++;
                        listSucessCount.Add(new SucessCount {TotalNoRows = dtbFinalTable.Rows.Count.ToString()});
                        listSucessCount.Add(new SucessCount {TotalFailureCount = FailureCount.ToString()});
                        listSucessCount.Add(new SucessCount {TotalSucessCount = Sucess.ToString()});
                        collegeBranchName = "";
                        dynamicValueData = "";

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
                const string addInfo = "Error while executing ImportCourse in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return listSucessCount;
        }

        public class CollegeData
        {
            public int CollegeId { get; set; }
            public string ErrMsg { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public string InserHostelDetails(string colegeBranchId, string hostelId, string collegeHostelLocation,
                                         string collegeHostelPower, string collegeCharge, bool collegeHostelStatus,
                                         string collegeHostelAc, string collegeHostelInternet, string collegeLoundary)
        {
            var objSecurePage = new SecurePage();
            var objCollegeBranchCourseHostelProperty = new CollegeBranchCourseHostelProperty
                {
                    HostelCategoryId = Convert.ToInt16(hostelId),
                    CollegeBranchCourseHostelCharge = collegeCharge,
                    CollegeBranchCourseHostelLocation = collegeHostelLocation,
                    CollegeBranchCourseId = Convert.ToInt16(colegeBranchId),
                    CollegeBranchCourseHostelStatus = collegeHostelStatus,
                    IsCollegeBranchCourseHostelHasAC =
                        collegeHostelAc == "0" ? true : false,
                    IsCollegeBranchCourseHostelHasInternet =
                        collegeHostelInternet == "0" ? true : false,
                    IsCollegeBranchCourseHostelHasLoundry =
                        collegeLoundary == "0" ? true : false,
                    IsCollegeBranchCourseHostelHasPowerBackup = true,

                };
            var errMsg = "";
            var collegeStatus =
                CollegeProvider.Instance.InsertCollegeBranchHostelInfo(objCollegeBranchCourseHostelProperty, 1,
                                                                       out errMsg);
            if (collegeStatus <= 0) return null;
            return errMsg;
        }

        [WebMethod(EnableSession = true)]
        public CourseBranchData[] InsertCollegeBranchCourseDetails(string colegeBranchId, string courseId,
                                                                   string universityId, string title, string metatTag,
                                                                   string url, string metaDesc, string courseEst,
                                                                   bool status, string description)
        {
            var objSecurePage = new SecurePage();
            var objCollegeBranchCourseProperty = new CollegeBranchCourseProperty
                {
                    CollegeBranchId = Convert.ToInt16(colegeBranchId),
                    CourseId = Convert.ToInt16(courseId),
                    UniversityId = Convert.ToInt16(universityId),
                    CollegeBranchCourseMetaTag = metatTag,
                    CollegeBranchCourseUrl = url,
                    CollegeBranchCourseTitle = title,
                    CollegeBranchCourseMetaDesc = metaDesc,
                    CollegeBranchCourseEst = courseEst,
                    CollegeBranchCourseStatus = status,
                    CollegeBranchCourseDesc = description,
                };
            var errMsg = "";
            int courseBranchId;
            var objCollegeCourseData = new List<CourseBranchData>();
            var objCollegeCourse = new CourseBranchData();
            var collegeStatus = CollegeProvider.Instance.InsertCollegeBranchCourseInfo(objCollegeBranchCourseProperty, 1,
                                                                                       out errMsg, out courseBranchId);
            if (collegeStatus <= 0) return objCollegeCourseData.ToArray();
            objCollegeCourse.CourseBranchId = courseBranchId;
            objCollegeCourse.ErrMsg = errMsg;
            objCollegeCourseData.Add(objCollegeCourse);
            return objCollegeCourseData.ToArray();
        }

        public class CourseBranchData
        {
            public int CourseBranchId { get; set; }
            public string ErrMsg { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public string InsertCollegeBranchStreamDetails(string courseBranchId, string streamId, int streamMode,
                                                       string streamSeat, string duration, string fees,
                                                       string eligibilty, string quotaSeat, string lateralSeat,
                                                       string streamStatus, string descrition)
        {
            var objSecurePage = new SecurePage();
            var objCollegeBranchCourseStreamProperty = new CollegeBranchCourseStreamProperty
                {
                    CollegeBranchCourseId = Convert.ToInt16(courseBranchId),
                    StreamId = Convert.ToInt16(streamId),
                    CollegeBranchCourseStreamModeId = streamMode,
                    CollegeBranchCourseStreamSeat = streamSeat,
                    CollegeBranchCourseStreamFees = fees,
                    CollegeBranchCourseStreamEligibity = eligibilty,
                    CollegeBranchCourseStreamDesc = descrition,
                    CollegeBranchCourseStreamStatus =
                        streamStatus == "0" ? true : false,
                    CollegeBranchCourseStreamLateralEntrySeat = lateralSeat,
                    CollegeBranchCourseStreamManagementQuotaSeat = quotaSeat,
                    CollegeBranchCourseStreamDuration = duration
                };
            var errMsg = "";
            var collegeStatus =
                CollegeProvider.Instance.InsertCollegeBranchCourseStreamInfo(objCollegeBranchCourseStreamProperty, 1,
                                                                             out errMsg);
            return collegeStatus <= 0 ? null : errMsg;
        }

        [WebMethod(EnableSession = true)]
        public string InsertCollegeBranchExamDetails(string courseBranchId, string examId, bool examStatus)
        {
            var objSecurePage = new SecurePage();
            var objCollegeBranchCourseExamProperty = new CollegeBranchCourseExamProperty
                {
                    CollegeBranchCourseId = Convert.ToInt16(courseBranchId),
                    ExamId = Convert.ToInt16(examId),

                    CollegeCourseExamStatus = examStatus

                };
            var errMsg = "";
            var collegeStatus =
                CollegeProvider.Instance.InsertCollegeBranchCourseExamInfo(objCollegeBranchCourseExamProperty, 1,
                                                                           out errMsg);
            return collegeStatus <= 0 ? null : errMsg;
        }

        [WebMethod(EnableSession = true)]
        public string InsertCollegeBranchFacalityDetails(string courseBranchId, string facality, bool facalityStatus,
                                                         string facalityDesc)
        {
            var objSecurePage = new SecurePage();
            var objCollegeBranchCourseFacalityProperty = new CollegeBranchCourseFacilitiesProperty
                {
                    CollegeBranchCourseId = Convert.ToInt16(courseBranchId),
                    CollegeBranchCourseFacilitieName = facality,
                    CollegeBranchCourseFacilitieDesc = facalityDesc,
                    CollegeBranchCourseFacilitieStatus = facalityStatus
                };
            var errMsg = "";
            var collegeStatus =
                CollegeProvider.Instance.InsertCollegeBranchCourseFacilities(objCollegeBranchCourseFacalityProperty, 1,
                                                                             out errMsg);
            return collegeStatus <= 0 ? null : errMsg;
        }

        [WebMethod(EnableSession = true)]
        public string InsertCollegeBranchHighLightsDetails(string courseBranchId, string highlights,
                                                           bool highlightsStatus)
        {
            var objSecurePage = new SecurePage();
            var objCollegeBranchCourseHighLightsProperty = new CollegeBranchCourseHighlightsProperty
                {
                    CollegeBranchCourseId =
                        Convert.ToInt16(courseBranchId),
                    CollegeBranchCourseHighlight = highlights,
                    CollegeBranchCourseHighlightStatus = highlightsStatus
                };
            var errMsg = "";
            var collegeStatus =
                CollegeProvider.Instance.InsertCollegeBranchCourseHighlights(objCollegeBranchCourseHighLightsProperty, 1,
                                                                             out errMsg);
            return collegeStatus <= 0 ? null : errMsg;
        }

        [WebMethod(EnableSession = true)]
        public string InsertCollegeBranchRankSouceDetails(string courseBranchId, string rankSource, string souceYear,
                                                          string rankOverAll, bool rankStatus)
        {
            var objSecurePage = new SecurePage();
            var objCollegeBranchRankProperty = new CollegeBranchRankProperty
                {
                    CollegeBranchCourseId = Convert.ToInt16(courseBranchId),
                    CollegeRankSourceId = Convert.ToInt16(rankSource),
                    CollegeRankYear = Convert.ToInt16(souceYear),
                    CollegeOverAllRank = rankOverAll,
                    CollegeRankStatus = rankStatus
                };
            var errMsg = "";
            var collegeStatus = CollegeProvider.Instance.InsertCollegeBranchRank(objCollegeBranchRankProperty, 1,
                                                                                 out errMsg);
            return collegeStatus <= 0 ? null : errMsg;
        }

        [WebMethod(EnableSession = true)]
        public string InsertCollegeHostelDetails(string courseBranchId, string hostelCategory, string location,
                                                 string charge, bool hostelStatus, string ac, string loundary,
                                                 string internet, string powerBackUp)
        {
            var objSecurePage = new SecurePage();
            var objCollegeBranchCourseHostelProperty = new CollegeBranchCourseHostelProperty
                {
                    CollegeBranchCourseId = Convert.ToInt16(courseBranchId),
                    HostelCategoryId = Convert.ToInt16(hostelCategory),
                    CollegeBranchCourseHostelLocation = location,
                    CollegeBranchCourseHostelCharge = charge,
                    CollegeBranchCourseHostelStatus = hostelStatus,
                    IsCollegeBranchCourseHostelHasInternet =
                        ac == "0" ? true : false,
                    IsCollegeBranchCourseHostelHasPowerBackup =
                        powerBackUp == "0" ? true : false,
                    IsCollegeBranchCourseHostelHasAC =
                        ac == "0" ? true : false,
                    IsCollegeBranchCourseHostelHasLoundry =
                        loundary == "0" ? true : false
                };
            var errMsg = "";
            var collegeStatus =
                CollegeProvider.Instance.InsertCollegeBranchHostelInfo(objCollegeBranchCourseHostelProperty, 1,
                                                                       out errMsg);
            return collegeStatus <= 0 ? null : errMsg;
        }

        [WebMethod(EnableSession = true)]
        public string InsertCollegeGroup(string collegeGroup)
        {
            string errMsg;
            CollegeGroupProperty _objCollegeGroupProperty = new CollegeGroupProperty
                {
                    CollegeGroupName = collegeGroup,

                };
            var result = CollegeProvider.Instance.InsertCollegeGroupDetails(_objCollegeGroupProperty,
                                                                            new SecurePage().LoggedInUserId, out errMsg);
            return errMsg;
        }

        [WebMethod(EnableSession = true)]
        public GridDataSet FetchData(int pageNumber, int pageSize)
        {
            return new GridDataSet
                {
                    PageNumber = pageNumber,
                    TotalRecords = CourseProvider.Instance.GetAllCourseCategoryList().Count,
                    PageSize = pageSize,
                    MessageList =
                        CourseProvider.Instance.GetAllCourseCategoryList()
                                      .Skip(pageSize*pageNumber)
                                      .Take(pageSize)
                                      .ToList<CourseCategoryProperty>()
                };
        }


        [WebMethod]
        public string SaveStudentExamQuery(string name, string emailId, string mobileNo, string cityName,
                                           string courseId, string examId, string query, string examName)
        {
            Query objQuery = new Query();
            MailTemplates _objClsMailTemplete = new MailTemplates();
            _objCommon = new Common();
            string Msg = "";
            if (Utils.IsEmailValid(emailId))
            {
                if (Utils.IsMobileValid(mobileNo))
                {
                    QueryProperty objQueryProperty = new QueryProperty
                        {
                            StudentName = name,
                            UserEmailId = emailId,
                            UserMobileNo = mobileNo,
                            StudentCityName = cityName,
                            StudentCourseId = Convert.ToInt32(courseId),
                            StudentSourceId = Convert.ToInt32(examId),
                            StudentQuery = query
                        };
                    var result = QueryProvider.Instance.InsertExamQuickQuery(objQueryProperty, out Msg);
                    var courseData = CourseProvider.Instance.GetCourseById(Convert.ToInt32(courseId));
                    new com.admissionjankari.crm.CommonServices().ImportAdmissionJankariLeads(0, mobileNo,
                                                                                              name,
                                                                                              emailId,
                                                                                              courseData.First()
                                                                                                        .CourseName,
                                                                                              examName, null,
                                                                                              cityName
                                                                                              , null,
                                                                                              "Exam Query:-" + query,
                                                                                              true);
                    if (result == 2)
                    {
                        var ObjMail = new MailMessage
                            {
                                From = new MailAddress(ApplicationSettings.Instance.Email),
                                Subject = "AdmissionJankari:Registration"
                            };
                        var mailbody = _objClsMailTemplete.MailBodyForRegistation(name, emailId, mobileNo);
                        ObjMail.Body = mailbody;
                        ObjMail.To.Add(objQueryProperty.UserEmailId);
                        ObjMail.IsBodyHtml = true;
                        Utils.SendMailMessageAsync(ObjMail);
                    }

                    var courseDetails = CourseProvider.Instance.GetCourseById(Convert.ToInt16(courseId));
                    var courseQuery = courseDetails.First();
                    var mail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "AdmissionJankari: ExamQuery "
                        };
                    var body = _objClsMailTemplete.MailBodyForExamQuery(name, courseQuery.CourseName, query, mobileNo,
                                                                        examName);
                    mail.Body = body;
                    mail.To.Add(objQueryProperty.UserEmailId);
                    mail.IsBodyHtml = true;
                    Utils.SendMailMessageAsync(mail);
                    return Msg;
                }
                else
                {
                    return Msg = _objCommon.GetValidationMessage("revEmail");
                }
            }
            else
            {
                return Msg = _objCommon.GetValidationMessage("revEmail");
            }
        }

        public class GridDataSet
        {
            public int PageNumber { get; set; }
            public int TotalRecords { get; set; }
            public int PageSize { get; set; }
            public List<CourseCategoryProperty> MessageList { get; set; }
        }

        [WebMethod]
        public string GetBankName()
        {
            string BankName = "";
            var data = BankProvider.Instance.GetAllBankList();
            if (data.Count > 0)
            {
                var bankExamList = (from test in data select test.BankName).ToArray();
                BankName = String.Join(",", bankExamList);
            }

            return BankName;
        }

        // Method to get The Bank List

        [WebMethod]
        public ListItem[] BankList()
        {
            var data = BankProvider.Instance.GetAllBankList();
            if (data != null && data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.BankName,
                        Value =
                            result.BankId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;
        }

        public class SucessCount
        {
            public string TotalNoRows;
            public string TotalSucessCount;
            public string TotalFailureCount;
        }

        [WebMethod(EnableSession = true)]
        public string InsertQueryCallFromInstitute(string name, string email, string number, string query,
                                                   string cityName, int collegeCourseId, int courseId,
                                                   string collegeBranchName)
        {
            var objClsMailTemplete = new MailTemplates();
            var errMsg = "";
            try
            {
                if (Utils.IsEmailValid(email))
                {
                    if (Utils.IsMobileValid(number))
                    {
                        var objQueryProperty = new QueryProperty
                            {
                                UserEmailId = email,
                                StudentName = name,
                                UserMobileNo = number,
                                StudentCityName = cityName,
                                StudentSourceId = Convert.ToInt32(collegeCourseId),
                                StudentCourseId = Convert.ToInt32(courseId),
                                StudentQuery = query

                            };
                        var result1 = UserManagerProvider.Instance.GetUserListByEmailId(email);

                        if (result1.Count > 0)
                        {
                            var _objSecurePage = new SecurePage();
                            if (result1.First().UserCategoryName == "College")
                            {

                                if (result1.First().UserStatus != false)
                                {
                                    var query1 = result1.First();
                                    _objSecurePage.LoggedInUserId = query1.UserId;
                                    _objSecurePage.LoggedInUserType = query1.UserCategoryId;
                                    _objSecurePage.LoggedInUserEmailId = query1.UserEmailid;
                                    _objSecurePage.LoggedInUserName = query1.UserFullName;
                                    _objSecurePage.LoggedInUserMobile = query1.MobileNo;
                                }

                            }
                            else
                            {
                                var query1 = result1.First();
                                _objSecurePage.LoggedInUserId = query1.UserId;
                                _objSecurePage.LoggedInUserType = query1.UserCategoryId;
                                _objSecurePage.LoggedInUserEmailId = query1.UserEmailid;
                                _objSecurePage.LoggedInUserName = query1.UserFullName;
                                _objSecurePage.LoggedInUserMobile = query1.MobileNo;
                            }
                        }

                        var result = QueryProvider.Instance.InsertCollegeQuickQuery(objQueryProperty, out errMsg);
                        var courseData = CourseProvider.Instance.GetCourseById(Convert.ToInt32(courseId));
                        new com.admissionjankari.crm.CommonServices().ImportAdmissionJankariLeads(0, number,
                                                                                                  name,
                                                                                                  email,
                                                                                                  courseData.First()
                                                                                                            .CourseName,
                                                                                                  null, null,
                                                                                                  cityName
                                                                                                  , collegeBranchName,
                                                                                                  "College Query:-" +
                                                                                                  query, true);
                        if (result == 2)
                        {
                            var objMail = new MailMessage
                                {
                                    From = new MailAddress(ApplicationSettings.Instance.Email),
                                    Subject = "AdmissionJankari: Registration Mail"
                                };
                            var mailbody = objClsMailTemplete.MailBodyForRegistation(name, email, number);
                            objMail.Body = mailbody;
                            objMail.To.Add(objQueryProperty.UserEmailId);
                            objMail.IsBodyHtml = true;
                            Utils.SendMailMessageAsync(objMail);
                        }

                        var courseDetails = CourseProvider.Instance.GetCourseById(courseId);
                        var courseQuery = courseDetails.First();
                        var mail = new MailMessage
                            {
                                From = new MailAddress(ApplicationSettings.Instance.Email),
                                Subject = "Admissionjankari Call from Institute for : " + collegeBranchName
                            };
                        var body = objClsMailTemplete.MailBodyForCallFromInstitute(name, collegeBranchName);
                        mail.Body = body;
                        mail.To.Add(objQueryProperty.UserEmailId);
                        mail.IsBodyHtml = true;
                        Utils.SendMailMessageAsync(mail);


                        mail = new MailMessage
                            {
                                From = new MailAddress(ApplicationSettings.Instance.Email),
                                Subject = "Admissionjankari Call from Institute for : " + collegeBranchName
                            };
                        body = objClsMailTemplete.MailBodyForCallFromInstituteForAdmin(name, email, number,
                                                                                       courseQuery.CourseName,
                                                                                       collegeBranchName);
                        mail.Body = body;
                        mail.To.Add(ClsSingelton.bccDirectAdmission);
                        mail.IsBodyHtml = true;
                        Utils.SendMailMessageAsync(mail);
                    }
                    else
                    {
                        errMsg = "Mobile number is not valid";
                    }
                }
                else
                {
                    errMsg = "Email Id is not valid";
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
                    "Error while executing InsertCommonQuickQuery in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

            return errMsg;
        }

        [WebMethod(EnableSession = true)]
        public string InsertCollegeQuickQuery(string name, string email, string number, string query, string cityName,
                                              int collegeCourseId, int courseId, string collegeBranchName)
        {
            var objClsMailTemplete = new MailTemplates();
            var errMsg = "";
            _objCommon = new Common();
            try
            {
                if (Utils.IsEmailValid(email))
                {
                    if (Utils.IsMobileValid(number))
                    {
                        var objQueryProperty = new QueryProperty
                            {
                                UserEmailId = email,
                                StudentName = name,
                                UserMobileNo = number,
                                StudentCityName = cityName,
                                StudentSourceId = Convert.ToInt32(collegeCourseId),
                                StudentCourseId = Convert.ToInt32(courseId),
                                StudentQuery = query

                            };
                        var result1 = UserManagerProvider.Instance.GetUserListByEmailId(email);
                        if (result1.Count > 0)
                        {
                            var _objSecurePage = new SecurePage();
                            if (result1.First().UserCategoryName == "College")
                            {

                                if (result1.First().UserStatus != false)
                                {
                                    var query1 = result1.First();
                                    _objSecurePage.LoggedInUserId = query1.UserId;
                                    _objSecurePage.LoggedInUserType = query1.UserCategoryId;
                                    _objSecurePage.LoggedInUserEmailId = query1.UserEmailid;
                                    _objSecurePage.LoggedInUserName = query1.UserFullName;
                                    _objSecurePage.LoggedInUserMobile = query1.MobileNo;
                                }

                            }
                            else
                            {
                                var query1 = result1.First();
                                _objSecurePage.LoggedInUserId = query1.UserId;
                                _objSecurePage.LoggedInUserType = query1.UserCategoryId;
                                _objSecurePage.LoggedInUserEmailId = query1.UserEmailid;
                                _objSecurePage.LoggedInUserName = query1.UserFullName;
                                _objSecurePage.LoggedInUserMobile = query1.MobileNo;
                            }
                        }

                        var result = QueryProvider.Instance.InsertCollegeQuickQuery(objQueryProperty, out errMsg);
                        var courseData = CourseProvider.Instance.GetCourseById(Convert.ToInt32(courseId));
                        new com.admissionjankari.crm.CommonServices().ImportAdmissionJankariLeads(0, number,
                                                                                                  name,
                                                                                                  email,
                                                                                                  courseData.First()
                                                                                                            .CourseName,
                                                                                                  null, null,
                                                                                                  cityName
                                                                                                  , collegeBranchName,
                                                                                                  "College Query:-" +
                                                                                                  query, true);
                        if (result == 2)
                        {
                            var objMail = new MailMessage
                                {
                                    From = new MailAddress(ApplicationSettings.Instance.Email),
                                    Subject = "AdmissionJankari: Registration mail"
                                };
                            var mailbody = objClsMailTemplete.MailBodyForRegistation(name, email, number);
                            objMail.Body = mailbody;
                            objMail.To.Add(objQueryProperty.UserEmailId);
                            objMail.IsBodyHtml = true;
                            Utils.SendMailMessageAsync(objMail);
                        }

                        var courseDetails = CourseProvider.Instance.GetCourseById(courseId);
                        var courseQuery = courseDetails.First();
                        var mail = new MailMessage
                            {
                                From = new MailAddress(ApplicationSettings.Instance.Email),
                                Subject = "AdmissionJankari:College Query "
                            };
                        var body = objClsMailTemplete.MailBodyForCollegeQuery(name, courseQuery.CourseName, query,
                                                                              number,
                                                                              collegeBranchName);
                        mail.Body = body;
                        mail.To.Add(objQueryProperty.UserEmailId);
                        mail.IsBodyHtml = true;
                        Utils.SendMailMessageAsync(mail);
                    }
                    else
                    {
                        errMsg = "Mobile number is not valid";
                    }
                }

                else
                {
                    errMsg = "Email Id is not valid";
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
                    "Error while executing InsertCommonQuickQuery in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;
        }

        [WebMethod]
        public List<CourseMasterProperty> GetCourseSourceHome()
        {
            var data = CourseProvider.Instance.GetCourseSourceHome();
            return data.Count > 0 ? data.ToList() : null;
        }

        [WebMethod]
        public TopRankedDataset GetTopRankedColleges(int pageNumber, int pageSize, int courseId)
        {
            int totalRecords = 0;
            var data = CollegeProvider.Instance.GetTopRankedColleges(courseId, (pageNumber + 1), pageSize,
                                                                     out totalRecords);

            return new TopRankedDataset
                {
                    PageNumber = pageNumber,
                    TotalRecords = totalRecords,
                    PageSize = pageSize,
                    MessageList = data

                };
        }

        [WebMethod]
        public TopRankedDataset GetPrivateColleges(int pageNumber, int pageSize, int courseId)
        {
            int totalRecords = 0;
            var data = CollegeProvider.Instance.GetPrivateCollegeList(courseId, (pageNumber + 1), pageSize,
                                                                      out totalRecords);
            return new TopRankedDataset
                {
                    PageNumber = pageNumber,
                    TotalRecords = totalRecords,
                    PageSize = pageSize,
                    MessageList = data

                };
        }

        public class TopRankedDataset
        {
            public int PageNumber { get; set; }
            public int TotalRecords { get; set; }
            public int PageSize { get; set; }
            public List<CollegeBranchProperty> MessageList { get; set; }
        }

        [WebMethod]
        public string GetExamListByBranchCourseId(int branchCourseId)
        {
            var examSubjectSeperatedList = "";
            var examData =
                CollegeProvider.Instance.GetCollegeCourseExamDetailsByBranchCourseId(
                    Convert.ToInt16(branchCourseId));
            if (examData.Count > 0)
            {
                var examSubjectLists = (from test in examData select test.ExamName).ToArray();
                examSubjectSeperatedList = String.Join(",", examSubjectLists);
            }

            return examSubjectSeperatedList;
        }

        [WebMethod]
        public List<CollegeBranchRankProperty> GetRankSourceByCollegeBranchCourseId(int collegeId)
        {
            var objCollegeRank = new List<CollegeBranchRankProperty>();
            var objCollegeBranchRankProperty = new CollegeBranchRankProperty();
            var examSubjectLists = "";
            var data = CollegeProvider.Instance.GetCollegeCourseRankByCollegeBranchId(Convert.ToInt16(collegeId));

            if (data.Count > 0)
            {
                var query = data.Select(result => new
                    {
                        result.RankSourceName,
                        result.CollegeOverAllRank
                    }).First();
                objCollegeBranchRankProperty.CollegeOverAllRank = query.CollegeOverAllRank;
                objCollegeBranchRankProperty.RankSourceName = query.RankSourceName;
                objCollegeRank.Add(objCollegeBranchRankProperty);

            }

            return objCollegeRank.ToList();
        }

        [WebMethod]
        public string GetCollegeByCourseSearch(string courseId)
        {
            var collegeSeperatedList = "";
            _objCommon = new Common();
            var ds = new DataSet();
            ds = _objCommon.GetCollegeNameList(Convert.ToInt16(courseId));
            if (ds.Tables.Count > 1)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var query = ds.Tables[0].AsEnumerable().ToList();
                    //var query1 = ds.Tables[1].AsEnumerable();
                    collegeSeperatedList = string.Join(",",
                                                       from k in query select k.Field<string>("AjCollegeBranchName"));
                    //collegeSeperatedList = collegeSeperatedList + "," +
                    //                       string.Join(",",
                    //                                   from k in query1
                    //                                   select k.Field<string>("AjCollegeBranchPopularName"));
                }
            }
            return collegeSeperatedList;
        }

        [WebMethod]

        public string GetStreamListByCourse(int courseId)
        {
            string streamSubjectSeperatedList = "";
            var data = StreamProvider.Instance.GetStreamListByCourse(Convert.ToInt16(courseId));
            if (data.Count > 0)
            {
                var streamSubjectLists = (from test in data select test.CourseStreamName).ToArray();
                streamSubjectSeperatedList = String.Join(",", streamSubjectLists);
            }

            return streamSubjectSeperatedList;
        }


        //Code Added by Abhishek
        [WebMethod]
        public string GeCollegeSpeechNameList()
        {
            string collegeSpeechNameSeperatedList = "";
            var CollegeSpeechDetails = CollegeSpeechProvider.Instance.GetAllCollegeSpeechList();

            if (CollegeSpeechDetails.Count > 0)
            {
                var collegeSpeechLists =
                    (from test in CollegeSpeechDetails select test.CollegeSpeechPersonName).ToArray();
                collegeSpeechNameSeperatedList = String.Join(",", collegeSpeechLists);
            }

            return collegeSpeechNameSeperatedList;
        }





        [WebMethod(EnableSession = true)]
        public string ValidateLogin(string emailId, string password)
        {
            SecurePage objSecurePage = new SecurePage();

            string errMsg = "";
            int UserTypeId = 0, UserId = 0;
            string UserName = "", LandingPage = "", MobileNo = "";
            bool CanCreateUser = false;
            bool userStatus = false;
            var callStatus = true;
            try
            {

                bool Sucess = UserManagerProvider.Instance.DoLogin(emailId, password, out UserTypeId, out UserId,
                                                                   out UserName, out LandingPage, out MobileNo,
                                                                   out CanCreateUser, out errMsg, out userStatus);
                if (Sucess == true)
                {
                    if (UserTypeId == 6)
                    {
                        callStatus = false;
                        if (userStatus)
                        {

                            objSecurePage.LoggedInUserEmailId = emailId;
                            objSecurePage.LoggedInUserId = UserId;
                            objSecurePage.LoggedInUserMobile = MobileNo;
                            objSecurePage.LoggedInUserName = UserName;
                            objSecurePage.LoggedInUserType = UserTypeId;
                            objSecurePage.CanCreateUser = CanCreateUser;
                        }
                    }
                    else
                    {
                        objSecurePage.LoggedInUserEmailId = emailId;
                        objSecurePage.LoggedInUserId = UserId;
                        objSecurePage.LoggedInUserMobile = MobileNo;
                        objSecurePage.LoggedInUserName = UserName;
                        objSecurePage.LoggedInUserType = UserTypeId;
                        objSecurePage.CanCreateUser = CanCreateUser;
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
                const string addInfo = "Error while executing ValidateLogin in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

            return errMsg + "-status-" + callStatus;
        }

        [WebMethod]
        public ListItem[] GetUserType()
        {
            var data =
                UserManagerProvider.Instance.GetAllUserCategoryList()
                                   .Where(result => result.UserCategoryStatus == true)
                                   .ToList();
            if (data != null && data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.UserCategoryName,
                        Value =
                            result.UserCategoryId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;

        }

        [WebMethod]
        public List<Factor> GetFactor()
        {
            var objFactorList = new List<Factor>();
            var data = new Common().GetFactor();
            if (data.Rows.Count > 0)
            {
                for (var i = 0; i < data.Rows.Count; i++)
                {
                    var objFactor = new Factor
                        {
                            FactorId = Convert.ToInt32(data.Rows[i]["AjFactorId"].ToString()),
                            FactorName = data.Rows[i]["AjFactorName"].ToString()
                        };
                    objFactorList.Add(objFactor);

                }
            }
            return objFactorList;
        }

        [WebMethod]
        public OnlineParticipate[] UpdateCollegeOnlineParticipation(bool onlineParticipation, bool virtualParticipation,
                                                                    int collegeBranchCourseId, string AdmissionDate)
        {
            var objOnlineParticipatelist = new List<OnlineParticipate>();
            var objOnlineParticipate = new OnlineParticipate();
            var i = 0;
            var errMsg = "";
            i = CollegeProvider.Instance.UpdateCollegeCourseOnlineParticipation(collegeBranchCourseId,
                                                                                onlineParticipation,
                                                                                virtualParticipation, AdmissionDate, 1,
                                                                                out errMsg);
            if (i > 0)
            {

                objOnlineParticipate.Result = i;
                objOnlineParticipate.ErrMsg = errMsg;
                objOnlineParticipatelist.Add(objOnlineParticipate);
            }
            return objOnlineParticipatelist.ToArray();
        }

        public class OnlineParticipate
        {
            public int Result { set; get; }
            public string ErrMsg { set; get; }
        }

        [WebMethod]
        public string InsertFactorValuesAndUpdateRating(bool onlineParticipation, bool virtualParticipation,
                                                        int collegeBranchCourseId, string factorIds, string factorValues,
                                                        string AdmissionDate)
        {
            var errMsg = "";
            try
            {

                double rating = 0;
                factorIds = factorIds.TrimEnd(',');
                factorValues = factorValues.TrimEnd(',');
                var factorValuesSplit = factorValues.Split(',');
                for (var i = 0; i < factorValuesSplit.Length; i++)
                {
                    rating = rating + Convert.ToDouble(factorValuesSplit[i]);

                }
                var avgRating = rating/factorValuesSplit.Length;

                var result =
                    CollegeProvider.Instance.UpdateCollegeCourseOnlineParticipationAndRating(collegeBranchCourseId,
                                                                                             onlineParticipation,
                                                                                             virtualParticipation,
                                                                                             factorIds, factorValues,
                                                                                             avgRating, AdmissionDate, 1,
                                                                                             out errMsg);
            }
            catch (Exception ex)
            {
            }
            return errMsg;
        }

        [WebMethod]
        public List<CollegeBranchOnLineCounsellingProperty> GetCollegeBranchCourseValuesForOnlineStatus(
            int collegeBranchCourseId)
        {
            var objOnlineParticipatelist = new List<CollegeBranchOnLineCounsellingProperty>();
            var objOnlineParticipate = new CollegeBranchOnLineCounsellingProperty();

            var collegeData = CollegeProvider.Instance.GetCollegeForOnline(collegeBranchCourseId);

            var query = collegeData.Select(result => new
                {
                    result.AdmissionDate,
                    result.CollegeOnlineParticipateStatus,
                    result.CollegeOnlineParticipationVirualStatus
                }).FirstOrDefault();

            if (query != null)
            {
                objOnlineParticipate.AdmissionDate = query.AdmissionDate;
                objOnlineParticipate.CollegeOnlineParticipateStatus = query.CollegeOnlineParticipateStatus;
                objOnlineParticipate.CollegeOnlineParticipationVirualStatus =
                    query.CollegeOnlineParticipationVirualStatus;
            }
            objOnlineParticipatelist.Add(objOnlineParticipate);
            return objOnlineParticipatelist.ToList();
        }

        [WebMethod]
        public List<Factor> FillFactorValues(int collegeBranchCourseId)
        {
            var data = CollegeProvider.Instance.GetFactorValues(collegeBranchCourseId);
            return data.ToList();
        }

        [WebMethod(EnableSession = true)]
        public string InsertStudentInterestedCollege(string collegeBranchCourseId)
        {
            var objConsulling = new Consulling();
            var securePage = new SecurePage();

            var errMsg = "";
            try
            {
                int i = objConsulling.InsertStudentCollegeInterested(Convert.ToInt32(collegeBranchCourseId),
                                                                     securePage.LoggedInUserId, out errMsg);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing InsertStudentInterestedCollege in CommonWebService.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;
        }

        [WebMethod(EnableSession = true)]
        public string GetIntertestedForConsulling()
        {
            var objConsulling = new Consulling();
            var securePage = new SecurePage();
            var ds = new DataSet();
            try
            {
                ds = objConsulling.GetIntertestedCollege(securePage.LoggedInUserId);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing GetIntertestedForConsulling in CommonWebService.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return ds.Tables.Count > 0 ? ds.GetXml() : string.Empty;
        }

        [WebMethod]
        public string DeleteIntertestedForConsulling(string interestedCollegeId)
        {
            Consulling objConsulling = new Consulling();
            string errMsg = "";
            try
            {
                int i = objConsulling.DeleteIntertestedCollege(Convert.ToInt32(interestedCollegeId), out errMsg);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing GetIntertestedForConsulling in CommonWebService.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;
        }

        [WebMethod]
        public List<CollegeBranchProperty> GetCollegeList()
        {

            var data = CollegeProvider.Instance.GetCollegeList();
            return data.ToList();
        }

        [WebMethod]
        public List<CollegeBranchProperty> GetCollegeListByCourse(int courseId)
        {

            var data = CollegeProvider.Instance.GetCollegeListByCourse(courseId);
            return data.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<UserRegistrationProperty> GetUserListById()
        {
            objSecurePage = new SecurePage();

            var data = UserManager.Instance.GetUserListById(objSecurePage.LoggedInUserId);
            return data.ToList();
        }

        [WebMethod]
        public List<CollegeBranchProperty> GetParticipentCollegeListByCourse(int courseId)
        {

            var data = new Consulling().GetParticipentCollegeListByCourse(courseId);
            return data.ToList();
        }

        [WebMethod]
        public List<CollegeBranchCourseStreamProperty> GetCollegeCourseStreamDetails()
        {

            var data = CollegeProvider.Instance.GetCollegeCourseStreamDetails();
            return data.ToList();
        }

        [WebMethod(EnableSession = true)]
        public int UpdateUserProfileDetails(string value, string field)
        {
            objSecurePage = new SecurePage();
            int result = 0;

            try
            {
                if (objSecurePage.IsLoggedInUSer)
                {


                    result = UserManager.Instance.UpdateUserProfile(value, field, objSecurePage.LoggedInUserId);

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateUserProfileDetails in CommonWebServices.asmx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return result;

        }

        [WebMethod(EnableSession = true)]
        public int UpdateUserCityDetails(int country, int state, int city)
        {
            objSecurePage = new SecurePage();
            int result = 0;

            try
            {
                if (objSecurePage.IsLoggedInUSer)
                {
                    result = UserManager.Instance.UpdateUserCityDetails(country, state, city,
                                                                        objSecurePage.LoggedInUserId);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateUserCityDetails in CommonWebServices.asmx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return result;

        }

        [WebMethod(EnableSession = true)]
        public List<ExamAppearedProperty> GetStudentExamAppeared()
        {
            objSecurePage = new SecurePage();

            var data = ProfileProvider.Instance.GetAllExamAppearedList(objSecurePage.LoggedInUserId);
            return data.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<CollegePrefered> GetStudentCollegePreference()
        {
            objSecurePage = new SecurePage();

            var data = ProfileProvider.Instance.GetStudentCollegePreference(objSecurePage.LoggedInUserId);
            return data.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<CoursePreffered> GetCoursePreferedForStudent()
        {
            objSecurePage = new SecurePage();

            var data = ProfileProvider.Instance.GetAllCoursePreferList(objSecurePage.LoggedInUserId);
            return data.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<CourseStreamPreffered> GetCourseStreamPreferedByStudent()
        {
            objSecurePage = new SecurePage();

            var data = ProfileProvider.Instance.GetCourseStreamListPreferedByStudent(objSecurePage.LoggedInUserId);
            return data.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<CityPrefferedProperty> GetCityPreferenceByStudent()
        {
            objSecurePage = new SecurePage();

            var data = ProfileProvider.Instance.GetCityPreferedByStudent(objSecurePage.LoggedInUserId);
            return data.ToList();
        }

        [WebMethod(EnableSession = true)]
        public int StudentInsertExamAppeared(string examName, string rank)
        {
            objSecurePage = new SecurePage();
            int result = 0;
            try
            {
                result = ProfileProvider.Instance.StudentInsertExamAppeared(examName, rank, objSecurePage.LoggedInUserId);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing StudentInsertExamAppeared in CommonWebServices.asmx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        public List<StudentQueryProperty> GetLastQuery()
        {
            objSecurePage = new SecurePage();

            var data = ProfileProvider.Instance.GetStudentQuery(objSecurePage.LoggedInUserId);
            data = data.OrderByDescending(result => result.StudentQueryId).Take(5).ToList();
            return data.ToList();
        }



        [WebMethod(EnableSession = true)]
        public string ChangePassword(string emailId, string newPwd)
        {
            objSecurePage = new SecurePage();
            var objClsMailTemplete = new MailTemplates();
            var errMsg = "";
            try
            {
                var result = UserManagerProvider.Instance.ChangePassword(objSecurePage.LoggedInUserId, "",
                                                                         newPwd, out errMsg);
                if (result > 0)
                {
                    var mail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "AdmissionJankari: Reset Password"
                        };

                    var body = objClsMailTemplete.MailBodyForGetPassword(emailId, newPwd, objSecurePage.LoggedInUserName);

                    mail.Body = body;
                    mail.To.Add(emailId);
                    mail.IsBodyHtml = true;
                    Utils.SendMailMessageAsync(mail);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing ChangePassword in CommonWebServices.asmx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

            return errMsg;
        }

        [WebMethod(EnableSession = true)]
        public StudentQueryPaging GetStudentQuery(int pageNumber, int pageSize)
        {
            objSecurePage = new SecurePage();

            return new StudentQueryPaging
                {
                    PageNumber = pageNumber,
                    TotalRecords = ProfileProvider.Instance.GetStudentQuery(objSecurePage.LoggedInUserId).Count,
                    PageSize = pageSize,
                    MessageList =
                        ProfileProvider.Instance.GetStudentQuery(objSecurePage.LoggedInUserId)
                                       .Skip(pageSize*pageNumber)
                                       .Take(pageSize).OrderByDescending(result => result.StudentQueryId)
                                       .ToList<StudentQueryProperty>()
                };
        }

        [WebMethod(EnableSession = true)]
        public StudentQueryPaging GetAnsweredQuery(int pageNumber, int pageSize)
        {
            objSecurePage = new SecurePage();
            return new StudentQueryPaging
                {
                    PageNumber = pageNumber,
                    TotalRecords = ProfileProvider.Instance.GetStudentQuery(objSecurePage.LoggedInUserId)
                                                  .Where(result => result.ReplyStatatus == true).Count(),
                    PageSize = pageSize,
                    MessageList =
                        ProfileProvider.Instance.GetStudentQuery(objSecurePage.LoggedInUserId)
                                       .Where(result => result.ReplyStatatus == true)
                                       .Skip(pageSize*pageNumber)
                                       .Take(pageSize).OrderByDescending(result => result.StudentQueryId)
                                       .ToList<StudentQueryProperty>()
                };
            //var data = ProfileProvider.Instance.GetStudentQuery(objSecurePage.LoggedInUserId);
            //data = data.Where(result => result.ReplyStatatus).ToList();
            //return data.ToList();
        }

        [WebMethod(EnableSession = true)]
        public StudentQueryPaging GetUnAnsweredQuery(int pageNumber, int pageSize)
        {
            objSecurePage = new SecurePage();

            return new StudentQueryPaging
                {
                    PageNumber = pageNumber,
                    TotalRecords = ProfileProvider.Instance.GetStudentQuery(objSecurePage.LoggedInUserId)
                                                  .Where(result => result.ReplyStatatus == false).Count(),
                    PageSize = pageSize,
                    MessageList =
                        ProfileProvider.Instance.GetStudentQuery(objSecurePage.LoggedInUserId)
                                       .Where(result => result.ReplyStatatus == false)
                                       .Skip(pageSize*pageNumber)
                                       .Take(pageSize).OrderByDescending(result => result.StudentQueryId)
                                       .ToList<StudentQueryProperty>()
                };
        }

        public class StudentQueryPaging
        {
            public int PageNumber { get; set; }
            public int TotalRecords { get; set; }
            public int PageSize { get; set; }
            public List<StudentQueryProperty> MessageList { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public string GetAccademicInfoOfStudent()
        {
            objSecurePage = new SecurePage();
            var courseEligibilty = "";
            var data = new UserManager().GetStudentAccademicInfoStatus(objSecurePage.LoggedInUserId);
            if (data.Rows.Count > 0)
            {

                courseEligibilty = Convert.ToString(data.Rows[0]["AjCollegeCourseEligibiltyName"].ToString());

            }
            return courseEligibilty;

        }

        [WebMethod(EnableSession = true)]
        public List<StudentHighSchoolProperty> GetStudentHighSchoolDetails()
        {
            objSecurePage = new SecurePage();

            var data = new Profile().GetStudentHighSchoolDetails(objSecurePage.LoggedInUserId);

            return data.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<StudentInterSchoolProperty> GetInterMediateDetails()
        {
            objSecurePage = new SecurePage();
            var data = new Profile().GetInterMediateDetails(objSecurePage.LoggedInUserId);

            return data.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<StudentDiplomaProperty> GetDiplomaDetails()
        {
            objSecurePage = new SecurePage();
            var data = new Profile().GetDiplomaDetails(objSecurePage.LoggedInUserId);

            return data.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<StudentGraduationproperty> GetGraduationDetails()
        {
            objSecurePage = new SecurePage();

            var data = new Profile().GetGraduationDetails(objSecurePage.LoggedInUserId);

            return data.ToList();
        }

        [WebMethod]
        public ListItem[] GetStudentBoard()
        {
            var data = ProfileProvider.Instance.GetBoardDetails().OrderBy(result => result.BoardFullName).ToList();
            if (data.Count > 0)
                return data.Select(result => new ListItem()
                    {
                        Text = result.BoardFullName,
                        Value =
                            result.BoardId.ToString(CultureInfo.InvariantCulture)

                    }).ToArray();
            return null;
        }


        [WebMethod(EnableSession = true)]
        public int InsertHighSchoolDetails(string HighSchoolName, string highSchoolYop, string highSchoolCgp,
                                           int boardId, int studyModeId)
        {
            objSecurePage = new SecurePage();
            string errMsg = "";
            int _i = 0;
            try
            {
                var objStudentHighSchoolProperty = new StudentHighSchoolProperty();
                objStudentHighSchoolProperty.HigherSecondarySchoolName = HighSchoolName.Trim();
                objStudentHighSchoolProperty.HigherSecondarySchoolPassingYear = highSchoolYop.Trim();
                objStudentHighSchoolProperty.HigherSecondarySchoolScoreCGPA = highSchoolCgp.Trim();
                objStudentHighSchoolProperty.StudyModeId = studyModeId;
                objStudentHighSchoolProperty.BoardId = boardId;
                _i = ProfileProvider.Instance.InsertStudentHighSchoolDetails(objStudentHighSchoolProperty,
                                                                             objSecurePage.LoggedInUserId, out errMsg);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertHighSchoolDetails in College.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        [WebMethod(EnableSession = true)]
        public int UpdateHighSchoolDetails(int highSchoolId, string HighSchoolName, string highSchoolYop,
                                           string highSchoolCgp, int boardId, int studyModeId)
        {
            objSecurePage = new SecurePage();
            string errMsg = "";
            int _i = 0;
            try
            {
                var objStudentHighSchoolProperty = new StudentHighSchoolProperty();
                objStudentHighSchoolProperty.HigherSecondaryScoreCardId = highSchoolId;
                objStudentHighSchoolProperty.HigherSecondarySchoolName = HighSchoolName.Trim();
                objStudentHighSchoolProperty.HigherSecondarySchoolPassingYear = highSchoolYop.Trim();
                objStudentHighSchoolProperty.HigherSecondarySchoolScoreCGPA = highSchoolCgp.Trim();
                objStudentHighSchoolProperty.StudyModeId = studyModeId;
                objStudentHighSchoolProperty.BoardId = boardId;
                _i = ProfileProvider.Instance.UpdateStudentHighSchoolDetails(objStudentHighSchoolProperty,
                                                                             objSecurePage.LoggedInUserId, out errMsg);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateHighSchoolDetails in College.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        [WebMethod(EnableSession = true)]
        public int InsertInterMediateDetails(string interSchoolName, string interSchoolYop, string hinterSchoolCgp,
                                             int boardId, int studyModeId, string interCombination, string interSpecial,
                                             string interPercentage, string inetrMarks)
        {
            objSecurePage = new SecurePage();
            string errMsg = "";
            int _i = 0;
            try
            {
                var objStudentInterSchoolProperty = new StudentInterSchoolProperty();
                objStudentInterSchoolProperty.SeniorSecondarySchoolName = interSchoolName.Trim();
                objStudentInterSchoolProperty.SeniorSecondarySchoolPassingYear = interSchoolYop.Trim();
                objStudentInterSchoolProperty.SeniorSecondarySchoolScoreCgpa = hinterSchoolCgp.Trim();
                objStudentInterSchoolProperty.SeniorSecondarySchoolSubjectCombination = interCombination.Trim();
                objStudentInterSchoolProperty.SeniorSecondarySchoolSpecialization = interSpecial.Trim();
                objStudentInterSchoolProperty.SeniorSecondarySchoolSubjectMarks = inetrMarks.Trim();
                objStudentInterSchoolProperty.SeniorSecondarySchoolSubjectPercent = interPercentage.Trim();
                objStudentInterSchoolProperty.StudyModeId = studyModeId;
                objStudentInterSchoolProperty.BoardId = boardId;
                _i = ProfileProvider.Instance.InsertStudentInterSchoolDetails(objStudentInterSchoolProperty,
                                                                              objSecurePage.LoggedInUserId, out errMsg);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing InsertInterMediateDetails in CommonWebservices.asmx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        [WebMethod(EnableSession = true)]
        public int UpdateInterMediateDetails(int interSchoolId, string interSchoolName, string interSchoolYop,
                                             string hinterSchoolCgp, int boardId, int studyModeId,
                                             string interCombination, string interSpecial, string interPercentage,
                                             string inetrMarks)
        {
            objSecurePage = new SecurePage();
            string errMsg = "";
            int _i = 0;
            try
            {
                var objStudentInterSchoolProperty = new StudentInterSchoolProperty();
                objStudentInterSchoolProperty.SeniorSecondaryScoreCardId = interSchoolId;
                objStudentInterSchoolProperty.SeniorSecondarySchoolName = interSchoolName.Trim();
                objStudentInterSchoolProperty.SeniorSecondarySchoolPassingYear = interSchoolYop.Trim();
                objStudentInterSchoolProperty.SeniorSecondarySchoolScoreCgpa = hinterSchoolCgp.Trim();
                objStudentInterSchoolProperty.SeniorSecondarySchoolSubjectCombination = interCombination.Trim();
                objStudentInterSchoolProperty.SeniorSecondarySchoolSpecialization = interSpecial.Trim();
                objStudentInterSchoolProperty.SeniorSecondarySchoolSubjectMarks = inetrMarks.Trim();
                objStudentInterSchoolProperty.SeniorSecondarySchoolSubjectPercent = interPercentage.Trim();
                objStudentInterSchoolProperty.StudyModeId = studyModeId;
                objStudentInterSchoolProperty.BoardId = boardId;
                _i = ProfileProvider.Instance.UpdateStudentInterSchoolDetails(objStudentInterSchoolProperty,
                                                                              objSecurePage.LoggedInUserId, out errMsg);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing UpdateInterMediateDetails in CommonWebservices.asmx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        [WebMethod(EnableSession = true)]
        public int InsertDiplomaDetails(string diplomaSchoolName, string diplomaSchoolYop, string diplomaSchoolCgp,
                                        string diplomaCourse, string diplomaPer)
        {
            objSecurePage = new SecurePage();
            var errMsg = "";
            var _i = 0;
            try
            {
                var objStudentDiplomaProperty = new StudentDiplomaProperty();
                objStudentDiplomaProperty.StudentDicCollegeName = diplomaSchoolName.Trim();
                objStudentDiplomaProperty.StudentDicPercentage = diplomaPer.Trim();
                objStudentDiplomaProperty.StudentDicCourse = diplomaCourse.Trim();
                objStudentDiplomaProperty.StudentDicCGPA = diplomaSchoolCgp.Trim();
                objStudentDiplomaProperty.StudentDicYOP = diplomaSchoolYop.Trim();

                _i = ProfileProvider.Instance.InsertStudentDiplomaDetails(objStudentDiplomaProperty,
                                                                          objSecurePage.LoggedInUserId, out errMsg);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertDiplomaDetails in CommonWebservices.asmx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        [WebMethod(EnableSession = true)]
        public int UpdateDiplomaDetails(int diplomaSchoolId, string diplomaSchoolName, string diplomaSchoolYop,
                                        string diplomaSchoolCgp, string diplomaCourse, string diplomaPer)
        {
            objSecurePage = new SecurePage();
            var errMsg = "";
            var _i = 0;
            try
            {
                var objStudentDiplomaProperty = new StudentDiplomaProperty();
                objStudentDiplomaProperty.StudentDicScoreCardId = diplomaSchoolId;
                objStudentDiplomaProperty.StudentDicCollegeName = diplomaSchoolName.Trim();
                objStudentDiplomaProperty.StudentDicPercentage = diplomaPer.Trim();
                objStudentDiplomaProperty.StudentDicCourse = diplomaCourse.Trim();
                objStudentDiplomaProperty.StudentDicCGPA = diplomaSchoolCgp.Trim();
                objStudentDiplomaProperty.StudentDicYOP = diplomaSchoolYop.Trim();

                _i = ProfileProvider.Instance.UpdateStudentDiplomaDetails(objStudentDiplomaProperty,
                                                                          objSecurePage.LoggedInUserId, out errMsg);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing UpdateDiplomaDetails in CommonWebservices.asmx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        [WebMethod(EnableSession = true)]
        public int InsertGraduationDetails(string gradCollegeName, string gradYop, string gradCgp, string gradSpecial,
                                           string gradPer)
        {
            objSecurePage = new SecurePage();
            string errMsg = "";
            int _i = 0;
            try
            {
                var objStudentGraduationproperty = new StudentGraduationproperty();
                objStudentGraduationproperty.StudentGrdCollegeName = gradCollegeName.Trim();
                objStudentGraduationproperty.StudentGrdCGPA = gradCgp.Trim();
                objStudentGraduationproperty.StudentGrdPer = gradPer.Trim();
                objStudentGraduationproperty.StudentGrdSpecialization = gradSpecial.Trim();
                objStudentGraduationproperty.StudentGrdYOP = gradYop.Trim();

                _i = ProfileProvider.Instance.InsertStudentGraduationDetails(objStudentGraduationproperty,
                                                                             objSecurePage.LoggedInUserId, out errMsg);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertGraduationDetails in CommonWebservices.asmx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        [WebMethod(EnableSession = true)]
        public int UpdateGraduationDetails(int gradlId, string gradCollegeName, string gradYop, string gradCgp,
                                           string gradSpecial, string gradPer)
        {
            objSecurePage = new SecurePage();
            string errMsg = "";
            int _i = 0;
            try
            {

                var objStudentGraduationproperty = new StudentGraduationproperty();
                objStudentGraduationproperty.StudentGrdScorecardId = gradlId;
                objStudentGraduationproperty.StudentGrdCollegeName = gradCollegeName.Trim();
                objStudentGraduationproperty.StudentGrdCGPA = gradCgp.Trim();
                objStudentGraduationproperty.StudentGrdPer = gradPer.Trim();
                objStudentGraduationproperty.StudentGrdSpecialization = gradSpecial.Trim();
                objStudentGraduationproperty.StudentGrdYOP = gradYop.Trim();

                _i = ProfileProvider.Instance.UpdateStudentGraduationDetails(objStudentGraduationproperty,
                                                                             objSecurePage.LoggedInUserId, out errMsg);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing UpdateGraduationDetails in CommonWebservices.asmx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }


        [WebMethod(EnableSession = true)]
        public string InsertStudentStreamPrioty(string collegeBranchCourseStreamId, string collegePriorty,
                                                string collegeBranchCourseId)
        {

            string errMsg = "";
            SecurePage objSecurePage = new SecurePage();
            Consulling objConsulling = new Consulling();
            try
            {
                int i1 = objConsulling.InsertStudentCollegeInterested(Convert.ToInt32(collegeBranchCourseId),
                                                                      objSecurePage.LoggedInUserId, out errMsg,
                                                                      Convert.ToInt32(collegePriorty));
                int i = objConsulling.InsertUserInterestedStream(objSecurePage.LoggedInUserId,
                                                                 Convert.ToInt32(collegeBranchCourseStreamId),
                                                                 out errMsg);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertStudentStreamPrioty in CommonWebService.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;
        }

        [WebMethod(EnableSession = true)]
        public string UpdateStudentStreamPrioty(string collegeBranchCourseStreamId, string streamPriorty)
        {
            string errMsg = "";
            SecurePage objSecurePage = new SecurePage();
            Consulling objConsulling = new Consulling();
            try
            {
                objSecurePage = new SecurePage();
                int i = objConsulling.InsertUserInterestedStream(objSecurePage.LoggedInUserId,
                                                                 Convert.ToInt32(collegeBranchCourseStreamId),
                                                                 out errMsg, Convert.ToInt32(streamPriorty));

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateStudentStreamPrioty in CommonWebService.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;
        }

        [WebMethod(EnableSession = true)]
        public List<AccountPaymentMasterProp> GetStudentTransationDetails()
        {
            objSecurePage = new SecurePage();

            var data = new Consulling().GetPaymentTransactionStatus(objSecurePage.LoggedInUserId);

            return data.ToList();
        }

        [WebMethod(EnableSession = true)]

        public int StudentCollegePrefrenceInsert(int courseId, string collegeName)
        {
            objSecurePage = new SecurePage();
            int _i = 0;

            try
            {
                _i = new Consulling().InsertStudentCollegePrefrance(objSecurePage.LoggedInUserId, collegeName, courseId);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing StudentCollegePrefrenceInsert in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;


        }

        [WebMethod]

        public int DeleteCollegePreference(int collegePrefernceId)
        {
            int _i = 0;

            try
            {
                _i = new Consulling().DeleteCollegePreference(collegePrefernceId);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DeleteCollegePreference in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;


        }

        [WebMethod(EnableSession = true)]

        public int StudentCityPrefrenceInsert(string cityId)
        {
            objSecurePage = new SecurePage();
            int _i = 0;

            try
            {
                _i = new Consulling().InsertStudentCityPrefrance(objSecurePage.LoggedInUserId, cityId);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing StudentCityPrefrenceInsert in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;


        }

        [WebMethod]

        public int DeleteCityPrefernce(int cityPrefernceId)
        {
            int _i = 0;

            try
            {
                _i = new Consulling().DeleteCityPrefernce(cityPrefernceId);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DeleteCollegePreference in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;


        }

        [WebMethod(EnableSession = true)]

        public int StreamPrefernceInsert(int streamId)
        {
            objSecurePage = new SecurePage();
            int _i = 0;

            try
            {
                _i = new Consulling().InsertStudentStreamPrefrance(objSecurePage.LoggedInUserId, streamId);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing StreamPrefernceInsert in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;


        }

        [WebMethod]

        public int DeleteStreamPrefernce(int streamPreferId)
        {
            int _i = 0;

            try
            {
                _i = new Consulling().DeleteStreamPrefernce(streamPreferId);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DeleteStreamPrefernce in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;


        }

        [WebMethod]
        public string InsertSponserCollegeQuickQuery(string name, string email, string number, string query,
                                                     string cityName, int collegeCourseId, int courseId, int streamId,
                                                     string collegeBranchName, string collegeEmailId, string streamName)
        {
            var objClsMailTemplete = new MailTemplates();
            var errMsg = "";
            try
            {

                var objQueryProperty = new QueryProperty
                    {
                        UserEmailId = email,
                        StudentName =
                            Common.GetStringProperCase(Regex.Replace(name, "\\s+", " ")),
                        UserMobileNo = number,
                        StudentCityName = cityName,
                        StudentSourceId = Convert.ToInt32(collegeCourseId),
                        StudentCourseId = Convert.ToInt32(courseId),
                        StudentCourseStreamId = streamId,
                        StudentQuery = query
                    };



                var result = QueryProvider.Instance.InsertCollegeQuickQuery(objQueryProperty, out errMsg);
                var courseData = CourseProvider.Instance.GetCourseById(Convert.ToInt32(courseId));
                new com.admissionjankari.crm.CommonServices().ImportAdmissionJankariLeads(0, number,
                                                                                          name,
                                                                                          email,
                                                                                          courseData.First().CourseName,
                                                                                          null, streamName,
                                                                                          cityName
                                                                                          , collegeBranchName,
                                                                                          "College Query:-" + query,
                                                                                          true);
                if (result == 2)
                {
                    var objMail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "AdmissionJankari: Registration mail"
                        };
                    var mailbody = objClsMailTemplete.MailBodyForRegistation(name, email, number);
                    objMail.Body = mailbody;
                    objMail.To.Add(objQueryProperty.UserEmailId);
                    objMail.IsBodyHtml = true;
                    Utils.SendMailMessageAsync(objMail);
                }

                var courseDetails = CourseProvider.Instance.GetCourseById(courseId);
                var courseQuery = courseDetails.First();
                var mail = new MailMessage
                    {
                        From = new MailAddress(ApplicationSettings.Instance.Email),
                        Subject = "AdmissionJankari: College Query "
                    };
                var body = objClsMailTemplete.MailBodyForUserSponserCollegeQuery(name, courseQuery.CourseName,
                                                                                 streamName, query, number,
                                                                                 collegeBranchName);
                mail.Body = body;
                mail.To.Add(objQueryProperty.UserEmailId);
                mail.IsBodyHtml = true;
                Utils.SendMailMessageAsync(mail);
                var collegedata =
                    CollegeProvider.Instance.GetCollegeBasicDetailsByBranchCourseId(collegeCourseId).First();
                if (collegedata.CollegeBranchCourseSponserStatus == true)
                {
                    var collegebody = objClsMailTemplete.MailBodyForSponserCollegeQuery(name, courseQuery.CourseName,
                                                                                        streamName, query, number,
                                                                                        collegeBranchName);
                    mail.Body = collegebody;
                    mail.To.Add(collegeEmailId);
                    mail.IsBodyHtml = true;
                    Utils.SendMailMessageAsync(mail);
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
                    "Error while executing InsertCommonQuickQuery in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;
        }

        [WebMethod(EnableSession = true)]
        public string UserRegister(string mobileNo, string emailId, string name, int userType, int courseId)
        {
            var _objmailTemplete = new MailTemplates();
            var _objSecurePage = new SecurePage();
            var errMsg = "";
            var result = 0;
            try
            {
                if (Utils.IsEmailValid(emailId))
                {
                    if (Utils.IsMobileValid(mobileNo))
                    {
                        var _objUserRegistrationProperty = new UserRegistrationProperty
                            {
                                UserFullName = name,
                                UserEmailid = emailId,
                                MobileNo = mobileNo,
                                UserCategoryId = userType,
                                CourseId = courseId,
                                UserPassword = mobileNo
                            };

                        var Msg = "";
                        result = UserManagerProvider.Instance.InsertUserInfo(_objUserRegistrationProperty, userType,
                                                                             out Msg);

                        var result1 = UserManagerProvider.Instance.GetUserListByEmailId(emailId.Trim());

                        if (result1.Count > 0)
                        {

                            var query1 = result1.First();
                            _objSecurePage.LoggedInUserId = query1.UserId;
                            _objSecurePage.LoggedInUserType = query1.UserCategoryId;
                            _objSecurePage.LoggedInUserEmailId = query1.UserEmailid;
                            _objSecurePage.LoggedInUserName = query1.UserFullName;
                            _objSecurePage.LoggedInUserMobile = query1.MobileNo;
                        }

                        if (result > 0)
                        {

                            var mail = new MailMessage
                                {
                                    From = new MailAddress(ApplicationSettings.Instance.Email),
                                    Subject = "AdmissionJankari: Registration mail "
                                };




                            var body = _objmailTemplete.MailBodyForRegistation(name, emailId, mobileNo);
                            mail.Body = body;
                            mail.To.Add(_objUserRegistrationProperty.UserEmailid);
                            Utils.SendMailMessageAsync(mail);
                        }
                    }
                    else
                    {
                        errMsg = "Field mobile number is invalid,Please try again";
                    }
                }
                else
                {
                    errMsg = "Field email is invalid,Please try again";
                }


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing UserRegister in CommonWebServices.asmx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;

        }

        [WebMethod(EnableSession = true)]
        public string[] InterestedQuickQuery(string collegeBranchName, string cityName, int branchCourseId, int courseId)
        {
            var status = true;
            var objClsMailTemplete = new MailTemplates();
            var objSecurePage = new SecurePage();
            var errMsg = "";
            try
            {
                var objQueryProperty = new QueryProperty
                    {
                        UserEmailId = objSecurePage.LoggedInUserEmailId,
                        StudentName =
                            Common.GetStringProperCase(
                                Common.GetStringProperCase(
                                    Regex.Replace(objSecurePage.LoggedInUserName.Trim(), "\\s+",
                                                  " "))),
                        UserMobileNo = objSecurePage.LoggedInUserMobile,
                        StudentCityName = cityName,
                        StudentSourceId = Convert.ToInt32(branchCourseId),
                        StudentCourseId = Convert.ToInt32(courseId),
                        StudentQuery =
                            "Student want call for the institute  " + collegeBranchName,

                    };



                var result = QueryProvider.Instance.InsertCollegeQuickQuery(objQueryProperty, out errMsg);
                var courseData = CourseProvider.Instance.GetCourseById(Convert.ToInt32(courseId));
                new com.admissionjankari.crm.CommonServices().ImportAdmissionJankariLeads(0,
                                                                                          objSecurePage
                                                                                              .LoggedInUserMobile,
                                                                                          objSecurePage.LoggedInUserName
                                                                                                       .Trim(),
                                                                                          objSecurePage
                                                                                              .LoggedInUserEmailId,
                                                                                          courseData.First().CourseName,
                                                                                          null, null,
                                                                                          cityName
                                                                                          , collegeBranchName,
                                                                                          "Students want call from this college-" +
                                                                                          collegeBranchName, true);
                if (result == 2)
                {
                    var objMail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "AdmissionJankari: Registration mail"
                        };
                    var mailbody = objClsMailTemplete.MailBodyForRegistation(objSecurePage.LoggedInUserName,
                                                                             objSecurePage.LoggedInUserEmailId,
                                                                             objSecurePage.LoggedInUserMobile);
                    objMail.Body = mailbody;
                    objMail.To.Add(objQueryProperty.UserEmailId);
                    objMail.IsBodyHtml = true;
                    Utils.SendMailMessageAsync(objMail);
                }

                var courseDetails = CourseProvider.Instance.GetCourseById(courseId);
                var courseQuery = courseDetails.First();
                var mail = new MailMessage
                    {
                        From = new MailAddress(ApplicationSettings.Instance.Email),
                        Subject = "AdmissionJankari:Interested College Query "
                    };
                var body = objClsMailTemplete.MailBodyForCollegeQuery(objSecurePage.LoggedInUserName,
                                                                      courseQuery.CourseName,
                                                                      "Student want call for the institute  " +
                                                                      collegeBranchName,
                                                                      objSecurePage.LoggedInUserMobile,
                                                                      collegeBranchName);
                mail.Body = body;
                mail.To.Add(objQueryProperty.UserEmailId);
                mail.IsBodyHtml = true;
                Utils.SendMailMessageAsync(mail);
                if (objSecurePage.LoggedInUserType == 6)
                    status = false;


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing InsertCommonQuickQuery in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            string[] strList = new string[2];
            strList[0] = Convert.ToString(status);
            strList[1] = errMsg;
            return strList;
        }

        [WebMethod(EnableSession = true)]
        public void Logout()
        {
            var objSecurePage = new SecurePage();
            if (objSecurePage.IsLoggedInUSer)
            {
                Session.Abandon();
            }
        }

        [WebMethod(EnableSession = true)]
        public int UpdateCourseByUser(int courseId)
        {
            var result = 0;
            try
            {
                UserRegistrationProperty objUserCategoryProperty = new UserRegistrationProperty();
                var objSecurePage = new SecurePage();
                objUserCategoryProperty.CourseId = courseId;
                var errMsg = "";
                result = UserManagerProvider.Instance.UpdateCourseByUser(objUserCategoryProperty,
                                                                         objSecurePage.LoggedInUserId);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateCourseByUser in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return result;
        }

        [WebMethod]
        public ListItem[] BindManagement()
        {
            var dv = ClsSingelton.GetMode();
            if (dv.Count > 0)
            {

                return dv.ToTable().AsEnumerable().Select(r => new ListItem()
                    {

                        Text =
                            r.Field<string>("AjMasterValues").ToString(),
                        Value =
                            r.Field<int>("AjMasterValueId").ToString()

                    }).ToArray();
            }
            return null;
        }

        [WebMethod]
        public string GetPassword(string emailId)
        {
            var errMsg = "";

            var objMailTemplates = new MailTemplates();
            try
            {

                var result = UserManagerProvider.Instance.GetUserPassword(emailId.Trim(), out errMsg);

                if (!string.IsNullOrEmpty(result))
                {

                    var mail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "AdmissionJankari:Password Information"
                        };

                    var userDetails = UserManagerProvider.Instance.GetUserListByEmailId(emailId);
                    var userDetailsQuery = userDetails.First();
                    var body = objMailTemplates.MailBodyForGetPassword(emailId, result, userDetailsQuery.UserFullName);
                    mail.Body = body;
                    mail.To.Add(emailId);
                    Utils.SendMailMessageAsync(mail);


                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing GetPassword in CommonWebServices.asmx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

            return errMsg;
        }

        [WebMethod]
        public string GetAllExamPopularList()
        {
            var examSeperatedList = "";
            var data = ExamProvider.Instance.GetAllExamList().Where(result => result.ExamStatus).ToList();
            if (data.Count > 0)
            {
                var examLists = (from test in data select test.ExamPopularName).ToArray();
                examSeperatedList = String.Join(",", examLists);

            }

            return examSeperatedList;
        }

        [WebMethod(EnableSession = true)]
        public string UpdateCourseId(int courseId)
        {
            var objCommon = new Common {CourseId = courseId};
            var coursedata = CourseProvider.Instance.GetCourseById(courseId);
            objCommon.CourseName = "";
            objCommon.CourseName = coursedata.First().CourseName;
            return courseId.ToString(CultureInfo.InvariantCulture);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string RemoveIlLegalCahrachter(string str)
        {
            return Utils.RemoveIllegalCharacters(str);
        }

        [WebMethod]
        public string GetFaqDetails()
        {
            var faqDetailsList = "";
            var data = FAQProvider.Instance.GetAllFAQDetailsList();
            if (data.Count > 0)
            {
                var faqList = (from test in data select test.FAQDetailsQuestion).ToArray().Distinct();
                faqDetailsList = String.Join(",", faqList);
            }
            return faqDetailsList;
        }

        [WebMethod(EnableSession = true)]
        public string RegisterStudent(string mobileNo, string emailId, string name, string dob)
        {
            var collegeStatus = true;
            var errmsg = "";
            var _objmailTemplete = new MailTemplates();
            var _objSecurePage = new SecurePage();
            var result = -1;
            var errMsg = "";
            if (Utils.IsEmailValid(emailId))
            {

                if (Utils.IsMobileValid(mobileNo))
                {
                    try
                    {
                        var _objUserRegistrationProperty = new UserRegistrationProperty
                            {
                                UserFullName = name,
                                UserEmailid = emailId,
                                MobileNo = mobileNo,
                                UserCategoryId =
                                    Convert.ToInt16(Usertype.Student),
                                UserPassword = mobileNo,
                                UserDOB = Common.GetDateFromString(dob),
                                CourseId = Convert.ToInt32(new Common().CourseId),

                            };


                        result = UserManagerProvider.Instance.InsertUserInfo(_objUserRegistrationProperty,
                                                                             Convert.ToInt16(Usertype.Student),
                                                                             out errMsg);

                        if (result > 0)
                        {

                            var result1 = UserManagerProvider.Instance.GetUserListByEmailId(emailId.Trim());

                            if (result1.Count > 0)
                            {
                                if (result1.First().UserCategoryName == "College")
                                {
                                    collegeStatus = false;
                                    if (result1.First().UserStatus != false)
                                    {
                                        var query1 = result1.First();
                                        _objSecurePage.LoggedInUserId = query1.UserId;
                                        _objSecurePage.LoggedInUserType = query1.UserCategoryId;
                                        _objSecurePage.LoggedInUserEmailId = query1.UserEmailid;
                                        _objSecurePage.LoggedInUserName = query1.UserFullName;
                                        _objSecurePage.LoggedInUserMobile = query1.MobileNo;
                                    }

                                }
                                else
                                {
                                    var query1 = result1.First();
                                    _objSecurePage.LoggedInUserId = query1.UserId;
                                    _objSecurePage.LoggedInUserType = query1.UserCategoryId;
                                    _objSecurePage.LoggedInUserEmailId = query1.UserEmailid;
                                    _objSecurePage.LoggedInUserName = query1.UserFullName;
                                    _objSecurePage.LoggedInUserMobile = query1.MobileNo;
                                }


                            }
                            var mail = new MailMessage
                                {
                                    From = new MailAddress(ApplicationSettings.Instance.Email),
                                    Subject = "AdmissionJankari: Registration mail "
                                };




                            var body = _objmailTemplete.MailBodyForRegistation(name, emailId, mobileNo);
                            mail.Body = body;
                            mail.To.Add(_objUserRegistrationProperty.UserEmailid);
                            Utils.SendMailMessageAsync(mail);
                        }
                        else
                        {
                            var userDetails =
                                UserManagerProvider.Instance.GetUserListByEmailId(emailId);
                            if (userDetails != null)
                            {

                                if (userDetails.First().UserCategoryName == "College")
                                {
                                    collegeStatus = false;

                                    if (userDetails.First().UserStatus != false)
                                    {
                                        var query1 = userDetails.First();
                                        _objSecurePage.LoggedInUserId = query1.UserId;
                                        _objSecurePage.LoggedInUserType = query1.UserCategoryId;
                                        _objSecurePage.LoggedInUserEmailId = query1.UserEmailid;
                                        _objSecurePage.LoggedInUserName = query1.UserFullName;
                                        _objSecurePage.LoggedInUserMobile = query1.MobileNo;
                                    }


                                }
                                else
                                {
                                    var query1 = userDetails.First();
                                    _objSecurePage.LoggedInUserId = query1.UserId;
                                    _objSecurePage.LoggedInUserType = query1.UserCategoryId;
                                    _objSecurePage.LoggedInUserEmailId = query1.UserEmailid;
                                    _objSecurePage.LoggedInUserName = query1.UserFullName;
                                    _objSecurePage.LoggedInUserMobile = query1.MobileNo;
                                }
                            }
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
                            "Error while executing UserRegister in CommonWebServices.asmx  :: -> ";
                        var objPub = new ClsExceptionPublisher();
                        objPub.Publish(err, addInfo);
                    }
                    return errMsg + "-status-" + collegeStatus + "?" + _objSecurePage.LoggedInUserId;
                }
                else
                {
                    return errmsg = new Common().GetValidationMessage("revContactNo");
                }
            }
            else
            {
                return errmsg = new Common().GetValidationMessage("revEmail");
            }
        }

        [WebMethod(EnableSession = true)]

        public int StudentCityPrefrenceInsertById(int cityId)
        {
            objSecurePage = new SecurePage();
            int _i = 0;
            var cityData = CityProvider.Instacnce.GetCityById(cityId);
            try
            {
                _i = new Consulling().InsertStudentCityPrefrance(objSecurePage.LoggedInUserId, cityData.First().CityName);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing StudentCityPrefrenceInsertById in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;


        }

        [WebMethod(EnableSession = true)]

        public int StudentCollegePrefrenceInsertById(int courseId, int collegeName)
        {
            objSecurePage = new SecurePage();
            int _i = 0;
            var collegeData = CollegeProvider.Instance.GetCollegeListById(collegeName);
            try
            {
                _i = new Consulling().InsertStudentCollegePrefrance(objSecurePage.LoggedInUserId,
                                                                    collegeData.First().CollegeBranchName, courseId);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing StudentCollegePrefrenceInsert in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;


        }

        [WebMethod(EnableSession = true)]

        public string InsertStudentCollegeInterestedById(int collegeCourseId)
        {
            var objmailTemplete = new MailTemplates();
            var _objSecurePage = new SecurePage();
            string errMsg = "";
            string errMsg1 = "";

            try
            {

                var result1 = new Consulling().InsertStudentCollegeInterested(collegeCourseId,
                                                                              _objSecurePage.LoggedInUserId,
                                                                              out errMsg, 1);
                int result = new Consulling().InsertStudentCollegeInterested(collegeCourseId,
                                                                             _objSecurePage.LoggedInUserId,
                                                                             out errMsg1, 1);
                var formNumber = "ADMJ" + System.DateTime.Now.Year + new Common().CourseId.ToString() +
                                 new SecurePage().LoggedInUserId.ToString();
                if (result > 0)
                {
                    var mail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "AdmissionJankari:Participating For Direct Admission "
                        };

                    var collegeData = CollegeProvider.Instance.GetCollegeBasicDetailsByBranchCourseId(collegeCourseId);
                    var body = objmailTemplete.MailBodyForDirectCounsulling(_objSecurePage.LoggedInUserName,
                                                                            collegeData.First().CollegeBranchName,
                                                                            collegeData.First().CourseName, formNumber);
                    mail.Body = body;
                    mail.To.Add(_objSecurePage.LoggedInUserEmailId);
                    Utils.SendMailMessageAsync(mail);
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
                    "Error while executing InsertStudentCollegeInterestedById in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

            return errMsg1;

        }

        [WebMethod(EnableSession = true)]

        public int InsertUpdateUserTransctionalDetails(int courseId)
        {
            objSecurePage = new SecurePage();
            var i = 0;

            try
            {
                var formNumber = "ADMJ" + System.DateTime.Now.Year + new Common().CourseId.ToString() +
                                 new SecurePage().LoggedInUserId.ToString();
                i = new Consulling().InsertUpdateUserTransctionalDetails(new SecurePage().LoggedInUserId, formNumber);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing InsertStudentCollegeInterestedById in Consulling.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

            return i;

        }

        [WebMethod(EnableSession = true)]
        public string CheckReult(string mobileNo, string emailId, string name, int courseId)
        {
            var errmsg = "";
            var _objmailTemplete = new MailTemplates();
            var _objSecurePage = new SecurePage();
            var result = -1;
            var errMsg = "";
            if (Utils.IsEmailValid(emailId))
            {

                if (Utils.IsMobileValid(mobileNo))
                {
                    try
                    {
                        var _objUserRegistrationProperty = new UserRegistrationProperty
                            {
                                UserFullName = name,
                                UserEmailid = emailId,
                                MobileNo = mobileNo,
                                UserCategoryId =
                                    Convert.ToInt16(Usertype.Student),
                                UserPassword = mobileNo,
                                CourseId = courseId
                            };


                        result = UserManagerProvider.Instance.InsertUserInfo(_objUserRegistrationProperty,
                                                                             Convert.ToInt16(Usertype.Student),
                                                                             out errMsg);

                        if (result > 0)
                        {

                            var result1 = UserManagerProvider.Instance.GetUserListByEmailId(emailId.Trim());

                            if (result1.Count > 0)
                            {

                                if (result1.First().UserCategoryName == "College")
                                {
                                    if (result1.First().UserStatus)
                                    {
                                        _objSecurePage.LoggedInUserId = result1.First().UserId;
                                        _objSecurePage.LoggedInUserType = result1.First().UserCategoryId;
                                        _objSecurePage.LoggedInUserEmailId = result1.First().UserEmailid;
                                        _objSecurePage.LoggedInUserName = result1.First().UserFullName;
                                        _objSecurePage.LoggedInUserMobile = result1.First().MobileNo;
                                    }
                                }
                                else
                                {
                                    _objSecurePage.LoggedInUserId = result1.First().UserId;
                                    _objSecurePage.LoggedInUserType = result1.First().UserCategoryId;
                                    _objSecurePage.LoggedInUserEmailId = result1.First().UserEmailid;
                                    _objSecurePage.LoggedInUserName = result1.First().UserFullName;
                                    _objSecurePage.LoggedInUserMobile = result1.First().MobileNo;

                                }

                            }
                            var mail = new MailMessage
                                {
                                    From = new MailAddress(ApplicationSettings.Instance.Email),
                                    Subject = "AdmissionJankari: Registration mail "
                                };




                            var body = _objmailTemplete.MailBodyForRegistation(name, emailId, mobileNo);
                            mail.Body = body;
                            mail.To.Add(_objUserRegistrationProperty.UserEmailid);
                            Utils.SendMailMessageAsync(mail);
                        }
                        else
                        {
                            var userDetails =
                                UserManagerProvider.Instance.GetUserListByEmailId(emailId).FirstOrDefault();
                            if (userDetails != null)
                            {
                                if (userDetails.UserCategoryName == "College")
                                {
                                    if (userDetails.UserStatus)
                                    {
                                        _objSecurePage.LoggedInUserId = userDetails.UserId;
                                        _objSecurePage.LoggedInUserType = userDetails.UserCategoryId;
                                        _objSecurePage.LoggedInUserEmailId = userDetails.UserEmailid;
                                        _objSecurePage.LoggedInUserName = userDetails.UserFullName;
                                        _objSecurePage.LoggedInUserMobile = userDetails.MobileNo;
                                    }
                                }
                                else
                                {
                                    _objSecurePage.LoggedInUserId = userDetails.UserId;
                                    _objSecurePage.LoggedInUserType = userDetails.UserCategoryId;
                                    _objSecurePage.LoggedInUserEmailId = userDetails.UserEmailid;
                                    _objSecurePage.LoggedInUserName = userDetails.UserFullName;
                                    _objSecurePage.LoggedInUserMobile = userDetails.MobileNo;

                                }
                            }
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
                            "Error while executing UserRegister in CommonWebServices.asmx  :: -> ";
                        var objPub = new ClsExceptionPublisher();
                        objPub.Publish(err, addInfo);
                    }
                    return errMsg;
                }
                else
                {
                    return errmsg = new Common().GetValidationMessage("revContactNo");
                }
            }
            else
            {
                return errmsg = new Common().GetValidationMessage("revEmail");
            }
        }

        [WebMethod(EnableSession = true)]
        public bool CheckSession()
        {
            bool check = false;
            if (Session.Count > 0)
            {
                if (new SecurePage().IsLoggedInUSer)
                {
                    check = true;
                }
            }
            return check;
        }

        [WebMethod(EnableSession = true)]
        public int GetLoginUserCategory()
        {
            int loginUCat = -1;
            if (Session.Count > 0)
            {
                if (new SecurePage().IsLoggedInUSer)
                {
                    loginUCat = new SecurePage().LoggedInUserType;
                }
            }
            return loginUCat;
        }

        [WebMethod(EnableSession = true)]
        public string GetLoginUserName()
        {
            string loginUserName = "";
            if (Session.Count > 0)
            {
                if (new SecurePage().IsLoggedInUSer)
                {
                    loginUserName = new SecurePage().LoggedInUserName;
                }
            }
            return loginUserName;
        }

        [WebMethod]
        public void SendPaymentLink(string courseId, string userId, string emailId, string userName, string formNumber)
        {
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            var _objmailTemplete = new MailTemplates();


            try
            {
                var mail = new MailMessage
                    {
                        From = new MailAddress(ApplicationSettings.Instance.Email),
                        Subject = "AdmissionJankari: Online payment information"
                    };
                var url = "<a href=" + Utils.AbsoluteWebRoot + "counselling/PaymentOptions.aspx?CourseId=" +
                          objCrypto.Encrypt(courseId) + "&UserId=" +
                          objCrypto.Encrypt(userId) + ">";
                var body = _objmailTemplete.MailForPayment(userName, url, formNumber);
                mail.Body = body;
                mail.To.Add(emailId);
                Utils.SendMailMessageAsync(mail);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo =
                    "Error while executing SendPaymentLink in CommonWebServices.asmx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }


        }

        [WebMethod]
        public string GetAccademicOfStudent(int userId)
        {
            objSecurePage = new SecurePage();
            var courseEligibilty = "";
            var data = new UserManager().GetStudentAccademicInfoStatus(userId);
            if (data.Rows.Count > 0)
            {

                courseEligibilty = Convert.ToString(data.Rows[0]["AjCollegeCourseEligibiltyName"].ToString());

            }
            return courseEligibilty;

        }

        [WebMethod]
        public List<StudentHighSchoolProperty> GetStudentHighSchlDetails(int userId)
        {
            objSecurePage = new SecurePage();

            var data = new Profile().GetStudentHighSchoolDetails(userId);

            return data.ToList();
        }

        [WebMethod]
        public List<StudentInterSchoolProperty> GetStudentInterMediateDetails(int userId)
        {
            objSecurePage = new SecurePage();
            var data = new Profile().GetInterMediateDetails(userId);

            return data.ToList();
        }

        [WebMethod]
        public List<StudentDiplomaProperty> GetStudentDiplomaDetails(int userId)
        {
            objSecurePage = new SecurePage();
            var data = new Profile().GetDiplomaDetails(userId);

            return data.ToList();
        }

        [WebMethod]
        public List<StudentGraduationproperty> GetStudentGraduationDetails(int userId)
        {
            objSecurePage = new SecurePage();

            var data = new Profile().GetGraduationDetails(userId);

            return data.ToList();
        }

        [WebMethod]
        public string RemoveIllegealFromCourse(string str)
        {
            return Utils.RemoveIllegealFromCourse(str);
        }

        [WebMethod(EnableSession = true)]

        public string GetCourse()
        {

            return new Common().CourseName;

        }

        [WebMethod]
        public List<LeadSourceProperty> GetCollegeListByQuery(int courseId, string strStream, string strCities,
                                                              bool findByCities, bool participated)
        {

            var data = CollegeProvider.Instance.GetLeadCollegeList(courseId, strStream, strCities, findByCities,
                                                                   participated);

            return data.ToList();
        }

        [WebMethod]
        public List<LeadSourceProperty> GetCollegeNameByQuery(int courseId, string strStream, string strCities,
                                                              bool findByCities, bool participated)
        {

            var data = CollegeProvider.Instance.GetLeadCollegeNameList(courseId, strStream, strCities, findByCities,
                                                                       participated);

            return data.ToList();
        }

        [WebMethod]
        public List<CourseMasterProperty> GetCourseWithEligibiltyName()
        {

            var data = CourseProvider.Instance.GetAllCourseList();


            return data.ToList();
        }

        [WebMethod]
        public string GetBannerList(int courseId, int positionId)
        {
            var objDataSet = new DataSet();
            try
            {
                var ds = new Common().GetBannerById(courseId);
                var dv = ds.Tables[0].DefaultView;
                dv.RowFilter = " AjBannerStatus=1 and AjBannerPositionId=" + positionId + " and AjAdsBannerStartDate <= '" +
                               DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")) +
                               "'" + " and '" + DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")) +
                               "' <=AjAdsBannerEndDate";
                var objDt = dv.ToTable();

                objDataSet.Tables.Add(objDt);
                objDataSet.Tables[0].Columns.Add("CollegeUrl", typeof (String));

                for (int i = 0; i <= objDataSet.Tables[0].Rows.Count - 1; i++)
                {
                    objDataSet.Tables[0].Rows[i]["CollegeUrl"] =
                        !string.IsNullOrEmpty(Convert.ToString(objDataSet.Tables[0].Rows[i]["AjBannerUrl"]))
                            ? Convert.ToString(objDataSet.Tables[0].Rows[i]["AjBannerUrl"])
                            : "/college-details/" +
                              Common.RemoveIllegealFromCourseBL(
                                  Convert.ToString(objDataSet.Tables[0].Rows[i]["AjCourseName"]))
                                    .ToLower() + "/" +
                              Common.RemoveIllegalCharactersBL(
                                  Convert.ToString(
                                      objDataSet.Tables[0].Rows[i]["AjCollegeBranchName"]))
                                    .ToLower();

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
                    "Error while executing GetBannerList in CommonWebService.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objDataSet.Tables.Count > 0 ? objDataSet.GetXml() : string.Empty;
        }

        [WebMethod]
        public void SendMessageToFrind(string yourName, string frndName, string frndEmail, string message,
                                       string link)
        {

            _objCommon = new Common();
            var Msg = "";

            var objMailTemplete = new MailTemplates();

            var objMail = new MailMessage
                {
                    From = new MailAddress(ApplicationSettings.Instance.Email),
                    Subject = yourName + " recommended an article"
                };
            var mailbody = objMailTemplete.MailBodyForreferAfrnd(yourName, link, message, frndName);
            objMail.Body = mailbody;
            objMail.To.Add(frndEmail);
            objMail.IsBodyHtml = true;
            Utils.SendMailMessageAsync(objMail);

            var mail = new MailMessage
                {
                    From = new MailAddress(ApplicationSettings.Instance.Email),
                    Subject = "AdmissionJankari:recommended an article by " + yourName + " to " + frndName
                };
            var body = objMailTemplete.MailBodyForUserTellAfrndToAdmin(yourName, frndName, frndEmail, message, link);
            mail.Body = body;
            mail.To.Add(ClsSingelton.CommentMailId);
            mail.IsBodyHtml = true;
            Utils.SendMailMessageAsync(mail);
        }

        [WebMethod]
        public string CheckCollegeRegisteration(string collegeName)
        {
            var status = "0";
            try
            {
                var collegeData =
                    new Common().CheckCollegeRegisteration(collegeName);
                if (collegeData.Rows.Count > 0)
                {
                    if (collegeData.Rows[0]["TotalCount"].ToString() != "0")
                        status = "1";
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
                    "Error while executing CheckCollegeRegisteration in CommonWebService.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return status;
        }



        public class CollegeQueryPaging
        {
            public int PageNumber { get; set; }
            public int TotalRecords { get; set; }
            public int PageSize { get; set; }
            public List<QueryProperty> MessageList { get; set; }
        }

        [WebMethod]
        public CollegeQueryPaging GetCollegeQuery(int pageNumber, int pageSize, int collegeId, int course)
        {
            objSecurePage = new SecurePage();

            return new CollegeQueryPaging
                {
                    PageNumber = pageNumber,
                    TotalRecords = QueryProvider.Instance.GetCollegeQuery(collegeId, 0, course).Count(),
                    PageSize = pageSize,
                    MessageList =
                        QueryProvider.Instance.GetCollegeQuery(collegeId, 0, course).ToList()
                                     .Skip(pageSize*pageNumber)
                                     .Take(pageSize)
                                     .ToList<QueryProperty>()
                };
        }

        [WebMethod]
        public CollegeQueryPaging GetCollegeAnsweredQuery(int pageNumber, int pageSize, int collegeId, int course)
        {
            objSecurePage = new SecurePage();

            return new CollegeQueryPaging
                {
                    PageNumber = pageNumber,
                    TotalRecords = QueryProvider.Instance.GetCollegeQuery(collegeId, 0, course).Count(),
                    PageSize = pageSize,
                    MessageList =
                        QueryProvider.Instance.GetCollegeQuery(collegeId, 0, course).ToList()
                                     .Where(result => result.ReplyStatus == true)
                                     .Skip(pageSize*pageNumber)
                                     .Take(pageSize)
                                     .ToList<QueryProperty>()
                };
        }

        [WebMethod(EnableSession = true)]
        public string ReplyToUserByCollege(int userId, string textReply, string collegeName, int queryId)
        {
            var message = "";
            _objCommon = new Common();
            var errMsg = "";
            var userData = UserManagerProvider.Instance.GetUserListById(userId);
            if (userData.Count > 0)
            {
                var objQueryProperty = new QueryProperty()
                    {
                        UserId = userId,
                        StudentQueryId = Convert.ToString(queryId),
                        ReplyStatus = true,
                    };
                var result = QueryProvider.Instance.UpdateQueryReplyStatus(objQueryProperty, out errMsg);
                if (result > 0)
                {

                    message = errMsg;
                    var objReplyProperty = new ReplyProperty()
                        {
                            QueryId = queryId,
                            ReplyBy = new SecurePage().LoggedInUserId,
                            QueryReply = textReply
                        };
                    var msg = "";
                    var queryResult = QueryProvider.Instance.InsertQueryReply(objReplyProperty, out msg);
                }
                var querData = QueryProvider.Instance.GetCollegeQuery(0, queryId, 0);
                var objMailTemplete = new MailTemplates();

                var objMail = new MailMessage
                    {
                        From = new MailAddress(ApplicationSettings.Instance.Email),
                        Subject = "Query reply information from college " + collegeName
                    };
                var mailbody = objMailTemplete.MailReplyByCollegeToserForQueryPosted(userData.First().UserFullName,
                                                                                     querData.First().StudentQuery,
                                                                                     textReply, collegeName);
                objMail.Body = mailbody;
                objMail.To.Add(userData.First().UserEmailid);
                objMail.IsBodyHtml = true;
                Utils.SendMailMessageAsync(objMail);

            }
            return message;
        }

        [WebMethod]
        public CollegeQueryPaging GetCollegeUnAnsweredQuery(int pageNumber, int pageSize, int collegeId, int course)
        {
            objSecurePage = new SecurePage();

            return new CollegeQueryPaging
                {
                    PageNumber = pageNumber,
                    TotalRecords =
                        QueryProvider.Instance.GetCollegeQuery(collegeId, 0, course)
                                     .Count(result => result.ReplyStatus == false),
                    PageSize = pageSize,
                    MessageList =
                        QueryProvider.Instance.GetCollegeQuery(collegeId, 0, course).ToList()
                                     .Where(result => result.ReplyStatus == false)
                                     .Skip(pageSize*pageNumber)
                                     .Take(pageSize)
                                     .ToList<QueryProperty>()
                };
        }

        [WebMethod]
        public List<QueryProperty> GetCollegeLastQuery(string collegeId, string course)
        {
            objSecurePage = new SecurePage();

            var data =
                QueryProvider.Instance.GetCollegeQuery(Convert.ToInt32(collegeId), 0, Convert.ToInt32(course)).ToList();
            data = data.Take(5).ToList();
            return data.ToList();
        }

        [WebMethod]
        public string GetRegisteredCollegeList()
        {
            string collegeSeperatedList = "";
            var data = new Common().GetCollegeRegistered(0, null);
            if (data.Tables.Count > 0)
            {
                if (data.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                    {
                        collegeSeperatedList += data.Tables[0].Rows[i]["AjCollegeBranchName"].ToString();
                        collegeSeperatedList += ",";
                    }
                }
            }
            return collegeSeperatedList.TrimEnd(',');
        }

        [WebMethod]
        public string[] CheckCollegeBookSeatStatus(int branchCourseId)
        {
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            string[] str = new string[4];
            str[0] = "0";
            try
            {
                var collegeData =
                    new Common().CheckCollegeBookSeatStatus(branchCourseId);
                if (collegeData.Rows.Count > 0)
                {
                    str[0] = "1";
                    str[1] = objCrypto.Encrypt(collegeData.Rows[0]["AjBookSeatAmount"].ToString());

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
                    "Error while executing CheckCollegeBookSeatStatus in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

            return str;
        }

        [WebMethod]
        public TopRankedDataset GetBookSeatCollege(int pageNumber, int pageSize, int courseId)
        {

            int totalRecords = 0;
            var data = CollegeProvider.Instance.GetBookSeatCollege(courseId, (pageNumber + 1), pageSize,
                                                                   out totalRecords);


            return new TopRankedDataset
                {
                    PageNumber = pageNumber,
                    TotalRecords = totalRecords,
                    PageSize = pageSize,
                    MessageList = data

                };


        }

        [WebMethod(EnableSession = true)]
        public string InsertBookSeatStatus(int collegeBranchCourseId)
        {
            int _i = 0;
            var errMsg = "";
            try
            {
                _i = new Common().InsertUpdateBookSeatStatus(collegeBranchCourseId, new SecurePage().LoggedInUserId,
                                                             out errMsg);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertBookSeatStatsu in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;


        }

        [WebMethod(EnableSession = true)]
        public string[] CheckUserBookSeatStatus()
        {
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            string[] str = new string[5];
            str[0] = "0";
            try
            {
                var collegeData =
                    new Common().CheckUserBookSeatStatus(new SecurePage().LoggedInUserId);
                if (collegeData.Tables[0].Rows.Count > 0)
                {

                    str[0] = "1";
                    str[1] = collegeData.Tables[0].Rows[0]["AjCollegeBranchCourseId"].ToString();
                    str[2] = collegeData.Tables[0].Rows[0]["AjCollegeBranchName"].ToString();
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
                    "Error while executing CheckUserBookSeatStatus in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }


            return str;
        }

        [WebMethod(EnableSession = true)]
        public void SendMailToUserForBookSeat(string collegeBranchCourseId)
        {
            try
            {
                var collegeData =
                    CollegeProvider.Instance.GetCollegeBasicDetailsByBranchCourseId(
                        Convert.ToInt32(collegeBranchCourseId));
                var userData = UserManager.Instance.GetUserListById(new SecurePage().LoggedInUserId);
                var objMailTemplete = new MailTemplates();

                var objMail = new MailMessage
                    {
                        From = new MailAddress(ApplicationSettings.Instance.Email),
                        Subject = "Book Your Seat Regarding College- " + collegeData.First().CollegeBranchName
                    };
                var mailbody = objMailTemplete.MailBodyForBookseatToUser(userData.First().UserFullName,
                                                                         userData.First().UserEmailid,
                                                                         collegeData.First().CollegeBranchName);
                objMail.Body = mailbody;
                objMail.To.Add(userData.First().UserEmailid);
                objMail.IsBodyHtml = true;
                Utils.SendMailMessageAsync(objMail);

                var objMail1 = new MailMessage
                    {
                        From = new MailAddress(ApplicationSettings.Instance.Email),
                        Subject =
                            "Book Seat Regarding College- " + collegeData.First().CollegeBranchName + " From User " +
                            userData.First().UserFullName
                    };
                var mailbody1 = objMailTemplete.MailToAdminRegardingBookSeatByStudent(userData.First().UserFullName,
                                                                                      userData.First().UserEmailid,
                                                                                      userData.First().MobileNo,
                                                                                      collegeData.First()
                                                                                                 .CollegeBranchName);
                objMail1.Body = mailbody1;
                objMail1.To.Add(ClsSingelton.donationMailId);
                objMail1.IsBodyHtml = true;
                Utils.SendMailMessageAsync(objMail1);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing SendMailToUserForBookSeat in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        [WebMethod]
        public string GetSponserCollegeDetails()
        {

            var collegeSeperatedList = "";
            _objCommon = new Common();
            var ds = new DataSet();

            ds = _objCommon.GetCollegeNameList(0,null, true);
            if (ds.Tables.Count > 1)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var query = ds.Tables[0].AsEnumerable().ToList();
                    var query1 = ds.Tables[1].AsEnumerable();
                    collegeSeperatedList = string.Join(",",
                                                       from k in query select k.Field<string>("AjCollegeBranchName"));
                    collegeSeperatedList = collegeSeperatedList + "," +
                                           string.Join(",",
                                                       from k in query1
                                                       select k.Field<string>("AjCollegeBranchPopularName"));

                }
            }

            return collegeSeperatedList;
        }


        [WebMethod(EnableSession = true)]
        public String[] GetXids(string userId)
        {
            _objCommon = new Common();
            string xids =
                _objCommon.GetXids(userId);
            return xids.Split(',');
        }

        [WebMethod(EnableSession = true)]
        public string BindColleges()
        {
            var collegeSeperatedList = "";
            DataTable _dt = new DataTable();
            var data = CollegeProvider.Instance.GetCollegeList();
            _dt = Common.ConvertToDataTable(data);

            if (_dt.Rows.Count > 0)
            {
                var query = _dt.AsEnumerable().ToList();
                var query1 = _dt.AsEnumerable();
                collegeSeperatedList = string.Join(",",
                                                   from k in query select k.Field<string>("CollegeBranchName"));

                collegeSeperatedList = collegeSeperatedList + "," +
                                       string.Join(",",
                                                   from k in query1
                                                   select k.Field<string>("CollegePopulaorName"));

            }
            return collegeSeperatedList;
        }


        [WebMethod(EnableSession = true)]
        public string BindCollegesByCourse(string courseid)
        {
            var collegeSeperatedList = "";
            var _dt = new DataTable();

            if (courseid == "0")
            {
                var data = CollegeProvider.Instance.GetCollegeList();
                _dt = Common.ConvertToDataTable(data);
            }
            else
            {
                var data = CollegeProvider.Instance.GetCollegeListByCourse(Convert.ToInt32(courseid));
                _dt = Common.ConvertToDataTable(data);
            }


            if (_dt.Rows.Count > 0)
            {
                var query = _dt.AsEnumerable().ToList();
                var query1 = _dt.AsEnumerable();
                collegeSeperatedList = string.Join(",",
                                                   from k in query select k.Field<string>("CollegeBranchName"));

                //collegeSeperatedList = collegeSeperatedList + "," +
                //                       string.Join(",",
                //                                   from k in query1
                //                                   select k.Field<string>("CollegePopulaorName"));

            }
            return collegeSeperatedList;
        }


        //webmethod to get sponsered or non sponsered college by course or without course by indu kumar pandey on 17/07/2013.....
        [WebMethod]
        public string GetSponseredOrNonSponseredCollege(int course, bool sponserStatus)
        {
            var collegeSeperatedList = "";
            _objCommon = new Common();

            var ds =
                _objCommon.GetCollegeListBySponserStatus(course, sponserStatus);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var query = ds.Tables[0].AsEnumerable().ToList();

                    collegeSeperatedList = string.Join(",",
                                                       from k in query select k.Field<string>("AjCollegeBranchName"));


                }
            }

            return collegeSeperatedList;
        }

        [WebMethod]
        public string GetExamSubject()
        {
            var examSeperatedList = "";
            var data = ExamProvider.Instance.GetAllExamFormDetails();
            if (data.Count > 0)
            {
                var examSubjectList = (from test in data select test.ExamFormSubject).ToArray();
                examSeperatedList = String.Join(",", examSubjectList);
            }
            return examSeperatedList;
        }

        [WebMethod]
        public string GetParticipatingColleges(int course)
        {
            var collegeSeperatedList = "";
            _objCommon = new Common();

            var ds =
                _objCommon.GetCollegeListByOnilneStatus(course, true);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var query = ds.Tables[0].AsEnumerable().ToList();

                    collegeSeperatedList = string.Join(",",
                                                       from k in query select k.Field<string>("AjCollegeBranchName"));


                }
            }

            return collegeSeperatedList;
        }

        [WebMethod]
        public List<CourseMasterProperty> GetCourseEligibiltyByCourse(int course)
        {
            var data = CourseProvider.Instance.GetAllCourseList().Where(x => x.CourseId == course).ToList();
            return data.ToList();
        }

        [WebMethod(EnableSession = true)]
        public string ReplyUserQuery(int queryId, string queryReply, string queryUserEmail, string userFullName,
                                     string query)
        {
            var objCommon = new Common();
            objSecurePage = new SecurePage();
            string errMsg = "";
            try
            {
                int i = objCommon.InsertUserQueryReply(objSecurePage.LoggedInUserId, queryId, queryReply);
                if (i > 0)
                {
                    errMsg = Resources.label.ThankYouQueryReply;
                    var objMailTemplete = new MailTemplates();

                    var objMail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "Query Reply From Admissionjankari.com"
                        };
                    var mailbody = objMailTemplete.QueryResponseMailToUser(userFullName, query);
                    objMail.Body = mailbody;
                    objMail.To.Add(queryUserEmail);
                    objMail.IsBodyHtml = true;
                    Utils.SendMailMessageAsync(objMail);


                    var objMail1 = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "Query Reply Confirmation"
                        };
                    var mailbody1 = objMailTemplete.QueryResponseMailToAdmin(userFullName, query, queryReply);
                    objMail1.Body = mailbody1;
                    objMail1.To.Add(ClsSingelton.queryReplyMailId);
                    objMail1.IsBodyHtml = true;
                    Utils.SendMailMessageAsync(objMail1);
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
                    "Error while executing ReplyUserQuery in CommonWebServices.asmx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;
        }

        [WebMethod]
        public string GetCollegeGalleryList()
        {
            var collegeSeperatedList = "";
            _objCommon = new Common();
            DataTable ds = new DataTable();

            ds = _objCommon.GetCollegeImageGallery();
            if (ds.Rows.Count > 0)
            {
                if (ds.Rows.Count > 0)
                {
                    var query = ds.AsEnumerable().ToList();

                    collegeSeperatedList = string.Join(",",
                                                       from k in query select k.Field<string>("AjCollegeBranchName"));

                }
            }

            return collegeSeperatedList;
        }

        [WebMethod]
        public List<CollegeBranchGallery> GetCollegeImageGalleryByCollegeBranchId(int collegeBranchId)
        {
            var collegeFilterGalleryData =
                CollegeProvider.Instance.GetCollegeGalleryList()
                               .Where(c => c.CollegeBranchId == collegeBranchId)
                               .ToList();

            return collegeFilterGalleryData;
        }

        [WebMethod]
        public string GetCollegeContactPerson(string userId)
        {
            _objCommon = new Common();
            var data = _objCommon.GetCollegeContactPerson(Convert.ToInt32(userId));
            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }

        [WebMethod]
        public List<ReplyProperty> GetReply(string queryId)
        {
            _objCommon = new Common();
            var data = _objCommon.GetReply(Convert.ToInt32(queryId), "A");
            return data;
        }

        [WebMethod(EnableSession = true)]

        public string ModerateReply(string replyId, bool replystatus)
        {
            _objCommon = new Common();

            return _objCommon.ModerateReply(Convert.ToInt32(replyId), replystatus, new SecurePage().LoggedInUserId);

        }

        [WebMethod]
        public string GetSponserCollegeList()
        {
            var collegeSeperatedList = "";
            _objCommon = new Common();

            var data = _objCommon.GetCollegeSponser();
            if (data.Rows.Count > 1)
            {

                var query = data.AsEnumerable().ToList();
                collegeSeperatedList = string.Join(",",
                                                   from k in query select k.Field<string>("AjCollegeBranchName"));

            }
            return collegeSeperatedList;
        }

        [WebMethod]
        public List<SearchPriorityListingCollege> SearchPriorityListingCollege(int courseId, int stateId, int cityId,
                                                                               int examId, int mgtId)
        {
            var searchPatrern = "";
            var searchPriorityListingCollege =
                CollegeProvider.Instance.GetSearchPriorityListingCollege(cityId, stateId, examId, courseId, mgtId,
                                                                         out searchPatrern).ToList();
            return searchPriorityListingCollege;
        }

        [WebMethod]
        public string GetBannerCollegeList()
        {
            var collegeSeperatedList = "";
            _objCommon = new Common();

            var data = _objCommon.GetBannerById();
            if (data.Tables[0].Rows.Count > 1)
            {

                var query = data.Tables[0].AsEnumerable().ToList();
                collegeSeperatedList = string.Join(",",
                                                   from k in query select k.Field<string>("AjCollegeBranchName"));

            }
            return collegeSeperatedList;
        }

        [WebMethod(EnableSession = true)]
        public string ModerateStudentQuery(int queryId, bool status)
        {
            string errMsg = "";
            var data = QueryProvider.Instance.ModerateStudentQuery(queryId, new SecurePage().LoggedInUserId,
                                                                   Convert.ToBoolean(status), out errMsg);
            return errMsg;
        }

        [WebMethod]
        public string GetProductNameList()
        {
            var productSepereatedList = "";
            var data = new Common().GetProductAds();
            if (data.Tables[0].Rows.Count > 0)
            {
                var query = data.Tables[0].AsEnumerable().ToList();
               
                productSepereatedList = string.Join(",",
                                                    from k in query select k.Field<string>("AjProductName"));
            }
            return productSepereatedList;
        }

        [WebMethod]
        public bool CheckReplyModerate(int replyId)
        {

            return new Common().CheckModerateReply(replyId);

        }

        [WebMethod(EnableSession = true)]
        public string BindCourseForProduct(int advertismentDiscountId, int collegeId, int advertismentType)
        {

            var objDataSet = new DataSet();
            try
            {
                if (new SecurePage().IsLoggedInUSer)
                {
                    objDataSet = new Common().GetCourseForProduct(advertismentDiscountId, collegeId, advertismentType);
                }
                else
                {
                    return "401";

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
                    "Error while executing BindCourseForProduct in CommonWebService.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objDataSet.Tables.Count > 0 ? objDataSet.GetXml() : string.Empty;
        }

         [WebMethod(EnableSession = true)]
        public string GetProductForCart()
        {

            var objDataSet = new DataSet();
            try
            {
                if (new SecurePage().IsLoggedInUSer)
                {
                    objDataSet = new Common().GetProductForCart(new SecurePage().LoggedInUserId, null, 0);
                }
                else
                {
                    return "401";

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
                    "Error while executing GetProductForCart in CommonWebService.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objDataSet.Tables.Count > 0 ? objDataSet.GetXml() : string.Empty;
        }

        [WebMethod(EnableSession = true)]
        public void InsertProduct(int advertisementDiscountId, string collegeCourseIds, int advertismentType)
        {
            try
            {
                new Common().InsertProductForCart(advertisementDiscountId, collegeCourseIds, advertismentType, new SecurePage().LoggedInUserId);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing InsertProduct in CommonWebService.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        [WebMethod(EnableSession = true)]
        public void DeleteProduct(int productPaymentId, int collegeCourseId)
        {
            try
            {
                new Common().DeleteProductFromCart(productPaymentId, collegeCourseId);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing DeleteProduct in CommonWebService.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        [WebMethod(EnableSession = true)]
        public string GetBannerAdsProduct(int collegeId)
        {

            var objDataSet = new DataSet();
            try
            {
                if (new SecurePage().IsLoggedInUSer)
                {
                    objDataSet = new Common().GetBannerAdsProduct(collegeId);
                }
                else
                {
                    return "401";

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
                    "Error while executing GetBannerAdsProduct in CommonWebService.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objDataSet.Tables.Count > 0 ? objDataSet.GetXml() : string.Empty;
        }
        [WebMethod(EnableSession = true)]
        public string GetTextAdsProduct(int collegeId)
        {

            var objDataSet = new DataSet();
            try
            {
                if (new SecurePage().IsLoggedInUSer)
                {
                    objDataSet = new Common().GetTextAdsProduct(collegeId);
                }
                else
                {

                    return "401";

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
                    "Error while executing GetBannerAdsProduct in CommonWebService.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objDataSet.Tables.Count > 0 ? objDataSet.GetXml() : string.Empty;
        }
        [WebMethod(EnableSession = true)]
        public int GetProductCount()
        {

            var count = 0;
            try
            {
                if (new SecurePage().IsLoggedInUSer)
                {
                    var objDataTable = new Common().GetProductCount(new SecurePage().LoggedInUserId);
                    if (objDataTable.Rows.Count > 0)
                    {
                        count = (int) objDataTable.Rows[0]["TotalProductCount"];
                    }
                }
                else
                {

                    return 401;

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
                    "Error while executing GetBannerAdsProduct in CommonWebService.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return count;
        }
         [WebMethod]
        public List<CollegeBranchCourseProperty> GetCollegeBranchCourseDetails(int collegeBranchCourseId)
        {
            return CollegeProvider.Instance.GetCollegeCourseListByBranchCourseId(Convert.ToInt32(collegeBranchCourseId));
        }
         [WebMethod]
         public List<CollegeBranchCourseStreamProperty> GetCollegeBranchCourseStreamDetails(int collegeCourseStreamId)
         {
             return CollegeProvider.Instance.GetCollegeCourseStreamListByCollegeCourseStreamId(
                            Convert.ToInt32(collegeCourseStreamId));
         }
         [WebMethod]
         public List<CollegeBranchCourseExamProperty> GetCollegeBranchCourseExamDetails(int collegeCourseExamId)
         {
             return CollegeProvider.Instance.GetCollegeCourseStreamListByCollegeExamId(
                            Convert.ToInt32(collegeCourseExamId));
         }
         [WebMethod]
         public List<CollegeBranchCourseHostelProperty> GetCollegeBranchCourseHostelDetails(int collegeCourseHostelId)
         {
             return CollegeProvider.Instance.GetCollegeCourseHostelByHostelId(collegeCourseHostelId);
         }
         [WebMethod]
         public List<CollegeBranchRankProperty> GetCollegeBranchCourseRankDetails(int collegeCourseRankId)
         {
             return  CollegeProvider.Instance.GetCollegeCourseRankByRankId(collegeCourseRankId);
         }
         [WebMethod]
         public List<CollegeBranchCourseHighlightsProperty> GetCollegeBranchCourseHighLightsDetails(int collegeCourseRankId)
         {
             return CollegeProvider.Instance.GetCollegeCourseHighLightsByHighLightsId(collegeCourseRankId);
         }
         [WebMethod]
         public List<CollegeBranchCoursePlacementProperty> GetCollegeBranchCoursePlacementDetails(int collegeCourseplacementId)
         {
             return CollegeProvider.Instance.GetCollegeTopHirerByPlacementID(collegeCourseplacementId);
         }
         [WebMethod]
         public List<NoticeDetails> GetCollegeNoticeDetails(int collegeNoticeId)
         {
             return NewsArticleNoticeProvider.Instance.GetNoticeListById(collegeNoticeId);

         }
         [WebMethod]
         public string GetDetailsOfAdvertimentDiscount(int advstTypes, int advstTypeIds)
         {
             var data = new Common().GetAdvertisementDiscountDetails(advstTypes, Convert.ToInt32(advstTypeIds));
             return new Common().ConvertDataTabletoString(data);
         }
         [WebMethod]
         public string GetDetailsOfAdvertimentDiscountByDiscountId(int advstDiscountId)
         {
             var data = new Common().GetAdvstDiscountDetails(advstDiscountId: advstDiscountId);
             return new Common().ConvertDataTabletoString(data);
         }

         [WebMethod(EnableSession = true)]
         public string GetProductAfterPayment(int advertisementType)
         {

             var objDataSet = new DataSet();
             try
             {
                 if (new SecurePage().IsLoggedInUSer)
                 {
                     objDataSet = new Common().GetProductAfterPayment(new SecurePage().LoggedInUserId, advertisementType);
                     if (objDataSet.Tables.Count > 0)
                     {
                         if (objDataSet.Tables[0].Rows.Count > 0)
                         {
                             objDataSet.Tables[0].Columns.Add("Amount", typeof(String));

                             for (int i = 0; i <= objDataSet.Tables[0].Rows.Count - 1; i++)
                             {
                                 objDataSet.Tables[0].Rows[i]["Amount"] = Common.ConvertRupee(Convert.ToString(objDataSet.Tables[0].Rows[i]["PAYMENTAMOUNT"]));


                             }
                         }
                     }
                 }
                 else
                 {

                     return "401";

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
                     "Error while executing GetProductAfterPayment in CommonWebService.cs  :: -> ";
                 var objPub = new ClsExceptionPublisher();
                 objPub.Publish(err, addInfo);
             }
             return objDataSet.Tables.Count > 0 ? objDataSet.GetXml() : string.Empty;
         }
         [WebMethod]
         public string GetCollegeEventDetails(int collegeEventId)
         {
             var data = CollegeProvider.Instance.GetEventById(collegeEventId);
             return new Common().ConvertDataTabletoString(data);
         }
         [WebMethod]
         public string GetCollegeTestomonialDetails(int testomonialId)
         {
             Common objCommon = new Common();
             var noticeData = objCommon.GetTestimonialDetails(0, 0, testomonialId);
             return objCommon.ConvertDataTabletoString(noticeData.Tables[0]);
         }
         [WebMethod(EnableSession = true)]
         public string GetProductByPaymentId(int paymentId)
         {

             var objDataSet = new DataSet();
             try
             {

                 objDataSet = new Common().GetProductForCart(0, null, paymentId);
                

             }
             catch (Exception ex)
             {
                 var err = ex.Message;
                 if (ex.InnerException != null)
                 {
                     err = err + " :: Inner Exception :- " + ex.ToString();
                 }
                 const string addInfo =
                     "Error while executing GetProductForCart in CommonWebService.cs  :: -> ";
                 var objPub = new ClsExceptionPublisher();
                 objPub.Publish(err, addInfo);
             }
             return objDataSet.Tables.Count > 0 ? objDataSet.GetXml() : string.Empty;
         }
         [WebMethod]
         public string  GetBannerById(int bannerId)
         {
             var data = new Common().GetCollegeBannerList(0,0,bannerId);
             return new Common().ConvertDataTabletoString(data.Tables[0]);
         }

        //ONLY ACTIVE COLLEGE FOR AUTOCOMPLETE TEXTBOX...............
         [WebMethod]
         public string GetCollegeForFrontEnd()
         {

             var collegeSeperatedList = "";
             _objCommon = new Common();
             var ds = new DataSet();

             ds = _objCommon.GetCollegeNameList(0, "ACTIVE");
             if (ds.Tables.Count > 1)
             {
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     var query = ds.Tables[0].AsEnumerable().ToList();
                     var query1 = ds.Tables[1].AsEnumerable();
                     collegeSeperatedList = string.Join(",",
                                                        from k in query select k.Field<string>("AjCollegeBranchName").Trim());
                     collegeSeperatedList = collegeSeperatedList + "," +
                                            string.Join(",",
                                                        from k in query1
                                                        select k.Field<string>("AjCollegeBranchPopularName").Trim());
                 }
             }

             return collegeSeperatedList;
         }
         [WebMethod]
         public int UpdateCollegeImage(int collegeBranchId,string collegeLogo)
         {
             _objCommon = new Common();
             return _objCommon.UpdateCollegeLogo(collegeBranchId, collegeLogo);
         }
         [WebMethod]
         public string GetPaymentedCourse(int paymentId)
         {
             _objCommon = new Common();
             var data = _objCommon.GetPaymentedCourse(paymentId);
             if (data.Rows.Count > 0)
             {
                 return data.Rows[0]["COURSE"].ToString();
             }
             return "";
         }
       
    }
}

