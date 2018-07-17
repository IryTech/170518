using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class HostelMaster : SecurePage 
    {
        Common _objCommon;
      HostelCategoryProperty _objHostelCategoryProperty;
        protected void Page_Load(object sender, EventArgs e)
      {
          ucCustomPaging.PageSize = ClsSingelton.PageSize;
         ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
          ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
           if (IsPostBack) return;
            BindHostelTypeDetails();
            ValidationErrorMessages();

        }
        private void ValidationErrorMessages()
        {
            _objCommon = new Common();
            rfvHostel.ErrorMessage = _objCommon.GetValidationMessage("rfvHostelType");
            rfvExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvExcelUpload");
        }
        private void BindHostelTypeDetails()
        {
            try
            {
                
                _objCommon = new Common();
                var data = CollegeProvider.Instance.GetAllHostelCategory();
                if (data.Count > 0)
                {
                    lblEditStatus.Visible = true;
                    rptHostel.Visible = true;
                    lblEditStatus.Text = "Details of Hostel ";

                    ucCustomPaging.BindDataWithPaging(rptHostel, Common.ConvertToDataTable(data));
                }
                else
                {
                    rptHostel.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindHostelTypeDetails in HostelMaster.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void BtnSaveClick(object sender, EventArgs e)
        {
            _objHostelCategoryProperty = new HostelCategoryProperty
                                             {
                                                 HostelCategoryType = txtHostelType.Text.Trim(),
                                                 HostelCategoryStatus = chkStatus.Checked
                                             };
            InsertInstituteType();
             BindHostelTypeDetails();
        }
        private void InsertInstituteType()
        {
            try
            {
                var errMsg = "";
                var result = CollegeProvider.Instance.InsertHostelCategory(_objHostelCategoryProperty, LoggedInUserId,
                                                                           out errMsg);
                if (result > 0)
                {

                    lblSuccess.Visible = true;
                    lblSuccess.Text = errMsg;
                    ClearFields();
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
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertInstituteType in HostelMaster.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
         [System.Web.Services.WebMethod]
        public static string  UpdateInstituteType(int hostelTypeId,string hostelType,bool hostelTypeStatus)
        {
            string errMsg="";
            HostelCategoryProperty _objHostelCategoryProperty= new HostelCategoryProperty();
            _objHostelCategoryProperty.HostelCategoryId = hostelTypeId;
            _objHostelCategoryProperty.HostelCategoryType = hostelType;
            _objHostelCategoryProperty.HostelCategoryStatus = hostelTypeStatus;
            try
            {

                var result = CollegeProvider.Instance.UpdateHostelCategory(_objHostelCategoryProperty, new SecurePage().LoggedInUserId,
                                                                           out errMsg);
                
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateInstituteType in HostelMaster.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;
        }

        private void ClearFields()
        {
            txtHostelType.Text = string.Empty;
            chkStatus.Checked = false;

        }

   
        protected void BtnUploadClick(object sender, EventArgs e)
        {
            lblSuccess.Visible = false;
            var path = MapPath("~/AdminPanel/ExcelPreview/HostelMaster.xlsx");
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
                    foreach (string t in excelSheets)
                    {
                        DataSet ds = objClsOledbdatalayer.getdata(path, t);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                            {

                                var errMsg = "";
                                _objHostelCategoryProperty = new HostelCategoryProperty
                                {
                                    HostelCategoryType = Convert.ToString(ds.Tables[0].Rows[j]["HostelCategoryType"]),
                                    HostelCategoryStatus = ds.Tables[0].Rows[j]["HostelCategoryStatus"].ToString() == "True" ? true : false
                                };

                            }
                            lblSuccess.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                            lblSuccess.Visible = true;
                            BindHostelTypeDetails();
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


        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();

            
            var data = CollegeProvider.Instance.GetAllHostelCategory();
            //var data = _objCommon.GetApplicationErrorList();
            if (data.Count > 0)
            {
                try
                {
                    rptHostel.Visible = true;
                    lblError.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptHostel, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in HostelMaster.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptHostel.Visible = false;
                lblError.Visible = true;
                lblError.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }


        
    }
}