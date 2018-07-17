using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components.Web.Controls;
using System.Web.UI.HtmlControls;
namespace IryTech.AdmissionJankari20.Web.NewsAndArticles
{
    public partial class NoticeDetails : PageBase
    {
        Common _ObjCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["NoticeId"] != null)
            {
                UcComment.CommentType = Convert.ToString(CommentType.Notice);
                UcComment.CommentTypeId = Request.QueryString["NoticeId"];
                GetUserComment();
                ADMJReportAbuse.AbuseType = Convert.ToString(CommentType.Notice);
                ADMJReportAbuse.AbuseTypeId = Request.QueryString["NoticeId"];
                
            }
            if (IsPostBack) return;
            if(Request.QueryString["NoticeId"]!=null)
            {
                BindPageTitleAndKeyWords();
                BindNewsDetails(Request.QueryString["NoticeId"]);
                _ObjCommon = new Common();
                int i = _ObjCommon.InsertNoticePageClick(Convert.ToInt32(Request.QueryString["NoticeId"]));
                GetTotalPageCount(Convert.ToInt32(Request.QueryString["NoticeId"]));
            }
            UcRating.RatingType = Convert.ToString(CommentType.News);
            UcRating.RatingId = Convert.ToInt32(Request.QueryString["NoticeId"]);
            BindUserRating();
        }
        private void GetUserComment()
        {
            var lblCount = UcComment.FindControl("lblCount") as Label;
            var dataset = new Common().GetUserComment(UcComment.CommentType, UcComment.CommentTypeId);
            lblCount.Text = "0";
            ADMJCommentCount.TotalCommentCount = "0";
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
                        var control = UcComment.FindControl("rptComment");
                        var divUserComment = UcComment.FindControl("divUserComment") as HtmlGenericControl;
                      
                        var repeater = control as Repeater;
                        if (repeater != null)
                        {
                            if (divUserComment != null) divUserComment.Visible = true;
                            repeater.DataSource = rowResults.ToList();
                            repeater.DataBind();
                            if (lblCount != null)
                                lblCount.Text = !string.IsNullOrEmpty(rowResults.Count().ToString()) ? Convert.ToString(rowResults.Count()) : "0";
                            ADMJCommentCount.TotalCommentCount = !string.IsNullOrEmpty(rowResults.Count().ToString()) ? Convert.ToString(rowResults.Count()) : "0";
                        }
                    }
                }
              
            }

        }
        private void GetTotalPageCount(int noticeId)
        {
            var count = new Common().GetNoticePageClick(noticeId);
            ADMJTotalViews.TotalViewCount = !string.IsNullOrEmpty(count.ToString()) ? count.ToString(CultureInfo.InvariantCulture) : "0";
        }
      
        private void BindNewsDetails(string noticeId)
        {
            var data = NewsArticleNoticeProvider.Instance.GetNoticeListById(Convert.ToInt16(noticeId));
        
            if (data.Count <= 0) return;
            var query = data.Select(result => new
            {
                noticeDesc = result.NoticeDesc,
                noticeImage = result.NoticeImage,
                noticeTitle = result.NoticeSubject,
                noticeDate=result.NoticeDate 
            }).First();
           
                ImgNotice.ImageUrl = String.Format("{0}{1}", "/image.axd?Notice=", query.noticeImage==null ? "NoImage.jpg" : query.noticeImage); 
                ImgNotice.ToolTip = query.noticeTitle;
                UcRating.RatingToolTip = query.noticeTitle;
                ADMJTotalViews.TotalViewsTooltip = query.noticeTitle;
                ADMJCommentCount.TotalCommentTooltip = query.noticeTitle;
                spnDate.InnerHtml = query.noticeDate.ToString("dd  MMM, yyyy");
           
            if (!string.IsNullOrEmpty(query.noticeDesc))
            {
                //string newTag = System.Text.RegularExpressions.Regex.Replace(query.noticeDesc, "<[^>]*>",
                // "", System.Text.RegularExpressions.RegexOptions.Multiline | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                noticeDesc.InnerHtml = query.noticeDesc;
            }

            if (!string.IsNullOrEmpty(query.noticeTitle))
            {
                NoticeTitle.InnerHtml = query.noticeTitle;
            }
        }
          private void BindPageTitleAndKeyWords()
          {
              var objNotice = NewsArticleNoticeProvider.Instance.GetNoticeListById(Convert.ToInt32(Request.QueryString["NoticeId"]));
              try
              {
                  if (objNotice.Count > 0)
                  {
                      var noticeQuery = objNotice.First();
                      Page.Title = "";
                      Page.Title = noticeQuery.NoticeSubject;

                      var metadesc = new HtmlMeta();
                      metadesc.Attributes.Clear();
                      metadesc.Name = "description";

                      metadesc.Content = "Applications are invited  for admission in [" + DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) + " " + (DateTime.Now.Year + 1) + "]" +
                                         noticeQuery.NoticeSubject;

                      Page.Header.Controls.Add(metadesc);

                      var metaKeywords = new HtmlMeta();
                      metaKeywords.Name = "keywords";

                      metaKeywords.Content = "Notices, Admission Notice [" + DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) + " " + (DateTime.Now.Year + 1) + "]" +
                                             noticeQuery.NoticeSubject;

                      Page.Header.Controls.Add(metaKeywords);

                  }
              }
              catch (Exception ex)
              {
                  var err = ex.Message;
                  if (ex.InnerException != null)
                  {
                      err = err + " :: Inner Exception :- " + ex.ToString();
                  }
                  const string addInfo = "Error while executing BindPageTitleAndKeyWords() in NoticeDetails.aspx.cs  :: -> ";
                  var objPub = new ClsExceptionPublisher();
                  objPub.Publish(err, addInfo);
              }
          }

        private void BindUserRating()
          {
              var objCommon = new Common();
            try
              {
                  var DT = objCommon.GetUserRating(Convert.ToString(CommentType.Notice), Convert.ToInt32(Request.QueryString["NoticeId"]));
                  if (DT.Rows.Count > 0)
                  {
                      for (int i = 0; i < DT.Rows.Count; i++)
                      {
                          UcRating.Rate1 = UcRating.Rate1 + Convert.ToInt16(DT.Rows[i]["AjRating1"]);
                          UcRating.Rate2 = UcRating.Rate2 + Convert.ToInt16(DT.Rows[i]["AjRating2"]);
                          UcRating.Rate3 = UcRating.Rate3 + Convert.ToInt16(DT.Rows[i]["AjRating3"]);
                          UcRating.Rate4 = UcRating.Rate4 + Convert.ToInt16(DT.Rows[i]["AjRating4"]);
                          UcRating.Rate5 = UcRating.Rate5 + Convert.ToInt16(DT.Rows[i]["AjRating5"]);
                      }
                  }
                  else
                  {
                      UcRating.Rate1 = 0;
                      UcRating.Rate2 = 0;
                      UcRating.Rate3 = 0;
                      UcRating.Rate4 = 0;
                      UcRating.Rate5 = 0;
                  }
              }
              catch (Exception ex)
              {
                  var err = ex.Message;
                  if (ex.InnerException != null)
                  {
                      err = err + " :: Inner Exception :- " + ex;
                  }
                  const string addInfo = "Error while executing BindUserRating() in NoticeDetails.aspx.cs  :: -> ";
                  var objPub = new ClsExceptionPublisher();
                  objPub.Publish(err, addInfo);
              }
          }
    }
}