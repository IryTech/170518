using System;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcCommonComment : System.Web.UI.UserControl
    {
        private readonly SecurePage _objSecurePage = new SecurePage();

        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomCommentPaging.PageSize = ApplicationSettings.Instance.CommentsPerPage;
            ucCustomCommentPaging.ButtonsCount = ApplicationSettings.Instance.NewsArticlePageCount;
            ucCustomCommentPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;

            BindCourse();
        }

        private void BindCourse()
        {
            var course = CourseProvider.Instance.GetAllCourseList().Where(result => result.CourseStatus);
            if (course.Any())
            {
                ddlCommentCourse.DataSource = course;
                ddlCommentCourse.DataTextField = "CourseName";
                ddlCommentCourse.DataValueField = "CourseId";
                ddlCommentCourse.DataBind();
                ddlCommentCourse.Items.Insert(0, new ListItem("select", "0"));
                ddlCommentCourse.SelectedValue = Convert.ToString(new Common().CourseId);
            }
            else
            {
                ddlCommentCourse.Items.Insert(0, new ListItem("select", "0"));
            }
        }

        private void PagerPageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var dataset = new Common().GetUserComment(CommentType, CommentTypeId);
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
                            divUserComment.Visible = true;
                            lblCount.Text = Convert.ToString(rowResults.Count());
                            rptComment.Visible = true;
                            rptComment.DataSource = rowResults.ToList();
                            rptComment.DataBind();
                        }
                        else
                        {
                            divUserComment.Visible = false;
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
                    "Error while executing UcCommonComment in PagerPageIndexChanged  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }


        }

        public string CommentType { get; set; }
        public string CommentTypeId { get; set; }

        public string CourseId
        {
            get { return hdnCourseComment.Value; }
            set { hdnCourseComment.Value = value; }
        }



        protected void BtnCommentClick(object sender, EventArgs e)
        {
            var errmsg = "";
            var objmailTemplete = new MailTemplates();

            try
            {

                var errCommentMsg = "";
                var commentResult = new Common().InsertUserCoomment(_objSecurePage.LoggedInUserId, CommentType,
                                                                    CommentTypeId,
                                                                    txtComment.Text, out errCommentMsg);
                if (commentResult > 0)
                {
                    var mail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "AdmissionJankari:Comment information"
                        };
                    var commentSection = CommentSection(CommentType);

                    var body = objmailTemplete.MailBodyForUserCommentToAdmin(_objSecurePage.LoggedInUserName,
                                                                             _objSecurePage.LoggedInUserMobile,
                                                                             _objSecurePage.LoggedInUserEmailId,
                                                                             commentSection, txtComment.Text.Trim());

                    mail.To.Add(ClsSingelton.CommentMailId);
                    mail.Body = body;
                    Utils.SendMailMessageAsync(mail);
                    SetStatus("CommentSuccess", errCommentMsg);
                
                }
                else
                {
                    SetStatus("CommentError", errCommentMsg);
               
                }
                txtComment.Text = string.Empty;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo =
                    "Error while executing UcCommonComment in BtnCommentClick  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }

        protected void BtnAddCommentClick(object sender, EventArgs e)
        {
            var errmsg = "";
            var objmailTemplete = new MailTemplates();


            try
            {
                var objUserRegistrationProperty = new UserRegistrationProperty
                    {
                        UserFullName = txtUserNameComment.Text.Trim(),
                        UserEmailid = txtEmailIdComment.Text.Trim(),
                        MobileNo = txtMobileComment.Text.Trim(),
                        UserCategoryId =
                            Convert.ToInt16(Usertype.Student),
                        UserPassword = txtMobileComment.Text.Trim(),
                        CourseId = Convert.ToInt32(hdnCourseComment.Value)
                    };


                var errMsg = "";
                var result = UserManagerProvider.Instance.InsertUserInfo(objUserRegistrationProperty,
                                                                         Convert.ToInt16(Usertype.Student),
                                                                         out errMsg);

                if (result > 0)
                {
                    var result1 = UserManagerProvider.Instance.GetUserListByEmailId(txtEmailIdComment.Text.Trim());

                    if (result1.Count > 0)
                    {
                        var query = result1.First();
                        _objSecurePage.LoggedInUserId = query.UserId;
                        _objSecurePage.LoggedInUserType = query.UserCategoryId;
                        _objSecurePage.LoggedInUserEmailId = query.UserEmailid;
                        _objSecurePage.LoggedInUserName = query.UserFullName;
                        _objSecurePage.LoggedInUserMobile = query.MobileNo;


                    }
                    var mail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "AdmissionJankari: Registration mail "
                        };




                    var body = objmailTemplete.MailBodyForRegistation(txtUserNameComment.Text.Trim(),
                                                                      txtEmailIdComment.Text.Trim(),
                                                                      txtMobileComment.Text.Trim());
                    mail.Body = body;
                    mail.To.Add(objUserRegistrationProperty.UserEmailid);
                    Utils.SendMailMessageAsync(mail);
                }
                else
                {
                    var userDetails =
                        UserManagerProvider.Instance.GetUserListByEmailId(txtEmailIdComment.Text.Trim())
                                           .FirstOrDefault();
                    if (userDetails != null)
                    {
                        _objSecurePage.LoggedInUserId = userDetails.UserId;
                        _objSecurePage.LoggedInUserType = userDetails.UserCategoryId;
                        _objSecurePage.LoggedInUserEmailId = userDetails.UserEmailid;
                        _objSecurePage.LoggedInUserName = userDetails.UserFullName;
                        _objSecurePage.LoggedInUserMobile = userDetails.MobileNo;
                    }
                }
                var errCommentMsg = "";
                var commentResult = new Common().InsertUserCoomment(_objSecurePage.LoggedInUserId, CommentType,
                                                                    CommentTypeId,
                                                                    txtComment.Text, out errCommentMsg);
                if (commentResult > 0)
                {
                    var mail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "AdmissionJankari:Comment information"
                        };
                    var commentSection = CommentSection(CommentType);

                    var body = objmailTemplete.MailBodyForUserCommentToAdmin(_objSecurePage.LoggedInUserName,
                                                                             _objSecurePage.LoggedInUserMobile,
                                                                             _objSecurePage.LoggedInUserEmailId,
                                                                             commentSection, txtComment.Text.Trim());

                    mail.To.Add(ClsSingelton.CommentMailId);
                    mail.Body = body;
                    Utils.SendMailMessageAsync(mail);
                    SetStatus("CommentSuccess", errCommentMsg);
                  
               
                }
                else
                {
                    SetStatus("CommentError", errCommentMsg);
                    
                    
                }
                txtComment.Text = string.Empty;
                Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript",
                                                        "close();", true);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo =
                    "Error while executing UcCommonComment in BtnAddCommentClick  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }

        private string CommentSection(string commentType)
        {
            var comment = "";
            switch (commentType)
            {
                case "Col":
                    comment = "College";
                    break;
                case "Exam":
                    comment = "Exam";
                    break;
                case "News":
                    comment = "News";
                    break;
                case "Notice":
                    comment = "Notice";
                    break;
                case "Loan":
                    comment = "Loan";
                    break;
            }
            return comment;
        }

        public void SetStatus(string status, string msg)
        {
        
            lblCommenttResult.Attributes.Clear();
            lblCommenttResult.Attributes.Add("class", status);
            lblCommenttResult.Visible = true;
            lblCommenttResult.InnerHtml =
                string.Format(
                    "{0}<a href=\"javascript:HideMessageStatus()\" style=\"position: relative;top: -32px;float: right; right: -17px;\">X</a>",
                    Server.HtmlEncode(msg));
        }
    }
}