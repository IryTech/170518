using System;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.NewsArticle
{
    public partial class AddFAQDetails : SecurePage 
    {
        Common _objCommon;
        FAQDetailsProperty _objFaqDetailsProperty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateFields();
            if (!IsPostBack)
            {
                lblHeader.Text = "Add FAQ";
                BindDropdownGetAllFaqCategoryList();
                if (Request.QueryString["FAQDetailsId"] != null)
                {
                    BindFaqDetails(Convert.ToInt16(Request.QueryString["FAQDetailsId"]));
                   
                }
            }
        }
        private void BindFaqDetails(int faqDetailsId)
        {
            try
            {
                var data = FAQProvider.Instance.GetFAQDetailsById(faqDetailsId);

                if (data.Count <= 0) return;
                var query = data.Select(result => new
                                                      {
                                                          result.FAQCategoryId,
                                                          result.FAQDetailsQuestion,
                                                          result.FAQDetailsAnswer,
                                                          result.FAQDetailsStatus

                                                      }).First();

                ddlFAQCategoryId.SelectedValue = !string.IsNullOrEmpty(Convert.ToString(query.FAQCategoryId))
                                                     ? Convert.ToString(query.FAQCategoryId.ToString())
                                                     : "0";
                txtFAQQuestion.Text = !string.IsNullOrEmpty(Convert.ToString(query.FAQDetailsQuestion))
                                          ? Convert.ToString(query.FAQDetailsQuestion)
                                          : "N/A";
                txtFAQDetailsAnswer.Text = !string.IsNullOrEmpty(Convert.ToString(query.FAQDetailsAnswer))
                                               ? Convert.ToString(query.FAQDetailsAnswer)
                                               : "N/A";
                chkFAQDetailsStatus.Checked = query.FAQDetailsStatus;
                lblInsertUpdate.Text = "Record Update Of" + query.FAQDetailsQuestion;
                BtnSubmit.Text = "Update";
                lblHeader.Text = "Edit FAQ Details";
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BindFaqDetails in AddFAQDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindDropdownGetAllFaqCategoryList()
        {
            var data = FAQProvider.Instance.GetAllFAQCategoryList();
            if (data.Count > 0)
            {
                ddlFAQCategoryId.DataSource = data;
                ddlFAQCategoryId.DataTextField = "FAQCategoryName";
                ddlFAQCategoryId.DataValueField = "FAQCategoryId";
                ddlFAQCategoryId.DataBind();
                ddlFAQCategoryId.Items.Insert(0, new ListItem("--Select--"));
            }
            else
            {
                ddlFAQCategoryId.Items.Insert(0, new ListItem("--Select--"));
            }

        }
        private void ValidateFields()
        {
            _objCommon = new Common();
            rfvFAQCategoryId.ErrorMessage = _objCommon.GetValidationMessage("rfvFAQCategoryId");
            rfvFAQDetailsAnswer.ErrorMessage = _objCommon.GetValidationMessage("rfvFAQDetailsAnswer");
            rfvfileUploadExcel.ErrorMessage = _objCommon.GetValidationMessage("rfvfileUploadExcel");
            rfvFAQName.ErrorMessage = _objCommon.GetValidationMessage("rfvFAQName");

        }
        private void InsertFaqDetails()
        {
            try
            {
                string errMsg;
                _objFaqDetailsProperty = new FAQDetailsProperty
                                             {

                                                 FAQDetailsAnswer = txtFAQDetailsAnswer.Text.Trim(),
                                                 FAQDetailsQuestion = txtFAQQuestion.Text.Trim(),
                                                 FAQCategoryId = ddlFAQCategoryId.SelectedIndex,
                                                 FAQDetailsStatus = chkFAQDetailsStatus.Checked
                                             };
                var result = FAQProvider.Instance.InsertFAQDetails(_objFaqDetailsProperty, LoggedInUserId, out errMsg);
                if (result > 0)
                {
                    Response.Redirect("FAQDetails.aspx");
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing InsertFaqDetails in AddFAQDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        private void UpdateFaqDetails()
        {
            try
            {
                string errMsg;
                _objFaqDetailsProperty = new FAQDetailsProperty
                                             {
                                                 FAQDetailsId = Convert.ToInt16(Request.QueryString["FAQDetailsId"]),
                                                 FAQDetailsAnswer = txtFAQDetailsAnswer.Text.Trim(),
                                                 FAQDetailsQuestion = txtFAQQuestion.Text.Trim(),
                                                 FAQCategoryId = ddlFAQCategoryId.SelectedIndex,
                                                 FAQDetailsStatus = chkFAQDetailsStatus.Checked
                                             };

                var result = FAQProvider.Instance.UpdateFAQDetails(_objFaqDetailsProperty, LoggedInUserId, out errMsg);
                if (result > 0)
                {
                    Response.Redirect("FAQDetails.aspx");

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing UpdateFaqDetails in AddFAQDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void BtnSubmitClick(object sender, EventArgs e)
        {
            switch (BtnSubmit.Text)
            {
                case "Save":
                    InsertFaqDetails();
                    break;
                case "Update":
                    UpdateFaqDetails();
                    break;
            }
        }

        protected void BtnSeeExcelFormatClick(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/FAQDetailsSheet.xlsx");
            var objDocProcess = new System.Diagnostics.Process
                                    {
                                        EnableRaisingEvents = false,
                                        StartInfo = {FileName = @path}
                                    };
            objDocProcess.Start();
        }

        protected void BtnUploadClick(object sender, EventArgs e)
        {
            _objCommon = new Common();
            try
            {
                _objFaqDetailsProperty = new FAQDetailsProperty();
                var objClsOledbdatalayer = new ClsOleDBDataWrapper();
                var path = MapPath(fileUploadExcel.FileName);
                fileUploadExcel.SaveAs(path);
                var excelSheets = objClsOledbdatalayer.CountTotalSheets(path);
                if (excelSheets.Length > 0)
                {
                    foreach (var t in excelSheets)
                    {
                        var ds = objClsOledbdatalayer.getdata(path, t);
                        if (ds.Tables[0].Rows.Count <= 0) continue;
                        for (var j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                        {
                            _objFaqDetailsProperty = new FAQDetailsProperty
                                                         {
                                                             FAQCategoryId = Convert.ToInt16(ds.Tables[0].Rows[j]["FAQCategoryId"]),
                                                             FAQDetailsQuestion = ds.Tables[0].Rows[j]["FAQDetailsQuestion"].ToString(),
                                                             FAQDetailsAnswer = ds.Tables[0].Rows[j]["FAQDetailsAnswer"].ToString(),
                                                             FAQDetailsStatus = Convert.ToBoolean(ds.Tables[0].Rows[j]["FAQDetailsStatus"].ToString())
                                                         };
                            string errMsg;
                            var result = FAQProvider.Instance.InsertFAQDetails(_objFaqDetailsProperty, LoggedInUserId, out errMsg);
                            if (result <= 0) continue;
                            lblRecordsInserted.Text = "";
                            lblRecordsInserted.Text = j + " row inserted out of " + ds.Tables[0].Rows.Count;
                        }
                        lblSuccess.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                        lblSuccess.Visible = true;
                        Response.Redirect("FAQDetails.aspx");
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
                const string addInfo = "Error while executing BtnUploadClick in AddFAQDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        
        }

    }

