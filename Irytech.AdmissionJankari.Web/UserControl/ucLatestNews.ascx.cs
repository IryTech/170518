using System;
using System.Linq;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class ucLatestNews : System.Web.UI.UserControl
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
                var data = NewsArticleNoticeProvider.Instance.GetLatestNews();
                if (data.Count <= 0) return;
                if (Request.QueryString["NewsId"] != null)
                {
                    data =
                        data.Where(result => result.NewsId != Convert.ToInt32(Request.QueryString["NewsId"]))
                            .ToList();
                }
                dtlLatestNews.Visible = true;
                dtlLatestNews.DataSource = data;
                dtlLatestNews.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in ucLatestNews.ascx:: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindLatestNewsList()
        {
          
            try
            {
                var data = NewsArticleNoticeProvider.Instance.GetLatestNews();
                if (data.Count <= 0) return;
                if (Request.QueryString["NewsId"] != null)
                {
                    data =
                        data.Where(result => result.NewsId != Convert.ToInt32(Request.QueryString["NewsId"]))
                            .ToList();
                }
                dtlLatestNews.Visible = true;
                dtlLatestNews.DataSource = data;
                dtlLatestNews.DataBind();
            }

            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in ucLatestNews.ascx:: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}