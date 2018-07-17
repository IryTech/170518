using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcMostViewdNews : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Pager.PageSize = ApplicationSettings.Instance.MostViewNewsPageSize;
            Pager.ButtonsCount = ApplicationSettings.Instance.MostViewNewsPageCount;
            Pager.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindMostViewsNews();
        }

        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            var data = NewsArticleNoticeProvider.Instance.GetMostViewdNews();
            data = data.Where(news => news.NewsStatus == true).ToList();

                try
                 {
                     if (data.Count > 0)
                     {
                         rptPapulorNews.Visible = true;
                         if (Request.QueryString["NewsId"] != null)
                         {

                             var query =
                                 data.Where(result => result.NewsId != Convert.ToInt32(Request.QueryString["NewsId"]));
                             rptPapulorNews.Visible = true;
                             Pager.BindDataWithPaging(rptPapulorNews, Common.ConvertToDataTable(query.ToList()));
                         }
                         else
                         {
                             rptPapulorNews.Visible = true;
                             Pager.BindDataWithPaging(rptPapulorNews, Common.ConvertToDataTable(data));
                         }
                     }
                     else
                     {
                         rptPapulorNews.Visible = false;


                     }

             
               
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in UcMostViewdNews.ascx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        // Method to Bind The Most Viewd News
        protected void BindMostViewsNews()
        {
           

            try
            {
                var data = NewsArticleNoticeProvider.Instance.GetMostViewdNews();
                data = data.Where(news => news.NewsStatus == true).ToList();
                if (data.Count > 0)
                {
                    rptPapulorNews.Visible = true;
                    if (Request.QueryString["NewsId"] != null)
                    {

                        var query =
                            data.Where(result => result.NewsId != Convert.ToInt32(Request.QueryString["NewsId"]));
                        rptPapulorNews.Visible = true;
                        Pager.BindDataWithPaging(rptPapulorNews, Common.ConvertToDataTable(query.ToList()));
                    }
                    else
                    {
                        rptPapulorNews.Visible = true;
                        Pager.BindDataWithPaging(rptPapulorNews, Common.ConvertToDataTable(data));
                    }
                }
                else
                {
                    rptPapulorNews.Visible = false;



            }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindMostViewsNews in UcMostViewdNews.ascx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}