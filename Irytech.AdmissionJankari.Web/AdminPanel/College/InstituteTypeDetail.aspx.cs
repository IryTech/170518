using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class InstituteTypeDetail : SecurePage 
    {
        private Common _objCommon;
        private InstituteTypeProperty _objInstituteTypeProperty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            lblHeader.Text = "Add Institute Type Detail";
            BindInstituteTypeDetails();
            ValidationErrorMessages();
        }
        private void ValidationErrorMessages()
        {
            _objCommon = new Common();
            rfvInstituteType.ErrorMessage = _objCommon.GetValidationMessage("rfvInstituteType");
            rfvExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvExcelUpload");
        }
        private void BindInstituteTypeDetails()
        {
            lblInfo.Visible = false;
            _objCommon = new Common();
            var data = CollegeProvider.Instance.GetAllInstituteTypeList();
            if (data.Count > 0)
            {

                 rptInstituteType.Visible = true;
                 rptInstituteType.DataSource = data;
                rptInstituteType.DataBind();
            }
            else
            {
                rptInstituteType.Visible = false;
                lblInfo.Visible = true;
                lblInfo.Text = _objCommon.GetErrorMessage("noRecords");
            }
        }

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            _objInstituteTypeProperty = new InstituteTypeProperty {InstituteType = txtInstituteType.Text.Trim()};
            InsertInstituteType();
            BindInstituteTypeDetails();
        }
        private void InsertInstituteType()
        {
            var errMsg = "";
            var result = CollegeProvider.Instance.InsertInstituteTypeDetails(_objInstituteTypeProperty, LoggedInUserId, out errMsg);
            if (result > 0)
            {

                lblSuccess.Visible = true;
                lblSuccess.CssClass = "success show";
                lblSuccess.Text = errMsg;
                ClearFields();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = errMsg;
            }

        }
        [System.Web.Services.WebMethod]
        public static string  UpdateInstituteType(int instituteTypeId,string instituteType)
        {
            InstituteTypeProperty _objInstituteTypeProperty = new InstituteTypeProperty();
            _objInstituteTypeProperty.InstituteTypeId = instituteTypeId;
            _objInstituteTypeProperty.InstituteType = instituteType;
            var errMsg = "";
            var result = CollegeProvider.Instance.UpdateInstituteTypeDetails(_objInstituteTypeProperty, new SecurePage().LoggedInUserId, out errMsg);

            return errMsg;
        }
        private void ClearFields()
        {
            txtInstituteType.Text = string.Empty;

        }

     
        protected void BtnUploadClick(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/InstituteType.xlsx");
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
                if (excelSheets.Length > 0)
                {
                    foreach (var t in excelSheets)
                    {
                        DataSet ds = objClsOledbdatalayer.getdata(path, t);
                        if (ds.Tables[0].Rows.Count <= 0) continue;
                        for (var j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                        {
                            var errMsg = "";
                            _objInstituteTypeProperty = new InstituteTypeProperty { InstituteType = Convert.ToString(ds.Tables[0].Rows[j]["InstituteType"]) };
                        }
                    }
                    lblSuccess.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                    lblSuccess.Visible = true;
                    BindInstituteTypeDetails();
                }
                else
                {
                    lblError.Text = _objCommon.GetErrorMessage("lblErrMsg");
                }
            }
            catch (Exception ex)
            { }
        }
    }
}
