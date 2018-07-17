using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Setting
{
    public partial class UserNotification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindUserNotifiaction();
        }


        // Method to Bind The User Notification
        protected void BindUserNotifiaction()
        {
            string collegeQuery, examQuery, loanQuery, commonQuery, replyModerate, collegeComment, examComment, newsComment, loanComment, collegeUser;
            new Common().AdminUserNotification(out collegeQuery,out examQuery,out loanQuery,out commonQuery,out replyModerate,out collegeComment,out examComment,out newsComment,out loanComment,out  collegeUser );
            lblCollegeQuery.Text=collegeQuery;
            lblExamQuery.Text =examQuery;
            lblLoanQuery.Text=commonQuery;
            lblCommonQuery.Text=replyModerate;
            lblReply.Text=replyModerate;
            lblCollegeComment.Text=collegeComment;
            lblExamComment.Text=examComment;
            lblNewsComment.Text=newsComment;
            lblLoanComment.Text=loanComment;
            lblCollegeLoginCount.Text = collegeUser;
        }
    }
}