using System;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using System.Data;
using System.Web.UI;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class CollegeAssociationCategoryMaster : SecurePage 
    {
        Common _objCommon;
        CollegeAssociationCategoryProperty _objCollegeAssociationCategoryProperty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindAllCollegeAssociate();
            ValidationErrorMessages();
            BindDisplayAds();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            var path = MapPath("~/AdminPanel/ExcelPreview/StateSheet.xlsx");
            if (path == null) throw new ArgumentNullException("File bit Found");
            var objDocProcess = new System.Diagnostics.Process
                                    {
                                        EnableRaisingEvents = false,
                                        StartInfo = {FileName = @path}
                                    };
            objDocProcess.Start();
        }


        private void BindAllCollegeAssociate()
        {
            var collegeAssociation = CollegeProvider.Instance.GetAllCollegeAssociationCategoryType("T");
            if (collegeAssociation.Count > 0)
            {
                rptCollegeAssociate.Visible = true;
                rptCollegeAssociate.DataSource = collegeAssociation;
                rptCollegeAssociate.DataBind();
            }
        }
        private void BindDisplayAds()
        {
            var collegeAssociation = CollegeProvider.Instance.GetAllCollegeAssociationCategoryType("D");
            if (collegeAssociation.Count > 0)
            {
                rptDisplayAds.Visible = true;
                rptDisplayAds.DataSource = collegeAssociation;
                rptDisplayAds.DataBind();
            }
        }
        private void ClearFeilds()
        {
            txtAssociationCategoryName.Text = string.Empty;
            chkAssociationStatus.Checked = false;
        }
        private void ValidationErrorMessages()
        {
            _objCommon = new Common();
            rfvAssociationCategoryType.ErrorMessage = _objCommon.GetValidationMessage("rfvAssociationCategoryType");
            rfvExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvExcelUpload");

        }

        protected void btnCollegeAssociat_Click(object sender, EventArgs e)
        {
              var errorMsg = "";
              var insert = 0;
            try
            {
                var objCollegeproperty = new CollegeAssociationCategoryProperty
                                             {
                                                 AssociationCategoryType =
                                                     Convert.ToString(
                                                         txtAssociationCategoryName
                                                             .Text.Trim()),
                                                 AssociationCategoryStatus =
                                                     chkAssociationStatus.Checked,
                                                    AssociationCategoryAmount=txtAmount.Text,
                                                 AssociationCategoryDescription = fckProductDesc.FckValue,
                                                 AssociationType=Convert.ToString(ddlProductType.SelectedValue)

                                             };
              
               if (ViewState["Id"]==null)
               {

                    insert = CollegeProvider.Instance.InsertCollegeAssociationCategoryType(objCollegeproperty, LoggedInUserId, out errorMsg);
               }
               else
               {
                   objCollegeproperty.AssociationCategoryTypeId=Convert.ToInt32(ViewState["Id"]);
                    insert = CollegeProvider.Instance.UpdateCollegeAssociationCategoryType(objCollegeproperty, LoggedInUserId, out errorMsg);
               }
                 BindAllCollegeAssociate();
                 BindDisplayAds();
                 ClearControl();
                
                lblSeccessMsg.Text = errorMsg;
                lblSeccessMsg.CssClass = insert > 0 ? "success show" : "info show";
            }
            catch (Exception ex)
            {
                 var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnCollegeAssociat_Click in AdminPanel/College/CollegeAssociationGroupMaster.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
      

        protected void btnUpload_Click1(object sender, EventArgs e)
        {
            var errMsg = "";

            _objCommon = new Common();
            try
            {
                _objCollegeAssociationCategoryProperty = new CollegeAssociationCategoryProperty();
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

                                _objCollegeAssociationCategoryProperty = new CollegeAssociationCategoryProperty
                                {

                                    AssociationCategoryType = _ds.Tables[0].Rows[j]["AssociationCategoryType"].ToString(),
                                    AssociationCategoryStatus = Convert.ToBoolean(_ds.Tables[0].Rows[j]["AssociationCategoryStatus"].ToString())

                                };
                                int result = CollegeProvider.Instance.UpdateCollegeAssociationCategoryType(_objCollegeAssociationCategoryProperty, LoggedInUserId, out errMsg);

                                if (result > 0)
                                {
                                    lblRecordsInserted.Text = "";
                                    lblRecordsInserted.Text = j + " row inserted out of " + _ds.Tables[0].Rows.Count;
                                }
                            }
                            lblSeccessMsg.Text = _objCommon.GetErrorMessage("lblSucessMsg");
                            Response.Redirect("CollegeGroupMaster.aspx");
                        }
                    }

                }
                else
                {
                    lblSeccessMsg.Text = _objCommon.GetErrorMessage("lblErrMsg");
                }
            }
            catch (Exception ex)
            {  var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnUpload_Click1 in AdminPanel/College/CollegeAssociationGroupMaster.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);}

        }

        protected void rptCollegeAssociate_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
               
                var collegeAssociation = CollegeProvider.Instance.GetCollegeAssociationCategoryTypeById(Convert.ToInt32( e.CommandArgument),"T");
                if (collegeAssociation.Count > 0)
                {
                    var data = collegeAssociation.FirstOrDefault();
                    if (data != null)
                    {
                        ViewState["Id"] = e.CommandArgument;
                        txtAmount.Text = data.AssociationCategoryAmount;
                        txtAssociationCategoryName.Text = data.AssociationCategoryType;
                        ddlProductType.ClearSelection();
                        ddlProductType.Items.FindByValue("T").Selected = true;
                        fckProductDesc.FckValue = data.AssociationCategoryDescription;
                        chkAssociationStatus.Checked = !string.IsNullOrEmpty(Convert.ToString( data.AssociationCategoryStatus)) ? Convert.ToBoolean(data.AssociationCategoryStatus) : false;
                        btnCollegeAssociat.Text = "Update";
                        lblInsertUpdate.Text = "Update Text Ads Code Details of " + data.AssociationCategoryType;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweqeewqe", "OpenPoup('divRankSourceInsert','650px','sndAddCollegeAssociation');return false;", true);
                    }
                }

            }
        }

        protected void rptDisplayAds_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {

                var collegeAssociation = CollegeProvider.Instance.GetCollegeAssociationCategoryTypeById(Convert.ToInt32(e.CommandArgument), "D");
                if (collegeAssociation.Count > 0)
                {
                    var data = collegeAssociation.FirstOrDefault();
                    if (data != null)
                    {
                        ViewState["Id"] = e.CommandArgument;
                        txtAmount.Text = data.AssociationCategoryAmount;
                        txtAssociationCategoryName.Text = data.AssociationCategoryType;
                        ddlProductType.ClearSelection();
                        ddlProductType.Items.FindByValue("D").Selected = true;
                        fckProductDesc.FckValue = data.AssociationCategoryDescription;
                        chkAssociationStatus.Checked = !string.IsNullOrEmpty(Convert.ToString(data.AssociationCategoryStatus)) ? Convert.ToBoolean(data.AssociationCategoryStatus) : false;
                        btnCollegeAssociat.Text = "Update";
                        lblInsertUpdate.Text = "Update Display Ads Code Details of " + data.AssociationCategoryType;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIfdw", "OpenPoup('divRankSourceInsert','650px','sndAddCollegeAssociation');return false;", true);
                    }
                }

            }
        }

        // Method to Clear The Control
        private void ClearControl()
        {
            txtAmount.Text="";
            txtAssociationCategoryName.Text="";
            ddlProductType.ClearSelection();
            fckProductDesc.FckValue="";
            chkAssociationStatus.Checked=false;
            btnCollegeAssociat.Text="Add";
            ViewState["Id"]=null;
            lblInsertUpdate.Text="Add Ads Code";

        }

        // Method to get the Total count of discount and Duration
        public string GetCountDiscountAndDuration(int advstType, int advstTypeId)
        {
            Common objCommon = new Common();
           return Convert.ToString(objCommon.GetAdvertisementDiscountDetails(advstType, advstTypeId).Rows.Count);
        }

        protected void UpdateDiscountDetails(object sender, EventArgs e)
        {
            string errMsg;
            var i = 0;
            Common objCommon = new Common();
            if (string.IsNullOrEmpty(hndDiscountId.Value))
            {
                i = objCommon.InsertAdvstDiscount(Convert.ToInt16(hndAdvstType.Value), Convert.ToInt32(hndAdvstYpeId.Value), Convert.ToInt16(txtProDiscount.Text), Convert.ToInt16(txtProductDuration.Text),
                            Common.GetDateFromString(txtValidityStartTime.Text), Common.GetDateFromString(txtProEndTime.Text), chkDefaultSelection.Checked, chkDiscountStatus.Checked,
                             out errMsg);
            }
            else
            {
                i = objCommon.UpdateAdvstDiscount(Convert.ToInt32(hndDiscountId.Value), Convert.ToInt16(hndAdvstType.Value), Convert.ToInt32(hndAdvstYpeId.Value), Convert.ToInt16(txtProDiscount.Text), Convert.ToInt16(txtProductDuration.Text),
                            Common.GetDateFromString(txtValidityStartTime.Text), Common.GetDateFromString(txtProEndTime.Text), chkDefaultSelection.Checked, chkDiscountStatus.Checked,
                             out errMsg);
            }
            
            lblSeccessMsg.Text = errMsg;
            lblSeccessMsg.CssClass = "show success";
           if (i > 0)
           {
               BindAllCollegeAssociate();
               BindDisplayAds();
               hndDiscountId.Value = "";
               hndAdvstType.Value = "";
               hndAdvstYpeId.Value = "";
           }
           else
           {
               ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweqeewqess", "OpenPoup('ProductDuration', '800', 'lnkViewDiscountDetails');return false;", true);
           }
        }
    }
}