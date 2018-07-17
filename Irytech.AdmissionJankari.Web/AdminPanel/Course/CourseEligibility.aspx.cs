using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Course
{
    public partial class CourseEligibility :SecurePage 
    {
         Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
         {
            
             usCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
             usCustomPaging.PageSize = ClsSingelton.PageSize;
             usCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            if (IsPostBack) return;
             BindCourseEligibility();
             ValidationErrorMessages();
          
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            
            _objCommon=new Common();
            var data = CourseProvider.Instance.GetAllCourseEligibiltyList();
            if (data.Count > 0)
            {
                try
                {
                    rptCourseEligibility.Visible = true;
                    lblInfo.Visible = false;
                    usCustomPaging.BindDataWithPaging(rptCourseEligibility, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in CourseEligibilty.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptCourseEligibility.Visible = false;
                lblInfo.Visible = true;
                lblInfo.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }
        
        private void ValidationErrorMessages()
        {
            _objCommon = new Common();
            rfvCourseEligibilty.ErrorMessage = _objCommon.GetValidationMessage("rfvCourseEligibilty") ?? "N/A";
            rfvUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvUploadExcel") ?? "N/A";
            revExcelUpload.ValidationExpression = ClsSingelton.aRegExpExcelUpload;
            revExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("revUploadExcel") ?? "N/A";
           }
        private void BindCourseEligibility()
        {
            _objCommon = new Common();
            var data = CourseProvider.Instance.GetAllCourseEligibiltyList();
            if (data.Count > 0)
            {
                try
                {
                    rptCourseEligibility.Visible = true;
                    lblInfo.Visible = false;
                    usCustomPaging.BindDataWithPaging(rptCourseEligibility, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in CourseEligibilty.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptCourseEligibility.Visible = false;
                lblInfo.Visible = true;
                lblInfo.Text = _objCommon.GetErrorMessage("noRecords");
            }
        }
        protected void BtnCourseEligibilityClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateCourseEligibilty();
            }
        }
        private void InsertUpdateCourseEligibilty()
        {
            string errMsg = "";
            int result;
            
                result = CourseProvider.Instance.InsertCourseEligibilty(txtCourseEligibilty.Text.Trim(), LoggedInUserId,
                                                                            out errMsg, chkStatus.Checked);
                if (result > 0)
                {
                    lblSuccess.CssClass = "success show";
                    lblSuccess.Text = errMsg; 
                    ClearFields();
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = errMsg;
                }
           
           
            BindCourseEligibility();
        }
        private void ClearFields()
        {
            txtCourseEligibilty.Text = string.Empty;
            chkStatus.Checked = false;
        }
   
        protected void BtnSeeExcelFormatClick(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/CourseEligibilty.xlsx");
            if (path == null) throw new ArgumentNullException("path");
            var objDocProcess = new System.Diagnostics.Process { EnableRaisingEvents = false, StartInfo = { FileName = @path } };
            objDocProcess.Start(); 
        }

        protected void BtnUploadClick(object sender, EventArgs e)
        {
            _objCommon = new Common();
            try
            {
                DataSet _ds = new DataSet();
                var errMsg = "";
                var _objClsOledbdatalayer = new ClsOleDBDataWrapper();
                var excelSheets = new String[0];
                string path = MapPath(fulUploadExcel.FileName);
                fulUploadExcel.SaveAs(path);
                excelSheets = _objClsOledbdatalayer.CountTotalSheets(path);
                if (excelSheets.Length > 0)
                {
                    foreach (string t in excelSheets)
                    {
                        _ds = _objClsOledbdatalayer.getdata(path, t);
                        if (_ds.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j <= _ds.Tables[0].Rows.Count - 1; j++)
                            {
                                System.Threading.Thread.Sleep(2000);
                                var result = CourseProvider.Instance.InsertCourseEligibilty(_ds.Tables[0].Rows[j]["CourseEligibiltyName"].ToString(), LoggedInUserId, out errMsg, _ds.Tables[0].Rows[j]["CourseEligibiltyStatus"].ToString() == "True" ? true : false);
                               
                            }
                            lblSuccess.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                            lblSuccess.Visible = true;
                            BindCourseEligibility();
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
        
        // Method to Update Course Eligibilty

        [System.Web.Services.WebMethod]
        public static string UpdateCourseEligibilty(int courseEligibiltyId,string courseEligibiltyName,bool courseEligibiltyStatus)
        {
            var errMsg = "";
          var   result = CourseProvider.Instance.UpdateCourseEligibilty(courseEligibiltyId,
                                                                        courseEligibiltyName, new SecurePage().LoggedInUserId, out errMsg,
                                                                     courseEligibiltyStatus);

          return errMsg;
           
        }

    }
}