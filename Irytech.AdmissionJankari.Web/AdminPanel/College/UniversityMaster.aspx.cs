using System;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class UniversityMaster : SecurePage 
    {
        Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindUniversityDetails();
            BindUniversityCategory();

        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            lblInform.Visible = false;
            var data = UniversityProvider.Instance.GetAllUniversityList();
            if (data.Count > 0)
            {try
            {
                rptUniversityMaster.Visible = true;
                ucCustomPaging.BindDataWithPaging(rptUniversityMaster, Common.ConvertToDataTable(data));
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in UniversityMaster.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            }
            else
            {
                rptUniversityMaster.Visible = false;

            }
        }
        private void BindUniversityDetails()
        {
            _objCommon = new Common();
            var data = UniversityProvider.Instance.GetAllUniversityList();
            if (data.Count > 0)
            {
                try
                {
                    ucCustomPaging.Visible = true;
                    rptUniversityMaster.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptUniversityMaster, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in UniversityMaster.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                ucCustomPaging.Visible = false;
                rptUniversityMaster.Visible = false;

            }
        }
        private void BindUniversityCategory()
        {
            _objCommon = new Common();
            var data = UniversityProvider.Instance.GetAllUniversityCategoryList();
            if (data.Count > 0)
            {
                ddlUniversityCategory.DataSource = data;
                ddlUniversityCategory.DataTextField = "UniversityCategoryName";
                ddlUniversityCategory.DataValueField = "UniversityCategoryId";
                ddlUniversityCategory.DataBind();
                ddlUniversityCategory.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else { ddlUniversityCategory.Items.Insert(0, new ListItem("--Select--", "0")); }
        }
        protected void RptUniversityMasterItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch(e.CommandName)
            {
                case "Edit":
                    Response.Redirect("AddUniversityMaster.aspx?UniversityId="+e.CommandArgument.ToString(),true);
                    break;
            }
        }

        protected void BtnSearchClick(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtUniversityListByName.Text))
            {
                BindUniversityDetailsByName(txtUniversityListByName.Text);
            }
            else
            {
                BindUniversityDetails();
            }
        }
        private void BindUniversityDetailsByName(string universityName)
        {
            lblInform.Visible = false;
            _objCommon = new Common();
            var data = UniversityProvider.Instance.GetUniversityListByName(universityName);
            if (data.Count > 0)
            {
                ucCustomPaging.Visible = true;
                rptUniversityMaster.Visible = true;
                lblEditStatus.Visible = true;
                ucCustomPaging.BindDataWithPaging(rptUniversityMaster, Common.ConvertToDataTable(data),true);
            }
            else
            {
                ucCustomPaging.Visible = false;
                rptUniversityMaster.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }
        }
        private void BindUniversityDetailsByCategory(int universityCategoryId)
        {
            lblInform.Visible = false;
            _objCommon = new Common();
            var data = UniversityProvider.Instance.GetUniversityListByCategory(universityCategoryId);
            if (data.Count > 0)
            {
                ucCustomPaging.Visible = true;
                rptUniversityMaster.Visible = true;
                lblEditStatus.Visible = true;
                ucCustomPaging.BindDataWithPaging(rptUniversityMaster, Common.ConvertToDataTable(data));
              
            }
            else
            {
                ucCustomPaging.Visible = false;
                rptUniversityMaster.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }
        }
        private void BindUniversityDetailsByIdAndName(int universityCategoryId,string universityName)
        {
            lblInform.Visible = false;
            _objCommon = new Common();
            var data = UniversityProvider.Instance.GetUniversityListByCategoryUniversityName(universityCategoryId,
                                                                                             universityName);
            if (data.Count > 0)
            {
                ucCustomPaging.Visible = true;
                rptUniversityMaster.Visible = true;
                lblEditStatus.Visible = true;
                ucCustomPaging.BindDataWithPaging(rptUniversityMaster, Common.ConvertToDataTable(data));
             
            }
            else
            {
                ucCustomPaging.Visible = false;
                rptUniversityMaster.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }
        }

        protected void ddlUniversityCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUniversityCategory.SelectedIndex > 0)
            {
                BindUniversityDetailsByCategory(Convert.ToInt16(ddlUniversityCategory.SelectedValue));
            }
            else
            {
                BindUniversityDetails();
            }
        }
    }
}