using System.Collections.Generic;
using IryTech.AdmissionJankari.BO;
using System;
namespace IryTech.BaseClassLibraray
{
    public interface INewsArticle
    {
        int InsertNoticeCategory(string noticeCategoryName, bool status, int createdBy, out string errmsg);
        int UpdateNoticeCategory(int noticeCategoryId, string noticeCategoryName, bool status, int modifiedBy, out string errmsg);
        List<NewArticleNoticeProperty> GetAllNoticeCategoryList();
        List<NewArticleNoticeProperty> GetNoticeCategoryListById(int noticeCategoryId);
        int InsertNoticeDetails(NoticeDetails objNoticeDetails, int createdBy, out string errmsg);
        int UpdateNoticeDetails(NoticeDetails objNoticeDetails, int modifiedBy, out string errmsg);
        List<NoticeDetails> GetAllNoticeList();
      
        List<NoticeDetails> GetNoticeListById(int noticeId);
        List<NoticeDetails> GetNoticeListByName(string noticeName);
        List<NoticeDetails> GetNoticeListOfColleges();
        List<NoticeDetails> GetNoticeListOfParticulerCollege(int collegeId);
        List<NoticeDetails> GetNoticeListByNoticeCategory(int noticeCategoryId);
        List<NoticeDetails> GetMostViewdNotice();

        int InsertNewsDetails(NewsArticleProperty objNewsArticle ,int createdBy,out string errmsg);
        int InsertArticleDetails(NewsArticleProperty objNewsArticle, int createdBy, out string errmsg);
        int UpdateNewsDetails(NewsArticleProperty objNewsArticle, int modifiedBy, out string errmsg);
        int UpdateArticleDetails(NewsArticleProperty objNewsArticle, int modifiedBy, out string errmsg);
        List<NewsArticleProperty>GetAllNewsArticleList();
        List<NewsArticleProperty>GetAllNewsList();
        List<NewsArticleProperty>GetAllArticleList();
        List<NewsArticleProperty>GetNewsDetailsById(int newsId);
        List<NewsArticleProperty>GetArticleDetailsById(int articleId);
        List<NewsArticleProperty>GetNewsArticleByName(string newsArticleName);
        List<NewsArticleProperty>GetNewsByName(string newsName);
        List<NewsArticleProperty>GetArticleByName(string articleName);
        List<NewsArticleProperty> GetMostViewdNews();

        List<NoticeDetails> GetAllNoticeListByPaging(out int totalRecords, int pageNum = 0, int pageSize = 0);

        List<NewsArticleProperty> GetAllNewsListByPage(out int TotalRecords, int PageNum = 0, int PageSize = 0);

    }
}
