using System;
using System.Data;
using System.Linq;
using IryTech.AdmissionJankari.BL;


namespace IryTech.AdmissionJankari20.Web.AdminPanel.NewsArticle
{
    public partial class NoticeCategory : SecurePage 
    {
        private Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateField();
            if (IsPostBack) return;
               BindNoticeCategory();
        }
        private void ValidateField()
        {
            _objCommon = new Common();
            
            if (ClsSingelton.aRegExpExcelUpload != null)
                revExcelUpload.ValidationExpression = ClsSingelton.aRegExpExcelUpload;
            revExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("revUploadExcel") ?? "N/A";
            rfvExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvExcelUpload");
        }

        private void BindNoticeCategory()
        {
            var data = NewsArticleNoticeProvider.Instance.GetAllNoticeCategoryList();
            if (data.Count > 0)
            {  rptNoticeCategory.Visible = true;
               rptNoticeCategory.DataSource = data;
               rptNoticeCategory.DataBind();
                
            }
            else
            {
                rptNoticeCategory.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords") ?? "N/A";
            }
        }

        private void InsertUpdateNoticeCategory()
        {
            try
            {
                int result;
                var errMsg = "";
                result = NewsArticleNoticeProvider.Instance.InsertNoticeCategory(txtNoticeCategory.Text.Trim(),
                                                                                 chkNoticeStatus.Checked,
                                                                                 LoggedInUserId,
                                                                                 out errMsg);
                if (result > 0)
                {
                    lblSuccess.Visible = true;
                    lblSuccess.Text = errMsg;
                    ClearFileds();
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = errMsg;
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing InsertUpdateNoticeCategory in NoticeCategory.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }



        [System.Web.Services.WebMethod]
        public static string UpdateNoticeCategory(int noticeCategoryId, string noticeCategoryName, bool noticeCategoryStatus)
        {
            var errMsg = "";
           
            var result =
                        NewsArticleNoticeProvider.Instance.UpdateNoticeCategory(
                           noticeCategoryId,
                           noticeCategoryName,
                            noticeCategoryStatus, new SecurePage().LoggedInUserId, out errMsg);

            return errMsg;
        }

        private void ClearFileds()
        {
            txtNoticeCategory.Text = string.Empty;
            chkNoticeStatus.Checked = false;
        }

        protected void BtnNoticeCategoryClick(object sender, EventArgs e)
        {
            InsertUpdateNoticeCategory();
            BindNoticeCategory();
        }

   

        protected void BtnSeeExcelFormatClick(object sender, EventArgs e)
        {

            var path = MapPath("~/AdminPanel/ExcelPreview/NoticeCategory.xlsx");
            if (path == null) throw new ArgumentNullException("path");
            var objDocProcess = new System.Diagnostics.Process { EnableRaisingEvents = false, StartInfo = { FileName = @path } };
            objDocProcess.Start();      
          
        }
        protected void BtnUploadClick(object sender, EventArgs e)
        {
            _objCommon = new Common();
            try
            {
                var objClsOledbdatalayer = new ClsOleDBDataWrapper();
                var path = MapPath(fulUploadExcel.FileName);
                fulUploadExcel.SaveAs(path);
                var excelSheets = objClsOledbdatalayer.CountTotalSheets(path);
                if (excelSheets.Length > 0)
                {
                    foreach (var t in excelSheets)
                    {
                        var ds = objClsOledbdatalayer.getdata(path, t);
                        if (ds.Tables[0].Rows.Count <= 0) continue;
                        for (var j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                        {
                            string errMsg;
                            NewsArticleNoticeProvider.Instance.InsertNoticeCategory(Convert.ToString(ds.Tables[0].Rows[j]["NoticeCategory"]),
                                                                                    ds.Tables[0].Rows[j]["NoticeCategoryStatus"].ToString() == "True", LoggedInUserId, out errMsg);
                        }
                        lblSuccess.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                        BindNoticeCategory();
                    }
                    
                }
                else
                {
                    lblError.Text = _objCommon.GetErrorMessage("lblErrMsg");
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BtnUploadClick in CommonWebServices.asmx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}