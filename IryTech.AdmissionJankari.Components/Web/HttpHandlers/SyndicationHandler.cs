using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Xml;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari.Components.Web.HttpHandlers
{
    internal class SyndicationHandler : IHttpHandler
    {
        #region Properties

        /// <summary>
        ///     Gets a value indicating whether another request can use the <see cref = "T:System.Web.IHttpHandler"></see> instance.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref = "T:System.Web.IHttpHandler"></see> instance is reusable; otherwise, false.</returns>
        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

        #region IHttpHandler

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that 
        ///     implements the <see cref="T:System.Web.IHttpHandler"></see> interface.
        /// </summary>
        /// <param name="context">
        /// An <see cref="T:System.Web.HttpContext"></see> 
        ///     object that provides references to the intrinsic server objects 
        ///     (for example, Request, Response, Session, and Server) used to service HTTP requests.
        /// </param>
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString.Count <= 0) return;

            switch (context.Request.QueryString[0])
            {

                case "News":
                    {
                        RssFeedForNews(context);
                        break;
                    }
                case "Notices":
                    {
                        RssFeedForNotices(context);
                        break;
                    }
            }

            
        }
        #endregion   IHttpHandler
        private void RssFeedForNews(HttpContext context)
        {
               context.Response.ContentType = "application/rss+xml";
  
        // Create the feed and specify the feed's attributes
        var myFeed = new SyndicationFeed
                         {
                             Title =
                                 SyndicationContent.CreatePlaintextContent(
                                     "admissionjankari.com- Education News - news"),
                             Description =
                                 SyndicationContent.CreatePlaintextContent(
                                     "admissionjankari.com- Education News - news"),
                             Copyright = SyndicationContent.CreatePlaintextContent("© " + DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) + " Copyright | www.AdmissionJankari.com"),
                             Language = "en-us",
                             ImageUrl = new Uri(Utils.AbsoluteWebRoot + "image.axd?Common=logo.png")
                         };
        myFeed.Links.Add(new SyndicationLink(new Uri("http://www.admissionjankari.com/latest-news/"), "alternate", "Education-news", "text/html", 1000));
        myFeed.LastUpdatedTime = DateTimeOffset.Now;
           
        var feedItems = new List<SyndicationItem>();


        foreach (
            var newsList in
                NewsArticleNoticeProvider.Instance.GetLatestNews()
                                         .Where(result => result.NewsStatus)
                                         .ToList()
                                         .Take(10).OrderByDescending(result => result.NewsDate))
        {
          
            var item = new SyndicationItem
                           {
                               Title = SyndicationContent.CreatePlaintextContent(newsList.NewsSubject)
                           };
            item.Links.Add(
                SyndicationLink.CreateAlternateLink(
                    new Uri(
                            Utils.AbsoluteWebRoot +
                            ("news-details/" + Utils.RemoveIllegalCharacters(Convert.ToString(newsList.NewsSubject)))
                                .ToLower())));
            item.Summary = SyndicationContent.CreatePlaintextContent(newsList.NewsDesc);
           
            item.Categories.Add(new SyndicationCategory(newsList.NewsSubject));
            item.PublishDate = newsList.NewsDate;

        
            feedItems.Add(item);
        }

        myFeed.Items = feedItems;

        
        var outputSettings = new XmlWriterSettings
                                 {
                                     Indent = true
                                 };
            var feedWriter = XmlWriter.Create(context.Response.OutputStream, outputSettings);
      
            // Emit RSS 2.0
            var rssFormatter = new Rss20FeedFormatter(myFeed);
          
            rssFormatter.WriteTo(feedWriter);
    

        feedWriter.Close();
        }
        private void RssFeedForNotices(HttpContext context)
        {
            context.Response.ContentType = "application/rss+xml";

           
            var myFeed = new SyndicationFeed
            {
                Title =
                    SyndicationContent.CreatePlaintextContent(
                        "College notices and admission-notices"),
                Description =
                    SyndicationContent.CreatePlaintextContent(
                        "College notices and admission-notices that will help you"),
                Copyright = SyndicationContent.CreatePlaintextContent("© " + DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) + " Copyright | www.AdmissionJankari.com"),
                Language = "en-us",
                ImageUrl = new Uri(Utils.AbsoluteWebRoot + "image.axd?Common=logo.png"),
                
            };
            myFeed.Links.Add(new SyndicationLink(new Uri("http://www.admissionjankari.com/admission-notices/"), "alternate", "admission-notices", "text/html", 1000));
            myFeed.LastUpdatedTime = DateTimeOffset.Now;
          
            var feedItems = new List<SyndicationItem>();


            foreach (
                var noticeList in
                    NewsArticleNoticeProvider.Instance.GetAllNoticeList()
                                             .Where(result => result.NoticeStatus)
                                             .ToList()
                                             .Take(10).OrderByDescending(result => result.NoticeDate))
            {

                var item = new SyndicationItem
                {
                    Title = SyndicationContent.CreatePlaintextContent(noticeList.NoticeSubject)
                };
                item.Links.Add(
                    SyndicationLink.CreateAlternateLink(
                        new Uri(
                                Utils.AbsoluteWebRoot +
                                ("notice-details/" + Utils.RemoveIllegalCharacters(Convert.ToString(noticeList.NoticeSubject)))
                                    .ToLower())));
                item.Summary = SyndicationContent.CreatePlaintextContent(noticeList.NoticeSubject);
                item.Categories.Add(new SyndicationCategory(noticeList.NoticeSubject));
                item.PublishDate = noticeList.NoticeDate;


             
                feedItems.Add(item);
            }

            myFeed.Items = feedItems;

 
            var outputSettings = new XmlWriterSettings
            {
                Indent = true
            };
            var feedWriter = XmlWriter.Create(context.Response.OutputStream, outputSettings);

       
            var rssFormatter = new Rss20FeedFormatter(myFeed);
            rssFormatter.WriteTo(feedWriter);


            feedWriter.Close();
        }
    }
}
