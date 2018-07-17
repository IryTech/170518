using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using System.Data;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel
{
    public partial class Country : SecurePage 
    {
        Common _objCommon;
        CountryProperty _objCountryProperty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
                ValidateFields();
                BindCountry();
            
        }
        #region Method
        private void ValidateFields()
        {
            _objCommon=new Common();
            rfvCountryName.ErrorMessage = _objCommon.GetValidationMessage("rfvCountryName");
            rfvCountryCode.ErrorMessage = _objCommon.GetValidationMessage("rfvCountryCode");
            rfvExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvExcelUpload");
        }
   
        protected void BindCountry()
        {
            _objCommon = new Common();
            var data = CountryProvider.Instance.GetAllCountry();
            if (data.Count > 0)
            {
               

                rptCountry.Visible = true;
                lblError.Visible = false;
                ucCustomPaging.BindDataWithPaging(rptCountry, Common.ConvertToDataTable(data));
                               
            }
            else
            {
                rptCountry.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }
        }
        protected void InsertCountryMaster()
        {
            try
            {
                string ErrMsg = "";
                int result = CountryProvider.Instance.InsertCountry(txtCountryName.Text.Trim(), LoggedInUserId,
                                                                    out ErrMsg, txtCountryCode.Text.Trim());
                if (result > 0)
                {
                    btnCountryMasterSubmit.Text = "Save";
                    lblSuccess.CssClass = "success show";
                    lblSuccess.Text = ErrMsg;
                    CleatlErrMsglabels();
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = ErrMsg;
                }
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertCountryMaster in Country.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        [System.Web.Services.WebMethod]
        public static string UpdateCountryMaster(int countryId,string countryName,string countyCode)
        {
            string ErrMsg = "";
            try
            {
                
                int result = CountryProvider.Instance.UpdateCountry(countryId,
                                                                    countryName,
                                                                  new SecurePage().LoggedInUserId, out ErrMsg,
                                                                   countyCode);
               
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateCountryMaster in Country.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return ErrMsg;
        }
        #endregion
        #region Excel Preview & Upload
        //Excel sheet preview
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/CountrySheet.xlsx");
            System.Diagnostics.Process objDocProcess = new System.Diagnostics.Process();
            objDocProcess.EnableRaisingEvents = false;
            objDocProcess.StartInfo.FileName = @path; objDocProcess.Start();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            var errMsg = "";
         
            _objCommon = new Common();
            try
            {
                _objCountryProperty = new CountryProperty();
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

                                int result = CountryProvider.Instance.InsertCountry(_ds.Tables[0].Rows[j]["CountryName"].ToString(),
                                                                                    LoggedInUserId, out errMsg, _ds.Tables[0].Rows[j]["CountryCode"].ToString());
                                if (result > 0)
                                {
                                    lblRecordsInserted.Text = "";
                                   lblRecordsInserted.Text = j + " row inserted out of " + _ds.Tables[0].Rows.Count;
                                }
                            }
                            lblSuccess.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                            lblSuccess.Visible = true;
                            //CountryMasterBind();
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
        #endregion
       
        protected void btnCountryMasterSubmit_Click(object sender, EventArgs e)
        {
            InsertCountryMaster();
            ClearText();
            BindCountry();
        }
        protected void ClearText()
        {
            txtCountryName.Text = "";
            txtCountryCode.Text = "";
        }
        //Methord for clear errorLabels
        protected void CleatlErrMsglabels()
        {
            lblError.Text = "";
            lblInform.Text = "";
        }

        #region "Event"

        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            var data = CountryProvider.Instance.GetAllCountry();
            if (data.Count > 0)
            {
                try
                {
                    rptCountry.Visible = true;
                    lblError.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptCountry, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in Country.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptCountry.Visible = false;
                lblError.Visible = true;
                lblError.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }

        #endregion


    }
}