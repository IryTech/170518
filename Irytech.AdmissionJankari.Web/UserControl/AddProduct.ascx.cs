using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class AddProduct : System.Web.UI.UserControl
    {
        private readonly Common _objCommon = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindProductList();
        }

        private void BindProductList()
        {
            try
            {
                var objDataset = new Common().GetProductForCollege(CollegeId);
                if (objDataset.Tables[0].Rows.Count > 0)
                {
                    var data = objDataset.Tables[0].AsEnumerable().Take(3).CopyToDataTable();
                    dtlProduct.Visible = true;
                    dtlProduct.DataSource = data;
                    dtlProduct.DataBind();
                }
                else
                {
                    dtlProduct.Visible = false;

                }
            }
            catch
                (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo =
                    "Error in Executing  BindProduct in Account/AddProduct.ascx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void dtlProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    var objCommon = new Common();
                    var rptProductFeatures = (Repeater) e.Item.FindControl("rptFeatures");
                    var hdnProductId = (HiddenField) (e.Item.FindControl("hdnProductMasterId"));
                    var rptProductDiscount = (Repeater) e.Item.FindControl("rptProductDiscount");

                    if (rptProductFeatures != null)
                    {


                        var dt1 =
                            objCommon.GetProductFeatures(
                                Convert.ToInt32(hdnProductId.Value));
                        if (dt1.Tables.Count > 0)
                        {
                            if (dt1.Tables[0].Rows.Count > 0)
                            {

                                rptProductFeatures.Visible = true;
                                rptProductFeatures.DataSource = dt1.Tables[0];
                                rptProductFeatures.DataBind();

                            }
                            else
                            {
                                rptProductFeatures.Visible = false;

                            }
                        }
                        else
                        {
                            rptProductFeatures.Visible = false;

                        }


                    }
                    if (rptProductDiscount != null)
                    {


                        var dt =
                            objCommon.GetProductDiscount(
                                Convert.ToInt32(hdnProductId.Value));
                        if (dt.Tables.Count > 0)
                        {
                            if (dt.Tables[0].Rows.Count > 0)
                            {
                                var liItem = new StringBuilder();
                               var defaultSelected =
                                    dt.Tables[0].AsEnumerable().Where(result => result.Field<bool>("AjDefaultSelection")).CopyToDataTable();
                                liItem.Append("<div class='plan-droplist-selected'><span>" +
                                              defaultSelected.Rows[0]["AjMonthValue"] + "</span> <span class='sale'>" +
                                              Common.ConvertRupee(Convert.ToString(defaultSelected.Rows[0]["AjProductPayAmount"])) +
                                              "</span> <span class='sale lastChild'>On Sale</span></div> <div class='plan-droplist-selectbtn'> <span></span></div><ul> ");
                                var rowCount=0;
                                for (rowCount=0; rowCount <= dt.Tables[0].Rows.Count - 1; rowCount++)
                                {
                                    var productAmount =
                                        Common.ConvertRupee(
                                            Convert.ToString(dt.Tables[0].Rows[rowCount]["AjProductAmount"]));
                                    var productPayAmount =
                                        Common.ConvertRupee(
                                            Convert.ToString(dt.Tables[0].Rows[rowCount]["AjProductPayAmount"]));

                                    if (string.IsNullOrEmpty(
                                            Convert.ToString(dt.Tables[0].Rows[rowCount]["AjProductDiscount"])))
                                       
                                    {

                                        liItem.Append("<li class='" +
                                                      (Convert.ToBoolean(
                                                          dt.Tables[0].Rows[rowCount]["AjDefaultSelection"]) == true
                                                           ? "selected"
                                                           : "") + "' data-main='" +
                                                      dt.Tables[0].Rows[rowCount]["AjAdvertismentDiscountId"] +
                                                      "|1|1' data-view='" + productAmount +
                                                      "||0'><div data-main='true'><span>" +
                                                      dt.Tables[0].Rows[rowCount]["AjMonthValue"] + "</span><span>Rs " +
                                                      productAmount +
                                                      "/mo</span></div></li>");
                                    }
                                    else
                                    {
                                        liItem.Append("<li data-main='" +
                                                      dt.Tables[0].Rows[rowCount]["AjAdvertismentDiscountId"] +
                                                      "|1|1' class='" +
                                                      (Convert.ToBoolean(
                                                          dt.Tables[0].Rows[rowCount]["AjDefaultSelection"]) == true
                                                           ? "selected"
                                                           : "") + "' data-view='" +
                                                      productPayAmount +
                                                      "|&lt;strike&gt;" +
                                                      productAmount +
                                                      "&lt;/strike&gt;|" + dt.Tables[0].Rows[rowCount]["AjProductDiscount"] + "'><div data-main='true'><span> " +
                                                      dt.Tables[0].Rows[rowCount]["AjMonthValue"] +
                                                      "</span><span class='sale'>" +
                                                      productPayAmount +
                                                      "/mo</span><span class='sale lastChild'>On Sale</span></div><div><span>&nbsp;</span><span><strike>Rs. " +
                                                      productAmount +
                                                      "</strike>/mo</span><span class='sale lastChild'>Save " +
                                                      dt.Tables[0].Rows[rowCount]["AjProductDiscount"] +
                                                      "%</span></div></li>");

                                    }

                                }
                                if (rowCount == dt.Tables[0].Rows.Count)
                                {
                                    liItem.Append("</ul>");

                                }
                                var lst = new List<TestObject> {new TestObject() {Text = liItem.ToString()}};
                                rptProductDiscount.Visible = true;
                                rptProductDiscount.DataSource = lst;
                                rptProductDiscount.DataBind();

                            }
                            else
                            {
                                rptProductDiscount.Visible = false;

                            }
                        }
                        else
                        {
                            rptProductDiscount.Visible = false;

                        }
                    }
                }
            }
            catch
                (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo =
                    "Error in Executing  dtlProduct_ItemDataBound in Account/AddProduct.ascx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

      
        public int CollegeId
        {
            set { hdnCollegeId.Value = value.ToString(); }
            get { return Convert.ToInt32(hdnCollegeId.Value); }
        }

        public class TestObject
        {
            public string Text { get; set; }
        }

        protected void lnkBannerMroeProduct_Click(object sender, EventArgs e)
        {
            divMoreProduct.Visible = true;
            BannerProd.BindBannerProduct(CollegeId);
            UcTextProduct.BindTextAdsProduct(CollegeId);
            lnkBannerMroeProduct.Visible = false;
            lnkHideProduct.Visible = true;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp",
                                                     "<script type='text/javascript'>SetAdvertiseBanner();</script>",
                                                     false);

        }

        protected void lnkHideProduct_Click(object sender, EventArgs e)
        {
            divMoreProduct.Visible = false;
            lnkBannerMroeProduct.Visible = true;
            lnkHideProduct.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp",
                                                "<script type='text/javascript'>SetAdvertiseBanner();</script>",
                                                false);

        }

    }
}