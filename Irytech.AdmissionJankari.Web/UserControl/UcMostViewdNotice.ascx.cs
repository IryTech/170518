using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcMostViewdNotice : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Pager.PageSize = ApplicationSettings.Instance.MostViewNewsPageSize;
            Pager.ButtonsCount = ApplicationSettings.Instance.MostViewNewsPageCount;
            Pager.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindMostViewdNotice();
           
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var data = NewsArticleNoticeProvider.Instance.GetMostViewdNotice();
                data = data.Where(notice => notice.NoticeStatus == true).ToList();
                if (data.Count > 0)
                {
                    if (Request.QueryString["NoticeId"] != null)
                    {

                        var query =
                            data.Where(result => result.NoticeId != Convert.ToInt32(Request.QueryString["NoticeId"]));

                        Pager.BindDataWithPaging(rptPapulorNotice, Common.ConvertToDataTable(query.ToList()));
                        Pager.Visible = true;
                        rptPapulorNotice.Visible = true;
                    }
                    else
                    {
                        Pager.BindDataWithPaging(rptPapulorNotice, Common.ConvertToDataTable(data));
                        Pager.Visible = true;
                        rptPapulorNotice.Visible = true;
                    }
                }
                else
                {
                    divPopularNotice.Visible = false;
                    Pager.Visible = true;
                    rptPapulorNotice.Visible = true;
                }


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in UcMostViewdNotice.ascx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        // Method to Bind The Most Viewd Notices 

        protected void BindMostViewdNotice()
        {

            try
            {
                var data = NewsArticleNoticeProvider.Instance.GetMostViewdNotice();
                data = data.Where(notice => notice.NoticeStatus == true).ToList();
                if (data.Count > 0)
                {
                    if (Request.QueryString["NoticeId"] != null)
                    {

                        var query =
                            data.Where(result => result.NoticeId != Convert.ToInt32(Request.QueryString["NoticeId"]));

                        Pager.BindDataWithPaging(rptPapulorNotice, Common.ConvertToDataTable(query.ToList()));
                        Pager.Visible = true;
                        rptPapulorNotice.Visible = true;
                    }
                    else
                    {
                        Pager.BindDataWithPaging(rptPapulorNotice, Common.ConvertToDataTable(data));
                        Pager.Visible = true;
                        rptPapulorNotice.Visible = true;
                    }
                }
                else
                {
                    divPopularNotice.Visible = false;
                    Pager.Visible = true;
                    rptPapulorNotice.Visible = true;
                }
        

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindMostViewdNotice in UcMostViewdNotice.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}