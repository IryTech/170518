using System;
using System.Data;
using System.Linq;
using System.Net.Mail;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.User
{
    public partial class UserComment : System.Web.UI.Page
    {
        DataSet _dataset = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
            ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            if (Request.QueryString["N"] != null)
            {
                rbtSearchQuery.ClearSelection();
                rbtSearchQuery.Items.FindByValue(Convert.ToString(Request.QueryString["N"])).Selected = true;
            }

            BindConditinalComment();
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            BindConditinalComment();

        }

        // Method to Bind The conditional Comment 
        private void BindConditinalComment()
        {
            switch (rbtSearchQuery.SelectedValue)
            {
                case "1":
                    BindCommentList(BindCollegeCommentList());
                    break;
                case "2":
                    BindCommentList(BindExamCommentList());
                    break;
                case "3":
                    BindCommentList(BindNewsCommentList());
                    break;
                case "4":
                    BindCommentList(BindNoticeCommentList());
                    break;
                case "5":
                    BindCommentList(BindLoanCommentList());
                    break;
            }
        }
        protected void rbtSearchQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindConditinalComment();
        }
        private void BindCommentList(DataSet objDataset)
        {
            try
            {
                if (objDataset.Tables[0].Rows.Count>0)
                {
                    rptComment.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptComment, objDataset.Tables[0]);
                    ucCustomPaging.Visible = true;

                }
                else
                {
                    rptComment.Visible = false;
                    ucCustomPaging.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCommentList in UserComment.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        // Method to Bind the Comment List according to college
        private DataSet BindCollegeCommentList()
        {
           
            try
            {
                _dataset = new Common().GetUserCommentByCommentType(Convert.ToString(CommentType.Col));
               
            

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindCollegeQueryList in UserComment.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataset;
        }
        // Method to Bind the Exam List according to college
        private DataSet BindExamCommentList()
        {
          try
            {
                _dataset = new Common().GetUserCommentByCommentType(Convert.ToString(CommentType.Exam));



            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindExamCommentList in UserComment.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataset;
        }
        // Method to Bind the News List according to college
        private DataSet BindNewsCommentList()
        {
          
            try
            {
                _dataset = new Common().GetUserCommentByCommentType(Convert.ToString(CommentType.News));



            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindNewsCommentList in UserComment.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataset;
        }
        // Method to Bind the Notice List according to college
        private DataSet BindNoticeCommentList()
        {
           
            try
            {
                _dataset = new Common().GetUserCommentByCommentType(Convert.ToString(CommentType.Notice));



            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindNoticeCommentList in UserComment.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataset;
        }
        // Method to Bind the Loan List according to college
        private DataSet BindLoanCommentList()
        {
          
            try
            {
                _dataset = new Common().GetUserCommentByCommentType(Convert.ToString(CommentType.Loan));
                
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindLoanCommentList in UserComment.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataset;
        }

        protected void BtnUpdateStatus(object sender, EventArgs e)
        {
            try
            {
                var errmsg = "";
                hdnCommentId.Value = hdnCommentId.Value.Replace(",", "");
                var result = new Common().UpdateCommentStatus(Convert.ToInt32(hdnCommentId.Value),
                                                              Convert.ToBoolean(rbtCommentStatus.SelectedValue),
                                                              new SecurePage().LoggedInUserId, out errmsg);
                if (Convert.ToBoolean(rbtCommentStatus.SelectedValue))
                {
                    var mail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "AdmissionJankari:Comment information"
                        };
                    hdnUserId.Value = hdnUserId.Value.Replace(",", "");
                  var commentSection = CommentSection(rbtSearchQuery.SelectedValue);

                    var objmailTemplete = new MailTemplates();
                   var body = objmailTemplete.MailBodyForUserCommentByAdmin(hdnUserName.Value, commentSection,hdnCommentFor.Value);

                    mail.To.Add(hdnEmail.Value);
                    mail.Body = body;
                    Utils.SendMailMessageAsync(mail);
                }
                hdnCommentId.Value = "";
                hdnUserId.Value = "";
                hdnEmail.Value = string.Empty;
                hdnUserName.Value = string.Empty;
                rbtCommentStatus.ClearSelection();

                Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript",
                                                        "close();", true);
                switch (rbtSearchQuery.SelectedValue)
                {
                    case "1":
                        BindCommentList(BindCollegeCommentList());
                        break;
                    case "2":
                        BindCommentList(BindExamCommentList());
                        break;
                    case "3":
                        BindCommentList(BindNewsCommentList());
                        break;
                    case "4":
                        BindCommentList(BindNoticeCommentList());
                        break;
                    case "5":
                        BindCommentList(BindLoanCommentList());
                        break;

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BtnUpdateStatus in UserComment.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

        }
        private string CommentSection(string commentType)
        {
            var comment = "";
            switch (commentType)
            {
                case "1":
                    comment = "College";
                    break;
                case "2":
                    comment = "Exam";
                    break;
                case "3":
                    comment = "News";
                    break;
                case "4":
                    comment = "Notice";
                    break;
                case "5":
                    comment = "Loan";
                    break;
            }
            return comment;
        }

        public string GetModerateCommentClass(object commentId)
        {
            return new Common().CheckModerateComment(Convert.ToInt32(commentId));
        }
    }
}