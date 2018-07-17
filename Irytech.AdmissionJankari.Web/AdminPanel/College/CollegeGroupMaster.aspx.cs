using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using System.Data;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class CollegeGroupMaster : SecurePage
    {
        Common _objCommon;
        CollegeGroupProperty _objCollegeGroupProperty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
            ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (!IsPostBack)
            {
                ValidateFields();
                BindCollegeGroupMastrerDetails();
                lblHeader.Text = "Add college group";
            }
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            var data = CollegeProvider.Instance.GetAllCollegeGroupList();
            if (data.Count > 0)
            {
                try
                {
                    ucCustomPaging.BindDataWithPaging(rptCollegeGroupMaster, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in CollegeGroupMaster.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptCollegeGroupMaster.Visible = false;

            }

        }

        protected void BindCollegeGroupMastrerDetails()
        {
            var data = CollegeProvider.Instance.GetAllCollegeGroupList();
            if (data.Count > 0)
            {
                try
                {
                    ucCustomPaging.BindDataWithPaging(rptCollegeGroupMaster, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in CollegeGroupMaster.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptCollegeGroupMaster.Visible = false;

            }


        }


        protected void BindGetCollegeGroupListByGroupName()
        {

            _objCommon = new Common();
            var data = CollegeProvider.Instance.GetCollegeGroupListByGroupName(txtCollegeGroupNameSearch.Text.Trim());
            if (data.Count > 0)
            {
                rptCollegeGroupMaster.DataSource = data;
                rptCollegeGroupMaster.DataBind();
                lblInform.Visible = false;
            }
            else
            {
                rptCollegeGroupMaster.Visible = true;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCollegeGroupNameSearch.Text))
            {
                BindGetCollegeGroupListByGroupName();
            }
            else
            {
                BindCollegeGroupMastrerDetails();
            }
        }

        protected void btnSeeExcelFormat_Click(object sender, EventArgs e)
        {
            lblSuccess.Visible = false;
            var path = MapPath("~/AdminPanel/ExcelPreview/ExcelCollegeGroupMaster.xlsx");
            var objDocProcess = new System.Diagnostics.Process
            {
                EnableRaisingEvents = false,
                StartInfo = { FileName = @path }
            };
            objDocProcess.Start();
        }
        //code for Excel upload
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            lblSuccess.Visible = false;
            var errMsg = "";
            try
            {
                _objCollegeGroupProperty = new CollegeGroupProperty();
                _objCommon = new Common();
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

                                _objCollegeGroupProperty = new CollegeGroupProperty
                                {
                                    CollegeGroupName = _ds.Tables[0].Rows[j]["CollegeGroupName"].ToString(),
                                    CollegeGropuStatus = Convert.ToBoolean(_ds.Tables[0].Rows[j]["CollegeGropuStatus"].ToString())

                                };
                                int result = CollegeProvider.Instance.InsertCollegeGroupDetails(_objCollegeGroupProperty, LoggedInUserId, out errMsg);

                                if (result > 0)
                                {
                                    lblRecordsInserted.Text = "";
                                    lblRecordsInserted.Text = j + " row inserted out of " + _ds.Tables[0].Rows.Count;
                                }
                            }
                            lblSuccess.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                            Response.Redirect("CollegeGroupMaster.aspx");
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

        private void ValidateFields()
        {
            _objCommon = new Common();
            rfvCollegeGroupName.ErrorMessage = _objCommon.GetValidationMessage("rfvCollegeGroupName");
            rfvImageUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvImageUpload");
            rfvExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvExcelUpload");

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblSuccess.Visible = false;
            switch (btnSave.Text)
            {
                case "Save":
                    InsertCollegeGroupDetails();
                    break;
                case "Update":

                    UpdateCollegeGroupDetails();
                    break;
            }
            BindCollegeGroupMastrerDetails();
            ClearControl();
        }
        protected void InsertCollegeGroupDetails()
        {
            try
            {
                if (flpImgUpload.HasFile)
                {
                    hdnFileName.Value = flpImgUpload.FileName;
                    flpImgUpload.SaveAs(Server.MapPath(new Common().GetFilepath("UniversityImage") + hdnFileName.Value));
                }
                string errMsg;
                _objCollegeGroupProperty = new CollegeGroupProperty
                {
                    CollegeGroupName = txtCollegeGroupName.Text,
                    CollegeGropuStatus = chkStatus.Checked,
                    CollegeGroupLogo = hdnFileName.Value
                };
                var result = CollegeProvider.Instance.InsertCollegeGroupDetails(_objCollegeGroupProperty, LoggedInUserId,
                                                                                out errMsg);
                 lblSuccess.Visible = true;
                 lblSuccess.Text = errMsg;
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing InsertCollegeGroupDetails() in AddCollegeGroupMaster.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void UpdateCollegeGroupDetails()
        {
            try
            {
                if (flpImgUpload.HasFile)
                {
                    hdnFileName.Value = flpImgUpload.FileName;
                    flpImgUpload.SaveAs(Server.MapPath(new Common().GetFilepath("UniversityImage") + hdnFileName.Value));
                }

                string errMsg;
               _objCollegeGroupProperty = new CollegeGroupProperty
                {
                    CollegeGroupId =
                        Convert.ToInt32(ViewState["CollegeGropuId"]),
                    CollegeGroupName = txtCollegeGroupName.Text,
                    CollegeGropuStatus = chkStatus.Checked,
                    CollegeGroupLogo = hdnFileName.Value

                };

                var result = CollegeProvider.Instance.UpdateCollegeGroupDetails(_objCollegeGroupProperty, LoggedInUserId,
                                                                                out errMsg);
                if (result > 0)
                    lblSuccess.CssClass = "success";
                else
                {
                    lblSuccess.CssClass = "info";
                    
                }
                lblSuccess.Visible = true;
                lblSuccess.Text = errMsg;
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateCollegeGroupDetails() in AddCollegeGroupMaster.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void rptCollegeGroupMaster_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                ViewState["CollegeGropuId"] = Convert.ToInt32(e.CommandArgument);
                BindCollegeGroupById(Convert.ToInt32( ViewState["CollegeGropuId"]));
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweqeewqe", "OpenPoup('divRankSourceInsert','650px','sndAddCollegeGroup');", true);
            }
        }

        protected void BindCollegeGroupById(int collegeGroupId)
        {
          
            var data = CollegeProvider.Instance.GetCollegeGroupListById(collegeGroupId);
            //query for Retrive Stream Id 
            var query = data.Select(result => new
            {
                result.CollegeGroupName,
                result.CollegeGropuStatus,
                result.CollegeGroupLogo
            });
            var records = query.First();
            txtCollegeGroupName.Text = !string.IsNullOrEmpty(Convert.ToString(records.CollegeGroupName)) ? Convert.ToString(records.CollegeGroupName) : "N/A";

            lblHeader.Text = "Edit the details of " + txtCollegeGroupName.Text;

            chkStatus.Checked = records.CollegeGropuStatus;

            iumgUniversity.Visible = true;
           

            iumgUniversity.ImageUrl = String.Format("{0}{1}", "/image.axd?CollegeGroup=", string.IsNullOrEmpty(records.CollegeGroupLogo) ? "NoImage.jpg" : records.CollegeGroupLogo);
            btnSave.Text = "Update";
        }

        private void ClearControl()
        {
            txtCollegeGroupName.Text = "";
            chkStatus.Checked = false;
            hdnFileName.Value = "";
            iumgUniversity.Visible = false;
            btnSave.Text = "Save";
            ViewState["CollegeGropuId"] = "";
            lblHeader.Text = "Add college group";

        }
    }
}
