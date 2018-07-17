using System;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.NewsArticle
{
    public partial class NewsAndArticles : SecurePage 
    {
        Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindNewsCategory();
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            var data = NewsArticleNoticeProvider.Instance.GetAllNewsList();
            if (data.Count > 0)
            {
                try
                {
                    rptNewsAndArticles.Visible = true;
                    lblInform.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptNewsAndArticles, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in NoticeAndArticles.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptNewsAndArticles.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }

        private void BindNewsCategory()
        {
            _objCommon=new Common();
            var data = NewsArticleNoticeProvider.Instance.GetAllNewsList();
            if (data.Count > 0)
            {
                try
                {
                    rptNewsAndArticles.Visible = true;
                    lblInform.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptNewsAndArticles, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in NoticeAndArticles.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptNewsAndArticles.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }
        
        protected void BtnSearchClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewsName.Text.Trim()))
                BindNewsByText();
            else
                BindNewsCategory();
        }
        private void BindNewsByText()
        {
            lblInform.Visible = false;
            _objCommon = new Common();
            var data = NewsArticleNoticeProvider.Instance.GetNewsByName(txtNewsName.Text.Trim());
            if (data.Count > 0)
            {
                rptNewsAndArticles.Visible = true;
                lblEditStatus.Visible = true;
                ucCustomPaging.BindDataWithPaging(rptNewsAndArticles, Common.ConvertToDataTable(data),true);
            }
            else
            {
                rptNewsAndArticles.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }
        }

        
    }
}