using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
namespace IryTech.AdmissionJankari20.Web.AdminPanel.BankDetails
{
    public partial class AddBankLoanDetails : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.imgUpload.Click += this.BtnUploadImageClick;
            if (!IsPostBack)
            {
                if (Request.QueryString["BankId"] != null)
                {
                    BindBankDetails(Request.QueryString["BankId"]);
                    BindLoanDetails(Request.QueryString["BankId"]);
                    BindStudyPlace();
                    BindLoanRange();
                }
                else{
                    loanCriteria.InnerHtml="Bank Loan Criteria";
                    loanCriteria.Visible=true;
                   
                }
                ValidateField();
                BindStudyPlace();
                BindLoanRange(); 
            }
        }
        private void ValidateField()
        {
            Common _objCommon = new Common();
            rfvMobile.ErrorMessage = _objCommon.GetValidationMessage("rfvMobile") ?? "N/A";
            rfvEmailId.ErrorMessage = _objCommon.GetValidationMessage("rfvEmailId") ?? "N/A";
            revEmailId.ValidationExpression = ClsSingelton.aRegExpEmail;
            revEmailId.ErrorMessage = _objCommon.GetValidationMessage("revEmail") ?? "N/A";
            revMobile.ValidationExpression = ClsSingelton.aRegExpMobile;
            revMobile.ErrorMessage = _objCommon.GetValidationMessage("revMobile") ?? "N/A";
        }
        private void BindBankDetails(string bankId)
        {
            var bankData = BankProvider.Instance.GetBankListById(Convert.ToInt16(bankId));
            if (bankData.Count > 0)
            {

                var query = bankData.Select(result => new
                    {
                        BankName = result.BankName,
                        BankUrl = result.BankUrl,
                        BankShortName = result.BankShortName,
                        BankAddress = result.BankAddress,
                        BankShortDescription = result.BankShortDescription,
                        BankContactPerson = result.BankContactPerson,
                        BankContactPersonDesignation = result.BankContactPersonDesignation,
                        BankContactPersonMobile = result.BankContactPersonMobile,
                        BankPhoneNo = result.BankPhoneNo,
                        BankContactPersonEmailId = result.BankContactPersonEmailId,
                        BankLogo = result.BankLogo
                    }).First();
                lblInsertUpdate.Text = "Update Records Of " + query.BankName;
                txtBankName.Text = query.BankName != "" ? query.BankName : "N/A";
                txtBankShortName.Text = query.BankShortName != "" ? query.BankShortName : "N/A";
                txtBankUrl.Text = query.BankUrl != "" ? query.BankUrl : "N/A";
                txtBankPhone.Text = query.BankPhoneNo != "" ? query.BankPhoneNo : "N/A";
                txtBankAddress.Text = query.BankAddress != "" ? query.BankAddress : "N/A";
                txtContactPerson.Text = query.BankContactPerson != "" ? query.BankContactPerson : "N/A";
                txtContactEmailId.Text = query.BankContactPersonEmailId != "" ? query.BankContactPersonEmailId : "N/A";
                txtContactMobile.Text = query.BankContactPersonMobile != "" ? query.BankContactPersonMobile : "N/A";
                txtContactDesignation.Text = query.BankContactPersonDesignation != "" ? query.BankContactPersonDesignation : "N/A";
                fckBankDesc.FckValue = query.BankShortDescription != "" ? query.BankShortDescription : "N/A";
                hdnFileName.Value = query.BankLogo != "" ? query.BankLogo : "N/A";
                Common _objCommon = new Common();
                var relativeFolder = DateTime.Now.Year.ToString() + Path.DirectorySeparatorChar + DateTime.Now.Month +
                                     Path.DirectorySeparatorChar;

                var path = _objCommon.GetFilepath("BankImage");
                var folder = string.Format("{0}", _objCommon.GetFilepath("BankImage"));
                var fileName = query.BankLogo;
                            imgBank.Visible = true;
                imgBank.ImageUrl = folder + fileName;
                btnAdd.Text = "Update";
            }
        }
        private void BindLoanDetails(string bankId)
        {
            Loan.Visible = false; fldLoan.Visible = true;
            var loanData = BankProvider.Instance.GetLoanListByBankId(Convert.ToInt16(bankId));
            if (loanData.Count > 0)
            {
                rptLoanDetails.DataSource = loanData;
                rptLoanDetails.DataBind();

            }

        }
        private void BindStudyPlace()
        {
            var studyData = BankProvider.Instance.GetStudyPlace();
            if (studyData.Count > 0)
            {
                ddlCategory1.DataSource = studyData;
                ddlCategory1.DataTextField = "StudyPlaceName";
                ddlCategory1.DataValueField = "StudyPlaceId";
                ddlCategory1.DataBind();
                ddlCategory1.Items.Insert(0, new ListItem("Please Select","0"));
                ddlCategory2.DataSource = studyData;
                ddlCategory2.DataTextField = "StudyPlaceName";
                ddlCategory2.DataValueField = "StudyPlaceId";
                ddlCategory2.DataBind();
                ddlCategory2.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlCategory3.DataSource = studyData;
                ddlCategory3.DataTextField = "StudyPlaceName";
                ddlCategory3.DataValueField = "StudyPlaceId";
                ddlCategory3.DataBind();
                ddlCategory3.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlCategory4.DataSource = studyData;
                ddlCategory4.DataTextField = "StudyPlaceName";
                ddlCategory4.DataValueField = "StudyPlaceId";
                ddlCategory4.DataBind();
                ddlCategory4.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlLoanCategory.DataSource = studyData;
                ddlLoanCategory.DataTextField = "StudyPlaceName";
                ddlLoanCategory.DataValueField = "StudyPlaceId";
                ddlLoanCategory.DataBind();
                ddlLoanCategory.Items.Insert(0, new ListItem("Please Select", "0"));

            }
        }
        private void BindLoanRange()
        {
            var LoanRangeData = BankProvider.Instance.GetLoanRange();
            if (LoanRangeData.Count > 0)
            {
                ddlLoanRange1.DataSource = LoanRangeData;
                ddlLoanRange1.DataTextField = "Amount";
                ddlLoanRange1.DataValueField = "LoanRangeId";
                ddlLoanRange1.DataBind();
                ddlLoanRange1.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlLoanRange2.DataSource = LoanRangeData;
                ddlLoanRange2.DataTextField = "Amount";
                ddlLoanRange2.DataValueField = "LoanRangeId";
                ddlLoanRange2.DataBind();
                ddlLoanRange2.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlLoanRange3.DataSource = LoanRangeData;
                ddlLoanRange3.DataTextField = "Amount";
                ddlLoanRange3.DataValueField = "LoanRangeId";
                ddlLoanRange3.DataBind();
                ddlLoanRange3.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlLoanRange4.DataSource = LoanRangeData;
                ddlLoanRange4.DataTextField = "Amount";
                ddlLoanRange4.DataValueField = "LoanRangeId";
                ddlLoanRange4.DataBind();
                ddlLoanRange4.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlLoanRange.DataSource = LoanRangeData;
                ddlLoanRange.DataTextField = "Amount";
                ddlLoanRange.DataValueField = "LoanRangeId";
                ddlLoanRange.DataBind();
                ddlLoanRange.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            LoanDetailsProperty objLoanDetailsProperty = new LoanDetailsProperty();

            int result;
            int bankId;
            string errMsg;
            var objBankDetailsProperty = new BankDetailsProperty
            {
                BankName = txtBankName.Text.Trim(),
                BankShortName = txtBankShortName.Text.Trim(),
                BankAddress = txtBankAddress.Text.Trim(),
                BankShortDescription = fckBankDesc.FckValue,
                BankPhoneNo = txtBankPhone.Text.Trim(),
                BankUrl = txtBankUrl.Text.Trim(),
                BankContactPerson = txtContactPerson.Text.Trim(),
                BankContactPersonDesignation = txtContactDesignation.Text.Trim(),
                BankContactPersonEmailId = txtContactEmailId.Text.Trim(),
                BankContactPersonMobile = txtContactMobile.Text.Trim(),
                BankLogo = hdnFileName.Value

            };
            if (btnAdd.Text == "Save")
            {
                result = BankProvider.Instance.InsertBankInfo(objBankDetailsProperty, LoggedInUserId, out errMsg, out bankId);
                if (result > 0)
                {
                    objLoanDetailsProperty.BankId = bankId;
                    objLoanDetailsProperty.Eligibilty = txtEligibilty.Text.Trim();
                    objLoanDetailsProperty.StudyPlaceId = Convert.ToInt16(ddlCategory1.SelectedValue)!=0?Convert.ToInt16(ddlCategory1.SelectedValue):0;
                    objLoanDetailsProperty.LoanRangeId = Convert.ToInt16(ddlLoanRange1.SelectedValue) != 0 ? Convert.ToInt16(ddlLoanRange1.SelectedValue) : 0;
                    objLoanDetailsProperty.Security = txtSecurity.Text.Trim();
                    objLoanDetailsProperty.RateOfInterest = txtRateOfInterest.Text.Trim();
                    objLoanDetailsProperty.Margin = txtMargin.Text.Trim();
                    objLoanDetailsProperty.ProcessingFees = txtProcessingFee.Text.Trim();
                    objLoanDetailsProperty.ProcessingTime = txtProcessingTime.Text.Trim();
                    objLoanDetailsProperty.Remark = txtRemark.Text.Trim();
                    objLoanDetailsProperty.RepaymentDuration = txtRepaymentDuration.Text.Trim();
                    int result1 = BankProvider.Instance.InsertLoanInfo(objLoanDetailsProperty, LoggedInUserId, out errMsg);

                    objLoanDetailsProperty.StudyPlaceId = Convert.ToInt16(ddlCategory2.SelectedValue) != 0 ? Convert.ToInt16(ddlCategory2.SelectedValue) : 0;
                    objLoanDetailsProperty.LoanRangeId = Convert.ToInt16(ddlLoanRange2.SelectedValue) != 0 ? Convert.ToInt16(ddlLoanRange2.SelectedValue) : 0;
                    objLoanDetailsProperty.Security = txtSecurity1.Text.Trim();
                    objLoanDetailsProperty.RateOfInterest = txtRateOfInterest1.Text.Trim();
                    objLoanDetailsProperty.Margin = txtMargin1.Text.Trim();
                    objLoanDetailsProperty.ProcessingFees = txtProcessingFee1.Text.Trim();
                    objLoanDetailsProperty.ProcessingTime = txtProcessingTime1.Text.Trim();
                    objLoanDetailsProperty.Remark = txtRemark1.Text.Trim();
                    objLoanDetailsProperty.RepaymentDuration = txtRepaymentDuration1.Text.Trim();
                    int result2 = BankProvider.Instance.InsertLoanInfo(objLoanDetailsProperty, LoggedInUserId, out errMsg);

                    objLoanDetailsProperty.StudyPlaceId = Convert.ToInt16(ddlCategory3.SelectedValue) != 0 ? Convert.ToInt16(ddlCategory3.SelectedValue) : 0;
                    objLoanDetailsProperty.LoanRangeId = Convert.ToInt16(ddlLoanRange3.SelectedValue) != 0 ? Convert.ToInt16(ddlLoanRange3.SelectedValue) : 0;
                    objLoanDetailsProperty.Security = txtSecurity2.Text.Trim();
                    objLoanDetailsProperty.RateOfInterest = txtRateOfInterest2.Text.Trim();
                    objLoanDetailsProperty.Margin = txtMargin2.Text.Trim();
                    objLoanDetailsProperty.ProcessingFees = txtProcessingFee2.Text.Trim();
                    objLoanDetailsProperty.ProcessingTime = txtProcessingTime2.Text.Trim();
                    objLoanDetailsProperty.Remark = txtRemark2.Text.Trim();
                    objLoanDetailsProperty.RepaymentDuration = txtRepaymentDuration2.Text.Trim();
                    int result3 = BankProvider.Instance.InsertLoanInfo(objLoanDetailsProperty, LoggedInUserId, out errMsg);

                    objLoanDetailsProperty.StudyPlaceId = Convert.ToInt16(ddlCategory4.SelectedValue) != 0 ? Convert.ToInt16(ddlCategory4.SelectedValue) : 0;
                    objLoanDetailsProperty.LoanRangeId = Convert.ToInt16(ddlLoanRange4.SelectedValue) != 0 ? Convert.ToInt16(ddlLoanRange4.SelectedValue) : 0;
                    objLoanDetailsProperty.Security = txtSecurity3.Text.Trim();
                    objLoanDetailsProperty.RateOfInterest = txtRateOfInterest3.Text.Trim();
                    objLoanDetailsProperty.Margin = txtMargin3.Text.Trim();
                    objLoanDetailsProperty.ProcessingFees = txtProcessingFee3.Text.Trim();
                    objLoanDetailsProperty.ProcessingTime = txtProcessingTime3.Text.Trim();
                    objLoanDetailsProperty.Remark = txtRemark3.Text.Trim();
                    objLoanDetailsProperty.RepaymentDuration = txtRepaymentDuration3.Text.Trim();
                    int result4 = BankProvider.Instance.InsertLoanInfo(objLoanDetailsProperty, LoggedInUserId, out errMsg);
                    Response.Redirect("",true);
                }
            }
            else
            {
                objBankDetailsProperty.BankId = Convert.ToInt16(Request.QueryString["BankId"]);
                result = BankProvider.Instance.UpdateBankInfo(objBankDetailsProperty, LoggedInUserId, out errMsg);
                if (result > 0)
                {
                    ClearControl();
                    btnAdd.Text = "Save";
                    lblSuccess.Visible = true;
                    lblSuccess.Text = errMsg;
                }
                else
                {
                    lblInform.Visible = true;
                    lblInform.Text = errMsg;
                }
            }
        }
        #region ImageUploadHandler
        private void BtnUploadImageClick(object sender, EventArgs e)
        {
            Common _objCommon = new Common();
            var relativeFolder = DateTime.Now.Year.ToString() + Path.DirectorySeparatorChar + DateTime.Now.Month +
                                 Path.DirectorySeparatorChar;

            var path = _objCommon.GetFilepath("BankImage");
            var folder = string.Format("{0}", _objCommon.GetFilepath("BankImage"));
            var fileName = this.flpImgUpload.FileName;
            hdnFileName.Value = fileName;
            this.Upload(folder, this.flpImgUpload, fileName);
            imgBank.Visible = true;
            imgBank.ImageUrl = folder + fileName;
        }
        #endregion
        #region upload method
        private void Upload(string virtualFolder, FileUpload control, string fileName)
        {
            var folder = this.Server.MapPath(virtualFolder);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            control.PostedFile.SaveAs(folder + fileName);

        }
        #endregion

        protected void rptLoanDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int loanId = Convert.ToInt16(e.CommandArgument);
                var data = BankProvider.Instance.GetLoanListByLoan(loanId);
                var query = data.Select(result => new
                   {
                                              Eligibilty = result.Eligibilty,
                       Security = result.Security,
                       StudyPlaceId = result.StudyPlaceId,
                       LoanRangeId = result.LoanRangeId,
                       Margin = result.Margin,
                       ProcessingFees = result.ProcessingFees,
                       ProcessingTime = result.ProcessingTime,
                       RateOfInterest = result.RateOfInterest,
                       Remark = result.Remark,
                       RepaymentDuration = result.RepaymentDuration,
                       BankId = result.BankId
                   }).First();

                txtEligibiltyUp.Text = query.Eligibilty != "" ? query.Eligibilty : "N/A";
                txtSecurityUp.Text = query.Security != "" ? query.Security : "N/A";
                ddlLoanCategory.SelectedValue = Convert.ToString(query.StudyPlaceId);
                ddlLoanRange.SelectedValue = Convert.ToString(query.LoanRangeId);
                txtMarginUp.Text = query.Margin != "" ? query.Margin : "N/A";
                txtFeesUp.Text = query.ProcessingFees != "" ? query.ProcessingFees : "N/A";
                txtPTimeUp.Text = query.ProcessingTime != "" ? query.ProcessingTime : "N/A";
                txtRoi.Text = query.RateOfInterest != "" ? query.RateOfInterest : "N/A";
                txtRemarkUp.Text = query.Remark != "" ? query.Remark : "N/A";
                hdnLoanId.Value = Convert.ToString(loanId);
                txtRepayentUP.Text = query.RepaymentDuration != "" ? query.RepaymentDuration : "N/A";
                Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript",
                                                                "openLoanPop('loanPOpUp');", true);
            }
        }

        protected void btnLoanUpdate_Click(object sender, EventArgs e)
        {
            string errMsg = "";
                   LoanDetailsProperty objLoanDetailsProperty = new LoanDetailsProperty();
                   objLoanDetailsProperty.LoanId = Convert.ToInt16(hdnLoanId.Value);
                    objLoanDetailsProperty.BankId =Convert.ToInt16( Request.QueryString["BankId"]);
                    objLoanDetailsProperty.Eligibilty = txtEligibiltyUp.Text.Trim();
                    objLoanDetailsProperty.StudyPlaceId = Convert.ToInt16(ddlLoanCategory.SelectedValue);
                    objLoanDetailsProperty.LoanRangeId = Convert.ToInt16(ddlLoanRange.SelectedValue);
                    objLoanDetailsProperty.Security = txtSecurityUp.Text.Trim();
                    objLoanDetailsProperty.RateOfInterest = txtRoi.Text.Trim();
                    objLoanDetailsProperty.Margin = txtMarginUp.Text.Trim();
                    objLoanDetailsProperty.ProcessingFees = txtFeesUp.Text.Trim();
                    objLoanDetailsProperty.ProcessingTime = txtPTimeUp.Text.Trim();
                    objLoanDetailsProperty.Remark = txtRemarkUp.Text.Trim();
                    objLoanDetailsProperty.RepaymentDuration = txtRepayentUP.Text.Trim();
                    int result1 = BankProvider.Instance.UpdateLoanInfo(objLoanDetailsProperty, LoggedInUserId, out errMsg);
                    if (result1 > 0)
                    {
                        BindLoanDetails(Request.QueryString["BankId"]);
                        lblSuccess.Visible = true;
                        lblSuccess.Text = errMsg;
                    }
                    else
                    {
                        lblInform.Visible = true;
                        lblInform.Text = errMsg;
                    }
                  
        }
        private void ClearControl(){
        txtBankName.Text=string.Empty;
            txtBankAddress.Text=string.Empty;
            txtBankPhone.Text=string.Empty;
            txtBankShortName.Text=string.Empty;
            txtBankUrl.Text=string.Empty;
            txtContactDesignation.Text=string.Empty;
            txtContactEmailId.Text=string.Empty;
            txtContactMobile.Text=string.Empty;
            txtContactPerson.Text=string.Empty;
            fckBankDesc.FckValue=string.Empty;
        }
    }
}