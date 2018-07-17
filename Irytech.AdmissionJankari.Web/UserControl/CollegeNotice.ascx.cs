using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class CollegeNotice : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            GetCollegeNoticeDetails();
        }
        private void GetCollegeNoticeDetails()
        {
            var noticeData = NewsArticleNoticeProvider.Instance.GetNoticeListOfParticulerCollege(CollegeBranchId);
            noticeData = noticeData.Where(news => news.NoticeStatus == true).OrderByDescending(news => news.NoticeId).Take(5).ToList();
            if (noticeData.Count > 0)
            {
                divNotice.Visible = true;
                rptNotice.DataSource = noticeData;
                rptNotice.DataBind();
            }
            else
            {
                divNotice.Visible = false; 
            }
        }
        public int CollegeBranchId
        {
            get { return Convert.ToInt32(ViewState["CollegeBranchId"]); }
            set { ViewState["CollegeBranchId"] = value; }
        }
        protected string RemoveCss(string noticeDesc)
        {
            if (!string.IsNullOrEmpty(noticeDesc))
            {
                string newTag = System.Text.RegularExpressions.Regex.Replace(noticeDesc, "<[^>]*>",
                 "", System.Text.RegularExpressions.RegexOptions.Multiline | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                return newTag;
            }
            else
                return "N/A";
        }
    }
}