using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.BL;
using System.Data;
using System.Data.SqlClient;
using IryTech.AdmissionJankari.Components;
namespace IryTech.AdmissionJankari20.Web.AdminPanel.BankDetails
{
    public partial class ManageBankDetails : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            if (IsPostBack) return;
            BindBankDetails();

        }
        private void BindBankDetails() {
            var bankData = BankProvider.Instance.GetAllBankList();
            if (bankData.Count > 0) { 
                rptBankDetails.DataSource = bankData;
                 rptBankDetails.DataBind();
            }
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            Common _objCommon = new Common();
            var bankData = BankProvider.Instance.GetAllBankList();
            if (bankData.Count > 0)
            {
                try
                {
                    rptBankDetails.Visible = true;

                    ucCustomPaging.BindDataWithPaging(rptBankDetails, Common.ConvertToDataTable(bankData));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in ManageBankDetails.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptBankDetails.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }
        protected void rptBankDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect("AddBankLoanDetails.aspx?BankId=" + e.CommandArgument);
                    break;
            }
        }

        protected void rptBankDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var rptModelVersion = (Repeater)(e.Item.FindControl("rptLoanDetails"));
                var hdnBankId = (HiddenField)e.Item.FindControl("hdnBankId");
                var lblResult = (Label) e.Item.FindControl("lblBankResult");
                var bankData = BankProvider.Instance.GetLoanListByBankId(Convert.ToInt16(hdnBankId.Value));
                if (bankData.Count > 0)
                {
                    lblResult.Visible = false;
                    rptModelVersion.Visible = true;
                    rptModelVersion.DataSource = bankData;
                    rptModelVersion.DataBind();
                }
                else
                {
                    lblResult.CssClass = "success";
                    lblResult.Visible = true;
                    lblResult.Text = new Common().GetErrorMessage("noRecords");
                    rptModelVersion.Visible = false;


                }

            }
        }
    }
}