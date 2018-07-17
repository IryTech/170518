using System;
using System.Linq;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using System.Web.Services;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class CollegeRankSourceCategory : SecurePage
    {
        private Common _objCommon;
        private CollegeRankSource _objCollegeRankSource;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
            ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
        
            if (IsPostBack) return;
            ValidationErrorMessages();
            BindCollegeRankSource();
        }
        private void ValidationErrorMessages()
        {
            _objCommon = new Common();
            rfvCollegeRank.ErrorMessage = _objCommon.GetValidationMessage("rfvCollegeRank") ?? "N/A";
            revExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("rfvUploadExcel") ?? "N/A";
            revExcelUpload.ValidationExpression = ClsSingelton.aRegExpExcelUpload;
            revExcelUpload.ErrorMessage = _objCommon.GetValidationMessage("revUploadExcel") ?? "N/A";
        }
        private void BindCollegeRankSource()
        {
            _objCommon = new Common();
            var data = CollegeProvider.Instance.GetAllCollegeRankSourceList();
            if (data.Count > 0)
            {
                lblInform.Visible = false;
                rptCollegeRankSource.Visible = true;
                ucCustomPaging.BindDataWithPaging(rptCollegeRankSource, Common.ConvertToDataTable(data));
                
            }
            else
            {
                rptCollegeRankSource.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords") ?? "N/A";
            }
        }
        private void InsertUpdateRankSource()
        {
            _objCollegeRankSource=new CollegeRankSource
                                      {
                                          CollegeRankSourceName = txtCollegeRank.Text.Trim(),
                                          CollegeRankSourceStatus = chkCollegeRankStatus.Checked
                                      };

            int result;
            string errMsg;
            if (btnCollegeRank.Text == "Save")
            {
                result = CollegeProvider.Instance.InsertCollegeRankSource(_objCollegeRankSource, new SecurePage().LoggedInUserId, out errMsg);
                                                                        
                if (result > 0)
                {

                    lblSuccess.CssClass = "success show";
                    lblSuccess.Text = errMsg;
                    ClearFileds();
                    BindCollegeRankSource();
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = errMsg;
                }
            }
            
        }
        private void ClearFileds()
        {
            txtCollegeRank.Text = string.Empty;
            chkCollegeRankStatus.Checked = false;
        }

        protected void BtnCollegeRankClick(object sender, EventArgs e)
        {
            InsertUpdateRankSource();

        }

        [WebMethod]
        public static string  UpdateRankSource(int rankSourceId, string rankSource, bool rankSourceStatus)
        {

            CollegeRankSource _objCollegeRankSource = new CollegeRankSource
                                {
                                    CollegeRankSourceName = rankSource.Trim(),
                                    CollegeRankSourceStatus = rankSourceStatus,
                                    CollegeRankSourceId = rankSourceId
                                };

            string errMsg = "";
        var    result = CollegeProvider.Instance.UpdateCollegeRankSource(_objCollegeRankSource, new SecurePage().LoggedInUserId, out errMsg);

            return errMsg;
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            BindCollegeRankSource();

        }
        
    }
}