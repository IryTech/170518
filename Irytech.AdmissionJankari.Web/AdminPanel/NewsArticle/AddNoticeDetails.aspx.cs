using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using System.Linq;
using IryTech.AdmissionJankari.Components;


namespace IryTech.AdmissionJankari20.Web.AdminPanel.NewsArticle
{
    public partial class AddNoticeDetails : SecurePage 
    {
        private NoticeDetails _objNoticeDetails;
        private Common _objCommon;
         

        protected void Page_Load(object sender, EventArgs e)
        {
            FlUpload.uploadToDirectory = new Common().GetFilepath("NoticeImage");
             if (IsPostBack) return;
            GetUrlTitle();
            lblHeader.Text = "Add Notice Master";
            BindNoticeCategory();
            BindRelatedColleges();
            ValidateField();
            if (Request.QueryString["NoticeId"] != null)
            {
                BindNoticeDetails(Request.QueryString["NoticeId"]);
            }
        }
     
        private void GetUrlTitle()
        {
            hdnNoticeUrl.Value = Convert.ToString(ApplicationSettings.Instance.UrlLenght);
            hdnNoticeMetaTag.Value = Convert.ToString(ApplicationSettings.Instance.MetaTagLenght);
            hdnNoticeTitle.Value = Convert.ToString(ApplicationSettings.Instance.TitleLenght);
            hdnNoticeMetaDesc.Value = Convert.ToString(ApplicationSettings.Instance.MetaKeywordLenght);
        }
        private void ValidateField()
        {
            _objCommon = new Common();
            rfvUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvUploadExcel") ?? "N/A";
            if (ClsSingelton.aRegExpExcelUpload != null)
                revExcelUpload.ValidationExpression = ClsSingelton.aRegExpExcelUpload;
           revExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("revUploadExcel") ?? "N/A";
          
        }

       
        private void BindNoticeCategory()
        {
            var noticeData = NewsArticleNoticeProvider.Instance.GetAllNoticeCategoryList();
            if (noticeData.Count <= 0)
            {
                ddlNoticeType.Items.Insert(0, new ListItem("Please Select","0"));
            }
            else
            {
                ddlNoticeType.DataSource = noticeData;
                ddlNoticeType.DataTextField = "NoticeCategoryName";
                ddlNoticeType.DataValueField = "NoticecategoryId";
                ddlNoticeType.DataBind();
                ddlNoticeType.Items.Insert(0, new ListItem("Please Select","0"));
            }
        }

        private void BindNoticeDetails(string noticeId)
        {
            try
            {
                var noticeData = NewsArticleNoticeProvider.Instance.GetNoticeListById(Convert.ToInt16(noticeId));

                if (noticeData.Count <= 0) return;
                {
                    var query = noticeData.Select(result => new
                                                                {
                                                                    noticeId = result.NoticeId,
                                                                    noticeImage = result.NoticeImage,
                                                                    noticeUrl = result.NoticeUrl,
                                                                    noticeDesc = result.NoticeDesc,
                                                                    noticeMetaTag = result.NoticeMetaTag,
                                                                    noticeMetaDesc = result.NoticeMetaDesc,
                                                                    noticeStatus = result.NoticeStatus,
                                                                    noticeTitle = result.NoticeTitle,
                                                                    noticeSubject = result.NoticeSubject,
                                                                    noticeTypeId = result.NoticeTypeId,
                                                                    relatedCollegeId = result.RealtedCollegeId,
                                                                    NoticeShortDesc = result.NoticeShortDesc
                                                                }).First();
                    txtNoticeUrl.Text = query.noticeUrl != "" ? query.noticeUrl : "N/A";

                    var img = query.noticeImage != "" ? query.noticeImage : "N/A";
                    var path = _objCommon.GetFilepath("NoticeImage");
                  if(!img.Equals("N/A"))
                      hdnFileName.Value = img;
                    txtNoticeTitle.Text = query.noticeTitle != "" ? query.noticeTitle : "N/A";
                    txtNoticeTag.Text = query.noticeMetaTag != "" ? query.noticeMetaTag : "N/A";
                    txtMetaDesc.Text = query.noticeMetaDesc != "" ? query.noticeMetaDesc : "N/A";
                    txtNoticeSubject.Text = query.noticeSubject != "" ? query.noticeSubject : "N/A";
                    fckNoticeDesc.FckValue = query.noticeDesc != "" ? query.noticeDesc : "N/A";
                    txtNoticeShortDesc.Text = query.NoticeShortDesc != "" ? query.NoticeShortDesc : "N/A";
                    imgNews.Visible = true;
                    imgNews.ImageUrl = String.Format("{0}{1}", "/image.axd?Notices=", string.IsNullOrEmpty(img) ? "NoImage.jpg" : img);
                   imgNews.AlternateText = query.noticeSubject;
                    if (!string.IsNullOrEmpty(query.relatedCollegeId.ToString()))
                    {
                        if (query.relatedCollegeId.ToString().Equals("0"))
                        {
                            rbtNoticeType.Enabled = false;
                            liRelated.Visible = false;
                            rbtNoticeType.SelectedValue = "1";

                        }
                        else
                        {
                            rbtNoticeType.SelectedValue = "0";
                            ddlRelatedCollege.SelectedValue =
                                Convert.ToString(query.relatedCollegeId > 0 ? query.relatedCollegeId : 0);
                            rbtNoticeType.Enabled = false;

                        }

                    }

                    chkNoticeStatus.Checked = query.noticeStatus;
                    ddlNoticeType.SelectedValue = Convert.ToString(query.noticeTypeId > 0 ? query.noticeTypeId : 0);
                    lblInsertUpdate.Text = "Record Update of " + query.noticeTitle;
                    lblHeader.Text = "Edit Notices";
                }
                btnSave.Text = "Update";
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing  in BindNoticeDetailsat at page AddNoticeDetails.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindRelatedColleges()
        {
            var collegeData = CollegeProvider.Instance.GetCollegeList();
            if(collegeData.Count>0)
            {
                ddlRelatedCollege.DataSource = collegeData;
                ddlRelatedCollege.DataTextField = "CollegeBranchName";
                ddlRelatedCollege.DataValueField = "CollegeIdBranchId";
                ddlRelatedCollege.DataBind();
                ddlRelatedCollege.Items.Insert(0,new ListItem("--Select--","0"));
            }else
            {
                ddlRelatedCollege.Items.Insert(0, new ListItem("--Select--", "0"));
            }

        }
        protected void BtnSaveClick(object sender, EventArgs e)
        {
           
            InsertUpdateNotice();
        }
        private void InsertUpdateNotice()
        {
            try
            {
                var errMsg = "";
                var fileName = this.FlUpload.UploadedImageName;
                if (fileName != null)
                {
                    hdnFileName.Value = fileName;
                }


                _objNoticeDetails = new NoticeDetails
                                        {
                                            NoticeShortDesc = txtNoticeShortDesc.Text.Trim().TrimEnd('-'),
                                            NoticeUrl = txtNoticeUrl.Text.Trim().TrimEnd('-'),
                                            NoticeTitle = txtNoticeTitle.Text.Trim().TrimEnd('-'),
                                            NoticeSubject = txtNoticeSubject.Text.Trim().TrimEnd('-'),
                                            NoticeMetaTag = txtNoticeTag.Text.Trim().TrimEnd('-'),
                                            NoticeMetaDesc = txtMetaDesc.Text.Trim().TrimEnd('-'),
                                            NoticeDesc = fckNoticeDesc.FckValue.Trim().TrimEnd('-'),
                                            NoticeStatus = chkNoticeStatus.Checked,
                                            RealtedCollegeId = Convert.ToInt16(ddlRelatedCollege.SelectedValue),
                                            NoticeImage = hdnFileName.Value,
                                            NoticeTypeId = Convert.ToInt16(ddlNoticeType.SelectedValue)
                                        };
                if (btnSave.Text == "Save")
                {
                    var result = NewsArticleNoticeProvider.Instance.InsertNoticeDetails(_objNoticeDetails,
                                                                                        LoggedInUserId, out errMsg);
                    if (result <= 0) return;
                    Response.Redirect("NoticeMaster.aspx");
                }
                else
                {
                   
                    _objNoticeDetails.NoticeId = Convert.ToInt16(Request.QueryString["NoticeId"]);
                    var result = NewsArticleNoticeProvider.Instance.UpdateNoticeDetails(_objNoticeDetails,
                                                                                        LoggedInUserId, out errMsg);
                    if (result <= 0) return;
                    btnSave.Text = "Update";
                    Response.Redirect("NoticeMaster.aspx");
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing  in InsertUpdateNotice at page AddNoticeDetails.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void BtnSeeExcel(object sender, EventArgs e)
        {
            try
            {
                var path = MapPath("~/AdminPanel/ExcelPreview/NoticeDetails.xlsx");
                if (path == null) throw new ArgumentNullException("path");
                var objDocProcess = new System.Diagnostics.Process
                                        {EnableRaisingEvents = false, StartInfo = {FileName = @path}};
                objDocProcess.Start();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing  in BtnSeeExcel at page AddNoticeDetails.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void BtnUploadExcel(object sender, EventArgs e)
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
                        for (var j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                        {
                            var errMsg = "";
                            _objNoticeDetails = new NoticeDetails
                                                    {
                                                        NoticeUrl = ds.Tables[0].Rows[j]["NoticeUrl"].ToString(),
                                                        NoticeTitle = ds.Tables[0].Rows[j]["NoticeTitle"].ToString(),
                                                        NoticeSubject = ds.Tables[0].Rows[j]["NoticeSubject"].ToString(),
                                                        NoticeMetaTag = ds.Tables[0].Rows[j]["NoticeMetaTag"].ToString(),
                                                        NoticeShortDesc = ds.Tables[0].Rows[j]["NoticeShortDesc"].ToString(),
                                                        NoticeMetaDesc = ds.Tables[0].Rows[j]["NoticeMetaDesc"].ToString(),
                                                        NoticeDesc = ds.Tables[0].Rows[j]["NoticeDesc"].ToString(),
                                                        NoticeStatus =
                                                            ds.Tables[0].Rows[j]["NoticeStatus"].ToString() == "True"
                                                                ? true
                                                                : false,
                                                        RealtedCollegeId =
                                                            Convert.ToInt16(
                                                                ds.Tables[0].Rows[j]["RealtedCollegeId"].ToString()),
                                                        NoticeImage = ds.Tables[0].Rows[j]["NoticeImage"].ToString(),
                                                        NoticeTypeId =
                                                            Convert.ToInt16(ds.Tables[0].Rows[j]["NoticeTypeId"].ToString())
                                                    };
                            var result = NewsArticleNoticeProvider.Instance.InsertNoticeDetails(_objNoticeDetails, LoggedInUserId,
                                                                                                out errMsg);
                        }
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
                const string addInfo = "Error while executing  in BtnUploadExcel at page AddNoticeDetails.aspx.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

       
    }
}