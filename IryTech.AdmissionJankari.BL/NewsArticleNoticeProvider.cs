using System.Collections.Generic;
using IryTech.BaseClassLibraray;
using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari.BL
{
    public abstract class NewsArticleNoticeProvider : INewsArticle
    {

        #region INewsArticle Members

        public static NewsArticleNoticeProvider Instance
        {
            get
            {
                return new NewsArticleNotice();
            }
        }


        public abstract int InsertNoticeCategory(string noticeCategoryName, bool status, int createdBy, out string errmsg);
        public abstract int UpdateNoticeCategory(int noticeCategoryId, string noticeCategoryName, bool status, int modifiedBy, out string errmsg);
        public abstract  List<NewArticleNoticeProperty> GetAllNoticeCategoryList();
        public abstract List<NewArticleNoticeProperty> GetNoticeCategoryListById(int noticeCategoryId);
       #endregion

        #region INewsArticle Members


        public abstract int InsertNoticeDetails(NoticeDetails objNoticeDetails, int createdBy, out string errmsg);
        

        public abstract int UpdateNoticeDetails(NoticeDetails objNoticeDetails, int modifiedBy, out string errmsg);


        public abstract List<NoticeDetails> GetAllNoticeList();
        

        public abstract List<NoticeDetails> GetNoticeListById(int noticeId);
        

        public abstract List<NoticeDetails> GetNoticeListByName(string noticeName);
        

        public abstract List<NoticeDetails> GetNoticeListOfColleges();
       

        public abstract List<NoticeDetails> GetNoticeListOfParticulerCollege(int collegeId);


        public abstract List<NoticeDetails> GetNoticeListByNoticeCategory(int noticeCategoryId);
        public abstract List<NewsArticleProperty> GetMostViewdNews();
        public abstract List<NoticeDetails> GetMostViewdNotice();
        public abstract List<NoticeDetails> GetAllNoticeListByPaging(out int totalRecords, int pageNum = 0, int pageSize = 0);

        public abstract List<NewsArticleProperty> GetAllNewsListByPage(out int totalRecords, int pageNum = 0, int pageSize = 0);
        #endregion

        #region INewsArticle Members


        public abstract int InsertNewsDetails(NewsArticleProperty objNewsArticle, int createdBy, out string errmsg);


        public abstract int InsertArticleDetails(NewsArticleProperty objNewsArticle, int createdBy, out string errmsg);

        public abstract int UpdateNewsDetails(NewsArticleProperty objNewsArticle, int modifiedBy, out string errmsg);


        public abstract int UpdateArticleDetails(NewsArticleProperty objNewsArticle, int modifiedBy, out string errmsg);


        public abstract List<NewsArticleProperty> GetAllNewsArticleList();


        public abstract List<NewsArticleProperty> GetAllNewsList();

        public abstract List<NewsArticleProperty> GetAllArticleList();


        public abstract List<NewsArticleProperty> GetNewsDetailsById(int newsId);


        public abstract List<NewsArticleProperty> GetArticleDetailsById(int articleId);


        public abstract List<NewsArticleProperty> GetNewsArticleByName(string newsArticleName);


        public abstract List<NewsArticleProperty> GetNewsByName(string newsName);


        public abstract List<NewsArticleProperty> GetArticleByName(string articleName);
        public abstract List<NewsArticleProperty> GetLatestNews();
       

        #endregion

        #region "ITestimonials"

        public abstract int InsertTestimonilasDetails(TestimonialProperty objTestimonilas, int createdBy, out string errmsg);
        public abstract int UpdateTestimonilasDetails(TestimonialProperty objTestimonilas, int createdBy, out string errmsg);

        public abstract List<TestimonialProperty> GetTestimonialsByUserId(int UserID);
        public abstract List<TestimonialProperty> GetTestimonialsDetails();
        public abstract List<TestimonialProperty> GetTestimonialsDetailsById(int TestimonialId);

        #endregion

        public abstract List<NoticeDetails> GetLatestNotice();
    }
}
