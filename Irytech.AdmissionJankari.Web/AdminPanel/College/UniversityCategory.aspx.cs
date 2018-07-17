using System;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using System.Data;
using IryTech.AdmissionJankari.Components;
using System.Web.Services;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{

    public partial class UniversityCategory : SecurePage
    {
        private Common _objCommon;
        private UniversityCategoryProperty _objUniversityCategoryProperty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
            ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            ValidateFields();
            BindGetAllUniversityCategoryList();

        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            var data = UniversityProvider.Instance.GetAllUniversityCategoryList();
            if (data.Count > 0)
            {
                try
                {
                    rptUniversityCategory.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptUniversityCategory, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in UniversityCategory.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptUniversityCategory.Visible = false;

            }
        }
        private void ValidateFields()
        {
            _objCommon = new Common();
            rfvUniversityCategoryName.ErrorMessage = _objCommon.GetValidationMessage("rfvUniversityCategoryName");
            rfvExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvExcelUpload");
        }
        protected void BindGetAllUniversityCategoryList()
        {
            _objCommon = new Common();
            var data = UniversityProvider.Instance.GetAllUniversityCategoryList();
            if (data.Count > 0)
            {
                try
                {
                    rptUniversityCategory.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptUniversityCategory, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in UniversityCategory.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptUniversityCategory.Visible = false;

            }
        }
        protected void InsertUniversityCategoryDetails()
        {
            try
            {
                var errMsg = "";
                _objUniversityCategoryProperty = new UniversityCategoryProperty
                                                     {
                                                         UniversityCategoryName =
                                                             txtUniversityCategoryName.Text
                                                     };
                var result = UniversityProvider.Instance.InsertUniversityCategoryDetails(
                    _objUniversityCategoryProperty, LoggedInUserId, out errMsg);
                if (result > 0)
                {
                    btnUniversityCategoryName.Text = "Save";
                    lblSuccess.CssClass = "success show";
                    lblSuccess.Text = errMsg;
                    txtUniversityCategoryName.Text = "";
                    lblError.Text = "";
                    lblInform.Text = "";
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
                    err = err + " :: Inner Exception :- " + ex;
                }
                const string addInfo = "Error while executing InsertUniversityCategoryDetails in UniversityCategory.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        [WebMethod]
        public static string UpdateUniversityCategoryDetails(int universityCategoryId, string universityCategoryType)
        {
            var errMsg = "";
            try
            {

                UniversityCategoryProperty _objUniversityCategoryProperty = new UniversityCategoryProperty();
                var categoryId = Convert.ToInt16(0);
                _objUniversityCategoryProperty.UniversityCategoryId = categoryId;
                _objUniversityCategoryProperty.UniversityCategoryName = universityCategoryType;
                var result = UniversityProvider.Instance.UpdateUnivesityCategoryDetails(_objUniversityCategoryProperty,
                                                                                       new SecurePage().LoggedInUserId, out errMsg);

            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex;
                }
                const string addInfo = "Error while executing UpdateUniversityCategoryDetails in UniversityCategory.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;
        }
        protected void BtntCourseCategoryClick(object sender, EventArgs e)
        {
            lblError.Text = " ";
            lblInform.Text = " ";
            lblSuccess.Text = " ";
            InsertUniversityCategoryDetails();
            txtUniversityCategoryName.Text = " ";

            BindGetAllUniversityCategoryList();
        }



        protected void BtnSeeExcelFormatClick(object sender, EventArgs e)
        {

            var path = MapPath("~/AdminPanel/ExcelPreview/UniversityCategory.xlsx");
            var objDocProcess = new System.Diagnostics.Process
                                    {
                                        EnableRaisingEvents = false,
                                        StartInfo = { FileName = @path }
                                    };
            objDocProcess.Start();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            var errMsg = "";

            _objCommon = new Common();
            try
            {
                var _objUniversityCategoryProperty = new UniversityCategoryProperty();
                DataSet _ds = new DataSet();
                var _objClsOledbdatalayer = new ClsOleDBDataWrapper();
                var excelSheets = new String[0];
                string path = MapPath(fileUploadExcel.FileName);
                fileUploadExcel.SaveAs(path);
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
                                _objUniversityCategoryProperty = new UniversityCategoryProperty
                              {
                                  UniversityCategoryName = _ds.Tables[0].Rows[j]["UniversityCategoryName"].ToString(),


                              };

                                int result = UniversityProvider.Instance.InsertUniversityCategoryDetails(_objUniversityCategoryProperty, LoggedInUserId, out errMsg);
                                if (result > 0)
                                {
                                    lblRecordsInserted.Text = "";
                                    lblRecordsInserted.Text = j + " row inserted out of " + _ds.Tables[0].Rows.Count;

                                }
                            }
                            lblSuccess.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                            lblSuccess.Visible = true;
                            BindGetAllUniversityCategoryList();
                        }
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
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnUpload_Click in Exam.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


    }
}
