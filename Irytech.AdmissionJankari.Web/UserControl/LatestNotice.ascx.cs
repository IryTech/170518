using System;
using System.Linq;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class LatestNotice : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindLatestNewsList();
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
           
            try
            {
                var data = NewsArticleNoticeProvider.Instance.GetLatestNotice();
                if (data.Count <= 0) return;
                if (Request.QueryString["NoticeId"] != null)
                {
                    data =
                        data.Where(result => result.NoticeId != Convert.ToInt32(Request.QueryString["NoticeId"]))
                            .ToList();
                }
                dtlLatestNotice.Visible = true;
                dtlLatestNotice.DataSource = data;
                dtlLatestNotice.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in LatestNotice.ascx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindLatestNewsList()
        {

            try
            {
                var data = NewsArticleNoticeProvider.Instance.GetLatestNotice();
                if (data.Count <= 0) return;
                if (Request.QueryString["NoticeId"] != null)
                {
                    data =
                        data.Where(result => result.NoticeId != Convert.ToInt32(Request.QueryString["NoticeId"]))
                            .ToList();
                }
                dtlLatestNotice.Visible = true;
                dtlLatestNotice.DataSource = data;
                dtlLatestNotice.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in LatestNotice.ascx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}