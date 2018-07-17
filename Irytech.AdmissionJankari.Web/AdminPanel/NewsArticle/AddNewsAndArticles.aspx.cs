using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.NewsArticle
{
    public partial class AddNewsAndArticles : SecurePage 
    { 
         Common _objCommon;
        NewsArticleProperty _objNewsDetails;
        protected void Page_Load(object sender, EventArgs e)
        {
            FlUpload.uploadToDirectory = new Common().GetFilepath("NewsImage");
           if (IsPostBack) return; ValidateFields();
            GetUrlTitle();
            lblHeader.Text = "Add News Master";
            if (Request.QueryString["NewsId"] != null)
            {
                BindNewsAndArticlesDetails(Convert.ToInt16(Request.QueryString["NewsId"]));
            }
        }
        private void GetUrlTitle()
        {
            hdnNewsUrl.Value = Convert.ToString(ApplicationSettings.Instance.UrlLenght);
            hdnNewsMetaTag.Value = Convert.ToString(ApplicationSettings.Instance.MetaTagLenght);
            hdnNewsTitle.Value = Convert.ToString(ApplicationSettings.Instance.TitleLenght);
            hdnNewsMetaDesc.Value = Convert.ToString(ApplicationSettings.Instance.MetaKeywordLenght);
        }
       
        #region validatefields
        private void ValidateFields()
        {
            _objCommon=new Common();
            rfvNewsSubject.ErrorMessage = _objCommon.GetValidationMessage("rfvNewsSubject");
            rfvNewsTitle.ErrorMessage = _objCommon.GetValidationMessage("rfvNewsTitle");
            rfvNewsMetaTag.ErrorMessage = _objCommon.GetValidationMessage("rfvNewsMetaTag");
            rfvNewsShortDesc.ErrorMessage = _objCommon.GetValidationMessage("rfvNewsShortDesc");
            rfvNewsUrl.ErrorMessage = _objCommon.GetValidationMessage("rfvNewsUrl");
            rfvNewsBy.ErrorMessage = _objCommon.GetValidationMessage("rfvNewsBy");
            rfvNewsDate.ErrorMessage = _objCommon.GetValidationMessage("rfvNewsDate");
           rfvNewsMetaDesc.ErrorMessage = _objCommon.GetValidationMessage("rfvNewsMetaDesc");
           //rfvImageUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvImageUpload");
           rfvExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvExcelUpload");
        }
        #endregion
        #region BindNewsAndArticlesDetails
        private void BindNewsAndArticlesDetails(int newsId)
        {
            try
            {
                var data = NewsArticleNoticeProvider.Instance.GetNewsDetailsById(newsId);
                if (data.Count > 0)
                {
                    var query = data.Select(result => new
                                                          {
                                                              newsSubject = result.NewsSubject,
                                                              newsUrl = result.NewsUrl,
                                                              newsTitle = result.NewsTitle,
                                                              newsTag = result.NewsTag,
                                                              newsMetaDesc = result.NewsMetaDesc,
                                                              newsDesc = result.NewsDesc,
                                                              newsDate = result.NewsDate,
                                                              newsBy = result.NewsBy,
                                                              newsStatus = result.NewsStatus,
                                                              newsShortDesc = result.NewsShortDesc,
                                                              newsImage = result.NewsImage
                                                          }).First();

                    txtNewsSubject.Text = !string.IsNullOrEmpty(Convert.ToString(query.newsSubject))
                                              ? query.newsSubject
                                              : "N/A";
                    var img = query.newsImage != "" ? query.newsImage : "N/A";
                    _objCommon.GetFilepath("NoticeImage");
                    hdnFileName.Value = img;
                    txtNewsUrl.Text = !string.IsNullOrEmpty(Convert.ToString(query.newsUrl)) ? query.newsUrl : "N/A";
                    txtNewsTitle.Text = !string.IsNullOrEmpty(Convert.ToString(query.newsTitle))
                                            ? query.newsTitle
                                            : "N/A";
                    fckNewsDesc.FckValue = !string.IsNullOrEmpty(Convert.ToString(query.newsDesc))
                                               ? query.newsDesc
                                               : "N/A";
                    txtNewsBy.Text = !string.IsNullOrEmpty(Convert.ToString(query.newsBy)) ? query.newsBy : "N/A";
                    txtNewsMetaTag.Text = !string.IsNullOrEmpty(Convert.ToString(query.newsTag)) ? query.newsTag : "N/A";
                    txtNewsMetaDesc.Text = !string.IsNullOrEmpty(Convert.ToString(query.newsMetaDesc))
                                               ? query.newsMetaDesc
                                               : "N/A";
                    txtNewsDate.Text = Convert.ToString(query.newsDate) != "" ? Convert.ToString(query.newsDate) : "N/A";
                    txtShortDesc.Text = !string.IsNullOrEmpty(Convert.ToString(query.newsShortDesc))
                                            ? query.newsShortDesc
                                            : "N/A";
                    chkStatus.Checked = query.newsStatus;
                    lblInsertUpdate.Text = "Update Record Of " + query.newsTitle;
                    btnSave.Text = "Update";
                    lblHeader.Text = "Edit News And Articles";
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BindNewsAndArticlesDetails in AddNewsAndArticles.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        #endregion
        #region BtnSaveClick
        protected void BtnSaveClick(object sender, EventArgs e)
        {
            InsertUpdateNewsDetails();

        }
        #endregion
        #region insert and update
        private void InsertUpdateNewsDetails()
        {
            try
            {
                int result;
                string errMsg;

                var fileName = this.FlUpload.UploadedImageName;
                if (fileName != null)
                {
                    hdnFileName.Value = fileName;
                }

                _objNewsDetails = new NewsArticleProperty
                                      {
                                          NewsSubject = txtNewsSubject.Text.Trim().TrimEnd('-'),
                                          NewsUrl = txtNewsUrl.Text.Trim().TrimEnd('-'),
                                          NewsTitle = txtNewsTitle.Text.Trim().TrimEnd('-'),
                                          NewsTag = txtNewsMetaTag.Text.Trim(),
                                          NewsMetaDesc = txtNewsMetaDesc.Text.Trim().TrimEnd('-'),
                                          NewsDesc = fckNewsDesc.FckValue.Trim().TrimEnd('-'),
                                          NewsImage = hdnFileName.Value,
                                          NewsShortDesc = txtShortDesc.Text.Trim().TrimEnd('-'),
                                          NewsStatus = chkStatus.Checked,
                                          NewsBy = txtNewsBy.Text.Trim(),
                                          NewsDate =
                                              txtNewsDate.Text == ""
                                                  ? DateTime.Now
                                                  : Convert.ToDateTime(txtNewsDate.Text.Trim()),
                                      };
                if (btnSave.Text == "Save")
                {
                    result = NewsArticleNoticeProvider.Instance.InsertNewsDetails(_objNewsDetails, LoggedInUserId,
                                                                                  out errMsg);
                    if (result <= 0) return;
                    Response.Redirect("NewsAndArticles.aspx");
                }
                else
                {
                    _objNewsDetails.NewsId = Convert.ToInt16((Request.QueryString["NewsId"]));
                    result = NewsArticleNoticeProvider.Instance.UpdateNewsDetails(_objNewsDetails, LoggedInUserId,
                                                                                  out errMsg);
                    if (result <= 0) return;
                    Response.Redirect("NewsAndArticles.aspx",true);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing InsertUpdateNewsDetails in AddNewsAndArticles.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void BtnUploadClick(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/NewsDetails.xlsx");
            if (path == null) throw new ArgumentNullException("path");
            var objDocProcess = new System.Diagnostics.Process { EnableRaisingEvents = false, StartInfo = { FileName = @path } };
            objDocProcess.Start();
        }

        protected void BtnUploadClick1(object sender, EventArgs e)
        {
            _objCommon = new Common();
            try
            {
                var objClsOledbdatalayer = new ClsOleDBDataWrapper();
                var path = MapPath(fulUploadExcel.FileName);
                fulUploadExcel.SaveAs(path);
                var excelSheets = objClsOledbdatalayer.CountTotalSheets(path);
                if (excelSheets.Length <= 0) return;
                foreach (string t in excelSheets)
                {
                    DataSet ds = objClsOledbdatalayer.getdata(path, t);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                        {

                            var errMsg = "";
                            _objNewsDetails = new NewsArticleProperty
                                                  {
                                                      NewsSubject = ds.Tables[0].Rows[j]["NewsSubject"].ToString(),
                                                      NewsUrl = ds.Tables[0].Rows[j]["NewsUrl"].ToString(),
                                                      NewsTitle = ds.Tables[0].Rows[j]["NewsTitle"].ToString(),
                                                      NewsTag = ds.Tables[0].Rows[j]["NewsTag"].ToString(),
                                                      NewsMetaDesc = ds.Tables[0].Rows[j]["NewsMetaDesc"].ToString(),
                                                      NewsDesc = ds.Tables[0].Rows[j]["NewsDesc"].ToString(),
                                                      NewsImage = ds.Tables[0].Rows[j]["NewsImage"].ToString(),
                                                      NewsShortDesc = ds.Tables[0].Rows[j]["NewsShortDesc"].ToString(),
                                                      NewsStatus = ds.Tables[0].Rows[j]["NewsStatus"].ToString() == "True" ? true : false,
                                                      NewsBy = ds.Tables[0].Rows[j]["NewsBy"].ToString(),
                                                      NewsDate = ds.Tables[0].Rows[j]["NewsDate"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(ds.Tables[0].Rows[j]["NewsDate"].ToString()),
                                                  };
                            var result = NewsArticleNoticeProvider.Instance.InsertNewsDetails(_objNewsDetails, LoggedInUserId, out errMsg);

                        }
                    }
                }
            }
            catch (Exception EX)
            { }
        }
        

        #endregion
    }
}