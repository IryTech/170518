using System;
using System.Data;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Product
{
    public partial class ManageProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ucCustomPaging.PageSize = ClsSingelton.PageSize;
            ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            ucCustomPaging.CurrentPageIndex = Session["PageNum"] != null ? Convert.ToInt32(Session["PageNum"]) : 0;
            BindProduct();
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {

            try
            {
                var objDataset = new Common().GetProductAds();
                if (objDataset.Tables.Count > 0)
                {
                    if (objDataset.Tables[0].Rows.Count > 0)
                    {
                        rptProduct.Visible = true;
                        ucCustomPaging.Visible = true;
                        ucCustomPaging.BindDataWithPaging(rptProduct, objDataset.Tables[0]);
                    }
                    else
                    {
                        ucCustomPaging.Visible = false;
                        rptProduct.Visible = false;
                    }
                }
                else
                {
                    ucCustomPaging.Visible = false;
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
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in AdminPanel/Product/ManageProduct.aspx:: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        private void BindProduct()
        {
            try
            {
                var objDataset = new Common().GetProductAds();
                if (objDataset.Tables.Count > 0)
                {
                    if (objDataset.Tables[0].Rows.Count > 0)
                    {
                        rptProduct.Visible = true;
                        ucCustomPaging.Visible = true;
                        ucCustomPaging.BindDataWithPaging(rptProduct, objDataset.Tables[0]);
                    }
                    else
                    {
                        ucCustomPaging.Visible = false;
                        rptProduct.Visible = false;
                    }
                }
                else
                {
                    ucCustomPaging.Visible = false;
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
                const string addInfo = "Error in Executing  BindProduct in AdminPanel/Product/ManageProduct.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

    

        protected void rptProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Update")
                {
                    var objCommon = new Common();
                    var dt =
                        objCommon.GetProductAdsByProductId(
                            Convert.ToInt32(e.CommandArgument));
                    if (dt.Rows.Count <= 0) return;
                    Session["PageNum"] = ucCustomPaging.CurrentPageIndex;
                    Response.Redirect("AddAdsProduct.aspx?ProductMasterId=" + Convert.ToString(dt.Rows[0]["AjProductMasterId"]));
                }
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                ucCustomPaging.CurrentPageIndex = 0;    //to reset current page index
                if (!string.IsNullOrEmpty(txtProductSearch.Text))
                {
                    var objDataset = new Common().GetProductAdsByProductName(txtProductSearch.Text.Trim());
                    if (objDataset.Tables.Count > 0)
                    {
                        if (objDataset.Tables[0].Rows.Count > 0)
                        {
                            ucCustomPaging.Visible = false;
                            rptProduct.Visible = true;
                            rptProduct.DataSource = objDataset.Tables[0];
                            rptProduct.DataBind();
                        }
                        else
                        {
                            ucCustomPaging.Visible = false;
                            rptProduct.Visible = false;
                        }
                    }
                    else
                    {
                        ucCustomPaging.Visible = false;
                        rptProduct.Visible = false;

                    }

                }
                else
                {
                    BindProduct();
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing btnSearch_Click in AdminPanel/Product/ManageProduct.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        public string GetDurationNumer(object productId)
        {
            var data= new Common().GetAdvertisementDiscountDetails(1, Convert.ToInt32(productId));
            return data.Rows.Count.ToString();
        }
    }
}