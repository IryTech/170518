using System;
using System.Data;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Web.UI;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Product
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request["productid"] == null) return;
            BindProduct();
            GetProductDurationAndDiscount(Convert.ToInt32(Request["productid"]));
        }

        private void BindProduct()
        {
            try
            {
                var objDataset = new Common().GetProductAdsByProductId(Convert.ToInt32(Request["productid"]));
                if (objDataset.Rows.Count > 0)
                {
                    lnkUpdate.ToolTip = string.Format("Update product {0}", objDataset.Rows[0]["AjProductName"]);
                   hDetails.InnerHtml = Convert.ToString(objDataset.Rows[0]["AjProductName"]);
                   spnDescription.InnerHtml = Convert.ToString(objDataset.Rows[0]["AjProductDescription"]);
                    rptProduct.Visible = true;
                    rptProduct.DataSource = objDataset;
                    rptProduct.DataBind();
                    BindProductCategories();
                    BindProductFeatures();
                }
                else
                {
                    rptProduct.Visible = false;

                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo =
                    "Error in Executing  BindProduct in AdminPanel/Product/ProductDetails.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
  
        private void BindProductCategories()
        {
            try
            {
                var objCommon = new Common();

                var dt =
                    objCommon.GetProductCategoryByProductId(
                        Convert.ToInt32(Convert.ToInt32(Request["productid"])));
                if (dt.Tables.Count > 0)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        spnProductCount.InnerHtml = "Combined Products (" + dt.Tables[0].Rows.Count + ")";
                        var dat = dt.Tables[0].AsEnumerable()
                                              .Where(
                                                  i => i.Field<bool>("AjProductAdStatus")).CopyToDataTable();

                        rptProductCategory.Visible = true;
                        rptProductCategory.DataSource = dat;
                        rptProductCategory.DataBind();

                    }
                    else
                    {
                        rptProductCategory.Visible = false;

                    }
                }
                else
                {
                    rptProductCategory.Visible = false;

                }
            }
            catch
                (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing rptProduct_ItemDataBound in AdminPanel/Product/ProductDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindProductFeatures()
        {
            try
            {
                var objCommon = new Common();

                var dt =
                    objCommon.GetProductFeatures(
                        Convert.ToInt32(Request["productid"]));
                if (dt.Tables.Count > 0)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        spnBenefits.InnerHtml = "Benefits (" + dt.Tables[0].Rows.Count + ")";
                        rptFeatures.Visible = true;
                        rptFeatures.DataSource = dt.Tables[0];
                        rptFeatures.DataBind();

                    }
                    else
                    {
                        rptFeatures.Visible = false;

                    }
                }
                else
                {
                    rptFeatures.Visible = false;

                }
            }
            catch
                (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing BindProductFeatures in AdminPanel/Product/ProductDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                Response.Redirect("AddAdsProduct.aspx?ProductMasterId=" + Request["productid"]);
              
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing rptProduct_ItemCommand in AdminPanel/Product/ManageProduct.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        // Method to Get The Product Duration And Discount

        private void GetProductDurationAndDiscount(int productId)
        {
            Common objCommon = new Common();

            var data = objCommon.GetAdvertisementDiscountDetails(Convert.ToInt16(IryTech.AdmissionJankari.BO.AdvertismentType.Combo), productId);
            if (data.Rows.Count > 0)
            {
                rptProductDiscount.DataSource = data;
                rptProductDiscount.DataBind();
                rptProductDiscount.Visible = true;
            }
            else
            {
                rptProductDiscount.Visible = false;
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Common objCommon = new Common();
            var errMsg = "";

            var i = objCommon.UpdateAdvstDiscount(Convert.ToInt32(hndDiscountId.Value), Convert.ToInt16(IryTech.AdmissionJankari.BO.AdvertismentType.Combo), Convert.ToInt32(Request["productid"]), Convert.ToInt16(txtProDiscount.Text), Convert.ToInt16(txtProductDuration.Text),
                              Convert.ToDateTime(txtValidityStartTime.Text), Convert.ToDateTime(txtProEndTime.Text), chkDefaultSelection.Checked, chkDiscountStatus.Checked,
                              out errMsg);
            
            lblInsertUpdate.Text = errMsg;
            lblInsertUpdate.CssClass = "success";
            if (i > 0)
            {
                GetProductDurationAndDiscount(Convert.ToInt32(Request["productid"]));
                hndDiscountId.Value = "";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweqeewqe", "OpenPoup('ProductDuration','650px','lnkDiscount');return false;", true);
            }
            
        }
    }
}