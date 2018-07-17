using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Product
{
    public partial class AddAdsProduct : System.Web.UI.Page
    {
        Common objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            StaticContent();
            BindBannerPosition();
            BindAssociationType();
            if (Request["ProductMasterId"] != null)
            {
                btnAdsProduct.Text = @"Update";
                BindProductDetails();
                GetProductDurationAndDiscount(Convert.ToInt32(Request["ProductMasterId"]));
            }

        }

        private void StaticContent()
        {
            lblHeader.Text = @"Add Ads Product";
            fckProductDesc.ValidationGroup = "AdsProduct";
            fckProductDesc.ErrorMessage = "Please enter product description";
            fckProductDesc.TooTip = "Please enter product description";

        }

        private void BindBannerPosition()
        {
            objCommon = new Common();
            try
            {

                var data = objCommon.GetBanner();
                if (data.Tables.Count <= 0) return;
                if (data.Tables[0].Rows.Count <= 0) return;
                rptBannerPosition.DataSource = data;
                rptBannerPosition.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing BindBannerPosition in AdminPanel/Product/AddAdsProduct.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void BindAssociationType()
        {
            try
            {
                var collegeAssociation =
                    CollegeProvider.Instance.GetAllCollegeAssociationCategoryType()
                                   .OrderBy(x => x.AssociationCategoryTypeId)
                                   .ToList();
                if (collegeAssociation.Count <= 0) return;
                rptTextAds.Visible = true;
                rptTextAds.DataSource = collegeAssociation;
                rptTextAds.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing BindAssociationType in AdminPanel/Product/AddAdsProduct.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void BindProductDetails()
        {
            objCommon = new Common();
            try
            {
                
                var dt =
                    objCommon.GetProductAdsByProductId(
                        Convert.ToInt32(Request["ProductMasterId"]));
                if (dt.Rows.Count > 0)
                {
                    txtProductName.Text = Convert.ToString(dt.Rows[0]["AjProductName"]);
                    txtProPriority.Text = Convert.ToString(dt.Rows[0]["AjProductDisplayPriority"]);
                    chkStatus.Checked = Convert.ToBoolean(dt.Rows[0]["AjProductStatus"]);
                    fckProductDesc.FckValue = Convert.ToString(dt.Rows[0]["AjProductDescription"]);

                    var featuresDataset = new Common().GetProductFeatures(Convert.ToInt32(Request["ProductMasterId"]));
                    if (featuresDataset.Tables[0].Rows.Count > 0)
                    {
                        switch (featuresDataset.Tables[0].Rows.Count)
                        {
                            case 1:
                                txtFeatures1.Text =
                                    Convert.ToString(featuresDataset.Tables[0].Rows[0]["AjProductFeatures"]);
                                break;
                            case 2:
                                txtFeatures1.Text =
                                    Convert.ToString(featuresDataset.Tables[0].Rows[0]["AjProductFeatures"]);
                                txtFeatures2.Text =
                                    Convert.ToString(featuresDataset.Tables[0].Rows[1]["AjProductFeatures"]);
                                break;
                            case 3:
                                txtFeatures1.Text =
                                    Convert.ToString(featuresDataset.Tables[0].Rows[0]["AjProductFeatures"]);
                                txtFeatures2.Text =
                                    Convert.ToString(featuresDataset.Tables[0].Rows[1]["AjProductFeatures"]);
                                txtFeatures3.Text =
                                    Convert.ToString(featuresDataset.Tables[0].Rows[2]["AjProductFeatures"]);
                                break;
                            case 4:
                                txtFeatures1.Text =
                                    Convert.ToString(featuresDataset.Tables[0].Rows[0]["AjProductFeatures"]);
                                txtFeatures2.Text =
                                    Convert.ToString(featuresDataset.Tables[0].Rows[1]["AjProductFeatures"]);
                                txtFeatures3.Text =
                                    Convert.ToString(featuresDataset.Tables[0].Rows[2]["AjProductFeatures"]);
                                txtFeatures4.Text =
                                    Convert.ToString(featuresDataset.Tables[0].Rows[3]["AjProductFeatures"]);

                                break;
                            default:
                                txtFeatures1.Text =
                                    Convert.ToString(featuresDataset.Tables[0].Rows[0]["AjProductFeatures"]);
                                txtFeatures2.Text =
                                    Convert.ToString(featuresDataset.Tables[0].Rows[1]["AjProductFeatures"]);
                                txtFeatures3.Text =
                                    Convert.ToString(featuresDataset.Tables[0].Rows[2]["AjProductFeatures"]);
                                txtFeatures4.Text =
                                    Convert.ToString(featuresDataset.Tables[0].Rows[3]["AjProductFeatures"]);
                                txtFeatures5.Text =
                                    Convert.ToString(featuresDataset.Tables[0].Rows[4]["AjProductFeatures"]);
                                break;
                        }

                    }

                    var dt1 = objCommon.GetProductCategoryByProductId(Convert.ToInt32(Request["ProductMasterId"]));
                    if (dt1.Tables[0].Rows.Count > 0)
                    {
                        var query =
                            dt1.Tables[0].AsEnumerable()
                                         .Where(
                                             i =>
                                             i.Field<bool>("AjProductIsDisplayBanner") == true &&
                                             i.Field<bool>("AjProductAdStatus") == true)
                                         .ToList();
                        var productSepereatedList = string.Join(",",
                                                                from k in query select k.Field<Int32>("AddId"));
                        var productSepereatedList1 = string.Join(",",
                                                                 from k in
                                                                     dt1.Tables[0].AsEnumerable()
                                                                                  .Where(
                                                                                      i =>
                                                                                      i.Field<bool>(
                                                                                          "AjProductIsDisplayBanner") ==
                                                                                      false &&
                                                                                      i.Field<bool>("AjProductAdStatus") ==
                                                                                      true)
                                                                                  .ToList()
                                                                 select k.Field<Int32>("AddId"));
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp",
                                                            "<script type='text/javascript'>SelectList('" +
                                                            productSepereatedList + "','" + productSepereatedList1 +
                                                            "','" +
                                                            dt.Rows[0]["AjProductAmount"] + "','" + featuresDataset.Tables[0].Rows.Count + "');</script>", false);


                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing BindProductDetails in AdminPanel/Product/AddAdsProduct.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        protected void btnAdsProduct_Click(object sender, EventArgs e)
        {
            InsertUpdateProductDetails();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddAdsProduct.aspx");
        }



        private void InsertUpdateProductDetails()
        {
            objCommon = new Common();
            try
            {
                int productId = 0;
                string errmsg = "";
                if (Request["ProductMasterId"] == null)
                {
                    var result = objCommon.InsertAdsProduct(txtProductName.Text.Trim(),
                                                               fckProductDesc.FckValue.Trim(),
                                                               Convert.ToInt32(txtProductCots.Text),
                                                               Convert.ToInt16(txtProPriority.Text), chkStatus.Checked,
                                                               hdnBanner.Value.TrimEnd(','),
                                                               hdnAssociationId.Value.TrimEnd(','),
                                                               new SecurePage().LoggedInUserId, out errmsg, out productId,chkBestvalue.Checked == true ? true : false);

                    if (result > 0)
                    {
                        InsertProductFeatures(productId);
                        Session["PageNum"] = null;
                        Response.Redirect("ManageProduct.aspx");
                    }
                }
                else
                {
                    var result = objCommon.UpdateAdsProduct(Convert.ToInt32(Request["ProductMasterId"]),
                                                               txtProductName.Text.Trim(),
                                                               fckProductDesc.FckValue.Trim(),
                                                               Convert.ToInt32(txtProductCots.Text),
                                                               Convert.ToInt16(txtProPriority.Text), chkStatus.Checked,
                                                               hdnBanner.Value.TrimEnd(','),
                                                               hdnAssociationId.Value.TrimEnd(','),
                                                               new SecurePage().LoggedInUserId, out errmsg, out productId,chkBestvalue.Checked==true?true:false);
                    if (result > 0)
                    {
                        objCommon.DeleteProductFeatures(Convert.ToInt32(Request["ProductMasterId"]));
                        InsertProductFeatures(Convert.ToInt32(Request["ProductMasterId"]));
                        Response.Redirect("ManageProduct.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing InsertUpdateProductDetails in AdminPanel/Product/AddAdsProduct.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void InsertProductFeatures(int productId)
        {
            objCommon = new Common();
            try
            {

                objCommon.InsertProductFeatures(txtFeatures1.Text.Trim(), productId,
                                                   new SecurePage().LoggedInUserId);

                if (!string.IsNullOrEmpty(txtFeatures2.Text.Trim()))
                {

                    objCommon.InsertProductFeatures(txtFeatures2.Text.Trim(), productId,
                                                       new SecurePage().LoggedInUserId);
                }
                if (!String.IsNullOrEmpty(txtFeatures3.Text.Trim()))
                {

                    objCommon.InsertProductFeatures(txtFeatures3.Text.Trim(), productId,
                                                       new SecurePage().LoggedInUserId);
                }
                if (!String.IsNullOrEmpty(txtFeatures4.Text.Trim()))
                {

                    objCommon.InsertProductFeatures(txtFeatures4.Text.Trim(), productId,
                                                       new SecurePage().LoggedInUserId);
                }
                if (!String.IsNullOrEmpty(txtFeatures5.Text.Trim()))
                {

                    objCommon.InsertProductFeatures(txtFeatures5.Text.Trim(), productId,
                                                       new SecurePage().LoggedInUserId);
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing InsertUpdateProductDetails in AdminPanel/Product/AddAdsProduct.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        // Method to Get The Product Duration And Discount

        private void GetProductDurationAndDiscount(int productId)
        {
            objCommon = new Common();
            divProductDuration.Visible = true;
            var data = objCommon.GetAdvertisementDiscountDetails(Convert.ToInt16(AdvertismentType.Combo), productId);
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
            objCommon= new Common();
            var errMsg="";
            var i = 0;
            if (string.IsNullOrEmpty(hndDiscountId.Value))
            {
                i = objCommon.InsertAdvstDiscount(Convert.ToInt16(AdvertismentType.Combo), Convert.ToInt32(Request["ProductMasterId"]), Convert.ToInt16(txtProDiscount.Text), Convert.ToInt16(txtProductDuration.Text),
                                Convert.ToDateTime(txtValidityStartTime.Text), Convert.ToDateTime(txtProEndTime.Text), chkDefaultSelection.Checked, chkDiscountStatus.Checked,
                                out errMsg);
            }
            else
            {
                i = objCommon.UpdateAdvstDiscount(Convert.ToInt32(hndDiscountId.Value), Convert.ToInt16(AdvertismentType.Combo), Convert.ToInt32(Request["ProductMasterId"]), Convert.ToInt16(txtProDiscount.Text), Convert.ToInt16(txtProductDuration.Text),
                               Convert.ToDateTime(txtValidityStartTime.Text), Convert.ToDateTime(txtProEndTime.Text), chkDefaultSelection.Checked, chkDiscountStatus.Checked,
                               out errMsg);
            }
            lblInsertUpdate.Text = errMsg;
            lblInsertUpdate.CssClass = "success";
            if (i > 0)
            {
                GetProductDurationAndDiscount(Convert.ToInt32(Request["ProductMasterId"]));
                hndDiscountId.Value = "";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweqeewqe", "OpenPoup('ProductDuration','650px','lnkAddProductDiscount');return false;", true);
            }
           
        }

        

        
    }
}