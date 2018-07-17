using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using System.Web.Services;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Course
{
    public partial class CourseCategory : SecurePage 
    {
        private Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
           
            ValidationErrorMessages();
            BindCourseCategory();
          }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon=new Common();
            var data = CourseProvider.Instance.GetAllCourseCategoryList();
              if (data.Count > 0)
            {
            try
            {
                rptCourseCategoryData.Visible = true;
                lblInform.Visible = false;
                ucCustomPaging.BindDataWithPaging(rptCourseCategoryData, Common.ConvertToDataTable(data));

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing CourseCategory.aspx in College.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            }
              else
              {
                  rptCourseCategoryData.Visible = false;
                  lblInform.Visible = true;
                  lblInform.Text = _objCommon.GetErrorMessage("noRecords")??"N/A";
              }

        }
        #region method
        private void ValidationErrorMessages()
        {
            _objCommon = new Common();
            rfvCourse.ErrorMessage = _objCommon.GetValidationMessage("rfvCourseCategory") ?? "N/A";
            revExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvUploadExcel") ?? "N/A";
            revExcelUpload.ValidationExpression = ClsSingelton.aRegExpExcelUpload;
            revExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("revUploadExcel") ?? "N/A";
   }
       
        private void BindCourseCategory()
        {
            _objCommon = new Common();
            var data = CourseProvider.Instance.GetAllCourseCategoryList();
            if (data.Count > 0)
            {
                try
                {
                    rptCourseCategoryData.Visible = true;
                    lblInform.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptCourseCategoryData, Common.ConvertToDataTable(data));

                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.ToString();
                    }
                    const string addInfo = "Error while executing CourseCategory.aspx in College.cs  :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptCourseCategoryData.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords") ?? "N/A";
            }

        }
        private void InsertUpdateCategory()
        {
            int result;
            var errMsg = "";
            result = CourseProvider.Instance.InsertCourseCategory(txtCourseCategory.Text.Trim(), out errMsg, LoggedInUserId,
                                                                           chkCategoryStatus.Checked);
            if (result > 0)
            {

                lblSuccess.CssClass = "success show";
                lblSuccess.Text = errMsg;
                lblSuccess.Focus();
                ClearFileds();
                BindCourseCategory();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = errMsg;
                lblError.Focus();
            }
           
            
        }
        private void ClearFileds()
        {
            txtCourseCategory.Text = string.Empty;
            chkCategoryStatus.Checked = false;
        }
        #endregion
        #region btntCourseCategory_Click
        protected void BtntCourseCategoryClick(object sender, EventArgs e)
        {
            InsertUpdateCategory();
        }
        #endregion
        

        protected void BtnUploadClick(object sender, EventArgs e)
        {
            _objCommon = new Common();
            try
            {
                var _ds = new DataSet();
                var _objClsOledbdatalayer = new ClsOleDBDataWrapper();
                var excelSheets = new String[0];
                var path = MapPath(fulUploadExcel.FileName);
                fulUploadExcel.SaveAs(path);
                excelSheets = _objClsOledbdatalayer.CountTotalSheets(path);

                if (excelSheets.Length > 0)
                {
                    foreach (string t in excelSheets)
                    {

                        _ds = _objClsOledbdatalayer.getdata(path, t);
                        if (_ds.Tables[0].Rows.Count > 0)
                        {

                            for (int j = 0; j < _ds.Tables[0].Rows.Count - 1; j++)
                            {
                                System.Threading.Thread.Sleep(500);
                                var errMsg = "";
                                var result = CourseProvider.Instance.InsertCourseCategory(_ds.Tables[0].Rows[j]["CourseCategoryName"].ToString(), out errMsg, LoggedInUserId, _ds.Tables[0].Rows[j]["CourseCategoryStatus"].ToString() == "True" ? true : false);
                                if (result > 0)
                                    exclSuccessCount.SetProgress(j + 1, _ds.Tables[0].Rows.Count - 1);
                            }
                            lblSuccess.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                            lblSuccess.Visible = true;
                            BindCourseCategory();
                        }
                    }
                   
                }
                else
                {
                    lblError.Text = _objCommon.GetErrorMessage("lblErrMsg");
                }
            }
            catch (Exception ex)
            { }
                
        }
        protected void BtnSeeExcelFormatClick(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/CourseCategory.xlsx");
            if (path == null) throw new ArgumentNullException("path");
            var objDocProcess = new System.Diagnostics.Process { EnableRaisingEvents = false, StartInfo = { FileName = @path } };
            objDocProcess.Start();
        }


        [WebMethod]
        public static string UpdateCourseCategoryStatus(int courseCategoryId, string courseCategoryName, bool courseCategoryStatus)
        {
            string errMsg = "";
          var  result = CourseProvider.Instance.UpdateCourseCategory(courseCategoryId,
                                                                         courseCategoryName, out errMsg, new SecurePage().LoggedInUserId,
                                                                         courseCategoryStatus);

            return errMsg;
        }
    }
}