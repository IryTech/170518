using System.Collections.Generic;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.DAL;
using System.Data;
using System.Data.SqlClient;
using System;


namespace IryTech.AdmissionJankari.BL
{
    public class NewsArticleNotice : NewsArticleNoticeProvider
    {
          private   DbWrapper _objDataWrapper;
          private  DataSet  _dataset;
          private   int _i;
        public override int InsertNoticeCategory(string noticeCategoryName, bool status, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";

            try
            {
                _objDataWrapper.AddParameter("@NoticeCategoryName", noticeCategoryName);
                _objDataWrapper.AddParameter("@NoticeCategoryStatus", status);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objErrMsg = (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateNoticeCategory");
                if (objErrMsg != null && objErrMsg.Value != null)
                    errmsg = Convert.ToString(objErrMsg.Value);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertNoticeCategory in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;

        }

        public override int UpdateNoticeCategory(int noticeCategoryId, string noticeCategoryName, bool status, int modifiedBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";

            try
            {
                _objDataWrapper.AddParameter("@NoticeCategoryId", noticeCategoryId);
                _objDataWrapper.AddParameter("@NoticeCategoryName", noticeCategoryName);
                _objDataWrapper.AddParameter("@NoticeCategoryStatus", status);
                _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
                var objErrMsg = (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateNoticeCategory");
                if (objErrMsg != null && objErrMsg.Value != null)
                    errmsg = Convert.ToString(objErrMsg.Value);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateNoticeCategory in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override List<NewArticleNoticeProperty> GetAllNoticeCategoryList()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataset = new DataSet();
            var noticeCategoryList = new List<NewArticleNoticeProperty>();
            try
            {
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNoticeCategoryList");
                 noticeCategoryList=  BindNoticeCategoryObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllNoticeCategoryList in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return noticeCategoryList;

        }

        public override List<NewArticleNoticeProperty> GetNoticeCategoryListById(int noticeCategoryId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataset = new DataSet();
            var noticeCategoryList = new List<NewArticleNoticeProperty>();
            try
            {
                _objDataWrapper.AddParameter("@NoticeCategoryId", noticeCategoryId);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNoticeCategoryList");
                noticeCategoryList = BindNoticeCategoryObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetNoticeCategoryListById in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return noticeCategoryList;
        }

        public override int InsertNoticeDetails(NoticeDetails objNoticeDetails, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";

            try
            {
                _objDataWrapper.AddParameter("@NoticeUrl", objNoticeDetails.NoticeUrl);
                _objDataWrapper.AddParameter("@NoticeShortDesc", objNoticeDetails.NoticeShortDesc);
                _objDataWrapper.AddParameter("@NoticeSubject", objNoticeDetails.NoticeSubject);
                _objDataWrapper.AddParameter("@NoticeTitle", objNoticeDetails.NoticeTitle);
                _objDataWrapper.AddParameter("@NoticeMetaDesc", objNoticeDetails.NoticeMetaDesc);
                _objDataWrapper.AddParameter("@NoticeMetaTag", objNoticeDetails.NoticeMetaTag);
                _objDataWrapper.AddParameter("@NoticeTypeId", objNoticeDetails.NoticeTypeId);
                _objDataWrapper.AddParameter("@NoticeDesc", objNoticeDetails.NoticeDesc);
                _objDataWrapper.AddParameter("@NoticeImage", objNoticeDetails.NoticeImage);
                _objDataWrapper.AddParameter("@NoticeStatus", objNoticeDetails.NoticeStatus);
                _objDataWrapper.AddParameter("@RealatedCollegeId", objNoticeDetails.RealtedCollegeId);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objErrMsg = (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateNoticeDetails");
                if (objErrMsg != null && objErrMsg.Value != null)
                    errmsg = Convert.ToString(objErrMsg.Value);
                    

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertNoticeDetails in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int UpdateNoticeDetails(NoticeDetails objNoticeDetails, int modifiedBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@NoticeShortDesc", objNoticeDetails.NoticeShortDesc);
                _objDataWrapper.AddParameter("@NoticeId", objNoticeDetails.NoticeId);
                _objDataWrapper.AddParameter("@NoticeUrl", objNoticeDetails.NoticeUrl);
                _objDataWrapper.AddParameter("@NoticeSubject", objNoticeDetails.NoticeSubject);
                _objDataWrapper.AddParameter("@NoticeTitle", objNoticeDetails.NoticeTitle);
                _objDataWrapper.AddParameter("@NoticeMetaDesc", objNoticeDetails.NoticeMetaDesc);
                _objDataWrapper.AddParameter("@NoticeMetaTag", objNoticeDetails.NoticeMetaTag);
                _objDataWrapper.AddParameter("@NoticeTypeId", objNoticeDetails.NoticeTypeId);
                _objDataWrapper.AddParameter("@NoticeDesc", objNoticeDetails.NoticeDesc);
                _objDataWrapper.AddParameter("@NoticeImage", objNoticeDetails.NoticeImage);
                _objDataWrapper.AddParameter("@NoticeStatus", objNoticeDetails.NoticeStatus);
                _objDataWrapper.AddParameter("@RealatedCollegeId", objNoticeDetails.RealtedCollegeId);
                _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
                var objErrMsg = (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateNoticeDetails");
                if (objErrMsg != null && objErrMsg.Value != null)
                    errmsg = Convert.ToString(objErrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertNoticeDetails in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override List<NoticeDetails> GetAllNoticeList()
        {
            var noticeDetailsList = new List<NoticeDetails>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataset = new DataSet();
        
            try
            {
               
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNoticeList");
                noticeDetailsList = BindNoticeDetailsObject(_dataset.Tables[0]);
                
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllNoticeList in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return noticeDetailsList;
        }
        public override List<NoticeDetails> GetAllNoticeListByPaging(out int totalRecords, int pageNum = 0, int pageSize = 0)
        {
            var noticeDetailsList = new List<NoticeDetails>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataset = new DataSet();
            totalRecords = 0;
            try
            {
                _objDataWrapper.AddParameter("@PageNum", pageNum);
                _objDataWrapper.AddParameter("@PageSize", pageSize);
                var objTotalRecords = (SqlParameter)(_objDataWrapper.AddParameter("@TotalRowsNum", "", SqlDbType.Int, ParameterDirection.Output));
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNoticeListSearch");
                noticeDetailsList = BindNoticeDetailsObject(_dataset.Tables[0]);
                totalRecords = Convert.ToInt32(objTotalRecords.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllNoticeListByPaging in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return noticeDetailsList;
        }

        public override List<NoticeDetails> GetNoticeListById(int noticeId)
        {
            var noticeDetailsList = new List<NoticeDetails>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataset = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@NoticeId", noticeId);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNoticeList");
                noticeDetailsList = BindNoticeDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetNoticeListById in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return noticeDetailsList;
        }

        public override List<NoticeDetails> GetNoticeListByName(string noticeName)
        {
            var noticeDetailsList = new List<NoticeDetails>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataset = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@NoticeName", noticeName);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNoticeList");
                noticeDetailsList = BindNoticeDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetNoticeListByName in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return noticeDetailsList;
        }
        public override List<NoticeDetails> GetLatestNotice()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objNoticeList = new List<NoticeDetails>();
            _dataset = new DataSet();
            try
            {
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetLatestNotice");
                objNoticeList = BindNoticeDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing Aj_Proc_GetLatestNotice in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objNoticeList;
        }
        public override List<NoticeDetails> GetNoticeListOfColleges()
        {
            var noticeDetailsList = new List<NoticeDetails>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataset = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@RelatedToCollege", "Yes");
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNoticeList");
                noticeDetailsList = BindNoticeDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetNoticeListOfColleges in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return noticeDetailsList;
        }

        public override List<NoticeDetails> GetNoticeListOfParticulerCollege(int collegeId)
        {
            var noticeDetailsList = new List<NoticeDetails>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataset = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@CollegeId", collegeId);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNoticeList");
                noticeDetailsList = BindNoticeDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetNoticeListOfParticulerCollege in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return noticeDetailsList;
        }

        public override List<NoticeDetails> GetNoticeListByNoticeCategory(int noticeCategoryId)
        {
            var noticeDetailsList = new List<NoticeDetails>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataset = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@NoticeCategoryId", noticeCategoryId);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNoticeList");
                noticeDetailsList = BindNoticeDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetNoticeListByNoticeCategory in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return noticeDetailsList;
        }

        public override int InsertNewsDetails(NewsArticleProperty objNewsArticle, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";

            try
            {
                _objDataWrapper.AddParameter("@NewsUrl", objNewsArticle.NewsUrl);
                _objDataWrapper.AddParameter("@NewsTitle", objNewsArticle.NewsTitle);
                _objDataWrapper.AddParameter("@NewsMetaDesc", objNewsArticle.NewsMetaDesc);
                _objDataWrapper.AddParameter("@NewsMetaTag", objNewsArticle.NewsTag);
                _objDataWrapper.AddParameter("@NewsSubject", objNewsArticle.NewsSubject);
                _objDataWrapper.AddParameter("@NewsImage", objNewsArticle.NewsImage);
                _objDataWrapper.AddParameter("@NewsBy", objNewsArticle.NewsBy);
                _objDataWrapper.AddParameter("@NewsDate", objNewsArticle.NewsDate);
                _objDataWrapper.AddParameter("@NewsShortDesc", objNewsArticle.NewsShortDesc);
                _objDataWrapper.AddParameter("@NewsDesc", objNewsArticle.NewsDesc);
                _objDataWrapper.AddParameter("@NewsType", NewsArticleType.News);
                _objDataWrapper.AddParameter("@NewsStatus", objNewsArticle.NewsStatus);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objerrMsg = 
                        (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateNewsArticleDetails");
                if (objerrMsg != null && objerrMsg.Value != null)
                    errmsg = Convert.ToString(objerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertNewsDetails in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int InsertArticleDetails(NewsArticleProperty objNewsArticle, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";

            try
            {
                _objDataWrapper.AddParameter("@NewsUrl", objNewsArticle.NewsUrl);
                _objDataWrapper.AddParameter("@NewsTitle", objNewsArticle.NewsTitle);
                _objDataWrapper.AddParameter("@NewsMetaDesc", objNewsArticle.NewsMetaDesc);
                _objDataWrapper.AddParameter("@NewsMetaTag", objNewsArticle.NewsTag);
                _objDataWrapper.AddParameter("@NewsSubject", objNewsArticle.NewsSubject);
                _objDataWrapper.AddParameter("@NewsImage", objNewsArticle.NewsImage);
                _objDataWrapper.AddParameter("@NewsBy", objNewsArticle.NewsBy);
                _objDataWrapper.AddParameter("@NewsDate", objNewsArticle.NewsDate);
                _objDataWrapper.AddParameter("@NewsShortDesc", objNewsArticle.NewsShortDesc);
                _objDataWrapper.AddParameter("@NewsDesc", objNewsArticle.NewsDesc);
                _objDataWrapper.AddParameter("@NewsType", NewsArticleType.Article);
                _objDataWrapper.AddParameter("@NewsStatus", objNewsArticle.NewsStatus);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objerrMsg =
                        (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateNewsArticleDetails");
                if (objerrMsg != null && objerrMsg.Value != null)
                    errmsg = Convert.ToString(objerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertArticleDetails in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int UpdateNewsDetails(NewsArticleProperty objNewsArticle, int modifiedBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";

            try
            {
                _objDataWrapper.AddParameter("@NewsId", objNewsArticle.NewsId);
                _objDataWrapper.AddParameter("@NewsUrl", objNewsArticle.NewsUrl);
                _objDataWrapper.AddParameter("@NewsTitle", objNewsArticle.NewsTitle);
                _objDataWrapper.AddParameter("@NewsMetaDesc", objNewsArticle.NewsMetaDesc);
                _objDataWrapper.AddParameter("@NewsMetaTag", objNewsArticle.NewsTag);
                _objDataWrapper.AddParameter("@NewsSubject", objNewsArticle.NewsSubject);
                _objDataWrapper.AddParameter("@NewsImage", objNewsArticle.NewsImage);
                _objDataWrapper.AddParameter("@NewsBy", objNewsArticle.NewsBy);
                _objDataWrapper.AddParameter("@NewsDate", objNewsArticle.NewsDate);
                _objDataWrapper.AddParameter("@NewsShortDesc", objNewsArticle.NewsShortDesc);
                _objDataWrapper.AddParameter("@NewsDesc", objNewsArticle.NewsDesc);
                _objDataWrapper.AddParameter("@NewsType", NewsArticleType.News);
                _objDataWrapper.AddParameter("@NewsStatus", objNewsArticle.NewsStatus);
                _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
                var objerrMsg =
                        (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateNewsArticleDetails");
                if (objerrMsg != null && objerrMsg.Value != null)
                    errmsg = Convert.ToString(objerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateNewsDetails in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int UpdateArticleDetails(NewsArticleProperty objNewsArticle, int modifiedBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";

            try
            {
                _objDataWrapper.AddParameter("@NewsId", objNewsArticle.NewsId);
                _objDataWrapper.AddParameter("@NewsUrl", objNewsArticle.NewsUrl);
                _objDataWrapper.AddParameter("@NewsTitle", objNewsArticle.NewsTitle);
                _objDataWrapper.AddParameter("@NewsMetaDesc", objNewsArticle.NewsMetaDesc);
                _objDataWrapper.AddParameter("@NewsMetaTag", objNewsArticle.NewsTag);
                _objDataWrapper.AddParameter("@NewsSubject", objNewsArticle.NewsSubject);
                _objDataWrapper.AddParameter("@NewsImage", objNewsArticle.NewsImage);
                _objDataWrapper.AddParameter("@NewsBy", objNewsArticle.NewsBy);
                _objDataWrapper.AddParameter("@NewsDate", objNewsArticle.NewsDate);
                _objDataWrapper.AddParameter("@NewsShortDesc", objNewsArticle.NewsShortDesc);
                _objDataWrapper.AddParameter("@NewsDesc", objNewsArticle.NewsDesc);
                _objDataWrapper.AddParameter("@NewsType", NewsArticleType.Article);
                _objDataWrapper.AddParameter("@NewsStatus", objNewsArticle.NewsStatus);
                _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
                var objerrMsg =
                        (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateNewsArticleDetails");
                if (objerrMsg != null && objerrMsg.Value != null)
                    errmsg = Convert.ToString(objerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateArticleDetails in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override List<NewsArticleProperty> GetAllNewsArticleList()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objNewsArticleList = new List<NewsArticleProperty>();
            _dataset = new DataSet();
            try
            {
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNewsArticleList");
                objNewsArticleList = BindNewsArticleDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllNewsArticleList in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objNewsArticleList;
        }
        public override List<NewsArticleProperty> GetAllNewsListByPage(out int totalRecords, int pageNum = 0, int pageSize = 0)
        {
            var objNewsArticleProperty = new List<NewsArticleProperty>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataset = new DataSet();
            totalRecords = 0;
            try
            {
                _objDataWrapper.AddParameter("@PageNum", pageNum);
                _objDataWrapper.AddParameter("@PageSize", pageSize);
                var ObjTotalRecords = (SqlParameter)(_objDataWrapper.AddParameter("@TotalRowsNum", "", SqlDbType.Int, ParameterDirection.Output));
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNewsListSearch");
                objNewsArticleProperty = BindNewsArticleDetailsObject(_dataset.Tables[0]);
                if (ObjTotalRecords.Value != DBNull.Value || ObjTotalRecords != null)
                {
                    totalRecords = Convert.ToInt32(ObjTotalRecords.Value);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllNoticeListByPaging in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objNewsArticleProperty;
        }

        public override List<NewsArticleProperty> GetAllNewsList()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objNewsArticleList = new List<NewsArticleProperty>();
            _dataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@NewsType", NewsArticleType.News);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNewsArticleList");
                objNewsArticleList = BindNewsArticleDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllNewsList in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objNewsArticleList;
        }

        public override List<NewsArticleProperty> GetAllArticleList()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objNewsArticleList = new List<NewsArticleProperty>();
            _dataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@NewsType", NewsArticleType.Article);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNewsArticleList");
                objNewsArticleList = BindNewsArticleDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllArticleList in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objNewsArticleList;
        }

        public override List<NewsArticleProperty> GetNewsDetailsById(int newsId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objNewsArticleList = new List<NewsArticleProperty>();
            _dataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@NewsId", newsId);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNewsArticleList");
                objNewsArticleList = BindNewsArticleDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetNewsDetailsById in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objNewsArticleList;
        }

        public override List<NewsArticleProperty> GetArticleDetailsById(int articleId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objNewsArticleList = new List<NewsArticleProperty>();
            _dataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@NewsId", articleId);
                _objDataWrapper.AddParameter("@NewsType", NewsArticleType.Article);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNewsArticleList");
                objNewsArticleList = BindNewsArticleDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetNewsDetailsById in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objNewsArticleList;
        }

        public override List<NewsArticleProperty> GetNewsArticleByName(string newsArticleName)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objNewsArticleList = new List<NewsArticleProperty>();
            _dataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@NewsName", newsArticleName);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNewsArticleList");
                objNewsArticleList = BindNewsArticleDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetNewsArticleByName in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objNewsArticleList;
        }

        public override List<NewsArticleProperty> GetNewsByName(string newsName)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objNewsArticleList = new List<NewsArticleProperty>();
            _dataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@NewsName", newsName);
                _objDataWrapper.AddParameter("@NewsType", NewsArticleType.News);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNewsArticleList");
                objNewsArticleList = BindNewsArticleDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetNewsByName in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objNewsArticleList;
        }

        public override List<NewsArticleProperty> GetArticleByName(string articleName)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objNewsArticleList = new List<NewsArticleProperty>();
            _dataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@NewsName", articleName);
                _objDataWrapper.AddParameter("@NewsType", NewsArticleType.Article);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetNewsArticleList");
                objNewsArticleList = BindNewsArticleDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetArticleByName in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objNewsArticleList;
        }
        public override List<NewsArticleProperty> GetLatestNews()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objNewsArticleList = new List<NewsArticleProperty>();
            _dataset = new DataSet();
            try
            {
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetLatestNews");
                objNewsArticleList = BindNewsArticleDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetLatestNews in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objNewsArticleList;
        }
        public override List<NewsArticleProperty> GetMostViewdNews()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objNewsArticleList = new List<NewsArticleProperty>();
            _dataset = new DataSet();
            try
            {
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetMostViewNews");
                objNewsArticleList = BindNewsArticleMostViewDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetMostViewdNews in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objNewsArticleList;
        }
        public override List<NoticeDetails> GetMostViewdNotice()
        {
            var noticeDetailsList = new List<NoticeDetails>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataset = new DataSet();
            try
            {

                _dataset = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetMostViewdNotice");
                noticeDetailsList = BindMostViewdNoticeDetailsObject(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetMostViewdNotice in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return noticeDetailsList;
        }
        #region INewsArticle Members
        private List<NewArticleNoticeProperty> BindNoticeCategoryObject(DataTable datatable)
        {
            var noticeCategoryList = new List<NewArticleNoticeProperty>();
            
            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {
                        var objNoticeCategory = new NewArticleNoticeProperty
                        {
                            NoticecategoryId = Convert.ToInt32(datatable.Rows[j]["AjNoticeCategoryId"]),
                            NoticeCategoryName = Convert.ToString(datatable.Rows[j]["AjNoticeCategoryName"]),
                            NoticeCategoryStatus = Convert.ToBoolean(datatable.Rows[j]["Status"])

                        };
                        noticeCategoryList.Add(objNoticeCategory);
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindNoticeCategoryObject in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return noticeCategoryList;
        }

        private List<NoticeDetails> BindNoticeDetailsObject(DataTable datatable)
        {
            var noticeDetailsList = new List<NoticeDetails>();
            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {
                        var objNoticeDetail = new NoticeDetails
                                    {
                                        NoticeDesc = (datatable.Rows[j]["AjNoticeDesc"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeDesc"]),
                                        NoticeId = (datatable.Rows[j]["AjNoticeId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjNoticeId"]),
                                        NoticeImage = (datatable.Rows[j]["AjNoticeImage"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeImage"]),
                                        NoticeMetaDesc =(datatable.Rows[j]["AjNoticeMetaDesc"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeMetaDesc"]),
                                        NoticeMetaTag =(datatable.Rows[j]["AjNoticeMetaTag"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeMetaTag"]),
                                        NoticeStatus = (datatable.Rows[j]["AjNoticeStatus"] is DBNull) ? true : Convert.ToBoolean(datatable.Rows[j]["AjNoticeStatus"]),
                                        NoticeSubject =(datatable.Rows[j]["AjNoticeSubject"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeSubject"]),
                                        NoticeTitle =(datatable.Rows[j]["AjNoticeTitle"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeTitle"]),
                                        NoticeTypeId = (datatable.Rows[j]["AjNoticeTypeId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjNoticeTypeId"]),
                                        NoticeTypeName =(datatable.Rows[j]["AjNoticeCategoryName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeCategoryName"]),
                                        NoticeUrl = (datatable.Rows[j]["AjNoticeUrl"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeUrl"]),
                                        RealtedCollegeId =(datatable.Rows[j]["AjRelatedCollegeId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjRelatedCollegeId"]),
                                        RelatedCollegeName =(datatable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCollegeBranchName"]),
                                        NoticeDate = (datatable.Rows[j]["CreatedOn"] is DBNull) ? System.DateTime.Now : Convert.ToDateTime(datatable.Rows[j]["CreatedOn"]),
                                       NoticeShortDesc =(datatable.Rows[j]["AjNoticeShortDescription"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeShortDescription"]),
                                    };
                        noticeDetailsList.Add(objNoticeDetail);
                    }
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindNoticeDetailsObject in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return noticeDetailsList;
        }
        private List<NoticeDetails> BindMostViewdNoticeDetailsObject(DataTable datatable)
        {
            var noticeDetailsList = new List<NoticeDetails>();
            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {
                        var objNoticeDetail = new NoticeDetails
                        {
                            NoticeDesc = (datatable.Rows[j]["AjNoticeDesc"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeDesc"]),
                            NoticeId = (datatable.Rows[j]["AjNoticeId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjNoticeId"]),
                            NoticeImage = (datatable.Rows[j]["AjNoticeImage"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeImage"]),
                            NoticeMetaDesc = (datatable.Rows[j]["AjNoticeMetaDesc"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeMetaDesc"]),
                            NoticeMetaTag = (datatable.Rows[j]["AjNoticeMetaTag"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeMetaTag"]),
                            NoticeStatus = (datatable.Rows[j]["AjNoticeStatus"] is DBNull) ? true : Convert.ToBoolean(datatable.Rows[j]["AjNoticeStatus"]),
                            NoticeSubject = (datatable.Rows[j]["AjNoticeSubject"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeSubject"]),
                            NoticeTitle = (datatable.Rows[j]["AjNoticeTitle"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeTitle"]),
                            NoticeTypeId = (datatable.Rows[j]["AjNoticeTypeId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjNoticeTypeId"]),
                            NoticeTypeName = (datatable.Rows[j]["AjNoticeCategoryName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeCategoryName"]),
                            NoticeUrl = (datatable.Rows[j]["AjNoticeUrl"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNoticeUrl"]),
                            RealtedCollegeId = (datatable.Rows[j]["AjRelatedCollegeId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjRelatedCollegeId"]),
                            NoticeDate = (datatable.Rows[j]["CreatedOn"] is DBNull) ? System.DateTime.Now : Convert.ToDateTime(datatable.Rows[j]["CreatedOn"])
                        };
                        noticeDetailsList.Add(objNoticeDetail);
                    }
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindMostViewdNoticeDetailsObject in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return noticeDetailsList;
        }

        private List<NewsArticleProperty> BindNewsArticleDetailsObject(DataTable datatable)
        {
            var objNewsArticleList = new List<NewsArticleProperty>();

            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for(var j=0;j<datatable.Rows.Count;j++)
                    {
                        var objNewsArticle = new NewsArticleProperty
                                    {

                                        NewsBy = (datatable.Rows[j]["AjNewsBy"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNewsBy"]),
                                        NewsDate = (datatable.Rows[j]["AjNewsDate"] is DBNull) ? System.DateTime.Now  : Convert.ToDateTime(datatable.Rows[j]["AjNewsDate"]),
                                        NewsId = (datatable.Rows[j]["AjNewsId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjNewsId"]),
                                        NewsImage = (datatable.Rows[j]["AjNewsImage"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNewsImage"]),
                                        NewsStatus = (datatable.Rows[j]["AjNewsStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjNewsStatus"]),
                                        NewsSubject = (datatable.Rows[j]["AjNewsSubject"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNewsSubject"]),
                                        NewsShortDesc = (datatable.Rows[j]["AjNewsShortDes"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNewsShortDes"]),
                                        NewsDesc = (datatable.Rows[j]["AjNewsLongDesc"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNewsLongDesc"]),
                                        NewsType = (datatable.Rows[j]["AjNewsType"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNewsType"]),
                                       };
                        objNewsArticleList.Add(objNewsArticle);
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindNewsArticleDetailsObject in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objNewsArticleList;
        }


        private List<NewsArticleProperty> BindNewsArticleMostViewDetailsObject(DataTable datatable)
        {
            var objNewsArticleList = new List<NewsArticleProperty>();

            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {
                        var objNewsArticle = new NewsArticleProperty
                        {

                            NewsBy = (datatable.Rows[j]["AjNewsBy"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNewsBy"]),
                            NewsId = (datatable.Rows[j]["AjNewsId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjNewsId"]),
                            NewsImage = (datatable.Rows[j]["AjNewsImage"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNewsImage"]),
                            NewsStatus = (datatable.Rows[j]["AjNewsStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjNewsStatus"]),
                            NewsSubject = (datatable.Rows[j]["AjNewsSubject"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNewsSubject"]),
                            NewsType = (datatable.Rows[j]["AjNewsType"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjNewsType"]),
                        };
                        objNewsArticleList.Add(objNewsArticle);
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindNewsArticleDetailsObject in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objNewsArticleList;
        }

        #endregion


        #region "ITestimonialMembers"
        public override int InsertTestimonilasDetails(TestimonialProperty objTestimonilasDetails, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";

            try
            {
                _objDataWrapper.AddParameter("@UserID", objTestimonilasDetails.UserID);
                _objDataWrapper.AddParameter("@UserImage", objTestimonilasDetails.UserImage);
                _objDataWrapper.AddParameter("@Testimonial", objTestimonilasDetails.Testimonials);
                _objDataWrapper.AddParameter("@TestimonialStatus", objTestimonilasDetails.TestimonilaStatus);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objErrMsg = (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateTestimonialDetails");
                if (objErrMsg != null && objErrMsg.Value != null)
                    errmsg = Convert.ToString(objErrMsg.Value);


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertTestimonilasDetails in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }
        public override int UpdateTestimonilasDetails(TestimonialProperty objTestimonilasDetails, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";

            try
            {
                _objDataWrapper.AddParameter("@TestimonialId", objTestimonilasDetails.TestimonialID);
                _objDataWrapper.AddParameter("@UserID", objTestimonilasDetails.UserID);
                _objDataWrapper.AddParameter("@UserImage", objTestimonilasDetails.UserImage);
                _objDataWrapper.AddParameter("@Testimonial", objTestimonilasDetails.Testimonials);
                _objDataWrapper.AddParameter("@TestimonialStatus", objTestimonilasDetails.TestimonilaStatus);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objErrMsg = (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateTestimonialDetails");
                if (objErrMsg != null && objErrMsg.Value != null)
                    errmsg = Convert.ToString(objErrMsg.Value);


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateTestimonilasDetails in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }
        #endregion



        public override List<TestimonialProperty> GetTestimonialsDetailsById( int TestmonialId)
        {
            var noticeDetailsList = new List<TestimonialProperty>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@TestimonilaId", TestmonialId);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_GetTestimonials");
                noticeDetailsList = BindTestimonialsDetails(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetMostViewdNotice in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return noticeDetailsList;
        }




        public override List<TestimonialProperty> GetTestimonialsByUserId(int UserID)
        {
            var noticeDetailsList = new List<TestimonialProperty>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataset = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@Userid", UserID);
                _dataset = _objDataWrapper.ExecuteDataSet("Aj_GetTestimonials");
                noticeDetailsList = BindTestimonialsDetails(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetMostViewdNotice in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return noticeDetailsList;
        }







        public override List<TestimonialProperty> GetTestimonialsDetails()
        {
            var noticeDetailsList = new List<TestimonialProperty>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataset = new DataSet();
            try
            {

                _dataset = _objDataWrapper.ExecuteDataSet("Aj_GetTestimonials");
                noticeDetailsList = BindTestimonialsDetails(_dataset.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetMostViewdNotice in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return noticeDetailsList;
        }


      

        private List<TestimonialProperty> BindTestimonialsDetails(DataTable datatable)
        {
            var testimonilasDetailsList = new List<TestimonialProperty>();
            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {
                        var objTestimonilasDetail = new TestimonialProperty
                        {
                            TestimonialID = (datatable.Rows[j]["AjTestimonialId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjTestimonialId"]),
                            UserID = (datatable.Rows[j]["AjUserId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjUserId"]),
                            Testimonials = (datatable.Rows[j]["AjTestimonial"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjTestimonial"]),
                            TestimonilaStatus = (datatable.Rows[j]["AjTestimonialStatus"] is DBNull) ? true : Convert.ToBoolean(datatable.Rows[j]["AjTestimonialStatus"]),
                            MobileNo = (datatable.Rows[j]["AjUserMobile"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUserMobile"]),
                            UserName = (datatable.Rows[j]["AjUserFullName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUserFullName"]),
                            UserImage = (datatable.Rows[j]["AjUserImage"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUserImage"]),
                        };

                        testimonilasDetailsList.Add(objTestimonilasDetail);
                    }
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindTestimonialsDetails in NewsArticleNotice.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return testimonilasDetailsList;
        }



       
    }
}
