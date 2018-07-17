using System;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.NewsArticle
{
    public partial class FAQDetails : SecurePage 
    {
        Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;

            if (IsPostBack) return;           
                BindGetAllFaqDetailsList();
                BindGetAllFaqCategoryList();
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            var data = FAQProvider.Instance.GetAllFAQDetailsList();
            if (data.Count > 0)
            {
                try
                {
                    rptFAQDeatils.Visible = true;
                    lblInform.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptFAQDeatils, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in FAQDetails.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptFAQDeatils.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }


        private void BindGetAllFaqDetailsList()
        {
            _objCommon = new Common();
          var data=  FAQProvider.Instance.GetAllFAQDetailsList();
          if (data.Count > 0)
          {
             rptFAQDeatils.Visible = true;
              lblEditStatus.Visible = true;
              ucCustomPaging.BindDataWithPaging(rptFAQDeatils, Common.ConvertToDataTable(data));
            
              lblError.Visible = false;

          }
          else
          {
              rptFAQDeatils.Visible = false;
              lblInform.Visible = true;
              lblInform.Text = _objCommon.GetErrorMessage("noRecords");
          }
        }
        private void BindGetAllFaqCategoryList()
        {
          var data=  FAQProvider.Instance.GetAllFAQCategoryList();
            if (data.Count > 0)
            {
               ddlFAQCategory.DataSource = data;
               ddlFAQCategory.DataTextField = "FAQCategoryName";
               ddlFAQCategory.DataValueField = "FAQCategoryId";
               ddlFAQCategory.DataBind();
               ddlFAQCategory.Items.Insert(0, new ListItem("Please Select"));

            }
            else
            {
                ddlFAQCategory.Items.Insert(0, new ListItem("Please Select"));

            }
        }
        private void BindGetFaqDetailsByName()
        {

            _objCommon = new Common();
            var data= FAQProvider.Instance.GetFAQDetailsByName(txtFAQName.Text);
            if (data.Count > 0)
            {
                rptFAQDeatils.Visible = true;
                ucCustomPaging.BindDataWithPaging(rptFAQDeatils, Common.ConvertToDataTable(data));

            lblInform.Visible = false;
            }
            else
            {
                rptFAQDeatils.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }
            
        }
        private void BindGetFaqDetailsByFaqCategory()
        {
            _objCommon = new Common();
            try
            {
                var data = FAQProvider.Instance.GetFAQDetailsByFAQCategory(Convert.ToInt32(ddlFAQCategory.SelectedValue.ToString()));
                if (data.Count > 0)
                {
                    rptFAQDeatils.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptFAQDeatils, Common.ConvertToDataTable(data));
                    lblInform.Visible = false;
                    rptFAQDeatils.Visible = true;
                }
                else
                {
                    rptFAQDeatils.Visible = false;
                    lblInform.Visible = true;
                    lblInform.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing BindGetFaqDetailsByFaqCategory in FAQDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void BtnSearchClick(object sender, EventArgs e)
        {            
                BindGetFaqDetailsByName();
        }
       
        protected void DdlFaqCategorySelectedIndexChanged(object sender, EventArgs e)
        {
                BindGetFaqDetailsByFaqCategory();
          
            
            
        }

    }
}