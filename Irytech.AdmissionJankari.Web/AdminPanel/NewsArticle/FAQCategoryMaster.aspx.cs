using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.Components.Web.Controls;
using System.Web.UI;
using System.Data;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.NewsArticle
{
    public partial class FAQCategoryMaster : SecurePage 
    {
        Common _objCommon;
        FAQCategoryProperty _objFaqCategoryProperty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;

            if (IsPostBack) return;
                ValidateFields();
                GetAllFaqCategoryList();
                lblHeader.Text = "Add FAQ Category Master";
            

        }

        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            var data = FAQProvider.Instance.GetAllFAQCategoryList();
            if (data.Count > 0)
            {
                try
                {
                    rptCategoryMaster.Visible = true;
                    lblError.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptCategoryMaster, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in FAQCategoryMaster.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptCategoryMaster.Visible = false;
                lblError.Visible = true;
                lblError.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }

        private void ValidateFields()
        {
            _objCommon = new Common();
            rfvFAQCategoryName.ErrorMessage = _objCommon.GetValidationMessage("rfvFAQCategoryName");
            rfvExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvExcelUpload");
        }
        protected void btnSeeExcelFormat_Click(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/FAQCategoryMasterSheet.xlsx");
            System.Diagnostics.Process objDocProcess = new System.Diagnostics.Process();
            objDocProcess.EnableRaisingEvents = false;
            objDocProcess.StartInfo.FileName = @path; objDocProcess.Start();
        }
        private void GetAllFaqCategoryList()
        {
            _objCommon = new Common();
            var data = FAQProvider.Instance.GetAllFAQCategoryList();
            if (data.Count > 0)
            {
                ucCustomPaging.BindDataWithPaging(rptCategoryMaster, Common.ConvertToDataTable(data));
                rptCategoryMaster.Visible = true;
            }
            else
            {
                rptCategoryMaster.Visible = false;
                rptCategoryMaster.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }
        protected void InsertFaqCategory()
        {
            string errMsg;
            _objFaqCategoryProperty = new FAQCategoryProperty
            {
                FAQCategoryName = txtFAQCategoryName.Text,
                FAQCategoryStatus = chkFAQDetailsStatus.Checked,

            };
            var result = FAQProvider.Instance.InsertFAQCategory(_objFaqCategoryProperty, LoggedInUserId, out errMsg);
            if (result > 0)
            {
                btnSave.Text = "Save";
                lblSuccess.Visible = true;
                lblSuccess.Text = errMsg;
                lblError.Text = "";
                lblInform.Text = "";
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = errMsg;
            }
        }
        protected void UpdtaeFaqCategory()
        {
            string errMsg;
            _objFaqCategoryProperty = new FAQCategoryProperty
            {
                FAQCategoryId = Convert.ToInt16(hdnFAQCategoryMaster.Value),
                FAQCategoryName = txtFAQCategoryName.Text,
                FAQCategoryStatus = chkFAQDetailsStatus.Checked,
            };
            var result = FAQProvider.Instance.UpdtaeFAQCategory(_objFaqCategoryProperty, LoggedInUserId, out errMsg);
            if (result > 0)
            {
                btnSave.Text = "Update";
                lblSuccess.Visible = true;
                lblSuccess.Text = errMsg;
                lblError.Text = "";
                lblInform.Text = string.Empty;
                lblInsert.Text = "Insert";
                lblError.Text = "";
                lblSuccess.Focus();
                lblHeader.Text = "Add FAQ Category Master";
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = errMsg;
                lblSuccess.Text = "";
            }
            lblInsert.Text = "Insert";
            btnSave.Text = "Save";
        }

        protected void rptCategoryMaster_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblError.Text = "";
            lblInform.Text = "";
            lblSuccess.Visible = false;
            switch (e.CommandName)
            {
                case "Edit":
                    hdnFAQCategoryMaster.Value = e.CommandArgument.ToString();
                    var data = FAQProvider.Instance.GetAllFAQCategoryById(Convert.ToInt16(hdnFAQCategoryMaster.Value));
                    var query = from result in data
                                select new
                                {
                                    result.FAQCategoryName,
                                    result.FAQCategoryStatus
                                };
                    var records = query.First();
                    txtFAQCategoryName.Text = records.FAQCategoryName != "" ? records.FAQCategoryName : "N/A";
                    btnSave.Text = "Update";
                    lblInsert.Text = "Record Update Of " + records.FAQCategoryName;
                    lblSuccess.Visible = false;
                    lblHeader.Text = "Edit FAQ Category Master";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>focusOnField();</script>", false);
                    break;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            switch (btnSave.Text)
            {
                case "Save":
                    InsertFaqCategory();
                    txtFAQCategoryName.Text = "";
                    chkFAQDetailsStatus.Checked = false;
                    break;
                case "Update":
                    UpdtaeFaqCategory();
                    txtFAQCategoryName.Text = "";
                    chkFAQDetailsStatus.Checked = false;
                    break;
            }
            GetAllFaqCategoryList();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            _objCommon = new Common();
            try
            {
                _objFaqCategoryProperty = new FAQCategoryProperty();
                var objClsOledbdatalayer = new ClsOleDBDataWrapper();
                string[] excelSheets;
                var path = MapPath(fileUploadExcel.FileName);
                fileUploadExcel.SaveAs(path);
                excelSheets = objClsOledbdatalayer.CountTotalSheets(path);
                if (excelSheets.Length > 0)
                {
                    foreach (var t in excelSheets)
                    {
                        var _ds = objClsOledbdatalayer.getdata(path, t);
                        if (_ds.Tables[0].Rows.Count <= 0) continue;
                        for (int j = 0; j <= _ds.Tables[0].Rows.Count - 1; j++)
                        {
                            _objFaqCategoryProperty = new FAQCategoryProperty
                                                          {
                                                              FAQCategoryName = _ds.Tables[0].Rows[j]["FAQCategoryName"].ToString(),
                                                              FAQCategoryStatus = Convert.ToBoolean(_ds.Tables[0].Rows[j]["FAQCategoryStatus"].ToString())
                                                          };
                            var errMsg = "";
                            int result = FAQProvider.Instance.InsertFAQCategory(_objFaqCategoryProperty, LoggedInUserId, out errMsg);
                            if (result > 0)
                            {
                                lblRecordsInserted.Text = "";
                                lblRecordsInserted.Text = j + " row inserted out of " + _ds.Tables[0].Rows.Count;
                            }
                        }
                        lblSuccess.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                        lblSuccess.Visible = true;
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
                const string addInfo = "Error while executing btnUpload_Click in FAQCategoryMaster.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        
    }
}