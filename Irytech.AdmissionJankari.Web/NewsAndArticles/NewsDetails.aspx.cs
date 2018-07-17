using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components.Web.Controls;
using IryTech.AdmissionJankari.BO;
using System.Web.UI.HtmlControls;
namespace IryTech.AdmissionJankari20.Web.NewsAndArticles
{
    public partial class NewsDetails : PageBase
    {
        Common _ObjCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            
                 UcComment.CommentType = Convert.ToString(CommentType.News);
                 UcComment.CommentTypeId = Request.QueryString["NewsId"];
                 ADMJReportAbuse.AbuseType = Convert.ToString(CommentType.News);
                 ADMJReportAbuse.AbuseTypeId= Request.QueryString["NewsId"];
                 GetUserComment();
              if (!IsPostBack)
              {
                  if (Request.QueryString["NewsId"] != null)
                  {
                      BindNewsDetails(Request.QueryString["NewsId"]);
                      _ObjCommon = new Common();
                      int i = _ObjCommon.InsertNewsPageClickType(Convert.ToInt32(Request.QueryString["NewsId"]));
                      GetTotalPageCount(Convert.ToInt32(Request.QueryString["NewsId"]));
                    
                  }
              }
              

                  UcRating.RatingType = Convert.ToString(CommentType.News);
                  UcRating.RatingId = Convert.ToInt32(Request.QueryString["NewsId"]);
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
                            ADMJCommentCount.TotalCommentCount = !string.IsNullOrEmpty(rowResults.Count().ToString())?Convert.ToString(rowResults.Count()):"0";
                        }
                    }
                }
            }

        }
        private void GetTotalPageCount(int newsId)
        {

            var count = new Common().GetNewsPageClickType(newsId);
            ADMJTotalViews.TotalViewCount = !string.IsNullOrEmpty(count.ToString(CultureInfo.InvariantCulture)) ? count.ToString(CultureInfo.InvariantCulture) : "0";
        }

        private void BindNewsDetails(string newsId)
        {
            var data = NewsArticleNoticeProvider.Instance.GetNewsDetailsById(Convert.ToInt16(newsId));
            BindPageTitleAndKeyWords(data);
            if (data.Count <= 0) return;
            var query = data.Select(result => new
            {
                newsDesc = result.NewsDesc,
                newsImage = result.NewsImage,
                newsDate = result.NewsDate,
                newsBy = result.NewsBy,
               newsTitle=result.NewsSubject
            }).First();

                UcRating.RatingToolTip =query.newsTitle;
                ImgNews.ImageUrl = String.Format("{0}{1}", "/image.axd?News=", string.IsNullOrEmpty(Convert.ToString(query.newsImage)) ? "NoImage.jpg" : query.newsImage);
                ImgNews.ToolTip =query.newsTitle;
                ADMJTotalViews.TotalViewsTooltip = query.newsTitle;
                ADMJCommentCount.TotalCommentTooltip = query.newsTitle;
            if (!string.IsNullOrEmpty(query.newsDesc))
            {
                //string newTag = System.Text.RegularExpressions.Regex.Replace(query.newsDesc, "<[^>]*>",
                //     "", System.Text.RegularExpressions.RegexOptions.Multiline | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                newsDesc.InnerHtml = query.newsDesc;
            }
            if (!string.IsNullOrEmpty(Convert.ToString(query.newsDate)))
            {
                spnNewsDate.InnerHtml = query.newsDate.ToString("dd  MMM, yyyy");
            }
            if (!string.IsNullOrEmpty(query.newsBy))
            {
                spnNewsBy.InnerHtml ="By:" + " " + query.newsBy;
            }
            if (!string.IsNullOrEmpty(query.newsTitle))
            {
                NewsTitle.InnerHtml = query.newsTitle ;
            }
        }
         private void BindPageTitleAndKeyWords(List<NewsArticleProperty> objNews)
          {
             try{
                 if (objNews.Count > 0)
                 {
                     Page.Title = "";
                     Page.Title = objNews.First().NewsSubject + "-Admission Jankari";

                     var metadesc = new HtmlMeta();
                     metadesc.Attributes.Clear();
                     metadesc.Name = "description";

                     metadesc.Content = objNews.First().NewsSubject + "-Admission Jankari";

                     Page.Header.Controls.Add(metadesc);

                     var metaKeywords = new HtmlMeta
                                            {
                                                Name = "keywords",
                                                Content = objNews.First().NewsSubject + "-Admission Jankari"
                                            };

                     Page.Header.Controls.Add(metaKeywords);
                 }
             }
             catch (Exception ex)
             {
                 var err = ex.Message;
                 if (ex.InnerException != null)
                 {
                     err = err + " :: Inner Exception :- " + ex;
                 }
                 const string addInfo = "Error while executing BindPageTitleAndKeyWords() in NewDetails.aspx.cs  :: -> ";
                 var objPub = new ClsExceptionPublisher();
                 objPub.Publish(err, addInfo);
             }
            }


         // Method to Bind The Rating 
         protected void BindUserRating()
         {
             Common objCommon = new Common();
             System.Data.DataTable DT = new System.Data.DataTable();           
             try
             {
                 DT=objCommon.GetUserRating(Convert.ToString(CommentType.News),Convert.ToInt32(Request.QueryString["NewsId"]));
                 if (DT.Rows.Count > 0)
                 {
                    for (int i = 0; i < DT.Rows.Count; i++)
                     {
                         UcRating.Rate1 = UcRating.Rate1+ Convert.ToInt16(DT.Rows[i]["AjRating1"]);
                         UcRating.Rate2 =  UcRating.Rate2+ Convert.ToInt16(DT.Rows[i]["AjRating2"]);
                         UcRating.Rate3 = UcRating.Rate3+ Convert.ToInt16(DT.Rows[i]["AjRating3"]);
                         UcRating.Rate4 = UcRating.Rate4 + Convert.ToInt16(DT.Rows[i]["AjRating4"]);
                         UcRating.Rate5 = UcRating.Rate5 + Convert.ToInt16(DT.Rows[i]["AjRating5"]);

                     }

                 }
                 else
                 {
                     UcRating.Rate1 = 1;
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
                 const string addInfo = "Error while executing BindUserRating() in NewDetails.aspx.cs  :: -> ";
                 var objPub = new ClsExceptionPublisher();
                 objPub.Publish(err, addInfo);
             }
         }
    }
}